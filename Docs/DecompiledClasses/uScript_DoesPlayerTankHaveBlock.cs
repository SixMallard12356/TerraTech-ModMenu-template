[FriendlyName("Does player tank have")]
public class uScript_DoesPlayerTankHaveBlock : uScriptLogic
{
	private bool m_True;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(BlockTypes blockType, int amount = 1)
	{
		if (m_True || !Singleton.playerTank)
		{
			return;
		}
		int num = 0;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.visible.ItemType == (int)blockType)
			{
				num++;
			}
		}
		m_True = num >= amount;
	}

	public void OnDisable()
	{
		m_True = false;
	}
}
