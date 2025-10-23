using System;

[FriendlyName("uScript_MoneyFromBlockSaleEvent", "Listen for block sell events and pass them through if they match the type we're insterested in")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_MoneyFromBlockSaleEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, MoneyFromBlockSaleEventArgs args);

	public class MoneyFromBlockSaleEventArgs : EventArgs
	{
		private BlockTypes m_BlockType;

		private int m_BlockTypeTotal;

		private int m_MoneyTotal;

		[SocketState(false, false)]
		[FriendlyName("Block Type", "Type of the block that was sold.")]
		public BlockTypes BlockType => m_BlockType;

		[SocketState(false, false)]
		[FriendlyName("Block Type total", "The total number of blocks sold of this type.")]
		public int BlockTypeTotal => m_BlockTypeTotal;

		[FriendlyName("Block Total", "The total number of blocks sold of all types combined.")]
		[SocketState(false, false)]
		public int MoneyTotal => m_MoneyTotal;

		public MoneyFromBlockSaleEventArgs(BlockTypes blockType, int blockTypeTotal, int moneyTotal)
		{
			m_BlockType = blockType;
			m_BlockTypeTotal = blockTypeTotal;
			m_MoneyTotal = moneyTotal;
		}
	}

	[FriendlyName("Block Sold", "Called every time a block is sold")]
	public event uScriptEventHandler MoneyFromBlockSaleEvent;

	private void OnMoneyFromBlockSale(int blockTypeIdx, int blockTypeTotal, int moneyTotal)
	{
		if (base.gameObject.activeInHierarchy && this.MoneyFromBlockSaleEvent != null)
		{
			this.MoneyFromBlockSaleEvent(this, new MoneyFromBlockSaleEventArgs((BlockTypes)blockTypeIdx, blockTypeTotal, moneyTotal));
		}
	}

	private void OnEnable()
	{
		Singleton.Manager<ManStats>.inst.MoneyFromBlockSaleEvent += OnMoneyFromBlockSale;
	}

	private void OnDisable()
	{
		Singleton.Manager<ManStats>.inst.MoneyFromBlockSaleEvent -= OnMoneyFromBlockSale;
	}
}
