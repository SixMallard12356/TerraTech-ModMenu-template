public class uScript_CheckPlayerTechControlScheme : uScriptLogic
{
	private bool m_Matches;

	public bool Out => true;

	public bool True => m_Matches;

	public bool False => !m_Matches;

	public void In(ControlSchemeCategory category)
	{
		m_Matches = false;
		if (Singleton.playerTank != null)
		{
			ControlScheme activeScheme = Singleton.playerTank.control.ActiveScheme;
			if (activeScheme != null)
			{
				m_Matches = activeScheme.Category == category;
			}
		}
	}
}
