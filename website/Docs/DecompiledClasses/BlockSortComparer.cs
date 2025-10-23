using System.Collections.Generic;

public struct BlockSortComparer : IComparer<BlockTypes>
{
	private CategoryOrder m_CategoryOrder;

	private CorporationOrder m_CorporationOrder;

	private BlockSortOrder m_InidividualBlockOrder;

	public BlockSortComparer(CategoryOrder categoryOrder, CorporationOrder corporationOrder, BlockSortOrder blockOrder = null)
	{
		m_CategoryOrder = categoryOrder;
		m_CorporationOrder = corporationOrder;
		m_InidividualBlockOrder = blockOrder;
	}

	public int Compare(BlockTypes blockA, BlockTypes blockB)
	{
		int num = CompareRestricted(blockA, blockB);
		if (num == 0)
		{
			num = CompareCategory(blockA, blockB);
		}
		if (num == 0)
		{
			num = CompareFaction(blockA, blockB);
		}
		if (num == 0 && m_InidividualBlockOrder != null)
		{
			num = m_InidividualBlockOrder.CompareOrder(blockA, blockB);
		}
		if (num == 0)
		{
			num = CompareTier(blockA, blockB);
		}
		if (num == 0)
		{
			num = CompareExclusivity(blockA, blockB);
		}
		if (num == 0)
		{
			num = CompareEnumInt(blockA, blockB);
		}
		return num;
	}

	public int CompareRestricted(BlockTypes blockA, BlockTypes blockB)
	{
		bool num = Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockA);
		bool flag = Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(blockB);
		return (num ? 1 : (-1)) + ((!flag) ? 1 : (-1));
	}

	public int CompareCategory(BlockTypes blockA, BlockTypes blockB)
	{
		BlockCategories category = GetCategory(blockA);
		BlockCategories category2 = GetCategory(blockB);
		int order;
		int order2;
		if (m_CategoryOrder != null)
		{
			m_CategoryOrder.Lookup(category, out order);
			m_CategoryOrder.Lookup(category2, out order2);
		}
		else
		{
			order = (int)category;
			order2 = (int)category2;
		}
		return order.CompareTo(order2);
	}

	public int CompareFaction(BlockTypes blockA, BlockTypes blockB)
	{
		FactionSubTypes corporation = GetCorporation(blockA);
		FactionSubTypes corporation2 = GetCorporation(blockB);
		int order;
		int order2;
		if (m_CorporationOrder != null)
		{
			m_CorporationOrder.Lookup(corporation, out order);
			m_CorporationOrder.Lookup(corporation2, out order2);
		}
		else
		{
			order = (int)corporation;
			order2 = (int)corporation2;
		}
		return order.CompareTo(order2);
	}

	public int CompareTier(BlockTypes blockA, BlockTypes blockB)
	{
		int tier = GetTier(blockA);
		int tier2 = GetTier(blockB);
		return tier.CompareTo(tier2);
	}

	public int CompareExclusivity(BlockTypes blockA, BlockTypes blockB)
	{
		int result = 0;
		if (SKU.IsNetEase)
		{
			int num = ((!CheckIsNetEaseOnly(blockA)) ? 1 : 0);
			int value = ((!CheckIsNetEaseOnly(blockB)) ? 1 : 0);
			result = num.CompareTo(value);
		}
		return result;
	}

	public bool CheckIsNetEaseOnly(BlockTypes blockType)
	{
		if (Singleton.Manager<ManSpawn>.inst.GetBlockAvailabilityFlags(blockType, out var flags))
		{
			int num = -4;
			return (flags & num) == 128;
		}
		return false;
	}

	public int CompareEnumInt(BlockTypes blockA, BlockTypes blockB)
	{
		int num = (int)blockA;
		return num.CompareTo((int)blockB);
	}

	private BlockCategories GetCategory(BlockTypes blockType)
	{
		return Singleton.Manager<ManSpawn>.inst.GetCategory(blockType);
	}

	private FactionSubTypes GetCorporation(BlockTypes blockType)
	{
		return Singleton.Manager<ManSpawn>.inst.GetCorporation(blockType);
	}

	private int GetTier(BlockTypes blockType)
	{
		return Singleton.Manager<ManLicenses>.inst.GetBlockTier(blockType);
	}
}
