using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public class SingleplayerInventory : IInventory<BlockTypes>
{
	[NonSerialized]
	public Event<BlockTypes, int> InventoryChanged;

	[JsonProperty]
	private BlockCountList m_BlockCounts = new BlockCountList();

	public IEnumerator<KeyValuePair<BlockTypes, int>> GetEnumerator()
	{
		return m_BlockCounts.GetEnumerator();
	}

	public int GetQuantity(BlockTypes blockType)
	{
		return m_BlockCounts.GetQuantity(blockType);
	}

	public int GetUnreservedQuantity(BlockTypes blockType)
	{
		return GetQuantity(blockType);
	}

	public bool GetIsDeathStreakReward(BlockTypes blockType)
	{
		return false;
	}

	public void SubscribeToInventoryChanged(Action<BlockTypes, int> _delegate)
	{
		InventoryChanged.Subscribe(_delegate);
	}

	public void UnsubscribeToInventoryChanged(Action<BlockTypes, int> _delegate)
	{
		InventoryChanged.Unsubscribe(_delegate);
	}

	public bool IsAvailableToLocalPlayer(BlockTypes blockType)
	{
		return GetQuantity(blockType) != 0;
	}

	public int GetNumReserved(BlockTypes blockType)
	{
		return 0;
	}

	public bool CanReserveItem(int netPlayerID, BlockTypes blockType)
	{
		return GetQuantity(blockType) != 0;
	}

	public bool HostReserveItem(int netPlayerID, BlockTypes blockType)
	{
		return CanReserveItem(netPlayerID, blockType);
	}

	public bool CancelReserveItem(int netPlayerID, BlockTypes blockType)
	{
		return true;
	}

	public bool HasReservedItem(int netPlayerID, BlockTypes blockType)
	{
		return true;
	}

	public bool CanConsumeItem(int netPlayerID, BlockTypes blockType)
	{
		return GetQuantity(blockType) != 0;
	}

	public int HostConsumeItem(int netPlayerID, BlockTypes blockType, int count = 1)
	{
		int num = m_BlockCounts.ConsumeItem(blockType, count);
		InventoryQuantityChanged(new BlockCount(blockType, num));
		return num;
	}

	public void HostAddItem(BlockTypes blockType, int count = 1)
	{
		int quantity = m_BlockCounts.AddItem(blockType, count);
		InventoryQuantityChanged(new BlockCount(blockType, quantity));
	}

	public void HostStoreTech(TechData techData)
	{
		foreach (TankPreset.BlockSpec blockSpec in techData.m_BlockSpecs)
		{
			HostAddItem(blockSpec.GetBlockType());
		}
	}

	public bool HasItemsToSpawnTech(TechData techData)
	{
		return HasItemsToSpawnTech(new BlockCountList(techData));
	}

	public bool HasItemsToSpawnTech(BlockCountList counts)
	{
		foreach (KeyValuePair<BlockTypes, int> count in counts)
		{
			BlockTypes key = count.Key;
			int value = count.Value;
			int quantity = GetQuantity(key);
			if (quantity != -1 && quantity < value)
			{
				return false;
			}
		}
		return true;
	}

	public void SetBlockCount(BlockTypes blockType, int count)
	{
		m_BlockCounts.SetQuantity(blockType, count);
		InventoryQuantityChanged(new BlockCount(blockType, count));
	}

	public void Clear()
	{
		m_BlockCounts.Clear();
	}

	public void FillTo(InventoryBlockList list)
	{
		if (list == null)
		{
			return;
		}
		for (int i = 0; i < list.m_BlockList.Length; i++)
		{
			BlockCount blockCount = list.m_BlockList[i];
			int quantity = GetQuantity(blockCount.m_BlockType);
			if (quantity != -1)
			{
				if (blockCount.m_Quantity == -1)
				{
					HostAddItem(blockCount.m_BlockType, -1);
				}
				else if (blockCount.m_Quantity > quantity)
				{
					HostAddItem(blockCount.m_BlockType, blockCount.m_Quantity - quantity);
				}
			}
		}
	}

	protected void InventoryQuantityChanged(BlockCount blockCount)
	{
		if (this == Singleton.Manager<ManPlayer>.inst.PlayerInventory)
		{
			Singleton.Manager<ManStats>.inst.InventoryQuantityUpdated(blockCount.m_BlockType, blockCount.m_Quantity);
		}
		InventoryChanged.Send(blockCount.m_BlockType, blockCount.m_Quantity);
	}
}
