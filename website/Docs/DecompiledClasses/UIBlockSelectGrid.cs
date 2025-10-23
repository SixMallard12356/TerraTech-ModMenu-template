#define UNITY_EDITOR
using System.Collections.Generic;

public class UIBlockSelectGrid : UIItemSelectGrid
{
	public delegate bool CorpFilterFn(FactionSubTypes corp);

	public delegate bool BlockFilterFn(BlockTypes blockType);

	private CategoryOrder m_CategoryOrder;

	private CorporationOrder m_CorporationOrder;

	private BlockSortOrder m_IndividualSortOrder;

	private BlockSortComparer m_BlockSortComparer;

	public CorpFilterFn CorpFilter { get; set; }

	public BlockFilterFn BlockFilter { get; set; }

	public CategoryOrder CategoryOrder
	{
		set
		{
			m_CategoryOrder = value;
			m_BlockSortComparer = new BlockSortComparer(m_CategoryOrder, m_CorporationOrder, m_IndividualSortOrder);
		}
	}

	public CorporationOrder CorporationOrder
	{
		set
		{
			m_CorporationOrder = value;
			m_BlockSortComparer = new BlockSortComparer(m_CategoryOrder, m_CorporationOrder, m_IndividualSortOrder);
		}
	}

	public BlockSortOrder IndividualSortOrder
	{
		set
		{
			m_IndividualSortOrder = value;
			m_BlockSortComparer = new BlockSortComparer(m_CategoryOrder, m_CorporationOrder, m_IndividualSortOrder);
		}
	}

	public bool TryGetSelection(out BlockTypes type)
	{
		bool result = false;
		type = BlockTypes.GSOAIController_111;
		ItemTypeInfo selection = GetSelection();
		if (selection != null && selection.ObjectType == ObjectTypes.Block)
		{
			type = (BlockTypes)selection.ItemType;
			result = true;
		}
		return result;
	}

	protected override void GetFilteredItemTypes(List<ItemTypeInfo> itemTypes)
	{
		if (Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory)
		{
			BlockTypes[] loadedTankBlockNames = Singleton.Manager<ManSpawn>.inst.GetLoadedTankBlockNames();
			foreach (BlockTypes blockTypes in loadedTankBlockNames)
			{
				bool num = CorpFilter == null || CorpFilter(Singleton.Manager<ManSpawn>.inst.GetCorporation(blockTypes));
				bool flag = BlockFilter == null || BlockFilter(blockTypes);
				if (num && flag)
				{
					itemTypes.Add(new ItemTypeInfo(ObjectTypes.Block, (int)blockTypes));
				}
			}
			itemTypes.Sort(BlockSortComparer);
			return;
		}
		BlockUnlockTable blockUnlockTable = Singleton.Manager<ManLicenses>.inst.GetBlockUnlockTable();
		d.Assert(blockUnlockTable, "GetFilteredBlockTypes: Unable to get block unlock table");
		if (!blockUnlockTable)
		{
			return;
		}
		for (int j = 0; j < Singleton.Manager<ManPurchases>.inst.AvailableCorporations.Count; j++)
		{
			FactionSubTypes factionSubTypes = Singleton.Manager<ManPurchases>.inst.AvailableCorporations[j];
			if (CorpFilter != null && !CorpFilter(factionSubTypes))
			{
				continue;
			}
			List<BlockTypes> allBlocksForFaction = blockUnlockTable.GetAllBlocksForFaction(factionSubTypes, filterByGameMode: false);
			for (int k = 0; k < allBlocksForFaction.Count; k++)
			{
				BlockTypes blockTypes2 = allBlocksForFaction[k];
				bool flag2 = Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(blockTypes2) && Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(blockTypes2);
				if (flag2 && BlockFilter != null)
				{
					flag2 = BlockFilter(blockTypes2);
				}
				if (flag2)
				{
					itemTypes.Add(new ItemTypeInfo(ObjectTypes.Block, (int)blockTypes2));
				}
			}
		}
		itemTypes.Sort(BlockSortComparer);
	}

	private int BlockSortComparer(ItemTypeInfo itemA, ItemTypeInfo itemB)
	{
		BlockTypes itemType = (BlockTypes)itemA.ItemType;
		BlockTypes itemType2 = (BlockTypes)itemB.ItemType;
		return m_BlockSortComparer.Compare(itemType, itemType2);
	}
}
