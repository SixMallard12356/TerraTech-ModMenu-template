using UnityEngine;

public class uScript_CraftingUIHighlightItem : uScriptLogic
{
	private bool m_IsSelected;

	public bool Out => true;

	public bool Waiting => !m_IsSelected;

	public bool Selected => m_IsSelected;

	public void In(ManHUD.HUDElementType targetMenuType, ItemTypeInfo itemToHighlight)
	{
		if (m_IsSelected)
		{
			return;
		}
		UIItemRecipeSelect uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(targetMenuType) as UIItemRecipeSelect;
		if (!(uIItemRecipeSelect != null))
		{
			return;
		}
		if (uIItemRecipeSelect.GetSelectedItem() == itemToHighlight)
		{
			Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
			m_IsSelected = true;
			Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
			return;
		}
		uIItemRecipeSelect.TryShowItem(itemToHighlight);
		RectTransform itemTransform = uIItemRecipeSelect.GetItemTransform(itemToHighlight, allowNull: true);
		bool flag = itemTransform != null;
		if (flag != Singleton.Manager<ManTechBuildingTutorial>.inst.IsHelpArrowVisible())
		{
			if (flag)
			{
				RectTransform[] unmaskedTransforms = new RectTransform[2]
				{
					itemTransform,
					uIItemRecipeSelect.GetRecipeDisplayTransform()
				};
				Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(itemTransform, unmaskedTransforms);
				Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(itemTransform);
				uIItemRecipeSelect.SetAllowGridScroll(enableScroll: false);
				uIItemRecipeSelect.SetElementForTutorial(UIShopBlockSelect.TutorialElement.ItemSelect);
			}
			else
			{
				Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
				Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
				uIItemRecipeSelect.SetAllowGridScroll(enableScroll: true);
				uIItemRecipeSelect.SetElementForTutorial(UIShopBlockSelect.TutorialElement.None);
			}
		}
	}

	public void OnEnable()
	{
		m_IsSelected = false;
	}
}
