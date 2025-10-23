public class uScript_DisableQuickMenu : uScriptLogic
{
	public bool Out => true;

	public void In(bool disableQuickMenu)
	{
		Singleton.Manager<ManHUD>.inst.QuickMenuDisabled = disableQuickMenu;
	}
}
