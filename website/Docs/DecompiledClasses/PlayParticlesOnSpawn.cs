using UnityEngine;

public class PlayParticlesOnSpawn : MonoBehaviour
{
	private ParticleSystem particles;

	private void OnPool()
	{
		particles = GetComponent<ParticleSystem>();
	}

	private void OnSpawn()
	{
		particles.Play();
	}

	private void OnRecycle()
	{
		particles.Stop();
	}
}
