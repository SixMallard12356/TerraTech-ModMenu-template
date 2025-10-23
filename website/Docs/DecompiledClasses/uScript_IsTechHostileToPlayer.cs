using System.Collections.Generic;

[NodePath("TerraTech/Techs")]
public class uScript_IsTechHostileToPlayer : uScriptLogic
{
	private bool m_IsHostile;

	private List<Tank> m_Techs = new List<Tank>();

	public bool Out => true;

	public bool True => m_IsHostile;

	public bool False => !m_IsHostile;

	public void In(Tank[] techsIn, ref Tank[] techsOut)
	{
		m_IsHostile = false;
		m_Techs.Clear();
		foreach (Tank tank in techsIn)
		{
			if (tank != null && tank.IsEnemy())
			{
				m_IsHostile = true;
				m_Techs.Add(tank);
			}
		}
		if (m_Techs.Count > 0)
		{
			techsOut = m_Techs.ToArray();
		}
	}
}
