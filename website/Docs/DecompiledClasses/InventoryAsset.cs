#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class InventoryAsset : ScriptableObject
{
	[SerializeField]
	private InventoryBlockList m_BlockList = new InventoryBlockList();

	public void BuildInventory(IInventory<BlockTypes> inventory)
	{
		m_BlockList.BuildInventory(inventory);
	}

	public bool Contains(BlockTypes bt)
	{
		if (m_BlockList != null && m_BlockList.m_BlockList != null)
		{
			for (int i = 0; i < m_BlockList.m_BlockList.Length; i++)
			{
				if (m_BlockList.m_BlockList[i].m_BlockType == bt)
				{
					return true;
				}
			}
		}
		return false;
	}

	public IEnumerable<BlockTypes> EnumerateBlockTypes()
	{
		if (m_BlockList == null || m_BlockList.m_BlockList == null)
		{
			yield break;
		}
		int i = 0;
		while (i < m_BlockList.m_BlockList.Length)
		{
			if (m_BlockList.m_BlockList[i].m_Quantity == 0)
			{
				d.LogWarningFormat("BlockType {0} has a quantity of 0 in inventory {1}", m_BlockList.m_BlockList[i].m_BlockType, base.name);
			}
			yield return m_BlockList.m_BlockList[i].m_BlockType;
			int num = i + 1;
			i = num;
		}
	}
}
