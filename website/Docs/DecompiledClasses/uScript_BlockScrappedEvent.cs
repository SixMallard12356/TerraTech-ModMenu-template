using System;

[FriendlyName("uScript_BlockScrappedEvent", "Listen for block scrap events and pass them through if they match the type we're insterested in")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_BlockScrappedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, BlockScrappedEventArgs args);

	public class BlockScrappedEventArgs : EventArgs
	{
		private BlockTypes m_BlockType;

		private int m_BlockTypeTotal;

		private int m_BlockTotal;

		private TankBlock m_Block;

		private TankBlock m_CrafterBlock;

		[SocketState(false, false)]
		[FriendlyName("Block Type", "Type of the block that was scrapped.")]
		public BlockTypes BlockType => m_BlockType;

		[FriendlyName("Block Type total", "The total number of blocks scrapped of this type.")]
		[SocketState(false, false)]
		public int BlockTypeTotal => m_BlockTypeTotal;

		[FriendlyName("Block Total", "The total number of blocks scrapped of all types combined.")]
		[SocketState(false, false)]
		public int BlockTotal => m_BlockTotal;

		[FriendlyName("Block", "The block that was scrapped")]
		[SocketState(false, false)]
		public TankBlock Block => m_Block;

		[SocketState(false, false)]
		[FriendlyName("Crafter Block", "The block that did the scrapping")]
		public TankBlock CrafterBlock => m_CrafterBlock;

		public BlockScrappedEventArgs(TankBlock crafter, TankBlock block, BlockTypes blockType, int blockTypeTotal, int scrappedTotal)
		{
			m_BlockType = blockType;
			m_BlockTypeTotal = blockTypeTotal;
			m_BlockTotal = scrappedTotal;
			m_Block = block;
			m_CrafterBlock = crafter;
		}
	}

	[FriendlyName("Block Scrapped", "Called every time a block is scrapped")]
	public event uScriptEventHandler BlockScrappedEvent;

	private void OnBlockScrapped(TankBlock crafter, TankBlock block, int blockTypeIdx, int blockTypeTotal, int scrappedTotal)
	{
		if (base.gameObject.activeInHierarchy && this.BlockScrappedEvent != null)
		{
			this.BlockScrappedEvent(this, new BlockScrappedEventArgs(crafter, block, (BlockTypes)blockTypeIdx, blockTypeTotal, scrappedTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.BlockScrappedEvent += OnBlockScrapped;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.BlockScrappedEvent -= OnBlockScrapped;
	}
}
