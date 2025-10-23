#define UNITY_EDITOR
using UnityEngine;

public class uScript_GetEncounterTimeRemaining : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public float TimeRemaining(GameObject owner)
	{
		float result = 0f;
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			d.Assert(m_Encounter.QuestLog.EncounterTimer != null, "uScript_GetEncounterTimeRemaining.TimeRemaining - No timer running on this encounter! " + m_Encounter.EncounterName);
			if (m_Encounter.QuestLog.EncounterTimer != null)
			{
				result = m_Encounter.QuestLog.EncounterTimer.TimeRemaining;
			}
		}
		return result;
	}

	public float TimeRemainingPercent(GameObject owner)
	{
		float result = 0f;
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			d.Assert(m_Encounter.QuestLog.EncounterTimer != null, "uScript_GetEncounterTimeRemaining.TimeRemainingPercent - No timer running on this encounter! " + m_Encounter.EncounterName);
			if (m_Encounter.QuestLog.EncounterTimer != null)
			{
				result = m_Encounter.QuestLog.EncounterTimer.TimeRemainingPercent;
			}
		}
		return result;
	}
}
