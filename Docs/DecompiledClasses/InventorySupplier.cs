#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class InventorySupplier
{
	[Serializable]
	public class SaveData
	{
		[JsonConverter(typeof(InventoryJsonConverter))]
		public IInventory<BlockTypes> inventory = new SingleplayerInventory();

		public int lastUpdateDay;
	}

	[Serializable]
	private struct RarityRangeParams
	{
		[Tooltip("Number of block types of this rarity offered as default")]
		public int m_NumBlockTypes;

		[Tooltip("Additional number of block types offered for each total license grade unlocked; always rounds down")]
		public float m_AdditionalTypesPerGrade;

		[Tooltip("Quantity offered of each block type of this rarity")]
		public int m_QuantityPerBlockType;
	}

	[SerializeField]
	[Tooltip("List of blocks and their quantities that are always available at a trading station")]
	private InventoryBlockList m_BasicBlockList = new InventoryBlockList();

	[SerializeField]
	[EnumArray(typeof(BlockRarity))]
	private RarityRangeParams[] m_RarityRangeParams = new RarityRangeParams[1];

	[SerializeField]
	private int m_ResetEveryNDays = 1;

	[Range(0f, 24f)]
	[SerializeField]
	private float m_TimeOfDayToResetAt = 6f;

	private SaveData m_SaveData = new SaveData();

	private static List<BlockCount> s_BlockList;

	private static WeightedGroup<BlockTypes>[] s_RarityGroups;

	public IInventory<BlockTypes> GetInventory()
	{
		return m_SaveData.inventory;
	}

	public void SetInventory(IInventory<BlockTypes> inventory)
	{
		m_SaveData.inventory = inventory;
	}

	public void Save(ref SaveData saveData)
	{
		UpdateInventoryIfResetPeriodPassed();
		if (saveData == null)
		{
			saveData = new SaveData();
		}
		saveData.inventory.Clear();
		if (m_SaveData.inventory != null)
		{
			foreach (KeyValuePair<BlockTypes, int> item in m_SaveData.inventory)
			{
				saveData.inventory.SetBlockCount(item.Key, item.Value);
			}
		}
		saveData.lastUpdateDay = m_SaveData.lastUpdateDay;
	}

	public void Load(SaveData saveData)
	{
		if (saveData != null)
		{
			if (m_SaveData.inventory == null)
			{
				d.LogError("InventorySupplier.Load: Cannot load into null inventory");
				Clear();
				return;
			}
			if (saveData.inventory == null)
			{
				d.LogError("InventorySupplier.Load: Cannot load from null inventory");
				Clear();
				return;
			}
			m_SaveData.inventory.Clear();
			foreach (KeyValuePair<BlockTypes, int> item in saveData.inventory)
			{
				m_SaveData.inventory.SetBlockCount(item.Key, item.Value);
			}
			m_SaveData.lastUpdateDay = saveData.lastUpdateDay;
			Globals.inst.m_BlockPairsList.InitIgnoreList();
		}
		else
		{
			Clear();
		}
	}

	public void Clear()
	{
		if (m_SaveData.inventory != null)
		{
			m_SaveData.inventory.Clear();
		}
		m_SaveData.lastUpdateDay = 0;
		Globals.inst.m_BlockPairsList.InitIgnoreList();
	}

	public void UpdateInventoryIfResetPeriodPassed()
	{
		float num = (float)((m_SaveData.lastUpdateDay + m_ResetEveryNDays) * 24) + m_TimeOfDayToResetAt;
		float num2 = Singleton.Manager<ManTimeOfDay>.inst.GameDay * 24 + Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay;
		if (ManNetwork.IsHost && num2 >= num)
		{
			RebuildInventory();
			m_SaveData.lastUpdateDay = Singleton.Manager<ManTimeOfDay>.inst.GameDay;
		}
	}

	private void RebuildInventory()
	{
		if (m_SaveData.inventory == null)
		{
			d.LogError("InventorySupplier.RebuildInventory: inventory is null");
			return;
		}
		m_SaveData.inventory.Clear();
		BlockPairsList.BlockPairs[] blockPairs = Globals.inst.m_BlockPairsList.m_BlockPairs;
		CreateRandomInventoryBlockList(ref s_BlockList);
		foreach (BlockCount s_Block in s_BlockList)
		{
			m_SaveData.inventory.HostAddItem(s_Block.m_BlockType, s_Block.m_Quantity);
			for (int i = 0; i < blockPairs.Length; i++)
			{
				if (blockPairs[i].m_Block == s_Block.m_BlockType && blockPairs[i].m_Block != blockPairs[i].m_PairedBlock)
				{
					m_SaveData.inventory.HostAddItem(blockPairs[i].m_PairedBlock, s_Block.m_Quantity);
					Singleton.Manager<ManLicenses>.inst.DiscoverBlock(blockPairs[i].m_PairedBlock);
				}
			}
		}
		s_BlockList.Clear();
	}

	private void CreateRandomInventoryBlockList(ref List<BlockCount> blockList)
	{
		if (blockList == null)
		{
			blockList = new List<BlockCount>();
		}
		blockList.Clear();
		List<BlockTypes> list = new List<BlockTypes>();
		for (int i = 0; i < m_BasicBlockList.Blocks.Length; i++)
		{
			BlockCount blockCount = m_BasicBlockList.Blocks[i];
			if (Singleton.Manager<ManLicenses>.inst.GetBlockState(blockCount.m_BlockType) == ManLicenses.BlockState.Discovered && !Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(blockCount.m_BlockType))
			{
				blockList.Add(blockCount);
				list.Add(blockCount.m_BlockType);
			}
		}
		int num = m_RarityRangeParams.Length;
		if (s_RarityGroups == null)
		{
			s_RarityGroups = new WeightedGroup<BlockTypes>[num];
		}
		BlockUnlockTable blockUnlockTable = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable();
		for (int j = 0; j < Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Count; j++)
		{
			FactionSubTypes factionSubTypes = Singleton.Manager<ManPurchases>.inst.AvailableCorporations[j];
			bool flag = factionSubTypes == FactionSubTypes.SPE;
			bool flag2 = Singleton.Manager<ManLicenses>.inst.IsLicenseDiscovered(factionSubTypes);
			if (Singleton.Manager<ManMods>.inst.IsModdedCorp(factionSubTypes))
			{
				ModdedCorpDefinition moddedCorpDefinition = Singleton.Manager<ManMods>.inst.FindCorp((int)factionSubTypes);
				if (moddedCorpDefinition != null)
				{
					flag2 = Singleton.Manager<ManLicenses>.inst.IsLicenseDiscovered(Singleton.Manager<ManMods>.inst.GetCorpIndex(moddedCorpDefinition.m_RewardCorp));
				}
				else
				{
					d.LogError($"Modded corp {factionSubTypes} had no definition in the current session? Nothing will appear in shops");
				}
			}
			if (!(flag2 || flag))
			{
				continue;
			}
			List<BlockTypes> allBlocksForFaction = blockUnlockTable.GetAllBlocksForFaction(factionSubTypes, filterByGameMode: true, filterPurchasable: true);
			for (int k = 0; k < allBlocksForFaction.Count; k++)
			{
				BlockTypes blockTypes = allBlocksForFaction[k];
				if (Singleton.Manager<ManLicenses>.inst.GetBlockState(blockTypes) == ManLicenses.BlockState.Discovered && !Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(blockTypes))
				{
					BlockRarity rarity = Singleton.Manager<ManSpawn>.inst.GetRarity(blockTypes);
					int num2 = 1;
					s_RarityGroups[(int)rarity].SetWeight(blockTypes, num2);
				}
			}
		}
		for (int l = 0; l < num; l++)
		{
			int num3 = (int)(m_RarityRangeParams[l].m_AdditionalTypesPerGrade * (float)Singleton.Manager<ManLicenses>.inst.GetTotalLicenseGrades());
			int num4 = Mathf.Min(m_RarityRangeParams[l].m_NumBlockTypes + num3, s_RarityGroups[l].Count);
			for (int m = 0; m < num4; m++)
			{
				BlockTypes random;
				bool flag3;
				do
				{
					random = s_RarityGroups[l].GetRandom();
					s_RarityGroups[l].SetWeight(random, 0f);
					flag3 = !Globals.inst.m_BlockPairsList.PairedBlocksToIgnore.Contains(random) && !list.Contains(random);
				}
				while (!flag3 && s_RarityGroups[l].Count > 0);
				if (flag3)
				{
					int quantityPerBlockType = m_RarityRangeParams[l].m_QuantityPerBlockType;
					blockList.Add(new BlockCount(random, quantityPerBlockType));
				}
				if (s_RarityGroups[l].Count == 0)
				{
					break;
				}
			}
			s_RarityGroups[l].Clear();
		}
	}
}
