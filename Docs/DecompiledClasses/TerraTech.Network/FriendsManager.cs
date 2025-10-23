#define UNITY_EDITOR
using System.Collections.Generic;
using Epic.OnlineServices;
using Epic.OnlineServices.Friends;
using PlayEveryWare.EpicOnlineServices;

namespace TerraTech.Network;

internal class FriendsManager
{
	private HashSet<TTNetworkID> m_CachedFriends = new HashSet<TTNetworkID>();

	private NotifyEventHandle m_FriendStatusUpdatedHandle;

	public bool IsFriend(TTNetworkID playerID)
	{
		return m_CachedFriends.Contains(playerID);
	}

	public FriendsManager()
	{
		InitialiseFriendsList();
		AddNotifyFriendsUpdateOptions options = default(AddNotifyFriendsUpdateOptions);
		m_FriendStatusUpdatedHandle = new NotifyEventHandle(EOSManager.Instance.GetEOSFriendsInterface().AddNotifyFriendsUpdate(ref options, null, OnFriendStatusUpdated), EOSManager.Instance.GetEOSFriendsInterface().RemoveNotifyFriendsUpdate);
		Singleton.Manager<ManEOS>.inst.InitStateChangedEvent.Subscribe(OnEOSInitStateChanged);
	}

	public void Cleanup()
	{
		m_FriendStatusUpdatedHandle.Dispose();
	}

	private void InitialiseFriendsList()
	{
		m_CachedFriends.Clear();
		QueryFriendsOptions options = new QueryFriendsOptions
		{
			LocalUserId = EOSManager.Instance.GetLocalUserId()
		};
		EOSManager.Instance.GetEOSFriendsInterface().QueryFriends(ref options, null, OnQueryFriendsComplete);
	}

	private void OnEOSInitStateChanged(ManEOS.InitState prevState, ManEOS.InitState newState)
	{
		if (prevState.IsNot(ManEOS.InitState.ConnectLoggedIn) && newState.Is(ManEOS.InitState.ConnectLoggedIn))
		{
			InitialiseFriendsList();
		}
		else if (newState.IsNot(ManEOS.InitState.ConnectLoggedIn))
		{
			m_CachedFriends.Clear();
		}
	}

	private void OnQueryFriendsComplete(ref QueryFriendsCallbackInfo data)
	{
		d.AssertFormat(data.ResultCode == Result.Success, "Failed to Query Friends data, result: {0}", data.ResultCode);
		FriendsInterface eOSFriendsInterface = EOSManager.Instance.GetEOSFriendsInterface();
		if (data.ResultCode != Result.Success)
		{
			return;
		}
		GetFriendsCountOptions options = new GetFriendsCountOptions
		{
			LocalUserId = EOSManager.Instance.GetLocalUserId()
		};
		int friendsCount = eOSFriendsInterface.GetFriendsCount(ref options);
		for (int i = 0; i < friendsCount; i++)
		{
			GetFriendAtIndexOptions options2 = new GetFriendAtIndexOptions
			{
				LocalUserId = EOSManager.Instance.GetLocalUserId(),
				Index = i
			};
			EpicAccountId friendAtIndex = eOSFriendsInterface.GetFriendAtIndex(ref options2);
			GetStatusOptions options3 = new GetStatusOptions
			{
				LocalUserId = EOSManager.Instance.GetLocalUserId(),
				TargetUserId = friendAtIndex
			};
			if (eOSFriendsInterface.GetStatus(ref options3) == FriendsStatus.Friends)
			{
				m_CachedFriends.Add(friendAtIndex.ToTTID());
			}
		}
	}

	private void OnFriendStatusUpdated(ref OnFriendsUpdateInfo data)
	{
		bool num = data.PreviousStatus == FriendsStatus.Friends;
		bool flag = data.CurrentStatus == FriendsStatus.Friends;
		if (num != flag)
		{
			TTNetworkID item = data.TargetUserId.ToTTID();
			if (flag)
			{
				m_CachedFriends.Add(item);
			}
			else
			{
				m_CachedFriends.Remove(item);
			}
		}
	}
}
