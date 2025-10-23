public abstract class DebugMenuObject
{
	public string Name { get; protected set; }

	public abstract void TriggerMenuOption();

	public abstract ManDebugMenu.DebugMenuType MenuType();
}
