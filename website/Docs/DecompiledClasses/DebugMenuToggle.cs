using System;

public class DebugMenuToggle : DebugMenuObject
{
	private Func<bool> m_ToggleFunction;

	private Func<bool> m_IsToggledFunction;

	public DebugMenuToggle(string name, Func<bool> isToggledFunction, Func<bool> toggleFunction)
	{
		base.Name = name;
		m_IsToggledFunction = isToggledFunction;
		m_ToggleFunction = toggleFunction;
	}

	public bool IsToggled()
	{
		return m_IsToggledFunction();
	}

	public override void TriggerMenuOption()
	{
		m_ToggleFunction();
	}

	public override ManDebugMenu.DebugMenuType MenuType()
	{
		return ManDebugMenu.DebugMenuType.Toggle;
	}
}
