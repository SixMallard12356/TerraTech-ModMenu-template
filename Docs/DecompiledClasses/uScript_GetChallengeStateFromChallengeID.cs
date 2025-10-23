#define UNITY_EDITOR
public class uScript_GetChallengeStateFromChallengeID : uScriptLogic
{
	private bool m_ChallengeWasInProgress;

	private bool m_ChallengeIsInProgress;

	public bool Out => true;

	public bool NotRunning => !m_ChallengeIsInProgress;

	public bool JustStarted
	{
		get
		{
			if (m_ChallengeIsInProgress)
			{
				return !m_ChallengeWasInProgress;
			}
			return false;
		}
	}

	public bool InProgress => m_ChallengeIsInProgress;

	public bool JustEnded
	{
		get
		{
			if (m_ChallengeWasInProgress)
			{
				return !m_ChallengeIsInProgress;
			}
			return false;
		}
	}

	public void In(string uniqueChallengeID)
	{
		if (!uniqueChallengeID.NullOrEmpty())
		{
			m_ChallengeWasInProgress = m_ChallengeIsInProgress;
			m_ChallengeIsInProgress = Singleton.Manager<ManChallenge>.inst.IsChallengeInProgress(uniqueChallengeID);
		}
		else
		{
			d.LogError("uScript_GetChallengeStateFromChallengeID - Invalid uniqueChallengeID passed in!");
		}
	}

	public void OnDisable()
	{
		m_ChallengeWasInProgress = false;
		m_ChallengeIsInProgress = false;
	}
}
