[FriendlyName("Set tank block limit hidden")]
public class uScript_SetTankHideBlockLimit : uScriptLogic
{
	public bool Out => true;

	public void In(bool hidden, Tank tech)
	{
		if (tech != null)
		{
			Singleton.Manager<ManBlockLimiter>.inst.SetTankHidden(tech, hidden);
		}
	}
}
