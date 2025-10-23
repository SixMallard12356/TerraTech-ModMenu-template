#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Projectile))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class SeekingProjectile : MonoBehaviour
{
	public enum TargetType
	{
		CenterOfMass,
		BestAimCabOrCentre,
		RandomBlockOnVehicle
	}

	[SerializeField]
	[FormerlySerializedAs("visionConeAngle")]
	private float m_VisionConeAngle = 150f;

	[SerializeField]
	[FormerlySerializedAs("visionRange")]
	private float m_VisionRange = 20f;

	[SerializeField]
	[FormerlySerializedAs("turnSpeed")]
	private float m_TurnSpeed = 1f;

	[SerializeField]
	[FormerlySerializedAs("seekType")]
	private ObjectTypes m_SeekType;

	[FormerlySerializedAs("updateVisibleInterval")]
	[SerializeField]
	private float m_UpdateVisibleInterval = 0.3f;

	[SerializeField]
	private float m_ActivationDelay;

	[SerializeField]
	private bool m_LockOntoTarget = true;

	[SerializeField]
	private bool m_ApplyRotationTowardsTarget;

	[SerializeField]
	private TargetType m_TargetingType;

	private float m_NetSyncInterval = 0.1f;

	private Transform m_MyTransform;

	private Projectile m_MyProjectile;

	private Visible.WeakReference m_Target = new Visible.WeakReference();

	private Visible.WeakReference m_OptionalSubTargetAimPoint = new Visible.WeakReference();

	private bool m_HasTargetLock;

	private float m_UpdateVisibleTimeout;

	private int m_IgnoreTeam;

	private Func<Visible, bool> m_IgnorePredicate;

	private Bitfield<ObjectTypes> m_SeekTypeBitField;

	private float m_LastNetSyncTime;

	private List<Visible.ConeFiltered> m_Visibles = new List<Visible.ConeFiltered>();

	private static ManVisible.CachedSearchIterator s_TempVisiblesIterator = new ManVisible.CachedSearchIterator();

	private static List<Visible> s_TempVisList = new List<Visible>();

	private bool IgnoreNonEnemyTank(Visible v)
	{
		if (!v.tank)
		{
			return false;
		}
		return !v.tank.IsEnemy(m_IgnoreTeam);
	}

	private void UpdateVision()
	{
		m_Visibles.Clear();
		if (m_SeekType == ObjectTypes.Vehicle && (bool)m_MyProjectile.Shooter)
		{
			m_IgnoreTeam = m_MyProjectile.Shooter.Team;
		}
		Vector3 position = m_MyTransform.position;
		Vector3 forward = m_MyTransform.forward;
		Vector3 scenePos = position;
		float num = m_VisionRange;
		if (m_VisionConeAngle < 150f)
		{
			num = m_VisionRange * 0.5f;
			scenePos = position + forward * num;
		}
		ManVisible.CachedSearchIterator cachedSearchIterator;
		if (m_SeekType == ObjectTypes.Vehicle)
		{
			foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
			{
				s_TempVisList.Add(currentTech.visible);
			}
			cachedSearchIterator = s_TempVisiblesIterator;
			cachedSearchIterator.Init(s_TempVisList);
		}
		else
		{
			cachedSearchIterator = Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadiusCached(scenePos, num, m_SeekTypeBitField);
		}
		Visible.ConeFilterOptim(cachedSearchIterator, m_Visibles, m_VisionRange, m_VisionConeAngle, position, forward, ignoreY: false, m_IgnorePredicate);
		s_TempVisList.Clear();
	}

	private void PickNewTarget()
	{
		m_Target.Set(null);
		m_OptionalSubTargetAimPoint.Set(null);
		float num = 0f;
		float num2 = 360f;
		float num3 = float.MaxValue;
		foreach (Visible.ConeFiltered visible in m_Visibles)
		{
			if (!visible.visible.IsNotNull() || !visible.visible.isActive)
			{
				continue;
			}
			if (visible.angleDot > num)
			{
				m_Target.Set(visible.visible);
				num = visible.angleDot;
				num2 = Mathf.Acos(visible.angleDot);
				num3 = visible.distSq;
			}
			else if (visible.distSq < num3)
			{
				float num4 = Mathf.Acos(visible.angleDot);
				if (num4 - 0.08726646f < num2)
				{
					m_Target.Set(visible.visible);
					num = visible.angleDot;
					num2 = num4;
					num3 = visible.distSq;
				}
			}
		}
		if (m_LockOntoTarget)
		{
			m_HasTargetLock = m_Target.Get().IsNotNull();
		}
	}

	public void ClientSetTarget(Visible target, TankBlock block)
	{
		m_Target.Set(target);
		if (block.IsNotNull())
		{
			m_OptionalSubTargetAimPoint.Set(block.visible);
		}
		else
		{
			m_OptionalSubTargetAimPoint.Set(null);
		}
	}

	private void OnPool()
	{
		m_MyTransform = base.transform;
		m_MyProjectile = GetComponent<Projectile>();
		m_SeekTypeBitField = new Bitfield<ObjectTypes>((int)m_SeekType);
		if (m_SeekType == ObjectTypes.Vehicle)
		{
			m_IgnorePredicate = IgnoreNonEnemyTank;
		}
	}

	private void OnSpawn()
	{
		m_UpdateVisibleTimeout = Time.time + m_ActivationDelay + UnityEngine.Random.Range(0f, 0.15f);
		m_Target.Set(null);
		m_OptionalSubTargetAimPoint.Set(null);
		m_HasTargetLock = false;
	}

	private void Update()
	{
		Visible currentTarget = GetCurrentTarget();
		if (m_HasTargetLock)
		{
			if (currentTarget.IsNull() || !currentTarget.isActive)
			{
				m_HasTargetLock = false;
			}
			else
			{
				Vector3 vector = GetTargetAimPosition() - m_MyTransform.position;
				float num = Vector3.Dot(m_MyTransform.forward, vector.normalized);
				m_HasTargetLock = num >= 0f;
			}
		}
		if (ManNetwork.IsHost && !m_HasTargetLock && GetManualTarget().IsNull() && Time.time > m_UpdateVisibleTimeout)
		{
			m_UpdateVisibleTimeout = Time.time + m_UpdateVisibleInterval;
			UpdateVision();
			PickNewTarget();
		}
		if (m_SeekType == ObjectTypes.Vehicle && m_TargetingType == TargetType.RandomBlockOnVehicle && m_OptionalSubTargetAimPoint.Get().IsNull() && currentTarget.IsNotNull() && currentTarget.tank.IsNotNull())
		{
			int blockCount = currentTarget.tank.blockman.blockCount;
			if (blockCount > 0)
			{
				int index = UnityEngine.Random.Range(0, blockCount);
				TankBlock blockWithIndex = currentTarget.tank.blockman.GetBlockWithIndex(index);
				m_OptionalSubTargetAimPoint.Set(blockWithIndex.visible);
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && m_MyProjectile.Shooter != null && m_MyProjectile.Shooter.netTech != null && Time.time >= m_LastNetSyncTime + m_NetSyncInterval)
		{
			m_LastNetSyncTime = Time.time;
			m_MyProjectile.Shooter.netTech.ServerQueueProjectileUpdate(m_MyProjectile.ShortlivedUID, m_Target.Get(), m_OptionalSubTargetAimPoint, base.transform.position, base.transform.rotation);
		}
	}

	private void FixedUpdate()
	{
		if (GetCurrentTarget().IsNotNull())
		{
			Vector3 vector = GetTargetAimPosition() - m_MyTransform.position;
			Vector3 normalized = Vector3.Cross(m_MyProjectile.rbody.velocity, vector).normalized;
			float b = Vector3.Angle(m_MyProjectile.trans.forward, vector);
			Quaternion quaternion = Quaternion.AngleAxis(Mathf.Min(m_TurnSpeed * Time.deltaTime, b), normalized);
			m_MyProjectile.rbody.velocity = quaternion * m_MyProjectile.rbody.velocity;
			if (m_ApplyRotationTowardsTarget)
			{
				Quaternion rot = quaternion * m_MyProjectile.rbody.rotation;
				m_MyProjectile.rbody.MoveRotation(rot);
			}
		}
	}

	private Visible GetManualTarget()
	{
		Visible visible = m_MyProjectile.Shooter?.Weapons?.GetManualTarget();
		if (visible != null && !visible.gameObject.activeInHierarchy)
		{
			visible = null;
		}
		return visible;
	}

	private Visible GetCurrentTarget()
	{
		Visible manualTarget = GetManualTarget();
		if (!manualTarget.IsNotNull())
		{
			return m_Target.Get();
		}
		return manualTarget;
	}

	private Vector3 GetTargetAimPosition()
	{
		Vector3 result;
		if (m_OptionalSubTargetAimPoint.Get().IsNotNull())
		{
			result = m_OptionalSubTargetAimPoint.Get().centrePosition;
		}
		else
		{
			Visible currentTarget = GetCurrentTarget();
			if (currentTarget.IsNotNull())
			{
				TargetType targetingType = m_TargetingType;
				if (targetingType != TargetType.CenterOfMass && (uint)(targetingType - 1) <= 1u)
				{
					result = currentTarget.GetAimPoint(m_MyTransform.position);
				}
				else
				{
					result = currentTarget.centrePosition;
					d.Assert(m_TargetingType == TargetType.CenterOfMass, "SeekingProjectile - Unhandled targeting type: " + m_TargetingType);
				}
			}
			else
			{
				d.LogError("SeekingProjectile.GetTargetAimPosition - Target was null!");
				result = Vector3.zero;
			}
		}
		return result;
	}

	private void OnDrawGizmos()
	{
		m_Target.Get().IsNotNull();
	}
}
