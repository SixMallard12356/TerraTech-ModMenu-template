using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleCircuitReceiver), typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_Signal_Delay : Module, ICircuitDispensor, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public Circuits.BlockChargeData[] m_ExtensiveChargeBuffer;

		public int m_ChargeBufferPointer;

		[Obsolete("We now store a buffer of whole charges as opposed to ints")]
		public int[] m_ChargeBuffer;
	}

	[FormerlySerializedAs("m_ValueSlider")]
	[SerializeField]
	private ModuleHUDSliderControl m_SignalDelayValueSlider;

	[SerializeField]
	[Header("Analogue Display")]
	private Transform m_ClockDialDisplay;

	[SerializeField]
	[Header("Digital Display")]
	private BufferDisplay m_BufferDisplay;

	private Circuits.BlockChargeData[] m_ChargeBuffer;

	private int m_ChargeBufferPointer;

	private Vector3 m_ClockDialStartingRot;

	private NetworkedProperty<ByteArrayBlockMessage> net_BufferDisplayData;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		int num = 0;
		Circuits.BlockChargeData blockChargeData = m_ChargeBuffer[m_ChargeBufferPointer];
		if (blockChargeData != null)
		{
			foreach (KeyValuePair<Vector3, int> allChargeAPsAndCharge in blockChargeData.AllChargeAPsAndCharges)
			{
				if (!(allChargeAPsAndCharge.Key == outputAP))
				{
					int value = allChargeAPsAndCharge.Value;
					num = Mathf.Max(num, value);
				}
			}
		}
		return num;
	}

	private void UpdateSizeAndClearDelayBuffer()
	{
		int num = Mathf.FloorToInt(m_SignalDelayValueSlider.Value / 0.02f);
		if (m_ChargeBuffer == null || num != m_ChargeBuffer.Length)
		{
			m_ChargeBuffer = new Circuits.BlockChargeData[num];
			for (int i = 0; i < num; i++)
			{
				m_ChargeBuffer[i] = new Circuits.BlockChargeData();
			}
			m_ChargeBufferPointer = 0;
		}
		if (base.block.IsAttached)
		{
			m_BufferDisplay?.SetBufferSize(m_ChargeBuffer.Length);
			UpdateClockVisual(netSend: false);
		}
	}

	private void UpdateClockVisual(bool netSend)
	{
		if (base.block.IsAttached && m_BufferDisplay != null && (!ManNetwork.IsNetworked || ManNetwork.IsHost))
		{
			int num = m_ChargeBuffer.Length - 1;
			int num2 = m_ChargeBufferPointer;
			while (num2 < m_ChargeBuffer.Length)
			{
				m_BufferDisplay.SetColorAtIndex(num, GetColorForDisplayAtBufferIndex(num2));
				num2++;
				num--;
			}
			int num3 = 0;
			while (num3 < m_ChargeBufferPointer)
			{
				m_BufferDisplay.SetColorAtIndex(num, GetColorForDisplayAtBufferIndex(num3));
				num3++;
				num--;
			}
			m_BufferDisplay.ApplyTextureAndUpdatePropertyBlock();
			net_BufferDisplayData.Data.value = m_BufferDisplay.GetColorTypeData();
			if (netSend)
			{
				net_BufferDisplayData.Sync();
			}
		}
		if (m_ClockDialDisplay != null)
		{
			m_ClockDialDisplay.localRotation = Quaternion.Euler(new Vector3(m_ClockDialStartingRot.x, m_ClockDialStartingRot.y, 180f * GetPeriodUntilNextStateChange()));
		}
	}

	private BufferDisplay.ColorTypes GetColorForDisplayAtBufferIndex(int index)
	{
		if (m_ChargeBuffer[index].ChargeStrength == 0)
		{
			return BufferDisplay.ColorTypes.None;
		}
		if (m_ChargeBuffer.Length == 1 || index == m_ChargeBufferPointer)
		{
			return BufferDisplay.ColorTypes.Any;
		}
		int num = ((index == 0) ? (m_ChargeBuffer.Length - 1) : (index - 1));
		if (m_ChargeBuffer[num].ChargeStrength == 0 || m_ChargeBuffer[num].ChargeStrength == m_ChargeBuffer[index].ChargeStrength)
		{
			return BufferDisplay.ColorTypes.Any;
		}
		if (m_ChargeBuffer[num].ChargeStrength <= m_ChargeBuffer[index].ChargeStrength)
		{
			return BufferDisplay.ColorTypes.Positive;
		}
		return BufferDisplay.ColorTypes.Negative;
	}

	private float GetPeriodUntilNextStateChange()
	{
		int num = m_ChargeBufferPointer;
		for (int i = 1; i < m_ChargeBuffer.Length; i++)
		{
			num = ((num + 1 < m_ChargeBuffer.Length) ? (num + 1) : 0);
			if (m_ChargeBuffer[m_ChargeBufferPointer] != m_ChargeBuffer[num])
			{
				return (float)i / (float)(m_ChargeBuffer.Length - 1);
			}
		}
		return 0f;
	}

	private void OnDelayChanged()
	{
		UpdateSizeAndClearDelayBuffer();
	}

	private void OnChargeSet(Circuits.BlockChargeData chargeInfo)
	{
		if (m_ChargeBuffer != null && m_ChargeBuffer.Length != 0)
		{
			m_ChargeBuffer[m_ChargeBufferPointer].CopyFrom(chargeInfo);
			m_ChargeBufferPointer = ((m_ChargeBufferPointer + 1 < m_ChargeBuffer.Length) ? (m_ChargeBufferPointer + 1) : 0);
		}
	}

	private void OnCircuitVisualUpdate()
	{
		UpdateClockVisual(netSend: true);
	}

	private void OnAttached()
	{
		UpdateSizeAndClearDelayBuffer();
	}

	private void OnDetaching()
	{
		if (m_ChargeBuffer != null)
		{
			for (int i = 0; i < m_ChargeBuffer.Length; i++)
			{
				m_ChargeBuffer[i].SetDefault();
			}
		}
		m_ChargeBufferPointer = 0;
		m_BufferDisplay?.Clear();
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec blockSpec)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				m_ExtensiveChargeBuffer = m_ChargeBuffer,
				m_ChargeBufferPointer = m_ChargeBufferPointer
			};
			serialData.Store(blockSpec.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(blockSpec.saveState);
		if (serialData != null)
		{
			m_ChargeBuffer = serialData.m_ExtensiveChargeBuffer;
			m_ChargeBufferPointer = serialData.m_ChargeBufferPointer;
		}
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
		net_BufferDisplayData = new NetworkedProperty<ByteArrayBlockMessage>(this, TTMsgType.SyncCircuitDelay_Buffer, OnMPSync);
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeEvent.Subscribe(OnSerialze);
		m_SignalDelayValueSlider.OptionSetEvent.Subscribe(OnDelayChanged);
		if (m_ClockDialDisplay != null)
		{
			m_ClockDialStartingRot = m_ClockDialDisplay.localRotation.eulerAngles;
		}
		m_BufferDisplay?.Init();
	}

	private void Depool()
	{
		base.block.AttachedEvent.Unsubscribe(OnAttached);
		base.block.DetachingEvent.Unsubscribe(OnDetaching);
		base.block.serializeEvent.Unsubscribe(OnSerialze);
		m_SignalDelayValueSlider.OptionSetEvent.Unsubscribe(OnDelayChanged);
	}

	private void OnSpawn()
	{
		base.block.CircuitNode.Receiver.SubscribeToChargeData(OnChargeSet, null, null, null, requireExtensiveChargeData: true);
		Circuits.PostSlowUpdate.Subscribe(OnCircuitVisualUpdate);
	}

	private void OnRecycle()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(OnChargeSet, null, null, null);
		Circuits.PostSlowUpdate.Unsubscribe(OnCircuitVisualUpdate);
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleCircuit_Delay;
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
