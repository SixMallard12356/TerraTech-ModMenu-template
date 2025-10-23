public class uScript_CompareCheckpointChallengeEndReason : uScriptLogic
{
	private bool m_EqualTo;

	[FriendlyName("(Equal To)   ==")]
	public bool EqualTo => m_EqualTo;

	[FriendlyName("(Not Equal To)  !=")]
	public bool NotEqualTo => !m_EqualTo;

	public void In([FriendlyName("Result", "Result of the challenge")] CheckpointChallenge.EndReason result, [FriendlyName("Comparison", "Is the result equal to this value")] CheckpointChallenge.EndReason expected)
	{
		m_EqualTo = false;
		if (result == expected)
		{
			m_EqualTo = true;
		}
	}
}
