public class uScript_IsOnScreenMessageStringValid : uScriptLogic
{
	private bool m_Valid;

	public bool True => m_Valid;

	public bool False => !m_Valid;

	public void In(LocalisedString[] locString)
	{
		m_Valid = locString != null && locString.Length != 0;
	}
}
