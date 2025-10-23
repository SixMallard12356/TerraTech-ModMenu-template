public class uScript_HasPlayerPurchased : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(BlockTypes blockType, int quantity)
	{
		int numBlocksPurchased = Singleton.Manager<ManStats>.inst.GetNumBlocksPurchased(blockType);
		m_True = numBlocksPurchased >= quantity;
	}
}
