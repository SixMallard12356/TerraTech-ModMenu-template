#define UNITY_EDITOR
using System;
using UnityEngine;

public class EncounterDetails : MonoBehaviour
{
	[Serializable]
	private struct Objective
	{
		[SerializeField]
		[Tooltip("The text of this objective. Should be a short, single line statement of what to complete.")]
		public string m_DescriptionStringID;

		[Tooltip("Does this objective show up on the log by default (or only after the script tells it to)?")]
		[SerializeField]
		public bool m_ShowByDefault;

		[Tooltip("Optional target count to reach for this objective.")]
		[SerializeField]
		public int m_TargetCount;
	}

	public enum AnalyticsMissionType
	{
		DoNotTrack,
		Tutorial,
		EnemyTechs,
		Ambush,
		DeliveryCrate,
		EnemyBase,
		DefendAlly,
		Harvest,
		StuntRace,
		SetPiece
	}

	[SerializeField]
	[Tooltip("String bank(s) to use for this Encounter")]
	private string[] m_StringBanks = new string[1];

	[SerializeField]
	private string m_StringBank;

	[SerializeField]
	[Tooltip("Title of the encounter in quest log")]
	private string m_TitleStringID;

	[SerializeField]
	[Tooltip("Do we show accept/complete messages in multiplayer chat")]
	private bool m_ShowMessagesInChat = true;

	[SerializeField]
	[Tooltip("Is this one encounter in a string of Encounters for the same Mission")]
	private bool m_IsMultiStageEncounter;

	[SerializeField]
	[Tooltip("Is this the first encounter in a string of Encounters for the same Mission")]
	private bool m_MultiStage_IsFirstEncounter;

	[SerializeField]
	[Tooltip("Is this the last encounter in a string of Encounters for the same Mission")]
	private bool m_MultiStage_IsLastEncounter;

	[SerializeField]
	[Tooltip("Full lore description. Gives context to the encounter and why you're doing it")]
	private string m_FullDescriptionStringID;

	[Tooltip("List of 'sub objectives' in an encounter. List of tasks to complete")]
	[SerializeField]
	private Objective[] m_Objectives;

	[Tooltip("Does the encounter have a timed component?")]
	[SerializeField]
	private bool m_IsTimed;

	[Tooltip("Total time to (initially) countdown from.")]
	[SerializeField]
	private float m_EncounterTime;

	[Tooltip("Should this award XP?")]
	[SerializeField]
	private bool m_AwardXP;

	[SerializeField]
	[Tooltip("Amount to award")]
	private int m_XPAmount;

	[SerializeField]
	[Tooltip("Corp to award to")]
	private FactionSubTypes m_XPCorp;

	[Tooltip("Should this award BB?")]
	[SerializeField]
	private bool m_AwardBB;

	[Tooltip("Amount to award")]
	[SerializeField]
	private int m_BBAmount;

	[SerializeField]
	[Tooltip("Should we award blocks on completion?")]
	private bool m_AwardBlocks;

	[SerializeField]
	[Tooltip("Are the blocks already rewarded through the script? In which case they won't be spawned at mission complete.")]
	private bool m_BlocksRewardedFromScript;

	[Tooltip("Concrete blocks to reward the player with")]
	[SerializeField]
	private BlockTypes[] m_BlocksToAward;

	[SerializeField]
	[Tooltip("Do we give (additional) rewards from the corp reward pool?")]
	private bool m_RewardFromCorpPool;

	[SerializeField]
	[Tooltip("Reward pool from what Corp to use.")]
	private FactionSubTypes m_RewardPoolCorp;

	[SerializeField]
	[Tooltip("Amount of blocks to award from reward pool.")]
	private int m_AmountToAwardFromPool = 1;

	[Tooltip("Hide blocks on reward panel?")]
	[SerializeField]
	private bool m_HideBlockReward;

	[Tooltip("Should this award a Tech?")]
	[SerializeField]
	private bool m_AwardTech;

	[SerializeField]
	[Tooltip("Is the tech already rewarded through the script? In which case it won't be spawned at mission complete.")]
	private bool m_TechRewardedFromScript;

	[Tooltip("Tech to award to player")]
	[SerializeField]
	private TankPreset m_TechToAward;

	[SerializeField]
	[Tooltip("Should this award a Corporation License?")]
	private bool m_AwardLicense;

	[SerializeField]
	[Tooltip("Corporation License to award to player")]
	private FactionSubTypes m_CorpLicenseToAward;

	[Tooltip("What type of mission is this?")]
	[SerializeField]
	private AnalyticsMissionType m_AnalyticsMissionType;

	public int NumObjectives => m_Objectives.Length;

	public bool IsTimed => m_IsTimed;

	public float EncounterTime => m_EncounterTime;

	public bool AwardXP => m_AwardXP;

	public int XPAmount => m_XPAmount;

	public FactionSubTypes XPCorp => m_XPCorp;

	public bool AwardBB => m_AwardBB;

	public int BBAmount => m_BBAmount;

	public bool AwardBlocks => m_AwardBlocks;

	public bool BlocksNeedAwardingOnCompletion => !m_BlocksRewardedFromScript;

	public BlockTypes[] BlocksToAward => m_BlocksToAward;

	public bool RewardBlocksFromCorpPool => m_RewardFromCorpPool;

	public FactionSubTypes RewardPoolCorp => m_RewardPoolCorp;

	public int AmountToAwardFromPool => m_AmountToAwardFromPool;

	public bool HideBlockReward => m_HideBlockReward;

	public bool AwardTech => m_AwardTech;

	public bool TechNeedsAwarding => m_TechRewardedFromScript;

	public TechData TechToAward => m_TechToAward.GetTechDataFormatted();

	public bool AwardLicense => m_AwardLicense;

	public FactionSubTypes CorpLicenseToAward => m_CorpLicenseToAward;

	public AnalyticsMissionType MissionTypeForAnalytics => m_AnalyticsMissionType;

	public bool ShowMessagesInChat(bool onAdd)
	{
		bool flag = !m_IsMultiStageEncounter || (m_MultiStage_IsFirstEncounter && onAdd) || (m_MultiStage_IsLastEncounter && !onAdd);
		return m_ShowMessagesInChat && flag;
	}

	public int GetRandomStringBankIdx()
	{
		return UnityEngine.Random.Range(0, m_StringBanks.Length);
	}

	public string TitleString(int stringBankIdx = 0)
	{
		return GetString(stringBankIdx, m_TitleStringID);
	}

	public string FullDescriptionString(int stringBankIdx = 0)
	{
		return GetString(stringBankIdx, m_FullDescriptionStringID);
	}

	public string ObjectiveDescription(int objectiveIndex, int stringBankIdx = 0)
	{
		return GetString(stringBankIdx, m_Objectives[objectiveIndex].m_DescriptionStringID);
	}

	public bool IsObjectiveVisibleByDefault(int objectiveIndex)
	{
		return m_Objectives[objectiveIndex].m_ShowByDefault;
	}

	public int ObjectiveTargetCount(int objectiveIndex)
	{
		return m_Objectives[objectiveIndex].m_TargetCount;
	}

	private string GetString(int stringBankIdx, string stringID)
	{
		return Singleton.Manager<Localisation>.inst.GetLocalisedString(GetStringBank(stringBankIdx), stringID);
	}

	private string GetStringBank(int idx)
	{
		d.Assert(idx >= 0 && idx < m_StringBanks.Length, "EncounterDetails.GetStringBank - String bank index was out of range for '" + m_StringBanks[0] + "'. Received index " + idx);
		return m_StringBanks[idx];
	}
}
