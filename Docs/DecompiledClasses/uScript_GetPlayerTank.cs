[FriendlyName("Get player tank (local player's tank)")]
public class uScript_GetPlayerTank : uScriptLogic
{
	private bool m_True;

	public bool Returned => m_True;

	public bool NotReturned => !m_True;

	public Tank In()
	{
		Tank playerTank = Singleton.playerTank;
		if (!playerTank)
		{
			m_True = false;
			return null;
		}
		m_True = true;
		return playerTank;
	}
}
