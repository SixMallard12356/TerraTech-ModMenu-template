#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class uScript_CraftingUIHighlightCraftButton : uScriptLogic
{
	private bool m_FirstFrame;

	private bool m_IsSelected;

	private Button m_TargetButton;

	private ManHUD.HUDElementType m_TargetMenuType;

	public bool Out => true;

	public bool Waiting => !m_IsSelected;

	public bool Selected => m_IsSelected;

	public void In([DefaultValue(ManHUD.HUDElementType.BlockRecipeSelect)] ManHUD.HUDElementType targetMenuType = ManHUD.HUDElementType.BlockRecipeSelect)
	{
		if (!m_IsSelected && m_FirstFrame)
		{
			m_TargetMenuType = targetMenuType;
			InitHelpArrow();
			m_FirstFrame = false;
		}
	}

	private void InitHelpArrow()
	{
		UIItemRecipeSelect uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(m_TargetMenuType) as UIItemRecipeSelect;
		if (uIItemRecipeSelect != null)
		{
			m_TargetButton = uIItemRecipeSelect.GetCraftButton();
		}
		else
		{
			d.LogError(string.Concat("uScript_CraftingUIHighlightCraftButton - HUD element of type ", m_TargetMenuType, " was not of type UIItemRecipeSelect or any of its derived types!"));
		}
		if (m_TargetButton != null)
		{
			m_TargetButton.onClick.AddListener(OnButtonClicked);
			RectTransform rectTransform = m_TargetButton.transform as RectTransform;
			RectTransform[] unmaskedTransforms = new RectTransform[2]
			{
				rectTransform,
				uIItemRecipeSelect.GetRecipeDisplayTransform()
			};
			Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(m_TargetButton.transform, unmaskedTransforms);
			Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
			uIItemRecipeSelect.SetAllowGridScroll(enableScroll: false);
			uIItemRecipeSelect.SetElementForTutorial(UIShopBlockSelect.TutorialElement.Submit);
		}
	}

	private void OnButtonClicked()
	{
		m_IsSelected = true;
		Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
		Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
		UIItemRecipeSelect obj = Singleton.Manager<ManHUD>.inst.GetHudElement(m_TargetMenuType) as UIItemRecipeSelect;
		obj.SetAllowGridScroll(enableScroll: true);
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
