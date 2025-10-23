public class uScript_ShowHUDElement : uScriptLogic
{
	public bool Out => true;

	public void In(ManHUD.HUDElementType hudElement)
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(hudElement);
	}
}
