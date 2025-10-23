#define UNITY_EDITOR
using System;
using UnityEngine;

public abstract class AchievementObject : ScriptableObject
{
	[Serializable]
	public abstract class Stat
	{
		[SerializeField]
		[Header("Steam")]
		private string m_SteamID;

		[SerializeField]
		[Header("EOS")]
		private string m_EosID;

		[SerializeField]
		[Header("PS4")]
		private string m_PsnID;

		[Header("Xbox One")]
		[SerializeField]
		private string m_XdpID;

		protected StatType m_StatType;

		private int m_IntValue;

		private float m_FloatValue;

		public string SteamID => m_SteamID;

		public string EosID => m_EosID;

		public string PsnID => m_PsnID;

		public string XdpID => m_XdpID;

		public StatType StatType => m_StatType;

		public int IntValue
		{
			get
			{
				AssertTypeMatches(StatType.Int);
				return m_IntValue;
			}
			set
			{
				AssertTypeMatches(StatType.Int);
				m_IntValue = value;
			}
		}

		public float FloatValue
		{
			get
			{
				AssertTypeMatches(StatType.Float);
				return m_FloatValue;
			}
			set
			{
				AssertTypeMatches(StatType.Float);
				m_FloatValue = value;
			}
		}

		private void AssertTypeMatches(StatType type)
		{
			d.AssertFormat(m_StatType == type, "Stat type missmatch! Stat is of type {0} but was queried as {1}.", m_StatType, type);
		}
	}

	[Serializable]
	public class IntStat : Stat
	{
		public IntStat()
		{
			m_StatType = StatType.Int;
		}
	}

	[Serializable]
	public class FloatStat : Stat
	{
		public FloatStat()
		{
			m_StatType = StatType.Float;
		}
	}

	public enum StatType
	{
		Int,
		Float
	}

	[SerializeField]
	[Header("Steam")]
	private string m_SteamID;

	[Header("Epic Online Services")]
	[SerializeField]
	private string m_EosID;

	[Header("PS4")]
	[SerializeField]
	private int m_PsnID;

	[Header("Xbox One")]
	[SerializeField]
	private string m_XdpID;

	[SerializeField]
	private ManGameMode.GameType m_AchievableInMode = ManGameMode.GameType.MainGame;

	public Event<Stat> OnStatUpdated;

	public Event<AchievementObject> OnAchievementCompleted;

	public bool IsCompleted { get; set; }

	public ManGameMode.GameType AchievableInMode => m_AchievableInMode;

	public string SteamID => m_SteamID;

	public string EosID => m_EosID;

	public int PsnID => m_PsnID;

	public string XdpID => m_XdpID;

	public virtual void LoadStats(AchievementPlatformImpl achievementPlatform)
	{
	}

	public virtual void Initialise()
	{
	}

	public virtual void Update()
	{
	}

	public virtual bool IsActive()
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == m_AchievableInMode)
		{
			return Singleton.Manager<ManAchievements>.inst.ActiveInCurrentMode;
		}
		return false;
	}

	public void RegisterCallbacks(Action<Stat> onStatUpdatedCallback, Action<AchievementObject> onAchievementCompleteCallback)
	{
		OnStatUpdated.Subscribe(onStatUpdatedCallback);
		OnAchievementCompleted.Subscribe(onAchievementCompleteCallback);
	}

	public void UnregisterCallbacks(Action<Stat> onStatUpdatedCallback, Action<AchievementObject> onAchievementCompleteCallback)
	{
		OnStatUpdated.Unsubscribe(onStatUpdatedCallback);
		OnAchievementCompleted.Unsubscribe(onAchievementCompleteCallback);
	}

	public void CompleteAchievement()
	{
		d.AssertFormat(IsActive(), "CompleteAchievement - Trying to complete Achievement '{0}' type {1}, but the achievement is not currently active!?", SteamID, GetType());
		if (!IsCompleted)
		{
			IsCompleted = true;
			OnAchievementCompleted.Send(this);
		}
	}

	protected void UpdateStat(Stat stat)
	{
		OnStatUpdated.Send(stat);
	}
}
