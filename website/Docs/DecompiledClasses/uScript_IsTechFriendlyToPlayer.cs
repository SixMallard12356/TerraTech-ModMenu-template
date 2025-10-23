using System.Collections.Generic;

[NodePath("TerraTech/Techs")]
public class uScript_IsTechFriendlyToPlayer : uScriptLogic
{
	private bool m_IsFriendly;

	private List<Tank> m_Techs = new List<Tank>();

	public bool Out => true;

	public bool True => m_IsFriendly;

	public bool False => !m_IsFriendly;

	public void In(Tank[] techsIn, ref Tank[] techsOut)
	{
		m_IsFriendly = false;
		m_Techs.Clear();
		foreach (Tank tank in techsIn)
		{
			if (tank != null && tank.IsFriendly())
			{
				m_IsFriendly = true;
				m_Techs.Add(tank);
			}
		}
		if (m_Techs.Count > 0)
		{
			techsOut = m_Techs.ToArray();
		}
	}
}
