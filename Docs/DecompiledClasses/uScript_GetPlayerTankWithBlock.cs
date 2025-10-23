public class uScript_GetPlayerTankWithBlock : uScriptLogic
{
	private Tank m_Tank;

	private bool m_UseBlockType;

	private int m_BlockType;

	private TankBlock m_Block;

	public bool Returned => m_Tank != null;

	public bool NotReturned => m_Tank == null;

	public bool Out => true;

	public Tank In(BlockTypes block, TankBlock tankBlock = null, bool useBlockType = true)
	{
		m_UseBlockType = useBlockType;
		m_BlockType = (int)block;
		m_Block = tankBlock;
		m_Tank = Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsFriendly).FirstOrDefault(MatchAnyBlock);
		return m_Tank;
	}

	private static bool TechIsFriendly(Tank tech)
	{
		return tech.IsFriendly(0);
	}

	private bool MatchAnyBlock(Tank tech)
	{
		return tech.blockman.IterateBlocks().Any(MatchBlockOrType);
	}

	private bool MatchBlockOrType(TankBlock block)
	{
		if (!m_UseBlockType)
		{
			return block == m_Block;
		}
		return block.visible.ItemType == m_BlockType;
	}
}
