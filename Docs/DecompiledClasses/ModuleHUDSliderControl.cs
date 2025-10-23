#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleHUDSliderControl : ModuleHUDContextControlField, IHUDRadialSliderControlledModule, INetworkedModule
{
	[Serializable]
	private new class SerialData : SerialData<SerialData>
	{
		public float Value;
	}

	[Serializable]
	private struct ValueTitleOverride
	{
		[SerializeField]
		private float m_Value;

		[SerializeField]
		private float m_Variance;

		[SerializeField]
		private LocalisedString m_ReplacementTitle;

		private const float kUnlikelySmallNumber = 0.001f;

		public float Value => m_Value;

		public LocalisedString ReplacementTitle => m_ReplacementTitle;

		public bool HasValueInRange(float val)
		{
			return m_Value.Approximately(val, (m_Variance < 0.001f) ? 0.001f : m_Variance);
		}
	}

	[Flags]
	public enum ValidationFlagTypes
	{
		ImperfectRangeAndStep = 2,
		UnmatchedValueTitleOverrides = 4,
		MinMaxDecimalPlaceMismatch = 8
	}

	[Header("Value")]
	[SerializeField]
	private Vector2 m_AdjustableValueRange = Vector2.up;

	[SerializeField]
	[HideInInspector]
	public float m_AdjustableValueStep = 0.1f;

	[HideInInspector]
	[SerializeField]
	public float m_AdjustableValueDefault;

	[Header("Display")]
	[SerializeField]
	private Vector2Int m_MinMaxDecimalPlacesToDisplay = new Vector2Int(0, 7);

	[SerializeField]
	private UISliderControlRadialMenu.LeftOptionIconType m_DefaultOptionIcon;

	[SerializeField]
	private ValueTitleOverride[] m_ValueTitleOverrides = new ValueTitleOverride[0];

	[SerializeField]
	protected LocalisedString m_HUDSliderValueFormat;

	[SerializeField]
	protected LocalisedString m_HUDResetTooltip;

	[SerializeField]
	protected LocalisedString m_HUDSliderTooltip;

	private NetworkedProperty<FloatParamBlockMessage> m_SliderValueProp;

	private const float k_Epsilon = 1E-06f;

	public float Value { get; private set; }

	public float AdjustableValueFulfillment01
	{
		get
		{
			return GetRangeFulfillmentFromValue(Value);
		}
		set
		{
			SetValueMultiplayerSafe(GetValueFromRangeFulfillment(value));
		}
	}

	public Vector2 AdjustableValueRange => m_AdjustableValueRange;

	public float AdjustableValueStep => m_AdjustableValueStep;

	public override Type BlockContextFieldType => typeof(UIBlockContext_SliderField);

	Vector2 IHUDRadialSliderControlledModule.AdjustableValueRange => m_AdjustableValueRange;

	bool IHUDRadialSliderControlledModule.IsAdjustableValueUpdatedRealtime => m_ApplyChangesRealtime;

	UISliderControlRadialMenu.LeftOptionIconType IHUDRadialSliderControlledModule.LeftOptionIconType => m_DefaultOptionIcon;

	public LocalisedString ValueFormat => m_HUDSliderValueFormat;

	LocalisedString IHUDRadialSliderControlledModule.SliderTitle => m_HUDTitle;

	LocalisedString IHUDRadialSliderControlledModule.SliderTooltip => m_HUDSliderTooltip;

	LocalisedString IHUDRadialSliderControlledModule.SliderWarningTooltip => m_HUDSliderTooltip;

	LocalisedString IHUDRadialSliderControlledModule.ResetTooltip => m_HUDResetTooltip;

	LocalisedString IHUDRadialSliderControlledModule.ResetWarningTooltip => m_HUDResetTooltip;

	ModuleControlCategory IHUDRadialSliderControlledModule.BlockControlCategory => ModuleControlCategory.NotImplemented;

	public void SetAdjustableValueDefault()
	{
		SetValueLocally(m_AdjustableValueDefault);
	}

	public string GetAdjustableValueDisplayText(float value, bool includeUnit, out string overrideText)
	{
		overrideText = string.Empty;
		for (int i = 0; i < m_ValueTitleOverrides.Length; i++)
		{
			if (m_ValueTitleOverrides[i].HasValueInRange(value))
			{
				overrideText = m_ValueTitleOverrides[i].ReplacementTitle.Value;
				break;
			}
		}
		int decimalPlaceCount = value.GetDecimalPlaceCount();
		decimalPlaceCount = Mathf.Clamp(decimalPlaceCount, m_MinMaxDecimalPlacesToDisplay.x, m_MinMaxDecimalPlacesToDisplay.y);
		string text = value.ToString($"F{decimalPlaceCount}");
		if (includeUnit)
		{
			text = string.Format(m_HUDSliderValueFormat.Value, text);
		}
		return text;
	}

	public float GetValueFromRangeFulfillment(float fulfillment)
	{
		return GetStandardisedValue(m_AdjustableValueRange.Lerp(fulfillment));
	}

	public float GetRangeFulfillmentFromValue(float value)
	{
		return m_AdjustableValueRange.InverseLerp(value);
	}

	public ValidationFlagTypes TryValidateConfiguration(out string log)
	{
		ValidationFlagTypes validationFlagTypes = (ValidationFlagTypes)0;
		log = "";
		if (m_AdjustableValueStep > Mathf.Abs(m_AdjustableValueRange.x - m_AdjustableValueRange.y))
		{
			m_AdjustableValueStep = Mathf.Abs(m_AdjustableValueRange.x - m_AdjustableValueRange.y);
		}
		else if (m_AdjustableValueStep < 0f)
		{
			m_AdjustableValueStep = 0f;
		}
		if (Mathf.Abs(m_AdjustableValueRange.x - m_AdjustableValueRange.y) < float.Epsilon)
		{
			m_AdjustableValueRange.y += ((m_AdjustableValueStep == 0f) ? 1f : m_AdjustableValueStep);
		}
		float closestValueInRange;
		if (m_AdjustableValueStep != 0f && !(m_AdjustableValueRange.y - m_AdjustableValueRange.x).ApproximatelyDivisibleBy(m_AdjustableValueStep, 1E-06f))
		{
			log += $"Warning: The range and step are imperfect! Currently the range ({0}-{1}) size '{Mathf.Abs(m_AdjustableValueRange.y - m_AdjustableValueRange.x)}' cannot be perfectly divided by '{m_AdjustableValueStep}'. This will lead to issues where unintended values could be reached by the player! Please fix!\n";
			validationFlagTypes |= ValidationFlagTypes.ImperfectRangeAndStep;
		}
		else if (!m_AdjustableValueRange.ContainsValueInRange(m_AdjustableValueDefault, out closestValueInRange))
		{
			m_AdjustableValueDefault = closestValueInRange;
		}
		else if (!IsValueStandardised(m_AdjustableValueDefault))
		{
			m_AdjustableValueDefault = GetStandardisedValue(m_AdjustableValueDefault);
		}
		if (m_AdjustableValueStep != 0f)
		{
			for (int i = 0; i < m_ValueTitleOverrides.Length; i++)
			{
				if (!IsValueStandardised(m_ValueTitleOverrides[i].Value))
				{
					log += "Warning: One or more ValueTitleOverrides have values that do not match the step within the given range and will never be used! Please adjust!\n";
					validationFlagTypes |= ValidationFlagTypes.UnmatchedValueTitleOverrides;
					break;
				}
			}
		}
		if (m_MinMaxDecimalPlacesToDisplay.x > m_MinMaxDecimalPlacesToDisplay.y)
		{
			log += "Warning: Min Max Decimal Places To Display has a minimum value lower than the maximum value!\n";
			validationFlagTypes |= ValidationFlagTypes.MinMaxDecimalPlaceMismatch;
		}
		return validationFlagTypes;
	}

	public void SetValueMultiplayerSafe(float value)
	{
		SetValueLocally(value);
		m_SliderValueProp.Sync();
	}

	public void SetValueLocally(float value)
	{
		value = GetStandardisedValue(value);
		Value = value;
		m_SliderValueProp.Data.value = Value;
		OptionSetEvent.Send();
	}

	public float GetStandardisedValue(float value)
	{
		if (!m_AdjustableValueRange.ContainsValueInRange(value, out var closestValueInRange))
		{
			return closestValueInRange;
		}
		if (m_AdjustableValueStep == 0f)
		{
			return value;
		}
		float num = value - m_AdjustableValueRange.x;
		num %= m_AdjustableValueStep;
		num = ((num >= m_AdjustableValueStep / 2f) ? (m_AdjustableValueStep - num) : (0f - num));
		return value + num;
	}

	private bool IsValueStandardised(float value)
	{
		if (m_AdjustableValueRange.ContainsValueInRange(value))
		{
			if (m_AdjustableValueStep != 0f)
			{
				return (value - m_AdjustableValueRange.x).ApproximatelyDivisibleBy(m_AdjustableValueStep, 1E-06f);
			}
			return true;
		}
		return false;
	}

	private void OnSerialze(bool saving, TankPreset.BlockSpec context)
	{
		SerialData serialData;
		if (saving)
		{
			serialData = new SerialData
			{
				Value = Value
			};
			serialData.Store(context.saveState);
			return;
		}
		serialData = SerialData<SerialData>.Retrieve(context.saveState);
		if (serialData != null)
		{
			SetValueLocally(serialData.Value);
			InstantRefreshEvent.Send();
		}
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		float floatVal;
		if (saving)
		{
			context.Store(this, "moduleHUDSliderControl_Value", Value);
		}
		else if (context.TryRetrieve(this, "moduleHUDSliderControl_Value", out floatVal, m_AdjustableValueDefault))
		{
			SetValueLocally(floatVal);
			InstantRefreshEvent.Send();
		}
		else
		{
			d.LogFormat(this, "ModuleHUDSliderControl.OnSerializeText - Failed to parse any 'Value' from save data on block '{0}'. Leaving untouched", base.block.name);
		}
	}

	private void OnMPSliderValueChanged(FloatParamBlockMessage msg)
	{
		SetValueLocally(msg.value);
	}

	private void OnPool()
	{
		base.block.serializeEvent.Subscribe(OnSerialze);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
		m_SliderValueProp = new NetworkedProperty<FloatParamBlockMessage>(this, TTMsgType.SetHUDSliderControl_Values, OnMPSliderValueChanged);
	}

	private void OnSpawn()
	{
		SetAdjustableValueDefault();
		InstantRefreshEvent.Send();
	}

	public TankBlock GetBlock()
	{
		return base.block;
	}

	public NetworkedModuleID GetModuleID()
	{
		return NetworkedModuleID.ModuleHUDSliderControl;
	}

	public void OnSerialize(NetworkWriter writer)
	{
		writer.Write((double)Value);
	}

	public void OnDeserialize(NetworkReader reader)
	{
		SetValueLocally((float)reader.ReadDouble());
	}
}
