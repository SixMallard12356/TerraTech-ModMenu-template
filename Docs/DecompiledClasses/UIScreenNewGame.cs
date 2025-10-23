#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIScreenNewGame : UIScreen
{
	[SerializeField]
	private Text m_TitleTextField;

	[SerializeField]
	private RectTransform[] m_ModeDisplayGroup;

	[SerializeField]
	private RectTransform m_ModeInfoParent;

	[SerializeField]
	private UIGameMode m_GameModes;

	private Stack<UIGameMode> m_ModeStack = new Stack<UIGameMode>();

	private UIGameMode m_RootGameModes;

	private UIGameMode m_CurrentlyPreviewedMode;

	private UIGameMode m_CurrentlySelectedMode;

	public override void ScreenInitialize(ManUI.ScreenType type)
	{
		base.ScreenInitialize(type);
		m_RootGameModes = Object.Instantiate(m_GameModes);
		m_RootGameModes.name = "Game Mode Screen elements";
		m_RootGameModes.transform.parent = base.transform;
		m_RootGameModes.Initialise();
		SetupInputHandlers(m_RootGameModes);
		PushMode(m_RootGameModes);
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		BlockScreenExit(exitBlocked: true);
		if (!fromStackPop)
		{
			if ((bool)CurrentMode())
			{
				HideModeDisplay(CurrentMode());
			}
			m_ModeStack.Clear();
		}
		if (m_ModeStack.Count == 0)
		{
			PushMode(m_RootGameModes);
		}
		if (SKU.ConsoleUI)
		{
			m_ExitButton.gameObject.SetActive(value: false);
		}
		Singleton.Manager<ManNavUI>.inst.RegisterUIInputHandler(22, OnGlobalCancel);
	}

	public override void Hide()
	{
		base.Hide();
		if ((bool)CurrentMode() && m_CurrentlySelectedMode != null)
		{
			HidePlaySettings();
		}
		Singleton.Manager<ManNavUI>.inst.UnregisterUIInputHandler(22, OnGlobalCancel);
	}

	public override bool GoBack()
	{
		bool result = true;
		if (m_CurrentlySelectedMode != null)
		{
			HidePlaySettings();
			result = false;
		}
		else if (m_ModeStack.Count > 1)
		{
			m_CurrentlySelectedMode = null;
			PopMode();
			result = false;
		}
		return result;
	}

	public void OnBackClicked()
	{
		if (m_CurrentlySelectedMode != null && m_CurrentlySelectedMode.InPlayMode)
		{
			m_CurrentlySelectedMode.HidePlaySettings();
			UIGameMode currentlySelectedMode = m_CurrentlySelectedMode;
			m_CurrentlySelectedMode = null;
			HighlightModeButton(null);
			OnModeButtonHoverEnter(currentlySelectedMode);
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && !EventSystem.current.alreadySelecting)
			{
				EventSystem.current.SetSelectedGameObject(currentlySelectedMode.gameObject);
			}
		}
		else if (m_ModeStack.Count > 1)
		{
			m_CurrentlySelectedMode = null;
			PopMode();
		}
		else
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		}
	}

	public void ApplyCurrentModeSettings()
	{
		ApplyModeSettings(m_CurrentlySelectedMode);
	}

	public void ApplyModeSettings(UIGameMode mode)
	{
		Singleton.Manager<ManGameMode>.inst.NextModeSetting = mode.GetModeSettings();
	}

	public void StartGameMode()
	{
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.NextModeSetting.SwitchToMode();
	}

	public void ApplyModeSettingsAndStart()
	{
		ApplyCurrentModeSettings();
		StartGameMode();
	}

	private void OnModeClicked(UIGameMode clickedMode)
	{
		if (clickedMode.HasSubMode())
		{
			m_CurrentlySelectedMode = null;
			PushMode(clickedMode);
		}
		else
		{
			ShowPlaySettings(clickedMode);
		}
	}

	private void InitialiseModeGroup(UIGameMode modeGroup)
	{
		SetScreenTitle(modeGroup.GetTitle());
		RectTransform rectTransform = SetDisplayGroup(modeGroup.GetSubModeDisplayType());
		if (!modeGroup.HasSubMode())
		{
			return;
		}
		d.Assert(rectTransform != null, $"UIScreenNewGame - Trying to initialise sub modes of mode {modeGroup.name}, but UIScreenNewGame on {base.name} has no valid node associated with display '{modeGroup.GetSubModeDisplayType().ToString()}'");
		if (!(rectTransform != null))
		{
			return;
		}
		bool flag = false;
		List<UIGameMode> subModes = modeGroup.GetSubModes();
		for (int i = 0; i < subModes.Count; i++)
		{
			UIGameMode uIGameMode = subModes[i];
			if (!uIGameMode.IsAvailable())
			{
				continue;
			}
			uIGameMode.ShowButton(rectTransform);
			if (flag)
			{
				continue;
			}
			flag = true;
			if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
			{
				OnModeButtonHoverEnter(uIGameMode);
				Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(uIGameMode.gameObject);
				Button button = uIGameMode.GetButton();
				if (button != null)
				{
					SelectButton(button.gameObject);
				}
			}
		}
	}

	private void HideModeDisplay(UIGameMode modeGroup)
	{
		SetDisplayGroup(UIGameMode.ModeListDisplayType.None);
		if (modeGroup.HasSubMode())
		{
			List<UIGameMode> subModes = modeGroup.GetSubModes();
			for (int i = 0; i < subModes.Count; i++)
			{
				subModes[i].Hide();
				Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(subModes[i].gameObject);
			}
		}
	}

	private void SetupInputHandlers(UIGameMode mode)
	{
		Button button = mode.GetButton();
		if (button != null)
		{
			EventTrigger eventTrigger = button.gameObject.GetComponent<EventTrigger>();
			if (eventTrigger == null)
			{
				eventTrigger = button.gameObject.AddComponent<EventTrigger>();
			}
			button.onClick.AddListener(delegate
			{
				OnModeClicked(mode);
			});
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerEnter;
			entry.callback.AddListener(delegate
			{
				OnModeButtonHoverEnter(mode);
			});
			EventTrigger.Entry entry2 = new EventTrigger.Entry();
			entry2.eventID = EventTriggerType.PointerExit;
			entry2.callback.AddListener(delegate
			{
				OnModeButtonHoverExit(mode);
			});
			EventTrigger.Entry entry3 = new EventTrigger.Entry();
			entry3.eventID = EventTriggerType.Select;
			entry3.callback.AddListener(delegate
			{
				OnModeButtonSelect(mode);
			});
			EventTrigger.Entry entry4 = new EventTrigger.Entry();
			entry4.eventID = EventTriggerType.Deselect;
			entry4.callback.AddListener(delegate
			{
				OnModeButtonDeselect(mode);
			});
			EventTrigger.Entry entry5 = new EventTrigger.Entry();
			entry5.eventID = EventTriggerType.Submit;
			entry5.callback.AddListener(delegate
			{
				OnModeButtonSubmit(mode);
			});
			eventTrigger.triggers.Add(entry);
			eventTrigger.triggers.Add(entry2);
			eventTrigger.triggers.Add(entry3);
			eventTrigger.triggers.Add(entry4);
			eventTrigger.triggers.Add(entry5);
		}
		if (mode.HasSubMode())
		{
			List<UIGameMode> subModes = mode.GetSubModes();
			for (int num = 0; num < subModes.Count; num++)
			{
				SetupInputHandlers(subModes[num]);
			}
		}
	}

	private RectTransform SetDisplayGroup(UIGameMode.ModeListDisplayType groupType)
	{
		RectTransform result = null;
		int num = EnumValuesIterator<UIGameMode.ModeListDisplayType>.Count - 1;
		for (int i = 0; i < num; i++)
		{
			bool flag = i + 1 == (int)groupType;
			m_ModeDisplayGroup[i].gameObject.SetActive(flag);
			if (flag)
			{
				result = m_ModeDisplayGroup[i];
				break;
			}
		}
		return result;
	}

	private void SetScreenTitle(string titleText)
	{
		m_TitleTextField.text = titleText.ToUpper();
	}

	private void PushMode(UIGameMode mode)
	{
		if ((bool)CurrentMode())
		{
			HideModeDisplay(CurrentMode());
		}
		m_ModeStack.Push(mode);
		InitialiseModeGroup(mode);
	}

	private void PopMode()
	{
		HideModeDisplay(CurrentMode());
		m_ModeStack.Pop();
		InitialiseModeGroup(CurrentMode());
	}

	private void ShowInfo(UIGameMode focusedMode)
	{
		focusedMode.ShowInfo(m_ModeInfoParent);
	}

	private void ShowPlaySettings(UIGameMode focussedMode)
	{
		if (m_CurrentlyPreviewedMode == null)
		{
			m_CurrentlyPreviewedMode.HideInfo();
			m_CurrentlyPreviewedMode = null;
		}
		if (m_CurrentlySelectedMode != null)
		{
			m_CurrentlySelectedMode.HidePlaySettings();
		}
		focussedMode.HideInfo();
		focussedMode.ShowPlaySettings(m_ModeInfoParent);
		HighlightModeButton(focussedMode);
		SetScreenTitle(focussedMode.GetTitle());
		m_CurrentlySelectedMode = focussedMode;
	}

	private void HidePlaySettings()
	{
		m_CurrentlySelectedMode.HidePlaySettings();
		m_CurrentlySelectedMode = null;
		if (m_CurrentlyPreviewedMode != null)
		{
			ShowInfo(m_CurrentlyPreviewedMode);
		}
		HighlightModeButton(null);
		SetScreenTitle(CurrentMode().GetTitle());
	}

	private void HighlightModeButton(UIGameMode modeToHighlight)
	{
		List<UIGameMode> subModes = CurrentMode().GetSubModes();
		for (int i = 0; i < subModes.Count; i++)
		{
			subModes[i].OnModeHighlighted(modeToHighlight);
		}
	}

	private UIGameMode CurrentMode()
	{
		if (m_ModeStack != null && m_ModeStack.Count > 0)
		{
			return m_ModeStack.Peek();
		}
		return null;
	}

	private void OnModeButtonHoverEnter(UIGameMode mode)
	{
		if (SKU.ConsoleUI)
		{
			Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.NewGame);
		}
		if (m_CurrentlyPreviewedMode != null)
		{
			m_CurrentlyPreviewedMode.HideInfo();
		}
		m_CurrentlyPreviewedMode = mode;
		if (m_CurrentlySelectedMode == null)
		{
			ShowInfo(mode);
		}
	}

	private void OnModeButtonHoverExit(UIGameMode mode)
	{
	}

	private void OnModeButtonSelect(UIGameMode mode)
	{
		OnModeButtonHoverEnter(mode);
	}

	private void OnModeButtonDeselect(UIGameMode mode)
	{
		OnModeButtonHoverExit(mode);
	}

	private void OnModeButtonSubmit(UIGameMode mode)
	{
		if (CurrentMode() != mode)
		{
			OnModeClicked(mode);
		}
	}

	private void OnGlobalCancel(PayloadUIEventData eventData)
	{
		OnBackClicked();
		eventData.Use();
	}

	private void Update()
	{
	}
}
