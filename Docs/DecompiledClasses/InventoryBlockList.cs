using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class InventoryBlockList : ICloneable
{
	[SerializeField]
	public BlockCount[] m_BlockList = new BlockCount[0];

	public BlockCount[] Blocks => m_BlockList;

	public InventoryBlockList()
	{
	}

	public InventoryBlockList(IEnumerable<BlockCount> counts)
	{
		m_BlockList = counts.ToArray();
	}

	public void BuildInventory(IInventory<BlockTypes> inventory)
	{
		for (int i = 0; i < m_BlockList.Length; i++)
		{
			BlockCount blockCount = m_BlockList[i];
			inventory.SetBlockCount(blockCount.m_BlockType, blockCount.m_Quantity);
		}
	}

	public object Clone()
	{
		InventoryBlockList inventoryBlockList = new InventoryBlockList();
		inventoryBlockList.m_BlockList = new BlockCount[m_BlockList.Length];
		for (int i = 0; i < m_BlockList.Length; i++)
		{
			inventoryBlockList.m_BlockList[i] = (BlockCount)m_BlockList[i].Clone();
		}
		return inventoryBlockList;
	}

	public void AddBlocks(InventoryBlockList additionalBlockList)
	{
		List<BlockCount> list = m_BlockList.ToList();
		for (int i = 0; i < additionalBlockList.m_BlockList.Length; i++)
		{
			bool flag = true;
			for (int j = 0; j < list.Count; j++)
			{
				if (additionalBlockList.m_BlockList[i].m_BlockType == list[j].m_BlockType)
				{
					list[j].m_Quantity += additionalBlockList.m_BlockList[i].m_Quantity;
					flag = false;
				}
			}
			if (flag)
			{
				list.Add((BlockCount)additionalBlockList.m_BlockList[i].Clone());
			}
		}
		m_BlockList = list.ToArray();
	}

	public void NetSerialize(NetworkWriter writer)
	{
		writer.Write(m_BlockList.Length);
		for (int i = 0; i < m_BlockList.Length; i++)
		{
			writer.Write((int)m_BlockList[i].m_BlockType);
			writer.Write(m_BlockList[i].m_Quantity);
		}
	}

	public void NetDeserialize(NetworkReader reader)
	{
		int num = reader.ReadInt32();
		m_BlockList = new BlockCount[num];
		for (int i = 0; i < num; i++)
		{
			m_BlockList[i] = new BlockCount((BlockTypes)reader.ReadInt32(), reader.ReadInt32());
		}
	}
}
