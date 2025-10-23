public class uScript_HasHintBeenShownBefore : uScriptLogic
{
	private bool m_Shown;

	public bool Shown => m_Shown;

	public bool NotShown => !m_Shown;

	public void In(GameHints.HintID hintID)
	{
		m_Shown = Singleton.Manager<ManProfile>.inst.GetCurrentUser().HasSeenHint(hintID);
	}

	public void OnEnable()
	{
		m_Shown = false;
	}
}
