public class uScript_IsDebugSkip : uScriptLogic
{
	public bool Out => true;

	public bool True => Mode<ModeMain>.inst.DebugSkipTutorial;

	public bool False => !Mode<ModeMain>.inst.DebugSkipTutorial;

	public void In()
	{
	}
}
