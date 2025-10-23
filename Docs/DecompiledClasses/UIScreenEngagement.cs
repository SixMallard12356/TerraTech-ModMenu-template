public class UIScreenEngagement : UIScreen
{
	private void Update()
	{
		if (Singleton.Manager<ManInput>.inst.PollPlayerNewJoystick())
		{
			Mode<ModeAttract>.inst.ShowNextEnterScreenOrMainMenu();
		}
	}
}
