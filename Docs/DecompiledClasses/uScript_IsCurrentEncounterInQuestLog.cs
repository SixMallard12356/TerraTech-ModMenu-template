using UnityEngine;

[FriendlyName("uScript_IsCurrentEncounterInQuestLog", "Does the current mission have an entry on the quest log?")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_IsCurrentEncounterInQuestLog : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_InQuestLog;

	public bool Out => true;

	public bool True => m_InQuestLog;

	public bool False => !m_InQuestLog;

	public void In(GameObject owner)
	{
		m_InQuestLog = false;
		if ((bool)owner)
		{
			if (!m_Encounter)
			{
				m_Encounter = owner.GetComponent<Encounter>();
			}
			if ((bool)m_Encounter)
			{
				m_InQuestLog = m_Encounter.QuestLog != null;
			}
		}
	}
}
