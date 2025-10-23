#define UNITY_EDITOR
using System;
using Binding;
using MonsterLove.StateMachine;
using UnityEngine;

namespace Snapshots;

public class SnapshotServiceTwitter : MonoBehaviour, ISnapshotService
{
	public enum TwitterStates
	{
		TwitterIdle,
		TwitterAuth,
		TwitterLoading,
		TwitterComplete
	}

	public enum CommunityTagGroup
	{
		Default,
		Sumo
	}

	private TwitterAPI.TweetWithMediaDataThreaded m_FetchedTweets;

	private SnapshotCollectionTwitter m_SnapshotsTwitter = new SnapshotCollectionTwitter(null, null, null);

	private Bindable<ManSnapshots.QueryStatus> m_QueryStatus = new Bindable<ManSnapshots.QueryStatus>(ManSnapshots.QueryStatus.Nil);

	private bool m_FinishedFetchingVehicleData;

	private bool m_TwitterLoginFailed;

	private bool m_TwitterAuthPending;

	private int m_NumTweetsDecoded;

	private CommunityTagGroup m_CommunityTagGroup;

	private StateMachine<TwitterStates> m_TwitterFSM;

	private static string[] m_CommunityHashtag = new string[2] { "#myTerraTech", "#myTerraTechSumo" };

	public SnapshotCollectionTwitter SnapshotCollection => m_SnapshotsTwitter;

	public Bindable<ManSnapshots.QueryStatus> QueryStatus => m_QueryStatus;

	public void Load()
	{
		m_QueryStatus.Value = ManSnapshots.QueryStatus.Requesting;
		m_FinishedFetchingVehicleData = false;
		m_TwitterLoginFailed = false;
		m_TwitterFSM.ChangeState(TwitterStates.TwitterAuth);
	}

	public bool SupportsRenameAndDelete()
	{
		return false;
	}

	public void DeleteSnapshot(Snapshot snapshot)
	{
		throw new NotImplementedException();
	}

	public bool SnapshotExists(string snapshotName)
	{
		throw new NotImplementedException();
	}

	public void RenameSnapshot(Snapshot snapshot, string newName)
	{
		throw new NotImplementedException();
	}

	public bool SupportsFavourites()
	{
		return false;
	}

	public bool SupportsFolders()
	{
		return false;
	}

	public void SetSnapshotFavourite(Snapshot snapshot, bool favourite)
	{
		throw new NotImplementedException();
	}

	public void SetSnapshotMetadata(Snapshot snapshot, Snapshot.MetaData metadata)
	{
		throw new NotImplementedException();
	}

	public void SetSnapshotFolder(Snapshot snapshot, string folderName)
	{
		throw new NotImplementedException();
	}

	public void ApplyCachedMetadataToSnapshot(Snapshot snapshot)
	{
		throw new NotImplementedException();
	}

	private void TwitterAuth_Enter()
	{
		m_TwitterAuthPending = true;
		Singleton.Manager<TwitterAuthenticationUIHandler>.inst.TryAuthenticateAsync(TwitterAuth_OnComplete);
	}

	private void TwitterAuth_OnComplete(bool loggedIn)
	{
		m_TwitterAuthPending = false;
		m_TwitterLoginFailed = !loggedIn;
	}

	private void TwitterAuth_Update()
	{
		if (!m_TwitterAuthPending)
		{
			if (!m_TwitterLoginFailed)
			{
				m_TwitterFSM.ChangeState(TwitterStates.TwitterLoading);
			}
			else
			{
				m_TwitterFSM.ChangeState(TwitterStates.TwitterComplete);
			}
		}
	}

	private void TwitterAuth_Exit()
	{
		if (m_TwitterAuthPending)
		{
			m_TwitterAuthPending = false;
			Singleton.Manager<TwitterAuthenticationUIHandler>.inst.CancelAsync();
		}
	}

	private void TwitterLoading_Enter()
	{
		string loadVehicleModeRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleModeRestriction;
		string loadVehicleSubModeRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleSubModeRestriction;
		string loadVehicleUserDataRestriction = Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleUserDataRestriction;
		m_SnapshotsTwitter.Reset(loadVehicleModeRestriction, loadVehicleSubModeRestriction, loadVehicleUserDataRestriction);
		m_FinishedFetchingVehicleData = false;
		m_FetchedTweets = new TwitterAPI.TweetWithMediaDataThreaded();
		string text = m_CommunityHashtag[(int)m_CommunityTagGroup];
		Singleton.Manager<TwitterAPI>.inst.RetrieveTaggedTweetsAsync(text, myTweets: false, m_FetchedTweets, TwitterLoading_RequestCompleted);
		m_NumTweetsDecoded = 0;
	}

	private void TwitterLoading_Update()
	{
		if (!DecodeTweet(m_SnapshotsTwitter) && m_FinishedFetchingVehicleData)
		{
			m_TwitterFSM.ChangeState(TwitterStates.TwitterComplete);
		}
	}

	private void TwitterLoading_RequestCompleted()
	{
		m_FinishedFetchingVehicleData = true;
	}

	private void TwitterLoading_Exit()
	{
		if (!m_FinishedFetchingVehicleData)
		{
			Singleton.Manager<TwitterAPI>.inst.Cancel();
		}
	}

	private void TwitterComplete_Enter()
	{
		m_QueryStatus.Value = (m_TwitterLoginFailed ? ManSnapshots.QueryStatus.AuthFailed : ManSnapshots.QueryStatus.Done);
	}

	private void Twitter_Exit()
	{
		m_TwitterFSM.ChangeState(TwitterStates.TwitterIdle);
	}

	private bool DecodeTweet(SnapshotCollectionTwitter snapshotCollection)
	{
		d.Assert(m_FetchedTweets != null, "m_FetchedTweets is null, has the Twitter API been called correctly?");
		lock (m_FetchedTweets.m_Lock)
		{
			if (m_NumTweetsDecoded < m_FetchedTweets.m_Links.Count)
			{
				TwitterAPI.TweetWithMedia tweetData = m_FetchedTweets.m_Links[m_NumTweetsDecoded];
				Texture2D texture2D = new Texture2D(4, 4);
				texture2D.LoadImage(tweetData.ImageArray);
				snapshotCollection.TryAddFromImage(texture2D, tweetData, out var _, convertLegacy: false);
				m_NumTweetsDecoded++;
				return true;
			}
		}
		return false;
	}

	private void Awake()
	{
		m_TwitterFSM = StateMachine<TwitterStates>.Initialize(this, TwitterStates.TwitterIdle);
	}
}
