#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public abstract class UIItemSelectGrid : MonoBehaviour
{
	public delegate UIItemGridButton.DisplayParams ItemDisplayFn(ItemTypeInfo itemTypeInfo);

	[SerializeField]
	private ScrollRect m_GridScrollRect;

	[SerializeField]
	private Scrollbar m_GridScrollBar;

	[SerializeField]
	[FormerlySerializedAs("m_BlockItemPrefab")]
	private UIItemGridButton m_ItemPrefab;

	[SerializeField]
	private int m_MaxEntries;

	[Tooltip("Selects first item when last selection no longer exists - takes priority over m_MaintainSelectionPos & m_ClearSelectionWhenNotVisible")]
	[SerializeField]
	private bool m_SelectFirstElementIfNotVisible;

	[Tooltip("Selects nearest item when last selection no longer exists - takes priority over m_ClearSelectionWhenNotVisible")]
	[SerializeField]
	private bool m_MaintainSelectionPos;

	[Tooltip("clears selection - lowest priority")]
	[SerializeField]
	private bool m_ClearSelectionWhenNotVisible;

	[SerializeField]
	private int m_NumItemColumns = 3;

	[SerializeField]
	private int m_NumItemRows = 6;

	[SerializeField]
	private Vector2 m_ItemSize = new Vector2(70f, 70f);

	[SerializeField]
	[Tooltip("Padding on the Bottom Right side of grid elements")]
	private Vector2 m_ItemPadding = Vector2.zero;

	public EventNoParams SelectionChanged;

	private UIItemGridButton[] m_ItemButtonReserve;

	private List<UIItemGridButton> m_ActiveItemButtons = new List<UIItemGridButton>();

	private ItemTypeInfo m_SelectedType;

	private RectTransform m_ParentTrans;

	private RectTransform m_ItemGrid;

	private int m_NeighbouringBlockType;

	private bool m_PreventSelection;

	private bool m_ScrollEnabled;

	private int m_VisibleRowIndex;

	private const float kGridSizeTolerance = 0.1f;

	private List<ItemTypeInfo> m_FilteredItemList = new List<ItemTypeInfo>();

	public ItemDisplayFn ItemDisplayCallback { get; set; }

	public bool PreventSelection
	{
		get
		{
			return m_PreventSelection;
		}
		set
		{
			if (m_PreventSelection != value)
			{
				m_PreventSelection = value;
				UIItemGridButton[] itemButtonReserve = m_ItemButtonReserve;
				for (int i = 0; i < itemButtonReserve.Length; i++)
				{
					itemButtonReserve[i].SetInteractable(!m_PreventSelection);
				}
			}
		}
	}

	public void Init()
	{
		m_ItemGrid = m_GridScrollRect.content;
		d.Assert(m_ItemGrid != null, "UIItemSelectGrid: m_GridScrollRect does not have its Content set");
		m_ParentTrans = m_ItemGrid.parent as RectTransform;
		Vector2 size = m_GridScrollRect.viewport.rect.size;
		int num = Mathf.CeilToInt(size.x / (m_ItemSize.x + m_ItemPadding.x) - 0.1f);
		int num2 = Mathf.CeilToInt(size.y / (m_ItemSize.y + m_ItemPadding.y) - 0.1f);
		if (num > m_NumItemColumns)
		{
			d.LogWarning("ItemGrid on " + base.name + " was configured for " + m_NumItemColumns + " columns, but calculated at least " + num + " were needed");
			m_NumItemColumns = num;
		}
		if (num2 > m_NumItemRows)
		{
			d.LogWarning("ItemGrid on " + base.name + " was configured for " + m_NumItemRows + " rows, but calculated at least " + num2 + " were needed");
			m_NumItemRows = num2;
		}
		SetupReserve();
		ClearSelectedItem();
	}

	public void SetAllowScroll(bool enableScroll)
	{
		m_ScrollEnabled = enableScroll;
		m_GridScrollRect.enabled = enableScroll;
		m_GridScrollBar.interactable = enableScroll;
	}

	public ItemTypeInfo GetSelection()
	{
		return m_SelectedType;
	}

	public bool TrySelectByType(ItemTypeInfo itemTypeInfo, bool allowNotFound = false)
	{
		bool flag = m_FilteredItemList.Contains(itemTypeInfo);
		if (flag)
		{
			TryScrollToItem(itemTypeInfo);
			UIItemGridButton uIItemGridButton = FindButtonByItemType(itemTypeInfo);
			if (uIItemGridButton != null)
			{
				uIItemGridButton.IsOn = true;
				return true;
			}
		}
		d.Assert(allowNotFound || flag, "Could not find item representing item type " + itemTypeInfo.ToString());
		return false;
	}

	public void TryScrollToItem(ItemTypeInfo itemTypeInfo, bool allowNotFound = false)
	{
		int num = m_FilteredItemList.IndexOf(itemTypeInfo);
		if (num >= 0)
		{
			int num2 = num / m_NumItemColumns;
			Vector2 anchoredPosition = m_ItemGrid.anchoredPosition;
			float height = m_GridScrollRect.viewport.rect.height;
			float num3 = m_ItemSize.y + m_ItemPadding.y;
			float num4 = (float)num2 * num3;
			float num5 = num4 + num3 - height;
			bool flag = false;
			if (anchoredPosition.y < num5)
			{
				anchoredPosition.y = num5;
				flag = true;
			}
			else if (anchoredPosition.y > num4)
			{
				anchoredPosition.y = num4;
				flag = true;
			}
			if (flag)
			{
				m_ItemGrid.anchoredPosition = anchoredPosition;
				UpdateItemVisibility();
			}
		}
		d.Assert(allowNotFound || num >= 0, "Could not find item representing item type " + itemTypeInfo.ToString());
	}

	public bool TrySelectFirstItem()
	{
		if (m_ScrollEnabled && m_FilteredItemList != null && m_FilteredItemList.Count > 0)
		{
			TrySelectByType(m_FilteredItemList[0]);
			return true;
		}
		int count = m_ActiveItemButtons.Count;
		for (int i = 0; i < count; i++)
		{
			UIItemGridButton uIItemGridButton = m_ActiveItemButtons[i];
			if (uIItemGridButton.IsVisible)
			{
				uIItemGridButton.IsOn = true;
				return true;
			}
		}
		return false;
	}

	public void TrySelectNearestItem()
	{
		if (m_NeighbouringBlockType == -1)
		{
			TrySelectFirstItem();
		}
		else
		{
			TrySelectByType(new ItemTypeInfo(ObjectTypes.Block, m_NeighbouringBlockType), allowNotFound: true);
		}
	}

	public void ClearSelection()
	{
		if (m_SelectedType != null)
		{
			UIItemGridButton uIItemGridButton = FindSelectedButton(allowNull: true);
			if (uIItemGridButton != null)
			{
				uIItemGridButton.IsOn = false;
			}
			ClearSelectedItem();
		}
	}

	public bool MoveSelection(Vector2 dir)
	{
		bool result = false;
		if (m_SelectedType == null || !m_FilteredItemList.Contains(m_SelectedType))
		{
			result = TrySelectFirstItem();
		}
		else
		{
			ItemTypeInfo nextItemInDirection = GetNextItemInDirection(m_SelectedType, dir);
			if (nextItemInDirection != null)
			{
				TryScrollToItem(nextItemInDirection);
				UIItemGridButton uIItemGridButton = FindButtonByItemType(nextItemInDirection);
				if (uIItemGridButton != null)
				{
					uIItemGridButton.IsOn = true;
					result = true;
				}
			}
		}
		return result;
	}

	public void Repopulate()
	{
		if (m_FilteredItemList.Capacity == 0)
		{
			m_FilteredItemList.Capacity = m_MaxEntries;
		}
		int value = m_FilteredItemList.IndexOf(m_SelectedType);
		m_FilteredItemList.Clear();
		GetFilteredItemTypes(m_FilteredItemList);
		if (m_FilteredItemList.Count > m_MaxEntries)
		{
			d.LogWarning($"UIItemSelectGrid: Ideal maximum array size {m_MaxEntries} does not accomodate required number of items {m_FilteredItemList.Count}");
		}
		for (int i = 0; i < m_ActiveItemButtons.Count; i++)
		{
			m_ActiveItemButtons[i].SetVisible(visible: false);
		}
		m_VisibleRowIndex = -1;
		Vector2 sizeDelta = new Vector2((float)m_NumItemColumns * (m_ItemSize.x + m_ItemPadding.x), Mathf.Ceil((float)m_FilteredItemList.Count / (float)m_NumItemColumns) * (m_ItemSize.y + m_ItemPadding.y));
		m_ItemGrid.sizeDelta = sizeDelta;
		int num = -1;
		if (m_SelectedType != null)
		{
			int num2 = m_FilteredItemList.IndexOf(m_SelectedType);
			if (num2 >= 0)
			{
				num = num2;
			}
			else if (m_SelectFirstElementIfNotVisible && m_ActiveItemButtons.Count > 0)
			{
				num = 0;
			}
			else if (m_MaintainSelectionPos && m_ActiveItemButtons.Count > 0)
			{
				num2 = Mathf.Clamp(value, 0, m_FilteredItemList.Count - 1);
			}
			else if (m_ClearSelectionWhenNotVisible)
			{
				ClearSelectedItem();
			}
			else
			{
				Vector2 anchoredPosition = m_ItemGrid.anchoredPosition;
				anchoredPosition.y = 0f;
				m_ItemGrid.anchoredPosition = anchoredPosition;
			}
		}
		else if ((m_SelectFirstElementIfNotVisible || m_MaintainSelectionPos) && m_ActiveItemButtons.Count > 0)
		{
			num = 0;
		}
		UpdateItemVisibility();
		if (num != -1 && num < m_FilteredItemList.Count)
		{
			TrySelectByType(m_FilteredItemList[num], allowNotFound: true);
		}
	}

	public void UpdateItemVisibility()
	{
		if (m_ParentTrans != null && m_ItemGrid != null)
		{
			int a = Mathf.FloorToInt(m_ItemGrid.anchoredPosition.y / (m_ItemSize.y + m_ItemPadding.y - 0.1f));
			a = Mathf.Max(a, 0);
			if (a != m_VisibleRowIndex)
			{
				m_VisibleRowIndex = a;
				BuildItemList();
			}
		}
	}

	private void BuildItemList()
	{
		int num = m_VisibleRowIndex * m_NumItemColumns;
		for (int i = 0; i < m_ActiveItemButtons.Count; i++)
		{
			UIItemGridButton uIItemGridButton = m_ActiveItemButtons[i];
			if (num >= 0 && num < m_FilteredItemList.Count)
			{
				ItemTypeInfo itemType = m_FilteredItemList[num];
				UIItemGridButton.DisplayParams dParams = ((ItemDisplayCallback != null) ? ItemDisplayCallback(itemType) : UIItemGridButton.DisplayParams.kDefault);
				uIItemGridButton.SetupItem(m_FilteredItemList[num], dParams);
				uIItemGridButton.OnToggled.Clear();
				uIItemGridButton.OnToggled.Subscribe(delegate(UIItemGridButton item, bool enabled)
				{
					if (enabled)
					{
						SetSelectedItem(itemType);
					}
					else if (m_SelectedType != null && m_SelectedType == item.ItemTypeInfo)
					{
						ClearSelectedItem();
					}
				});
				uIItemGridButton.SetVisible(visible: true);
				uIItemGridButton.IsOn = itemType == m_SelectedType;
				int num2 = num / m_NumItemColumns;
				int num3 = num % m_NumItemColumns;
				uIItemGridButton.RectTransform.anchoredPosition = new Vector2((float)num3 * (m_ItemSize.x + m_ItemPadding.x), 0f - (float)(num2 + 1) * (m_ItemSize.y + m_ItemPadding.y)) + uIItemGridButton.RectTransform.rect.size * 0.5f;
				num++;
			}
			else
			{
				uIItemGridButton.SetVisible(visible: false);
				uIItemGridButton.OnToggled.Clear();
			}
		}
	}

	public bool IsItemTypeAvailableInView(ItemTypeInfo type)
	{
		return m_FilteredItemList.Contains(type);
	}

	public UIItemGridButton FindButtonByItemType(ItemTypeInfo type, bool allowNull = false)
	{
		UIItemGridButton uIItemGridButton = null;
		for (int i = 0; i < m_ActiveItemButtons.Count; i++)
		{
			if (m_ActiveItemButtons[i].ItemTypeInfo == type)
			{
				uIItemGridButton = m_ActiveItemButtons[i];
				break;
			}
		}
		d.Assert(allowNull || (bool)uIItemGridButton, "Could not find button representing item type " + type.ToString());
		return uIItemGridButton;
	}

	protected abstract void GetFilteredItemTypes(List<ItemTypeInfo> itemTypes);

	private void SetupReserve()
	{
		if (m_ItemButtonReserve == null)
		{
			ResizeItemReserve(m_NumItemColumns * (m_NumItemRows + 1));
		}
	}

	private UIItemGridButton FindSelectedButton(bool allowNull = false)
	{
		UIItemGridButton result = null;
		if (m_SelectedType != null)
		{
			result = FindButtonByItemType(m_SelectedType, allowNull);
		}
		else
		{
			d.Assert(allowNull, "Could not find selected button, as no item is selected");
		}
		return result;
	}

	private void ResizeItemReserve(int newCount)
	{
		int num = ((m_ItemButtonReserve != null) ? m_ItemButtonReserve.Length : 0);
		if (newCount <= num && m_ItemButtonReserve != null)
		{
			d.Assert(condition: false, "ResizeItemReserve didn't send a larger count");
			newCount = num;
		}
		UIItemGridButton[] array = new UIItemGridButton[newCount];
		for (int i = 0; i < num; i++)
		{
			array[i] = m_ItemButtonReserve[i];
		}
		m_ItemButtonReserve = array;
		for (int j = num; j < newCount; j++)
		{
			bool worldPositionStays = false;
			UIItemGridButton uIItemGridButton = UnityEngine.Object.Instantiate(m_ItemPrefab, m_ItemGrid, worldPositionStays);
			uIItemGridButton.Init();
			uIItemGridButton.SetInteractable(!m_PreventSelection);
			uIItemGridButton.RectTransform.sizeDelta = m_ItemSize;
			m_ItemButtonReserve[j] = uIItemGridButton;
			m_ActiveItemButtons.Add(uIItemGridButton);
		}
	}

	private ItemTypeInfo GetNextItemInDirection(ItemTypeInfo fromItemTypeInfo, Vector2 dir)
	{
		ItemTypeInfo result = null;
		int num = m_FilteredItemList.IndexOf(fromItemTypeInfo);
		if (num >= 0)
		{
			int num2 = num % m_NumItemColumns;
			int num3 = num / m_NumItemColumns;
			if (Math.Abs(dir.x) > Math.Abs(dir.y))
			{
				int value = num2 + Math.Sign(dir.x);
				value = Mathf.Clamp(value, 0, m_NumItemColumns - 1);
				if (value != num2)
				{
					int value2 = num3 * m_NumItemColumns + value;
					value2 = Mathf.Clamp(value2, 0, m_FilteredItemList.Count - 1);
					if (value2 != num)
					{
						result = m_FilteredItemList[value2];
					}
				}
			}
			else
			{
				int max = m_FilteredItemList.Count / m_NumItemColumns;
				int value3 = num3 - Math.Sign(dir.y);
				value3 = Mathf.Clamp(value3, 0, max);
				if (!m_ScrollEnabled)
				{
					value3 = Mathf.Clamp(value3, m_VisibleRowIndex, m_VisibleRowIndex + m_NumItemRows);
				}
				if (value3 != num3)
				{
					int value4 = value3 * m_NumItemColumns + num2;
					value4 = Mathf.Clamp(value4, 0, m_FilteredItemList.Count - 1);
					if (value4 != num)
					{
						result = m_FilteredItemList[value4];
					}
				}
			}
		}
		return result;
	}

	private void SetSelectedItem(ItemTypeInfo itemTypeInfo)
	{
		if (PreventSelection || (!(m_SelectedType == null) && !(m_SelectedType != itemTypeInfo)))
		{
			return;
		}
		UIItemGridButton uIItemGridButton = FindSelectedButton(allowNull: true);
		if (uIItemGridButton != null)
		{
			uIItemGridButton.IsOn = false;
		}
		m_SelectedType = itemTypeInfo;
		TryScrollToItem(itemTypeInfo);
		SelectionChanged.Send();
		m_NeighbouringBlockType = -1;
		ItemTypeInfo nextItemInDirection = GetNextItemInDirection(itemTypeInfo, Vector2.left);
		if (nextItemInDirection != null)
		{
			m_NeighbouringBlockType = nextItemInDirection.ItemType;
			return;
		}
		ItemTypeInfo nextItemInDirection2 = GetNextItemInDirection(itemTypeInfo, Vector2.right);
		if (nextItemInDirection2 != null)
		{
			m_NeighbouringBlockType = nextItemInDirection2.ItemType;
		}
	}

	private void ClearSelectedItem()
	{
		m_SelectedType = null;
		SelectionChanged.Send();
	}

	private void OnSpawn()
	{
		SetAllowScroll(enableScroll: true);
	}

	private void OnRecycle()
	{
		m_PreventSelection = false;
		UIItemGridButton[] itemButtonReserve = m_ItemButtonReserve;
		for (int i = 0; i < itemButtonReserve.Length; i++)
		{
			itemButtonReserve[i].SetInteractable(interactable: true);
		}
		m_SelectedType = null;
		m_FilteredItemList.Clear();
	}
}
