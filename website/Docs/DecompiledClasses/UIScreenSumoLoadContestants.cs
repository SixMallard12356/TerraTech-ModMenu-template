using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIScreenSumoLoadContestants : UIScreen
{
	public class SumoRankedPlayer
	{
		public Snapshot m_Enemy;

		public int m_Level;
	}

	public UISumoContestant[] m_MyContestants;

	private int m_EnemyCount = 3;

	private SumoRankedPlayer[] m_Enemies;

	private bool[] m_FetchingTweets;

	private bool[] m_EnemiesFetched;

	private SnapshotTwitter m_Player;

	public void SetPlayer(SnapshotTwitter capture)
	{
		m_Player = capture;
	}

	public override void Show(bool fromStackPop)
	{
		base.Show(fromStackPop);
		for (int i = 0; i < m_EnemyCount; i++)
		{
			m_MyContestants[i].Hide();
		}
		ManUI inst = Singleton.Manager<ManUI>.inst;
		inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Combine(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
		FetchSumoEnemies();
	}

	private void OnScreenChanged(bool pushed, ManUI.ScreenType screenType)
	{
		if (screenType == ManUI.ScreenType.SumoRankedEnemies && !pushed)
		{
			StopAllCoroutines();
			ManUI inst = Singleton.Manager<ManUI>.inst;
			inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Remove(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
		}
	}

	private void Awake()
	{
		m_EnemyCount = m_MyContestants.Length;
	}

	private void Update()
	{
		bool flag = false;
		bool flag2 = false;
		for (int i = 0; i < m_EnemyCount; i++)
		{
			if (m_EnemiesFetched[i])
			{
				if (m_Enemies[i].m_Enemy != null)
				{
					flag2 = true;
				}
				m_MyContestants[i].SetData(m_Enemies[i].m_Enemy, m_Enemies[i].m_Level, m_Player);
			}
			else
			{
				flag = true;
			}
		}
		if (!flag && !flag2)
		{
			UIScreenSumoChampion uIScreenSumoChampion = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SumoRankedChampion) as UIScreenSumoChampion;
			if (uIScreenSumoChampion != null)
			{
				uIScreenSumoChampion.SetScreenInfo(m_Player);
				Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenSumoChampion);
			}
		}
	}

	private int GetEnemyRank(int index)
	{
		int num = Mode<ModeSumo>.inst.CurrentRank - Mode<ModeSumo>.inst.PreviousRank;
		return Mode<ModeSumo>.inst.CurrentRank + num * index;
	}

	private IEnumerator FetchSumoEnemy(int slotIndex, int currentLevel, int lastLevel)
	{
		int level = currentLevel;
		bool foundEnemy = false;
		while (!foundEnemy && level >= lastLevel)
		{
			m_FetchingTweets[slotIndex] = true;
			string userDataRestriction = Mode<ModeSumo>.inst.RankedHashtag + level;
			SnapshotCollectionTwitter captures = new SnapshotCollectionTwitter("Sumo", null, userDataRestriction);
			TwitterAPI.TweetWithMediaDataThreaded fetchedTweets = new TwitterAPI.TweetWithMediaDataThreaded();
			GetSumoEnemy(slotIndex, level, fetchedTweets);
			while (m_FetchingTweets[slotIndex])
			{
				yield return null;
			}
			for (int i = 0; i < fetchedTweets.m_Links.Count(); i++)
			{
				TwitterAPI.TweetWithMedia tweetData = fetchedTweets.m_Links[i];
				yield return captures.TryAddFromImage(tweetData);
			}
			if (captures != null && captures.Snapshots.Count > 0)
			{
				List<Snapshot> list = new List<Snapshot>();
				foreach (SnapshotTwitter snapshot in captures.Snapshots)
				{
					if (!("Sumo" != snapshot.techData.m_CreationData.mode))
					{
						list.Add(snapshot);
					}
				}
				if (list.Count > 0)
				{
					int index = UnityEngine.Random.Range(0, list.Count - 1);
					m_Enemies[slotIndex].m_Enemy = list[index];
					m_Enemies[slotIndex].m_Level = level;
					foundEnemy = true;
				}
			}
			level--;
		}
		m_EnemiesFetched[slotIndex] = true;
	}

	private void FetchSumoEnemies()
	{
		m_Enemies = new SumoRankedPlayer[m_EnemyCount];
		m_FetchingTweets = new bool[m_EnemyCount];
		m_EnemiesFetched = new bool[m_EnemyCount];
		int lastLevel = GetEnemyRank(0);
		for (int i = 0; i < m_EnemyCount; i++)
		{
			m_Enemies[i] = new SumoRankedPlayer();
			int enemyRank = GetEnemyRank(i);
			StartCoroutine(FetchSumoEnemy(i, enemyRank, lastLevel));
			lastLevel = enemyRank + 1;
		}
	}

	private void GetSumoEnemy(int index, int level, TwitterAPI.TweetWithMediaDataThreaded tweets)
	{
		string text = Mode<ModeSumo>.inst.RankedHashtag + level;
		Singleton.Manager<TwitterAPI>.inst.RetrieveTaggedTweetsAsync(text, myTweets: false, tweets, delegate
		{
			m_FetchingTweets[index] = false;
		});
	}
}
