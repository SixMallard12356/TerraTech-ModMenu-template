public class Permute
{
	private uint m_index;

	private uint m_intermediateOffset;

	private static uint permuteQPR(uint x)
	{
		if (x >= 4294967291u)
		{
			return x;
		}
		uint num = (uint)((ulong)((long)x * (long)x) % 4294967291uL);
		if (x > 2147483645)
		{
			return 4294967291u - num;
		}
		return num;
	}

	public void RandomSequenceOfUnique(uint seedBase, uint seedOffset)
	{
		m_index = permuteQPR(permuteQPR(seedBase) + 1747911009);
		m_intermediateOffset = permuteQPR(permuteQPR(seedOffset) + 1182337285);
	}

	public uint next()
	{
		return permuteQPR((permuteQPR(m_index++) + m_intermediateOffset) ^ 0x5BF03635);
	}
}
