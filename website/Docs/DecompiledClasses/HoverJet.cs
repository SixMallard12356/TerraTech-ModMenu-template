#define UNITY_EDITOR
using System;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class HoverJet : MonoBehaviour
{
	public Transform effector;

	public float jetRadius = 0.4f;

	public AnimationCurve forceFunction;

	public float forceMax = 20f;

	public float forceRangeMax = 1f;

	[SerializeField]
	private float m_DampingScale = 0.02f;

	[SerializeField]
	private float m_GroundMaxSlopeAngle = 75f;

	[SerializeField]
	private float m_MaxClimbDistance = 4f;

	[SerializeField]
	private Transform m_VectoredThrustTransform;

	[SerializeField]
	private float m_VectoredThrustMaxAngle = 15f;

	[SerializeField]
	private float m_VectoredThrustAnglePerSecond = 3f;

	[SerializeField]
	private float m_AutoStabiliseStrength = 0.4f;

	[SerializeField]
	[HideInInspector]
	private Quaternion m_VectorThrustBindRotation = Quaternion.identity;

	[SerializeField]
	private float m_VectoredThrustMaxForceAngle = 10f;

	[SerializeField]
	private float m_HoverPowerChangePerSecond = 1f;

	[SerializeField]
	private float m_HoverPowerMin;

	[SerializeField]
	private float m_HoverPowerMax = 2f;

	[SerializeField]
	private Spinner[] m_Spinners;

	[Range(0f, 1f)]
	[SerializeField]
	private float m_SpinnerMinSpeedPercent = 0.25f;

	[SerializeField]
	[HideInInspector]
	private TankBlock m_ParentBlock;

	private ParticleSystem particles;

	private float m_NormalizedPushForce;

	private float m_CosGroundMaxSlopeAngle;

	private Vector3 m_RotationContributionUp;

	private Vector3 m_ThrustContributionUp;

	private Vector3 m_RotationContributionRight;

	private Vector3 m_ThrustContributionRight;

	private Vector3 m_ThrustContributionHover;

	private bool m_HoverEnabled;

	private float m_DriveInput;

	private float m_TurnInput;

	private float m_HoverInput;

	private float m_Drive;

	private float m_Turn;

	private float m_Hover;

	private bool m_AnyMovementControl;

	private Vector3 m_ThrustDirUp;

	private Vector3 m_ThrustDirRight;

	private Vector3 m_EffectorDir;

	private AutoStabiliser m_AutoStabiliser;

	private static int k_LayerIgnoreMask;

	private bool m_JetEffectActive;

	private static RaycastHit[] s_Hits = new RaycastHit[32];

	public bool grounded { get; private set; }

	public float NormalizedPushForce
	{
		get
		{
			if (!m_HoverEnabled)
			{
				return 0f;
			}
			return m_NormalizedPushForce;
		}
	}

	public void OnControlInput(TankControl.ControlState driveData, float hover)
	{
		Vector3 lhs = driveData.Throttle + driveData.InputMovement;
		float value = Vector3.Dot(driveData.InputRotation, m_RotationContributionUp) + Vector3.Dot(lhs, m_ThrustContributionUp);
		float value2 = Vector3.Dot(driveData.InputRotation, m_RotationContributionRight) + Vector3.Dot(lhs, m_ThrustContributionRight);
		m_DriveInput = Mathf.Clamp(value, -1f, 1f);
		m_TurnInput = Mathf.Clamp(value2, -1f, 1f);
		m_AnyMovementControl = driveData.AnyMovementOrBoostControl;
		float t = 0f;
		if (m_HoverEnabled)
		{
			t = hover + Vector3.Dot(lhs, m_ThrustContributionHover);
		}
		m_HoverInput = Mathf.Lerp(m_HoverPowerMin, m_HoverPowerMax, t);
	}

	public void InitHoverPower(float hover)
	{
		m_HoverInput = Mathf.Lerp(m_HoverPowerMin, m_HoverPowerMax, hover);
		m_Hover = m_HoverInput;
	}

	public void SetEnabled(bool enabled)
	{
		m_HoverEnabled = enabled;
	}

	private void AutoStabiliseTank(ref float driveInput, ref float turnInput)
	{
		if ((bool)m_ParentBlock.tank && m_ParentBlock.tank.ShouldAutoStabilise && m_AutoStabiliseStrength > 0f && (bool)m_VectoredThrustTransform && !m_AnyMovementControl)
		{
			Rigidbody rbody = m_ParentBlock.tank.rbody;
			Quaternion rotation = m_ParentBlock.tank.rootBlockTrans.rotation;
			Vector3 effectorPos = rbody.position + (effector.position - rbody.transform.position);
			Vector3 autoStabilisationVelocity = m_AutoStabiliser.GetAutoStabilisationVelocity(rbody, effectorPos);
			Vector3 velLocal = Quaternion.Inverse(rotation) * autoStabilisationVelocity;
			velLocal = m_ParentBlock.tank.control.ZeroLocalVelocityInThrottledAxes(velLocal);
			float num = 0f;
			float num2 = 0f;
			if (grounded)
			{
				float num3 = -1f / (m_VectoredThrustMaxAngle * ((float)Math.PI / 180f));
				num = Mathf.Asin((rotation * m_ThrustContributionUp).y) * num3;
				num2 = Mathf.Asin((rotation * m_ThrustContributionRight).y) * num3;
			}
			float num4 = 1f;
			driveInput = 0f - Mathf.Clamp(m_AutoStabiliseStrength * Vector3.Dot(velLocal, m_ThrustContributionUp) + num, 0f - num4, num4);
			turnInput = 0f - Mathf.Clamp(m_AutoStabiliseStrength * Vector3.Dot(velLocal, m_ThrustContributionRight) + num2, 0f - num4, num4);
		}
		else
		{
			m_AutoStabiliser.Reset();
		}
	}

	private void EnableJetEffects(bool active)
	{
		if (active && !m_JetEffectActive)
		{
			if (particles != null)
			{
				particles.Play();
			}
		}
		else if (!active && m_JetEffectActive && particles != null)
		{
			particles.Stop();
		}
		m_JetEffectActive = active;
	}

	public void OnAttached()
	{
		if ((bool)particles && base.enabled)
		{
			particles.gameObject.SetActive(value: true);
			m_JetEffectActive = true;
			EnableJetEffects(active: false);
		}
	}

	public void OnDetaching()
	{
		if ((bool)particles)
		{
			particles.gameObject.SetActive(value: false);
		}
		m_DriveInput = (m_TurnInput = (m_HoverInput = 0f));
		m_Drive = (m_Turn = (m_Hover = 0f));
		m_AnyMovementControl = false;
	}

	public void OnResetTechPhysics()
	{
		if ((bool)effector && (bool)m_VectoredThrustTransform && (bool)m_ParentBlock && (bool)m_ParentBlock.tank)
		{
			Quaternion localRotation = m_VectoredThrustTransform.localRotation;
			m_VectoredThrustTransform.localRotation = m_VectorThrustBindRotation;
			TankControl.GetInputEffect(m_ParentBlock.tank, effector.transform.position, effector.transform.up, out m_RotationContributionUp, out m_ThrustContributionUp);
			TankControl.GetInputEffect(m_ParentBlock.tank, effector.transform.position, effector.transform.right, out m_RotationContributionRight, out m_ThrustContributionRight);
			TankControl.GetInputEffect(m_ParentBlock.tank, effector.transform.position, -effector.transform.forward, out var _, out m_ThrustContributionHover, TankControl.ControlContribution.Movement);
			m_VectoredThrustTransform.localRotation = localRotation;
		}
	}

	private void PrePool()
	{
		if ((bool)m_VectoredThrustTransform)
		{
			m_VectorThrustBindRotation = m_VectoredThrustTransform.localRotation;
		}
		m_CosGroundMaxSlopeAngle = Mathf.Cos(m_GroundMaxSlopeAngle * ((float)Math.PI / 180f));
		k_LayerIgnoreMask = ~(Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask | Globals.inst.layerShield.mask | Globals.inst.layerCosmetic.mask | Globals.inst.layerSceneryCoarse.mask | Globals.inst.layerBullet.mask | Globals.inst.layerShieldPiercingBullet.mask);
		m_ParentBlock = this.GetComponentInParents<TankBlock>();
		Spinner[] spinners = m_Spinners;
		if (spinners != null && spinners.Length == 0)
		{
			m_Spinners = null;
		}
	}

	private void OnPool()
	{
		d.AssertFormat(effector != null, "HoverJet '{0}' on block '{1}' is invalid without an effector assigned!", base.name, m_ParentBlock.name);
		particles = GetComponentsInChildren<ParticleSystem>(includeInactive: true).FirstOrDefault();
		m_ParentBlock.AttachedEvent.Subscribe(OnAttached);
		m_ParentBlock.DetachingEvent.Subscribe(OnDetaching);
		m_ParentBlock.BlockUpdate.Subscribe(OnUpdate);
		m_ParentBlock.BlockFixedUpdate.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		if ((bool)particles)
		{
			particles.gameObject.SetActive(value: false);
		}
		m_AutoStabiliser.Reset();
		m_NormalizedPushForce = 0f;
		forceRangeMax = Mathf.Max(forceRangeMax, 0.1f);
		m_HoverEnabled = true;
	}

	private void OnEnable()
	{
		if ((bool)particles)
		{
			particles.gameObject.SetActive(value: true);
		}
		OnResetTechPhysics();
	}

	private void OnDisable()
	{
		EnableJetEffects(active: false);
	}

	private void OnUpdate()
	{
		bool flag = m_HoverEnabled && m_Hover > 0f;
		float deltaTime = Time.deltaTime;
		EnableJetEffects(flag);
		if ((bool)m_VectoredThrustTransform)
		{
			float driveInput = (flag ? m_DriveInput : 0f);
			float turnInput = (flag ? m_TurnInput : 0f);
			if (flag)
			{
				AutoStabiliseTank(ref driveInput, ref turnInput);
			}
			float maxDelta = m_VectoredThrustAnglePerSecond * deltaTime;
			m_Drive = Mathf.MoveTowards(m_Drive, driveInput, maxDelta);
			m_Turn = Mathf.MoveTowards(m_Turn, turnInput, maxDelta);
			Quaternion quaternion = Quaternion.AngleAxis((0f - m_Turn) * m_VectoredThrustMaxAngle, Vector3.forward) * Quaternion.AngleAxis(m_Drive * m_VectoredThrustMaxAngle, Vector3.right);
			m_VectoredThrustTransform.localRotation = m_VectorThrustBindRotation;
			m_EffectorDir = effector.forward;
			m_ThrustDirUp = effector.up;
			m_ThrustDirRight = effector.right;
			m_VectoredThrustTransform.localRotation = m_VectorThrustBindRotation * quaternion;
		}
		else
		{
			m_EffectorDir = effector.forward;
		}
		m_Hover = Mathf.MoveTowards(m_Hover, m_HoverInput, m_HoverPowerChangePerSecond * deltaTime);
		if (m_Spinners != null && flag)
		{
			float t = Mathf.InverseLerp(m_HoverPowerMin, m_HoverPowerMax, m_Hover);
			Spinner[] spinners = m_Spinners;
			foreach (Spinner spinner in spinners)
			{
				float num = Mathf.Lerp(m_SpinnerMinSpeedPercent * spinner.m_Speed, spinner.m_Speed, t);
				spinner.UpdateSpin(360f * num * deltaTime);
			}
		}
	}

	private void OnFixedUpdate()
	{
		grounded = false;
		if (!m_HoverEnabled || m_Hover == 0f)
		{
			return;
		}
		Rigidbody rbody = m_ParentBlock.tank.rbody;
		Vector3 vector = rbody.position + (effector.position - rbody.transform.position);
		float num = forceRangeMax * m_Hover;
		int num2 = Physics.SphereCastNonAlloc(new Ray(vector - m_EffectorDir * jetRadius, m_EffectorDir), jetRadius, s_Hits, num, k_LayerIgnoreMask, QueryTriggerInteraction.Ignore);
		float num3 = 0f;
		float num4 = 0f;
		for (int i = 0; i < num2; i++)
		{
			RaycastHit raycastHit = s_Hits[i];
			if (raycastHit.distance == 0f || Vector3.Dot(raycastHit.normal, -m_EffectorDir) < m_CosGroundMaxSlopeAngle || (object)Singleton.Manager<ManVisible>.inst.FindVisible(raycastHit.collider) == m_ParentBlock.visible)
			{
				continue;
			}
			float num5 = Vector3.Dot(vector - raycastHit.point, m_EffectorDir);
			if (num5 > m_MaxClimbDistance)
			{
				continue;
			}
			float num6 = raycastHit.distance / num;
			float num7 = forceFunction.Evaluate(num6);
			PhysicsModifier component = raycastHit.collider.gameObject.GetComponent<PhysicsModifier>();
			if ((bool)component)
			{
				num7 *= component.HoverForceScale;
				if (num5 > component.HoverMaxClimbDistance)
				{
					continue;
				}
			}
			num4 = Mathf.Max(1f - num6, num4);
			num3 = Mathf.Max(num7, num3);
		}
		m_NormalizedPushForce = num4;
		if (num3 > 0f)
		{
			if ((bool)m_VectoredThrustTransform)
			{
				float num8 = num3 * forceMax * Mathf.Sin(m_VectoredThrustMaxForceAngle * ((float)Math.PI / 180f));
				rbody.AddForceAtPosition(m_ThrustDirUp * m_Drive * num8, vector);
				rbody.AddForceAtPosition(m_ThrustDirRight * m_Turn * num8, vector);
			}
			float num9 = Vector3.Dot(-m_EffectorDir, rbody.GetPointVelocity(vector));
			num3 -= num9 * m_DampingScale;
			if (num3 > 0f)
			{
				rbody.AddForceAtPosition(-m_EffectorDir * num3 * forceMax, vector);
			}
			grounded = true;
			m_ParentBlock.tank.grounded = true;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if ((bool)m_ParentBlock)
		{
			Rigidbody rigidbody = (m_ParentBlock.tank ? m_ParentBlock.tank.rbody : m_ParentBlock.rbody);
			Vector3 vector = rigidbody.position + (effector.position - rigidbody.transform.position) - m_EffectorDir * jetRadius;
			Vector3 vector2 = vector + m_EffectorDir * forceRangeMax;
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawWireSphere(vector, jetRadius);
			Gizmos.DrawWireSphere(vector2, jetRadius);
			Gizmos.matrix = Matrix4x4.identity;
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(effector.position, effector.position + m_ThrustDirUp);
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine(effector.position, effector.position + m_ThrustDirRight);
		}
	}
}
