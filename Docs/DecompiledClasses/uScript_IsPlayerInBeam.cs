[FriendlyName("Is player in beam")]
public class uScript_IsPlayerInBeam : uScriptLogic
{
	private bool m_True;

	public bool True => m_True;

	public bool False => !m_True;

	public void In()
	{
		if ((bool)Singleton.playerTank)
		{
			m_True = Singleton.playerTank.beam.IsActive;
		}
		else
		{
			m_True = false;
		}
	}
}
