using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionDetails : MonoBehaviour
{
	[SerializeField]
	private RectTransform m_ContentPanel;

	[SerializeField]
	private UIScrollRectSetter m_ScrollResetter;

	[SerializeField]
	private Image m_CorpIcon;

	[SerializeField]
	private Text m_GradeLevel;

	[SerializeField]
	private Text m_Title;

	[SerializeField]
	private TextMeshProUGUI m_Description;

	[SerializeField]
	private RectTransform m_TimedMissionHeader;

	[SerializeField]
	private RectTransform m_MissionTimePanel;

	[SerializeField]
	private Text m_MissionTime;

	[SerializeField]
	private UIMissionObjective m_MissionObjectivePrefab;

	[SerializeField]
	private RectTransform m_MissionObjectivePanel;

	[SerializeField]
	private RectTransform m_XPAwardPanel;

	[SerializeField]
	private Image m_XPAwardCorpIcon;

	[SerializeField]
	private TooltipComponent m_XPAwardTooltip;

	[SerializeField]
	private Text m_XPAwardField;

	[SerializeField]
	private RectTransform m_BBAwardPanel;

	[SerializeField]
	private Text m_BBAwardField;

	[SerializeField]
	private RectTransform m_ItemRewardPanel;

	[SerializeField]
	private UIItemDisplay m_BlockRewardPrefab;

	[SerializeField]
	private RectTransform m_BlockRewardFromPoolPanel;

	[SerializeField]
	private Text m_BlockRewardFromPoolAmount;

	[SerializeField]
	private UITechDisplay m_TechReward;

	[SerializeField]
	private RectTransform m_LicenseAwardPanel;

	[SerializeField]
	private Image m_LicenseAwardCorpIcon;

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private LocalisedString[] m_RandomBlockString;

	private List<UIMissionObjective> m_ActiveObjectives = new List<UIMissionObjective>();

	private List<UIItemDisplay> m_BlockRewardItems = new List<UIItemDisplay>();

	public EncounterDisplayData Data { get; private set; }

	public void SetEncounter(EncounterDisplayData encounter)
	{
		if (encounter != Data)
		{
			if (Data != null && Data.ActiveQuestLog != null)
			{
				Data.ActiveQuestLog.LogDetailsUpdated.Unsubscribe(UpdateObjectives);
			}
			Data = encounter;
			if (encounter != null)
			{
				m_ContentPanel.gameObject.SetActive(value: true);
				if (encounter.ActiveQuestLog != null)
				{
					encounter.ActiveQuestLog.LogDetailsUpdated.Subscribe(UpdateObjectives);
				}
				if ((bool)m_CorpIcon)
				{
					m_CorpIcon.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(encounter.Corp);
					m_CorpIcon.enabled = encounter.Corp != FactionSubTypes.NULL;
				}
				if (m_GradeLevel != null)
				{
					m_GradeLevel.text = encounter.Grade.ToString();
					m_GradeLevel.enabled = encounter.Grade > 0;
				}
				m_Title.text = encounter.Title;
				if ((bool)m_Description)
				{
					m_Description.text = encounter.FullDescription;
				}
				UpdateObjectives();
				UpdateRewardsDisplay();
				if (m_ScrollResetter != null)
				{
					m_ScrollResetter.SetToTop();
				}
			}
			if (encounter == null)
			{
				m_ContentPanel.gameObject.SetActive(value: false);
			}
		}
		else if (encounter == null)
		{
			m_ContentPanel.gameObject.SetActive(value: false);
		}
	}

	private void UpdateObjectives(EncounterIdentifier unused = default(EncounterIdentifier))
	{
		if (m_MissionObjectivePrefab != null)
		{
			int num = 0;
			int numObjectives = Data.NumObjectives;
			for (int i = 0; i < numObjectives; i++)
			{
				if (Data.Objectives[i].IsVisible)
				{
					UIMissionObjective uIMissionObjective;
					if (i < m_ActiveObjectives.Count)
					{
						uIMissionObjective = m_ActiveObjectives[i];
					}
					else
					{
						Transform obj = m_MissionObjectivePrefab.transform.Spawn();
						obj.SetParent(m_MissionObjectivePanel, worldPositionStays: false);
						uIMissionObjective = obj.GetComponent<UIMissionObjective>();
						m_ActiveObjectives.Add(uIMissionObjective);
					}
					uIMissionObjective.Init(Data.Objectives[i]);
					num++;
				}
			}
			for (int num2 = m_ActiveObjectives.Count - 1; num2 >= num; num2--)
			{
				m_ActiveObjectives[num2].transform.SetParent(null, worldPositionStays: false);
				m_ActiveObjectives[num2].transform.Recycle();
				m_ActiveObjectives.RemoveAt(num2);
			}
		}
		UpdateObjectiveTime();
	}

	private void UpdateObjectiveTime()
	{
		if (m_TimedMissionHeader != null)
		{
			m_TimedMissionHeader.gameObject.SetActive(Data != null && Data.IsTimed);
		}
		if (m_MissionTimePanel != null)
		{
			m_MissionTimePanel.gameObject.SetActive(Data != null && Data.IsTimed);
		}
	}

	private void UpdateRewardsDisplay()
	{
		bool flag = Data.AwardPlayerXP && Data.AwardXPAmount > 0;
		int num = (flag ? Data.AwardXPAmount : 0);
		m_XPAwardField.text = $"{num.ToString()}";
		if (m_XPAwardPanel != null)
		{
			m_XPAwardPanel.gameObject.SetActive(flag);
			if (flag)
			{
				if ((bool)m_XPAwardCorpIcon)
				{
					m_XPAwardCorpIcon.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(Data.AwardXPCorp);
				}
				if (m_XPAwardTooltip != null)
				{
					int num2 = Data.AwardXPCorp switch
					{
						FactionSubTypes.GSO => 77, 
						FactionSubTypes.GC => 76, 
						FactionSubTypes.VEN => 134, 
						FactionSubTypes.HE => 79, 
						_ => -1, 
					};
					if (num2 != -1)
					{
						string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, num2);
						m_XPAwardTooltip.SetText(localisedString);
					}
				}
			}
		}
		if (m_BBAwardPanel != null)
		{
			bool flag2 = Data.AwardPlayerBB && Data.AwardBBAmount > 0;
			m_BBAwardPanel.gameObject.SetActive(flag2);
			if (flag2)
			{
				m_BBAwardField.text = Singleton.Manager<Localisation>.inst.GetMoneyString(Data.AwardBBAmount);
			}
		}
		if (m_ItemRewardPanel != null && m_BlockRewardPrefab != null)
		{
			int num3 = 0;
			if (Data.AwardPlayerBlocks && !Data.HideBlockReward && Data.BlocksToAward != null && Data.BlocksToAward.Length != 0)
			{
				for (int i = 0; i < Data.BlocksToAward.Length; i++)
				{
					UIItemDisplay uIItemDisplay;
					if (i < m_BlockRewardItems.Count)
					{
						uIItemDisplay = m_BlockRewardItems[i];
					}
					else
					{
						Transform obj = m_BlockRewardPrefab.transform.Spawn();
						obj.SetParent(m_ItemRewardPanel, worldPositionStays: false);
						uIItemDisplay = obj.GetComponent<UIItemDisplay>();
						m_BlockRewardItems.Add(uIItemDisplay);
					}
					ItemTypeInfo itemType = new ItemTypeInfo(ObjectTypes.Block, (int)Data.BlocksToAward[i]);
					uIItemDisplay.Setup(itemType);
					num3++;
				}
			}
			for (int num4 = m_BlockRewardItems.Count - 1; num4 >= num3; num4--)
			{
				m_BlockRewardItems[num4].transform.SetParent(null, worldPositionStays: false);
				m_BlockRewardItems[num4].transform.Recycle();
				m_BlockRewardItems.RemoveAt(num4);
			}
		}
		if (m_BlockRewardFromPoolPanel != null)
		{
			bool active = Data.RewardFromCorpPool && !Data.HideBlockReward;
			m_BlockRewardFromPoolPanel.gameObject.SetActive(active);
			if (Data.RewardFromCorpPool)
			{
				TooltipComponent componentInChildren = m_BlockRewardFromPoolPanel.GetComponentInChildren<TooltipComponent>();
				if ((bool)componentInChildren)
				{
					if ((int)Data.RewardPoolCorp < m_RandomBlockString.Length)
					{
						componentInChildren.SetText(m_RandomBlockString[(int)Data.RewardPoolCorp].Value);
					}
					else
					{
						Debug.LogError($"Corporation {Data.RewardPoolCorp} has no entry in m_RandomBlockString in UIMissionDetails");
					}
				}
				if (m_BlockRewardFromPoolAmount != null)
				{
					m_BlockRewardFromPoolAmount.text = ((Data.AmountToAwardFromPool > 1) ? Data.AmountToAwardFromPool.ToString() : string.Empty);
				}
			}
		}
		if (m_TechReward != null)
		{
			bool flag3 = Data.AwardPlayerTech && Data.AwardTechData != null;
			m_TechReward.gameObject.SetActive(flag3);
			if (flag3)
			{
				m_TechReward.Setup(Data.AwardTechData);
			}
			else
			{
				m_TechReward.Clear();
			}
		}
		if (m_LicenseAwardPanel != null)
		{
			bool awardLicense = Data.AwardLicense;
			m_LicenseAwardPanel.gameObject.SetActive(awardLicense);
			if (awardLicense && m_LicenseAwardCorpIcon != null)
			{
				m_LicenseAwardCorpIcon.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(Data.CorpLicenseToAward);
			}
			TooltipComponent componentInChildren2 = m_LicenseAwardPanel.GetComponentInChildren<TooltipComponent>();
			if ((bool)componentInChildren2)
			{
				string text = $"Unlock {StringLookup.GetCorporationName(Data.CorpLicenseToAward)} License";
				componentInChildren2.SetText(text);
			}
		}
	}

	private void OnLanguageChanged()
	{
		EncounterDisplayData data = Data;
		Data = null;
		SetEncounter(data);
	}

	private void OnSpawn()
	{
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChanged);
	}

	private void OnRecycle()
	{
		if (Data != null && Data.ActiveQuestLog != null)
		{
			Data.ActiveQuestLog.LogDetailsUpdated.Unsubscribe(UpdateObjectives);
		}
		for (int i = 0; i < m_ActiveObjectives.Count; i++)
		{
			m_ActiveObjectives[i].transform.SetParent(null, worldPositionStays: false);
			m_ActiveObjectives[i].transform.Recycle();
		}
		m_ActiveObjectives.Clear();
		for (int j = 0; j < m_BlockRewardItems.Count; j++)
		{
			m_BlockRewardItems[j].transform.SetParent(null, worldPositionStays: false);
			m_BlockRewardItems[j].transform.Recycle();
		}
		m_BlockRewardItems.Clear();
		if (m_TechReward != null)
		{
			m_TechReward.Clear();
		}
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(OnLanguageChanged);
	}

	private void Update()
	{
		if (m_MissionTime != null && Data != null && Data.IsTimed)
		{
			bool forceHourDisplay = Data.EncounterTotalTime > 3600f;
			m_MissionTime.text = Singleton.Manager<Localisation>.inst.GetTimeDisplayString(Data.EncounterTime, forceHourDisplay);
		}
		if ((bool)m_ScrollResetter)
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(47);
			m_ScrollResetter.Scroll((0f - axis) * Time.deltaTime * Globals.inst.m_StickScrollSensitivity);
		}
	}
}
