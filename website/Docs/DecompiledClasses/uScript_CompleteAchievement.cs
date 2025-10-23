public class uScript_CompleteAchievement : uScriptLogic
{
	public bool Out => true;

	public void In(ManAchievements.AchievementTypes achievementID)
	{
		Singleton.Manager<ManAchievements>.inst.CompleteAchievement(achievementID);
	}
}
