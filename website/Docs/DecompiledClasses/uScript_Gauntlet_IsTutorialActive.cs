public class uScript_Gauntlet_IsTutorialActive : uScriptLogic
{
	private bool m_Active;

	public bool True => m_Active;

	public bool False => !m_Active;

	public void In()
	{
		m_Active = Mode<ModeGauntlet>.inst.IsTutorialActive();
	}

	public void OnDestroy()
	{
		m_Active = false;
	}
}
