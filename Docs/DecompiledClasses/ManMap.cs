#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Newtonsoft.Json;
using Unity.Burst;
using Unity.Collections;
using Unity.IL2CPP.CompilerServices;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ManMap : Singleton.Manager<ManMap>, Mode.IManagerModeEvents
{
	public struct TileData
	{
		public IntVector2 coord;

		public byte[] maskData;
	}

	private struct SaveData
	{
		[JsonProperty]
		private List<TileData> tileData;

		[JsonProperty]
		private List<int> exploredTSIDs;

		[JsonProperty]
		private List<Vector3> navigationWaypointPositions;

		[JsonProperty]
		private List<TileData> serverTileData;

		[JsonProperty]
		private List<IntVector2> locallyTouchedTileCoords;

		public bool ConstructFrom(ManMap map)
		{
			tileData = new List<TileData>(map.m_TileImages.Count);
			foreach (KeyValuePair<Vector2Int, Texture2D> tileImage in map.m_TileImages)
			{
				Vector2Int key = tileImage.Key;
				tileData.Add(new TileData
				{
					coord = key,
					maskData = map.m_LocalPlayerState.GetCompressedMask(key)
				});
			}
			exploredTSIDs = new List<int>(map.m_ExploredTradingStations.Count);
			foreach (TrackedVisible exploredTradingStation in map.m_ExploredTradingStations)
			{
				exploredTSIDs.Add(exploredTradingStation.ID);
			}
			if (map.m_NavigationWaypoints.Count > 0)
			{
				navigationWaypointPositions = new List<Vector3>(map.m_NavigationWaypoints.Count);
				foreach (TrackedVisible navigationWaypoint in map.m_NavigationWaypoints)
				{
					navigationWaypointPositions.Add(navigationWaypoint.GetWorldPosition().GameWorldPosition);
				}
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && ManNetwork.IsHost)
			{
				serverTileData = new List<TileData>(map.m_MPServerMapData.Count);
				foreach (Vector2Int key2 in map.m_MPServerMapData.Keys)
				{
					serverTileData.Add(new TileData
					{
						coord = key2,
						maskData = map.m_ServerState.GetCompressedMask(key2)
					});
				}
				locallyTouchedTileCoords = new List<IntVector2>(map.m_LocallyTouchedTileCoords.Count);
				foreach (Vector2Int locallyTouchedTileCoord in map.m_LocallyTouchedTileCoords)
				{
					locallyTouchedTileCoords.Add(locallyTouchedTileCoord);
				}
			}
			return true;
		}

		public void ApplyTo(ManMap map)
		{
			map.ClearMapTileTextures();
			map.m_TileImagesCreated.Clear();
			map.m_TileHeights.Clear();
			map.m_LocalPlayerState.Clear();
			map.m_ExploredTradingStations.Clear();
			if (tileData != null)
			{
				foreach (TileData tileDatum in tileData)
				{
					map.m_TileImages[tileDatum.coord] = map.m_TilePlaceholderDuringLoad;
					map.RequestAsyncTileRender(tileDatum.coord);
					Texture2D texture2D = map.NewMaskTexture();
					byte[] uncompressedData = GetUncompressedData(tileDatum.maskData, createCopy: false);
					texture2D.LoadRawTextureData(uncompressedData);
					texture2D.Apply();
					map.m_MaskImages[tileDatum.coord] = texture2D;
					map.m_LocalPlayerState.SetCompressedMask(tileDatum.coord, tileDatum.maskData);
				}
			}
			if (exploredTSIDs != null)
			{
				foreach (int exploredTSID in exploredTSIDs)
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(exploredTSID);
					if (trackedVisible != null)
					{
						map.m_ExploredTradingStations.Add(trackedVisible);
					}
					else
					{
						d.LogError($"Failed to find tv {exploredTSID}");
					}
				}
			}
			if (navigationWaypointPositions != null && ManNetwork.IsHostOrWillBe)
			{
				map.RemoveAllNavigationWaypoints();
				foreach (Vector3 navigationWaypointPosition in navigationWaypointPositions)
				{
					map.AddNavigationWaypointAtPos(navigationWaypointPosition);
				}
			}
			if (serverTileData != null && ManNetwork.IsHostOrWillBe)
			{
				map.m_MPServerMapData.Clear();
				map.m_ServerState.Clear();
				foreach (TileData serverTileDatum in serverTileData)
				{
					map.m_MPServerMapData[serverTileDatum.coord] = new TileData
					{
						coord = serverTileDatum.coord,
						maskData = GetUncompressedData(serverTileDatum.maskData)
					};
					map.m_ServerState.SetCompressedMask(serverTileDatum.coord, serverTileDatum.maskData);
				}
			}
			if (locallyTouchedTileCoords == null)
			{
				return;
			}
			map.m_LocallyTouchedTileCoords.Clear();
			foreach (IntVector2 locallyTouchedTileCoord in locallyTouchedTileCoords)
			{
				map.m_LocallyTouchedTileCoords.Add(locallyTouchedTileCoord);
			}
		}
	}

	private class CachedCompressedMaskTiles
	{
		private Dictionary<Vector2Int, byte[]> m_CachedCompressedTileMasks;

		private Func<Vector2Int, byte[]> m_GetMaskBytesFunc;

		public CachedCompressedMaskTiles(Func<Vector2Int, byte[]> getMaskBytesFunc)
		{
			m_CachedCompressedTileMasks = new Dictionary<Vector2Int, byte[]>();
			m_GetMaskBytesFunc = getMaskBytesFunc;
		}

		public byte[] GetCompressedMask(Vector2Int coord)
		{
			if (!m_CachedCompressedTileMasks.TryGetValue(coord, out var value))
			{
				value = GetCompressedData(m_GetMaskBytesFunc(coord));
				m_CachedCompressedTileMasks[coord] = value;
			}
			return value;
		}

		public void SetCompressedMask(Vector2Int coord, byte[] compressedMask)
		{
			m_CachedCompressedTileMasks[coord] = compressedMask;
		}

		public void InvalidateMask(Vector2Int coord)
		{
			m_CachedCompressedTileMasks.Remove(coord);
		}

		public void Clear()
		{
			m_CachedCompressedTileMasks.Clear();
		}
	}

	private struct TexJob
	{
		public Vector2Int coord;

		public JobHandle job;

		public Texture2D tex;
	}

	public enum TexturePartition
	{
		Row = 0,
		Block4x4 = 4,
		Block8x8 = 8,
		Block16x16 = 0x10
	}

	public enum NavWaypointLimit
	{
		NoneAllowed,
		SpecificCount,
		Unlimited
	}

	public enum NavWaypointLimitExceedBehaviour
	{
		PreventAdd,
		RemoveOldestBeforeAdd
	}

	private class PriorityList<T>
	{
		private List<T> elements = new List<T>();

		private bool dirty;

		private Comparison<T> comparer;

		public int Count => elements.Count;

		public PriorityList(Comparison<T> customComparer)
		{
			comparer = (T a, T b) => customComparer(a, b) * -1;
		}

		public void Add(T item)
		{
			elements.Add(item);
			dirty = true;
		}

		public T Pop()
		{
			T result = Peek();
			elements.RemoveAt(elements.Count - 1);
			return result;
		}

		public T Peek()
		{
			SortIfNeeded();
			return elements[elements.Count - 1];
		}

		public void Clear()
		{
			elements.Clear();
			dirty = false;
		}

		private void SortIfNeeded()
		{
			if (dirty)
			{
				elements.Sort(comparer);
				dirty = false;
			}
		}
	}

	private struct ColorRGB24
	{
		public byte r;

		public byte g;

		public byte b;

		public static implicit operator ColorRGB24(Color32 c)
		{
			return new ColorRGB24
			{
				r = c.r,
				g = c.g,
				b = c.b
			};
		}
	}

	[BurstCompile]
	private struct UpdateMapRevealJob : IJobParallelFor
	{
		[NativeDisableParallelForRestriction]
		public NativeArray<byte> imagePixels;

		public int imageSize;

		public Vector2Int scenePosPx;

		public Vector2Int tilePosPx;

		public float affectedRadiusPx;

		public float explorationRadiusPx;

		public float maxSDFDistancePx;

		public TexturePartition partitioning;

		private const float kDiagonalMultiplier = 1.415f;

		public void Execute(int jobIndex)
		{
			float affectedRadiusPxSqr = affectedRadiusPx * affectedRadiusPx;
			if (partitioning == TexturePartition.Row)
			{
				int num = jobIndex * imageSize;
				for (int i = 0; i < imageSize; i++)
				{
					DoPixel(num, i, jobIndex, affectedRadiusPxSqr, explorationRadiusPx, maxSDFDistancePx);
					num++;
				}
				return;
			}
			int num2 = (int)partitioning;
			int num3 = jobIndex * num2;
			int num4 = num3 % imageSize;
			int num5 = num3 / imageSize * num2;
			int num6 = num2 / 2;
			float num7 = (float)num6 * 1.415f;
			Vector2 vector = tilePosPx + new Vector2Int(num4 + num6, num5 + num6) - scenePosPx;
			float num8 = affectedRadiusPx + num7;
			float num9 = num8 * num8;
			if (vector.sqrMagnitude > num9)
			{
				return;
			}
			for (int j = num5; j < num5 + num2; j++)
			{
				int num10 = j * imageSize + num4;
				for (int k = num4; k < num4 + num2; k++)
				{
					DoPixel(num10, k, j, affectedRadiusPxSqr, explorationRadiusPx, maxSDFDistancePx);
					num10++;
				}
			}
		}

		private void DoPixel(int i, int x, int y, float _affectedRadiusPxSqr, float _explorationRadiusPx, float _maxSDFDistancePx)
		{
			float sqrMagnitude = ((Vector2)(tilePosPx + new Vector2Int(x, y) - scenePosPx)).sqrMagnitude;
			if (sqrMagnitude < _affectedRadiusPxSqr)
			{
				byte b = imagePixels[i];
				if (b < byte.MaxValue)
				{
					float num = Mathf.Sqrt(sqrMagnitude);
					byte b2 = (byte)(Mathf.FloorToInt(Mathf.Clamp((_explorationRadiusPx - num) / _maxSDFDistancePx, -1f, 1f) * 0.5f * 255f) + 128);
					b = (byte)Mathf.Max(b, b2);
					imagePixels[i] = b;
				}
			}
		}
	}

	[Header("Mask settings")]
	[SerializeField]
	private int m_MaskImageSize = 32;

	[SerializeField]
	private bool m_MaskGenerateMips;

	[Header("Exploration Settings")]
	[SerializeField]
	private float m_MaxSDFDistanceWorld = 50f;

	[SerializeField]
	private int m_ExplorationMinDist = 3;

	[SerializeField]
	private TexturePartition m_Partitioning = TexturePartition.Block16x16;

	[SerializeField]
	[Header("User waypoints")]
	private NavWaypointLimit m_NavWaypointLimit = NavWaypointLimit.Unlimited;

	[InspectorVisibilityControl("m_NavWaypointLimit", NavWaypointLimit.SpecificCount, InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	[SerializeField]
	private int m_MaxNavWaypointCount;

	[InspectorVisibilityControl("m_NavWaypointLimit", NavWaypointLimit.SpecificCount, InspectorVisibilityControlAttribute.ComparisonType.Equals)]
	[SerializeField]
	private NavWaypointLimitExceedBehaviour m_NavWaypointLimitExceedBehaviour;

	[Header("Legacy save handling")]
	[SerializeField]
	private float legacyGameRadiusAroundPlayerTech = 550f;

	[SerializeField]
	private float legacyExploreTSRadius = 350f;

	[Header("Debug")]
	[SerializeField]
	private bool m_TriggerExplore;

	[SerializeField]
	private Vector2 m_ExplorePos;

	[SerializeField]
	private float m_ExploreRange = 500f;

	[SerializeField]
	private float m_ExploreDuration = 2f;

	public EventNoParams TileImagesChangedEvent;

	public Event<Vector2Int> TileImageChangedEvent;

	public EventNoParams ClientMapDataReceivedEvent;

	private bool m_IsExploreAroundLocalPlayerEnabled;

	private WorldPosition m_LastExploredPos;

	private int m_LastContainerCount;

	private List<TexJob> m_TextureJobs = new List<TexJob>();

	public Dictionary<Vector2Int, bool> m_TileImagesCreated = new Dictionary<Vector2Int, bool>();

	public Dictionary<Vector2Int, Texture2D> m_TileImages = new Dictionary<Vector2Int, Texture2D>();

	public Dictionary<Vector2Int, Texture2D> m_MaskImages = new Dictionary<Vector2Int, Texture2D>();

	public Dictionary<Vector2Int, byte[]> m_TileHeights = new Dictionary<Vector2Int, byte[]>();

	[ReadOnly(ReadOnlyAttribute.EnabledState.EditorOnly)]
	[SerializeField]
	private Texture2D m_AtlasTexture;

	private Texture2D m_TilePlaceholderDuringLoad;

	private PriorityList<Vector2Int> m_QueuedTileRenders;

	private SaveData? m_DeferredLoadSaveData;

	private bool m_ExploreAroundPlayerTech_LegacySave;

	private HashSet<TrackedVisible> m_ExploredTradingStations = new HashSet<TrackedVisible>();

	private HashSet<Vector2Int> m_LocallyTouchedTileCoords = new HashSet<Vector2Int>();

	private CachedCompressedMaskTiles m_LocalPlayerState;

	private CachedCompressedMaskTiles m_ServerState;

	private Dictionary<Vector2Int, TileData> m_MPServerMapData;

	private const int kImageSize = 64;

	private List<TrackedVisible> m_NavigationWaypoints = new List<TrackedVisible>();

	private readonly int kAtlasTexParamID = Shader.PropertyToID("_AtlasTex");

	private static byte[] s_sharedCompressionBuff = new byte[1024];

	public bool MapAvailable => true;

	public bool TerrainScanDirty { get; set; }

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		m_DeferredLoadSaveData = null;
		m_ExploreAroundPlayerTech_LegacySave = false;
		if (optionalLoadState != null)
		{
			SaveData saveData2;
			bool saveData = optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManMap, out saveData2);
			if (saveData)
			{
				m_DeferredLoadSaveData = saveData2;
			}
			m_ExploreAroundPlayerTech_LegacySave = !saveData;
		}
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = default(SaveData);
		if (saveData.ConstructFrom(this))
		{
			saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManMap, saveData);
		}
	}

	public void ModeExit()
	{
		m_IsExploreAroundLocalPlayerEnabled = false;
		Singleton.Manager<ManUpdate>.inst.RemoveAction(ManUpdate.Type.Update, ManUpdate.Order.First, UpdateExploredArea_Schedule);
		Singleton.Manager<ManUpdate>.inst.RemoveAction(ManUpdate.Type.LateUpdate, ManUpdate.Order.Last, UpdateExploredArea_Apply);
		m_QueuedTileRenders?.Clear();
		CompleteAsyncTextureUpdates();
		ClearMapTileTextures();
		m_TileImagesCreated.Clear();
		m_TileHeights.Clear();
		m_ExploredTradingStations.Clear();
		m_LocallyTouchedTileCoords.Clear();
		m_LocalPlayerState.Clear();
		m_LastExploredPos = default(WorldPosition);
		m_LastContainerCount = int.MinValue;
		RemoveAllNavigationWaypoints();
		m_MPServerMapData?.Clear();
		m_ServerState?.Clear();
	}

	public void EnableExploreAroundPlayer()
	{
		m_IsExploreAroundLocalPlayerEnabled = true;
	}

	public void RefreshEntireMap()
	{
		foreach (KeyValuePair<Vector2Int, Texture2D> tileImage in m_TileImages)
		{
			RenderTileImage(tileImage.Key, tileImage.Value);
		}
	}

	public void SaveAndReload()
	{
		CompleteAsyncTextureUpdates();
		Vector2Int key = new Vector2Int(0, 1);
		byte[] rawTextureData = m_MaskImages[key].GetRawTextureData();
		ManSaveGame.State state = new ManSaveGame.State();
		SaveData saveData = default(SaveData);
		if (saveData.ConstructFrom(this))
		{
			state.AddSaveData(ManSaveGame.SaveDataJSONType.ManMap, saveData);
		}
		else
		{
			d.LogError("Failed to create savedata");
		}
		int saveDataSize = state.GetSaveDataSize(ManSaveGame.SaveDataJSONType.ManMap, compressed: false);
		int saveDataSize2 = state.GetSaveDataSize(ManSaveGame.SaveDataJSONType.ManMap, compressed: true);
		int count = m_MaskImages.Count;
		d.Log($"Map average data size is {saveDataSize / count} bytes ({(float)(saveDataSize / count) / 1024f:.0}kb) uncompressed, and {saveDataSize2 / count} bytes ({(float)(saveDataSize2 / count) / 1024f:.0}kb) compressed, on average per tile.");
		if (state.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManMap, out var saveData2))
		{
			saveData2.ApplyTo(this);
		}
		else
		{
			d.LogError("Problem loading data!");
		}
		byte[] rawTextureData2 = m_MaskImages[key].GetRawTextureData();
		if (HaveSameContents(rawTextureData, rawTextureData2))
		{
			d.Log($"Reloaded ManMap save data for {m_MaskImages.Count} tiles");
		}
		else
		{
			d.LogError("Difference in data between before and after save-load cycle!");
		}
		static bool HaveSameContents(byte[] a, byte[] b)
		{
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}
	}

	public TrackedVisible AddNavigationWaypointAtPos(Vector3 worldPos)
	{
		TrackedVisible trackedVisible = null;
		bool flag;
		switch (m_NavWaypointLimit)
		{
		case NavWaypointLimit.NoneAllowed:
			flag = false;
			break;
		case NavWaypointLimit.SpecificCount:
		{
			bool flag2 = m_NavigationWaypoints.Count == m_MaxNavWaypointCount;
			if (flag2 && m_NavWaypointLimitExceedBehaviour == NavWaypointLimitExceedBehaviour.RemoveOldestBeforeAdd)
			{
				RemoveNavigationWaypoint(m_NavigationWaypoints[0]);
				flag = true;
			}
			else
			{
				flag = !flag2;
			}
			break;
		}
		default:
			flag = true;
			break;
		}
		if (flag)
		{
			Vector3 position = worldPos + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			Visible.DisableAddToTileOnSpawn = true;
			trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Waypoint, 0), position, Quaternion.identity, addToObjectManager: false, forceSpawn: true);
			Visible.DisableAddToTileOnSpawn = false;
			trackedVisible.RadarType = RadarTypes.MapNavTarget;
			trackedVisible.visible.StopManagingVisible();
			Singleton.Manager<ManVisible>.inst.TrackWithoutSaving(trackedVisible);
			trackedVisible.visible.trans.parent = base.transform;
			m_NavigationWaypoints.Add(trackedVisible);
		}
		return trackedVisible;
	}

	public void RemoveNavigationWaypoint(TrackedVisible waypoint)
	{
		d.Assert(m_NavigationWaypoints.Remove(waypoint), "Trying to remove nav waypoint that isn't in our tracked list!?");
		d.Assert(waypoint.visible != null, "RemoveNavigationWaypoint had Null visible on waypoint trackedVis?! This is invalid");
		waypoint.visible.RemoveFromGame();
	}

	public void RemoveAllNavigationWaypoints()
	{
		foreach (TrackedVisible navigationWaypoint in m_NavigationWaypoints)
		{
			d.Assert(navigationWaypoint.visible != null, "RemoveNavigationWaypoint had Null visible on waypoint trackedVis?! This is invalid");
			navigationWaypoint.visible.RemoveFromGame();
		}
		m_NavigationWaypoints.Clear();
	}

	public void MarkTradingStationAsScanPerformed(TrackedVisible tv)
	{
		m_ExploredTradingStations.Add(tv);
	}

	public bool HasTradingStationPerformedScan(TrackedVisible tv)
	{
		return m_ExploredTradingStations.Contains(tv);
	}

	public void RequestMapData()
	{
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestMapData, new RequestMapDataMessage());
	}

	public void UploadMapData()
	{
		CompleteAsyncTextureUpdates(m_LocallyTouchedTileCoords);
		List<TileData> list = new List<TileData>();
		foreach (Vector2Int locallyTouchedTileCoord in m_LocallyTouchedTileCoords)
		{
			byte[] compressedMask = m_LocalPlayerState.GetCompressedMask(locallyTouchedTileCoord);
			list.Add(new TileData
			{
				coord = locallyTouchedTileCoord,
				maskData = compressedMask
			});
		}
		MapDataMessage message = new MapDataMessage
		{
			tileData = list
		};
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.MapData, message);
		m_LocallyTouchedTileCoords.Clear();
	}

	public bool IsTileImageCreated(Vector2Int tileCoord)
	{
		bool value;
		return m_TileImagesCreated.TryGetValue(tileCoord, out value) && value;
	}

	private static byte[] GetCompressedData(byte[] uncompressedData)
	{
		using MemoryStream memoryStream = new MemoryStream(s_sharedCompressionBuff);
		using (GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress))
		{
			gZipStream.Write(uncompressedData, 0, uncompressedData.Length);
		}
		return memoryStream.ToArray();
	}

	private static byte[] GetUncompressedData(byte[] compressedData, bool createCopy = true)
	{
		using (MemoryStream stream = new MemoryStream(compressedData))
		{
			using GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
			gZipStream.Read(s_sharedCompressionBuff, 0, s_sharedCompressionBuff.Length);
		}
		if (!createCopy)
		{
			return s_sharedCompressionBuff;
		}
		return s_sharedCompressionBuff.ToArray();
	}

	private byte[] GetBytesFromMaskTex(Vector2Int coord)
	{
		return m_MaskImages[coord].GetRawTextureData();
	}

	private void RequestAsyncTileRender(Vector2Int tileCoord)
	{
		Vector2Int playerPos;
		int cacheValidity;
		if (m_QueuedTileRenders == null)
		{
			playerPos = default(Vector2Int);
			cacheValidity = int.MinValue;
			m_QueuedTileRenders = new PriorityList<Vector2Int>(ComparePlayerToTileCoordDistance);
		}
		m_QueuedTileRenders.Add(tileCoord);
		int ComparePlayerToTileCoordDistance(Vector2Int aPos, Vector2Int bPos)
		{
			if (cacheValidity != Time.frameCount)
			{
				playerPos = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(Singleton.playerPos);
				cacheValidity = Time.frameCount;
			}
			return (playerPos - aPos).sqrMagnitude.CompareTo((playerPos - bPos).sqrMagnitude);
		}
	}

	private void CreateTileImage(Vector2Int tileCoord)
	{
		m_TileImages[tileCoord] = m_TilePlaceholderDuringLoad;
		m_TileImagesCreated[tileCoord] = false;
		RequestAsyncTileRender(tileCoord);
		Texture2D texture2D = NewMaskTexture();
		NativeArray<byte> rawTextureData = texture2D.GetRawTextureData<byte>();
		for (int i = 0; i < rawTextureData.Length; i++)
		{
			rawTextureData[i] = 0;
		}
		texture2D.Apply();
		m_MaskImages[tileCoord] = texture2D;
		m_LocalPlayerState.InvalidateMask(tileCoord);
	}

	private Texture2D RenderTileImage(Vector2Int tileCoord)
	{
		Texture2D texture2D = new Texture2D(64, 64, TextureFormat.RGB24, mipChain: false, linear: true);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.filterMode = FilterMode.Point;
		RenderTileImage(tileCoord, texture2D);
		return texture2D;
	}

	private void RenderTileImage(Vector2Int tileCoord, Texture2D tileImage)
	{
		int seedValue = Singleton.Manager<ManWorld>.inst.SeedValue;
		NativeArray<ColorRGB24> imagePixelsRGB24 = tileImage.GetRawTextureData<ColorRGB24>();
		int pixelIndex = 0;
		Vector2 origin = (tileCoord - Vector2.one * 0.5f + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset / Singleton.Manager<ManWorld>.inst.TileSize) * 64f;
		Vector2 size = Vector2.one * 64f;
		Singleton.Manager<ManWorld>.inst.CurrentBiomeMap.Render_Jobbed(origin, size, 1, seedValue, 64, 6, FillPixelWithColourSequential, 3, BiomeMap.RenderMode.Weighted);
		tileImage.Apply();
		void FillPixelWithColourSequential(Color32 c32)
		{
			imagePixelsRGB24[pixelIndex++] = c32;
		}
	}

	private Texture2D NewMaskTexture()
	{
		return new Texture2D(m_MaskImageSize, m_MaskImageSize, TextureFormat.Alpha8, m_MaskGenerateMips, linear: true)
		{
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Bilinear
		};
	}

	private void ClearMapTileTextures()
	{
		foreach (Texture2D value in m_TileImages.Values)
		{
			UnityEngine.Object.Destroy(value);
		}
		m_TileImages.Clear();
		foreach (Texture2D value2 in m_MaskImages.Values)
		{
			UnityEngine.Object.Destroy(value2);
		}
		m_MaskImages.Clear();
	}

	private void UpdateExploredArea_Schedule()
	{
		if (m_IsExploreAroundLocalPlayerEnabled && Singleton.playerTank != null && Singleton.playerTank.Radar.IsMappingTerrain)
		{
			float num = m_ExplorationMinDist;
			float num2 = num * num;
			Vector3 scenePos = ((Singleton.playerTank != null) ? Singleton.playerTank.trans : Singleton.cameraTrans).position.SetY(0f);
			if ((scenePos - m_LastExploredPos.ScenePosition).sqrMagnitude > num2 || m_LastContainerCount != m_TileImages.Count || TerrainScanDirty)
			{
				m_LastExploredPos = WorldPosition.FromScenePosition(in scenePos);
				m_LastContainerCount = m_TileImages.Count;
				float range = Singleton.playerTank.Radar.GetRange(ModuleRadar.RadarScanType.Terrain);
				ExploreArea(scenePos, range);
				TerrainScanDirty = false;
			}
		}
	}

	private void UpdateExploredArea_Apply()
	{
		CompleteAsyncTextureUpdates();
	}

	private void CompleteAsyncTextureUpdates()
	{
		foreach (TexJob textureJob in m_TextureJobs)
		{
			JobHandle job = textureJob.job;
			job.Complete();
			textureJob.tex.Apply(m_MaskGenerateMips);
		}
		m_TextureJobs.Clear();
	}

	private void CompleteAsyncTextureUpdates(IEnumerable<Vector2Int> collection)
	{
		for (int num = m_TextureJobs.Count - 1; num >= 0; num--)
		{
			TexJob texJob = m_TextureJobs[num];
			if (collection.Contains(texJob.coord))
			{
				texJob.job.Complete();
				texJob.tex.Apply(m_MaskGenerateMips);
				m_TextureJobs.RemoveAt(num);
			}
		}
	}

	public void ExploreArea(Vector3 scenePosition, float radius)
	{
		int maskImageSize = m_MaskImageSize;
		float num = (float)maskImageSize / Singleton.Manager<ManWorld>.inst.TileSize;
		float explorationRadiusPx = radius * num;
		float num2 = radius + m_MaxSDFDistanceWorld;
		float affectedRadiusPx = num2 * num;
		float maxSDFDistancePx = m_MaxSDFDistanceWorld * num;
		Vector2Int scenePosPx = Vector2Int.RoundToInt(scenePosition.ToVector2XZ() * num);
		TileManager.TilesWithinRadius enumerator = new TileManager.TilesWithinRadius(scenePosition, num2).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Vector2Int coord = enumerator.Current;
			if (!m_TileImages.ContainsKey(coord))
			{
				CreateTileImage(coord);
			}
			if (m_MaskImages.TryGetValue(coord, out var value))
			{
				Vector2Int tilePosPx = Vector2Int.RoundToInt(Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene((IntVector2)coord).ToVector2XZ() * num);
				UpdateMapRevealJob jobData = new UpdateMapRevealJob
				{
					imagePixels = value.GetRawTextureData<byte>(),
					imageSize = maskImageSize,
					scenePosPx = scenePosPx,
					tilePosPx = tilePosPx,
					explorationRadiusPx = explorationRadiusPx,
					affectedRadiusPx = affectedRadiusPx,
					maxSDFDistancePx = maxSDFDistancePx,
					partitioning = m_Partitioning
				};
				int partitioning = (int)m_Partitioning;
				int arrayLength = ((m_Partitioning == TexturePartition.Row) ? maskImageSize : (maskImageSize * maskImageSize / (partitioning * partitioning)));
				JobHandle dependsOn = default(JobHandle);
				int num3 = m_TextureJobs.FindLastIndex((TexJob j) => j.coord == coord);
				if (num3 >= 0)
				{
					dependsOn = m_TextureJobs[num3].job;
				}
				JobHandle job = jobData.Schedule(arrayLength, 1, dependsOn);
				m_TextureJobs.Add(new TexJob
				{
					coord = coord,
					job = job,
					tex = value
				});
				m_LocallyTouchedTileCoords.Add(coord);
				m_LocalPlayerState.InvalidateMask(coord);
			}
		}
	}

	private void CreateBiomeAtlas(BiomeMap biomeMap)
	{
		if (m_AtlasTexture == null || !m_AtlasTexture.isReadable)
		{
			m_AtlasTexture = new Texture2D(512, 256, TextureFormat.RGBA32, mipChain: false);
			m_AtlasTexture.wrapMode = TextureWrapMode.Clamp;
			m_AtlasTexture.filterMode = FilterMode.Point;
		}
		NativeArray<Color32> rawTextureData = m_AtlasTexture.GetRawTextureData<Color32>();
		int numBiomes = biomeMap.GetNumBiomes();
		for (int i = 0; i < numBiomes; i++)
		{
			int atlasSquareX = i % 8;
			int atlasSquareY = 3 - i / 8;
			Biome biome = biomeMap.LookupBiome((byte)i);
			Texture2D mapTexture = biome.MapTexture;
			if (mapTexture != null)
			{
				d.Assert(mapTexture.width == 64);
				Color32[] biomePixels = mapTexture.GetPixels32();
				FillAtlasSquare(rawTextureData, atlasSquareX, atlasSquareY, (int x, int y) => biomePixels[y * 64 + x]);
				continue;
			}
			d.LogError("Biome " + biome.name + " did not have a mapTexture assigned!", biome);
			Texture2D diffuseTexture = biome.MainMaterialLayer.diffuseTexture;
			int srcRow = diffuseTexture.width;
			int stepSize = srcRow / 64;
			int startOffset = stepSize / 2;
			Color32[] biomePixels2 = diffuseTexture.GetPixels32();
			FillAtlasSquare(rawTextureData, atlasSquareX, atlasSquareY, delegate(int x, int y)
			{
				int num = (startOffset + y * stepSize) * srcRow;
				return biomePixels2[num + startOffset + x * stepSize];
			});
		}
		m_AtlasTexture.Apply();
		Shader.SetGlobalTexture(kAtlasTexParamID, m_AtlasTexture);
		static void FillAtlasSquare(NativeArray<Color32> rawAtlasPixels, int num3, int num2, Func<int, int, Color32> getColourFunc)
		{
			int num = num2 * 32768 + num3 * 64;
			for (int j = 0; j < 64; j++)
			{
				int num4 = num + j * 512;
				for (int k = 0; k < 64; k++)
				{
					rawAtlasPixels[num4 + k] = getColourFunc(k, j);
				}
			}
		}
	}

	[Conditional("UNITY_EDITOR")]
	private void DrawShaderKeywordToggle(string shaderKeyword, string enableKeywordText, string disableKeywordText)
	{
		bool flag = Shader.IsKeywordEnabled(shaderKeyword);
		if (GUILayout.Button(flag ? enableKeywordText : disableKeywordText))
		{
			if (flag)
			{
				Shader.DisableKeyword(shaderKeyword);
			}
			else
			{
				Shader.EnableKeyword(shaderKeyword);
			}
		}
	}

	private void OnModeStart(Mode mode)
	{
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Unsubscribe(OnModeStart);
		CreateBiomeAtlas(Singleton.Manager<ManWorld>.inst.CurrentBiomeMap);
		if (m_MPServerMapData == null && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && ManNetwork.IsHostOrWillBe)
		{
			m_MPServerMapData = new Dictionary<Vector2Int, TileData>();
			m_ServerState = new CachedCompressedMaskTiles((Vector2Int coord) => m_MPServerMapData[coord].maskData);
		}
		if (m_DeferredLoadSaveData.HasValue)
		{
			m_DeferredLoadSaveData.Value.ApplyTo(this);
			m_DeferredLoadSaveData = null;
		}
		if (m_ExploreAroundPlayerTech_LegacySave)
		{
			Dictionary<int, TrackedVisible>.Enumerator allTrackedVisiblesEnumerator = Singleton.Manager<ManVisible>.inst.AllTrackedVisiblesEnumerator;
			while (allTrackedVisiblesEnumerator.MoveNext())
			{
				TrackedVisible value = allTrackedVisiblesEnumerator.Current.Value;
				if (ManSpawn.IsPlayerTeam(value.RadarTeamID))
				{
					ExploreArea(value.Position, legacyGameRadiusAroundPlayerTech);
				}
				else if (value.RadarType == RadarTypes.Vendor)
				{
					ExploreArea(value.Position, legacyExploreTSRadius);
				}
			}
		}
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.First, UpdateExploredArea_Schedule);
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.LateUpdate, ManUpdate.Order.Last, UpdateExploredArea_Apply);
	}

	private void OnServerMapDataRequested(NetworkMessage netMsg)
	{
		netMsg.ReadMessage<RequestMapDataMessage>();
		NetPlayer sender = netMsg.GetSender();
		List<TileData> list = new List<TileData>(m_MPServerMapData.Count);
		foreach (Vector2Int key in m_MPServerMapData.Keys)
		{
			TileData item = new TileData
			{
				coord = key,
				maskData = m_ServerState.GetCompressedMask(key)
			};
			list.Add(item);
		}
		MapDataMessage message = new MapDataMessage
		{
			tileData = list
		};
		Singleton.Manager<ManNetwork>.inst.SendToClient(sender.connectionToClient.connectionId, TTMsgType.MapData, message);
	}

	private void OnServerMapDataReceived(NetworkMessage netMsg)
	{
		foreach (TileData tileDatum in netMsg.ReadMessage<MapDataMessage>().tileData)
		{
			Vector2Int vector2Int = tileDatum.coord;
			byte[] uncompressedData = GetUncompressedData(tileDatum.maskData, createCopy: false);
			bool flag = false;
			bool flag2 = false;
			byte[] array;
			if (m_MPServerMapData.TryGetValue(vector2Int, out var value))
			{
				array = value.maskData;
				for (int i = 0; i < array.Length; i++)
				{
					byte b = array[i];
					byte b2 = uncompressedData[i];
					if (b2 > b)
					{
						array[i] = b2;
						flag = true;
					}
					else
					{
						flag2 = b2 != b;
					}
				}
			}
			else
			{
				array = uncompressedData.ToArray();
				flag = true;
			}
			m_MPServerMapData[vector2Int] = new TileData
			{
				coord = vector2Int,
				maskData = array
			};
			if (flag)
			{
				if (flag2)
				{
					m_ServerState.InvalidateMask(vector2Int);
				}
				else
				{
					m_ServerState.SetCompressedMask(vector2Int, tileDatum.maskData);
				}
			}
		}
	}

	private void OnServerAddNavWaypointRequested(NetworkMessage netMsg)
	{
		RequestAddNavWaypointMessage requestAddNavWaypointMessage = netMsg.ReadMessage<RequestAddNavWaypointMessage>();
		AddNavigationWaypointAtPos(requestAddNavWaypointMessage.worldPos);
	}

	private void OnServerRemoveNavWaypointRequested(NetworkMessage netMsg)
	{
		RequestRemoveNavWaypointMessage requestRemoveNavWaypointMessage = netMsg.ReadMessage<RequestRemoveNavWaypointMessage>();
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(requestRemoveNavWaypointMessage.hostID);
		if (trackedVisible != null)
		{
			RemoveNavigationWaypoint(trackedVisible);
		}
		else
		{
			d.LogError($"Server failed to find TrackedVis with hostID {requestRemoveNavWaypointMessage.hostID}; did not remove any waypoint.");
		}
	}

	private void OnClientMapDataReceived(NetworkMessage netMsg)
	{
		MapDataMessage mapDataMessage = netMsg.ReadMessage<MapDataMessage>();
		CompleteAsyncTextureUpdates(((IEnumerable<TileData>)mapDataMessage.tileData).Select((Func<TileData, Vector2Int>)((TileData t) => t.coord)));
		foreach (TileData tileDatum in mapDataMessage.tileData)
		{
			bool flag = false;
			bool flag2 = false;
			Vector2Int vector2Int = tileDatum.coord;
			byte[] uncompressedData = GetUncompressedData(tileDatum.maskData, createCopy: false);
			if (!m_MaskImages.TryGetValue(vector2Int, out var value))
			{
				CreateTileImage(vector2Int);
				value = m_MaskImages[vector2Int];
			}
			NativeArray<byte> rawTextureData = value.GetRawTextureData<byte>();
			for (int num = 0; num < rawTextureData.Length; num++)
			{
				byte b = rawTextureData[num];
				byte b2 = uncompressedData[num];
				if (b2 > b)
				{
					rawTextureData[num] = b2;
					flag2 = true;
				}
				else
				{
					flag = b2 != b;
				}
			}
			if (flag2)
			{
				value.Apply();
				if (flag)
				{
					m_LocalPlayerState.InvalidateMask(vector2Int);
				}
				else
				{
					m_LocalPlayerState.SetCompressedMask(vector2Int, tileDatum.maskData);
				}
			}
			if (!flag)
			{
				m_LocallyTouchedTileCoords.Remove(vector2Int);
			}
		}
		ClientMapDataReceivedEvent.Send();
	}

	private void Start()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.RequestMapData, OnServerMapDataRequested);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.MapData, OnServerMapDataReceived);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.RequestAddNavWaypoint, OnServerAddNavWaypointRequested);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.RequestRemoveNavWaypoint, OnServerRemoveNavWaypointRequested);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.MapData, OnClientMapDataReceived);
		m_TilePlaceholderDuringLoad = new Texture2D(64, 64, TextureFormat.RGBA32, mipChain: false, linear: true);
		m_TilePlaceholderDuringLoad.wrapMode = TextureWrapMode.Clamp;
		m_TilePlaceholderDuringLoad.filterMode = FilterMode.Point;
		NativeArray<Color32> rawTextureData = m_TilePlaceholderDuringLoad.GetRawTextureData<Color32>();
		Color32 value = new Color32
		{
			r = 0,
			g = 0,
			b = 0,
			a = 0
		};
		for (int i = 0; i < rawTextureData.Length; i++)
		{
			rawTextureData[i] = value;
		}
		m_TilePlaceholderDuringLoad.Apply();
		m_LocalPlayerState = new CachedCompressedMaskTiles(GetBytesFromMaskTex);
	}

	private void Update()
	{
		if (MapAvailable && Singleton.playerTank != null)
		{
			if (Singleton.Manager<ManUI>.inst.IsStackEmpty() && Singleton.Manager<ManInput>.inst.GetButtonDown(97) && Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.WorldMapButton))
			{
				Singleton.Manager<ManHUD>.inst.ToggleHudElementShown(ManHUD.HUDElementType.WorldMap);
			}
		}
		else if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.WorldMap))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.WorldMap);
		}
		if (m_QueuedTileRenders != null && m_QueuedTileRenders.Count > 0)
		{
			Vector2Int vector2Int = m_QueuedTileRenders.Pop();
			m_TileImages[vector2Int] = RenderTileImage(vector2Int);
			m_TileImagesCreated[vector2Int] = true;
			TileImageChangedEvent.Send(vector2Int);
		}
	}

	public void ExploreArea(Vector3 scenePos, float range, float duration, float initialDelayS = 0f)
	{
		WorldPosition pos = WorldPosition.FromScenePosition(in scenePos);
		StartCoroutine(PreCreateMapTiles(pos, range));
		StartCoroutine(GrowExplorationArea(pos, range, duration, initialDelayS));
	}

	private IEnumerator PreCreateMapTiles(WorldPosition pos, float range)
	{
		TileManager.TilesWithinRadius tilesInRangeIterator = new TileManager.TilesWithinRadius(pos.ScenePosition, range);
		Stopwatch deferredCreationStopwatch = new Stopwatch();
		bool keepIterating = true;
		while (keepIterating)
		{
			long num = 0L;
			deferredCreationStopwatch.Restart();
			while (deferredCreationStopwatch.ElapsedMilliseconds + (int)((float)num * 0.7f) < 6)
			{
				if (!tilesInRangeIterator.MoveNext())
				{
					keepIterating = false;
					break;
				}
				if (!m_TileImages.ContainsKey(tilesInRangeIterator.Current))
				{
					long elapsedMilliseconds = deferredCreationStopwatch.ElapsedMilliseconds;
					CreateTileImage(tilesInRangeIterator.Current);
					num = deferredCreationStopwatch.ElapsedMilliseconds - elapsedMilliseconds;
				}
			}
			yield return null;
		}
	}

	private IEnumerator GrowExplorationArea(WorldPosition pos, float range, float duration, float initialDelayS = 0f)
	{
		if (initialDelayS > 0f)
		{
			yield return new WaitForSeconds(initialDelayS);
		}
		float invTime = 1f / duration;
		float curRange = 0f;
		while (curRange < range)
		{
			curRange = Mathf.MoveTowards(curRange, range, range * Time.deltaTime * invTime);
			ExploreArea(pos.ScenePosition, curRange);
			yield return null;
		}
	}
}
