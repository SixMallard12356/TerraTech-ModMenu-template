using Steamworks;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/DebugAchievement")]
public class DebugAchievement : AchievementObject
{
	[SerializeField]
	private bool m_TestIntStat;

	[SerializeField]
	private IntStat m_IntStat;

	[SerializeField]
	private int m_IntStatIncrementAmount;

	[SerializeField]
	private bool m_TestFloatStat;

	[SerializeField]
	private FloatStat m_FloatStat;

	[SerializeField]
	private int m_FloatStatIncrementAmount;

	[RewiredAction]
	[SerializeField]
	private int m_IncrementStatAction;

	[RewiredAction]
	[SerializeField]
	private int m_StoreAchievementProgressAction;

	[SerializeField]
	[RewiredAction]
	private int m_TriggerAchievementAction;

	[RewiredAction]
	[SerializeField]
	private int m_ResetAchievementAction;

	public override void LoadStats(AchievementPlatformImpl achievementPlatform)
	{
		base.LoadStats(achievementPlatform);
		achievementPlatform.LoadStat(m_IntStat);
		achievementPlatform.LoadStat(m_FloatStat);
	}

	public override void Initialise()
	{
		base.Initialise();
		base.IsCompleted = false;
	}

	public override void Update()
	{
		base.Update();
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(m_IncrementStatAction))
		{
			if (m_TestIntStat)
			{
				m_IntStat.IntValue += m_IntStatIncrementAmount;
				UpdateStat(m_IntStat);
			}
			if (m_TestFloatStat)
			{
				m_FloatStat.FloatValue += m_FloatStatIncrementAmount;
				UpdateStat(m_FloatStat);
			}
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(m_StoreAchievementProgressAction))
		{
			Singleton.Manager<ManAchievements>.inst.StoreAchievementProgress();
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(m_TriggerAchievementAction))
		{
			CompleteAchievement();
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(m_ResetAchievementAction))
		{
			ResetAchievement();
		}
	}

	protected new void CompleteAchievement()
	{
		OnAchievementCompleted.Send(this);
		base.IsCompleted = false;
	}

	private void ResetAchievement()
	{
		if (SKU.IsSteam)
		{
			SteamUserStats.ResetAllStats(bAchievementsToo: true);
			Singleton.Manager<ManAchievements>.inst.LoadAchievementState();
		}
	}
}
