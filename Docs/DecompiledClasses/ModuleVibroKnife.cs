using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleVibroKnife : ModuleMeleeWeapon
{
	private class Blade
	{
		public Transform m_Transform;

		public Vector3 m_StartingLocalPos;

		public float m_Countdown;

		public Blade(Transform transform, float startingCountdown)
		{
			m_Transform = transform;
			m_Countdown = startingCountdown;
			m_StartingLocalPos = m_Transform.localPosition;
		}

		public void OffsetPosition(Vector3 localPosOffset)
		{
			m_Transform.localPosition = m_StartingLocalPos + localPosOffset;
		}
	}

	[FormerlySerializedAs("damagePerSecond")]
	[SerializeField]
	private float m_DamagePerSecond;

	[SerializeField]
	[FormerlySerializedAs("m_SpinOnCollisionDuration")]
	private float m_ActivateOnCollisionDuration;

	[SerializeField]
	private GameObject m_BladeContainer;

	[SerializeField]
	private Collider m_BladeCollider;

	[SerializeField]
	private Vector3 m_VibrationPositionRange = new Vector3(0.4f, 0.1f, 0.3f);

	[SerializeField]
	private MinMaxFloat m_VibrationTimeStep = MinMaxFloat.ZeroOne;

	[SerializeField]
	private ParticleSystem[] m_ParticleSystems = new ParticleSystem[0];

	private Blade[] m_Blades;

	protected override bool PlaySFXWhileActive => true;

	private void OnTrig(TriggerCatcher.Interaction type, Collider otherCollider)
	{
		if (type != TriggerCatcher.Interaction.Exit && (bool)Visible.FindVisibleUpwards(otherCollider))
		{
			HandleCollision(otherCollider.TriggerToFrameCollisionInfo(otherCollider.ClosestPoint(m_BladeCollider.bounds.center)), m_ActivateOnCollisionDuration);
		}
	}

	protected override void SetWeaponActive(bool state, bool instantly = false)
	{
		UpdateVibrations(!state);
		base.SetWeaponActive(state, instantly);
		m_AudioUpdateRate = (state ? 1f : 0f);
	}

	protected void SetBladeEnabled(bool state)
	{
		if (state)
		{
			TriggerCatcher.Subscribe(m_BladeCollider.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnTrig);
		}
		else
		{
			TriggerCatcher.Unsubscribe(m_BladeCollider.gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnTrig);
		}
		for (int i = 0; i < m_ParticleSystems.Length; i++)
		{
			m_ParticleSystems[i].gameObject.SetActive(state);
			if (state && m_ParticleSystems[i].isStopped)
			{
				m_ParticleSystems[i].Play();
			}
		}
		m_BladeContainer.SetActive(state);
	}

	protected void UpdateVibrations(bool reset = false)
	{
		if (!m_IsActive && !reset)
		{
			return;
		}
		for (int i = 0; i < m_Blades.Length; i++)
		{
			if (reset)
			{
				m_Blades[i].OffsetPosition(Vector3.zero);
				continue;
			}
			m_Blades[i].m_Countdown -= Time.deltaTime;
			if (m_Blades[i].m_Countdown < 0f)
			{
				m_Blades[i].m_Countdown = m_VibrationTimeStep.Random;
				m_Blades[i].OffsetPosition(m_VibrationPositionRange.RandomizeSignedRange());
			}
		}
	}

	public override float GetHitDamage()
	{
		return m_DamagePerSecond / 30f;
	}

	public override float GetHitsPerSec()
	{
		return 30f;
	}

	protected override void HandleLastFrameCollisions()
	{
		if (m_LastTargetCollisionsInfo.Count == 0 || m_DamagePerSecond == 0f)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < m_LastTargetCollisionsInfo.Count; i++)
		{
			if (!m_LastTargetCollisionsInfo[i].IsNull)
			{
				TryDoDamageToFrameCollision(m_LastTargetCollisionsInfo[i], m_DamagePerSecond * Time.deltaTime);
				if (!flag)
				{
					flag = true;
					TriggerHitVFX(m_LastTargetCollisionsInfo[i].Point, m_LastTargetCollisionsInfo[i].Normal);
				}
			}
		}
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		SetBladeEnabled(state: true);
	}

	protected override void OnDetaching()
	{
		SetBladeEnabled(state: false);
		base.OnDetaching();
	}

	private void OnPool()
	{
		m_Blades = new Blade[m_BladeContainer.transform.childCount];
		for (int i = 0; i < m_Blades.Length; i++)
		{
			m_Blades[i] = new Blade(m_BladeContainer.transform.GetChild(i), m_VibrationTimeStep.Random);
			m_Blades[i].OffsetPosition(m_VibrationPositionRange.RandomizeSignedRange());
		}
	}

	private void OnSpawn()
	{
		SetBladeEnabled(state: false);
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		UpdateVibrations();
	}
}
