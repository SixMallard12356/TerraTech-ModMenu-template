using System.Collections.Generic;

public class BlockTypesComparer : IEqualityComparer<BlockTypes>
{
	public bool Equals(BlockTypes x, BlockTypes y)
	{
		return x == y;
	}

	public int GetHashCode(BlockTypes obj)
	{
		return (int)obj;
	}
}
