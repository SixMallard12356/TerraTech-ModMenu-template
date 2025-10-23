#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("uScript_GetNamedTech", "Get SINGLE named Tech from the encounter and return the state of it.")]
[NodePath("TerraTech/Actions/Techs")]
public class uScript_GetNamedTech : uScriptLogic
{
	private bool m_Valid;

	private bool m_Dead;

	private Encounter m_Encounter;

	[FriendlyName("Alive", "Tech is Ready and Alive")]
	public bool Alive
	{
		get
		{
			if (m_Valid)
			{
				return !m_Dead;
			}
			return false;
		}
	}

	[FriendlyName("Dead", "Tech is Ready and Dead")]
	public bool Dead
	{
		get
		{
			if (m_Valid)
			{
				return m_Dead;
			}
			return false;
		}
	}

	[FriendlyName("Waiting To Spawn", "Tech isn't Ready yet")]
	public bool WaitingToSpawn => !m_Valid;

	public void In([FriendlyName("Name", "Name of Tech to find in encounter.")] string name, [FriendlyName("Owner Node", "Owner Node of Encounter.")] GameObject ownerNode, ref Tank tech)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (!name.NullOrEmpty())
			{
				m_Valid = true;
				m_Dead = true;
				EncounterVisibleData visible = m_Encounter.GetVisible(name);
				tech = null;
				if (visible != null)
				{
					if (visible.m_VisibleId != -2)
					{
						TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.m_VisibleId);
						if (trackedVisible.visible == null)
						{
							m_Valid = false;
						}
						else
						{
							tech = trackedVisible.visible.tank;
						}
						m_Dead = false;
					}
				}
				else
				{
					m_Valid = false;
				}
			}
			else
			{
				d.LogError("uScript_GetNamedTech - name is either null or length zero for " + ownerNode.name);
			}
		}
		else
		{
			string text = (ownerNode ? ("No Encounter Component on " + ownerNode.name) : "OwnerNode is null");
			d.LogError("uScript_GetNamedTech - " + text);
		}
	}
}
