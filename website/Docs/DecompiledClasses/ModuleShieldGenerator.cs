#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleEnergy))]
public class ModuleShieldGenerator : Module, INetworkedModule
{
	private enum State
	{
		Disabled,
		Charging,
		PoweredUp
	}

	private struct HealingItem
	{
		public readonly Visible Visible;

		public readonly Damageable Damageable;

		public HealingItem(Visible visible, Damageable damageable)
		{
			Visible = visible;
			Damageable = damageable;
		}
	}

	private new class SerialData : SerialData<SerialData>
	{
		public WarningHolder warning;

		public float deficit;

		public float drain;

		public bool poweredUp;
	}

	[Tooltip("This is actually the Diameter of the shield bubble, not Radius, as it maps 1:1 to scale which is the diameter.")]
	public float m_Radius = 5f;

	public float m_EnergyConsumptionPerSec = 2.5f;

	public float m_InitialChargeEnergy = 300f;

	public AnimationCurve m_InterpPowerOn;

	public AnimationCurve m_InterpPowerOff;

	public float m_InterpTimeOn = 1f;

	public float m_InterpTimeOff = 1f;

	public float m_ParticleLife = 2.5f;

	public float m_ParticleSpeed = 8f;

	public bool m_Repulsion = true;

	public float m_RepelKick = 1f;

	public float m_RepelKickAngular = 1f;

	public float m_EnergyConsumedPerDamagePoint = 1f;

	public Transform hitEffect;

	public Transform hitEffectConsole;

	public bool m_ScriptDisabled;

	public bool m_Healing;

	public float m_HealingHeartbeatInterval = 1f;

	public float m_MaxHealingPerHeartbeat = 200f;

	public float m_EnergyConsumedPerPointHealed = 1f;

	[SerializeField]
	private float m_PowerUpDelay = 2f;

	[SerializeField]
	private GameObject m_ShieldBulletTriggerArea;

	[SerializeField]
	private GameObject m_TechTriggerArea;

	[SerializeField]
	private float m_MinTechForce = 50f;

	[SerializeField]
	private float m_MaxTechForce = 100f;

	[SerializeField]
	private ParticleSystem m_ChargingParticlesPrefab;

	[SerializeField]
	private FMODEvent m_PowerOnShieldSfxEvent;

	[SerializeField]
	private FMODEvent m_PowerOffShieldSfxEvent;

	[SerializeField]
	[Range(-1f, 1f)]
	private int m_localXAxisCheck;

	[Range(-1f, 1f)]
	[SerializeField]
	private int m_localYAxisCheck;

	[Range(-1f, 1f)]
	[SerializeField]
	private int m_localZAxisCheck;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	[SerializeField]
	private bool m_CircuitControlSkipsChain;

	private ModuleEnergy m_ModuleEnergy;

	private BubbleShield m_Shield;

	private WarningHolder m_Warning;

	[HideInInspector]
	[SerializeField]
	private ParticleSystem m_ChargingParticles;

	private float m_EnergyDeficit;

	private float m_EnergyDrain;

	private float m_HealingHeartbeatNextTime;

	private float m_TimeWithoutEnergy;

	private bool m_EnoughPower;

	private bool m_ChargingEffect;

	private static List<HealingItem> s_HealingList = new List<HealingItem>(100);

	private static List<Damageable> s_DamageableList = new List<Damageable>(4);

	private List<Tank> m_TechCollisionsThisFrame = new List<Tank>();

	private TechSequencer.SequenceNode m_SequenceNode;

	private State m_State;

	private float m_PowerUpTimer;

	private int m_CachedCollisionFixedFrameCount;

	private const float m_TimeBeforeDisable = 0.3f;

	private static readonly Bitfield<ObjectTypes> k_MaskBlocks = new Bitfield<ObjectTypes>(2);

	public bool IsPowered => m_State == State.PoweredUp;

	private bool CircuitControlled
	{
		get
		{
			if (m_IsUsedOnCircuit)
			{
				return base.block.CircuitReceiver.IsConnectedToOtherNodes;
			}
			return false;
		}
	}

	private void OnSerialize(bool saving, TankPreset.BlockSpec blockSpec)
	{
		if (saving)
		{
			SerialData serialData = new SerialData();
			serialData.warning = m_Warning;
			serialData.poweredUp = IsPowered;
			serialData.deficit = m_EnergyDeficit;
			serialData.drain = m_EnergyDrain;
			serialData.Store(blockSpec.saveState);
			return;
		}
		SerialData serialData2 = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData2 != null)
		{
			m_Warning.Restore(serialData2.warning);
			if (serialData2.poweredUp)
			{
				m_State = State.PoweredUp;
				m_Shield.SetTargetScale(m_Radius);
			}
			m_EnergyDeficit = serialData2.deficit;
			m_EnergyDrain = serialData2.drain;
		}
	}

	private float HealContainedVisibles(float energyAvailable)
	{
		float result = 0f;
		float num = energyAvailable / m_EnergyConsumedPerPointHealed;
		if (num > 0f)
		{
			float num2 = 0f;
			if (m_Shield.RepulsorBulletTrigger is SphereCollider)
			{
				SphereCollider sphereCollider = m_Shield.RepulsorBulletTrigger as SphereCollider;
				num2 = m_Shield.RepulsorBulletTrigger.transform.lossyScale.x * sphereCollider.radius;
			}
			float num3 = 0f;
			if (num2 > Mathf.Epsilon)
			{
				float num4 = num2 * num2;
				Vector3 position = m_Shield.RepulsorBulletTrigger.transform.position;
				int mask = Globals.inst.layerTank.mask;
				float cacheRadiusMul = 2f;
				foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadiusCached(base.block.centreOfMassWorld, num2, k_MaskBlocks, cacheRadiusMul, mask))
				{
					item.GetDamageablesInChildren(s_DamageableList);
					foreach (Damageable s_Damageable in s_DamageableList)
					{
						if (!s_Damageable.IsAtFullHealth && (position - s_Damageable.GetHealingOriginWorld()).sqrMagnitude < num4)
						{
							s_HealingList.Add(new HealingItem(item, s_Damageable));
							num3 += s_Damageable.MaxHealth - s_Damageable.Health;
						}
					}
				}
			}
			float num5 = Mathf.Min(Mathf.Min(m_MaxHealingPerHeartbeat, num) / num3, 1f);
			float num6 = 0f;
			foreach (HealingItem s_Healing in s_HealingList)
			{
				float num7 = num5 * (s_Healing.Damageable.MaxHealth - s_Healing.Damageable.Health);
				if (num7 > 0f)
				{
					num6 += num7;
					s_Healing.Damageable.Repair(num7);
					s_Healing.Visible.KeepAwake();
					if ((bool)base.block.tank && base.block.tank.IsFriendly(0) && (bool)s_Healing.Visible.block.tank && s_Healing.Visible.block.tank.IsFriendly(0))
					{
						Singleton.Manager<ManStats>.inst.PlayerBlockHealed(this, s_Healing.Visible, num7);
					}
				}
			}
			s_HealingList.Clear();
			result = energyAvailable * num6 / num;
		}
		return result;
	}

	private bool UpdateHealingHeartbeat()
	{
		if (m_Healing && Time.time >= m_HealingHeartbeatNextTime)
		{
			m_HealingHeartbeatNextTime += m_HealingHeartbeatInterval;
			return true;
		}
		return false;
	}

	public void UpdateCollisionCache(bool enable)
	{
		if (m_Repulsion)
		{
			SetTriggerCatcherEnabled(m_ShieldBulletTriggerArea, enable, TriggerCatcher.Interaction.Enter, OnBulletTriggerEvent);
			if (m_TechTriggerArea != null)
			{
				SetTriggerCatcherEnabled(m_TechTriggerArea, enable, TriggerCatcher.Interaction.Enter | TriggerCatcher.Interaction.Stay, OnTankTriggerEvent);
			}
			m_Shield.RepulsorBulletTrigger.enabled = enable;
			if (m_Shield.RepulsorTechTrigger != null)
			{
				m_Shield.RepulsorTechTrigger.enabled = enable;
			}
		}
	}

	private void SetTriggerCatcherEnabled(GameObject target, bool enabled, TriggerCatcher.Interaction flags, Action<TriggerCatcher.Interaction, Collider> delegateFunc)
	{
		if (enabled)
		{
			TriggerCatcher.Subscribe(target, flags, delegateFunc);
		}
		else
		{
			TriggerCatcher.Unsubscribe(target, flags, delegateFunc);
		}
	}

	private void OnAttached()
	{
		UpdateCollisionCache(enable: true);
		m_HealingHeartbeatNextTime = Time.time + m_HealingHeartbeatInterval;
		base.block.tank.Sequencer.RegisterNode(m_SequenceNode, (!m_Healing) ? TechSequencer.ChainType.ShieldBubble : TechSequencer.ChainType.RepairBubble);
	}

	private void OnDetaching()
	{
		UpdateCollisionCache(enable: false);
		m_Shield.SetTargetScale(0f, 0f, m_InterpPowerOff, m_InterpTimeOff, forceNewInterp: true);
		if ((bool)m_ChargingParticles)
		{
			m_ChargingParticles.Stop();
		}
		m_State = State.Disabled;
		m_EnergyDeficit = m_InitialChargeEnergy;
		m_EnergyDrain = 0f;
		m_Warning.Reset();
		base.block.tank.Sequencer.UnregisterNode(m_SequenceNode);
	}

	private void OnUpdateConsumeEnergy()
	{
		d.Assert(base.block.tank);
		bool flag = IsCategoryEnabled() && !m_ScriptDisabled;
		bool flag2 = (CircuitControlled ? (base.block.CircuitReceiver.CurrentChargeData > 0) : flag);
		bool flag3 = flag || flag2;
		base.block.tank.Sequencer.SetChainDisabled((!m_Healing) ? TechSequencer.ChainType.ShieldBubble : TechSequencer.ChainType.RepairBubble, !flag3);
		bool isPowered = IsPowered;
		switch (m_State)
		{
		case State.Disabled:
			if (ManNetwork.IsHost)
			{
				OnServerSetChargingEffect(flag2 && m_ModuleEnergy.GetCurrentAmount(TechEnergy.EnergyType.Electric) > 0f);
			}
			if (flag2 && m_SequenceNode.CheckIsReady())
			{
				m_State = State.Charging;
				m_PowerUpTimer = m_PowerUpDelay;
				if (ManNetwork.IsHost && m_EnergyDeficit <= 0f)
				{
					OnServerSetEnergyDeficit(m_EnergyConsumptionPerSec * m_PowerUpDelay);
				}
			}
			break;
		case State.Charging:
		{
			if (!flag2)
			{
				m_State = State.Disabled;
				if (ManNetwork.IsHost)
				{
					OnServerSetEnergyDeficit(m_InitialChargeEnergy);
				}
				m_EnergyDrain = 0f;
			}
			float num2 = Mathf.Min(Time.deltaTime / Mathf.Max(m_PowerUpTimer, 0.01f), 1f);
			float num3 = m_EnergyDeficit * num2;
			if (ManNetwork.IsHost)
			{
				float num4 = m_ModuleEnergy.ConsumeUpToMax(TechEnergy.EnergyType.Electric, num3);
				OnServerSetEnergyDeficit(m_EnergyDeficit - num4);
				if (num4 >= num3)
				{
					m_PowerUpTimer = Mathf.MoveTowards(m_PowerUpTimer, 0f, Time.deltaTime);
					OnServerSetChargingEffect(charging: true);
				}
				if (num4 <= 0f)
				{
					m_TimeWithoutEnergy += Time.deltaTime;
					if (m_TimeWithoutEnergy > 0.3f)
					{
						m_State = State.Disabled;
					}
				}
				else
				{
					m_TimeWithoutEnergy = 0f;
				}
			}
			if (m_EnergyDeficit <= 0f)
			{
				m_State = State.PoweredUp;
			}
			break;
		}
		case State.PoweredUp:
			if (ManNetwork.IsHost)
			{
				m_EnoughPower = m_ModuleEnergy.ConsumeIfEnough(TechEnergy.EnergyType.Electric, m_EnergyConsumptionPerSec * Time.deltaTime);
				OnServerSetChargingEffect(m_EnoughPower && m_Shield.IsScaling);
				if (m_EnergyDrain > 0f)
				{
					float num = m_EnergyDrain * Time.deltaTime;
					OnServerSetEnergyDeficit(m_EnergyDeficit + num);
				}
				if (m_EnergyDeficit > 0f)
				{
					OnServerSetEnergyDeficit(m_EnergyDeficit - m_ModuleEnergy.ConsumeUpToMax(TechEnergy.EnergyType.Electric, m_EnergyDeficit));
					m_EnoughPower = m_EnergyDeficit <= 0f;
				}
			}
			if (!m_EnoughPower || !flag2)
			{
				m_State = State.Disabled;
				if (ManNetwork.IsHost)
				{
					OnServerSetEnergyDeficit(m_InitialChargeEnergy);
				}
				m_EnergyDrain = 0f;
			}
			break;
		}
		if (isPowered != IsPowered)
		{
			if (IsPowered)
			{
				if (m_PowerOnShieldSfxEvent.IsValid())
				{
					m_PowerOnShieldSfxEvent.PlayOneShot(base.transform.position);
				}
				m_Shield.SetTargetScale(m_Radius, 0f, m_InterpPowerOn, m_InterpTimeOn, forceNewInterp: true);
			}
			else
			{
				if (m_PowerOffShieldSfxEvent.IsValid())
				{
					m_PowerOffShieldSfxEvent.PlayOneShot(base.transform.position);
				}
				m_Shield.SetTargetScale(0f, 0f, m_InterpPowerOff, m_InterpTimeOff, forceNewInterp: true);
			}
		}
		if (Singleton.Manager<ManPlayer>.inst.SkipPowerupSequencing)
		{
			m_SequenceNode.SetComplete(m_State != State.Disabled);
		}
		else
		{
			bool flag4 = ((m_CircuitControlSkipsChain && CircuitControlled) ? (m_State != State.Disabled) : (IsPowered && !m_Shield.IsScaling));
			m_SequenceNode.SetComplete(!flag2 || flag4);
		}
		if ((bool)m_ChargingParticles)
		{
			if (m_ChargingEffect && !m_ChargingParticles.isPlaying)
			{
				m_ChargingParticles.Play();
			}
			else if (!m_ChargingEffect && m_ChargingParticles.isPlaying)
			{
				m_ChargingParticles.Stop();
			}
		}
		if (IsPowered || m_ChargingEffect)
		{
			m_Warning.Remove();
		}
		else
		{
			m_Warning.TryRegisterWarning(LocalisationEnums.Warnings.warningTitleShieldNotPowered, LocalisationEnums.Warnings.warningMsgShieldNotPowered, 10);
		}
		if ((bool)base.block.tank && UpdateHealingHeartbeat())
		{
			float energyAvailable = m_MaxHealingPerHeartbeat * m_EnergyConsumedPerPointHealed;
			float num5 = HealContainedVisibles(energyAvailable);
			m_EnergyDrain = num5 / m_HealingHeartbeatInterval;
			m_ModuleEnergy.ConsumeUpToMax(TechEnergy.EnergyType.Electric, num5);
		}
	}

	private void OnBulletTriggerEvent(TriggerCatcher.Interaction triggerType, Collider bulletCollider)
	{
		if (triggerType != TriggerCatcher.Interaction.Enter)
		{
			return;
		}
		Projectile componentInParent = bulletCollider.GetComponentInParent<Projectile>();
		if (!(componentInParent != null) || !m_Repulsion)
		{
			return;
		}
		bool flag = base.block.tank == null || componentInParent.Shooter == null || componentInParent.Shooter.IsEnemy(base.block.tank.Team);
		Vector3 position = m_Shield.RepulsorBulletTrigger.transform.position;
		if (flag && (m_localXAxisCheck != 0 || m_localYAxisCheck != 0 || m_localZAxisCheck != 0))
		{
			Vector3 vector = m_Shield.RepulsorBulletTrigger.transform.InverseTransformPoint(bulletCollider.transform.position);
			vector -= m_Shield.ShieldColliderCenterOffset;
			if ((m_localXAxisCheck != 0 && Mathf.Sign(vector.x) != (float)m_localXAxisCheck) || (m_localYAxisCheck != 0 && Mathf.Sign(vector.y) != (float)m_localYAxisCheck) || (m_localZAxisCheck != 0 && Mathf.Sign(vector.z) != (float)m_localZAxisCheck))
			{
				flag = false;
			}
		}
		if (m_Shield.RepulsorBulletTrigger is SphereCollider && flag)
		{
			flag = !((componentInParent.FirePosition - position).sqrMagnitude < m_Shield.ShieldRadius * m_Shield.ShieldRadius);
		}
		if (!flag)
		{
			return;
		}
		if (m_Shield.RepulsorBulletTrigger is SphereCollider)
		{
			Vector3 position2 = bulletCollider.transform.position;
			Vector3 normalized = (position - position2).normalized;
			Vector3 hitPoint = position - normalized * m_Shield.ShieldRadius;
			componentInParent.HandleCollision(m_Shield.Damageable, hitPoint, bulletCollider, ForceDestroy: true);
		}
		else if (m_Shield.RepulsorBulletTrigger is BoxCollider)
		{
			Vector3 hitPoint2 = m_Shield.RepulsorBulletTrigger.ClosestPoint(bulletCollider.transform.position);
			componentInParent.HandleCollision(m_Shield.Damageable, hitPoint2, bulletCollider, ForceDestroy: true);
		}
		else if (m_Shield.RepulsorBulletTrigger is CapsuleCollider)
		{
			if (m_Shield.IsBulletTriggerCylinder)
			{
				CapsuleCollider capsuleCollider = m_Shield.RepulsorBulletTrigger as CapsuleCollider;
				if ((double)Mathf.Abs((m_Shield.RepulsorBulletTrigger.transform.InverseTransformPoint(bulletCollider.transform.position) - m_Shield.ShieldColliderCenterOffset).x) < (double)(capsuleCollider.height - capsuleCollider.radius * 2f) * 0.5)
				{
					Vector3 hitPoint3 = m_Shield.RepulsorBulletTrigger.ClosestPoint(bulletCollider.transform.position);
					componentInParent.HandleCollision(m_Shield.Damageable, hitPoint3, bulletCollider, ForceDestroy: true);
				}
			}
			else
			{
				Vector3 hitPoint4 = m_Shield.RepulsorBulletTrigger.ClosestPoint(bulletCollider.transform.position);
				componentInParent.HandleCollision(m_Shield.Damageable, hitPoint4, bulletCollider, ForceDestroy: true);
			}
		}
		else if (m_Shield.RepulsorBulletTrigger is MeshCollider)
		{
			Vector3 hitPoint5 = m_Shield.RepulsorBulletTrigger.ClosestPoint(bulletCollider.transform.position);
			componentInParent.HandleCollision(m_Shield.Damageable, hitPoint5, bulletCollider, ForceDestroy: true);
		}
	}

	private void OnTankTriggerEvent(TriggerCatcher.Interaction triggerType, Collider blockCollider)
	{
		if (triggerType != TriggerCatcher.Interaction.Enter && triggerType != TriggerCatcher.Interaction.Stay)
		{
			return;
		}
		TankBlock componentInParent = blockCollider.GetComponentInParent<TankBlock>();
		Tank tank = ((componentInParent != null) ? componentInParent.tank : null);
		int fixedFrameCount = Singleton.instance.FixedFrameCount;
		if (fixedFrameCount != m_CachedCollisionFixedFrameCount)
		{
			m_TechCollisionsThisFrame.Clear();
			m_CachedCollisionFixedFrameCount = fixedFrameCount;
		}
		if ((bool)tank && tank.Team != base.block.tank.Team && !m_TechCollisionsThisFrame.Contains(tank))
		{
			m_TechCollisionsThisFrame.Add(tank);
			Vector3 position = m_Shield.RepulsorBulletTrigger.transform.position;
			Vector3 normalized = (position - tank.trans.position).normalized;
			Vector3 position2 = position - normalized * m_Shield.ShieldRadius;
			if (triggerType == TriggerCatcher.Interaction.Enter)
			{
				hitEffect.Spawn(position2).forward = normalized;
			}
			float num = m_Shield.ShieldRadius + tank.visible.Radius;
			float num2 = (position - tank.trans.position).magnitude / num;
			float num3 = m_MinTechForce + num2 * (m_MaxTechForce - m_MinTechForce);
			tank.rbody.AddForce(-normalized * num3, ForceMode.Acceleration);
		}
	}

	private bool OnRejectShieldDamage(ManDamage.DamageInfo info, bool actuallyDealDamage)
	{
		if (IsPowered && actuallyDealDamage)
		{
			if (!base.block.tank || !info.SourceTank || info.SourceTank.IsEnemy(base.block.tank.Team))
			{
				OnServerSetEnergyDeficit(m_EnergyDeficit + m_EnergyConsumedPerDamagePoint * info.Damage);
				if ((bool)base.block.tank && base.block.tank.IsFriendly(0))
				{
					Singleton.Manager<ManStats>.inst.DamageAbsorbedByShield(this, info);
				}
			}
			Transform obj = hitEffect.Spawn(info.HitPosition);
			Vector3 normalized = (m_Shield.RepulsorBulletTrigger.transform.position - info.HitPosition).normalized;
			obj.rotation = Quaternion.LookRotation(normalized);
		}
		return true;
	}

	private void OnServerSetEnergyDeficit(float energyDeficit)
	{
		if (energyDeficit != m_EnergyDeficit)
		{
			m_EnergyDeficit = energyDeficit;
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
		}
	}

	private void OnServerSetChargingEffect(bool charging)
	{
		if (m_ChargingEffect != charging)
		{
			m_ChargingEffect = charging;
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
		}
	}

	private void OnServerSetScriptDisabled(bool scriptDisabled)
	{
		if (m_ScriptDisabled != scriptDisabled)
		{
			m_ScriptDisabled = scriptDisabled;
			if (Singleton.Manager<ManNetwork>.inst.IsServer && base.block.tank.IsNotNull())
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
		}
	}

	public void SetScriptDisabled(bool scriptDisabled)
	{
		OnServerSetScriptDisabled(scriptDisabled);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleShieldGenerator;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write(m_EnergyDeficit);
		writer.Write(m_EnoughPower);
		writer.Write(m_ChargingEffect);
		writer.Write(m_ScriptDisabled);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		m_EnergyDeficit = reader.ReadSingle();
		m_EnoughPower = reader.ReadBoolean();
		m_ChargingEffect = reader.ReadBoolean();
		m_ScriptDisabled = reader.ReadBoolean();
	}

	private void PrePool()
	{
		if ((bool)m_ChargingParticlesPrefab)
		{
			m_ChargingParticles = UnityEngine.Object.Instantiate(m_ChargingParticlesPrefab);
			m_ChargingParticles.transform.parent = base.transform;
			m_ChargingParticles.transform.localRotation = Quaternion.identity;
		}
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
	}

	private void OnPool()
	{
		m_ControlCategoryType = (m_Healing ? ModuleControlCategory.Regen : ModuleControlCategory.Shield);
		m_Shield = GetComponentsInChildren<BubbleShield>(includeInactive: true).FirstOrDefault();
		m_Shield.SetRanges(m_Radius, new float[2] { 0f, m_ParticleLife }, new float[2] { 0f, m_ParticleSpeed });
		if (m_Repulsion)
		{
			Damageable damageable = m_Shield.Damageable;
			damageable.InitHealth(-1337f);
			damageable.SetRejectDamageHandler(OnRejectShieldDamage);
		}
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		m_ModuleEnergy.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		m_Warning = new WarningHolder(base.block.visible, WarningHolder.WarningType.ShieldPowered);
		base.block.serializeEvent.Subscribe(OnSerialize);
		m_SequenceNode = new TechSequencer.SequenceNode(base.block);
	}

	private void OnSpawn()
	{
		m_State = State.Disabled;
		m_EnergyDeficit = m_InitialChargeEnergy;
		m_EnergyDrain = 0f;
		m_Shield.SetTargetScale(0f);
		if ((bool)m_ChargingParticles)
		{
			m_ChargingParticles.transform.position = base.block.centreOfMassWorld;
			m_ChargingParticles.Stop();
		}
	}

	private void OnRecycle()
	{
		m_ScriptDisabled = false;
	}
}
