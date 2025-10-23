using UnityEngine;

public interface IHUDRadialSliderControlledModule
{
	float AdjustableValueFulfillment01 { get; set; }

	Vector2 AdjustableValueRange { get; }

	bool IsAdjustableValueUpdatedRealtime { get; }

	UISliderControlRadialMenu.LeftOptionIconType LeftOptionIconType { get; }

	ModuleControlCategory BlockControlCategory { get; }

	LocalisedString SliderTitle { get; }

	LocalisedString SliderTooltip { get; }

	LocalisedString SliderWarningTooltip { get; }

	LocalisedString ResetTooltip { get; }

	LocalisedString ResetWarningTooltip { get; }

	void SetAdjustableValueDefault();

	float GetValueFromRangeFulfillment(float fulfillment);

	string GetAdjustableValueDisplayText(float value, bool includeUnit, out string overrideText);
}
