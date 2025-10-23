using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("Reset Elapsed Time on Mission Timer", "Resets the elapsed time on the custom mission timer for an encounter")]
public class uScript_ResetMissionTimerTimeElapsed : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject owner, float startTime = 0f)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			Singleton.Manager<ManQuestLog>.inst.ResetMissionTimerTimeElapsed(m_Encounter.EncounterDef, startTime);
		}
	}
}
