using System;

[FriendlyName("uScript_InventoryNumBlocksChangedEvent", "Listen for inventory block quantity changed events and pass them through if they match the type we're insterested in")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_InventoryNumBlocksChangedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, BlockCountChangedEventArgs args);

	public class BlockCountChangedEventArgs : EventArgs
	{
		private BlockTypes m_BlockType;

		private int m_BlockTypeTotal;

		[FriendlyName("Block Type", "Type of the block that had its quantity changed.")]
		[SocketState(false, false)]
		public BlockTypes BlockType => m_BlockType;

		[FriendlyName("Block Total", "The number of blocks of this type stored in the inventory.")]
		[SocketState(false, false)]
		public int BlockTypeTotal => m_BlockTypeTotal;

		public BlockCountChangedEventArgs(BlockTypes blockType, int blockTypeTotal)
		{
			m_BlockType = blockType;
			m_BlockTypeTotal = blockTypeTotal;
		}
	}

	[FriendlyName("Block Count Changed", "Called every time a block is added or removed from the inventory")]
	public event uScriptEventHandler BlockInventoryCountChanged;

	private void InventoryCountChanged(int blockTypeIdx, int blockTypeTotal, int fakeTotalCount)
	{
		if (base.gameObject.activeInHierarchy && this.BlockInventoryCountChanged != null)
		{
			this.BlockInventoryCountChanged(this, new BlockCountChangedEventArgs((BlockTypes)blockTypeIdx, blockTypeTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.BlockInventoryQuantityChangedEvent += InventoryCountChanged;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.BlockInventoryQuantityChangedEvent -= InventoryCountChanged;
	}
}
