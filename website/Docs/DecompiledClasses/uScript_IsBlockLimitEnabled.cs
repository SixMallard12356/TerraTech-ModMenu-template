[FriendlyName("uScript_IsBlockLimitEnabled", "Checks whether or not the Block Limiter is in effect")]
[NodePath("TerraTech/UI")]
public class uScript_IsBlockLimitEnabled : uScriptLogic
{
	private bool m_Active;

	public bool True => m_Active;

	public bool False => !m_Active;

	public void In()
	{
		m_Active = Singleton.Manager<ManBlockLimiter>.inst.LimiterActive;
	}
}
