#define UNITY_EDITOR
using UnityEngine;

public class uScript_SetEncounterRotation : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject ownerNode, Quaternion rotation)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
			if (!m_Encounter)
			{
				string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
				d.LogError("ERROR: uScript_SetEncounterRotation - " + text);
				return;
			}
		}
		if ((bool)m_Encounter)
		{
			m_Encounter.SetRotation(rotation);
		}
	}
}
