using UnityEngine;

public class uScript_GetCheckPointIndex : uScriptLogic
{
	private int m_Index;

	public bool Out => true;

	public int In()
	{
		return m_Index;
	}

	public void OnEnable()
	{
		CheckpointChallenge.OnCheckpointPassed.Subscribe(OnCheckpointPassedHandler);
	}

	public void OnDisable()
	{
		CheckpointChallenge.OnCheckpointPassed.Unsubscribe(OnCheckpointPassedHandler);
		m_Index = 0;
	}

	private void OnCheckpointPassedHandler(int index, Vector3 position, Quaternion rotation)
	{
		m_Index = index;
	}
}
