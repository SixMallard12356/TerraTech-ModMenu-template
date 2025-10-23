public class uScript_DisableTechRadialMenu : uScriptLogic
{
	public bool Out => true;

	public void In(bool disableRadialMenu)
	{
		Singleton.Manager<ManPointer>.inst.DisableRadialMenu(disableRadialMenu);
	}
}
