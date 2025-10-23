using UnityEngine;

public class uScript_Wait : uScriptLogic
{
	private float m_TimeElapsed;

	private float m_TimeToWait;

	private bool m_FinishedWaiting;

	private bool m_Repeated;

	public bool Waited => m_FinishedWaiting;

	public void In(float seconds, bool repeat)
	{
		m_TimeToWait = seconds;
		if (repeat && !m_Repeated)
		{
			m_TimeElapsed = 0f;
			m_Repeated = true;
			m_FinishedWaiting = false;
		}
		if (m_TimeElapsed >= m_TimeToWait && !m_FinishedWaiting)
		{
			m_FinishedWaiting = true;
			m_Repeated = false;
		}
		else
		{
			m_TimeElapsed += Time.deltaTime;
		}
	}

	public void OnDisable()
	{
		m_TimeElapsed = 0f;
		m_FinishedWaiting = false;
		m_Repeated = false;
	}
}
