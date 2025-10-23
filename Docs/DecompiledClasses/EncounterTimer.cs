using System;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class EncounterTimer
{
	public EventNoParams OnCountdownExpired;

	[JsonProperty]
	private float m_TotalCountdownTime;

	[JsonProperty]
	private float m_TimeRemaining;

	[JsonProperty]
	private bool m_TimerExpired;

	[JsonProperty]
	private bool m_Active;

	public bool IsRunning => !m_TimerExpired;

	public bool IsExpired => m_TimerExpired;

	public float TimeRemaining => m_TimeRemaining;

	public float TimeRemainingPercent => m_TimeRemaining / m_TotalCountdownTime * 100f;

	public float TotalCountdownTime => m_TotalCountdownTime;

	public void FillEncounterTimerData(out EncounterUpdateMessage.EncounterTimerData data)
	{
		data.m_Active = m_Active;
		data.m_TimeRemaining = m_TimeRemaining;
		data.m_TimerExpired = m_TimerExpired;
		data.m_TotalCountdownTime = m_TotalCountdownTime;
	}

	public EncounterTimer()
	{
	}

	public EncounterTimer(float countdownTime, bool startTimer = true)
	{
		m_TotalCountdownTime = countdownTime;
		m_TimeRemaining = countdownTime;
		m_TimerExpired = false;
		m_Active = startTimer;
	}

	public EncounterTimer(EncounterUpdateMessage.EncounterTimerData data)
	{
		m_Active = data.m_Active;
		m_TimerExpired = data.m_TimerExpired;
		m_TimeRemaining = data.m_TimeRemaining;
		m_TotalCountdownTime = data.m_TotalCountdownTime;
	}

	public void AddTimeToTimer(float timeToAdd)
	{
		m_TimeRemaining += timeToAdd;
	}

	public void UpdateTimer()
	{
		if (m_Active)
		{
			m_TimeRemaining -= Time.deltaTime;
			if (m_TimeRemaining <= 0f)
			{
				m_TimeRemaining = 0f;
				m_TimerExpired = true;
				m_Active = false;
				OnCountdownExpired.Send();
			}
		}
	}

	public void Pause()
	{
		m_Active = false;
	}

	public void Resume()
	{
		m_Active = true;
	}
}
