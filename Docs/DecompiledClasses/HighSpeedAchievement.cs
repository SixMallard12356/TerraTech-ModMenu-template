using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/HighSpeedAchievement")]
public class HighSpeedAchievement : AchievementObject
{
	[Tooltip("in mph")]
	[SerializeField]
	private float m_SpeedThresholdMph;

	[Tooltip("Time allowed to dip below speed threshold; in seconds")]
	[SerializeField]
	private float m_SpeedThresholdTimeout;

	[SerializeField]
	[Tooltip("in seconds")]
	private float m_TimeToHoldSpeed;

	[Tooltip("Does the tech need to be grounded?")]
	[SerializeField]
	private bool m_Grounded;

	[Tooltip("Time allowed to briefly lift off the ground; in seconds")]
	[SerializeField]
	private float m_GroundContactTimeout;

	private bool m_IsSatisfyingHighspeedCondition;

	private float m_HighspeedStartTime = -1f;

	private float m_LastHighSpeedTime = -1f;

	private float m_LastGroundTouchTime;

	public override void Update()
	{
		if (IsActive() && Singleton.playerTank != null)
		{
			float playerSpeed = GetPlayerSpeed();
			float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
			if (playerSpeed > GameUnits.GetSpeed_MphToCurrent(m_SpeedThresholdMph))
			{
				m_IsSatisfyingHighspeedCondition = true;
				m_LastHighSpeedTime = currentModeRunningTime;
			}
			else if (currentModeRunningTime > m_LastHighSpeedTime + m_SpeedThresholdTimeout)
			{
				m_IsSatisfyingHighspeedCondition = false;
			}
			if (m_IsSatisfyingHighspeedCondition && m_Grounded)
			{
				if (Singleton.playerTank.grounded)
				{
					m_LastGroundTouchTime = currentModeRunningTime;
				}
				else if (currentModeRunningTime > m_LastGroundTouchTime + m_GroundContactTimeout)
				{
					m_IsSatisfyingHighspeedCondition = false;
				}
			}
			if (m_IsSatisfyingHighspeedCondition)
			{
				if (m_HighspeedStartTime < 0f)
				{
					m_HighspeedStartTime = currentModeRunningTime;
				}
				if (currentModeRunningTime > m_HighspeedStartTime + m_TimeToHoldSpeed)
				{
					CompleteAchievement();
				}
			}
			else
			{
				m_HighspeedStartTime = -1f;
			}
		}
		base.Update();
	}

	private float GetPlayerSpeed()
	{
		return GameUnits.GetSpeed((Singleton.playerTank != null) ? Singleton.playerTank.GetForwardSpeed() : 0f);
	}
}
