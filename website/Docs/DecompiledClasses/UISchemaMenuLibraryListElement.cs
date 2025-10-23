using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISchemaMenuLibraryListElement : MonoBehaviour
{
	[SerializeField]
	private CustomExplicitNavigationButton m_Button;

	[SerializeField]
	private GameObject m_ActiveHighlight;

	[SerializeField]
	private Text m_Text;

	[SerializeField]
	private Toggle m_Toggle;

	private Event<ControlScheme> OnClicked;

	private Event<ControlScheme, bool> OnToggled;

	private ControlScheme m_Scheme;

	public void Setup(ControlScheme scheme, bool isAvailableOnTech, Action<ControlScheme> clickedCallback, Action<ControlScheme, bool> toggledCallback)
	{
		m_Scheme = scheme;
		UpdateText();
		m_Toggle.SetValue(isAvailableOnTech);
		OnClicked.Subscribe(clickedCallback);
		OnToggled.Subscribe(toggledCallback);
	}

	public bool MatchesControlScheme(ControlScheme controlScheme)
	{
		return m_Scheme == controlScheme;
	}

	public void SetToggleToMatch(List<ControlScheme> techSchemes)
	{
		m_Toggle.SetValue(techSchemes.Contains(m_Scheme));
	}

	public void SetHighlighted(bool highlight)
	{
		if (highlight)
		{
			AddDynamicButtonPrompts();
		}
		if ((bool)m_ActiveHighlight)
		{
			m_ActiveHighlight.SetActive(highlight);
		}
	}

	public void AddDynamicButtonPrompts()
	{
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSelect);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
		promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextToggle);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
		promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(GetDesiredRestoreDeletePrompt());
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
	}

	public void UpdateText()
	{
		m_Text.text = m_Scheme.GetName();
	}

	public CustomExplicitNavigationButton GetButton()
	{
		return m_Button;
	}

	private ManBtnPrompt.PromptType GetDesiredRestoreDeletePrompt()
	{
		if (!m_Scheme.IsCustom)
		{
			return ManBtnPrompt.PromptType.ContextSchemeRestore;
		}
		return ManBtnPrompt.PromptType.ContextSchemeDelete;
	}

	private void OnClick()
	{
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			m_Toggle.SetValue(!m_Toggle.isOn);
			OnToggled.Send(m_Scheme, m_Toggle.isOn);
		}
		else
		{
			OnClicked.Send(m_Scheme);
		}
	}

	private void OnToggleChanged(bool state)
	{
		OnToggled.Send(m_Scheme, state);
	}

	private void OnSelected(bool selected)
	{
		if (selected)
		{
			OnClicked.Send(m_Scheme);
			return;
		}
		ManBtnPrompt.PromptData promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextToggle);
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
		promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(GetDesiredRestoreDeletePrompt());
		Singleton.Manager<ManUI>.inst.ToggleBtnPrompt(active: false, promptDataByType.prompts[0].m_InlineGlyphs);
		promptDataByType = Singleton.Manager<ManBtnPrompt>.inst.GetPromptDataByType(ManBtnPrompt.PromptType.ContextSelect);
		Singleton.Manager<ManUI>.inst.UpdateScreenPrompt(promptDataByType);
		Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
	}

	private void OnRecycle()
	{
		m_Button.onClick.RemoveListener(OnClick);
		m_Button.OnSelected.Unsubscribe(OnSelected);
		m_Toggle.onValueChanged.RemoveListener(OnToggleChanged);
		OnClicked.Clear();
		OnToggled.Clear();
	}

	private void OnSpawn()
	{
		m_Button.onClick.AddListener(OnClick);
		m_Toggle.onValueChanged.AddListener(OnToggleChanged);
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			m_Button.OnSelected.Subscribe(OnSelected);
		}
		SetHighlighted(highlight: false);
	}
}
