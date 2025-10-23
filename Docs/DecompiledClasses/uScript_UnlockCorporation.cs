public class uScript_UnlockCorporation : uScriptLogic
{
	public bool Out => true;

	public void In(FactionSubTypes corporation)
	{
		Singleton.Manager<ManLicenses>.inst.UnlockCorp(corporation, showLevelUp: true);
	}
}
