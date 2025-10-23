[FriendlyName("Techs/Is tech grounded")]
public class uScript_IsTechGrounded : uScriptLogic
{
	private bool m_Grounded;

	public bool Out => true;

	public bool True => m_Grounded;

	public bool False => !m_Grounded;

	public void In(Tank tech)
	{
		m_Grounded = false;
		if ((bool)tech)
		{
			m_Grounded = tech.grounded;
		}
	}
}
