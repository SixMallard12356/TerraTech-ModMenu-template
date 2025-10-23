#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("uScript_SpawnDeliveryCrate", "Spawns a delivery crate which provides a reward when opened")]
[NodePath("TerraTech/Spawn")]
public class uScript_SpawnDeliveryCrate : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In([FriendlyName("Position Name", "Position defined within the encounter of where to try spawning the crate")] string positionName, [FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode, [DefaultValue(true)] bool visibleOnRadar)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
			if (!m_Encounter)
			{
				string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
				d.LogError("ERROR: uScript_SpawnDeliveryCrate - " + text);
				return;
			}
		}
		Vector3 position = m_Encounter.GetPosition(positionName);
		Quaternion rotation = m_Encounter.GetRotation(positionName);
		Singleton.Manager<ManEncounter>.inst.TrySpawnRewardCrate(m_Encounter, position, rotation, visibleOnRadar);
	}
}
