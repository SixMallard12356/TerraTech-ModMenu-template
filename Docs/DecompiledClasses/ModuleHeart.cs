using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(ModuleItemPickup))]
[RequireComponent(typeof(ModuleItemHolder))]
public class ModuleHeart : Module
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float chargeTimeRemaining;
	}

	[SerializeField]
	private AnimationCurve m_ScaleAnim;

	[SerializeField]
	private float m_SetupTime = 30f;

	[SerializeField]
	private float m_DamageFactorInterruption = 0.1f;

	[SerializeField]
	private float m_EjectSpeedBase = 5f;

	[SerializeField]
	private float m_EjectSpeedVariance = 1f;

	[SerializeField]
	private float m_EjectSpin = 10f;

	[SerializeField]
	private EnergyGauge m_ChargingGauge;

	[SerializeField]
	private float m_EventHorizonRadius = 1f;

	[SerializeField]
	private float m_StartShrinkingRadius = 5f;

	[SerializeField]
	private ParticleSystem m_EjectingParticlesPrefab;

	[SerializeField]
	private float m_MinTimeBeforeDrop = 3f;

	[SerializeField]
	private float m_ImpactDamageThreshold;

	[SerializeField]
	private TechAudio.SFXType m_SFXStored = TechAudio.SFXType.SCUItemConsumed;

	[SerializeField]
	private TechAudio.SFXType m_SFXOnline = TechAudio.SFXType.SCULoop;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private ModuleItemHolder m_Holder;

	private ModuleItemPickup m_Pickup;

	private float m_ReadyAfterTime;

	private const int ANIM_STATE_OFFLINE = 0;

	private const int ANIM_STATE_CHARGING = 1;

	private const int ANIM_STATE_ONLINE = 2;

	private ModuleAnimator m_Animator;

	private AnimatorInt m_AnimIntHeartState = new AnimatorInt("HeartState");

	private bool m_ShouldSetAsTracked;

	private bool m_HasAnchor;

	private const float k_AudioCooldown = 0.1f;

	private ModuleAudioProvider m_AudioProvider;

	public bool IsOnline
	{
		get
		{
			if (CanPowerUp)
			{
				return Time.time > m_ReadyAfterTime;
			}
			return false;
		}
	}

	private bool CanPowerUp
	{
		get
		{
			if (base.block.IsAttached && (!m_HasAnchor || base.block.tank.IsAnchored) && !IsDisabledVendor)
			{
				return !IsCircuitInterrupted;
			}
			return false;
		}
	}

	private bool IsDisabledVendor
	{
		get
		{
			if (Singleton.Manager<ManWorld>.inst.Vendors.IsVendorSCU(base.block.BlockType))
			{
				return !Singleton.Manager<ManWorld>.inst.Vendors.ScuActive;
			}
			return false;
		}
	}

	private bool CircuitControlled
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

	private bool IsCircuitInterrupted
	{
		get
		{
			if (CircuitControlled)
			{
				return base.block.CircuitReceiver.CurrentChargeData.ChargeStrength == 0;
			}
			return false;
		}
	}

	private void UpdatePickupTargets()
	{
		Vector3 vector = m_Holder.SingleStack.BasePosWorld();
		for (int num = m_Holder.SingleStack.items.Count - 1; num >= 0; num--)
		{
			Visible visible = m_Holder.SingleStack.items[num];
			if (!visible.IsPrePickup)
			{
				TankBlock tankBlock = visible.block;
				float magnitude = (visible.centrePosition - vector).magnitude;
				if (magnitude <= m_EventHorizonRadius && Singleton.Manager<ManPointer>.inst.DraggingItem != visible)
				{
					if (ManSpawn.IsPlayerTeam(base.block.tank.Team) || Singleton.Manager<ManWorld>.inst.Vendors.IsVendorSCU(base.block.BlockType))
					{
						if (!ManSpawn.IsPlayerTeam(tankBlock.LastTechTeam))
						{
							Singleton.Manager<ManStats>.inst.BlockScavenged(tankBlock.BlockType);
						}
						if (ManNetwork.IsHost)
						{
							Singleton.Manager<ManPlayer>.inst.AddBlockToInventory(tankBlock.BlockType);
						}
					}
					TechAudio.AudioTickData data = TechAudio.AudioTickData.ConfigureOneshot(base.block, m_SFXStored, 0.1f);
					base.block.tank.TechAudio.PlayOneshot(data);
					if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
					{
						visible.trans.Recycle();
					}
					else if (Singleton.Manager<ManNetwork>.inst.IsServer)
					{
						visible.ServerDestroy();
					}
				}
				else
				{
					float num2 = (tankBlock.IsNotNull() ? ((0.5f + tankBlock.BlockCellBounds.extents.Average()) * tankBlock.trans.localScale.x) : 0.5f);
					float time = Mathf.Clamp01(magnitude / Mathf.Max(m_StartShrinkingRadius + num2, m_EventHorizonRadius));
					float num3 = m_ScaleAnim.Evaluate(time);
					visible.trans.localScale = new Vector3(num3, num3, num3);
				}
			}
		}
	}

	private void DropAllItems()
	{
		if (ManNetwork.IsHost)
		{
			ModuleItemHolder.Stack.ItemIterator enumerator = m_Holder.Contents.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Visible current = enumerator.Current;
				Extensions.AddRandomVelocity(baseVelocity: ((current.centrePosition - m_Holder.GetStack(0).BasePosWorld()).normalized + Vector3.up * 0.5f) * m_EjectSpeedBase, rbody: current.rbody, randomVelocity: Vector3.one * m_EjectSpeedVariance, randomAngVel: m_EjectSpin);
			}
			m_Holder.DropAll();
		}
	}

	private void ResetState()
	{
		m_Pickup.IsEnabled = false;
	}

	private void ResetReadyTime()
	{
		if (m_ReadyAfterTime < Time.time)
		{
			m_ReadyAfterTime = Time.time + m_SetupTime;
		}
	}

	private void ReevaluateActiveState()
	{
		bool num = Time.time > m_ReadyAfterTime;
		bool canPowerUp = CanPowerUp;
		if (num != canPowerUp)
		{
			if (canPowerUp)
			{
				ResetReadyTime();
				return;
			}
			DropAllItems();
			ResetState();
			m_ReadyAfterTime = 0f;
		}
	}

	private bool CanAcceptItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		return IsOnline;
	}

	private bool CanReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack, ModuleItemHolder.PassType passType)
	{
		return false;
	}

	private void OnReleaseItem(Visible item, ModuleItemHolder.Stack fromStack, ModuleItemHolder.Stack toStack)
	{
		item.trans.localScale = Vector3.one;
	}

	private void OnAttached()
	{
		ReevaluateActiveState();
		base.block.tank.DamageEvent.Subscribe(OnTechDamaged);
		base.block.tank.Anchors.AnchorEvent.Subscribe(OnAnchor);
		if (ManSpawn.IsPlayerTeam(base.block.tank.Team))
		{
			m_ShouldSetAsTracked = true;
		}
	}

	private void OnDetaching()
	{
		ReevaluateActiveState();
		if ((bool)m_ChargingGauge)
		{
			m_ChargingGauge.OnDetach(base.block.tank);
		}
		base.block.tank.DamageEvent.Unsubscribe(OnTechDamaged);
		base.block.tank.Anchors.AnchorEvent.Unsubscribe(OnAnchor);
		if (ManSpawn.IsPlayerTeam(base.block.tank.Team) && !m_ShouldSetAsTracked)
		{
			Singleton.Manager<ManPlayer>.inst.TrackedTechHeartRemoved(base.block.tank, ManSaveGame.Storing);
		}
		if (!ManSaveGame.Storing && !ManSpawn.IsPlayerTeam(base.block.tank.Team))
		{
			base.block.damage.SelfDestruct(2f);
		}
		m_ShouldSetAsTracked = false;
	}

	private void OnAnchor(bool anchored, bool skyAnchor)
	{
		ReevaluateActiveState();
	}

	private void OnTechDamaged(ManDamage.DamageInfo damage)
	{
		if (damage.SourceTank != null || (damage.DamageType == ManDamage.DamageType.Impact && damage.Damage > m_ImpactDamageThreshold))
		{
			DropAllItems();
			float num = damage.Damage * m_DamageFactorInterruption;
			m_ReadyAfterTime = Mathf.Max(m_ReadyAfterTime, Time.time) + num;
			m_ReadyAfterTime = Mathf.Min(m_ReadyAfterTime, Time.time + m_SetupTime);
		}
	}

	private void OnChargeChanged(Circuits.BlockChargeData charge)
	{
		ReevaluateActiveState();
	}

	private void OnConnectedToCircuitNetwork(bool state)
	{
		ReevaluateActiveState();
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.chargeTimeRemaining = (IsOnline ? (-1f) : (m_ReadyAfterTime - Time.time));
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			if (serialData2.chargeTimeRemaining > 0f)
			{
				m_ReadyAfterTime = Time.time + serialData2.chargeTimeRemaining;
			}
			else
			{
				m_ReadyAfterTime = Time.time;
			}
		}
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
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialize);
		base.block.BlockUpdate.Subscribe(OnUpdate);
		m_Holder = GetComponent<ModuleItemHolder>();
		m_Holder.ReleaseItemEvent.Subscribe(OnReleaseItem);
		m_Holder.SetAcceptFilterCallback(CanAcceptItem);
		m_Holder.SetReleaseFilterCallback(CanReleaseItem);
		m_Holder.SingleStack.NetClientsUpdateItemPos = false;
		m_Pickup = GetComponent<ModuleItemPickup>();
		ModuleItemHolderBeam component = GetComponent<ModuleItemHolderBeam>();
		component.OverrideHeightCorrectionLiftFactor(20f);
		if (m_MinTimeBeforeDrop > 0f)
		{
			component.OverrideDropAfterMinTime(m_MinTimeBeforeDrop);
		}
		m_Animator = GetComponent<ModuleAnimator>();
		m_EventHorizonRadius = Mathf.Max(m_EventHorizonRadius, 0.2f);
		m_HasAnchor = GetComponent<ModuleAnchor>() != null;
		m_AudioProvider = GetComponent<ModuleAudioProvider>();
		if (m_IsUsedOnCircuit)
		{
			base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
			base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		}
	}

	private void OnSpawn()
	{
		ResetState();
	}

	private void OnUpdate()
	{
		if (m_ShouldSetAsTracked)
		{
			Singleton.Manager<ManPlayer>.inst.TrackTechWithHeart(base.block.tank);
			m_ShouldSetAsTracked = false;
		}
		if (IsOnline)
		{
			UpdatePickupTargets();
		}
		m_Pickup.IsEnabled = IsOnline;
		if ((bool)m_Animator)
		{
			int value = (IsOnline ? 2 : (CanPowerUp ? 1 : 0));
			m_Animator.Set(m_AnimIntHeartState, value);
		}
		if (m_AudioProvider != null)
		{
			m_AudioProvider.SetNoteOn(m_SFXOnline, IsOnline);
		}
		if ((bool)m_ChargingGauge)
		{
			float fullness = 0f;
			if (CanPowerUp)
			{
				fullness = 1f - Mathf.Clamp01((m_ReadyAfterTime - Time.time) / m_SetupTime);
			}
			m_ChargingGauge.UpdateGaugeLevel(base.block.tank, fullness);
		}
	}
}
