[FriendlyName("Tech/Is tech anchored")]
public class uScript_IsTechAnchored : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(Tank tech)
	{
		if ((bool)tech)
		{
			m_True = tech.IsAnchored;
		}
	}

	public void OnDisable()
	{
		m_True = false;
	}
}
