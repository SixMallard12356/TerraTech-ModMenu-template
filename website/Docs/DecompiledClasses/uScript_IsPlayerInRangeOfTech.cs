using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Distance/Is player in range of tech")]
public class uScript_IsPlayerInRangeOfTech : uScriptLogic
{
	private bool m_InRange;

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	private static bool CheckRange(Vector3 playerPosition, Tank tech, float range, Tank[] techs)
	{
		if ((bool)tech)
		{
			return (playerPosition - tech.boundsCentreWorld).SetY(0f).magnitude <= range;
		}
		if (techs.Length != 0)
		{
			foreach (Tank tank in techs)
			{
				if ((bool)tank && (playerPosition - tank.trans.position).SetY(0f).magnitude <= range)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void In(Tank tech, float range, Tank[] techs = null)
	{
		List<Tank> allPlayerTechs = Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs();
		m_InRange = false;
		if (allPlayerTechs.Count != 0)
		{
			foreach (Tank item in allPlayerTechs)
			{
				if (CheckRange(item.boundsCentreWorld, tech, range, techs))
				{
					m_InRange = true;
					break;
				}
			}
			return;
		}
		if ((bool)Singleton.Manager<ManPointer>.inst.DraggingItem && Singleton.Manager<ManPointer>.inst.DraggingItem.type == ObjectTypes.Block && Singleton.Manager<ManPointer>.inst.DraggingItem.block.IsController)
		{
			Vector3 centrePosition = Singleton.Manager<ManPointer>.inst.DraggingItem.centrePosition;
			m_InRange = CheckRange(centrePosition, tech, range, techs);
		}
	}

	public void OnDisable()
	{
		m_InRange = false;
	}
}
