#define UNITY_EDITOR
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIItemRecipeSelect : UIHUDElement, IUIGridSelectTutorialHelper, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler, ITabChangePrevHandler, ITabChangeNextHandler, IUIExtraButtonHandler2
{
	public delegate void CraftButtonPressedCallback(RecipeTable.Recipe recipe);

	[SerializeField]
	private UIItemSelectGrid m_Grid;

	[SerializeField]
	private UICategoryToggles m_CategoryToggles;

	[SerializeField]
	private Image m_CorpImage;

	[SerializeField]
	private Text m_SelectedItemName;

	[SerializeField]
	private Text m_SelectedItemDescription;

	[SerializeField]
	private TextMeshProUGUI m_SelectedItemDescriptionTMP;

	[SerializeField]
	private LocalisedString m_NoItemSelectedDescription;

	[SerializeField]
	private UIRecipe m_RecipeDisplay;

	[SerializeField]
	private UIItemDisplay m_CurrentlyBuildingItemDisplay;

	[SerializeField]
	private Button m_CraftButton;

	[SerializeField]
	private Button m_CancelButton;

	[SerializeField]
	private Toggle m_RepeatToggle;

	[SerializeField]
	[Tooltip("Shows which category we are currently building")]
	private Transform m_BuildingItemCategoryHighlight;

	[SerializeField]
	[Tooltip("Shows which item we are currently building")]
	private Transform m_BuildingItemHighlight;

	[SerializeField]
	private GameObject m_StandardBG;

	[SerializeField]
	private GameObject m_JoypadBG;

	[SerializeField]
	private UIBlockPaletteFilterText m_FilterText;

	[SerializeField]
	private GameObject[] m_HiddenUIJoypad;

	[SerializeField]
	private UIScrollRectSetter m_InformationScroller;

	[SerializeField]
	private bool m_UseGamepadBumperButtonsForCategoryNavigation = true;

	[SerializeField]
	private bool m_VerticalCategories = true;

	private ModuleItemConsume m_Consume;

	private Dictionary<ItemTypeInfo, RecipeTable.Recipe> m_RecipesWeCanMake = new Dictionary<ItemTypeInfo, RecipeTable.Recipe>();

	private bool m_BuiltItemWasSelected;

	private Bitfield<UIShopBlockSelect.TutorialElement> m_RestrictedTutorialElements = new Bitfield<UIShopBlockSelect.TutorialElement>();

	private bool m_ShowJoypadInfo;

	public ModuleItemConsume Consume => m_Consume;

	public bool BuiltItemWasSelected => m_BuiltItemWasSelected;

	protected UIItemSelectGrid ItemGrid => m_Grid;

	protected UICategoryToggles CategoryToggles => m_CategoryToggles;

	protected IEnumerable<RecipeTable.Recipe> RecipesWeCanMake => m_RecipesWeCanMake.Values;

	public UIItemSelectGrid GetGrid()
	{
		return m_Grid;
	}

	public override void Show(object context)
	{
		m_Grid.SetAllowScroll(enableScroll: true);
		m_RestrictedTutorialElements.Clear();
		ModuleItemConsume moduleItemConsume = context as ModuleItemConsume;
		d.Assert(moduleItemConsume != null, "UIItemRecipeSelect.Show expected a ModuleItemConsume to be passed as context");
		if (moduleItemConsume != null)
		{
			SetTargetConsumer(moduleItemConsume);
			if (m_CorpImage != null && moduleItemConsume.CraftingFaction != FactionSubTypes.SPE)
			{
				m_CorpImage.sprite = Singleton.Manager<ManUI>.inst.GetCorpIcon(moduleItemConsume.CraftingFaction);
				TooltipComponent component = m_CorpImage.GetComponent<TooltipComponent>();
				if ((bool)component)
				{
					component.SetText(StringLookup.GetCorporationName(moduleItemConsume.CraftingFaction));
				}
			}
		}
		UpdateAvailableCategories();
		m_Grid.Repopulate();
		m_RepeatToggle.SetValue(m_Consume.RecipeRepeating);
		m_RepeatToggle.onValueChanged.AddListener(OnRepeatToggled);
		RecipeTable.Recipe recipe = ((m_Consume != null) ? m_Consume.Recipe : null);
		UpdateCraftButtonState(recipe != null);
		if (m_BuiltItemWasSelected && recipe != null)
		{
			ItemTypeInfo item = recipe.m_OutputItems[0].m_Item;
			TryShowItem(item);
			m_Grid.TrySelectByType(item);
		}
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIItemMenu);
		EventSystem.current.SetSelectedGameObject(base.gameObject);
		m_ShowJoypadInfo = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
		UpdateJoypadUI();
		base.Show(context);
	}

	public override void Hide(object context)
	{
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIItemMenu);
		base.Hide(context);
		m_Grid.ClearSelection();
		SetTargetConsumer(null);
		m_RepeatToggle.onValueChanged.RemoveListener(OnRepeatToggled);
	}

	public override EscapeKeyActionType GetEscapeKeyAction()
	{
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.HUDMask) || m_RestrictedTutorialElements.AnySet)
		{
			return EscapeKeyActionType.None;
		}
		return base.GetEscapeKeyAction();
	}

	public void Close()
	{
		HideSelf();
	}

	public void ShowCurrentlyBuildingItem()
	{
		if (m_Consume != null && m_Consume.Recipe != null)
		{
			ItemTypeInfo item = m_Consume.Recipe.m_OutputItems[0].m_Item;
			TryShowItem(item);
			m_Grid.TrySelectByType(item);
		}
	}

	public void TryShowItem(ItemTypeInfo itemInfo)
	{
		if (!m_Grid.IsItemTypeAvailableInView(itemInfo))
		{
			int itemCategory = GetItemCategory(itemInfo);
			m_CategoryToggles.SetToggleSelected(itemCategory, selected: true);
		}
		m_Grid.TryScrollToItem(itemInfo);
	}

	public void SetAllowGridScroll(bool enableScroll)
	{
		m_Grid.SetAllowScroll(enableScroll);
	}

	public RectTransform GetRecipeDisplayTransform()
	{
		return m_RecipeDisplay.transform as RectTransform;
	}

	public Button GetCraftButton()
	{
		return m_CraftButton;
	}

	public Toggle GetRepeatToggle()
	{
		return m_RepeatToggle;
	}

	public ToggleWrapper GetCategoryToggle(int category)
	{
		if (category == 0)
		{
			return m_CategoryToggles.GetAllToggle();
		}
		return m_CategoryToggles.GetToggle(category);
	}

	public ItemTypeInfo GetSelectedItem()
	{
		return m_Grid.GetSelection();
	}

	public RectTransform GetItemTransform(ItemTypeInfo itemTypeInfo, bool allowNull = false)
	{
		RectTransform result = null;
		UIItemGridButton uIItemGridButton = m_Grid.FindButtonByItemType(itemTypeInfo, allowNull);
		if (uIItemGridButton != null)
		{
			result = uIItemGridButton.RectTransform;
		}
		return result;
	}

	public RectTransform GetRecipeIngredientTransform(int ingredientIndex)
	{
		return m_RecipeDisplay.GetIngredientTransform(ingredientIndex) as RectTransform;
	}

	public void SetElementForTutorial(UIShopBlockSelect.TutorialElement e)
	{
		m_RestrictedTutorialElements.Clear();
		if (e != UIShopBlockSelect.TutorialElement.None)
		{
			m_RestrictedTutorialElements.SetFlags(~(1 << (int)e));
		}
	}

	public bool TryGetItemTransform(ItemTypeInfo itemInfo, bool allowNull, out RectTransform itemTransform)
	{
		UIItemGridButton uIItemGridButton = m_Grid.FindButtonByItemType(itemInfo, allowNull);
		itemTransform = ((uIItemGridButton != null) ? uIItemGridButton.RectTransform : null);
		return itemTransform != null;
	}

	public RectTransform GetAdditionalItemTransform()
	{
		return null;
	}

	public Button GetConfirmButton()
	{
		return m_CraftButton;
	}

	protected virtual bool CanMakeRecipe(RecipeTable.Recipe recipe)
	{
		return true;
	}

	protected abstract void SetupGridFilterCallbacks();

	protected abstract int GetItemCategory(ItemTypeInfo itemInfo);

	protected virtual void UpdateAvailableCategories()
	{
	}

	protected abstract void SetupCategoryToggles();

	protected virtual string GetItemDescriptionString(ItemTypeInfo itemInfo)
	{
		return StringLookup.GetItemDescription(itemInfo.ObjectType, itemInfo.ItemType);
	}

	protected bool HaveRecipeForItem(ItemTypeInfo itemInfo)
	{
		return m_RecipesWeCanMake.ContainsKey(itemInfo);
	}

	private void UpdateCraftButtonState(bool itemIsBuilding)
	{
		m_BuiltItemWasSelected = itemIsBuilding;
		if (!Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			m_CraftButton.gameObject.SetActive(!itemIsBuilding);
			m_CraftButton.interactable = !itemIsBuilding;
			m_CancelButton.gameObject.SetActive(itemIsBuilding);
			m_CancelButton.interactable = itemIsBuilding;
		}
	}

	private void SetTargetConsumer(ModuleItemConsume consumer)
	{
		if (consumer != m_Consume)
		{
			if (m_Consume != null)
			{
				m_Consume.OnConsumerRecipesChanged.Unsubscribe(OnConsumerRecipesChanged);
			}
			m_Consume = consumer;
			UpdateAvailableRecipes();
			if (m_Consume != null)
			{
				m_Consume.OnConsumerRecipesChanged.Subscribe(OnConsumerRecipesChanged);
			}
		}
	}

	private void UpdateAvailableRecipes()
	{
		m_RecipesWeCanMake.Clear();
		if (!(m_Consume != null))
		{
			return;
		}
		foreach (RecipeTable.Recipe allRecipe in m_Consume.AllRecipes)
		{
			if (allRecipe.m_OutputItems.Length == 1 && CanMakeRecipe(allRecipe))
			{
				ItemTypeInfo item = allRecipe.m_OutputItems[0].m_Item;
				if (!m_RecipesWeCanMake.ContainsKey(item))
				{
					m_RecipesWeCanMake.Add(item, allRecipe);
				}
			}
		}
	}

	private void UpdateJoypadUI()
	{
		if (m_JoypadBG != null && m_StandardBG != null)
		{
			m_JoypadBG.SetActive(m_ShowJoypadInfo);
			m_StandardBG.SetActive(!m_ShowJoypadInfo);
		}
		if (m_FilterText != null)
		{
			m_FilterText.UpdateCategoryText(m_ShowJoypadInfo, m_CategoryToggles, GetCategoryName, new Localisation.GlyphInfo(41));
		}
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			GameObject[] hiddenUIJoypad = m_HiddenUIJoypad;
			for (int i = 0; i < hiddenUIJoypad.Length; i++)
			{
				hiddenUIJoypad[i].gameObject.SetActive(value: false);
			}
		}
	}

	protected abstract string GetCategoryName(int categoryIdx);

	private void OnFiltersChanged()
	{
		m_Grid.Repopulate();
	}

	private void OnSelectionChanged()
	{
		ItemTypeInfo selection = m_Grid.GetSelection();
		if (selection != null && m_RecipesWeCanMake.TryGetValue(selection, out var value))
		{
			m_SelectedItemName.text = StringLookup.GetItemName(selection.ObjectType, selection.ItemType);
			if ((bool)m_SelectedItemDescription)
			{
				m_SelectedItemDescription.text = GetItemDescriptionString(selection);
			}
			if ((bool)m_SelectedItemDescriptionTMP)
			{
				m_SelectedItemDescriptionTMP.text = GetItemDescriptionString(selection);
			}
			m_CraftButton.interactable = true;
			m_RecipeDisplay.ShowRecipe(value);
			m_RecipeDisplay.SetDisplayItemNames(m_ShowJoypadInfo);
		}
		else
		{
			m_SelectedItemName.text = string.Empty;
			if ((bool)m_SelectedItemDescription)
			{
				m_SelectedItemDescription.text = m_NoItemSelectedDescription.Value;
			}
			if ((bool)m_SelectedItemDescriptionTMP)
			{
				m_SelectedItemDescriptionTMP.text = m_NoItemSelectedDescription.Value;
			}
			m_CraftButton.interactable = false;
			m_RecipeDisplay.ShowRecipe(null);
			m_RecipeDisplay.SetDisplayItemNames(m_ShowJoypadInfo);
		}
		if (m_InformationScroller != null)
		{
			m_InformationScroller.SetToTop();
		}
	}

	private void OnCraftButtonPressed()
	{
		ItemTypeInfo selection = m_Grid.GetSelection();
		if (selection != null && m_RecipesWeCanMake.TryGetValue(selection, out var value))
		{
			m_Consume.RequestBeginCraftingRecipe(value);
			HideSelf();
		}
	}

	private void OnCancelButtonPressed()
	{
		m_Consume.RequestCancelRecipe();
		HideSelf();
	}

	private void OnRepeatToggled(bool enabled)
	{
		m_Consume.RequestSetRecipeRepeating(enabled);
	}

	private void OnConsumerRecipesChanged(ModuleItemConsume changedConsumer)
	{
		ItemTypeInfo selection = m_Grid.GetSelection();
		UpdateAvailableRecipes();
		UpdateAvailableCategories();
		m_Grid.Repopulate();
		if (selection != null && m_RecipesWeCanMake.ContainsKey(selection))
		{
			m_Grid.TrySelectByType(selection);
		}
		else
		{
			m_Grid.ClearSelection();
		}
		UpdateCraftButtonState(m_Consume.Recipe != null);
		if (m_FilterText != null)
		{
			m_FilterText.UpdateCategoryText(m_ShowJoypadInfo, m_CategoryToggles, GetCategoryName, new Localisation.GlyphInfo(41));
		}
	}

	public void OnMove(AxisEventData eventData)
	{
		if (!base.IsVisible || !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			return;
		}
		if (!m_UseGamepadBumperButtonsForCategoryNavigation && Singleton.Manager<ManInput>.inst.GetButton(66))
		{
			if (!m_RestrictedTutorialElements.Contains(1))
			{
				switch (eventData.moveDir)
				{
				case MoveDirection.Left:
					if (!m_VerticalCategories)
					{
						m_CategoryToggles.CycleSingleToggle(forward: false);
					}
					break;
				case MoveDirection.Right:
					if (!m_VerticalCategories)
					{
						m_CategoryToggles.CycleSingleToggle();
					}
					break;
				case MoveDirection.Up:
					if (m_VerticalCategories)
					{
						m_CategoryToggles.CycleSingleToggle(forward: false);
					}
					break;
				case MoveDirection.Down:
					if (m_VerticalCategories)
					{
						m_CategoryToggles.CycleSingleToggle();
					}
					break;
				default:
					d.LogErrorFormat("Unhandled Move Dir in UIPaletteBlockSelect {0}", eventData.moveDir);
					break;
				case MoveDirection.None:
					break;
				}
			}
		}
		else if (!m_RestrictedTutorialElements.Contains(3))
		{
			m_Grid.MoveSelection(eventData.moveVector);
		}
		eventData.Use();
	}

	public void OnTabChangePrev(BaseEventData eventData)
	{
		if (base.IsVisible && !m_RestrictedTutorialElements.Contains(1) && m_UseGamepadBumperButtonsForCategoryNavigation)
		{
			m_CategoryToggles.CycleSingleToggle(forward: false);
			eventData.Use();
		}
	}

	public void OnTabChangeNext(BaseEventData eventData)
	{
		if (base.IsVisible && !m_RestrictedTutorialElements.Contains(1) && m_UseGamepadBumperButtonsForCategoryNavigation)
		{
			m_CategoryToggles.CycleSingleToggle();
			eventData.Use();
		}
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (base.IsVisible && !m_RestrictedTutorialElements.Contains(4))
		{
			if (!m_BuiltItemWasSelected)
			{
				m_CraftButton.onClick.Invoke();
			}
			else
			{
				m_CancelButton.onClick.Invoke();
			}
			eventData.Use();
		}
	}

	public void OnCancel(BaseEventData eventData)
	{
		if (base.IsVisible && !m_RestrictedTutorialElements.Contains(5))
		{
			HideSelf();
			eventData.Use();
		}
	}

	public void OnUIExtraButton2(BaseEventData eventData)
	{
		if (base.IsVisible && !m_RestrictedTutorialElements.Contains(6))
		{
			if (m_RepeatToggle != null && m_RepeatToggle.IsActive() && m_RepeatToggle.interactable)
			{
				m_RepeatToggle.isOn = !m_RepeatToggle.isOn;
			}
			eventData.Use();
		}
	}

	private void OnPool()
	{
		SetupGridFilterCallbacks();
		m_Grid.SelectionChanged.Subscribe(OnSelectionChanged);
		m_CategoryToggles.OnChanged.Subscribe(OnFiltersChanged);
		m_CraftButton.onClick.AddListener(OnCraftButtonPressed);
		m_CancelButton.onClick.AddListener(OnCancelButtonPressed);
		m_Grid.Init();
	}

	private void OnSpawn()
	{
		SetupCategoryToggles();
		m_CraftButton.interactable = false;
		AddElementToGroup(ManHUD.HUDGroup.Main);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements);
		AddElementToGroup(ManHUD.HUDGroup.ContextMenuBlocking);
	}

	private void OnRecycle()
	{
		m_CategoryToggles.TakeDown();
		m_RecipeDisplay.ShowRecipe(null);
	}

	private void Update()
	{
		RecipeTable.Recipe recipe = ((m_Consume != null) ? m_Consume.Recipe : null);
		ItemTypeInfo itemTypeInfo = null;
		bool flag = recipe != null;
		bool flag2 = false;
		if (recipe != null)
		{
			itemTypeInfo = recipe.m_OutputItems[0].m_Item;
			ItemTypeInfo selection = m_Grid.GetSelection();
			flag2 = itemTypeInfo == selection;
		}
		if (m_BuiltItemWasSelected != flag2)
		{
			UpdateCraftButtonState(flag2);
		}
		if (m_BuildingItemCategoryHighlight != null)
		{
			GameObject gameObject = m_BuildingItemCategoryHighlight.gameObject;
			if (gameObject.activeSelf != flag)
			{
				gameObject.SetActive(flag);
			}
			if (flag)
			{
				int itemCategory = GetItemCategory(itemTypeInfo);
				m_BuildingItemCategoryHighlight.position = GetCategoryToggle(itemCategory).transform.position;
			}
		}
		if (m_BuildingItemHighlight != null)
		{
			UIItemGridButton uIItemGridButton = (flag ? m_Grid.FindButtonByItemType(itemTypeInfo, allowNull: true) : null);
			bool flag3 = uIItemGridButton != null;
			GameObject gameObject2 = m_BuildingItemHighlight.gameObject;
			if (gameObject2.activeSelf != flag3)
			{
				gameObject2.SetActive(flag3);
			}
			if (flag3)
			{
				m_BuildingItemHighlight.position = uIItemGridButton.RectTransform.position;
			}
		}
		if (m_CurrentlyBuildingItemDisplay != null)
		{
			GameObject gameObject3 = m_CurrentlyBuildingItemDisplay.gameObject;
			bool flag4 = itemTypeInfo != null;
			if (gameObject3.activeSelf != flag4)
			{
				gameObject3.SetActive(flag4);
			}
			if (itemTypeInfo != null)
			{
				m_CurrentlyBuildingItemDisplay.Setup(itemTypeInfo);
			}
		}
		if (m_InformationScroller != null)
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(47);
			m_InformationScroller.Scroll((0f - axis) * Time.deltaTime * Globals.inst.m_StickScrollSensitivity);
		}
	}

	private void LateUpdate()
	{
		m_Grid.UpdateItemVisibility();
	}
}
