using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/FlyingGauntletAchievement")]
public class FlyingGauntletAchievement : AchievementObject
{
	[SerializeField]
	private float m_GracePeriodAfterFirstCheckpoint;

	private bool m_GauntletInProgress;

	private float m_GauntletStartTime;

	private bool m_PlayerTouchedGround;

	public override void Initialise()
	{
		base.Initialise();
		Mode<ModeGauntlet>.inst.GauntletStartedEvent.Subscribe(OnGauntletStarted);
		Mode<ModeGauntlet>.inst.GauntletFinishedEvent.Subscribe(OnGauntletFinished);
		m_GauntletInProgress = false;
	}

	public override void Update()
	{
		base.Update();
		if (IsActive() && m_GauntletInProgress && Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() > m_GauntletStartTime + m_GracePeriodAfterFirstCheckpoint && Singleton.playerTank != null && Singleton.playerTank.grounded)
		{
			m_PlayerTouchedGround = true;
		}
	}

	private void OnGauntletStarted()
	{
		m_GauntletInProgress = true;
		m_GauntletStartTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		m_PlayerTouchedGround = false;
	}

	private void OnGauntletFinished(bool finishedSuccess)
	{
		m_GauntletInProgress = false;
		if (IsActive() && finishedSuccess && !m_PlayerTouchedGround)
		{
			CompleteAchievement();
			Mode<ModeGauntlet>.inst.GauntletStartedEvent.Unsubscribe(OnGauntletStarted);
			Mode<ModeGauntlet>.inst.GauntletFinishedEvent.Unsubscribe(OnGauntletFinished);
		}
	}
}
