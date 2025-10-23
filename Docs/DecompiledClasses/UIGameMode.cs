#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIGameMode : MonoBehaviour
{
	public enum ModeListDisplayType
	{
		None,
		Carousel,
		Grid
	}

	public Event<UIGameMode> ModeHighlightEvent;

	[SerializeField]
	private bool m_AvailableOnConsole = true;

	[SerializeField]
	private LocalisedString m_ModeTitle;

	[SerializeField]
	private LocalisedString m_ModeDescription;

	[SerializeField]
	private UIGameModeInfoContainer m_ModeInfoPrefab;

	[SerializeField]
	[Space(10f)]
	private bool m_HasSubModes;

	[SerializeField]
	[HideInInspector]
	private ModeListDisplayType m_SubModeDisplayType;

	[SerializeField]
	[HideInInspector]
	private UIGameMode[] m_SubModes;

	[SerializeField]
	[HideInInspector]
	[FormerlySerializedAs("m_HasConsolePlayPrefab")]
	private bool m_HasNoTwitterPlayPrefab;

	[SerializeField]
	[HideInInspector]
	private GameObject m_ModePlayPrefab;

	[HideInInspector]
	[FormerlySerializedAs("m_ModePlayPrefabConsole")]
	[SerializeField]
	private GameObject m_ModePlayPrefabNoTwitter;

	[SerializeField]
	[HideInInspector]
	private UIGameModeSettings m_ModeSettings;

	private UIGameModeInfoContainer m_ModeInfo;

	private GameObject m_PlaySettingsObject;

	private List<UIGameMode> m_SubModeList;

	private Transform m_InactiveRoot;

	private UIGameModeSettings.ModeInitSettingProvider[] m_SettingProviders;

	public bool InPlayMode
	{
		get
		{
			if (m_PlaySettingsObject != null)
			{
				return m_PlaySettingsObject.activeInHierarchy;
			}
			return false;
		}
	}

	public void Initialise(bool hide = true)
	{
		m_InactiveRoot = base.transform.parent;
		if ((bool)m_ModeInfoPrefab)
		{
			m_ModeInfo = m_ModeInfoPrefab.UnpooledSpawn();
			m_ModeInfo.name = $"{base.name} {m_ModeInfo.name}";
			m_ModeInfo.transform.SetParent(base.transform, worldPositionStays: false);
		}
		if (!m_HasSubModes)
		{
			GameObject gameObject = null;
			if (m_HasNoTwitterPlayPrefab)
			{
				if (m_ModePlayPrefabNoTwitter != null)
				{
					gameObject = m_ModePlayPrefabNoTwitter;
				}
				else
				{
					d.LogErrorFormat("UIGameMode.Initialise - Game mode ({0}) is set to override play prefab for no twitter, but no m_ModePlayPrefabNoTwitter has been assigned", base.name);
				}
			}
			else
			{
				gameObject = m_ModePlayPrefab;
			}
			if (gameObject != null)
			{
				m_PlaySettingsObject = gameObject.transform.UnpooledSpawn().gameObject;
				m_PlaySettingsObject.name = $"{base.name} {m_PlaySettingsObject.name}";
				UIGameModePlayButton componentInChildren = m_PlaySettingsObject.GetComponentInChildren<UIGameModePlayButton>();
				if (componentInChildren != null)
				{
					componentInChildren.Initialise(this);
				}
				m_SettingProviders = m_PlaySettingsObject.GetComponentsInChildren<UIGameModeSettings.ModeInitSettingProvider>();
			}
		}
		if (m_SubModeDisplayType != ModeListDisplayType.None && m_SubModes.Length != 0)
		{
			m_SubModeList = new List<UIGameMode>(m_SubModes.Length);
			for (int i = 0; i < m_SubModes.Length; i++)
			{
				if (m_SubModes[i] != null)
				{
					bool flag = true;
					if (SKU.ConsoleUI && !m_SubModes[i].m_AvailableOnConsole)
					{
						flag = false;
					}
					if (flag)
					{
						UIGameMode uIGameMode = Object.Instantiate(m_SubModes[i]);
						uIGameMode.transform.SetParent(base.transform, worldPositionStays: false);
						uIGameMode.Initialise();
						m_SubModeList.Add(uIGameMode);
					}
				}
			}
		}
		if (hide)
		{
			Hide();
		}
	}

	public string GetTitle()
	{
		return m_ModeTitle.Value;
	}

	public bool HasSubMode()
	{
		bool flag = m_SubModeDisplayType != ModeListDisplayType.None && m_SubModeList != null && m_SubModeList.Count > 0;
		d.Assert(!m_HasSubModes || flag, "UIGameMode '" + base.name + "' wants sub-modes but they have not been setup completely!");
		return m_HasSubModes && flag;
	}

	public bool IsAvailable()
	{
		bool result = true;
		SetUIButtonDemoState component = GetComponent<SetUIButtonDemoState>();
		if (component != null)
		{
			result = component.IsAvailableInSKU();
		}
		else
		{
			UISetElementFromSKU component2 = GetComponent<UISetElementFromSKU>();
			if (component2 != null)
			{
				result = component2.IsAvailableInSKU();
			}
		}
		return result;
	}

	public ManGameMode.ModeSettings GetModeSettings()
	{
		ManGameMode.ModeSettings modeSettings = m_ModeSettings.GetModeSettings();
		if (m_SettingProviders != null)
		{
			for (int i = 0; i < m_SettingProviders.Length; i++)
			{
				m_SettingProviders[i].AddSettings(modeSettings);
			}
		}
		return modeSettings;
	}

	public ModeListDisplayType GetSubModeDisplayType()
	{
		return m_SubModeDisplayType;
	}

	public List<UIGameMode> GetSubModes()
	{
		return m_SubModeList;
	}

	public Button GetButton()
	{
		return GetComponent<Button>();
	}

	public void ShowButton(RectTransform rectTransform)
	{
		base.transform.SetParent(rectTransform.transform, worldPositionStays: false);
		base.gameObject.SetActive(value: true);
	}

	public void HideButton()
	{
		base.gameObject.SetActive(value: false);
		base.transform.SetParent(m_InactiveRoot, worldPositionStays: false);
	}

	public void ShowInfo(RectTransform rectTransform)
	{
		if ((bool)m_ModeInfo)
		{
			m_ModeInfo.SetText(m_ModeTitle.Value, m_ModeDescription.Value);
			m_ModeInfo.transform.SetParent(rectTransform.transform, worldPositionStays: false);
			m_ModeInfo.gameObject.SetActive(value: true);
		}
	}

	public void HideInfo()
	{
		if ((bool)m_ModeInfo)
		{
			m_ModeInfo.gameObject.SetActive(value: false);
			m_ModeInfo.transform.SetParent(base.transform, worldPositionStays: false);
		}
	}

	public void ShowPlaySettings(RectTransform rectTransform)
	{
		if (!(m_PlaySettingsObject != null))
		{
			return;
		}
		d.Assert(m_PlaySettingsObject != null, "UIGameMode.ShowPlaySettings - PlaySettingsPrefab not setup correctly in prefab " + base.gameObject.name);
		m_PlaySettingsObject.transform.SetParent(rectTransform.transform, worldPositionStays: false);
		m_PlaySettingsObject.gameObject.SetActive(value: true);
		if (m_SettingProviders != null)
		{
			for (int i = 0; i < m_SettingProviders.Length; i++)
			{
				m_SettingProviders[i].InitComponent();
			}
		}
	}

	public void HidePlaySettings()
	{
		if ((bool)m_PlaySettingsObject)
		{
			m_PlaySettingsObject.gameObject.SetActive(value: false);
			m_PlaySettingsObject.transform.SetParent(base.transform, worldPositionStays: false);
		}
	}

	public void Hide()
	{
		HideButton();
		HideInfo();
		HidePlaySettings();
		OnModeHighlighted(null);
	}

	public void OnModeHighlighted(UIGameMode mode)
	{
		ModeHighlightEvent.Send(mode);
	}
}
