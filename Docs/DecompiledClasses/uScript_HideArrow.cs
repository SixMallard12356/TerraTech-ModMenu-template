public class uScript_HideArrow : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.BouncingArrow);
	}
}
