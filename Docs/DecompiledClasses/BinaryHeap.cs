#define UNITY_EDITOR
using System;
using System.Collections.Generic;

public class BinaryHeap<T>
{
	private struct Node
	{
		public T item;

		public int priority;
	}

	private List<Node> m_List = new List<Node>();

	public bool Empty => m_List.Count <= 1;

	public int Count => Math.Max(m_List.Count - 1, 0);

	public BinaryHeap()
	{
		m_List.Add(default(Node));
	}

	public void Add(T item, int priority)
	{
		m_List.Add(new Node
		{
			item = item,
			priority = priority
		});
		int num = m_List.Count - 1;
		while (num > 1)
		{
			int num2 = num >> 1;
			if (m_List[num2].priority <= priority)
			{
				Node value = m_List[num2];
				m_List[num2] = m_List[num];
				m_List[num] = value;
				num = num2;
				continue;
			}
			break;
		}
	}

	public T PeekAt(int index)
	{
		if (index >= Count)
		{
			return default(T);
		}
		return m_List[index + 1].item;
	}

	public T PeekFirst()
	{
		if (!Empty)
		{
			return m_List[1].item;
		}
		return default(T);
	}

	public void RemoveFirst()
	{
		bool empty = Empty;
		d.Assert(!empty);
		if (empty)
		{
			return;
		}
		int count = m_List.Count;
		if (count > 2)
		{
			int index = count - 1;
			m_List[1] = m_List[index];
			m_List.RemoveAt(index);
			count--;
			int num = 1;
			int priority = m_List[num].priority;
			int num2 = num;
			while (true)
			{
				int num3 = num << 1;
				int num4 = num3 + 1;
				if (num3 < count && m_List[num3].priority > priority)
				{
					num2 = num3;
				}
				if (num4 < count && m_List[num4].priority > m_List[num2].priority)
				{
					num2 = num4;
				}
				if (num2 != num)
				{
					Node value = m_List[num2];
					m_List[num2] = m_List[num];
					m_List[num] = value;
					num = num2;
					continue;
				}
				break;
			}
		}
		else
		{
			m_List.RemoveAt(count - 1);
		}
	}

	public void Clear()
	{
		if (m_List.Count > 1)
		{
			m_List.RemoveRange(1, m_List.Count - 1);
		}
	}
}
