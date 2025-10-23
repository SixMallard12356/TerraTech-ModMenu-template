public class uScript_Gauntlet_EndTutorial : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Mode<ModeGauntlet>.inst.EndTutorial();
	}
}
