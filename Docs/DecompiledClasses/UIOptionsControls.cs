using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsControls : UIOptions
{
	[SerializeField]
	private Toggle m_DisableControllers;

	[SerializeField]
	private Toggle m_ConsoleStyleJoypad;

	[SerializeField]
	private GameObject m_RequireMappingWarningPrefab;

	[SerializeField]
	private ScrollRect m_ControlsScrollRect;

	private UIKeyBindDisplay[] m_KeyBindButtons;

	private string m_CachedControllerMapping;

	private bool m_Initialised;

	private UIOptionsBehaviourKeyBinding[] m_KeyBindingBehaviours;

	private EventNoParams ChangesMadeEventToCall;

	public void RestoreDefaultMapping()
	{
		Singleton.Manager<ManInput>.inst.CancelKeyAssign();
		UIScreenNotifications uIScreenNotifications = (UIScreenNotifications)Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.ControlOptions, 34);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
				DoRestoreDefaultMapping();
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 22
		};
		uIScreenNotifications.Set(localisedString, accept, decline);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	public void DoRestoreDefaultMapping()
	{
		Singleton.Manager<ManInput>.inst.RestoreDefaults();
		Singleton.Manager<ManInput>.inst.EnableAllControllerMapsOfType(!m_DisableControllers.isOn, ControllerType.Joystick);
		UIKeyBindDisplay[] keyBindButtons = m_KeyBindButtons;
		for (int i = 0; i < keyBindButtons.Length; i++)
		{
			keyBindButtons[i].Refresh();
		}
		RemoveAllWarnings();
		ChangesMadeEventToCall.Send();
	}

	public override void Setup(EventNoParams onChangeEvent)
	{
		ChangesMadeEventToCall = onChangeEvent;
		Init();
		m_ConsoleStyleJoypad.transform.parent.gameObject.SetActive(Singleton.Manager<ManInput>.inst.CanSwitchInputModeAtRuntime());
		m_CachedControllerMapping = Singleton.Manager<ManInput>.inst.SaveControllerMaps();
		m_DisableControllers.isOn = Globals.inst.m_DisableControllers;
		m_ConsoleStyleJoypad.isOn = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		UIKeyBindDisplay[] keyBindButtons = m_KeyBindButtons;
		for (int i = 0; i < keyBindButtons.Length; i++)
		{
			keyBindButtons[i].Setup();
		}
	}

	public override void SaveSettings()
	{
		Globals.inst.m_DisableControllers = m_DisableControllers.isOn;
		Singleton.Manager<ManInput>.inst.SetUseConsoleStyleJoypad(m_ConsoleStyleJoypad.isOn);
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			currentUser.m_ControllerSettings.m_DisableControllers = m_DisableControllers.isOn;
			currentUser.m_ControllerSettings.m_ConsoleStyleJoypad = m_ConsoleStyleJoypad.isOn;
			currentUser.m_ControllerSettings.m_ControllerMapping = Singleton.Manager<ManInput>.inst.SaveControllerMaps();
		}
	}

	public override UIScreenOptions.SaveFailureType CanSave()
	{
		if (CheckForAnyUnmappedRequiredKeys())
		{
			return UIScreenOptions.SaveFailureType.RequiredKeyRebind;
		}
		return UIScreenOptions.SaveFailureType.None;
	}

	public override void ClearSettings()
	{
		Singleton.Manager<ManInput>.inst.LoadControllerMaps(m_CachedControllerMapping);
		Singleton.Manager<ManInput>.inst.SetInitialControllerMaps();
		if (m_CachedControllerMapping != null)
		{
			m_CachedControllerMapping = null;
		}
	}

	public override void ResetSettings()
	{
	}

	public override void OnCloseScreen()
	{
		RemoveAllWarnings();
	}

	private void Init()
	{
		if (!m_Initialised)
		{
			m_KeyBindingBehaviours = GetComponentsInChildren<UIOptionsBehaviourKeyBinding>(includeInactive: true);
			UIOptionsBehaviourKeyBinding[] keyBindingBehaviours = m_KeyBindingBehaviours;
			for (int i = 0; i < keyBindingBehaviours.Length; i++)
			{
				keyBindingBehaviours[i].Init();
			}
			m_KeyBindButtons = GetComponentsInChildren<UIKeyBindDisplay>(includeInactive: true);
			UIKeyBindDisplay[] keyBindButtons = m_KeyBindButtons;
			for (int i = 0; i < keyBindButtons.Length; i++)
			{
				keyBindButtons[i].OnKeyBindChanged.Subscribe(OnButtonChanged);
			}
			m_DisableControllers.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_DisableControllers.onValueChanged.AddListener(delegate(bool x)
			{
				Singleton.Manager<ManInput>.inst.EnableAllControllerMapsOfType(!x, ControllerType.Joystick);
			});
			m_ConsoleStyleJoypad.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_Initialised = true;
		}
	}

	private void RemoveAllWarnings()
	{
		if (m_KeyBindingBehaviours != null)
		{
			UIOptionsBehaviourKeyBinding[] keyBindingBehaviours = m_KeyBindingBehaviours;
			for (int i = 0; i < keyBindingBehaviours.Length; i++)
			{
				keyBindingBehaviours[i].RemoveWarning();
			}
		}
	}

	private void OnButtonChanged(UIKeyBindDisplay button)
	{
		if (m_KeyBindingBehaviours != null)
		{
			UIOptionsBehaviourKeyBinding[] keyBindingBehaviours = m_KeyBindingBehaviours;
			foreach (UIOptionsBehaviourKeyBinding uIOptionsBehaviourKeyBinding in keyBindingBehaviours)
			{
				if (uIOptionsBehaviourKeyBinding.IsKeyBindingRequired() && uIOptionsBehaviourKeyBinding.ContainsButton(button))
				{
					if (uIOptionsBehaviourKeyBinding.CountAssignedKeys() == 0)
					{
						uIOptionsBehaviourKeyBinding.ShowWarning(m_RequireMappingWarningPrefab);
					}
					else
					{
						uIOptionsBehaviourKeyBinding.RemoveWarning();
					}
				}
			}
		}
		ChangesMadeEventToCall.Send();
	}

	private bool CheckForAnyUnmappedRequiredKeys()
	{
		if (m_KeyBindingBehaviours != null)
		{
			UIOptionsBehaviourKeyBinding[] keyBindingBehaviours = m_KeyBindingBehaviours;
			foreach (UIOptionsBehaviourKeyBinding uIOptionsBehaviourKeyBinding in keyBindingBehaviours)
			{
				if (uIOptionsBehaviourKeyBinding.IsKeyBindingRequired() && uIOptionsBehaviourKeyBinding.CountAssignedKeys() == 0)
				{
					if ((bool)m_ControlsScrollRect)
					{
						UIHelpers.VertScrollToItem(m_ControlsScrollRect.content, uIOptionsBehaviourKeyBinding.GetComponent<RectTransform>(), m_ControlsScrollRect.viewport.rect.height);
					}
					return true;
				}
			}
		}
		return false;
	}
}
