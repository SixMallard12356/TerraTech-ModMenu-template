using UnityEngine;

[FriendlyName("Show Mission Timer UI")]
[NodePath("TerraTech/Actions/Encounters")]
public class uScript_ShowMissionTimerUI : uScriptLogic
{
	private Encounter m_Encounter;

	private UICheckpointChallengeHUD m_TimerHUD;

	public bool Out => true;

	public void In(GameObject owner, bool showBestTime = false)
	{
		if ((bool)owner && !m_Encounter)
		{
			m_Encounter = owner.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			Singleton.Manager<ManQuestLog>.inst.ShowMissionTimerUI(m_Encounter.EncounterDef);
		}
	}
}
