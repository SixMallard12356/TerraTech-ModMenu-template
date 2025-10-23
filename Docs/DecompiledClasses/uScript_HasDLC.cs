public class uScript_HasDLC : uScriptLogic
{
	private bool m_HasDLC;

	public bool True => m_HasDLC;

	public bool False => !m_HasDLC;

	public void In(ManDLC.DLCType dlcType)
	{
		m_HasDLC = Singleton.Manager<ManDLC>.inst.HasAnyDLCOfType(dlcType);
	}
}
