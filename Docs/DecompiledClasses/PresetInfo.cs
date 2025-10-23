#define UNITY_EDITOR
using System.Collections.Generic;

public class PresetInfo
{
	public enum InclusionType
	{
		All,
		Any,
		None
	}

	private TechData m_TechData;

	private Dictionary<int, int> m_UndiscoveredBlocks;

	private Dictionary<int, int> m_AvailableBlocks;

	private int m_UndiscoveredBlockCount;

	private int m_AvailableBlockCount;

	private List<FactionSubTypes> m_Corps;

	private float[] m_CorpPercentages;

	private bool m_AllBlocksAvailableInMode;

	private Dictionary<int, int> m_Categories;

	private Dictionary<int, int> m_HighestTier;

	private List<BlockTypes>[][] m_CorpTierBlocks;

	private bool m_AnchoredFlagIsDirty;

	private bool m_Anchored;

	private float m_Value;

	private float m_Radius;

	private int m_BlockCount;

	private int m_BlockLimiterCost;

	public TechData TechData => m_TechData;

	public PresetInfo(TankPreset preset)
	{
		m_TechData = preset.GetTechDataFormatted();
		if (m_TechData == null || m_TechData.m_BlockSpecs == null)
		{
			d.LogError("Preset Info has a preset " + preset.name + " with no TechData. How?");
			return;
		}
		m_UndiscoveredBlocks = new Dictionary<int, int>(preset.m_NumUniqueBlockTypes);
		m_AvailableBlocks = new Dictionary<int, int>(preset.m_NumUniqueBlockTypes);
		int numCorps = Singleton.Manager<ManLicenses>.inst.GetNumCorps();
		m_Corps = new List<FactionSubTypes>(numCorps);
		m_CorpPercentages = new float[numCorps];
		m_Categories = new Dictionary<int, int>();
		m_HighestTier = new Dictionary<int, int>();
		m_CorpTierBlocks = new List<BlockTypes>[numCorps][];
		int maxGradeEditorOnly = Singleton.Manager<ManLicenses>.inst.GetMaxGradeEditorOnly();
		for (int i = 0; i < numCorps; i++)
		{
			m_CorpTierBlocks[i] = new List<BlockTypes>[maxGradeEditorOnly];
			for (int j = 0; j < maxGradeEditorOnly; j++)
			{
				m_CorpTierBlocks[i][j] = new List<BlockTypes>();
			}
		}
		List<TankPreset.BlockSpec> blockSpecs = m_TechData.m_BlockSpecs;
		m_BlockCount = blockSpecs.Count;
		m_Value = m_TechData.GetValue();
		m_Radius = m_TechData.Radius;
		m_BlockLimiterCost = preset.m_LimiterCost;
		m_AnchoredFlagIsDirty = true;
		for (int k = 0; k < m_BlockCount; k++)
		{
			BlockTypes blockType = blockSpecs[k].GetBlockType();
			UpdateCorps(blockType);
			UpdateCategories(blockType);
		}
		for (int l = 0; l < numCorps; l++)
		{
			m_CorpPercentages[l] /= m_BlockCount;
		}
	}

	public void ReInitForMode()
	{
		m_AvailableBlocks.Clear();
		m_AvailableBlockCount = 0;
		m_UndiscoveredBlocks.Clear();
		m_UndiscoveredBlockCount = 0;
		List<TankPreset.BlockSpec> blockSpecs = m_TechData.m_BlockSpecs;
		BlockTypes blockTypes = (BlockTypes)2147483647;
		int num = 1;
		for (int i = 0; i < m_BlockCount; i++)
		{
			BlockTypes blockType = blockSpecs[i].GetBlockType();
			if (blockType == blockTypes)
			{
				num++;
				continue;
			}
			num = 1;
			blockTypes = blockType;
			UpdateBlocks(blockType, num);
		}
		m_AllBlocksAvailableInMode = true;
		for (int j = 0; j < m_BlockCount; j++)
		{
			BlockTypes blockType2 = blockSpecs[j].GetBlockType();
			if (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockType2) || !Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(blockType2) || Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockType2))
			{
				m_AllBlocksAvailableInMode = false;
				break;
			}
		}
	}

	public bool ContainsBlocks(TechSpawnFilter.BlockQuantity[] blocks, InclusionType inclusionType)
	{
		bool result;
		switch (inclusionType)
		{
		case InclusionType.All:
		{
			result = true;
			for (int k = 0; k < blocks.Length; k++)
			{
				int num2 = blocks[k].m_QuantityRequired;
				for (int l = 0; l < m_TechData.m_BlockSpecs.Count; l++)
				{
					if (m_TechData.m_BlockSpecs[l].GetBlockType() == blocks[k].m_Block)
					{
						num2--;
						if (num2 <= 0)
						{
							break;
						}
					}
				}
				if (num2 > 0)
				{
					result = false;
					break;
				}
			}
			break;
		}
		case InclusionType.Any:
		case InclusionType.None:
		{
			bool flag = false;
			for (int i = 0; i < blocks.Length; i++)
			{
				int num = blocks[i].m_QuantityRequired;
				for (int j = 0; j < m_TechData.m_BlockSpecs.Count; j++)
				{
					if (m_TechData.m_BlockSpecs[j].GetBlockType() == blocks[i].m_Block)
					{
						num--;
						if (num <= 0)
						{
							flag = true;
							break;
						}
					}
				}
				if (flag)
				{
					break;
				}
			}
			result = ((inclusionType == InclusionType.Any) ? flag : (!flag));
			break;
		}
		default:
			d.Assert(condition: false, "PresetInfo.ContainsBlocks has unknown check type " + inclusionType);
			result = false;
			break;
		}
		return result;
	}

	public bool ContainsCorps(FactionSubTypes[] corps, bool rejectCorps)
	{
		bool result = true;
		for (int i = 0; i < corps.Length; i++)
		{
			bool flag = m_Corps.Contains(corps[i]);
			if (rejectCorps)
			{
				if (flag)
				{
					result = false;
					break;
				}
			}
			else if (!flag)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public bool ContainsCorpPercentage(TechSpawnFilter.CorpPercentage[] corpPercentage)
	{
		bool result = true;
		for (int i = 0; i < corpPercentage.Length; i++)
		{
			float percentage = corpPercentage[i].m_Percentage;
			int corp = (int)corpPercentage[i].m_Corp;
			if (m_CorpPercentages[corp] < percentage)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	public bool ContainsBlockCategories(TechSpawnFilter.CategoryQuantity[] categories)
	{
		bool result = true;
		for (int i = 0; i < categories.Length; i++)
		{
			if (m_Categories.TryGetValue((int)categories[i].RequiredCategory, out var value))
			{
				if (value < categories[i].m_QuantityRequired)
				{
					result = false;
					break;
				}
				continue;
			}
			result = false;
			break;
		}
		return result;
	}

	public bool ContainsNoUnknownBlocks(TechSpawnFilter.BlockQuantity[] blocksToIgnore)
	{
		int num = m_UndiscoveredBlockCount;
		if (blocksToIgnore != null)
		{
			for (int i = 0; i < blocksToIgnore.Length; i++)
			{
				BlockTypes block = blocksToIgnore[i].m_Block;
				if (m_UndiscoveredBlocks.TryGetValue((int)block, out var value))
				{
					num -= value;
				}
			}
		}
		return m_UndiscoveredBlockCount == 0;
	}

	public bool ContainsNoAvailableBlocks(TechSpawnFilter.BlockQuantity[] blocksToIgnore)
	{
		int num = m_AvailableBlockCount;
		if (blocksToIgnore != null)
		{
			for (int i = 0; i < blocksToIgnore.Length; i++)
			{
				BlockTypes block = blocksToIgnore[i].m_Block;
				if (m_AvailableBlocks.TryGetValue((int)block, out var value))
				{
					num -= value;
				}
			}
		}
		return num == 0;
	}

	public bool ContainsOnlyAvailableBlocks()
	{
		return m_AllBlocksAvailableInMode;
	}

	public bool CompareTier()
	{
		bool result = true;
		for (int i = 0; i < m_Corps.Count; i++)
		{
			FactionSubTypes factionSubTypes = m_Corps[i];
			if (Singleton.Manager<ManLicenses>.inst.IsLicenseSupported(factionSubTypes) && Singleton.Manager<ManLicenses>.inst.IsLicenseDiscovered(factionSubTypes))
			{
				if (m_HighestTier.TryGetValue((int)factionSubTypes, out var value))
				{
					int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(factionSubTypes);
					if (value > currentLevel)
					{
						result = false;
						break;
					}
				}
				continue;
			}
			result = false;
			break;
		}
		return result;
	}

	public bool CompareValue(float min, float max)
	{
		if (m_Value >= min)
		{
			return m_Value <= max;
		}
		return false;
	}

	public bool CompareRadius(float min, float max)
	{
		if (m_Radius >= min)
		{
			return m_Radius <= max;
		}
		return false;
	}

	public bool CompareBlockCount(int min, int max)
	{
		if (m_BlockCount >= min)
		{
			return m_BlockCount <= max;
		}
		return false;
	}

	public bool CompareBlockLimiterCost(int min, int max)
	{
		if (m_BlockLimiterCost >= min)
		{
			return m_BlockLimiterCost <= max;
		}
		return false;
	}

	public bool IsAnchored()
	{
		if (m_AnchoredFlagIsDirty)
		{
			m_Anchored = m_TechData.CheckIsAnchored();
			m_AnchoredFlagIsDirty = false;
		}
		return m_Anchored;
	}

	public void BlockDiscovered(BlockTypes blockDiscovered)
	{
		if (m_AvailableBlocks.TryGetValue((int)blockDiscovered, out var value))
		{
			m_AvailableBlocks.Remove((int)blockDiscovered);
			m_AvailableBlockCount -= value;
		}
		if (m_UndiscoveredBlocks.TryGetValue((int)blockDiscovered, out value))
		{
			m_UndiscoveredBlocks.Remove((int)blockDiscovered);
			m_UndiscoveredBlockCount -= value;
		}
	}

	public void BlockAvailable(BlockTypes blockAvailable)
	{
		if (m_UndiscoveredBlocks.TryGetValue((int)blockAvailable, out var value))
		{
			m_UndiscoveredBlocks.Remove((int)blockAvailable);
			m_UndiscoveredBlockCount -= value;
			m_AvailableBlocks.Add((int)blockAvailable, value);
			m_AvailableBlockCount += value;
		}
	}

	public void GetBlockRarityPercentages(List<float> blockRarityPercentages)
	{
		int num = EnumNamesIterator<BlockRarity>.Names.Length;
		blockRarityPercentages.Clear();
		for (int i = 0; i < num; i++)
		{
			blockRarityPercentages.Add(0f);
		}
		int count = m_TechData.m_BlockSpecs.Count;
		for (int j = 0; j < count; j++)
		{
			BlockRarity rarity = Singleton.Manager<ManSpawn>.inst.GetRarity(m_TechData.m_BlockSpecs[j].GetBlockType());
			int index = (int)rarity;
			float value = blockRarityPercentages[index] + 1f;
			blockRarityPercentages[index] = value;
		}
		for (int k = 0; k < num; k++)
		{
			blockRarityPercentages[k] /= count;
		}
	}

	private void UpdateBlocks(BlockTypes type, int count)
	{
		switch (Singleton.Manager<ManLicenses>.inst.GetBlockState(type))
		{
		case ManLicenses.BlockState.Available:
		{
			m_AvailableBlocks.TryGetValue((int)type, out var value2);
			value2 += count;
			m_AvailableBlocks[(int)type] = value2;
			m_AvailableBlockCount += count;
			break;
		}
		case ManLicenses.BlockState.Unknown:
		{
			m_UndiscoveredBlocks.TryGetValue((int)type, out var value);
			value += count;
			m_UndiscoveredBlocks[(int)type] = value;
			m_UndiscoveredBlockCount += count;
			break;
		}
		}
	}

	private void UpdateCategories(BlockTypes type)
	{
		int category = (int)Singleton.Manager<ManSpawn>.inst.GetCategory(type);
		int value = 0;
		m_Categories.TryGetValue(category, out value);
		value++;
		m_Categories[category] = value;
	}

	private void UpdateCorps(BlockTypes type)
	{
		FactionSubTypes corporation = Singleton.Manager<ManSpawn>.inst.GetCorporation(type);
		if (!m_Corps.Contains(corporation))
		{
			m_Corps.Add(corporation);
		}
		int num = (int)corporation;
		bool supressWarnings = true;
		int blockTier = Singleton.Manager<ManLicenses>.inst.GetBlockTier(type, supressWarnings);
		if (blockTier != BlockUnlockTable.kDefaultBlockTier)
		{
			bool flag = true;
			if (m_HighestTier.TryGetValue(num, out var value))
			{
				if (value < blockTier)
				{
					m_HighestTier.Remove(num);
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				m_HighestTier.Add(num, blockTier);
			}
			if (!m_CorpTierBlocks[num][blockTier].Contains(type))
			{
				m_CorpTierBlocks[num][blockTier].Add(type);
			}
		}
		m_CorpPercentages[num] += 1f;
	}
}
