#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(EncounterDetails))]
public class Encounter : MonoBehaviour
{
	public enum MultiplayerMessageMode
	{
		AllPlayersInEncounterRadius,
		AllPlayers
	}

	public enum TriggerShape
	{
		None,
		Sphere,
		Box,
		Cylinder
	}

	[Serializable]
	public class EncounterSpawnPosition
	{
		public string m_PTName;

		public PositionWithFacing m_Position;

		public Color m_Colour = new Color(0f, 0f, 0f, 0.5f);

		public ObjectTypes m_ObjectType;

		public UnityEngine.Object m_Object;

		public float m_Size;

		public float m_SizeY;

		public float m_SizeZ;

		public float m_FlatTerrainRadius;

		public TriggerShape m_TriggerShape;

		public bool m_IsSpawnBlocker;

		public bool m_CanStartWithoutTile;

		public bool m_UseForInitialWaypointPos;

		public EncounterSpawnPosition(PositionWithFacing posWithFacing)
		{
			m_Position = posWithFacing;
		}

		public float UpdateSize()
		{
			switch (m_ObjectType)
			{
			case ObjectTypes.Block:
			{
				TankBlock tankBlock = m_Object as TankBlock;
				if ((bool)tankBlock)
				{
					Bounds blockCellBounds = tankBlock.BlockCellBounds;
					m_Size = new Vector3((blockCellBounds.extents.x == 0f) ? 1f : blockCellBounds.extents.x, (blockCellBounds.extents.z == 0f) ? 1f : blockCellBounds.extents.z).magnitude;
				}
				break;
			}
			case ObjectTypes.Vehicle:
			{
				TankPreset tankPreset = m_Object as TankPreset;
				if ((bool)tankPreset)
				{
					m_Size = new Vector3((tankPreset.Bounds.x == 0) ? 1 : tankPreset.Bounds.x, (tankPreset.Bounds.z == 0) ? 1 : tankPreset.Bounds.z).magnitude;
				}
				break;
			}
			}
			return m_Size;
		}
	}

	public struct TriggerTester
	{
		private EncounterSpawnPosition m_SpawnPos;

		private Matrix4x4 m_TransformToLocal;

		public TriggerTester(EncounterSpawnPosition spawnPos, float offsetHeight, Transform encounterTransform)
		{
			m_SpawnPos = spawnPos;
			offsetHeight -= encounterTransform.position.y;
			m_TransformToLocal = Matrix4x4.Rotate(Quaternion.Inverse(spawnPos.m_Position.orientation)) * Matrix4x4.Translate(-spawnPos.m_Position.position - Vector3.up * offsetHeight) * encounterTransform.worldToLocalMatrix;
		}

		public bool Test(Vector3 worldPos, float radius)
		{
			Vector3 coord = m_TransformToLocal.MultiplyPoint(worldPos);
			switch (m_SpawnPos.m_TriggerShape)
			{
			case TriggerShape.None:
			case TriggerShape.Sphere:
				return coord.sqrMagnitude <= (m_SpawnPos.m_Size + radius) * (m_SpawnPos.m_Size + radius);
			case TriggerShape.Box:
				if (Mathf.Abs(coord.x) <= m_SpawnPos.m_Size + radius && (m_SpawnPos.m_SizeY <= 0f || Mathf.Abs(coord.y) <= m_SpawnPos.m_SizeY + radius))
				{
					return Mathf.Abs(coord.z) <= m_SpawnPos.m_SizeZ + radius;
				}
				return false;
			case TriggerShape.Cylinder:
				if (m_SpawnPos.m_SizeY <= 0f || Mathf.Abs(coord.y) <= m_SpawnPos.m_SizeY + radius)
				{
					return coord.ToVector2XZ().sqrMagnitude <= (m_SpawnPos.m_Size + radius) * (m_SpawnPos.m_Size + radius);
				}
				return false;
			default:
				return false;
			}
		}
	}

	public class SaveData
	{
		public EncounterIdentifier m_EncounterDef = EncounterIdentifier.Invalid;

		public bool m_CurrentlyActive;

		public bool m_IsScriptUpdateEnabled;

		public bool m_VisibleInLog;

		public int m_EncounterStringBankIdx;

		public bool m_CrateDelivered;

		public int m_CrateId;

		public V3Serial m_Position;

		public WorldPosition m_PositionWorld;

		public QuatSerial m_Rotation;

		public int m_WaypointVisibleId = -1;

		public int m_TrackedVisID = -1;

		public bool m_ShowAreaOnMiniMap;

		public Vector3 m_DefaultWaypointOffset;

		public EncounterTimer m_EncounterTimer;

		public Dictionary<string, string> m_StoredData = new Dictionary<string, string>();

		public Dictionary<string, EncounterVisibleData> m_SpawnedVisibles = new Dictionary<string, EncounterVisibleData>();

		public Dictionary<string, uint> m_TrackedObjectLookup = new Dictionary<string, uint>();

		public List<EncounterObject> m_StoredObjects = new List<EncounterObject>();

		public Dictionary<string, ObjectSpawner.SaveData> m_SpawnerData = new Dictionary<string, ObjectSpawner.SaveData>();

		public MultiObjectSpawner.DebugHistory m_SpawnHistory = new MultiObjectSpawner.DebugHistory();

		public List<ChallengeData.SafeAreaData> m_CallengeSafeAreas = new List<ChallengeData.SafeAreaData>();

		public void Load()
		{
			foreach (EncounterObject storedObject in m_StoredObjects)
			{
				Transform value = null;
				if (!Singleton.Manager<ManEncounter>.inst.m_EncounterObjectsPrefabs.TryGetValue(storedObject.m_PrefabName, out value))
				{
					continue;
				}
				Vector3 position = storedObject.m_Position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
				if (storedObject.m_WorldPosition != default(WorldPosition) && storedObject.m_Position == Vector3.zero)
				{
					position = storedObject.m_WorldPosition.ScenePosition;
				}
				Visible.DisableAddToTileOnSpawn = true;
				Transform transform = value.Spawn(position, storedObject.m_Rotation);
				Visible.DisableAddToTileOnSpawn = false;
				if (transform != null)
				{
					Visible component = transform.GetComponent<Visible>();
					if (component != null)
					{
						component.SetManagedByTile(managed: false);
					}
				}
				storedObject.m_Trans = transform;
				transform.gameObject.SetActive(storedObject.m_Active);
			}
		}

		public void LookupVisibles()
		{
			foreach (EncounterVisibleData item in m_SpawnedVisibles.Select((KeyValuePair<string, EncounterVisibleData> x) => x.Value))
			{
				item.Load();
			}
		}

		public void Save(Encounter encounter)
		{
			foreach (EncounterObject storedObject in m_StoredObjects)
			{
				storedObject.UpdateObjectData();
			}
			m_EncounterTimer = encounter.m_QuestLog.EncounterTimer;
		}

		public void AddEncounterData(string key, string data)
		{
			if (m_StoredData.ContainsKey(key))
			{
				m_StoredData[key] = data;
			}
			else
			{
				m_StoredData.Add(key, data);
			}
		}

		public string GetEncounterData(string key)
		{
			m_StoredData.TryGetValue(key, out var value);
			return value;
		}

		public void AddVisible(string name, TrackedVisible vis, ObjectTypes type, PerVisibleParams visibleParams)
		{
			if (!m_SpawnedVisibles.ContainsKey(name))
			{
				m_SpawnedVisibles.Add(name, new EncounterVisibleData(vis, type, visibleParams));
			}
			else
			{
				m_SpawnedVisibles[name] = new EncounterVisibleData(vis, type, visibleParams);
			}
		}

		public void RemoveDeadVisible(string name)
		{
			if (m_SpawnedVisibles.ContainsKey(name))
			{
				m_SpawnedVisibles.Remove(name);
			}
			else
			{
				d.LogError("Encounter.RemoveDeadVisible could not find visible " + name);
			}
		}

		public EncounterVisibleData GetVisible(string name)
		{
			EncounterVisibleData value = null;
			if (m_SpawnedVisibles.TryGetValue(name, out value))
			{
				return value;
			}
			return null;
		}

		public void AddTrackedObjectLookup(string uniqueName, TrackedObjectReference trackedObj)
		{
			if (!m_TrackedObjectLookup.ContainsKey(uniqueName))
			{
				m_TrackedObjectLookup.Add(uniqueName, trackedObj.TrackedId);
			}
			else
			{
				d.LogError("Encounter.SaveData.AddTrackedObjectLookup - Trying to add lookup from unique Name to tracked ID, but unique name '" + uniqueName + "' is already in use!");
			}
		}

		public TrackedObjectReference GetTrackedObject(string uniqueName)
		{
			TrackedObjectReference result = null;
			if (m_TrackedObjectLookup.TryGetValue(uniqueName, out var value))
			{
				result = Singleton.Manager<ManVisible>.inst.GetTrackedObject(value);
			}
			else
			{
				d.LogError("Encounter.SaveData.GetTrackedObject - Trying to get tracked object with unique Name '" + uniqueName + "', but name was not found in the lookup!");
			}
			return result;
		}

		public void AddStoredObject(Transform storedObject, string uniqueName)
		{
			m_StoredObjects.Add(new EncounterObject(storedObject, uniqueName));
		}

		public void RemoveStoredObject(Transform storedObject)
		{
			EncounterObject encounterObject = m_StoredObjects.Find((EncounterObject x) => x.m_Trans = storedObject);
			if ((bool)encounterObject)
			{
				m_StoredObjects.Remove(encounterObject);
			}
		}

		public Transform GetStoredObject(string prefabName, string uniqueName)
		{
			EncounterObject encounterObject = m_StoredObjects.FirstOrDefault((EncounterObject x) => x.m_UniqueName == uniqueName && x.m_PrefabName == prefabName);
			if (encounterObject != null)
			{
				return encounterObject.m_Trans;
			}
			return null;
		}

		public void RecycleStoredObjects(bool instant)
		{
			for (int num = m_StoredObjects.Count - 1; num >= 0; num--)
			{
				if ((bool)m_StoredObjects[num].m_Trans)
				{
					if (instant)
					{
						m_StoredObjects[num].m_Trans.Recycle();
					}
					else
					{
						DespawnAfterTime component = m_StoredObjects[num].m_Trans.GetComponent<DespawnAfterTime>();
						if ((bool)component)
						{
							component.Despawn();
						}
						else
						{
							m_StoredObjects[num].m_Trans.Recycle();
						}
					}
				}
			}
			m_StoredObjects.Clear();
		}
	}

	public enum EncounterVisibleState
	{
		AliveAndSpawned,
		NotCurrentlySpawned,
		Killed,
		NotFoundInEncounter,
		TrackedVisibleNotFound
	}

	private struct BlockerData
	{
		public WorldTile Tile;

		public SceneryBlocker Blocker;
	}

	[SerializeField]
	public EncounterSpawnPosition[] m_Children = new EncounterSpawnPosition[0];

	[SerializeField]
	[FormerlySerializedAs("m_SpawnRadius")]
	[Tooltip("The amount of free space the encounter requires to spawn")]
	private float m_FreeSpaceRadius;

	[Tooltip("The actual size of the encounter")]
	[SerializeField]
	private float m_EncounterRadius;

	[SerializeField]
	[Tooltip("Whether population is disabled when player is within radius")]
	private bool m_DisablesPopulation;

	[SerializeField]
	[Tooltip("Begin scripts before all tiles loaded")]
	private bool m_StartsImmediately;

	[SerializeField]
	[Tooltip("Negative values mean we don't care which way the player is coming from")]
	private float m_PreferredPlayerApproachDirection = -1f;

	[SerializeField]
	[Tooltip("Which players to show messages to in multiplayer")]
	private MultiplayerMessageMode m_MultiplayerMessageMode;

	[SerializeField]
	[Tooltip("Name of child node where player re-spawn preferrentially occurs")]
	private string m_PlayerRespawnChildName;

	[SerializeField]
	private ChallengeData m_ChallengeData;

	[Tooltip("Takes precedent over MaximumObservedBlockLimit if >= 0")]
	[SerializeField]
	[Header("Block/World limiter")]
	private int m_SpecifiedBlockLimit = -1;

	[SerializeField]
	[Tooltip("Takes precedent over m_SpecifiedBlockLimit on switch if >= 0")]
	private int m_SpecifiedBlockLimitSwitch = -1;

	[Tooltip("Automatically updated when you play this mission")]
	[SerializeField]
	private int m_MinimumObservedBlockLimit = -1;

	[SerializeField]
	[Tooltip("Automatically updated when you play this mission")]
	private int m_MaximumObservedBlockLimit;

	[SerializeField]
	[Tooltip("Automatically updated when you play this mission")]
	private bool m_SpawnsTechsFarAway;

	[Header("Set piece mission data")]
	[SerializeField]
	private TerrainSetPiece m_SetPieceRequirement;

	[SerializeField]
	[Tooltip("Allow re-use of existing set-piece terrain")]
	private bool m_CanUseExistingSetPiece = true;

	[SerializeField]
	[Tooltip("When this mission spawns, create the terrain if necessary")]
	private bool m_CanSpawnSetPiece = true;

	[Tooltip("By default tiles under all child locators must be loaded before mission starts, use this to disable that check")]
	[SerializeField]
	private bool m_DisableConservativeTileLoadingChecks;

	[Header("Debug")]
	[SerializeField]
	private bool m_DrawGizmos;

	public static bool s_DebugState;

	[SerializeField]
	[HideInInspector]
	private Dictionary<string, EncounterSpawnPosition> m_SpawnPosLookup = new Dictionary<string, EncounterSpawnPosition>();

	private Transform m_Transform;

	protected bool m_BlockFutureEncountersInThisRadius;

	private TrackedVisible m_TrackedWaypoint;

	private TrackedVisible m_DefaultWaypoint;

	private TrackedVisible m_TrackedVisToFollow;

	private MultiObjectSpawner m_MultiSpawner = new MultiObjectSpawner();

	private SaveData m_SaveData = new SaveData();

	private bool m_FlatRadiusCalculated;

	private float m_FlatTerrainRadius;

	private EncounterDetails m_EncounterDetails;

	private PlacementRefiner m_PlacementRefiner;

	private QuestLogData m_QuestLog;

	private OnGuiCallbackWrapper m_GuiCallbackWrapper = new OnGuiCallbackWrapper();

	private uScript_SaveLoad m_uScriptSaveLoad;

	private List<SceneryBlocker> m_SceneryBlockers = new List<SceneryBlocker>();

	private List<BlockerData> m_SceneryBlockerData = new List<BlockerData>();

	private IntVector2 m_BlockerTileMin = new IntVector2(int.MaxValue, int.MaxValue);

	private IntVector2 m_BlockerTileMax = new IntVector2(int.MinValue, int.MinValue);

	public string EncounterName => m_SaveData.m_EncounterDef.m_Name;

	public EncounterIdentifier EncounterDef => m_SaveData.m_EncounterDef;

	public int SpecifiedBlockLimit
	{
		get
		{
			if (!SKU.SwitchUI || m_SpecifiedBlockLimitSwitch < 0)
			{
				return m_SpecifiedBlockLimit;
			}
			return m_SpecifiedBlockLimitSwitch;
		}
	}

	public bool HasSpecificBlockLimit
	{
		get
		{
			if (SpecifiedBlockLimit < 0)
			{
				return m_MinimumObservedBlockLimit >= 0;
			}
			return true;
		}
	}

	public int BlockLimit
	{
		get
		{
			if (SpecifiedBlockLimit < 0)
			{
				return m_MaximumObservedBlockLimit;
			}
			return SpecifiedBlockLimit;
		}
	}

	public bool SpawnsTechsFarAway => m_SpawnsTechsFarAway;

	public bool DisableConservativeTileLoadingChecks => m_DisableConservativeTileLoadingChecks;

	public bool CanSpawnSetPiece => m_CanSpawnSetPiece;

	public bool CanUseExistingSetPiece => m_CanUseExistingSetPiece;

	public bool HasPreferredPlayerApproachDirection => m_PreferredPlayerApproachDirection >= 0f;

	public float PreferredPlayerApproachDirection => m_PreferredPlayerApproachDirection;

	public bool StartsImmediately => m_StartsImmediately;

	public PlacementRefiner PlacementRefiner
	{
		get
		{
			if (m_PlacementRefiner == null)
			{
				m_PlacementRefiner = GetComponent<PlacementRefiner>();
			}
			return m_PlacementRefiner;
		}
	}

	public ChallengeData ChallengeData => m_ChallengeData;

	public EncounterDetails EncounterDetails
	{
		get
		{
			if (m_EncounterDetails == null)
			{
				m_EncounterDetails = GetComponent<EncounterDetails>();
			}
			return m_EncounterDetails;
		}
	}

	public QuestLogData QuestLog => m_QuestLog;

	public bool VisibleInLog => m_SaveData.m_VisibleInLog;

	public int EncounterStringBankIdx => m_SaveData.m_EncounterStringBankIdx;

	public bool HasNoPosition { get; private set; }

	public bool CanAcceptFromQuestGiver { get; private set; }

	public bool CanBeCancelled { get; private set; }

	public bool IsScriptUpdateEnabled
	{
		get
		{
			return m_SaveData.m_IsScriptUpdateEnabled;
		}
		set
		{
			m_SaveData.m_IsScriptUpdateEnabled = value;
			OnScriptUpdateEnabled(value);
		}
	}

	public bool AllowsPointOfInterest { get; private set; }

	public Vector3 Position => m_Transform.position;

	public bool BaseTechIsRadarPosition { get; private set; }

	public bool BlockFutureEncountersInThisRadius
	{
		get
		{
			if ((bool)m_SetPieceRequirement)
			{
				return m_BlockFutureEncountersInThisRadius;
			}
			return false;
		}
		private set
		{
			m_BlockFutureEncountersInThisRadius = value;
		}
	}

	public int EncounterWaypointID => m_DefaultWaypoint.ID;

	public float EncounterRadius => m_EncounterRadius;

	public float SpawnRadius => m_FreeSpaceRadius;

	public float FlatTerrainRadius
	{
		get
		{
			if (!m_FlatRadiusCalculated)
			{
				m_FlatTerrainRadius = DetermineFlatTerrainRadius();
				m_FlatRadiusCalculated = true;
			}
			return m_FlatTerrainRadius;
		}
	}

	public MultiplayerMessageMode MessageMode => m_MultiplayerMessageMode;

	public string CrateName => base.name + "_RewardCrate";

	public TrackedVisible Crate { get; private set; }

	public TrackedVisible DefaultWaypoint => m_DefaultWaypoint;

	public TerrainSetPiece GetRequiredSetPiece()
	{
		return m_SetPieceRequirement;
	}

	public void Begin(EncounterIdentifier encounterDef, TrackedVisible trackedWaypoint, EncounterData encounterInfo, int displayStringBankIdx, bool restoredFromSaveData, NetPlayer fromPlayer, out bool setTrackedEncounter)
	{
		m_SaveData.m_EncounterDef = encounterDef;
		HasNoPosition = encounterInfo.m_HasNoPosition;
		CanAcceptFromQuestGiver = encounterInfo.m_CanAcceptFromQuestGiver;
		CanBeCancelled = encounterInfo.m_CanBeCancelled;
		AllowsPointOfInterest = encounterInfo.m_AllowsPointOfInterest;
		BaseTechIsRadarPosition = encounterInfo.m_BaseTechIsRadarPosition;
		BlockFutureEncountersInThisRadius = encounterInfo.m_BlockFutureEncountersInThisRadius;
		m_SaveData.m_ShowAreaOnMiniMap = encounterInfo.m_ShowAreaOnMiniMap;
		m_SaveData.m_EncounterStringBankIdx = displayStringBankIdx;
		m_QuestLog = new QuestLogData(this);
		m_QuestLog.RestoreEncounterTimer(m_SaveData.m_EncounterTimer);
		if (!restoredFromSaveData)
		{
			d.Assert(m_Children.Count((EncounterSpawnPosition c) => c.m_UseForInitialWaypointPos) <= 1, $"More than one child of Encounter {this} is set as initial waypoint position.", this);
			EncounterSpawnPosition encounterSpawnPosition = m_Children.FirstOrDefault((EncounterSpawnPosition c) => c.m_UseForInitialWaypointPos);
			if (encounterSpawnPosition != null)
			{
				m_SaveData.m_DefaultWaypointOffset = encounterSpawnPosition.m_Position.position;
			}
		}
		SetupDefaultWaypoint();
		if (trackedWaypoint != null)
		{
			SetTrackedWaypoint(trackedWaypoint);
		}
		bool num = encounterInfo.m_AddLog || (!restoredFromSaveData && !encounterInfo.m_SpawnWithoutUserAccept);
		setTrackedEncounter = false;
		if (num || m_SaveData.m_VisibleInLog)
		{
			m_SaveData.m_VisibleInLog = true;
			Singleton.Manager<ManQuestLog>.inst.AddLog(this, fromPlayer, restoredFromSaveData);
			if (m_SaveData.m_CurrentlyActive)
			{
				Singleton.Manager<ManQuestLog>.inst.SetTrackedEncounter(this);
				setTrackedEncounter = true;
			}
			else if (!restoredFromSaveData && encounterInfo.m_SetActiveInLog)
			{
				Singleton.Manager<ManQuestLog>.inst.SetTrackedEncounter(this);
				setTrackedEncounter = true;
			}
		}
		UpdateActiveWaypoint();
		if (m_ChallengeData != null)
		{
			m_ChallengeData.SpawnChallengeSceneryBlockers(base.transform.position, base.transform.rotation);
			if (!restoredFromSaveData)
			{
				AddSafeAreasForChallenge();
			}
		}
	}

	public void ShowInQuestLog()
	{
		m_SaveData.m_VisibleInLog = true;
		UpdateActiveWaypoint();
	}

	public bool GetPlayerRespawnOverrideLocation(ref Vector3 scenePos, ref Vector3 facingDir)
	{
		if (!IsScriptUpdateEnabled)
		{
			return false;
		}
		if (m_PlayerRespawnChildName == null || m_PlayerRespawnChildName.Length == 0)
		{
			return false;
		}
		EncounterSpawnPosition[] children = m_Children;
		foreach (EncounterSpawnPosition encounterSpawnPosition in children)
		{
			if (encounterSpawnPosition.m_PTName == m_PlayerRespawnChildName)
			{
				Vector3 scenePos2 = base.transform.TransformPoint(encounterSpawnPosition.m_Position.position);
				if (Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(in scenePos2))
				{
					scenePos = scenePos2;
					facingDir = base.transform.TransformVector(encounterSpawnPosition.m_Position.forward);
					return true;
				}
				d.Log($"Ignoring respawn override {m_PlayerRespawnChildName} for encounter {base.name} as it's on a non-loaded tile {Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in scenePos2)}");
			}
		}
		return false;
	}

	public void Load(SaveData saveData)
	{
		m_SaveData = saveData;
		m_SaveData.Load();
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			m_SaveData.LookupVisibles();
		}
		bool autoRetry = true;
		m_MultiSpawner.Load(m_SaveData.m_SpawnerData, m_SaveData.m_SpawnHistory, autoRetry);
		if (m_SaveData.m_CrateId != 0)
		{
			Crate = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_SaveData.m_CrateId);
		}
		RemoveSpawnedSafeAreas();
		if (m_SaveData.m_CallengeSafeAreas == null || m_SaveData.m_CallengeSafeAreas.Count <= 0)
		{
			return;
		}
		foreach (ChallengeData.SafeAreaData callengeSafeArea in m_SaveData.m_CallengeSafeAreas)
		{
			SpawnSafeArea(callengeSafeArea.worldPosition, callengeSafeArea.radius);
		}
	}

	public SaveData Save()
	{
		m_MultiSpawner.Save(ref m_SaveData.m_SpawnerData, ref m_SaveData.m_SpawnHistory);
		m_SaveData.Save(this);
		m_SaveData.m_Position = Vector3.zero;
		m_SaveData.m_PositionWorld = WorldPosition.FromScenePosition(m_Transform.position);
		m_SaveData.m_Rotation = m_Transform.rotation;
		return m_SaveData;
	}

	public void FixupLateSpawnedVisibles()
	{
		if (m_SaveData.m_CrateId != 0 && Crate == null)
		{
			Crate = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_SaveData.m_CrateId);
		}
		m_SaveData.LookupVisibles();
	}

	public void SetActiveEncounter(bool active)
	{
		if (m_SaveData.m_CurrentlyActive != active)
		{
			m_SaveData.m_CurrentlyActive = active;
			UpdateActiveWaypoint();
		}
	}

	public void SetTrackedWaypoint(TrackedVisible newWaypoint)
	{
		if (newWaypoint != m_TrackedWaypoint)
		{
			if (m_TrackedWaypoint != null)
			{
				m_SaveData.m_TrackedVisID = -1;
				m_TrackedWaypoint.IsQuestObject = false;
			}
			m_TrackedWaypoint = newWaypoint;
			if (newWaypoint != null)
			{
				m_SaveData.m_WaypointVisibleId = newWaypoint.ID;
			}
			UpdateActiveWaypoint();
		}
	}

	public Vector3 GetPosition(string positionName)
	{
		Vector3 position = m_Transform.position;
		EncounterSpawnPosition spawnPosition = GetSpawnPosition(positionName);
		if (spawnPosition != null)
		{
			position += m_Transform.rotation * spawnPosition.m_Position.position;
		}
		return position;
	}

	public Quaternion GetRotation(string positionName)
	{
		Quaternion result = Quaternion.identity;
		EncounterSpawnPosition spawnPosition = GetSpawnPosition(positionName);
		if (spawnPosition != null)
		{
			result = spawnPosition.m_Position.orientation * m_Transform.rotation;
		}
		return result;
	}

	public float GetRadius(string positionName)
	{
		float result = 0f;
		EncounterSpawnPosition spawnPosition = GetSpawnPosition(positionName);
		if (spawnPosition != null)
		{
			result = spawnPosition.m_Size;
		}
		return result;
	}

	public bool GetTriggerTester(string positionName, out TriggerTester tester)
	{
		EncounterSpawnPosition spawnPosition = GetSpawnPosition(positionName);
		if (spawnPosition != null)
		{
			Singleton.Manager<ManWorld>.inst.GetTerrainHeight(base.transform.localToWorldMatrix.MultiplyPoint(spawnPosition.m_Position.position), out var outHeight);
			tester = new TriggerTester(spawnPosition, outHeight, base.transform);
			return true;
		}
		tester = default(TriggerTester);
		return false;
	}

	public void FollowTrackedVisible(TrackedVisible trackedVisToFollow)
	{
		if (trackedVisToFollow != null)
		{
			m_TrackedVisToFollow = trackedVisToFollow;
			m_SaveData.m_TrackedVisID = trackedVisToFollow.ID;
		}
	}

	public void StopFollowingTrackedVisible()
	{
		m_TrackedVisToFollow = null;
		m_SaveData.m_TrackedVisID = -1;
	}

	public void ClearTrackedWaypoint()
	{
		SetTrackedWaypoint(null);
	}

	public void AddEncounterData(string key, string data)
	{
		m_SaveData.AddEncounterData(key, data);
	}

	public string GetEncounterData(string key)
	{
		return m_SaveData.GetEncounterData(key);
	}

	public void AddVisible(string name, TrackedVisible trackedVis, ObjectTypes type)
	{
		AddVisible(name, trackedVis, type, null);
	}

	public void AddVisible(string name, TrackedVisible trackedVis, ObjectTypes type, PerVisibleParams encounterParams)
	{
		SetupEncounterVisible(trackedVis);
		m_SaveData.AddVisible(name, trackedVis, type, encounterParams);
	}

	public void RemoveDeadVisible(string name)
	{
		m_SaveData.RemoveDeadVisible(name);
	}

	public void UpdateBlockLimiterExtents(bool missionComplete)
	{
	}

	public bool TrySpawnUniqueParticlesOnce(Transform particlePrefab, string uniqueName, Vector3 position, Quaternion rotation, out Transform particles)
	{
		particles = GetStoredObject(particlePrefab.name, uniqueName);
		if (particles != null)
		{
			return false;
		}
		SpawnParticles(particlePrefab, position, rotation, out particles);
		AddStoredObject(particles, uniqueName);
		return true;
	}

	public void SpawnParticles(Transform particlePrefab, Vector3 position, Quaternion rotation, out Transform particles)
	{
		particles = particlePrefab.Spawn(Singleton.dynamicContainer, position, rotation);
		particles.name = particlePrefab.name;
		particles.gameObject.SendMessage("SetEncounter", this, SendMessageOptions.DontRequireReceiver);
	}

	public void SpawnObject(ManSpawn.ObjectSpawnParams objectSpawnParams, ManFreeSpace.FreeSpaceParams freeSpaceParams, string name)
	{
		SpawnObject(objectSpawnParams, freeSpaceParams, null, name);
	}

	public void SpawnObject(ManSpawn.ObjectSpawnParams objectSpawnParams, ManFreeSpace.FreeSpaceParams freeSpaceParams, PerVisibleParams encounterParams, string name)
	{
		m_MultiSpawner.TrySpawn(objectSpawnParams, freeSpaceParams, encounterParams, name, autoRetry: true);
	}

	public void SpawnBlock(ManSpawn.BlockSpawnParams blockSpawnParams, Vector3 spawnPosScene, string name)
	{
		TrackedVisible trackedVis = blockSpawnParams.SpawnCurrentFreeSpaceObject(spawnPosScene);
		AddVisible(name, trackedVis, ObjectTypes.Block);
	}

	public void TrySpawnBlockRewardCrate(Vector3 spawnPosScene, Quaternion spawnRot, bool locked, bool visibleOnRadar)
	{
		TrySpawnBlockRewardCrateWithSpawner(spawnPosScene, spawnRot, locked, visibleOnRadar, m_MultiSpawner);
	}

	public void TrySpawnBlockRewardCrateWithSpawner(Vector3 spawnPosScene, Quaternion spawnRot, bool locked, bool visibleOnRadar, MultiObjectSpawner spawner)
	{
		if (!EncounterDetails.AwardBlocks || m_SaveData.m_CrateDelivered)
		{
			return;
		}
		string crateName = CrateName;
		int num = ((EncounterDetails.BlocksToAward != null) ? EncounterDetails.BlocksToAward.Length : 0) + (EncounterDetails.RewardBlocksFromCorpPool ? EncounterDetails.AmountToAwardFromPool : 0);
		if (num <= 0)
		{
			return;
		}
		Crate.Definition crateDef = new Crate.Definition
		{
			m_Contents = new Crate.ItemDefinition[num],
			m_Locked = locked
		};
		int num2 = 0;
		if (EncounterDetails.BlocksToAward != null && EncounterDetails.BlocksToAward.Length != 0)
		{
			for (int i = 0; i < EncounterDetails.BlocksToAward.Length; i++)
			{
				crateDef.m_Contents[num2].m_BlockType = EncounterDetails.BlocksToAward[i];
				num2++;
			}
		}
		if (EncounterDetails.RewardBlocksFromCorpPool && EncounterDetails.AmountToAwardFromPool > 0)
		{
			BlockTypes[] rewardBlocks = Singleton.Manager<ManLicenses>.inst.GetRewardPoolTable().GetRewardBlocks(EncounterDetails.RewardPoolCorp, EncounterDetails.AmountToAwardFromPool);
			for (int j = 0; j < EncounterDetails.AmountToAwardFromPool; j++)
			{
				crateDef.m_Contents[num2].m_BlockType = rewardBlocks[j];
				num2++;
			}
		}
		ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
		{
			m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
			m_AvoidLandmarks = true,
			m_CircleRadius = Singleton.Manager<ManSpawn>.inst.GetCrateSpawnClearance(EncounterDetails.CorpLicenseToAward),
			m_CenterPosWorld = WorldPosition.FromScenePosition(in spawnPosScene),
			m_CircleIndex = 0,
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
			m_CheckSafeArea = false,
			m_RejectFunc = null
		};
		ManSpawn.CrateSpawnParams objectSpawnParams = new ManSpawn.CrateSpawnParams
		{
			m_CrateDef = crateDef,
			m_Rotation = spawnRot,
			m_CorpType = EncounterDef.m_Corp,
			m_Name = crateName,
			m_VisibleOnRadar = visibleOnRadar
		};
		bool autoRetry = true;
		spawner.TrySpawn(objectSpawnParams, freeSpaceParams, crateName, autoRetry);
		m_SaveData.m_CrateDelivered = true;
	}

	public EncounterVisibleState GetVisibleState(string uniqueName)
	{
		Visible visible;
		return GetVisibleState(uniqueName, out visible);
	}

	public EncounterVisibleState GetVisibleState(string uniqueName, out Visible visible)
	{
		visible = null;
		EncounterVisibleData visible2 = GetVisible(uniqueName);
		if (visible2 != null)
		{
			if (visible2.m_VisibleId == -2)
			{
				return EncounterVisibleState.Killed;
			}
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible2.m_VisibleId);
			if (trackedVisible == null)
			{
				d.LogError("ERROR: Encounter.GetVisibleState - Could not find tracked visible for id " + uniqueName);
				return EncounterVisibleState.TrackedVisibleNotFound;
			}
			if (trackedVisible.visible == null)
			{
				return EncounterVisibleState.NotCurrentlySpawned;
			}
			visible = trackedVisible.visible;
			return EncounterVisibleState.AliveAndSpawned;
		}
		return EncounterVisibleState.NotFoundInEncounter;
	}

	public EncounterVisibleData GetVisible(string name)
	{
		return m_SaveData.GetVisible(name);
	}

	public IEnumerable<string> GetVisibleNamesWithPrefix(string prefix)
	{
		foreach (KeyValuePair<string, EncounterVisibleData> spawnedVisible in m_SaveData.m_SpawnedVisibles)
		{
			if (spawnedVisible.Key.StartsWith(prefix))
			{
				yield return spawnedVisible.Key;
			}
		}
	}

	public void AddTrackableObjectLookup(string uniqueName, TrackedObjectReference trackedObj)
	{
		m_SaveData.AddTrackedObjectLookup(uniqueName, trackedObj);
	}

	public TrackedObjectReference GetTrackedObject(string uniqueName)
	{
		return m_SaveData.GetTrackedObject(uniqueName);
	}

	public void RemoveStoredObject(Transform storedObject)
	{
		m_SaveData.RemoveStoredObject(storedObject);
	}

	public void AddStoredObject(Transform storedObject, string uniqueName)
	{
		m_SaveData.AddStoredObject(storedObject, uniqueName);
	}

	public Transform GetStoredObject(string prefabName, string uniqueName)
	{
		return m_SaveData.GetStoredObject(prefabName, uniqueName);
	}

	public void Kill(bool instant)
	{
		m_SaveData.RecycleStoredObjects(instant);
		ClearUpEncounterVisibles();
		ClearUpTrackableObjectLookup();
		RemoveSpawnedSafeAreas();
		DespawnSceneryBlockers();
		m_SaveData.m_CallengeSafeAreas.Clear();
		if (m_ChallengeData != null)
		{
			m_ChallengeData.RecycleChallengeSceneryBlockers();
		}
		this.Recycle();
	}

	public void SetPosition(Vector3 position)
	{
		if (m_Transform.SetPositionIfChanged(position))
		{
			NetUpdateWaypointPosition(position);
		}
	}

	public void SetRotation(Quaternion rotation)
	{
		m_Transform.SetRotationIfChanged(rotation);
	}

	public void StartEncounterTimer(float timeRemaining)
	{
		m_QuestLog.StartEncounterTimer(timeRemaining);
	}

	public void RemoveEncounterTimer()
	{
		m_QuestLog.RemoveEncounterTimer();
	}

	public void AddTimeToEncounterTimer(float timeToAdd)
	{
		d.Assert(m_QuestLog.EncounterTimer != null && !m_QuestLog.EncounterTimer.IsExpired, "Encounter.AddTimeToEncounterTimer - Encounter does not currently have a timer running, or it has already expired!");
		if (m_QuestLog.EncounterTimer != null && !m_QuestLog.EncounterTimer.IsExpired)
		{
			m_QuestLog.EncounterTimer.AddTimeToTimer(timeToAdd);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(EncounterDef);
			}
		}
	}

	public void RemoveAllManagedObjects()
	{
		m_SaveData.RecycleStoredObjects(instant: true);
		foreach (EncounterVisibleData value in m_SaveData.m_SpawnedVisibles.Values)
		{
			if (value.m_VisibleId != -2 && (value.Params == null || !value.Params.SurvivesEncounter))
			{
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(value.m_VisibleId);
				if (trackedVisible != null)
				{
					Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(trackedVisible);
					continue;
				}
				d.LogError(string.Concat("Encounter.RecycleAllManagedObjects - Encounter ", base.name, " cannont find tracked visible for object ", value.ObjectType, ": ", value.m_VisibleId));
			}
		}
		m_SaveData.m_SpawnedVisibles.Clear();
		RemoveAllTrackedObjects();
		RemoveSpawnedSafeAreas();
		m_SaveData.m_CallengeSafeAreas.Clear();
	}

	public void RemoveAllTrackedObjects(bool removeResourceSeams = true)
	{
		foreach (uint value in m_SaveData.m_TrackedObjectLookup.Values)
		{
			TrackedObjectReference trackedObject = Singleton.Manager<ManVisible>.inst.GetTrackedObject(value);
			if (trackedObject != null)
			{
				if (removeResourceSeams || !(trackedObject.TrackedObject != null) || !trackedObject.TrackedObject.GetComponent<ResourceDispenser>().IsNotNull())
				{
					if (trackedObject.TrackedObject.IsNull())
					{
						Singleton.Manager<ManVisible>.inst.ObliterateTrackedObjectFromWorld(trackedObject);
					}
					else if (!PostChallengeDestructionDelay.StaticTryInitDelayedDestruction(trackedObject.TrackedId, errorIfNotFound: false))
					{
						Singleton.Manager<ManVisible>.inst.ObliterateTrackedObjectFromWorld(trackedObject);
					}
				}
			}
			else
			{
				d.LogError("Encounter.RecycleAllManagedObjects - Encounter " + base.name + " cannont find tracked object for object with id " + value);
			}
		}
		m_SaveData.m_TrackedObjectLookup.Clear();
	}

	public IEnumerable<EncounterVisibleData> GetSpawnedVisibles()
	{
		return m_SaveData.m_SpawnedVisibles.Values;
	}

	public bool CheckAtLeastOneTileIsLoadedAtAllChildPositions()
	{
		EncounterSpawnPosition[] children = m_Children;
		foreach (EncounterSpawnPosition encounterSpawnPosition in children)
		{
			if (!encounterSpawnPosition.m_CanStartWithoutTile)
			{
				Vector3 scenePos = base.transform.TransformPoint(encounterSpawnPosition.m_Position.position);
				float radius = ((encounterSpawnPosition.m_Size <= 20f) ? 0f : encounterSpawnPosition.m_Size);
				if (!Singleton.Manager<ManWorld>.inst.CheckAtLeastOneTileAtPositionHasReachedLoadStep(scenePos, radius))
				{
					return false;
				}
			}
		}
		return true;
	}

	private EncounterSpawnPosition GetSpawnPosition(string positionName)
	{
		if (!m_SpawnPosLookup.TryGetValue(positionName, out var value))
		{
			d.LogError("Cannot Find SpawnPosition " + positionName + " in encounter object " + base.name);
		}
		return value;
	}

	private bool CheckTrigger(EncounterSpawnPosition spawnPos, Vector3 worldPos, float offsetHeight)
	{
		Vector3 coord = (Matrix4x4.Rotate(Quaternion.Inverse(spawnPos.m_Position.orientation)) * Matrix4x4.Translate(-spawnPos.m_Position.position - Vector3.up * offsetHeight) * base.transform.worldToLocalMatrix).MultiplyPoint(worldPos);
		switch (spawnPos.m_TriggerShape)
		{
		case TriggerShape.None:
		case TriggerShape.Sphere:
			return coord.sqrMagnitude <= spawnPos.m_Size * spawnPos.m_Size;
		case TriggerShape.Box:
			if (Mathf.Abs(coord.x) <= spawnPos.m_Size && (spawnPos.m_SizeY <= 0f || Mathf.Abs(coord.y) <= spawnPos.m_SizeY))
			{
				return Mathf.Abs(coord.z) <= spawnPos.m_SizeZ;
			}
			return false;
		case TriggerShape.Cylinder:
			if (spawnPos.m_SizeY <= 0f || Mathf.Abs(coord.y) <= spawnPos.m_SizeY)
			{
				return coord.ToVector2XZ().sqrMagnitude <= spawnPos.m_Size * spawnPos.m_Size;
			}
			return false;
		default:
			return false;
		}
	}

	private void SetupDefaultWaypoint()
	{
		Vector3 position = m_Transform.localToWorldMatrix.MultiplyPoint(m_SaveData.m_DefaultWaypointOffset);
		position.y += 1f;
		Visible.DisableAddToTileOnSpawn = true;
		TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Waypoint, 0), position, Quaternion.identity, addToObjectManager: false, forceSpawn: true);
		Visible.DisableAddToTileOnSpawn = false;
		if (trackedVisible.visible != null)
		{
			trackedVisible.visible.StopManagingVisible();
			Singleton.Manager<ManVisible>.inst.TrackWithoutSaving(trackedVisible);
			m_DefaultWaypoint = trackedVisible;
			m_DefaultWaypoint.visible.trans.parent = m_Transform;
			m_DefaultWaypoint.visible.WorldSpaceComponent.SetEnabled(enabled: false);
		}
		else
		{
			d.LogError("EncounterInstance.SetDefaultWaypoint - Waypoint is null for encounter " + base.name);
		}
	}

	public void MoveDefaultWaypoint(Vector3 scenePos)
	{
		Vector3 vector = m_Transform.worldToLocalMatrix.MultiplyPoint(scenePos).SetY(0f);
		if (vector != m_SaveData.m_DefaultWaypointOffset)
		{
			m_SaveData.m_DefaultWaypointOffset = vector;
			if (m_DefaultWaypoint != null)
			{
				d.Log($"MoveDefaultWaypoint for {base.name} to scenePos={scenePos} hostId={m_DefaultWaypoint.HostID}");
				scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos, hitScenery: true) + Vector3.up;
				NetUpdateWaypointPosition(scenePos);
			}
		}
	}

	private void NetUpdateWaypointPosition(Vector3 scenePos)
	{
		if (m_DefaultWaypoint == null)
		{
			return;
		}
		if (m_DefaultWaypoint.visible.IsNotNull())
		{
			Visible visible = m_DefaultWaypoint.visible;
			visible.trans.position = scenePos;
			if ((bool)visible.Waypoint && (bool)visible.Waypoint.netWaypoint)
			{
				visible.Waypoint.netWaypoint.SetMoved();
			}
		}
		m_DefaultWaypoint.SetPos(scenePos);
	}

	public void UpdateActiveWaypoint()
	{
		bool flag = !HasNoPosition && Singleton.Manager<ManEncounter>.inst.VisibleInHud;
		if (m_TrackedWaypoint != null)
		{
			if (BaseTechIsRadarPosition && m_TrackedWaypoint.RadarType == RadarTypes.Base)
			{
				flag = true;
			}
			m_TrackedWaypoint.IsQuestObject = flag;
			if (m_DefaultWaypoint != null)
			{
				m_DefaultWaypoint.IsQuestObject = false;
				m_DefaultWaypoint.RadarType = RadarTypes.Hidden;
			}
		}
		else if (m_DefaultWaypoint != null)
		{
			m_DefaultWaypoint.IsQuestObject = flag;
			if (flag)
			{
				if (m_SaveData.m_ShowAreaOnMiniMap && m_SaveData.m_DefaultWaypointOffset == Vector3.zero)
				{
					m_DefaultWaypoint.RadarType = RadarTypes.AreaQuest;
				}
				else
				{
					m_DefaultWaypoint.RadarType = (m_SaveData.m_VisibleInLog ? RadarTypes.DiscoveredQuest : RadarTypes.UndiscoveredQuest);
				}
			}
			else
			{
				m_DefaultWaypoint.RadarType = RadarTypes.Hidden;
			}
		}
		TrackedVisible encounterWaypoint = (flag ? (m_TrackedWaypoint ?? m_DefaultWaypoint) : null);
		if (m_SaveData.m_CurrentlyActive)
		{
			Singleton.Manager<ManEncounter>.inst.ConfigureNavigationOverlay(encounterWaypoint);
		}
		if (m_QuestLog != null)
		{
			m_QuestLog.SetEncounterWaypoint(encounterWaypoint);
		}
	}

	private void SetupEncounterVisible(TrackedVisible trackedVis)
	{
		ObjectTypes objectType = trackedVis.ObjectType;
		if (objectType == ObjectTypes.Block)
		{
			EnableAutoExpireVisible(trackedVis, enable: false);
		}
	}

	private void EnableAutoExpireVisible(TrackedVisible trackedVis, bool enable)
	{
		if (trackedVis.visible != null)
		{
			trackedVis.visible.EnableAutoExpire(enable);
		}
		else
		{
			Singleton.Manager<ManEncounter>.inst.EnableAutoExpireVisibleOnRespawn(trackedVis, enable);
		}
	}

	private void ClearUpEncounterVisibles()
	{
		foreach (EncounterVisibleData value in m_SaveData.m_SpawnedVisibles.Values)
		{
			if (value.ObjectType == ObjectTypes.Block && value.m_VisibleId != -2)
			{
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(value.m_VisibleId);
				if (trackedVisible != null)
				{
					EnableAutoExpireVisible(trackedVisible, enable: true);
					Singleton.Manager<ManVisible>.inst.StopTrackingVisible(value.m_VisibleId);
					continue;
				}
				d.LogError(string.Concat("Encounter ", base.name, " cannont find tracked visible for object ", value.ObjectType, ": ", value.m_VisibleId));
			}
		}
	}

	private void ClearUpTrackableObjectLookup()
	{
		foreach (uint value in m_SaveData.m_TrackedObjectLookup.Values)
		{
			if (Singleton.Manager<ManVisible>.inst.GetTrackedObject(value) != null)
			{
				Singleton.Manager<ManVisible>.inst.StopTrackingObject(value);
			}
		}
	}

	private void AddSafeAreasForChallenge()
	{
		List<ChallengeData.SafeAreaData> list = new List<ChallengeData.SafeAreaData>();
		m_ChallengeData.GetSafeAreaList(m_Transform.position, m_Transform.rotation, list);
		foreach (ChallengeData.SafeAreaData item in list)
		{
			AddSafeArea(item);
		}
	}

	public void AddSafeArea(ChallengeData.SafeAreaData safeAreaData)
	{
		m_SaveData.m_CallengeSafeAreas.Add(safeAreaData);
		SpawnSafeArea(safeAreaData.worldPosition, safeAreaData.radius);
	}

	private void SpawnSafeArea(WorldPosition position, float radius)
	{
		ManFreeSpace.SafeArea area = new ManFreeSpace.EncounterSafeArea(position, radius, this);
		Singleton.Manager<ManFreeSpace>.inst.AddSafeArea(area);
	}

	private void RemoveSpawnedSafeAreas()
	{
		Singleton.Manager<ManFreeSpace>.inst.RemoveSafeAreas(IsSafeAreaAssociatedWithEncounter);
	}

	private bool IsSafeAreaAssociatedWithEncounter(ManFreeSpace.SafeArea safeArea)
	{
		if (safeArea is ManFreeSpace.EncounterSafeArea encounterSafeArea)
		{
			return encounterSafeArea.m_LinkedEncounter == this;
		}
		return false;
	}

	public void SpawnSceneryBlockersForTile(WorldTile tile)
	{
		if (m_SceneryBlockers.Count <= 0 || tile.Coord.x < m_BlockerTileMin.x || tile.Coord.y < m_BlockerTileMin.y || tile.Coord.x > m_BlockerTileMax.x || tile.Coord.y > m_BlockerTileMax.y)
		{
			return;
		}
		foreach (SceneryBlocker sceneryBlocker in m_SceneryBlockers)
		{
			if (!tile.HasSceneryBlocker(sceneryBlocker) && sceneryBlocker.OverlapsBoundsApprox(Singleton.Manager<ManWorld>.inst.TileManager.CalcMinWorldCoords(tile.Coord), Singleton.Manager<ManWorld>.inst.TileManager.CalcMaxWorldCoords(tile.Coord)))
			{
				BlockerData item = new BlockerData
				{
					Blocker = sceneryBlocker,
					Tile = tile
				};
				Singleton.Manager<ManWorld>.inst.AddSceneryBlockerToTile(item.Blocker, item.Tile);
				m_SceneryBlockerData.Add(item);
			}
		}
	}

	public void RemoveSceneryBlockersForTile(WorldTile tile)
	{
		if (m_SceneryBlockerData.Count <= 0 || tile.Coord.x < m_BlockerTileMin.x || tile.Coord.y < m_BlockerTileMin.y || tile.Coord.x > m_BlockerTileMax.x || tile.Coord.y > m_BlockerTileMax.y)
		{
			return;
		}
		for (int num = m_SceneryBlockerData.Count - 1; num >= 0; num--)
		{
			BlockerData blockerData = m_SceneryBlockerData[num];
			if (blockerData.Tile == tile)
			{
				tile.RemoveSceneryBlocker(blockerData.Blocker);
				m_SceneryBlockerData.RemoveAt(num);
			}
		}
	}

	private void SpawnSceneryBlockers()
	{
		d.Assert(m_SceneryBlockers.Count == 0, "SpawnSceneryBlockers called too many times");
		m_BlockerTileMin = new IntVector2(int.MaxValue, int.MaxValue);
		m_BlockerTileMax = new IntVector2(int.MinValue, int.MinValue);
		EncounterSpawnPosition[] children = m_Children;
		foreach (EncounterSpawnPosition encounterSpawnPosition in children)
		{
			if (encounterSpawnPosition.m_IsSpawnBlocker)
			{
				Vector3 scenePos = base.transform.TransformPoint(encounterSpawnPosition.m_Position.position);
				SceneryBlocker item;
				if (encounterSpawnPosition.m_TriggerShape == TriggerShape.Cylinder)
				{
					item = SceneryBlocker.Create2DCircularBlocker(SceneryBlocker.BlockMode.Spawn, WorldPosition.FromScenePosition(in scenePos), encounterSpawnPosition.m_Size);
				}
				else if (encounterSpawnPosition.m_TriggerShape == TriggerShape.Box)
				{
					Vector3 size = new Vector3(encounterSpawnPosition.m_Size, encounterSpawnPosition.m_SizeY, encounterSpawnPosition.m_SizeZ) * 2f;
					item = SceneryBlocker.CreateRectangularPrismBlocker(SceneryBlocker.BlockMode.Spawn, WorldPosition.FromScenePosition(in scenePos), encounterSpawnPosition.m_Position.orientation, size);
				}
				else
				{
					item = SceneryBlocker.CreateSphereBlocker(SceneryBlocker.BlockMode.Spawn, WorldPosition.FromScenePosition(in scenePos), encounterSpawnPosition.m_Size);
				}
				m_SceneryBlockers.Add(item);
				Vector3 vector = new Vector3(encounterSpawnPosition.m_Size, 0f, encounterSpawnPosition.m_Size);
				IntVector2 intVector = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(scenePos - vector);
				IntVector2 intVector2 = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(scenePos + vector);
				m_BlockerTileMin.x = Mathf.Min(m_BlockerTileMin.x, intVector.x);
				m_BlockerTileMin.y = Mathf.Min(m_BlockerTileMin.y, intVector.y);
				m_BlockerTileMax.x = Mathf.Max(m_BlockerTileMax.x, intVector2.x);
				m_BlockerTileMax.y = Mathf.Max(m_BlockerTileMax.y, intVector2.y);
			}
		}
		IntVector2 coord = m_BlockerTileMin;
		while (coord.x <= m_BlockerTileMax.x)
		{
			coord.y = m_BlockerTileMin.y;
			while (coord.y <= m_BlockerTileMax.y)
			{
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord, allowEmpty: true);
				if (worldTile != null)
				{
					SpawnSceneryBlockersForTile(worldTile);
				}
				coord.y++;
			}
			coord.x++;
		}
	}

	private void DespawnSceneryBlockers()
	{
		foreach (BlockerData sceneryBlockerDatum in m_SceneryBlockerData)
		{
			sceneryBlockerDatum.Tile.RemoveSceneryBlocker(sceneryBlockerDatum.Blocker);
		}
		m_SceneryBlockerData.Clear();
		m_SceneryBlockers.Clear();
	}

	private void OnScriptUpdateEnabled(bool value)
	{
		if (value)
		{
			SpawnSceneryBlockers();
		}
		else
		{
			DespawnSceneryBlockers();
		}
	}

	private void OnObjectSpawned(TrackedVisible tv, string name, PerVisibleParams encounterParams)
	{
		d.Assert(tv != null, "OnObjectSpawned NULL tracked visible");
		if (tv != null)
		{
			if (tv.ObjectType == ObjectTypes.Crate)
			{
				d.Assert(m_SaveData.m_CrateId == 0, "Crate spawned in encounter when we already have a tracked ID");
				Crate = tv;
				m_SaveData.m_CrateId = tv.ID;
			}
			d.Log(string.Format("Encounter {0} spawned ID={1} name={2} visibleName={3}", base.name, tv.ID, name, tv.visible ? tv.visible.name : "NoVisible"));
			AddVisible(name, tv, tv.ObjectType, encounterParams);
		}
	}

	private float DetermineFlatTerrainRadius()
	{
		bool flag = false;
		float result = 0f;
		for (int i = 0; i < m_Children.Length; i++)
		{
			EncounterSpawnPosition encounterSpawnPosition = m_Children[i];
			if (encounterSpawnPosition.m_FlatTerrainRadius > 0f && !flag && encounterSpawnPosition.m_Position.position == Vector3.zero)
			{
				result = encounterSpawnPosition.m_FlatTerrainRadius;
				flag = true;
			}
		}
		return result;
	}

	private void PrePool()
	{
		base.gameObject.AddComponent<WorldSpaceObject>();
	}

	private void OnPool()
	{
		for (int i = 0; i < m_Children.Length; i++)
		{
			string pTName = m_Children[i].m_PTName;
			m_SpawnPosLookup.Add(pTName, m_Children[i]);
		}
		m_Transform = base.transform;
		m_MultiSpawner.OnObjectSpawned.Subscribe(OnObjectSpawned);
		m_uScriptSaveLoad = GetComponent<uScript_SaveLoad>();
	}

	private void OnSpawn()
	{
		m_SaveData = new SaveData();
	}

	private void OnRecycle()
	{
		SetActiveEncounter(active: false);
		ClearTrackedWaypoint();
		StopFollowingTrackedVisible();
		m_MultiSpawner.Clear();
		if (m_QuestLog != null)
		{
			m_QuestLog.LogDetailsUpdated.Clear();
		}
		if (m_DefaultWaypoint != null)
		{
			if ((bool)m_DefaultWaypoint.visible)
			{
				m_DefaultWaypoint.IsQuestObject = false;
				m_DefaultWaypoint.visible.RemoveFromGame();
			}
			else
			{
				d.LogError("Encounter.OnRecycle - Default Waypoint has no visible in Encounter " + base.name);
				Singleton.Manager<ManVisible>.inst.StopTrackingVisible(m_DefaultWaypoint.ID);
				m_DefaultWaypoint.StopTracking();
			}
			m_DefaultWaypoint = null;
		}
		Crate = null;
		m_SaveData = null;
		m_QuestLog = null;
	}

	public void Update()
	{
		if (m_TrackedVisToFollow != null)
		{
			if (!m_TrackedVisToFollow.wasDestroyed)
			{
				Vector3 position = (m_TrackedVisToFollow.visible ? m_TrackedVisToFollow.visible.centrePosition : m_TrackedVisToFollow.Position);
				m_Transform.position = position;
			}
			else
			{
				m_TrackedVisToFollow = null;
			}
		}
		if (m_TrackedWaypoint != null && m_TrackedWaypoint.wasDestroyed)
		{
			ClearTrackedWaypoint();
		}
		if (m_QuestLog != null && m_QuestLog.EncounterTimer != null)
		{
			m_QuestLog.EncounterTimer.UpdateTimer();
		}
		if (m_DisablesPopulation && !HasNoPosition)
		{
			bool flag = false;
			foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
			{
				if ((allPlayerTech.boundsCentreWorld - Position).ToVector2XZ().sqrMagnitude <= EncounterRadius * EncounterRadius)
				{
					flag = true;
					break;
				}
			}
			if (flag)
			{
				Singleton.Manager<ManPop>.inst.SetPaused(paused: true);
			}
		}
		bool flag2 = s_DebugState && m_SaveData.m_CurrentlyActive;
		m_GuiCallbackWrapper.SetEnabled(flag2, base.gameObject, OnDebugGuiCallback);
		if (flag2 && m_uScriptSaveLoad != null)
		{
			m_uScriptSaveLoad.Save(null);
		}
	}

	private void OnDebugGuiCallback()
	{
		if (!m_SaveData.m_CurrentlyActive)
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder("---Encounter " + base.name + "---\n");
		bool flag = true;
		foreach (KeyValuePair<string, string> storedDatum in m_SaveData.m_StoredData)
		{
			string text = storedDatum.Key + "=" + storedDatum.Value;
			if (flag)
			{
				stringBuilder.Append(text);
			}
			else
			{
				stringBuilder.Append(" | " + text + "\n");
			}
			flag = !flag;
		}
		if (!flag)
		{
			stringBuilder.Append("\n");
		}
		stringBuilder.Append("\n");
		flag = true;
		foreach (KeyValuePair<string, EncounterVisibleData> spawnedVisible in m_SaveData.m_SpawnedVisibles)
		{
			string text2 = ((spawnedVisible.Value.m_VisibleId == -2) ? "DEAD" : spawnedVisible.Value.m_VisibleId.ToString());
			string text3 = spawnedVisible.Key + "=" + text2;
			if (flag)
			{
				stringBuilder.Append(text3);
			}
			else
			{
				stringBuilder.Append(" | " + text3 + "\n");
			}
			flag = !flag;
		}
		if (!flag)
		{
			stringBuilder.Append("\n");
		}
		stringBuilder.Append("\n");
		GUI.Box(new Rect(10f, 10f, Screen.width - 20, Screen.height - 20), stringBuilder.ToString());
	}
}
