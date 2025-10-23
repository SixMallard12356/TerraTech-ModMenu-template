#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class WorldTile
{
	public enum State
	{
		Empty,
		Created,
		Populated,
		Loaded
	}

	public enum LoadStep
	{
		Empty,
		GeneratingTerrain,
		Creating,
		Created,
		GeneratingScenery,
		StartPopulatingScenery,
		PopulatingScenery,
		Populated,
		Loading,
		Loaded
	}

	public enum Priority
	{
		Null,
		Lowest,
		Low,
		Medium,
		High,
		Highest
	}

	public class TilePosInfo
	{
		public WorldTile tile;

		public Vector2 coordInTile;

		public float[] biomeWeighting = new float[s_BiomeTypeValues.Length];

		public List<Biome> biomeList = new List<Biome>();

		public float biggestFreeRadius;

		public Vector3 WorldPos => tile.WorldOrigin + coordInTile.ToVector3XZ();

		public Vector3 ScenePos => tile.CalcSceneOrigin() + coordInTile.ToVector3XZ();

		public TilePosInfo()
		{
			Clear();
		}

		public void Clear()
		{
			tile = null;
			coordInTile = Vector2.zero;
			biggestFreeRadius = 0f;
			for (int i = 0; i < biomeWeighting.Length; i++)
			{
				biomeWeighting[i] = 0f;
			}
			biomeList.Clear();
		}
	}

	public struct TileInfoIterator
	{
		private List<TilePosInfo> m_TileData;

		private int m_Index;

		private int m_IterationCount;

		private bool m_IterateForwards;

		public TilePosInfo Current => m_TileData[m_Index];

		public TileInfoIterator(List<TilePosInfo> tileData, IntVector2 startingPos, bool iterateForwards = true)
		{
			m_TileData = tileData;
			d.AssertFormat(m_TileData.Count == s_NumTileInfoPoints, "TileInfoIterator - Missmatch between expected list length and actual length! Got {0}, expected {1}", m_TileData.Count, s_NumTileInfoPoints);
			d.Assert(startingPos.x >= 0 && (float)startingPos.x < Singleton.Manager<ManWorld>.inst.TileSize && startingPos.y >= 0 && (float)startingPos.y < Singleton.Manager<ManWorld>.inst.TileSize, "TileInfoIterator - Invalid starting coord for specified tile! " + startingPos.ToString());
			int num = startingPos.x / Singleton.Manager<ManWorld>.inst.TileInfoStepSize;
			int num2 = startingPos.y / Singleton.Manager<ManWorld>.inst.TileInfoStepSize;
			m_Index = num2 * s_NumTileInfoPointsPerSide + num;
			m_Index = Mathf.Clamp(m_Index, 0, s_NumTileInfoPoints - 1);
			m_Index -= (iterateForwards ? 1 : (-1));
			m_IterationCount = -1;
			m_IterateForwards = iterateForwards;
		}

		public TileInfoIterator GetEnumerator()
		{
			return this;
		}

		public bool MoveNext()
		{
			TilePosInfo current;
			do
			{
				if (m_IterateForwards)
				{
					m_Index++;
					if (m_Index >= s_NumTileInfoPoints)
					{
						m_Index = 0;
					}
				}
				else
				{
					m_Index--;
					if (m_Index < 0)
					{
						m_Index = s_NumTileInfoPoints - 1;
					}
				}
				m_IterationCount++;
				if (m_IterationCount >= s_NumTileInfoPoints)
				{
					return false;
				}
				current = Current;
			}
			while (!IsRelevant(current));
			return true;
		}

		private bool IsRelevant(TilePosInfo tileData)
		{
			return true;
		}
	}

	public ReflectionProbe reflectionProbe;

	public int[] patchesToPopulate = new int[2];

	public State m_RequestState;

	public LoadStep m_LoadStep;

	public bool m_Regenerate;

	public ModifiedQuadTree m_ModifiedQuadTree;

	private List<SceneryBlocker> m_SceneryBlockers;

	private List<TerrainObject> m_OverlappingTerrain;

	private List<TilePosInfo> m_TilePosData;

	private static int[] s_BiomeTypeValues = (int[])Enum.GetValues(typeof(BiomeTypes));

	private float[] m_AverageBiomeWeights = new float[s_BiomeTypeValues.Length];

	private float[] m_MaximumBiomeWeights = new float[s_BiomeTypeValues.Length];

	public Dictionary<ChunkTypes, HashSet<Visible>> m_ResourcesOnTile = new Dictionary<ChunkTypes, HashSet<Visible>>();

	private List<Biome> m_BiomesInTile = new List<Biome>();

	private int m_LargestFreeSpaceRadiusOnTile;

	private static int[] objectTypesValues = Enum.GetValues(typeof(ObjectTypes)) as int[];

	private static int s_SpawnIDCounter = 0;

	private const int k_InitialTileObjectArraySize = 100;

	private static readonly int[] kTileInfoFreeSpaceTestRadi = new int[7] { 10, 20, 25, 35, 50, 75, 100 };

	private static readonly int kNumTestRadii = kTileInfoFreeSpaceTestRadi.Length;

	private static readonly int s_NumTileInfoPointsPerSide = Mathf.FloorToInt(Singleton.Manager<ManWorld>.inst.TileSize / (float)Singleton.Manager<ManWorld>.inst.TileInfoStepSize) + 1;

	private static readonly int s_NumTileInfoPoints = s_NumTileInfoPointsPerSide * s_NumTileInfoPointsPerSide;

	private const float kTileCoordPrecisionBias = 0.0001f;

	private static readonly float invCellScale = 1f / Singleton.Manager<ManWorld>.inst.CellScale;

	public int SpawnID { get; private set; }

	public int ScenerySpawnID { get; set; }

	public IntVector2 Coord { get; private set; }

	public Vector3 WorldOrigin { get; private set; }

	public Vector3 WorldCentre { get; private set; }

	public Terrain Terrain { get; private set; }

	public Transform StaticParent { get; private set; }

	public BiomeMap.MapData BiomeMapData { get; private set; }

	public ManSaveGame.StoredTile SaveData { get; private set; }

	public Dictionary<int, Visible>[] Visibles { get; private set; }

	public HashSet<TerrainObject> ManuallyAddedTerrainObjects { get; private set; }

	public List<ManSaveGame.StoredVisible> StoredVisiblesWaitingToLoad { get; private set; }

	public bool HasTerrain => m_LoadStep > LoadStep.GeneratingTerrain;

	public bool IsCreated => m_LoadStep >= LoadStep.Created;

	public bool IsPopulated => m_LoadStep >= LoadStep.Populated;

	public bool IsLoading => m_RequestState == State.Loaded;

	public bool IsLoaded => m_LoadStep == LoadStep.Loaded;

	public bool HasTilePosInfo { get; private set; }

	public NetWorldTile NetTile { get; set; }

	public int LargestFreeSpaceOnTile
	{
		get
		{
			d.Assert(HasTilePosInfo, "LargestFreeSpaceOnTile - Called while CachedTileInfo wasn't created yet");
			return m_LargestFreeSpaceRadiusOnTile;
		}
	}

	public WorldTile()
	{
		BiomeMapData = new BiomeMap.MapData();
		Visibles = new Dictionary<int, Visible>[objectTypesValues.Length];
		d.Assert(objectTypesValues[0] == 0, "Expecting ObjectTypes.Null to be first...");
		for (int i = 1; i < objectTypesValues.Length; i++)
		{
			Visibles[i] = new Dictionary<int, Visible>(100);
		}
		m_SceneryBlockers = new List<SceneryBlocker>();
		m_OverlappingTerrain = new List<TerrainObject>();
	}

	public void ClearNonSceneryVisibles()
	{
		for (int i = 1; i < objectTypesValues.Length; i++)
		{
			if (i != 3)
			{
				Visibles[i].Clear();
			}
		}
		if (StoredVisiblesWaitingToLoad != null)
		{
			StoredVisiblesWaitingToLoad.Clear();
		}
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnTileLoaded);
	}

	public void ClearPersistentTerrainObjects()
	{
		if (ManuallyAddedTerrainObjects != null)
		{
			ManuallyAddedTerrainObjects.Clear();
		}
	}

	public void ClearSceneryVisibles()
	{
		Visibles[3].Clear();
	}

	public void ClearStaticChildren(bool includeTerrain)
	{
		if (!StaticParent)
		{
			return;
		}
		for (int num = StaticParent.childCount - 1; num >= 0; num--)
		{
			Transform child = StaticParent.GetChild(num);
			if (includeTerrain || child.gameObject.layer != (int)Globals.inst.layerTerrain)
			{
				child.Recycle();
			}
		}
	}

	public void ClearAndReset()
	{
		SpawnID = 0;
		Coord = IntVector2.invalid;
		patchesToPopulate[0] = -1;
		patchesToPopulate[1] = -1;
		m_RequestState = State.Empty;
		m_LoadStep = LoadStep.Empty;
		m_Regenerate = false;
		HasTilePosInfo = false;
		ReleaseSaveState();
		ClearNonSceneryVisibles();
		ClearPersistentTerrainObjects();
		ClearSceneryVisibles();
		ClearStaticChildren(includeTerrain: true);
		m_SceneryBlockers.Clear();
		m_OverlappingTerrain.Clear();
		ClearTerrainData();
	}

	public void ClearTerrainData()
	{
		if ((bool)Terrain)
		{
			Terrain.transform.Recycle();
			Terrain.terrainData = null;
			if (!Globals.inst.m_ManuallyConnectTerrainTiles)
			{
				Terrain.SetConnectivityDirty();
			}
			SetTerrain(null);
		}
		BiomeMapData.RecycleHeightBuffer();
		BiomeMapData.RecycleSplatBuffer();
		BiomeMapData.ReturnTerrainLayerArray();
		BiomeMapData.RecycleSceneryBuffer();
	}

	public void Init(IntVector2 coord, int dimension, Vector3 origin, Vector3 extent)
	{
		d.Assert(Terrain == null && StaticParent == null, "Terrain or static parent remaining on recycled tile");
		d.Assert(m_SceneryBlockers.Count == 0, "Spawn blockers remaining on recycled tile");
		d.Assert(m_OverlappingTerrain.Count == 0, "Terrain objects remaining on recycled tile");
		for (int i = 1; i < objectTypesValues.Length; i++)
		{
			d.AssertFormat(Visibles[i].Count == 0, "Visibles of type {0} remaining on recycled tile", (ObjectTypes)i);
		}
		d.Assert(BiomeMapData.heightData == null && BiomeMapData.splatData == null && BiomeMapData.terrainLayers == null && BiomeMapData.sceneryPlacement == null, "Generation buffers remaining allocated on recycled tile");
		ClearAndReset();
		SpawnID = ++s_SpawnIDCounter;
		Coord = coord;
		WorldOrigin = origin;
		WorldCentre = origin + extent;
		BiomeMapData.Init(dimension);
		Rect bounds = new Rect(Vector2.zero, extent.ToVector2XZ() * 2f);
		m_ModifiedQuadTree = new ModifiedQuadTree(0, bounds);
	}

	public void AcquireSaveState()
	{
		if (SaveData == null)
		{
			d.Assert(Coord != IntVector2.invalid);
			SaveData = Singleton.Manager<ManSaveGame>.inst.GetStoredTile(Coord);
		}
	}

	public void ReleaseSaveState(bool moveSaveDataToJSON = true)
	{
		if (SaveData != null)
		{
			if (moveSaveDataToJSON)
			{
				Singleton.Manager<ManSaveGame>.inst.CleanupStoredTile(SaveData);
			}
			SaveData = null;
		}
	}

	public void SetTerrain(Terrain terrain)
	{
		Terrain = terrain;
		StaticParent = (Terrain ? Terrain.transform.GetChild(0) : null);
		d.Assert(StaticParent == null || StaticParent.name == "static parent");
	}

	public Vector3 CalcSceneOrigin()
	{
		return Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene(Coord);
	}

	public Vector3 CalcSceneCentre()
	{
		return Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(Coord);
	}

	public ResourceDispenser GetResdisp(IntVector2 cellCoord)
	{
		ResourceDispenser result = null;
		foreach (KeyValuePair<int, Visible> item in Visibles[3])
		{
			if (item.Value.resdisp.cellCoord == cellCoord)
			{
				result = item.Value.resdisp;
				break;
			}
		}
		return result;
	}

	public void AddVisible(Visible visible)
	{
		if (visible.type == ObjectTypes.Null)
		{
			return;
		}
		try
		{
			Visibles[(int)visible.type].Add(visible.ID, visible);
			if (visible.type == ObjectTypes.Vehicle && Singleton.playerTank != null)
			{
				_ = visible.ID;
				_ = Singleton.playerTank.visible.ID;
			}
		}
		catch (ArgumentException)
		{
			d.LogError($"adding visible #{visible.ID} '{visible.name}': ID clash with '{Visibles[(int)visible.type][visible.ID].name}'");
		}
	}

	public void AddPersistentTerrainObject(TerrainObject terrainObject)
	{
		d.Assert(terrainObject.PrefabGUID != string.Empty, "WorldTile.AddPersistentTerrainObject - Trying to add Terrain Object that is not setup in TerrainObjectTable, and does not have prefab GUID assigned!");
		if (ManuallyAddedTerrainObjects == null)
		{
			ManuallyAddedTerrainObjects = new HashSet<TerrainObject>();
		}
		ManuallyAddedTerrainObjects.Add(terrainObject);
	}

	public void RemovePersistentTerrainObject(TerrainObject terrainObject)
	{
		d.Assert(terrainObject.PrefabGUID != string.Empty, "WorldTile.RemovePersistentTerrainObject - Trying to remove Terrain Object that is not setup in TerrainObjectTable, and does not have prefab GUID assigned!");
		if (ManuallyAddedTerrainObjects != null)
		{
			ManuallyAddedTerrainObjects.Remove(terrainObject);
		}
	}

	public void AddTerrainObjectNav(TerrainObject terrainObject, bool overlapsTileEdge)
	{
		Bounds sceneBounds = terrainObject.GetSceneBounds();
		Rect rect = new Rect(sceneBounds.min.ToVector2XZ(), sceneBounds.size.ToVector2XZ());
		rect.center -= CalcSceneOrigin().ToVector2XZ();
		m_ModifiedQuadTree.Insert(rect);
		if (overlapsTileEdge && !m_OverlappingTerrain.Contains(terrainObject))
		{
			m_OverlappingTerrain.Add(terrainObject);
		}
	}

	public void RemoveOverlappingTerrainObject(TerrainObject terrainObject)
	{
		m_OverlappingTerrain.Remove(terrainObject);
	}

	public void AddSceneryBlocker(SceneryBlocker blocker)
	{
		m_SceneryBlockers.Add(blocker);
	}

	public void RemoveSceneryBlocker(SceneryBlocker blocker)
	{
		m_SceneryBlockers.Remove(blocker);
	}

	public bool HasSceneryBlocker(SceneryBlocker blocker)
	{
		return m_SceneryBlockers.Contains(blocker);
	}

	public void RemoveVisible(Visible visible)
	{
		if (visible.type != ObjectTypes.Null)
		{
			Visibles[(int)visible.type].Remove(visible.ID);
			if (visible.type == ObjectTypes.Vehicle && Singleton.playerTank != null)
			{
				_ = visible.ID;
				_ = Singleton.playerTank.visible.ID;
			}
		}
	}

	public bool CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode mode, Vector3 scenePos, float radius)
	{
		bool result = false;
		foreach (SceneryBlocker sceneryBlocker in m_SceneryBlockers)
		{
			if (sceneryBlocker.IsBlockingPos(mode, scenePos, radius))
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public void AddOverlappingObjectsFromNeighbours()
	{
		IntVector2 intVector = Coord - IntVector2.one;
		IntVector2 intVector2 = Coord + IntVector2.one;
		for (int i = intVector.x; i <= intVector2.x; i++)
		{
			for (int j = intVector.y; j <= intVector2.y; j++)
			{
				if (i == Coord.x && j == Coord.y)
				{
					continue;
				}
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(new IntVector2(i, j));
				if (worldTile == null || !worldTile.IsCreated)
				{
					continue;
				}
				for (int k = 0; k < worldTile.m_OverlappingTerrain.Count; k++)
				{
					TerrainObject terrainObject = worldTile.m_OverlappingTerrain[k];
					if (terrainObject != null)
					{
						if (Coord.x <= terrainObject.TileCoordsMax.x && Coord.x >= terrainObject.TileCoordsMin.x && Coord.y <= terrainObject.TileCoordsMax.y && Coord.y >= terrainObject.TileCoordsMin.y)
						{
							AddTerrainObjectNav(terrainObject, overlapsTileEdge: true);
						}
					}
					else
					{
						d.LogErrorFormat("WorldTile.AddOverlappingObjectsFromNeighbours - TerrainObject on tile {0} with radius {1} was NULL but still present in the m_OverlappingTerrain list!", worldTile.Coord, ((object)terrainObject == null) ? "[NULL Object]" : terrainObject.GroundRadius.ToString());
					}
				}
			}
		}
	}

	public void AddResourceSource(Visible visible, ChunkTypes resourceType)
	{
		if (resourceType != ChunkTypes.Null)
		{
			if (!m_ResourcesOnTile.TryGetValue(resourceType, out var value))
			{
				value = new HashSet<Visible>();
				m_ResourcesOnTile[resourceType] = value;
			}
			value.Add(visible);
		}
	}

	public void RemoveResourceSource(Visible visible, ChunkTypes resourceType)
	{
		if (resourceType != ChunkTypes.Null && m_ResourcesOnTile.TryGetValue(resourceType, out var value))
		{
			value.Remove(visible);
		}
	}

	public Vector3 GetTerrainNormal(Vector3 scenePos)
	{
		Vector3 vector = scenePos - Terrain.transform.position;
		float value = vector.x / Terrain.terrainData.size.x;
		float value2 = vector.z / Terrain.terrainData.size.z;
		value = Mathf.Clamp(value, 0f, 1f);
		value2 = Mathf.Clamp(value2, 0f, 1f);
		return Terrain.terrainData.GetInterpolatedNormal(value, value2);
	}

	public float GetTerrainheight(Vector3 scenePos)
	{
		Vector3 vector = scenePos - Terrain.transform.position;
		float value = vector.x / Terrain.terrainData.size.x;
		float value2 = vector.z / Terrain.terrainData.size.z;
		value = Mathf.Clamp(value, 0f, 1f);
		value2 = Mathf.Clamp(value2, 0f, 1f);
		return Terrain.terrainData.GetInterpolatedHeight(value, value2);
	}

	public ref readonly BiomeMap.MapCell GetMapCell(Vector3 tileRelativePos)
	{
		return ref GetMapCell(tileRelativePos, BiomeMapData);
	}

	public static ref readonly BiomeMap.MapCell GetMapCell(Vector3 tileRelativePos, BiomeMap.MapData biomeMapData)
	{
		int num = (int)(tileRelativePos.x * invCellScale);
		int num2 = (int)(tileRelativePos.z * invCellScale);
		d.Assert(num >= 0 && num < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge && num2 >= 0 && num2 < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, "Cell coords out of range");
		return ref biomeMapData.cells[num + 1, num2 + 1];
	}

	public bool HasReachedLoadState(State state)
	{
		switch (state)
		{
		case State.Empty:
			return true;
		case State.Created:
			return IsCreated;
		case State.Populated:
			return IsPopulated;
		case State.Loaded:
			return IsLoaded;
		default:
			d.AssertFormat(false, "WorldTile.HasReachedState has unhandled state type {0}", state);
			return false;
		}
	}

	public bool HasReachedRequestedState()
	{
		return HasReachedLoadState(m_RequestState);
	}

	public bool StoreLoadedVisible(Visible visible)
	{
		bool result = false;
		if (IsLoaded || IsLoading)
		{
			ManSaveGame.StoredVisible storedVisible = ManSaveGame.CreateStoredVisible(visible);
			AddStoredVisibleWaitingToLoad(storedVisible);
			result = true;
		}
		else if (SaveData != null)
		{
			SaveData.StoreLoadedVisible(visible);
			result = true;
		}
		return result;
	}

	public void AddStoredVisibleWaitingToLoad(ManSaveGame.StoredVisible storedVisible)
	{
		if (StoredVisiblesWaitingToLoad == null)
		{
			StoredVisiblesWaitingToLoad = new List<ManSaveGame.StoredVisible>();
		}
		if (StoredVisiblesWaitingToLoad.Count == 0)
		{
			Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Subscribe(OnTileLoaded);
		}
		else
		{
			for (int i = 0; i < StoredVisiblesWaitingToLoad.Count; i++)
			{
				if (StoredVisiblesWaitingToLoad[i].m_ID == storedVisible.m_ID)
				{
					Visible visible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(storedVisible.m_ID)?.visible;
					d.LogErrorFormat("WorldTile.AddStoredVisibleWaitingToLoad: Trying to store a visible with ID that's already in the list id={0} {1}", storedVisible.m_ID, visible ? visible.name : string.Empty);
					StoredVisiblesWaitingToLoad.RemoveAt(i);
					break;
				}
			}
		}
		StoredVisiblesWaitingToLoad.Add(storedVisible);
	}

	public void RemoveStoredVisibleWaitingToLoad(int visibleId)
	{
		if (StoredVisiblesWaitingToLoad == null)
		{
			return;
		}
		for (int i = 0; i < StoredVisiblesWaitingToLoad.Count; i++)
		{
			if (StoredVisiblesWaitingToLoad[i].m_ID == visibleId)
			{
				StoredVisiblesWaitingToLoad.RemoveAt(i);
				break;
			}
		}
		if (StoredVisiblesWaitingToLoad.Count == 0)
		{
			Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnTileLoaded);
		}
	}

	private void OnTileLoaded(WorldTile loadedTile)
	{
		ManSaveGame.TryRestoreStoredVisibles(StoredVisiblesWaitingToLoad, Coord);
		if (StoredVisiblesWaitingToLoad.Count == 0)
		{
			Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnTileLoaded);
		}
	}

	public void CacheTileInfo()
	{
		if (m_TilePosData == null)
		{
			m_TilePosData = new List<TilePosInfo>(s_NumTileInfoPoints);
			for (int i = 0; i < s_NumTileInfoPoints; i++)
			{
				m_TilePosData.Add(new TilePosInfo());
			}
		}
		EnumValuesIterator<BiomeTypes> enumerator = EnumIterator<BiomeTypes>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			int current = (int)enumerator.Current;
			m_AverageBiomeWeights[current] = 0f;
			m_MaximumBiomeWeights[current] = 0f;
		}
		m_LargestFreeSpaceRadiusOnTile = 0;
		int tileInfoStepSize = Singleton.Manager<ManWorld>.inst.TileInfoStepSize;
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		int num = (int)tileSize % Singleton.Manager<ManWorld>.inst.TileInfoStepSize / 2;
		d.AssertFormat(num > 0, "WorldTile.CacheTileInfo - stepStartOffset is {0}. Tile positions will be on or very near to the edge of the Tile, which may cause issues down the line when it comes to rounding errors in WorldToTileCoordinate resulting in the wrong tile. Consider Changing TileInfoStepSize to leave a small remainder when dividing TileSize.", num);
		float num2 = 0f;
		int num3 = 0;
		m_BiomesInTile.Clear();
		for (int j = num; (float)j < tileSize; j += tileInfoStepSize)
		{
			for (int k = num; (float)k < tileSize; k += tileInfoStepSize)
			{
				TilePosInfo tilePosInfo = m_TilePosData[num3];
				num3++;
				tilePosInfo.Clear();
				tilePosInfo.tile = this;
				tilePosInfo.coordInTile = new Vector2(k, j);
				Vector3 vector = new Vector3(k, 0f, j);
				ManWorld.CachedBiomeBlendWeights cachedBiomeBlendWeights = new ManWorld.CachedBiomeBlendWeights(GetMapCell(vector));
				int numWeights = cachedBiomeBlendWeights.NumWeights;
				if (numWeights > 0)
				{
					for (int l = 0; l < numWeights; l++)
					{
						Biome biome = cachedBiomeBlendWeights.Biome(l);
						float num4 = cachedBiomeBlendWeights.Weight(l);
						if (biome != null && num4 > 0f)
						{
							BiomeTypes biomeType = biome.BiomeType;
							tilePosInfo.biomeWeighting[(int)biomeType] += num4;
							if (!tilePosInfo.biomeList.Contains(biome))
							{
								tilePosInfo.biomeList.Add(biome);
							}
							if (num4 > m_MaximumBiomeWeights[(int)biomeType])
							{
								m_MaximumBiomeWeights[(int)biomeType] = num4;
							}
							m_AverageBiomeWeights[(int)biomeType] += num4;
							num2 += num4;
							if (!m_BiomesInTile.Contains(biome))
							{
								m_BiomesInTile.Add(biome);
							}
						}
					}
				}
				int num5 = 0;
				for (int m = 0; m < kNumTestRadii; m++)
				{
					int num6 = kTileInfoFreeSpaceTestRadi[m];
					if (!IsAreaFree(vector, num6))
					{
						break;
					}
					num5 = num6;
					if (num5 > m_LargestFreeSpaceRadiusOnTile)
					{
						m_LargestFreeSpaceRadiusOnTile = num5;
					}
				}
				tilePosInfo.biggestFreeRadius = num5;
			}
		}
		enumerator = EnumIterator<BiomeTypes>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			int current2 = (int)enumerator.Current;
			m_AverageBiomeWeights[current2] /= num2;
		}
		HasTilePosInfo = true;
	}

	public float GetAverageBiomeWeight(BiomeTypes biomeType)
	{
		d.Assert(HasTilePosInfo, "GetAverageBiomeWeight - Called while CachedTileInfo wasn't created yet");
		return m_AverageBiomeWeights[(int)biomeType];
	}

	public bool HasAnyOfBiomeType(BiomeTypes biomeType)
	{
		d.Assert(HasTilePosInfo, "HasAnyOfBiomeType - Called while CachedTileInfo wasn't created yet");
		return m_AverageBiomeWeights[(int)biomeType] > 0f;
	}

	public bool HasBiome(Biome biome)
	{
		d.Assert(HasTilePosInfo, "HasBiome - Called while CachedTileInfo wasn't created yet");
		return m_BiomesInTile.Contains(biome);
	}

	public bool HasOtherBiomes(Biome[] biomes)
	{
		d.Assert(HasTilePosInfo, "HasOtherBiomes - Called while CachedTileInfo wasn't created yet");
		bool result = false;
		for (int i = 0; i < m_BiomesInTile.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < biomes.Length; j++)
			{
				if (m_BiomesInTile[i] == biomes[j])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	public float GetMaximumBiomeWeight(BiomeTypes biomeType)
	{
		d.Assert(HasTilePosInfo, "GetMaximumBiomeWeight - Called while CachedTileInfo wasn't created yet");
		return m_MaximumBiomeWeights[(int)biomeType];
	}

	public TilePosInfo GetNearestTilePosInfo(Vector3 scenePos)
	{
		d.Assert(HasTilePosInfo, "GetNearestTilePosInfo - Called while CachedTileInfo wasn't created yet");
		Vector3 vector = scenePos - Terrain.transform.position;
		int num = Mathf.Clamp(Mathf.RoundToInt(vector.x / (float)Singleton.Manager<ManWorld>.inst.TileInfoStepSize), 0, s_NumTileInfoPointsPerSide - 1);
		int value = Mathf.Clamp(Mathf.RoundToInt(vector.z / (float)Singleton.Manager<ManWorld>.inst.TileInfoStepSize), 0, s_NumTileInfoPointsPerSide - 1) * s_NumTileInfoPointsPerSide + num;
		value = Mathf.Clamp(value, 0, s_NumTileInfoPoints - 1);
		return m_TilePosData[value];
	}

	public TileInfoIterator IterateTileInfo()
	{
		d.Assert(HasTilePosInfo, "IterateTileInfo - Called while CachedTileInfo wasn't created yet");
		return new TileInfoIterator(m_TilePosData, IntVector2.zero);
	}

	public TileInfoIterator IterateTileInfo(IntVector2 startingPos, bool iterateForward = true)
	{
		d.Assert(HasTilePosInfo, "IterateTileInfo - Called while CachedTileInfo wasn't created yet");
		return new TileInfoIterator(m_TilePosData, startingPos, iterateForward);
	}

	public bool IsAreaFree(Vector3 posRelative, int radius)
	{
		bool flag = IsAreaWalkable(posRelative.ToVector2XZ(), radius);
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		if (flag && (posRelative.x - (float)radius < 0f || posRelative.x + (float)radius >= tileSize || posRelative.z - (float)radius < 0f || posRelative.z + (float)radius >= tileSize))
		{
			Vector3 vector = CalcSceneOrigin();
			Bounds sceneBounds = new Bounds(vector + posRelative, Vector3.one * radius * 2f);
			Singleton.Manager<ManWorld>.inst.TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
			for (int i = min.x; i <= max.x; i++)
			{
				for (int j = min.y; j <= max.y; j++)
				{
					IntVector2 coord = new IntVector2(i, j);
					if (coord != Coord)
					{
						WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord);
						Vector2 tileRelativePos = posRelative.ToVector2XZ();
						tileRelativePos += (Vector2)(Coord - coord) * tileSize;
						if (worldTile == null || !worldTile.HasReachedLoadState(State.Populated) || !worldTile.IsAreaWalkable(tileRelativePos, radius))
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					break;
				}
			}
		}
		return flag;
	}

	private bool IsAreaWalkable(Vector2 tileRelativePos, float areaRadius)
	{
		int x = (int)(tileRelativePos.x / (float)Singleton.Manager<ManPath>.inst.GridSquareSize);
		int y = (int)(tileRelativePos.y / (float)Singleton.Manager<ManPath>.inst.GridSquareSize);
		return m_ModifiedQuadTree.IsWalkable(x, y, Mathf.RoundToInt(areaRadius));
	}

	public void DrawGizmos(float tileSize)
	{
		Vector3 vector = new Vector3(tileSize, 0f, tileSize);
		Vector3 vector2 = CalcSceneOrigin() + Vector3.up * 100f;
		if (IsCreated)
		{
			Gizmos.color = (IsLoaded ? Color.green : (IsPopulated ? Color.yellow : Color.green.ScaleRGB(0.3f)));
			Gizmos.DrawWireCube(vector2 + vector * 0.5f, vector * 0.995f);
		}
		Gizmos.color = Color.cyan;
		foreach (SceneryBlocker sceneryBlocker in m_SceneryBlockers)
		{
			sceneryBlocker.DrawGizmos();
		}
	}
}
