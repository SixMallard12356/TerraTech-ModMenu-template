#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class ManEncounter : Singleton.Manager<ManEncounter>, Mode.IManagerModeEvents
{
	public enum FinishState
	{
		None,
		Cancelled,
		Failed,
		Completed
	}

	public enum UpdateChannel
	{
		GameMode,
		Challenge
	}

	[Serializable]
	public class EncounterNameChange
	{
		public string m_OldName = "Old Name";

		public string m_NewName = "New Name";
	}

	public class SaveData
	{
		public List<Encounter.SaveData> m_ActiveEncounters = new List<Encounter.SaveData>();

		public List<InActiveSetPieceBlocker> m_InActiveSetPieceBlockers = new List<InActiveSetPieceBlocker>();

		public Dictionary<string, ObjectSpawner.SaveData> m_SpawnerData = new Dictionary<string, ObjectSpawner.SaveData>();

		public MultiObjectSpawner.DebugHistory m_SpawnHistory = new MultiObjectSpawner.DebugHistory();

		public Dictionary<int, bool> m_AutoExpireVisibleToSetOnSpawn = new Dictionary<int, bool>();

		public Dictionary<string, int> m_TrackedCrates = new Dictionary<string, int>();

		public List<int> m_UnlockedCrates = new List<int>();

		public bool m_VisibleInHud = true;

		public List<uint> m_TrackedObjectsWithDestructionDelay = new List<uint>();

		public List<EncounterToSpawn> m_AvailableCoreEncounters = new List<EncounterToSpawn>();

		public List<EncounterToSpawn> m_CoreEncountersToSpawn = new List<EncounterToSpawn>();

		public List<EncounterIdentifier> m_CompletedEncounters = new List<EncounterIdentifier>();

		public bool m_UseRandomEncounterSpawn;

		public float m_SpawnTimer;

		public float m_LastTimeEncounterInLog;

		public int m_LastMissionRefreshDay;
	}

	[SerializeField]
	private TechSpawnFilter m_EncounterSizeSpawnFilter;

	[SerializeField]
	private float m_CancelMissionMinRange = 300f;

	[SerializeField]
	private float m_NearbyEncounterDist = 550f;

	[SerializeField]
	[Tooltip("Radius increase for mission areas when examing block limit reserve values")]
	public float m_ExpandMissionRadiusForLimiter;

	[Tooltip("Radius to use for missions which spawn techs before the player goes near them")]
	[SerializeField]
	private float m_BlockLimiterRadiusFar;

	[Tooltip("Block limit estimate used for missions with no other value specified")]
	[SerializeField]
	private int m_DefaultBlockLimitForMission;

	[SerializeField]
	private PrefabTable m_EncounterObjects;

	public Transform m_BlockDropEffect;

	public Dictionary<string, Transform> m_EncounterObjectsPrefabs = new Dictionary<string, Transform>();

	public const int KilledVisibleId = -2;

	public Dictionary<string, int> m_RandomEncounterTypeLookup = new Dictionary<string, int>();

	public Event<SaveData> LoadEvent;

	public Event<SaveData> SaveEvent;

	public EventNoParams ResetEvent;

	public Event<Encounter> EncounterCancelledEvent;

	public Event<EncounterToSpawn> EncounterAcceptedEvent;

	public Event<Encounter, bool> EncounterCompletedEvent;

	public Event<EncounterToSpawn, NetPlayer, bool> ClientEncounterStartedEvent;

	public Event<EncounterIdentifier, FinishState, NetPlayer> ClientEncounterFinishedEvent;

	private Dictionary<int, EncounterIdentifier> m_EncounterIDLookup = new Dictionary<int, EncounterIdentifier>(32);

	private Dictionary<EncounterIdentifier, EncounterData> m_EncounterDataLookup = new Dictionary<EncounterIdentifier, EncounterData>(32);

	private Dictionary<string, Transform> m_EncounterParentObjects = new Dictionary<string, Transform>();

	private List<Encounter> m_ActiveEncounters = new List<Encounter>();

	private List<InActiveSetPieceBlocker> m_InActiveSetPieceBlockers = new List<InActiveSetPieceBlocker>();

	private SaveData m_SaveData;

	private Bitfield<UpdateChannel> m_UpdateDisabledChannels = new Bitfield<UpdateChannel>(new UpdateChannel[1]);

	private MultiObjectSpawner m_MultiSpawner = new MultiObjectSpawner();

	private Dictionary<string, int> m_TrackedCrates = new Dictionary<string, int>();

	private List<int> m_UnlockedCrates = new List<int>();

	private Dictionary<int, float> m_EncounterRadiusLookup = new Dictionary<int, float>();

	private Dictionary<TrackedVisible, bool> m_AutoExpireVisibleToSetOnSpawn = new Dictionary<TrackedVisible, bool>();

	private TrackedVisible m_CurrentWaypointOverlay;

	public bool UpdateEnabled
	{
		get
		{
			if (m_UpdateDisabledChannels.IsNull)
			{
				return ManNetwork.IsHostOrWillBe;
			}
			return false;
		}
	}

	public TechSpawnFilter EncounterRadiusFilter => m_EncounterSizeSpawnFilter;

	public float MinEncounterCancelDistance => m_CancelMissionMinRange;

	public List<Encounter> ActiveEncounters => m_ActiveEncounters;

	public List<InActiveSetPieceBlocker> InActiveSetPieceBlockers => m_InActiveSetPieceBlockers;

	public Dictionary<EncounterIdentifier, EncounterData> EncounterData => m_EncounterDataLookup;

	public bool TestBlockLimiterForAnalytics => true;

	public bool VisibleInHud { get; private set; }

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		m_SaveData = null;
		if (optionalLoadState == null)
		{
			m_AutoExpireVisibleToSetOnSpawn.Clear();
			ModeStartComplete();
		}
		else
		{
			optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManEncounter, out m_SaveData);
			Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		}
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.RequestAcceptMission, OnServerRequestMissionAccept);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.EncounterStarted, OnClientEncounterStarted);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.EncounterFinished, OnClientEncounterFinished);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.ChallengeTimerChange, OnClientChallengeTimerChange);
	}

	private void OnModeStart(Mode mode)
	{
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Unsubscribe(OnModeStart);
		SaveData saveData = m_SaveData;
		if (saveData != null)
		{
			VisibleInHud = saveData.m_VisibleInHud;
			HashSet<EncounterIdentifier> hashSet = new HashSet<EncounterIdentifier>();
			m_InActiveSetPieceBlockers.Clear();
			for (int i = 0; i < saveData.m_InActiveSetPieceBlockers.Count; i++)
			{
				m_InActiveSetPieceBlockers.Add(saveData.m_InActiveSetPieceBlockers[i]);
			}
			for (int j = 0; j < saveData.m_ActiveEncounters.Count; j++)
			{
				Encounter.SaveData saveData2 = saveData.m_ActiveEncounters[j];
				if (hashSet.Contains(saveData2.m_EncounterDef))
				{
					continue;
				}
				EncounterData encounterData = GetEncounterData(saveData2.m_EncounterDef);
				if (encounterData != null)
				{
					WorldPosition worldPosition = saveData2.m_PositionWorld;
					if (saveData2.m_Position != Vector3.zero && worldPosition == default(WorldPosition))
					{
						worldPosition = WorldPosition.FromGameWorldPosition((Vector3)saveData2.m_Position);
					}
					EncounterToSpawn spawnParams = new EncounterToSpawn(encounterData, saveData2.m_EncounterDef)
					{
						m_Position = worldPosition,
						m_Rotation = saveData2.m_Rotation
					};
					if (StartEncounter(spawnParams, saveData2))
					{
						hashSet.Add(saveData2.m_EncounterDef);
						continue;
					}
					d.LogError("Failed to restore encounter: " + encounterData.m_Name + ", EncounterDef=" + saveData2.m_EncounterDef);
				}
				else
				{
					d.LogError("Trying to restore an non existing encounter: EncounterDef=" + saveData2.m_EncounterDef);
				}
			}
			foreach (KeyValuePair<int, bool> item in saveData.m_AutoExpireVisibleToSetOnSpawn)
			{
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(item.Key);
				if (trackedVisible != null)
				{
					m_AutoExpireVisibleToSetOnSpawn.Add(trackedVisible, item.Value);
				}
				else
				{
					d.LogError("ManEncounter.Load: Can't find TrackedVisible for ID " + item.Key + " for changing AutoExpire state on respawn");
				}
			}
			bool autoRetry = true;
			m_MultiSpawner.Load(saveData.m_SpawnerData, saveData.m_SpawnHistory, autoRetry);
			m_TrackedCrates = saveData.m_TrackedCrates;
			m_UnlockedCrates = saveData.m_UnlockedCrates;
			foreach (uint item2 in saveData.m_TrackedObjectsWithDestructionDelay)
			{
				if (!PostChallengeDestructionDelay.StaticTryInitDelayedDestruction(item2))
				{
					TrackedObjectReference trackedObject = Singleton.Manager<ManVisible>.inst.GetTrackedObject(item2);
					if (trackedObject != null)
					{
						Singleton.Manager<ManVisible>.inst.ObliterateTrackedObjectFromWorld(trackedObject);
					}
				}
			}
		}
		else
		{
			m_TrackedCrates = null;
			m_UnlockedCrates = null;
		}
		if (m_TrackedCrates == null)
		{
			m_TrackedCrates = new Dictionary<string, int>();
		}
		if (m_UnlockedCrates == null)
		{
			m_UnlockedCrates = new List<int>();
		}
		for (int num = m_UnlockedCrates.Count - 1; num >= 0; num--)
		{
			int id = m_UnlockedCrates[num];
			m_UnlockedCrates.RemoveAt(num);
			UnlockCrateById(id);
		}
		LoadEvent.Send(saveData);
		ModeStartComplete();
	}

	private void ModeStartComplete()
	{
		SetUpdateEnabled(UpdateChannel.GameMode, enabled: true);
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		SaveEvent.Send(saveData);
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter.SaveData item = m_ActiveEncounters[i].Save();
			saveData.m_ActiveEncounters.Add(item);
		}
		saveData.m_InActiveSetPieceBlockers.Clear();
		for (int j = 0; j < m_InActiveSetPieceBlockers.Count; j++)
		{
			saveData.m_InActiveSetPieceBlockers.Add(m_InActiveSetPieceBlockers[j]);
		}
		saveData.m_VisibleInHud = VisibleInHud;
		saveData.m_TrackedCrates = m_TrackedCrates;
		saveData.m_UnlockedCrates = m_UnlockedCrates;
		saveData.m_TrackedObjectsWithDestructionDelay.Clear();
		saveData.m_TrackedObjectsWithDestructionDelay.Capacity = PostChallengeDestructionDelay.s_SaveData.DelayedDestructionIDs.Count;
		foreach (uint delayedDestructionID in PostChallengeDestructionDelay.s_SaveData.DelayedDestructionIDs)
		{
			saveData.m_TrackedObjectsWithDestructionDelay.Add(delayedDestructionID);
		}
		foreach (KeyValuePair<TrackedVisible, bool> item2 in m_AutoExpireVisibleToSetOnSpawn)
		{
			saveData.m_AutoExpireVisibleToSetOnSpawn.Add(item2.Key.ID, item2.Value);
		}
		m_MultiSpawner.Save(ref saveData.m_SpawnerData, ref saveData.m_SpawnHistory);
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManEncounter, saveData);
	}

	public void ModeExit()
	{
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromServerMessage(TTMsgType.RequestAcceptMission, OnServerRequestMissionAccept);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.EncounterStarted, OnClientEncounterStarted);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.EncounterFinished, OnClientEncounterFinished);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.ChallengeTimerChange, OnClientChallengeTimerChange);
	}

	public void FixupLateSpawnedVisibles()
	{
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			m_ActiveEncounters[i].FixupLateSpawnedVisibles();
		}
	}

	public void ResetEncounters()
	{
		for (int num = m_ActiveEncounters.Count - 1; num >= 0; num--)
		{
			Singleton.Manager<ManQuestLog>.inst.RemoveLog(m_ActiveEncounters[num], FinishState.None, null);
			m_ActiveEncounters[num].Kill(instant: true);
			m_ActiveEncounters.RemoveAt(num);
		}
		ResetEvent.Send();
		m_EncounterRadiusLookup.Clear();
		VisibleInHud = false;
		m_AutoExpireVisibleToSetOnSpawn.Clear();
		m_MultiSpawner.Clear();
		m_TrackedCrates.Clear();
		m_UnlockedCrates.Clear();
		PostChallengeDestructionDelay.s_SaveData.Clear();
		RemoveNavigationOverlay(m_CurrentWaypointOverlay);
	}

	public void RegisterEncounters(EncounterList list)
	{
		m_EncounterIDLookup.Clear();
		m_EncounterDataLookup.Clear();
		foreach (CorpEncounters encounter in list.Encounters)
		{
			for (int i = 0; i < encounter.m_Grades.Count; i++)
			{
				foreach (EncounterCategory category in encounter.m_Grades[i].m_Categories)
				{
					foreach (EncounterData encounter2 in category.m_Encounters)
					{
						EncounterIdentifier encounterDef = new EncounterIdentifier(encounter.m_Corp, i + 1, category.m_Name, encounter2.m_Name);
						RegisterEncounter(encounterDef, encounter2);
					}
				}
			}
		}
	}

	public void RegisterEncounter(EncounterIdentifier encounterDef, EncounterData encounterData)
	{
		m_EncounterIDLookup.Add(encounterDef.GetHashCode(), encounterDef);
		m_EncounterDataLookup.Add(encounterDef, encounterData);
	}

	public EncounterIdentifier GetEncounterIdentifier(int hash)
	{
		EncounterIdentifier value = EncounterIdentifier.Invalid;
		if (!m_EncounterIDLookup.TryGetValue(hash, out value))
		{
			d.LogError("Could not find encounter ID with hash " + hash);
		}
		return value;
	}

	public EncounterData GetEncounterData(EncounterIdentifier id, bool errorIfNotFound = true)
	{
		EncounterData value = null;
		if (!m_EncounterDataLookup.TryGetValue(id, out value) && errorIfNotFound)
		{
			d.LogError("Could not find encounter data with ID " + id);
		}
		return value;
	}

	public void SetUpdateEnabled(UpdateChannel channel, bool enabled)
	{
		if (!enabled)
		{
			m_UpdateDisabledChannels.Add((int)channel);
		}
		else
		{
			m_UpdateDisabledChannels.Remove((int)channel);
		}
	}

	public bool GetPlayerRespawnOverride(ref Vector3 scenePos, ref Vector3 facingDir)
	{
		foreach (Encounter activeEncounter in m_ActiveEncounters)
		{
			if (activeEncounter.GetPlayerRespawnOverrideLocation(ref scenePos, ref facingDir))
			{
				return true;
			}
		}
		return false;
	}

	public bool StartEncounter(EncounterToSpawn spawnParams, Encounter.SaveData sourceData = null, NetPlayer fromPlayer = null)
	{
		if (spawnParams.m_EncounterData == null)
		{
			d.LogError("Attempting to start encounter with invalid params!");
			return false;
		}
		if (!ManNetwork.IsHost)
		{
			d.LogWarning("Attempting to start encounter " + spawnParams.m_EncounterData.m_Name + " on client. Ignored.");
			return false;
		}
		d.AssertFormat(GetEncounterData(spawnParams.m_EncounterDef, errorIfNotFound: false) != null, "Spawning encounter '{0}' but failed to find it in the RegisteredEncounters lookup table! Was it correctly part of the list?", spawnParams.m_EncounterData.m_EncounterPrefab.name);
		d.Assert(GetActiveEncounter(spawnParams.m_EncounterDef) == null, "StartEncounter - Expecting to only ever have 1 encounter active per UniqueID (EncounterIdentity). Code does not support multiples.");
		Vector3 vector = spawnParams.m_Position.ScenePosition;
		float spawnRadius = spawnParams.m_EncounterData.m_EncounterPrefab.SpawnRadius;
		if (sourceData == null && !spawnParams.m_UsePosForPlacement)
		{
			d.AssertFormat(Singleton.Manager<ManWorld>.inst.CheckAllTilesAtPositionHaveReachedLoadStep(vector, spawnRadius) || spawnParams.m_EncounterData.m_CanSpawnOffTile, "ManEncounter.StartEncounter - Encounter {0} is (partially) overlapping non-loaded Tiles at spawn pos (scene) {1}!", spawnParams.m_EncounterData.m_Name, vector);
		}
		if (sourceData == null)
		{
			TerrainSetPiece requiredSetPiece = spawnParams.m_EncounterData.m_EncounterPrefab.GetRequiredSetPiece();
			if ((bool)requiredSetPiece && !Singleton.Manager<ManWorld>.inst.AddTerrainSetPiece(requiredSetPiece, spawnParams.m_Position, spawnParams.GetRotationForSetPiece()))
			{
				d.LogError("Attempting to start encounter but set piece terrain can't be added");
				return false;
			}
		}
		if (sourceData == null)
		{
			vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(vector);
		}
		Encounter encounter = spawnParams.m_EncounterData.m_EncounterPrefab.Spawn(vector, spawnParams.m_Rotation);
		m_ActiveEncounters.Add(encounter);
		string key = (encounter.name = spawnParams.m_EncounterData.m_Name.ToLower());
		if (!m_EncounterParentObjects.ContainsKey(key))
		{
			Transform transform = new GameObject().transform;
			transform.name = key;
			transform.parent = base.transform;
			m_EncounterParentObjects.Add(key, transform);
		}
		encounter.transform.parent = m_EncounterParentObjects[key];
		TrackedVisible trackedWaypoint = null;
		int num = 0;
		bool flag = sourceData != null;
		if (flag)
		{
			if (sourceData.m_WaypointVisibleId != -1)
			{
				trackedWaypoint = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(sourceData.m_WaypointVisibleId);
			}
			encounter.Load(sourceData);
			num = sourceData.m_EncounterStringBankIdx;
		}
		else
		{
			num = spawnParams.m_EncounterStringBankIdx;
		}
		encounter.Begin(spawnParams.m_EncounterDef, trackedWaypoint, spawnParams.m_EncounterData, num, flag, fromPlayer, out var setTrackedEncounter);
		encounter.IsScriptUpdateEnabled = (flag && sourceData.m_IsScriptUpdateEnabled) || EnoughTilesLoadedForEncounter(encounter);
		if (encounter.VisibleInLog && ManNetwork.IsHost)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				EncounterStartedMessage message = new EncounterStartedMessage
				{
					m_EncounterSpawn = spawnParams,
					m_WaypointHostID = encounter.EncounterWaypointID,
					m_PlayerNetID = (fromPlayer ? fromPlayer.netId : NetworkInstanceId.Invalid),
					m_SetTracked = setTrackedEncounter
				};
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.EncounterStarted, message);
				ServerSendQuestLogUpdate(encounter);
				encounter.QuestLog.LogDetailsUpdated.Subscribe(ServerOnQuestLogUpdated);
			}
			ReportEncounterStarted(spawnParams, fromPlayer, encounter.EncounterWaypointID, setTrackedEncounter);
		}
		return true;
	}

	public void SendEncountersToClient(NetPlayer targetPlayer)
	{
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter encounter = m_ActiveEncounters[i];
			if (encounter.VisibleInLog)
			{
				EncounterToSpawn encounterToSpawn = new EncounterToSpawn(encounter.EncounterDef);
				encounterToSpawn.m_EncounterStringBankIdx = encounter.EncounterStringBankIdx;
				encounterToSpawn.m_Rotation = encounter.transform.rotation;
				encounterToSpawn.m_Position = WorldPosition.FromScenePosition(encounter.transform.position);
				EncounterStartedMessage message = new EncounterStartedMessage
				{
					m_EncounterSpawn = encounterToSpawn,
					m_WaypointHostID = encounter.EncounterWaypointID,
					m_SetTracked = false
				};
				Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.EncounterStarted, message);
				EncounterUpdateMessage encounterUpdateMessage = new EncounterUpdateMessage
				{
					m_Id = encounter.EncounterDef
				};
				encounter.QuestLog.FillMessage(encounterUpdateMessage);
				Singleton.Manager<ManNetwork>.inst.SendToClient(targetPlayer.connectionToClient.connectionId, TTMsgType.QuestLogDataUpdated, encounterUpdateMessage);
			}
		}
	}

	public void TryAddRemovedEncounterToSetPieceBlockers(Encounter encounter)
	{
		if (encounter.BlockFutureEncountersInThisRadius)
		{
			InActiveSetPieceBlocker inActiveEncounter = new InActiveSetPieceBlocker(encounter);
			InActiveSetPieceBlocker inActiveSetPieceBlocker = m_InActiveSetPieceBlockers.Where((InActiveSetPieceBlocker r) => r.WorldPos == inActiveEncounter.WorldPos).FirstOrDefault();
			if (inActiveSetPieceBlocker != null && inActiveSetPieceBlocker.EncounterRadius < inActiveEncounter.EncounterRadius)
			{
				inActiveSetPieceBlocker.EncounterRadius = inActiveEncounter.EncounterRadius;
			}
			else
			{
				m_InActiveSetPieceBlockers.Add(inActiveEncounter);
			}
		}
	}

	public void FinishEncounter(Encounter encounter, FinishState state, NetPlayer fromPlayer = null)
	{
		if (encounter != null)
		{
			bool flag = state == FinishState.Completed;
			Singleton.Manager<ManQuestLog>.inst.RemoveLog(encounter, state, fromPlayer);
			if (m_ActiveEncounters.Contains(encounter))
			{
				m_ActiveEncounters.Remove(encounter);
				TryAddRemovedEncounterToSetPieceBlockers(encounter);
			}
			else
			{
				d.LogError("ManEncounter.EncounterFinished: encounter data is not in active list.");
			}
			m_EncounterRadiusLookup.Remove(encounter.EncounterWaypointID);
			EncounterCompletedEvent.Send(encounter, flag);
			if (flag)
			{
				_ = encounter.EncounterDef;
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.MissionComplete);
				DeliverRewards(encounter);
			}
			else
			{
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.MissionFailed);
			}
			string crateName = encounter.CrateName;
			if (m_TrackedCrates.ContainsKey(crateName))
			{
				m_TrackedCrates.Remove(crateName);
			}
			if (flag)
			{
				encounter.UpdateBlockLimiterExtents(missionComplete: true);
			}
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					encounter.QuestLog.LogDetailsUpdated.Unsubscribe(ServerOnQuestLogUpdated);
					EncounterFinishedMessage encounterFinishedMessage = new EncounterFinishedMessage
					{
						m_Id = encounter.EncounterDef,
						m_State = state,
						m_WaypointHostID = encounter.EncounterWaypointID
					};
					if ((bool)fromPlayer)
					{
						encounterFinishedMessage.m_PlayerNetID = fromPlayer.netId;
					}
					Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.EncounterFinished, encounterFinishedMessage);
				}
			}
			else
			{
				ClientEncounterFinishedEvent.Send(encounter.EncounterDef, state, null);
			}
			encounter.RemoveAllTrackedObjects(removeResourceSeams: false);
			encounter.Kill(instant: false);
		}
		else
		{
			d.LogError("ManEncounter.EncounterFinished: encounter data is null");
		}
	}

	public void CancelEncounter(Encounter encounter, NetPlayer fromPlayer)
	{
		if (encounter != null)
		{
			EncounterCancelledEvent.Send(encounter);
			EncounterIdentifier encounterDef = encounter.EncounterDef;
			EncounterData encounterData = GetEncounterData(encounterDef);
			if (encounterData == null || encounterData.m_RecycleAllManagedObjectsOnCancel)
			{
				encounter.RemoveAllManagedObjects();
			}
			FinishEncounter(encounter, FinishState.Cancelled, fromPlayer);
		}
		else
		{
			d.LogError("ManEncounter.CancelEncounter - Passed in Encounter is null");
		}
	}

	public Vector3 GetRewardCratePosition(Encounter encounter)
	{
		Vector3 result = Singleton.playerPos;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !encounter.HasNoPosition)
		{
			float num = float.MaxValue;
			foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
			{
				float magnitude = (allPlayerTech.boundsCentreWorld - encounter.Position).magnitude;
				if (magnitude < num)
				{
					num = magnitude;
					result = allPlayerTech.boundsCentreWorld;
				}
			}
		}
		return result;
	}

	public void DeliverRewards(Encounter encounter)
	{
		if (encounter.EncounterDetails.AwardXP)
		{
			Singleton.Manager<ManLicenses>.inst.AddXP(encounter.EncounterDetails.XPCorp, encounter.EncounterDetails.XPAmount);
		}
		if (encounter.EncounterDetails.AwardBB)
		{
			Singleton.Manager<ManPlayer>.inst.AddMoney(encounter.EncounterDetails.BBAmount);
		}
		if (encounter.EncounterDetails.BlocksNeedAwardingOnCompletion)
		{
			bool locked = false;
			bool visibleOnRadar = true;
			Vector3 rewardCratePosition = GetRewardCratePosition(encounter);
			encounter.TrySpawnBlockRewardCrateWithSpawner(rewardCratePosition, Quaternion.identity, locked, visibleOnRadar, m_MultiSpawner);
		}
		if (encounter.EncounterDetails.AwardTech && encounter.EncounterDetails.TechNeedsAwarding && encounter.EncounterDetails.TechToAward != null)
		{
			ManSpawn.TechSpawnParams objectSpawnParams = new ManSpawn.TechSpawnParams
			{
				m_TechToSpawn = encounter.EncounterDetails.TechToAward,
				m_Team = 0,
				m_Rotation = Quaternion.Euler(0f, UnityEngine.Random.value * 360f, 0f),
				m_Grounded = true,
				m_SpawnVisualType = ManSpawn.SpawnVisualType.Bomb
			};
			ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
			{
				m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
				m_CircleRadius = encounter.EncounterDetails.TechToAward.Radius,
				m_CenterPosWorld = WorldPosition.FromScenePosition(Singleton.playerPos),
				m_CircleIndex = 0,
				m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
				m_CheckSafeArea = false,
				m_RejectFunc = null
			};
			string text = $"{base.name}_Tech_{encounter.EncounterDetails.TechToAward.Name}";
			bool autoRetry = true;
			m_MultiSpawner.TrySpawn(objectSpawnParams, freeSpaceParams, text, autoRetry);
		}
		if (encounter.EncounterDetails.AwardLicense)
		{
			Singleton.Manager<ManLicenses>.inst.UnlockCorp(encounter.EncounterDetails.CorpLicenseToAward, showLevelUp: true);
		}
	}

	public bool IsActiveEncounter(EncounterIdentifier encounterID)
	{
		return GetActiveEncounter(encounterID).IsNotNull();
	}

	public Encounter GetActiveEncounter(EncounterIdentifier encounterID)
	{
		foreach (Encounter activeEncounter in m_ActiveEncounters)
		{
			if (activeEncounter.EncounterDef == encounterID)
			{
				return activeEncounter;
			}
		}
		return null;
	}

	public Transform SpawnFireTrail(Transform parent, Vector3 pos)
	{
		return m_BlockDropEffect.Spawn(parent, pos);
	}

	public void SetEncountersVisibleInHud(bool visible)
	{
		if (VisibleInHud != visible)
		{
			VisibleInHud = visible;
			for (int i = 0; i < m_ActiveEncounters.Count(); i++)
			{
				m_ActiveEncounters[i].UpdateActiveWaypoint();
			}
		}
	}

	public float GetEncounterRadius(int defaultWaypointHostID)
	{
		float value = 0f;
		if (!m_EncounterRadiusLookup.TryGetValue(defaultWaypointHostID, out value))
		{
			d.LogError("ManEncounter.GetEncounterRadius - no radius tracked for HostID " + defaultWaypointHostID);
		}
		return value;
	}

	public void TrySpawnRewardCrate(Encounter encounter, Vector3 pos, Quaternion rot, bool visibleOnRadar)
	{
		bool locked = true;
		encounter.TrySpawnBlockRewardCrate(pos, rot, locked, visibleOnRadar);
	}

	public void UnlockRewardCrate(Encounter encounter)
	{
		bool flag = false;
		int value = 0;
		TrackedVisible crate = encounter.Crate;
		if (crate != null)
		{
			flag = true;
			value = crate.ID;
		}
		if (!flag && m_TrackedCrates.TryGetValue(encounter.CrateName, out value))
		{
			flag = true;
		}
		if (flag)
		{
			UnlockCrateById(value);
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.RewardCrate);
		}
		else
		{
			d.LogWarning("UnlockRewardCrate: trying to unlock a crate called " + encounter.name + " which isn't spawned yet");
		}
	}

	public bool IsCrateSpawned(Encounter encounter)
	{
		bool flag = false;
		if (encounter.Crate != null)
		{
			flag = true;
		}
		if (!flag && m_TrackedCrates.ContainsKey(encounter.CrateName))
		{
			flag = true;
		}
		return flag;
	}

	public Crate GetCrate(Encounter encounter)
	{
		Crate crate = null;
		if (encounter.Crate != null)
		{
			TrackedVisible crate2 = encounter.Crate;
			crate = ((crate2.visible != null) ? crate2.visible.crate : null);
		}
		if (crate == null && m_TrackedCrates.TryGetValue(encounter.CrateName, out var value))
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(value);
			if (trackedVisible != null && trackedVisible.visible != null)
			{
				crate = trackedVisible.visible.crate;
			}
		}
		return crate;
	}

	public void EnableAutoExpireVisibleOnRespawn(TrackedVisible trackedVis, bool enable)
	{
		bool value;
		bool num = m_AutoExpireVisibleToSetOnSpawn.TryGetValue(trackedVis, out value);
		if (num)
		{
			d.LogWarning("ManEncounter.EnableAutoExpireVisibleOnRespawn - visible already waiting for respawn to set AutoExpire: " + value);
			m_AutoExpireVisibleToSetOnSpawn.Remove(trackedVis);
		}
		m_AutoExpireVisibleToSetOnSpawn.Add(trackedVis, enable);
		if (!num)
		{
			trackedVis.OnRespawnEvent.Subscribe(OnVisibleRespawnSetAutoExpire);
		}
	}

	public bool SpawnAndStartListedEncounter(EncounterToSpawn encounterToSpawn, NetPlayer fromPlayer)
	{
		bool flag = false;
		if (!ManNetwork.IsHost)
		{
			return true;
		}
		Encounter activeEncounter = GetActiveEncounter(encounterToSpawn.m_EncounterDef);
		if (activeEncounter.IsNotNull())
		{
			Singleton.Manager<ManQuestLog>.inst.AddLog(activeEncounter, fromPlayer, restoredFromSaveData: false);
			flag = true;
		}
		else
		{
			EncounterData encounterData = encounterToSpawn.m_EncounterData;
			EncounterAcceptedEvent.Send(encounterToSpawn);
			d.Assert(encounterData.m_EncounterPrefab != null && (Singleton.playerTank != null || encounterData.m_HasNoPosition), "ManEncounter.SpawnAndStartListedEncounter - Trying to start encounter " + encounterData.m_Name + " but was unable to given the current parameters");
			if (encounterData.m_EncounterPrefab != null && (Singleton.playerTank != null || encounterData.m_HasNoPosition))
			{
				flag = StartEncounter(encounterToSpawn, null, fromPlayer);
			}
			d.Assert(flag, "ManEncounter.SpawnAndStartListedEncounter - Failed to spawn selected encounter!");
		}
		return flag;
	}

	public bool SpawnEncounter(EncounterToSpawn encounterToSpawn)
	{
		bool result = false;
		if (encounterToSpawn.m_EncounterData.m_EncounterPrefab != null)
		{
			result = StartEncounter(encounterToSpawn);
		}
		else
		{
			d.LogError("ManEncounter.SpawnEncounter - Failed to spawn encounter - Encounter prefab was null for encounter! " + encounterToSpawn.m_EncounterData.m_Name);
		}
		return result;
	}

	private void OnServerRequestMissionAccept(NetworkMessage netMsg)
	{
		EncounterToSpawn encounterSpawn = netMsg.ReadMessage<EncounterMessage>().m_EncounterSpawn;
		encounterSpawn.m_EncounterData = Singleton.Manager<ManEncounter>.inst.GetEncounterData(encounterSpawn.m_EncounterDef);
		if (Singleton.Manager<ManEncounterPlacement>.inst.IsValidListedEncounter(encounterSpawn))
		{
			SpawnAndStartListedEncounter(encounterSpawn, netMsg.GetSender());
		}
	}

	public void RequestStartEncounter(EncounterToSpawn encounterParams)
	{
		d.Assert(encounterParams != null, "RequestStartEncounter - Was passed NULL encounterParams!");
		if (encounterParams != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				EncounterMessage message = new EncounterMessage
				{
					m_EncounterSpawn = encounterParams
				};
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestAcceptMission, message);
			}
			else if (Singleton.Manager<ManEncounterPlacement>.inst.IsValidListedEncounter(encounterParams))
			{
				Singleton.Manager<ManEncounter>.inst.SpawnAndStartListedEncounter(encounterParams, null);
			}
		}
	}

	public int GetLimiterCostOfEncounterNear(Vector3 pos, float radius, Encounter encounter, Vector3 encounterPos)
	{
		bool flag = false;
		if (encounter.HasNoPosition)
		{
			flag = true;
		}
		else
		{
			Vector2 vector = (pos - encounterPos).ToVector2XZ();
			float num = GetEncounterRadiusForLimiter(encounter) + radius;
			flag = vector.sqrMagnitude <= num * num;
		}
		if (!flag)
		{
			return 0;
		}
		return GetLimiterCostEstimateForEncounter(encounter);
	}

	public int GetMaxNearbyLimitForMission(Encounter encounter)
	{
		int limiterCostEstimateForEncounter = GetLimiterCostEstimateForEncounter(encounter);
		int num = Singleton.Manager<ManBlockLimiter>.inst.UsableMissionLimit - limiterCostEstimateForEncounter;
		if (num <= 0)
		{
			d.LogError($"Mission {encounter.name} cannot spawn - cost {limiterCostEstimateForEncounter} > maximum allowed {Singleton.Manager<ManBlockLimiter>.inst.UsableMissionLimit}", encounter);
		}
		return num;
	}

	public float GetEncounterRadiusForLimiter(Encounter encounter)
	{
		float num = encounter.EncounterRadius + m_ExpandMissionRadiusForLimiter;
		if (encounter.SpawnsTechsFarAway)
		{
			num = Mathf.Max(num, m_BlockLimiterRadiusFar);
		}
		return num;
	}

	public int GetLimiterCostEstimateForEncounter(Encounter encounter)
	{
		if (encounter.HasSpecificBlockLimit)
		{
			return encounter.BlockLimit;
		}
		return Mathf.Max(encounter.BlockLimit, m_DefaultBlockLimitForMission);
	}

	public int GetLimiterCostOfEncountersNear(Vector3 pos, float radius)
	{
		int num = 0;
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter encounter = m_ActiveEncounters[i];
			num += GetLimiterCostOfEncounterNear(pos, radius, m_ActiveEncounters[i], encounter.Position);
		}
		return num;
	}

	public int GetNumNearbyEncounters(Vector3 scenePos, float nearbyRange = -1f, Func<Encounter, bool> isValidPredicate = null)
	{
		int num = 0;
		nearbyRange = ((nearbyRange > 0f) ? nearbyRange : m_NearbyEncounterDist);
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter encounter = m_ActiveEncounters[i];
			if (!encounter.HasNoPosition)
			{
				Vector3 vector = (scenePos - encounter.Position).SetY(0f);
				float num2 = (nearbyRange + encounter.EncounterRadius) * (nearbyRange + encounter.EncounterRadius);
				if (vector.sqrMagnitude <= num2 && (isValidPredicate == null || isValidPredicate(encounter)))
				{
					num++;
				}
			}
		}
		return num;
	}

	public void ConfigureNavigationOverlay(TrackedVisible encounterWaypoint)
	{
		if (encounterWaypoint != m_CurrentWaypointOverlay)
		{
			if (m_CurrentWaypointOverlay != null)
			{
				Singleton.Manager<ManOverlay>.inst.RemoveWaypointOverlay(m_CurrentWaypointOverlay);
				m_CurrentWaypointOverlay = null;
			}
			if (encounterWaypoint != null)
			{
				Singleton.Manager<ManOverlay>.inst.AddWaypointOverlay(encounterWaypoint);
				m_CurrentWaypointOverlay = encounterWaypoint;
			}
		}
	}

	public void RemoveNavigationOverlay(TrackedVisible encounterWaypoint)
	{
		if (m_CurrentWaypointOverlay != null && encounterWaypoint == m_CurrentWaypointOverlay)
		{
			Singleton.Manager<ManOverlay>.inst.RemoveWaypointOverlay(m_CurrentWaypointOverlay);
			m_CurrentWaypointOverlay = null;
		}
	}

	[Conditional("USE_ANALYTICS")]
	public void SendEncounterAnalyticEvent(Encounter encounter, string eventName)
	{
	}

	[Conditional("USE_ANALYTICS")]
	public void SendEncounterAnalyticEvent(EncounterToSpawn encounter, string eventName)
	{
	}

	[Conditional("USE_ANALYTICS")]
	public void SendEncounterAnalyticEvent(EncounterIdentifier encounterID, EncounterDetails encounterDetails, string eventName)
	{
		if (encounterDetails.MissionTypeForAnalytics != EncounterDetails.AnalyticsMissionType.DoNotTrack)
		{
			new Dictionary<string, object>
			{
				{ "type", encounterDetails.MissionTypeForAnalytics },
				{ "name", encounterID.m_Name },
				{ "corporation", encounterID.m_Corp },
				{ "grade", encounterID.m_Grade }
			};
		}
	}

	private void OnVisibleRespawnSetAutoExpire(Visible visible)
	{
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.ID);
		if (trackedVisible != null)
		{
			if (m_AutoExpireVisibleToSetOnSpawn.TryGetValue(trackedVisible, out var value))
			{
				visible.EnableAutoExpire(value);
				trackedVisible.OnRespawnEvent.Unsubscribe(OnVisibleRespawnSetAutoExpire);
				m_AutoExpireVisibleToSetOnSpawn.Remove(trackedVisible);
			}
			else
			{
				d.LogError("ManEncounter.OnVisibleRespawnSetAutoExpire - Visible " + visible.name + " has respawned, but isn't in dictionary");
			}
		}
		else
		{
			d.LogError("ManEncounter.OnVisibleRespawnSetAutoExpire - Visible " + visible.name + " has respawned, but doesn have a tracked Visible");
		}
	}

	private void UnlockCrateById(int id)
	{
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(id);
		if (trackedVisible != null)
		{
			if ((bool)trackedVisible.visible)
			{
				if ((bool)trackedVisible.visible.crate)
				{
					trackedVisible.visible.crate.Unlock();
				}
				else
				{
					d.LogWarning("UnlockRewardCrate: Crate object doesn't have crate component with id" + id);
				}
			}
			else
			{
				m_UnlockedCrates.Add(trackedVisible.ID);
				trackedVisible.OnRespawnEvent.Subscribe(UnlockCrateOnRespawn);
			}
		}
		else
		{
			d.LogWarning("UnlockRewardCrate: Unable to find tracked visible for id " + id);
		}
	}

	private void OnModeExit(Mode mode)
	{
		SetUpdateEnabled(UpdateChannel.GameMode, enabled: false);
		ResetEncounters();
	}

	private void UnlockCrateOnRespawn(Visible crateVis)
	{
		if (crateVis != null)
		{
			if (crateVis.crate != null)
			{
				crateVis.crate.Unlock();
			}
			m_UnlockedCrates.Remove(crateVis.ID);
		}
	}

	private void ServerSendQuestLogUpdate(Encounter encounter)
	{
		EncounterUpdateMessage encounterUpdateMessage = new EncounterUpdateMessage
		{
			m_Id = encounter.EncounterDef
		};
		encounter.QuestLog.FillMessage(encounterUpdateMessage);
		Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.QuestLogDataUpdated, encounterUpdateMessage);
	}

	public void ServerOnQuestLogUpdated(EncounterIdentifier id)
	{
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			if (m_ActiveEncounters[i].EncounterDef == id)
			{
				ServerSendQuestLogUpdate(m_ActiveEncounters[i]);
				break;
			}
		}
	}

	private void OnClientEncounterStarted(NetworkMessage netMsg)
	{
		EncounterStartedMessage encounterStartedMessage = netMsg.ReadMessage<EncounterStartedMessage>();
		EncounterToSpawn encounterSpawn = encounterStartedMessage.m_EncounterSpawn;
		encounterSpawn.m_EncounterData = GetEncounterData(encounterSpawn.m_EncounterDef);
		NetPlayer fromPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(encounterStartedMessage.m_PlayerNetID.Value);
		ReportEncounterStarted(encounterSpawn, fromPlayer, encounterStartedMessage.m_WaypointHostID, encounterStartedMessage.m_SetTracked);
	}

	private void ReportEncounterStarted(EncounterToSpawn encounterParams, NetPlayer fromPlayer, int waypointHostID, bool setTracked)
	{
		ClientEncounterStartedEvent.Send(encounterParams, fromPlayer, setTracked);
		if (encounterParams.m_EncounterData.m_ShowAreaOnMiniMap)
		{
			m_EncounterRadiusLookup[waypointHostID] = encounterParams.m_EncounterData.m_EncounterPrefab.EncounterRadius;
		}
	}

	private void OnClientEncounterFinished(NetworkMessage netMsg)
	{
		EncounterFinishedMessage encounterFinishedMessage = netMsg.ReadMessage<EncounterFinishedMessage>();
		ClientEncounterFinishedEvent.Send(encounterFinishedMessage.m_Id, encounterFinishedMessage.m_State, Singleton.Manager<ManNetwork>.inst.FindPlayerById(encounterFinishedMessage.m_PlayerNetID.Value));
		m_EncounterRadiusLookup.Remove(encounterFinishedMessage.m_WaypointHostID);
	}

	private void OnClientChallengeTimerChange(NetworkMessage netMsg)
	{
		ChallengeTimerMessage msg = netMsg.ReadMessage<ChallengeTimerMessage>();
		Singleton.Manager<ManQuestLog>.inst.HandleMissionTimerMessage(msg);
	}

	private bool EnoughTilesLoadedForEncounter(Encounter encounter)
	{
		if (encounter.GetRequiredSetPiece().IsNotNull())
		{
			if (encounter.StartsImmediately)
			{
				UnityEngine.Debug.LogError("Ignoring flag encounter.StartsImmediately for " + encounter.name + ", because this is a set piece encounter");
			}
			bool flag = Singleton.Manager<ManWorld>.inst.CheckAtLeastOneTileAtPositionHasReachedLoadStep(encounter.Position, encounter.EncounterRadius);
			if (!encounter.DisableConservativeTileLoadingChecks)
			{
				flag &= encounter.CheckAtLeastOneTileIsLoadedAtAllChildPositions();
			}
			return flag;
		}
		if (encounter.StartsImmediately)
		{
			return true;
		}
		return Singleton.Manager<ManWorld>.inst.CheckAllTilesAtPositionHaveReachedLoadStep(encounter.Position, encounter.EncounterRadius);
	}

	private void OnTileLoaded(WorldTile tile)
	{
		if (!UpdateEnabled)
		{
			return;
		}
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter encounter = m_ActiveEncounters[i];
			if (!encounter.IsScriptUpdateEnabled && EnoughTilesLoadedForEncounter(encounter))
			{
				encounter.IsScriptUpdateEnabled = true;
			}
		}
	}

	private void OnTileCreated(WorldTile tile)
	{
		if (!UpdateEnabled)
		{
			return;
		}
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter encounter = m_ActiveEncounters[i];
			if (encounter.IsScriptUpdateEnabled)
			{
				encounter.SpawnSceneryBlockersForTile(tile);
			}
		}
	}

	private void OnTileDestroyed(WorldTile tile)
	{
		if (!UpdateEnabled)
		{
			return;
		}
		for (int i = 0; i < m_ActiveEncounters.Count; i++)
		{
			Encounter encounter = m_ActiveEncounters[i];
			if (encounter.IsScriptUpdateEnabled)
			{
				encounter.RemoveSceneryBlockersForTile(tile);
			}
		}
	}

	private void Awake()
	{
		foreach (GameObject allObject in m_EncounterObjects.GetAllObjects())
		{
			m_EncounterObjectsPrefabs.Add(allObject.name, allObject.transform);
		}
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeExit);
		Singleton.Manager<ManWorld>.inst.TileManager.TileCreatedEvent.Subscribe(OnTileCreated);
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Subscribe(OnTileLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.TileDestroyedEvent.Subscribe(OnTileDestroyed);
		SetUpdateEnabled(UpdateChannel.GameMode, enabled: false);
	}

	private void Update()
	{
		if (UpdateEnabled)
		{
			PostChallengeDestructionDelay.StaticUpdateDelayedDestructionObjects();
		}
	}
}
