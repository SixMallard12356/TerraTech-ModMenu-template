#define UNITY_EDITOR
using System;
using UnityEngine;

[NodePath("TerraTech/Actions/Techs")]
[FriendlyName("uScript_GetAndCheckWaveTechs", "Get techs from the encounter, spawned as part of a wave and return the state of them. Will also fill Tech Array with Tech. Some may be NUll.")]
public class uScript_GetAndCheckWaveTechs : uScriptLogic
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

	public int In(SpawnTechData spawnData, GameObject ownerNode, ref Tank[] techs)
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
			if (spawnData != null)
			{
				m_AllDead = true;
				m_AllAlive = true;
				m_AllValid = true;
				int num2 = 0;
				string prefix = $"{spawnData.UniqueName}_waveTech_";
				foreach (string item in m_Encounter.GetVisibleNamesWithPrefix(prefix))
				{
					Visible visible;
					Encounter.EncounterVisibleState visibleState = m_Encounter.GetVisibleState(item, out visible);
					d.Log(string.Format("uScript_GetAndCheckWaveTechs: checking {0} => visible {1} state = {2}", item, (visible == null) ? "NULL" : visible.name, visibleState));
					if ((bool)visible && (bool)visible.tank)
					{
						if (techs == null || techs.Length == 0)
						{
							techs = new Tank[4];
						}
						if (num2 >= techs.Length)
						{
							Array.Resize(ref techs, techs.Length * 2);
						}
						techs[num2] = visible.tank;
						num2++;
					}
					m_AllAlive = m_AllAlive && visibleState == Encounter.EncounterVisibleState.AliveAndSpawned;
					m_AllDead = m_AllDead && visibleState == Encounter.EncounterVisibleState.Killed;
					m_AllValid = m_AllValid && (visibleState == Encounter.EncounterVisibleState.AliveAndSpawned || visibleState == Encounter.EncounterVisibleState.Killed);
					if (visibleState == Encounter.EncounterVisibleState.AliveAndSpawned)
					{
						num++;
					}
				}
				for (int i = num2; i < techs.Length; i++)
				{
					techs[i] = null;
				}
			}
			else
			{
				d.LogError("uScript_GetAndCheckWaveTechs - spawnData is null for " + ownerNode.name);
			}
		}
		else
		{
			string text = (ownerNode ? ("No Encounter Component on " + ownerNode.name) : "OwnerNode is null");
			d.LogError("uScript_GetAndCheckWaveTechs - " + text);
		}
		return num;
	}
}
