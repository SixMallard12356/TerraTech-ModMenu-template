#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingPanel : OverlayPanel, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	[SerializeField]
	private Text m_MainTitle;

	[SerializeField]
	private UIRecipe m_RecipeDisplay;

	[SerializeField]
	private RectTransform m_PopupParent;

	[SerializeField]
	private UIRecipe m_IngredientsDisplay;

	[SerializeField]
	private Text m_IngredientsTitle;

	[SerializeField]
	private CanvasGroup m_CanvasGroup;

	[SerializeField]
	private Image m_Background;

	[SerializeField]
	private float m_DimmedAlpha;

	[SerializeField]
	private float m_DefaultBackgroundAlpha;

	private ModuleItemConsume m_Consumer;

	private RecipeTable.Recipe m_DisplayedRecipe;

	public override void SetContext(object context)
	{
		m_Consumer = context as ModuleItemConsume;
		d.Assert(context == null || m_Consumer != null, "CraftingPanel - Failed to cast context to ModuleItemConsume");
		m_RecipeDisplay.FollowConsumer(m_Consumer);
		m_DisplayedRecipe = (m_Consumer ? m_Consumer.Recipe : null);
		SetTitleFromRecipe(m_DisplayedRecipe);
		if (m_PopupParent != null)
		{
			m_PopupParent.gameObject.SetActive(value: false);
		}
	}

	private void SetTitleFromRecipe(RecipeTable.Recipe curRecipe)
	{
		if (m_MainTitle != null)
		{
			if (curRecipe != null && curRecipe.m_OutputItems.Length == 1)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Crafting, 2);
				string itemName = StringLookup.GetItemName(curRecipe.m_OutputItems[0].m_Item);
				m_MainTitle.text = string.Format(localisedString, itemName);
			}
			else
			{
				m_MainTitle.text = "";
			}
		}
	}

	private void OnItemPointerEntered(UIItemDisplay item)
	{
		m_PopupParent.gameObject.SetActive(value: true);
		Vector3 position = m_PopupParent.position;
		position.x = item.RectTrans.position.x;
		m_PopupParent.position = position;
		if (m_IngredientsTitle != null)
		{
			m_IngredientsTitle.text = StringLookup.GetItemName(item.ItemType.ObjectType, item.ItemType.ItemType);
		}
		RecipeTable.Recipe recipeByOutputType = Singleton.Manager<RecipeManager>.inst.GetRecipeByOutputType(item.ItemType);
		bool active = false;
		if (recipeByOutputType != null && (m_DisplayedRecipe == null || m_DisplayedRecipe.m_OutputType != RecipeTable.Recipe.OutputType.Items || !(m_DisplayedRecipe.m_OutputItems[0].m_Item == item.ItemType)))
		{
			m_IngredientsDisplay.ShowRecipe(recipeByOutputType);
			active = true;
		}
		m_IngredientsDisplay.gameObject.SetActive(active);
	}

	private void OnItemPointerExited(UIItemDisplay item)
	{
		m_PopupParent.gameObject.SetActive(value: false);
	}

	private void OnPool()
	{
		if (m_RecipeDisplay != null && m_PopupParent != null)
		{
			m_RecipeDisplay.PointerEntered.Subscribe(OnItemPointerEntered);
			m_RecipeDisplay.PointerExited.Subscribe(OnItemPointerExited);
		}
	}

	public override void SetFocusLevel(ManHUD.FocusLevel level)
	{
		bool flag = level == ManHUD.FocusLevel.Dimmed;
		m_CanvasGroup.alpha = (flag ? m_DimmedAlpha : 1f);
		m_RecipeDisplay.SetItemOutlinesEnabled(!flag);
		if (m_Background != null)
		{
			bool flag2 = level == ManHUD.FocusLevel.Highlighted;
			m_Background.color = m_Background.color.SetAlpha(flag2 ? 1f : m_DefaultBackgroundAlpha);
		}
	}

	private void Update()
	{
		if ((bool)m_Consumer && m_Consumer.Recipe != m_DisplayedRecipe)
		{
			m_DisplayedRecipe = m_Consumer.Recipe;
			SetTitleFromRecipe(m_DisplayedRecipe);
		}
	}

	public void OnPointerEnter(PointerEventData dataName)
	{
		Singleton.Manager<ManHUD>.inst.SetFocus(this);
	}

	public void OnPointerExit(PointerEventData dataName)
	{
		Singleton.Manager<ManHUD>.inst.ClearFocus(this);
	}
}
