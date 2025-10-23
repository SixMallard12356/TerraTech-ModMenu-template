using UnityEngine;

[NodeDeprecated(typeof(uScript_ShowQuestLog))]
public class uScript_AddQuestLog : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject owner, LocalisedString title, LocalisedString description)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			Singleton.Manager<ManQuestLog>.inst.AddLog(m_Encounter, null, restoredFromSaveData: false);
		}
	}
}
