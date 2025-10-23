public class uScript_SkipEncountersForTutorialSkip : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManProgression>.inst.SkipEncountersForTutorialSkip();
	}
}
