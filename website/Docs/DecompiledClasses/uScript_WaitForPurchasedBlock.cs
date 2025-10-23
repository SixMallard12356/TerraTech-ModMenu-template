public class uScript_WaitForPurchasedBlock : uScriptLogic
{
	private bool m_BlockFound;

	private bool m_FirstFrame;

	private BlockTypes m_BlockType;

	private TankBlock m_Block;

	public bool Out => true;

	[FriendlyName("Block Found", "Hooray! The block has been purchased and we have a reference to it")]
	public bool PurchaseFound => m_BlockFound;

	[FriendlyName("Waiting", "The block we care about doesn't exist yet")]
	public bool Waiting => !m_BlockFound;

	public TankBlock In(BlockTypes blockType)
	{
		if (m_FirstFrame)
		{
			Singleton.Manager<ManPurchases>.inst.OnBlockPurchased.Subscribe(OnBlockPurchased);
			m_BlockType = blockType;
			m_FirstFrame = false;
		}
		if (m_Block != null)
		{
			m_BlockFound = true;
		}
		return m_Block;
	}

	private void OnBlockPurchased(TankBlock block)
	{
		if (block.BlockType == m_BlockType)
		{
			Singleton.Manager<ManPurchases>.inst.OnBlockPurchased.Unsubscribe(OnBlockPurchased);
			m_Block = block;
		}
	}

	public void OnEnable()
	{
		m_BlockFound = false;
		m_FirstFrame = true;
		m_Block = null;
	}

	public void OnDisable()
	{
		Singleton.Manager<ManPurchases>.inst.OnBlockPurchased.Unsubscribe(OnBlockPurchased);
	}
}
