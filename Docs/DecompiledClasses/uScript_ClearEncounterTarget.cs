using UnityEngine;

public class uScript_ClearEncounterTarget : uScriptLogic
{
	private Encounter m_DataComponent;

	public bool Out => true;

	public void In(GameObject owner)
	{
		if ((bool)owner && !m_DataComponent)
		{
			m_DataComponent = owner.GetComponent<Encounter>();
		}
		if ((bool)m_DataComponent)
		{
			m_DataComponent.ClearTrackedWaypoint();
		}
	}
}
