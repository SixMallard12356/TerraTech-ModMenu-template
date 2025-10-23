using UnityEngine;

public class uScript_CraftingUIHighlightComponentTier : uScriptLogic
{
	private bool m_FirstFrame;

	private bool m_IsSelected;

	private ToggleWrapper m_TargetButton;

	public bool Out => true;

	public bool Waiting => !m_IsSelected;

	public bool Selected => m_IsSelected;

	public void Category(ComponentTier componentTier)
	{
		if (!m_IsSelected && m_FirstFrame)
		{
			InitHelpArrow(componentTier);
			m_FirstFrame = false;
		}
	}

	private void InitHelpArrow(ComponentTier componentTier)
	{
		UIItemRecipeSelect uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockRecipeSelect) as UIItemRecipeSelect;
		if (uIItemRecipeSelect != null)
		{
			m_TargetButton = uIItemRecipeSelect.GetCategoryToggle((int)componentTier);
		}
		if (m_TargetButton != null)
		{
			m_TargetButton.OnToggled.Subscribe(OnElementToggled);
			RectTransform rectTransform = m_TargetButton.transform as RectTransform;
			RectTransform[] unmaskedTransforms = new RectTransform[1] { rectTransform };
			Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(m_TargetButton.transform, unmaskedTransforms);
			Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
			uIItemRecipeSelect.SetAllowGridScroll(enableScroll: false);
			uIItemRecipeSelect.SetElementForTutorial(UIShopBlockSelect.TutorialElement.CategorySelect);
		}
	}

	private void OnElementToggled(bool toggled)
	{
		m_IsSelected = true;
		Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
		Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
		UIItemRecipeSelect obj = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.BlockRecipeSelect) as UIItemRecipeSelect;
		obj.SetAllowGridScroll(enableScroll: true);
		obj.SetElementForTutorial(UIShopBlockSelect.TutorialElement.None);
		m_TargetButton.OnToggled.Unsubscribe(OnElementToggled);
	}

	public void OnEnable()
	{
		m_FirstFrame = true;
		m_IsSelected = false;
		m_TargetButton = null;
	}

	public void OnDisable()
	{
		if (m_TargetButton != null && !m_IsSelected)
		{
			m_TargetButton.OnToggled.Unsubscribe(OnElementToggled);
		}
	}
}
