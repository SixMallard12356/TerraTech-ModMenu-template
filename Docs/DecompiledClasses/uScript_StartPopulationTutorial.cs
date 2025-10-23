[FriendlyName("Progression/Start population tutorials")]
[NodeDeprecated]
public class uScript_StartPopulationTutorial : uScriptLogic
{
	private bool m_Started;

	public bool Out => true;

	public void In(bool partOne, bool partTwo)
	{
		if (!m_Started)
		{
			Singleton.Manager<ManFTUE>.inst.StartPopulationTutorial(partOne, partTwo);
			m_Started = true;
		}
	}

	public void OnDisable()
	{
		m_Started = false;
	}
}
