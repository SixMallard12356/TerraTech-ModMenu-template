#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("uScript_UnlockDeliveryCrate", "Causes a spawned delivery crate to unlock")]
[NodePath("TerraTech")]
public class uScript_UnlockDeliveryCrate : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
			if (!m_Encounter)
			{
				string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
				d.LogError("ERROR: uScript_UnlockDeliveryCrate - " + text);
				return;
			}
		}
		Singleton.Manager<ManEncounter>.inst.UnlockRewardCrate(m_Encounter);
	}
}
