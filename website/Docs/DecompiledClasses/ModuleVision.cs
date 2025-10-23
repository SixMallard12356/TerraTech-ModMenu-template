#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Visible))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleVision : Module
{
	private enum PlaneCullMode
	{
		None,
		Either,
		Both
	}

	public float visionConeAngle = 180f;

	public float visionRange = 10f;

	private float m_Range;

	private Plane m_ConeCullPlaneA;

	private Plane m_ConeCullPlaneB;

	private PlaneCullMode m_PlaneCullMode;

	public float Range
	{
		get
		{
			if (m_Range != 0f)
			{
				return m_Range;
			}
			return visionRange;
		}
	}

	public void SetRange(float range)
	{
		m_Range = range;
	}

	public bool CanSee(Visible item, out float distSq)
	{
		Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
		Vector3 centrePosition = item.centrePosition;
		distSq = (centrePosition - centreOfMassWorld).sqrMagnitude;
		float num = 0f;
		float num2;
		if (item.type == ObjectTypes.Vehicle)
		{
			float magnitude = item.tank.blockBounds.extents.magnitude;
			num2 = Range + magnitude;
			num2 *= num2;
			num = 0f - magnitude;
		}
		else
		{
			num2 = Range * Range;
		}
		if (distSq > num2)
		{
			return false;
		}
		if (item.type == ObjectTypes.Vehicle && !item.tank.BoundsIntersectSphere(centreOfMassWorld, Range))
		{
			return false;
		}
		switch (m_PlaneCullMode)
		{
		case PlaneCullMode.Either:
			if (!(m_ConeCullPlaneA.GetDistanceToPoint(centrePosition) > num))
			{
				return m_ConeCullPlaneB.GetDistanceToPoint(centrePosition) > num;
			}
			return true;
		case PlaneCullMode.Both:
			if (m_ConeCullPlaneA.GetDistanceToPoint(centrePosition) > num)
			{
				return m_ConeCullPlaneB.GetDistanceToPoint(centrePosition) > num;
			}
			return false;
		default:
			d.Assert(m_PlaneCullMode == PlaneCullMode.None);
			return true;
		}
	}

	private void UpdateConeCullPlanes()
	{
		if (visionConeAngle == 0f || visionConeAngle >= 360f)
		{
			m_PlaneCullMode = PlaneCullMode.None;
			return;
		}
		Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
		float f = (visionConeAngle * 0.5f - 90f) * ((float)Math.PI / 180f);
		float num = Mathf.Sin(f);
		float z = Mathf.Cos(f);
		m_ConeCullPlaneA = new Plane(base.block.trans.TransformDirection(new Vector3(num, 0f, z)), centreOfMassWorld);
		m_ConeCullPlaneB = new Plane(base.block.trans.TransformDirection(new Vector3(0f - num, 0f, z)), centreOfMassWorld);
		m_PlaneCullMode = ((!(visionConeAngle < 180f)) ? PlaneCullMode.Either : PlaneCullMode.Both);
	}

	private void OnAttached()
	{
		base.block.tank.Vision.AddVision(this);
	}

	private void OnDetaching()
	{
		base.block.tank.Vision.RemoveVision(this);
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
	}

	private void OnSpawn()
	{
		m_Range = 0f;
	}

	private void OnFixedUpdate()
	{
		if ((bool)base.block.tank)
		{
			UpdateConeCullPlanes();
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		if (m_PlaneCullMode != PlaneCullMode.None)
		{
			Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
			DebugUtil.GizmosDrawArrow(centreOfMassWorld, centreOfMassWorld + m_ConeCullPlaneA.normal);
			DebugUtil.GizmosDrawArrow(centreOfMassWorld, centreOfMassWorld + m_ConeCullPlaneB.normal);
		}
		if (!base.block)
		{
			return;
		}
		Matrix4x4 localToWorldMatrix = base.block.transform.localToWorldMatrix;
		if (Application.isPlaying)
		{
			localToWorldMatrix.m03 = base.block.centreOfMassWorld.x;
			localToWorldMatrix.m13 = base.block.centreOfMassWorld.y;
			localToWorldMatrix.m23 = base.block.centreOfMassWorld.z;
		}
		Gizmos.matrix = localToWorldMatrix;
		Vector3 vector = Vector3.zero;
		int num = Mathf.CeilToInt(visionConeAngle / 45f);
		for (int i = -num; i <= num; i++)
		{
			float f = (float)Math.PI / 180f * (float)i * visionConeAngle / (float)(num * 2);
			Vector3 vector2 = new Vector3(0f, 1f, 0f) + Range * new Vector3(Mathf.Sin(f), 0f, Mathf.Cos(f));
			Gizmos.DrawLine(Vector3.zero, vector2);
			if (vector != Vector3.zero)
			{
				Gizmos.DrawLine(vector, vector2);
			}
			vector = vector2;
		}
	}
}
