using UnityEngine;

public class uScript_SetEncounterTargetPosition : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public override void SetParent(GameObject parent)
	{
		base.SetParent(parent);
		m_Encounter = parent.GetComponent<Encounter>();
	}

	public void In(string positionName)
	{
		if (m_Encounter.IsNotNull())
		{
			Vector3 position = m_Encounter.GetPosition(positionName);
			m_Encounter.MoveDefaultWaypoint(position);
			m_Encounter.SetTrackedWaypoint(null);
		}
	}
}
