#define UNITY_EDITOR
using System;
using UnityEngine;

[NodePath("TerraTech/Actions/Techs")]
[FriendlyName("uScript_GetAndCheckTechs", "Get techs from the encounter and return the state of them. Will also fill Tech Array with Tech. Some may be NUll.")]
public class uScript_GetAndCheckTechs : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_AllDead = true;

	private bool m_AllAlive;

	private bool m_AllValid;

	[FriendlyName("All Alive", "Are all the techs Ready and Alive")]
	public bool AllAlive => m_AllAlive;

	[FriendlyName("Some Alive", "Are all the techs Ready and Some Alive")]
	public bool SomeAlive
	{
		get
		{
			if (m_AllValid && !m_AllAlive)
			{
				return !m_AllDead;
			}
			return false;
		}
	}

	[FriendlyName("All Dead", "Are all the techs Ready and Dead")]
	public bool AllDead => m_AllDead;

	[FriendlyName("Waiting To Spawn", "Some of the techs aren't Ready yet")]
	public bool WaitingToSpawn => !m_AllValid;

	public int In(SpawnTechData[] techData, GameObject ownerNode, ref Tank[] techs)
	{
		int num = 0;
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		m_AllDead = false;
		m_AllAlive = false;
		m_AllValid = false;
		if ((bool)m_Encounter)
		{
			if (techData != null && techData.Length != 0)
			{
				if (techs == null)
				{
					techs = new Tank[techData.Length];
				}
				else if (techs.Length != techData.Length)
				{
					Array.Resize(ref techs, techData.Length);
				}
				m_AllDead = true;
				m_AllAlive = true;
				m_AllValid = true;
				for (int i = 0; i < techData.Length; i++)
				{
					if (techData[i].CanSpawnOnCurrentSKU())
					{
						Visible visible;
						Encounter.EncounterVisibleState visibleState = m_Encounter.GetVisibleState(techData[i].UniqueName, out visible);
						techs[i] = (visible ? visible.tank : null);
						if (techs[i] != null && techs[i].IsBeingRecycled())
						{
							d.LogWarningFormat("uScript_GetAndCheckTechs returned a tech that's being recycled '{0}' ", (techs[i] == null) ? "" : techs[i].name);
						}
						m_AllAlive = m_AllAlive && visibleState == Encounter.EncounterVisibleState.AliveAndSpawned;
						m_AllDead = m_AllDead && visibleState == Encounter.EncounterVisibleState.Killed;
						m_AllValid = m_AllValid && (visibleState == Encounter.EncounterVisibleState.AliveAndSpawned || visibleState == Encounter.EncounterVisibleState.Killed);
						if (visibleState == Encounter.EncounterVisibleState.AliveAndSpawned)
						{
							num++;
						}
					}
				}
				m_AllAlive = m_AllAlive && !m_AllDead;
			}
			else
			{
				d.LogError("uScript_GetAndCheckTechs - TechData is either null or length zero for " + ownerNode.name);
			}
		}
		else
		{
			string text = (ownerNode ? ("No Encounter Component on " + ownerNode.name) : "OwnerNode is null");
			d.LogError("uScript_GetAndCheckTechs - " + text);
		}
		return num;
	}
}
