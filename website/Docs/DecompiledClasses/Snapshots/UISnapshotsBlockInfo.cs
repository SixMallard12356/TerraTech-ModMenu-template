#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Snapshots;

public class UISnapshotsBlockInfo : MonoBehaviour, IMoveHandler, IEventSystemHandler
{
	[SerializeField]
	private VMSnapshotPanel m_ViewModel;

	[SerializeField]
	private GameObject m_UndiscoveredBlocksGridContainer;

	[SerializeField]
	private RectTransform m_UndiscoveredBlocksGrid;

	[SerializeField]
	private UIItemGridButton m_BlockUIPrefab;

	[SerializeField]
	private UIScrollRectSetter m_UndiscoveredBlockScrollRect;

	[SerializeField]
	private GameObject m_UndiscoveredBlockScrollBar;

	[SerializeField]
	private GameObject m_TitleText;

	[SerializeField]
	private GameObject m_BlockLimiterText;

	[SerializeField]
	private Button m_CloseBtn;

	[SerializeField]
	private UISnapshotsBlockInfoGrid m_Grid;

	[Header("Block Info")]
	[SerializeField]
	private Text m_BlockTypeText;

	[SerializeField]
	private Text m_BlockCorpText;

	[SerializeField]
	private Text m_BlockGradeText;

	[SerializeField]
	private Text m_BlockLimitCostText;

	[SerializeField]
	private Text m_BlockAmountRequiredText;

	[SerializeField]
	private Text m_BlockAmountInInventoryText;

	[SerializeField]
	private Text m_BlockAmountOnPlayerText;

	[SerializeField]
	private LocalisedString m_BlockAmountRequiredFormatString;

	[SerializeField]
	private LocalisedString m_BlockAmountInInventoryFormatString;

	[SerializeField]
	private LocalisedString m_BlockAmountOnPlayerFormatString;

	[SerializeField]
	private UIItemDisplay m_BlockIcon;

	[SerializeField]
	private CategoryOrder m_CategoryOrder;

	[SerializeField]
	private CorporationOrder m_CorporationOrder;

	[SerializeField]
	private BlockSortOrder m_IndividualSortOrder;

	[SerializeField]
	private float m_AlphaOnNoBlocksToShow = 0.2f;

	[SerializeField]
	[HideInInspector]
	private CanvasGroup m_CanvasGroup;

	private bool m_Dirty;

	private BlockSortComparer m_BlockSortComparer;

	private void ShowBlockInfo(ItemTypeInfo itemInfo, TechDataAvailValidation.BlockTypeAvailability blockTypeData)
	{
		if (itemInfo != null)
		{
			BlockTypes itemType = (BlockTypes)itemInfo.ItemType;
			m_BlockTypeText.text = StringLookup.GetItemName(itemInfo);
			FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(itemType);
			if (corporation != FactionSubTypes.NULL)
			{
				m_BlockCorpText.text = StringLookup.GetCorporationName(corporation);
			}
			else
			{
				m_BlockCorpText.text = string.Empty;
			}
			m_BlockGradeText.text = StringLookup.GetBlockTierName(itemType);
			m_BlockLimitCostText.text = StringLookup.GetBlockLimiterCostName(itemType);
			m_BlockAmountRequiredText.text = string.Format(m_BlockAmountRequiredFormatString.Value, blockTypeData.numRequired);
			string inventoryQuantityString = Singleton.Manager<Localisation>.inst.GetInventoryQuantityString(blockTypeData.numInInventory);
			m_BlockAmountInInventoryText.text = string.Format(m_BlockAmountInInventoryFormatString.Value, inventoryQuantityString);
			m_BlockAmountOnPlayerText.text = string.Format(m_BlockAmountOnPlayerFormatString.Value, blockTypeData.numOnPlayerTech);
			m_BlockIcon.gameObject.SetActive(value: true);
			m_BlockIcon.Setup(itemInfo);
		}
		else
		{
			m_BlockTypeText.text = string.Empty;
			m_BlockCorpText.text = string.Empty;
			m_BlockGradeText.text = string.Empty;
			m_BlockLimitCostText.text = string.Empty;
			m_BlockAmountRequiredText.text = string.Empty;
			m_BlockAmountInInventoryText.text = string.Empty;
			m_BlockAmountOnPlayerText.text = string.Empty;
			m_BlockIcon.gameObject.SetActive(value: false);
		}
	}

	private void RefreshHasValidBlockSelection()
	{
		bool flag = m_ViewModel.m_SelectedBlockInfo.Value != null && m_ViewModel.m_SelectedBlockInfo.Value.BlockAvailability.Keys.Count > 0;
		if (m_CanvasGroup != null)
		{
			m_CanvasGroup.alpha = (flag ? 1f : m_AlphaOnNoBlocksToShow);
		}
		if (!flag)
		{
			m_Grid.ClearSelection();
			ShowBlockInfo(null, null);
		}
	}

	private void OnTechAvailDataChanged(TechDataAvailValidation availData)
	{
		m_Dirty = true;
	}

	private void OnVisibleChanged(bool isVisible)
	{
		base.gameObject.SetActive(isVisible);
		if (isVisible)
		{
			Singleton.Manager<ManNavUI>.inst.AddAndSelectEntryTarget(base.gameObject);
		}
		else
		{
			Singleton.Manager<ManNavUI>.inst.ForgetEntryTarget(base.gameObject);
		}
	}

	private UIItemGridButton.DisplayParams ProcessItemDisplay(ItemTypeInfo itemTypeInfo)
	{
		d.AssertFormat(itemTypeInfo.ObjectType == ObjectTypes.Block, "UISnapshotBlockInfo expects blocks, not ObjectType {0}", itemTypeInfo.ObjectType);
		TechDataAvailValidation value = m_ViewModel.m_SelectedBlockInfo.Value;
		BlockTypes itemType = (BlockTypes)itemTypeInfo.ItemType;
		if (!value.BlockAvailability.TryGetValue(itemType, out var value2))
		{
			d.LogError("UISnapshotBlockInfo - Could not find block type {0} in list of known blocks on tech. Likely GetFilteredItemTypes and ProcessItemDisplay are in race condition error");
		}
		bool flag = value2.availability == TechDataAvailValidation.BlockAvailableState.Available;
		return new UIItemGridButton.DisplayParams
		{
			m_ItemColour = Color.white,
			m_ItemBackgroundColour = (flag ? new Color(1f, 1f, 1f, 0f) : new Color(1f, 0f, 0f, 0.5f)),
			m_ShowAsUndiscovered = (value2.availability == TechDataAvailValidation.BlockAvailableState.NotAvailableInProgression),
			m_DisplayQuantity = true,
			m_Quantity = value2.numRequired,
			m_ShowDeathStreakReward = false
		};
	}

	private void ProcessItemFilter(List<ItemTypeInfo> itemTypes)
	{
		TechDataAvailValidation value = m_ViewModel.m_SelectedBlockInfo.Value;
		if (value == null)
		{
			return;
		}
		foreach (BlockTypes key in value.BlockAvailability.Keys)
		{
			itemTypes.Add(new ItemTypeInfo(ObjectTypes.Block, (int)key));
		}
		itemTypes.Sort(BlockSortComparer);
	}

	private int BlockSortComparer(ItemTypeInfo itemA, ItemTypeInfo itemB)
	{
		BlockTypes itemType = (BlockTypes)itemA.ItemType;
		BlockTypes itemType2 = (BlockTypes)itemB.ItemType;
		TechDataAvailValidation value = m_ViewModel.m_SelectedBlockInfo.Value;
		value.BlockAvailability.TryGetValue(itemType, out var value2);
		bool flag = value2.availability == TechDataAvailValidation.BlockAvailableState.Available;
		value.BlockAvailability.TryGetValue(itemType2, out value2);
		bool flag2 = value2.availability == TechDataAvailValidation.BlockAvailableState.Available;
		int num = ((flag != flag2) ? (flag ? 1 : (-1)) : 0);
		if (num != 0)
		{
			return num;
		}
		return m_BlockSortComparer.Compare(itemType, itemType2);
	}

	private void OnSelectionChanged()
	{
		ItemTypeInfo selection = m_Grid.GetSelection();
		TechDataAvailValidation.BlockTypeAvailability value = null;
		if (selection != null)
		{
			m_ViewModel.m_SelectedBlockInfo.Value.BlockAvailability.TryGetValue((BlockTypes)selection.ItemType, out value);
		}
		ShowBlockInfo(selection, value);
	}

	private void OnCloseButtonClicked()
	{
		m_ViewModel.HideBlockInfo();
	}

	public void OnMove(AxisEventData eventData)
	{
		m_Grid.MoveSelection(eventData.moveVector);
		eventData.Use();
	}

	public void MoveGrid(Vector2 dir)
	{
		m_Grid.MoveSelection(dir);
	}

	private void PrePool()
	{
		m_CanvasGroup = GetComponent<CanvasGroup>();
	}

	private void OnPool()
	{
		m_BlockSortComparer = new BlockSortComparer(m_CategoryOrder, m_CorporationOrder, m_IndividualSortOrder);
		m_CloseBtn.onClick.AddListener(OnCloseButtonClicked);
		m_Grid.ItemDisplayCallback = ProcessItemDisplay;
		m_Grid.ItemFilterCallback = ProcessItemFilter;
		m_Grid.SelectionChanged.Subscribe(OnSelectionChanged);
		m_Grid.Init();
		OnSelectionChanged();
	}

	private void OnSpawn()
	{
		m_ViewModel.m_SelectedBlockInfo.Bind(OnTechAvailDataChanged);
		m_ViewModel.m_BlockInfoVisible.Bind(OnVisibleChanged);
	}

	private void OnRecycle()
	{
		m_ViewModel.m_SelectedBlockInfo.Unbind(OnTechAvailDataChanged);
		m_ViewModel.m_BlockInfoVisible.Unbind(OnVisibleChanged);
	}

	private void Update()
	{
		if (m_Dirty)
		{
			m_Grid.Repopulate();
			RefreshHasValidBlockSelection();
			m_Dirty = false;
		}
	}

	private void LateUpdate()
	{
		m_Grid.UpdateItemVisibility();
	}
}
