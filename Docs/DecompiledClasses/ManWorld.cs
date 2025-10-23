#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ManWorld : Singleton.Manager<ManWorld>, Mode.IManagerModeEvents, ISceneToWorldGetter
{
	public struct CachedBiomeBlendWeights
	{
		private bool hasData;

		private BiomeMap.MapCell currentMapCell;

		private float _weightedFriction;

		private Biome m_SetPieceBiome;

		private float m_SetPieceBiomeWeight;

		public bool Valid => hasData;

		public int NumWeights
		{
			get
			{
				if (!hasData)
				{
					return 0;
				}
				return 4;
			}
		}

		public float WeightedFriction
		{
			get
			{
				if (_weightedFriction == -1f)
				{
					CalculateWeightedFriction();
				}
				return _weightedFriction;
			}
		}

		public Biome SetPieceBiome => m_SetPieceBiome;

		public float SetPieceBiomeWeight => m_SetPieceBiomeWeight;

		public Biome Biome(int i)
		{
			return LookupBiomeInCurrentMap(i);
		}

		public float Weight(int i)
		{
			return currentMapCell.Weight(i);
		}

		public CachedBiomeBlendWeights(BiomeMap.MapCell mapCell)
		{
			hasData = true;
			currentMapCell = mapCell;
			_weightedFriction = -1f;
			m_SetPieceBiome = null;
			m_SetPieceBiomeWeight = 0f;
		}

		public CachedBiomeBlendWeights(BiomeMap.MapCell mapCell, Vector3 scenePos)
			: this(mapCell)
		{
			WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in scenePos);
			TerrainSetPiece setPiece = worldTile.BiomeMapData.setPiece;
			if (setPiece != null && setPiece.SuppressedSceneryBiome != null)
			{
				m_SetPieceBiome = setPiece.SuppressedSceneryBiome;
				m_SetPieceBiomeWeight = setPiece.IsScenePosInside(worldTile, scenePos, worldTile.BiomeMapData);
			}
		}

		public static CachedBiomeBlendWeights CreateInvalid()
		{
			return new CachedBiomeBlendWeights
			{
				hasData = false,
				_weightedFriction = -1f,
				m_SetPieceBiome = null,
				m_SetPieceBiomeWeight = 0f
			};
		}

		private Biome LookupBiomeInCurrentMap(int i)
		{
			if (!hasData || !Singleton.Manager<ManWorld>.inst.CurrentBiomeMap)
			{
				return null;
			}
			return Singleton.Manager<ManWorld>.inst.CurrentBiomeMap.LookupBiome(currentMapCell.Index(i));
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		private void CalculateWeightedFriction()
		{
			_weightedFriction = 0f;
			if (!hasData)
			{
				return;
			}
			for (int i = 0; i < 4; i++)
			{
				float num = currentMapCell.Weight(i);
				if (num != 0f)
				{
					_weightedFriction += Singleton.Manager<ManWorld>.inst.CurrentBiomeMap.LookupBiome(currentMapCell.Index(i)).SurfaceFriction * num;
					continue;
				}
				break;
			}
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	private class SaveData
	{
		public Vendors.SaveData m_VendorData;

		public IntVector3 m_FloatingOriginOffset;

		public List<SavedSetPiece> m_ScenerySetPieces = new List<SavedSetPiece>();
	}

	public struct TerrainSetPiecePlacement
	{
		public WorldPosition m_WorldPosition;

		public TerrainSetPiece m_SetPiece;

		public IntVector2 m_CellPosition;

		public float m_BaseHeight;

		public int m_Rotation;
	}

	public struct SavedSetPiece
	{
		public string m_Name;

		public WorldPosition m_WorldPosition;

		public float m_BaseHeight;

		public int m_Rotation;

		public SavedSetPiece(TerrainSetPiecePlacement from)
		{
			m_Name = from.m_SetPiece.name;
			m_WorldPosition = from.m_WorldPosition;
			m_BaseHeight = from.m_BaseHeight;
			m_Rotation = from.m_Rotation;
			d.Assert(m_BaseHeight >= 0f && m_BaseHeight <= 1f, $"Invalid height for SavedSetPiece {m_Name}: {m_BaseHeight}");
		}

		public void Write(BinaryWriter stream)
		{
			long position = stream.BaseStream.Position;
			string name = m_Name;
			int num = Singleton.Manager<ManWorld>.inst.m_AllSetPieces.FindIndex((TerrainSetPiece i) => i.name == name);
			if (num < 0)
			{
				d.LogError("Unknown name for SavedSetPiece: " + m_Name + " not found in ManWorld.m_AllSetPieces");
				num = 0;
			}
			stream.WritePackedUInt32((uint)num);
			stream.WritePackedInt32(m_WorldPosition.TileCoord.x);
			stream.WritePackedInt32(m_WorldPosition.TileCoord.y);
			stream.WritePackedInt32(Mathf.RoundToInt(m_WorldPosition.TileRelativePos.x));
			stream.WritePackedInt32(Mathf.RoundToInt(m_WorldPosition.TileRelativePos.y));
			stream.WritePackedInt32(Mathf.RoundToInt(m_WorldPosition.TileRelativePos.z));
			stream.Write((ushort)(m_BaseHeight * 65535f));
			stream.Write((byte)(m_Rotation / 90));
			d.Log($"[SavedSetPiece] Write: {stream.BaseStream.Position - position} bytes used for {m_Name} (nameIndex={num} TileCoord={m_WorldPosition.TileCoord} rel={m_WorldPosition.TileRelativePos} h={m_BaseHeight} r={m_Rotation})");
		}

		public SavedSetPiece(BinaryReader stream)
		{
			long position = stream.BaseStream.Position;
			int num = (int)stream.ReadPackedUInt32();
			if (num >= Singleton.Manager<ManWorld>.inst.m_AllSetPieces.Count)
			{
				d.LogError($"Unknown name index for SavedSetPiece: {num} not valid for ManWorld.m_AllSetPieces");
				num = 0;
			}
			m_Name = Singleton.Manager<ManWorld>.inst.m_AllSetPieces[num].name;
			int x = stream.ReadPackedInt32();
			int y = stream.ReadPackedInt32();
			float x2 = stream.ReadPackedInt32();
			float y2 = stream.ReadPackedInt32();
			float z = stream.ReadPackedInt32();
			m_WorldPosition = new WorldPosition(new IntVector2(x, y), new Vector3(x2, y2, z));
			m_BaseHeight = (float)(int)stream.ReadUInt16() / 65535f;
			m_Rotation = stream.ReadByte() * 90;
			d.Log($"[SavedSetPiece] Read: {stream.BaseStream.Position - position} bytes read for {m_Name} (nameIndex={num} TileCoord={m_WorldPosition.TileCoord} rel={m_WorldPosition.TileRelativePos} h={m_BaseHeight} r={m_Rotation})");
		}
	}

	[SerializeField]
	private ReflectionProbe m_TileReflectionProbePrefab;

	[SerializeField]
	public Material m_TerrainMaterial;

	[SerializeField]
	private int m_CellsPerTileEdge = 64;

	[SerializeField]
	private int m_CellsPerSceneryPatchEdge = 8;

	[SerializeField]
	private int m_CellsPerSceneryMergePatchEdge = 32;

	[SerializeField]
	private float m_CellScale = 6f;

	[SerializeField]
	private float m_SceneryPositionJitter = 0.25f;

	[SerializeField]
	private PhysicMaterial m_PhysicMaterial;

	[SerializeField]
	private bool DBG_RegenAll;

	[SerializeField]
	[Tooltip("How far into a tile you need to go before it becomes the centre of the 3x3 grid of loaded tiles")]
	private float m_FocussedTileExtraWeighting = 0.1f;

	[SerializeField]
	[Tooltip("Bonus given to already created tiles when prioritising neighbours")]
	private float m_CreatedTileExtraWeighting = 0.07f;

	[Tooltip("Bonus given to already populated tiles when prioritising neighbours")]
	[SerializeField]
	private float m_PopulatedTileExtraWeighting = 0.1f;

	[SerializeField]
	[Tooltip("Set to required radius in order to test the curvature checking code")]
	private float m_DebugCurvatureRadius;

	[SerializeField]
	private bool m_ShowDebugInfo;

	[SerializeField]
	public int TileInfoStepSize = 20;

	[SerializeField]
	public Transform m_BatchSceneryPrefab;

	[SerializeField]
	public Transform m_InstancedSceneryPrefab;

	[SerializeField]
	public NetWorldTile m_NetworkedTilePrefab;

	[SerializeField]
	public bool CheckerboardTileHeight;

	[SerializeField]
	[Tooltip("A curve to set the gas density (kg/m^3) of the air at different altitudes. The time dimension describes altitude in game units (m). In real life the density of air around sea level is 1.22 kg/m^3")]
	protected AnimationCurve m_UniversalAtmosphereDensityCurve;

	[SerializeField]
	[Header("Terrain Set Pieces")]
	private List<TerrainSetPiece> m_AllSetPieces;

	[SerializeField]
	private Transform m_DebugStationPrefab;

	public const int kMaxTileCoord = 2147383647;

	public EventNoParams TileManagerInitialisedEvent;

	private BiomeMap.MapCell m_SingleCellBlendLookupCache;

	private Vector2 m_TerrainGenerationOffset;

	private const float k_GroundProjectSceneryFudge = 250f;

	private int k_GroundProjectSceneryMask;

	private float[] m_QueryHeightCache = new float[5];

	private Dictionary<int, SceneryBlocker> m_DynamicSceneryBlockers = new Dictionary<int, SceneryBlocker>();

	private bool m_AutoUpdateFocalPoint = true;

	private List<TerrainSetPiecePlacement> m_SetPiecesPlacement = new List<TerrainSetPiecePlacement>();

	private List<TerrainSetPiece> m_RotatedSetPieces = new List<TerrainSetPiece>();

	public static bool s_DebugTradingStations;

	private Transform m_DebugStationsContainer;

	private bool m_CalcWindTurbulence;

	private float[] m_WindTurbulence;

	public float FocussedTileExtraWeighting => m_FocussedTileExtraWeighting;

	public float CreatedTileExtraWeighting => m_CreatedTileExtraWeighting;

	public float PopulatedTileExtraWeighting => m_PopulatedTileExtraWeighting;

	public PhysicMaterial PhysicMaterial => m_PhysicMaterial;

	public WorldPosition FocalPoint { get; private set; }

	public int CellsPerTileEdge => m_CellsPerTileEdge;

	public int CellsPerSceneryPatchEdge => m_CellsPerSceneryPatchEdge;

	public int CellsPerSceneryMergePatchEdge => m_CellsPerSceneryMergePatchEdge;

	public float CellScale => m_CellScale;

	public float SceneryPositionJitter => m_SceneryPositionJitter;

	public float TileSize => m_CellScale * (float)m_CellsPerTileEdge;

	public float MaximumWorldDistanceFromOrigin => 2.1473837E+09f * TileSize;

	public BiomeMap CurrentBiomeMap { get; private set; }

	public CachedBiomeBlendWeights CurrentBiomeWeights { get; private set; }

	public string SeedString { get; set; }

	public string BiomeChoice { get; set; }

	public int SeedValue
	{
		get
		{
			if (!SeedString.NullOrEmpty())
			{
				return SeedString.GetDotNet3HashCode();
			}
			return 0;
		}
	}

	public Landmarks LandmarkSpawner { get; private set; }

	public VendorSpawner VendorSpawner { get; private set; }

	public StuntRampSpawner StuntRampSpawner { get; private set; }

	public TileManager TileManager { get; private set; }

	public ReflectionProbeManager ReflectionProbeManager { get; private set; }

	public Vendors Vendors { get; } = new Vendors();

	public IntVector3 FloatingOrigin { get; private set; }

	public IntVector2 FloatingOriginTile { get; private set; }

	public IntVector3 GameWorldToScene => -FloatingOrigin;

	public IntVector3 SceneToGameWorld => FloatingOrigin;

	public Vector2 TerrainGenerationOffset => m_TerrainGenerationOffset;

	public AnimationCurve UniversalAtmosphereDensityCurve => m_UniversalAtmosphereDensityCurve;

	public float[] WindTurbulence
	{
		get
		{
			m_CalcWindTurbulence = true;
			return m_WindTurbulence;
		}
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		Singleton.Manager<ManWorldTreadmill>.inst.OnAfterWorldOriginMoved.Subscribe(OnAfterWorldOriginMoved);
		Vendors.Init();
		ClearSetPieces();
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManWorld, out var saveData))
		{
			Vendors.Load(saveData.m_VendorData);
			SetFloatingOrigin(saveData.m_FloatingOriginOffset);
			foreach (SavedSetPiece scenerySetPiece in saveData.m_ScenerySetPieces)
			{
				AddTerrainSetPieceFromSaveData(scenerySetPiece);
			}
		}
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.AddSetPieceTerrain, OnAddSetPieceTerrainMessage);
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		Vendors.Save(ref saveData.m_VendorData);
		saveData.m_FloatingOriginOffset = FloatingOrigin;
		saveData.m_ScenerySetPieces.Clear();
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			saveData.m_ScenerySetPieces.Add(new SavedSetPiece(item));
		}
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManWorld, saveData);
	}

	public void ModeExit()
	{
		Singleton.Manager<ManWorldTreadmill>.inst.OnAfterWorldOriginMoved.Unsubscribe(OnAfterWorldOriginMoved);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.AddSetPieceTerrain, OnAddSetPieceTerrainMessage);
		ClearSetPieces();
		Vendors.Deinit();
		DebugHideClosestTradingStations();
	}

	public static void ToggleDebugTradingStations()
	{
		s_DebugTradingStations = !s_DebugTradingStations;
		if ((bool)Singleton.Manager<ManWorld>.inst)
		{
			if (s_DebugTradingStations)
			{
				Singleton.Manager<ManWorld>.inst.DebugShowClosestTradingStations();
			}
			else
			{
				Singleton.Manager<ManWorld>.inst.DebugHideClosestTradingStations();
			}
		}
	}

	private void ClearSetPieces()
	{
		m_SetPiecesPlacement.Clear();
		foreach (TerrainSetPiece rotatedSetPiece in m_RotatedSetPieces)
		{
			UnityEngine.Object.DestroyImmediate(rotatedSetPiece);
		}
		m_RotatedSetPieces.Clear();
	}

	private void ClearSpawners()
	{
		LandmarkSpawner?.Reset(enable: false);
		VendorSpawner?.Reset(enable: false);
		VendorSpawner?.SetPlacementBlocker(null, 0f);
		StuntRampSpawner?.Reset(enable: false);
		LandmarkSpawner = null;
		VendorSpawner = null;
		StuntRampSpawner = null;
	}

	private void SetupSpawners()
	{
		if (CurrentBiomeMap != null)
		{
			if ((bool)CurrentBiomeMap.LandmarkSpawner)
			{
				LandmarkSpawner = CurrentBiomeMap.LandmarkSpawner;
				LandmarkSpawner.Reset(enable: true);
			}
			if ((bool)CurrentBiomeMap.VendorSpawner)
			{
				VendorSpawner = CurrentBiomeMap.VendorSpawner;
				VendorSpawner.Reset(enable: true);
				VendorSpawner.SetPlacementBlocker(LandmarkSpawner, CurrentBiomeMap.VendorLandmarkMinSeparation);
			}
			if ((bool)CurrentBiomeMap.StuntRampSpawner)
			{
				StuntRampSpawner = CurrentBiomeMap.StuntRampSpawner;
				StuntRampSpawner.Reset(enable: true);
			}
		}
	}

	public void Reset(BiomeMap map, bool clearFixedTiles = true)
	{
		ClearSpawners();
		ReflectionProbeManager.Reset();
		TileManager.Reset(clearFixedTiles);
		CurrentBiomeMap = map;
		if ((bool)map)
		{
			map.ApplyQualitySettings();
		}
		SetupSpawners();
		if ((bool)CurrentBiomeMap)
		{
			if (map.WorldGenVersionData < WorldGenVersionData.kLegacy_14775_BiomeDB)
			{
				map.InitBiomeSwapIndex(SeedValue);
			}
			CurrentBiomeWeights = GetBiomeWeightsAtScenePosition(Singleton.playerTank ? Singleton.playerPos : Singleton.cameraTrans.position);
			Singleton.Manager<ManTimeOfDay>.inst.BlendImmediately();
		}
		else
		{
			CurrentBiomeWeights = CachedBiomeBlendWeights.CreateInvalid();
		}
		m_AutoUpdateFocalPoint = true;
	}

	public float GetWeightedFrictionAtScenePosition(Vector3 scenePos)
	{
		WorldTile worldTile = TileManager.LookupTile(in scenePos);
		CachedBiomeBlendWeights biomeWeightsAtScenePosition = GetBiomeWeightsAtScenePosition(scenePos);
		float num = biomeWeightsAtScenePosition.WeightedFriction;
		if (worldTile != null && (bool)worldTile.BiomeMapData.setPiece)
		{
			Vector2 tileRelativeCellPos = (scenePos - TileManager.CalcTileOriginScene(worldTile.Coord)).ToVector2XZ() / CellScale;
			num = worldTile.BiomeMapData.setPiece.GetFrictionBlended(worldTile.BiomeMapData, tileRelativeCellPos, num);
		}
		if (biomeWeightsAtScenePosition.SetPieceBiomeWeight > 0f)
		{
			num += (biomeWeightsAtScenePosition.SetPieceBiome.SurfaceFriction - num) * biomeWeightsAtScenePosition.SetPieceBiomeWeight;
		}
		return num;
	}

	public CachedBiomeBlendWeights GetBiomeWeightsAtScenePosition(Vector3 scenePos)
	{
		if (!CurrentBiomeMap)
		{
			return CachedBiomeBlendWeights.CreateInvalid();
		}
		WorldTile worldTile = TileManager.LookupTile(in scenePos);
		if (worldTile != null && worldTile.HasTerrain)
		{
			return new CachedBiomeBlendWeights(TileManager.GetMapCell(worldTile, in scenePos), scenePos);
		}
		Vector3 coord = scenePos + SceneToGameWorld;
		CurrentBiomeMap.GenerateBiomeBlendsAtPoint(ref m_SingleCellBlendLookupCache, coord.ToVector2XZ(), SeedValue, CellsPerTileEdge, (int)CellScale);
		return new CachedBiomeBlendWeights(m_SingleCellBlendLookupCache);
	}

	public Vector3 ProjectToGround(Vector3 scenePos, bool hitScenery = false)
	{
		TryProjectToGround(ref scenePos, hitScenery);
		return scenePos;
	}

	public bool TryProjectToGround(ref Vector3 scenePos, bool hitScenery = false)
	{
		Vector3 outNormal;
		return TryProjectToGround(ref scenePos, out outNormal, hitScenery);
	}

	public bool TryProjectToGround(ref Vector3 scenePos, out Vector3 outNormal, bool hitScenery = false)
	{
		outNormal = Vector3.up;
		if (!CurrentBiomeMap)
		{
			return false;
		}
		scenePos.y = TileManager.GetTerrainHeightAtPosition(scenePos, out var onTile);
		if (!onTile)
		{
			return false;
		}
		int layerMask = (hitScenery ? k_GroundProjectSceneryMask : Globals.inst.layerTerrain.mask);
		if (Physics.Raycast(scenePos.SetY(scenePos.y + 250f), -Vector3.up, out var hitInfo, 251f, layerMask, QueryTriggerInteraction.Ignore))
		{
			scenePos.y = scenePos.y + 250f - hitInfo.distance;
			outNormal = hitInfo.normal;
		}
		return true;
	}

	public bool RaycastGround(Ray ray, out RaycastHit hit, float distance, int mask = 0)
	{
		WorldTile worldTile = TileManager.LookupTile(ray.origin);
		if (worldTile == null || !worldTile.IsCreated)
		{
			hit = default(RaycastHit);
			return false;
		}
		if (mask != 0)
		{
			return Physics.Raycast(ray, out hit, distance, mask, QueryTriggerInteraction.Ignore);
		}
		return worldTile.Terrain.GetComponent<Collider>().Raycast(ray, out hit, distance);
	}

	public Vector3 GetTerrainNormal(Vector3 scenePos)
	{
		if (!GetTerrainNormal(scenePos, out var outNormal))
		{
			DebugUtil.AssertRelease(condition: false, $"ERROR: ManWorld.GetTerrainNormal at {scenePos} but tile isnt loaded");
		}
		return outNormal;
	}

	public bool GetTerrainNormal(Vector3 scenePos, out Vector3 outNormal)
	{
		WorldTile worldTile = TileManager.LookupTile(in scenePos);
		if (worldTile == null || !worldTile.IsCreated)
		{
			outNormal = Vector3.up;
			return false;
		}
		outNormal = worldTile.GetTerrainNormal(scenePos);
		return true;
	}

	public bool GetTerrainHeight(Vector3 scenePos, out float outHeight)
	{
		outHeight = TileManager.GetTerrainHeightAtPosition(scenePos, out var _);
		return true;
	}

	public bool TestCurvatureAndLogResult(Vector3 scenePos, float radius, string logContext)
	{
		float flatTerrainMaxSlope = Globals.inst.m_FlatTerrainMaxSlope;
		float flatTerrainMinCurve = Globals.inst.m_FlatTerrainMinCurve;
		bool flag2;
		if (QueryCurvature(scenePos, radius, out float slopeInDegrees, out float minCurvatureDegrees, out float largestHeightVarianceBetweenPoints))
		{
			bool num = slopeInDegrees <= flatTerrainMaxSlope;
			bool flag = minCurvatureDegrees >= flatTerrainMinCurve;
			if (num && flag)
			{
				float num2 = Mathf.Tan(Globals.inst.m_FlatTerrainMaxSlope * ((float)Math.PI / 180f)) * radius * 0.5f;
				flag2 = largestHeightVarianceBetweenPoints <= num2;
			}
			else
			{
				flag2 = false;
			}
			d.Log(string.Format("Testing curvature for \"{0}\" at {1} radius {2} was {3}: slope={4:N2} curve={5:N2}, height={6:N2}", logContext, scenePos, radius, flag2 ? "passed" : "failed", slopeInDegrees, minCurvatureDegrees, largestHeightVarianceBetweenPoints));
		}
		else
		{
			flag2 = false;
			d.Log($"Testing curvature for \"{logContext}\" at {scenePos} radius {radius} was failed because normals could not be sampled");
		}
		return flag2;
	}

	public bool QueryCurvature(Vector3 scenePos, float radius, out float slopeInDegrees, out float minCurvatureDegrees, out float largestHeightVarianceBetweenPoints)
	{
		Vector3 averageNormal;
		bool num = QueryCurvature(scenePos, radius, out averageNormal, out minCurvatureDegrees, out largestHeightVarianceBetweenPoints);
		if (num)
		{
			float f = Vector3.Dot(averageNormal, Vector3.up);
			slopeInDegrees = Mathf.Acos(f) * 57.29578f;
			return num;
		}
		slopeInDegrees = float.PositiveInfinity;
		return num;
	}

	public bool QueryCurvature(Vector3 scenePos, float radius, out Vector3 averageNormal, out float minCurvatureDegrees, out float largestHeightVarianceBetweenPoints)
	{
		if (GetTerrainHeight(scenePos, out var outHeight) && GetTerrainHeight(scenePos + Vector3.right * radius, out var outHeight2) && GetTerrainHeight(scenePos - Vector3.right * radius, out var outHeight3) && GetTerrainHeight(scenePos + Vector3.forward * radius, out var outHeight4) && GetTerrainHeight(scenePos - Vector3.forward * radius, out var outHeight5) && GetTerrainNormal(scenePos, out var outNormal) && GetTerrainNormal(scenePos + Vector3.right * radius, out var outNormal2) && GetTerrainNormal(scenePos - Vector3.right * radius, out var outNormal3) && GetTerrainNormal(scenePos + Vector3.forward * radius, out var outNormal4) && GetTerrainNormal(scenePos - Vector3.forward * radius, out var outNormal5))
		{
			averageNormal = (outNormal + outNormal2 + outNormal3 + outNormal4 + outNormal5) * 0.2f;
			float num = Vector3.Angle(outNormal2, outNormal3);
			if (Vector3.Dot(outNormal2 - outNormal3, Vector3.right) < 0f)
			{
				num = 0f - num;
			}
			float num2 = Vector3.Angle(outNormal4, outNormal5);
			if (Vector3.Dot(outNormal4 - outNormal5, Vector3.forward) < 0f)
			{
				num2 = 0f - num2;
			}
			minCurvatureDegrees = Mathf.Min(num, num2);
			largestHeightVarianceBetweenPoints = Mathf.Abs(outHeight - outHeight2);
			largestHeightVarianceBetweenPoints = Mathf.Max(Mathf.Abs(outHeight - outHeight3), largestHeightVarianceBetweenPoints);
			largestHeightVarianceBetweenPoints = Mathf.Max(Mathf.Abs(outHeight - outHeight4), largestHeightVarianceBetweenPoints);
			largestHeightVarianceBetweenPoints = Mathf.Max(Mathf.Abs(outHeight - outHeight5), largestHeightVarianceBetweenPoints);
			return true;
		}
		averageNormal = Vector3.up;
		minCurvatureDegrees = float.PositiveInfinity;
		largestHeightVarianceBetweenPoints = float.PositiveInfinity;
		return false;
	}

	public float QueryHeightVariance(Vector3 scenePos, float radius)
	{
		bool onTile;
		float terrainHeightAtPosition = TileManager.GetTerrainHeightAtPosition(scenePos, out onTile);
		float terrainHeightAtPosition2 = TileManager.GetTerrainHeightAtPosition(scenePos + Vector3.right * radius, out onTile);
		float terrainHeightAtPosition3 = TileManager.GetTerrainHeightAtPosition(scenePos - Vector3.right * radius, out onTile);
		float terrainHeightAtPosition4 = TileManager.GetTerrainHeightAtPosition(scenePos + Vector3.forward * radius, out onTile);
		float terrainHeightAtPosition5 = TileManager.GetTerrainHeightAtPosition(scenePos - Vector3.forward * radius, out onTile);
		m_QueryHeightCache[0] = terrainHeightAtPosition;
		m_QueryHeightCache[1] = terrainHeightAtPosition2;
		m_QueryHeightCache[2] = terrainHeightAtPosition3;
		m_QueryHeightCache[3] = terrainHeightAtPosition4;
		m_QueryHeightCache[4] = terrainHeightAtPosition5;
		float num = float.PositiveInfinity;
		float num2 = float.NegativeInfinity;
		for (int i = 0; i < m_QueryHeightCache.Length; i++)
		{
			num = Mathf.Min(num, m_QueryHeightCache[i]);
			num2 = Mathf.Max(num2, m_QueryHeightCache[i]);
		}
		return num2 - num;
	}

	public bool CheckIsTileAtPositionLoaded(Vector3 scenePos)
	{
		return TileManager.LookupTile(in scenePos)?.IsLoaded ?? false;
	}

	public bool CheckAllTilesAtPositionHaveReachedLoadStep(Vector3 scenePos, float radius, WorldTile.LoadStep minLoadStep = WorldTile.LoadStep.Loaded)
	{
		bool result = true;
		Bounds sceneBounds = new Bounds(scenePos, Vector3.one * radius * 2f);
		TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
		for (int i = min.x; i <= max.x; i++)
		{
			for (int j = min.y; j <= max.y; j++)
			{
				IntVector2 coord = new IntVector2(i, j);
				WorldTile worldTile = TileManager.LookupTile(in coord);
				if (worldTile == null || worldTile.m_LoadStep < minLoadStep)
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	public bool CheckAtLeastOneTileAtPositionHasReachedLoadStep(Vector3 scenePos, float radius, WorldTile.LoadStep minLoadStep = WorldTile.LoadStep.Loaded)
	{
		bool result = false;
		Bounds sceneBounds = new Bounds(scenePos, Vector3.one * radius * 2f);
		TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
		for (int i = min.x; i <= max.x; i++)
		{
			for (int j = min.y; j <= max.y; j++)
			{
				IntVector2 coord = new IntVector2(i, j);
				WorldTile worldTile = TileManager.LookupTile(in coord);
				if (worldTile != null && worldTile.m_LoadStep >= minLoadStep)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public bool TryFindNearestVendorPos(Vector3 worldPos, out Vector3 nearestVendorPosWorld)
	{
		return VendorSpawner.TryFindNearestVendorPos(worldPos, out nearestVendorPosWorld);
	}

	public bool TryFindNearestNeighbouringVendorPos(Vector3 worldPos, out Vector3 secondNearestVendorPosWorld)
	{
		return VendorSpawner.TryFindNearestNeighbouringVendorPos(worldPos, out secondNearestVendorPosWorld);
	}

	public void SetTerrainGenerationTileOffset(IntVector2 terrainGenerationTileOffset)
	{
		m_TerrainGenerationOffset = (Vector2)terrainGenerationTileOffset * TileSize;
	}

	public void DisableSceneryObjectsForTesting()
	{
		TileManager.m_DisableSceneryObjects = true;
		TileManager.TileIterator enumerator = TileManager.IterateTiles().GetEnumerator();
		while (enumerator.MoveNext())
		{
			foreach (Transform item in enumerator.Current.StaticParent)
			{
				item.gameObject.SetActive(value: false);
			}
		}
	}

	public void AddSceneryBlockerToTile(SceneryBlocker blocker, WorldTile tile)
	{
		tile.AddSceneryBlocker(blocker);
	}

	public bool HasDynamicSceneryBlocker(int visibleID)
	{
		return m_DynamicSceneryBlockers.ContainsKey(visibleID);
	}

	public void AddDynamicSceneryBlocker(int visibleID, SceneryBlocker blocker)
	{
		d.Assert(!m_DynamicSceneryBlockers.ContainsKey(visibleID), "Trying to add dynamic spawn blocker twice with visible ID " + visibleID);
		d.Assert(blocker.Mode != SceneryBlocker.BlockMode.Spawn, "Can't dynamically block spawn points");
		m_DynamicSceneryBlockers[visibleID] = blocker;
	}

	public void UpdateDynamicSceneryBlockerBoundsSphere(int visibleID, float radius)
	{
		if (m_DynamicSceneryBlockers.TryGetValue(visibleID, out var value))
		{
			value.UpdateBoundsSphere(radius);
		}
		else
		{
			d.Assert(condition: false, "Could not find scenery blocker to update bounds");
		}
	}

	public void RemoveDynamicSceneryBlocker(int visibleID)
	{
		d.Assert(m_DynamicSceneryBlockers.Remove(visibleID), "Failed to remove dynamic spawn blocker with visible ID " + visibleID);
	}

	public bool CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode mode, Vector3 scenePos, float radius)
	{
		bool flag = false;
		foreach (SceneryBlocker value in m_DynamicSceneryBlockers.Values)
		{
			if (value.IsBlockingPos(mode, scenePos, radius))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			if (radius > 0f)
			{
				Bounds sceneBounds = new Bounds(scenePos, Vector3.one * radius * 2f);
				TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
				TileManager.TileIterator enumerator2 = TileManager.IterateTiles(min, max).GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.CheckIfInsideSceneryBlocker(mode, scenePos, radius))
					{
						flag = true;
						break;
					}
				}
			}
			else
			{
				WorldTile worldTile = TileManager.LookupTile(in scenePos);
				if (worldTile != null)
				{
					flag = worldTile.CheckIfInsideSceneryBlocker(mode, scenePos, radius);
				}
			}
		}
		return flag;
	}

	public Vector3 GetSceneToGameWorld()
	{
		return SceneToGameWorld;
	}

	public void SetFocalPointOverride(WorldPosition newFocalPoint)
	{
		FocalPoint = newFocalPoint;
		m_AutoUpdateFocalPoint = false;
	}

	public bool CircleOverlapsAnySetPieceXZ(Vector3 scenePos, float radius)
	{
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			Vector3 coord = item.m_WorldPosition.ScenePosition - scenePos;
			float num = item.m_SetPiece.GetApproxCellRadius() * CellScale;
			if (coord.ToVector2XZ().magnitude < radius + num)
			{
				return true;
			}
		}
		return false;
	}

	public List<TerrainSetPiecePlacement> GetSetPiecesByDistance(TerrainSetPiece setPiece, WorldPosition near)
	{
		List<TerrainSetPiecePlacement> list = new List<TerrainSetPiecePlacement>();
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			if (item.m_SetPiece == setPiece)
			{
				list.Add(item);
			}
		}
		list.Sort((TerrainSetPiecePlacement i, TerrainSetPiecePlacement j) => (!((i.m_WorldPosition.ScenePosition - near.ScenePosition).sqrMagnitude < (j.m_WorldPosition.ScenePosition - near.ScenePosition).sqrMagnitude)) ? 1 : (-1));
		return list;
	}

	private IntVector2 WorldPosToCellPos(WorldPosition pos)
	{
		return new IntVector2(Mathf.RoundToInt(pos.TileRelativePos.x / CellScale), Mathf.RoundToInt(pos.TileRelativePos.z / CellScale)) + pos.TileCoord * CellsPerTileEdge;
	}

	public void AddTerrainSetPiecesForNetworkedGame(List<SavedSetPiece> placements)
	{
		if (placements == null)
		{
			return;
		}
		foreach (SavedSetPiece placement in placements)
		{
			AddTerrainSetPieceFromSaveData(placement);
		}
	}

	private void AddTerrainSetPieceFromSaveData(SavedSetPiece savedSetPiece)
	{
		TerrainSetPiece terrainSetPiece = null;
		foreach (TerrainSetPiece allSetPiece in m_AllSetPieces)
		{
			if (allSetPiece.name == savedSetPiece.m_Name)
			{
				terrainSetPiece = allSetPiece;
				break;
			}
		}
		if ((bool)terrainSetPiece)
		{
			foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
			{
				if (item.m_WorldPosition.TileCoord == savedSetPiece.m_WorldPosition.TileCoord)
				{
					return;
				}
			}
			WorldPosition worldPosition = savedSetPiece.m_WorldPosition;
			m_SetPiecesPlacement.Add(new TerrainSetPiecePlacement
			{
				m_SetPiece = terrainSetPiece,
				m_BaseHeight = savedSetPiece.m_BaseHeight,
				m_Rotation = savedSetPiece.m_Rotation,
				m_CellPosition = WorldPosToCellPos(worldPosition),
				m_WorldPosition = worldPosition
			});
			terrainSetPiece.LoadTerrainData();
			GetTilesTouchedBySetPiece(terrainSetPiece, worldPosition, savedSetPiece.m_Rotation, out var tileMin, out var tileMax);
			TileManager.RegenerateTilesForSetPiece(tileMin, tileMax);
		}
		else
		{
			d.LogError("Failed to load set piece data for name " + savedSetPiece.m_Name);
		}
	}

	public bool AddTerrainSetPiece(TerrainSetPiece setPiece, WorldPosition pos, int rotation)
	{
		if (rotation < 0)
		{
			rotation = 0;
		}
		bool flag = false;
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			if (item.m_SetPiece == setPiece && (pos.GameWorldPosition - item.m_WorldPosition.GameWorldPosition).ToVector2XZ().magnitude < 12f)
			{
				d.Log($"SetPiece {setPiece.name} already exists at {item.m_WorldPosition} which is close enough to {pos}");
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			if (CanPlaceNewTerrainSetPiece(setPiece, pos, rotation))
			{
				IntVector2 intVector = WorldPosToCellPos(pos);
				TerrainSetPiecePlacement terrainSetPiecePlacement = new TerrainSetPiecePlacement
				{
					m_SetPiece = setPiece,
					m_BaseHeight = setPiece.ChooseHeightOffset(intVector, CurrentBiomeMap),
					m_Rotation = rotation,
					m_CellPosition = intVector,
					m_WorldPosition = pos
				};
				m_SetPiecesPlacement.Add(terrainSetPiecePlacement);
				setPiece.LoadTerrainData();
				flag = true;
				d.Log($"Added SetPiece {setPiece.name} at {pos} (cellPos = {intVector})");
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.AddSetPieceTerrain, new AddSetPieceTerrainMessage
					{
						m_SetPiece = new SavedSetPiece(terrainSetPiecePlacement)
					});
				}
			}
			else
			{
				d.Log($"Cannot spawn SetPiece {setPiece.name} at {pos}");
				flag = false;
			}
		}
		return flag;
	}

	public List<SavedSetPiece> GetSetPiecePlacement()
	{
		List<SavedSetPiece> list = new List<SavedSetPiece>();
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			list.Add(new SavedSetPiece(item));
		}
		return list;
	}

	public void NetSendAllSetPiecesTo(NetPlayer targetPlayer)
	{
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			AddSetPieceTerrainMessage message = new AddSetPieceTerrainMessage
			{
				m_SetPiece = new SavedSetPiece(item)
			};
			Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.AddSetPieceTerrain, message);
		}
	}

	private void OnAddSetPieceTerrainMessage(NetworkMessage msg)
	{
		AddSetPieceTerrainMessage addSetPieceTerrainMessage = msg.ReadMessage<AddSetPieceTerrainMessage>();
		AddTerrainSetPieceFromSaveData(addSetPieceTerrainMessage.m_SetPiece);
	}

	public void GetTilesTouchedBySetPiece(TerrainSetPiece setPiece, WorldPosition pos, int rotation, out IntVector2 tileMin, out IntVector2 tileMax)
	{
		IntVector2 min;
		IntVector2 max;
		if (rotation < 0)
		{
			setPiece.GetCellBoundsRotationInvariant(out min, out max);
		}
		else
		{
			setPiece.GetCellBounds(out min, out max, rotation);
		}
		IntVector2 intVector = new IntVector2(Mathf.RoundToInt(pos.TileRelativePos.x / CellScale), Mathf.RoundToInt(pos.TileRelativePos.z / CellScale)) + pos.TileCoord * CellsPerTileEdge;
		min += intVector;
		max += intVector;
		tileMin = new IntVector2(Mathf.FloorToInt((float)min.x / (float)CellsPerTileEdge), Mathf.FloorToInt((float)min.y / (float)CellsPerTileEdge));
		tileMax = new IntVector2(Mathf.FloorToInt((float)max.x / (float)CellsPerTileEdge), Mathf.FloorToInt((float)max.y / (float)CellsPerTileEdge));
	}

	public bool CanPlaceNewTerrainSetPiece(TerrainSetPiece setPiece)
	{
		if (setPiece.MaxInstancesInWorld >= 0)
		{
			int num = 0;
			foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
			{
				if (item.m_SetPiece == setPiece)
				{
					num++;
					if (num >= setPiece.MaxInstancesInWorld)
					{
						return false;
					}
				}
			}
		}
		return true;
	}

	public bool CanPlaceNewTerrainSetPiece(TerrainSetPiece setPiece, WorldPosition pos, int rotation)
	{
		GetTilesTouchedBySetPiece(setPiece, pos, rotation, out var tileMin, out var tileMax);
		IntVector2 tileCoord = default(IntVector2);
		tileCoord.y = tileMin.y;
		while (tileCoord.y <= tileMax.y)
		{
			tileCoord.x = tileMin.x;
			while (tileCoord.x <= tileMax.x)
			{
				if (!IsTileUsableForNewSetPiece(tileCoord))
				{
					return false;
				}
				tileCoord.x++;
			}
			tileCoord.y++;
		}
		return true;
	}

	public bool IsTileUsableForNewSetPiece(IntVector2 tileCoord)
	{
		if (!TileManager.IsTileUsableForNewSetPiece(tileCoord))
		{
			return false;
		}
		if (Singleton.Manager<ManSaveGame>.inst.GetStoredTile(tileCoord, createNewIfNotFound: false) != null)
		{
			return false;
		}
		if (GetSetPieceDataForTile(tileCoord))
		{
			return false;
		}
		return true;
	}

	private TerrainSetPiece GetRotatedSetPiece(TerrainSetPiece original, int rotation)
	{
		TerrainSetPiece terrainSetPiece = null;
		rotation %= 360;
		if (rotation < 0)
		{
			rotation += 360;
		}
		if (rotation == 0)
		{
			terrainSetPiece = original;
		}
		else
		{
			foreach (TerrainSetPiece rotatedSetPiece in m_RotatedSetPieces)
			{
				if (rotatedSetPiece.OriginalSetPiece == original && rotatedSetPiece.ModifiedRotation == rotation)
				{
					terrainSetPiece = rotatedSetPiece;
					break;
				}
			}
		}
		if (terrainSetPiece == null)
		{
			terrainSetPiece = original.CreateRotatedCopy(rotation);
			m_RotatedSetPieces.Add(terrainSetPiece);
		}
		return terrainSetPiece;
	}

	public bool GetSetPieceDataForTile(IntVector2 tileCoord, bool fillData, out TerrainSetPiece outSetPiece, out float setPieceOffsetHeight, out IntVector2 setPieceOfset)
	{
		outSetPiece = null;
		setPieceOffsetHeight = 0f;
		setPieceOfset = IntVector2.zero;
		bool flag = false;
		foreach (TerrainSetPiecePlacement item in m_SetPiecesPlacement)
		{
			GetTilesTouchedBySetPiece(item.m_SetPiece, item.m_WorldPosition, item.m_Rotation, out var tileMin, out var tileMax);
			if (tileCoord.x < tileMin.x || tileCoord.y < tileMin.y || tileCoord.x > tileMax.x || tileCoord.y > tileMax.y)
			{
				continue;
			}
			if (flag)
			{
				if (outSetPiece != null)
				{
					d.LogError($"Multiple SetPieces attempting to affect tile ({tileCoord.x},{tileCoord.y}) - {outSetPiece.name} and {item.m_SetPiece.name}. This may result in holes in terrain!");
				}
				break;
			}
			if (fillData)
			{
				outSetPiece = GetRotatedSetPiece(item.m_SetPiece, item.m_Rotation);
				setPieceOffsetHeight = item.m_BaseHeight;
				setPieceOfset = tileCoord * CellsPerTileEdge - item.m_CellPosition;
			}
			flag = true;
		}
		return flag;
	}

	public bool GetSetPieceDataForTile(IntVector2 tileCoord, BiomeMap.MapData outData = null)
	{
		TerrainSetPiece outSetPiece;
		float setPieceOffsetHeight;
		IntVector2 setPieceOfset;
		bool setPieceDataForTile = GetSetPieceDataForTile(tileCoord, outData != null, out outSetPiece, out setPieceOffsetHeight, out setPieceOfset);
		if (setPieceDataForTile && outData != null)
		{
			outData.setPiece = outSetPiece;
			outData.setPieceOffset = setPieceOfset;
			outData.setPieceOffsetHeight = setPieceOffsetHeight;
		}
		return setPieceDataForTile;
	}

	public void OnAfterWorldOriginMoved(IntVector3 amountMoved)
	{
		IntVector3 floatingOrigin = FloatingOrigin - amountMoved;
		SetFloatingOrigin(floatingOrigin);
		TileManager.UpdatePhysicsBounds();
		if (s_DebugTradingStations)
		{
			DebugShowClosestTradingStations();
		}
	}

	private void OnModeCleanup(Mode modeToCleanup)
	{
		SetFloatingOrigin(IntVector3.zero);
		m_TerrainGenerationOffset = Vector2.zero;
		m_AutoUpdateFocalPoint = true;
	}

	private void SetFloatingOrigin(IntVector3 floatingOrigin)
	{
		FloatingOrigin = floatingOrigin;
		float tileSize = TileSize;
		FloatingOriginTile = new IntVector2(FloatingOrigin.x / (int)tileSize, FloatingOrigin.z / (int)tileSize);
	}

	private void Start()
	{
		CurrentBiomeWeights = CachedBiomeBlendWeights.CreateInvalid();
		m_SingleCellBlendLookupCache = default(BiomeMap.MapCell);
		TileManager = new TileManager();
		ReflectionProbeManager = new ReflectionProbeManager();
		ReflectionProbeManager.Init(m_TileReflectionProbePrefab);
		TileManager.Init();
		TileManagerInitialisedEvent.Send();
		k_GroundProjectSceneryMask = Globals.inst.layerScenery.mask | Globals.inst.layerTerrain.mask;
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
		DBG_RegenAll = false;
	}

	private void OnDestroy()
	{
		TileManager.Terminate();
		if (Singleton.Manager<DebugUtil>.inst.m_Settings.m_LogPoolBufferSizes)
		{
			BiomeMap.MapData.LogBufferPoolSizes();
			Singleton.Manager<DebugUtil>.inst.m_Settings.m_LogPoolBufferSizes = false;
		}
	}

	private void Update()
	{
		if (m_AutoUpdateFocalPoint)
		{
			if (Singleton.playerTank == null || Singleton.Manager<CameraManager>.inst.IsCurrent<FirstPersonFlyCam>())
			{
				FocalPoint = WorldPosition.FromScenePosition(Singleton.cameraTrans.position);
			}
			else
			{
				FocalPoint = WorldPosition.FromScenePosition(Singleton.playerPos);
			}
		}
		TileManager.Update();
		ReflectionProbeManager.Update();
		if ((bool)CurrentBiomeMap)
		{
			CurrentBiomeWeights = GetBiomeWeightsAtScenePosition(Singleton.playerTank ? Singleton.playerPos : Singleton.cameraTrans.position);
		}
		else
		{
			CurrentBiomeWeights = CachedBiomeBlendWeights.CreateInvalid();
		}
		ParticleRecycler.StaticUpdate();
		UpdateWindTurbulence();
	}

	private void UpdateWindTurbulence()
	{
		if (!m_CalcWindTurbulence)
		{
			return;
		}
		if (m_WindTurbulence == null)
		{
			m_WindTurbulence = new float[16];
		}
		float num = 4f / (float)m_WindTurbulence.Length;
		float num2 = 4f * Time.timeSinceLevelLoad;
		int num3 = 0;
		while (num3 < m_WindTurbulence.Length)
		{
			float num4 = 4f;
			float num5 = 0f;
			float num6 = 0f;
			while (num4 >= 1f)
			{
				num5 += Mathf.PerlinNoise(num2 / num4, 0.5f) * num4;
				num6 += num4;
				num4 *= 0.5f;
			}
			m_WindTurbulence[num3] = num5 / num6;
			num3++;
			num2 += num;
		}
	}

	private void DebugShowClosestTradingStations()
	{
		DebugHideClosestTradingStations();
		if (m_DebugStationsContainer == null)
		{
			m_DebugStationsContainer = new GameObject("Debug Trading Stations").transform;
		}
		if (m_DebugStationPrefab != null)
		{
			if (!(VendorSpawner != null))
			{
				return;
			}
			List<Vector3> list = new List<Vector3>();
			VendorSpawner.ListNearbyVendors(FocalPoint.GameWorldPosition, 1, list);
			{
				foreach (Vector3 item in list)
				{
					Transform obj = UnityEngine.Object.Instantiate(m_DebugStationPrefab, m_DebugStationsContainer, worldPositionStays: false);
					obj.name = $"Station at {item}";
					obj.position = item + GameWorldToScene;
				}
				return;
			}
		}
		d.LogError("Cannot spawn debug trading stations, as no prefab given");
	}

	private void DebugHideClosestTradingStations()
	{
		if (m_DebugStationsContainer != null)
		{
			while (m_DebugStationsContainer.childCount > 0)
			{
				Transform child = m_DebugStationsContainer.GetChild(0);
				child.SetParent(null, worldPositionStays: false);
				UnityEngine.Object.Destroy(child.gameObject);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (!Application.isPlaying || TileManager == null || (!base.gameObject.EditorSelectedSingle() && !m_ShowDebugInfo))
		{
			return;
		}
		TileManager.TileIterator enumerator = TileManager.IterateTiles(WorldTile.State.Empty).GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.DrawGizmos(TileSize);
		}
		TileManager.DrawGizmos();
		Gizmos.color = Color.cyan.SetAlpha(0.5f);
		foreach (SceneryBlocker value in m_DynamicSceneryBlockers.Values)
		{
			value.DrawGizmos();
		}
	}
}
