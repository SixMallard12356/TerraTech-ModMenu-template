using System;

[FriendlyName("uScript_GetBlockMouseDown", "An event to return when a block is clicked on.")]
[NodePath("TerraTech/Actions/Blocks")]
public class uScript_GetBlockMouseDown : uScriptEvent
{
	public delegate void uScriptEventHandler(TankBlock sender, BlockMouseDownEventArgs args);

	public class BlockMouseDownEventArgs : EventArgs
	{
		private BlockTypes m_BlockID;

		[FriendlyName("Block Type")]
		public BlockTypes BlockID => m_BlockID;

		public BlockMouseDownEventArgs(BlockTypes blockTypeID)
		{
			m_BlockID = blockTypeID;
		}
	}

	private TankBlock m_TankBlock;

	private static uScript_GetBlockMouseDown m_Instance;

	[FriendlyName("On Block Mouse Down")]
	public event uScriptEventHandler OnBlockMouseDown;

	public void OnSpawn()
	{
		m_TankBlock = GetComponent<TankBlock>();
		if (m_TankBlock != null)
		{
			m_TankBlock.MouseDownEvent.Subscribe(OnTankMouseDown);
		}
		else
		{
			m_Instance = this;
		}
	}

	public void OnRecycle()
	{
		if (m_TankBlock != null)
		{
			m_TankBlock.MouseDownEvent.Unsubscribe(OnTankMouseDown);
		}
	}

	private void OnTankMouseDown(TankBlock tankBlock, int mouseButton)
	{
		if (m_Instance != null && m_Instance.OnBlockMouseDown != null)
		{
			m_Instance.OnBlockMouseDown(tankBlock, new BlockMouseDownEventArgs(tankBlock.BlockType));
		}
	}

	private void OnDestroy()
	{
		if (m_Instance == this)
		{
			m_Instance = null;
		}
	}
}
