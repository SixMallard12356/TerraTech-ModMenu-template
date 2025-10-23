#define UNITY_EDITOR
using System;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitReceiver))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_Time_Stopwatch : Module, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public int m_Version;

		[Obsolete("Not handing the timer stuff in this class anymore")]
		public float m_SecondsElapsed;

		public Timer m_Timer;
	}

	[SerializeField]
	protected Vector3 m_CountAP;

	[SerializeField]
	protected Vector3 m_ResetAP;

	[SerializeField]
	protected Spinner[] m_Spinners;

	[SerializeField]
	protected ModuleHUDContextControl_ColorPickerField m_TagPickerField;

	private Timer m_Timer;

	private NetworkedProperty<TimerParamBlockMessage> net_Timer;

	protected bool m_StopwatchRegistered;

	protected bool m_NextStopwatchRegisteredState;

	public Timer Timer => m_Timer;

	public ModuleHUDContextControl_ColorPickerField TagPickerField => m_TagPickerField;

	public Color TagColor => m_TagPickerField.CurrentOption.m_Params.m_Return_Color;

	private void OnMPTimerSync(TimerParamBlockMessage msg)
	{
		msg.value.ApplyToTimer(m_Timer);
		net_Timer.Data.value = msg.value;
	}

	private void ResetTimer()
	{
		m_Timer.Reset();
		net_Timer.Data.value.CopyFromTimer(m_Timer);
	}

	private void EnableTimer(bool state)
	{
		if (m_Timer.IsRunningSet != state)
		{
			m_Timer.SetRunning(state);
			net_Timer.Data.value.CopyFromTimer(m_Timer);
			net_Timer.Sync();
		}
	}

	private void TryRegisterStopwatch()
	{
		if (m_StopwatchRegistered != m_NextStopwatchRegisteredState)
		{
			if (m_NextStopwatchRegisteredState)
			{
				Singleton.Manager<ManCircuits>.inst.RegisterStopwatch(this);
			}
			else
			{
				Singleton.Manager<ManCircuits>.inst.DeregisterStopwatch(this);
			}
			m_StopwatchRegistered = m_NextStopwatchRegisteredState;
		}
	}

	protected void OnPlayerJoined_Sync(NetPlayer player)
	{
		d.Assert(ManNetwork.IsNetworked);
		net_Timer.Data.value.CopyFromTimer(m_Timer);
		net_Timer.Sync();
	}

	protected void OnTimerTickingStateChanged(bool state)
	{
		for (int i = 0; i < m_Spinners.Length; i++)
		{
			m_Spinners[i].SetAutoSpin(state);
		}
	}

	protected void OnChargeChanged(Circuits.BlockChargeData chargeInfo)
	{
		if (!ManNetwork.IsNetworked || ManNetwork.IsHost)
		{
			if (chargeInfo.AllChargeAPsAndCharges.ContainsKey(m_ResetAP))
			{
				ResetTimer();
				net_Timer.Sync();
			}
			else
			{
				EnableTimer(chargeInfo.AllChargeAPsAndCharges.Keys.Contains(m_CountAP));
			}
		}
	}

	private void OnDetached()
	{
		if (!ManSaveGame.Storing)
		{
			ResetTimer();
		}
	}

	private void OnTimerTickingActivated(bool state)
	{
		m_NextStopwatchRegisteredState = state;
		if (!state)
		{
			Spinner[] spinners = m_Spinners;
			for (int i = 0; i < spinners.Length; i++)
			{
				spinners[i].Reset();
			}
		}
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec context)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				m_Timer = m_Timer,
				m_Version = 1
			};
			serialData.Store(context.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(context.saveState);
		if (serialData != null && serialData.m_Version == 1)
		{
			m_Timer.ApplyConfigFrom(serialData.m_Timer);
			net_Timer.Data.value.CopyFromTimer(m_Timer);
		}
	}

	private void OnPool()
	{
		base.block.serializeEvent.Subscribe(OnSerialze);
		net_Timer = new NetworkedProperty<TimerParamBlockMessage>(this, TTMsgType.SyncCircuitStopwatchTimerState, OnMPTimerSync);
		m_Timer = new Timer();
	}

	private void OnDepool()
	{
		base.block.serializeEvent.Unsubscribe(OnSerialze);
	}

	private void OnSpawn()
	{
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerJoined_Sync);
		m_Timer.Start();
		m_Timer.SetRunning(state: false);
		m_Timer.TimerActivatedEvent.Subscribe(OnTimerTickingActivated);
		m_Timer.TickingStateChangedEvent.Subscribe(OnTimerTickingStateChanged);
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: true);
		base.block.DetachedEvent.Subscribe(OnDetached);
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerJoined_Sync);
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
		base.block.DetachedEvent.Unsubscribe(OnDetached);
		ResetTimer();
		OnTimerTickingActivated(m_Timer.Activated);
		TryRegisterStopwatch();
		m_Timer.TimerActivatedEvent.Unsubscribe(OnTimerTickingActivated);
		m_Timer.TickingStateChangedEvent.Unsubscribe(OnTimerTickingStateChanged);
	}

	private void Update()
	{
		m_Timer.Update();
		TryRegisterStopwatch();
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Stopwatch;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		net_Timer.Serialise(writer);
	}

	void INetworkedModule.OnDeserialize(NetworkReader reader)
	{
		net_Timer.Deserialise(reader);
	}
}
