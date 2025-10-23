using UnityEngine;

public class uScript_GridUIHighlightItem : uScriptLogic
{
	private bool m_IsSelected;

	public bool Out => true;

	public bool Waiting => !m_IsSelected;

	public bool Selected => m_IsSelected;

	public void In(ManHUD.HUDElementType hudElement, ItemTypeInfo itemToHighlight)
	{
		if (m_IsSelected || !(Singleton.Manager<ManHUD>.inst.GetHudElement(hudElement) is IUIGridSelectTutorialHelper iUIGridSelectTutorialHelper))
		{
			return;
		}
		if (iUIGridSelectTutorialHelper.GetSelectedItem() == itemToHighlight)
		{
			Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
			m_IsSelected = true;
			Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
			return;
		}
		iUIGridSelectTutorialHelper.TryShowItem(itemToHighlight);
		RectTransform rectTransform;
		bool flag = iUIGridSelectTutorialHelper.TryGetItemTransform(itemToHighlight, allowNull: true, out rectTransform);
		if (flag != Singleton.Manager<ManTechBuildingTutorial>.inst.IsHelpArrowVisible())
		{
			if (flag)
			{
				RectTransform[] unmaskedTransforms = ComposeRectList(rectTransform, iUIGridSelectTutorialHelper);
				Singleton.Manager<ManTechBuildingTutorial>.inst.ShowHelpArrow(rectTransform, unmaskedTransforms);
				Singleton.Manager<ManUI>.inst.ShowTutorialHighlight(rectTransform);
				iUIGridSelectTutorialHelper.SetAllowGridScroll(isAllowed: false);
				iUIGridSelectTutorialHelper.SetElementForTutorial(UIShopBlockSelect.TutorialElement.ItemSelect);
			}
			else
			{
				Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
				Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
				iUIGridSelectTutorialHelper.SetAllowGridScroll(isAllowed: true);
				iUIGridSelectTutorialHelper.SetElementForTutorial(UIShopBlockSelect.TutorialElement.None);
			}
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

	public void OnEnable()
	{
		m_IsSelected = false;
	}
}
