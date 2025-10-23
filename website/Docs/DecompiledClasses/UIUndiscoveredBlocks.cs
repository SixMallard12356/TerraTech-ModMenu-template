using System.Collections.Generic;
using UnityEngine;

public class UIUndiscoveredBlocks : MonoBehaviour
{
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

	private List<UIItemGridButton> m_MissingBlockUIItems = new List<UIItemGridButton>();

	public void SetActive(bool isActive)
	{
		base.gameObject.SetActive(isActive);
	}

	public void PopulateItems(TechDataAvailValidation techBlockCache)
	{
		List<UIItemGridButton> missingBlockUIItems = m_MissingBlockUIItems;
		for (int i = 0; i < missingBlockUIItems.Count; i++)
		{
			missingBlockUIItems[i].SetUsed(used: false);
		}
		int num = 0;
		foreach (KeyValuePair<BlockTypes, TechDataAvailValidation.BlockTypeAvailability> item in techBlockCache.BlockAvailability)
		{
			BlockTypes key = item.Key;
			TechDataAvailValidation.BlockTypeAvailability value = item.Value;
			if (value.availability < TechDataAvailValidation.BlockAvailableState.Available)
			{
				UIItemGridButton.DisplayParams dParams = new UIItemGridButton.DisplayParams
				{
					m_ItemColour = Color.white,
					m_ItemBackgroundColour = new Color(1f, 1f, 1f, 0f),
					m_ShowAsUndiscovered = (value.availability == TechDataAvailValidation.BlockAvailableState.NotAvailableInProgression),
					m_DisplayQuantity = true,
					m_Quantity = value.numRequired - value.numInInventory,
					m_ShowDeathStreakReward = false
				};
				UIItemGridButton blockUI = GetBlockUI(m_MissingBlockUIItems, num);
				ItemTypeInfo itemTypeInfo = new ItemTypeInfo(ObjectTypes.Block, (int)key);
				blockUI.SetupItem(itemTypeInfo, dParams);
				num++;
			}
		}
		for (int num2 = missingBlockUIItems.Count - 1; num2 >= 0; num2--)
		{
			UIItemGridButton uIItemGridButton = missingBlockUIItems[num2];
			if (!uIItemGridButton.GetUsed())
			{
				uIItemGridButton.RectTransform.SetParent(null, worldPositionStays: false);
				uIItemGridButton.Recycle();
				missingBlockUIItems.Remove(uIItemGridButton);
			}
		}
		m_UndiscoveredBlocksGridContainer.SetActive(!techBlockCache.m_ExceedsBlockLimitPlace);
		m_TitleText.SetActive(!techBlockCache.m_ExceedsBlockLimitPlace);
		m_BlockLimiterText.SetActive(techBlockCache.m_ExceedsBlockLimitPlace);
	}

	private UIItemGridButton GetBlockUI(List<UIItemGridButton> items, int i)
	{
		UIItemGridButton uIItemGridButton;
		if (i < items.Count)
		{
			uIItemGridButton = items[i];
		}
		else
		{
			uIItemGridButton = m_BlockUIPrefab.Spawn();
			uIItemGridButton.Init();
			uIItemGridButton.RectTransform.SetParent(m_UndiscoveredBlocksGrid, worldPositionStays: false);
			m_MissingBlockUIItems.Add(uIItemGridButton);
		}
		uIItemGridButton.SetUsed(used: true);
		uIItemGridButton.SetVisible(visible: true);
		return uIItemGridButton;
	}

	private void Clear()
	{
		List<UIItemGridButton> missingBlockUIItems = m_MissingBlockUIItems;
		for (int num = missingBlockUIItems.Count - 1; num >= 0; num--)
		{
			UIItemGridButton uIItemGridButton = missingBlockUIItems[num];
			uIItemGridButton.SetUsed(used: false);
			uIItemGridButton.RectTransform.SetParent(null, worldPositionStays: false);
			uIItemGridButton.Recycle();
			missingBlockUIItems.Remove(uIItemGridButton);
		}
	}

	private void OnRecycle()
	{
		Clear();
	}

	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled() && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.MainGame)
		{
			float axis = Singleton.Manager<ManInput>.inst.GetAxis(7);
			m_UndiscoveredBlockScrollRect.Scroll((0f - axis) * Time.deltaTime * Globals.inst.m_StickScrollSensitivity);
			bool activeSelf = m_UndiscoveredBlockScrollBar.activeSelf;
			TankCamera.inst.SetMouseControlEnabled(!activeSelf);
		}
	}
}
