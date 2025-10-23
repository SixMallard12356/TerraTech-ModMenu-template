[FriendlyName("Get Mode Running Time")]
public class uScript_GetModeRunningTime : uScriptLogic
{
	public bool Out => true;

	public float In()
	{
		return Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
	}
}
