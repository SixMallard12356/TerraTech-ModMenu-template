[NodeToolTip("Locks Send to SCU button in the radial menu to prevent storing mission tech")]
public class uScript_LockSendToSCU : uScriptLogic
{
	public bool Out => true;

	public void In(bool lockSendToSCU)
	{
		float delay = (lockSendToSCU ? 1f : 0f);
		Singleton.Manager<ManUI>.inst.SetUILockTimer(ManUI.LockTimerTypes.SendToSCU, delay);
	}

	public void OnDisable()
	{
		Singleton.Manager<ManUI>.inst.SetUILockTimer(ManUI.LockTimerTypes.SendToSCU, 0f);
	}
}
