#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Techs")]
[FriendlyName("uScript_SpawnTechsFromData", "Spawn array of Techs from SpawnTechData and add to the encounter")]
public class uScript_SpawnTechsFromData : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void InitialSpawn([FriendlyName("Tech Data", "The tech data to spawn from")] SpawnTechData[] spawnData, [FriendlyName("Owner", "Owner Node of Encounter")] GameObject ownerNode, [DefaultValue(0.3f)][FriendlyName("Interval", "Delay between spawning each tech")] float delayBetweenSpawns = 0.3f, [FriendlyName("Respawns?", "Whether we allow respawning of dead techs")][DefaultValue(false)] bool allowResurrection = false)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			if (spawnData != null && spawnData.Length != 0)
			{
				for (int i = 0; i < spawnData.Length; i++)
				{
					spawnData[i].SpawnTechInEncounter(m_Encounter, (float)i * delayBetweenSpawns, null, allowResurrection);
				}
			}
			else
			{
				d.LogError("ERROR: uScript_SpawnTechsFromData - tech data is null or empty for " + m_Encounter.name);
			}
		}
		else
		{
			string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
			d.LogError("ERROR: uScript_SpawnTechsFromData - " + text);
		}
	}
}
