using System;
using Epic.OnlineServices;
using Epic.OnlineServices.UI;
using PlayEveryWare.EpicOnlineServices;
using UnityEngine;

namespace TerraTech.Network;

public class EOSAccountPlatform_Epic : EOSAccountPlatform_Base
{
	private FriendsManager m_FriendsManager;

	public override ExternalAccountType PlatformType => ExternalAccountType.Epic;

	public override bool MirrorEOSLobbyToPlatform => false;

	public override bool SupportsAvatars => false;

	public EOSAccountPlatform_Epic()
	{
		m_FriendsManager = new FriendsManager();
	}

	public override void Shutdown()
	{
		m_FriendsManager.Cleanup();
		base.Shutdown();
	}

	public override bool IsFriend(TTNetworkID platformID)
	{
		return m_FriendsManager.IsFriend(platformID);
	}

	public override int GetLargeFriendAvatarImageID(TTNetworkID platformID)
	{
		throw new NotImplementedException();
	}

	public override Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight)
	{
		throw new NotImplementedException();
	}

	public override void OpenFriendInviteScreen(TTNetworkID lobbyIDToInviteTo)
	{
		ShowFriendsOptions options = new ShowFriendsOptions
		{
			LocalUserId = EOSManager.Instance.GetLocalUserId()
		};
		EOSManager.Instance.GetEOSUIInterface().ShowFriends(ref options, null, null);
	}
}
