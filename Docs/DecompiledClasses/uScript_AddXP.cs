public class uScript_AddXP : uScriptLogic
{
	public bool Out => true;

	public void In(FactionSubTypes corporation, int amount, bool showLevelUpUI)
	{
		Singleton.Manager<ManLicenses>.inst.AddXP(corporation, amount, showLevelUpUI);
	}
}
