using UnityEngine;

public class uScript_TechInRangeOfPosition : uScriptLogic
{
	private bool m_InRange;

	public bool Out => true;

	public bool True => m_InRange;

	public bool False => !m_InRange;

	public void In(Tank tech, Vector3 position, float range)
	{
		if (tech != null)
		{
			m_InRange = (position - tech.boundsCentreWorld).magnitude <= range;
		}
	}
}
