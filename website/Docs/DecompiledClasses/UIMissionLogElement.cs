using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMissionLogElement : MonoBehaviour
{
	public enum RemoveType
	{
		None,
		Cancelled,
		Complete,
		Accepted
	}

	[SerializeField]
	private Image m_CorpIcon;

	[SerializeField]
	private Text m_GradeLevel;

	[SerializeField]
	private TextMeshProUGUI m_Title;

	[SerializeField]
	private UIMissionObjective m_CurrentObjectiveDisplay;

	[SerializeField]
	private TextMeshProUGUI m_DistanceField;

	[SerializeField]
	private RectTransform m_MissionTimePanel;

	[SerializeField]
	private Text m_MissionTimeLabel;

	[SerializeField]
	private float m_WarningTimeRemaining = 10f;

	[SerializeField]
	private Color m_WarningTimeColour = Color.red;

	[SerializeField]
	private float m_WarningTimeBlinkInterval = 0.4f;

	[SerializeField]
	private RectTransform m_EncounterUpdatedIndicator;

	[SerializeField]
	private RectTransform m_EncounterCancelledIndicator;

	[SerializeField]
	private RectTransform m_EncounterCompletedIndicator;

	[SerializeField]
	private RectTransform m_EncounterAcceptedIndicator;

	[SerializeField]
	private RectTransform m_XPAwardPanel;

	[SerializeField]
	private TooltipComponent m_XPAwardTooltip;

	[SerializeField]
	private TextMeshProUGUI m_XPAwardField;

	[SerializeField]
	private RectTransform m_BBAwardPanel;

	[SerializeField]
	private TextMeshProUGUI m_BBAwardField;

	public Event<EncounterDisplayData> OnItemSelected;

	private Toggle m_ToggleButton;

	private Color m_DefaultTextColour = Color.white;

	private RemoveType m_Removed;

	public EncounterDisplayData Data { get; private set; }

	public void Init(EncounterDisplayData encounterDisplayData)
	{
		if (Data != encounterDisplayData || m_Removed != RemoveType.None)
		{
			if (Data != null && Data.ActiveQuestLog != null)
			{
				Data.ActiveQuestLog.LogDetailsUpdated.Unsubscribe(UpdateLog);
			}
			m_Removed = RemoveType.None;
			Data = encounterDisplayData;
			UpdateUI();
			if (Data != null && Data.ActiveQuestLog != null)
			{
				Data.ActiveQuestLog.LogDetailsUpdated.Subscribe(UpdateLog);
			}
		}
	}

	private void UpdateUI()
	{
		bool flag = Data != null;
		if ((bool)m_CorpIcon)
		{
			if (flag)
			{
				m_CorpIcon.sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(Data.Corp);
			}
			m_CorpIcon.gameObject.SetActive(flag && Data.Corp != FactionSubTypes.NULL);
		}
		if (m_GradeLevel != null)
		{
			m_GradeLevel.text = ((flag && Data.Grade > 0) ? Data.Grade.ToString() : string.Empty);
		}
		if (m_DistanceField != null)
		{
			m_DistanceField.gameObject.SetActive(flag && Data.HasPosition);
		}
		if (m_EncounterCancelledIndicator != null)
		{
			m_EncounterCancelledIndicator.gameObject.SetActive(m_Removed == RemoveType.Cancelled);
		}
		if (m_EncounterCompletedIndicator != null)
		{
			m_EncounterCompletedIndicator.gameObject.SetActive(m_Removed == RemoveType.Complete);
		}
		if (m_EncounterAcceptedIndicator != null)
		{
			m_EncounterAcceptedIndicator.gameObject.SetActive(m_Removed == RemoveType.Accepted);
		}
		if (m_EncounterUpdatedIndicator != null && m_Removed != RemoveType.None)
		{
			m_EncounterUpdatedIndicator.gameObject.SetActive(value: false);
		}
		UpdateLog();
	}

	private void UpdateLog(EncounterIdentifier unused = default(EncounterIdentifier))
	{
		m_Title.text = ((Data != null) ? Data.Title : string.Empty);
		if (m_CurrentObjectiveDisplay != null)
		{
			bool flag = Data != null && Data.ActiveObjective != null;
			m_CurrentObjectiveDisplay.gameObject.SetActive(flag);
			if (flag)
			{
				m_CurrentObjectiveDisplay.Init(Data.ActiveObjective);
			}
		}
		if (m_MissionTimePanel != null)
		{
			m_MissionTimePanel.gameObject.SetActive(Data != null && Data.IsTimed);
		}
	}

	public bool IsToggled()
	{
		if (!(m_ToggleButton != null))
		{
			return false;
		}
		return m_ToggleButton.isOn;
	}

	public void SetToggled(bool toggleOn)
	{
		if (m_ToggleButton != null)
		{
			m_ToggleButton.SetValue(toggleOn);
		}
	}

	public void SetToggleGroup(ToggleGroup group)
	{
		if (m_ToggleButton != null)
		{
			m_ToggleButton.group = group;
		}
	}

	public void OpenFullMissionLog()
	{
		UIMissionLogFullHUD.MissionLogShowContext missionLogShowContext = new UIMissionLogFullHUD.MissionLogShowContext
		{
			encounterDisplayData = Data
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MissionLog, missionLogShowContext);
	}

	public void ShowAsRemoved(RemoveType removeType)
	{
		if (removeType != m_Removed)
		{
			m_Removed = removeType;
			UpdateUI();
		}
	}

	public bool IsShownAsRemoved()
	{
		return m_Removed != RemoveType.None;
	}

	public void OnToggleClicked(bool selected)
	{
		if (selected)
		{
			OnItemSelected.Send(Data);
		}
	}

	public void OnButtonClicked()
	{
		OnItemSelected.Send(Data);
	}

	private void OnRecentEncountersUpdated(EncounterIdentifier id, bool wasAdded)
	{
		if (m_EncounterUpdatedIndicator != null && Data != null && Data.Identifier == id)
		{
			m_EncounterUpdatedIndicator.gameObject.SetActive(wasAdded);
		}
	}

	private void OnLanguageChanged()
	{
		UpdateUI();
	}

	private void OnPool()
	{
		m_ToggleButton = GetComponent<Toggle>();
		if (m_ToggleButton != null)
		{
			m_ToggleButton.onValueChanged.AddListener(OnToggleClicked);
		}
		Button component = GetComponent<Button>();
		if (component != null)
		{
			component.onClick.AddListener(OnButtonClicked);
		}
		if (m_MissionTimeLabel != null)
		{
			m_DefaultTextColour = m_MissionTimeLabel.color;
		}
	}

	private void OnSpawn()
	{
		if (m_EncounterUpdatedIndicator != null)
		{
			m_EncounterUpdatedIndicator.gameObject.SetActive(value: false);
			Singleton.Manager<ManQuestLog>.inst.OnRecentEncountersUpdated.Subscribe(OnRecentEncountersUpdated);
		}
		if (m_MissionTimeLabel != null)
		{
			m_MissionTimeLabel.color = m_DefaultTextColour;
		}
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLanguageChanged);
	}

	private void OnRecycle()
	{
		if (Data != null && Data.ActiveQuestLog != null)
		{
			Data.ActiveQuestLog.LogDetailsUpdated.Unsubscribe(UpdateLog);
		}
		if (m_EncounterUpdatedIndicator != null)
		{
			Singleton.Manager<ManQuestLog>.inst.OnRecentEncountersUpdated.Unsubscribe(OnRecentEncountersUpdated);
		}
		OnItemSelected.Clear();
		Data = null;
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(OnLanguageChanged);
	}

	private void Update()
	{
		if (m_DistanceField != null && Data != null && Data.HasPosition)
		{
			float gameUnits = Vector3.Distance(Singleton.playerPos, Data.ScenePosition);
			m_DistanceField.text = GameUnits.GetDistanceText(gameUnits);
		}
		if (m_MissionTimeLabel != null && Data != null && Data.IsTimed)
		{
			bool forceHourDisplay = Data.EncounterTotalTime > 3600f;
			float encounterTime = Data.EncounterTime;
			m_MissionTimeLabel.text = Singleton.Manager<Localisation>.inst.GetTimeDisplayString(encounterTime, forceHourDisplay);
			if (m_WarningTimeRemaining > 0f)
			{
				bool flag = encounterTime < m_WarningTimeRemaining && (int)(encounterTime / m_WarningTimeBlinkInterval) % 2 == 0;
				m_MissionTimeLabel.color = (flag ? m_WarningTimeColour : m_DefaultTextColour);
			}
		}
		if (m_XPAwardPanel != null)
		{
			bool flag2 = Data.AwardPlayerXP && Data.AwardXPAmount > 0;
			m_XPAwardPanel.gameObject.SetActive(flag2);
			if (flag2)
			{
				if (m_XPAwardTooltip != null)
				{
					int num = Data.AwardXPCorp switch
					{
						FactionSubTypes.GSO => 77, 
						FactionSubTypes.GC => 76, 
						FactionSubTypes.VEN => 134, 
						FactionSubTypes.HE => 79, 
						_ => -1, 
					};
					if (num != -1)
					{
						string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, num);
						m_XPAwardTooltip.SetText(localisedString);
					}
				}
				m_XPAwardField.text = $"{Data.AwardXPAmount.ToString()}";
			}
		}
		if (m_BBAwardPanel != null)
		{
			bool flag3 = Data.AwardPlayerBB && Data.AwardBBAmount > 0;
			m_BBAwardPanel.gameObject.SetActive(flag3);
			if (flag3)
			{
				m_BBAwardField.text = Singleton.Manager<Localisation>.inst.GetMoneyString(Data.AwardBBAmount);
			}
		}
	}
}
