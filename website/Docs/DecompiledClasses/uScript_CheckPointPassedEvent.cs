using System;
using UnityEngine;

public class uScript_CheckPointPassedEvent : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, CheckpointPassedEventArgs args);

	public class CheckpointPassedEventArgs : EventArgs
	{
		private int m_CheckpointIndex;

		public int CheckpointIndex => m_CheckpointIndex;

		public CheckpointPassedEventArgs(int checkpointIndex)
		{
			m_CheckpointIndex = checkpointIndex;
		}
	}

	public event uScriptEventHandler OnCheckPointPassed;

	public void OnEnable()
	{
		CheckpointChallenge.OnCheckpointPassed.Subscribe(OnCheckPointPassedHandler);
	}

	public void OnDisable()
	{
		CheckpointChallenge.OnCheckpointPassed.Unsubscribe(OnCheckPointPassedHandler);
	}

	private void OnCheckPointPassedHandler(int index, Vector3 position, Quaternion rotation)
	{
		if (this.OnCheckPointPassed != null)
		{
			int checkpointIndex = index + 1;
			this.OnCheckPointPassed(this, new CheckpointPassedEventArgs(checkpointIndex));
		}
	}
}
