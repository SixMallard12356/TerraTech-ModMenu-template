#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class CrateDropPopulator
{
	private class CategoryWeighting
	{
		public BlockCategories m_blockCategory;

		public float m_percChance;

		public float m_maxPercContents;

		public float m_numCurrentBlocks;

		public CategoryWeighting(BlockCategories blkCat, float percChance, float maxPercContents)
		{
			m_blockCategory = blkCat;
			m_percChance = percChance;
			m_maxPercContents = maxPercContents;
			m_numCurrentBlocks = 0f;
		}
	}

	private class BlockRarityGroup
	{
		public BlockRarity m_blockRarity;

		public List<BlockTypes> m_blocks;
	}

	private class CategoryGroup
	{
		public BlockCategories m_blockCategory;

		public List<BlockRarityGroup> m_blockRarities;

		public BlockRarityGroup FindOrAddBlockRarity(BlockRarity blkRarity, bool allowAdd)
		{
			BlockRarityGroup blockRarityGroup = null;
			for (int i = 0; i < m_blockRarities.Count; i++)
			{
				if (m_blockRarities[i].m_blockRarity == blkRarity)
				{
					blockRarityGroup = m_blockRarities[i];
					break;
				}
			}
			if (blockRarityGroup == null && allowAdd)
			{
				blockRarityGroup = new BlockRarityGroup
				{
					m_blockRarity = blkRarity,
					m_blocks = new List<BlockTypes>(128)
				};
				m_blockRarities.Add(blockRarityGroup);
			}
			return blockRarityGroup;
		}

		public BlockRarityGroup ChooseRarityGroupAtRandom(out BlockRarity chosenRarity)
		{
			float num = Random.Range(0f, 100f);
			BlockRarity blockRarity = BlockRarity.Common;
			if (num > 60f && num <= 90f)
			{
				blockRarity = BlockRarity.Uncommon;
			}
			else if (num > 90f)
			{
				blockRarity = BlockRarity.Rare;
			}
			chosenRarity = blockRarity;
			BlockRarityGroup blockRarityGroup = FindOrAddBlockRarity(blockRarity, allowAdd: false);
			if (blockRarityGroup == null && blockRarity == BlockRarity.Rare)
			{
				blockRarityGroup = FindOrAddBlockRarity(BlockRarity.Uncommon, allowAdd: false);
				if (blockRarityGroup == null)
				{
					blockRarity = BlockRarity.Uncommon;
				}
			}
			if (blockRarityGroup == null && blockRarity == BlockRarity.Uncommon)
			{
				blockRarityGroup = FindOrAddBlockRarity(BlockRarity.Common, allowAdd: false);
			}
			return blockRarityGroup;
		}
	}

	private class CategoryChoice
	{
		public BlockCategories m_blockCategory;

		public float m_weighting;
	}

	private List<CategoryGroup> m_CategoryGroups;

	private CategoryWeighting[] m_CategoryWeightings = new CategoryWeighting[5]
	{
		new CategoryWeighting(BlockCategories.Weapons, 50f, 50f),
		new CategoryWeighting(BlockCategories.Flight, 5f, 5f),
		new CategoryWeighting(BlockCategories.Standard, 20f, 20f),
		new CategoryWeighting(BlockCategories.Wheels, 15f, 15f),
		new CategoryWeighting(BlockCategories.Accessories, 10f, 10f)
	};

	private Dictionary<BlockTypes, BlockTypes> m_BlockPairs;

	public BlockCount[] PopulateCrate(int nBlocksReqd)
	{
		if (m_CategoryGroups == null)
		{
			CreateBlockLists();
		}
		if (m_BlockPairs == null)
		{
			CreateBlockPairs();
		}
		List<BlockCount> list = new List<BlockCount>(nBlocksReqd * 2);
		for (int i = 0; i < m_CategoryWeightings.Length; i++)
		{
			m_CategoryWeightings[i].m_numCurrentBlocks = 0f;
		}
		for (int j = 0; j < nBlocksReqd; j++)
		{
			BlockTypes blockTypes = ChooseRandomCrateDropBlock(nBlocksReqd);
			list.Add(new BlockCount(blockTypes, 1));
			if (m_BlockPairs != null && m_BlockPairs.TryGetValue(blockTypes, out var value))
			{
				list.Add(new BlockCount(value, 1));
			}
		}
		return list.ToArray();
	}

	private bool IsBlockCategoryAllowed(BlockCategories blkCat)
	{
		if ((uint)(blkCat - 2) <= 3u || blkCat == BlockCategories.Flight)
		{
			return true;
		}
		return false;
	}

	private CategoryGroup FindOrAddCategoryGroup(BlockCategories blkCat, bool allowAdd)
	{
		CategoryGroup categoryGroup = null;
		for (int i = 0; i < m_CategoryGroups.Count; i++)
		{
			if (m_CategoryGroups[i].m_blockCategory == blkCat)
			{
				categoryGroup = m_CategoryGroups[i];
				break;
			}
		}
		if (categoryGroup == null && allowAdd)
		{
			categoryGroup = new CategoryGroup
			{
				m_blockCategory = blkCat,
				m_blockRarities = new List<BlockRarityGroup>(4)
			};
			m_CategoryGroups.Add(categoryGroup);
		}
		return categoryGroup;
	}

	private void AddBlockType(BlockTypes blkType, BlockCategories blkCat, BlockRarity blkRarity)
	{
		FindOrAddCategoryGroup(blkCat, allowAdd: true).FindOrAddBlockRarity(blkRarity, allowAdd: true).m_blocks.Add(blkType);
	}

	private BlockCategories ChooseCategory(int nBlocksReqd)
	{
		d.Assert(nBlocksReqd > 0);
		List<CategoryChoice> list = new List<CategoryChoice>(6);
		float num = 0f;
		for (int i = 0; i < m_CategoryWeightings.Length; i++)
		{
			if (!(100f * m_CategoryWeightings[i].m_numCurrentBlocks / (float)nBlocksReqd >= m_CategoryWeightings[i].m_maxPercContents))
			{
				list.Add(new CategoryChoice
				{
					m_blockCategory = m_CategoryWeightings[i].m_blockCategory,
					m_weighting = m_CategoryWeightings[i].m_percChance
				});
				num += m_CategoryWeightings[i].m_percChance;
			}
		}
		if (num < 100f)
		{
			float num2 = 100f / num;
			for (int j = 0; j < list.Count; j++)
			{
				list[j].m_weighting = Mathf.Clamp(list[j].m_weighting * num2, 0f, 100f);
			}
		}
		if (list.Count == 0)
		{
			list.Add(new CategoryChoice
			{
				m_blockCategory = BlockCategories.Standard,
				m_weighting = 100f
			});
		}
		d.Assert(list.Count > 0);
		float num3 = Random.Range(0, 100);
		float num4 = 0f;
		BlockCategories blockCategory = list[0].m_blockCategory;
		for (int k = 0; k < list.Count; k++)
		{
			float num5 = num4 + list[k].m_weighting;
			if (num4 <= num3 && num3 < num5)
			{
				blockCategory = list[k].m_blockCategory;
				break;
			}
			num4 = num5;
		}
		return blockCategory;
	}

	private void CreateBlockLists()
	{
		if (m_CategoryGroups != null)
		{
			return;
		}
		m_CategoryGroups = new List<CategoryGroup>(6);
		foreach (BlockTypes item in Singleton.Manager<ManNetwork>.inst.CrateDropWhiteList.EnumerateBlockTypes())
		{
			if (!item.ToString().ToLower().Contains("deprecated") && Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(item))
			{
				BlockCategories category = Singleton.Manager<ManSpawn>.inst.GetCategory(item);
				if (IsBlockCategoryAllowed(category))
				{
					BlockRarity rarity = Singleton.Manager<ManSpawn>.inst.GetRarity(item);
					AddBlockType(item, category, rarity);
				}
			}
		}
	}

	private void CreateBlockPairs()
	{
		if (m_BlockPairs != null)
		{
			return;
		}
		BlockPairsList.BlockPairs[] blockPairs = Globals.inst.m_BlockPairsList.m_BlockPairs;
		m_BlockPairs = new Dictionary<BlockTypes, BlockTypes>(blockPairs.Length * 2, new BlockTypesComparer());
		for (int i = 0; i < blockPairs.Length; i++)
		{
			BlockPairsList.BlockPairs blockPairs2 = blockPairs[i];
			if (!m_BlockPairs.ContainsKey(blockPairs2.m_Block))
			{
				m_BlockPairs.Add(blockPairs2.m_Block, blockPairs2.m_PairedBlock);
			}
			if (!m_BlockPairs.ContainsKey(blockPairs2.m_PairedBlock))
			{
				m_BlockPairs.Add(blockPairs2.m_PairedBlock, blockPairs2.m_Block);
			}
		}
	}

	private BlockTypes ChooseRandomCrateDropBlock(int nBlocksReqd)
	{
		BlockTypes result = BlockTypes.GSOBlock_111;
		if (m_CategoryGroups != null && m_CategoryGroups.Count > 0)
		{
			BlockCategories blockCategories = ChooseCategory(nBlocksReqd);
			BlockRarity chosenRarity;
			BlockRarityGroup blockRarityGroup = FindOrAddCategoryGroup(blockCategories, allowAdd: false).ChooseRarityGroupAtRandom(out chosenRarity);
			if (blockRarityGroup != null)
			{
				int num = 5;
				do
				{
					int index = Random.Range(0, blockRarityGroup.m_blocks.Count);
					BlockTypes blockTypes = blockRarityGroup.m_blocks[index];
					if (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(blockTypes) || Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockTypes))
					{
						d.LogError($"Tried to put block {blockTypes} in a crate, but block was not allowed in mode!");
						continue;
					}
					result = blockTypes;
					break;
				}
				while (num-- > 0);
				for (int i = 0; i < m_CategoryWeightings.Length; i++)
				{
					if (m_CategoryWeightings[i].m_blockCategory == blockCategories)
					{
						m_CategoryWeightings[i].m_numCurrentBlocks += 1f;
						break;
					}
				}
			}
			else
			{
				d.LogError("CrateDropPopulator: Failed to spawn a random crate block for the Category: " + blockCategories.ToString() + " and Rarity: " + chosenRarity.ToString() + " - Falling back to GSOBlock_111");
			}
		}
		return result;
	}
}
