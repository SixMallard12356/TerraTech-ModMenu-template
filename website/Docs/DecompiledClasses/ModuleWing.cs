#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleWing : Module
{
	[Serializable]
	public class Aerofoil
	{
		[Tooltip("The origin point of this aerofoil's lift calculations (takes rotation into account as well)")]
		public Transform trans;

		public AnimationCurve liftCurve;

		public float liftStrength = 1f;

		public Spinner flap;

		public float flapAngleRangeActual = 30f;

		public float flapAngleRangeVisual = 30f;

		public float flapTurnSpeed = 1f;
	}

	private class AerofoilState
	{
		public Vector3 rotationContribution;

		public float attackAngleDamped;

		public float attackAngleModifier;

		public float flapActualToVisual;

		public float flapControl;

		public Vector3 force { get; private set; }

		public float liftCoeff { get; private set; }

		public Vector3 CalculateForce(Vector3 airVelocity, Aerofoil aerofoil, float attackAngleDamping, float attackAngleModifier, ref float attackAngleDamped, out float liftCoeff)
		{
			liftCoeff = 0f;
			Vector3 vector = Vector3.Dot(airVelocity, aerofoil.trans.right) * aerofoil.trans.right;
			Vector3 lhs = airVelocity - vector;
			float magnitude = lhs.magnitude;
			d.Assert(Globals.inst.m_WingAirspeedIgnore >= 0.01f);
			if (magnitude < Globals.inst.m_WingAirspeedIgnore)
			{
				return Vector3.zero;
			}
			float num = Mathf.Acos(Mathf.Clamp(Vector3.Dot(lhs, aerofoil.trans.up) / magnitude, -1f, 1f)) * 57.29578f - 90f;
			num += attackAngleModifier;
			attackAngleDamped = Mathf.Lerp(attackAngleDamped, num, Mathf.Min(attackAngleDamping * Time.deltaTime, 1f));
			liftCoeff = aerofoil.liftCurve.Evaluate(attackAngleDamped);
			float num2 = magnitude * magnitude * liftCoeff * aerofoil.liftStrength;
			if (Mathf.Abs(num2) < Globals.inst.m_WingLiftIgnore)
			{
				return Vector3.zero;
			}
			return num2 * aerofoil.trans.up;
		}

		public void UpdateState(Aerofoil aerofoil, TankBlock block, float attackAngleDamping, Vector3 foilPos)
		{
			Vector3 pointVelocity = block.tank.rbody.GetPointVelocity(foilPos);
			force = CalculateForce(pointVelocity, aerofoil, attackAngleDamping, attackAngleModifier, ref attackAngleDamped, out var num);
			liftCoeff = num;
		}
	}

	[SerializeField]
	public float m_AttackAngleDamping = 1f;

	[SerializeField]
	public float m_TrailMinVelocity = 8f;

	[SerializeField]
	public float m_TrailAlphaStrength = 0.01f;

	[SerializeField]
	public float m_TrailFadeSpeed = 0.5f;

	[SerializeField]
	public Aerofoil[] m_Aerofoils;

	private List<SmokeTrail> m_WingTrails;

	private AerofoilState[] m_FoilState;

	private float m_AverageLiftCoeff;

	private WarningHolder m_Warning;

	public float LiftStrength => m_Aerofoils[0].liftStrength;

	private void DriveControlInput(TankControl.ControlState driveData)
	{
		for (int i = 0; i < m_Aerofoils.Length; i++)
		{
			AerofoilState aerofoilState = m_FoilState[i];
			aerofoilState.flapControl = Vector3.Dot(driveData.InputRotation, aerofoilState.rotationContribution);
			aerofoilState.flapControl = Mathf.Clamp(aerofoilState.flapControl, -1f, 1f);
		}
	}

	private void OnAxesWarning(bool show, int inputAxesBitfield)
	{
		int num = 0;
		for (int i = 0; i < m_FoilState.Length; i++)
		{
			num |= TankControl.GetInputAxisBitfieldForRotation(m_FoilState[i].rotationContribution);
		}
		if (show && num != 0 && (inputAxesBitfield & num) == 0)
		{
			m_Warning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleNoControls, LocalisationEnums.Warnings.warningMsgNoControls, 12);
		}
		else
		{
			m_Warning.Remove();
		}
	}

	private float UpdateTrailAlpha(float currentAlpha)
	{
		if (!base.block.tank)
		{
			return 0f;
		}
		float magnitude = base.block.tank.rbody.GetPointVelocity(base.transform.position).magnitude;
		if (magnitude > m_TrailMinVelocity)
		{
			return (magnitude - m_TrailMinVelocity) * m_TrailAlphaStrength * Mathf.Abs(m_AverageLiftCoeff);
		}
		return Mathf.Clamp(currentAlpha - Time.deltaTime * m_TrailFadeSpeed, 0f, 1f);
	}

	private void OnAttached()
	{
		AerofoilState[] foilState = m_FoilState;
		foreach (AerofoilState obj in foilState)
		{
			obj.attackAngleDamped = 0f;
			obj.attackAngleModifier = 0f;
			obj.flapControl = 0f;
		}
		base.block.tank.control.driveControlEvent.Subscribe(DriveControlInput);
		base.block.tank.control.axesWarningEvent.Subscribe(OnAxesWarning);
		base.block.tank.ResetEvent.Subscribe(OnResetTank);
		base.block.tank.ResetPhysicsEvent.Subscribe(OnResetTechPhysics);
		base.block.tank.TechAudio.AddWing(this);
		foreach (SmokeTrail wingTrail in m_WingTrails)
		{
			wingTrail.enabled = true;
		}
	}

	private void OnDetaching()
	{
		base.block.tank.control.driveControlEvent.Unsubscribe(DriveControlInput);
		base.block.tank.control.axesWarningEvent.Unsubscribe(OnAxesWarning);
		base.block.tank.ResetEvent.Unsubscribe(OnResetTank);
		base.block.tank.ResetPhysicsEvent.Unsubscribe(OnResetTechPhysics);
		base.block.tank.TechAudio.RemoveWing(this);
		foreach (SmokeTrail wingTrail in m_WingTrails)
		{
			wingTrail.Reset();
			wingTrail.enabled = false;
		}
		m_Warning.Remove();
	}

	private void OnResetTank(int unused = 0)
	{
		foreach (SmokeTrail wingTrail in m_WingTrails)
		{
			wingTrail.Reset();
		}
	}

	private void OnResetTechPhysics()
	{
		_ = base.block.tank.rootBlockTrans.forward;
		for (int i = 0; i < m_Aerofoils.Length; i++)
		{
			Aerofoil aerofoil = m_Aerofoils[i];
			AerofoilState aerofoilState = m_FoilState[i];
			aerofoilState.rotationContribution = Vector3.zero;
			if ((bool)aerofoil.flap)
			{
				TankControl.GetInputEffect(base.block.tank, aerofoil.trans.position, aerofoil.trans.up, out aerofoilState.rotationContribution, out var _, TankControl.ControlContribution.Rotation);
				aerofoilState.flapActualToVisual = (0f - aerofoil.flapAngleRangeVisual) / aerofoil.flapAngleRangeActual;
			}
		}
	}

	private void OnPool()
	{
		m_FoilState = new AerofoilState[m_Aerofoils.Length];
		for (int i = 0; i < m_Aerofoils.Length; i++)
		{
			d.Assert(m_Aerofoils[i].trans.IsChildOf(base.block.trans), "aerofoils must be children of block transform (" + base.block.name + ")");
			if ((bool)m_Aerofoils[i].flap)
			{
				d.Assert(m_Aerofoils[i].flap.transform != m_Aerofoils[i].trans && !m_Aerofoils[i].trans.IsChildOf(m_Aerofoils[i].flap.transform), "aerofoil's transform for force application must not be on the flap (" + base.block.name + ")");
			}
			m_FoilState[i] = new AerofoilState();
		}
		m_WingTrails = new List<SmokeTrail>(GetComponentsInChildren<SmokeTrail>(includeInactive: true));
		foreach (SmokeTrail wingTrail in m_WingTrails)
		{
			wingTrail.UpdateAlphaFn = UpdateTrailAlpha;
		}
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.NoControlsMapped);
	}

	private void OnUpdate()
	{
		for (int i = 0; i < m_Aerofoils.Length; i++)
		{
			Aerofoil aerofoil = m_Aerofoils[i];
			AerofoilState aerofoilState = m_FoilState[i];
			aerofoilState.attackAngleModifier = Mathf.MoveTowards(aerofoilState.attackAngleModifier, aerofoilState.flapControl * aerofoil.flapAngleRangeActual, aerofoil.flapTurnSpeed);
			if ((bool)aerofoil.flap)
			{
				aerofoil.flap.SetAngle(aerofoilState.attackAngleModifier * aerofoilState.flapActualToVisual);
				aerofoilState.flapControl = 0f;
			}
		}
	}

	public Vector3 QueryLiftInConditions(Vector3 airVelocity, out Vector3 localCenterOfLift, float attackAngleModifier = 0f, bool useCurrentAttackAngleDampState = false)
	{
		Vector3 zero = Vector3.zero;
		localCenterOfLift = Vector3.zero;
		float num = 0f;
		Vector3 vector = base.block.tank.rbody.position - base.block.tank.trans.position;
		for (int i = 0; i < m_Aerofoils.Length; i++)
		{
			Vector3 position = m_Aerofoils[i].trans.position + vector;
			float attackAngleDamped = (useCurrentAttackAngleDampState ? m_FoilState[i].attackAngleDamped : 0f);
			float liftCoeff;
			Vector3 vector2 = m_FoilState[i].CalculateForce(airVelocity, m_Aerofoils[i], useCurrentAttackAngleDampState ? m_AttackAngleDamping : 0f, attackAngleModifier, ref attackAngleDamped, out liftCoeff);
			if (vector2.magnitude != 0f)
			{
				zero += vector2;
				num += vector2.magnitude;
				localCenterOfLift += base.block.trans.InverseTransformPoint(position) * vector2.magnitude;
			}
		}
		localCenterOfLift /= ((num == 0f) ? 1f : num);
		return zero;
	}

	private void OnFixedUpdate()
	{
		if ((bool)base.block.tank)
		{
			m_AverageLiftCoeff = 0f;
			Vector3 vector = base.block.tank.rbody.position - base.block.tank.trans.position;
			for (int i = 0; i < m_Aerofoils.Length; i++)
			{
				Vector3 vector2 = m_Aerofoils[i].trans.position + vector;
				m_FoilState[i].UpdateState(m_Aerofoils[i], base.block, m_AttackAngleDamping, vector2);
				m_AverageLiftCoeff += m_FoilState[i].liftCoeff;
				base.block.tank.rbody.AddForceAtPosition(m_FoilState[i].force, vector2);
			}
			m_AverageLiftCoeff /= m_Aerofoils.Length;
		}
	}

	private void OnDrawGizmosSelected()
	{
		for (int i = 0; i < m_Aerofoils.Length; i++)
		{
			Aerofoil aerofoil = m_Aerofoils[i];
			Gizmos.color = Color.cyan;
			Vector3 position = aerofoil.trans.position;
			Vector3 vector = aerofoil.trans.right * 0.25f;
			DebugUtil.GizmosDrawArrow(position - vector * 2f, position - vector * 2f + aerofoil.trans.forward);
			DebugUtil.GizmosDrawArrow(position - vector, position - vector + aerofoil.trans.forward);
			DebugUtil.GizmosDrawArrow(position, position + aerofoil.trans.forward);
			DebugUtil.GizmosDrawArrow(position + vector, position + vector + aerofoil.trans.forward);
			DebugUtil.GizmosDrawArrow(position + vector * 2f, position + vector * 2f + aerofoil.trans.forward);
			if (Application.isPlaying)
			{
				Gizmos.color = Color.red;
				position = aerofoil.trans.position;
				DebugUtil.GizmosDrawArrow(position, position + m_FoilState[i].force * Globals.inst.m_WheelGizmoForceScale);
			}
		}
	}
}
