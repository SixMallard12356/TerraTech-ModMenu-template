#define UNITY_EDITOR
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Timer
{
	public struct Data
	{
		private bool IsRunning;

		private bool Stopped;

		private bool LastTickingState;

		private double TimeElapsed;

		private double StartTimeRemaining;

		private long FirstTickTime;

		public Data(Timer timer)
		{
			IsRunning = timer.m_IsRunning;
			Stopped = timer.m_Stopped;
			TimeElapsed = timer.m_TimeElapsed;
			StartTimeRemaining = timer.m_StartTimeRemaining;
			LastTickingState = timer.m_LastTickingState;
			FirstTickTime = timer.m_FirstTickTime;
		}

		public void ApplyToTimer(Timer timer)
		{
			timer.m_IsRunning = IsRunning;
			timer.m_Stopped = Stopped;
			timer.TimeElapsed = (float)TimeElapsed;
			timer.m_StartTimeRemaining = (float)StartTimeRemaining;
			timer.m_LastTickingState = LastTickingState;
			timer.SetFirstTickTime(FirstTickTime);
			timer.RefreshIsTicking(forceRefresh: true);
		}

		public void CopyFromTimer(Timer timer)
		{
			IsRunning = timer.m_IsRunning;
			Stopped = timer.m_Stopped;
			TimeElapsed = timer.m_TimeElapsed;
			StartTimeRemaining = timer.m_StartTimeRemaining;
			LastTickingState = timer.m_LastTickingState;
			FirstTickTime = timer.m_FirstTickTime;
		}

		public bool Serialize(NetworkWriter writer)
		{
			writer.Write(IsRunning);
			writer.Write(Stopped);
			writer.Write(TimeElapsed);
			writer.Write(StartTimeRemaining);
			writer.Write(LastTickingState);
			writer.Write(FirstTickTime);
			return true;
		}

		public bool Deserialize(NetworkReader reader)
		{
			IsRunning = reader.ReadBoolean();
			Stopped = reader.ReadBoolean();
			TimeElapsed = reader.ReadDouble();
			StartTimeRemaining = reader.ReadDouble();
			LastTickingState = reader.ReadBoolean();
			FirstTickTime = reader.ReadInt64();
			return true;
		}
	}

	[JsonIgnore]
	public Event<bool> TickingStateChangedEvent;

	[JsonIgnore]
	public Event<float> TimeElapsedChangedEvent;

	[JsonIgnore]
	public Event<bool> TimerActivatedEvent;

	[JsonProperty]
	protected bool m_IsRunning;

	[JsonProperty]
	protected bool m_Stopped;

	[JsonProperty]
	protected float m_TimeElapsed;

	[JsonProperty]
	protected float m_StartTimeRemaining;

	[JsonProperty]
	protected bool m_LastTickingState;

	[JsonProperty]
	protected long m_FirstTickTime;

	protected bool m_AllowNegativeDisplayTime = true;

	[JsonIgnore]
	public bool IsRunningSet => m_IsRunning;

	[JsonIgnore]
	public bool IsTicking
	{
		get
		{
			if (m_IsRunning)
			{
				return !m_Stopped;
			}
			return false;
		}
	}

	[JsonIgnore]
	public bool Activated => (float)m_FirstTickTime > 0f;

	[JsonIgnore]
	public long FirstTickTime => m_FirstTickTime;

	[JsonIgnore]
	public float TimeElapsed
	{
		get
		{
			return m_TimeElapsed;
		}
		protected set
		{
			if (value == 0f && m_TimeElapsed > 0f)
			{
				SetFirstTickTime(0L);
			}
			bool num = m_TimeElapsed != value;
			m_TimeElapsed = value;
			if (num)
			{
				TimeElapsedChangedEvent.Send(m_TimeElapsed);
			}
		}
	}

	[JsonIgnore]
	public float DisplayTime
	{
		get
		{
			float num = ((m_StartTimeRemaining > 0f) ? (m_StartTimeRemaining - m_TimeElapsed) : m_TimeElapsed);
			if (!m_AllowNegativeDisplayTime)
			{
				return Mathf.Max(0f, num);
			}
			return num;
		}
	}

	[JsonIgnore]
	public bool AllowNegativeDisplayTime
	{
		get
		{
			return m_AllowNegativeDisplayTime;
		}
		set
		{
			m_AllowNegativeDisplayTime = value;
		}
	}

	private void RefreshIsTicking(bool forceRefresh = false)
	{
		if (forceRefresh || IsTicking != m_LastTickingState)
		{
			m_LastTickingState = IsTicking;
			TickingStateChangedEvent.Send(IsTicking);
		}
	}

	private void SetFirstTickTime(long unixTime)
	{
		m_FirstTickTime = unixTime;
		TimerActivatedEvent.Send(Activated);
	}

	public void Start(float startTimeRemaining = 0f)
	{
		m_StartTimeRemaining = startTimeRemaining;
		m_Stopped = false;
		m_IsRunning = true;
	}

	public void SetRunning(bool state)
	{
		m_IsRunning = state;
		RefreshIsTicking();
	}

	public void AddToCountdownTime(float timeToAdd)
	{
		d.Assert(m_StartTimeRemaining > 0f, "Timer.AddToCountdownTime - Calling AddToCountdownTime on a timer that is not counting down. If you want to start a count down timer start it with a time value > 0f passed in.");
		m_StartTimeRemaining += timeToAdd;
	}

	public void Update()
	{
		if (IsTicking && !Singleton.Manager<ManQuestLog>.inst.DebugPauseMissionTimer)
		{
			if (m_FirstTickTime == 0L)
			{
				SetFirstTickTime(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
			}
			TimeElapsed += Time.deltaTime;
		}
	}

	public void Stop()
	{
		m_IsRunning = false;
		m_Stopped = true;
		RefreshIsTicking();
	}

	public void Restart()
	{
		ResetTimeElapsed();
		Start(m_StartTimeRemaining);
	}

	public void ResetTimeElapsed(float startingTime = 0f)
	{
		TimeElapsed = startingTime;
	}

	public void Reset(bool stop = true)
	{
		TimeElapsed = 0f;
		m_StartTimeRemaining = 0f;
		SetRunning(!stop);
	}

	public Data ToData()
	{
		return new Data(this);
	}

	public void ApplyConfigFrom(Timer timer)
	{
		timer.ToData().ApplyToTimer(this);
	}
}
