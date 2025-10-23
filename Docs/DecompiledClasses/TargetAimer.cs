#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class TargetAimer : MonoBehaviour
{
	private Func<Vector3, Vector3> AimDelegate;

	private List<GimbalAimer> m_GimbalAimers = new List<GimbalAimer>();

	private TankBlock m_Block;

	private Vector3 m_TargetPosition;

	private float m_ChangeTargetTimeout;

	private float m_ChangeTargetInteval = 0.5f;

	public bool HasTarget => Target.IsNotNull();

	public Visible Target { get; private set; }

	public void Init(TankBlock block, float changeTargetInterval, Func<Vector3, Vector3> aimDelegate)
	{
		if (m_Block == null)
		{
			m_Block = block;
			m_ChangeTargetInteval = changeTargetInterval;
			AimDelegate = aimDelegate;
			return;
		}
		d.Assert(m_Block == block);
		m_ChangeTargetInteval = Mathf.Min(changeTargetInterval, m_ChangeTargetInteval);
		if (aimDelegate != null)
		{
			d.Assert(AimDelegate == null, $"Multiple modules trying to set an aim delegate on the same TargetAimer for block {m_Block.name}");
			AimDelegate = aimDelegate;
		}
	}

	public void Reset()
	{
		Target = null;
		m_TargetPosition = Vector3.zero;
		m_ChangeTargetTimeout = 0f;
	}

	public bool UpdateAndAimAtTarget(float rotateSpeed)
	{
		UpdateTarget();
		return AimAtTarget(rotateSpeed);
	}

	public bool UpdateAndCanAimAtTarget()
	{
		UpdateTarget();
		return CanAimAtTarget();
	}

	public void AimAtWorldPos(Vector3 position, float rotateSpeed)
	{
		for (int i = 0; i < m_GimbalAimers.Count; i++)
		{
			m_GimbalAimers[i].Aim(position, rotateSpeed);
		}
	}

	public void ResetGimbalAngles()
	{
		for (int i = 0; i < m_GimbalAimers.Count; i++)
		{
			m_GimbalAimers[i].ResetAngles();
		}
	}

	private Visible GetManualTarget()
	{
		TankBlock block = m_Block;
		Tank tank = (block ? block.tank : null);
		TechWeapon techWeapon = (tank ? tank.Weapons : null);
		if (!techWeapon)
		{
			return null;
		}
		return techWeapon.GetManualTarget();
	}

	private void UpdateTarget()
	{
		Visible manualTarget = GetManualTarget();
		if ((bool)manualTarget)
		{
			Target = manualTarget;
		}
		else if (HasTarget && (!Target.isActive || Time.time > m_ChangeTargetTimeout))
		{
			Target = null;
		}
		if (Target == null && m_Block.tank.control.targetType != ObjectTypes.Null && (!HasTarget || !m_Block.tank.Vision.CanSee(Target)))
		{
			if (m_Block.tank.control.targetType == ObjectTypes.Vehicle)
			{
				Target = m_Block.tank.Vision.GetFirstVisibleTechIsEnemy(m_Block.tank.Team);
			}
			else
			{
				Target = m_Block.tank.Vision.GetFirstVisible();
			}
			m_ChangeTargetTimeout = Time.time + m_ChangeTargetInteval;
		}
		if (HasTarget)
		{
			m_TargetPosition = Target.GetAimPoint(m_Block.trans.position);
		}
	}

	private bool AimAtTarget(float rotateSpeed)
	{
		Vector3 targetWorld = m_TargetPosition;
		bool flag = true;
		if ((bool)Target && AimDelegate != null)
		{
			targetWorld = AimDelegate(m_TargetPosition);
		}
		for (int i = 0; i < m_GimbalAimers.Count; i++)
		{
			if ((bool)Target && flag)
			{
				flag = m_GimbalAimers[i].Aim(targetWorld, rotateSpeed);
			}
			else
			{
				m_GimbalAimers[i].AimDefault(rotateSpeed);
			}
		}
		return flag;
	}

	private bool CanAimAtTarget()
	{
		Vector3 targetWorld = m_TargetPosition;
		bool flag = true;
		if ((bool)Target && AimDelegate != null)
		{
			targetWorld = AimDelegate(m_TargetPosition);
		}
		for (int i = 0; i < m_GimbalAimers.Count; i++)
		{
			if ((bool)Target && flag)
			{
				flag = m_GimbalAimers[i].CanAim(targetWorld);
			}
		}
		return flag;
	}

	private void OnPool()
	{
		m_GimbalAimers = new List<GimbalAimer>(GetComponentsInChildren<GimbalAimer>(includeInactive: true));
	}
}
