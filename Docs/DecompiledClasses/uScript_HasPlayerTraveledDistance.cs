using UnityEngine;

[FriendlyName("Has player traveled distance")]
public class uScript_HasPlayerTraveledDistance : uScriptLogic
{
	private bool m_True;

	private bool m_Started;

	private Vector3 m_StartPos;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(float distance)
	{
		if ((bool)Singleton.playerTank)
		{
			if (!m_Started)
			{
				m_StartPos = Singleton.playerTank.boundsCentreWorld;
				m_Started = true;
			}
			if ((m_StartPos - Singleton.playerTank.boundsCentreWorld).SetY(0f).magnitude > distance)
			{
				m_True = true;
			}
		}
	}

	public void OnDisable()
	{
		m_True = false;
		m_Started = false;
	}
}
