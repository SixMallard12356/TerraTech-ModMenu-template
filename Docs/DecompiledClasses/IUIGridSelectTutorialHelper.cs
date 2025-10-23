using UnityEngine;
using UnityEngine.UI;

public interface IUIGridSelectTutorialHelper
{
	ToggleWrapper GetCategoryToggle(int category);

	void SetAllowGridScroll(bool isAllowed);

	void SetElementForTutorial(UIShopBlockSelect.TutorialElement element);

	void TryShowItem(ItemTypeInfo itemTypeInfo);

	ItemTypeInfo GetSelectedItem();

	bool TryGetItemTransform(ItemTypeInfo itemTypeInfo, bool allowNull, out RectTransform rectTransform);

	RectTransform GetAdditionalItemTransform();

	Button GetConfirmButton();
}
