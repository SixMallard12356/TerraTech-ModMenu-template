public class uScript_HoldFadeUntilSceneryPopulated : uScriptLogic
{
	private bool m_FadeFinished;

	public bool Out => m_FadeFinished;

	public void In()
	{
		m_FadeFinished = false;
		if (Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating)
		{
			Singleton.Manager<ManUI>.inst.FadeToBlack(3f, forceFront: true);
			return;
		}
		Singleton.Manager<ManUI>.inst.ClearFade(3f);
		m_FadeFinished = true;
	}
}
