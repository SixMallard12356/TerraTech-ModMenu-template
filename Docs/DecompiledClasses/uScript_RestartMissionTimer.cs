using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("Restart Mission Timer", "Restart the custom mission timer for an encounter")]
public class uScript_RestartMissionTimer : uScriptLogic
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
			Singleton.Manager<ManQuestLog>.inst.RestartMissionTimer(m_Encounter.EncounterDef);
		}
	}
}
