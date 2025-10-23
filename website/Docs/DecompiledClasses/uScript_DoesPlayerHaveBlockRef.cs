[FriendlyName("Player/Does player have block")]
public class uScript_DoesPlayerHaveBlockRef : uScriptLogic
{
	private bool m_True;

	private bool m_UseBlockType;

	private int m_BlockType;

	private TankBlock m_Block;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(TankBlock block, BlockTypes type, bool useBlockType)
	{
		m_UseBlockType = useBlockType;
		m_BlockType = (int)type;
		m_Block = block;
		m_True = Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsFriendly).Any(MatchAnyBlock);
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
