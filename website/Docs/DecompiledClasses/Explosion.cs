using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class Explosion : MonoBehaviour
{
	private struct HitDesc
	{
		public Visible visible;

		public Vector3 point;

		public Vector3 impulse;

		public float sqDist;

		public HitDesc(Visible v, Vector3 p, float sd, Vector3 i)
		{
			visible = v;
			point = p;
			sqDist = sd;
			impulse = i;
		}
	}

	public float m_EffectRadius = 1f;

	public float m_EffectRadiusMaxStrength = 0.5f;

	public float m_MaxDamageStrength = 1f;

	public float m_MaxImpulseStrength = 1f;

	[SerializeField]
	private ManDamage.DamageType m_DamageType = ManDamage.DamageType.Blast;

	[SerializeField]
	private ManSFX.ExplosionType m_ExplosionType;

	[SerializeField]
	private ManSFX.ExplosionSize m_ExplosionSize = ManSFX.ExplosionSize.Medium;

	private Collider[] s_SphereOverlapResultBuffer = new Collider[64];

	private bool m_Once;

	private Tank m_DamageSource;

	private Damageable m_DirectHitTarget;

	private FactionSubTypes m_CorpExplosionAudioType;

	private static Dictionary<int, HitDesc> s_VisibleHits = new Dictionary<int, HitDesc>(100);

	private static Dictionary<int, HitDesc> s_TankHits = new Dictionary<int, HitDesc>(10);

	public bool DoDamage { get; set; }

	private void StoreHit(Dictionary<int, HitDesc> hitDict, Visible visible, Vector3 point, float sqDist, Vector3 impulse)
	{
		if (!hitDict.TryGetValue(visible.GetHashCode(), out var value) || sqDist < value.sqDist)
		{
			hitDict[visible.GetHashCode()] = new HitDesc(visible, point, sqDist, impulse);
		}
	}

	private void GatherVisibleHits()
	{
		int num = 0;
		do
		{
			if (num >= s_SphereOverlapResultBuffer.Length)
			{
				Array.Resize(ref s_SphereOverlapResultBuffer, s_SphereOverlapResultBuffer.Length * 2);
			}
			num = Physics.OverlapSphereNonAlloc(base.transform.position, m_EffectRadius, s_SphereOverlapResultBuffer, Singleton.Manager<ManVisible>.inst.VisiblePickerMaskNoTechs, QueryTriggerInteraction.Ignore);
		}
		while (num >= s_SphereOverlapResultBuffer.Length);
		for (int i = 0; i < num; i++)
		{
			Visible visible = Visible.FindVisibleUpwards(s_SphereOverlapResultBuffer[i]);
			if (visible != null)
			{
				Vector3 centrePosition = visible.centrePosition;
				float sqrMagnitude = (centrePosition - base.transform.position).sqrMagnitude;
				if ((bool)visible && (bool)visible.damageable)
				{
					StoreHit(s_VisibleHits, visible, centrePosition, sqrMagnitude, Vector3.zero);
				}
			}
		}
	}

	private void Explode()
	{
		float a = 1f / (m_EffectRadius * m_EffectRadius);
		float b = 1f / (m_EffectRadiusMaxStrength * m_EffectRadiusMaxStrength);
		s_VisibleHits.Clear();
		s_TankHits.Clear();
		GatherVisibleHits();
		Vector3 position = base.transform.position;
		Dictionary<int, HitDesc>.Enumerator enumerator = s_VisibleHits.GetEnumerator();
		while (enumerator.MoveNext())
		{
			HitDesc value = enumerator.Current.Value;
			float num = ((!(m_DirectHitTarget != null) || !(value.visible.damageable == m_DirectHitTarget)) ? Mathf.InverseLerp(a, b, 1f / value.sqDist) : 1f);
			if (!(num <= 0f))
			{
				Vector3 vector = (value.point - position).normalized * num * m_MaxImpulseStrength;
				float damage = (DoDamage ? (num * m_MaxDamageStrength) : 0f);
				if (ManNetwork.IsHost)
				{
					Singleton.Manager<ManDamage>.inst.DealDamage(value.visible.damageable, damage, m_DamageType, this, m_DamageSource, value.visible.centrePosition, vector);
				}
				if (value.visible.type == ObjectTypes.Block && (bool)value.visible.block.tank)
				{
					StoreHit(s_TankHits, value.visible.block.tank.visible, value.point, value.sqDist, vector);
				}
				else if (value.visible.type != ObjectTypes.Scenery && (bool)value.visible.rbody)
				{
					value.visible.rbody.AddForceAtPosition(vector, value.point, ForceMode.Impulse);
				}
			}
		}
		Dictionary<int, HitDesc>.Enumerator enumerator2 = s_TankHits.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			HitDesc value2 = enumerator2.Current.Value;
			value2.visible.rbody.AddForceAtPosition(value2.impulse, value2.point, ForceMode.Impulse);
		}
		Singleton.Manager<ManSFX>.inst.PlayExplosionSFX(position, m_ExplosionType, m_ExplosionSize, m_CorpExplosionAudioType);
	}

	public void SetDamageSource(Tank sourceTank)
	{
		m_DamageSource = sourceTank;
	}

	public void SetDirectHitTarget(Damageable directHit)
	{
		m_DirectHitTarget = directHit;
	}

	public void SetCorpType(FactionSubTypes corpType)
	{
		m_CorpExplosionAudioType = corpType;
	}

	private void OnSpawn()
	{
		m_Once = true;
		m_DamageSource = null;
		DoDamage = true;
		m_CorpExplosionAudioType = FactionSubTypes.NULL;
		m_DirectHitTarget = null;
	}

	private void FixedUpdate()
	{
		if (m_Once)
		{
			m_Once = false;
			Explode();
		}
	}
}
