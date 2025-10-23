#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.Stats;
using PlayEveryWare.EpicOnlineServices;

public class AchievementImplEpicGameStore : AchievementPlatformImpl
{
	private Action<bool> m_InitialisationCompleteHandler;

	private HashSet<AchievementObject.Stat> m_DirtyStats = new HashSet<AchievementObject.Stat>();

	private HashSet<AchievementObject> m_DirtyAchievements = new HashSet<AchievementObject>();

	private int m_DataReloadCounter;

	public override bool Init(Action<bool> initCompleteHandler)
	{
		m_InitialisationCompleteHandler = initCompleteHandler;
		d.Assert(Singleton.Manager<ManEOS>.inst.IsConnected, "Cannot initialise EOS achievements before user has logged in!");
		if (Singleton.Manager<ManEOS>.inst.IsConnected)
		{
			QueryPlayerAchievementsOptions options = new QueryPlayerAchievementsOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				TargetUserId = EOSManager.Instance.GetProductUserId()
			};
			EOSManager.Instance.GetEOSAchievementInterface().QueryPlayerAchievements(ref options, null, OnQueryPlayerAchievementsComplete);
			return true;
		}
		return false;
	}

	public override void Update()
	{
		m_DataReloadCounter = 0;
		if (m_DirtyStats.Count > 0)
		{
			m_DataReloadCounter++;
			IngestData[] array = new IngestData[m_DirtyStats.Count];
			int num = 0;
			foreach (AchievementObject.Stat dirtyStat in m_DirtyStats)
			{
				array[num] = new IngestData
				{
					StatName = dirtyStat.EosID,
					IngestAmount = dirtyStat.IntValue
				};
				num++;
			}
			IngestStatOptions options = new IngestStatOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				Stats = array
			};
			EOSManager.Instance.GetEOSStatsInterface().IngestStat(ref options, array, OnUpdateStatComplete);
			m_DirtyStats.Clear();
		}
		if (m_DirtyAchievements.Count <= 0)
		{
			return;
		}
		m_DataReloadCounter++;
		Utf8String[] array2 = new Utf8String[m_DirtyAchievements.Count];
		int num2 = 0;
		foreach (AchievementObject dirtyAchievement in m_DirtyAchievements)
		{
			array2[num2] = dirtyAchievement.EosID;
			num2++;
		}
		UnlockAchievementsOptions options2 = new UnlockAchievementsOptions
		{
			UserId = EOSManager.Instance.GetProductUserId(),
			AchievementIds = array2
		};
		EOSManager.Instance.GetEOSAchievementInterface().UnlockAchievements(ref options2, array2, OnUnlockAchievementsComplete);
		m_DirtyAchievements.Clear();
	}

	public override void LoadAchievementState(AchievementObject achievement)
	{
		CopyPlayerAchievementByAchievementIdOptions options = new CopyPlayerAchievementByAchievementIdOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			TargetUserId = EOSManager.Instance.GetProductUserId(),
			AchievementId = achievement.EosID
		};
		d.AssertFormat(EOSManager.Instance.GetEOSAchievementInterface().CopyPlayerAchievementByAchievementId(ref options, out var outAchievement) == Result.Success && outAchievement.HasValue, "AchievementImplEpicGameStore.LoadAchievementState - Achievement with ID '{0}' did not exist in the EOS config, or changes were not Published!", achievement.EosID);
		bool isCompleted = outAchievement.HasValue && outAchievement.Value.UnlockTime.HasValue;
		achievement.IsCompleted = isCompleted;
		achievement.LoadStats(this);
	}

	public override void CompleteAchievement(AchievementObject achievement)
	{
		m_DirtyAchievements.Add(achievement);
	}

	public override void LoadStat(AchievementObject.Stat stat)
	{
		d.AssertFormat(stat.StatType == AchievementObject.StatType.Int, "LoadStat - Stat of type {0} is not supported on EOS platform!", stat.StatType);
		CopyStatByNameOptions options = new CopyStatByNameOptions
		{
			Name = stat.EosID,
			TargetUserId = EOSManager.Instance.GetProductUserId()
		};
		if (EOSManager.Instance.GetEOSStatsInterface().CopyStatByName(ref options, out var outStat) == Result.Success)
		{
			stat.IntValue = outStat.Value.Value;
			return;
		}
		d.LogErrorFormat("EOS Achievements.LoadStat - Failed to get data for {0} stat '{1}'. Either it did not exist in the EOS developer config, or changes were not Published. Or the stat type is configured differently.", stat.StatType, stat.EosID);
	}

	public override void UpdateStat(AchievementObject.Stat stat)
	{
		d.AssertFormat(stat.StatType == AchievementObject.StatType.Int, "UpdateStat - Stat of type {0} is not supported on EOS platform!", stat.StatType);
		m_DirtyStats.Add(stat);
	}

	public override void StoreProgress()
	{
	}

	public override void ResetAllAchievements()
	{
		d.LogError("Resetting Achievements is not supported through the EOS API. Perhaps this is possible from the user account portal?");
	}

	private void OnQueryPlayerAchievementsComplete(ref OnQueryPlayerAchievementsCompleteCallbackInfo data)
	{
		if (data.ResultCode == Result.Success)
		{
			d.Log("EOS achievements were received successfully!");
		}
		else
		{
			d.LogErrorFormat("OnQueryPlayerAchievementsComplete - Failed to Receive player achievement data. Result: {0}", data.ResultCode);
		}
		QueryStatsOptions options = new QueryStatsOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			TargetUserId = EOSManager.Instance.GetProductUserId()
		};
		EOSManager.Instance.GetEOSStatsInterface().QueryStats(ref options, null, OnQueryStatsComplete);
	}

	private void OnQueryStatsComplete(ref OnQueryStatsCompleteCallbackInfo data)
	{
		bool flag = data.ResultCode == Result.Success;
		if (flag)
		{
			d.Log("EOS stats were received successfully!");
		}
		else
		{
			d.LogErrorFormat("OnQueryStatsComplete - Failed to Receive player stat data. Result: {0}", data.ResultCode);
		}
		m_InitialisationCompleteHandler.Send(flag);
	}

	private void OnUpdateStatComplete(ref IngestStatCompleteCallbackInfo data)
	{
		IngestData[] source = data.ClientData as IngestData[];
		m_DataReloadCounter--;
		if (data.ResultCode == Result.Success)
		{
			if (m_DataReloadCounter <= 0)
			{
				m_DataReloadCounter = 0;
				Singleton.Manager<ManAchievements>.inst.LoadAchievementState();
			}
		}
		else
		{
			d.LogErrorFormat("OnUpdateStatComplete - Failed to Ingest player stats {0}. Result: {1}", string.Join(", ", source.Select((IngestData ingestData) => ingestData.StatName)), data.ResultCode);
		}
	}

	private void OnUnlockAchievementsComplete(ref OnUnlockAchievementsCompleteCallbackInfo data)
	{
		Utf8String[] source = data.ClientData as Utf8String[];
		m_DataReloadCounter--;
		if (data.ResultCode == Result.Success)
		{
			if (m_DataReloadCounter <= 0)
			{
				m_DataReloadCounter = 0;
				Singleton.Manager<ManAchievements>.inst.LoadAchievementState();
			}
		}
		else
		{
			d.LogErrorFormat("OnUnlockAchievementsComplete - Failed to Unlock player achievements {0}. Result: {1}", string.Join(", ", source.Select((Utf8String s) => s.ToString())), data.ResultCode);
		}
	}
}
