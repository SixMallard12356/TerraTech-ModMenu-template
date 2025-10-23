#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("Distance/Is player in range of visible")]
public class uScript_IsPlayerInRangeOfVisible : uScriptLogic
{
	private bool m_InRange;

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	public void In(object visibleObject, float range)
	{
		m_InRange = false;
		if (visibleObject != null)
		{
			Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(visibleObject);
			if (!visibleFromObject)
			{
				return;
			}
			Vector3 centrePosition = visibleFromObject.centrePosition;
			{
				foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
				{
					if ((allPlayerTech.boundsCentreWorld - centrePosition).ToVector2XZ().magnitude <= range)
					{
						m_InRange = true;
						break;
					}
				}
				return;
			}
		}
		d.LogError("uScript_IsPlayerInRangeOfVisible - System.Object is null");
	}

	public void OnDisable()
	{
		m_InRange = false;
	}
}
