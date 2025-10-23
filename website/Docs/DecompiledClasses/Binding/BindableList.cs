#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;

namespace Binding;

public class BindableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
{
	private Event<BindableList<T>, int, BindableChange> Changed;

	private IList<T> m_List;

	public IList<T> List
	{
		get
		{
			return m_List;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentException("Bindable List - Attempting to set list to null. Illegal Operation");
			}
			m_List = value;
			Changed.Send(this, -1, BindableChange.Reset);
		}
	}

	public int Count => m_List.Count;

	public bool IsReadOnly => m_List.IsReadOnly;

	public T this[int index]
	{
		get
		{
			if (index < 0 || index >= m_List.Count)
			{
				throw new ArgumentOutOfRangeException($"BindableList- index {index} out of bounds {m_List.Count}");
			}
			return m_List[index];
		}
		set
		{
			if (index < 0 || index >= m_List.Count)
			{
				throw new ArgumentOutOfRangeException($"BindableList- index {index} out of bounds {m_List.Count}");
			}
			m_List[index] = value;
			Changed.Send(this, index, BindableChange.ItemChanged);
		}
	}

	public BindableList()
	{
		m_List = new List<T>();
	}

	public BindableList(IList<T> list)
	{
		if (list == null)
		{
			throw new ArgumentException("BindableList - Attempting to set list to null. Illegal Operation");
		}
		m_List = list;
	}

	public void Add(T item)
	{
		m_List.Add(item);
		Changed.Send(this, m_List.Count - 1, BindableChange.ItemAdded);
	}

	public void Clear()
	{
		m_List.Clear();
		Changed.Send(this, -1, BindableChange.Reset);
	}

	public bool Contains(T item)
	{
		return m_List.Contains(item);
	}

	public void CopyTo(T[] array, int arrayIndex)
	{
		m_List.CopyTo(array, arrayIndex);
	}

	public bool Remove(T item)
	{
		int num = m_List.IndexOf(item);
		if (num < 0)
		{
			return false;
		}
		m_List.RemoveAt(num);
		Changed.Send(this, num, BindableChange.ItemDeleted);
		return true;
	}

	public int IndexOf(T item)
	{
		return m_List.IndexOf(item);
	}

	public void Insert(int index, T item)
	{
		if (index < 0 || index > m_List.Count)
		{
			throw new ArgumentOutOfRangeException($"BindableList- index {index} out of bounds {m_List.Count}");
		}
		m_List.Insert(index, item);
		Changed.Send(this, index, BindableChange.ItemAdded);
	}

	public void RemoveAt(int index)
	{
		if (index < 0 || index >= m_List.Count)
		{
			throw new ArgumentOutOfRangeException($"BindableList- index {index} out of bounds {m_List.Count}");
		}
		m_List.RemoveAt(index);
		Changed.Send(this, index, BindableChange.ItemDeleted);
	}

	public IEnumerator<T> GetEnumerator()
	{
		return m_List.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Sort(Comparison<T> comparison)
	{
		if (m_List is List<T> list)
		{
			list.Sort(comparison);
			Changed.Send(this, -1, BindableChange.Reset);
		}
		else
		{
			d.LogErrorFormat("BindableList.Sort: IList {0} is not of type {1}, sorting is not supported", m_List.GetType(), typeof(List<T>));
		}
	}

	public void Sort(IComparer<T> comparer)
	{
		if (m_List is List<T> list)
		{
			list.Sort(comparer);
			Changed.Send(this, -1, BindableChange.Reset);
		}
		else
		{
			d.LogErrorFormat("BindableList.Sort: IList {0} is not of type {1}, sorting is not supported", m_List.GetType(), typeof(List<T>));
		}
	}

	public void Bind(Action<BindableList<T>, int, BindableChange> listener)
	{
		if (listener == null)
		{
			throw new ArgumentException("BindableList - listener can not be null");
		}
		Changed.Unsubscribe(listener);
		Changed.Subscribe(listener);
		listener(this, -1, BindableChange.Reset);
	}

	public void Unbind(Action<BindableList<T>, int, BindableChange> listener)
	{
		if (listener == null)
		{
			throw new ArgumentException("BindableList - listener can not be null");
		}
		Changed.Unsubscribe(listener);
	}
}
