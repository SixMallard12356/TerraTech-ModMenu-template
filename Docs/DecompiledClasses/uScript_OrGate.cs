[FriendlyName("Bool operators/Group or gate")]
public class uScript_OrGate : uScriptLogic
{
	private bool m_True;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(bool boolOne, bool boolTwo)
	{
		m_True = boolOne || boolTwo;
	}
}
