#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Get Techs in Trigger Area")]
public class uScript_GetTechsInTrigger : uScriptLogic
{
	private Encounter m_Encounter;

	private List<Tank> PrevInside = new List<Tank>();

	public bool Out => true;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(string triggerAreaName, out Tank[] Entered, out Tank[] Inside, out Tank[] Exited)
	{
		List<Tank> list = new List<Tank>();
		List<Tank> list2 = new List<Tank>();
		List<Tank> list3 = new List<Tank>();
		if (m_Encounter.IsNotNull())
		{
			if (m_Encounter.GetTriggerTester(triggerAreaName, out var tester))
			{
				foreach (Tank allPlayerTech in Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs())
				{
					float radiusXZWorld = allPlayerTech.blockBounds.GetRadiusXZWorld(allPlayerTech.trans);
					if (tester.Test(allPlayerTech.boundsCentreWorld, radiusXZWorld))
					{
						list2.Add(allPlayerTech);
						if (!PrevInside.Contains(allPlayerTech))
						{
							list.Add(allPlayerTech);
						}
					}
					else if (PrevInside.Contains(allPlayerTech))
					{
						list3.Add(allPlayerTech);
					}
				}
			}
			else
			{
				d.LogWarning("uScript_GetPlayersInTrigger - unable to find trigger called " + triggerAreaName);
			}
		}
		Entered = list.ToArray();
		Inside = list2.ToArray();
		Exited = list3.ToArray();
		PrevInside = list2;
	}
}
