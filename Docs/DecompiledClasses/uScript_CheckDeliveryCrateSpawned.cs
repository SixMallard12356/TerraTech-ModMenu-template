#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Spawn")]
[FriendlyName("uScript_CheckDeliveryCrateSpawned", "Checks to see if a delivery crate is spawned")]
public class uScript_CheckDeliveryCrateSpawned : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_Spawned;

	public bool Out => true;

	public bool Yes => m_Spawned;

	public bool No => !m_Spawned;

	public void In([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
			if (!m_Encounter)
			{
				string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
				d.LogError("ERROR: uScript_CheckDeliveryCrateSpawned - " + text);
				return;
			}
		}
		m_Spawned = Singleton.Manager<ManEncounter>.inst.IsCrateSpawned(m_Encounter);
	}

	public void OnEnable()
	{
		m_Spawned = false;
	}
}
