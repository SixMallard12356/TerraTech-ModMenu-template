#define UNITY_EDITOR
using System;
using Steamworks;

public class AchievementImplSteam : AchievementPlatformImpl
{
	private Action<bool> m_InitialisationCompleteHandler;

	private bool m_StatsDirty;

	private Callback<UserStatsReceived_t> m_UserStatsReceivedCallback;

	private Callback<UserStatsStored_t> m_UserStatsStoredCallback;

	public override bool Init(Action<bool> initCompleteHandler)
	{
		m_InitialisationCompleteHandler = initCompleteHandler;
		if (m_UserStatsReceivedCallback == null)
		{
			m_UserStatsReceivedCallback = Callback<UserStatsReceived_t>.Create(OnUserStatsReceived);
		}
		bool num = SteamUserStats.RequestCurrentStats();
		d.AssertFormat(num, "AchievementImplSteam.Init - Failed to initiate request! Steam is not logged in!");
		return num;
	}

	public override void Update()
	{
		if (m_StatsDirty)
		{
			if (m_UserStatsStoredCallback == null)
			{
				m_UserStatsStoredCallback = Callback<UserStatsStored_t>.Create(OnUserStatsStored);
			}
			d.AssertFormat(SteamUserStats.StoreStats(), "AchievementImplSteam.StoreStats - Failed to initiate request! Steam is either not logged in or there are no Stats Published for the current game.");
			m_StatsDirty = false;
		}
	}

	public override void LoadAchievementState(AchievementObject achievement)
	{
		d.AssertFormat(SteamUserStats.GetAchievement(achievement.SteamID, out var pbAchieved), "AchievementImplSteam.LoadAchievementState - Achievement with ID '{0}' did not exist in the Steamworks App config, or changes were not Published!", achievement.SteamID);
		achievement.IsCompleted = pbAchieved;
		achievement.LoadStats(this);
	}

	public override void CompleteAchievement(AchievementObject achievement)
	{
		d.AssertFormat(SteamUserStats.SetAchievement(achievement.SteamID), "AchievementImplSteam.CompleteAchievement - Achievement with ID '{0}' did not exist in the Steamworks App config, or changes were not Published!", achievement.SteamID);
		m_StatsDirty = true;
	}

	public override void LoadStat(AchievementObject.Stat stat)
	{
		bool flag = false;
		switch (stat.StatType)
		{
		case AchievementObject.StatType.Int:
		{
			flag = SteamUserStats.GetStat(stat.SteamID, out int pData2);
			if (flag)
			{
				stat.IntValue = pData2;
			}
			break;
		}
		case AchievementObject.StatType.Float:
		{
			flag = SteamUserStats.GetStat(stat.SteamID, out float pData);
			if (flag)
			{
				stat.FloatValue = pData;
			}
			break;
		}
		default:
			d.LogError("LoadStat - Unhandled stat type!");
			break;
		}
		if (!flag)
		{
			d.LogErrorFormat("SteamAchievement.LoadStat - Failed to get data for {0} stat '{1}'. Either it did not exist in the Steamworks App config, or changes were not Published. Or the stat type is configured differently.", stat.StatType, stat.SteamID);
		}
	}

	public override void UpdateStat(AchievementObject.Stat stat)
	{
		bool flag = false;
		switch (stat.StatType)
		{
		case AchievementObject.StatType.Int:
			flag = SteamUserStats.SetStat(stat.SteamID, stat.IntValue);
			break;
		case AchievementObject.StatType.Float:
			flag = SteamUserStats.SetStat(stat.SteamID, stat.FloatValue);
			break;
		default:
			d.LogError("UpdateStat - Unhandled stat type!");
			break;
		}
		if (!flag)
		{
			d.LogErrorFormat("SteamAchievement.LoadStat - Failed to set data for {0} stat '{1}'. Either it did not exist in the Steamworks App config, or changes were not Published. Or the stat type is configured differently.", stat.StatType, stat.SteamID);
		}
	}

	public override void StoreProgress()
	{
		m_StatsDirty = true;
	}

	public override void ResetAllAchievements()
	{
		SteamUserStats.ResetAllStats(bAchievementsToo: true);
	}

	private void OnUserStatsReceived(UserStatsReceived_t result)
	{
		if (result.m_nGameID == Singleton.Manager<ManSteamworks>.inst.GameID.m_GameID)
		{
			bool flag = result.m_eResult == EResult.k_EResultOK;
			if (flag)
			{
				d.Log("SteamUserStats were received successfully!");
			}
			else
			{
				d.LogErrorFormat("OnUserStatsReceived - Failed to Receive UserStats - with result {0}", result.m_eResult);
			}
			m_InitialisationCompleteHandler.Send(flag);
		}
	}

	private void OnUserStatsStored(UserStatsStored_t result)
	{
		if (result.m_nGameID == Singleton.Manager<ManSteamworks>.inst.GameID.m_GameID)
		{
			if (result.m_eResult == EResult.k_EResultOK)
			{
				d.Log("SteamUserStats were stored successfully!");
			}
			else if (result.m_eResult == EResult.k_EResultInvalidParam)
			{
				d.LogError("OnUserStatsStored - Failed to Store UserStats with result k_EResultInvalidParam. This happens when one or more stats uploaded has been rejected, either because they broke constraints or were out of date. In this case the server sends back updated values and the stats should be updated locally to keep in sync.");
				Singleton.Manager<ManAchievements>.inst.LoadAchievementState();
			}
			else
			{
				d.LogErrorFormat("OnUserStatsStored - Failed to Store UserStats - with result {0}", result.m_eResult);
			}
		}
	}
}
