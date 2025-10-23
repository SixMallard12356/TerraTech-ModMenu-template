[FriendlyName("Lock Interaction Mode")]
public class uScript_LockInteractionMode : uScriptLogic
{
	public bool Out => true;

	public void Lock()
	{
		SetLocked(locked: true);
	}

	public void Unlock()
	{
		SetLocked(locked: false);
	}

	private void SetLocked(bool locked)
	{
		Singleton.Manager<ManPointer>.inst.SetInteractionModeToggleLocked(locked);
	}
}
