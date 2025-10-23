using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleDrill : ModuleMeleeWeapon
{
	[SerializeField]
	[FormerlySerializedAs("impactDamageMultiplier")]
	private float m_ImpactDamageMultiplier = 1f;

	[SerializeField]
	[FormerlySerializedAs("damagePerSecond")]
	private float m_DamagePerSecond;

	private Spinner[] m_AllSpinners;

	protected override bool PlaySFXWhileActive => true;

	public override float GetHitDamage()
	{
		return m_DamagePerSecond / 30f;
	}

	public override float GetHitsPerSec()
	{
		return 30f;
	}

	protected override void HandleLastFrameCollisions()
	{
		if (m_LastTargetCollisionsInfo.Count == 0)
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < m_LastTargetCollisionsInfo.Count; i++)
		{
			if (m_LastTargetCollisionsInfo[i].IsNull)
			{
				continue;
			}
			float num = 0f;
			if (m_LastTargetCollisionsInfo[i].WasStartOfCollision)
			{
				num = m_LastTargetCollisionsInfo[i].ImpulseMagnitude * Globals.inst.impactDamageMultiplier * m_ImpactDamageMultiplier;
			}
			else if (!m_LastTargetCollisionsInfo[i].WasStartOfCollision)
			{
				num = m_DamagePerSecond * Time.deltaTime;
			}
			if (num != 0f)
			{
				TryDoDamageToFrameCollision(m_LastTargetCollisionsInfo[i], num);
				if (!flag)
				{
					flag = true;
					TriggerHitVFX(m_LastTargetCollisionsInfo[0].Point, m_LastTargetCollisionsInfo[0].Normal);
				}
			}
		}
	}

	protected override void OnDetaching()
	{
		if (m_AllSpinners != null)
		{
			Spinner[] allSpinners = m_AllSpinners;
			for (int i = 0; i < allSpinners.Length; i++)
			{
				allSpinners[i].SetAngle(0f);
			}
		}
		base.OnDetaching();
	}

	private void OnPool()
	{
		m_AllSpinners = GetComponentsInChildren<Spinner>(includeInactive: true);
	}

	protected override void OnUpdate()
	{
		float num = 0f;
		Spinner[] allSpinners = m_AllSpinners;
		foreach (Spinner obj in allSpinners)
		{
			obj.SetAutoSpin(m_IsActive);
			float speedFraction = obj.SpeedFraction;
			if (speedFraction > num)
			{
				num = speedFraction;
			}
		}
		m_AudioUpdateRate = num;
		base.OnUpdate();
	}
}
