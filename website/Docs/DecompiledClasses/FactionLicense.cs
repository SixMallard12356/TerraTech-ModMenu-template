#define UNITY_EDITOR
using System;
using UnityEngine;

public class FactionLicense
{
	public class Progress
	{
		public bool m_Discovered;

		public int m_CurrentLevel;

		public int m_CurrentXP;
	}

	[Serializable]
	public class Thresholds
	{
		public int m_MaxSupportedLevel;

		public int[] m_XPLevels;

		public int MaxXP => GetMaxXPAtLevel(m_XPLevels.Length - 1);

		public int GetMaxXPAtLevel(int level)
		{
			int num = 0;
			int num2 = Mathf.Min(level, m_XPLevels.Length - 1);
			d.AssertFormat(level == num2, "Thresholds.GetMaxXPAtLevel level passed in is higher than max available level! {0}/{1}", level, m_XPLevels.Length - 1);
			for (int i = 0; i <= num2; i++)
			{
				num += m_XPLevels[i];
			}
			return num;
		}

		public bool PassLevel(int before, int after)
		{
			for (int i = 0; i < m_XPLevels.Length; i++)
			{
				if (before < m_XPLevels[i] && after >= m_XPLevels[i])
				{
					return true;
				}
			}
			return false;
		}
	}

	private FactionSubTypes m_Corporation;

	private Thresholds m_Thresholds;

	private Progress m_Progress;

	public FactionSubTypes Corporation => m_Corporation;

	public bool IsSupported => NumXpLevels > 0;

	public int NumXpLevels
	{
		get
		{
			int maxSupportedLevel = m_Thresholds.m_MaxSupportedLevel;
			return Mathf.Min(m_Thresholds.m_XPLevels.Length, maxSupportedLevel);
		}
	}

	public bool IsDiscovered => m_Progress.m_Discovered;

	public int CurrentLevel => m_Progress.m_CurrentLevel;

	public bool HasReachedMaxLevel
	{
		get
		{
			if (CurrentLevel == NumXpLevels - 1)
			{
				return CurrentAbsoluteXP == NextLevelAbsoluteXP;
			}
			return false;
		}
	}

	public int CurrentAbsoluteXP => m_Progress.m_CurrentXP;

	public int BaseLevelAbsoluteXP
	{
		get
		{
			int num = 0;
			for (int i = 0; i < CurrentLevel; i++)
			{
				if (i < m_Thresholds.m_XPLevels.Length)
				{
					num += m_Thresholds.m_XPLevels[i];
				}
			}
			return num;
		}
	}

	public int NextLevelAbsoluteXP
	{
		get
		{
			int num = 0;
			for (int i = 0; i <= CurrentLevel; i++)
			{
				if (i < m_Thresholds.m_XPLevels.Length)
				{
					num += m_Thresholds.m_XPLevels[i];
				}
			}
			return num;
		}
	}

	public FactionLicense(FactionSubTypes corporation, Thresholds thresholds, Progress progress)
	{
		m_Corporation = corporation;
		m_Thresholds = thresholds;
		m_Progress = progress;
	}

	public float GetXPScaleValue()
	{
		return (float)(CurrentAbsoluteXP - BaseLevelAbsoluteXP) / (float)(NextLevelAbsoluteXP - BaseLevelAbsoluteXP);
	}

	public bool AddXP(int value, out int remainingXP, out bool leveledUP)
	{
		int currentXP = m_Progress.m_CurrentXP;
		leveledUP = false;
		remainingXP = 0;
		if (!HasReachedMaxLevel)
		{
			int nextLevelAbsoluteXP = NextLevelAbsoluteXP;
			int num = m_Progress.m_CurrentXP + value;
			if (num >= nextLevelAbsoluteXP)
			{
				if (CurrentLevel + 1 < NumXpLevels)
				{
					remainingXP = num - nextLevelAbsoluteXP;
				}
				num = nextLevelAbsoluteXP;
			}
			m_Progress.m_CurrentXP = num;
			if (m_Progress.m_CurrentXP == nextLevelAbsoluteXP)
			{
				leveledUP = true;
			}
		}
		return currentXP != m_Progress.m_CurrentXP;
	}

	public void LevelUp(int levelRequired)
	{
		if (!HasReachedMaxLevel)
		{
			d.Assert(CurrentLevel + 1 == levelRequired, "Not Current Level");
			m_Progress.m_CurrentLevel++;
		}
		else
		{
			d.LogError("Not Maxed");
		}
	}

	public void Discover()
	{
		m_Progress.m_Discovered = true;
		m_Progress.m_CurrentLevel = 0;
		m_Progress.m_CurrentXP = 0;
	}

	public int GetMaxXP()
	{
		return m_Thresholds.MaxXP;
	}
}
