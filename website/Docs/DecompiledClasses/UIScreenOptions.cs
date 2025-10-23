using System;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenOptions : UIScreen
{
	public enum OptionsType
	{
		Gameplay,
		Controls,
		Graphics,
		Audio
	}

	public enum SaveFailureType
	{
		None,
		RequiredKeyRebind
	}

	[SerializeField]
	private LocalisedString m_ExitWithChangesMsg;

	[SerializeField]
	private LocalisedString m_SaveChangesButton;

	[SerializeField]
	private LocalisedString m_DiscardChangesButton;

	[EnumArray(typeof(OptionsType))]
	[SerializeField]
	private UIOptions[] m_OptionsElements = new UIOptions[0];

	[EnumArray(typeof(OptionsType))]
	[SerializeField]
	private Toggle[] m_OptionsTabs = new Toggle[0];

	[SerializeField]
	private GameObject m_ChangesMade;

	[SerializeField]
	private UIOptions m_ConsoleUiOption;

	[SerializeField]
	private GameObject m_PCScreen;

	[SerializeField]
	private GameObject m_ConsoleScreen;

	private OptionsType m_OptionsType;

	private int m_OptionsTypeCount;

	private bool m_Changed;

	private EventNoParams OnChangeEvent;

	private OptionsType m_SelectedOptionType;

	private bool m_OneFrameUnblockScreenExitDelaySet;

	public void ShowOptions(OptionsType type)
	{
		for (int i = 0; i < m_OptionsTypeCount; i++)
		{
			if (m_OptionsElements[i] != null)
			{
				bool flag = i == (int)type;
				if (flag != m_OptionsElements[i].gameObject.activeInHierarchy)
				{
					m_OptionsElements[i].gameObject.SetActive(flag);
				}
				if (flag)
				{
					m_OptionsTabs[i].isOn = true;
				}
			}
			m_SelectedOptionType = type;
		}
	}

	public void SelectNextOption()
	{
		int num = (int)(m_SelectedOptionType + 1);
		if (num >= m_OptionsTypeCount)
		{
			num = 0;
		}
		ShowOptions((OptionsType)num);
	}

	public void SelectPrevOption()
	{
		int num = (int)(m_SelectedOptionType - 1);
		if (num < 0)
		{
			num = m_OptionsTypeCount - 1;
		}
		ShowOptions((OptionsType)num);
	}

	public void Save()
	{
		for (int i = 0; i < m_OptionsTypeCount; i++)
		{
			SaveFailureType saveFailureType = m_OptionsElements[i].CanSave();
			if (saveFailureType != SaveFailureType.None)
			{
				ShowSaveFailedMessage(saveFailureType);
				return;
			}
		}
		if (!SKU.ConsoleUI)
		{
			for (int j = 0; j < m_OptionsTypeCount; j++)
			{
				if (m_OptionsElements[j] != null)
				{
					m_OptionsElements[j].SaveSettings();
				}
			}
		}
		else
		{
			m_ConsoleUiOption.SaveSettings();
		}
		if (Singleton.Manager<ManProfile>.inst.Save())
		{
			CloseScreen(showPrev: true);
		}
	}

	public void ExitScreen()
	{
		if (m_Changed && !SKU.ConsoleUI)
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			UIButtonData decline = new UIButtonData
			{
				m_Label = m_SaveChangesButton.Value,
				m_Callback = delegate
				{
					Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
					Save();
				},
				m_RewiredAction = 21
			};
			UIButtonData accept = new UIButtonData
			{
				m_Label = m_DiscardChangesButton.Value,
				m_Callback = delegate
				{
					CloseScreen(showPrev: false);
					ClearSettings();
				},
				m_RewiredAction = 22
			};
			uIScreenNotifications.Set(m_ExitWithChangesMsg.Value, accept, decline);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
		else if (SKU.ConsoleUI)
		{
			Save();
		}
		else
		{
			CloseScreen(showPrev: true);
		}
	}

	private void CloseScreen(bool showPrev)
	{
		for (int i = 0; i < m_OptionsTypeCount; i++)
		{
			m_OptionsElements[i].OnCloseScreen();
		}
		Singleton.Manager<ManInput>.inst.CancelKeyAssign();
		Singleton.Manager<ManUI>.inst.PopScreen(showPrev);
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		if (fromStackPop)
		{
			return;
		}
		if (SKU.ConsoleUI)
		{
			Toggle[] optionsTabs = m_OptionsTabs;
			for (int i = 0; i < optionsTabs.Length; i++)
			{
				optionsTabs[i].gameObject.SetActive(value: false);
			}
			m_ConsoleScreen.SetActive(value: true);
			m_PCScreen.SetActive(value: false);
			m_ConsoleUiOption.Setup(OnChangeEvent);
		}
		else
		{
			m_PCScreen.SetActive(value: true);
			m_ConsoleScreen.SetActive(value: false);
			for (int j = 0; j < m_OptionsTypeCount; j++)
			{
				if (m_OptionsElements[j] != null)
				{
					m_OptionsElements[j].Setup(OnChangeEvent);
				}
			}
			int num = 0;
			m_OptionsTabs[num].isOn = true;
		}
		ShowChangesMade(changed: false);
	}

	public override bool GoBack()
	{
		ExitScreen();
		return false;
	}

	private void ShowChangesMade(bool changed)
	{
		if ((bool)m_ChangesMade && m_ChangesMade.activeSelf != changed)
		{
			m_ChangesMade.SetActive(changed);
		}
		m_Changed = changed;
	}

	private void ClearSettings()
	{
		if (!SKU.ConsoleUI)
		{
			for (int i = 0; i < m_OptionsTypeCount; i++)
			{
				if (m_OptionsElements[i] != null)
				{
					m_OptionsElements[i].ClearSettings();
				}
			}
		}
		else
		{
			m_ConsoleUiOption.ClearSettings();
		}
		Singleton.Manager<ManUI>.inst.PopScreen();
	}

	private void RevertToDefaultSettings()
	{
		m_ConsoleUiOption.ResetSettings();
	}

	private void OnSettingsChanged()
	{
		ShowChangesMade(changed: true);
	}

	private void ShowSaveFailedMessage(SaveFailureType failType)
	{
		if (failType == SaveFailureType.RequiredKeyRebind)
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 12);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			uIScreenNotifications.Set(localisedString, delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			}, localisedString2);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
	}

	private void Awake()
	{
		m_OptionsTypeCount = Enum.GetNames(typeof(OptionsType)).Length;
		for (int i = 0; i < m_OptionsTypeCount; i++)
		{
			int index = i;
			m_OptionsTabs[i].onValueChanged.AddListener(delegate(bool set)
			{
				if (set)
				{
					ShowOptions((OptionsType)index);
				}
			});
		}
		OnChangeEvent.Subscribe(OnSettingsChanged);
	}

	private void Update()
	{
		if (!SKU.ConsoleUI)
		{
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(41))
			{
				SelectNextOption();
			}
			if (Singleton.Manager<ManInput>.inst.GetButtonDown(42))
			{
				SelectPrevOption();
			}
		}
		else if (Singleton.Manager<ManInput>.inst.GetButtonDown(57))
		{
			RevertToDefaultSettings();
		}
		bool isPollingForKeyAssignment = Singleton.Manager<ManInput>.inst.IsPollingForKeyAssignment;
		bool flag = !base.CanExit;
		if (isPollingForKeyAssignment != flag)
		{
			if (isPollingForKeyAssignment || !m_OneFrameUnblockScreenExitDelaySet)
			{
				BlockScreenExit(isPollingForKeyAssignment);
			}
			m_OneFrameUnblockScreenExitDelaySet = isPollingForKeyAssignment;
		}
	}
}
