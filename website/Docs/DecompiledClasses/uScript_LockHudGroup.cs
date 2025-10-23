public class uScript_LockHudGroup : uScriptLogic
{
	public bool Out => true;

	public void In(ManHUD.HUDGroup group, bool locked)
	{
		Singleton.Manager<ManHUD>.inst.SetHUDGroupLocked(group, locked);
	}
}
