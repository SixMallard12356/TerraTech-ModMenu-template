#define UNITY_EDITOR
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class UIDBucket
{
	[JsonProperty]
	private int m_Current;

	private int m_RangeMinInclusive;

	private int m_RangeMaxExclusive;

	private bool m_LoopWarning;

	public UIDBucket(int rangeMin, int rangeMax, bool warnAboutLooping = true)
	{
		m_RangeMinInclusive = rangeMin;
		m_RangeMaxExclusive = rangeMax;
		m_Current = rangeMin;
		m_LoopWarning = warnAboutLooping;
	}

	public int GetNextAndIncrement()
	{
		m_Current++;
		if (m_Current >= m_RangeMaxExclusive)
		{
			m_Current = m_RangeMinInclusive;
			if (m_LoopWarning)
			{
				d.LogWarningFormat("UIDBucket with range [{0}, {1}] has reached max value and is looping back to the start!", m_RangeMinInclusive, m_RangeMaxExclusive);
			}
		}
		return m_Current;
	}

	public int GetCurrent()
	{
		return m_Current;
	}

	public void SetCurrent(int newValue)
	{
		if (newValue >= m_RangeMinInclusive && newValue < m_RangeMaxExclusive)
		{
			m_Current = newValue;
			return;
		}
		d.LogErrorFormat("UIDBucket.SetCurrent - Value passed in {0} is not in the valid range [{1}, {2}]! Skipping set!", newValue, m_RangeMinInclusive, m_RangeMaxExclusive);
	}

	public void Reset()
	{
		m_Current = m_RangeMinInclusive;
	}

	[OnDeserialized]
	private void OnDeserialized(StreamingContext context)
	{
		SetCurrent(m_Current);
		Mathf.Clamp(m_Current, m_RangeMinInclusive, m_RangeMaxExclusive - 1);
	}
}
