public class uScript_PausePopulation : uScriptLogic
{
	public bool Out => true;

	public void Pause()
	{
		Singleton.Manager<ManPop>.inst.SetPaused(paused: true);
	}

	public void UnPause()
	{
		Singleton.Manager<ManPop>.inst.SetPaused(paused: false);
	}
}
