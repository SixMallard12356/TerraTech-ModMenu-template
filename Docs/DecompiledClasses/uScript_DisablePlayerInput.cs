public class uScript_DisablePlayerInput : uScriptLogic
{
	public bool Out => true;

	public void In(bool disableInput)
	{
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = disableInput;
	}
}
