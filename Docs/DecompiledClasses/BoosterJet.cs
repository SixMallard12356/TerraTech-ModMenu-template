#define UNITY_EDITOR
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class BoosterJet : Thruster
{
	[Tooltip("Units consumed per second while boosting")]
	[FormerlySerializedAs("burnRate")]
	[SerializeField]
	private float m_BurnRate = 25f;

	[Tooltip("How long it takes for thrust to fall off when you stop firing, in seconds")]
	[SerializeField]
	[FormerlySerializedAs("fireRateFalloff")]
	private float m_FireRateFalloff = 1f;

	[SerializeField]
	[Tooltip("Whether or not this jet applies fuel consumption (Eg steering 'hovers' don't).")]
	private bool m_ConsumesFuel = true;

	[HideInInspector]
	[SerializeField]
	private JetTrail[] m_Trails;

	private TechBooster m_NextTechBoosterManager;

	public bool IsFiring => m_TargetThrustRate > 0f;

	public float BurnRate => m_BurnRate;

	public bool ConsumesFuel => m_ConsumesFuel;

	protected override float ValidateActuationRate(float unvalidatedFireStrength)
	{
		return Mathf.Clamp01(unvalidatedFireStrength);
	}

	protected override void SetTankAutostabilisation(float stabilisationValue)
	{
		m_CurrentThrustRate = Mathf.Clamp(stabilisationValue, m_CurrentThrustRate, 1f);
	}

	protected override void OnTargetRigidbodyRefreshed()
	{
		m_NextTechBoosterManager = (base.IsAttachedToTank ? m_ParentBlock.tank.Boosters : null);
	}

	private void PrePool()
	{
		m_Trails = (from o in base.gameObject.EnumerateHierarchy()
			select o.GetComponent<JetTrail>() into jt
			where jt != null
			select jt).ToArray();
		d.Assert(m_Trails.Length != 0, "BoosterJet " + base.name + " needs at least one JetTrail in hierarchy");
	}

	private void OnSpawn()
	{
		JetTrail[] trails = m_Trails;
		for (int i = 0; i < trails.Length; i++)
		{
			trails[i].Cease();
		}
	}

	protected override void OnUpdate()
	{
		if (m_CurrentThrustRate != 0f)
		{
			JetTrail[] trails = m_Trails;
			for (int i = 0; i < trails.Length; i++)
			{
				trails[i].Fire(m_CurrentThrustRate);
			}
		}
		else
		{
			JetTrail[] trails = m_Trails;
			for (int i = 0; i < trails.Length; i++)
			{
				trails[i].Cease();
			}
		}
		base.OnUpdate();
	}

	protected override void OnFixedUpdate()
	{
		if (m_TargetThrustRate > 0f)
		{
			m_CurrentThrustRate = m_TargetThrustRate;
		}
		else if (m_CurrentThrustRate != 0f)
		{
			m_CurrentThrustRate = Mathf.MoveTowards(m_CurrentThrustRate, 0f, Time.deltaTime / m_FireRateFalloff);
		}
		if (m_CurrentThrustRate != 0f)
		{
			m_LastForce = QueryBoostThrustVector(m_CurrentThrustRate, out var centerOfBoosterThrustWorld);
			if (m_ConsumesFuel && m_NextTechBoosterManager != null)
			{
				m_NextTechBoosterManager.Burn(m_CurrentThrustRate * m_BurnRate * Time.deltaTime);
			}
			if (m_TargetRigidbody != null && m_LastForce.magnitude != 0f)
			{
				m_TargetRigidbody.AddForceAtPosition(m_LastForce, centerOfBoosterThrustWorld);
			}
			base.OnFixedUpdate();
		}
	}
}
