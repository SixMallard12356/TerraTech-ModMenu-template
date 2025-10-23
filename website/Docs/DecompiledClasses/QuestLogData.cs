#define UNITY_EDITOR
public class QuestLogData
{
	public class EncounterObjective
	{
		private readonly EncounterDetails m_EncounterDetails;

		private readonly int m_EncounterStringBankIdx;

		private readonly int m_ObjectiveIndex;

		public string Description => m_EncounterDetails.ObjectiveDescription(m_ObjectiveIndex, m_EncounterStringBankIdx);

		public int TargetCount => m_EncounterDetails.ObjectiveTargetCount(m_ObjectiveIndex);

		public bool IsVisible { get; set; }

		public bool ShowCount { get; set; }

		public int CurrentCount { get; set; }

		public bool IsCompleted { get; set; }

		public EncounterObjective(EncounterDetails encounterDetails, int encounterBankIdx, int objectiveIndex)
		{
			m_EncounterDetails = encounterDetails;
			m_EncounterStringBankIdx = encounterBankIdx;
			m_ObjectiveIndex = objectiveIndex;
			IsVisible = m_EncounterDetails.IsObjectiveVisibleByDefault(m_ObjectiveIndex);
			ShowCount = false;
			CurrentCount = 0;
			IsCompleted = false;
		}
	}

	public Event<EncounterIdentifier> LogDetailsUpdated;

	private EncounterObjective[] m_Objectives;

	private int m_ActiveObjectiveIdx = -1;

	private EncounterTimer m_EncounterTimer;

	private int m_EncounterWaypointHostID = -1;

	private EncounterIdentifier m_EncounterId;

	private EncounterDetails m_EncounterDetails;

	public EncounterObjective ActiveObjective
	{
		get
		{
			if (m_ActiveObjectiveIdx < 0)
			{
				return null;
			}
			return m_Objectives[m_ActiveObjectiveIdx];
		}
	}

	public int NumObjectives => m_EncounterDetails.NumObjectives;

	public bool HasVisibleTimer
	{
		get
		{
			if (EncounterTimer != null)
			{
				if (!EncounterTimer.IsRunning)
				{
					return EncounterTimer.IsExpired;
				}
				return true;
			}
			return false;
		}
	}

	public EncounterTimer EncounterTimer => m_EncounterTimer;

	public EncounterObjective[] InternalObjectives => m_Objectives;

	public int EncounterWaypointHostID => m_EncounterWaypointHostID;

	public QuestLogData(EncounterIdentifier id, EncounterDetails details, int stringBankIdx)
	{
		m_EncounterId = id;
		m_EncounterDetails = details;
		m_Objectives = new EncounterObjective[NumObjectives];
		for (int i = 0; i < NumObjectives; i++)
		{
			m_Objectives[i] = new EncounterObjective(m_EncounterDetails, stringBankIdx, i);
		}
		UpdateActiveObjective();
	}

	public QuestLogData(Encounter encounter)
		: this(encounter.EncounterDef, encounter.EncounterDetails, encounter.EncounterStringBankIdx)
	{
	}

	public void FillMessage(EncounterUpdateMessage msg)
	{
		msg.m_ActiveObjectiveIdx = m_ActiveObjectiveIdx;
		msg.m_ObjectiveData = new EncounterUpdateMessage.ObjectiveData[m_Objectives.Length];
		for (int i = 0; i < m_Objectives.Length; i++)
		{
			EncounterObjective encounterObjective = m_Objectives[i];
			msg.m_ObjectiveData[i] = new EncounterUpdateMessage.ObjectiveData
			{
				m_Completed = encounterObjective.IsCompleted,
				m_Count = encounterObjective.CurrentCount,
				m_ShowCount = encounterObjective.ShowCount,
				m_Visible = encounterObjective.IsVisible
			};
		}
		msg.m_EncounterTimer = default(EncounterUpdateMessage.EncounterTimerData);
		if (m_EncounterTimer != null)
		{
			m_EncounterTimer.FillEncounterTimerData(out msg.m_EncounterTimer);
		}
		msg.m_EncounterWaypointHostID = m_EncounterWaypointHostID;
	}

	public void UpdateFromMessage(EncounterUpdateMessage msg)
	{
		d.Assert(!ManNetwork.IsHost, "Host is authoritative on missions - should not be calling QuestLogData.UpdateFromMessage");
		d.Assert(msg.m_Id == m_EncounterId);
		if (msg.m_ObjectiveData.Length != m_Objectives.Length)
		{
			d.LogError($"Failed to apply quest log update due to mismatched objective counts server:{msg.m_ObjectiveData.Length} client:{m_Objectives.Length} on mission {m_EncounterDetails.name}");
			return;
		}
		m_ActiveObjectiveIdx = msg.m_ActiveObjectiveIdx;
		for (int i = 0; i < m_Objectives.Length; i++)
		{
			m_Objectives[i].IsVisible = msg.m_ObjectiveData[i].m_Visible;
			m_Objectives[i].IsCompleted = msg.m_ObjectiveData[i].m_Completed;
			m_Objectives[i].ShowCount = msg.m_ObjectiveData[i].m_ShowCount;
			m_Objectives[i].CurrentCount = msg.m_ObjectiveData[i].m_Count;
		}
		if (msg.m_EncounterTimer.m_TotalCountdownTime == 0f)
		{
			m_EncounterTimer = null;
		}
		else
		{
			m_EncounterTimer = new EncounterTimer(msg.m_EncounterTimer);
		}
		int encounterWaypointHostID = m_EncounterWaypointHostID;
		m_EncounterWaypointHostID = msg.m_EncounterWaypointHostID;
		if (encounterWaypointHostID != m_EncounterWaypointHostID)
		{
			if (encounterWaypointHostID != -1)
			{
				Singleton.Manager<ManQuestLog>.inst.RemoveWaypointToEncounterLookup(encounterWaypointHostID);
			}
			if (m_EncounterWaypointHostID != -1)
			{
				Singleton.Manager<ManQuestLog>.inst.AddWaypointToEncounterLookup(m_EncounterWaypointHostID, m_EncounterId);
			}
			TrackedVisible encounterWaypoint = GetEncounterWaypoint();
			Singleton.Manager<ManEncounter>.inst.ConfigureNavigationOverlay(encounterWaypoint);
		}
		LogDetailsUpdated.Send(m_EncounterId);
	}

	public void SetObjectiveCount(int objectiveIndex, int currentCount)
	{
		d.AssertFormat(objectiveIndex >= 0 && objectiveIndex < NumObjectives, "NewQuestLog.SetObjectiveCount - Invalid objectiveIndex passed in. Got {0}, expected [0, {1}]", objectiveIndex, NumObjectives - 1);
		if (objectiveIndex >= 0 && objectiveIndex < NumObjectives)
		{
			m_Objectives[objectiveIndex].CurrentCount = currentCount;
			m_Objectives[objectiveIndex].ShowCount = true;
			LogDetailsUpdated.Send(m_EncounterId);
		}
	}

	public void SetObjectiveCompleted(int objectiveIndex, bool completed = true)
	{
		d.AssertFormat(objectiveIndex >= 0 && objectiveIndex < NumObjectives, "NewQuestLog.SetObjectiveCompleted - Invalid objectiveIndex passed in. Got {0}, expected [0, {1}]", objectiveIndex, NumObjectives - 1);
		if (objectiveIndex >= 0 && objectiveIndex < NumObjectives)
		{
			m_Objectives[objectiveIndex].IsCompleted = completed;
			UpdateActiveObjective();
			LogDetailsUpdated.Send(m_EncounterId);
		}
	}

	public void SetObjectiveVisible(int objectiveIndex, bool visible = true)
	{
		d.AssertFormat(objectiveIndex >= 0 && objectiveIndex < NumObjectives, "NewQuestLog.SetObjectiveVisible - Invalid objectiveIndex passed in. Got {0}, expected [0, {1}]", objectiveIndex, NumObjectives - 1);
		if (objectiveIndex >= 0 && objectiveIndex < NumObjectives)
		{
			m_Objectives[objectiveIndex].IsVisible = visible;
			UpdateActiveObjective();
			LogDetailsUpdated.Send(m_EncounterId);
		}
	}

	public int GetObjectiveTargetCount(int objectiveIndex)
	{
		int result = 0;
		d.AssertFormat(objectiveIndex >= 0 && objectiveIndex < NumObjectives, "NewQuestLog.GetObjectiveTargetCount - Invalid objectiveIndex passed in. Got {0}, expected [0, {1}]", objectiveIndex, NumObjectives - 1);
		if (objectiveIndex >= 0 && objectiveIndex < NumObjectives)
		{
			result = m_Objectives[objectiveIndex].TargetCount;
		}
		return result;
	}

	public bool GetObjectiveCompleted(int objectiveIndex)
	{
		bool result = false;
		d.AssertFormat(objectiveIndex >= 0 && objectiveIndex < NumObjectives, "NewQuestLog.GetObjectiveCompleted - Invalid objectiveIndex passed in. Got {0}, expected [0, {1}]", objectiveIndex, NumObjectives - 1);
		if (objectiveIndex >= 0 && objectiveIndex < NumObjectives)
		{
			result = m_Objectives[objectiveIndex].IsCompleted;
		}
		return result;
	}

	public void StartEncounterTimer(float totalTime)
	{
		d.Assert(m_EncounterTimer == null, "QuestLogData.StartEncounterTimer - Already has a countdown timer! Currently do not support multiple timers per encounter!");
		m_EncounterTimer = new EncounterTimer(totalTime);
		m_EncounterTimer.OnCountdownExpired.Subscribe(OnQuestLogTimerExpired);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(m_EncounterId);
		}
		LogDetailsUpdated.Send(m_EncounterId);
	}

	public void RemoveEncounterTimer()
	{
		d.Assert(m_EncounterTimer != null, "QuestLogData.RemoveEncounterTimer - No countdown timer present on encounter! Could not remove!");
		m_EncounterTimer = null;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(m_EncounterId);
		}
		LogDetailsUpdated.Send(m_EncounterId);
	}

	public void RestoreEncounterTimer(EncounterTimer savedTimer)
	{
		m_EncounterTimer = savedTimer;
		if (m_EncounterTimer != null)
		{
			m_EncounterTimer.OnCountdownExpired.Subscribe(OnQuestLogTimerExpired);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(m_EncounterId);
			}
		}
	}

	public void SetEncounterWaypoint(TrackedVisible waypointTV)
	{
		d.Assert(ManNetwork.IsHost);
		int encounterWaypointHostID = m_EncounterWaypointHostID;
		m_EncounterWaypointHostID = waypointTV?.HostID ?? (-1);
		if (encounterWaypointHostID != m_EncounterWaypointHostID)
		{
			if (encounterWaypointHostID != -1)
			{
				Singleton.Manager<ManQuestLog>.inst.RemoveWaypointToEncounterLookup(encounterWaypointHostID);
			}
			if (m_EncounterWaypointHostID != -1)
			{
				Singleton.Manager<ManQuestLog>.inst.AddWaypointToEncounterLookup(m_EncounterWaypointHostID, m_EncounterId);
			}
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(m_EncounterId);
			}
		}
	}

	public TrackedVisible GetEncounterWaypoint()
	{
		if (m_EncounterWaypointHostID != -1)
		{
			return Singleton.Manager<ManVisible>.inst.GetTrackedVisibleByHostID(m_EncounterWaypointHostID);
		}
		return null;
	}

	private void OnQuestLogTimerExpired()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(m_EncounterId);
		}
		LogDetailsUpdated.Send(m_EncounterId);
	}

	private void UpdateActiveObjective()
	{
		m_ActiveObjectiveIdx = -1;
		for (int i = 0; i < NumObjectives; i++)
		{
			if (m_Objectives[i].IsVisible && !m_Objectives[i].IsCompleted)
			{
				m_ActiveObjectiveIdx = i;
				break;
			}
		}
	}
}
