[FriendlyName("Toggle build beam")]
public class uScript_ToggleBuildBeam : uScriptLogic
{
	private bool m_True;

	public bool Out => true;

	public void In(bool active)
	{
		if ((bool)Singleton.playerTank && !m_True)
		{
			Singleton.playerTank.beam.EnableBeam(active);
			m_True = true;
		}
	}

	public void OnDisable()
	{
		m_True = false;
	}
}
