[FriendlyName("Tank/Does Tank Have")]
public class uScript_DoesTankHave : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(Tank tank, int amountOfPieces, BlockTypes block, bool checkForAmountOnly = false)
	{
		if (tank == null)
		{
			return;
		}
		if (checkForAmountOnly)
		{
			m_True = tank.blockman.blockCount >= amountOfPieces;
			return;
		}
		int num = 0;
		if (block == BlockTypes.GSOWheel_111)
		{
			BlockManager.BlockIterator<ModuleWheels>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleWheels>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.block.IsController)
				{
					num++;
				}
			}
		}
		else
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator2.MoveNext())
			{
				if (enumerator2.Current.visible.ItemType == (int)block)
				{
					num++;
				}
			}
		}
		m_True = num >= amountOfPieces;
	}
}
