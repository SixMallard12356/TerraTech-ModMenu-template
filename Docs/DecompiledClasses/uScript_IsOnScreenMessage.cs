public class uScript_IsOnScreenMessage : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public bool True => m_True;

	public bool False => !m_True;

	public void In(ManOnScreenMessages.OnScreenMessage onScreenMessage)
	{
		m_True = false;
		if (onScreenMessage != null)
		{
			m_True = Singleton.Manager<ManOnScreenMessages>.inst.IsCurrentMessage(onScreenMessage);
		}
	}
}
