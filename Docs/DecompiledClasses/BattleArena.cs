using UnityEngine;

public class BattleArena : MonoBehaviour
{
	[SerializeField]
	private BoxCollider m_TriggerBox;

	[SerializeField]
	private float m_TriggerDelay = 3f;

	[SerializeField]
	private BoxCollider m_ArenaBox;

	[SerializeField]
	private SphereCollider m_ArenaSphere;

	[SerializeField]
	private GameObject m_EnemySpawnLocation;

	[SerializeField]
	private TechSpawnFilter m_EnemySpawnFilter;

	private TrackedVisible m_EnemyTech;

	private UICheckpointChallengeHUD m_TimerHUD;

	private ChallengeTimer m_Timer = new ChallengeTimer();

	private static Collider[] s_Colliders = new Collider[1024];

	private void OnSpawn()
	{
	}

	private void OnRecycle()
	{
		m_EnemyTech = null;
	}

	private bool IsPlayerTankInBox(BoxCollider box)
	{
		if (Singleton.playerTank == null)
		{
			return false;
		}
		Vector3 boundsCentreWorld = Singleton.playerTank.boundsCentreWorld;
		boundsCentreWorld = box.transform.InverseTransformPoint(boundsCentreWorld);
		if (Mathf.Abs(boundsCentreWorld.x) < box.size.x * 0.5f && Mathf.Abs(boundsCentreWorld.y) < box.size.y * 0.5f)
		{
			return Mathf.Abs(boundsCentreWorld.z) < box.size.z * 0.5f;
		}
		return false;
	}

	private bool IsPlayerTankInSphere(SphereCollider sphere)
	{
		if (Singleton.playerTank == null)
		{
			return false;
		}
		Vector3 boundsCentreWorld = Singleton.playerTank.boundsCentreWorld;
		return sphere.transform.InverseTransformPoint(boundsCentreWorld).magnitude < sphere.radius;
	}

	private void SpawnTech()
	{
		AITreeType AITreeToUse = null;
		TechData techData = Singleton.Manager<ManPop>.inst.ChoosePopTechToSpawn(m_EnemySpawnFilter, out AITreeToUse);
		if (techData == null)
		{
			TankPreset randomFallbackTech = m_EnemySpawnFilter.GetRandomFallbackTech();
			if (randomFallbackTech != null)
			{
				techData = randomFallbackTech.GetTechDataFormatted();
			}
		}
		if (techData != null)
		{
			ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
			{
				techData = techData,
				teamID = -1,
				position = m_EnemySpawnLocation.transform.position,
				rotation = m_EnemySpawnLocation.transform.rotation,
				placement = ManSpawn.TankSpawnParams.Placement.BaseCentredAtPosition,
				hideMarker = false,
				isPopulation = false,
				grounded = true,
				forceSpawn = true
			};
			m_EnemyTech = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
		}
	}

	private void StartTimer()
	{
		m_TimerHUD.SetChallengeTimer(m_Timer);
		m_TimerHUD.SetBestTimeText("");
		m_Timer.Reset();
		m_Timer.Start(m_TriggerDelay);
	}

	private void StopTimer()
	{
		if (m_TimerHUD.ActiveChallengeTimer == m_Timer)
		{
			m_TimerHUD.SetChallengeTimer(null);
			if (m_Timer.IsRunningSet)
			{
				m_Timer.Stop();
				m_Timer.Reset();
			}
		}
	}

	private void Update()
	{
		if (!Singleton.playerTank)
		{
			return;
		}
		if (m_TimerHUD == null)
		{
			m_TimerHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.CheckpointChallenge) as UICheckpointChallengeHUD;
		}
		if (m_EnemyTech != null)
		{
			if (m_EnemyTech.wasDestroyed || ManSpawn.IsPlayerTeam(m_EnemyTech.TeamID))
			{
				m_EnemyTech = null;
			}
			else if ((m_ArenaBox != null && !IsPlayerTankInBox(m_ArenaBox)) || (m_ArenaSphere != null && !IsPlayerTankInSphere(m_ArenaSphere)))
			{
				Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(m_EnemyTech);
				m_EnemyTech = null;
			}
		}
		else
		{
			if (!(m_TriggerBox != null))
			{
				return;
			}
			if (IsPlayerTankInBox(m_TriggerBox))
			{
				if (m_Timer.IsRunningSet)
				{
					m_Timer.Update();
				}
				else if (m_Timer.TimeElapsed == 0f)
				{
					StartTimer();
				}
				if (m_Timer.TimeElapsed >= m_TriggerDelay)
				{
					StopTimer();
					if (m_EnemySpawnLocation != null && m_EnemySpawnFilter != null)
					{
						SpawnTech();
					}
				}
			}
			else
			{
				StopTimer();
			}
		}
	}
}
