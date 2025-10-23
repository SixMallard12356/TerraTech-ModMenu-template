#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitNode))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuitReceiver : Module, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public Circuits.BlockChargeData CurrentCharge;

		public Circuits.BlockChargeData CurrentFrameCharge;

		public Circuits.BlockChargeData PreviousCharge;

		public bool InitialisedOnNetwork;
	}

	public EventNoParams InstantRefreshEvent;

	private Circuits.BlockChargeData m_CachedChargeInfo = Circuits.BlockChargeData.empty;

	private bool m_ProcessCharge;

	private bool m_GatherExtensiveChargeData;

	private CircuitAPSignalIndicator[] m_InputIndicators;

	private static ProfilerMarker markerCollectInputAPs = new ProfilerMarker("Circuit.ReleaseCharge.ReleaseCharge.CollectInputAPs");

	private static ProfilerMarker markerCacheAPCharges = new ProfilerMarker("Circuit.ReleaseCharge.ReleaseCharge.CacheAPCharges");

	private static ProfilerMarker markerCacheHighestAPCharges = new ProfilerMarker("Circuit.ReleaseCharge.ReleaseCharge.CacheHighestAPCharges");

	private static HashSet<Vector3> _s_NextInputAPs = new HashSet<Vector3>();

	public bool InitialisedOnNetwork { get; private set; }

	public bool IsConnectedToOtherNodes => base.block.CircuitNode.IsConnectedToOtherNodes;

	public bool ShouldProcessInput
	{
		get
		{
			if (base.block.IsAttached)
			{
				return !base.block.tank.beam.IsActive;
			}
			return false;
		}
	}

	private Circuits.BlockChargeDataWrapper CurrentCharge { get; set; } = new Circuits.BlockChargeDataWrapper();

	private Circuits.BlockChargeDataWrapper CurrentSlowCharge { get; set; } = new Circuits.BlockChargeDataWrapper();

	private Circuits.BlockChargeDataWrapper PreviousCharge { get; set; } = new Circuits.BlockChargeDataWrapper();

	public Circuits.BlockChargeData CurrentChargeData => CurrentCharge.Value;

	public Circuits.BlockChargeData PreviousChargeData => PreviousCharge.Value;

	public Circuits.BlockChargeData CurrentSlowChargeData => CurrentSlowCharge.Value;

	public int CurrentHighestChargeFromNetwork => m_CachedChargeInfo.ChargeStrength;

	public void ReceiveCharge()
	{
		if (m_ProcessCharge)
		{
			if (!InitialisedOnNetwork)
			{
				InitialisedOnNetwork = true;
			}
			PreviousCharge.Set(CurrentCharge.Value);
			CurrentCharge.Set(m_CachedChargeInfo);
		}
	}

	public void AddChargeToCache(int charge, int inputCircuitNetworkID, ModuleCircuitNode.Connexion nextChargeOriginConnexion)
	{
		if (m_CachedChargeInfo.ChargeStrength < charge)
		{
			m_CachedChargeInfo.ChargeStrength = charge;
		}
		foreach (ModuleCircuitNode.Connexion item in base.block.CircuitNode.ConnexionMetadata.NetworkInputConnexionsLookup[inputCircuitNetworkID])
		{
			if (item != nextChargeOriginConnexion && charge > 0)
			{
				_s_NextInputAPs.Add(item.AttachPoint);
			}
		}
		m_CachedChargeInfo.ChargedAPs.AddRange(_s_NextInputAPs);
		if (m_GatherExtensiveChargeData)
		{
			foreach (Vector3 s_NextInputAP in _s_NextInputAPs)
			{
				if (!m_CachedChargeInfo.AllChargeAPsAndCharges.TryGetValue(s_NextInputAP, out var value) || charge > value)
				{
					m_CachedChargeInfo.AllChargeAPsAndCharges[s_NextInputAP] = charge;
				}
			}
		}
		_s_NextInputAPs.Clear();
		m_ProcessCharge = true;
	}

	public void SetRequireExtensiveChargeData()
	{
		m_GatherExtensiveChargeData = true;
	}

	public void SubscribeToChargeData(Action<Circuits.BlockChargeData> ChargeSet_Delegate, Action<Circuits.BlockChargeData> ChargeChanged_Delegate, Action<Circuits.BlockChargeData> SlowChargeSet_Delegate, Action<Circuits.BlockChargeData> SlowChargeChanged_Delegate, bool requireExtensiveChargeData)
	{
		if (requireExtensiveChargeData)
		{
			SetRequireExtensiveChargeData();
		}
		if (ChargeSet_Delegate != null)
		{
			CurrentCharge.SetEvent.Subscribe(ChargeSet_Delegate);
		}
		if (ChargeChanged_Delegate != null)
		{
			CurrentCharge.ChangedEvent.Subscribe(ChargeChanged_Delegate);
		}
		if (SlowChargeSet_Delegate != null)
		{
			CurrentSlowCharge.SetEvent.Subscribe(SlowChargeSet_Delegate);
		}
		if (SlowChargeChanged_Delegate != null)
		{
			CurrentSlowCharge.ChangedEvent.Subscribe(SlowChargeChanged_Delegate);
		}
	}

	public void UnSubscribeFromChargeData(Action<Circuits.BlockChargeData> ChargeSet_Delegate, Action<Circuits.BlockChargeData> ChargeChanged_Delegate, Action<Circuits.BlockChargeData> SlowChargeSet_Delegate, Action<Circuits.BlockChargeData> SlowChargeChanged_Delegate)
	{
		if (ChargeSet_Delegate != null)
		{
			CurrentCharge.SetEvent.Unsubscribe(ChargeSet_Delegate);
		}
		if (ChargeChanged_Delegate != null)
		{
			CurrentCharge.ChangedEvent.Unsubscribe(ChargeChanged_Delegate);
		}
		if (SlowChargeSet_Delegate != null)
		{
			CurrentSlowCharge.SetEvent.Unsubscribe(SlowChargeSet_Delegate);
		}
		if (SlowChargeChanged_Delegate != null)
		{
			CurrentSlowCharge.ChangedEvent.Unsubscribe(SlowChargeChanged_Delegate);
		}
	}

	private void ResetCharge()
	{
		CurrentSlowCharge.Reset();
		CurrentCharge.Reset();
		PreviousCharge.Reset();
		InitialisedOnNetwork = false;
	}

	private void UpdateIndicators()
	{
		CircuitAPSignalIndicator.TryRefreshIndicators(CurrentCharge.Value.ChargedAPs, m_InputIndicators);
	}

	private void OnStartChargeUpdate()
	{
		m_CachedChargeInfo.SetDefault();
		m_ProcessCharge = false;
	}

	private void OnEndChargeUpdate()
	{
		if (CurrentCharge.IsNetPropertyDirty || CurrentSlowCharge.IsNetPropertyDirty || PreviousCharge.IsNetPropertyDirty)
		{
			CurrentSlowCharge.SyncNetProperty();
			CurrentCharge.SyncNetProperty();
			PreviousCharge.SyncNetProperty();
		}
	}

	private void OnSlowChargeSet(Circuits.BlockChargeData chargeData)
	{
		UpdateIndicators();
	}

	private void OnPostSlowUpdate()
	{
		CurrentSlowCharge.Set(CurrentCharge.Value);
	}

	private void OnNeighbourBlockDetached(TankBlock _)
	{
		RefreshConnectedToNetwork();
	}

	private void OnConnectedToCircuitNetwork(bool state)
	{
		RefreshConnectedToNetwork();
	}

	private void RefreshConnectedToNetwork()
	{
		if (InitialisedOnNetwork && (!IsConnectedToOtherNodes || base.block.CircuitNode.ConnexionMetadata.ConnectedNodeCount == 0))
		{
			PreviousCharge.Set(Circuits.BlockChargeData.empty);
			CurrentCharge.Set(Circuits.BlockChargeData.empty);
			CurrentSlowCharge.Set(CurrentCharge.Value);
			InitialisedOnNetwork = false;
		}
	}

	private void OnAttachedDetaching()
	{
		ResetCharge();
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec blockSpec)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				CurrentCharge = CurrentCharge.Value,
				CurrentFrameCharge = CurrentSlowCharge.Value,
				PreviousCharge = PreviousCharge.Value,
				InitialisedOnNetwork = InitialisedOnNetwork
			};
			serialData.Store(blockSpec.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData != null)
		{
			InitialisedOnNetwork = serialData.InitialisedOnNetwork;
			CurrentCharge.Set(serialData.CurrentCharge);
			CurrentSlowCharge.Set(serialData.CurrentFrameCharge);
			if (serialData.PreviousCharge != null)
			{
				PreviousCharge.Set(serialData.PreviousCharge);
			}
			InstantRefreshEvent.Send();
		}
	}

	private void PrePool()
	{
		if (base.block.GetComponent<ModuleCircuitNode>() == null)
		{
			d.LogError("NO Module Circuit Node Component on " + base.gameObject.name + " while one of its circuit receiver modules requires it");
		}
	}

	private void OnPool()
	{
		CurrentCharge.Init(this, TTMsgType.SyncCircuitReceiver_Charge);
		PreviousCharge.Init(this, TTMsgType.SyncCircuitReceiver_PreviousCharge);
		CurrentSlowCharge.Init(this, TTMsgType.SyncCircuitReceiver_SlowCharge);
		CurrentSlowCharge.SetEvent.Subscribe(OnSlowChargeSet);
		base.block.AttachedEvent.Subscribe(OnAttachedDetaching);
		base.block.DetachingEvent.Subscribe(OnAttachedDetaching);
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Subscribe(OnConnectedToCircuitNetwork);
		base.block.serializeEvent.Subscribe(OnSerialze);
		m_InputIndicators = (from r in GetComponentsInChildren<CircuitAPSignalIndicator>(includeInactive: true)
			where (r.ConnexionDisplayFlags & ModuleCircuitNode.ConnexionTypes.Input) != 0
			select r).ToArray();
		CircuitAPSignalIndicator[] inputIndicators = m_InputIndicators;
		for (int num = 0; num < inputIndicators.Length; num++)
		{
			inputIndicators[num].InitOnBlock(base.block);
		}
	}

	private void OnDepool()
	{
		base.block.CircuitNode.ConnectedToOtherNodesEvent.Unsubscribe(OnConnectedToCircuitNetwork);
		base.block.serializeEvent.Unsubscribe(OnSerialze);
		base.block.AttachedEvent.Unsubscribe(OnAttachedDetaching);
		base.block.DetachingEvent.Unsubscribe(OnAttachedDetaching);
	}

	private void OnSpawn()
	{
		Circuits.PostSlowUpdate.Subscribe(OnPostSlowUpdate);
		Circuits.StartChargeUpdate.Subscribe(OnStartChargeUpdate);
		Circuits.EndChargeUpdate.Subscribe(OnEndChargeUpdate);
		base.block.NeighbourDetachedEvent.Subscribe(OnNeighbourBlockDetached);
		base.block.CircuitNode.ConnexionsUpdatedEvent.Subscribe(RefreshConnectedToNetwork);
		ResetCharge();
		InstantRefreshEvent.Send();
	}

	private void OnRecycle()
	{
		Circuits.PostSlowUpdate.Unsubscribe(OnPostSlowUpdate);
		Circuits.StartChargeUpdate.Unsubscribe(OnStartChargeUpdate);
		Circuits.EndChargeUpdate.Unsubscribe(OnEndChargeUpdate);
		ResetCharge();
		base.block.NeighbourDetachedEvent.Unsubscribe(OnNeighbourBlockDetached);
		base.block.CircuitNode.ConnexionsUpdatedEvent.Unsubscribe(RefreshConnectedToNetwork);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Receiver;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		CurrentCharge.Serialise(writer);
		CurrentSlowCharge.Serialise(writer);
		PreviousCharge.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		CurrentCharge.Deserialise(reader);
		CurrentSlowCharge.Deserialise(reader);
		PreviousCharge.Deserialise(reader);
	}
}
