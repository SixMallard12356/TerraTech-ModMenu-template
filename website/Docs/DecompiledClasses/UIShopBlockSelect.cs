#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIShopBlockSelect : UIHUDElement, IUIGridSelectTutorialHelper, IMoveHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
{
	public class ExpandContext
	{
		public ExpandReason expandReason;

		public bool forceClose;
	}

	public enum ExpandReason
	{
		Button,
		Beam
	}

	public enum TutorialElement
	{
		None,
		CategorySelect,
		CorpSelect,
		ItemSelect,
		Submit,
		Cancel,
		RepeatToggle,
		All
	}

	public class ShowContext
	{
		public FactionSubTypes singleCorpToShow;

		public bool inventorySpecified;

		public IInventory<BlockTypes> inventory;

		public uint shopBlockPoolID;
	}

	[SerializeField]
	private UIBlockSelectGrid m_Grid;

	[SerializeField]
	private UICategoryToggles m_CategoryToggles;

	[SerializeField]
	private UICorpToggles m_CorpToggles;

	[SerializeField]
	private Transform m_CorpTogglesParent;

	[SerializeField]
	private UIBlockInfoDisplay m_BlockInfoDisplay;

	[SerializeField]
	private Button m_PurchaseBlockButton;

	[SerializeField]
	private string m_ExpandAnimation;

	[SerializeField]
	private string m_CollapseAnimation;

	[SerializeField]
	private bool m_ResetStateEachTimeShown;

	[SerializeField]
	private bool m_SelectFirstItemOnReset;

	[SerializeField]
	private Toggle m_ShowSearchToggle;

	[SerializeField]
	private GameObject m_SearchPanel;

	[SerializeField]
	private InputField m_SearchStringInput;

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private Transform[] m_CorpHeadings;

	[SerializeField]
	private GameObject m_StandardBG;

	[SerializeField]
	private GameObject m_JoypadBG;

	[SerializeField]
	private UIBlockPaletteFilterText m_FilterText;

	[SerializeField]
	private UIScrollRectSetter m_ScrollRectSetter;

	[SerializeField]
	private CategoryOrder m_CategoryOrder;

	[SerializeField]
	private CorporationOrder m_CorporationOrder;

	[SerializeField]
	private BlockSortOrder m_IndividualSortOrder;

	private int m_BlockCost;

	private uint m_ShopBlockPoolID = uint.MaxValue;

	private bool m_UpdateGrid;

	private bool m_UpdateBlock;

	private bool m_ShowJoypadInfo;

	private Animator m_Animator;

	private Bitfield<ExpandReason> m_ExpandReasonBitfield = new Bitfield<ExpandReason>();

	private bool m_ShowAllCorps;

	private FactionSubTypes m_SingleCorpToShow;

	private bool m_FirstShow;

	private ButtonCombo m_SearchButtonCombo = new ButtonCombo(117, 116);

	private IInventory<BlockTypes> m_Inventory;

	private Bitfield<TutorialElement> m_RestrictedTutorialElements = new Bitfield<TutorialElement>();

	private string[] m_SearchFilterGroups;

	public bool ShowAllCorps => m_ShowAllCorps;

	private bool IgnoreBlockRestrictions
	{
		get
		{
			if (Singleton.Manager<DebugUtil>.inst != null)
			{
				return Singleton.Manager<DebugUtil>.inst.UnlimitedShopBlocks;
			}
			return false;
		}
	}

	private void SetupCorpHeader(FactionSubTypes corp)
	{
		int num = 0;
		EnumValuesIterator<FactionSubTypes> enumerator = EnumIterator<FactionSubTypes>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			FactionSubTypes current = enumerator.Current;
			bool num2 = num < m_CorpHeadings.Length && m_CorpHeadings[num] != null;
			bool flag = current == corp;
			if (num2)
			{
				m_CorpHeadings[num].gameObject.SetActive(flag);
			}
			else if (flag)
			{
				d.LogWarningFormat("Unable to show corporation header icon in shop for faction {0}", current);
			}
			num++;
		}
		if (m_CorpTogglesParent != null)
		{
			m_CorpTogglesParent.gameObject.SetActive(m_SingleCorpToShow == FactionSubTypes.NULL);
		}
	}

	public void CloseButton()
	{
		Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockShop);
	}

	public override void Show(object context)
	{
		ShowContext showContext = context as ShowContext;
		FactionSubTypes singleCorpToShow = m_SingleCorpToShow;
		if (showContext != null)
		{
			m_ShowAllCorps = showContext.singleCorpToShow == FactionSubTypes.NULL;
			m_SingleCorpToShow = showContext.singleCorpToShow;
			m_ShopBlockPoolID = showContext.shopBlockPoolID;
		}
		else
		{
			m_ShowAllCorps = true;
			m_SingleCorpToShow = FactionSubTypes.NULL;
			m_ShopBlockPoolID = uint.MaxValue;
		}
		SetupCorpHeader(m_SingleCorpToShow);
		m_Grid.SetAllowScroll(enableScroll: true);
		m_RestrictedTutorialElements.Clear();
		IInventory<BlockTypes> inventory = showContext?.inventory;
		bool flag = showContext != null && showContext.inventorySpecified && inventory != m_Inventory;
		if (IgnoreBlockRestrictions)
		{
			inventory = null;
			flag = inventory != m_Inventory;
		}
		bool flag2 = singleCorpToShow != m_SingleCorpToShow || flag;
		if (flag)
		{
			SetInventory(inventory);
		}
		if (base.IsExpanded)
		{
			Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: true, UIInputMode.UIItemMenu);
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}
		int num;
		if (!base.IsVisible)
		{
			if (!m_FirstShow)
			{
				num = (m_ResetStateEachTimeShown ? 1 : 0);
				if (num == 0)
				{
					goto IL_0127;
				}
			}
			else
			{
				num = 1;
			}
			if (m_ShowAllCorps)
			{
				m_CorpToggles.ToggleAllOn();
			}
			m_CategoryToggles.ToggleAllOn();
			goto IL_0127;
		}
		if (flag2)
		{
			m_Grid.Repopulate();
			UpdateJoypadUI();
			m_UpdateGrid = false;
		}
		goto IL_0161;
		IL_0161:
		base.Show(context);
		return;
		IL_0127:
		if (num != 0 && m_SelectFirstItemOnReset)
		{
			m_Grid.TrySelectFirstItem();
		}
		m_FirstShow = false;
		goto IL_0161;
	}

	public override void Hide(object context)
	{
		CollapseSelf(new ExpandContext
		{
			forceClose = true
		});
		SetInventory(null);
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, uiModeEnable: false, UIInputMode.UIItemMenu);
		m_SearchButtonCombo.Reset();
		base.Hide(context);
	}

	public override bool Expand(object context)
	{
		bool result = false;
		if (base.IsVisible)
		{
			if (context is ExpandContext expandContext)
			{
				m_ExpandReasonBitfield.Add((int)expandContext.expandReason);
				m_ShowJoypadInfo = Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
				if (!base.IsExpanded)
				{
					ExpandInternal(expand: true);
					result = base.Expand(context);
				}
			}
			else
			{
				d.LogError("Invalid context passed in in UIPaletteBlockSelect.Expand! Could not complete operation!");
			}
		}
		return result;
	}

	public override bool Collapse(object context)
	{
		bool result = false;
		if (base.IsVisible)
		{
			ExpandContext expandContext = context as ExpandContext;
			if (context == null || (expandContext != null && expandContext.forceClose))
			{
				m_ExpandReasonBitfield.Clear();
			}
			else if (expandContext != null)
			{
				m_ExpandReasonBitfield.Remove((int)expandContext.expandReason);
			}
			else
			{
				d.Assert(context == null || expandContext != null, "Invalid context passed in in UIShopBlockSelect.Collapse! Could not complete operation!");
			}
			if (m_ExpandReasonBitfield.IsNull)
			{
				ExpandInternal(expand: false);
				result = base.Collapse(context);
			}
		}
		return result;
	}

	private void ExpandInternal(bool expand)
	{
		if (expand == base.IsExpanded)
		{
			return;
		}
		Singleton.Manager<ManInput>.inst.SetControllerMapsForUI(this, expand, UIInputMode.UIItemMenu);
		if (expand)
		{
			if ((bool)m_Animator)
			{
				m_Animator.Play(m_ExpandAnimation, 0, 0f);
			}
			m_CorpToggles.Setup(m_CorporationOrder);
			m_CorpToggles.ToggleAllOn();
			m_Grid.Repopulate();
			UpdateJoypadUI();
			m_UpdateGrid = false;
			m_UpdateBlock = true;
			if (m_ResetStateEachTimeShown && m_SelectFirstItemOnReset)
			{
				m_Grid.TrySelectFirstItem();
			}
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
		}
		else
		{
			if ((bool)m_Animator)
			{
				m_Animator.Play(m_CollapseAnimation, 0, 0f);
			}
			m_Grid.ClearSelection();
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
			ShowSearchPanel(showPanel: false);
		}
	}

	public override EscapeKeyActionType GetEscapeKeyAction()
	{
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.HUDMask) || m_RestrictedTutorialElements.AnySet)
		{
			return EscapeKeyActionType.None;
		}
		return base.GetEscapeKeyAction();
	}

	public void EnsureBlockInInvetory(BlockTypes blockType)
	{
		if (m_Inventory != null && m_Inventory.GetQuantity(blockType) == 0)
		{
			m_Inventory.HostAddItem(blockType);
		}
	}

	public ToggleWrapper GetCategoryToggle(int category)
	{
		if (category == 0)
		{
			return m_CategoryToggles.GetAllToggle();
		}
		return m_CategoryToggles.GetToggle(category);
	}

	public void SetAllowGridScroll(bool enableScroll)
	{
		m_Grid.SetAllowScroll(enableScroll);
	}

	public void SetElementForTutorial(TutorialElement e)
	{
		m_RestrictedTutorialElements.Clear();
		if (e != TutorialElement.None)
		{
			m_RestrictedTutorialElements.SetFlags(~(1 << (int)e));
		}
	}

	public int GetItemCategory(ItemTypeInfo itemTypeInfo)
	{
		return (int)Singleton.Manager<ManSpawn>.inst.GetCategory((BlockTypes)itemTypeInfo.ItemType);
	}

	public void TryShowItem(ItemTypeInfo itemTypeInfo)
	{
		if (!m_UpdateGrid)
		{
			bool allowNull = true;
			if (m_Grid.FindButtonByItemType(itemTypeInfo, allowNull) == null)
			{
				int itemCategory = GetItemCategory(itemTypeInfo);
				m_CategoryToggles.SetToggleSelected(itemCategory, selected: true);
			}
			m_Grid.TryScrollToItem(itemTypeInfo);
		}
	}

	public ItemTypeInfo GetSelectedItem()
	{
		return m_Grid.GetSelection();
	}

	public bool TryGetItemTransform(ItemTypeInfo itemInfo, bool allowNull, out RectTransform itemTransform)
	{
		itemTransform = null;
		if (m_UpdateGrid)
		{
			return false;
		}
		UIItemGridButton uIItemGridButton = m_Grid.FindButtonByItemType(itemInfo, allowNull);
		if (uIItemGridButton != null)
		{
			itemTransform = uIItemGridButton.RectTransform;
		}
		return itemTransform != null;
	}

	public RectTransform GetAdditionalItemTransform()
	{
		return null;
	}

	public Button GetConfirmButton()
	{
		return m_PurchaseBlockButton;
	}

	private void PurchaseBlock()
	{
		if (m_Grid.TryGetSelection(out var type) && CanPurchaseBlock(type))
		{
			Singleton.Manager<ManPurchases>.inst.RequestPurchaseBlock(m_ShopBlockPoolID, type);
		}
	}

	public void OnModeChange()
	{
		m_CorpToggles.Setup(m_CorporationOrder);
		m_CorpToggles.ToggleAllOn();
	}

	public void ToggleModdedCorpsPanel()
	{
		m_CorpToggles.ToggleModdedCorpsPanel();
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
			m_FilterText.UpdateText(m_CorpToggles, m_ShowAllCorps, m_CategoryToggles, showCategoryText: true);
		}
	}

	private bool CanPurchaseBlock(BlockTypes blockType)
	{
		bool result = false;
		int num = ((m_Inventory == null) ? (-1) : m_Inventory.GetQuantity(blockType));
		bool flag = num > 0 || num == -1;
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() && !IgnoreBlockRestrictions)
		{
			if (Singleton.Manager<ManLicenses>.inst.GetBlockState(blockType) == ManLicenses.BlockState.Discovered)
			{
				FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType);
				int blockTier = Singleton.Manager<ManLicenses>.inst.GetBlockTier(blockType);
				int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corporation);
				if (Singleton.Manager<ManLicenses>.inst.IsLicenseSupported(corporation))
				{
					if (currentLevel >= blockTier || currentLevel >= Singleton.Manager<ManLicenses>.inst.MaxSupportedTier(corporation))
					{
						result = flag;
					}
				}
				else
				{
					result = flag;
				}
			}
		}
		else
		{
			result = flag;
		}
		return result;
	}

	private void SetInventory(IInventory<BlockTypes> inventory)
	{
		if (m_Inventory != null)
		{
			m_Inventory.UnsubscribeToInventoryChanged(OnInventoryChanged);
		}
		m_Inventory = inventory;
		if (m_Inventory != null)
		{
			m_Inventory.SubscribeToInventoryChanged(OnInventoryChanged);
		}
	}

	private void ShowSearchPanel(bool showPanel)
	{
		if (!showPanel)
		{
			m_SearchStringInput.text = string.Empty;
			m_SearchFilterGroups = null;
		}
		m_SearchPanel.SetActive(showPanel);
		if (m_ShowSearchToggle.isOn != showPanel)
		{
			m_ShowSearchToggle.SetValue(showPanel);
		}
	}

	private void OpenVirtualKeyboardSearch()
	{
		d.Assert(SKU.ConsoleUI, "We're trying to open a virtual keybaord on a non-console build! Why??");
		ShowSearchPanel(showPanel: true);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 1);
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.VirtualKeyboard, 2);
		VirtualKeyboard.PromptInput(localisedString, localisedString2, m_SearchStringInput.text, delegate(bool accepted, string result)
		{
			d.Log("OpenVirtualKeyboard - Accepted: " + accepted + " Input = " + ((result == null) ? "NULL" : result));
			if (accepted)
			{
				m_SearchStringInput.SetValue(result);
			}
			OnSearchStringChanged(m_SearchStringInput.text);
		});
	}

	private bool CorpFilterFunction(FactionSubTypes corp)
	{
		if (m_ShowAllCorps)
		{
			return m_CorpToggles.Selection.Contains((int)corp);
		}
		return corp == m_SingleCorpToShow;
	}

	private bool BlockFilterFunction(BlockTypes blockType)
	{
		BlockCategories category = Singleton.Manager<ManSpawn>.inst.GetCategory(blockType);
		bool flag = m_CategoryToggles.Selection.Contains((int)category);
		if (flag && m_Inventory != null)
		{
			int quantity = m_Inventory.GetQuantity(blockType);
			flag = quantity > 0 || quantity == -1;
		}
		if (flag && Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType))
		{
			flag = false;
		}
		if (flag)
		{
			string[] searchFilterGroups = m_SearchFilterGroups;
			if (searchFilterGroups != null && searchFilterGroups.Length != 0)
			{
				flag = Util.StringPassesSearchFilter(StringLookup.GetItemName(ObjectTypes.Block, (int)blockType), m_SearchFilterGroups);
			}
		}
		return flag;
	}

	private void OnBlockSelectionChanged()
	{
		m_UpdateBlock = true;
	}

	private void OnFiltersChanged()
	{
		m_UpdateGrid = true;
	}

	private void OnInventoryChanged(BlockTypes blockType, int itemQuantity)
	{
		m_UpdateGrid = true;
	}

	private UIItemGridButton.DisplayParams BlockDisplayCallback(ItemTypeInfo itemTypeInfo)
	{
		BlockTypes itemType = (BlockTypes)itemTypeInfo.ItemType;
		bool flag = Singleton.Manager<ManLicenses>.inst.GetBlockState(itemType) == ManLicenses.BlockState.Discovered || !Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() || IgnoreBlockRestrictions;
		return new UIItemGridButton.DisplayParams
		{
			m_ItemColour = (flag ? Color.white : Color.black),
			m_ItemBackgroundColour = new Color(1f, 1f, 1f, 0f),
			m_DisplayQuantity = true,
			m_Quantity = ((m_Inventory != null) ? m_Inventory.GetQuantity(itemType) : (-1)),
			m_ShowAsUndiscovered = false,
			m_ShowDeathStreakReward = false
		};
	}

	private void OnBlockDiscovered(BlockTypes blockDiscovered)
	{
		m_UpdateGrid = true;
		m_UpdateBlock = true;
	}

	private void OnMoneyChanged(int money)
	{
		m_UpdateBlock = true;
	}

	private void OnShowSearchToggled(bool active)
	{
		ShowSearchPanel(active);
	}

	private void OnSearchStringChanged(string searchStr)
	{
		m_SearchFilterGroups = Util.GetSearchFilters(searchStr);
		m_UpdateGrid = true;
		if (SKU.ConsoleUI)
		{
			ShowSearchPanel(m_SearchFilterGroups.Length != 0);
		}
	}

	public void OnMove(AxisEventData eventData)
	{
		if (!base.IsExpanded || !base.IsVisible || !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			return;
		}
		m_SearchButtonCombo.Reset();
		if (Singleton.Manager<ManInput>.inst.GetButton(66))
		{
			if (!m_RestrictedTutorialElements.Contains(1))
			{
				switch (eventData.moveDir)
				{
				case MoveDirection.Up:
					m_CategoryToggles.CycleSingleToggle(forward: false);
					break;
				case MoveDirection.Down:
					m_CategoryToggles.CycleSingleToggle();
					break;
				default:
					d.LogErrorFormat("Unhandled Move Dir in UIPaletteBlockSelect {0}", eventData.moveDir);
					break;
				case MoveDirection.Left:
				case MoveDirection.Right:
				case MoveDirection.None:
					break;
				}
				m_ScrollRectSetter.SetToTop();
			}
		}
		else if (Singleton.Manager<ManInput>.inst.GetButton(79))
		{
			if (!m_RestrictedTutorialElements.Contains(1))
			{
				switch (eventData.moveDir)
				{
				case MoveDirection.Up:
					m_CorpToggles.CycleSingleToggle(forward: false);
					break;
				case MoveDirection.Down:
					m_CorpToggles.CycleSingleToggle();
					break;
				default:
					d.LogErrorFormat("Unhandled Move Dir in UIPaletteBlockSelect {0}", eventData.moveDir);
					break;
				case MoveDirection.Left:
				case MoveDirection.Right:
				case MoveDirection.None:
					break;
				}
				m_ScrollRectSetter.SetToTop();
			}
		}
		else if (!m_RestrictedTutorialElements.Contains(3) && m_Grid.MoveSelection(eventData.moveVector))
		{
			m_ScrollRectSetter.SetToTop();
		}
		eventData.Use();
	}

	public void OnSubmit(BaseEventData eventData)
	{
		if (base.IsExpanded && base.IsVisible && !m_RestrictedTutorialElements.Contains(4))
		{
			if ((bool)m_PurchaseBlockButton && m_PurchaseBlockButton.interactable)
			{
				m_PurchaseBlockButton.onClick.Invoke();
			}
			eventData.Use();
		}
	}

	public void OnCancel(BaseEventData eventData)
	{
		if (base.IsExpanded && base.IsVisible && !m_RestrictedTutorialElements.Contains(5))
		{
			CollapseSelf();
			eventData.Use();
		}
	}

	private void OnPool()
	{
		m_Grid.Init();
		m_Grid.CorpFilter = CorpFilterFunction;
		m_Grid.BlockFilter = BlockFilterFunction;
		m_Grid.CategoryOrder = m_CategoryOrder;
		m_Grid.CorporationOrder = m_CorporationOrder;
		m_Grid.IndividualSortOrder = m_IndividualSortOrder;
		m_Grid.ItemDisplayCallback = BlockDisplayCallback;
		m_Grid.SelectionChanged.Subscribe(OnBlockSelectionChanged);
		m_CategoryToggles.OnChanged.Subscribe(OnFiltersChanged);
		m_CorpToggles.OnChanged.Subscribe(OnFiltersChanged);
		m_ShowSearchToggle.interactable = !SKU.ConsoleUI;
		m_SearchStringInput.interactable = !SKU.ConsoleUI;
		m_ShowSearchToggle.onValueChanged.AddListener(OnShowSearchToggled);
		m_SearchStringInput.onValueChanged.AddListener(OnSearchStringChanged);
		SetInventory(null);
	}

	private void OnSpawn()
	{
		m_FirstShow = true;
		m_CategoryToggles.SetupAsBlockCategoryToggles(m_CategoryOrder);
		m_CorpToggles.Setup(m_CorporationOrder);
		if ((bool)m_PurchaseBlockButton)
		{
			m_PurchaseBlockButton.onClick.RemoveAllListeners();
			m_PurchaseBlockButton.onClick.AddListener(PurchaseBlock);
		}
		m_Animator = GetComponent<Animator>();
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Subscribe(OnBlockDiscovered);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Subscribe(OnMoneyChanged);
		AddElementToGroup(ManHUD.HUDGroup.Main, UIHUD.ShowAction.Expand);
		AddElementToGroup(ManHUD.HUDGroup.GamepadCursorHidingElements, UIHUD.ShowAction.Expand);
		ShowSearchPanel(showPanel: false);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Unsubscribe(OnBlockDiscovered);
		Singleton.Manager<ManPlayer>.inst.MoneyAmountChanged.Unsubscribe(OnMoneyChanged);
		m_CorpToggles.TakeDown();
		m_CategoryToggles.TakeDown();
		SetInventory(null);
	}

	private void Update()
	{
		if (base.IsExpanded)
		{
			if (m_SearchButtonCombo.IsJustReleased())
			{
				OpenVirtualKeyboardSearch();
			}
			if (m_UpdateGrid)
			{
				m_Grid.Repopulate();
				UpdateJoypadUI();
				ItemTypeInfo selection = m_Grid.GetSelection();
				if (selection == null || !m_Grid.IsItemTypeAvailableInView(selection))
				{
					m_Grid.TrySelectNearestItem();
				}
				m_UpdateGrid = false;
			}
			if (m_UpdateBlock)
			{
				m_BlockInfoDisplay.UpdateBlockFromGridSelection(m_Grid, CanPurchaseBlock);
				m_UpdateBlock = false;
			}
		}
		if (m_ScrollRectSetter != null)
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(47);
			m_ScrollRectSetter.Scroll((0f - axis) * Time.deltaTime * Globals.inst.m_StickScrollSensitivity);
		}
	}

	private void LateUpdate()
	{
		m_Grid.UpdateItemVisibility();
	}

	public UIItemGridButton GetItem(ItemTypeInfo itemInfo, bool allowNull)
	{
		return m_Grid.FindButtonByItemType(itemInfo, allowNull);
	}
}
