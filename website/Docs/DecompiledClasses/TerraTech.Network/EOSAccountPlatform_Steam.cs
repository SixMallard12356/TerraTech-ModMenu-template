#define UNITY_EDITOR
using Epic.OnlineServices;
using Steamworks;
using UnityEngine;

namespace TerraTech.Network;

public class EOSAccountPlatform_Steam : EOSAccountPlatform_Base
{
	private Callback<AvatarImageLoaded_t> m_AvatarImageLoadedSteamCallback;

	private CallResult<LobbyCreated_t> m_LobbyCreationCallResult;

	private CallResult<LobbyEnter_t> m_LobbyJoinCallResult;

	private TTNetworkID m_LobbyBeingCreated_EosLobbyID = TTNetworkID.Invalid;

	private MirrorLobbyCreateCallback m_LobbyCreationCallback;

	private Callback<GameLobbyJoinRequested_t> m_LobbyJoinRequestionCallResult;

	private Callback<LobbyDataUpdate_t> m_LobbyDataUpdate;

	private CSteamID m_LobbyWaitingToJoin = CSteamID.Nil;

	private const string kEosLobbyIDKey = "eosLobbyID";

	public override ExternalAccountType PlatformType => ExternalAccountType.Steam;

	public override bool MirrorEOSLobbyToPlatform => true;

	public override bool SupportsAvatars => true;

	public EOSAccountPlatform_Steam()
	{
		m_AvatarImageLoadedSteamCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
		m_LobbyCreationCallResult = CallResult<LobbyCreated_t>.Create(OnMirrorLobbyCreated);
		m_LobbyJoinCallResult = CallResult<LobbyEnter_t>.Create(OnMirrorLobbyJoined);
		m_LobbyJoinRequestionCallResult = Callback<GameLobbyJoinRequested_t>.Create(OnLobbyJoinRequested);
		m_LobbyDataUpdate = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
	}

	public override void Shutdown()
	{
		m_AvatarImageLoadedSteamCallback.Dispose();
		m_LobbyCreationCallResult.Dispose();
		m_LobbyJoinCallResult.Dispose();
		m_LobbyJoinRequestionCallResult.Dispose();
		m_LobbyDataUpdate.Dispose();
		base.Shutdown();
	}

	public override bool IsFriend(TTNetworkID platformID)
	{
		return SteamFriends.HasFriend(platformID.ToSteamID(), EFriendFlags.k_EFriendFlagImmediate);
	}

	public override int GetLargeFriendAvatarImageID(TTNetworkID platformID)
	{
		PlatformLobby_Steam.TryGetLargeFriendAvatarImageID(platformID, out var imageID);
		return imageID;
	}

	public override Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight)
	{
		PlatformLobby_Steam.TryGetSpriteFromImageID(imageID, imageWidthHeight, out var sprite);
		return sprite;
	}

	public override void OpenFriendInviteScreen(TTNetworkID eosLobbyIDToInviteTo)
	{
		SteamFriends.ActivateGameOverlayInviteDialog(base.CurrentMirrorLobbyPlatformID.ToSteamID());
	}

	private void OnAvatarImageLoaded(AvatarImageLoaded_t pCallback)
	{
		TTNetworkID platformID = pCallback.m_steamID.ToTTID();
		AvatarImageLoadedEvent(platformID, pCallback.m_iImage, pCallback.m_iWide, pCallback.m_iTall);
	}

	public override void CreateMirrorLobby(TTNetworkID eosLobbyID, Lobby.LobbyVisibility visibility, int maxLobbyMembers, MirrorLobbyCreateCallback creationCallback)
	{
		m_LobbyBeingCreated_EosLobbyID = eosLobbyID;
		SteamAPICall_t hAPICall = SteamMatchmaking.CreateLobby((ELobbyType)visibility, maxLobbyMembers);
		m_LobbyCreationCallResult.Set(hAPICall, OnMirrorLobbyCreated);
		m_LobbyCreationCallback = creationCallback;
		SteamFriends.SetRichPresence("status", "Creating a lobby");
	}

	public override void JoinMirrorLobby(TTNetworkID eosLobbyID, TTNetworkID platformLobbyID)
	{
		SteamAPICall_t hAPICall = SteamMatchmaking.JoinLobby(platformLobbyID.ToSteamID());
		m_LobbyJoinCallResult.Set(hAPICall, OnMirrorLobbyJoined);
	}

	public override bool LeaveMirrorLobby(TTNetworkID eosLobbyID, TTNetworkID platformLobbyID, bool persistLobby)
	{
		if (eosLobbyID == m_LobbyBeingCreated_EosLobbyID)
		{
			m_LobbyBeingCreated_EosLobbyID = TTNetworkID.Invalid;
			m_LobbyCreationCallback = null;
			return false;
		}
		bool flag = persistLobby;
		CSteamID steamIDLobby = platformLobbyID.ToSteamID();
		if (persistLobby)
		{
			int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(steamIDLobby);
			flag = numLobbyMembers > 1;
			if (flag)
			{
				CSteamID lobbyOwner = SteamMatchmaking.GetLobbyOwner(steamIDLobby);
				TTNetworkID localPlayerID = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLocalPlayerID();
				if (lobbyOwner.ToTTID() == localPlayerID)
				{
					CSteamID cSteamID = CSteamID.Nil;
					for (int i = 0; i < numLobbyMembers; i++)
					{
						cSteamID = SteamMatchmaking.GetLobbyMemberByIndex(steamIDLobby, i);
						if (cSteamID != lobbyOwner)
						{
							break;
						}
					}
					SteamMatchmaking.SetLobbyOwner(steamIDLobby, cSteamID);
				}
			}
		}
		SteamMatchmaking.LeaveLobby(steamIDLobby);
		base.CurrentMirrorLobbyPlatformID = TTNetworkID.Invalid;
		return flag;
	}

	public override void DestroyMirrorLobby(TTNetworkID eosLobbyID, TTNetworkID platformLobbyID)
	{
		d.Assert(base.CurrentMirrorLobbyPlatformID == TTNetworkID.Invalid, "Destroy mirror lobby called, but cached lobby ID still set as if we were in a lobby?!");
		if (eosLobbyID == m_LobbyBeingCreated_EosLobbyID)
		{
			m_LobbyBeingCreated_EosLobbyID = TTNetworkID.Invalid;
			m_LobbyCreationCallback = null;
		}
	}

	private void OnMirrorLobbyCreated(LobbyCreated_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_eResult == EResult.k_EResultOK && !bIOFailure)
		{
			CSteamID cSteamID = new CSteamID(pCallback.m_ulSteamIDLobby);
			if (m_LobbyBeingCreated_EosLobbyID == TTNetworkID.Invalid)
			{
				d.LogErrorFormat("Steam lobby Mirror was being created, but cached eos LobbyID was not set (was it already destroyed?!) {0}", cSteamID);
				SteamMatchmaking.LeaveLobby(cSteamID);
				base.CurrentMirrorLobbyPlatformID = TTNetworkID.Invalid;
			}
			else
			{
				TTNetworkID lobbyBeingCreated_EosLobbyID = m_LobbyBeingCreated_EosLobbyID;
				SteamMatchmaking.SetLobbyData(cSteamID, "eosLobbyID", lobbyBeingCreated_EosLobbyID.ToString());
				base.CurrentMirrorLobbyPlatformID = cSteamID.ToTTID();
				m_LobbyCreationCallback(createSuccess: true, lobbyBeingCreated_EosLobbyID, cSteamID.ToTTID());
			}
		}
		else
		{
			base.CurrentMirrorLobbyPlatformID = TTNetworkID.Invalid;
			m_LobbyCreationCallback(createSuccess: false, m_LobbyBeingCreated_EosLobbyID, TTNetworkID.Invalid);
		}
		m_LobbyBeingCreated_EosLobbyID = TTNetworkID.Invalid;
	}

	private void OnMirrorLobbyJoined(LobbyEnter_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_EChatRoomEnterResponse != 1)
		{
			base.CurrentMirrorLobbyPlatformID = TTNetworkID.Invalid;
		}
		else
		{
			base.CurrentMirrorLobbyPlatformID = new CSteamID(pCallback.m_ulSteamIDLobby).ToTTID();
		}
	}

	private void OnLobbyJoinRequested(GameLobbyJoinRequested_t pCallback)
	{
		CSteamID cSteamID = new CSteamID(pCallback.m_steamIDLobby.m_SteamID);
		SteamMatchmaking.RequestLobbyData(cSteamID);
		m_LobbyWaitingToJoin = cSteamID;
	}

	private void OnLobbyDataUpdated(LobbyDataUpdate_t pCallback)
	{
		CSteamID cSteamID = new CSteamID(pCallback.m_ulSteamIDLobby);
		if (pCallback.m_bSuccess != 0 && cSteamID == m_LobbyWaitingToJoin)
		{
			string lobbyData = SteamMatchmaking.GetLobbyData(cSteamID, "eosLobbyID");
			TTNetworkID eosLobbyID = new TTNetworkID(lobbyData);
			(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem as PlatformLobbySystem_EOS).TryJoinLobbyFromMirrorInvite(eosLobbyID);
			m_LobbyWaitingToJoin = CSteamID.Nil;
		}
	}
}
