#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class ManAchievements : Singleton.Manager<ManAchievements>
{
	public enum AchievementTypes
	{
		DefeatBigTony,
		DefeatEnemyFromDistance,
		ReachHighSpeed,
		BuildDifferentBlockTypes,
		DefeatLargerTech,
		DiscoverBiome,
		DefeatTraderTrollInTime,
		UnlockAllCorps,
		MaxAllCorps,
		HoldMoneyAmount,
		ReattachShotOffBlocks,
		FinishGauntletFlying,
		CraftBlocksOnce,
		FlipTechWithScoop,
		GlideForDistance
	}

	[SerializeField]
	private bool m_EnableAchievements = true;

	[SerializeField]
	[EnumArray(typeof(AchievementTypes))]
	private AchievementObject[] m_Achievements;

	private AchievementPlatformImpl m_AchievementPlatformImpl;

	private bool m_IsInitialised;

	private HashSet<ManGameMode.GameType> m_ModesWithAchievements = new HashSet<ManGameMode.GameType>();

	public bool ActiveInCurrentMode { get; set; } = true;

	public void Initialise()
	{
		if (m_EnableAchievements && !m_IsInitialised)
		{
			if (SKU.IsSteam)
			{
				m_AchievementPlatformImpl = new AchievementImplSteam();
			}
			else if (SKU.IsEpicGS)
			{
				m_AchievementPlatformImpl = new AchievementImplEpicGameStore();
			}
			if (m_AchievementPlatformImpl != null)
			{
				d.Assert(m_AchievementPlatformImpl.Init(OnInitialiseComplete), "ManAchievements.Initialise - Failed to start platform initialisation.");
			}
		}
	}

	public void LoadAchievementState()
	{
		for (int i = 0; i < m_Achievements.Length; i++)
		{
			if (m_Achievements[i] != null)
			{
				m_AchievementPlatformImpl.LoadAchievementState(m_Achievements[i]);
			}
		}
	}

	public void StoreAchievementProgress()
	{
		if (m_AchievementPlatformImpl != null)
		{
			m_AchievementPlatformImpl.StoreProgress();
		}
	}

	public void CompleteAchievement(AchievementTypes achievementID)
	{
		AchievementObject achievementObject = m_Achievements[(int)achievementID];
		d.AssertFormat(achievementObject != null, "ManAchievements.CompleteAchievement - Achievement with ID '{0}' is not set up!");
		if (achievementObject != null)
		{
			achievementObject.CompleteAchievement();
		}
	}

	public void ResetAllAchievements()
	{
		if (m_AchievementPlatformImpl != null)
		{
			m_AchievementPlatformImpl.ResetAllAchievements();
			Singleton.Manager<ManAchievements>.inst.LoadAchievementState();
		}
	}

	public bool HasModeGotAchievements(ManGameMode.GameType gameType)
	{
		return m_ModesWithAchievements.Contains(gameType);
	}

	private void OnInitialiseComplete(bool initSuccess)
	{
		m_IsInitialised = initSuccess;
		if (!initSuccess)
		{
			return;
		}
		LoadAchievementState();
		for (int i = 0; i < m_Achievements.Length; i++)
		{
			if (m_Achievements[i] != null)
			{
				m_ModesWithAchievements.Add(m_Achievements[i].AchievableInMode);
				if (!m_Achievements[i].IsCompleted || m_Achievements[i] is DebugAchievement)
				{
					m_Achievements[i].RegisterCallbacks(OnStatUpdated, OnAchievementCompleted);
					m_Achievements[i].Initialise();
				}
			}
		}
		Singleton.Manager<ManGameMode>.inst.ModeSwitchEvent.Subscribe(OnModeSwitch);
	}

	private void OnStatUpdated(AchievementObject.Stat stat)
	{
		if (m_AchievementPlatformImpl != null)
		{
			m_AchievementPlatformImpl.UpdateStat(stat);
		}
	}

	private void OnAchievementCompleted(AchievementObject achievement)
	{
		d.LogFormat("Achievement with SteamID '{0}' type '{1}' was completed!", achievement.SteamID, achievement.GetType());
		if (m_AchievementPlatformImpl != null)
		{
			m_AchievementPlatformImpl.CompleteAchievement(achievement);
		}
		achievement.UnregisterCallbacks(OnStatUpdated, OnAchievementCompleted);
	}

	private void OnModeSwitch()
	{
		ActiveInCurrentMode = true;
		StoreAchievementProgress();
	}

	private void Awake()
	{
		if (SKU.IsSteam)
		{
			if (Singleton.Manager<ManSteamworks>.inst.Inited)
			{
				Initialise();
			}
			else
			{
				Singleton.Manager<ManSteamworks>.inst.SteamWorksInitialisedEvent.Subscribe(Initialise);
			}
		}
		else if (SKU.IsEpicGS)
		{
			if (Singleton.Manager<ManEOS>.inst.IsConnected)
			{
				Initialise();
			}
			else
			{
				Singleton.Manager<ManEOS>.inst.InitStateChangedEvent.Subscribe(InitAfterEpicConnectLogin);
			}
		}
		void InitAfterEpicConnectLogin(ManEOS.InitState prevState, ManEOS.InitState newState)
		{
			if (prevState.IsNot(ManEOS.InitState.ConnectLoggedIn) && newState.Is(ManEOS.InitState.ConnectLoggedIn))
			{
				Initialise();
				Singleton.Manager<ManEOS>.inst.InitStateChangedEvent.Unsubscribe(InitAfterEpicConnectLogin);
			}
		}
	}

	private void Update()
	{
		if (!m_IsInitialised)
		{
			return;
		}
		for (int i = 0; i < m_Achievements.Length; i++)
		{
			if (m_Achievements[i] != null && !m_Achievements[i].IsCompleted)
			{
				m_Achievements[i].Update();
			}
		}
		if (m_AchievementPlatformImpl != null)
		{
			m_AchievementPlatformImpl.Update();
		}
	}
}
