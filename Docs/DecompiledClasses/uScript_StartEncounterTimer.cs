#define UNITY_EDITOR
using UnityEngine;

public class uScript_StartEncounterTimer : uScriptLogic
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
			d.Assert(m_Encounter.EncounterDetails.IsTimed && m_Encounter.EncounterDetails.EncounterTime > 0f, "uScript_StartEncounterTimer - Starting a timer on a mission that is not flagged as 'Timed'");
			m_Encounter.StartEncounterTimer(m_Encounter.EncounterDetails.EncounterTime);
		}
	}
}
