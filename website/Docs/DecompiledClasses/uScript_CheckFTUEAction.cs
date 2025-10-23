public class uScript_CheckFTUEAction : uScriptLogic
{
	private bool m_ActionSet;

	public bool True => m_ActionSet;

	public bool False => !m_ActionSet;

	public void In(FTUEEnumType enumType)
	{
		m_ActionSet = Singleton.Manager<ManNewFTUE>.inst.CheckEvent(enumType);
	}
}
