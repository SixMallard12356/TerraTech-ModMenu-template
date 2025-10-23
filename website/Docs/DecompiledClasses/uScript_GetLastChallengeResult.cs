#define UNITY_EDITOR
public class uScript_GetLastChallengeResult : uScriptLogic
{
	public bool Success => Singleton.Manager<ManChallenge>.inst.LastChallengeResult;

	public bool Failure => !Singleton.Manager<ManChallenge>.inst.LastChallengeResult;

	public void In()
	{
		if (Singleton.Manager<ManChallenge>.inst.IsChallengeRunning)
		{
			d.LogError("uScript_GetLastChallengeResult - Checking for last challenge result, but there is a challenge currently running!");
		}
	}
}
