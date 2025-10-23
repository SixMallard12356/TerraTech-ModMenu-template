using System.Collections.Generic;
using UnityEngine;

public class PlayVFXOnCheckpoint : MonoBehaviour, ICheckpointVisualer
{
	[SerializeField]
	private Checkpoint m_TriggeringCheckpoint;

	[SerializeField]
	private ParticleSystem[] m_Particles;

	[SerializeField]
	private bool m_AddChildParticles;

	private List<ParticleSystem> m_TriggerParticles = new List<ParticleSystem>();

	private Checkpoint m_Checkpoint;

	private bool m_Triggered;

	public void Initialise(Checkpoint checkpoint, int relativeCheckpointIndex, float time, int numFutureGatesToShow)
	{
		SetupCheckpointRef(checkpoint);
	}

	public void RelativeIndexUpdated(int relativeCheckpointIndex, int numFutureGatesToShow)
	{
	}

	public void StartCleanup()
	{
	}

	public bool IsReadyWithCleanup()
	{
		bool flag = false;
		if (m_Triggered)
		{
			for (int i = 0; i < m_TriggerParticles.Count; i++)
			{
				if (m_TriggerParticles[i].particleCount > 0)
				{
					flag = true;
					break;
				}
			}
		}
		return !flag;
	}

	private void SetupCheckpointRef(Checkpoint targetCheckpoint)
	{
		if (m_Checkpoint != null)
		{
			m_Checkpoint.OnCheckpointPassed.Unsubscribe(OnCheckpointTriggered);
		}
		m_Checkpoint = targetCheckpoint;
		if (m_Checkpoint != null)
		{
			m_Checkpoint.OnCheckpointPassed.Subscribe(OnCheckpointTriggered);
		}
	}

	private void OnCheckpointTriggered(Checkpoint checkpoint, Tank tank)
	{
		if (!m_Triggered)
		{
			for (int i = 0; i < m_TriggerParticles.Count; i++)
			{
				m_TriggerParticles[i].Play();
			}
			m_Triggered = true;
		}
	}

	private void OnSpawn()
	{
		m_TriggerParticles.Clear();
		if (m_AddChildParticles)
		{
			ParticleSystem[] componentsInChildren = GetComponentsInChildren<ParticleSystem>();
			if (componentsInChildren != null)
			{
				m_TriggerParticles.AddRange(componentsInChildren);
			}
		}
		if (m_Particles != null)
		{
			m_TriggerParticles.AddRange(m_Particles);
		}
		if (m_TriggeringCheckpoint != null)
		{
			SetupCheckpointRef(m_TriggeringCheckpoint);
		}
		m_Triggered = false;
	}

	private void OnRecycle()
	{
		if (m_Checkpoint != null)
		{
			m_Checkpoint.OnCheckpointPassed.Unsubscribe(OnCheckpointTriggered);
			m_Checkpoint = null;
		}
	}
}
