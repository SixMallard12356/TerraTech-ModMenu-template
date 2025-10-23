#define UNITY_EDITOR
using UnityEngine;

public class uScript_CraftingUIHighlightRecipeIngredient : uScriptLogic
{
	private bool m_FirstFrame;

	public bool Out => true;

	public void In(ManHUD.HUDElementType targetMenuType, int ingredientNumber)
	{
		if (m_FirstFrame)
		{
			m_FirstFrame = false;
			RectTransform rectTransform = null;
			UIItemRecipeSelect uIItemRecipeSelect = Singleton.Manager<ManHUD>.inst.GetHudElement(targetMenuType) as UIItemRecipeSelect;
			if (uIItemRecipeSelect != null)
			{
				d.Assert(ingredientNumber > 0, "uScript_CraftingUIHighlightRecipeIngredient - Ingredient number (" + ingredientNumber + ") was invalid; Expected 1 or higher.");
				int ingredientIndex = ingredientNumber - 1;
				rectTransform = uIItemRecipeSelect.GetRecipeIngredientTransform(ingredientIndex);
			}
			else
			{
				d.LogError(string.Concat("uScript_CraftingUIHighlightRecipeIngredient - HUD element of type ", targetMenuType, " was not of type UIItemRecipeSelect or any of its derived types!"));
			}
			if (rectTransform != null)
			{
				RectTransform[] unmaskedTransforms = new RectTransform[1] { rectTransform };
				Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(rectTransform, unmaskedTransforms);
				Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
				uIItemRecipeSelect.SetAllowGridScroll(enableScroll: false);
				uIItemRecipeSelect.SetElementForTutorial(UIShopBlockSelect.TutorialElement.All);
			}
			else
			{
				d.LogError("uScript_CraftingUIHighlightRecipeIngredient - Could not find a transform for ingredient nr " + ingredientNumber + ". Did the recipe require enough ingredients?");
			}
		}
	}

	public void OnEnable()
	{
		m_FirstFrame = true;
	}
}
