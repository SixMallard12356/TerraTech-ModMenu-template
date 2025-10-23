public class uScript_LockPause : uScriptLogic
{
	public bool Out => true;

	public void In(bool lockPause, ManPauseGame.DisablePauseReason disabledReason)
	{
		Singleton.Manager<ManPauseGame>.inst.LockPause(lockPause, disabledReason);
	}
}
