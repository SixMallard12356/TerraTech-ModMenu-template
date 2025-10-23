#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[FriendlyName("Distance/Is Player in Trigger Area [Smart]")]
public class uScript_IsPlayerInTriggerSmart : uScriptLogic
{
	private bool m_AllInside;

	private bool m_Changed;

	private bool m_AnyInside;

	private Encounter m_Encounter;

	public bool Out => true;

	public bool FirstEntered
	{
		get
		{
			if (m_AnyInside)
			{
				return m_Changed;
			}
			return false;
		}
	}

	public bool AllInside => m_AllInside;

	public bool LastExited
	{
		get
		{
			if (!m_AnyInside)
			{
				return m_Changed;
			}
			return false;
		}
	}

	public bool AllOutside => !m_AllInside;

	public bool SomeInside => m_AnyInside;

	public bool SomeOutside => !m_AllInside;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(string innerTriggerArea, string outerTriggerArea, ref bool inside)
	{
		bool flag = inside;
		if (m_Encounter.IsNotNull())
		{
			string text = (inside ? outerTriggerArea : innerTriggerArea);
			if (m_Encounter.GetTriggerTester(text, out var tester))
			{
				bool flag2 = false;
				m_AnyInside = false;
				inside = false;
				List<Tank> allPlayerTechs = Singleton.Manager<ManNetwork>.inst.GetAllPlayerTechs();
				foreach (Tank item in allPlayerTechs)
				{
					float radiusXZWorld = item.blockBounds.GetRadiusXZWorld(item.trans);
					if (tester.Test(item.boundsCentreWorld, radiusXZWorld))
					{
						m_AnyInside = true;
						inside = true;
					}
					else
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					int num = ((!Singleton.Manager<ManNetwork>.inst.IsMultiplayer()) ? 1 : Singleton.Manager<ManNetwork>.inst.GetNumPlayers());
					flag2 = allPlayerTechs.Count < num;
				}
				m_AllInside = m_AnyInside && !flag2;
			}
			else
			{
				d.LogWarning("uScript_IsPlayerInTrigger - unable to find trigger called " + text);
			}
		}
		m_Changed = inside != flag;
	}
}
