using UnityEngine;

public class uScript_InitChallengeStarterWithEncounterChallengeData : uScriptLogic
{
	private Encounter m_Encounter;

	private ChallengeStarter m_ChallengeStarter;

	public bool Out => true;

	public void In(GameObject owner, GameObject targetChallengeStarterObject)
	{
		if (m_Encounter == null)
		{
			m_Encounter = owner.GetComponent<Encounter>();
			m_ChallengeStarter = targetChallengeStarterObject.GetComponent<ChallengeStarter>();
		}
		if (m_Encounter != null && m_ChallengeStarter != null)
		{
			m_ChallengeStarter.SetChallengeData(m_Encounter.ChallengeData);
		}
	}

	public void OnDisable()
	{
		m_Encounter = null;
		m_ChallengeStarter = null;
	}
}
