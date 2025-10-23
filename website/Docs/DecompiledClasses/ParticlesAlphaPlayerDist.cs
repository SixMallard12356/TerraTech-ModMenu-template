using UnityEngine;

public class ParticlesAlphaPlayerDist : MonoBehaviour
{
	public float m_AlphaToDistance = 0.01f;

	private ParticleSystem m_Particles;

	private void Awake()
	{
		m_Particles = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		if ((bool)Singleton.playerTank)
		{
			float magnitude = (base.transform.position - Singleton.playerTank.boundsCentreWorld).SetY(0f).magnitude;
			ParticleSystem.MinMaxGradient minMaxGradient = m_Particles.main.startColor.color.SetAlpha(magnitude * m_AlphaToDistance);
		}
	}
}
