#define UNITY_EDITOR
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(Damageable))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class BubbleShield : MonoBehaviour
{
	[SerializeField]
	[HideInInspector]
	private Spinner m_Bubble;

	[SerializeField]
	[HideInInspector]
	private Spinner m_Projector;

	[SerializeField]
	private Collider m_RepulsorBulletTrigger;

	[SerializeField]
	private bool m_IsBulletTriggerCylinder;

	[SerializeField]
	private Collider m_RepulsorTechTrigger;

	[SerializeField]
	private bool m_IsTechTriggerCylinder;

	[SerializeField]
	[HideInInspector]
	private ParticleSystem[] m_ProjectorParticles;

	private float[] m_ParticleLifeRange;

	private float[] m_ParticleSpeedRange;

	private float m_TargetScale;

	private float m_MaxScale;

	private AnimationCurve m_CurrentInterpCurve;

	private float m_InterpStartTime;

	private float m_InterpDuration;

	private float m_InterpStartScale;

	private float m_InterpScaleMin;

	private Damageable m_Damageable;

	public Collider RepulsorBulletTrigger => m_RepulsorBulletTrigger;

	public Collider RepulsorTechTrigger => m_RepulsorTechTrigger;

	public bool IsBulletTriggerCylinder => m_IsBulletTriggerCylinder;

	public bool IsTechTriggerCylinder => m_IsTechTriggerCylinder;

	public bool IsScaling => m_CurrentInterpCurve != null;

	public float CurrentScale { get; private set; }

	public Damageable Damageable
	{
		get
		{
			if (m_Damageable == null)
			{
				m_Damageable = GetComponent<Damageable>();
			}
			return m_Damageable;
		}
	}

	public float ShieldRadius
	{
		get
		{
			SphereCollider sphereCollider = RepulsorBulletTrigger as SphereCollider;
			if (!(sphereCollider != null))
			{
				return 0f;
			}
			return sphereCollider.radius * CurrentScale;
		}
	}

	public Vector3 ShieldColliderCenterOffset
	{
		get
		{
			if (RepulsorBulletTrigger is SphereCollider)
			{
				return (RepulsorBulletTrigger as SphereCollider).center;
			}
			if (RepulsorBulletTrigger is BoxCollider)
			{
				return (RepulsorBulletTrigger as BoxCollider).center;
			}
			if (RepulsorBulletTrigger is CapsuleCollider)
			{
				return (RepulsorBulletTrigger as CapsuleCollider).center;
			}
			return Vector3.zero;
		}
	}

	private void SetBubbleScale(float scale, bool forceResetState = false)
	{
		if ((bool)m_Projector)
		{
			if (scale == 0f && (CurrentScale != 0f || forceResetState))
			{
				ParticleSystem[] projectorParticles = m_ProjectorParticles;
				for (int i = 0; i < projectorParticles.Length; i++)
				{
					projectorParticles[i].gameObject.SetActive(value: false);
				}
				m_Projector.SetAutoSpin(enableAutoSpin: false);
			}
			else if (scale != 0f && (CurrentScale == 0f || forceResetState))
			{
				ParticleSystem[] projectorParticles = m_ProjectorParticles;
				for (int i = 0; i < projectorParticles.Length; i++)
				{
					projectorParticles[i].gameObject.SetActive(value: true);
				}
				m_Projector.SetAutoSpin(enableAutoSpin: true);
			}
			if (scale != 0f)
			{
				ParticleSystem[] projectorParticles = m_ProjectorParticles;
				for (int i = 0; i < projectorParticles.Length; i++)
				{
					ParticleSystem.MainModule main = projectorParticles[i].main;
					main.startLifetimeMultiplier = Mathf.Lerp(m_ParticleLifeRange[0], m_ParticleLifeRange[1], scale / m_MaxScale);
					main.startSpeedMultiplier = Mathf.Lerp(m_ParticleSpeedRange[0], m_ParticleSpeedRange[1], scale / m_MaxScale);
				}
			}
		}
		if ((bool)m_Bubble)
		{
			if (scale == 0f && (CurrentScale != 0f || forceResetState))
			{
				m_Bubble.SetAutoSpin(enableAutoSpin: false);
			}
			else if (scale != 0f && (CurrentScale == 0f || forceResetState))
			{
				m_Bubble.SetAutoSpin(enableAutoSpin: true);
			}
			m_Bubble.trans.SetLocalScaleIfChanged(Vector3.one * scale);
		}
		CurrentScale = scale;
	}

	public void SetRanges(float scaleMax, float[] particleLifeRange, float[] particleSpeedRange)
	{
		m_MaxScale = scaleMax;
		m_ParticleLifeRange = particleLifeRange;
		m_ParticleSpeedRange = particleSpeedRange;
	}

	public void SetTargetScale(float newScale)
	{
		SetTargetScale(newScale, 0f, null, 0f, forceNewInterp: false);
	}

	public void SetTargetScale(float newScale, float minInterpScale, AnimationCurve interpCurve, float interpDuration, bool forceNewInterp)
	{
		m_TargetScale = newScale;
		if (interpCurve != null && (m_CurrentInterpCurve == null || forceNewInterp))
		{
			m_InterpStartTime = Time.time;
			m_InterpDuration = interpDuration;
			m_InterpStartScale = CurrentScale;
			m_CurrentInterpCurve = interpCurve;
			m_InterpScaleMin = minInterpScale;
		}
	}

	private void FindInternalComponents()
	{
		Spinner[] componentsInChildren = GetComponentsInChildren<Spinner>(includeInactive: true);
		foreach (Spinner spinner in componentsInChildren)
		{
			ParticleSystem[] componentsInChildren2 = spinner.GetComponentsInChildren<ParticleSystem>(includeInactive: true);
			if ((bool)m_RepulsorBulletTrigger)
			{
				m_Bubble = spinner;
			}
			else if (componentsInChildren2.Length != 0)
			{
				m_Projector = spinner;
				m_ProjectorParticles = componentsInChildren2;
			}
		}
		if (!m_Bubble)
		{
			d.LogError("BubbleShield - Failed to find shield on " + base.gameObject.name);
		}
	}

	private void PrePool()
	{
		FindInternalComponents();
	}

	private void OnPool()
	{
		TankBlock componentInParents = this.GetComponentInParents<TankBlock>();
		((componentInParents != null) ? componentInParents.BlockUpdate : new MonoBehaviourEvent<MB_Update>(base.gameObject)).Subscribe(OnUpdate);
		m_IsBulletTriggerCylinder = (bool)m_RepulsorBulletTrigger && m_RepulsorBulletTrigger is CapsuleCollider && m_IsBulletTriggerCylinder;
		m_IsTechTriggerCylinder = (bool)m_RepulsorTechTrigger && m_RepulsorTechTrigger is CapsuleCollider && m_IsTechTriggerCylinder;
	}

	private void OnSpawn()
	{
		CurrentScale = 0f;
		m_TargetScale = 0f;
		if ((bool)m_Bubble)
		{
			SetBubbleScale(0f, forceResetState: true);
		}
	}

	private void OnUpdate()
	{
		if (m_CurrentInterpCurve != null)
		{
			float num = (Time.time - m_InterpStartTime) / m_InterpDuration;
			float num2 = Mathf.Max(Mathf.Abs(m_TargetScale - m_InterpStartScale), m_InterpScaleMin) * Mathf.Sign(m_TargetScale - m_InterpStartScale);
			SetBubbleScale(m_InterpStartScale + num2 * m_CurrentInterpCurve.Evaluate(num));
			if (num >= 1f)
			{
				m_CurrentInterpCurve = null;
			}
		}
		else
		{
			SetBubbleScale(m_TargetScale);
		}
	}
}
