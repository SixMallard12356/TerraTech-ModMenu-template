using UnityEngine;

public class uScript_GridUIHighlightBlockCategory : uScriptLogic
{
	private bool m_FirstFrame;

	private bool m_IsSelected;

	private ManHUD.HUDElementType m_HudElement;

	private ToggleWrapper m_TargetButton;

	public bool Out => true;

	public bool Waiting => !m_IsSelected;

	public bool Selected => m_IsSelected;

	public void AllCategory(ManHUD.HUDElementType hudElement, BlockCategories unused)
	{
		if (!m_IsSelected && m_FirstFrame)
		{
			InitHelpArrow(hudElement, BlockCategories.Null);
			m_FirstFrame = false;
		}
		if (m_TargetButton != null && !m_IsSelected && m_TargetButton.isOn)
		{
			OnElementToggled(toggled: true);
		}
	}

	public void Category(ManHUD.HUDElementType hudElement, BlockCategories blockCategory)
	{
		if (!m_IsSelected && m_FirstFrame)
		{
			InitHelpArrow(hudElement, blockCategory);
			m_FirstFrame = false;
		}
		if (m_TargetButton != null && !m_IsSelected && m_TargetButton.isOn)
		{
			ToggleWrapper toggleWrapper = ((Singleton.Manager<ManHUD>.inst.GetHudElement(hudElement) is IUIGridSelectTutorialHelper iUIGridSelectTutorialHelper) ? iUIGridSelectTutorialHelper.GetCategoryToggle(0) : null);
			if (toggleWrapper == null || !toggleWrapper.isOn)
			{
				OnElementToggled(toggled: true);
			}
		}
	}

	private void InitHelpArrow(ManHUD.HUDElementType hudElement, BlockCategories category)
	{
		m_HudElement = hudElement;
		IUIGridSelectTutorialHelper iUIGridSelectTutorialHelper = Singleton.Manager<ManHUD>.inst.GetHudElement(hudElement) as IUIGridSelectTutorialHelper;
		if (iUIGridSelectTutorialHelper != null)
		{
			m_TargetButton = iUIGridSelectTutorialHelper.GetCategoryToggle((int)category);
		}
		if (m_TargetButton != null)
		{
			m_TargetButton.OnToggled.Subscribe(OnElementToggled);
			RectTransform rectTransform = m_TargetButton.transform as RectTransform;
			RectTransform[] unmaskedTransforms = new RectTransform[1] { rectTransform };
			Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(m_TargetButton.transform, unmaskedTransforms);
			iUIGridSelectTutorialHelper.SetAllowGridScroll(isAllowed: false);
			iUIGridSelectTutorialHelper.SetElementForTutorial(UIShopBlockSelect.TutorialElement.CategorySelect);
			Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
		}
	}

	private void OnElementToggled(bool toggled)
	{
		m_IsSelected = true;
		Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
		IUIGridSelectTutorialHelper obj = Singleton.Manager<ManHUD>.inst.GetHudElement(m_HudElement) as IUIGridSelectTutorialHelper;
		obj.SetAllowGridScroll(isAllowed: true);
		obj.SetElementForTutorial(UIShopBlockSelect.TutorialElement.None);
		Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
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
