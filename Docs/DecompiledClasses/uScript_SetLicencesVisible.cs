public class uScript_SetLicencesVisible : uScriptLogic
{
	public bool Out => true;

	public void In(bool visible)
	{
		Singleton.Manager<ManLicenses>.inst.SetHUDVisible(visible);
	}
}
