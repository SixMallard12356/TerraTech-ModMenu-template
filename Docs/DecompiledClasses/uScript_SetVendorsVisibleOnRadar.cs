public class uScript_SetVendorsVisibleOnRadar : uScriptLogic
{
	public bool Out => true;

	public void SetVisible()
	{
		Singleton.Manager<ManWorld>.inst.Vendors.SetVisibleOnRadar(visible: true);
	}

	public void SetInvisible()
	{
		Singleton.Manager<ManWorld>.inst.Vendors.SetVisibleOnRadar(visible: false);
	}
}
