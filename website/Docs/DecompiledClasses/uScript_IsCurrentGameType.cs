public class uScript_IsCurrentGameType : uScriptLogic
{
	private bool m_IsCurrentGameType;

	public bool True => m_IsCurrentGameType;

	public bool False => !m_IsCurrentGameType;

	public void In(ManGameMode.GameType gameType)
	{
		m_IsCurrentGameType = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == gameType;
	}
}
