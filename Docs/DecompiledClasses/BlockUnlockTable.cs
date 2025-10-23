#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockUnlockTable : ScriptableObject
{
	[Serializable]
	public class CorpBlockData
	{
		[SerializeField]
		public GradeData[] m_GradeList = new GradeData[0];
	}

	[Serializable]
	public class GradeData
	{
		[SerializeField]
		public UnlockData[] m_BlockList = new UnlockData[0];

		[SerializeField]
		public BlockTypes[] m_AdditionalUnlocks = new BlockTypes[0];
	}

	[Serializable]
	public class UnlockData
	{
		public BlockTypes m_BlockType;

		public bool m_BasicBlock;

		public bool m_DontRewardOnLevelUp;

		public bool m_HideOnLevelUpScreen;

		public bool m_DisallowInShop;
	}

	public enum BlockAvailabilty
	{
		LevelUpScreenBlocks,
		InitialRewardBlocks,
		BasicBlocks,
		AllBlocks
	}

	[SerializeField]
	private CorpBlockData[] m_CorpBlockList = new CorpBlockData[0];

	private Dictionary<int, CorpBlockData> m_CustomCorpBlockData = new Dictionary<int, CorpBlockData>();

	private Dictionary<int, Dictionary<BlockTypes, int>> m_CorpBlockLevelLookup;

	private Dictionary<BlockTypes, int> m_AllBlockLevelLookup;

	public static readonly int kDefaultBlockTier = int.MaxValue;

	public IEnumerator<KeyValuePair<int, CorpBlockData>> GetEnumerator()
	{
		for (int i = 0; i < m_CorpBlockList.Length; i++)
		{
			yield return new KeyValuePair<int, CorpBlockData>(i, m_CorpBlockList[i]);
		}
		foreach (KeyValuePair<int, CorpBlockData> customCorpBlockDatum in m_CustomCorpBlockData)
		{
			yield return customCorpBlockDatum;
		}
	}

	public CorpBlockData GetCorpBlockData(int corpIndex)
	{
		CorpBlockData value = null;
		if (0 <= corpIndex && corpIndex < m_CorpBlockList.Length)
		{
			value = m_CorpBlockList[corpIndex];
		}
		else if (!m_CustomCorpBlockData.TryGetValue(corpIndex, out value))
		{
			d.LogError("Could not load custom corp block. This feature is pending");
		}
		return value;
	}

	public void Init()
	{
		m_CorpBlockLevelLookup = new Dictionary<int, Dictionary<BlockTypes, int>>();
		m_AllBlockLevelLookup = new Dictionary<BlockTypes, int>(new BlockTypesComparer());
		using IEnumerator<KeyValuePair<int, CorpBlockData>> enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<int, CorpBlockData> current = enumerator.Current;
			int key = current.Key;
			CorpBlockData value = current.Value;
			Dictionary<BlockTypes, int> dictionary = new Dictionary<BlockTypes, int>(new BlockTypesComparer());
			m_CorpBlockLevelLookup.Add(key, dictionary);
			if (value == null)
			{
				continue;
			}
			for (int i = 0; i < value.m_GradeList.Length; i++)
			{
				for (int j = 0; j < value.m_GradeList[i].m_BlockList.Length; j++)
				{
					BlockTypes blockType = value.m_GradeList[i].m_BlockList[j].m_BlockType;
					if (!dictionary.ContainsKey(blockType))
					{
						dictionary.Add(blockType, i);
					}
					else
					{
						d.LogError(string.Concat("BlockUnlockTable.Init: Block ", blockType, " already added to CorpBlocksLookup"));
					}
					if (!m_AllBlockLevelLookup.ContainsKey(blockType))
					{
						m_AllBlockLevelLookup.Add(blockType, i);
					}
				}
			}
		}
	}

	public bool ContainsBlock(BlockTypes blockType)
	{
		return m_AllBlockLevelLookup.ContainsKey(blockType);
	}

	public void AddModdedCorps(Dictionary<int, ModdedCorpDefinition> corps)
	{
		foreach (KeyValuePair<int, ModdedCorpDefinition> item in corps)
		{
			m_CustomCorpBlockData.Add(item.Key, new CorpBlockData
			{
				m_GradeList = new GradeData[1]
				{
					new GradeData()
				}
			});
		}
	}

	public void RemoveModdedCorps()
	{
		m_CustomCorpBlockData.Clear();
	}

	public void AddModdedBlocks(int corpIndex, int gradeIndex, Dictionary<BlockTypes, ModdedBlockDefinition> blocks)
	{
		if (blocks.Count <= 0)
		{
			return;
		}
		CorpBlockData corpBlockData = GetCorpBlockData(corpIndex);
		if (corpBlockData == null)
		{
			return;
		}
		gradeIndex = Mathf.Clamp(gradeIndex, 0, corpBlockData.m_GradeList.Length - 1);
		int num = corpBlockData.m_GradeList[gradeIndex].m_BlockList.Length;
		Array.Resize(ref corpBlockData.m_GradeList[gradeIndex].m_BlockList, num + blocks.Count);
		int num2 = 0;
		foreach (KeyValuePair<BlockTypes, ModdedBlockDefinition> block in blocks)
		{
			corpBlockData.m_GradeList[gradeIndex].m_BlockList[num + num2] = new UnlockData
			{
				m_BlockType = block.Key,
				m_BasicBlock = true,
				m_DontRewardOnLevelUp = !block.Value.m_UnlockWithLicense,
				m_HideOnLevelUpScreen = true
			};
			num2++;
		}
	}

	public void RemoveModdedBlocks()
	{
		using IEnumerator<KeyValuePair<int, CorpBlockData>> enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			CorpBlockData value = enumerator.Current.Value;
			for (int i = 0; i < value.m_GradeList.Length; i++)
			{
				int num = -1;
				UnlockData[] array = value.m_GradeList[i].m_BlockList;
				for (int j = 0; j < array.Length; j++)
				{
					if (Singleton.Manager<ManMods>.inst.IsModdedBlock(array[j].m_BlockType))
					{
						num = j;
						break;
					}
				}
				if (num != -1)
				{
					Array.Resize(ref array, num);
					value.m_GradeList[i].m_BlockList = array;
				}
			}
		}
	}

	public int GetBlockTier(BlockTypes blockType, bool supressWarnings)
	{
		int corporation = (int)Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType);
		if (m_CorpBlockLevelLookup.TryGetValue(corporation, out var value))
		{
			if (value.TryGetValue(blockType, out var value2))
			{
				return value2;
			}
			if (!supressWarnings)
			{
				d.LogError(string.Concat("BlockUnlockTable.GetBlockTier: BlockType ", blockType, " is not in the Block Unlock Table - Returning Invalid Tier"));
			}
		}
		return kDefaultBlockTier;
	}

	public List<BlockTypes> GetAllBlocksInTier(int grade, FactionSubTypes corporation, bool includeBonusBlocks)
	{
		return GetBlocksInTier(BlockAvailabilty.AllBlocks, grade, corporation, includeBonusBlocks);
	}

	public List<BlockTypes> GetInitialBlocksInTier(int grade, FactionSubTypes corporation)
	{
		return GetBlocksInTier(BlockAvailabilty.BasicBlocks, grade, corporation, includeBonusBlocks: true);
	}

	public List<BlockTypes> GetInitialRewardBlocksInTier(int grade, FactionSubTypes corporation)
	{
		return GetBlocksInTier(BlockAvailabilty.InitialRewardBlocks, grade, corporation);
	}

	public List<BlockTypes> GetLevelUpScreenBlocksInTier(int grade, FactionSubTypes corporation)
	{
		return GetBlocksInTier(BlockAvailabilty.LevelUpScreenBlocks, grade, corporation);
	}

	public List<BlockTypes> GetAllBlocksForFaction(FactionSubTypes corporation, bool filterByGameMode = true, bool filterPurchasable = false)
	{
		List<BlockTypes> list = new List<BlockTypes>((m_AllBlockLevelLookup != null && m_CorpBlockList.Length != 0) ? (m_AllBlockLevelLookup.Count / m_CorpBlockList.Length) : 128);
		CorpBlockData corpBlockData = GetCorpBlockData((int)corporation);
		if (corpBlockData != null)
		{
			int num = corpBlockData.m_GradeList.Length;
			for (int i = 0; i < num; i++)
			{
				AddCorpBlocksFromGrade(i, corporation, BlockAvailabilty.AllBlocks, list, addBonusBlocks: false, filterByPlatform: true, filterByGameMode, filterPurchasable);
			}
		}
		return list;
	}

	public List<BlockTypes> GetAllBlocksUnfiltered()
	{
		List<BlockTypes> list = new List<BlockTypes>((m_AllBlockLevelLookup != null) ? m_AllBlockLevelLookup.Count : 512);
		GetAllBlocksUnfiltered(list);
		return list;
	}

	public ICollection<BlockTypes> GetAllBlocksUnfiltered(ICollection<BlockTypes> blockTypes, bool addBonusBlocks = true)
	{
		using IEnumerator<KeyValuePair<int, CorpBlockData>> enumerator = GetEnumerator();
		while (enumerator.MoveNext())
		{
			KeyValuePair<int, CorpBlockData> current = enumerator.Current;
			int key = current.Key;
			int num = current.Value.m_GradeList.Length;
			for (int i = 0; i < num; i++)
			{
				AddCorpBlocksFromGrade(i, (FactionSubTypes)key, BlockAvailabilty.AllBlocks, blockTypes, addBonusBlocks, filterByPlatform: false, filterByGameMode: false);
			}
		}
		return blockTypes;
	}

	public int GetMaxGrade(FactionSubTypes corporation)
	{
		int result = 0;
		CorpBlockData corpBlockData = GetCorpBlockData((int)corporation);
		if (corpBlockData != null)
		{
			result = corpBlockData.m_GradeList.Length;
		}
		else
		{
			d.LogError(string.Concat("BlockUnlockTable.GetMaxGrade: Corp ", corporation, " is not in Table"));
		}
		return result;
	}

	private List<BlockTypes> GetBlocksInTier(BlockAvailabilty availability, int grade, FactionSubTypes corporation, bool includeBonusBlocks = false)
	{
		List<BlockTypes> list = new List<BlockTypes>();
		AddCorpBlocksFromGrade(grade, corporation, availability, list, includeBonusBlocks, filterByPlatform: false);
		return list;
	}

	private static bool IsBlockAllowed(BlockTypes blockType, bool filterByPlatform, bool filterByGameMode)
	{
		if (filterByPlatform && !Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockType))
		{
			return false;
		}
		if (filterByGameMode && (Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType) || !Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(blockType)))
		{
			return false;
		}
		return true;
	}

	private void AddCorpBlocksFromGrade(int grade, FactionSubTypes corporation, BlockAvailabilty availability, ICollection<BlockTypes> toCollection, bool addBonusBlocks = false, bool filterByPlatform = true, bool filterByGameMode = true, bool filterPurchasable = false)
	{
		CorpBlockData corpBlockData = GetCorpBlockData((int)corporation);
		if (corpBlockData == null || grade >= corpBlockData.m_GradeList.Length)
		{
			return;
		}
		GradeData gradeData = corpBlockData.m_GradeList[grade];
		for (int i = 0; i < gradeData.m_BlockList.Length; i++)
		{
			UnlockData unlockData = gradeData.m_BlockList[i];
			bool flag = true;
			if (availability != BlockAvailabilty.AllBlocks)
			{
				if (!unlockData.m_BasicBlock)
				{
					flag = false;
				}
				else
				{
					switch (availability)
					{
					case BlockAvailabilty.InitialRewardBlocks:
						flag = !unlockData.m_DontRewardOnLevelUp;
						break;
					case BlockAvailabilty.LevelUpScreenBlocks:
						flag = !unlockData.m_HideOnLevelUpScreen;
						break;
					}
				}
			}
			if (filterPurchasable && unlockData.m_DisallowInShop)
			{
				flag = false;
			}
			if (flag && IsBlockAllowed(unlockData.m_BlockType, filterByPlatform, filterByGameMode) && !toCollection.Contains(unlockData.m_BlockType))
			{
				toCollection.Add(unlockData.m_BlockType);
			}
		}
		if (!addBonusBlocks)
		{
			return;
		}
		for (int j = 0; j < gradeData.m_AdditionalUnlocks.Length; j++)
		{
			BlockTypes blockTypes = gradeData.m_AdditionalUnlocks[j];
			if (IsBlockAllowed(blockTypes, filterByPlatform, filterByGameMode) && !toCollection.Contains(blockTypes))
			{
				toCollection.Add(blockTypes);
			}
		}
	}
}
