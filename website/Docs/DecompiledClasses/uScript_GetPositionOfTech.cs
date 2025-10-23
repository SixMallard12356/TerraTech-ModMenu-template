using UnityEngine;

public class uScript_GetPositionOfTech : uScriptLogic
{
	private bool m_TechValid;

	public bool Out => true;

	public bool TechValid => m_TechValid;

	public bool TechNull => !m_TechValid;

	public Vector3 In(Tank tech)
	{
		Vector3 result = Vector3.zero;
		m_TechValid = tech != null;
		if (m_TechValid)
		{
			result = tech.visible.centrePosition;
		}
		return result;
	}

	public void OnDisable()
	{
		m_TechValid = false;
	}
}
