public struct InventoryMetaData
{
	public IInventory<BlockTypes> m_Inventory;

	public bool m_Locked;

	public BlockFilterTable m_AllowedBlocks;

	public static readonly InventoryMetaData kUnrestrictedIntenvory = new InventoryMetaData(null);

	public bool IsLocked => m_Locked;

	public bool IsUnlimited
	{
		get
		{
			if (m_Locked)
			{
				return false;
			}
			return m_Inventory == null;
		}
	}

	public bool TakesAndStoresBlocks
	{
		get
		{
			if (!IsLocked)
			{
				return m_Inventory != null;
			}
			return false;
		}
	}

	public InventoryMetaData(IInventory<BlockTypes> inventory, bool locked = false, BlockFilterTable allowedBlocks = null)
	{
		this = default(InventoryMetaData);
		m_Inventory = inventory;
		m_Locked = locked;
		m_AllowedBlocks = allowedBlocks;
	}

	public int GetInventoryBlockCount(BlockTypes block)
	{
		if (m_AllowedBlocks != null && !m_AllowedBlocks.CheckBlockAllowed(block))
		{
			return 0;
		}
		if (!Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInLaunchedConfig(block) || !Singleton.Manager<ManSpawn>.inst.IsBlockAllowedInCurrentGameMode(block) || Singleton.Manager<ManSpawn>.inst.IsBlockUsageRestrictedInGameMode(block))
		{
			return 0;
		}
		if (IsUnlimited)
		{
			return -1;
		}
		if (m_Locked)
		{
			return 0;
		}
		return m_Inventory.GetUnreservedQuantity(block);
	}
}
