#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Distance/Is Any Tech in Trigger Area")]
public class uScript_IsTechInTrigger : uScriptLogic
{
	private bool m_InRange;

	private Encounter m_Encounter;

	private List<Tank> m_TechsInRange = new List<Tank>();

	public bool Out => true;

	public bool InRange => m_InRange;

	public bool OutOfRange => !m_InRange;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(string triggerAreaName, ref Tank[] techs)
	{
		m_TechsInRange.Clear();
		m_InRange = false;
		if (m_Encounter.IsNotNull())
		{
			if (m_Encounter.GetTriggerTester(triggerAreaName, out var tester))
			{
				foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
				{
					float radiusXZWorld = item.blockBounds.GetRadiusXZWorld(item.trans);
					if (tester.Test(item.boundsCentreWorld, radiusXZWorld))
					{
						m_InRange = true;
						m_TechsInRange.Add(item);
					}
				}
			}
			else
			{
				d.LogWarning("uScript_IsTechInTrigger - unable to find trigger called " + triggerAreaName);
			}
		}
		if (m_TechsInRange.Count > 0)
		{
			techs = m_TechsInRange.ToArray();
		}
	}
}
