#define UNITY_EDITOR
using UnityEngine;

public class uScript_GetChallengeStartTransform : uScriptLogic
{
	private Transform m_ChallengeStartTransform;

	public bool Out => true;

	public Transform In(GameObject challengeStarterObject)
	{
		if (m_ChallengeStartTransform == null)
		{
			ChallengeStarter component = challengeStarterObject.GetComponent<ChallengeStarter>();
			if (component != null)
			{
				m_ChallengeStartTransform = component.ChallengeStartTransform;
			}
			else
			{
				d.LogError("uScript_GetChallengeStartTransform - No ChallengeStarter component found on the passed in challengeStarterObject!");
			}
		}
		return m_ChallengeStartTransform;
	}

	public void OnDisable()
	{
		m_ChallengeStartTransform = null;
	}
}
