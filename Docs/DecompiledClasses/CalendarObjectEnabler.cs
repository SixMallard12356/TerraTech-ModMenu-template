using UnityEngine;

public class CalendarObjectEnabler : MonoBehaviour
{
	[SerializeField]
	private string m_CalendarPeriod;

	[SerializeField]
	private bool m_UserControlled;

	private bool m_InPool;

	private void Awake()
	{
		Singleton.DoOnceAfterStart(OnManagersCreated);
	}

	private void OnDestroy()
	{
		if (m_UserControlled)
		{
			Singleton.Manager<ManCalendar>.inst.UserTimePeriodChangedEvent.Unsubscribe(OnTimePeriodChanged);
		}
		else
		{
			Singleton.Manager<ManCalendar>.inst.RawTimePeriodChangedEvent.Unsubscribe(OnTimePeriodChanged);
		}
	}

	private void SetEnabledBasedOnCurrentState()
	{
		base.gameObject.SetActive(CheckTimePeriodCorrect(Singleton.Manager<ManCalendar>.inst.GetTimePeriodActive()));
	}

	private bool CheckTimePeriodCorrect(ManCalendar.TimePeriod period)
	{
		if (period != null && period.m_Name == m_CalendarPeriod)
		{
			if (m_UserControlled)
			{
				return period.m_UserEnabled;
			}
			return true;
		}
		return false;
	}

	private void OnManagersCreated()
	{
		SetEnabledBasedOnCurrentState();
		if (m_UserControlled)
		{
			Singleton.Manager<ManCalendar>.inst.UserTimePeriodChangedEvent.Subscribe(OnTimePeriodChanged);
		}
		else
		{
			Singleton.Manager<ManCalendar>.inst.RawTimePeriodChangedEvent.Subscribe(OnTimePeriodChanged);
		}
	}

	private void OnTimePeriodChanged(ManCalendar.TimePeriod timePeriod, bool enabled)
	{
		if (!m_InPool)
		{
			base.gameObject.SetActive(enabled && CheckTimePeriodCorrect(timePeriod));
		}
	}

	private void OnPool()
	{
		m_InPool = true;
	}

	private void OnSpawn()
	{
		m_InPool = false;
		SetEnabledBasedOnCurrentState();
	}

	private void OnRecycle()
	{
		base.gameObject.SetActive(value: true);
		m_InPool = true;
	}
}
