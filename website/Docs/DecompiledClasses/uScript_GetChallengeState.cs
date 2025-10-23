public class uScript_GetChallengeState : uScriptLogic
{
	public bool Running => Singleton.Manager<ManChallenge>.inst.IsChallengeRunning;

	public bool NotRunning => !Running;

	public bool Success
	{
		get
		{
			if (!Running)
			{
				return Singleton.Manager<ManChallenge>.inst.LastChallengeResult;
			}
			return false;
		}
	}

	public bool Failure
	{
		get
		{
			if (!Running)
			{
				return !Singleton.Manager<ManChallenge>.inst.LastChallengeResult;
			}
			return false;
		}
	}

	public void In()
	{
	}
}
