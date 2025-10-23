using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[RequireComponent(typeof(ModuleCircuitDispensor))]
public class ModuleCircuit_WiFi_Receiver : Module, ICircuitDispensor, ICircuit_WiFi
{
	[SerializeField]
	protected float m_DetectionRadius = 480f;

	[SerializeField]
	[FormerlySerializedAs("m_ValueSlider")]
	protected ModuleHUDSliderControl m_ReceivingFrequencyValueSlider;

	[SerializeField]
	private DigitalDisplay[] m_FrequencyDisplays;

	public float TransmissionRadius
	{
		get
		{
			return m_DetectionRadius;
		}
		set
		{
			m_DetectionRadius = value;
		}
	}

	public float TransmissionFrequency => m_ReceivingFrequencyValueSlider.Value;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		return Singleton.Manager<ManCircuits>.inst.GetTransmissionStrengthOnFrequency(m_ReceivingFrequencyValueSlider.Value, base.transform.position, base.block.tank.Team, TransmissionRadius);
	}

	private void OnTransmissionFrequencySet()
	{
		if (m_FrequencyDisplays != null)
		{
			int frequencyInt = (int)m_ReceivingFrequencyValueSlider.Value;
			Array.ForEach(m_FrequencyDisplays, delegate(DigitalDisplay digitalDisplay)
			{
				digitalDisplay.SetValue(frequencyInt);
			});
		}
	}

	private void OnPool()
	{
		m_ReceivingFrequencyValueSlider.OptionSetEvent.Subscribe(OnTransmissionFrequencySet);
	}

	private void OnDepool()
	{
		m_ReceivingFrequencyValueSlider.OptionSetEvent.Unsubscribe(OnTransmissionFrequencySet);
	}
}
