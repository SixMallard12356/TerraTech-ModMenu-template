public class uScript_HideHUDElement : uScriptLogic
{
	public bool Out => true;

	public void In(ManHUD.HUDElementType hudElement)
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(hudElement);
	}
}
