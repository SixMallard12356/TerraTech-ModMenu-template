public class uScript_HideHint : uScriptLogic
{
	public bool Out => true;

	public void In(GameHints.HintID hintId)
	{
		Singleton.Manager<ManHints>.inst.HideHint(hintId);
	}
}
