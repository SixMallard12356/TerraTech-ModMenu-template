using System;

public abstract class AchievementPlatformImpl
{
	public abstract bool Init(Action<bool> initCompleteHandler);

	public abstract void Update();

	public abstract void LoadAchievementState(AchievementObject achievement);

	public abstract void CompleteAchievement(AchievementObject achievement);

	public abstract void LoadStat(AchievementObject.Stat stat);

	public abstract void UpdateStat(AchievementObject.Stat stat);

	public abstract void StoreProgress();

	public abstract void ResetAllAchievements();
}
