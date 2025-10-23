#define UNITY_EDITOR
using UnityEngine;

public class TerrainObject : TrackableObject
{
	public struct SpawnedTerrainObjectData
	{
		public TerrainObject TerrainObject;

		public TrackedObjectReference TrackedObjectRef;
	}

	[SerializeField]
	private bool m_AllowHorizontalRotation = true;

	[SerializeField]
	[Range(0f, 180f)]
	private float m_MaxVerticalAngularDisplacement;

	[SerializeField]
	[Range(0f, 1f)]
	private float m_GroundNormalUpWeighting;

	[SerializeField]
	private bool m_IsLandmark;

	[SerializeField]
	[HideInInspector]
	private bool m_IsSimpleMeshMergeable;

	private Transform trans;

	[SerializeField]
	[HideInInspector]
	private float m_GroundRadius;

	[SerializeField]
	[HideInInspector]
	private string m_PersistentObjectGUID;

	private static Transform s_SpawningPrefab;

	private static WorldTile s_SpawningSetTile;

	private static IntVector2 s_SpawningSetTileCellCoord;

	public static readonly IntVector2 kTCC_NotSet = new IntVector2(int.MinValue, int.MaxValue);

	public static readonly IntVector2 kTCC_ManualSaved = new IntVector2(int.MaxValue, int.MinValue);

	public static readonly IntVector2 kTCC_Invalid = new IntVector2(int.MinValue, int.MinValue);

	public IntVector2 TileCellCoord { get; private set; } = kTCC_NotSet;

	public WorldTile Tile { get; private set; }

	public IntVector2 TileCoordsMin { get; private set; }

	public IntVector2 TileCoordsMax { get; private set; }

	public Visible visible { get; private set; }

	public string PrefabGUID => m_PersistentObjectGUID;

	public bool WasPlacedManually
	{
		get
		{
			if (!(TileCellCoord == kTCC_Invalid))
			{
				return TileCellCoord == kTCC_ManualSaved;
			}
			return true;
		}
	}

	public bool IsSimpleMeshMergeable => m_IsSimpleMeshMergeable;

	public float GroundRadius => m_GroundRadius;

	public bool IsLandmark => m_IsLandmark;

	public bool AllowHorizontalRotation => m_AllowHorizontalRotation;

	public Quaternion GetNormalWeightedSpawnOrientation(WorldTile tile, Vector2 tileRelativePos, Vector2 rotationHV)
	{
		Vector3 interpolatedNormal = tile.Terrain.terrainData.GetInterpolatedNormal(tileRelativePos.x, tileRelativePos.y);
		return GetNormalWeightedSpawnOrientation(interpolatedNormal, rotationHV);
	}

	public Quaternion GetNormalWeightedSpawnOrientation(Vector3 groundNormal, Vector2 rotationHV)
	{
		Quaternion quaternion = Quaternion.identity;
		if (m_GroundNormalUpWeighting != 0f)
		{
			quaternion = Quaternion.FromToRotation(Vector3.up, groundNormal);
			if (m_GroundNormalUpWeighting != 1f)
			{
				quaternion = Quaternion.LerpUnclamped(Quaternion.identity, quaternion, m_GroundNormalUpWeighting);
			}
		}
		if (m_AllowHorizontalRotation)
		{
			quaternion *= Quaternion.Euler(rotationHV.x * m_MaxVerticalAngularDisplacement, rotationHV.y * 360f, 0f);
		}
		return quaternion;
	}

	public SpawnedTerrainObjectData SpawnFromPrefabAndAddToSaveData(Vector3 pos, Quaternion rot, bool trackObject = false, float scale = 1f)
	{
		TerrainObject terrainObject = null;
		TrackedObjectReference trackedObjectReference = null;
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in pos);
		if (worldTile != null && worldTile.IsLoaded)
		{
			terrainObject = SpawnFromPrefab(worldTile, pos, rot, scale, kTCC_ManualSaved).GetComponent<TerrainObject>();
			terrainObject.AddToTileData();
			if (terrainObject.visible.IsNotNull() && terrainObject.visible.resdisp.IsNotNull())
			{
				terrainObject.visible.resdisp.SetAwake(awake: true);
			}
			if (trackObject)
			{
				trackedObjectReference = terrainObject.StartTrackingObject();
			}
		}
		else
		{
			ManSaveGame.StoredTile storedTileIfNotSpawned = Singleton.Manager<ManWorld>.inst.TileManager.GetStoredTileIfNotSpawned(in pos);
			uint num = 0u;
			if (trackObject)
			{
				num = Singleton.Manager<ManSaveGame>.inst.CurrentState.GetNextTrackableObjectID();
				trackedObjectReference = new TrackedObjectReference(new SavedTrackedObject
				{
					m_Id = num,
					m_Position = WorldPosition.FromScenePosition(in pos)
				});
				Singleton.Manager<ManVisible>.inst.TrackObject(trackedObjectReference);
			}
			if (storedTileIfNotSpawned != null)
			{
				storedTileIfNotSpawned.AddPersistentTerrainObjectToSaveData(this, pos, rot, trackObject, num);
			}
			else
			{
				d.LogErrorFormat("TerrainObject.SpawnFromPrefabAndAddToSaveData - Trying to add object {0} directly to save data at position {1}, but failed to get a stored tile for the location!", trans.name, pos + Singleton.Manager<ManWorld>.inst.SceneToGameWorld);
			}
		}
		return new SpawnedTerrainObjectData
		{
			TerrainObject = terrainObject,
			TrackedObjectRef = trackedObjectReference
		};
	}

	public Transform SpawnFromPrefab(WorldTile tile, Vector3 pos, Quaternion rot)
	{
		return SpawnFromPrefab(tile, pos, rot, 1f, kTCC_Invalid);
	}

	public Transform SpawnFromPrefab(WorldTile tile, Vector3 pos, Quaternion rot, float scale, IntVector2 cellCoord)
	{
		if (!trans)
		{
			trans = base.transform;
		}
		s_SpawningPrefab = trans;
		s_SpawningSetTile = tile;
		s_SpawningSetTileCellCoord = cellCoord;
		return trans.Spawn(tile.StaticParent, pos, rot, scale);
	}

	public void SetGroundRadius(float groundRadius)
	{
		if (!m_IsLandmark)
		{
			m_GroundRadius = groundRadius;
		}
		else
		{
			d.LogError("TerrainObject.SetGroundRadius - Trying to set ground radius on landmark");
		}
	}

	public Bounds GetSceneBounds()
	{
		return new Bounds(trans.position, Vector3.one * m_GroundRadius * 2f);
	}

	private void InitNavigationData()
	{
		if (!trans)
		{
			trans = base.transform;
		}
		if (Tile == null || m_GroundRadius == 0f)
		{
			return;
		}
		Bounds sceneBounds = GetSceneBounds();
		Singleton.Manager<ManWorld>.inst.TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
		TileCoordsMin = min;
		TileCoordsMax = max;
		bool flag = min != max;
		Tile.AddTerrainObjectNav(this, flag);
		if (flag)
		{
			TileManager.TileIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateTiles(TileCoordsMin, TileCoordsMax).GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.AddTerrainObjectNav(this, flag);
			}
		}
	}

	private void AddToTileData()
	{
		if (Tile != null)
		{
			Tile.AddPersistentTerrainObject(this);
		}
		else
		{
			d.LogError(string.Concat("TerrainObject.AddToSaveData - Tile at world pos ", base.transform.position, " was null! Could not add to save data!"));
		}
	}

	private void RemoveFromTileData()
	{
		if (Tile != null)
		{
			Tile.RemovePersistentTerrainObject(this);
		}
		else
		{
			d.LogError($"TerrainObject.RemoveFromTileData - Trying to remove {base.name} but Tile at world pos {base.transform.position} was null! Could not remove from save data!");
		}
	}

	private void PrePool()
	{
		if (!m_IsLandmark)
		{
			return;
		}
		MeshCollider[] componentsInChildren = GetComponentsInChildren<MeshCollider>(includeInactive: true);
		if (componentsInChildren.Length != 0)
		{
			Bounds bounds = componentsInChildren[0].bounds;
			MeshCollider[] array = componentsInChildren;
			foreach (MeshCollider meshCollider in array)
			{
				bounds.Encapsulate(meshCollider.bounds);
			}
			m_GroundRadius = Mathf.Max(bounds.extents.x, bounds.extents.z);
		}
		m_GroundRadius = Mathf.Sqrt(2f * (m_GroundRadius * m_GroundRadius));
	}

	private void OnPool()
	{
		trans = base.transform;
		visible = GetComponent<Visible>();
	}

	private void OnSpawn()
	{
		if (s_SpawningPrefab != null)
		{
			d.AssertFormat(trans.GetOriginalPrefab() == s_SpawningPrefab, "Spawn-time prefab {0} doesn't match expected cached prefab: {1}", trans.GetOriginalPrefab()?.name, s_SpawningPrefab.name);
			Tile = s_SpawningSetTile;
			TileCellCoord = s_SpawningSetTileCellCoord;
			s_SpawningSetTile = null;
			s_SpawningSetTileCellCoord = kTCC_NotSet;
		}
		else
		{
			Tile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(trans.position);
			TileCellCoord = kTCC_Invalid;
		}
		s_SpawningPrefab = null;
		s_SpawningSetTile = null;
		InitNavigationData();
	}

	private void OnRecycle()
	{
		if (!PrefabGUID.NullOrEmpty() && TileCellCoord == kTCC_ManualSaved)
		{
			RemoveFromTileData();
		}
		if (Tile != null && m_GroundRadius != 0f && TileCoordsMin != IntVector2.invalid && TileCoordsMax != IntVector2.invalid && TileCoordsMin != TileCoordsMax)
		{
			TileManager.TileIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateTiles(TileCoordsMin, TileCoordsMax).GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.RemoveOverlappingTerrainObject(this);
			}
		}
		Tile = null;
		TileCellCoord = kTCC_NotSet;
	}
}
