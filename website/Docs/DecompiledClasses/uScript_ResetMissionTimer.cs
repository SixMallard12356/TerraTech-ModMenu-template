using UnityEngine;

[NodePath("TerraTech/Actions/Encounters")]
[FriendlyName("Reset Mission Timer", "Resets the custom mission timer for an encounter")]
public class uScript_ResetMissionTimer : uScriptLogic
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
			Singleton.Manager<ManQuestLog>.inst.ResetMissionTimer(m_Encounter.EncounterDef);
		}
	}
}
