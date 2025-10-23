[NodeToolTip("Locks out ability to move the player tech")]
public class uScript_LockPlayerInput : uScriptLogic
{
	public bool Out => true;

	public void In(bool lockInput, bool includeCamera = true)
	{
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = lockInput;
		if (includeCamera || !lockInput)
		{
			TankCamera.inst.FreezeCamera(lockInput);
		}
	}
}
