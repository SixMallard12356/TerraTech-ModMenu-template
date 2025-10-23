using System;
using System.Linq;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(ModuleCircuitReceiver))]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleCircuit_Display_Color : ModuleCircuit_Display
{
	public enum DisplayType
	{
		ChargeGradient,
		ChargeRGBValue,
		MonochromeOnOff
	}

	[SerializeField]
	[Header("General")]
	protected DisplayType m_DisplayType;

	[SerializeField]
	[Tooltip("Only works with MonochromeOnOff Display type, allows for player controlled color selection")]
	[Header("Mono Settings")]
	protected ModuleHUDContextControl_ColorPickerField m_BlockColorOptionControl;

	[SerializeField]
	protected float m_ActiveEmissionStrength = 1f;

	[Header("Gradient Settings")]
	[SerializeField]
	protected Gradient m_DisplayColorRange;

	[SerializeField]
	protected AnimationCurve m_EmissionStrengthCurve;

	protected Renderer[] m_CurrentDisplayRenderers;

	private void RefreshDisplay()
	{
		UpdateDisplayWithChargeValue(base.block.CircuitNode.Receiver.CurrentSlowChargeData.ChargeStrength);
	}

	private void UpdateDisplayWithChargeValue(int chargeStrength)
	{
		chargeStrength = (int)m_ChargeDisplayRange.Clamp(chargeStrength);
		switch (m_DisplayType)
		{
		case DisplayType.MonochromeOnOff:
			_UpdateDisplay_MonochromeOnOff();
			break;
		case DisplayType.ChargeGradient:
			_UpdateDisplay_ChargeGradient();
			break;
		case DisplayType.ChargeRGBValue:
			_UpdateDisplay_ChargeRGBValue();
			break;
		}
		void _UpdateDisplay_ChargeGradient()
		{
			float time = m_ChargeDisplayRange.InverseLerp(chargeStrength);
			SetColor(m_DisplayColorRange.Evaluate(time), m_EmissionStrengthCurve.Evaluate(time));
		}
		void _UpdateDisplay_ChargeRGBValue()
		{
			if (chargeStrength == 0)
			{
				ResetColor();
			}
			else
			{
				Vector2[] array = new Vector2[Convert.ToInt32(Math.Floor(Math.Log10((int)m_ChargeDisplayRange.Max) + 1.0))];
				Vector2 vector = Vector2Int.zero;
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new Vector2((int)Mathf.Floor(((float)chargeStrength - vector.x) / Mathf.Pow(10f, array.Length - i - 1)), (int)Mathf.Floor((m_ChargeDisplayRange.Max - vector.y) / Mathf.Pow(10f, array.Length - i - 1)));
					vector += array[i] * Mathf.Pow(10f, array.Length - i - 1);
				}
				Color color = new Color(array[0].x / array[0].y, array[1].x / array[1].y, array[2].x / array[2].y);
				float num = new float[3] { color.r, color.g, color.b }.OrderByDescending((float r) => r).First();
				SetColor(color, num * m_ActiveEmissionStrength);
			}
		}
		void _UpdateDisplay_MonochromeOnOff()
		{
			if (chargeStrength == 0)
			{
				ResetColor();
			}
			else
			{
				SetColor(m_BlockColorOptionControl.CurrentOption.m_Params.m_Return_Color, m_ActiveEmissionStrength);
			}
		}
	}

	private void SetColor(Color color, float intensity)
	{
		base.block.RegisterVariableColours(m_CurrentDisplayRenderers, color, color * intensity);
	}

	private void ResetColor()
	{
		base.block.ClearVariableColours(m_CurrentDisplayRenderers);
	}

	private void RefreshDisplayRenderers()
	{
		m_CurrentDisplayRenderers = (from r in GetComponentsInChildren<CircuitDisplayRenderer>(includeInactive: true)
			select r.Renderer).ToArray();
		RefreshDisplay();
	}

	protected override void ResetDisplay()
	{
		UpdateDisplayWithChargeValue(0);
	}

	protected override void OnFrameChargeChanged(Circuits.BlockChargeData newCharge)
	{
		UpdateDisplayWithChargeValue(newCharge.ChargeStrength);
	}

	protected override void OnFrameChargeChanged(int chargeStrength)
	{
		UpdateDisplayWithChargeValue(chargeStrength);
	}

	protected void OnBlockColorOptionSet()
	{
		RefreshDisplay();
	}

	protected void OnMeshRenderersChanged()
	{
		RefreshDisplayRenderers();
	}

	private void OnPool()
	{
		if (m_DisplayType == DisplayType.MonochromeOnOff)
		{
			m_BlockColorOptionControl.OptionSetEvent.Subscribe(OnBlockColorOptionSet);
		}
		base.block.visible.MesheRenderersUpdatedEvent.Subscribe(OnMeshRenderersChanged);
	}

	private void OnDepool()
	{
		if (m_DisplayType == DisplayType.MonochromeOnOff)
		{
			m_BlockColorOptionControl.OptionSetEvent.Unsubscribe(OnBlockColorOptionSet);
		}
		base.block.visible.MesheRenderersUpdatedEvent.Unsubscribe(OnMeshRenderersChanged);
	}

	private void OnValidate()
	{
		if (m_DisplayType == DisplayType.ChargeRGBValue && (m_ChargeDisplayRange.Max != 999f || m_ChargeDisplayRange.Min != 0f))
		{
			m_ChargeDisplayRange = new MinMaxFloat(0f, 999f);
		}
	}
}
