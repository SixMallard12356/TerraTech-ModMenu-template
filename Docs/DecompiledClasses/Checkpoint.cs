using UnityEngine;

public abstract class Checkpoint : MonoBehaviour
{
	public Event<Checkpoint, Tank> OnCheckpointPassed;

	private int m_RelativeCheckpointIndex;

	private bool m_CleaningUp;

	private ICheckpointVisualer[] m_CheckpointVisualers;

	public void UpdateRelativeCheckpointIndex(int num, int numFutureGatesToShow)
	{
		m_RelativeCheckpointIndex = num;
		if (m_CheckpointVisualers != null && m_CheckpointVisualers.Length != 0)
		{
			for (int i = 0; i < m_CheckpointVisualers.Length; i++)
			{
				m_CheckpointVisualers[i].RelativeIndexUpdated(m_RelativeCheckpointIndex, numFutureGatesToShow);
			}
		}
	}

	public void Setup(Vector3 position, Vector3 fwdDir, Vector3 upDir, float width, float height, int relativeCheckpointIndex, float timeLimit, int numFutureGatesToShow)
	{
		SetupCheckpoint(position, fwdDir, upDir, width, height);
		if (m_CheckpointVisualers != null && m_CheckpointVisualers.Length != 0)
		{
			for (int i = 0; i < m_CheckpointVisualers.Length; i++)
			{
				m_CheckpointVisualers[i].Initialise(this, relativeCheckpointIndex, timeLimit, numFutureGatesToShow);
			}
		}
	}

	public void Cleanup(bool immediately = false)
	{
		if (!immediately && m_CheckpointVisualers != null && m_CheckpointVisualers.Length != 0)
		{
			for (int i = 0; i < m_CheckpointVisualers.Length; i++)
			{
				m_CheckpointVisualers[i].StartCleanup();
			}
			m_CleaningUp = true;
		}
		else
		{
			this.Recycle();
		}
	}

	public abstract bool IsTechInContact(Tank tank);

	protected abstract void SetupCheckpoint(Vector3 position, Vector3 fwdDir, Vector3 upDir, float width, float height);

	private void OnPool()
	{
		m_CheckpointVisualers = GetComponentsInChildren<ICheckpointVisualer>();
	}

	private void OnSpawn()
	{
		OnCheckpointPassed.EnsureNoSubscribers();
		m_RelativeCheckpointIndex = int.MinValue;
	}

	private void OnRecycle()
	{
		m_CleaningUp = false;
	}

	protected virtual void Update()
	{
		if (!m_CleaningUp)
		{
			return;
		}
		bool flag = true;
		if (m_CheckpointVisualers != null && m_CheckpointVisualers.Length != 0)
		{
			for (int i = 0; i < m_CheckpointVisualers.Length; i++)
			{
				if (!m_CheckpointVisualers[i].IsReadyWithCleanup())
				{
					flag = false;
					break;
				}
			}
		}
		if (flag)
		{
			this.Recycle();
		}
	}
}
