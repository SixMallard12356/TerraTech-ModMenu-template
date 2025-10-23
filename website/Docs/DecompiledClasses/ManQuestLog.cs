#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManQuestLog : Singleton.Manager<ManQuestLog>, Mode.IManagerModeEvents
{
	private class EncounterLogEntry
	{
		public EncounterIdentifier m_Id;

		public Encounter m_Encounter;

		public ChallengeTimer m_MissionTimer = new ChallengeTimer();

		public EncounterDisplayData m_DisplayData { private get; set; }

		public float MissionTimerTimeElapsed => m_MissionTimer.TimeElapsed;

		public float MissionTimerDisplayTime => m_MissionTimer.DisplayTime;

		public EncounterDisplayData GetEncounterDisplayData()
		{
			if (m_DisplayData != null)
			{
				return m_DisplayData;
			}
			if (m_Encounter != null)
			{
				m_DisplayData = EncounterDisplayData.CreateFromLiveEncounter(m_Encounter);
				return m_DisplayData;
			}
			return null;
		}

		public void UpdateMissionTimer()
		{
			if (!Singleton.Manager<ManPauseGame>.inst.IsPaused)
			{
				m_MissionTimer.Update();
			}
		}

		public void HandleMessage(ref ChallengeTimerMessage msg)
		{
			m_MissionTimer.HandleMessage(ref msg);
		}

		public void FillMissionTimerMessage(ref ChallengeTimerMessage msg)
		{
			m_MissionTimer.FillMessage(ref msg);
		}
	}

	private class SaveData
	{
		public bool m_QuestLogAvailable;

		public List<EncounterIdentifier> m_RecentEncounterUpdatesIdentifiers;
	}

	public Event<EncounterDisplayData, bool> OnEncounterAdded;

	public Event<EncounterIdentifier, ManEncounter.FinishState> OnEncounterRemoved;

	public EventNoParams OnTrackedEncounterChanged;

	public Event<EncounterIdentifier, bool> OnRecentEncountersUpdated;

	public Event<string> OnMissionNotification;

	private SaveData m_SaveData = new SaveData();

	private EncounterLogEntry m_TrackedEncounter;

	private List<EncounterLogEntry> m_LogLookup = new List<EncounterLogEntry>();

	private List<EncounterIdentifier> m_RecentlyUpdatedEncounters = new List<EncounterIdentifier>();

	private Dictionary<int, EncounterIdentifier> m_WaypointToEncounterDefLookup = new Dictionary<int, EncounterIdentifier>();

	private readonly Color k_ChatLogMissionColour = Color.yellow;

	public bool DebugPauseMissionTimer;

	public bool HasTrackedEncounter => m_TrackedEncounter != null;

	public int NumEncountersInLog => m_LogLookup.Count;

	public int NumRecentEncounterUpdates => m_RecentlyUpdatedEncounters.Count;

	public bool QuestLogAvailable => m_SaveData.m_QuestLogAvailable;

	public EncounterIdentifier TrackedEncounterId
	{
		get
		{
			if (m_TrackedEncounter == null)
			{
				return EncounterIdentifier.Invalid;
			}
			return m_TrackedEncounter.m_Id;
		}
	}

	public UICheckpointChallengeHUD ChallengeHUD => Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.CheckpointChallenge) as UICheckpointChallengeHUD;

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		Init();
		if (optionalLoadState != null)
		{
			optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManQuestLog, out m_SaveData);
			Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeStart);
		}
		if (m_SaveData == null)
		{
			m_SaveData = new SaveData();
		}
		Singleton.Manager<ManEncounter>.inst.ClientEncounterStartedEvent.Subscribe(OnClientMissionAdded);
		Singleton.Manager<ManEncounter>.inst.ClientEncounterFinishedEvent.Subscribe(OnClientMissionFinished);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.QuestLogDataUpdated, OnClientQuestLogDataUpdated);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.EncounterCancelRequest, OnServerMissionCancelRequest);
	}

	private void OnModeStart(Mode mode)
	{
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Unsubscribe(OnModeStart);
		if (m_SaveData != null && m_SaveData.m_QuestLogAvailable)
		{
			SetQuestLogAvailable();
		}
	}

	public void Save(ManSaveGame.State saveState)
	{
		m_SaveData.m_RecentEncounterUpdatesIdentifiers = new List<EncounterIdentifier>(m_RecentlyUpdatedEncounters);
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManQuestLog, m_SaveData);
	}

	public void ModeExit()
	{
		m_SaveData = new SaveData();
		Singleton.Manager<ManEncounter>.inst.ClientEncounterStartedEvent.Unsubscribe(OnClientMissionAdded);
		Singleton.Manager<ManEncounter>.inst.ClientEncounterFinishedEvent.Unsubscribe(OnClientMissionFinished);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.QuestLogDataUpdated, OnClientQuestLogDataUpdated);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromServerMessage(TTMsgType.EncounterCancelRequest, OnServerMissionCancelRequest);
		m_TrackedEncounter = null;
		m_LogLookup.Clear();
		m_RecentlyUpdatedEncounters.Clear();
		m_WaypointToEncounterDefLookup.Clear();
	}

	public void Init()
	{
		m_TrackedEncounter = null;
		m_LogLookup = new List<EncounterLogEntry>();
	}

	public void Reset()
	{
		m_SaveData = new SaveData();
	}

	public void Update()
	{
		if (m_SaveData.m_QuestLogAvailable && Singleton.Manager<ManUI>.inst.IsStackEmpty() && Singleton.Manager<ManInput>.inst.GetButtonDown(29))
		{
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.MissionLog))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.MissionLog);
			}
			else if (Singleton.Manager<ManHUD>.inst.CheckShowActionAllowed(ManHUD.HUDElementType.MissionLog, UIHUD.ShowAction.Show) && !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
			{
				UIMissionLogFullHUD.MissionLogShowContext missionLogShowContext = new UIMissionLogFullHUD.MissionLogShowContext
				{
					encounterDisplayData = GetTrackedEncounterDisplayData()
				};
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MissionLog, missionLogShowContext);
			}
		}
		foreach (EncounterLogEntry item in m_LogLookup)
		{
			EncounterDisplayData encounterDisplayData = item.GetEncounterDisplayData();
			if (encounterDisplayData != null && ManNetwork.IsNetworked && !ManNetwork.IsHost)
			{
				encounterDisplayData.UpdateClientEncounterTimer();
			}
			item.UpdateMissionTimer();
		}
	}

	public EncounterDisplayData GetTrackedEncounterDisplayData()
	{
		return m_TrackedEncounter?.GetEncounterDisplayData();
	}

	public void AddWaypointToEncounterLookup(int waypointHostID, EncounterIdentifier encounterDef)
	{
		d.AssertFormat(!m_WaypointToEncounterDefLookup.TryGetValue(waypointHostID, out var value), "Already had waypointID {0} in waypointToEncounter; mapped to {1}", waypointHostID, value.ToString());
		m_WaypointToEncounterDefLookup[waypointHostID] = encounterDef;
	}

	public void RemoveWaypointToEncounterLookup(int waypointHostID)
	{
		m_WaypointToEncounterDefLookup.Remove(waypointHostID);
	}

	public bool TryGetEncounterIdentifier(int waypointHostID, out EncounterIdentifier encounterID)
	{
		return m_WaypointToEncounterDefLookup.TryGetValue(waypointHostID, out encounterID);
	}

	public EncounterDisplayData GetEncounterDisplayData(EncounterIdentifier encounterId)
	{
		foreach (EncounterLogEntry item in m_LogLookup)
		{
			if (item.m_Id == encounterId)
			{
				return item.GetEncounterDisplayData();
			}
		}
		return null;
	}

	public void SetQuestLogAvailable()
	{
		m_SaveData.m_QuestLogAvailable = true;
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.HUDMissionTracker);
	}

	private EncounterLogEntry GetLogEntry(EncounterIdentifier id)
	{
		foreach (EncounterLogEntry item in m_LogLookup)
		{
			if (item.m_Id == id)
			{
				return item;
			}
		}
		return null;
	}

	private EncounterLogEntry GetLogEntry(Encounter encounter)
	{
		foreach (EncounterLogEntry item in m_LogLookup)
		{
			if (item.m_Encounter == encounter)
			{
				return item;
			}
		}
		return null;
	}

	public void AddLog(Encounter encounter, NetPlayer fromPlayer, bool restoredFromSaveData)
	{
		SetQuestLogAvailable();
		if (GetLogEntry(encounter) == null)
		{
			encounter.ShowInQuestLog();
			EncounterLogEntry logEntry = new EncounterLogEntry
			{
				m_Id = encounter.EncounterDef,
				m_Encounter = encounter
			};
			AddLog(logEntry, fromPlayer, restoredFromSaveData);
			encounter.QuestLog.LogDetailsUpdated.Subscribe(AddRecentlyUpdatedEncounter);
		}
	}

	private void AddLog(EncounterLogEntry logEntry, NetPlayer fromPlayer, bool restoredFromSaveData)
	{
		m_LogLookup.Add(logEntry);
		if (m_TrackedEncounter == null)
		{
			SetTrackedEncounter(logEntry);
		}
		EncounterDisplayData encounterDisplayData = logEntry.GetEncounterDisplayData();
		OnEncounterAdded.Send(encounterDisplayData, restoredFromSaveData);
		if (restoredFromSaveData)
		{
			TryRestoreRecentlyUpdatedEncounter(logEntry.m_Id);
			return;
		}
		AddRecentlyUpdatedEncounter(logEntry.m_Id);
		if (OnMissionNotification.HasSubscribers() && encounterDisplayData.ShowMessagesInChat(onAdd: true))
		{
			string arg = (fromPlayer ? fromPlayer.GetColouredName() : "");
			string arg2 = ColourConverter.RecolourRichText(k_ChatLogMissionColour, encounterDisplayData.Title);
			string paramA = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MissionLog.ChatStartMission), arg2, arg);
			OnMissionNotification.Send(paramA);
		}
	}

	public void RemoveLog(Encounter encounter, ManEncounter.FinishState state, NetPlayer fromPlayer)
	{
		if (RemoveLog(GetLogEntry(encounter), state, fromPlayer))
		{
			encounter.QuestLog.LogDetailsUpdated.Unsubscribe(AddRecentlyUpdatedEncounter);
		}
	}

	private bool RemoveLog(EncounterLogEntry logEntry, ManEncounter.FinishState state, NetPlayer fromPlayer)
	{
		bool result = false;
		int num = m_LogLookup.IndexOf(logEntry);
		if (logEntry != null && num >= 0)
		{
			result = true;
			m_LogLookup.RemoveAt(num);
			if (OnMissionNotification.HasSubscribers() && state != ManEncounter.FinishState.None)
			{
				EncounterDisplayData encounterDisplayData = logEntry.GetEncounterDisplayData();
				string text = null;
				string arg = ColourConverter.RecolourRichText(k_ChatLogMissionColour, encounterDisplayData.Title);
				switch (state)
				{
				case ManEncounter.FinishState.Completed:
					text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MissionLog.ChatCompleteMission), arg);
					if (encounterDisplayData.AwardPlayerBB || encounterDisplayData.AwardPlayerXP)
					{
						string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MissionLog.ChatCompleteMissionReward);
						text += string.Format(localisedString, encounterDisplayData.AwardXPAmount, StringLookup.GetCorporationName(encounterDisplayData.AwardXPCorp), encounterDisplayData.AwardBBAmount);
					}
					break;
				case ManEncounter.FinishState.Cancelled:
				{
					string arg2 = (fromPlayer ? fromPlayer.GetColouredName() : "");
					text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MissionLog.ChatCancelMission), arg, arg2);
					break;
				}
				case ManEncounter.FinishState.Failed:
					text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MissionLog.ChatFailMission), arg);
					break;
				}
				if (text != null && encounterDisplayData.ShowMessagesInChat(onAdd: false))
				{
					OnMissionNotification.Send(text);
				}
			}
			if (logEntry == m_TrackedEncounter)
			{
				EncounterLogEntry trackedEncounter = null;
				if (m_LogLookup.Count > 0)
				{
					int index = Mathf.Clamp(num, 0, m_LogLookup.Count - 1);
					trackedEncounter = m_LogLookup[index];
				}
				SetTrackedEncounter(trackedEncounter);
			}
			ClearRecentlyUpdatedEncounter(logEntry.m_Id);
			OnEncounterRemoved.Send(logEntry.m_Id, state);
		}
		return result;
	}

	public void SetTrackedEncounter(Encounter encounter)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounter);
		if (logEntry != null)
		{
			SetTrackedEncounter(logEntry);
		}
	}

	private void SetTrackedEncounter(EncounterLogEntry encounter)
	{
		if (encounter == m_TrackedEncounter)
		{
			return;
		}
		if (m_TrackedEncounter != null && m_TrackedEncounter.m_Encounter != null)
		{
			m_TrackedEncounter.m_Encounter.SetActiveEncounter(active: false);
		}
		m_TrackedEncounter = encounter;
		if (m_TrackedEncounter != null && m_TrackedEncounter.m_Encounter != null)
		{
			m_TrackedEncounter.m_Encounter.SetActiveEncounter(active: true);
		}
		OnTrackedEncounterChanged.Send();
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && !Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			QuestLogData questLogData = m_TrackedEncounter?.GetEncounterDisplayData().ActiveQuestLog;
			if (questLogData != null)
			{
				TrackedVisible encounterWaypoint = questLogData.GetEncounterWaypoint();
				Singleton.Manager<ManEncounter>.inst.ConfigureNavigationOverlay(encounterWaypoint);
			}
		}
	}

	public void SetTrackedEncounter(EncounterIdentifier encounterId)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterId);
		if (logEntry != null)
		{
			SetTrackedEncounter(logEntry);
		}
	}

	public void ClearRecentlyUpdatedEncounter(EncounterIdentifier encounterId)
	{
		if (m_RecentlyUpdatedEncounters.Remove(encounterId))
		{
			OnRecentEncountersUpdated.Send(encounterId, paramB: false);
		}
	}

	public void CancelEncounter(EncounterIdentifier encounterId)
	{
		NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
		if (ManNetwork.IsHost)
		{
			EncounterLogEntry logEntry = GetLogEntry(encounterId);
			if (logEntry != null && (bool)logEntry.m_Encounter)
			{
				Singleton.Manager<ManEncounter>.inst.CancelEncounter(logEntry.m_Encounter, myPlayer);
			}
			return;
		}
		EncounterFinishedMessage encounterFinishedMessage = new EncounterFinishedMessage
		{
			m_Id = encounterId,
			m_State = ManEncounter.FinishState.Cancelled
		};
		if ((bool)myPlayer)
		{
			encounterFinishedMessage.m_PlayerNetID = myPlayer.netId;
		}
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.EncounterCancelRequest, encounterFinishedMessage);
	}

	public void DebugCompleteActiveEncounter()
	{
		if (m_TrackedEncounter != null && (bool)m_TrackedEncounter.m_Encounter)
		{
			Singleton.Manager<ManEncounter>.inst.FinishEncounter(m_TrackedEncounter.m_Encounter, ManEncounter.FinishState.Completed);
		}
	}

	public void DebugTeleportToActiveEncounter()
	{
		EncounterDisplayData trackedEncounterDisplayData = GetTrackedEncounterDisplayData();
		if (Singleton.playerTank != null && trackedEncounterDisplayData != null && trackedEncounterDisplayData.HasPosition)
		{
			Vector3 scenePosition = trackedEncounterDisplayData.ScenePosition;
			float num = trackedEncounterDisplayData.Encounter.EncounterRadius + 50f;
			Vector3 normalized = (Singleton.playerPos - scenePosition).normalized;
			Vector3 vector = Singleton.cameraTrans.position - Singleton.playerTank.boundsCentreWorld;
			Singleton.playerTank.visible.Teleport(scenePosition + normalized * num, Singleton.playerTank.trans.rotation);
			Vector3 vector2 = (Singleton.playerTank ? Singleton.playerTank.boundsCentreWorld : Singleton.playerPos);
			Singleton.Manager<CameraManager>.inst.ResetCamera(vector2 + vector, Singleton.cameraTrans.rotation);
		}
	}

	public void SetCheckpointChallengeHUDToUseMissionTimer(EncounterIdentifier encounterIdentifier)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterIdentifier);
		if (logEntry != null)
		{
			logEntry.m_MissionTimer.AllowNegativeDisplayTime = false;
			ChallengeHUD.Init();
		}
	}

	public void StartMissionTimer(EncounterIdentifier encounterDef, float startTimeRemaining = 0f)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterDef);
		if (logEntry != null && !logEntry.m_MissionTimer.IsRunningSet)
		{
			logEntry.m_MissionTimer.AllowNegativeDisplayTime = false;
			logEntry.m_MissionTimer.Start(startTimeRemaining);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ChallengeTimerMessage msg = new ChallengeTimerMessage();
				FillMissionTimerMessage(ref msg, encounterDef);
				msg.m_IsShowBestTimeUI = false;
				msg.m_IsShowUI = true;
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
			}
		}
	}

	public void StopMissionTimer(EncounterIdentifier encounterDef)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterDef);
		if (logEntry != null && logEntry.m_MissionTimer.IsRunningSet)
		{
			logEntry.m_MissionTimer.Stop();
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ChallengeTimerMessage msg = new ChallengeTimerMessage();
				FillMissionTimerMessage(ref msg, encounterDef);
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
			}
		}
	}

	public void RestartMissionTimer(EncounterIdentifier encounterDef)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterDef);
		if (logEntry != null)
		{
			logEntry.m_MissionTimer.Restart();
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ChallengeTimerMessage msg = new ChallengeTimerMessage();
				FillMissionTimerMessage(ref msg, encounterDef);
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
			}
		}
	}

	public void ResetMissionTimer(EncounterIdentifier encounterDef)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterDef);
		if (logEntry != null)
		{
			logEntry.m_MissionTimer.Reset();
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ChallengeTimerMessage msg = new ChallengeTimerMessage();
				FillMissionTimerMessage(ref msg, encounterDef);
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
			}
		}
	}

	public void ResetMissionTimerTimeElapsed(EncounterIdentifier encounterDef, float startTime)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterDef);
		if (logEntry != null)
		{
			logEntry.m_MissionTimer.ResetTimeElapsed(startTime);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ChallengeTimerMessage msg = new ChallengeTimerMessage();
				FillMissionTimerMessage(ref msg, encounterDef);
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
			}
		}
	}

	public float GetMissionTimerDisplayTime(EncounterIdentifier encounterDef)
	{
		return GetLogEntry(encounterDef)?.MissionTimerDisplayTime ?? 0f;
	}

	public void ShowMissionTimerUI(EncounterIdentifier encounterDef, bool showBestTime = false)
	{
		SetCheckpointChallengeHUDToUseMissionTimer(encounterDef);
		if (!showBestTime)
		{
			ChallengeHUD.SetBestTimeText("");
		}
		EncounterLogEntry logEntry = GetLogEntry(encounterDef);
		if (logEntry != null)
		{
			ChallengeHUD.SetChallengeTimer(logEntry.m_MissionTimer);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				ChallengeTimerMessage msg = new ChallengeTimerMessage();
				FillMissionTimerMessage(ref msg, encounterDef);
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
			}
		}
	}

	public void HideMissionTimerUI(EncounterIdentifier encounterDef)
	{
		ChallengeHUD.SetChallengeTimer(null);
		if (GetLogEntry(encounterDef) != null && Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			ChallengeTimerMessage msg = new ChallengeTimerMessage();
			FillMissionTimerMessage(ref msg, encounterDef);
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChallengeTimerChange, msg);
		}
	}

	public void HandleMissionTimerMessage(ChallengeTimerMessage msg)
	{
		EncounterLogEntry logEntry = GetLogEntry(msg.m_EncounterId);
		if (logEntry == null)
		{
			return;
		}
		bool flag = Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.CheckpointChallenge);
		if (msg.m_IsShowUI != flag)
		{
			if (msg.m_IsShowUI)
			{
				Singleton.Manager<ManQuestLog>.inst.SetCheckpointChallengeHUDToUseMissionTimer(msg.m_EncounterId);
				ChallengeHUD.SetChallengeTimer(logEntry.m_MissionTimer);
			}
			else
			{
				ChallengeHUD.SetChallengeTimer(null);
			}
		}
		if (msg.m_IsShowBestTimeUI != ChallengeHUD.HasBestTimeText)
		{
			ChallengeHUD.SetBestTimeText(msg.m_IsShowBestTimeUI ? CheckpointChallenge.ConvertTimeToScoreString(msg.m_BestTime) : "");
		}
		logEntry.HandleMessage(ref msg);
	}

	public void FillMissionTimerMessage(ref ChallengeTimerMessage msg, EncounterIdentifier encounterId)
	{
		EncounterLogEntry logEntry = GetLogEntry(encounterId);
		if (logEntry != null)
		{
			msg.m_EncounterId = encounterId;
			msg.m_IsShowUI = Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.CheckpointChallenge);
			msg.m_IsShowBestTimeUI = ChallengeHUD.HasBestTimeText;
			logEntry.FillMissionTimerMessage(ref msg);
		}
	}

	private void TryRestoreRecentlyUpdatedEncounter(EncounterIdentifier encounterId)
	{
		if (m_SaveData.m_RecentEncounterUpdatesIdentifiers == null || m_SaveData.m_RecentEncounterUpdatesIdentifiers.Count <= 0)
		{
			return;
		}
		for (int i = 0; i < m_SaveData.m_RecentEncounterUpdatesIdentifiers.Count; i++)
		{
			if (encounterId == m_SaveData.m_RecentEncounterUpdatesIdentifiers[i])
			{
				m_RecentlyUpdatedEncounters.Add(encounterId);
				m_SaveData.m_RecentEncounterUpdatesIdentifiers.RemoveAt(i);
				OnRecentEncountersUpdated.Send(encounterId, paramB: true);
				break;
			}
		}
	}

	private void AddRecentlyUpdatedEncounter(EncounterIdentifier encounter)
	{
		if (!m_RecentlyUpdatedEncounters.Contains(encounter) && encounter != TrackedEncounterId)
		{
			m_RecentlyUpdatedEncounters.Add(encounter);
			OnRecentEncountersUpdated.Send(encounter, paramB: true);
		}
	}

	private void OnClientMissionAdded(EncounterToSpawn spawn, NetPlayer addingPlayer, bool setTracked)
	{
		if (!ManNetwork.IsHost)
		{
			spawn.m_EncounterData = Singleton.Manager<ManEncounter>.inst.GetEncounterData(spawn.m_EncounterDef);
			if (spawn.m_EncounterData != null)
			{
				AddClientMissionView(spawn, addingPlayer, setTracked);
			}
		}
	}

	private void AddClientMissionView(EncounterToSpawn spawn, NetPlayer fromPlayer, bool setTracked)
	{
		SetQuestLogAvailable();
		EncounterIdentifier encounterDef = spawn.m_EncounterDef;
		EncounterDisplayData displayData = EncounterDisplayData.CreateClientEncounterView(spawn);
		EncounterLogEntry encounterLogEntry = new EncounterLogEntry
		{
			m_Id = encounterDef,
			m_DisplayData = displayData
		};
		AddLog(encounterLogEntry, fromPlayer, restoredFromSaveData: false);
		if (setTracked)
		{
			SetTrackedEncounter(encounterLogEntry);
		}
	}

	private void OnClientMissionFinished(EncounterIdentifier id, ManEncounter.FinishState state, NetPlayer fromPlayer)
	{
		if (ManNetwork.IsHost)
		{
			return;
		}
		EncounterLogEntry logEntry = GetLogEntry(id);
		if (logEntry != null)
		{
			EncounterDisplayData encounterDisplayData = logEntry.GetEncounterDisplayData();
			if (encounterDisplayData != null && encounterDisplayData.ActiveQuestLog != null && encounterDisplayData.ActiveQuestLog.EncounterWaypointHostID != -1)
			{
				RemoveWaypointToEncounterLookup(encounterDisplayData.ActiveQuestLog.EncounterWaypointHostID);
			}
			RemoveLog(logEntry, state, fromPlayer);
		}
	}

	private void OnClientQuestLogDataUpdated(NetworkMessage netMsg)
	{
		EncounterUpdateMessage encounterUpdateMessage = netMsg.ReadMessage<EncounterUpdateMessage>();
		EncounterLogEntry logEntry = GetLogEntry(encounterUpdateMessage.m_Id);
		if (logEntry != null)
		{
			EncounterDisplayData encounterDisplayData = logEntry.GetEncounterDisplayData();
			if (encounterDisplayData != null && encounterDisplayData.ActiveQuestLog != null)
			{
				encounterDisplayData.ActiveQuestLog.UpdateFromMessage(encounterUpdateMessage);
				AddRecentlyUpdatedEncounter(logEntry.m_Id);
			}
		}
		else
		{
			d.LogError($"OnClientQuestLogDataUpdated - Discarding EncounterUpdateMessage - could not find LogEntry for msg ID {encounterUpdateMessage.m_Id}");
		}
	}

	private void OnServerMissionCancelRequest(NetworkMessage netMsg)
	{
		EncounterFinishedMessage encounterFinishedMessage = netMsg.ReadMessage<EncounterFinishedMessage>();
		EncounterLogEntry logEntry = GetLogEntry(encounterFinishedMessage.m_Id);
		if (logEntry != null && (bool)logEntry.m_Encounter)
		{
			Singleton.Manager<ManEncounter>.inst.CancelEncounter(logEntry.m_Encounter, Singleton.Manager<ManNetwork>.inst.FindPlayerById(encounterFinishedMessage.m_PlayerNetID.Value));
		}
	}
}
