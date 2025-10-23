#define UNITY_EDITOR
using UnityEngine;

public class uScript_ExtendEncounterTimer : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void ExtendTimer(GameObject owner, float timeToAdd)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if (!m_Encounter)
		{
			return;
		}
		d.Assert(m_Encounter.QuestLog.EncounterTimer != null, "uScript_ExtendEncounterTimer - No timer running on this encounter! " + m_Encounter.EncounterName);
		if (m_Encounter.QuestLog.EncounterTimer != null)
		{
			m_Encounter.QuestLog.EncounterTimer.AddTimeToTimer(timeToAdd);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManEncounter>.inst.ServerOnQuestLogUpdated(m_Encounter.EncounterDef);
			}
		}
	}
}
