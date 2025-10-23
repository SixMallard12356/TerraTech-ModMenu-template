#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Distance/Is Player in Trigger Area")]
public class uScript_IsPlayerInTrigger : uScriptLogic
{
	private bool m_AnyInRange;

	private bool m_AllInRange;

	private Encounter m_Encounter;

	public bool Out => true;

	[FriendlyName("AllIn")]
	public bool AllInRange => m_AllInRange;

	[FriendlyName("SomeIn")]
	public bool InRange => m_AnyInRange;

	[FriendlyName("SomeOut")]
	public bool SomeOutOfRange => !m_AllInRange;

	[FriendlyName("AllOut")]
	public bool OutOfRange => !m_AnyInRange;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(string triggerAreaName)
	{
		m_AnyInRange = false;
		m_AllInRange = false;
		if (!m_Encounter.IsNotNull())
		{
			return;
		}
		if (m_Encounter.GetTriggerTester(triggerAreaName, out var tester))
		{
			bool flag = false;
			List<Tank> allPlayerTechs = Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs();
			foreach (Tank item in allPlayerTechs)
			{
				float radiusXZWorld = item.blockBounds.GetRadiusXZWorld(item.trans);
				if (tester.Test(item.boundsCentreWorld, radiusXZWorld))
				{
					m_AnyInRange = true;
				}
				else
				{
					flag = true;
				}
			}
			if (!flag)
			{
				int num = ((!Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? 1 : Singleton.Manager<ManNetwork>.inst.GetNumPlayers());
				flag = allPlayerTechs.Count < num;
			}
			m_AllInRange = m_AnyInRange && !flag;
		}
		else
		{
			d.LogWarning("uScript_IsPlayerInTrigger - unable to find trigger called " + triggerAreaName);
		}
	}
}
