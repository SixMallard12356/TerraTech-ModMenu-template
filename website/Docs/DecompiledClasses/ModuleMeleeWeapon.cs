#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public abstract class ModuleMeleeWeapon : Module, TechAudio.IModuleAudioProvider, TechWeapon.IMeleeWeapon, IModuleDamager
{
	private class HitVFX
	{
		private Transform m_FollowTransform;

		private ParticleSystem m_HitEffect;

		private float m_PlayHitEffectDuration;

		public bool IsActive => m_PlayHitEffectDuration > 0f;

		public HitVFX(ParticleSystem m_HitEffect, float scale)
		{
			this.m_HitEffect = m_HitEffect;
			m_PlayHitEffectDuration = -1f;
			ParticleSystem.MainModule main = m_HitEffect.main;
			main.startSizeMultiplier *= scale;
			main.startSpeedMultiplier *= scale;
			float constantEmissionRate = m_HitEffect.GetConstantEmissionRate();
			m_HitEffect.SetConstantEmissionRate(constantEmissionRate * scale);
		}

		public void Trigger(Vector3 position, Vector3 direction, float duration)
		{
			m_FollowTransform = null;
			m_HitEffect.transform.SetPositionAndRotationIfChanged(position, Quaternion.LookRotation(direction));
			Trigger(duration);
		}

		public void Trigger(Transform transform, float duration)
		{
			m_FollowTransform = transform;
			Trigger(duration);
			FollowTransform();
		}

		private void Trigger(float duration)
		{
			if (m_PlayHitEffectDuration < duration)
			{
				m_PlayHitEffectDuration = duration;
			}
		}

		private void FollowTransform()
		{
			if (!(m_FollowTransform == null))
			{
				if (!IsActive)
				{
					m_FollowTransform = null;
				}
				else
				{
					m_HitEffect.transform.SetPositionAndRotationIfChanged(m_FollowTransform.position, m_FollowTransform.rotation);
				}
			}
		}

		public void CountdownPlayEffectDuration()
		{
			if (IsActive != m_HitEffect.isPlaying)
			{
				if (m_HitEffect.isPlaying)
				{
					m_HitEffect.Stop();
				}
				else
				{
					m_HitEffect.Play();
				}
			}
			FollowTransform();
			if (IsActive)
			{
				m_PlayHitEffectDuration -= Time.deltaTime;
			}
		}

		public void ResetPlayHitEffectDurations()
		{
			m_PlayHitEffectDuration = -1f;
		}
	}

	public struct FrameCollisionInfo
	{
		public Visible TargetVisible;

		public Vector3 Point;

		public Vector3 Normal;

		public Collider OtherCol;

		public Tank Tank;

		public float ImpulseMagnitude;

		public bool WasStartOfCollision;

		public static readonly FrameCollisionInfo clear = new FrameCollisionInfo(null);

		public bool IsNull => TargetVisible == null;

		public FrameCollisionInfo(Tank.CollisionInfo collisionInfo, bool WasStartOfCollision = false)
		{
			if (collisionInfo == null || collisionInfo.b.visible == null)
			{
				TargetVisible = null;
				Point = Vector3.negativeInfinity;
				Normal = Vector3.negativeInfinity;
				OtherCol = null;
				Tank = null;
				ImpulseMagnitude = 0f;
				this.WasStartOfCollision = false;
			}
			else
			{
				TargetVisible = collisionInfo.b.visible;
				Point = collisionInfo.point;
				Normal = collisionInfo.normal;
				OtherCol = collisionInfo.b.collider;
				Tank = collisionInfo.b.tank;
				ImpulseMagnitude = collisionInfo.impulse.magnitude;
				this.WasStartOfCollision = WasStartOfCollision;
			}
		}

		public void Clear()
		{
			this = clear;
		}
	}

	[Flags]
	protected enum ContactFlags
	{
		Null = 1,
		Vehicle = 2,
		Block = 4,
		Scenery = 8,
		Chunk = 0x10
	}

	[Flags]
	protected enum ActivationFlags
	{
		ControllerInput = 1,
		Collision = 2
	}

	[SerializeField]
	[FormerlySerializedAs("activeColliders")]
	protected Collider[] m_ActiveColliders;

	[SerializeField]
	[Tooltip("The area which a viable collision must exit before being this weapon stops applying damage to it")]
	protected Collider[] m_ExitTriggers = new Collider[0];

	[FormerlySerializedAs("scaleHitParticles")]
	[SerializeField]
	protected float m_ScaleHitParticles = 1f;

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	protected ManDamage.DamageType m_DamageType;

	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	[SerializeField]
	protected TechAudio.SFXType m_SFXType;

	[SerializeField]
	[Tooltip("Which visibles should this weapon be able to damage")]
	[EnumFlag]
	protected ContactFlags m_ContactFlags = (ContactFlags)(-1);

	[SerializeField]
	[Tooltip("Disable functionality if this block is part of a multi-block tech (ie, a cab with more than 1 block)")]
	private bool m_DisabledWhenAttachedToBlocks;

	[SerializeField]
	[EnumFlag]
	private ActivationFlags m_ActivationFlags = (ActivationFlags)(-1);

	[SerializeField]
	protected bool m_IsUsedOnCircuit = true;

	private HitVFX[] m_HitEffects;

	protected List<FrameCollisionInfo> m_LastTargetCollisionsInfo = new List<FrameCollisionInfo>();

	protected List<int> m_VisiblesInTriggerLastFrame = new List<int>();

	protected const float k_LastCollisionTimeTimeout = 0.2f;

	private FMODEvent.FMODParams m_CachedContactAudioParam;

	protected float m_AudioUpdateRate;

	protected bool m_IsActive;

	protected int m_ActivatedFlags;

	protected bool m_ReceivingChargeCheck;

	protected float m_LastCollisionTime;

	private bool m_IsEnabledWithCurrentBlockConnections;

	private List<int> m_Cached_IndexesToRemove = new List<int>();

	private TechAudio.AudioTickData m_CachedWeaponUpdateData;

	private FMODEvent.FMODParams m_CachedFModParams = FMODEvent.FMODParams.empty;

	ManDamage.DamageType IModuleDamager.DamageType => m_DamageType;

	public bool IsActive => m_IsActive;

	public TechAudio.SFXType SFXType => m_SFXType;

	public int NumHitEffectsActive
	{
		get
		{
			int num = 0;
			for (int i = 0; i < m_HitEffects.Length; i++)
			{
				if (m_HitEffects[i].IsActive)
				{
					num++;
				}
			}
			return num;
		}
	}

	protected bool IsCollided => m_LastCollisionTime != float.NegativeInfinity;

	protected abstract bool PlaySFXWhileActive { get; }

	protected bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitNode.Receiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	public event Action<TechAudio.AudioTickData, FMODEvent.FMODParams> OnAudioTickUpdate;

	public abstract float GetHitDamage();

	public abstract float GetHitsPerSec();

	protected void TriggerHitVFX(Vector3 position, Vector3 direction, int vfxIndex = 0, float duration = float.Epsilon)
	{
		TriggerHitVFXBase(null, position, direction, vfxIndex, duration);
	}

	protected void TriggerHitVFX(Transform transform, int vfxIndex = 0, float duration = float.Epsilon)
	{
		TriggerHitVFXBase(transform, Vector3.zero, Vector3.zero, vfxIndex, duration);
	}

	private void TriggerHitVFXBase(Transform transform, Vector3 position, Vector3 direction, int vfxIndex = 0, float duration = float.Epsilon)
	{
		if (m_HitEffects != null && m_HitEffects.Length != 0 && vfxIndex >= 0 && vfxIndex < m_HitEffects.Length && m_HitEffects[0] != null)
		{
			if (transform != null)
			{
				m_HitEffects[vfxIndex].Trigger(transform, duration);
			}
			else
			{
				m_HitEffects[vfxIndex].Trigger(position, direction, duration);
			}
		}
	}

	private void SetActiveDefault()
	{
		m_LastCollisionTime = float.NegativeInfinity;
		m_ActivatedFlags = 0;
		m_ReceivingChargeCheck = false;
		m_IsEnabledWithCurrentBlockConnections = false;
		SetWeaponActive(becomeActive: false, instantly: true);
	}

	private void SetReceivingChargeAndRefreshActive(bool onlyRefreshIfChanged = true)
	{
		if (!onlyRefreshIfChanged || m_ReceivingChargeCheck != base.block.CircuitNode.Receiver.CurrentChargeData > 0)
		{
			m_ReceivingChargeCheck = base.block.CircuitReceiver.ShouldProcessInput && base.block.CircuitReceiver.CurrentChargeData > 0;
			RefreshSetActive();
		}
	}

	private void SetCollidedAndRefreshActive(bool state, float overrideCollisionTimeOutDuration = -1f)
	{
		if (state)
		{
			float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
			if (overrideCollisionTimeOutDuration != -1f)
			{
				num += overrideCollisionTimeOutDuration - 0.2f;
			}
			if (m_LastCollisionTime < num)
			{
				m_LastCollisionTime = num;
			}
			m_ActivatedFlags |= 2;
		}
		else
		{
			m_LastCollisionTime = float.NegativeInfinity;
			m_ActivatedFlags &= -3;
		}
		m_ActivatedFlags = Bitfield.Set(m_ActivatedFlags, 2, IsCollided);
		RefreshSetActive();
	}

	private void SetControllerFiringAndRefreshActive(bool state)
	{
		if (state != ((m_ActivatedFlags & 1) != 0))
		{
			if (state)
			{
				m_ActivatedFlags |= 1;
			}
			else
			{
				m_ActivatedFlags &= -2;
			}
			RefreshSetActive();
		}
	}

	private void CheckAttachedToBlocksAndRefreshActive()
	{
		m_IsEnabledWithCurrentBlockConnections = !m_DisabledWhenAttachedToBlocks || !base.block.HasNeighbours;
		RefreshSetActive();
	}

	private void RefreshSetActive()
	{
		if ((m_IsEnabledWithCurrentBlockConnections && (CircuitControlled ? m_ReceivingChargeCheck : (((uint)m_ActivationFlags & (uint)m_ActivatedFlags) != 0))) != m_IsActive)
		{
			SetWeaponActive(!m_IsActive);
		}
	}

	protected virtual void SetWeaponActive(bool becomeActive, bool instantly = false)
	{
		m_IsActive = becomeActive;
	}

	protected bool TryDoDamageToFrameCollision(FrameCollisionInfo collisionInfo, float damage)
	{
		if (collisionInfo.IsNull)
		{
			return false;
		}
		Singleton.Manager<ManDamage>.inst.DealDamage(collisionInfo.TargetVisible.damageable, damage, m_DamageType, this, base.block.tank, collisionInfo.Point, collisionInfo.Normal);
		return true;
	}

	protected void HandleCollision(FrameCollisionInfo collisionInfo, float overrideCollisionTimeOutDuration = -1f)
	{
		if (!collisionInfo.TargetVisible || !collisionInfo.TargetVisible.damageable || collisionInfo.TargetVisible.damageable.Invulnerable)
		{
			return;
		}
		ObjectTypes objectTypes = (collisionInfo.Tank ? ObjectTypes.Vehicle : collisionInfo.TargetVisible.type);
		if (((uint)m_ContactFlags & (uint)(1 << (int)objectTypes)) == 0 || (objectTypes == ObjectTypes.Vehicle && collisionInfo.Tank.IsFriendly(base.block.tank.Team)))
		{
			return;
		}
		SetCollidedAndRefreshActive(state: true, overrideCollisionTimeOutDuration);
		for (int i = 0; i < m_LastTargetCollisionsInfo.Count; i++)
		{
			if (m_LastTargetCollisionsInfo[i].TargetVisible == collisionInfo.TargetVisible)
			{
				m_LastTargetCollisionsInfo[i] = collisionInfo;
				return;
			}
		}
		m_LastTargetCollisionsInfo.Add(collisionInfo);
	}

	protected virtual void HandleLastFrameCollisions()
	{
	}

	protected void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		SetReceivingChargeAndRefreshActive();
	}

	protected void OnConnectedToCircuitNetwork(bool state)
	{
		SetReceivingChargeAndRefreshActive(onlyRefreshIfChanged: false);
	}

	private void OnTankBeamEnabled(Tank tech, bool enabled)
	{
		if (base.block.tank.IsNotNull() && base.block.tank == tech)
		{
			SetReceivingChargeAndRefreshActive(onlyRefreshIfChanged: false);
		}
	}

	private void OnTankCollision(Tank.CollisionInfo collision, Tank.CollisionInfo.Event e)
	{
		if ((e == Tank.CollisionInfo.Event.Stay || e == Tank.CollisionInfo.Event.Enter) && m_ActiveColliders.Contains(collision.a.collider))
		{
			collision.DealImpactDamage = false;
			HandleCollision(new FrameCollisionInfo(collision, e == Tank.CollisionInfo.Event.Enter));
		}
	}

	private void OnExitCatcherTrigger(TriggerCatcher.Interaction type, Collider otherCollider)
	{
		if (type != TriggerCatcher.Interaction.Exit)
		{
			Visible visible = Visible.FindVisibleUpwards(otherCollider);
			if ((bool)visible)
			{
				m_VisiblesInTriggerLastFrame.Add(visible.ID);
			}
		}
	}

	protected void AudioTickUpdate()
	{
		if (this.OnAudioTickUpdate != null && TryGetAudioTickUpdateData(ref m_CachedWeaponUpdateData, ref m_CachedFModParams))
		{
			this.OnAudioTickUpdate.Send(m_CachedWeaponUpdateData, m_CachedFModParams);
		}
	}

	private void CountdownCollisionTime()
	{
		if (IsCollided && Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() > m_LastCollisionTime + 0.2f)
		{
			SetCollidedAndRefreshActive(state: false);
		}
	}

	protected bool TryGetAudioTickUpdateData(ref TechAudio.AudioTickData weaponUpdateData, ref FMODEvent.FMODParams fModParams)
	{
		if (!PlaySFXWhileActive)
		{
			return false;
		}
		weaponUpdateData = TechAudio.AudioTickData.ConfigureLoopedADSR(this, m_SFXType, IsActive, m_AudioUpdateRate, 0f, NumHitEffectsActive);
		m_CachedContactAudioParam.m_Value = (IsCollided ? 1f : 0f);
		fModParams = m_CachedContactAudioParam;
		return true;
	}

	protected void OnControlInput(int aim, bool fire)
	{
		SetControllerFiringAndRefreshActive(fire);
	}

	protected virtual void OnAttached()
	{
		base.block.tank.control.manualAimFireEvent.Subscribe(OnControlInput);
		base.block.tank.CollisionEvent.Subscribe(OnTankCollision);
		for (int i = 0; i < m_ExitTriggers.Length; i++)
		{
			m_ExitTriggers[i].isTrigger = true;
			TriggerCatcher.Subscribe(m_ExitTriggers[i].gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnExitCatcherTrigger);
		}
		base.block.tank.Weapons.RegisterMelee(this);
		base.block.tank.TechAudio.AddModule(this);
		SetActiveDefault();
		m_LastTargetCollisionsInfo.Clear();
		if (m_IsUsedOnCircuit)
		{
			TankBeam.OnBeamEnabled.Subscribe(OnTankBeamEnabled);
		}
		CheckAttachedToBlocksAndRefreshActive();
	}

	protected virtual void OnDetaching()
	{
		base.block.tank.control.manualAimFireEvent.Unsubscribe(OnControlInput);
		base.block.tank.CollisionEvent.Unsubscribe(OnTankCollision);
		for (int i = 0; i < m_ExitTriggers.Length; i++)
		{
			TriggerCatcher.Unsubscribe(m_ExitTriggers[i].gameObject, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnExitCatcherTrigger);
		}
		base.block.tank.Weapons.UnregisterMelee(this);
		base.block.tank.TechAudio.RemoveModule(this);
		m_AudioUpdateRate = 0f;
		SetActiveDefault();
		m_LastTargetCollisionsInfo.Clear();
		if (m_IsUsedOnCircuit)
		{
			TankBeam.OnBeamEnabled.Unsubscribe(OnTankBeamEnabled);
		}
		CheckAttachedToBlocksAndRefreshActive();
	}

	private void OnNeighbourAttachedOrDetached(TankBlock _)
	{
		CheckAttachedToBlocksAndRefreshActive();
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		d.Assert(!m_DisabledWhenAttachedToBlocks || GetComponent<ModuleAnimator>() != null, "ModuleMeleeWeapon.OnPool - m_DisabledWhenAttachedToBlocks was expexting an Animator to be present in order to function!");
		IEnumerable<ParticleSystem> enumerable = from r in GetComponentsInChildren<ParticleSystem>(includeInactive: true)
			where !r.gameObject.CompareTag("VFXclude")
			select r;
		m_HitEffects = new HitVFX[enumerable.Count()];
		int num = 0;
		foreach (ParticleSystem item in enumerable)
		{
			m_HitEffects[num] = new HitVFX(item, m_ScaleHitParticles);
			num++;
		}
		m_CachedContactAudioParam = new FMODEvent.FMODParams("contact", 0f);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.NeighbourAttachedEvent.Subscribe(OnNeighbourAttachedOrDetached);
		base.block.NeighbourDetachedEvent.Subscribe(OnNeighbourAttachedOrDetached);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		base.block.BlockFixedUpdate.Subscribe(OnFixedUpdate);
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		HitVFX[] hitEffects = m_HitEffects;
		for (int i = 0; i < hitEffects.Length; i++)
		{
			hitEffects[i].ResetPlayHitEffectDurations();
		}
		SetActiveDefault();
	}

	protected virtual void OnUpdate()
	{
		CountdownCollisionTime();
		HitVFX[] hitEffects = m_HitEffects;
		for (int i = 0; i < hitEffects.Length; i++)
		{
			hitEffects[i].CountdownPlayEffectDuration();
		}
		AudioTickUpdate();
	}

	protected virtual void OnFixedUpdate()
	{
		HandleLastFrameCollisions();
		m_Cached_IndexesToRemove.Clear();
		for (int i = 0; i < m_LastTargetCollisionsInfo.Count; i++)
		{
			if (!m_LastTargetCollisionsInfo[i].IsNull && m_VisiblesInTriggerLastFrame.Contains(m_LastTargetCollisionsInfo[i].TargetVisible.ID))
			{
				HandleCollision(m_LastTargetCollisionsInfo[i]);
			}
			else
			{
				m_Cached_IndexesToRemove.Add(i);
			}
		}
		for (int j = 0; j < m_Cached_IndexesToRemove.Count; j++)
		{
			m_LastTargetCollisionsInfo.RemoveAt(m_Cached_IndexesToRemove[j] - j);
		}
		m_VisiblesInTriggerLastFrame.Clear();
	}
}
