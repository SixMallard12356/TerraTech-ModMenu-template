public class uScript_Gauntlet_SetStartTutorialButtonVisibility : uScriptLogic
{
	public bool Out => true;

	public void In(bool enabled)
	{
		Mode<ModeGauntlet>.inst.ShowStartTutorial(enabled);
	}
}
