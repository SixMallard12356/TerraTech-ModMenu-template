#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class uScript_CraftingUIHighlightRepeatButton : uScriptLogic
{
	private bool m_FirstFrame;

	private bool m_HasBeenToggledToRequiredState;

	private bool m_RequiredToggleState;

	private Toggle m_TargetToggle;

	private ManHUD.HUDElementType m_TargetMenuType;

	public bool Out => true;

	public bool Waiting => !m_HasBeenToggledToRequiredState;

	public bool ToggledToRequiredState => m_HasBeenToggledToRequiredState;

	public void In([DefaultValue(true)] bool requiredToggleState = true, [DefaultValue(ManHUD.HUDElementType.BlockRecipeSelect)] ManHUD.HUDElementType targetMenuType = ManHUD.HUDElementType.BlockRecipeSelect)
	{
		if (!m_HasBeenToggledToRequiredState && m_FirstFrame)
		{
			m_TargetMenuType = targetMenuType;
			InitHelpArrow();
			m_RequiredToggleState = requiredToggleState;
			m_FirstFrame = false;
		}
	}

	private void InitHelpArrow()
	{
		UIItemRecipeSelect uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(m_TargetMenuType) as UIItemRecipeSelect;
		if (uIItemRecipeSelect != null)
		{
			m_TargetToggle = uIItemRecipeSelect.GetRepeatToggle();
		}
		else
		{
			d.LogError(string.Concat("uScript_CraftingUIHighlightCraftButton - HUD element of type ", m_TargetMenuType, " was not of type UIItemRecipeSelect or any of its derived types!"));
		}
		if (m_TargetToggle != null)
		{
			m_TargetToggle.onValueChanged.AddListener(OnToggled);
			RectTransform rectTransform = m_TargetToggle.transform as RectTransform;
			RectTransform[] unmaskedTransforms = new RectTransform[2]
			{
				rectTransform,
				uIItemRecipeSelect.GetRecipeDisplayTransform()
			};
			Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(m_TargetToggle.transform, unmaskedTransforms);
			Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
			uIItemRecipeSelect.SetAllowGridScroll(enableScroll: false);
			uIItemRecipeSelect.SetElementForTutorial(UIShopBlockSelect.TutorialElement.RepeatToggle);
		}
	}

	private void OnToggled(bool toggled)
	{
		if (m_RequiredToggleState == toggled)
		{
			m_HasBeenToggledToRequiredState = true;
			Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
			Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
			UIItemRecipeSelect obj = Singleton.Manager<ManHUD>.inst.GetHudElement(m_TargetMenuType) as UIItemRecipeSelect;
			obj.SetAllowGridScroll(enableScroll: true);
			obj.SetElementForTutorial(UIShopBlockSelect.TutorialElement.None);
			m_TargetToggle.onValueChanged.RemoveListener(OnToggled);
		}
	}

	public void OnEnable()
	{
		m_FirstFrame = true;
		m_HasBeenToggledToRequiredState = false;
		m_TargetToggle = null;
	}

	public void OnDisable()
	{
		if ((bool)m_TargetToggle && !m_HasBeenToggledToRequiredState)
		{
			m_TargetToggle.onValueChanged.RemoveListener(OnToggled);
		}
	}
}
