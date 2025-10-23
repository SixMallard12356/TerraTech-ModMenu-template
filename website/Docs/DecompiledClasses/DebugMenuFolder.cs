public class DebugMenuFolder : DebugMenuObject
{
	public static readonly string k_Prefix = "Folder: ";

	public string TargetFolder { get; private set; }

	public DebugMenuFolder(string name, string targetFolder)
	{
		base.Name = name;
		TargetFolder = targetFolder;
	}

	public override void TriggerMenuOption()
	{
		Singleton.Manager<ManDebugMenu>.inst.ShowMenu(TargetFolder);
	}

	public override ManDebugMenu.DebugMenuType MenuType()
	{
		return ManDebugMenu.DebugMenuType.Folder;
	}
}
