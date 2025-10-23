using UnityEngine;

public class uScript_GetNearestTech : uScriptLogic
{
	private bool m_Found;

	private Tank m_Nearest;

	private int m_Team;

	public bool Found => m_Found;

	public bool NotFound => !m_Found;

	public Tank In(int team, bool recheck)
	{
		if ((!m_Found || recheck) && (bool)Singleton.playerTank)
		{
			m_Found = false;
			m_Nearest = null;
			float num = float.MaxValue;
			Vector3 position = Singleton.playerTank.trans.position;
			m_Team = team;
			foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsEnemy))
			{
				float magnitude = (item.trans.position - position).magnitude;
				if (magnitude < num)
				{
					m_Nearest = item;
					num = magnitude;
				}
			}
			if (m_Nearest != null)
			{
				m_Found = true;
			}
		}
		return m_Nearest;
	}

	private bool TechIsSameTeamNotPlayer(Tank tech)
	{
		if (tech.Team == m_Team)
		{
			return tech != Singleton.playerTank;
		}
		return false;
	}

	private bool TechIsEnemy(Tank tech)
	{
		return tech.IsEnemy(0);
	}

	public void OnDisable()
	{
		m_Found = false;
		m_Nearest = null;
	}
}
