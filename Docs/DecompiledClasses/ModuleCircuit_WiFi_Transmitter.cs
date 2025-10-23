using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(ModuleCircuitReceiver))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_WiFi_Transmitter : Module, ICircuit_WiFi
{
	[SerializeField]
	protected float m_TramissionRadius = 480f;

	[SerializeField]
	[FormerlySerializedAs("m_ValueSlider")]
	protected ModuleHUDSliderControl m_TransmissionFrequencyValueSlider;

	[SerializeField]
	private DigitalDisplay[] m_FrequencyDisplay;

	private float m_LastFrequency = -1f;

	public int TransmissionStrength => base.block.CircuitNode.Receiver.CurrentChargeData.ChargeStrength;

	public float TransmissionRadius
	{
		get
		{
			return m_TramissionRadius;
		}
		set
		{
			m_TramissionRadius = value;
		}
	}

	private void StopTransmitting()
	{
		SetTransmitting(-1f);
	}

	private void SetTransmitting(float frequency)
	{
		if (m_LastFrequency != frequency)
		{
			if (m_LastFrequency >= 0f)
			{
				Singleton.Manager<ManCircuits>.inst.DeregisterTransmission(m_LastFrequency, this);
			}
			if (frequency >= 0f)
			{
				Singleton.Manager<ManCircuits>.inst.RegisterTransmission(frequency, this);
			}
			m_LastFrequency = frequency;
		}
	}

	public void OnChargeChanged(Circuits.BlockChargeData chargeInfo)
	{
		if (chargeInfo > 0)
		{
			SetTransmitting(m_TransmissionFrequencyValueSlider.Value);
		}
		else
		{
			StopTransmitting();
		}
	}

	private void OnTransmissionFrequencySet()
	{
		if (base.block.CircuitNode.Receiver.CurrentChargeData > 0)
		{
			SetTransmitting(m_TransmissionFrequencyValueSlider.Value);
		}
		else
		{
			StopTransmitting();
		}
		if (m_FrequencyDisplay != null)
		{
			int frequencyInt = (int)m_TransmissionFrequencyValueSlider.Value;
			Array.ForEach(m_FrequencyDisplay, delegate(DigitalDisplay digitalDisplay)
			{
				digitalDisplay.SetValue(frequencyInt);
			});
		}
	}

	private void OnPool()
	{
		base.block.CircuitNode.Receiver.SubscribeToChargeData(null, OnChargeChanged, null, null, requireExtensiveChargeData: false);
		m_TransmissionFrequencyValueSlider.OptionSetEvent.Subscribe(OnTransmissionFrequencySet);
	}

	private void OnDepool()
	{
		base.block.CircuitNode.Receiver.UnSubscribeFromChargeData(null, OnChargeChanged, null, null);
		m_TransmissionFrequencyValueSlider.OptionSetEvent.Unsubscribe(OnTransmissionFrequencySet);
	}
}
