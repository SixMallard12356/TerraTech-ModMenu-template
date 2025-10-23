using UnityEngine;

public class CalendarEnabledSetter : MonoBehaviour
{
	[SerializeField]
	private string m_Period;

	public void SetPeriodEnabled(bool toggledOn)
	{
		Singleton.Manager<ManCalendar>.inst.SetUserEnabledFlag(m_Period, toggledOn);
	}
}
