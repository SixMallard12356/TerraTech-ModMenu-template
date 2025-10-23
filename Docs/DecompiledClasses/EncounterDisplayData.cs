#define UNITY_EDITOR
using System;
using UnityEngine;

public class EncounterDisplayData : IEquatable<EncounterDisplayData>
{
	private bool m_HasLiveData;

	private Encounter m_Encounter;

	private EncounterIdentifier m_Identifier = EncounterIdentifier.Invalid;

	private EncounterDetails m_EncounterReadonlyDetails;

	private int m_EncounterStringBankIdx;

	private QuestLogData m_ActiveQuestLog;

	private EncounterToSpawn m_EncounterToSpawn;

	private bool m_EncounterHasPosition;

	private WorldPosition m_EncounterPosition;

	private QuestLogData.EncounterObjective[] m_EncounterObjectives;

	private bool m_CanBeCancelled;

	public bool IsLiveEncounter => m_HasLiveData;

	public string Title => m_EncounterReadonlyDetails.TitleString(m_EncounterStringBankIdx);

	public string FullDescription => m_EncounterReadonlyDetails.FullDescriptionString(m_EncounterStringBankIdx);

	public FactionSubTypes Corp => m_Identifier.m_Corp;

	public int Grade => m_Identifier.m_Grade;

	public string Category => m_Identifier.m_Category;

	public bool HasPosition
	{
		get
		{
			if (!m_HasLiveData)
			{
				return m_EncounterHasPosition;
			}
			return !m_Encounter.HasNoPosition;
		}
	}

	public Vector3 ScenePosition
	{
		get
		{
			if (!m_HasLiveData)
			{
				return m_EncounterPosition.ScenePosition;
			}
			return m_Encounter.Position;
		}
	}

	public int NumObjectives => m_EncounterReadonlyDetails.NumObjectives;

	public QuestLogData.EncounterObjective[] Objectives
	{
		get
		{
			if (m_ActiveQuestLog == null)
			{
				return m_EncounterObjectives;
			}
			return m_ActiveQuestLog.InternalObjectives;
		}
	}

	public QuestLogData.EncounterObjective ActiveObjective
	{
		get
		{
			if (m_ActiveQuestLog == null)
			{
				return null;
			}
			return m_ActiveQuestLog.ActiveObjective;
		}
	}

	public bool IsTimed
	{
		get
		{
			if (m_ActiveQuestLog == null)
			{
				return m_EncounterReadonlyDetails.IsTimed;
			}
			return m_ActiveQuestLog.HasVisibleTimer;
		}
	}

	public float EncounterTime
	{
		get
		{
			if (m_ActiveQuestLog == null)
			{
				return m_EncounterReadonlyDetails.EncounterTime;
			}
			return m_ActiveQuestLog.EncounterTimer.TimeRemaining + 1f;
		}
	}

	public float EncounterTotalTime
	{
		get
		{
			if (m_ActiveQuestLog == null)
			{
				return m_EncounterReadonlyDetails.EncounterTime;
			}
			return m_ActiveQuestLog.EncounterTimer.TotalCountdownTime;
		}
	}

	public bool AwardPlayerXP => m_EncounterReadonlyDetails.AwardXP;

	public FactionSubTypes AwardXPCorp => m_EncounterReadonlyDetails.XPCorp;

	public int AwardXPAmount => m_EncounterReadonlyDetails.XPAmount;

	public bool AwardPlayerBB => m_EncounterReadonlyDetails.AwardBB;

	public int AwardBBAmount => m_EncounterReadonlyDetails.BBAmount;

	public bool AwardPlayerBlocks => m_EncounterReadonlyDetails.AwardBlocks;

	public BlockTypes[] BlocksToAward => m_EncounterReadonlyDetails.BlocksToAward;

	public bool RewardFromCorpPool => m_EncounterReadonlyDetails.RewardBlocksFromCorpPool;

	public FactionSubTypes RewardPoolCorp => m_EncounterReadonlyDetails.RewardPoolCorp;

	public int AmountToAwardFromPool => m_EncounterReadonlyDetails.AmountToAwardFromPool;

	public bool HideBlockReward => m_EncounterReadonlyDetails.HideBlockReward;

	public bool AwardPlayerTech => m_EncounterReadonlyDetails.AwardTech;

	public TechData AwardTechData => m_EncounterReadonlyDetails.TechToAward;

	public bool AwardLicense => m_EncounterReadonlyDetails.AwardLicense;

	public FactionSubTypes CorpLicenseToAward => m_EncounterReadonlyDetails.CorpLicenseToAward;

	public bool CanBeCancelled
	{
		get
		{
			if (!IsLiveEncounter)
			{
				return m_CanBeCancelled;
			}
			return m_Encounter.CanBeCancelled;
		}
	}

	public Encounter Encounter => m_Encounter;

	public EncounterIdentifier Identifier => m_Identifier;

	public QuestLogData ActiveQuestLog => m_ActiveQuestLog;

	public EncounterToSpawn EncounterToSpawn => m_EncounterToSpawn;

	public bool ShowMessagesInChat(bool onAdd)
	{
		return m_EncounterReadonlyDetails.ShowMessagesInChat(onAdd);
	}

	public void UpdateClientEncounterTimer()
	{
		if (ManNetwork.IsNetworked && !ManNetwork.IsHost)
		{
			if (IsTimed)
			{
				m_ActiveQuestLog.EncounterTimer.UpdateTimer();
			}
		}
		else
		{
			d.LogError("UpdateClientEncounterTimer called on something that isn't a client in a networked game!");
		}
	}

	public static EncounterDisplayData CreateFromLiveEncounter(Encounter encounter)
	{
		d.Assert(encounter != null && encounter.EncounterDetails != null, "Trying to display a null Encounter or encounter without EncounterDetails! Not supported!");
		EncounterDisplayData result = null;
		if (encounter != null)
		{
			result = new EncounterDisplayData
			{
				m_HasLiveData = true,
				m_Encounter = encounter,
				m_Identifier = encounter.EncounterDef,
				m_EncounterReadonlyDetails = encounter.EncounterDetails,
				m_EncounterStringBankIdx = encounter.EncounterStringBankIdx,
				m_ActiveQuestLog = encounter.QuestLog
			};
		}
		return result;
	}

	public static EncounterDisplayData CreateFromUnspawnedEncounter(EncounterToSpawn encounterToSpawn)
	{
		d.Assert(encounterToSpawn.m_EncounterData.m_EncounterPrefab.EncounterDetails != null, "Trying to display Encounter without EncounterDetails! No longer supported!");
		Encounter activeEncounter = Singleton.Manager<ManEncounter>.inst.GetActiveEncounter(encounterToSpawn.m_EncounterDef);
		if (activeEncounter.IsNotNull())
		{
			d.Assert(condition: false, "Probably not what we want to do");
			return CreateFromLiveEncounter(activeEncounter);
		}
		EncounterDetails encounterDetails = encounterToSpawn.m_EncounterData.m_EncounterPrefab.EncounterDetails;
		QuestLogData.EncounterObjective[] array = new QuestLogData.EncounterObjective[encounterDetails.NumObjectives];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new QuestLogData.EncounterObjective(encounterDetails, encounterToSpawn.m_EncounterStringBankIdx, i);
		}
		return new EncounterDisplayData
		{
			m_HasLiveData = false,
			m_Encounter = encounterToSpawn.m_EncounterData.m_EncounterPrefab,
			m_Identifier = encounterToSpawn.m_EncounterDef,
			m_EncounterReadonlyDetails = encounterDetails,
			m_EncounterStringBankIdx = encounterToSpawn.m_EncounterStringBankIdx,
			m_EncounterToSpawn = encounterToSpawn,
			m_EncounterHasPosition = !encounterToSpawn.m_EncounterData.m_HasNoPosition,
			m_EncounterPosition = encounterToSpawn.m_Position,
			m_EncounterObjectives = array,
			m_CanBeCancelled = encounterToSpawn.m_EncounterData.m_CanBeCancelled
		};
	}

	public static EncounterDisplayData CreateClientEncounterView(EncounterToSpawn spawn)
	{
		EncounterDisplayData encounterDisplayData = CreateFromUnspawnedEncounter(spawn);
		encounterDisplayData.m_ActiveQuestLog = new QuestLogData(spawn.m_EncounterDef, spawn.m_EncounterData.m_EncounterPrefab.EncounterDetails, spawn.m_EncounterStringBankIdx);
		return encounterDisplayData;
	}

	public bool Equals(EncounterDisplayData other)
	{
		if (other != null && m_Identifier == other.m_Identifier && m_HasLiveData == other.m_HasLiveData && m_EncounterStringBankIdx == other.m_EncounterStringBankIdx)
		{
			if (!m_HasLiveData || !(m_Encounter == other.m_Encounter))
			{
				if (!m_HasLiveData)
				{
					return m_EncounterPosition == other.m_EncounterPosition;
				}
				return false;
			}
			return true;
		}
		return false;
	}

	public override bool Equals(object other)
	{
		if (!(other is EncounterDisplayData))
		{
			return false;
		}
		return Equals((EncounterDisplayData)other);
	}

	public override int GetHashCode()
	{
		int num = m_Identifier.GetHashCode() ^ (m_HasLiveData ? 1 : 0);
		if (!m_HasLiveData)
		{
			num ^= m_EncounterPosition.GetHashCode();
		}
		return num;
	}

	public static bool operator ==(EncounterDisplayData a, EncounterDisplayData b)
	{
		if ((object)a == b)
		{
			return true;
		}
		if ((object)a == null || (object)b == null)
		{
			return false;
		}
		return a.Equals(b);
	}

	public static bool operator !=(EncounterDisplayData a, EncounterDisplayData b)
	{
		return !(a == b);
	}
}
