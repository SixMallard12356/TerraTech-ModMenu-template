#define UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICorpLicense : MonoBehaviour
{
	[SerializeField]
	private Image[] m_Icons;

	[SerializeField]
	private RectTransform m_Minimised;

	[SerializeField]
	private RectTransform m_Maximised;

	[SerializeField]
	private Text[] m_CurrentLevel;

	[SerializeField]
	private Text[] m_NextLevel;

	[SerializeField]
	private Text m_LevelTitle;

	[SerializeField]
	private TMP_Text m_XPValueField;

	[SerializeField]
	private Image m_XPBar;

	[SerializeField]
	private Image m_XPUpgradeBar;

	[SerializeField]
	private float m_TimeToFillEntireBar = 2.5f;

	[SerializeField]
	private float m_ExpandExtraTime = 2.5f;

	[SerializeField]
	private ParticleSystem m_ParticleSystem;

	[SerializeField]
	private string m_MaxedHexValue = "#ff0000ff";

	[SerializeField]
	private TooltipComponent m_TooltipComponent;

	private FactionLicense m_License;

	private bool m_Expanded;

	private bool m_PlayerExpanded;

	private bool m_UpdateBar;

	private bool m_HoldBar;

	private float m_CurrentScale;

	private float m_TargetScale;

	private int m_LevelTitleStringID;

	private float m_Timer;

	private float m_FillTime;

	public void Init(FactionLicense license)
	{
		m_License = license;
		Setup();
	}

	public void Show(bool show)
	{
		m_PlayerExpanded = false;
		if (show)
		{
			SetExpanded(expanded: false);
		}
		base.gameObject.SetActive(show);
	}

	public void OnHover(bool pointerEnter)
	{
		if (pointerEnter)
		{
			m_PlayerExpanded = true;
			SetExpanded(expanded: true);
			return;
		}
		m_PlayerExpanded = false;
		if (!m_UpdateBar && !m_HoldBar)
		{
			SetExpanded(expanded: false);
		}
	}

	private void Setup()
	{
		if (m_License != null)
		{
			SetLevels();
			m_CurrentScale = m_License.GetXPScaleValue();
			m_TargetScale = m_CurrentScale;
			SetXPScale(m_CurrentScale, upgradeBar: false);
			SetXPScale(m_CurrentScale, upgradeBar: true);
			SetXPValue();
			int toolTip = -1;
			switch (m_License.Corporation)
			{
			case FactionSubTypes.GSO:
				m_LevelTitleStringID = 2;
				toolTip = 77;
				break;
			case FactionSubTypes.GC:
				m_LevelTitleStringID = 1;
				toolTip = 76;
				break;
			case FactionSubTypes.VEN:
				m_LevelTitleStringID = 7;
				toolTip = 134;
				break;
			case FactionSubTypes.HE:
				m_LevelTitleStringID = 3;
				toolTip = 79;
				break;
			case FactionSubTypes.BF:
				m_LevelTitleStringID = 0;
				toolTip = 52;
				break;
			case FactionSubTypes.SJ:
				m_LevelTitleStringID = 6;
				toolTip = 115;
				break;
			case FactionSubTypes.EXP:
				m_LevelTitleStringID = 5;
				toolTip = 104;
				break;
			default:
				d.LogError("UICorpLicense.Setup - Corporation " + m_License.Corporation.ToString() + " has no localized title");
				m_LevelTitleStringID = -1;
				break;
			}
			for (int i = 0; i < m_Icons.Length; i++)
			{
				m_Icons[i].sprite = Singleton.Manager<ManUI>.inst.GetSelectedCorpIcon(m_License.Corporation);
			}
			SetToolTip(toolTip);
			SetLevelTitle();
			Show(m_License.IsDiscovered);
		}
	}

	private void SetExpanded(bool expanded)
	{
		if (m_Expanded != expanded)
		{
			m_Expanded = expanded;
			m_Minimised.gameObject.SetActive(!m_Expanded);
			m_Maximised.gameObject.SetActive(m_Expanded);
		}
	}

	private void SetLevels()
	{
		int num = m_License.CurrentLevel + 1;
		int num2 = num + 1;
		if (m_CurrentLevel != null)
		{
			for (int i = 0; i < m_CurrentLevel.Length; i++)
			{
				m_CurrentLevel[i].text = num.ToString();
			}
		}
		if (m_NextLevel != null)
		{
			for (int j = 0; j < m_NextLevel.Length; j++)
			{
				m_NextLevel[j].text = num2.ToString();
			}
		}
	}

	private void SetXPScale(float filVal, bool upgradeBar)
	{
		if (upgradeBar)
		{
			if (m_XPUpgradeBar != null)
			{
				m_XPUpgradeBar.fillAmount = filVal;
			}
		}
		else if (m_XPBar != null)
		{
			m_XPBar.fillAmount = filVal;
		}
	}

	private void SetXPValue()
	{
		if (m_XPValueField != null)
		{
			string text = null;
			if (m_License.HasReachedMaxLevel)
			{
				text = "<color=" + m_MaxedHexValue + ">" + Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, 17) + "</color>";
			}
			else
			{
				int stringID = 8;
				text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, stringID);
				text = string.Format(text, m_License.CurrentAbsoluteXP, m_License.NextLevelAbsoluteXP);
			}
			m_XPValueField.text = text;
		}
	}

	private void SetLevelTitle()
	{
		if (m_LevelTitle != null && m_LevelTitleStringID != -1)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Licensing, m_LevelTitleStringID);
			m_LevelTitle.text = string.Format(localisedString, m_License.CurrentLevel + 1);
		}
	}

	private void SetToolTip(int stringID)
	{
		if (m_TooltipComponent != null && stringID != -1)
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, stringID);
			m_TooltipComponent.SetText(localisedString);
		}
	}

	private void OnAddXP(FactionSubTypes corp)
	{
		if (m_License != null && m_License.Corporation == corp)
		{
			m_TargetScale = m_License.GetXPScaleValue();
			if (m_CurrentScale != m_TargetScale)
			{
				SetXPValue();
				m_UpdateBar = true;
				SetXPScale(m_TargetScale, upgradeBar: true);
				m_FillTime = (m_TargetScale - m_CurrentScale) * m_TimeToFillEntireBar;
				m_Timer = 0f;
				SetExpanded(expanded: true);
			}
		}
	}

	private void OnLevelUP(FactionSubTypes corp, int level)
	{
		if (m_License != null && m_License.Corporation == corp)
		{
			SetLevels();
			SetXPScale(0f, upgradeBar: false);
			SetXPScale(0f, upgradeBar: true);
			SetXPValue();
			SetLevelTitle();
			m_CurrentScale = 0f;
			m_TargetScale = 0f;
		}
	}

	private void OnShowAllXpBars(bool expand)
	{
		SetExpanded(expand);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManLicenses>.inst.AddedXPEvent.Subscribe(OnAddXP);
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Subscribe(OnLevelUP);
		Singleton.Manager<ManLicenses>.inst.ShowAllXpBarsEvent.Subscribe(OnShowAllXpBars);
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(SetXPValue);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManLicenses>.inst.AddedXPEvent.Unsubscribe(OnAddXP);
		Singleton.Manager<ManLicenses>.inst.LevelUpEvent.Unsubscribe(OnLevelUP);
		Singleton.Manager<ManLicenses>.inst.ShowAllXpBarsEvent.Unsubscribe(OnShowAllXpBars);
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Unsubscribe(SetXPValue);
	}

	private void Update()
	{
		if (m_UpdateBar)
		{
			float t = Mathf.Clamp01(m_Timer / m_FillTime);
			m_Timer += Time.deltaTime;
			m_CurrentScale = Mathf.Lerp(m_CurrentScale, m_TargetScale, t);
			float num = 0.001f;
			if (Mathf.Abs(m_CurrentScale - m_TargetScale) < num)
			{
				m_CurrentScale = m_TargetScale;
				m_UpdateBar = false;
				m_HoldBar = true;
				m_Timer = 0f;
			}
			SetXPScale(m_CurrentScale, upgradeBar: false);
		}
		else if (m_HoldBar)
		{
			if (m_Timer >= m_ExpandExtraTime)
			{
				SetExpanded(m_PlayerExpanded);
				m_HoldBar = false;
				m_Timer = 0f;
			}
			else
			{
				m_Timer += Time.deltaTime;
			}
		}
	}
}
