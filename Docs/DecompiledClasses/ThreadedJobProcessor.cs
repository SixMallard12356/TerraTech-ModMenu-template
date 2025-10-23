#define UNITY_EDITOR
using System;
using System.Threading;
using UnityEngine;

public class ThreadedJobProcessor
{
	public delegate bool JobProcessorDelegate(TestShouldExitDelegate testShouldExit);

	public delegate bool TestShouldExitDelegate(bool allowPause = true);

	private Thread m_Thread;

	private UnityEngine.Object m_RunningLock;

	private ManualResetEvent m_PauseEvent;

	private bool m_Running;

	private bool m_Exit;

	private bool m_Interrupt;

	private bool m_JobInProgress;

	private bool m_JobCompleted;

	public bool IsInitialised
	{
		get
		{
			if (m_Thread != null)
			{
				return m_Thread.IsAlive;
			}
			return false;
		}
	}

	public bool JobInProgress => m_JobInProgress;

	public bool JobCompleted => m_JobCompleted;

	public void Initialise(string threadName, JobProcessorDelegate jobProcessor, System.Threading.ThreadPriority priority = System.Threading.ThreadPriority.Normal)
	{
		d.Assert(!string.IsNullOrEmpty(threadName), "threadName is null or empty");
		d.Assert(threadName.Length < 32, "threadName exceeds 31 characters");
		m_RunningLock = new UnityEngine.Object();
		m_PauseEvent = new ManualResetEvent(initialState: true);
		m_Running = false;
		m_Exit = false;
		m_Interrupt = false;
		m_JobInProgress = false;
		m_JobCompleted = false;
		m_Thread = new Thread((ThreadStart)delegate
		{
			Run(jobProcessor);
		});
		m_Thread.Name = threadName;
		m_Thread.Priority = priority;
		m_Thread.Start();
	}

	public void RequestExit()
	{
		m_Exit = true;
	}

	public void Terminate(bool waitForExit = true)
	{
		m_Exit = true;
		Stop();
		while (waitForExit && m_Running)
		{
			Thread.Sleep(10);
		}
	}

	public void Start()
	{
		d.Assert(m_Thread.IsAlive, "ThreadedJobProcessor.Start - Underlying thread is not running!?");
		d.Assert(!m_JobInProgress, "ThreadedJobProcessor.Start() called while a job is already in progress: please call ThreadedJobProcessor.Stop() first!");
		lock (m_RunningLock)
		{
			m_JobInProgress = true;
			m_JobCompleted = false;
			Monitor.Pulse(m_RunningLock);
			m_PauseEvent.Set();
		}
	}

	public void Stop()
	{
		d.Assert(m_Exit || m_Thread.IsAlive, "ThreadedJobProcessor.Stop - Underlying thread is expected to be a alive when calling stop!");
		m_Interrupt = true;
		lock (m_RunningLock)
		{
			m_JobInProgress = false;
			m_Interrupt = false;
			Monitor.Pulse(m_RunningLock);
		}
	}

	public void Pause(bool pauseOrUnpause)
	{
		d.Assert(m_Thread.IsAlive, "ThreadedJobProcessor.Pause - Underlying thread is not running!?");
		d.Assert(m_JobInProgress, "ThreadedJobProcessor.Pause() called while there is no job running");
		if (pauseOrUnpause)
		{
			m_PauseEvent.Reset();
		}
		else
		{
			m_PauseEvent.Set();
		}
	}

	private void Run(JobProcessorDelegate jobProcessor)
	{
		m_Running = true;
		string text = $"{jobProcessor.Target}.{jobProcessor.Method.Name} (ID {Thread.CurrentThread.ManagedThreadId})";
		d.Log("ThreadedJobProcessor enter: " + text);
		while (!m_Exit)
		{
			try
			{
				lock (m_RunningLock)
				{
					while (!m_Exit && (m_Interrupt || !m_JobInProgress))
					{
						Monitor.Wait(m_RunningLock);
					}
					if (!m_Exit)
					{
						m_JobCompleted = jobProcessor(JobFuncShouldExit);
						m_JobInProgress = false;
					}
					if (!m_Exit && m_JobCompleted)
					{
						Monitor.Wait(m_RunningLock);
					}
				}
			}
			catch (Exception ex)
			{
				d.LogError($"ThreadedJobProcessor exception on {text}: {ex}");
				Singleton.Manager<DebugUtil>.inst.ReRaiseException = ex;
				m_JobInProgress = false;
				m_JobCompleted = false;
				m_Interrupt = false;
			}
		}
		m_Running = false;
		d.Log("ThreadedJobProcessor exit: " + text);
	}

	private bool JobFuncShouldExit(bool allowPause)
	{
		bool flag = m_Interrupt || m_Exit;
		if (!flag && allowPause)
		{
			while (EditorHooks.Paused)
			{
				Monitor.Exit(m_RunningLock);
				Thread.Sleep(10);
				Monitor.Enter(m_RunningLock);
			}
			m_PauseEvent.WaitOne();
		}
		return flag;
	}
}
