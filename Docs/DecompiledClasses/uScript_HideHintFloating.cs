public class uScript_HideHintFloating : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.HintFloating);
	}
}
