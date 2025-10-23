public class uScript_BlockAttachedToTech : uScriptLogic
{
	private bool m_True;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(Tank tech, TankBlock block)
	{
		m_True = false;
		if (!tech)
		{
			return;
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current == block)
			{
				m_True = true;
				break;
			}
		}
	}
}
