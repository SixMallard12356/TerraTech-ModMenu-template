using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleWeapon))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleWeaponFlamethrower : Module, IModuleWeapon, IModuleDamager
{
	[SerializeField]
	private Transform m_FirePos;

	[SerializeField]
	private ParticleSystem m_Particles;

	[SerializeField]
	private float m_MaxFlameLength = 10f;

	[SerializeField]
	private float m_DPS = 100f;

	[SerializeField]
	private ManDamage.DamageType m_DamageType;

	[SerializeField]
	private StatusEffect.EffectTypes m_StatusEffectToApply;

	[SerializeField]
	private float m_ExtendTime = 2f;

	[SerializeField]
	private float m_RetractTime = 1f;

	[SerializeField]
	private Light m_TipLight;

	[SerializeField]
	private float m_TipLightVariance = 1.5f;

	[SerializeField]
	private float m_TipLightIntensity = 2f;

	[SerializeField]
	private Light m_JetLight;

	[SerializeField]
	private float m_JetLightVariance = 1.5f;

	[SerializeField]
	private float m_JetLightIntensity = 2f;

	private List<Damageable> m_HitThisFrame = new List<Damageable>();

	private bool m_WasFiring;

	private float m_CurrentFlameLength;

	private float m_Timer;

	private ParticleSystem.Particle[] m_ParticlesArray;

	ManDamage.DamageType IModuleDamager.DamageType => m_DamageType;

	private void OnAttached()
	{
		m_CurrentFlameLength = 0f;
		m_WasFiring = false;
	}

	private void OnDetaching()
	{
		m_Particles.Stop();
		m_TipLight.enabled = false;
		m_JetLight.enabled = false;
	}

	public bool UpdateDeployment(bool deploy)
	{
		return true;
	}

	public bool PrepareFiring(bool prepareFiring)
	{
		return prepareFiring;
	}

	public int ProcessFiring(bool fire)
	{
		m_HitThisFrame.Clear();
		if (m_WasFiring != fire)
		{
			m_Timer = (fire ? (m_CurrentFlameLength / m_MaxFlameLength * m_ExtendTime) : (Maths.QuadEaseIn(m_CurrentFlameLength / m_MaxFlameLength - 1f) * m_RetractTime));
			m_WasFiring = fire;
		}
		if (fire)
		{
			if (m_CurrentFlameLength < m_MaxFlameLength)
			{
				float num = Mathf.Clamp01(m_Timer / m_ExtendTime);
				m_CurrentFlameLength = num * m_MaxFlameLength;
			}
		}
		else if (m_CurrentFlameLength > 0f)
		{
			float time = Mathf.Clamp01(m_Timer / m_RetractTime);
			m_CurrentFlameLength = (1f - Maths.QuadEaseOut(time)) * m_MaxFlameLength;
		}
		if (m_CurrentFlameLength > 0f)
		{
			if (m_ParticlesArray == null || m_ParticlesArray.Length < m_Particles.main.maxParticles)
			{
				m_ParticlesArray = new ParticleSystem.Particle[m_Particles.main.maxParticles];
			}
			int particles = m_Particles.GetParticles(m_ParticlesArray);
			float num2 = float.MaxValue;
			Vector3 vector = default(Vector3);
			bool flag = false;
			for (int i = 0; i < particles; i++)
			{
				if (m_ParticlesArray[i].remainingLifetime < num2)
				{
					vector = m_ParticlesArray[i].position;
					num2 = m_ParticlesArray[i].remainingLifetime;
					flag = true;
				}
			}
			if (flag)
			{
				m_JetLight.enabled = true;
				if (m_Particles.main.simulationSpace == ParticleSystemSimulationSpace.Local)
				{
					m_JetLight.transform.position = m_Particles.transform.localToWorldMatrix.MultiplyPoint(vector);
				}
				else
				{
					m_JetLight.transform.position = vector;
				}
				m_JetLight.intensity = m_CurrentFlameLength / m_MaxFlameLength * m_JetLightIntensity + Random.Range(0f - m_JetLightVariance, m_JetLightVariance);
			}
			m_TipLight.enabled = true;
			m_TipLight.intensity = m_CurrentFlameLength / m_MaxFlameLength * m_TipLightIntensity + Random.Range(0f - m_TipLightVariance, m_TipLightVariance);
			if (!m_Particles.isPlaying)
			{
				m_Particles.Play();
			}
			ParticleSystem.MainModule main = m_Particles.main;
			main.startLifetimeMultiplier = m_CurrentFlameLength / main.startSpeedMultiplier;
		}
		else
		{
			m_JetLight.enabled = false;
			m_TipLight.enabled = false;
			m_Particles.Stop();
		}
		m_Timer += Time.deltaTime;
		if (!fire)
		{
			return 0;
		}
		return 1;
	}

	public bool ReadyToFire()
	{
		return true;
	}

	public bool FiringObstructed()
	{
		return false;
	}

	public float GetVelocity()
	{
		return m_Particles.main.startSpeedMultiplier;
	}

	public float GetRange()
	{
		ParticleSystem.MainModule main = m_Particles.main;
		return main.startLifetimeMultiplier * main.startSpeedMultiplier;
	}

	public bool AimWithTrajectory()
	{
		bool result = false;
		if (m_Particles.main.gravityModifier.constant > 0f)
		{
			result = true;
		}
		return result;
	}

	public Transform GetFireTransform()
	{
		return m_FirePos;
	}

	public float GetFireRateFraction()
	{
		return m_CurrentFlameLength / m_MaxFlameLength;
	}

	public bool IsAimingAtFloor(float limitedAngle)
	{
		if (Vector3.Angle(m_FirePos.transform.forward, -Vector3.up) < limitedAngle)
		{
			return true;
		}
		return false;
	}

	float IModuleDamager.GetHitDamage()
	{
		return m_DPS / 30f;
	}

	float IModuleDamager.GetHitsPerSec()
	{
		return 30f;
	}

	private void ParticleCollisionCallback(Visible other, Vector3 hitPos, Vector3 hitDir)
	{
		if (!(other != null))
		{
			return;
		}
		Damageable damageable = other.damageable;
		if ((bool)damageable && !m_HitThisFrame.Contains(damageable))
		{
			float damage = m_DPS * Time.deltaTime;
			Singleton.Manager<ManDamage>.inst.DealDamage(damageable, damage, m_DamageType, this, base.block.tank, hitPos, hitDir);
			if (m_StatusEffectToApply != StatusEffect.EffectTypes.Unassigned)
			{
				Singleton.Manager<ManStatusEffects>.inst.TryApplyUnnetworkedEffectOnVisible(m_StatusEffectToApply, other, base.block.visible);
			}
			m_HitThisFrame.Add(damageable);
		}
	}

	private void OnPool()
	{
		m_Particles.GetComponent<ParticleCollisionCallback>().SubscribeCallback(ParticleCollisionCallback);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
	}

	private void OnSpawn()
	{
		m_TipLight.enabled = false;
		m_JetLight.enabled = false;
	}
}
