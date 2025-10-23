using UnityEngine;

public interface IHUDPowerToggleControlledModule
{
	bool PowerControlSetting { get; set; }

	Gradient ToggleGradientOverride { get; }

	bool AutoCloseMenuOnComplete { get; }

	bool CanOpenMenuOnBlock { get; }
}
