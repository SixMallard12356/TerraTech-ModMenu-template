using System;
using Unity.IL2CPP.CompilerServices;

public struct Event<A> : IEvent
{
	private DelegateIterator<Action<A>> m_DelegateList;

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Subscribe(Action<A> _delegate)
	{
		if (m_DelegateList == null)
		{
			m_DelegateList = new DelegateIterator<Action<A>>();
		}
		m_DelegateList.Add(_delegate);
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Unsubscribe(Action<A> _delegate)
	{
		if (m_DelegateList != null)
		{
			m_DelegateList.Remove(_delegate);
			if (m_DelegateList.Count == 0)
			{
				m_DelegateList = null;
			}
		}
	}

	public void Clear()
	{
		m_DelegateList = null;
	}

	public bool HasSubscribers()
	{
		return m_DelegateList != null;
	}

	public int GetSubscriberCount()
	{
		if (m_DelegateList == null)
		{
			return 0;
		}
		return m_DelegateList.Count;
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Send(A paramA)
	{
		if (m_DelegateList == null)
		{
			return;
		}
		try
		{
			foreach (Action<A> @delegate in m_DelegateList)
			{
				@delegate(paramA);
			}
		}
		finally
		{
			if (m_DelegateList != null)
			{
				m_DelegateList.StopIterating();
			}
		}
	}

	public void PrintSubscribers(string contextMsg)
	{
		string text = contextMsg + " Current subscribers (" + GetSubscriberCount() + "):";
		if (m_DelegateList == null)
		{
			return;
		}
		foreach (Action<A> @delegate in m_DelegateList)
		{
			text = string.Concat(text, "\n\t[ object '", @delegate.Target, "' method '", @delegate.Method, "' ],");
		}
	}

	public void EnsureNoSubscribers()
	{
		Clear();
	}
}
public struct Event<A, B> : IEvent
{
	private DelegateIterator<Action<A, B>> m_DelegateList;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Subscribe(Action<A, B> _delegate)
	{
		if (m_DelegateList == null)
		{
			m_DelegateList = new DelegateIterator<Action<A, B>>();
		}
		m_DelegateList.Add(_delegate);
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Unsubscribe(Action<A, B> _delegate)
	{
		if (m_DelegateList != null)
		{
			m_DelegateList.Remove(_delegate);
			if (m_DelegateList.Count == 0)
			{
				m_DelegateList = null;
			}
		}
	}

	public void Clear()
	{
		m_DelegateList = null;
	}

	public bool HasSubscribers()
	{
		return m_DelegateList != null;
	}

	public int GetSubscriberCount()
	{
		if (m_DelegateList == null)
		{
			return 0;
		}
		return m_DelegateList.Count;
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Send(A paramA, B paramB)
	{
		if (m_DelegateList == null)
		{
			return;
		}
		try
		{
			foreach (Action<A, B> @delegate in m_DelegateList)
			{
				@delegate(paramA, paramB);
			}
		}
		finally
		{
			if (m_DelegateList != null)
			{
				m_DelegateList.StopIterating();
			}
		}
	}

	public void EnsureNoSubscribers()
	{
		Clear();
	}
}
public struct Event<A, B, C> : IEvent
{
	private DelegateIterator<Action<A, B, C>> m_DelegateList;

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Subscribe(Action<A, B, C> _delegate)
	{
		if (m_DelegateList == null)
		{
			m_DelegateList = new DelegateIterator<Action<A, B, C>>();
		}
		m_DelegateList.Add(_delegate);
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Unsubscribe(Action<A, B, C> _delegate)
	{
		if (m_DelegateList != null)
		{
			m_DelegateList.Remove(_delegate);
			if (m_DelegateList.Count == 0)
			{
				m_DelegateList = null;
			}
		}
	}

	public void Clear()
	{
		m_DelegateList = null;
	}

	public bool HasSubscribers()
	{
		return m_DelegateList != null;
	}

	public int GetSubscriberCount()
	{
		if (m_DelegateList == null)
		{
			return 0;
		}
		return m_DelegateList.Count;
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Send(A paramA, B paramB, C paramC)
	{
		if (m_DelegateList == null)
		{
			return;
		}
		try
		{
			foreach (Action<A, B, C> @delegate in m_DelegateList)
			{
				@delegate(paramA, paramB, paramC);
			}
		}
		finally
		{
			if (m_DelegateList != null)
			{
				m_DelegateList.StopIterating();
			}
		}
	}

	public void EnsureNoSubscribers()
	{
		Clear();
	}
}
public struct Event<A, B, C, D> : IEvent
{
	private DelegateIterator<Action<A, B, C, D>> m_DelegateList;

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Subscribe(Action<A, B, C, D> _delegate)
	{
		if (m_DelegateList == null)
		{
			m_DelegateList = new DelegateIterator<Action<A, B, C, D>>();
		}
		m_DelegateList.Add(_delegate);
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Unsubscribe(Action<A, B, C, D> _delegate)
	{
		if (m_DelegateList != null)
		{
			m_DelegateList.Remove(_delegate);
			if (m_DelegateList.Count == 0)
			{
				m_DelegateList = null;
			}
		}
	}

	public void Clear()
	{
		m_DelegateList = null;
	}

	public bool HasSubscribers()
	{
		return m_DelegateList != null;
	}

	public int GetSubscriberCount()
	{
		if (m_DelegateList == null)
		{
			return 0;
		}
		return m_DelegateList.Count;
	}

	[Il2CppSetOption(Option.NullChecks, false)]
	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	public void Send(A paramA, B paramB, C paramC, D paramD)
	{
		if (m_DelegateList == null)
		{
			return;
		}
		try
		{
			foreach (Action<A, B, C, D> @delegate in m_DelegateList)
			{
				@delegate(paramA, paramB, paramC, paramD);
			}
		}
		finally
		{
			if (m_DelegateList != null)
			{
				m_DelegateList.StopIterating();
			}
		}
	}

	public void EnsureNoSubscribers()
	{
		Clear();
	}
}
