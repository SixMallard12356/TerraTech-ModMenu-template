#define UNITY_EDITOR
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class DelegateIterator<T>
{
	private List<T> m_Delegates = new List<T>();

	private const ushort k_IterationInvalid = 65534;

	private const ushort k_IterationStart = ushort.MaxValue;

	private ushort m_IterationIndex = 65534;

	private ushort m_IterationEnd;

	private static readonly DelegateIterator<T> s_NullIterator = new DelegateIterator<T>();

	public int Count => m_Delegates.Count;

	public T Current
	{
		get
		{
			d.Assert(m_IterationIndex != 65534);
			return m_Delegates[m_IterationIndex];
		}
	}

	private bool IsIterating => m_IterationIndex != 65534;

	public bool Contains(T dd)
	{
		return m_Delegates.IndexOf(dd) >= 0;
	}

	public DelegateIterator<T> GetEnumerator()
	{
		if (IsIterating)
		{
			d.Assert(condition: false, "Illegal attempt to start iterating a delegate list, while an iteration is already in progress");
			return s_NullIterator;
		}
		m_IterationIndex = ushort.MaxValue;
		m_IterationEnd = (ushort)m_Delegates.Count;
		return this;
	}

	public bool MoveNext()
	{
		m_IterationIndex++;
		if (m_IterationIndex == m_IterationEnd)
		{
			m_IterationIndex = 65534;
			return false;
		}
		return true;
	}

	public void StopIterating()
	{
		m_IterationIndex = 65534;
	}

	public void Add(T _delegate)
	{
		d.Assert(!IsIterating, "Subscribing to Event handler while it is being iterated. This has the potential to go wrong and the functionality is being deprecated!");
		_ = IsIterating;
		d.Assert(_delegate != null, "Event Subscribe called with null delegate");
		if (_delegate != null)
		{
			m_Delegates.Add(_delegate);
			m_IterationEnd++;
		}
	}

	public void Remove(T _delegate)
	{
		d.Assert(_delegate != null, "Event Unsubscribe called with null delegate");
		if (_delegate == null)
		{
			return;
		}
		int num = m_Delegates.LastIndexOf(_delegate);
		if (num < 0)
		{
			return;
		}
		m_Delegates.RemoveAt(num);
		if (IsIterating)
		{
			if (num <= m_IterationIndex && m_IterationIndex != ushort.MaxValue)
			{
				m_IterationIndex--;
			}
			if (num <= m_IterationEnd)
			{
				m_IterationEnd--;
			}
		}
	}
}
