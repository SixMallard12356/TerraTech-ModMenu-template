#define UNITY_EDITOR
using UnityEngine;

public class uScript_FindNearestVendorToEncounter : uScriptLogic
{
	private Tank m_Tank;

	private Encounter m_Encounter;

	public bool Out => true;

	public bool Returned => m_Tank != null;

	public bool NotReturned => m_Tank == null;

	public Tank In(GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
			if (!m_Encounter)
			{
				string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
				d.LogError("ERROR: uScript_FindNearestVendorToEncounter - " + text);
				return null;
			}
		}
		m_Tank = ((m_Encounter != null) ? uScript_FindNearestVendor.FindNearestVendorTo(m_Encounter.Position) : null);
		return m_Tank;
	}
}
