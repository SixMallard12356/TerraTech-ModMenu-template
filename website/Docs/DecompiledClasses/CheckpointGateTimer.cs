using UnityEngine;

public class CheckpointGateTimer : MonoBehaviour, ICheckpointVisualer
{
	[SerializeField]
	private Animation m_Animation;

	public void Initialise(Checkpoint checkpoint, int relativeCheckpointIndex, float time, int numFutureGatesToShow)
	{
		PlayAnimation(time);
	}

	public void RelativeIndexUpdated(int relativeCheckpointIndex, int numFutureGatesToShow)
	{
	}

	public void StartCleanup()
	{
		StopAndResetAnimation();
	}

	public bool IsReadyWithCleanup()
	{
		return true;
	}

	private void PlayAnimation(float time)
	{
		if (!(time > 0f))
		{
			return;
		}
		foreach (AnimationState item in m_Animation)
		{
			item.speed = item.length / time;
		}
		m_Animation.Play();
	}

	private void StopAndResetAnimation()
	{
		foreach (AnimationState item in m_Animation)
		{
			item.time = 0f;
		}
		m_Animation.Stop();
	}
}
