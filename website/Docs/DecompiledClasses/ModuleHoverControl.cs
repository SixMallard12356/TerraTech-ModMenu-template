using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class ModuleHoverControl : Module, IHUDRadialSliderControlledModule
{
	[SerializeField]
	private LocalisedString m_HUDSliderTitle;

	[SerializeField]
	private LocalisedString m_HUDSliderValueFormat;

	[SerializeField]
	private LocalisedString m_HUDResetTooltip;

	[SerializeField]
	private LocalisedString m_HUDResetCircuitDisableTooltip;

	[SerializeField]
	private LocalisedString m_HUDSliderTooltip;

	private float m_HoverPower;

	public float AdjustableValueFulfillment01
	{
		get
		{
			return base.block.tank.BlockStateController.HoverPower;
		}
		set
		{
			base.block.tank.BlockStateController.HoverPower = value;
			base.block.tank.BlockStateController.SendHoverAndGyroToServer();
		}
	}

	public float HoverPower => m_HoverPower;

	Vector2 IHUDRadialSliderControlledModule.AdjustableValueRange => Vector2.up;

	bool IHUDRadialSliderControlledModule.IsAdjustableValueUpdatedRealtime => true;

	UISliderControlRadialMenu.LeftOptionIconType IHUDRadialSliderControlledModule.LeftOptionIconType => UISliderControlRadialMenu.LeftOptionIconType.Power;

	LocalisedString IHUDRadialSliderControlledModule.SliderTitle => m_HUDSliderTitle;

	LocalisedString IHUDRadialSliderControlledModule.ResetTooltip => m_HUDResetTooltip;

	LocalisedString IHUDRadialSliderControlledModule.ResetWarningTooltip => m_HUDResetCircuitDisableTooltip;

	LocalisedString IHUDRadialSliderControlledModule.SliderTooltip => m_HUDSliderTooltip;

	LocalisedString IHUDRadialSliderControlledModule.SliderWarningTooltip => m_HUDSliderTooltip;

	ModuleControlCategory IHUDRadialSliderControlledModule.BlockControlCategory => ModuleControlCategory.Hover;

	public string GetAdjustableValueDisplayText(float value, bool includeUnit, out string overrideText)
	{
		string text = (value * 100f).ToString("F2");
		overrideText = string.Empty;
		if (includeUnit)
		{
			text = string.Format(m_HUDSliderValueFormat.Value, text);
		}
		return text;
	}

	public void SetAdjustableValueDefault()
	{
		bool flag = base.block.tank.BlockStateController.IsCategoryActive(ModuleControlCategory.Hover);
		base.block.tank.BlockStateController.RequestSetCategoryActive(ModuleControlCategory.Hover, !flag);
	}

	public float GetValueFromRangeFulfillment(float fulfillment)
	{
		return fulfillment;
	}

	private void OnAttached()
	{
		base.block.tank.BlockStateController.AddHoverController(this);
	}

	private void OnDetaching()
	{
		m_HoverPower = base.block.tank.BlockStateController.HoverPower;
		base.block.tank.BlockStateController.RemoveHoverController(this);
	}

	private void OnSerializeText(bool saving, TankPreset.BlockSpec context, bool onTech)
	{
		if (saving)
		{
			if (base.block.IsAttached)
			{
				m_HoverPower = base.block.tank.BlockStateController.HoverPower;
			}
			context.Store(this, "HoverPower", m_HoverPower);
		}
		else
		{
			context.TryRetrieve(this, "HoverPower", out m_HoverPower, 0.5f);
		}
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		base.block.serializeTextEvent.Subscribe(OnSerializeText);
	}

	private void OnSpawn()
	{
		m_HoverPower = 0.5f;
	}
}
