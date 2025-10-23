using UnityEngine;

[FriendlyName("Distance/Player In Range")]
public class uScript_PlayerInRange : uScriptLogic
{
	private bool m_InRange;

	public bool True => m_InRange;

	public bool False => !m_InRange;

	public bool Out => true;

	public void In(Vector3 position, float range)
	{
		m_InRange = false;
		foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
		{
			if ((position - allPlayerTech.boundsCentreWorld).SetY(0f).magnitude <= range)
			{
				m_InRange = true;
				break;
			}
		}
	}
}
