using UnityEngine;

[FriendlyName("Distance/Is player in range of block")]
public class uScript_IsPlayerInRangeOfBlock : uScriptLogic
{
	private bool m_InRange;

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	public void In(TankBlock block, float range)
	{
		m_InRange = false;
		if (!block)
		{
			return;
		}
		Vector3 centrePosition = block.visible.centrePosition;
		foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
		{
			if ((allPlayerTech.boundsCentreWorld - centrePosition).SetY(0f).magnitude <= range)
			{
				m_InRange = true;
				break;
			}
		}
	}

	public void OnDisable()
	{
		m_InRange = false;
	}
}
