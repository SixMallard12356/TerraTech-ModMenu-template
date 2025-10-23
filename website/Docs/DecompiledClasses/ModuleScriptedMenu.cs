public class ModuleScriptedMenu : Module, ManPointer.OpenMenuEventConsumer
{
	public bool CanOpenMenu(bool isRadial)
	{
		return true;
	}

	public bool OnOpenMenuEvent(OpenMenuEventData openMenu)
	{
		return true;
	}
}
