[FriendlyName("Save options")]
public class uScript_Save : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public void In(ManFTUE.SaveStates state)
	{
		if (!m_True)
		{
			Singleton.Manager<ManFTUE>.inst.SaveState = state;
			m_True = true;
		}
	}

	public void OnDisable()
	{
		m_True = false;
	}
}
