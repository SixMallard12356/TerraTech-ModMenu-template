using System.Collections.Generic;

public class LinearAllocPool<T> where T : new()
{
	private List<T> m_Nodes;

	private int m_NumUsed;

	public int AllocCount => m_NumUsed;

	public LinearAllocPool(int initialCapacity = 128)
	{
		m_Nodes = new List<T>(initialCapacity);
	}

	public T Alloc()
	{
		if (m_NumUsed >= m_Nodes.Count)
		{
			m_Nodes.Add(new T());
		}
		int numUsed = m_NumUsed;
		m_NumUsed++;
		return m_Nodes[numUsed];
	}

	public void Reset()
	{
		m_NumUsed = 0;
	}
}
