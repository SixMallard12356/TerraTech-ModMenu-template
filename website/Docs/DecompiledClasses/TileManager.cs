#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class TileManager
{
	public enum CreationStage
	{
		None,
		PreCreate,
		PrePopulate
	}

	public enum UnloadBehaviour
	{
		Default,
		MultiplayerHost,
		ExternallyControlled
	}

	private enum ResponsibleThread
	{
		None,
		Main,
		Generation
	}

	public enum SceneryPatchType
	{
		PopulatedTileScenery,
		OptionalSceneryClutter
	}

	private struct TerrainDeferredLOD
	{
		public Terrain m_Terrain;

		public float[] m_OverrideHeightError;
	}

	public struct SpawnData
	{
		public TerrainObject prefab;

		public IntVector2 cellCoord;

		public Vector3 position;

		public Quaternion rotHV;

		public float scale;

		public bool ignoreBlockers;

		public SpawnData(TerrainObject prefab, IntVector2 cellCoord, Vector3 position, Vector2 rotHV, float scale, bool ignoreBlockers = false)
			: this(prefab, cellCoord, position, new Quaternion(rotHV.x, rotHV.y, 0f, 8f), scale, ignoreBlockers)
		{
		}

		public SpawnData(TerrainObject prefab, IntVector2 cellCoord, Vector3 position, Quaternion rotHV, float scale, bool ignoreBlockers = false)
		{
			this.prefab = prefab;
			this.cellCoord = cellCoord;
			this.position = position;
			this.rotHV = rotHV;
			this.scale = scale;
			this.ignoreBlockers = ignoreBlockers;
		}

		public bool HasFullRotation()
		{
			return rotHV.w < 2f;
		}
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	private class SceneryPatch
	{
		private struct CombineInstanceGPU
		{
			public readonly Mesh mesh;

			public readonly Vector2 tileRelative;

			public readonly int cellIndex;

			public readonly int meshId;

			public readonly int numInstances;

			public readonly TerrainObjectBehaviour data;

			public readonly Matrix4x4 transform;

			public CombineInstanceGPU(Vector2 tileRelative, int cellIndex, Mesh mesh, Matrix4x4 transform, TerrainObjectBehaviour data)
			{
				this.mesh = mesh;
				this.tileRelative = tileRelative;
				this.cellIndex = cellIndex;
				meshId = mesh.GetInstanceID();
				this.data = data;
				this.transform = transform;
				if (data == null)
				{
					numInstances = 1;
				}
				else
				{
					numInstances = UnityEngine.Random.Range(data.MinInstances, data.MaxInstances + 1);
				}
			}
		}

		private SpawnData[] m_ObjectsToSpawn;

		private bool m_MergeMeshes;

		private bool m_InstancedClutter;

		private static List<CombineInstanceGPU> s_CombineInstanceListGPU = new List<CombineInstanceGPU>();

		private static List<CombineInstance> s_CombineInstanceList = new List<CombineInstance>();

		public WorldTile Tile { get; private set; }

		public int TileSpawnID { get; private set; }

		public int TileScenerySpawnID { get; private set; }

		public IntVector2 TileCoord { get; private set; }

		public Vector3 Centre { get; private set; }

		public int StorageSize => m_ObjectsToSpawn.Length;

		public bool TileValid
		{
			get
			{
				if (Tile != null && Tile.Coord == TileCoord && Tile.SpawnID == TileSpawnID)
				{
					return Tile.ScenerySpawnID == TileScenerySpawnID;
				}
				return false;
			}
		}

		public SceneryPatch(int storageSize)
		{
			m_ObjectsToSpawn = new SpawnData[storageSize];
		}

		public void Init(WorldTile t, Vector3 worldCentre, List<SpawnData> spawnList, bool mergeMeshes, bool instancedClutter)
		{
			d.Assert(spawnList.Count <= m_ObjectsToSpawn.Length);
			Tile = t;
			TileSpawnID = Tile.SpawnID;
			TileScenerySpawnID = Tile.ScenerySpawnID;
			TileCoord = Tile.Coord;
			Centre = worldCentre;
			spawnList.CopyTo(m_ObjectsToSpawn);
			m_MergeMeshes = mergeMeshes;
			m_InstancedClutter = instancedClutter;
			if (spawnList.Count != m_ObjectsToSpawn.Length)
			{
				m_ObjectsToSpawn[spawnList.Count].prefab = null;
			}
		}

		private int CameraDistanceHeuristic()
		{
			return (int)((Centre - Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition).ToVector2XZ().sqrMagnitude * 0.00066666666f);
		}

		public static int CompareDist(SceneryPatch a, SceneryPatch b)
		{
			return a.CameraDistanceHeuristic() - b.CameraDistanceHeuristic();
		}

		private static void CollectInstancesGPU(Vector2 tileRelative, int cellIndex, Transform prefabTransform, Matrix4x4 matrix, TerrainObjectBehaviour data, List<CombineInstanceGPU> instances, Renderer[] filterRenderers = null)
		{
			if (filterRenderers == null)
			{
				LODGroup component = prefabTransform.GetComponent<LODGroup>();
				if (component != null)
				{
					if (QualitySettings.maximumLODLevel >= component.lodCount)
					{
						return;
					}
					filterRenderers = component.GetLODs()[QualitySettings.maximumLODLevel].renderers;
				}
			}
			TerrainObjectBehaviour component2 = prefabTransform.gameObject.GetComponent<TerrainObjectBehaviour>();
			if ((bool)component2)
			{
				data = component2;
			}
			MeshRenderer component3 = prefabTransform.GetComponent<MeshRenderer>();
			if (component3 != null && (filterRenderers == null || Array.IndexOf(filterRenderers, component3) != -1))
			{
				instances.Add(new CombineInstanceGPU(tileRelative, cellIndex, component3.GetComponent<MeshFilter>().sharedMesh, matrix, data));
			}
			for (int i = 0; i < prefabTransform.childCount; i++)
			{
				Transform child = prefabTransform.GetChild(i);
				CollectInstancesGPU(tileRelative, cellIndex, child, matrix * Matrix4x4.TRS(child.localPosition, child.localRotation, child.localScale), data, instances, filterRenderers);
			}
		}

		private static void CollectCombineInstances(Transform prefabTransform, Matrix4x4 matrix, List<CombineInstance> instances, Renderer[] filterRenderers = null)
		{
			if (filterRenderers == null)
			{
				LODGroup component = prefabTransform.GetComponent<LODGroup>();
				if (component != null)
				{
					if (QualitySettings.maximumLODLevel >= component.lodCount)
					{
						return;
					}
					filterRenderers = component.GetLODs()[QualitySettings.maximumLODLevel].renderers;
				}
			}
			MeshRenderer component2 = prefabTransform.GetComponent<MeshRenderer>();
			if (component2 != null && (filterRenderers == null || Array.IndexOf(filterRenderers, component2) != -1))
			{
				instances.Add(new CombineInstance
				{
					mesh = component2.GetComponent<MeshFilter>().sharedMesh,
					transform = matrix
				});
			}
			for (int i = 0; i < prefabTransform.childCount; i++)
			{
				Transform child = prefabTransform.GetChild(i);
				CollectCombineInstances(child, matrix * Matrix4x4.TRS(child.localPosition, child.localRotation, child.localScale), instances, filterRenderers);
			}
		}

		public void SpawnAll()
		{
			Vector3 vector = Tile.CalcSceneOrigin();
			SpawnData[] objectsToSpawn = m_ObjectsToSpawn;
			for (int i = 0; i < objectsToSpawn.Length; i++)
			{
				SpawnData spawnData = objectsToSpawn[i];
				if (spawnData.prefab == null)
				{
					break;
				}
				Vector3 vector2 = vector + spawnData.position;
				Vector2 vector3 = spawnData.position.ToVector2XZ() / Singleton.Manager<ManWorld>.inst.TileSize;
				if (spawnData.ignoreBlockers || !Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Spawn, vector2, 0f))
				{
					Quaternion quaternion = ((!spawnData.HasFullRotation()) ? spawnData.prefab.GetNormalWeightedSpawnOrientation(Tile, vector3, new Vector2(spawnData.rotHV.x, spawnData.rotHV.y)) : spawnData.rotHV);
					if (m_InstancedClutter)
					{
						CollectInstancesGPU(vector3, spawnData.cellCoord.y * Singleton.Manager<ManWorld>.inst.CellsPerTileEdge + spawnData.cellCoord.x, spawnData.prefab.transform, Matrix4x4.TRS(vector2, quaternion, Vector3.one * spawnData.scale), null, s_CombineInstanceListGPU);
					}
					else if (m_MergeMeshes)
					{
						vector2.y += Tile.BiomeMapData.terrainData.GetInterpolatedHeight(vector3.x, vector3.y);
						CollectCombineInstances(spawnData.prefab.transform, Matrix4x4.TRS(vector2 - vector, quaternion, Vector3.one * spawnData.scale), s_CombineInstanceList);
					}
					else
					{
						vector2.y += Tile.BiomeMapData.terrainData.GetInterpolatedHeight(vector3.x, vector3.y);
						spawnData.prefab.SpawnFromPrefab(Tile, vector2, quaternion, spawnData.scale, spawnData.cellCoord);
					}
				}
			}
			if (m_InstancedClutter)
			{
				if (s_CombineInstanceListGPU.Count > 0)
				{
					CreateInstancedScenery(Tile, s_CombineInstanceListGPU);
					s_CombineInstanceListGPU.Clear();
				}
			}
			else if (m_MergeMeshes && s_CombineInstanceList.Count > 0)
			{
				CreateBatchedScenery(s_CombineInstanceList);
				s_CombineInstanceList.Clear();
			}
		}

		private void CreateInstancedScenery(WorldTile tile, List<CombineInstanceGPU> combineInstance)
		{
			combineInstance.Sort((CombineInstanceGPU a, CombineInstanceGPU b) => (a.meshId != b.meshId) ? (a.meshId - b.meshId) : (a.cellIndex - b.cellIndex));
			Mesh mesh = null;
			int num = 0;
			int firstInstance = 0;
			int num2 = 0;
			for (int num3 = 0; num3 < combineInstance.Count; num3++)
			{
				Mesh mesh2 = combineInstance[num3].mesh;
				if (mesh != mesh2 || combineInstance[num3].numInstances + num2 > 1023)
				{
					if (num > 0)
					{
						CreateInstancedSceneryBatch(tile, combineInstance, firstInstance, num, num2, mesh);
					}
					firstInstance = num3;
					num = 1;
					num2 = combineInstance[num3].numInstances;
					mesh = mesh2;
				}
				else
				{
					num++;
					num2 += combineInstance[num3].numInstances;
				}
			}
			if (num > 0)
			{
				CreateInstancedSceneryBatch(tile, combineInstance, firstInstance, num, num2, mesh);
			}
		}

		private void CreateInstancedSceneryBatch(WorldTile tile, List<CombineInstanceGPU> combineInstance, int firstInstance, int numInstances, int numMeshes, Mesh mesh)
		{
			Transform transform = Singleton.Manager<ManWorld>.inst.m_InstancedSceneryPrefab.SpawnWithLocalTransform(Tile.StaticParent, Vector3.zero, Quaternion.identity);
			transform.gameObject.name = $"{Singleton.Manager<ManWorld>.inst.m_InstancedSceneryPrefab.name}_{mesh.name} ({numInstances})";
			InstancedSceneryBatch component = transform.GetComponent<InstancedSceneryBatch>();
			Matrix4x4[] array = new Matrix4x4[numMeshes];
			float num = 0f;
			int num2 = 0;
			for (int i = 0; i < numInstances; i++)
			{
				CombineInstanceGPU combineInstanceGPU = combineInstance[firstInstance + i];
				if (combineInstanceGPU.numInstances <= 0)
				{
					continue;
				}
				float num3 = 0f;
				if ((bool)combineInstanceGPU.data)
				{
					num3 = combineInstanceGPU.data.WindSwayScale;
					num = Mathf.Max(num, num3);
				}
				if (combineInstanceGPU.numInstances > 1)
				{
					float num4 = Singleton.Manager<ManWorld>.inst.CellScale / Singleton.Manager<ManWorld>.inst.TileSize;
					for (int j = 0; j < combineInstanceGPU.numInstances; j++)
					{
						array[num2] = combineInstanceGPU.transform;
						float num5 = UnityEngine.Random.Range(-0.5f, 0.5f) * combineInstanceGPU.data.SpreadFactor;
						float num6 = UnityEngine.Random.Range(-0.5f, 0.5f) * combineInstanceGPU.data.SpreadFactor;
						array[num2][0, 3] += num5 * Singleton.Manager<ManWorld>.inst.CellScale;
						array[num2][1, 3] += tile.BiomeMapData.terrainData.GetInterpolatedHeight(combineInstanceGPU.tileRelative.x + num5 * num4, combineInstanceGPU.tileRelative.y + num6 * num4);
						array[num2][2, 3] += num6 * Singleton.Manager<ManWorld>.inst.CellScale;
						array[num2][0, 1] = num3;
						num2++;
					}
				}
				else
				{
					array[num2] = combineInstanceGPU.transform;
					array[num2][1, 3] += tile.BiomeMapData.terrainData.GetInterpolatedHeight(combineInstanceGPU.tileRelative.x, combineInstanceGPU.tileRelative.y);
					array[num2][0, 1] = num3;
					num2++;
				}
			}
			component.Init(mesh, array, num > 0f);
		}

		private void CreateBatchedScenery(List<CombineInstance> combineInstance)
		{
			int num = 65535;
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < combineInstance.Count; i++)
			{
				int vertexCount = combineInstance[i].mesh.vertexCount;
				if (num2 + vertexCount > num)
				{
					CombineInstance[] array = new CombineInstance[i - num3];
					combineInstance.CopyTo(num3, array, 0, array.Length);
					CreateBatchedSceneryInstance(array);
					num3 = i;
					num2 = 0;
				}
				num2 += vertexCount;
			}
			if (combineInstance.Count > num3)
			{
				CombineInstance[] array2 = new CombineInstance[combineInstance.Count - num3];
				combineInstance.CopyTo(num3, array2, 0, array2.Length);
				CreateBatchedSceneryInstance(array2);
			}
		}

		private Transform CreateBatchedSceneryInstance(CombineInstance[] instances)
		{
			Transform transform = Singleton.Manager<ManWorld>.inst.m_BatchSceneryPrefab.SpawnWithLocalTransform(Tile.StaticParent, Vector3.zero, Quaternion.identity);
			MeshFilter component = transform.GetComponent<MeshFilter>();
			component.sharedMesh.CombineMeshes(instances);
			LODGroup component2 = transform.GetComponent<LODGroup>();
			if (component2 != null)
			{
				component2.localReferencePoint = component.sharedMesh.bounds.center;
				component2.size = Singleton.Manager<ManWorld>.inst.CellScale * (float)Singleton.Manager<ManWorld>.inst.CellsPerSceneryMergePatchEdge;
			}
			return transform;
		}
	}

	private struct SceneryPatchPool
	{
		public Stack<SceneryPatch> patches;

		public int maxPrefabs;
	}

	public struct TileCache
	{
		public WorldTile tile;

		public float nextUpdateTime;

		public IntVector2 tileOverlapDirection;

		public void Reset()
		{
			tile = null;
			nextUpdateTime = 0f;
			tileOverlapDirection = IntVector2.zero;
		}
	}

	public struct VisibleIterator
	{
		private IntVector2 rangeMin;

		private IntVector2 rangeMax;

		private IntVector2 currentCoord;

		private Dictionary<IntVector2, WorldTile>.ValueCollection.Enumerator tileEnumerator;

		private Dictionary<int, Visible>.ValueCollection.Enumerator visibleEnumerator;

		private Dictionary<IntVector2, WorldTile> tileStore;

		private ObjectTypes visibleType;

		private bool first;

		public Visible Current => visibleEnumerator.Current;

		public VisibleIterator(ObjectTypes type, Dictionary<IntVector2, WorldTile> tiles, IntVector2 min, IntVector2 max)
		{
			visibleType = type;
			tileStore = tiles;
			rangeMin = min;
			rangeMax = max;
			currentCoord = new IntVector2(rangeMin.x - 1, rangeMin.y);
			tileEnumerator = default(Dictionary<IntVector2, WorldTile>.ValueCollection.Enumerator);
			visibleEnumerator = default(Dictionary<int, Visible>.ValueCollection.Enumerator);
			first = true;
		}

		public VisibleIterator(ObjectTypes type, Dictionary<IntVector2, WorldTile> tiles)
		{
			visibleType = type;
			tileStore = tiles;
			rangeMin = IntVector2.invalid;
			rangeMax = IntVector2.invalid;
			currentCoord = IntVector2.invalid;
			tileEnumerator = tiles.Values.GetEnumerator();
			visibleEnumerator = default(Dictionary<int, Visible>.ValueCollection.Enumerator);
			first = true;
		}

		public VisibleIterator GetEnumerator()
		{
			return this;
		}

		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		[Il2CppSetOption(Option.NullChecks, false)]
		public bool MoveNext()
		{
			if (first || !visibleEnumerator.MoveNext())
			{
				first = false;
				do
				{
					if (currentCoord == IntVector2.invalid)
					{
						do
						{
							if (!tileEnumerator.MoveNext())
							{
								return false;
							}
						}
						while (!tileEnumerator.Current.IsLoaded);
						visibleEnumerator = tileEnumerator.Current.Visibles[(int)visibleType].Values.GetEnumerator();
						continue;
					}
					WorldTile value = null;
					do
					{
						currentCoord.x++;
						if (currentCoord.x > rangeMax.x)
						{
							currentCoord.x = rangeMin.x;
							currentCoord.y++;
							if (currentCoord.y > rangeMax.y)
							{
								return false;
							}
						}
						tileStore.TryGetValue(currentCoord, out value);
					}
					while (value == null || !value.IsLoaded);
					visibleEnumerator = value.Visibles[(int)visibleType].Values.GetEnumerator();
				}
				while (!visibleEnumerator.MoveNext());
			}
			return true;
		}
	}

	public struct TileIterator
	{
		private Dictionary<IntVector2, WorldTile> tileLookup;

		private Dictionary<IntVector2, WorldTile>.ValueCollection.Enumerator tileEnumerator;

		private WorldTile.State minState;

		private IntVector2 coordMin;

		private IntVector2 coordMax;

		private IntVector2 coordCurrent;

		public WorldTile Current { get; private set; }

		public TileIterator(Dictionary<IntVector2, WorldTile> tiles, WorldTile.State minimumState, IntVector2 min, IntVector2 max)
		{
			tileLookup = tiles;
			minState = minimumState;
			if (min != IntVector2.invalid && max != IntVector2.invalid && max.x >= min.x && max.y >= min.y)
			{
				coordMin = min;
				coordMax = max;
				coordCurrent = new IntVector2(coordMin.x - 1, coordMin.y);
				tileEnumerator = default(Dictionary<IntVector2, WorldTile>.ValueCollection.Enumerator);
			}
			else
			{
				coordMin = IntVector2.invalid;
				coordMax = IntVector2.invalid;
				coordCurrent = IntVector2.invalid;
				tileEnumerator = tiles.Values.GetEnumerator();
			}
			Current = null;
		}

		public TileIterator GetEnumerator()
		{
			return this;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public bool MoveNext()
		{
			do
			{
				if (coordMin == IntVector2.invalid)
				{
					if (!tileEnumerator.MoveNext())
					{
						return false;
					}
					Current = tileEnumerator.Current;
					continue;
				}
				do
				{
					if (++coordCurrent.x > coordMax.x)
					{
						coordCurrent.x = coordMin.x;
						if (++coordCurrent.y > coordMax.y)
						{
							return false;
						}
					}
					tileLookup.TryGetValue(coordCurrent, out var value);
					Current = value;
				}
				while (Current == null);
			}
			while (!Current.HasReachedLoadState(minState));
			return true;
		}
	}

	public struct TilesWithinRadius
	{
		private float rangeSqr;

		private Vector2 posXZ;

		private Util.CoordIterator coordIterator;

		private static readonly float kTileHalfDiagonal = Mathf.Sqrt(Singleton.Manager<ManWorld>.inst.TileSize * Singleton.Manager<ManWorld>.inst.TileSize * 2f) * 0.5f;

		public IntVector2 Current { get; private set; }

		public TilesWithinRadius(Vector3 scenePos, float range, bool centreOnly = false)
		{
			float num = (centreOnly ? range : (range + kTileHalfDiagonal));
			Bounds sceneBounds = new Bounds(scenePos, new Vector3(num * 2f, 0f, num * 2f));
			Singleton.Manager<ManWorld>.inst.TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
			rangeSqr = num * num;
			posXZ = scenePos.ToVector2XZ();
			coordIterator = new Util.CoordIterator(min, max);
			Current = default(IntVector2);
		}

		public TilesWithinRadius GetEnumerator()
		{
			return this;
		}

		[Il2CppSetOption(Option.NullChecks, false)]
		[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
		public bool MoveNext()
		{
			while (coordIterator.MoveNext())
			{
				if ((Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(coordIterator.Current).ToVector2XZ() - posXZ).sqrMagnitude < rangeSqr)
				{
					Current = coordIterator.Current;
					return true;
				}
			}
			return false;
		}
	}

	public Event<WorldTile> TileCreatedEvent;

	public Event<WorldTile> TileStartPopulatingEvent;

	public Event<WorldTile> TilePopulatedEvent;

	public Event<WorldTile> TileDepopulatedEvent;

	public Event<WorldTile> TileDestroyedEvent;

	public Event<WorldTile> TileLoadedEvent;

	public Event<WorldTile> TileUnloadedEvent;

	public Event<Visible, WorldTile, WorldTile> VisibleChangedTileEvent;

	private Dictionary<IntVector2, WorldTile> m_TileLookup = new Dictionary<IntVector2, WorldTile>();

	private Stack<WorldTile> m_TilePool = new Stack<WorldTile>();

	private int m_TilePoolDimension;

	private List<IntVector2> m_FixedTilesLoaded = new List<IntVector2>();

	private List<IntVector2> m_FixedTilesUnpopulated = new List<IntVector2>();

	private List<SceneryPatch>[] m_SceneryPatchesToGenerate = new List<SceneryPatch>[2];

	private Dictionary<int, SceneryPatchPool> m_PatchPools = new Dictionary<int, SceneryPatchPool>();

	private bool m_NeedSortSceneryPatches;

	private SceneryPatch m_CurrentPopulatingPatch;

	private Transform m_TerrainTemplate;

	private IntVector2[] m_TileRefUpdateFrequencyTable;

	private bool m_ClearAll;

	private bool m_PauseGenerationOneFrame;

	private List<IntVector2> m_TileCoordsToCreateWorking = new List<IntVector2>();

	private List<WorldTile> m_TilesToRecycleWorking = new List<WorldTile>();

	private List<SpawnData> m_SpawnListWorking = new List<SpawnData>();

	private Bounds physicsBounds;

	public bool m_DisableSceneryObjects;

	private UnloadBehaviour m_UnloadBehaviour;

	private ThreadedJobProcessor m_GenerationThread = new ThreadedJobProcessor();

	private DRNG m_DRNG = new DRNG();

	private IntVector2 m_LastFocalCoord = new IntVector2(0, 0);

	private bool m_HasLastFocalCoord;

	private List<WorldTile> m_TilesToCacheTileInfoFor = new List<WorldTile>();

	private List<TerrainDeferredLOD> m_DelayedTerrainHeightmapModificationList = new List<TerrainDeferredLOD>();

	private Dictionary<IntVector2, NetWorldTile> m_NetWorldTiles = new Dictionary<IntVector2, NetWorldTile>();

	private Stopwatch m_WorkloadStopwatch = new Stopwatch();

	private const float k_InverseSceneryPatchSortingGranularity = 0.00066666666f;

	private const int patchSortTimeoutFrames = 25;

	private int patchSortCounter = 25;

	private const int kWorkBudgetMSPerFrameBackground = 10;

	private const int kWorkBudgetMSPerFrameLoading = 100;

	private const float k_BackgroundSceneryGenTimePerFrame = 0.004f;

	private const float k_MaxSceneryGenTimePerFrame = 0.02f;

	private const float k_LoadingSceneryGenTimePerFrame = 0.1f;

	private const int k_InitTilePoolSize = 15;

	private const float k_TileCacheMaxSpeed = 100f;

	private static readonly int[] k_TileCacheUpdateIntervalThresholds = new int[2] { 5, 25 };

	public const float k_TerrainHeightRange = 100f;

	private const int k_MinimumPatchStorageListSize = 8;

	private const int k_InitialPatchPoolSize = 16;

	private static int s_ScenerySpawnIDCounter = 0;

	private const int k_BroadphaseSubdivisions = 16;

	private const float kPrecisionBias = 0.0001f;

	private static readonly float kInvTileSize = 1f / Singleton.Manager<ManWorld>.inst.TileSize;

	private StringBuilder m_DebugStoredTileCoords = new StringBuilder();

	public bool IsGenerating { get; private set; }

	public bool IsClearing => m_ClearAll;

	public bool IsCleared => m_TileLookup.Count == 0;

	public bool IsPopulating => m_CurrentPopulatingPatch != null;

	public WorldTile CurrentPopulatingPatchTile
	{
		get
		{
			if (m_CurrentPopulatingPatch == null)
			{
				return null;
			}
			return m_CurrentPopulatingPatch.Tile;
		}
	}

	public void Init()
	{
		InitTemplate();
		for (int i = 0; i < m_SceneryPatchesToGenerate.Length; i++)
		{
			m_SceneryPatchesToGenerate[i] = new List<SceneryPatch>();
		}
		List<IntVector2> list = new List<IntVector2>();
		int[] array = k_TileCacheUpdateIntervalThresholds;
		foreach (int num in array)
		{
			float f = 100f * (float)num * Time.fixedDeltaTime;
			list.Add(new IntVector2(Mathf.CeilToInt(f), num));
		}
		m_TileRefUpdateFrequencyTable = list.ToArray();
		m_GenerationThread.Initialise("TileManager", UpdateGeneration);
		m_GenerationThread.Start();
		QualitySettingsExtended.QualitySettingChangedEvent.Subscribe(OnQualitySettingUpdated);
	}

	public void Terminate()
	{
		m_GenerationThread.Terminate(waitForExit: false);
		Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
		while (enumerator.MoveNext())
		{
			WorldTile value = enumerator.Current.Value;
			if (value.IsLoaded)
			{
				value.SaveData.SetSceneryAwake(value.Visibles, awake: false);
			}
			DestroyTile(value);
			ReturnTileToPool(value);
		}
		LogPatchPoolSizes();
	}

	public void Reset(bool clearFixedTiles = true)
	{
		m_GenerationThread.Stop();
		IsGenerating = true;
		m_ClearAll = true;
		m_UnloadBehaviour = UnloadBehaviour.Default;
		if (clearFixedTiles)
		{
			ClearFixedTiles();
		}
		BiomeMap.ResetGenerationBuffers();
		List<SceneryPatch>[] sceneryPatchesToGenerate = m_SceneryPatchesToGenerate;
		foreach (List<SceneryPatch> list in sceneryPatchesToGenerate)
		{
			lock (list)
			{
				foreach (SceneryPatch item in list)
				{
					ReturnPatchToPool(item);
				}
				list.Clear();
			}
		}
		m_HasLastFocalCoord = false;
		m_LastFocalCoord = Vector2.zero;
		m_NetWorldTiles.Clear();
		m_GenerationThread.Start();
	}

	public void Update()
	{
		if (m_PauseGenerationOneFrame)
		{
			m_PauseGenerationOneFrame = false;
			return;
		}
		IsGenerating = m_ClearAll && Singleton.Manager<ManWorld>.inst.CurrentBiomeMap != null;
		m_TileCoordsToCreateWorking.Clear();
		UpdateTileRequestStates(m_TileCoordsToCreateWorking);
		if (m_TileCoordsToCreateWorking.Count != 0)
		{
			IsGenerating = true;
		}
		m_WorkloadStopwatch.Restart();
		int workBudget = GetWorkBudget();
		HandleTileUnloading(m_WorkloadStopwatch, workBudget);
		lock (m_TileLookup)
		{
			RemoveOldTiles();
			if (m_ClearAll && m_TileLookup.Count == 0)
			{
				m_ClearAll = false;
			}
			CreateNewTiles();
		}
		HandleTileLoading(m_WorkloadStopwatch, workBudget);
		if (!IsGenerating)
		{
			bool flag = true;
			while (flag && m_WorkloadStopwatch.ElapsedMilliseconds < workBudget)
			{
				flag = false;
				if (m_TilesToCacheTileInfoFor.Count > 0)
				{
					m_TilesToCacheTileInfoFor[0].CacheTileInfo();
					m_TilesToCacheTileInfoFor.RemoveAt(0);
					flag = true;
				}
				else if (m_DelayedTerrainHeightmapModificationList.Count > 0)
				{
					TerrainDeferredLOD terrainDeferredLOD = m_DelayedTerrainHeightmapModificationList[0];
					m_DelayedTerrainHeightmapModificationList.RemoveAt(0);
					terrainDeferredLOD.m_Terrain.ApplyDelayedHeightmapModification();
					if (terrainDeferredLOD.m_OverrideHeightError != null)
					{
						float[] maximumHeightError = terrainDeferredLOD.m_Terrain.terrainData.GetMaximumHeightError();
						d.Assert(maximumHeightError.Length == terrainDeferredLOD.m_OverrideHeightError.Length, $"Mismatched lengths for terrain error {maximumHeightError.Length} vs {terrainDeferredLOD.m_OverrideHeightError.Length}");
						for (int i = 0; i < maximumHeightError.Length; i++)
						{
							maximumHeightError[i] = Mathf.Max(maximumHeightError[i], terrainDeferredLOD.m_OverrideHeightError[i]);
						}
						terrainDeferredLOD.m_Terrain.terrainData.OverrideMaximumHeightError(maximumHeightError);
					}
					flag = true;
				}
				else
				{
					flag = PopulateSceneryPatches(SceneryPatchType.OptionalSceneryClutter);
				}
			}
		}
		m_WorkloadStopwatch.Stop();
	}

	public void SetUnloadBehaviour(UnloadBehaviour b)
	{
		m_UnloadBehaviour = b;
	}

	public List<IntVector2> GetLoadedTiles()
	{
		List<IntVector2> list = new List<IntVector2>();
		foreach (KeyValuePair<IntVector2, WorldTile> item in m_TileLookup)
		{
			if (item.Value.IsLoaded)
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	public List<IntVector2> GetCreatedTiles()
	{
		List<IntVector2> list = new List<IntVector2>();
		foreach (KeyValuePair<IntVector2, WorldTile> item in m_TileLookup)
		{
			if (item.Value.IsCreated)
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	public bool IsTileUsableForNewSetPiece(IntVector2 tileCoord)
	{
		if (m_TileLookup.TryGetValue(tileCoord, out var value) && value.m_LoadStep != WorldTile.LoadStep.Empty)
		{
			return false;
		}
		return true;
	}

	private void OnQualitySettingUpdated()
	{
		int terrainBaseMapDistance = QualitySettingsExtended.TerrainBaseMapDistance;
		float num = QualitySettingsExtended.TerrainLODPixelError;
		Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
		while (enumerator.MoveNext())
		{
			WorldTile value = enumerator.Current.Value;
			if (value != null && value.Terrain != null)
			{
				value.Terrain.basemapDistance = terrainBaseMapDistance;
				value.Terrain.heightmapPixelError = num;
			}
		}
		Globals.inst.m_TerrainBaseMapDistance = terrainBaseMapDistance;
		Globals.inst.m_TerrainLODPixelError = (int)num;
	}

	public void DrawGizmos()
	{
		if (Globals.inst.DynamicMultiBoxBroadphaseRegions)
		{
			Color color = Gizmos.color;
			Gizmos.color = Color.magenta;
			Gizmos.DrawWireCube(physicsBounds.center, physicsBounds.size);
			Gizmos.color = new Color(1f, 0.5f, 1f, 1f);
			for (int i = 1; i < 16; i++)
			{
				float z = physicsBounds.min.z + physicsBounds.size.z * (float)i / 16f;
				Gizmos.DrawLine(new Vector3(physicsBounds.min.x, physicsBounds.center.y, z), new Vector3(physicsBounds.max.x, physicsBounds.center.y, z));
				float x = physicsBounds.min.x + physicsBounds.size.x * (float)i / 16f;
				Gizmos.DrawLine(new Vector3(x, physicsBounds.center.y, physicsBounds.min.z), new Vector3(x, physicsBounds.center.y, physicsBounds.max.z));
			}
			Gizmos.color = color;
		}
	}

	public void UpdatePhysicsBounds()
	{
		if (!Globals.inst.DynamicMultiBoxBroadphaseRegions)
		{
			return;
		}
		Bounds worldBounds = default(Bounds);
		foreach (KeyValuePair<IntVector2, WorldTile> item in m_TileLookup)
		{
			if (item.Value.m_RequestState == WorldTile.State.Loaded)
			{
				worldBounds.Encapsulate(item.Value.CalcSceneCentre());
			}
		}
		worldBounds.Expand(Singleton.Manager<ManWorld>.inst.TileSize);
		if (!physicsBounds.Contains(worldBounds.min) || !physicsBounds.Contains(worldBounds.max))
		{
			d.LogFormat("Physics.RebuildBroadphaseRegions {0} - {1} ({2} subdivisions)", worldBounds.min, worldBounds.max, 16);
			Physics.RebuildBroadphaseRegions(worldBounds, 16);
			physicsBounds = worldBounds;
		}
	}

	private static ResponsibleThread GetThreadForLoadingStep(WorldTile tile)
	{
		switch (tile.m_LoadStep)
		{
		case WorldTile.LoadStep.Empty:
			return ResponsibleThread.None;
		case WorldTile.LoadStep.GeneratingTerrain:
			return ResponsibleThread.Generation;
		case WorldTile.LoadStep.Creating:
			return ResponsibleThread.Main;
		case WorldTile.LoadStep.Created:
			return ResponsibleThread.None;
		case WorldTile.LoadStep.GeneratingScenery:
			return ResponsibleThread.Generation;
		case WorldTile.LoadStep.StartPopulatingScenery:
			return ResponsibleThread.Main;
		case WorldTile.LoadStep.PopulatingScenery:
			return ResponsibleThread.Main;
		case WorldTile.LoadStep.Populated:
			return ResponsibleThread.None;
		case WorldTile.LoadStep.Loading:
			return ResponsibleThread.Main;
		case WorldTile.LoadStep.Loaded:
			return ResponsibleThread.None;
		default:
			d.AssertFormat(false, "TileManager.InGenerationThreadLoadingStep: unhandled loadstep {0}", tile.m_LoadStep);
			return ResponsibleThread.None;
		}
	}

	public void PauseGenerationOneFrame()
	{
		m_PauseGenerationOneFrame = true;
	}

	public WorldTile LookupTile(in Vector3 scenePos, bool allowEmpty = false)
	{
		return LookupTile(SceneToTileCoord(in scenePos), allowEmpty);
	}

	public WorldTile LookupTile(in IntVector2 coord, bool allowEmpty = false)
	{
		WorldTile value = null;
		m_TileLookup.TryGetValue(coord, out value);
		if (value == null || (!allowEmpty && !value.IsCreated))
		{
			return null;
		}
		return value;
	}

	public bool IsTileAtPositionLoaded(in Vector3 scenePos)
	{
		return LookupTile(in scenePos)?.IsLoaded ?? false;
	}

	public ManSaveGame.StoredTile GetStoredTileIfNotSpawned(in Vector3 scenePos, bool createNewDataIfNotFound = true)
	{
		IntVector2 intVector = SceneToTileCoord(in scenePos);
		if (intVector == IntVector2.invalid)
		{
			d.LogErrorFormat("TileManager.GetStoredTileIfNotSpawned - Trying to get coord for position {0} resulted in an invalid tile coord!", scenePos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld);
			return null;
		}
		WorldTile worldTile = LookupTile(in scenePos);
		if (worldTile != null && worldTile.IsLoaded)
		{
			return null;
		}
		return Singleton.Manager<ManSaveGame>.inst.GetStoredTile(intVector, createNewDataIfNotFound);
	}

	public ref readonly BiomeMap.MapCell GetMapCell(WorldTile tile, in Vector3 scenePos)
	{
		Vector3 vector = tile.CalcSceneOrigin();
		return ref tile.GetMapCell(scenePos - vector);
	}

	public IntVector2 WorldToTileCoord(in Vector3 posWorld)
	{
		return WorldToTileCoord(posWorld.x, posWorld.z);
	}

	public IntVector2 SceneToTileCoord(in Vector3 posScene)
	{
		return WorldToTileCoord(posScene.x, posScene.z) + Singleton.Manager<ManWorld>.inst.FloatingOriginTile;
	}

	public void GetTileCoordRange(Bounds sceneBounds, out IntVector2 min, out IntVector2 max)
	{
		min = SceneToTileCoord(sceneBounds.min);
		max = SceneToTileCoord(sceneBounds.max);
	}

	public Vector3 CalcTileOriginScene(in IntVector2 tileCoordWorld)
	{
		return CalcTileOrigin(tileCoordWorld - Singleton.Manager<ManWorld>.inst.FloatingOriginTile);
	}

	public Vector3 CalcTileOrigin(in IntVector2 tileCoord)
	{
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		return new Vector3((float)tileCoord.x * tileSize, -50f, (float)tileCoord.y * tileSize) - new Vector3(tileSize * 0.5f, 0f, tileSize * 0.5f);
	}

	public Vector3 CalcTileCentreScene(in IntVector2 tileCoordWorld)
	{
		return CalcTileCentre(tileCoordWorld - Singleton.Manager<ManWorld>.inst.FloatingOriginTile);
	}

	public Vector3 CalcTileCentre(in IntVector2 tileCoord)
	{
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		return new Vector3((float)tileCoord.x * tileSize, -50f, (float)tileCoord.y * tileSize);
	}

	public Vector2 CalcMinWorldCoords(in IntVector2 tileCoord)
	{
		return ((Vector2)tileCoord - Vector2.one * 0.5f) * Singleton.Manager<ManWorld>.inst.TileSize;
	}

	public Vector2 CalcMaxWorldCoords(in IntVector2 tileCoord)
	{
		return ((Vector2)tileCoord + Vector2.one * 0.5f) * Singleton.Manager<ManWorld>.inst.TileSize;
	}

	public Vector2 GetNormalisedPosInTile(in Vector3 scenePos, in IntVector2 tileCoord)
	{
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		Vector3 vector = CalcTileOriginScene(in tileCoord);
		return (scenePos - vector).ToVector2XZ() / tileSize;
	}

	private IntVector2 WorldToTileCoord(float worldX, float worldZ)
	{
		float num = worldX * kInvTileSize + 0.5f + 0.0001f;
		float num2 = worldZ * kInvTileSize + 0.5f + 0.0001f;
		if (Mathf.Abs(num) > 2.1473837E+09f || Mathf.Abs(num2) > 2.1473837E+09f)
		{
			d.LogErrorFormat("TileManager.WorldToTileCoord - Was passed world coords of ({0}, {1}) that generated tile coords greater than what is currently supported in WorldPosition (Int.MaxValue)", num, num2);
			return IntVector2.invalid;
		}
		return new IntVector2(Mathf.FloorToInt(num), Mathf.FloorToInt(num2));
	}

	public void UpdateTileCache(Visible visible, bool forceNow = false)
	{
		if (!visible.isActive || !visible.ManagedByTile)
		{
			return;
		}
		float time = Time.time;
		if (time < visible.tileCache.nextUpdateTime && !forceNow)
		{
			return;
		}
		if (visible.type == ObjectTypes.Scenery && m_CurrentPopulatingPatch != null)
		{
			visible.tileCache.tile = m_CurrentPopulatingPatch.Tile;
			m_CurrentPopulatingPatch.Tile.AddVisible(visible);
			return;
		}
		int num = 1;
		bool flag = false;
		Vector3 posScene = ((visible.rbody != null) ? visible.rbody.position : visible.trans.position);
		if (visible.tileCache.tile != null && visible.tileCache.tile.IsCreated)
		{
			Vector3 vector = Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(visible.tileCache.tile.Coord);
			Vector3 vector2 = posScene - vector;
			float num2 = Singleton.Manager<ManWorld>.inst.TileSize * 0.5f;
			float num3 = Mathf.Min(num2 - Mathf.Abs(vector2.x), num2 - Mathf.Abs(vector2.z));
			if (num3 < 0f)
			{
				flag = true;
			}
			else
			{
				IntVector2[] tileRefUpdateFrequencyTable = m_TileRefUpdateFrequencyTable;
				for (int i = 0; i < tileRefUpdateFrequencyTable.Length; i++)
				{
					IntVector2 intVector = tileRefUpdateFrequencyTable[i];
					if ((float)intVector[0] > num3)
					{
						break;
					}
					num = intVector[1];
				}
			}
		}
		else
		{
			flag = true;
		}
		float num4 = Mathf.Max((float)(num - 1) * Time.fixedDeltaTime, Time.fixedDeltaTime * 0.5f);
		visible.tileCache.nextUpdateTime = time + num4;
		WorldTile worldTile = visible.tileCache.tile;
		IntVector2 invalid = IntVector2.invalid;
		if (flag)
		{
			worldTile = LookupTile(SceneToTileCoord(in posScene));
		}
		bool flag2 = false;
		if (visible.tank != null && worldTile != null && m_UnloadBehaviour != UnloadBehaviour.ExternallyControlled)
		{
			flag2 = UpdateCachedOverlapData(visible, worldTile);
		}
		WorldTile tile = visible.tileCache.tile;
		if (flag)
		{
			flag2 = SetTileCache(visible, worldTile) || flag2;
			if (!flag2)
			{
				VisibleChangedTileEvent.Send(visible, tile, visible.tileCache.tile);
			}
		}
		if (flag && visible.type == ObjectTypes.Vehicle && Singleton.playerTank != null)
		{
			_ = visible.ID;
			_ = Singleton.playerTank.visible.ID;
		}
		if (!flag2 || m_UnloadBehaviour == UnloadBehaviour.ExternallyControlled)
		{
			return;
		}
		ManSaveGame.Storing = true;
		bool flag3 = false;
		if (worldTile != null)
		{
			flag3 = worldTile.StoreLoadedVisible(visible);
		}
		if (!flag3)
		{
			ManSaveGame.StoredTile storedTileIfNotSpawned = GetStoredTileIfNotSpawned(in posScene);
			if (storedTileIfNotSpawned != null)
			{
				storedTileIfNotSpawned.StoreLoadedVisible(visible);
			}
			else if (Mathf.Abs(posScene.x) > Singleton.Manager<ManWorld>.inst.MaximumWorldDistanceFromOrigin || Mathf.Abs(posScene.z) > Singleton.Manager<ManWorld>.inst.MaximumWorldDistanceFromOrigin)
			{
				d.LogErrorFormat("TileManager.UpdateTileCache - Trying to store object {0} at position {1} that generated tile coords greater than what is currently supported in WorldPosition (Int.MaxValue)", visible.name, posScene);
			}
			else
			{
				d.LogErrorFormat("TileManager.UpdateTileCache - Trying to store object {0} at position {1} but failed to get a stored tile for reasons unknown!", visible.name, posScene);
			}
		}
		if (m_UnloadBehaviour == UnloadBehaviour.MultiplayerHost)
		{
			visible.ServerDestroy();
		}
		else
		{
			visible.trans.Recycle();
		}
		ManSaveGame.Storing = false;
	}

	private bool SetTileCache(Visible visible, WorldTile newTile)
	{
		bool result = false;
		if (newTile != visible.tileCache.tile)
		{
			if (newTile != null && newTile.IsLoaded)
			{
				if (visible.tileCache.tile != null)
				{
					visible.tileCache.tile.RemoveVisible(visible);
					visible.tileCache.tile = null;
				}
				newTile.AddVisible(visible);
				visible.tileCache.tile = newTile;
			}
			else
			{
				result = true;
			}
		}
		return result;
	}

	private static IntVector2 GetTileOverlapDirectionFromCentre(Vector3 fromCentre, float radius)
	{
		float num = Singleton.Manager<ManWorld>.inst.TileSize * 0.5f - radius;
		float num2 = num - Mathf.Abs(fromCentre.x);
		float num3 = num - Mathf.Abs(fromCentre.z);
		int x = 0;
		int y = 0;
		if (num2 < 0f)
		{
			x = ((!(fromCentre.x < 0f)) ? 1 : (-1));
		}
		if (num3 < 0f)
		{
			y = ((!(fromCentre.z < 0f)) ? 1 : (-1));
		}
		return new IntVector2(x, y);
	}

	public IntVector2 GetTileOverlapDirection(WorldPosition worldPos, float radius)
	{
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		return GetTileOverlapDirectionFromCentre(worldPos.TileRelativePos - new Vector3(tileSize / 2f, 0f, tileSize / 2f), radius);
	}

	private bool UpdateCachedOverlapData(Visible visible, WorldTile parentTile)
	{
		bool result = false;
		Vector3 vector2;
		if (visible.rbody != null)
		{
			Vector3 position = visible.rbody.position;
			Vector3 vector = visible.centrePosition - visible.trans.position;
			vector2 = position + vector;
		}
		else
		{
			vector2 = visible.centrePosition;
		}
		Vector3 vector3 = Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(parentTile.Coord);
		Vector3 fromCentre = vector2 - vector3;
		float radius = visible.Radius;
		IntVector2 tileOverlapDirectionFromCentre = GetTileOverlapDirectionFromCentre(fromCentre, radius);
		if (visible.tileCache.tileOverlapDirection != IntVector2.zero)
		{
			Singleton.Manager<ManTechs>.inst.RemoveOverlappingTechData(visible.ID);
		}
		if (tileOverlapDirectionFromCentre != visible.tileCache.tileOverlapDirection)
		{
			if (visible.type == ObjectTypes.Vehicle && Singleton.playerTank != null)
			{
				_ = visible.ID;
				_ = Singleton.playerTank.visible.ID;
			}
			visible.tileCache.tileOverlapDirection = tileOverlapDirectionFromCentre;
		}
		if (tileOverlapDirectionFromCentre != IntVector2.zero)
		{
			Singleton.Manager<ManTechs>.inst.AddOverlappingTechData(visible);
			if (!CheckAllOverlappedNeighboursLoaded(parentTile.Coord, tileOverlapDirectionFromCentre))
			{
				result = true;
			}
		}
		return result;
	}

	public void RemoveTileCache(Visible visible)
	{
		if (visible.tileCache.tile != null)
		{
			visible.tileCache.tile.RemoveVisible(visible);
		}
		visible.tileCache.Reset();
	}

	public void SetFixedTilesLoaded(IEnumerable<IntVector2> coords)
	{
		m_FixedTilesLoaded.Clear();
		m_FixedTilesLoaded.AddRange(coords);
	}

	public void SetFixedTilesUnpopulated(IEnumerable<IntVector2> coords)
	{
		m_FixedTilesUnpopulated.Clear();
		m_FixedTilesUnpopulated.AddRange(coords);
	}

	public void ClearFixedTiles()
	{
		m_FixedTilesLoaded.Clear();
		m_FixedTilesUnpopulated.Clear();
	}

	public bool CheckAllOverlappedNeighboursLoaded(IntVector2 tileCoord, IntVector2 overlapDirection, bool testCentreTileLoadState = true)
	{
		bool flag = true;
		int num = Mathf.Min(0, overlapDirection.x);
		int num2 = Mathf.Max(0, overlapDirection.x);
		int num3 = Mathf.Min(0, overlapDirection.y);
		int num4 = Mathf.Max(0, overlapDirection.y);
		for (int i = num; i <= num2 && flag; i++)
		{
			for (int j = num3; j <= num4; j++)
			{
				if (testCentreTileLoadState || i != 0 || j != 0)
				{
					WorldTile worldTile = LookupTile(tileCoord + new IntVector2(i, j));
					if (worldTile == null || !worldTile.IsLoaded)
					{
						flag = false;
						break;
					}
				}
			}
		}
		return flag;
	}

	public void StoreAllLoadedTileData()
	{
		TileIterator enumerator = IterateTiles(WorldTile.State.Loaded).GetEnumerator();
		while (enumerator.MoveNext())
		{
			WorldTile current = enumerator.Current;
			current.SaveData.StoreVisibles(current.Visibles, current.StoredVisiblesWaitingToLoad, despawn: false);
			current.SaveData.StorePersistentTerrainObjects(current.ManuallyAddedTerrainObjects);
			current.SaveData.StoreScenery(current.Visibles);
			if (m_DebugStoredTileCoords.Length > 0)
			{
				m_DebugStoredTileCoords.Append(", ");
			}
			m_DebugStoredTileCoords.Append(current.Coord.ToString());
		}
		_ = Singleton.playerTank;
		m_DebugStoredTileCoords.Length = 0;
	}

	public VisibleIterator IterateVisibles(ObjectTypes type)
	{
		return new VisibleIterator(type, m_TileLookup);
	}

	public VisibleIterator IterateVisibles(ObjectTypes type, Vector3 centreScene, float boundsExtent = 0f)
	{
		IntVector2 min = SceneToTileCoord(centreScene - Vector3.one * boundsExtent);
		IntVector2 max = SceneToTileCoord(centreScene + Vector3.one * boundsExtent);
		return new VisibleIterator(type, m_TileLookup, min, max);
	}

	public VisibleIterator IterateVisibles(ObjectTypes type, IntVector2 centreScene)
	{
		return new VisibleIterator(type, m_TileLookup, centreScene, centreScene);
	}

	public TileIterator IterateTiles(WorldTile.State minimumSpawnState = WorldTile.State.Created)
	{
		return new TileIterator(m_TileLookup, minimumSpawnState, IntVector2.invalid, IntVector2.invalid);
	}

	public TileIterator IterateTiles(IntVector2 coordMin, IntVector2 coordMax, WorldTile.State minimumSpawnState = WorldTile.State.Created)
	{
		d.Assert(coordMin != IntVector2.invalid && coordMax != IntVector2.invalid && coordMax.x >= coordMin.x && coordMax.y >= coordMin.y);
		return new TileIterator(m_TileLookup, minimumSpawnState, coordMin, coordMax);
	}

	public float GetTerrainHeightAtPosition(Vector3 scenePos, out bool onTile, bool forceCalculate = false)
	{
		WorldTile worldTile = LookupTile(in scenePos);
		if (worldTile != null && worldTile.IsCreated && !forceCalculate)
		{
			onTile = true;
			return worldTile.Terrain.transform.position.y + worldTile.Terrain.SampleHeight(scenePos);
		}
		onTile = false;
		float num = 0.5f;
		if (Singleton.Manager<ManWorld>.inst.CurrentBiomeMap != null)
		{
			Vector3 coord = scenePos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld;
			num = Singleton.Manager<ManWorld>.inst.CurrentBiomeMap.GenerateHeightAtPoint(coord.ToVector2XZ(), Singleton.Manager<ManWorld>.inst.SeedValue, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, (int)Singleton.Manager<ManWorld>.inst.CellScale);
			IntVector2 tileCoord = SceneToTileCoord(in scenePos);
			if (Singleton.Manager<ManWorld>.inst.GetSetPieceDataForTile(tileCoord, fillData: true, out var outSetPiece, out var setPieceOffsetHeight, out var setPieceOfset))
			{
				num = outSetPiece.GenerateHeightForScenePos(tileCoord, scenePos, num, setPieceOffsetHeight, setPieceOfset);
			}
		}
		return 100f * (num - 0.5f);
	}

	private void InitTemplate()
	{
		m_TerrainTemplate = new GameObject("terrain pool template", typeof(Terrain), typeof(TerrainCollider)).transform;
		m_TerrainTemplate.gameObject.isStatic = true;
		GameObject gameObject = new GameObject("static parent");
		gameObject.transform.SetParent(m_TerrainTemplate, worldPositionStays: false);
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.layer = Globals.inst.layerContainer;
		Singleton.Manager<ManWorld>.inst.ReflectionProbeManager.AddProbeToTileTemplate(m_TerrainTemplate.transform);
		m_TerrainTemplate.GetComponent<TerrainCollider>().material = Singleton.Manager<ManWorld>.inst.PhysicMaterial;
		Terrain component = m_TerrainTemplate.GetComponent<Terrain>();
		component.drawTreesAndFoliage = false;
		component.basemapDistance = QualitySettingsExtended.TerrainBaseMapDistance;
		component.heightmapPixelError = QualitySettingsExtended.TerrainLODPixelError;
		if (Singleton.Manager<ManWorld>.inst.m_TerrainMaterial != null)
		{
			component.materialType = Terrain.MaterialType.Custom;
			component.materialTemplate = Singleton.Manager<ManWorld>.inst.m_TerrainMaterial;
		}
		if (Globals.inst.m_ManuallyConnectTerrainTiles)
		{
			component.allowAutoConnect = false;
		}
		else
		{
			component.groupingID = 1;
			component.allowAutoConnect = true;
		}
		component.castShadows = false;
		component.Flush();
		m_TerrainTemplate.gameObject.AddComponent<WorldSpaceObject>();
		m_TerrainTemplate.CreatePool(15);
		m_TerrainTemplate.parent = Singleton.Manager<ComponentPool>.inst.transform;
		m_TerrainTemplate.gameObject.SetActive(value: false);
	}

	private void InitTilePool(int dimension)
	{
		if (m_TilePool == null)
		{
			m_TilePool = new Stack<WorldTile>();
		}
		else
		{
			m_TilePool.Clear();
		}
		d.Assert(Mathf.IsPowerOfTwo(dimension), "cells per tile must be power 2");
		m_TilePoolDimension = dimension;
		for (int i = 0; i < 15; i++)
		{
			m_TilePool.Push(new WorldTile());
		}
	}

	private float TileToScenePointDistance(IntVector2 tileCoord, Vector3 scenePosition)
	{
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		Vector2 vector = new Vector2(scenePosition.x, scenePosition.z) - ((Vector2)tileCoord * tileSize + (Vector2)Singleton.Manager<ManWorld>.inst.GameWorldToScene.ToVector2XZ());
		vector.x = Mathf.MoveTowards(vector.x, 0f, tileSize * 0.5f);
		vector.y = Mathf.MoveTowards(vector.y, 0f, tileSize * 0.5f);
		return vector.magnitude;
	}

	private WorldTile GetTileFromPool(IntVector2 coord)
	{
		if (m_TilePool == null || m_TilePoolDimension != Singleton.Manager<ManWorld>.inst.CellsPerTileEdge)
		{
			InitTilePool(Singleton.Manager<ManWorld>.inst.CellsPerTileEdge);
		}
		WorldTile worldTile = ((m_TilePool.Count != 0) ? m_TilePool.Pop() : new WorldTile());
		Vector3 origin = CalcTileOrigin(in coord);
		worldTile.Init(coord, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, origin, new Vector3(0.5f, 0f, 0.5f) * Singleton.Manager<ManWorld>.inst.TileSize);
		Singleton.Manager<ManWorld>.inst.GetSetPieceDataForTile(coord, worldTile.BiomeMapData);
		return worldTile;
	}

	private void ReturnTileToPool(WorldTile tile)
	{
		m_TilePool.Push(tile);
	}

	private void CreateTile(WorldTile tile)
	{
		Vector3 position = tile.CalcSceneOrigin();
		Terrain component = m_TerrainTemplate.Spawn(Singleton.terrainContainer, position).GetComponent<Terrain>();
		d.Assert(component);
		component.name = "terrain tile " + tile.Coord;
		component.terrainData = tile.BiomeMapData.terrainData;
		component.terrainData.heightmapResolution = tile.BiomeMapData.heightData.heights.GetLength(0);
		component.terrainData.size = new Vector3(Singleton.Manager<ManWorld>.inst.TileSize, 100f, Singleton.Manager<ManWorld>.inst.TileSize);
		component.GetComponent<TerrainCollider>().terrainData = tile.BiomeMapData.terrainData;
		component.gameObject.layer = Globals.inst.layerTerrain;
		component.basemapDistance = QualitySettingsExtended.TerrainBaseMapDistance;
		component.heightmapPixelError = QualitySettingsExtended.TerrainLODPixelError;
		tile.SetTerrain(component);
		component.terrainData.alphamapResolution = tile.BiomeMapData.mtDimension;
		component.terrainData.SetHeightsDelayLOD(0, 0, tile.BiomeMapData.heightData.heights);
		m_DelayedTerrainHeightmapModificationList.Add(new TerrainDeferredLOD
		{
			m_Terrain = component,
			m_OverrideHeightError = tile.BiomeMapData.heightData.heightError
		});
		component.terrainData.terrainLayers = tile.BiomeMapData.terrainLayers;
		component.terrainData.SetAlphamaps(0, 0, tile.BiomeMapData.splatData);
		component.terrainData.RefreshPrototypes();
		tile.BiomeMapData.RecycleSplatBuffer();
		tile.BiomeMapData.ReturnTerrainLayerArray();
		ConnectNeighbouringTilesAndFlush(tile, topLevel: true);
		component.drawInstanced = Globals.inst.m_DrawTerrainInstanced;
		tile.AddOverlappingObjectsFromNeighbours();
		tile.m_LoadStep = WorldTile.LoadStep.Created;
		Singleton.Manager<ManWorld>.inst.LandmarkSpawner.SpawnLandmarks(tile);
		Singleton.Manager<ManWorld>.inst.VendorSpawner.SpawnOverlappingSceneryBlockers(tile);
		if (Singleton.Manager<ManWorld>.inst.StuntRampSpawner != null)
		{
			Singleton.Manager<ManWorld>.inst.StuntRampSpawner.SpawnStuntRamps(tile);
		}
	}

	private void CreateNetworkedTile(WorldTile tile)
	{
		d.AssertFormat(tile.IsLoaded, "Tile [{0},{1}] is not loaded, so can't be networked (is {2})", tile.Coord.x, tile.Coord.y, tile.m_LoadStep);
		Transform transform = Singleton.Manager<ManWorld>.inst.m_NetworkedTilePrefab.transform.Spawn(tile.CalcSceneOrigin());
		tile.NetTile = transform.GetComponent<NetWorldTile>();
		tile.NetTile.name = "NetTile " + tile.Coord;
		tile.NetTile.transform.SetParent(Singleton.terrainContainer);
		tile.NetTile.InitServer(tile);
		NetworkServer.Spawn(tile.NetTile.gameObject);
	}

	public void OnHostNetworkInitialized()
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer() || !Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			return;
		}
		foreach (IntVector2 loadedTile in GetLoadedTiles())
		{
			WorldTile worldTile = LookupTile(loadedTile);
			if (worldTile != null && worldTile.NetTile.IsNull())
			{
				CreateNetworkedTile(worldTile);
			}
		}
	}

	private void ConnectNeighbouringTilesAndFlush(WorldTile tile, bool topLevel)
	{
		if (Globals.inst.m_ManuallyConnectTerrainTiles)
		{
			if (!topLevel && !tile.IsCreated)
			{
				return;
			}
			m_TileLookup.TryGetValue(tile.Coord + new IntVector2(-1, 0), out var value);
			m_TileLookup.TryGetValue(tile.Coord + new IntVector2(0, 1), out var value2);
			m_TileLookup.TryGetValue(tile.Coord + new IntVector2(1, 0), out var value3);
			m_TileLookup.TryGetValue(tile.Coord + new IntVector2(0, -1), out var value4);
			tile.Terrain.SetNeighbors(value?.Terrain, value2?.Terrain, value3?.Terrain, value4?.Terrain);
			if (topLevel)
			{
				if (value != null)
				{
					ConnectNeighbouringTilesAndFlush(value, topLevel: false);
				}
				if (value2 != null)
				{
					ConnectNeighbouringTilesAndFlush(value2, topLevel: false);
				}
				if (value3 != null)
				{
					ConnectNeighbouringTilesAndFlush(value3, topLevel: false);
				}
				if (value4 != null)
				{
					ConnectNeighbouringTilesAndFlush(value4, topLevel: false);
				}
			}
		}
		else
		{
			Terrain.SetConnectivityDirty();
		}
		tile.Terrain.Flush();
	}

	private void DestroyTile(WorldTile tile)
	{
		for (int i = 0; i < m_DelayedTerrainHeightmapModificationList.Count; i++)
		{
			if (m_DelayedTerrainHeightmapModificationList[i].m_Terrain == tile.Terrain)
			{
				m_DelayedTerrainHeightmapModificationList.RemoveAt(i);
				break;
			}
		}
		tile.ClearAndReset();
	}

	private void LoadTile(WorldTile tile)
	{
		tile.m_LoadStep = WorldTile.LoadStep.Loaded;
		tile.Terrain.castShadows = true;
		if (!tile.SaveData.m_HasBeenSavedBefore)
		{
			Singleton.Manager<ManWorld>.inst.VendorSpawner.SpawnVendors(tile);
		}
		tile.ClearPersistentTerrainObjects();
		tile.SaveData.RestorePersistentTerrainObjects();
		tile.SaveData.SetSceneryAwake(tile.Visibles, awake: true);
		tile.SaveData.RestoreVisibles();
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			CreateNetworkedTile(tile);
		}
		TileLoadedEvent.Send(tile);
	}

	private bool PopulateSceneryPatches(SceneryPatchType patchType, WorldTile restrictToTile = null)
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup;
		bool flag = false;
		List<SceneryPatch> list = m_SceneryPatchesToGenerate[(int)patchType];
		float num2 = (Singleton.Manager<CameraManager>.inst.TeleportEffect.IsFadedOut ? float.MaxValue : ((Singleton.Manager<ManGameMode>.inst.GetModePhase() != ManGameMode.GameState.InGame) ? 0.1f : ((restrictToTile != null && restrictToTile.m_RequestState == WorldTile.State.Loaded) ? 0.02f : 0.004f)));
		while (list.Count != 0 && realtimeSinceStartup - num < num2)
		{
			SceneryPatch sceneryPatch = null;
			lock (list)
			{
				int num3;
				if (restrictToTile != null)
				{
					num3 = GetIndexOfClosestPatchForTile(list, restrictToTile);
					if (num3 >= 0)
					{
						goto IL_0106;
					}
					if (!flag)
					{
						d.LogErrorFormat("PopulateSceneryPatches - Trying to populate scenery patches {0} for tile tile {1} only, but the tile has no patches left to populate ({2})!", patchType, restrictToTile.Coord, restrictToTile.patchesToPopulate[(int)patchType]);
					}
					break;
				}
				num3 = 0;
				if (patchType == SceneryPatchType.OptionalSceneryClutter)
				{
					while (true)
					{
						SceneryPatch sceneryPatch2 = list[num3];
						if (!sceneryPatch2.TileValid || sceneryPatch2.Tile.IsLoaded)
						{
							break;
						}
						num3++;
						if (num3 >= list.Count)
						{
							num3 = -1;
							break;
						}
					}
				}
				goto IL_0106;
				IL_0106:
				if (num3 >= 0)
				{
					sceneryPatch = list[num3];
					list.RemoveAt(num3);
					goto IL_0130;
				}
			}
			break;
			IL_0130:
			if (sceneryPatch.TileValid)
			{
				if (sceneryPatch.Tile.patchesToPopulate[(int)patchType] <= 0 || sceneryPatch.Tile.BiomeMapData.sceneryPlacement == null)
				{
					d.LogError(string.Format("Patch population failed: patchesToPopulate={0}, sceneryPatchType={1}, sceneryPlacement={2}", sceneryPatch.Tile.patchesToPopulate[(int)patchType], patchType, (sceneryPatch.Tile.BiomeMapData.sceneryPlacement == null) ? "null" : "non-null"));
				}
				else
				{
					m_CurrentPopulatingPatch = sceneryPatch;
					if (!m_DisableSceneryObjects)
					{
						sceneryPatch.SpawnAll();
					}
					m_CurrentPopulatingPatch = null;
					flag = true;
					sceneryPatch.Tile.patchesToPopulate[(int)patchType]--;
				}
			}
			ReturnPatchToPool(sceneryPatch);
			realtimeSinceStartup = Time.realtimeSinceStartup;
		}
		if (flag && !Physics.autoSyncTransforms)
		{
			Physics.SyncTransforms();
		}
		return flag;
	}

	private int GetIndexOfClosestPatchForTile(List<SceneryPatch> sceneryPatches, WorldTile tile)
	{
		int count = sceneryPatches.Count;
		for (int i = 0; i < count; i++)
		{
			if (sceneryPatches[i].Tile == tile)
			{
				return i;
			}
		}
		return -1;
	}

	private void LogPatchPoolSizes()
	{
		int num = 0;
		string text = "";
		foreach (KeyValuePair<int, SceneryPatchPool> patchPool in m_PatchPools)
		{
			num += (12 + 32 * patchPool.Key) * patchPool.Value.patches.Count;
			text += $"{patchPool.Value.patches.Count} of size {patchPool.Key}, ";
		}
		d.Log($"patch pool usage: {text} = {(float)num / 1024f:0.0}k");
	}

	private bool UpdateGeneration(ThreadedJobProcessor.TestShouldExitDelegate testShouldExit)
	{
		m_NeedSortSceneryPatches = false;
		while (true)
		{
			WorldTile worldTile = null;
			lock (m_TileLookup)
			{
				float num = 0f;
				Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
				while (enumerator.MoveNext())
				{
					WorldTile value = enumerator.Current.Value;
					if (GetThreadForLoadingStep(value) != ResponsibleThread.Generation)
					{
						continue;
					}
					bool flag = value.HasReachedRequestedState();
					if (flag)
					{
						if (value.m_LoadStep == WorldTile.LoadStep.GeneratingTerrain)
						{
							value.m_LoadStep = WorldTile.LoadStep.Empty;
						}
						else if (value.m_LoadStep == WorldTile.LoadStep.GeneratingScenery)
						{
							value.m_LoadStep = WorldTile.LoadStep.Created;
						}
						else
						{
							d.AssertFormat(false, "Gen thread unable to unload from state {0}", value.m_LoadStep);
						}
					}
					if (!m_ClearAll && !flag)
					{
						float num2 = CalcTileLoadPriority(value);
						if (worldTile == null || num2 > num)
						{
							worldTile = value;
							num = num2;
						}
					}
				}
			}
			if (testShouldExit())
			{
				break;
			}
			if (worldTile != null)
			{
				switch (worldTile.m_LoadStep)
				{
				case WorldTile.LoadStep.GeneratingTerrain:
					if (GenerateTerrainData(worldTile, testShouldExit))
					{
						worldTile.m_LoadStep = WorldTile.LoadStep.Creating;
					}
					break;
				case WorldTile.LoadStep.GeneratingScenery:
				{
					GenerateSceneryMap(worldTile);
					worldTile.patchesToPopulate[0] = 0;
					worldTile.patchesToPopulate[1] = 0;
					worldTile.ScenerySpawnID = ++s_ScenerySpawnIDCounter;
					AddSceneryPatchesToQueue(worldTile, Singleton.Manager<ManWorld>.inst.CellsPerSceneryPatchEdge, SceneryPatchType.PopulatedTileScenery, mergeMeshes: false);
					bool mergeSceneryClutter = Globals.inst.m_MergeSceneryClutter;
					int sceneryPatchSize = (mergeSceneryClutter ? Singleton.Manager<ManWorld>.inst.CellsPerSceneryMergePatchEdge : Singleton.Manager<ManWorld>.inst.CellsPerSceneryPatchEdge);
					if (Globals.inst.m_InstanceSceneryClutter)
					{
						sceneryPatchSize = Singleton.Manager<ManWorld>.inst.CellsPerTileEdge / 4;
					}
					AddSceneryPatchesToQueue(worldTile, sceneryPatchSize, SceneryPatchType.OptionalSceneryClutter, mergeSceneryClutter);
					worldTile.m_LoadStep = WorldTile.LoadStep.StartPopulatingScenery;
					break;
				}
				default:
					d.AssertFormat(false, "Generate: Unsure how to progress with tile in load step {0}", worldTile.m_LoadStep);
					break;
				}
			}
			patchSortCounter--;
			if (patchSortCounter < 0)
			{
				patchSortCounter = 25;
				m_NeedSortSceneryPatches = true;
			}
			if (m_NeedSortSceneryPatches)
			{
				List<SceneryPatch>[] sceneryPatchesToGenerate = m_SceneryPatchesToGenerate;
				foreach (List<SceneryPatch> list in sceneryPatchesToGenerate)
				{
					lock (list)
					{
						list.Sort(SceneryPatch.CompareDist);
					}
				}
				m_NeedSortSceneryPatches = false;
			}
			if (worldTile == null)
			{
				Thread.Sleep(1);
			}
		}
		return false;
	}

	private bool GenerateTerrainData(WorldTile tile, ThreadedJobProcessor.TestShouldExitDelegate testShouldExit)
	{
		bool num = testShouldExit() || m_ClearAll;
		BiomeMap currentBiomeMap = Singleton.Manager<ManWorld>.inst.CurrentBiomeMap;
		Vector2 tileSize = Vector2.one * Singleton.Manager<ManWorld>.inst.TileSize;
		Vector2 vector = tile.WorldOrigin.ToVector2XZ() + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		if (!num)
		{
			currentBiomeMap.GenerateBiomeBlendMap(vector, tile, Singleton.Manager<ManWorld>.inst.SeedValue, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, (int)Singleton.Manager<ManWorld>.inst.CellScale);
		}
		int num2;
		if (!num)
		{
			num2 = (testShouldExit() ? 1 : 0);
			if (num2 == 0)
			{
				currentBiomeMap.GenerateHeightMap(tile, vector, tileSize, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, Singleton.Manager<ManWorld>.inst.SeedValue);
			}
		}
		else
		{
			num2 = 1;
		}
		int num3;
		if (num2 == 0 && !testShouldExit())
		{
			num3 = (m_ClearAll ? 1 : 0);
			if (num3 == 0)
			{
				currentBiomeMap.GenerateSplatMaps(tile, vector, tileSize, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, Singleton.Manager<ManWorld>.inst.SeedValue);
			}
		}
		else
		{
			num3 = 1;
		}
		if (num3 != 0)
		{
			tile.ClearTerrainData();
		}
		return num3 == 0;
	}

	private void GenerateSceneryMap(WorldTile tile)
	{
		Vector2 tileSize = Vector2.one * Singleton.Manager<ManWorld>.inst.TileSize;
		Vector2 tileMin = tile.WorldOrigin.ToVector2XZ() + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		Singleton.Manager<ManWorld>.inst.CurrentBiomeMap.GenerateSceneryMap(tile, tileMin, tileSize, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge, Singleton.Manager<ManWorld>.inst.SeedValue);
	}

	private void AddSceneryPatchesToQueue(WorldTile tile, int sceneryPatchSize, SceneryPatchType patchType, bool mergeMeshes)
	{
		if (tile.BiomeMapData.sceneryPlacement == null)
		{
			d.LogError("ERROR: tile.biomeMapData.sceneryPlacement is null. coord [" + tile.Coord[0] + "," + tile.Coord[1] + "] rs=" + tile.m_RequestState);
			return;
		}
		Vector3 vector = new Vector3(1f, 0f, 1f) * sceneryPatchSize * Singleton.Manager<ManWorld>.inst.CellScale / 2f;
		float num = 0f;
		if (Singleton.Manager<ManWorld>.inst.SceneryPositionJitter != 0f)
		{
			d.Assert(Singleton.Manager<ManWorld>.inst.SceneryPositionJitter >= 0f && Singleton.Manager<ManWorld>.inst.SceneryPositionJitter <= 0.5f, "position jitter is out of acceptable range");
			num = Singleton.Manager<ManWorld>.inst.SceneryPositionJitter * Singleton.Manager<ManWorld>.inst.CellScale;
		}
		int num2 = tile.Coord.x * Singleton.Manager<ManWorld>.inst.CellsPerTileEdge + Mathf.RoundToInt(Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset.x / Singleton.Manager<ManWorld>.inst.CellScale);
		int num3 = tile.Coord.y * Singleton.Manager<ManWorld>.inst.CellsPerTileEdge + Mathf.RoundToInt(Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset.y / Singleton.Manager<ManWorld>.inst.CellScale);
		bool flag = patchType == SceneryPatchType.OptionalSceneryClutter;
		List<SceneryPatch> list = m_SceneryPatchesToGenerate[(int)patchType];
		for (int i = 0; i < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; i += sceneryPatchSize)
		{
			for (int j = 0; j < Singleton.Manager<ManWorld>.inst.CellsPerTileEdge; j += sceneryPatchSize)
			{
				Vector3 centre = tile.WorldOrigin + new Vector3(i, 0f, j) * Singleton.Manager<ManWorld>.inst.CellScale + vector;
				int num4 = Mathf.Min(i + sceneryPatchSize, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge);
				int num5 = Mathf.Min(j + sceneryPatchSize, Singleton.Manager<ManWorld>.inst.CellsPerTileEdge);
				m_SpawnListWorking.Clear();
				for (int k = j; k < num5; k++)
				{
					for (int l = i; l < num4; l++)
					{
						ref BiomeMap.SpawnDetail reference = ref tile.BiomeMapData.sceneryPlacement[k, l];
						if (!(reference.t0 == null) || !(reference.t1 == null) || !(reference.t2 == null))
						{
							IntVector2 cellCoord = new IntVector2(l, k);
							Vector3 position = new Vector3(cellCoord.x, 0f, cellCoord.y) * Singleton.Manager<ManWorld>.inst.CellScale;
							int seed = num2 + cellCoord.x;
							int seed2 = num3 + cellCoord.y;
							m_DRNG.SetSeed((uint)seed, (uint)seed2);
							if (num != 0f)
							{
								Vector2 vector2 = new Vector2(m_DRNG.OnePosNeg(), m_DRNG.OnePosNeg());
								position += (vector2 * num).ToVector3XZ();
							}
							Vector2 rotHV = new Vector2(m_DRNG.One(), m_DRNG.OneInclusive());
							if ((bool)reference.t0 && reference.t0.IsSimpleMeshMergeable == flag)
							{
								m_SpawnListWorking.Add(new SpawnData(reference.t0, cellCoord, position, rotHV, reference.scale));
							}
							if ((bool)reference.t1 && reference.t1.IsSimpleMeshMergeable == flag)
							{
								m_SpawnListWorking.Add(new SpawnData(reference.t1, cellCoord, position, rotHV, reference.scale));
							}
							if ((bool)reference.t2 && reference.t2.IsSimpleMeshMergeable == flag)
							{
								m_SpawnListWorking.Add(new SpawnData(reference.t2, cellCoord, position, rotHV, reference.scale));
							}
						}
					}
				}
				if (tile.BiomeMapData.setPiece.IsNotNull() && patchType == SceneryPatchType.PopulatedTileScenery)
				{
					tile.BiomeMapData.setPiece.AddTerrainObjects(tile.BiomeMapData.setPieceOffset, new IntVector2(i, j), sceneryPatchSize, m_SpawnListWorking, num, m_DRNG);
				}
				if (m_SpawnListWorking.Count != 0)
				{
					SceneryPatch andInitPatchFromPool = GetAndInitPatchFromPool(tile, centre, m_SpawnListWorking, mergeMeshes, patchType == SceneryPatchType.OptionalSceneryClutter && Globals.inst.m_InstanceSceneryClutter);
					lock (list)
					{
						list.Add(andInitPatchFromPool);
						tile.patchesToPopulate[(int)patchType]++;
					}
					m_NeedSortSceneryPatches = true;
				}
			}
		}
	}

	private SceneryPatch GetAndInitPatchFromPool(WorldTile tile, Vector3 centre, List<SpawnData> spawnList, bool mergeMeshes, bool instancedClutter)
	{
		int num = Mathf.Max(Mathf.NextPowerOfTwo(spawnList.Count), 8);
		SceneryPatch sceneryPatch;
		lock (m_PatchPools)
		{
			SceneryPatchPool value = default(SceneryPatchPool);
			if (!m_PatchPools.TryGetValue(num, out value))
			{
				value = new SceneryPatchPool
				{
					patches = new Stack<SceneryPatch>(16),
					maxPrefabs = num
				};
				m_PatchPools.Add(num, value);
			}
			if (value.patches.Count == 0)
			{
				value.patches.Push(new SceneryPatch(num));
			}
			sceneryPatch = value.patches.Pop();
		}
		sceneryPatch.Init(tile, centre, spawnList, mergeMeshes, instancedClutter);
		return sceneryPatch;
	}

	public void AddNetTile(IntVector2 tileCoord, NetWorldTile netTile)
	{
		if (m_NetWorldTiles.TryGetValue(tileCoord, out var value))
		{
			d.LogWarning($"[TileManager] Adding NetWorldTile for tile {tileCoord.x},{tileCoord.y} when one is already present {value.name}");
		}
		m_NetWorldTiles[tileCoord] = netTile;
	}

	public void RemoveNetTile(IntVector2 tileCoord, NetWorldTile netTile)
	{
		if (m_NetWorldTiles.TryGetValue(tileCoord, out var value) && value == netTile)
		{
			m_NetWorldTiles.Remove(tileCoord);
		}
		else
		{
			d.LogWarning($"[TileManager] Removing NetWorldTile for tile {tileCoord.x},{tileCoord.y} but it's not in the dictionary");
		}
	}

	private void ReturnPatchToPool(SceneryPatch patch)
	{
		lock (m_PatchPools)
		{
			d.Assert(m_PatchPools.ContainsKey(patch.StorageSize));
			m_PatchPools[patch.StorageSize].patches.Push(patch);
		}
	}

	private void UpdateTileRequestStates(List<IntVector2> tileCoordsToCreate)
	{
		if (!m_ClearAll && Singleton.Manager<ManWorld>.inst.CurrentBiomeMap != null)
		{
			if (m_FixedTilesLoaded.Count == 0 && m_FixedTilesUnpopulated.Count == 0)
			{
				UpdateTileRequestStatesInStandardMode(tileCoordsToCreate);
			}
			else
			{
				UpdateTileRequestStatesInFixedMode(tileCoordsToCreate);
			}
		}
		else
		{
			Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Value.m_RequestState = WorldTile.State.Empty;
			}
		}
	}

	private void UpdateTileRequestStatesInStandardMode(List<IntVector2> tileCoordsToCreate)
	{
		Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.Value.m_RequestState = WorldTile.State.Empty;
		}
		float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
		IntVector2 intVector = DetermineFocalTileCoord();
		Vector3 scenePosition = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition;
		float farClipPlane = Singleton.camera.farClipPlane;
		float range = farClipPlane + tileSize * 0.5f;
		Plane[] planes = Util.CalculateFrustumPlanes(Singleton.camera);
		Vector3 size = new Vector3(tileSize, 100f, tileSize);
		TilesWithinRadius enumerator2 = new TilesWithinRadius(scenePosition, range, centreOnly: true).GetEnumerator();
		while (enumerator2.MoveNext())
		{
			IntVector2 tileCoordWorld = enumerator2.Current;
			if (m_TileLookup.TryGetValue(tileCoordWorld, out var value))
			{
				if (!value.m_Regenerate)
				{
					value.m_RequestState = WorldTile.State.Created;
				}
				continue;
			}
			Bounds bounds = new Bounds(CalcTileCentreScene(in tileCoordWorld), size);
			if (GeometryUtility.TestPlanesAABB(planes, bounds))
			{
				tileCoordsToCreate.Add(tileCoordWorld);
			}
		}
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				IntVector2 intVector2 = intVector + new IntVector2(j, i);
				if (m_TileLookup.TryGetValue(intVector2, out var value2))
				{
					if (!value2.m_Regenerate)
					{
						value2.m_RequestState = WorldTile.State.Loaded;
					}
				}
				else if (!tileCoordsToCreate.Contains(intVector2))
				{
					tileCoordsToCreate.Add(intVector2);
				}
			}
		}
		float b = QualitySettingsExtended.DetailDistanceRange.Lerp(Singleton.Manager<CameraManager>.inst.DetailDist01);
		b = Mathf.Min(farClipPlane, b);
		float range2 = b + tileSize * 0.5f;
		enumerator2 = new TilesWithinRadius(scenePosition, range2, centreOnly: true).GetEnumerator();
		while (enumerator2.MoveNext())
		{
			IntVector2 tileCoordWorld2 = enumerator2.Current;
			if (m_TileLookup.TryGetValue(tileCoordWorld2, out var value3))
			{
				if (!value3.m_Regenerate)
				{
					bool flag = value3.m_LoadStep >= WorldTile.LoadStep.PopulatingScenery;
					if (!flag)
					{
						Bounds bounds2 = new Bounds(CalcTileCentreScene(in tileCoordWorld2), size);
						flag = GeometryUtility.TestPlanesAABB(planes, bounds2);
					}
					WorldTile.State state = ((!flag) ? WorldTile.State.Created : WorldTile.State.Populated);
					if (state > value3.m_RequestState)
					{
						value3.m_RequestState = state;
					}
				}
			}
			else if (!tileCoordsToCreate.Contains(tileCoordWorld2))
			{
				tileCoordsToCreate.Add(tileCoordWorld2);
			}
		}
	}

	private IntVector2 DetermineFocalTileCoord()
	{
		IntVector2 intVector = Singleton.Manager<ManWorld>.inst.FocalPoint.TileCoord;
		if (m_HasLastFocalCoord)
		{
			Vector3 scenePosition = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition;
			float num = TileToScenePointDistance(intVector, scenePosition);
			float num2 = TileToScenePointDistance(m_LastFocalCoord, scenePosition);
			float num3 = Singleton.Manager<ManWorld>.inst.FocussedTileExtraWeighting * Singleton.Manager<ManWorld>.inst.TileSize;
			if (!(num + num3 < num2))
			{
				intVector = m_LastFocalCoord;
			}
		}
		m_LastFocalCoord = intVector;
		m_HasLastFocalCoord = true;
		return intVector;
	}

	private void UpdateTileRequestStatesInFixedMode(List<IntVector2> tileCoordsToCreate)
	{
		foreach (IntVector2 item in m_FixedTilesLoaded)
		{
			if (m_TileLookup.TryGetValue(item, out var value))
			{
				value.m_RequestState = WorldTile.State.Loaded;
			}
			else
			{
				tileCoordsToCreate.Add(item);
			}
		}
		foreach (IntVector2 item2 in m_FixedTilesUnpopulated)
		{
			if (m_TileLookup.TryGetValue(item2, out var value2))
			{
				value2.m_RequestState = WorldTile.State.Created;
			}
			else
			{
				tileCoordsToCreate.Add(item2);
			}
		}
		Dictionary<IntVector2, WorldTile>.Enumerator enumerator2 = m_TileLookup.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			WorldTile value3 = enumerator2.Current.Value;
			if (!m_FixedTilesLoaded.Contains(value3.Coord) && !m_FixedTilesUnpopulated.Contains(value3.Coord))
			{
				value3.m_RequestState = WorldTile.State.Empty;
			}
		}
	}

	public void RegenerateTilesForSetPiece(IntVector2 tileMin, IntVector2 tileMax)
	{
		IntVector2 intVector = tileMin;
		while (intVector.y <= tileMax.y)
		{
			intVector.x = tileMin.x;
			while (intVector.x <= tileMax.x)
			{
				m_TileLookup.TryGetValue(intVector, out var value);
				if (value != null && value.m_LoadStep != WorldTile.LoadStep.Empty && !value.m_Regenerate)
				{
					if (value.HasReachedLoadState(WorldTile.State.Loaded))
					{
						d.LogWarning($"[TileManager] Setting loaded tile {intVector} to regenerate, as it overlaps set piece ({tileMin} - {tileMax})");
					}
					else
					{
						d.Log($"[TileManager] Setting tile {intVector} to regenerate, as it overlaps set piece ({tileMin} - {tileMax})");
					}
					value.m_RequestState = WorldTile.State.Empty;
					value.m_Regenerate = true;
				}
				intVector.x++;
			}
			intVector.y++;
		}
	}

	private int GetWorkBudget()
	{
		if (Singleton.Manager<CameraManager>.inst.TeleportEffect.IsFadedOut)
		{
			return int.MaxValue;
		}
		if (Singleton.Manager<ManGameMode>.inst.GetModePhase() != ManGameMode.GameState.InGame)
		{
			return 100;
		}
		return 10;
	}

	private void HandleTileUnloading(Stopwatch stopwatch, int workBudgetMS)
	{
		bool flag;
		do
		{
			flag = false;
			Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
			while (enumerator.MoveNext())
			{
				WorldTile value = enumerator.Current.Value;
				if (value.m_LoadStep > WorldTile.LoadStep.Created && value.m_LoadStep <= WorldTile.LoadStep.StartPopulatingScenery && value.m_RequestState < WorldTile.State.Populated && GetThreadForLoadingStep(value) != ResponsibleThread.Generation)
				{
					value.ReleaseSaveState();
					value.m_LoadStep = WorldTile.LoadStep.Created;
					flag = true;
				}
				if (value.m_LoadStep <= WorldTile.LoadStep.Created && value.m_RequestState == WorldTile.State.Empty && GetThreadForLoadingStep(value) != ResponsibleThread.Generation && !m_TilesToRecycleWorking.Contains(value))
				{
					m_TilesToRecycleWorking.Add(value);
					flag = true;
				}
				if (value.m_LoadStep > WorldTile.LoadStep.StartPopulatingScenery && value.m_LoadStep <= WorldTile.LoadStep.Populated && value.m_RequestState < WorldTile.State.Populated && GetThreadForLoadingStep(value) != ResponsibleThread.Generation)
				{
					int maxMilliseconds = workBudgetMS - (int)stopwatch.ElapsedMilliseconds;
					if (value.SaveData.DespawnScenery(value.Visibles, maxMilliseconds))
					{
						value.ClearSceneryVisibles();
						value.ClearStaticChildren(includeTerrain: false);
						m_TilesToCacheTileInfoFor.Remove(value);
						value.ReleaseSaveState();
						value.m_LoadStep = WorldTile.LoadStep.Created;
						TileDepopulatedEvent.Send(value);
					}
					flag = true;
					break;
				}
				if (!value.IsLoaded || value.m_RequestState >= WorldTile.State.Loaded)
				{
					continue;
				}
				if (value.NetTile != null)
				{
					if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && value.NetTile.gameObject.activeInHierarchy)
					{
						NetworkServer.UnSpawn(value.NetTile.gameObject);
						value.NetTile.transform.Recycle();
					}
					value.NetTile = null;
				}
				if (m_UnloadBehaviour == UnloadBehaviour.ExternallyControlled)
				{
					value.ClearNonSceneryVisibles();
					value.SaveData.DespawnPersistentTerrainObjects(value.ManuallyAddedTerrainObjects);
				}
				else
				{
					bool flag2 = false;
					if (value.SaveData != null)
					{
						flag2 = value.SaveData.m_StoredVisibles.TryGetValue(1, out var value2) && value2 != null && value2.Count > 0;
					}
					value.SaveData.StoreVisibles(value.Visibles, value.StoredVisiblesWaitingToLoad);
					value.ClearNonSceneryVisibles();
					bool flag3 = false;
					if (value.SaveData != null)
					{
						flag3 = value.SaveData.m_StoredVisibles.TryGetValue(1, out var value3) && value3 != null && value3.Count > 0;
						if (flag3)
						{
							for (int i = 0; i < value3.Count && !(value3[i] as ManSaveGame.StoredTech).m_IsPlayerFocus; i++)
							{
							}
						}
					}
					if (flag2 || flag3)
					{
						_ = Singleton.playerTank;
					}
					value.SaveData.StorePersistentTerrainObjects(value.ManuallyAddedTerrainObjects);
					value.SaveData.DespawnPersistentTerrainObjects(value.ManuallyAddedTerrainObjects);
					value.ClearPersistentTerrainObjects();
					value.SaveData.StoreScenery(value.Visibles);
				}
				value.SaveData.SetSceneryAwake(value.Visibles, awake: false);
				value.Terrain.castShadows = false;
				value.m_LoadStep = WorldTile.LoadStep.Populated;
				TileUnloadedEvent.Send(value);
				flag = true;
				break;
			}
		}
		while (flag && stopwatch.ElapsedMilliseconds < workBudgetMS);
	}

	private void RemoveOldTiles()
	{
		foreach (WorldTile item in m_TilesToRecycleWorking)
		{
			d.Assert(m_TileLookup.Remove(item.Coord), $"Failed to remove tile {item.Coord} from TileLookup!?");
			item.m_LoadStep = WorldTile.LoadStep.Empty;
			TileDestroyedEvent.Send(item);
			DestroyTile(item);
			ReturnTileToPool(item);
		}
		m_TilesToRecycleWorking.Clear();
	}

	private void CreateNewTiles()
	{
		foreach (IntVector2 item in m_TileCoordsToCreateWorking)
		{
			WorldTile tileFromPool = GetTileFromPool(item);
			tileFromPool.m_RequestState = WorldTile.State.Created;
			d.Assert(!m_TileLookup.ContainsKey(item), string.Format("Tile at coord {0} was marked for creation, but the tile already exists as {1} going to {2}!", item, m_TileLookup.TryGetValue(item, out var value) ? value.m_LoadStep.ToString() : "", m_TileLookup.TryGetValue(item, out var value2) ? value2.m_RequestState.ToString() : ""));
			m_TileLookup[item] = tileFromPool;
		}
	}

	private void HandleTileLoading(Stopwatch stopwatch, int workBudgetMS)
	{
		bool flag = false;
		bool flag2;
		bool flag3;
		do
		{
			flag2 = false;
			flag3 = false;
			WorldTile worldTile = null;
			float num = 0f;
			Dictionary<IntVector2, WorldTile>.Enumerator enumerator = m_TileLookup.GetEnumerator();
			while (enumerator.MoveNext())
			{
				WorldTile value = enumerator.Current.Value;
				if (value.HasReachedRequestedState())
				{
					continue;
				}
				if (value.m_LoadStep == WorldTile.LoadStep.Empty)
				{
					value.m_LoadStep = WorldTile.LoadStep.GeneratingTerrain;
					flag2 = true;
				}
				if (value.m_LoadStep == WorldTile.LoadStep.Created)
				{
					value.m_LoadStep = WorldTile.LoadStep.GeneratingScenery;
					flag2 = true;
				}
				if (value.m_LoadStep == WorldTile.LoadStep.PopulatingScenery && value.patchesToPopulate[0] <= 0)
				{
					if (!ManNetwork.IsHost && m_NetWorldTiles.TryGetValue(value.Coord, out var value2))
					{
						value2.ApplyToTile(value);
					}
					value.SaveData.SetSceneryAwake(value.Visibles, awake: false);
					value.m_LoadStep = WorldTile.LoadStep.Populated;
					m_TilesToCacheTileInfoFor.Add(value);
					TilePopulatedEvent.Send(value);
					flag2 = true;
				}
				if (value.m_LoadStep == WorldTile.LoadStep.Populated && value.m_RequestState == WorldTile.State.Loaded)
				{
					value.m_LoadStep = WorldTile.LoadStep.Loading;
					if (!physicsBounds.Contains(value.CalcSceneCentre()))
					{
						flag = true;
					}
					flag2 = true;
				}
				if (GetThreadForLoadingStep(value) == ResponsibleThread.Main)
				{
					float num2 = CalcTileLoadPriority(value);
					if (worldTile == null || num2 > num)
					{
						worldTile = value;
						num = num2;
					}
				}
				if (!value.HasReachedRequestedState())
				{
					flag3 = true;
				}
			}
			if (worldTile != null)
			{
				switch (worldTile.m_LoadStep)
				{
				case WorldTile.LoadStep.Creating:
					CreateTile(worldTile);
					TileCreatedEvent.Send(worldTile);
					flag2 = true;
					break;
				case WorldTile.LoadStep.StartPopulatingScenery:
					worldTile.AcquireSaveState();
					TileStartPopulatingEvent.Send(worldTile);
					worldTile.m_LoadStep = WorldTile.LoadStep.PopulatingScenery;
					flag2 = true;
					break;
				case WorldTile.LoadStep.PopulatingScenery:
					flag2 = PopulateSceneryPatches(SceneryPatchType.PopulatedTileScenery, worldTile);
					break;
				case WorldTile.LoadStep.Loading:
					LoadTile(worldTile);
					flag2 = true;
					break;
				default:
					d.AssertFormat(false, "Update: Unsure how to progress with tile in load step {0}", worldTile.m_LoadStep);
					break;
				}
			}
		}
		while (flag2 && stopwatch.ElapsedMilliseconds < workBudgetMS);
		if (flag3 && workBudgetMS != int.MaxValue)
		{
			IsGenerating = true;
		}
		if (flag)
		{
			UpdatePhysicsBounds();
		}
	}

	private static float CalcTileLoadPriority(WorldTile tile)
	{
		float num;
		switch (tile.m_LoadStep)
		{
		case WorldTile.LoadStep.GeneratingTerrain:
			num = 20f;
			break;
		case WorldTile.LoadStep.GeneratingScenery:
			num = 10f;
			break;
		case WorldTile.LoadStep.Creating:
			num = 20f;
			break;
		case WorldTile.LoadStep.StartPopulatingScenery:
			num = 1f;
			break;
		case WorldTile.LoadStep.PopulatingScenery:
			num = 0f;
			break;
		case WorldTile.LoadStep.Loading:
			num = 40f;
			break;
		case WorldTile.LoadStep.Empty:
		case WorldTile.LoadStep.Created:
		case WorldTile.LoadStep.Populated:
		case WorldTile.LoadStep.Loaded:
			d.LogError("Trying to calculate a priority for an idle state");
			num = -1f;
			break;
		default:
			d.LogError("Trying to calculate a priority for an unsupported state " + tile.m_LoadStep);
			num = -1f;
			break;
		}
		if (Singleton.Manager<ManWorld>.inst.FocalPoint.TileCoord == tile.Coord)
		{
			num = 100f;
		}
		else
		{
			float num2 = 0f;
			Vector2 vector = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition.ToVector2XZ();
			Vector2 vector2 = tile.CalcSceneCentre().ToVector2XZ();
			float tileSize = Singleton.Manager<ManWorld>.inst.TileSize;
			float num3 = CalcDistancePriority(new Bounds(vector2, new Vector3(tileSize, 0f, tileSize)).ClosestPoint(vector), vector);
			num2 += num3 * 6f;
			if (GetThreadForLoadingStep(tile) == ResponsibleThread.Main)
			{
				float num4 = CalcViewDirectionPriority(vector2, vector);
				num2 += num4 * 4f;
			}
			num += Mathf.Clamp(num2, 0f, 9.9f);
			if (tile.m_RequestState == WorldTile.State.Loaded)
			{
				num += 10f;
			}
		}
		return num;
	}

	public static float CalcDistancePriority(Vector2 position, Vector2 referencePos)
	{
		float num = Mathf.Sqrt((position - referencePos).sqrMagnitude);
		float num2 = Singleton.Manager<ManWorld>.inst.TileSize * 5f;
		return 1f - Mathf.Clamp01(num / num2);
	}

	public static float CalcViewDirectionPriority(Vector2 position, Vector2 referencePos)
	{
		return Vector2.Dot((position - referencePos).normalized, Singleton.cameraTrans.forward.ToVector2XZ().normalized) * 0.5f + 0.5f;
	}
}
