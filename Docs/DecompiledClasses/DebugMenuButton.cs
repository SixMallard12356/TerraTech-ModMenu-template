using System;

public class DebugMenuButton : DebugMenuObject
{
	private Action m_ButtonAction;

	public DebugMenuButton(string name, Action buttonAction)
	{
		base.Name = name;
		m_ButtonAction = buttonAction;
	}

	public override void TriggerMenuOption()
	{
		m_ButtonAction();
	}

	public override ManDebugMenu.DebugMenuType MenuType()
	{
		return ManDebugMenu.DebugMenuType.Button;
	}
}
