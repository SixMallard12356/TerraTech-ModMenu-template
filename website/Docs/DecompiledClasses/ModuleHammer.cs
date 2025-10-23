using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleHammer : ModuleMeleeWeapon
{
	[SerializeField]
	[FormerlySerializedAs("actuator")]
	private Animation m_Actuator;

	[SerializeField]
	private float impactDamage;

	private AnimEvent m_AnimEvent;

	private AnimationState m_OperatingState;

	private bool m_IsTriggered;

	protected override bool PlaySFXWhileActive => false;

	private void OnHammerTriggered(int value)
	{
		m_IsTriggered = true;
		Invoke("ResetTrigger", 0.25f);
		TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(base.block, m_SFXType, m_OperatingState.length);
		base.block.tank.TechAudio.PlayOneshot(data);
	}

	private void ResetTrigger()
	{
		m_IsTriggered = false;
	}

	private void ReInitialiseAllAnimOffsets()
	{
		List<ModuleHammer> list = base.block.tank.blockman.IterateBlockComponents<ModuleHammer>().ToList();
		float num = 1f / (float)list.Count;
		float num2 = 0f;
		foreach (ModuleHammer item in list)
		{
			item.SetAnimOffset(num2);
			num2 += num;
		}
	}

	private void SetAnimOffset(float offset)
	{
		m_OperatingState.time = m_OperatingState.length * offset;
	}

	protected override void HandleLastFrameCollisions()
	{
		if (m_LastTargetCollisionsInfo.Count == 0)
		{
			return;
		}
		for (int i = 0; i < m_LastTargetCollisionsInfo.Count; i++)
		{
			if (!m_LastTargetCollisionsInfo[i].IsNull && m_IsTriggered)
			{
				TryDoDamageToFrameCollision(m_LastTargetCollisionsInfo[i], impactDamage);
				Singleton.Manager<ManSFX>.inst.PlayImpactSFX(base.block.tank, ManSFX.WeaponImpactSfxType.Hammer, m_LastTargetCollisionsInfo[i].TargetVisible.damageable, m_LastTargetCollisionsInfo[i].Point, m_LastTargetCollisionsInfo[i].OtherCol);
				TriggerHitVFX(m_LastTargetCollisionsInfo[i].Point, m_LastTargetCollisionsInfo[i].Normal);
				ResetTrigger();
				break;
			}
		}
	}

	public override float GetHitDamage()
	{
		return impactDamage;
	}

	public override float GetHitsPerSec()
	{
		float length = m_Actuator.clip.length;
		return 1f / length;
	}

	protected override void OnAttached()
	{
		base.OnAttached();
		m_Actuator.Play();
		m_OperatingState.enabled = false;
		ReInitialiseAllAnimOffsets();
		if ((bool)m_AnimEvent)
		{
			m_AnimEvent.HandleEvent.Subscribe(OnHammerTriggered);
		}
	}

	protected override void OnDetaching()
	{
		m_Actuator.Stop();
		m_Actuator.transform.localPosition = Vector3.zero;
		if ((bool)m_AnimEvent)
		{
			m_AnimEvent.HandleEvent.Unsubscribe(OnHammerTriggered);
		}
		base.OnDetaching();
	}

	private void OnPool()
	{
		m_AnimEvent = GetComponentInChildren<AnimEvent>();
		foreach (AnimationState item in m_Actuator)
		{
			if (m_OperatingState == null)
			{
				m_OperatingState = item;
			}
		}
	}

	protected override void SetWeaponActive(bool state, bool instantly = false)
	{
		base.SetWeaponActive(state, instantly);
		m_OperatingState.enabled = state;
	}
}
