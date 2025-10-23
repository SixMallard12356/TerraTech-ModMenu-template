using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitDispensor))]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleCircuit_Value_Input : Module, ICircuitDispensor
{
	[SerializeField]
	protected ModuleHUDSliderControl m_OutputValueSlider;

	[SerializeField]
	private DigitalDisplay[] m_ValueDisplays;

	private int m_CurrentOutputValue = int.MaxValue;

	int ICircuitDispensor.GetDispensableCharge(Vector3 outputAP)
	{
		return m_CurrentOutputValue;
	}

	private void OnValueSet()
	{
		int num = Mathf.RoundToInt(m_OutputValueSlider.Value);
		if (num == m_CurrentOutputValue)
		{
			return;
		}
		m_CurrentOutputValue = num;
		if (m_ValueDisplays != null)
		{
			Array.ForEach(m_ValueDisplays, delegate(DigitalDisplay digitalDisplay)
			{
				digitalDisplay.SetValue(m_CurrentOutputValue);
			});
		}
	}

	private void OnPool()
	{
		m_OutputValueSlider.OptionSetEvent.Subscribe(OnValueSet);
	}

	private void OnRecycle()
	{
		m_CurrentOutputValue = int.MaxValue;
	}

	private void OnDepool()
	{
		m_OutputValueSlider.OptionSetEvent.Unsubscribe(OnValueSet);
	}
}
