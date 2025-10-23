using System.Collections.Generic;

public class PooledLinkedList<T>
{
	public struct Iterator
	{
		private PooledLinkedList<T> m_List;

		private LinkedListNode<T> m_Current;

		private LinkedListNode<T> m_Next;

		public T Current => m_Current.Value;

		public LinkedListNode<T> CurrentNode => m_Current;

		public Iterator(PooledLinkedList<T> list)
		{
			m_List = list;
			m_Current = null;
			m_Next = m_List.First;
		}

		public bool MoveNext()
		{
			m_Current = m_Next;
			m_Next = ((m_Next != null) ? m_Next.Next : null);
			return m_Current != null;
		}

		public void RemoveCurrent()
		{
			if (m_Current != null)
			{
				m_List.RemoveNode(m_Current);
				m_Current = null;
			}
		}
	}

	private LinkedList<T> m_List = new LinkedList<T>();

	private Stack<LinkedListNode<T>> m_InstancePool;

	private static Stack<LinkedListNode<T>> s_StaticPool;

	public LinkedListNode<T> First => m_List.First;

	public LinkedListNode<T> Last => m_List.Last;

	public int Count => m_List.Count;

	private Stack<LinkedListNode<T>> GetPool()
	{
		if (m_InstancePool != null)
		{
			return m_InstancePool;
		}
		return s_StaticPool;
	}

	public PooledLinkedList(bool useStaticPool = true, int initialSize = 32)
	{
		if (useStaticPool)
		{
			if (s_StaticPool == null)
			{
				s_StaticPool = new Stack<LinkedListNode<T>>(initialSize);
			}
		}
		else
		{
			m_InstancePool = new Stack<LinkedListNode<T>>(initialSize);
		}
	}

	public LinkedListNode<T> Add(T item)
	{
		Stack<LinkedListNode<T>> pool = GetPool();
		if (pool.Count == 0)
		{
			pool.Push(new LinkedListNode<T>(item));
		}
		LinkedListNode<T> linkedListNode = pool.Pop();
		linkedListNode.Value = item;
		m_List.AddLast(linkedListNode);
		return linkedListNode;
	}

	public void Remove(LinkedListNode<T> node)
	{
		m_List.Remove(node);
		GetPool().Push(node);
	}

	public void Clear()
	{
		Stack<LinkedListNode<T>> pool = GetPool();
		Iterator iterator = default(Iterator);
		while (iterator.MoveNext())
		{
			pool.Push(iterator.CurrentNode);
		}
		m_List.Clear();
	}

	public void AddNode(LinkedListNode<T> node)
	{
		m_List.AddLast(node);
	}

	public void RemoveNode(LinkedListNode<T> node)
	{
		m_List.Remove(node);
	}

	public LinkedListNode<T> AllocateNode(T item)
	{
		Stack<LinkedListNode<T>> pool = GetPool();
		if (pool.Count == 0)
		{
			pool.Push(new LinkedListNode<T>(item));
		}
		LinkedListNode<T> linkedListNode = pool.Pop();
		linkedListNode.Value = item;
		return linkedListNode;
	}

	public void RecycleNode(LinkedListNode<T> node)
	{
		GetPool().Push(node);
	}

	public bool ContainsNode(LinkedListNode<T> node)
	{
		return node.List == m_List;
	}

	public Iterator GetEnumerator()
	{
		return new Iterator(this);
	}
}
