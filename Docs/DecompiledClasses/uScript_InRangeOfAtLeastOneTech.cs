using UnityEngine;

public class uScript_InRangeOfAtLeastOneTech : uScriptLogic
{
	private bool m_InRange;

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	public void In(Tank[] techs, float range)
	{
		m_InRange = false;
		for (int i = 0; i < techs.Length; i++)
		{
			if (m_InRange)
			{
				break;
			}
			if (!(techs[i] != null))
			{
				continue;
			}
			Vector3 boundsCentreWorld = techs[i].boundsCentreWorld;
			foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
			{
				if ((allPlayerTech.boundsCentreWorld - boundsCentreWorld).magnitude <= range)
				{
					m_InRange = true;
					break;
				}
			}
		}
	}

	public void OnDisable()
	{
		m_InRange = false;
	}
}
