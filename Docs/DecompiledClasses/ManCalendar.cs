#define UNITY_EDITOR
using System;
using System.Globalization;
using UnityEngine;

public class ManCalendar : Singleton.Manager<ManCalendar>
{
	[Serializable]
	public class TimePeriod
	{
		public string m_Name;

		public string m_StartDate;

		public string m_StartTime;

		public string m_EndDate;

		public string m_EndTime;

		public bool m_UserEnabled;

		public string StartDateTimeString => m_StartDate + " " + m_StartTime;

		public string EndDateTimeString => m_EndDate + " " + m_EndTime;

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public bool Valid { get; set; }
	}

	[SerializeField]
	private TimePeriod[] m_TimePeriod;

	public Event<TimePeriod, bool> RawTimePeriodChangedEvent;

	public Event<TimePeriod, bool> UserTimePeriodChangedEvent;

	private CultureInfo m_CultureInfo;

	private DateTimeStyles m_DateTimeStyles;

	private TimePeriod m_TimePeriodActive;

	public bool IsTimePeriodActive(string name)
	{
		bool result = false;
		TimePeriod timePeriodActive = GetTimePeriodActive();
		if (timePeriodActive != null && timePeriodActive.m_Name == name)
		{
			result = true;
		}
		return result;
	}

	public TimePeriod GetTimePeriodActive()
	{
		TimePeriod result = null;
		DateTime now = DateTime.Now;
		for (int i = 0; i < m_TimePeriod.Length; i++)
		{
			if (m_TimePeriod[i].Valid && DateTime.Compare(now, m_TimePeriod[i].StartDateTime) >= 0 && DateTime.Compare(now, m_TimePeriod[i].EndDateTime) < 0)
			{
				result = m_TimePeriod[i];
				break;
			}
		}
		return result;
	}

	public TimePeriod GetTimePeriod(string periodName)
	{
		TimePeriod result = null;
		for (int i = 0; i < m_TimePeriod.Length; i++)
		{
			if (m_TimePeriod[i].m_Name == periodName)
			{
				result = m_TimePeriod[i];
				break;
			}
		}
		return result;
	}

	public void SetUserEnabledFlag(string periodName, bool enabled)
	{
		TimePeriod timePeriod = GetTimePeriod(periodName);
		if (timePeriod != null)
		{
			if (timePeriod.m_UserEnabled != enabled)
			{
				timePeriod.m_UserEnabled = enabled;
				if (timePeriod == m_TimePeriodActive)
				{
					UserTimePeriodChangedEvent.Send(timePeriod, enabled);
				}
			}
		}
		else
		{
			d.LogWarning("ManCalendar:SetUserEnabledFlag could find no such period called \"" + periodName + "\"");
		}
	}

	public void Start()
	{
		m_CultureInfo = CultureInfo.CreateSpecificCulture("en-GB");
		m_DateTimeStyles = DateTimeStyles.None;
		for (int i = 0; i < m_TimePeriod.Length; i++)
		{
			if (DateTime.TryParse(m_TimePeriod[i].StartDateTimeString, m_CultureInfo, m_DateTimeStyles, out var result))
			{
				m_TimePeriod[i].StartDateTime = result;
				if (DateTime.TryParse(m_TimePeriod[i].EndDateTimeString, m_CultureInfo, m_DateTimeStyles, out var result2))
				{
					m_TimePeriod[i].EndDateTime = result2;
					if (DateTime.Compare(result2, result) <= 0)
					{
						d.LogError("ManCalendar - Time Period " + base.name + " Has EndDateTime before StartDateTime");
					}
					else
					{
						m_TimePeriod[i].Valid = true;
					}
				}
				else
				{
					d.LogError("ManCalendar - Time Period " + base.name + " Has invalid Start DateTime");
				}
			}
			else
			{
				d.LogError("ManCalendar - Time Period " + base.name + " Has invalid End DateTime");
			}
		}
		for (int j = 0; j < m_TimePeriod.Length - 1; j++)
		{
			DateTime startDateTime = m_TimePeriod[j].StartDateTime;
			DateTime endDateTime = m_TimePeriod[j].EndDateTime;
			for (int k = 2; k < m_TimePeriod.Length; k++)
			{
				DateTime startDateTime2 = m_TimePeriod[k].StartDateTime;
				DateTime endDateTime2 = m_TimePeriod[k].EndDateTime;
				if ((DateTime.Compare(startDateTime2, endDateTime) < 0 && DateTime.Compare(endDateTime2, endDateTime) >= 0) || (DateTime.Compare(endDateTime2, startDateTime) >= 0 && DateTime.Compare(startDateTime2, startDateTime) < 0))
				{
					d.LogError("ManCalendar - Time Periods " + m_TimePeriod[j].m_Name + " and " + m_TimePeriod[k].m_Name + " overlap");
				}
			}
		}
		m_TimePeriodActive = GetTimePeriodActive();
	}

	public void Update()
	{
		TimePeriod timePeriodActive = GetTimePeriodActive();
		if (timePeriodActive != null)
		{
			if (m_TimePeriodActive != timePeriodActive)
			{
				if (m_TimePeriodActive != null)
				{
					SendTimePeriodChangedEvents(m_TimePeriodActive, enabled: false);
				}
				SendTimePeriodChangedEvents(timePeriodActive, enabled: true);
				m_TimePeriodActive = timePeriodActive;
			}
		}
		else if (m_TimePeriodActive != null)
		{
			SendTimePeriodChangedEvents(m_TimePeriodActive, enabled: false);
			m_TimePeriodActive = null;
		}
	}

	private void SendTimePeriodChangedEvents(TimePeriod period, bool enabled)
	{
		RawTimePeriodChangedEvent.Send(period, enabled);
		if (period.m_UserEnabled)
		{
			UserTimePeriodChangedEvent.Send(period, enabled);
		}
	}
}
