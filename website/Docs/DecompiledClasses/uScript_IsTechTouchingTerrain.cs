[FriendlyName("Techs/Is tech touching terrain")]
public class uScript_IsTechTouchingTerrain : uScriptLogic
{
	private bool m_Touching;

	public bool Out => true;

	public bool True => m_Touching;

	public bool False => !m_Touching;

	public void In(Tank tech)
	{
		m_Touching = false;
		if ((bool)tech)
		{
			m_Touching = tech.touchingTerrain;
		}
	}
}
