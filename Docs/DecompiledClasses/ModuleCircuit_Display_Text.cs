using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
[RequireComponent(typeof(ModuleCircuitNode))]
public class ModuleCircuit_Display_Text : ModuleCircuit_Display
{
	[SerializeField]
	protected DigitalDisplay[] m_Displays;

	protected string m_DisplayText;

	protected int m_MaxDisplayValue;

	protected float DisplayValue
	{
		set
		{
			int value2 = Mathf.RoundToInt(Mathf.Min(value, m_MaxDisplayValue));
			for (int i = 0; i < m_Displays.Length; i++)
			{
				m_Displays[i].SetValue(value2);
			}
		}
	}

	protected override void ResetDisplay()
	{
		DisplayValue = 0f;
	}

	protected override void OnFrameChargeChanged(Circuits.BlockChargeData newCharge)
	{
		OnFrameChargeChanged(newCharge.ChargeStrength);
	}

	protected override void OnFrameChargeChanged(int newChargeStrength)
	{
		DisplayValue = m_ChargeDisplayRange.Clamp(newChargeStrength);
	}

	private void OnPool()
	{
		m_MaxDisplayValue = 0;
		for (int i = 0; Mathf.Pow(10f, i) <= m_ChargeDisplayRange.Max; i++)
		{
			m_MaxDisplayValue += 9 * Mathf.RoundToInt(Mathf.Pow(10f, i));
		}
	}
}
