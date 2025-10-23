#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleEnergy))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleRemoteCharger : Module, INetworkedModule
{
	private struct SortedStore
	{
		public ModuleEnergyStore store;

		public float distSq;
	}

	private new class SerialData : SerialData<SerialData>
	{
		public bool blockChargerEnabled;
	}

	public TechEnergy.EnergyType m_EnergyType;

	public float m_ChargingRadius = 10f;

	public float m_PowerTransferPerArc = 30f;

	public float m_ArcFiringInterval = 0.5f;

	public float m_MinPowerTransfer = 10f;

	public float m_ChargeConeAngleDegrees = 10f;

	[SerializeField]
	private FMODEvent m_ChargeSfxEvent;

	[SerializeField]
	private bool m_IsUsedOnCircuit = true;

	private ModuleEnergy m_ModuleEnergy;

	private ArcEffect[] m_ArcEffects;

	private ModuleEnergyStore m_CurrentTarget;

	private float m_AccumulatedCharge;

	private float m_ArcTimer;

	private int m_ArcEffectIndex;

	private int m_ArcEffectVariant;

	private bool m_EnableRemoteCharging = true;

	private List<SortedStore> m_StoresInRange = new List<SortedStore>(10);

	private static readonly Bitfield<ObjectTypes> k_MaskVehicles = new Bitfield<ObjectTypes>(1);

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

	public void SetChargerStatus(bool chargeEnabledStatus)
	{
		m_EnableRemoteCharging = chargeEnabledStatus;
	}

	private void UpdateStoresInRange()
	{
		float num = m_ChargingRadius * m_ChargingRadius;
		m_StoresInRange.Clear();
		int pickerMask = Globals.inst.layerTank.mask | Globals.inst.layerCosmetic.mask;
		float cacheRadiusMul = 2f;
		foreach (Visible item in Singleton.Manager<ManVisible>.inst.VisiblesTouchingRadiusCached(base.block.centreOfMassWorld, m_ChargingRadius, k_MaskVehicles, cacheRadiusMul, pickerMask))
		{
			if (item.tank == null || !(item.tank != base.block.tank) || item.tank.IsEnemy(base.block.tank.Team))
			{
				continue;
			}
			BlockManager.BlockIterator<ModuleEnergyStore>.Enumerator enumerator = item.tank.blockman.IterateBlockComponents<ModuleEnergyStore>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				ModuleEnergyStore current2 = enumerator.Current;
				if (current2.m_EnergyType == m_EnergyType && current2.m_AcceptRemoteCharge)
				{
					Vector3 a = current2.block.centreOfMassWorld - base.block.centreOfMassWorld;
					Vector3 b = base.block.transform.right.normalized * m_ChargingRadius;
					float sqrMagnitude = a.sqrMagnitude;
					a.y = 0f;
					b.y = 0f;
					float f = Mathf.Acos(a.Dot(b) / (b.magnitude * a.magnitude));
					if (sqrMagnitude < num && Mathf.Abs(f) < (float)Math.PI / 180f * m_ChargeConeAngleDegrees * 0.5f)
					{
						m_StoresInRange.Add(new SortedStore
						{
							store = current2,
							distSq = sqrMagnitude
						});
					}
				}
			}
		}
		m_StoresInRange.Sort(CompareSortedStores);
	}

	private static int CompareSortedStores(SortedStore a, SortedStore b)
	{
		return (int)((a.distSq - b.distSq) * 1000f);
	}

	private bool ChargeOneStoreInRange()
	{
		bool result = false;
		if (ManNetwork.IsHost)
		{
			ModuleEnergyStore moduleEnergyStore = null;
			ModuleEnergyStore moduleEnergyStore2 = null;
			float num = float.MinValue;
			foreach (SortedStore item in m_StoresInRange)
			{
				ModuleEnergyStore store = item.store;
				float spareCapacity = store.SpareCapacity;
				if (spareCapacity >= m_AccumulatedCharge || store.CurrentAmount == 0f)
				{
					moduleEnergyStore = store;
					break;
				}
				if (spareCapacity > num && spareCapacity > 0f && spareCapacity > m_MinPowerTransfer)
				{
					moduleEnergyStore2 = store;
					num = spareCapacity;
				}
			}
			if (moduleEnergyStore == null && moduleEnergyStore2 != null)
			{
				moduleEnergyStore = moduleEnergyStore2;
			}
			if (moduleEnergyStore != null)
			{
				float num2 = Mathf.Min(m_AccumulatedCharge, moduleEnergyStore.SpareCapacity);
				moduleEnergyStore.AddEnergy(num2);
				m_AccumulatedCharge -= num2;
				FireNewArcEffect();
				result = true;
			}
			OnServerSetTarget(moduleEnergyStore);
		}
		else if (m_CurrentTarget.IsNotNull())
		{
			FireNewArcEffect();
			result = true;
		}
		return result;
	}

	private void FireNewArcEffect()
	{
		bool flag = ((int)UnityEngine.Random.value & 1) == 1;
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
		if (m_ChargeSfxEvent.IsValid())
		{
			m_ChargeSfxEvent.PlayOneShot(base.transform.position);
		}
	}

	private void UpdateArcEffects()
	{
		if ((bool)m_CurrentTarget)
		{
			Vector3 centreOfMassWorld = base.block.centreOfMassWorld;
			Vector3 centreOfMassWorld2 = m_CurrentTarget.block.centreOfMassWorld;
			ArcEffect[] arcEffects = m_ArcEffects;
			for (int i = 0; i < arcEffects.Length; i++)
			{
				arcEffects[i].UpdatePositionIfActive(centreOfMassWorld, centreOfMassWorld2);
			}
		}
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleRemoteCharger;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		if (m_CurrentTarget.IsNotNull())
		{
			writer.Write(m_CurrentTarget.block.blockPoolID);
		}
		else
		{
			writer.Write(0);
		}
	}

	public void OnDeserialize(NetworkReader reader)
	{
		uint num = reader.ReadUInt32();
		if (num != 0)
		{
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(num);
			if (tankBlock.IsNotNull())
			{
				m_CurrentTarget = tankBlock.GetComponent<ModuleEnergyStore>();
			}
			else
			{
				d.LogError("Cannot find target block with id: " + num);
			}
		}
		else
		{
			m_CurrentTarget = null;
		}
	}

	private void OnServerSetTarget(ModuleEnergyStore target)
	{
		if (m_CurrentTarget != target)
		{
			m_CurrentTarget = target;
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				base.block.tank.netTech.SetModuleDirty(this);
			}
		}
	}

	private void OnUpdateConsumeEnergy()
	{
		if (!base.block.tank)
		{
			m_StoresInRange.Clear();
		}
		else
		{
			if (!m_EnableRemoteCharging || (CircuitControlled && base.block.CircuitReceiver.CurrentChargeData.ChargeStrength == 0))
			{
				return;
			}
			if (ManNetwork.IsHost && m_AccumulatedCharge < m_PowerTransferPerArc)
			{
				float amount = m_PowerTransferPerArc - m_AccumulatedCharge;
				m_AccumulatedCharge += m_ModuleEnergy.ConsumeUpToMax(m_EnergyType, amount);
			}
			bool flag = ManNetwork.IsHost && m_AccumulatedCharge >= m_PowerTransferPerArc;
			bool flag2 = !ManNetwork.IsHost && m_CurrentTarget.IsNotNull();
			if (Time.time >= m_ArcTimer)
			{
				bool flag3 = false;
				if (flag || flag2)
				{
					UpdateStoresInRange();
					flag3 = ChargeOneStoreInRange();
					m_ArcTimer = Time.time + m_ArcFiringInterval;
				}
				if (ManNetwork.IsHost && !flag3)
				{
					OnServerSetTarget(null);
				}
			}
			UpdateArcEffects();
		}
	}

	private void OnDetaching()
	{
		m_AccumulatedCharge = 0f;
	}

	private void PrePool()
	{
		if (m_IsUsedOnCircuit)
		{
			this.SetupForCircuitInput();
		}
		m_MinPowerTransfer = Mathf.Min(m_MinPowerTransfer, m_PowerTransferPerArc);
	}

	private void OnPool()
	{
		m_ModuleEnergy = GetComponent<ModuleEnergy>();
		m_ModuleEnergy.UpdateConsumeEvent.Subscribe(OnUpdateConsumeEnergy);
		m_EnableRemoteCharging = true;
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		LineRenderer[] componentsInChildren = GetComponentsInChildren<LineRenderer>(includeInactive: true);
		m_ArcEffects = componentsInChildren.Select((LineRenderer l) => new ArcEffect(l)).ToArray();
	}

	private void OnAttached()
	{
		TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(base.block.tank.visible.ID);
		if (trackedVisible != null && trackedVisible.IsVendor)
		{
			m_EnableRemoteCharging = Singleton.Manager<ManWorld>.inst.Vendors.RemoteChargingActive;
		}
	}

	private void OnSpawn()
	{
		m_CurrentTarget = null;
		m_StoresInRange.Clear();
		m_ArcTimer = UnityEngine.Random.Range(0f, m_ArcFiringInterval);
		m_ArcEffectIndex = 0;
		ArcEffect[] arcEffects = m_ArcEffects;
		for (int i = 0; i < arcEffects.Length; i++)
		{
			arcEffects[i].Hide();
		}
	}

	private void OnRecycle()
	{
	}

	private void OnDrawGizmos()
	{
		if ((bool)m_CurrentTarget)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(m_CurrentTarget.block.trans.position, Vector3.one * 1.1f);
		}
	}
}
