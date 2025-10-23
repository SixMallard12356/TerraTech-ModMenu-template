using UnityEngine;

public class MissileProjectile : Projectile
{
	[SerializeField]
	private float m_MaxBoosterLifetime;

	[SerializeField]
	[Tooltip("Optional time for the projectile to remain 'stuck' to the barrel before acting under physics")]
	private float m_StickyBarrelDuration;

	[SerializeField]
	private float m_BoosterActivationDelay;

	[SerializeField]
	[Tooltip("Time before the projectile is armed and ready to explode")]
	private float m_ArmDelay;

	[Tooltip("Used if the projectile collides before being armed.")]
	[SerializeField]
	private float m_DeathDelayNotArmed;

	[SerializeField]
	private bool m_RotateWithVelocityOnBoostersDeactivated = true;

	[SerializeField]
	private bool m_DeactivateBoostersOnFailedStick;

	[SerializeField]
	private bool m_DeactivateBoostersOnSuccessfulStick;

	[Tooltip("Indepentently recycled prefab for a smoke trail. Will be persistent after the projectile gets recycled. <Must have ParticleRecycler and FollowTransform components!>")]
	[SerializeField]
	private Transform m_SmokeTrailPrefab;

	[SerializeField]
	private Transform m_SmokeTrailTransform;

	private bool m_IsArmed;

	private bool m_UsesGravity;

	private ParticleRecycler m_SmokeTrail;

	private BoosterJet[] m_Boosters = new BoosterJet[0];

	private ManTimedEvents.ManagedEvent m_UnstickFromBarrelEvent;

	private ManTimedEvents.ManagedEvent m_BoosterActivationEvent;

	private ManTimedEvents.ManagedEvent m_BoosterDeactivationEvent;

	private ManTimedEvents.ManagedEvent m_WeaponActivationEvent;

	public override void Fire(Vector3 fireDirection, Transform firingOrigin, FireData fireData, ModuleWeapon weapon, Tank shooter = null, bool seekingRounds = false, bool replayRounds = false)
	{
		base.Fire(fireDirection, firingOrigin, fireData, weapon, shooter, seekingRounds, replayRounds);
		if (m_StickyBarrelDuration > 0f)
		{
			StickToObject(weapon.block.trans, weapon.block.visible);
			if (m_UnstickFromBarrelEvent == null)
			{
				m_UnstickFromBarrelEvent = new ManTimedEvents.ManagedEvent(OnUnstickFromBarrel);
			}
			m_UnstickFromBarrelEvent.Set(m_StickyBarrelDuration);
		}
		if (m_BoosterActivationDelay > 0f)
		{
			if (m_BoosterActivationEvent == null)
			{
				m_BoosterActivationEvent = new ManTimedEvents.ManagedEvent(ActivateBoosters);
			}
			m_BoosterActivationEvent.Set(m_BoosterActivationDelay);
		}
		else
		{
			ActivateBoosters();
		}
		if (m_MaxBoosterLifetime > 0f)
		{
			if (m_BoosterDeactivationEvent == null)
			{
				m_BoosterDeactivationEvent = new ManTimedEvents.ManagedEvent(DeactivateBoosters);
			}
			m_BoosterDeactivationEvent.Set(m_BoosterActivationDelay + m_MaxBoosterLifetime);
		}
		if (m_ArmDelay > 0f)
		{
			if (m_WeaponActivationEvent == null)
			{
				m_WeaponActivationEvent = new ManTimedEvents.ManagedEvent(ArmWeapon);
			}
			m_WeaponActivationEvent.Set(m_ArmDelay);
		}
		else
		{
			ArmWeapon();
		}
	}

	public void KillBoosters()
	{
		DeactivateBoosters();
	}

	protected override bool IsProjectileArmed()
	{
		return m_IsArmed;
	}

	protected override bool WillRotateWithVelocity()
	{
		return true;
	}

	protected override float GetDeathDelay()
	{
		if (IsProjectileArmed())
		{
			return base.GetDeathDelay();
		}
		return m_DeathDelayNotArmed;
	}

	protected override void OnDelayedDeathSet()
	{
		DeactivateBoosters();
		m_BoosterActivationEvent?.Clear();
		m_BoosterDeactivationEvent?.Clear();
		m_WeaponActivationEvent?.Clear();
		base.rbody.useGravity = true;
		SetRotateWithVelocity(enable: false);
	}

	private void OnStickAttempted(bool success, Projectile _, Visible __)
	{
		if (!success && m_DeactivateBoostersOnFailedStick)
		{
			DeactivateBoosters();
		}
	}

	private void OnStuck(bool success, Projectile _, Visible __)
	{
		if (success && m_DeactivateBoostersOnSuccessfulStick)
		{
			DeactivateBoosters();
		}
	}

	private void OnUnstickFromBarrel()
	{
		UnstickFromObject();
	}

	private void ActivateBoosters()
	{
		BoosterJet[] boosters = m_Boosters;
		for (int i = 0; i < boosters.Length; i++)
		{
			boosters[i].SetThrustRate(1f);
		}
		if ((bool)m_SmokeTrailPrefab)
		{
			Transform transform = m_SmokeTrailPrefab.Spawn(Singleton.dynamicContainer, m_SmokeTrailTransform.position, m_SmokeTrailTransform.rotation);
			m_SmokeTrail = transform.GetComponent<ParticleRecycler>();
			transform.GetComponent<FollowTransform>().SetFollowTransform(m_SmokeTrailTransform ?? base.transform);
		}
		Singleton.Manager<ManSFX>.inst.TryStartProjectileFlightSFX(this);
		base.rbody.useGravity = false;
	}

	private void DeactivateBoosters()
	{
		BoosterJet[] boosters = m_Boosters;
		for (int i = 0; i < boosters.Length; i++)
		{
			boosters[i].SetThrustRate(0f);
		}
		if ((bool)m_SmokeTrail)
		{
			m_SmokeTrail.StopEmitting();
			m_SmokeTrail = null;
		}
		Singleton.Manager<ManSFX>.inst.TryStopProjectileSFX(this);
		base.rbody.useGravity = true;
		SetRotateWithVelocity(m_RotateWithVelocityOnBoostersDeactivated);
	}

	private void ArmWeapon()
	{
		m_IsArmed = true;
	}

	private void OnPool()
	{
		m_Boosters = GetComponentsInChildren<BoosterJet>();
		m_UsesGravity = base.rbody.useGravity;
		m_StickOnVisibleAttemptedEvent.Subscribe(OnStickAttempted);
		m_StuckToVisibleEvent.Subscribe(OnStuck);
	}

	private void OnSpawn()
	{
		m_IsArmed = false;
	}

	private void OnRecycle()
	{
		DeactivateBoosters();
		m_BoosterActivationEvent?.Clear();
		m_BoosterDeactivationEvent?.Clear();
		m_WeaponActivationEvent?.Clear();
		base.rbody.useGravity = m_UsesGravity;
		SetRotateWithVelocity(enable: false);
	}
}
