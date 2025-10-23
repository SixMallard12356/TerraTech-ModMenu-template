#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesAnimBehaviour : ActionInStateAnimBehaviour
{
	[SerializeField]
	private Transform m_Prefab;

	[SerializeField]
	private bool m_StopEmittingParticlesOnStateExit;

	[SerializeField]
	private bool m_KillParticlesOnStateExit;

	[SerializeField]
	private bool m_AttachToParent = true;

	[SerializeField]
	[Tooltip("Float animator parameter which controls whether particle emission is enabled")]
	private string m_EmitterControlParameter;

	private int m_ControlParamId;

	private Transform m_CreatedParticlesTrans;

	private ParticleSystemSet m_CreatedParticlesSystem;

	private ParticleRecycler m_CreatedParticlesRecycler;

	private List<Transform> m_CurrentChildParticleSystems;

	protected override void BeginAction(Transform parentVisible)
	{
		d.Assert(m_CreatedParticlesTrans.IsNull(), "PlayParticlesAnimBehaviour.BeginAction - CreatedParticlesTrans was not Null. The previous particle system was not cleaned up correctly!");
		if (!parentVisible || !m_Prefab)
		{
			return;
		}
		m_CreatedParticlesTrans = m_Prefab.Spawn(parentVisible.position, parentVisible.rotation);
		m_CreatedParticlesSystem = m_CreatedParticlesTrans.GetComponent<ParticleSystemSet>();
		m_CreatedParticlesRecycler = m_CreatedParticlesTrans.GetComponent<ParticleRecycler>();
		if (m_AttachToParent)
		{
			m_CreatedParticlesTrans.SetParent(parentVisible);
			m_CurrentChildParticleSystems.Add(m_CreatedParticlesTrans);
			if (m_CreatedParticlesRecycler.IsNotNull())
			{
				m_CreatedParticlesRecycler.ParticleSystemRecycledEvent.Subscribe(OnParticleSystemRecycled);
			}
		}
		if ((bool)m_CreatedParticlesSystem)
		{
			m_CreatedParticlesSystem.SetEmissionEnabled(enabled: true);
		}
	}

	protected override void UpdateAction(Animator animator)
	{
		if (m_EmitterControlParameter.Length > 0)
		{
			bool flag = animator.GetFloat(m_ControlParamId) >= 0.5f;
			if (m_CreatedParticlesSystem.IsNotNull())
			{
				m_CreatedParticlesSystem.SetEmissionEnabled(flag);
			}
			if (m_CreatedParticlesRecycler.IsNotNull() && !flag)
			{
				m_CreatedParticlesRecycler.StopEmitting(stopImmediately: false);
			}
		}
	}

	protected override void EndAction()
	{
		CleanupParticlesOnStateExit();
	}

	private void CleanupParticlesOnStateExit()
	{
		if (m_StopEmittingParticlesOnStateExit)
		{
			if (m_CreatedParticlesSystem.IsNotNull())
			{
				m_CreatedParticlesSystem.SetEmissionEnabled(enabled: false);
			}
			if (m_CreatedParticlesRecycler.IsNotNull())
			{
				m_CreatedParticlesRecycler.StopEmitting(stopImmediately: false);
			}
		}
		if (m_KillParticlesOnStateExit)
		{
			if (m_CreatedParticlesTrans != null)
			{
				d.Assert(m_CreatedParticlesTrans.gameObject.activeInHierarchy, "PlayParticlesAnimBehaviour.CleanupParticlesOnStateExit - Particles " + m_Prefab.name + " were inactive in heirarchy when state was being exited. Were they already recycled!?");
				m_CreatedParticlesTrans.Recycle();
			}
		}
		else if (m_CreatedParticlesRecycler.IsNotNull() && m_CreatedParticlesRecycler.IsPotentiallyLooping)
		{
			d.LogWarning("PlayParticlesAnimBehaviour.CleanupParticlesOnStateExit - ParticleRecycler may still have looping particles on state exit! This may cause the particles to never recycle!");
		}
		m_CreatedParticlesTrans = null;
		m_CreatedParticlesSystem = null;
		m_CreatedParticlesRecycler = null;
	}

	protected override void OnInitialised()
	{
		if (m_AttachToParent && m_CurrentChildParticleSystems == null)
		{
			m_CurrentChildParticleSystems = new List<Transform>();
		}
		if (m_EmitterControlParameter.Length > 0)
		{
			m_ControlParamId = Animator.StringToHash(m_EmitterControlParameter);
		}
	}

	protected override void OnRecycled()
	{
		if (m_AttachToParent && m_CurrentChildParticleSystems != null)
		{
			foreach (Transform currentChildParticleSystem in m_CurrentChildParticleSystems)
			{
				if (currentChildParticleSystem != null)
				{
					currentChildParticleSystem.SetParent(null, worldPositionStays: false);
					currentChildParticleSystem.Recycle();
				}
				else
				{
					d.LogError("PlayParticlesAnimBehaviour - Recycling object but encountered Null childParticleTransforms that were not cleared from the list.");
				}
			}
			m_CurrentChildParticleSystems.Clear();
		}
		CleanupParticlesOnStateExit();
	}

	private void OnParticleSystemRecycled(ParticleRecycler particleRecycler)
	{
		m_CurrentChildParticleSystems.Remove(particleRecycler.transform);
		if (particleRecycler == m_CreatedParticlesRecycler)
		{
			m_CreatedParticlesTrans = null;
			m_CreatedParticlesSystem = null;
			m_CreatedParticlesRecycler = null;
		}
		particleRecycler.ParticleSystemRecycledEvent.Unsubscribe(OnParticleSystemRecycled);
	}
}
