#define UNITY_EDITOR
using UnityEngine;

public class PlayParticlesOnFirstUpdate : MonoBehaviour
{
	private ParticleSystem m_ParticleSystem;

	private void OnPool()
	{
		m_ParticleSystem = GetComponent<ParticleSystem>();
		d.Assert(m_ParticleSystem, "Particle system null");
	}

	private void OnSpawn()
	{
		base.enabled = true;
	}

	private void OnRecycle()
	{
	}

	private void Update()
	{
		m_ParticleSystem.Play();
		base.enabled = false;
	}
}
