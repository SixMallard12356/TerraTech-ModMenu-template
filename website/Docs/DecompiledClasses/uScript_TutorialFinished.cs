public class uScript_TutorialFinished : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			currentUser.m_TutorialSkipSettings.m_CompletedTutorial = true;
			Singleton.Manager<ManProfile>.inst.Save();
		}
	}
}
