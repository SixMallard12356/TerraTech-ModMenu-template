using UnityEngine;

public class uScript_ClearChallengeStarterChallengeData : uScriptLogic
{
	private ChallengeStarter m_ChallengeStarter;

	public bool Out => true;

	public void In(GameObject targetChallengeStarterObject)
	{
		if (m_ChallengeStarter == null)
		{
			m_ChallengeStarter = targetChallengeStarterObject.GetComponent<ChallengeStarter>();
		}
		if (m_ChallengeStarter != null)
		{
			m_ChallengeStarter.ClearChallengeData();
		}
	}

	public void OnDisable()
	{
		m_ChallengeStarter = null;
	}
}
