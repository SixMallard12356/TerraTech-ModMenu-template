using System;

[FriendlyName("uScript_BlockCraftedEvent", "Listen for block craft events and pass them through if they match the type we're insterested in")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_BlockCraftedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, BlockCraftedEventArgs args);

	public class BlockCraftedEventArgs : EventArgs
	{
		private BlockTypes m_BlockType;

		private int m_BlockTypeTotal;

		private int m_BlockTotal;

		private TankBlock m_Block;

		private TankBlock m_CrafterBlock;

		[FriendlyName("Block Type", "Type of the block that was crafted.")]
		[SocketState(false, false)]
		public BlockTypes BlockType => m_BlockType;

		[FriendlyName("Block Type total", "The total number of blocks crafted of this type.")]
		[SocketState(false, false)]
		public int BlockTypeTotal => m_BlockTypeTotal;

		[FriendlyName("Block Total", "The total number of blocks crafted of all types combined.")]
		[SocketState(false, false)]
		public int BlockTotal => m_BlockTotal;

		[FriendlyName("Block", "The block that was crafted")]
		[SocketState(false, false)]
		public TankBlock Block => m_Block;

		[FriendlyName("Crafter Block", "The block that did the crafting")]
		[SocketState(false, false)]
		public TankBlock CrafterBlock => m_CrafterBlock;

		public BlockCraftedEventArgs(TankBlock crafter, TankBlock block, BlockTypes blockType, int blockTypeTotal, int craftedTotal)
		{
			m_BlockType = blockType;
			m_BlockTypeTotal = blockTypeTotal;
			m_BlockTotal = craftedTotal;
			m_Block = block;
			m_CrafterBlock = crafter;
		}
	}

	[FriendlyName("Block Crafted", "Called every time a block is crafted")]
	public event uScriptEventHandler BlockCraftedEvent;

	private void OnBlockCrafted(TankBlock crafter, TankBlock block, int blockTypeIdx, int blockTypeTotal, int craftedTotal)
	{
		if (base.gameObject.activeInHierarchy && this.BlockCraftedEvent != null)
		{
			this.BlockCraftedEvent(this, new BlockCraftedEventArgs(crafter, block, (BlockTypes)blockTypeIdx, blockTypeTotal, craftedTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.BlockCraftedEvent += OnBlockCrafted;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.BlockCraftedEvent -= OnBlockCrafted;
	}
}
