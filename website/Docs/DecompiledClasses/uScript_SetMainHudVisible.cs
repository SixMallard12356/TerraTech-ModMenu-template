public class uScript_SetMainHudVisible : uScriptLogic
{
	public bool Out => true;

	public void In(bool visible)
	{
		Mode<ModeMain>.inst.SetMainHudVisible(visible);
	}
}
