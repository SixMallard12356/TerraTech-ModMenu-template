using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(ModuleCircuitReceiver), typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Signal_Extender : Module, ICircuitDispensor, INetworkedModule
{
	private class ExtendedSignal
	{
		public int signalValue;

		public int signalTickOrigin;
	}

	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public List<ExtendedSignal> extendedSignalBuffer;

		public int lastOutputValue;
	}

	[SerializeField]
	protected ModuleHUDSliderControl m_ExtensionValueSlider;

	[SerializeField]
	[Header("Display")]
	private BufferDisplay m_BufferDisplay;

	private int m_NumTicksToExtendBy;

	private List<ExtendedSignal> m_ExtendedSignals = new List<ExtendedSignal>();

	private int m_LastSignalOutput;

	private NetworkedProperty<ByteArrayBlockMessage> net_BufferDisplayData;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		int num = 0;
		if (m_ExtendedSignals.Count > 0)
		{
			ExtendedSignal extendedSignal = m_ExtendedSignals[0];
			num = extendedSignal.signalValue;
			if (Circuits.Time.tickCount > extendedSignal.signalTickOrigin + m_NumTicksToExtendBy)
			{
				m_ExtendedSignals.RemoveAt(0);
			}
		}
		m_LastSignalOutput = num;
		return num;
	}

	private void InitialiseDisplay()
	{
		m_BufferDisplay?.SetBufferSize(m_NumTicksToExtendBy);
		UpdateDisplay(netSend: false);
	}

	private void UpdateDisplayAndSync()
	{
		UpdateDisplay(netSend: true);
	}

	private void UpdateDisplay(bool netSend)
	{
		if ((ManNetwork.IsNetworked && !ManNetwork.IsHost) || !base.block.IsAttached || !(m_BufferDisplay != null))
		{
			return;
		}
		int num = Circuits.Time.tickCount;
		int num2 = 0;
		ExtendedSignal extendedSignal = ((m_ExtendedSignals.Count > 0) ? m_ExtendedSignals[num2] : null);
		for (int num3 = m_NumTicksToExtendBy - 1; num3 >= 0; num3--)
		{
			BufferDisplay.ColorTypes colorType = BufferDisplay.ColorTypes.None;
			if (extendedSignal != null)
			{
				if (extendedSignal.signalTickOrigin + m_NumTicksToExtendBy < num)
				{
					num2++;
					bool num4 = num2 < m_ExtendedSignals.Count;
					extendedSignal = (num4 ? m_ExtendedSignals[num2] : null);
					if (num4)
					{
						colorType = BufferDisplay.ColorTypes.Negative;
					}
				}
				else
				{
					colorType = BufferDisplay.ColorTypes.Any;
				}
			}
			m_BufferDisplay.SetColorAtIndex(num3, colorType);
			num++;
		}
		m_BufferDisplay.ApplyTextureAndUpdatePropertyBlock();
		net_BufferDisplayData.Data.value = m_BufferDisplay.GetColorTypeData();
		if (netSend)
		{
			net_BufferDisplayData.Sync();
		}
	}

	private void ResetQueuedCharges()
	{
		m_ExtendedSignals.Clear();
		m_LastSignalOutput = 0;
	}

	private void OnSliderValueSet()
	{
		m_NumTicksToExtendBy = Mathf.RoundToInt(m_ExtensionValueSlider.Value * 50f);
		if (base.block.IsAttached)
		{
			InitialiseDisplay();
		}
	}

	private void OnChargeSet(Circuits.BlockChargeData chargeInfo)
	{
		int receivedCharge = chargeInfo.ChargeStrength;
		if (receivedCharge <= 0)
		{
			return;
		}
		ExtendedSignal extendedSignal = null;
		if (receivedCharge > m_LastSignalOutput)
		{
			m_ExtendedSignals.Clear();
		}
		else
		{
			int num = m_ExtendedSignals.FindIndex((ExtendedSignal ex) => ex.signalValue <= receivedCharge);
			if (num >= 0)
			{
				m_ExtendedSignals.RemoveRange(num, m_ExtendedSignals.Count - num);
			}
		}
		extendedSignal = new ExtendedSignal
		{
			signalValue = receivedCharge
		};
		extendedSignal.signalTickOrigin = Circuits.Time.tickCount;
		m_ExtendedSignals.Add(extendedSignal);
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec blockSpec)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				extendedSignalBuffer = new List<ExtendedSignal>(m_ExtendedSignals),
				lastOutputValue = m_LastSignalOutput
			};
			serialData.Store(blockSpec.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData != null)
		{
			m_ExtendedSignals.Clear();
			m_ExtendedSignals.AddRange(serialData.extendedSignalBuffer);
			m_LastSignalOutput = serialData.lastOutputValue;
		}
	}

	private void OnAttached()
	{
		InitialiseDisplay();
		Circuits.PostSlowUpdate.Subscribe(UpdateDisplayAndSync);
	}

	private void OnDetaching()
	{
		Circuits.PostSlowUpdate.Unsubscribe(UpdateDisplayAndSync);
		ResetQueuedCharges();
		m_BufferDisplay?.Clear();
	}

	private void OnMPSync(ByteArrayBlockMessage msg)
	{
		if (!ManNetwork.IsHost)
		{
			m_BufferDisplay.SetAndApplyFromColorTypeData(msg.value);
		}
	}

	private void OnPool()
	{
		net_BufferDisplayData = new NetworkedProperty<ByteArrayBlockMessage>(this, TTMsgType.SyncCircuitExtender_Buffer, OnMPSync);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialze);
		m_ExtensionValueSlider.OptionSetEvent.Subscribe(OnSliderValueSet);
		m_ExtensionValueSlider.InstantRefreshEvent.Subscribe(ResetQueuedCharges);
		m_BufferDisplay?.Init();
	}

	private void OnSpawn()
	{
		base.block.CircuitNode.Receiver.SubscribeToChargeData(OnChargeSet, null, null, null, requireExtensiveChargeData: false);
	}

	private void OnRecycle()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(OnChargeSet, null, null, null);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Extender;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		net_BufferDisplayData.Serialise(writer);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		net_BufferDisplayData.Deserialise(reader);
	}
}
