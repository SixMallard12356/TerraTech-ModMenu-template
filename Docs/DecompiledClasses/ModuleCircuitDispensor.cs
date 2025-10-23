#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitNode))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuitDispensor : Module, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public Circuits.BlockChargeData m_LastDispensedChargeData;
	}

	[SerializeField]
	[Range(1f, 1000f)]
	protected int m_DefaultChargeStrength = 1;

	private ICircuitDispensor[] m_Dispensors;

	private CircuitAPSignalIndicator[] m_OutputIndicators;

	private Circuits.BlockChargeData NextDispensableChargeCache = new Circuits.BlockChargeData();

	private static int _s_dispensorOutputChargeOnAP;

	public int DefaultChargeStrength => m_DefaultChargeStrength;

	public int CurrentHighestChargeFromHere => LastDispensedChargeData.Value.ChargeStrength;

	public Circuits.BlockChargeDataWrapper LastDispensedChargeData { get; private set; } = new Circuits.BlockChargeDataWrapper();

	public Circuits.BlockChargeData LastDispensedCharge => LastDispensedChargeData.Value;

	protected void DispenseCharge()
	{
		if (!base.block.IsAttached)
		{
			return;
		}
		ICircuitDispensor[] dispensors = m_Dispensors;
		foreach (ICircuitDispensor circuitDispensor in dispensors)
		{
			Vector3[] chargeOutPoints = base.block.CircuitNode.ChargeOutPoints;
			foreach (Vector3 vector in chargeOutPoints)
			{
				_s_dispensorOutputChargeOnAP = circuitDispensor.GetDispensableCharge(vector);
				if (_s_dispensorOutputChargeOnAP > 0)
				{
					NextDispensableChargeCache.ChargedAPs.Add(vector);
				}
				NextDispensableChargeCache.AllChargeAPsAndCharges[vector] = ((!NextDispensableChargeCache.AllChargeAPsAndCharges.ContainsKey(vector) || _s_dispensorOutputChargeOnAP > NextDispensableChargeCache.AllChargeAPsAndCharges[vector]) ? _s_dispensorOutputChargeOnAP : NextDispensableChargeCache.AllChargeAPsAndCharges[vector]);
				NextDispensableChargeCache.ChargeStrength = ((_s_dispensorOutputChargeOnAP > NextDispensableChargeCache.ChargeStrength) ? _s_dispensorOutputChargeOnAP : NextDispensableChargeCache.ChargeStrength);
			}
		}
		SendChargeToOutputs();
		CaptureLastDispensedChargeData();
	}

	protected void SendChargeToOutputs()
	{
		if (base.block.CircuitNode == null)
		{
			d.LogError("No Circuit Node exists for circuit dispensor on " + base.gameObject.name + "!! Aborting!");
			return;
		}
		foreach (KeyValuePair<Vector3, int> allChargeAPsAndCharge in NextDispensableChargeCache.AllChargeAPsAndCharges)
		{
			Vector3 key = allChargeAPsAndCharge.Key;
			int value = allChargeAPsAndCharge.Value;
			base.block.CircuitNode.block.tank.Circuits.GetNetworkWithID(base.block.CircuitNode.OutputAPNetworkIDs[key]).AddCharge(value, base.block.CircuitNode.ConnexionLookupByAP[key]);
		}
	}

	private void ResetCachedInfo()
	{
		NextDispensableChargeCache.SetDefault();
	}

	private void CaptureLastDispensedChargeData()
	{
		LastDispensedChargeData.Set(NextDispensableChargeCache);
	}

	private void UpdateIndicators()
	{
		CircuitAPSignalIndicator.TryRefreshIndicators(LastDispensedChargeData.Value.ChargedAPs, m_OutputIndicators);
	}

	private void OnAttached()
	{
		Circuits.StartChargeUpdate.Subscribe(ResetCachedInfo);
		Circuits.GenerateChargeUpdate.Subscribe(DispenseCharge);
		Circuits.PostSlowUpdate.Subscribe(OnCircuitVisualUpdate);
		ResetCachedInfo();
		CaptureLastDispensedChargeData();
	}

	private void OnDetaching()
	{
		Circuits.StartChargeUpdate.Unsubscribe(ResetCachedInfo);
		Circuits.GenerateChargeUpdate.Unsubscribe(DispenseCharge);
		Circuits.PostSlowUpdate.Unsubscribe(OnCircuitVisualUpdate);
		ResetCachedInfo();
		CaptureLastDispensedChargeData();
		UpdateIndicators();
	}

	private void OnCircuitVisualUpdate()
	{
		if (ManNetwork.IsNetworked && ManNetwork.IsHost)
		{
			LastDispensedChargeData.SyncNetProperty();
		}
	}

	private void OnLastDispensedChargeDataChanged(Circuits.BlockChargeData newChargeData)
	{
		UpdateIndicators();
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec blockSpec)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				m_LastDispensedChargeData = LastDispensedChargeData.Value
			};
			serialData.Store(blockSpec.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData != null)
		{
			LastDispensedChargeData.Set(serialData.m_LastDispensedChargeData);
			if (ManNetwork.IsNetworked && ManNetwork.IsHost)
			{
				LastDispensedChargeData.SyncNetProperty();
			}
		}
	}

	private void OnPool()
	{
		LastDispensedChargeData.Init(this, TTMsgType.SyncCircuitDispensor_CachedCharge);
		LastDispensedChargeData.ChangedEvent.Subscribe(OnLastDispensedChargeDataChanged);
		m_Dispensors = GetComponents<ICircuitDispensor>();
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialze);
		m_OutputIndicators = (from r in GetComponentsInChildren<CircuitAPSignalIndicator>(includeInactive: true)
			where (r.ConnexionDisplayFlags & ModuleCircuitNode.ConnexionTypes.Output) != 0
			select r).ToArray();
		CircuitAPSignalIndicator[] outputIndicators = m_OutputIndicators;
		for (int num = 0; num < outputIndicators.Length; num++)
		{
			outputIndicators[num].InitOnBlock(base.block);
		}
	}

	private void OnDepool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Dispensor;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		LastDispensedChargeData.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		LastDispensedChargeData.Deserialise(reader);
	}
}
