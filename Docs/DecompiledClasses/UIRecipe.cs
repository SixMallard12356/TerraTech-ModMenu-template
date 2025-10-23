#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class UIRecipe : MonoBehaviour
{
	public class ItemSpecValueComparer : IComparer<RecipeTable.Recipe.ItemSpec>
	{
		public int Compare(RecipeTable.Recipe.ItemSpec specA, RecipeTable.Recipe.ItemSpec specB)
		{
			int itemValue = specA.m_Item.GetItemValue();
			int itemValue2 = specB.m_Item.GetItemValue();
			return itemValue.CompareTo(itemValue2);
		}
	}

	[SerializeField]
	private UIItemDisplay m_IngredientPrefab;

	[SerializeField]
	private UIItemDisplay m_ResultPrefab;

	[SerializeField]
	private Transform m_PlusSymbolPrefab;

	[SerializeField]
	private Transform m_EqualsSymbolPrefab;

	[SerializeField]
	private Transform m_ResultDisplayBox;

	[SerializeField]
	private Color m_DefaultQuantityColour = Color.white;

	[SerializeField]
	private Color m_FullQuantityColour = Color.white;

	[SerializeField]
	private bool m_DisplaysSingleQuantity;

	public Event<UIItemDisplay> PointerEntered;

	public Event<UIItemDisplay> PointerExited;

	private List<Transform> m_OperatorElements = new List<Transform>();

	private List<UIItemDisplay> m_ItemElements = new List<UIItemDisplay>();

	private ModuleItemConsume m_Consumer;

	private RecipeTable.Recipe m_CurRecipe;

	private int m_CurNumInputsRemaining;

	private Bitfield<int> m_CurInputWarningBitfield = new Bitfield<int>();

	private bool m_ItemOutlinesEnabled;

	private bool m_DisplayItemNames;

	public RectTransform RectTrans { get; private set; }

	public void FollowConsumer(ModuleItemConsume consume)
	{
		m_Consumer = consume;
		if ((bool)consume)
		{
			UpdateConsumerRecipe();
		}
		else
		{
			Clear();
		}
	}

	public void ShowRecipe(RecipeTable.Recipe recipe)
	{
		m_Consumer = null;
		m_CurRecipe = null;
		m_CurNumInputsRemaining = 0;
		m_CurInputWarningBitfield.Clear();
		DisplayRecipe(recipe, null, m_CurInputWarningBitfield);
	}

	public void Clear()
	{
		ShowRecipe(null);
	}

	public Transform GetIngredientTransform(int ingredientIndex)
	{
		if (ingredientIndex >= m_ItemElements.Count)
		{
			return null;
		}
		return m_ItemElements[ingredientIndex].RectTrans;
	}

	private void UpdateConsumerRecipe()
	{
		d.Assert(m_Consumer != null, "UIRecipe.UpdateConsumerRecipe has null consumer");
		RecipeTable.Recipe recipe = m_Consumer.Recipe;
		List<ItemTypeInfo> inputsRemaining = m_Consumer.InputsRemaining;
		int num = inputsRemaining?.Count ?? 0;
		Bitfield<int> wantedItemWarningBitfield = m_Consumer.WantedItemWarningBitfield;
		if (m_CurRecipe != recipe || m_CurNumInputsRemaining != num || m_CurInputWarningBitfield != wantedItemWarningBitfield)
		{
			DisplayRecipe(recipe, inputsRemaining, wantedItemWarningBitfield);
			m_CurRecipe = recipe;
			m_CurNumInputsRemaining = num;
			m_CurInputWarningBitfield.Assign(wantedItemWarningBitfield);
		}
	}

	private void DisplayRecipe(RecipeTable.Recipe recipe, List<ItemTypeInfo> inputsRemaining, Bitfield<int> inputWarningBitfield)
	{
		RecycleElements();
		if (recipe == null)
		{
			return;
		}
		if (m_IngredientPrefab != null)
		{
			RecipeTable.Recipe.ItemSpec[] inputItems = recipe.m_InputItems;
			for (int i = 0; i < inputItems.Length; i++)
			{
				ItemTypeInfo item = inputItems[i].m_Item;
				if (i > 0)
				{
					TryAddPlusSign();
				}
				Color quantityTextColour = m_DefaultQuantityColour;
				string quantityText;
				if (inputsRemaining != null)
				{
					int num = 0;
					for (int j = 0; j < inputsRemaining.Count; j++)
					{
						if (inputsRemaining[j] == item)
						{
							num++;
						}
					}
					int quantity = inputItems[i].m_Quantity;
					int num2 = quantity - num;
					quantityText = num2 + "/" + quantity;
					if (num2 >= quantity)
					{
						quantityTextColour = m_FullQuantityColour;
					}
				}
				else
				{
					quantityText = ((m_DisplaysSingleQuantity || inputItems[i].m_Quantity > 1) ? inputItems[i].m_Quantity.ToString() : "");
				}
				bool showWarningIcon = inputWarningBitfield.Contains(i);
				AddIngredientToDisplay(inputItems[i], quantityText, quantityTextColour, showWarningIcon, m_IngredientPrefab, base.transform);
			}
			if (m_EqualsSymbolPrefab != null)
			{
				Transform transform = m_EqualsSymbolPrefab.Spawn();
				m_OperatorElements.Add(transform);
				transform.SetParent(base.transform, worldPositionStays: false);
			}
		}
		if (!(m_ResultPrefab != null))
		{
			return;
		}
		RecipeTable.Recipe.ItemSpec[] outputItems = recipe.m_OutputItems;
		for (int k = 0; k < outputItems.Length; k++)
		{
			if (k > 0)
			{
				TryAddPlusSign();
			}
			Transform parent = (m_ResultDisplayBox ? m_ResultDisplayBox : base.transform);
			bool showWarningIcon2 = false;
			AddIngredientToDisplay(outputItems[k], "", Color.white, showWarningIcon2, m_ResultPrefab, parent);
		}
	}

	public void SetItemOutlinesEnabled(bool enabled)
	{
		for (int i = 0; i < m_ItemElements.Count; i++)
		{
			m_ItemElements[i].SetOutlinesEnabled(enabled);
		}
		m_ItemOutlinesEnabled = enabled;
	}

	public void SetDisplayItemNames(bool displayNames)
	{
		for (int i = 0; i < m_ItemElements.Count; i++)
		{
			m_ItemElements[i].SetDisplayItemName(displayNames);
		}
		m_DisplayItemNames = displayNames;
	}

	private void RecycleElements()
	{
		for (int i = 0; i < m_OperatorElements.Count; i++)
		{
			m_OperatorElements[i].SetParent(null, worldPositionStays: false);
			m_OperatorElements[i].Recycle();
		}
		m_OperatorElements.Clear();
		for (int j = 0; j < m_ItemElements.Count; j++)
		{
			UIItemDisplay uIItemDisplay = m_ItemElements[j];
			uIItemDisplay.PointerEntered.Unsubscribe(RoutePointerEntered);
			uIItemDisplay.PointerExited.Unsubscribe(RoutePointerExited);
			uIItemDisplay.RectTrans.SetParent(null, worldPositionStays: false);
			uIItemDisplay.Recycle();
		}
		m_ItemElements.Clear();
	}

	private void AddIngredientToDisplay(RecipeTable.Recipe.ItemSpec itemSpec, string quantityText, Color quantityTextColour, bool showWarningIcon, UIItemDisplay itemPrefab, Transform parent)
	{
		UIItemDisplay uIItemDisplay = itemPrefab.Spawn();
		m_ItemElements.Add(uIItemDisplay);
		bool showComponentTier = true;
		uIItemDisplay.Setup(itemSpec.m_Item, Color.white, Color.white, quantityText, quantityTextColour, showComponentTier, showWarningIcon);
		uIItemDisplay.SetOutlinesEnabled(m_ItemOutlinesEnabled);
		uIItemDisplay.SetDisplayItemName(m_DisplayItemNames);
		uIItemDisplay.PointerEntered.Subscribe(RoutePointerEntered);
		uIItemDisplay.PointerExited.Subscribe(RoutePointerExited);
		uIItemDisplay.RectTrans.SetParent(parent, worldPositionStays: false);
	}

	private void TryAddPlusSign()
	{
		if (m_PlusSymbolPrefab != null)
		{
			Transform transform = m_PlusSymbolPrefab.Spawn();
			m_OperatorElements.Add(transform);
			transform.SetParent(base.transform, worldPositionStays: false);
		}
	}

	private void RoutePointerEntered(UIItemDisplay display)
	{
		PointerEntered.Send(display);
	}

	private void RoutePointerExited(UIItemDisplay display)
	{
		PointerExited.Send(display);
	}

	private void OnPool()
	{
		RectTrans = base.transform as RectTransform;
	}

	private void OnSpawn()
	{
		m_ItemOutlinesEnabled = true;
		m_DisplayItemNames = true;
	}

	private void OnRecycle()
	{
		RecycleElements();
	}

	private void Update()
	{
		if ((bool)m_Consumer)
		{
			UpdateConsumerRecipe();
		}
	}
}
