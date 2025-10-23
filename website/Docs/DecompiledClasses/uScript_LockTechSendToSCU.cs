[NodeToolTip("Locks Send to SCU button for a specific tech")]
public class uScript_LockTechSendToSCU : uScriptLogic
{
	public bool Out => true;

	public void In(Tank tech, bool lockSendToSCU)
	{
		if (tech != null)
		{
			tech.visible.SetLockTimout(Visible.LockTimerTypes.SendToSCU, lockSendToSCU ? 1f : 0f);
			if (tech.netTech != null && ManNetwork.IsNetworked && ManNetwork.IsHost)
			{
				tech.netTech.OnServerSetLockTechSendToSCU();
			}
		}
	}
}
