#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.Networking;

public class NetScore
{
	public EventNoParams OnChanged;

	private float m_Points;

	private int m_Kills;

	private int m_KillStreak;

	private int m_Deaths;

	private int m_DeathStreak;

	public float Points => m_Points;

	public int Kills => m_Kills;

	public int KillStreak => m_KillStreak;

	public int Deaths => m_Deaths;

	public int DeathStreak => m_DeathStreak;

	public static int CompareDescending(NetScore scoreA, NetScore scoreB)
	{
		NetController.ScorePolicy currentScorePolicy = Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy;
		int num;
		switch (currentScorePolicy)
		{
		case NetController.ScorePolicy.Kills:
			num = scoreB.m_Kills - scoreA.m_Kills;
			if (num == 0)
			{
				num = scoreA.m_Deaths - scoreB.m_Deaths;
			}
			break;
		case NetController.ScorePolicy.SetTime:
		case NetController.ScorePolicy.NumWaves:
			num = (int)(scoreB.m_Points - scoreA.m_Points);
			break;
		case NetController.ScorePolicy.KillMinusDeath:
			num = scoreB.m_Kills - scoreB.m_Deaths - (scoreA.m_Kills - scoreA.m_Deaths);
			break;
		default:
			d.AssertFormat(false, "NetScore.CompareDescending unhandled policy type {0}", currentScorePolicy);
			num = 0;
			break;
		}
		return num;
	}

	public void Reset()
	{
		m_Points = 0f;
		m_Kills = 0;
		m_KillStreak = 0;
		m_Deaths = 0;
		m_DeathStreak = 0;
		OnChanged.Send();
	}

	public void IncKills()
	{
		m_Kills++;
		if (Singleton.Manager<ManNetwork>.inst.KillStreakRewardsEnabled)
		{
			m_KillStreak++;
		}
		if (Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled)
		{
			m_DeathStreak = 0;
		}
		OnChanged.Send();
	}

	public void IncDeaths(bool incDeathStreak)
	{
		m_Deaths++;
		if (Singleton.Manager<ManNetwork>.inst.KillStreakRewardsEnabled && Singleton.Manager<ManNetwork>.inst.KillStreakResetsWhenDestroyedEnabled)
		{
			m_KillStreak = 0;
		}
		if (incDeathStreak && Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled)
		{
			m_DeathStreak++;
		}
		OnChanged.Send();
	}

	public void ResetKillStreak()
	{
		if (m_KillStreak != 0)
		{
			m_KillStreak = 0;
			OnChanged.Send();
		}
	}

	public void ResetDeathStreak()
	{
		if (m_DeathStreak != 0)
		{
			m_DeathStreak = 0;
			OnChanged.Send();
		}
	}

	public void AddPoints(float addAmount)
	{
		d.AssertFormat(addAmount >= 0f, "NetPlayer.ServerAddScore() value {0} not positive", addAmount);
		m_Points += addAmount;
		OnChanged.Send();
	}

	public void SetPoints(float setValue)
	{
		d.AssertFormat(setValue >= 0f, "NetPlayer.ServerAddScore() value {0} not positive", setValue);
		m_Points = setValue;
		OnChanged.Send();
	}

	public float Evaluate(NetController.ScorePolicy scorePolicy)
	{
		float result = 0f;
		switch (scorePolicy)
		{
		case NetController.ScorePolicy.Kills:
			result = m_Kills;
			break;
		case NetController.ScorePolicy.SetTime:
		case NetController.ScorePolicy.NumWaves:
			result = m_Points;
			break;
		case NetController.ScorePolicy.KillMinusDeath:
			result = m_Kills - m_Deaths;
			break;
		}
		return result;
	}

	public string EvaluateToString(NetController.ScorePolicy scorePolicy)
	{
		float num = Evaluate(scorePolicy);
		if (scorePolicy == NetController.ScorePolicy.SetTime)
		{
			num = Mathf.Max(Singleton.Manager<ManNetwork>.inst.NetController.ScoreToWin - num, 0f);
		}
		if (scorePolicy != NetController.ScorePolicy.GameTime && scorePolicy != NetController.ScorePolicy.SetTime)
		{
			return ((int)num).ToString();
		}
		return Util.GetTimeString(num, showMinutes: false, showMilliseconds: true);
	}

	public void NetSerialize(NetworkWriter writer)
	{
		writer.Write(m_Points);
		writer.Write(m_Kills);
		writer.Write(m_KillStreak);
		writer.Write(m_Deaths);
		writer.Write(m_DeathStreak);
	}

	public void NetDeserialize(NetworkReader reader)
	{
		m_Points = reader.ReadSingle();
		m_Kills = reader.ReadInt32();
		m_KillStreak = reader.ReadInt32();
		m_Deaths = reader.ReadInt32();
		m_DeathStreak = reader.ReadInt32();
		OnChanged.Send();
	}
}
