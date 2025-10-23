[NodePath("TerraTech/Techs")]
[FriendlyName("uScript_IsTechAlive", "Check whether the target tech exists or not")]
public class uScript_IsTechAlive : uScriptLogic
{
	private bool m_Alive;

	public bool Alive => m_Alive;

	public bool Destroyed => !m_Alive;

	public void In(Tank tech)
	{
		m_Alive = tech != null;
	}

	public void OnDisable()
	{
		m_Alive = false;
	}
}
