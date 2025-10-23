#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockRewardPoolTable : ScriptableObject
{
	[Serializable]
	private class CorpRewardTiers
	{
		[SerializeField]
		public FactionSubTypes m_Corp;

		[SerializeField]
		public TierRewardPool[] m_Tiers = new TierRewardPool[0];

		public TierRewardPool this[int i]
		{
			get
			{
				return m_Tiers[i];
			}
			set
			{
				m_Tiers[i] = value;
			}
		}

		public int NumTiers => m_Tiers.Length;
	}

	[Serializable]
	private class TierRewardPool
	{
		[SerializeField]
		public RewardBlockInfo[] m_RewardPool = new RewardBlockInfo[0];

		public RewardBlockInfo this[int i]
		{
			get
			{
				return m_RewardPool[i];
			}
			set
			{
				m_RewardPool[i] = value;
			}
		}

		public int Length => m_RewardPool.Length;
	}

	[Serializable]
	public class RewardBlockInfo
	{
		public BlockTypes m_BlockType;

		public BlockTypes[] m_PrerequisiteBlocks;

		public bool PrerequisitesMet()
		{
			bool result = true;
			for (int i = 0; i < m_PrerequisiteBlocks.Length; i++)
			{
				if (Singleton.Manager<ManLicenses>.inst.GetBlockState(m_PrerequisiteBlocks[i]) != ManLicenses.BlockState.Discovered)
				{
					result = false;
					break;
				}
			}
			return result;
		}
	}

	[SerializeField]
	private CorpRewardTiers[] m_CorpRewardPools = new CorpRewardTiers[0];

	private Dictionary<FactionSubTypes, CorpRewardTiers> m_ModdedRewards = new Dictionary<FactionSubTypes, CorpRewardTiers>(default(FactionSubTypesComparer));

	public BlockTypes[] GetRewardBlocks(FactionSubTypes corp, int amount)
	{
		BlockTypes[] array = new BlockTypes[amount];
		int numFound = 0;
		bool flag = false;
		List<BlockTypes> blocksToIgnore = Globals.inst.m_BlockPairsList.PairedBlocksToIgnore;
		List<BlockTypes> undiscoveredBlocks = new List<BlockTypes>();
		List<BlockTypes> discoveredBlocks = new List<BlockTypes>();
		for (int i = 0; i < m_CorpRewardPools.Length; i++)
		{
			if (m_CorpRewardPools[i].m_Corp == corp)
			{
				FillFromCorp(m_CorpRewardPools[i], amount, ref undiscoveredBlocks, ref discoveredBlocks, ref blocksToIgnore);
				flag = true;
				break;
			}
		}
		if (m_ModdedRewards.TryGetValue(corp, out var value))
		{
			FillFromCorp(value, amount, ref undiscoveredBlocks, ref discoveredBlocks, ref blocksToIgnore);
			flag = true;
		}
		numFound = FillBlockList(array, undiscoveredBlocks, amount, numFound);
		if (numFound < amount)
		{
			numFound = FillBlockList(array, discoveredBlocks, amount, numFound);
			if (numFound < amount)
			{
				d.LogErrorFormat("BlockRewardPoolTable.GetRewardBlock - Not enough Discovered or Undiscovered Blocks available to pick from. Found {0}/{1}", numFound, amount);
			}
		}
		if (!flag)
		{
			d.LogError(string.Concat("BlockRewardPoolTable.GetRewardBlock - Corp: ", corp, " isn't in Reward Table"));
		}
		for (; numFound < amount; numFound++)
		{
			array[numFound] = BlockTypes.GSOBlock_111;
		}
		return array;
	}

	private void FillFromCorp(CorpRewardTiers rewards, int amount, ref List<BlockTypes> undiscoveredBlocks, ref List<BlockTypes> discoveredBlocks, ref List<BlockTypes> blocksToIgnore)
	{
		int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(rewards.m_Corp);
		for (int i = 0; i <= currentLevel; i++)
		{
			if (i >= rewards.NumTiers)
			{
				d.LogError($"No valid reward tier found for Corp {rewards.m_Corp} tier {i}. Have {rewards.NumTiers} reward tiers");
				continue;
			}
			TierRewardPool tierRewardPool = rewards[i];
			for (int j = 0; j < tierRewardPool.Length; j++)
			{
				BlockTypes blockType = tierRewardPool[j].m_BlockType;
				bool flag = !Singleton.Manager<ManDLC>.inst.IsBlockRandDRestricted(blockType);
				if (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockType))
				{
					flag = false;
				}
				if (flag)
				{
					flag = !Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType) && Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(blockType);
				}
				if (!flag)
				{
					continue;
				}
				ManLicenses.BlockState blockState = Singleton.Manager<ManLicenses>.inst.GetBlockState(blockType);
				if (Singleton.Manager<ManLicenses>.inst.GetBlockTier(blockType) != BlockUnlockTable.kDefaultBlockTier)
				{
					if (blockState != ManLicenses.BlockState.Discovered)
					{
						if (tierRewardPool[j].PrerequisitesMet() && !blocksToIgnore.Contains(blockType))
						{
							undiscoveredBlocks.Add(blockType);
						}
					}
					else if (blockState == ManLicenses.BlockState.Discovered)
					{
						discoveredBlocks.Add(blockType);
					}
				}
				else
				{
					d.LogError(string.Concat("ERROR: BlockRewardTable.GetRewardBlock - block ", blockType, " is in the Reward Table but not available in the Unlock Table. Please Fix."));
				}
			}
		}
	}

	private int FillBlockList(BlockTypes[] listToFill, List<BlockTypes> availableBlocks, int amount, int numFound)
	{
		while (numFound < amount && availableBlocks.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, availableBlocks.Count);
			BlockTypes blockTypes = (listToFill[numFound] = availableBlocks[index]);
			availableBlocks.RemoveAt(index);
			numFound++;
			BlockPairsList.BlockPairs[] blockPairs = Globals.inst.m_BlockPairsList.m_BlockPairs;
			for (int i = 0; i < blockPairs.Length; i++)
			{
				BlockPairsList.BlockPairs blockPairs2 = blockPairs[i];
				if (blockTypes == blockPairs2.m_Block)
				{
					if (numFound < amount)
					{
						listToFill[numFound] = blockPairs2.m_PairedBlock;
						numFound++;
					}
					else
					{
						numFound--;
					}
					break;
				}
			}
		}
		return numFound;
	}

	public void AddModdedBlockRewards(Dictionary<BlockTypes, ModdedBlockDefinition> blockIDs, int tier, FactionSubTypes corpToAddTo)
	{
		if (!m_ModdedRewards.TryGetValue(corpToAddTo, out var value))
		{
			value = new CorpRewardTiers
			{
				m_Corp = corpToAddTo
			};
			m_ModdedRewards.Add(corpToAddTo, value);
		}
		if (tier < 0 || tier >= 5)
		{
			d.LogError($"Tried to register blockIDs to faction {corpToAddTo} at tier {tier} which was out of range");
			return;
		}
		if (value.m_Tiers.Length == 0)
		{
			value.m_Tiers = new TierRewardPool[5];
			for (int i = 0; i < 5; i++)
			{
				value.m_Tiers[i] = new TierRewardPool();
			}
		}
		int num = value.m_Tiers[tier].m_RewardPool.Length;
		Array.Resize(ref value.m_Tiers[tier].m_RewardPool, num + blockIDs.Count);
		int num2 = 0;
		foreach (KeyValuePair<BlockTypes, ModdedBlockDefinition> blockID in blockIDs)
		{
			value.m_Tiers[tier].m_RewardPool[num + num2] = new RewardBlockInfo
			{
				m_BlockType = blockID.Key,
				m_PrerequisiteBlocks = new BlockTypes[0]
			};
			num2++;
		}
	}

	public void ClearModdedBlockRewards()
	{
		m_ModdedRewards.Clear();
	}
}
