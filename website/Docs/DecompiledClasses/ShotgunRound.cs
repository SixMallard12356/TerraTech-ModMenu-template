#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ShotgunRound : WeaponRound
{
	private struct ShotHit
	{
		public Visible visible;

		public Vector3 hitPoint;
	}

	private class RaycastHitDistanceComparer : IComparer<RaycastHit>
	{
		public Vector3 originPosition;

		public int Compare(RaycastHit hitA, RaycastHit hitB)
		{
			return (hitA.point - originPosition).sqrMagnitude.CompareTo((hitB.point - originPosition).sqrMagnitude);
		}
	}

	private Vector3 m_LastFireDirection;

	private Vector3 m_LastFireSpin;

	private static Dictionary<Damageable, ShotHit> s_CollectedTargets = new Dictionary<Damageable, ShotHit>();

	private static HashSet<Damageable> s_HandledBlockers = new HashSet<Damageable>();

	private const float k_AOEMinThreshold = 0.1f;

	private static RaycastHit[] s_RaycastHits = new RaycastHit[32];

	private static RaycastHitDistanceComparer s_RaycastHitDistanceComparer = new RaycastHitDistanceComparer();

	public override void Fire(Vector3 fireDirection, Transform firingOrigin, FireData fireData, ModuleWeapon weapon, Tank shooter = null, bool seekingRounds = false, bool replayRounds = false)
	{
		if (!replayRounds)
		{
			m_LastFireDirection = fireDirection;
		}
		else
		{
			fireDirection = m_LastFireDirection;
		}
		m_LastFireSpin = Vector3.zero;
		if (ManNetwork.IsHost)
		{
			FireDataShotgun fireDataShotgun = fireData as FireDataShotgun;
			d.Assert(fireDataShotgun, "ShotgunShell - Failed to cast FireData as shot from weapon '" + weapon.name + "' to FireDataShotgun! Please update the attached FireData!");
			ApplyDamageToObjectsInArc(fireDataShotgun, weapon, shooter);
		}
		this.Recycle();
	}

	public override void GetVariationParameters(out Vector3 fireDirection, out Vector3 fireSpin)
	{
		fireDirection = m_LastFireDirection;
		fireSpin = m_LastFireSpin;
	}

	public override void SetVariationParameters(Vector3 fireDirection, Vector3 fireSpin)
	{
		m_LastFireDirection = fireDirection;
		m_LastFireSpin = fireSpin;
	}

	private void ApplyDamageToObjectsInArc(FireDataShotgun shotgunFireData, ModuleWeapon weapon, Tank shooter)
	{
		Transform transform = base.transform;
		Vector3 position = transform.position;
		Vector3 forward = transform.forward;
		float num = Mathf.Cos(shotgunFireData.m_ShotArc * ((float)Math.PI / 180f));
		float num2 = shotgunFireData.m_ShotFullDamageRange * shotgunFireData.m_ShotFullDamageRange;
		float num3 = (shotgunFireData.m_ShotMaxRange - shotgunFireData.m_ShotMinRange) / 3f;
		for (int i = 0; i < 3; i++)
		{
			float num4 = shotgunFireData.m_ShotMinRange + (float)i * num3;
			float num5 = num4 + num3;
			float num6 = 2f * num5 * num5;
			float num7 = Mathf.Sqrt(num6 - num6 * num) * 0.5f;
			num4 = Mathf.Min(num4 + num7, num5);
			float maxDistance = num5 - num4;
			int layerMask = Singleton.Manager<ManVisible>.inst.VisiblePickerMaskNoTechs | Globals.inst.layerShieldBulletsFilter.mask;
			int num8;
			while (true)
			{
				num8 = Physics.SphereCastNonAlloc(transform.position + forward * num4, num7, forward, s_RaycastHits, maxDistance, layerMask, QueryTriggerInteraction.Collide);
				if (num8 < s_RaycastHits.Length)
				{
					break;
				}
				Array.Resize(ref s_RaycastHits, num8 * 2);
			}
			int num9 = Globals.inst.layerShieldBulletsFilter;
			for (int j = 0; j < num8; j++)
			{
				RaycastHit raycastHit = s_RaycastHits[j];
				Visible visible = Singleton.Manager<ManVisible>.inst.FindVisible(raycastHit.collider);
				if (!visible.IsNotNull())
				{
					continue;
				}
				Damageable damageable = visible.damageable;
				if (raycastHit.collider.gameObject.layer == num9)
				{
					damageable = raycastHit.collider.GetComponentInParents<Damageable>(thisObjectFirst: true);
					if (damageable.IsNotNull() && !s_CollectedTargets.ContainsKey(damageable))
					{
						Vector3 hitPoint = raycastHit.collider.ClosestPoint(transform.position);
						s_CollectedTargets.Add(damageable, new ShotHit
						{
							visible = visible,
							hitPoint = hitPoint
						});
					}
				}
				else if (damageable.IsNotNull() && !s_CollectedTargets.ContainsKey(damageable))
				{
					s_CollectedTargets.Add(damageable, new ShotHit
					{
						visible = visible,
						hitPoint = visible.centrePosition
					});
				}
			}
		}
		foreach (KeyValuePair<Damageable, ShotHit> s_CollectedTarget in s_CollectedTargets)
		{
			Damageable key = s_CollectedTarget.Key;
			Visible visible2 = s_CollectedTarget.Value.visible;
			if (visible2.IsNull())
			{
				d.LogError(string.Concat("ShotgunRound.ApplyDamageToObjectsInArc: Found damageable (", key, ")with no referenced visible."));
				continue;
			}
			bool flag = visible2.type == ObjectTypes.Block && visible2.damageable != key;
			if (!(visible2.block == null) && !(visible2.block.tank == null) && !(visible2.block.tank != shooter) && !shooter.IsEnemy(visible2.block.tank.Team))
			{
				continue;
			}
			Vector3 vector = (flag ? s_CollectedTarget.Value.hitPoint : visible2.centrePosition);
			Vector3 damageDirection = vector - position;
			float sqrMagnitude = damageDirection.sqrMagnitude;
			float num10 = 1f;
			if (sqrMagnitude > num2)
			{
				num10 = Mathf.InverseLerp(shotgunFireData.m_ShotFullDamageRange, shotgunFireData.m_ShotMaxRange, Mathf.Sqrt(sqrMagnitude));
			}
			if (!(num10 > 0f))
			{
				continue;
			}
			float unblockedAOEDamageFractionAtTarget = GetUnblockedAOEDamageFractionAtTarget(weapon.block.visible, position, key, vector);
			if (unblockedAOEDamageFractionAtTarget > 0f)
			{
				Vector3 hitPosition = vector;
				float num11 = Mathf.Lerp(shotgunFireData.m_MinDamagePercent, shotgunFireData.m_MaxDamagePercent, num10);
				float damage = (float)m_Damage * num11 * unblockedAOEDamageFractionAtTarget;
				float kickbackStrength = num10 * unblockedAOEDamageFractionAtTarget * shotgunFireData.m_TargetKickbackStrength;
				if (visible2.type == ObjectTypes.Scenery)
				{
					kickbackStrength = 0f;
				}
				Singleton.Manager<ManDamage>.inst.DealDamage(key, damage, m_DamageType, weapon, shooter, hitPosition, damageDirection, kickbackStrength, shotgunFireData.m_TargetKickbackDuration);
			}
		}
		s_CollectedTargets.Clear();
	}

	private float GetUnblockedAOEDamageFractionAtTarget(Visible damageSource, Vector3 damageOrigin, Damageable target, Vector3 targetPosition)
	{
		float num = 1f;
		Vector3 vector = targetPosition - damageOrigin;
		float magnitude = vector.magnitude;
		int num2 = 0;
		if (magnitude > 0f)
		{
			num2 = Physics.RaycastNonAlloc(base.transform.position, vector / magnitude, s_RaycastHits, magnitude, -5, QueryTriggerInteraction.Collide);
		}
		if (num2 > 0)
		{
			s_RaycastHitDistanceComparer.originPosition = base.transform.position;
			Array.Sort(s_RaycastHits, 0, num2, s_RaycastHitDistanceComparer);
			for (int i = 0; i < num2; i++)
			{
				RaycastHit raycastHit = s_RaycastHits[i];
				if (raycastHit.collider.gameObject.IsTerrain())
				{
					num = 0f;
					break;
				}
				Damageable damageable = ((raycastHit.collider.gameObject.layer != (int)Globals.inst.layerShieldBulletsFilter) ? Singleton.Manager<ManVisible>.inst.FindVisible(raycastHit.collider)?.damageable : raycastHit.collider.GetComponentInParents<Damageable>(thisObjectFirst: true));
				if (damageable.IsNotNull())
				{
					if (damageable == target)
					{
						break;
					}
					if (!s_HandledBlockers.Contains(damageable) && damageable != null && damageable.GetComponent<Visible>() != damageSource)
					{
						s_HandledBlockers.Add(damageable);
						float aoEDamageBlockPercent = damageable.AoEDamageBlockPercent;
						num *= 1f - aoEDamageBlockPercent;
					}
				}
				if (num < 0.1f)
				{
					num = 0f;
					break;
				}
			}
			s_HandledBlockers.Clear();
		}
		return num;
	}
}
