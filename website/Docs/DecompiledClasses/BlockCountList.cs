#define UNITY_EDITOR
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class BlockCountList
{
	[JsonProperty]
	private Dictionary<BlockTypes, int> m_Counts;

	public BlockCountList()
	{
		m_Counts = new Dictionary<BlockTypes, int>(new BlockTypesComparer());
	}

	public BlockCountList(TechData techData)
	{
		m_Counts = new Dictionary<BlockTypes, int>(new BlockTypesComparer());
		for (int i = 0; i < techData.m_BlockSpecs.Count; i++)
		{
			AddItem(techData.m_BlockSpecs[i].GetBlockType(), 1);
		}
	}

	public Dictionary<BlockTypes, int>.Enumerator GetEnumerator()
	{
		return m_Counts.GetEnumerator();
	}

	public int GetQuantity(BlockTypes blockType)
	{
		m_Counts.TryGetValue(blockType, out var value);
		return value;
	}

	public void SetQuantity(BlockTypes blockType, int count, bool keepZeroes = false)
	{
		if (count == 0 && !keepZeroes)
		{
			m_Counts.Remove(blockType);
		}
		else
		{
			m_Counts[blockType] = count;
		}
	}

	public int ConsumeItem(BlockTypes blockType, int count)
	{
		d.Assert(count != -1, string.Concat("Inventory - Attempting to remove INFINITE quantity of blocks of type ", blockType, ". Not currently supported!"));
		int num = GetQuantity(blockType);
		if (num != -1)
		{
			if (count > num)
			{
				d.LogError(string.Concat("Inventory - Attempting to remove ", count, " blocks of type ", blockType, ", but only have ", num, " available"));
			}
			count = Mathf.Min(count, num);
			num -= count;
		}
		SetQuantity(blockType, num);
		return num;
	}

	public int AddItem(BlockTypes blockType, int count)
	{
		int quantity = GetQuantity(blockType);
		quantity = ((quantity != -1 && count != -1) ? (quantity + count) : (-1));
		m_Counts[blockType] = quantity;
		return quantity;
	}

	public void Clear()
	{
		m_Counts.Clear();
	}

	public void CreateZeroedCopyOf(ref BlockCountList list)
	{
		m_Counts.Clear();
		foreach (BlockTypes key in list.m_Counts.Keys)
		{
			m_Counts.Add(key, 0);
		}
	}

	public void UpdateCountsFrom(BlockCountList other)
	{
		foreach (KeyValuePair<BlockTypes, int> count in other.m_Counts)
		{
			SetQuantity(count.Key, count.Value);
		}
	}

	public void WriteTo(NetworkWriter writer)
	{
		writer.Write(m_Counts.Count);
		foreach (KeyValuePair<BlockTypes, int> count in m_Counts)
		{
			writer.Write((int)count.Key);
			writer.Write(count.Value);
		}
	}

	public void ReadFrom(NetworkReader reader)
	{
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			SetQuantity((BlockTypes)reader.ReadInt32(), reader.ReadInt32(), keepZeroes: true);
		}
	}
}
