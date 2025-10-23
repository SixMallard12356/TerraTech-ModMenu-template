#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Thruster : MonoBehaviour
{
	[SerializeField]
	[FormerlySerializedAs("effector")]
	protected Transform m_Effector;

	[SerializeField]
	[FormerlySerializedAs("force")]
	[Tooltip("How much oomph have I got")]
	protected float m_Force = 5000f;

	[SerializeField]
	[FormerlySerializedAs("detachedForceFactor")]
	protected float m_DetachedForceFactor = 1f;

	[SerializeField]
	protected float m_AutoStabiliseStrength = 0.1f;

	[HideInInspector]
	[SerializeField]
	protected TankBlock m_ParentBlock;

	[SerializeField]
	[HideInInspector]
	protected bool m_HasParentBlock;

	[SerializeField]
	[HideInInspector]
	private Rigidbody m_GenericParentRigidBody;

	[HideInInspector]
	[SerializeField]
	private Quaternion m_EffectorDefaultLocalRotation = Quaternion.identity;

	public Event<Thruster, Vector3, Vector3> ThrustDirectionRecalculatedEvent;

	protected Rigidbody m_TargetRigidbody;

	protected float m_TargetThrustRate;

	protected float m_CurrentThrustRate;

	private Vector3 m_RotationContribution;

	private Vector3 m_LocalThrustDirection;

	protected Vector3 m_LastForce;

	public bool IsAttachedToTank
	{
		get
		{
			if (m_HasParentBlock)
			{
				return m_ParentBlock.IsAttached;
			}
			return false;
		}
	}

	public Vector3 EffectorForward => m_Effector.forward;

	public Vector3 EffectorDefaultForward => base.transform.TransformDirection(m_EffectorDefaultLocalRotation * Vector3.forward);

	public Vector3 RotationContribution => m_RotationContribution;

	public Vector3 LocalThrustDirection => m_LocalThrustDirection;

	public float ThrustRateCurrent => m_CurrentThrustRate;

	public float ThrustRateCurrent_Abs => Mathf.Abs(m_CurrentThrustRate);

	public virtual Vector3 QueryBoostThrustVector(float actuationT, out Vector3 centerOfBoosterThrustWorld)
	{
		float forceToApply = GetForceToApply(actuationT);
		RefreshTargetRigidbody();
		if (m_TargetRigidbody == null || actuationT == 0f)
		{
			centerOfBoosterThrustWorld = Vector3.zero;
			return Vector3.zero;
		}
		centerOfBoosterThrustWorld = GetCenterOfThrustWorldPosition();
		return -m_Effector.forward * forceToApply;
	}

	public void AutoStabiliseTank()
	{
		if (!(m_AutoStabiliseStrength > 0f))
		{
			return;
		}
		Rigidbody rbody = m_ParentBlock.tank.rbody;
		Vector3 worldPoint = rbody.position + (m_Effector.position - rbody.transform.position);
		Vector3 forward = m_Effector.forward;
		if (!m_ParentBlock.tank.control.AnyThrottleInAxes(Quaternion.Inverse(rbody.rotation) * forward))
		{
			Vector3 pointVelocity = rbody.GetPointVelocity(worldPoint);
			float num = m_AutoStabiliseStrength * Vector3.Dot(pointVelocity, forward);
			if (Mathf.Abs(num) < 0.1f)
			{
				num = 0f;
			}
			SetTankAutostabilisation(num);
		}
	}

	public void ResetThrustRate()
	{
		SetThrustRate(0f, immediately: true);
	}

	public void SetThrustRate(float actuationT, bool immediately = false)
	{
		m_TargetThrustRate = ValidateActuationRate(actuationT);
		if (immediately)
		{
			m_CurrentThrustRate = m_TargetThrustRate;
		}
	}

	public void RecalculateThrustDirection()
	{
		if (m_ParentBlock.IsAttached)
		{
			Vector3 localThrustDirection = m_LocalThrustDirection;
			TankControl.GetInputEffect(m_ParentBlock.tank, m_Effector.transform.position, -EffectorForward, out m_RotationContribution, out m_LocalThrustDirection);
			ThrustDirectionRecalculatedEvent.Send(this, localThrustDirection, m_LocalThrustDirection);
		}
	}

	protected virtual void SetTankAutostabilisation(float stabilisationValue)
	{
		SetThrustRate(stabilisationValue);
	}

	protected virtual float GetForceToApply(float actuationT)
	{
		float num = m_Force * actuationT;
		if (!IsAttachedToTank)
		{
			num *= m_DetachedForceFactor;
		}
		return num;
	}

	protected Vector3 GetCenterOfThrustWorldPosition()
	{
		return m_TargetRigidbody.position + (m_Effector.position - m_TargetRigidbody.transform.position);
	}

	protected virtual float ValidateActuationRate(float actuationT)
	{
		return actuationT;
	}

	protected void RefreshTargetRigidbody()
	{
		m_TargetRigidbody = GetRigidbodyToApplyForceTo();
		OnTargetRigidbodyRefreshed();
	}

	private Rigidbody GetRigidbodyToApplyForceTo()
	{
		if (!m_HasParentBlock)
		{
			return m_GenericParentRigidBody;
		}
		if (m_ParentBlock.IsAttached)
		{
			return m_ParentBlock.tank.rbody;
		}
		return m_ParentBlock.rbody;
	}

	protected virtual void OnTargetRigidbodyRefreshed()
	{
	}

	private void PrePool()
	{
		m_ParentBlock = this.GetComponentInParents<TankBlock>();
		m_HasParentBlock = m_ParentBlock != null;
		if (!m_HasParentBlock)
		{
			m_GenericParentRigidBody = this.GetComponentInParents<Rigidbody>();
			d.Assert(m_GenericParentRigidBody.IsNotNull(), "BoosterJet not on a TankBlock or object with a RigidBody... ??");
		}
		d.Assert(m_Effector, "Thruster " + base.name + " has no effector");
		m_EffectorDefaultLocalRotation = base.transform.localRotation;
	}

	private void OnPool()
	{
		(m_HasParentBlock ? m_ParentBlock.BlockUpdate : new MonoBehaviourEvent<MB_Update>(base.gameObject)).Subscribe(OnUpdate);
		(m_HasParentBlock ? m_ParentBlock.BlockFixedUpdate : new MonoBehaviourEvent<MB_FixedUpdate>(base.gameObject)).Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		ResetThrustRate();
	}

	protected virtual void OnUpdate()
	{
	}

	protected virtual void OnFixedUpdate()
	{
	}

	private void OnDrawGizmosSelected()
	{
		if (!Application.isPlaying)
		{
			Gizmos.color = Color.blue;
			DebugUtil.GizmosDrawArrow(m_Effector.position, m_Effector.position + EffectorForward);
		}
		else if (!m_LastForce.sqrMagnitude.Approximately(0f))
		{
			Gizmos.color = Color.red;
			DebugUtil.GizmosDrawArrow(m_Effector.position, m_Effector.position + m_LastForce * Globals.inst.m_WheelGizmoForceScale);
		}
	}
}
