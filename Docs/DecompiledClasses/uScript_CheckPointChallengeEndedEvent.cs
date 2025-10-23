using System;

public class uScript_CheckPointChallengeEndedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, CheckpointChallengeEndedEventArgs args);

	public class CheckpointChallengeEndedEventArgs : EventArgs
	{
		private CheckpointChallenge.EndReason m_EndReason;

		private float m_EndTime;

		public CheckpointChallenge.EndReason EndReason => m_EndReason;

		public float EndTime => m_EndTime;

		public CheckpointChallengeEndedEventArgs(CheckpointChallenge.EndReason endReason, float endTime)
		{
			m_EndReason = endReason;
			m_EndTime = endTime;
		}
	}

	public event uScriptEventHandler OnSuccess;

	public event uScriptEventHandler OnFail;

	public void OnEnable()
	{
		Singleton.Manager<ManChallenge>.inst.OnChallengeEnded.Subscribe(OnCheckPointEndedEventHandler);
	}

	public void OnDisable()
	{
		Singleton.Manager<ManChallenge>.inst.OnChallengeEnded.Unsubscribe(OnCheckPointEndedEventHandler);
	}

	private void OnCheckPointEndedEventHandler(Challenge.ChallengeEndData challengeEndData)
	{
		CheckpointChallenge.CheckpointChallengeEndData checkpointChallengeEndData = challengeEndData as CheckpointChallenge.CheckpointChallengeEndData;
		CheckpointChallengeEndedEventArgs args = new CheckpointChallengeEndedEventArgs(checkpointChallengeEndData.endReason, checkpointChallengeEndData.latestTime);
		if (challengeEndData.completedWithSuccess)
		{
			if (this.OnSuccess != null)
			{
				this.OnSuccess(this, args);
			}
		}
		else if (this.OnFail != null)
		{
			this.OnFail(this, args);
		}
	}
}
