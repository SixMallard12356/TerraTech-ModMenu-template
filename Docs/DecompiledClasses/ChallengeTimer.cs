public class ChallengeTimer : Timer
{
	public void HandleMessage(ref ChallengeTimerMessage msg)
	{
		base.TimeElapsed = msg.m_TimeElapsed;
		if (msg.m_IsRunning != m_IsRunning)
		{
			if (msg.m_IsRunning)
			{
				Start(msg.m_StartTimeRemaining);
			}
			else
			{
				Stop();
			}
		}
	}

	public void FillMessage(ref ChallengeTimerMessage msg)
	{
		msg.m_IsRunning = m_IsRunning;
		msg.m_StartTimeRemaining = m_StartTimeRemaining;
		msg.m_TimeElapsed = m_TimeElapsed;
	}
}
