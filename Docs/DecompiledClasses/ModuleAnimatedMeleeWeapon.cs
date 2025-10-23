#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleAnimatedMeleeWeapon : ModuleMeleeWeapon
{
	private class SpeedAnimation
	{
		protected Animation m_Animation;

		protected float m_Speed = -1f;

		public Animation Animation => m_Animation;

		public float Speed
		{
			get
			{
				if (m_Speed != -1f)
				{
					return m_Speed;
				}
				return 0f;
			}
			set
			{
				if (m_Speed == -1f)
				{
					return;
				}
				foreach (AnimationState item in m_Animation)
				{
					item.speed = value;
				}
				m_Speed = value;
			}
		}

		public SpeedAnimation(Animation anim)
		{
			m_Speed = -1f;
			m_Animation = anim;
			m_Animation.playAutomatically = true;
			m_Animation.Play();
			if (m_Animation == null || m_Animation.clip == null)
			{
				d.LogError("Attempted to create 'Speed Animation' from a null Animation or Animation clip! This is not allowed and should be checked for before hand...");
				return;
			}
			foreach (AnimationState item in m_Animation)
			{
				m_Speed = item.speed;
				item.enabled = true;
			}
		}

		public void SetNormalisedTime(float time, bool forceSample = false)
		{
			foreach (AnimationState item in m_Animation)
			{
				item.normalizedTime = time;
			}
			if (forceSample)
			{
				m_Animation.Sample();
			}
		}
	}

	[Serializable]
	public struct FiringPoint
	{
		public Transform Origin;

		public int DamagePerCall;

		public int VFXIndex;

		public float VFXDuration;

		public bool VFXFollowOrigin;
	}

	[SerializeField]
	private FiringPoint[] m_FiringPoints = new FiringPoint[0];

	[SerializeField]
	private bool m_DamageOnAnimationEvent = true;

	[SerializeField]
	private float m_ImpactDamageMultiplier = 1f;

	[Tooltip("Only used when damage isn't applied on animation events")]
	[SerializeField]
	private float m_DamagePerSecond;

	[Tooltip("The gradient at which the animation speed changes when powering up (0-1)")]
	[SerializeField]
	private AnimationCurve m_PowerUpCurve;

	[SerializeField]
	private float m_PowerUpDuration = 1f;

	[SerializeField]
	private float m_PoweredUpAnimationSpeed = 1f;

	[Tooltip("The gradient at which the animation speed changes when powering down (0-1)")]
	[SerializeField]
	private AnimationCurve m_PowerDownCurve;

	[SerializeField]
	private float m_PowerDownDuration = 1f;

	[Tooltip("Whether the animation will only stop at the end of a full loop")]
	[FormerlySerializedAs("m_CompleteLoop")]
	[SerializeField]
	private bool m_AlwaysCompleteAnimationLoop;

	private AnimEvent[] m_AnimEvents;

	private SpeedAnimation[] m_Anims;

	private float m_CurrentAnimSpeed = -1f;

	private float m_TargetAnimSpeed = 1f;

	private bool m_LerpToTargetSpeed;

	private float m_LerpDuration;

	private float m_LerpDurationRemaining;

	private float m_LerpTargetSpeed;

	private float m_LerpStartSpeed;

	private AnimationCurve m_LerpCurve;

	private float m_LerpCountDown;

	private float CurrentAnimSpeed
	{
		get
		{
			if (m_CurrentAnimSpeed != -1f)
			{
				return m_CurrentAnimSpeed;
			}
			return 1f;
		}
		set
		{
			if (m_CurrentAnimSpeed != -1f)
			{
				for (int i = 0; i < m_Anims.Length; i++)
				{
					m_Anims[i].Speed = value;
				}
				m_CurrentAnimSpeed = value;
			}
		}
	}

	protected override bool PlaySFXWhileActive => !m_DamageOnAnimationEvent;

	public override float GetHitDamage()
	{
		if (m_DamageOnAnimationEvent)
		{
			int num = 0;
			int num2 = 0;
			foreach (Animation item in AllAnimationComponents())
			{
				AnimationEvent[] events = item.clip.events;
				foreach (AnimationEvent animationEvent in events)
				{
					if (animationEvent.functionName == "Event")
					{
						int intParameter = animationEvent.intParameter;
						if (intParameter >= 0 && intParameter < m_FiringPoints.Length)
						{
							num = m_FiringPoints[intParameter].DamagePerCall;
							num2++;
						}
						else
						{
							d.LogErrorFormat("FiringPoint param index {0} out of range on {1}", intParameter, base.name);
						}
					}
				}
			}
			return (num2 > 0) ? (num / num2) : 0;
		}
		return m_DamagePerSecond / 30f;
	}

	public override float GetHitsPerSec()
	{
		if (m_DamageOnAnimationEvent)
		{
			int num = 0;
			float num2 = 0f;
			foreach (Animation item in AllAnimationComponents())
			{
				bool flag = false;
				AnimationEvent[] events = item.clip.events;
				foreach (AnimationEvent animationEvent in events)
				{
					if (animationEvent.functionName == "Event")
					{
						flag = true;
						int intParameter = animationEvent.intParameter;
						if (intParameter >= 0 && intParameter < m_FiringPoints.Length)
						{
							num++;
						}
					}
				}
				if (flag)
				{
					d.AssertFormat(num2 == 0f, "We don't yet support multiple animations that have anim events that deal damage (on {0})!", base.name);
					num2 = item.clip.length;
				}
			}
			if (!(num2 > 0f))
			{
				return 0f;
			}
			return (float)num / num2 * m_PoweredUpAnimationSpeed;
		}
		return 30f;
	}

	protected override void SetWeaponActive(bool state, bool instantly = false)
	{
		base.SetWeaponActive(state, instantly);
		m_TargetAnimSpeed = (state ? m_PoweredUpAnimationSpeed : 0f);
		if (instantly)
		{
			CurrentAnimSpeed = m_TargetAnimSpeed;
			m_LerpToTargetSpeed = false;
			if (m_AlwaysCompleteAnimationLoop)
			{
				for (int i = 0; i < m_Anims.Length; i++)
				{
					m_Anims[i].SetNormalisedTime(0f, forceSample: true);
				}
			}
			return;
		}
		m_LerpToTargetSpeed = CurrentAnimSpeed != m_TargetAnimSpeed;
		if (m_LerpToTargetSpeed)
		{
			m_LerpToTargetSpeed = true;
			m_LerpCountDown = 0f;
			m_LerpDuration = (m_IsActive ? m_PowerUpDuration : m_PowerDownDuration);
			m_LerpTargetSpeed = (m_IsActive ? m_PoweredUpAnimationSpeed : 0f);
			m_LerpStartSpeed = (m_IsActive ? 0f : m_PoweredUpAnimationSpeed);
			m_LerpCurve = (m_IsActive ? m_PowerUpCurve : m_PowerDownCurve);
			if (m_AlwaysCompleteAnimationLoop && !m_IsActive)
			{
				float num = GetTotalAnimDuration() - GetCurAnimTime();
				float num2 = m_PowerDownDuration * 0.5f;
				if (CurrentAnimSpeed == 0f)
				{
					m_LerpCountDown = 0f;
					m_LerpDuration = 0f;
				}
				else if (num > num2)
				{
					m_LerpCountDown = num / CurrentAnimSpeed - num2;
				}
				else
				{
					m_LerpCountDown = 0f;
					m_LerpDuration = Mathf.Max(num / CurrentAnimSpeed, 0.1f);
				}
			}
		}
		m_LerpDurationRemaining = m_LerpDuration - m_LerpDuration * m_LerpCurve.FindNearestT(Mathf.InverseLerp(m_LerpStartSpeed, m_LerpTargetSpeed, CurrentAnimSpeed), 100);
	}

	private void LerpAnimationSpeed()
	{
		if (!m_LerpToTargetSpeed)
		{
			return;
		}
		float num = Time.deltaTime;
		if (m_LerpCountDown > 0f)
		{
			if (!(num > m_LerpCountDown))
			{
				m_LerpCountDown -= num;
				return;
			}
			num -= m_LerpCountDown;
			m_LerpCountDown = 0f;
		}
		m_LerpDurationRemaining = Mathf.Max(m_LerpDurationRemaining - num, 0f);
		CurrentAnimSpeed = ((m_LerpDuration > 0f) ? Mathf.Lerp(m_LerpStartSpeed, m_LerpTargetSpeed, m_LerpCurve.Evaluate(1f - m_LerpDurationRemaining / m_LerpDuration)) : m_LerpTargetSpeed);
		if (m_AlwaysCompleteAnimationLoop && !m_IsActive)
		{
			float curAnimTime = GetCurAnimTime();
			if (GetTotalAnimDuration() - curAnimTime < 0.1f || curAnimTime < 0.1f)
			{
				CurrentAnimSpeed = 0f;
				m_LerpToTargetSpeed = false;
			}
			else
			{
				CurrentAnimSpeed = Mathf.Max(CurrentAnimSpeed, m_PoweredUpAnimationSpeed * 0.1f);
			}
		}
		else if (m_LerpDurationRemaining == 0f)
		{
			m_LerpToTargetSpeed = false;
		}
	}

	private void InitAnimations()
	{
		IEnumerable<Animation> enumerable = AllAnimationComponents();
		m_Anims = new SpeedAnimation[enumerable.Count()];
		int num = 0;
		foreach (Animation item in enumerable)
		{
			m_Anims[num] = new SpeedAnimation(item);
		}
		m_CurrentAnimSpeed = m_TargetAnimSpeed;
	}

	private IEnumerable<Animation> AllAnimationComponents()
	{
		return from r in GetComponentsInChildren<Animation>()
			where r.GetClipCount() > 0 && r.clip != null
			select r;
	}

	protected override void HandleLastFrameCollisions()
	{
		if (m_DamageOnAnimationEvent || m_LastTargetCollisionsInfo.Count == 0)
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

	private float GetTotalAnimDuration()
	{
		float num = 0f;
		SpeedAnimation[] anims = m_Anims;
		foreach (SpeedAnimation speedAnimation in anims)
		{
			d.AssertFormat(num == 0f, "We don't yet support multiple animations that have anim events that deal damage (on {0})!", base.name);
			float length = speedAnimation.Animation.clip.length;
			num = Mathf.Max(num, length);
		}
		d.AssertFormat(num != 0f, "No damage dealing animation found (on {0})!", base.name);
		return num;
	}

	private float GetCurAnimTime()
	{
		float num = 0f;
		SpeedAnimation[] anims = m_Anims;
		for (int i = 0; i < anims.Length; i++)
		{
			foreach (AnimationState item in anims[i].Animation)
			{
				num = Mathf.Max(num, item.time % item.length);
			}
		}
		return num;
	}

	private void OnAnimDamageEvent(int val)
	{
		if (val < 0 || m_FiringPoints == null || m_FiringPoints.Length == 0 || m_FiringPoints.Length <= val)
		{
			d.LogError("Attempted to call event with value " + val + " on melee anim weapon " + base.gameObject.name + " while there is no firing point that matches this value! Aborting...");
			return;
		}
		bool flag = false;
		for (int i = 0; i < m_LastTargetCollisionsInfo.Count; i++)
		{
			flag = TryDoDamageToFrameCollision(m_LastTargetCollisionsInfo[i], m_FiringPoints[val].DamagePerCall) || flag;
		}
		if (flag)
		{
			if (m_FiringPoints[val].VFXFollowOrigin)
			{
				TriggerHitVFX(m_FiringPoints[val].Origin, m_FiringPoints[val].VFXIndex, (m_FiringPoints[val].VFXDuration == 0f) ? float.Epsilon : m_FiringPoints[val].VFXDuration);
			}
			else
			{
				TriggerHitVFX(m_FiringPoints[val].Origin.position, m_FiringPoints[val].Origin.rotation.eulerAngles, m_FiringPoints[val].VFXIndex, (m_FiringPoints[val].VFXDuration == 0f) ? float.Epsilon : m_FiringPoints[val].VFXDuration);
			}
			TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(base.block, m_SFXType);
			base.block.tank.TechAudio.PlayOneshot(data);
		}
	}

	private void OnPool()
	{
		m_AnimEvents = GetComponentsInChildren<AnimEvent>();
		for (int i = 0; i < m_AnimEvents.Length; i++)
		{
			m_AnimEvents[i].HandleEvent.Subscribe(OnAnimDamageEvent);
		}
		InitAnimations();
	}

	private void OnDepool()
	{
		for (int i = 0; i < m_AnimEvents.Length; i++)
		{
			m_AnimEvents[i].HandleEvent.Unsubscribe(OnAnimDamageEvent);
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		LerpAnimationSpeed();
	}
}
