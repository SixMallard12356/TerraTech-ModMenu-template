using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("Start Mission Timer", "Starts the custom mission timer for an encounter")]
public class uScript_StartMissionTimer : uScriptLogic
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
			Singleton.Manager<ManQuestLog>.inst.StartMissionTimer(m_Encounter.EncounterDef, startTime);
		}
	}
}
