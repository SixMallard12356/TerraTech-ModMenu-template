#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("uScript_GetDeliveryCrate", "Gets the delivery crate for this encounter")]
[NodePath("TerraTech/Spawn")]
public class uScript_GetDeliveryCrate : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_Spawned;

	public bool Out => true;

	public bool Success => m_Spawned;

	public bool Failure => !m_Spawned;

	public Crate In([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
			if (!m_Encounter)
			{
				string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
				d.LogError("ERROR: uScript_GetDeliveryCrate - " + text);
				return null;
			}
		}
		Crate crate = Singleton.Manager<ManEncounter>.inst.GetCrate(m_Encounter);
		m_Spawned = crate != null;
		return crate;
	}

	public void OnEnable()
	{
		m_Spawned = false;
	}
}
