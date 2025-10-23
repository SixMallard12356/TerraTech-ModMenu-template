using UnityEngine;

[FriendlyName("Get Mission Timer Display Time", "Gets the current time (counting up or down) from the custom mission timer for an encounter")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_GetMissionTimerDisplayTime : uScriptLogic
{
	private float m_DisplayTime;

	private Encounter m_Encounter;

	public bool Out => true;

	public float In(GameObject owner)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			m_DisplayTime = Singleton.Manager<ManQuestLog>.inst.GetMissionTimerDisplayTime(m_Encounter.EncounterDef);
			return m_DisplayTime;
		}
		return 0f;
	}
}
