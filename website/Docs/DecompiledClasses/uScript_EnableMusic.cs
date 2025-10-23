public class uScript_EnableMusic : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManMusic>.inst.EnableSequencing = true;
	}
}
