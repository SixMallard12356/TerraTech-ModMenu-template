using UnityEngine;

[FriendlyName("Hide Mission Timer UI")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_HideMissionTimerUI : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject owner = null)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			Singleton.Manager<ManQuestLog>.inst.HideMissionTimerUI(m_Encounter.EncounterDef);
		}
	}
}
