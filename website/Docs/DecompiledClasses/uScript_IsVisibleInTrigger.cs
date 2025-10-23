#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("Distance/Is Visible in Trigger Area")]
public class uScript_IsVisibleInTrigger : uScriptLogic
{
	private bool m_InRange;

	private Encounter m_Encounter;

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(object visibleObject, string triggerAreaName)
	{
		m_InRange = false;
		if (visibleObject != null)
		{
			Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(visibleObject);
			if ((bool)visibleFromObject && m_Encounter.IsNotNull())
			{
				Vector3 centrePosition = visibleFromObject.centrePosition;
				float radius = (visibleFromObject.tank ? visibleFromObject.tank.blockBounds.GetRadiusXZWorld(visibleFromObject.trans) : visibleFromObject.Radius);
				if (m_Encounter.GetTriggerTester(triggerAreaName, out var tester))
				{
					m_InRange = tester.Test(centrePosition, radius);
				}
				else
				{
					d.LogWarning("uScript_IsVisibleInTrigger - unable to find trigger called " + triggerAreaName);
				}
			}
		}
		else
		{
			d.LogError("uScript_IsVisibleInTrigger - System.Object is null");
		}
	}
}
