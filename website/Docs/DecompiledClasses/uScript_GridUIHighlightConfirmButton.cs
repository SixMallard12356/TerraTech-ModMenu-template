#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class uScript_GridUIHighlightConfirmButton : uScriptLogic
{
	private bool m_FirstFrame;

	private bool m_IsSelected;

	private Button m_TargetButton;

	private ManHUD.HUDElementType m_HudElement;

	public bool Out => true;

	public bool Waiting => !m_IsSelected;

	public bool Selected => m_IsSelected;

	public void In([DefaultValue(ManHUD.HUDElementType.BlockShop)] ManHUD.HUDElementType hudElement)
	{
		if (!m_IsSelected && m_FirstFrame)
		{
			m_HudElement = hudElement;
			InitHelpArrow();
			m_FirstFrame = false;
		}
	}

	private void InitHelpArrow()
	{
		IUIGridSelectTutorialHelper iUIGridSelectTutorialHelper = Singleton.Manager<ManHUD>.inst.GetHudElement(m_HudElement) as IUIGridSelectTutorialHelper;
		if (iUIGridSelectTutorialHelper != null)
		{
			m_TargetButton = iUIGridSelectTutorialHelper.GetConfirmButton();
		}
		else
		{
			d.LogError(string.Concat("uScript_GridUIHighlightConfirmButton - HUD element of type ", m_HudElement, " was not of type UIItemRecipeSelect or any of its derived types!"));
		}
		if (m_TargetButton != null)
		{
			m_TargetButton.onClick.AddListener(OnButtonClicked);
			RectTransform rectTransform = m_TargetButton.transform as RectTransform;
			RectTransform[] unmaskedTransforms = ComposeRectList(rectTransform, iUIGridSelectTutorialHelper);
			Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(m_TargetButton.transform, unmaskedTransforms);
			Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
			iUIGridSelectTutorialHelper.SetAllowGridScroll(isAllowed: false);
			iUIGridSelectTutorialHelper.SetElementForTutorial(UIShopBlockSelect.TutorialElement.Submit);
		}
	}

	private RectTransform[] ComposeRectList(RectTransform target, IUIGridSelectTutorialHelper parent)
	{
		RectTransform additionalItemTransform = parent.GetAdditionalItemTransform();
		if (!(additionalItemTransform != null))
		{
			return new RectTransform[1] { target };
		}
		return new RectTransform[2] { target, additionalItemTransform };
	}

	private void OnButtonClicked()
	{
		m_IsSelected = true;
		Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
		Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
		IUIGridSelectTutorialHelper obj = Singleton.Manager<ManHUD>.inst.GetHudElement(m_HudElement) as IUIGridSelectTutorialHelper;
		obj.SetAllowGridScroll(isAllowed: true);
		obj.SetElementForTutorial(UIShopBlockSelect.TutorialElement.None);
		m_TargetButton.onClick.RemoveListener(OnButtonClicked);
	}

	public void OnEnable()
	{
		m_FirstFrame = true;
		m_IsSelected = false;
		m_TargetButton = null;
	}

	public void OnDisable()
	{
		if ((bool)m_TargetButton && !m_IsSelected)
		{
			m_TargetButton.onClick.RemoveListener(OnButtonClicked);
		}
	}
}
