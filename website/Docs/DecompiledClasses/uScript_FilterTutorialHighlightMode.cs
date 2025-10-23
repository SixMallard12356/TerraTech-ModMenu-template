using UnityEngine;

public class uScript_FilterTutorialHighlightMode : uScriptLogic
{
	public bool Out => true;

	public void In(UIFilterMenu.FilterAcceptMode filterMode)
	{
		UIFilterMenu uIFilterMenu = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.FilterMenu) as UIFilterMenu;
		if (uIFilterMenu != null)
		{
			UIRadialMenuOption filterModeButton = uIFilterMenu.GetFilterModeButton(filterMode);
			if (filterModeButton != null)
			{
				ShowHelpArrow(filterModeButton.transform, showTutorialHighlight: true);
			}
		}
	}

	public static void ShowHelpArrow(Transform targetTrans, bool showTutorialHighlight)
	{
		if (targetTrans != null)
		{
			RectTransform rectTransform = targetTrans as RectTransform;
			RectTransform[] unmaskedTransforms = new RectTransform[1] { rectTransform };
			Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(targetTrans, unmaskedTransforms);
			if (showTutorialHighlight)
			{
				Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
			}
			else
			{
				Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
			}
		}
	}
}
