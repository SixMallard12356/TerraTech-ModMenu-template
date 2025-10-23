public class uScript_ShowPlayerProfile : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Mode<ModeMain>.inst.ShowPlayerProfile(show: true);
	}
}
