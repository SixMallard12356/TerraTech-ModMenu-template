#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPaletteBlockSelect : UIHUDElement, IMoveHandler, IEventSystemHandler
{
	public enum ClearSelectionReason
	{
		GrabMode = 1,
		RemoveMode
	}

	[SerializeField]
	private UIBlockSelectGrid m_Grid;

	[SerializeField]
	private UICategoryToggles m_CategoryToggles;

	[SerializeField]
	private UICorpToggles m_CorpToggles;

	[SerializeField]
	private UIBlockInfoDisplay m_BlockInfoDisplayFull;

	[SerializeField]
	private UIBlockInfoDisplay m_BlockInfoDisplayCompact;

	[SerializeField]
	private string m_ExpandAnimation;

	[SerializeField]
	private string m_CollapseAnimation;

	[SerializeField]
	private Button m_GrabModeActiveButton;

	[SerializeField]
	private Button m_GrabModeInactiveButton;

	[SerializeField]
	private Toggle m_ShowSearchToggle;

	[SerializeField]
	private GameObject m_SearchPanel;

	[SerializeField]
	private InputField m_SearchStringInput;

	[SerializeField]
	private bool m_ResetStateEachTimeShown;

	[SerializeField]
	private bool m_SelectFirstItemOnReset;

	[SerializeField]
	private GameObject m_StandardBG;

	[SerializeField]
	private GameObject m_JoypadBG;

	[SerializeField]
	private UIBlockPaletteFilterText m_FilterText;

	[SerializeField]
	private CategoryOrder m_CategoryOrder;

	[SerializeField]
	private CorporationOrder m_CorporationOrder;

	[SerializeField]
	private BlockSortOrder m_IndividualSortOrder;

	[SerializeField]
	private GameObject m_CorpFilterHolder;

	[SerializeField]
	private GameObject m_BlockFilterHolder;

	private EventNoParams GridUpdatedEvent;

	private Event<BlockTypes, int> m_BlockSelectCallback;

	private EventNoParams m_NoBlockSelectedCallback;

	private bool m_UpdateGrid;

	private bool m_UpdateBlock;

	private bool m_ShowJoypadInfo;

	private Animator m_Animator;

	private Bitfield<UIShopBlockSelect.ExpandReason> m_ExpandReasonBitfield = new Bitfield<UIShopBlockSelect.ExpandReason>();

	private bool m_FirstShow;

	private UIBlockInfoDisplay m_ActiveBlockInfoDisplay;

	private ButtonCombo m_SearchButtonCombo = new ButtonCombo(117, 116);

	private BlockTypes m_PreLockSelection;

	private BlockTypes m_BlockToBeSelected;

	private Action m_DelayedBlockSelectionCallback;

	private string[] m_SearchFilterGroups;

	private ClearSelectionReason m_SelectionClearReasons;

	public IInventory<BlockTypes> CurrentInventory => Singleton.Manager<ManPurchases>.inst.GetInventory();

	public bool IsTemporaryPaintLockout => m_Grid.PreventSelection;

	public override void Show(object context)
	{
		int num;
		if (!base.IsVisible)
		{
			if (!m_FirstShow)
			{
				num = (m_ResetStateEachTimeShown ? 1 : 0);
				if (num == 0)
				{
					goto IL_0032;
				}
			}
			else
			{
				num = 1;
			}
			m_CorpToggles.ToggleAllOn();
			m_CategoryToggles.ToggleAllOn();
			goto IL_0032;
		}
		goto IL_005b;
		IL_0032:
		if (num != 0 && m_SelectFirstItemOnReset)
		{
			m_Grid.TrySelectFirstItem();
		}
		m_FirstShow = false;
		m_Grid.PreventSelection = false;
		goto IL_005b;
		IL_005b:
		base.Show(context);
	}

	public override void Hide(object context)
	{
		CollapseSelf(new UIShopBlockSelect.ExpandContext
		{
			forceClose = true
		});
		Singleton.Manager<ManPointer>.inst.EnableCursorEmulation(enable: false, ManPointer.CursorEmulationEnabledReason.PaintingBlock);
		m_SearchButtonCombo.Reset();
		base.Hide(context);
	}

	public override bool Expand(object context)
	{
		bool result = false;
		if (base.IsVisible)
		{
			if (context is UIShopBlockSelect.ExpandContext expandContext)
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
			UIShopBlockSelect.ExpandContext expandContext = context as UIShopBlockSelect.ExpandContext;
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
				d.Assert(context == null || expandContext != null, "Invalid context passed in in UIPaletteBlockSelect.Collapse! Could not complete operation!");
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
		Singleton.Manager<ManPointer>.inst.EnableCursorEmulation(expand, ManPointer.CursorEmulationEnabledReason.PaintingBlock, resetPosition: true);
		if (expand)
		{
			if ((bool)m_Animator)
			{
				m_Animator.Play(m_ExpandAnimation, 0, 0f);
			}
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Open);
			bool num = Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad();
			if (!num)
			{
				m_Grid.ClearSelection();
			}
			OnBlockSelectionChanged();
			m_Grid.Repopulate();
			UpdateJoypadUI();
			m_UpdateGrid = false;
			m_Grid.PreventSelection = false;
			m_SelectionClearReasons = (ClearSelectionReason)0;
			if (num)
			{
				ItemTypeInfo selection = m_Grid.GetSelection();
				if (selection == null || !m_Grid.IsItemTypeAvailableInView(selection))
				{
					m_Grid.TrySelectFirstItem();
				}
			}
			m_UpdateBlock = true;
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
			UIBlockLimit.ShowUI(UIBlockLimit.ShowReason.InventoryOpen);
		}
		else
		{
			if ((bool)m_Animator)
			{
				m_Animator.Play(m_CollapseAnimation, 0, 0f);
			}
			Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Close);
			m_NoBlockSelectedCallback.Send();
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
			UIBlockLimit.HideUI(UIBlockLimit.ShowReason.InventoryOpen);
			ShowSearchPanel(showPanel: false);
		}
	}

	public void RegisterBlockSelectCallback(Action<BlockTypes, int> selectedCallback, Action noBlockCallback)
	{
		if (selectedCallback != null)
		{
			m_BlockSelectCallback.Subscribe(selectedCallback);
		}
		if (noBlockCallback != null)
		{
			m_NoBlockSelectedCallback.Subscribe(noBlockCallback);
		}
	}

	public void UnregisterBlockSelectCallback(Action<BlockTypes, int> selectedCallback, Action noBlockCallback)
	{
		if (selectedCallback != null)
		{
			m_BlockSelectCallback.Unsubscribe(selectedCallback);
		}
		if (noBlockCallback != null)
		{
			m_NoBlockSelectedCallback.Unsubscribe(noBlockCallback);
		}
	}

	public void ClearBlockSelectCallback()
	{
		m_BlockSelectCallback.Clear();
		m_NoBlockSelectedCallback.Clear();
	}

	public bool TrySelectBlockType(BlockTypes type)
	{
		return m_Grid.TrySelectByType(new ItemTypeInfo(ObjectTypes.Block, (int)type), allowNotFound: true);
	}

	public bool TrySelectBlockTypeFilterAgnostic(BlockTypes type, Action onBlockSelectedCallback = null)
	{
		bool flag = false;
		int corporation = (int)Singleton.Manager<ManSpawn>.inst.GetCorporation(type);
		if (!m_CorpToggles.Selection.Contains(corporation))
		{
			m_CorpToggles.SetToggleSelected(corporation, selected: true);
			flag = true;
		}
		int category = (int)Singleton.Manager<ManSpawn>.inst.GetCategory(type);
		if (!m_CategoryToggles.Selection.Contains(category))
		{
			m_CategoryToggles.SetToggleSelected(category, selected: true);
			flag = true;
		}
		bool result = false;
		if (flag)
		{
			m_BlockToBeSelected = type;
			m_DelayedBlockSelectionCallback = onBlockSelectedCallback;
			GridUpdatedEvent.Subscribe(OnDelayedBlockSelection);
		}
		else
		{
			result = m_Grid.TrySelectByType(new ItemTypeInfo(ObjectTypes.Block, (int)type));
			onBlockSelectedCallback?.Invoke();
		}
		return result;
	}

	public void OnSwitchToGrabButton()
	{
		m_Grid.ClearSelection();
	}

	public void CloseButton()
	{
		Singleton.Manager<ManHUD>.inst.CollapseHudElement(ManHUD.HUDElementType.BlockPalette);
	}

	public void ShowExpandedBlockInfo(bool expanded)
	{
		UIBlockInfoDisplay uIBlockInfoDisplay = (expanded ? m_BlockInfoDisplayCompact : m_BlockInfoDisplayFull);
		UIBlockInfoDisplay uIBlockInfoDisplay2 = (expanded ? m_BlockInfoDisplayFull : m_BlockInfoDisplayCompact);
		if (uIBlockInfoDisplay2 != m_ActiveBlockInfoDisplay)
		{
			uIBlockInfoDisplay?.gameObject?.SetActive(value: false);
			m_ActiveBlockInfoDisplay = uIBlockInfoDisplay2;
			m_ActiveBlockInfoDisplay?.UpdateBlockFromGridSelection(m_Grid);
			m_ActiveBlockInfoDisplay?.gameObject?.SetActive(value: true);
			Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_GameplaySettings.m_DisplayExpandedInventoryBlockInfo = expanded;
		}
	}

	private void SetGrabModeActive(bool active)
	{
		if (!SKU.ConsoleUI)
		{
			if (m_GrabModeActiveButton.IsNotNull())
			{
				m_GrabModeActiveButton.gameObject.SetActive(active);
			}
			if (m_GrabModeInactiveButton.IsNotNull())
			{
				m_GrabModeInactiveButton.gameObject.SetActive(!active);
			}
		}
	}

	private void InventoryChangedCallback(BlockTypes blockType, int quantityOfType)
	{
		m_UpdateGrid = true;
		m_UpdateBlock = true;
	}

	private void UpdateJoypadUI()
	{
		if (m_JoypadBG != null && m_StandardBG != null)
		{
			m_JoypadBG.SetActive(m_ShowJoypadInfo);
			m_StandardBG.SetActive(!m_ShowJoypadInfo);
		}
		if (m_FilterText != null && m_ShowJoypadInfo)
		{
			m_FilterText.UpdateText(m_CorpToggles, showCorpText: true, m_CategoryToggles, showCategoryText: true);
		}
	}

	private void SetGridToUpdate()
	{
		m_UpdateGrid = true;
	}

	private void ShowSearchPanel(bool showPanel)
	{
		bool activeSelf = m_SearchPanel.activeSelf;
		if (!showPanel)
		{
			m_SearchStringInput.text = string.Empty;
			m_SearchFilterGroups = null;
		}
		m_SearchPanel.SetActive(showPanel);
		if (!SKU.ConsoleUI && !activeSelf && showPanel)
		{
			m_SearchStringInput.Select();
			m_SearchStringInput.ActivateInputField();
		}
		if (m_ShowSearchToggle.isOn != showPanel)
		{
			m_ShowSearchToggle.SetValue(showPanel);
		}
	}

	private void OpenVirtualKeyboardSearch()
	{
		d.Assert(SKU.ConsoleUI, "We're trying to open a virtual keybaord on a non-console build! Why??");
		ShowSearchPanel(showPanel: true);
		VirtualKeyboard.PromptInput(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.TechLoader, 18), string.Empty, m_SearchStringInput.text, delegate(bool accepted, string result)
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
		return m_CorpToggles.Selection.Contains((int)corp);
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

	private bool BlockFilterFunction(BlockTypes blockType)
	{
		BlockCategories category = Singleton.Manager<ManSpawn>.inst.GetCategory(blockType);
		bool flag = m_CategoryToggles.Selection.Contains((int)category);
		if (flag)
		{
			flag = CurrentInventory == null || CurrentInventory.GetQuantity(blockType) != 0;
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

	private UIItemGridButton.DisplayParams BlockDisplayFunction(ItemTypeInfo itemTypeInfo)
	{
		BlockTypes itemType = (BlockTypes)itemTypeInfo.ItemType;
		int quantity = -1;
		bool flag = true;
		bool showDeathStreakReward = false;
		if (CurrentInventory != null)
		{
			quantity = CurrentInventory.GetUnreservedQuantity(itemType);
			flag = CurrentInventory.IsAvailableToLocalPlayer(itemType);
			showDeathStreakReward = CurrentInventory.GetIsDeathStreakReward(itemType);
		}
		return new UIItemGridButton.DisplayParams
		{
			m_ItemColour = Color.white,
			m_ItemBackgroundColour = (flag ? new Color(1f, 1f, 1f, 0f) : new Color(1f, 0f, 0f, 0.5f)),
			m_DisplayQuantity = true,
			m_Quantity = quantity,
			m_ShowAsUndiscovered = false,
			m_ShowDeathStreakReward = showDeathStreakReward,
			m_ShowLocked = Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(itemType)
		};
	}

	public void CacheAndClearSelection(ClearSelectionReason reason)
	{
		m_SelectionClearReasons |= reason;
		if (!m_Grid.PreventSelection && m_Grid.TryGetSelection(out m_PreLockSelection))
		{
			m_Grid.ClearSelection();
			m_Grid.PreventSelection = true;
		}
	}

	public bool TryRestoreCachedSelection(ClearSelectionReason reason)
	{
		m_SelectionClearReasons &= ~reason;
		if (m_SelectionClearReasons == (ClearSelectionReason)0 && m_Grid.PreventSelection)
		{
			m_Grid.PreventSelection = false;
			return m_Grid.TrySelectByType(new ItemTypeInfo(ObjectTypes.Block, (int)m_PreLockSelection));
		}
		return false;
	}

	private void OnBlockDiscovered(BlockTypes blockDiscovered)
	{
		SetGridToUpdate();
	}

	private void OnBlockSelectionChanged()
	{
		m_UpdateBlock = true;
		if (m_Grid.TryGetSelection(out var type) && !Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(type))
		{
			int paramB = -1;
			if (CurrentInventory != null)
			{
				paramB = CurrentInventory.GetQuantity(type);
			}
			m_BlockSelectCallback.Send(type, paramB);
			SetGrabModeActive(active: false);
		}
		else
		{
			SetGrabModeActive(active: true);
			m_NoBlockSelectedCallback.Send();
		}
	}

	private void OnFiltersChanged()
	{
		m_UpdateGrid = true;
	}

	private void OnDelayedBlockSelection()
	{
		m_Grid.TrySelectByType(new ItemTypeInfo(ObjectTypes.Block, (int)m_BlockToBeSelected));
		m_DelayedBlockSelectionCallback?.Invoke();
		m_BlockToBeSelected = BlockTypes.GSOAIController_111;
		m_DelayedBlockSelectionCallback = null;
		GridUpdatedEvent.Unsubscribe(OnDelayedBlockSelection);
	}

	private void OnShowSearchButtonToggled(bool active)
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
		if (!base.IsExpanded || !Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			return;
		}
		m_SearchButtonCombo.Reset();
		if (Singleton.Manager<ManInput>.inst.GetButton(66))
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
		}
		else if (Singleton.Manager<ManInput>.inst.GetButton(79))
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
		}
		else
		{
			m_Grid.MoveSelection(eventData.moveVector);
		}
		eventData.Use();
	}

	private void OnPool()
	{
		m_Grid.Init();
		m_Grid.CorpFilter = CorpFilterFunction;
		m_Grid.BlockFilter = BlockFilterFunction;
		m_Grid.CategoryOrder = m_CategoryOrder;
		m_Grid.CorporationOrder = m_CorporationOrder;
		m_Grid.IndividualSortOrder = m_IndividualSortOrder;
		m_Grid.ItemDisplayCallback = BlockDisplayFunction;
		m_Grid.SelectionChanged.Subscribe(OnBlockSelectionChanged);
		m_CategoryToggles.OnChanged.Subscribe(OnFiltersChanged);
		m_CorpToggles.OnChanged.Subscribe(OnFiltersChanged);
		m_ShowSearchToggle.interactable = !SKU.ConsoleUI;
		m_SearchStringInput.interactable = !SKU.ConsoleUI;
		m_ShowSearchToggle.onValueChanged.AddListener(OnShowSearchButtonToggled);
		m_SearchStringInput.onValueChanged.AddListener(OnSearchStringChanged);
	}

	private void OnSpawn()
	{
		m_FirstShow = true;
		m_CategoryToggles.SetupAsBlockCategoryToggles(m_CategoryOrder);
		m_CorpToggles.Setup(m_CorporationOrder);
		m_Animator = GetComponent<Animator>();
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Subscribe(OnBlockDiscovered);
		Singleton.Manager<ManPurchases>.inst.OnInventoryChanged.Subscribe(InventoryChangedCallback);
		AddElementToGroup(ManHUD.HUDGroup.Main, UIHUD.ShowAction.Expand);
		Singleton.Manager<ManPointer>.inst.RegisterBlockPaintingCallbacks(this);
		bool displayExpandedInventoryBlockInfo = Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_GameplaySettings.m_DisplayExpandedInventoryBlockInfo;
		ShowExpandedBlockInfo(displayExpandedInventoryBlockInfo);
		ShowSearchPanel(showPanel: false);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManPointer>.inst.UnregisterBlockPaintingCallbacks(this);
		Singleton.Manager<ManPurchases>.inst.OnInventoryChanged.Unsubscribe(InventoryChangedCallback);
		Singleton.Manager<ManLicenses>.inst.BlockDiscoveredEvent.Unsubscribe(OnBlockDiscovered);
		m_CorpToggles.TakeDown();
		m_CategoryToggles.TakeDown();
		m_BlockSelectCallback.Clear();
		m_NoBlockSelectedCallback.Clear();
		m_Grid.Repopulate();
	}

	private void Update()
	{
		if (!base.IsExpanded)
		{
			return;
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDoublePressUp(120))
		{
			m_Grid.ClearSelection();
			m_Grid.PreventSelection = false;
		}
		else if (Singleton.Manager<ManInput>.inst.GetButton(120))
		{
			CacheAndClearSelection(ClearSelectionReason.GrabMode);
		}
		else
		{
			TryRestoreCachedSelection(ClearSelectionReason.GrabMode);
		}
		if (m_SearchButtonCombo.IsJustReleased())
		{
			OpenVirtualKeyboardSearch();
		}
		if (m_UpdateGrid)
		{
			m_Grid.Repopulate();
			UpdateJoypadUI();
			ItemTypeInfo selection = m_Grid.GetSelection();
			if (selection != null)
			{
				if (!m_Grid.IsItemTypeAvailableInView(selection))
				{
					m_Grid.TrySelectNearestItem();
				}
				else
				{
					m_Grid.TrySelectByType(selection);
				}
			}
			GridUpdatedEvent.Send();
			m_UpdateGrid = false;
		}
		if (m_UpdateBlock)
		{
			m_ActiveBlockInfoDisplay?.UpdateBlockFromGridSelection(m_Grid);
			m_UpdateBlock = false;
		}
		if (m_CorpToggles != null)
		{
			m_CorpToggles.UpdateMiniPalette();
		}
	}

	private void LateUpdate()
	{
		if (base.IsExpanded)
		{
			m_Grid.UpdateItemVisibility();
		}
	}
}
