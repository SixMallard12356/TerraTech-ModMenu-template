#define UNITY_EDITOR
using UnityEngine;

public class uScript_GetChallengeIDFromChallengeStarter : uScriptLogic
{
	private ChallengeStarter m_ChallengeStarter;

	private string m_UniqueChallengeID;

	public bool Out => true;

	public string In(GameObject challengeStarterObject)
	{
		if (m_UniqueChallengeID == null)
		{
			if (m_ChallengeStarter == null && challengeStarterObject != null)
			{
				m_ChallengeStarter = challengeStarterObject.GetComponent<ChallengeStarter>();
			}
			if (m_ChallengeStarter != null)
			{
				m_UniqueChallengeID = m_ChallengeStarter.GetUniqueChallengeID();
			}
			else
			{
				d.LogError((challengeStarterObject != null) ? $"uScript_GetChallengeIDFromChallengeStarter - Object passed in ({challengeStarterObject.name}) did not contain a Component of type ChallengeStarter!" : "uScript_GetChallengeIDFromChallengeStarter - Passed in object was null!");
			}
		}
		return m_UniqueChallengeID;
	}

	public void OnDisable()
	{
		m_ChallengeStarter = null;
		m_UniqueChallengeID = null;
	}
}
