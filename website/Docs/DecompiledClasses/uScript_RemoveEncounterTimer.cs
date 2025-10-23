#define UNITY_EDITOR
using UnityEngine;

public class uScript_RemoveEncounterTimer : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject owner)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			d.Assert(m_Encounter.EncounterDetails.IsTimed, "uScript_RemoveEncounterTimer - Removing a timer on a mission that is not flagged as 'Timed'!");
			m_Encounter.RemoveEncounterTimer();
		}
	}
}
