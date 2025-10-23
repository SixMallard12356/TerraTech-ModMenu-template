#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TechSpawnFilter : ScriptableObject
{
	[Serializable]
	public struct CategoryQuantity
	{
		public BlockCategories RequiredCategory;

		public int m_QuantityRequired;
	}

	[Serializable]
	public struct BlockQuantity
	{
		public BlockTypes m_Block;

		public int m_QuantityRequired;
	}

	[Serializable]
	public struct CorpPercentage
	{
		public FactionSubTypes m_Corp;

		[Range(0f, 1f)]
		public float m_Percentage;
	}

	[Serializable]
	public struct PercentageRange
	{
		public float minValue;

		public float maxValue;

		public bool Contains(float value)
		{
			if (value >= minValue)
			{
				return value <= maxValue;
			}
			return false;
		}
	}

	public enum FallbackStage
	{
		Normal,
		IgnoreBlockRarityStrict,
		IgnoreSizeDown,
		IgnoreBlockRarityLoose,
		IgnoreSizeUp,
		IgnoreSpecificBlocks,
		IgnoreCategory,
		IgnoreCorp
	}

	public enum CheckType
	{
		StaticAndDynamic,
		StaticOnly,
		DynamicOnly
	}

	[SerializeField]
	private bool m_AllowAnchoredTechs;

	[SerializeField]
	private bool m_AnchoredTechsOnly;

	[SerializeField]
	private bool m_AllowUndiscoveredBlocks;

	[SerializeField]
	private bool m_AllowUnknownBlocks;

	[SerializeField]
	private bool m_IgnoreTiers;

	[SerializeField]
	private bool m_NotRecentSpawn;

	[SerializeField]
	private bool m_UseBlockLimitFilter;

	[SerializeField]
	private int m_MinBlockLimiterCost;

	[SerializeField]
	private int m_MaxBlockLimiterCost;

	[SerializeField]
	private bool m_UseBlockFilter;

	[SerializeField]
	private PresetInfo.InclusionType m_BlocksCheckType;

	[SerializeField]
	private BlockQuantity[] m_RequiredBlocks;

	[SerializeField]
	private bool m_UseBlockCategory;

	[SerializeField]
	private CategoryQuantity[] m_RequiredCategories;

	[SerializeField]
	private bool m_UseBlockRarityFilter;

	[SerializeField]
	private PercentageRange[] m_BlockRarityPercentagesStrict = new PercentageRange[1];

	[SerializeField]
	private PercentageRange[] m_BlockRarityPercentagesLoose = new PercentageRange[1];

	[SerializeField]
	private bool m_UseSizeFilter;

	[SerializeField]
	private bool m_AbsoluteValue;

	[SerializeField]
	private bool m_UseTechValue;

	[SerializeField]
	private float m_MinValue;

	[SerializeField]
	private float m_MaxValue;

	[SerializeField]
	private bool m_UseTechBlockCount;

	[SerializeField]
	private int m_MinBlockCount;

	[SerializeField]
	private int m_MaxBlockCount;

	[SerializeField]
	private bool m_UseTechRadius;

	[SerializeField]
	private float m_MinRadiusSize;

	[SerializeField]
	private float m_MaxRadiusSize;

	[SerializeField]
	private bool m_UseCorpFilters;

	[SerializeField]
	private CorpPercentage[] m_CorpPercentage;

	[SerializeField]
	private FactionSubTypes[] m_CorpsToUse;

	[SerializeField]
	private FactionSubTypes[] m_CorpsToIgnore;

	[SerializeField]
	private bool m_UseFolderFilters;

	[SerializeField]
	private string[] m_FoldersToUse;

	[SerializeField]
	private TankPreset[] m_FallbackTechs;

	private static List<float> s_RarityPercentages = new List<float>(EnumNamesIterator<BlockRarity>.Names.Length);

	public bool UseFolderFilters => m_UseFolderFilters;

	public string[] FoldersToUse => m_FoldersToUse;

	public bool NotRecentlySpawned => m_NotRecentSpawn;

	public void UseRadius(float min, float max)
	{
		m_UseSizeFilter = true;
		m_UseTechRadius = true;
		m_AbsoluteValue = true;
		m_MinRadiusSize = min;
		m_MaxRadiusSize = max;
	}

	public bool PresetPasses(PresetInfo presetInfo, FallbackStage fallbackStage, CheckType checkType)
	{
		bool flag = false;
		bool num = checkType != CheckType.StaticOnly;
		bool flag2 = checkType != CheckType.DynamicOnly;
		if (flag2)
		{
			flag = flag || !BlocksAvailableCheck(presetInfo);
		}
		if (num)
		{
			flag = flag || !BlockLimiterCheck(presetInfo);
		}
		if (flag2 && fallbackStage < FallbackStage.IgnoreSpecificBlocks)
		{
			flag = flag || !BlockCheck(presetInfo);
		}
		if (num)
		{
			flag = flag || !TierCheck(presetInfo) || !DiscoveredBlockCheck(presetInfo);
		}
		if (flag2 && fallbackStage < FallbackStage.IgnoreCorp)
		{
			flag = flag || !CorpCheck(presetInfo);
		}
		if (flag2 && fallbackStage < FallbackStage.IgnoreCategory)
		{
			flag = flag || !CategoryCheck(presetInfo);
		}
		if (flag2 && fallbackStage < FallbackStage.IgnoreBlockRarityStrict)
		{
			flag = flag || !BlockRarityCheck(presetInfo, m_BlockRarityPercentagesStrict);
		}
		if (flag2 && fallbackStage < FallbackStage.IgnoreBlockRarityLoose)
		{
			flag = flag || !BlockRarityCheck(presetInfo, m_BlockRarityPercentagesLoose);
		}
		flag = flag || !SizeCheck(presetInfo, fallbackStage, checkType);
		return !flag;
	}

	public TankPreset GetRandomFallbackTech()
	{
		TankPreset tankPreset = null;
		if (m_FallbackTechs != null && m_FallbackTechs.Length != 0)
		{
			int num = m_FallbackTechs.Length;
			int num2 = UnityEngine.Random.Range(0, m_FallbackTechs.Length);
			tankPreset = m_FallbackTechs[num2];
			if (tankPreset == null)
			{
				d.LogError("Null entry in fallback techs! Bad you");
				for (int i = 0; i < num; i++)
				{
					tankPreset = m_FallbackTechs[i];
					if (tankPreset != null)
					{
						break;
					}
				}
			}
		}
		return tankPreset;
	}

	private bool BlocksAvailableCheck(PresetInfo presetInfo)
	{
		return presetInfo.ContainsOnlyAvailableBlocks();
	}

	private bool BlockLimiterCheck(PresetInfo presetInfo)
	{
		bool result = true;
		d.Assert(Singleton.Manager<ManBlockLimiter>.inst.IsNotNull(), "Block limiter check being made before ManBlockLimiter is constructed");
		if (m_UseBlockLimitFilter && Singleton.Manager<ManBlockLimiter>.inst.IsNotNull() && Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			result = presetInfo.CompareBlockLimiterCost(m_MinBlockLimiterCost, m_MaxBlockLimiterCost);
		}
		return result;
	}

	private bool BlockCheck(PresetInfo presetInfo)
	{
		bool result = true;
		if (m_UseBlockFilter)
		{
			result = presetInfo.ContainsBlocks(m_RequiredBlocks, m_BlocksCheckType);
		}
		return result;
	}

	private bool TierCheck(PresetInfo presetInfo)
	{
		bool result = true;
		if (!m_IgnoreTiers)
		{
			result = presetInfo.CompareTier();
		}
		return result;
	}

	private bool DiscoveredBlockCheck(PresetInfo presetInfo)
	{
		bool flag = false;
		BlockQuantity[] blocksToIgnore = (m_UseBlockFilter ? m_RequiredBlocks : null);
		if (m_AllowUndiscoveredBlocks)
		{
			if (!m_AllowUnknownBlocks)
			{
				flag = !presetInfo.ContainsNoUnknownBlocks(blocksToIgnore);
			}
		}
		else
		{
			flag = !presetInfo.ContainsNoAvailableBlocks(blocksToIgnore) || !presetInfo.ContainsNoUnknownBlocks(blocksToIgnore);
		}
		return !flag;
	}

	private bool CorpCheck(PresetInfo presetInfo)
	{
		bool flag = false;
		if (m_UseCorpFilters)
		{
			if (m_CorpPercentage.Length != 0)
			{
				flag = !presetInfo.ContainsCorpPercentage(m_CorpPercentage);
			}
			if (m_CorpsToUse.Length != 0)
			{
				bool rejectCorps = false;
				flag = flag || !presetInfo.ContainsCorps(m_CorpsToUse, rejectCorps);
			}
			if (m_CorpsToIgnore.Length != 0)
			{
				bool rejectCorps2 = true;
				flag = flag || !presetInfo.ContainsCorps(m_CorpsToIgnore, rejectCorps2);
			}
		}
		return !flag;
	}

	private bool CategoryCheck(PresetInfo presetInfo)
	{
		bool result = true;
		if (m_UseBlockCategory && m_RequiredCategories.Length != 0)
		{
			result = presetInfo.ContainsBlockCategories(m_RequiredCategories);
		}
		return result;
	}

	private bool SizeCheck(PresetInfo presetInfo, FallbackStage fallbackStage, CheckType checkType)
	{
		bool flag = false;
		bool flag2 = false;
		if (m_UseSizeFilter)
		{
			switch (checkType)
			{
			case CheckType.DynamicOnly:
				flag2 = !m_AbsoluteValue;
				break;
			case CheckType.StaticOnly:
				flag2 = m_AbsoluteValue;
				break;
			default:
				d.Assert(checkType == CheckType.StaticAndDynamic, "ERROR in SizeCheck Unhandled check type: " + checkType);
				flag2 = true;
				break;
			}
		}
		if (flag2)
		{
			Tank playerTank = Singleton.playerTank;
			float num = 0f;
			float num2 = 0f;
			int num3 = 0;
			if (!m_AbsoluteValue)
			{
				if (!(playerTank != null))
				{
					d.LogError("TechSpawnFilter.SizeCheck: Filter " + base.name + " is set to use values relative to player, but no player!");
					return false;
				}
				num = playerTank.GetValue();
				num2 = playerTank.visible.Radius;
				num3 = playerTank.blockman.blockCount;
			}
			if (m_UseTechValue)
			{
				float min = m_MinValue + num;
				float max = m_MaxValue + num;
				if (fallbackStage >= FallbackStage.IgnoreSizeUp)
				{
					max = float.MaxValue;
				}
				if (fallbackStage >= FallbackStage.IgnoreSizeDown)
				{
					min = 0f;
				}
				flag = !presetInfo.CompareValue(min, max);
			}
			if (m_UseTechRadius)
			{
				float min2 = m_MinRadiusSize + num2;
				float max2 = m_MaxRadiusSize + num2;
				if (fallbackStage >= FallbackStage.IgnoreSizeUp)
				{
					max2 = float.MaxValue;
				}
				if (fallbackStage >= FallbackStage.IgnoreSizeDown)
				{
					min2 = 0f;
				}
				flag = flag || !presetInfo.CompareRadius(min2, max2);
			}
			if (m_UseTechBlockCount)
			{
				int min3 = m_MinBlockCount + num3;
				int max3 = m_MaxBlockCount + num3;
				if (fallbackStage >= FallbackStage.IgnoreSizeUp)
				{
					max3 = int.MaxValue;
				}
				if (fallbackStage >= FallbackStage.IgnoreSizeDown)
				{
					min3 = 0;
				}
				flag = flag || !presetInfo.CompareBlockCount(min3, max3);
			}
		}
		return !flag;
	}

	private bool AnchorCheck(PresetInfo presetInfo)
	{
		bool result = true;
		if (!m_AllowAnchoredTechs || m_AnchoredTechsOnly)
		{
			if (presetInfo.IsAnchored())
			{
				if (!m_AllowAnchoredTechs)
				{
					result = false;
				}
			}
			else if (m_AnchoredTechsOnly)
			{
				result = false;
			}
		}
		return result;
	}

	private bool BlockRarityCheck(PresetInfo presetInfo, PercentageRange[] rarityRanges)
	{
		bool result = true;
		if (m_UseBlockRarityFilter)
		{
			presetInfo.GetBlockRarityPercentages(s_RarityPercentages);
			int count = s_RarityPercentages.Count;
			for (int i = 0; i < count; i++)
			{
				if (!rarityRanges[i].Contains(s_RarityPercentages[i]))
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}
}
