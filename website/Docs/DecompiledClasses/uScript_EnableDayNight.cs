public class uScript_EnableDayNight : uScriptLogic
{
	public bool Out => true;

	public void In(bool enable, bool clientOnly = false)
	{
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable, clientOnly);
		if (!enable)
		{
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
		}
	}
}
