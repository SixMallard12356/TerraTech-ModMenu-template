using System;
using Unity.IL2CPP.CompilerServices;
using Unity.Profiling;

public struct EventNoParams : IEvent
{
	private DelegateIterator<Action> m_DelegateList;

	private static ProfilerMarker _s_Marker;

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Subscribe(Action _delegate)
	{
		if (m_DelegateList == null)
		{
			m_DelegateList = new DelegateIterator<Action>();
		}
		m_DelegateList.Add(_delegate);
	}

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Unsubscribe(Action _delegate)
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

	public void SendTracked(string trackingTitle)
	{
		if (m_DelegateList == null)
		{
			return;
		}
		try
		{
			foreach (Action @delegate in m_DelegateList)
			{
				_s_Marker = new ProfilerMarker($"'{trackingTitle}'.'{@delegate.Target}'");
				@delegate();
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

	[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
	[Il2CppSetOption(Option.NullChecks, false)]
	public void Send()
	{
		if (m_DelegateList == null)
		{
			return;
		}
		try
		{
			foreach (Action @delegate in m_DelegateList)
			{
				@delegate();
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
