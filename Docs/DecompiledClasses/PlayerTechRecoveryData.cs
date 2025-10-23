using System.Collections.Generic;
using UnityEngine;

public class PlayerTechRecoveryData
{
	public TechData m_RecoveryTechData;

	public int m_TotalRecoveryCost;

	public int m_BlockRecoveryCost;

	public int m_BlocksFromShopCost;

	public int m_NumBlocksRecoverable;

	public int m_NumBlocksAvailableFromInventory;

	public int m_NumBlocksFromShop;

	public Dictionary<int, int> m_BlocksAvailableFromInventory;

	public PlayerTechRecoveryData(TechData techData, float blockShopCostMultiplier = 1f, List<Visible.WeakReference> recoveryBlocks = null, float blockRecoveryCostMultiplier = 0f)
	{
		m_RecoveryTechData = techData;
		m_TotalRecoveryCost = 0;
		m_BlockRecoveryCost = 0;
		m_BlocksFromShopCost = 0;
		m_NumBlocksRecoverable = 0;
		m_NumBlocksAvailableFromInventory = 0;
		m_NumBlocksFromShop = 0;
		m_BlocksAvailableFromInventory = new Dictionary<int, int>();
		if (techData == null)
		{
			return;
		}
		Dictionary<int, int> dictionary = new Dictionary<int, int>();
		for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
		{
			int blockType = (int)techData.m_BlockSpecs[i].GetBlockType();
			int value = 0;
			dictionary.TryGetValue(blockType, out value);
			value++;
			dictionary[blockType] = value;
		}
		float num = 0f;
		if (recoveryBlocks != null)
		{
			for (int j = 0; j < recoveryBlocks.Count; j++)
			{
				Visible visible = recoveryBlocks[j].Get();
				if (!(visible != null) || !(visible.block != null))
				{
					continue;
				}
				BlockTypes blockType2 = visible.block.BlockType;
				int key = (int)blockType2;
				int value2 = 0;
				if (dictionary.TryGetValue(key, out value2))
				{
					m_NumBlocksRecoverable++;
					num += (float)Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(blockType2, silentFail: true) * blockRecoveryCostMultiplier;
					if (value2 > 1)
					{
						dictionary[key] = value2 - 1;
					}
					else
					{
						dictionary.Remove(key);
					}
				}
			}
		}
		m_BlockRecoveryCost = Mathf.RoundToInt(num);
		float num2 = 0f;
		IInventory<BlockTypes> playerInventory = Singleton.Manager<ManPlayer>.inst.PlayerInventory;
		foreach (KeyValuePair<int, int> item in dictionary)
		{
			BlockTypes key2 = (BlockTypes)item.Key;
			int num3 = item.Value;
			int b = (Singleton.Manager<ManPlayer>.inst.InventoryIsUnrestricted ? num3 : playerInventory.GetQuantity(key2));
			int num4 = Mathf.Min(num3, b);
			if (num4 > 0)
			{
				m_NumBlocksAvailableFromInventory += num4;
				m_BlocksAvailableFromInventory.Add((int)key2, num4);
				num3 -= num4;
			}
			if (num3 > 0)
			{
				m_NumBlocksFromShop += num3;
				num2 += (float)(num3 * Singleton.Manager<RecipeManager>.inst.GetBlockBuyPrice(key2, silentFail: true)) * blockShopCostMultiplier;
			}
		}
		m_BlocksFromShopCost = Mathf.RoundToInt(num2);
		m_TotalRecoveryCost = m_BlockRecoveryCost + m_BlocksFromShopCost;
	}
}
