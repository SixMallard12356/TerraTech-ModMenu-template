using UnityEngine;

[FriendlyName("Stop Mission Timer", "Stops the custom mission timer for an encounter")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_StopMissionTimer : uScriptLogic
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
			Singleton.Manager<ManQuestLog>.inst.StopMissionTimer(m_Encounter.EncounterDef);
		}
	}
}
