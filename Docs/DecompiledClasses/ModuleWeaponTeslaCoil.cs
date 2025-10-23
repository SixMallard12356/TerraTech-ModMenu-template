#define UNITY_EDITOR
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleEnergy))]
public class ModuleWeaponTeslaCoil : Module, IModuleDamager, INetworkedModule
{
	private enum TeslaVisualState
	{
		Dormant,
		Charging,
		WaitingToFire,
		Firing,
		Cooldown
	}

	public TechEnergy.EnergyType m_EnergyType;

	public float m_TargetRadius = 10f;

	public float m_PowerUsagePerArc = 30f;

	[FormerlySerializedAs("m_ArcFiringInterval")]
	public float m_ArcFiringCooldown = 0.5f;

	public float m_DamagePerArc = 100f;

	public float m_EnergyCutOffPercentage = 25f;

	public float m_ChargeDurationBeforeFiring = 9f;

	public ManDamage.DamageType m_DamageType = ManDamage.DamageType.Energy;

	public bool m_checkLOS = true;

	[SerializeField]
	private bool m_FireWarningShotsAtPlayer;

	[SerializeField]
	[Tooltip("Anything found within this range, will be damaged")]
	private int m_WarningShotMinimumRange;

	[SerializeField]
	private Transform m_TeslaImpact;

	[SerializeField]
	private Transform m_TeslaWarningImpact;

	[SerializeField]
	private FMODEvent m_ArcSfxEvent;

	[SerializeField]
	private FMODEvent[] m_ChargeSfxEvents;

	[SerializeField]
	private float[] m_ChargeSfxDelays;

	public Transform m_ArcEffectOrigin;

	[SerializeField]
	protected bool m_IsUsedOnCircuit;

	[SerializeField]
	[Tooltip("When set to true, the entire charge cycle will be blocked until circuit power is provided. Set to false, the coil will charge up once but not fire until a circuit signal is received")]
	private bool m_CircuitBlocksCharging = true;

	[SerializeField]
	private bool m_ChargeWithoutTarget;

	private ModuleEnergy m_ModuleEnergy;

	private ArcEffect[] m_ArcEffects;

	private TankBlock m_CurrentTargetBlock;

	private Damageable m_CurrentTargetDamageable;

	private Vector3 m_CurrentTargetHitPos;

	private float m_AccumulatedCharge;

	private float m_ArcTimer;

	private float m_ChargeTimer;

	private int m_ArcEffectIndex;

	private int m_ArcEffectVariant;

	private float m_SfxTimer;

	private float m_EnergyRefundAmount;

	private Vector3 m_TargetImpactPosition;

	private bool m_ClientFiredArc;

	private Transform m_CurrentParticleEffect;

	private const float kTargetUpdateTimeout = 0.5f;

	private float m_NextTargetUpdateTime;

	private TeslaVisualState m_VisualState;

	private static readonly Bitfield<ObjectTypes> k_MaskVehicles = new Bitfield<ObjectTypes>(1);

	[SerializeField]
	private Transform m_ParticleSetupPrefab;

	ManDamage.DamageType IModuleDamager.DamageType => m_DamageType;

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

	private float SquareTargetRadius => m_TargetRadius * m_TargetRadius;

	private float SquareWarningRadius => m_WarningShotMinimumRange * m_WarningShotMinimumRange;

	private void UpdateTargetBlock()
	{
		if (!ManNetwork.IsHost)
		{
			return;
		}
		TankBlock target = null;
		Vector3 hitPos = Vector3.zero;
		Damageable damageable = null;
		int pickerMask = Globals.inst.layerTank.mask | Globals.inst.layerCosmetic.mask;
		float cacheRadiusMul = 2f;
		float num = float.MaxValue;
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadiusCached(base.block.centreOfMassWorld, m_TargetRadius, k_MaskVehicles, cacheRadiusMul, pickerMask))
		{
			if (item.tank == null)
			{
				continue;
			}
			bool flag = item.tank.IsEnemy(base.block.tank.Team);
			if (m_FireWarningShotsAtPlayer)
			{
				flag = ((!Singleton.Manager<ManNetwork>.inst.IsServer) ? item.tank.IsPlayer : (item.tank.IsPlayer || item.tank.netTech.NetPlayer.IsNotNull()));
			}
			if (!(item.tank != base.block.tank && flag))
			{
				continue;
			}
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = item.tank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current2 = enumerator.Current;
				float distSq = 0f;
				Vector3 from = Vector3.zero;
				Vector3 to = Vector3.zero;
				Vector3 hitPoint = Vector3.zero;
				Damageable hitDamageable = null;
				if (IsBlockValidTarget(current2, out distSq, out from, out to, out hitPoint, out hitDamageable) && distSq < num)
				{
					num = distSq;
					target = current2;
					hitPos = hitPoint;
					damageable = hitDamageable;
				}
			}
		}
		OnServerSetTargetBlock(target, hitPos, damageable);
	}

	private bool IsBlockValidTarget(TankBlock targetBlock, out float distSq, out Vector3 from, out Vector3 to, out Vector3 hitPoint, out Damageable hitDamageable)
	{
		distSq = 0f;
		from = Vector3.zero;
		to = Vector3.zero;
		hitPoint = Vector3.zero;
		hitDamageable = null;
		if (targetBlock == null || targetBlock.tank == null)
		{
			return false;
		}
		Damageable component = targetBlock.GetComponent<Damageable>();
		if (component == null || component.Health <= 0f)
		{
			return false;
		}
		float squareTargetRadius = SquareTargetRadius;
		distSq = (targetBlock.centreOfMassWorld - base.block.centreOfMassWorld).sqrMagnitude;
		if (distSq <= squareTargetRadius)
		{
			if ((bool)m_ArcEffectOrigin)
			{
				from = m_ArcEffectOrigin.position;
			}
			else
			{
				from = m_ArcEffects[m_ArcEffectIndex].GetLineTransform().position;
			}
			to = targetBlock.centreOfMassWorld;
			if (HasClearLineOfSight(from, to, targetBlock, out hitPoint, out hitDamageable))
			{
				return true;
			}
		}
		return false;
	}

	private void FireNewArcEffect()
	{
		bool flag = ((int)Random.value & 1) == 1;
		switch (m_ArcEffectVariant)
		{
		case 0:
			m_ArcEffectVariant = (flag ? 1 : 2);
			break;
		case 1:
			m_ArcEffectVariant = (flag ? 2 : 0);
			break;
		case 2:
			m_ArcEffectVariant = ((!flag) ? 1 : 0);
			break;
		}
		m_ArcEffects[m_ArcEffectIndex].Fire(m_ArcEffectVariant);
		m_ArcEffectIndex = (m_ArcEffectIndex + 1) % m_ArcEffects.Length;
		if (m_ArcSfxEvent.IsValid())
		{
			m_ArcSfxEvent.PlayOneShot(base.transform.position);
		}
	}

	private bool IsTargetWithinDamageRadius()
	{
		Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
		float sqrMagnitude = (m_CurrentTargetHitPos - centreOfMassWorld).sqrMagnitude;
		if (m_FireWarningShotsAtPlayer)
		{
			return sqrMagnitude <= SquareWarningRadius;
		}
		return sqrMagnitude <= SquareTargetRadius;
	}

	private void UpdateArcEffects()
	{
		if (m_CurrentTargetBlock.IsNotNull())
		{
			Vector3 position = m_ArcEffectOrigin.position;
			m_TargetImpactPosition = m_CurrentTargetHitPos;
			if (m_FireWarningShotsAtPlayer && !IsTargetWithinDamageRadius())
			{
				m_TargetImpactPosition = Singleton.Manager<ManWorld>.inst.ProjectToGround(position + (m_TargetImpactPosition - position).normalized * m_WarningShotMinimumRange);
			}
			ArcEffect[] arcEffects = m_ArcEffects;
			for (int i = 0; i < arcEffects.Length; i++)
			{
				arcEffects[i].UpdatePositionIfActive(position, m_TargetImpactPosition);
			}
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleWeaponTeslaCoil;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		if (m_CurrentTargetBlock.IsNotNull())
		{
			writer.Write(m_CurrentTargetBlock.blockPoolID);
		}
		else
		{
			writer.Write(0);
		}
		writer.Write((int)m_VisualState);
		writer.Write(m_CurrentTargetHitPos);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		uint num = reader.ReadUInt32();
		TeslaVisualState newState = (TeslaVisualState)reader.ReadInt32();
		m_CurrentTargetHitPos = reader.ReadVector3();
		OnClientVisualStateUpdate(newState);
		if (num != 0)
		{
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(num);
			if (tankBlock.IsNotNull())
			{
				m_CurrentTargetBlock = tankBlock;
			}
			else
			{
				d.LogError("Cannot find target block with id: " + num);
			}
		}
		else
		{
			m_CurrentTargetBlock = null;
		}
	}

	private void OnClientVisualStateUpdate(TeslaVisualState newState)
	{
		if (m_VisualState == newState)
		{
			return;
		}
		m_VisualState = newState;
		switch (m_VisualState)
		{
		case TeslaVisualState.Charging:
		case TeslaVisualState.WaitingToFire:
			StartChargeSequenceVisual();
			m_ClientFiredArc = false;
			break;
		case TeslaVisualState.Firing:
		case TeslaVisualState.Cooldown:
			if (!m_ClientFiredArc)
			{
				FireNewArcEffect();
				m_ClientFiredArc = true;
			}
			break;
		}
	}

	private void OnHostSetVisualState(TeslaVisualState newState)
	{
		if (newState != m_VisualState && ManNetwork.IsHost)
		{
			m_VisualState = newState;
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
			if (newState == TeslaVisualState.Charging)
			{
				UpdateTargetBlock();
			}
			if (newState == TeslaVisualState.Dormant && m_CurrentParticleEffect != null)
			{
				m_CurrentParticleEffect.Recycle();
				d.Assert(m_CurrentParticleEffect == null, "ParticleSystem Recycle should have gone through the recycle callback and already set m_CurrentParticleEffect to null!");
			}
		}
	}

	private void OnServerSetTargetBlock(TankBlock target, Vector3 hitPos, Damageable damageable)
	{
		if (m_CurrentTargetBlock != target)
		{
			m_CurrentTargetBlock = target;
			m_CurrentTargetDamageable = damageable;
			m_CurrentTargetHitPos = hitPos;
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
		}
	}

	private void StartChargeSequenceVisual()
	{
		m_CurrentParticleEffect = m_ParticleSetupPrefab.SpawnWithLocalTransform(base.transform, Vector3.zero, Quaternion.identity);
		m_CurrentParticleEffect.GetComponent<TeslaParticleSetup>().SetOffsetAndDuration(m_ChargeDurationBeforeFiring);
		m_ChargeTimer = Time.time + m_ChargeDurationBeforeFiring;
		m_SfxTimer = 0f;
		m_CurrentParticleEffect.GetComponent<ParticleRecycler>().ParticleSystemRecycledEvent.Subscribe(OnCoilParticlesRecycled);
	}

	private void ClearTarget()
	{
		OnServerSetTargetBlock(null, Vector3.zero, null);
		m_ArcTimer = Time.time + m_ArcFiringCooldown;
		OnHostSetVisualState(TeslaVisualState.Dormant);
	}

	float IModuleDamager.GetHitDamage()
	{
		return m_DamagePerArc;
	}

	float IModuleDamager.GetHitsPerSec()
	{
		return 1f / (m_ChargeDurationBeforeFiring + m_ArcFiringCooldown);
	}

	private void OnUpdateSupplyEnergy()
	{
		if (m_EnergyRefundAmount > 0f)
		{
			m_ModuleEnergy.Supply(m_EnergyType, m_EnergyRefundAmount);
			m_EnergyRefundAmount = 0f;
		}
	}

	private void OnUpdateConsumeEnergy()
	{
		bool num = !base.block.IsAttached || base.block.tank.beam.IsActive;
		bool flag = CircuitControlled && !base.block.CircuitReceiver.ShouldProcessInput;
		bool flag2 = CircuitControlled && m_CircuitBlocksCharging && base.block.CircuitReceiver.CurrentChargeData.ChargeStrength == 0;
		if (num || flag || flag2)
		{
			if (m_AccumulatedCharge > 0f)
			{
				m_EnergyRefundAmount = m_AccumulatedCharge;
				m_AccumulatedCharge = 0f;
			}
			if (m_VisualState != TeslaVisualState.Dormant)
			{
				OnHostSetVisualState(TeslaVisualState.Dormant);
			}
			return;
		}
		if (ManNetwork.IsHost && m_ModuleEnergy.GetCurrentAmount(m_EnergyType) / m_ModuleEnergy.GetTotalCapacity(m_EnergyType) * 100f > m_EnergyCutOffPercentage && m_AccumulatedCharge < m_PowerUsagePerArc)
		{
			float amount = m_PowerUsagePerArc - m_AccumulatedCharge;
			m_AccumulatedCharge += m_ModuleEnergy.ConsumeUpToMax(m_EnergyType, amount);
		}
		if (m_VisualState == TeslaVisualState.Cooldown)
		{
			if (Time.time > m_ArcTimer)
			{
				OnHostSetVisualState(TeslaVisualState.Dormant);
			}
			UpdateArcEffects();
			return;
		}
		if (m_VisualState == TeslaVisualState.Charging)
		{
			UpdateChargingSfx();
		}
		if (ManNetwork.IsHost)
		{
			CheckForTarget();
			if (m_AccumulatedCharge >= m_PowerUsagePerArc)
			{
				if (m_VisualState == TeslaVisualState.Firing)
				{
					if (m_CurrentTargetBlock != null)
					{
						if (!m_FireWarningShotsAtPlayer || IsTargetWithinDamageRadius())
						{
							Fire();
						}
						else if (m_FireWarningShotsAtPlayer && (bool)m_TeslaWarningImpact)
						{
							m_TeslaWarningImpact.Spawn(m_TargetImpactPosition);
						}
						m_AccumulatedCharge -= m_PowerUsagePerArc;
						FireNewArcEffect();
						m_ArcTimer = Time.time + m_ArcFiringCooldown + 0.05f;
						OnHostSetVisualState(TeslaVisualState.Cooldown);
					}
				}
				else if (m_VisualState == TeslaVisualState.WaitingToFire)
				{
					if (!CircuitControlled || base.block.CircuitReceiver.CurrentChargeData > 0)
					{
						OnHostSetVisualState(TeslaVisualState.Firing);
					}
				}
				else
				{
					if (m_VisualState == TeslaVisualState.Dormant && (m_CurrentTargetBlock.IsNotNull() || m_ChargeWithoutTarget))
					{
						StartChargeSequenceVisual();
						OnHostSetVisualState(TeslaVisualState.Charging);
					}
					if (m_VisualState == TeslaVisualState.Charging)
					{
						if (m_CurrentTargetBlock.IsNull() && !m_ChargeWithoutTarget)
						{
							ClearTarget();
						}
						if (Time.time >= m_ChargeTimer)
						{
							OnHostSetVisualState(TeslaVisualState.WaitingToFire);
						}
					}
				}
			}
		}
		else if (m_CurrentTargetBlock.IsNotNull())
		{
			Vector3 position = m_ArcEffects[m_ArcEffectIndex].GetLineTransform().position;
			Vector3 centreOfMassWorld = m_CurrentTargetBlock.centreOfMassWorld;
			Vector3 hitPoint = Vector3.zero;
			Damageable hitDamageable = null;
			HasClearLineOfSight(position, centreOfMassWorld, m_CurrentTargetBlock, out hitPoint, out hitDamageable);
			m_CurrentTargetHitPos = hitPoint;
			m_CurrentTargetDamageable = hitDamageable;
		}
		UpdateArcEffects();
	}

	private void OnCoilParticlesRecycled(ParticleRecycler particleRecycler)
	{
		particleRecycler.ParticleSystemRecycledEvent.Unsubscribe(OnCoilParticlesRecycled);
		m_CurrentParticleEffect = null;
	}

	private void CheckForTarget()
	{
		float time = Time.time;
		if (!(time < m_NextTargetUpdateTime))
		{
			m_NextTargetUpdateTime = time + 0.5f;
			if (!IsBlockValidTarget(m_CurrentTargetBlock, out var _, out var _, out var _, out var hitPoint, out var hitDamageable))
			{
				UpdateTargetBlock();
				return;
			}
			m_CurrentTargetDamageable = hitDamageable;
			m_CurrentTargetHitPos = hitPoint;
		}
	}

	private void Fire()
	{
		Singleton.Manager<ManDamage>.inst.DealDamage(m_CurrentTargetDamageable, m_DamagePerArc, m_DamageType, this, base.block.tank, m_CurrentTargetHitPos);
		m_TeslaImpact.Spawn(m_CurrentTargetHitPos);
	}

	private void UpdateChargingSfx()
	{
		float sfxTimer = m_SfxTimer;
		m_SfxTimer += Time.deltaTime;
		for (int i = 0; i < m_ChargeSfxEvents.Length && i < m_ChargeSfxDelays.Length; i++)
		{
			if (sfxTimer <= m_ChargeSfxDelays[i] && m_SfxTimer > m_ChargeSfxDelays[i] && m_ChargeSfxEvents[i].IsValid())
			{
				m_ChargeSfxEvents[i].PlayOneShot(base.transform.position);
			}
		}
	}

	private bool HasClearLineOfSight(Vector3 from, Vector3 to, TankBlock targetBlock, out Vector3 hitPoint, out Damageable hitDamageable)
	{
		bool result = true;
		int layerMask = Globals.inst.layerScenery.mask | Globals.inst.layerWater.mask | Globals.inst.layerLandmark.mask | Globals.inst.layerTerrain.mask | Globals.inst.layerPickup.mask | Globals.inst.layerTank.mask | Globals.inst.layerTankIgnoreTerrain.mask | Globals.inst.layerContainer.mask | Globals.inst.layerShieldBulletsFilter.mask;
		hitPoint = to;
		hitDamageable = targetBlock.GetComponent<Damageable>();
		if (!m_checkLOS)
		{
			return true;
		}
		Vector3 normalized = (to - from).normalized;
		float magnitude = (to - from).magnitude;
		RaycastHit[] array = Physics.RaycastAll(from, normalized, magnitude, layerMask, QueryTriggerInteraction.Collide);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit raycastHit = array[i];
			Visible visible = Visible.FindVisibleUpwards(raycastHit.collider);
			TankBlock tankBlock = ((visible != null) ? visible.block : null);
			if (raycastHit.rigidbody != base.block.tank.rbody && tankBlock != targetBlock)
			{
				hitPoint = raycastHit.collider.ClosestPoint(from);
				hitDamageable = raycastHit.collider.GetComponentInParents<Damageable>(thisObjectFirst: true);
				if (raycastHit.collider.gameObject.layer != (int)Globals.inst.layerShieldBulletsFilter)
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	private void OnDetaching()
	{
		m_AccumulatedCharge = 0f;
		OnHostSetVisualState(TeslaVisualState.Dormant);
		if (m_CurrentParticleEffect != null)
		{
			m_CurrentParticleEffect.Recycle();
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
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		m_ModuleEnergy.UpdateSupplyEvent.Subscribe(OnUpdateSupplyEnergy);
		m_ModuleEnergy.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		LineRenderer[] componentsInChildren = GetComponentsInChildren<LineRenderer>(includeInactive: true);
		m_ArcEffects = componentsInChildren.Select((LineRenderer l) => new ArcEffect(l)).ToArray();
	}

	private void OnSpawn()
	{
		m_CurrentTargetBlock = null;
		m_CurrentTargetDamageable = null;
		m_ArcTimer = Random.Range(0f, m_ArcFiringCooldown);
		m_ArcEffectIndex = 0;
		ArcEffect[] arcEffects = m_ArcEffects;
		for (int i = 0; i < arcEffects.Length; i++)
		{
			arcEffects[i].Hide();
		}
	}

	private void OnRecycle()
	{
		TeslaParticleSetup[] componentsInChildren = base.transform.GetComponentsInChildren<TeslaParticleSetup>();
		foreach (TeslaParticleSetup obj in componentsInChildren)
		{
			obj.gameObject.transform.parent = null;
			obj.gameObject.transform.Recycle();
		}
	}

	private void OnDrawGizmos()
	{
		_ = (bool)m_CurrentTargetBlock;
	}

	private void OnValidate()
	{
		d.Assert(m_ParticleSetupPrefab != null && m_ParticleSetupPrefab.GetComponent<ParticleRecycler>() != null, "Tesla Coil must have a particle system prefab with a ParticleRecycler attached!");
	}
}
