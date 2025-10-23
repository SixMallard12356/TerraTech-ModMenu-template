#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ThreadWorker
{
	public abstract class ThreadedData
	{
		public ReaderWriterLockSlim m_RWLock = new ReaderWriterLockSlim();

		public object m_Lock = new object();
	}

	private struct ActionEntry
	{
		public IEnumerator routine;

		public Action cancel;
	}

	private volatile bool m_ShouldStop;

	private volatile bool m_CancelAction;

	private object m_Lock = new object();

	private volatile List<ActionEntry> m_Actions = new List<ActionEntry>();

	private ActionEntry? m_CurrentAction;

	private volatile bool m_LogOutput;

	public void DoWork()
	{
		while (!m_ShouldStop)
		{
			if (m_CurrentAction.HasValue && m_CurrentAction.Value.routine != null)
			{
				if (!m_CancelAction)
				{
					if (!m_CurrentAction.Value.routine.MoveNext())
					{
						if (m_LogOutput)
						{
							Log("Ending action " + m_CurrentAction.Value.routine.ToString());
						}
						m_CurrentAction = null;
					}
					continue;
				}
				if (m_LogOutput)
				{
					Log("Cancelling action " + m_CurrentAction.Value.routine.ToString());
				}
				if (m_CurrentAction.Value.cancel != null)
				{
					m_CurrentAction.Value.cancel();
				}
				m_CurrentAction = null;
			}
			else if (m_Actions.Count > 0)
			{
				lock (m_Lock)
				{
					m_CurrentAction = m_Actions[0];
					m_Actions.RemoveAt(0);
					if (m_LogOutput)
					{
						Log("Starting action " + ((m_CurrentAction.HasValue && m_CurrentAction.Value.routine != null) ? m_CurrentAction.Value.routine.ToString() : "null"));
					}
				}
			}
			else
			{
				Thread.Sleep(100);
			}
		}
	}

	public void AddAction(IEnumerator routineEnumerator, Action cancelAction)
	{
		lock (m_Lock)
		{
			m_Actions.Add(new ActionEntry
			{
				routine = routineEnumerator,
				cancel = cancelAction
			});
		}
	}

	public void CancelAllActions()
	{
		lock (m_Lock)
		{
			if (m_CurrentAction.HasValue)
			{
				Log("Begin cancel action");
				m_CancelAction = true;
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				while (m_CurrentAction.HasValue)
				{
					if (Time.realtimeSinceStartup - realtimeSinceStartup > 5f)
					{
						d.LogError("Timed out whilst waiting for current action to complete");
						break;
					}
					Thread.Sleep(100);
				}
				m_CancelAction = false;
				Log("End cancel action");
			}
			for (int i = 0; i < m_Actions.Count; i++)
			{
				m_Actions[i].cancel?.Invoke();
			}
			m_Actions.Clear();
		}
	}

	public void RequestStop()
	{
		m_ShouldStop = true;
	}

	public bool IsBusy()
	{
		bool flag = false;
		lock (m_Lock)
		{
			return m_CurrentAction.HasValue || m_Actions.Count > 0;
		}
	}

	private void Log(string s)
	{
	}
}
