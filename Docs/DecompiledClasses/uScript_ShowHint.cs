public class uScript_ShowHint : uScriptLogic
{
	public bool Out => true;

	public void In(GameHints.HintID hintId)
	{
		Singleton.Manager<ManHints>.inst.ShowHint(hintId);
	}
}
