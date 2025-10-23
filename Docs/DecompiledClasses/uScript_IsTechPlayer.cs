public class uScript_IsTechPlayer : uScriptLogic
{
	private bool m_IsPlayer;

	public bool Out => true;

	public bool True => m_IsPlayer;

	public bool False => !m_IsPlayer;

	public void In(Tank tech)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			m_IsPlayer = tech != null && tech.netTech != null && tech.netTech.NetPlayer != null;
		}
		else
		{
			m_IsPlayer = tech != null && Singleton.playerTank == tech;
		}
	}
}
