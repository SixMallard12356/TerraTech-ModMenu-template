[FriendlyName("uScript_IsMultiplayerMode", "Check if we're playing a multiplayer mode")]
public class uScript_IsMultiplayerMode : uScriptLogic
{
	private bool m_Multiplayer;

	public bool SinglePlayer => !m_Multiplayer;

	public bool Multiplayer => m_Multiplayer;

	public void In()
	{
		m_Multiplayer = Singleton.Manager<ManNetwork>.inst.IsMultiplayer();
	}

	public void OnDisable()
	{
		m_Multiplayer = false;
	}
}
