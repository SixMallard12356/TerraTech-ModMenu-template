using System;

[Serializable]
public class BlockCount : ICloneable
{
	public const int INFINITE_QUANTITY = -1;

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, true)]
	public BlockTypes m_BlockType;

	public int m_Quantity;

	public BlockCount(BlockTypes blockType, int quantity)
	{
		m_BlockType = blockType;
		m_Quantity = quantity;
	}

	public override string ToString()
	{
		return string.Format("{0} x {1}", (m_Quantity == -1) ? "∞" : m_Quantity.ToString(), m_BlockType.ToString());
	}

	public object Clone()
	{
		return new BlockCount(m_BlockType, m_Quantity);
	}
}
