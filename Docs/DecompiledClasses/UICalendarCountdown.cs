using System;
using UnityEngine;
using UnityEngine.UI;

public class UICalendarCountdown : MonoBehaviour
{
	[SerializeField]
	private string m_PeriodToCountTo;

	private Text m_Text;

	private ManCalendar.TimePeriod m_Period;

	private void Awake()
	{
		m_Text = GetComponent<Text>();
		m_Period = Singleton.Manager<ManCalendar>.inst.GetTimePeriod(m_PeriodToCountTo);
	}

	private void Update()
	{
		if (m_Text != null && m_Period != null)
		{
			TimeSpan timeSpan = m_Period.StartDateTime - DateTime.Now;
			m_Text.text = string.Format("in {0}:{1}:{2}", Math.Floor(timeSpan.TotalHours).ToString("00"), timeSpan.Minutes.ToString("00"), timeSpan.Seconds.ToString("00"));
		}
	}
}
