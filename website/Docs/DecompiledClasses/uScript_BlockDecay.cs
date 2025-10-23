[FriendlyName("Allow block decay")]
public class uScript_BlockDecay : uScriptLogic
{
	public bool Out => true;

	public void In(bool allow)
	{
		Mode<ModeMain>.inst.AllowBlockDecay = allow;
	}
}
