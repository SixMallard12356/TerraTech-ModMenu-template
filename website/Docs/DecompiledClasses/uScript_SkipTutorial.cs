[FriendlyName("Is Skipping Tutorial")]
public class uScript_SkipTutorial : uScriptLogic
{
	private bool m_Skip;

	public bool Yes => m_Skip;

	public bool No => !m_Skip;

	public void In()
	{
		m_Skip = Mode<ModeMain>.inst.SkipTutorial;
	}
}
