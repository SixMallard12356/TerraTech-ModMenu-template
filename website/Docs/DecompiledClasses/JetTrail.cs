#define UNITY_EDITOR
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class JetTrail : MonoBehaviour
{
	public float randomScaleFactor = 0.2f;

	private ParticleSystem particles;

	private float m_Scale = 1f;

	private float initParticleSpeed;

	private float initParticleSize;

	private bool m_IsActive;

	public void Fire(float strength)
	{
		if (!m_IsActive)
		{
			base.gameObject.SetActive(value: true);
			m_IsActive = true;
		}
		m_Scale = strength * (1f + (Random.value * 2f - 1f) * randomScaleFactor);
	}

	public void Cease()
	{
		if (m_IsActive)
		{
			base.gameObject.SetActive(value: false);
			m_IsActive = false;
		}
		m_Scale = 1f;
	}

	private void OnPool()
	{
		particles = GetComponentsInChildren<ParticleSystem>(includeInactive: true).FirstOrDefault();
		if (particles == null)
		{
			d.LogError("JetTrail - " + base.gameObject.name + " should have one child ParticleSystem");
		}
		ParticleSystem.MainModule main = particles.main;
		initParticleSpeed = main.startSpeedMultiplier;
		initParticleSize = main.startSizeMultiplier;
		m_IsActive = false;
		base.gameObject.SetActive(value: false);
		TankBlock componentInParents = this.GetComponentInParents<TankBlock>();
		((componentInParents != null) ? componentInParents.BlockUpdate : new MonoBehaviourEvent<MB_Update>(base.gameObject)).Subscribe(OnUpdate);
	}

	private void OnUpdate()
	{
		if (m_IsActive && m_Scale != base.transform.localScale.x)
		{
			base.transform.localScale = new Vector3(m_Scale, base.transform.localScale.y, base.transform.localScale.z);
			ParticleSystem.MainModule main = particles.main;
			main.startSpeedMultiplier = initParticleSpeed * m_Scale;
			main.startSizeMultiplier = initParticleSize * m_Scale;
		}
	}
}
