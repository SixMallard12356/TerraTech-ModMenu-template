public class uScript_SetQuestLogVisible : uScriptLogic
{
	public bool Out => true;

	public void In([DefaultValue(true)] bool visible)
	{
		if (visible)
		{
			Singleton.Manager<ManQuestLog>.inst.SetQuestLogAvailable();
		}
	}
}
