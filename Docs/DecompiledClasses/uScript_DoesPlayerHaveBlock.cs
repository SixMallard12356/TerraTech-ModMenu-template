public class uScript_DoesPlayerHaveBlock : uScriptLogic
{
	private bool m_True;

	private int m_BlockType;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(BlockTypes block, [SocketState(false, false)][DefaultValue(false)] bool isDragging = false)
	{
		m_BlockType = (int)block;
		m_True = Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsFriendly).Any(MatchAnyBlock);
		if (isDragging && !m_True && (bool)Singleton.Manager<ManPointer>.inst.DraggingItem && (bool)Singleton.Manager<ManPointer>.inst.DraggingItem.block && Singleton.Manager<ManPointer>.inst.DraggingItem.ItemType == (int)block)
		{
			m_True = true;
		}
	}

	private static bool TechIsFriendly(Tank tech)
	{
		return tech.IsFriendly(0);
	}

	private bool MatchAnyBlock(Tank tech)
	{
		return tech.blockman.IterateBlocks().Any(MatchBlockType);
	}

	private bool MatchBlockType(TankBlock block)
	{
		return block.visible.ItemType == m_BlockType;
	}
}
