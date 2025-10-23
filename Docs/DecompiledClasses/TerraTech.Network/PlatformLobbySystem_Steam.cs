#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class PlatformLobbySystem_Steam : LobbySystem
{
	private class LobbyJoinRequest
	{
		public TTNetworkID m_LobbyID;

		public bool m_FromInvite;

		public LobbyJoinRequest(TTNetworkID lobbyID, bool fromInvite)
		{
			m_LobbyID = lobbyID;
			m_FromInvite = fromInvite;
		}
	}

	private struct CloseP2PData
	{
		public CSteamID m_SteamID;

		public float m_Delay;
	}

	private Callback<LobbyChatUpdate_t> m_OnLobbyChatUpdatedCallback;

	private Callback<LobbyDataUpdate_t> m_OnLobbyDataUpdatedCallback;

	private Callback<GameLobbyJoinRequested_t> m_LobbyJoinRequestionCallResult;

	private CallResult<LobbyCreated_t> m_LobbyCreationCallResult;

	private CallResult<LobbyMatchList_t> m_LobbyMatchListCallResult;

	private CallResult<LobbyEnter_t> m_LobbyJoinCallResult;

	private TTNetworkID m_LocalPlayerNetworkID;

	private const byte PING_ENQ = 5;

	private const byte PING_ACK = 6;

	private byte[] m_PingBuffer = new byte[1];

	private bool m_Inited;

	private ConnectionConfig m_ConnectionConfig;

	private int m_ChannelReliable;

	private int m_ChannelUnreliable;

	private int m_ChannelPing;

	private LobbyJoinRequest m_PendingJoinRequest;

	private ClientInviteStatus m_ClientInviteStatus = new ClientInviteStatus();

	private List<TTNetworkID> m_QueuedLobbyUpdates = new List<TTNetworkID>();

	private List<CloseP2PData> m_CloseP2PData = new List<CloseP2PData>();

	private PlatformLobby_Steam CurrentLobby_Platform => base.CurrentLobby as PlatformLobby_Steam;

	public void CloseP2PSessionAfterDelay(CSteamID steamID)
	{
		m_CloseP2PData.Add(new CloseP2PData
		{
			m_SteamID = steamID,
			m_Delay = 2f
		});
	}

	protected override Lobby Platform_CreateLobbyObject(LobbyData data, MultiplayerModeType gameType)
	{
		if (!SKU.IsSteam || !Singleton.Manager<ManSteamworks>.inst.Inited)
		{
			return null;
		}
		return new PlatformLobby_Steam(this, data, m_ConnectionConfig, gameType);
	}

	public override bool Init(LobbyConstants constants, GameStateQuerier gsq)
	{
		base.Init(constants, gsq);
		d.Assert(!SKU.IsSteam || Singleton.Manager<ManSteamworks>.inst.Inited, "[PlatformLobbySystem_Steam] Can't initialise lobby system before ManSteamworks");
		if (!Singleton.Manager<ManSteamworks>.inst.Inited || !SKU.IsSteam)
		{
			return false;
		}
		m_LobbyCreationCallResult = CallResult<LobbyCreated_t>.Create(OnLobbyCreated);
		m_LobbyMatchListCallResult = CallResult<LobbyMatchList_t>.Create(OnLobbyMatchList);
		m_LobbyJoinCallResult = CallResult<LobbyEnter_t>.Create(OnLobbyEntered);
		m_OnLobbyDataUpdatedCallback = Callback<LobbyDataUpdate_t>.Create(OnLobbyDataUpdated);
		m_OnLobbyChatUpdatedCallback = Callback<LobbyChatUpdate_t>.Create(OnLobbyChatUpdated);
		m_LobbyJoinRequestionCallResult = Callback<GameLobbyJoinRequested_t>.Create(OnLobbyJoinRequested);
		m_LocalPlayerNetworkID = SteamUser.GetSteamID().ToTTID();
		m_Inited = true;
		m_ConnectionConfig = new ConnectionConfig();
		m_ConnectionConfig.PacketSize = 1200;
		m_ChannelReliable = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_ChannelUnreliable = m_ConnectionConfig.AddChannel(QosType.Unreliable);
		m_ChannelPing = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		return true;
	}

	public override bool IsJoinOrCreateLobbyRequestActive()
	{
		if (!m_LobbyJoinCallResult.IsActive())
		{
			return m_LobbyCreationCallResult.IsActive();
		}
		return true;
	}

	protected override void Platform_CreateLobby(MultiplayerModeType gameType, Lobby.LobbyVisibility visibility, int maxPlayerCount)
	{
		if (!m_LobbyCreationCallResult.IsActive())
		{
			SteamAPICall_t hAPICall = SteamMatchmaking.CreateLobby((ELobbyType)visibility, maxPlayerCount);
			m_LobbyCreationCallResult.Set(hAPICall, OnLobbyCreated);
			SteamFriends.SetRichPresence("status", "Creating a lobby");
		}
	}

	protected override void Platform_JoinLobby(TTNetworkID lobbyID, bool fromInvite)
	{
		if (!m_LobbyJoinCallResult.IsActive())
		{
			SteamAPICall_t hAPICall = SteamMatchmaking.JoinLobby(lobbyID.ToSteamID());
			m_LobbyJoinCallResult.Set(hAPICall, OnLobbyEntered);
		}
	}

	protected override void Platform_LeaveLobby(TTNetworkID lobbyID)
	{
		if (m_Inited)
		{
			SteamMatchmaking.LeaveLobby(lobbyID.ToSteamID());
		}
	}

	protected override void Platform_RequestLobbyData(TTNetworkID lobbyID)
	{
		m_QueuedLobbyUpdates.Add(lobbyID);
		SteamMatchmaking.RequestLobbyData(lobbyID.ToSteamID());
	}

	public override string GetLobbyData(TTNetworkID lobbyID, string keyName)
	{
		return SteamMatchmaking.GetLobbyData(lobbyID.ToSteamID(), keyName);
	}

	private bool TryGetUserName(TTNetworkID playerID, out string userName)
	{
		CSteamID steamIDFriend = playerID.ToSteamID();
		userName = SteamFriends.GetFriendPersonaName(steamIDFriend);
		if (!userName.NullOrEmpty())
		{
			return userName != "[unknown]";
		}
		return false;
	}

	public override string GetUserName(TTNetworkID playerID)
	{
		d.AssertFormat(TryGetUserName(playerID, out var userName), "Username for SteamID {0} was not yet known to the system. Use Platform_GetUserName to retrieve it asynchronously", playerID);
		return userName;
	}

	public override void Platform_GetUserName(TTNetworkID playerID, Action<TTNetworkID, string> onUsernameRetrieved)
	{
		if (TryGetUserName(playerID, out var userName))
		{
			onUsernameRetrieved(playerID, userName);
			return;
		}
		CSteamID steamID = playerID.ToSteamID();
		Singleton.Manager<ManSteamworks>.inst.RequestPersonaName(steamID, delegate(CSteamID sid, string name)
		{
			onUsernameRetrieved(playerID, name);
		});
	}

	public override int GetLobbyNumFriends(TTNetworkID lobbyID)
	{
		return 0;
	}

	public override bool HasFriend(TTNetworkID playerID)
	{
		return SteamFriends.HasFriend(playerID.ToSteamID(), EFriendFlags.k_EFriendFlagImmediate);
	}

	public override void Update()
	{
		UpdateInternal();
		for (int num = m_CloseP2PData.Count - 1; num >= 0; num--)
		{
			CloseP2PData value = m_CloseP2PData[num];
			float num2 = value.m_Delay - Time.deltaTime;
			if (num2 <= 0f)
			{
				d.LogFormat("[PlatformLobby_Steam] Closing P2P session with user {0}", value.m_SteamID);
				SteamNetworking.CloseP2PSessionWithUser(value.m_SteamID);
				m_CloseP2PData.RemoveAt(num);
			}
			else
			{
				value.m_Delay = num2;
				m_CloseP2PData[num] = value;
			}
		}
		base.Update();
	}

	public override int GetNumLobbyMembers(TTNetworkID lobbyID)
	{
		return SteamMatchmaking.GetNumLobbyMembers(lobbyID.ToSteamID());
	}

	public override int GetLobbyMemberLimit(TTNetworkID lobbyID)
	{
		return SteamMatchmaking.GetLobbyMemberLimit(lobbyID.ToSteamID());
	}

	public override TTNetworkID GetLocalPlayerID()
	{
		return m_LocalPlayerNetworkID;
	}

	public override TTNetworkID GetLobbyMemberByIndex(TTNetworkID lobbyID, int idx)
	{
		return SteamMatchmaking.GetLobbyMemberByIndex(lobbyID.ToSteamID(), idx).ToTTID();
	}

	public override void SendInvites()
	{
	}

	public override List<TTNetworkID> GetPlayersThatAreTalking()
	{
		return new List<TTNetworkID>();
	}

	public override void GlobalMuteAll(bool mute)
	{
	}

	public override void MuteNetworkPlayer(TTNetworkID playerID, bool mute)
	{
	}

	protected override void Platform_SendPingRequest(TTNetworkID hostID)
	{
		CSteamID steamIDRemote = hostID.ToSteamID();
		m_PingBuffer[0] = 5;
		SteamNetworking.SendP2PPacket(steamIDRemote, m_PingBuffer, 1u, EP2PSend.k_EP2PSendReliable, m_ChannelPing);
	}

	protected override void Platform_RefreshLobbyList(LobbyFilterOptions filterOptions)
	{
		if (m_Inited)
		{
			if (filterOptions.m_FriendsGamesOnly == 1)
			{
				HandleLobbyMatchListBegin();
				AddFriendLobbies();
				HandleLobbyMatchListEnd();
				return;
			}
			if (filterOptions.m_ShowNearbyGamesOnly != 0)
			{
				SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterDefault);
			}
			else
			{
				SteamMatchmaking.AddRequestLobbyListDistanceFilter(ELobbyDistanceFilter.k_ELobbyDistanceFilterFar);
			}
			SteamAPICall_t hAPICall = SteamMatchmaking.RequestLobbyList();
			m_LobbyMatchListCallResult.Set(hAPICall, OnLobbyMatchList);
		}
		else
		{
			HandleLobbyMatchListFailed();
		}
	}

	public override void OpenFriendInviteScreen()
	{
		SteamFriends.ActivateGameOverlayInviteDialog(base.CurrentLobby.ID.ToSteamID());
	}

	public override bool SupportsVisibilityType(Lobby.LobbyVisibility visibilityType)
	{
		return visibilityType != Lobby.LobbyVisibility.FriendsOnly;
	}

	public override void JoinLobbyAfterUpdate(TTNetworkID lobbyID)
	{
		SetupLobbyData(lobbyID, requestRefreshedData: true);
		m_PendingJoinRequest = new LobbyJoinRequest(lobbyID, fromInvite: false);
	}

	private void UpdateInternal()
	{
		if (!m_Inited)
		{
			return;
		}
		if (m_PendingJoinRequest != null && !m_QueuedLobbyUpdates.Contains(m_PendingJoinRequest.m_LobbyID))
		{
			LobbyData lobby = GetLobby(m_PendingJoinRequest.m_LobbyID);
			string lobbyData = GetLobbyData(m_PendingJoinRequest.m_LobbyID, "protocolVersion");
			if (lobby != null && lobbyData != "")
			{
				m_ClientInviteStatus.readyToJoin = false;
				ClientInvitationEvent.Send(m_ClientInviteStatus);
				if (m_ClientInviteStatus.readyToJoin)
				{
					if (lobby.NumMods > 0)
					{
						TTNetworkID lobbyID = m_PendingJoinRequest.m_LobbyID;
						bool fromInvite = m_PendingJoinRequest.m_FromInvite;
						(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuJoinMultiplayer, 7), delegate
						{
							Singleton.Manager<ManUI>.inst.PopScreen();
							JoinLobby(lobbyID, fromInvite);
						}, delegate
						{
							Singleton.Manager<ManUI>.inst.PopScreen();
						}, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4), Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 5));
						Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
					}
					else
					{
						JoinLobby(m_PendingJoinRequest.m_LobbyID, m_PendingJoinRequest.m_FromInvite);
					}
					m_PendingJoinRequest = null;
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.F5))
		{
			TTNetworkID tTnetworkID = TTNetworkID.Invalid;
			if (base.CurrentLobby != null)
			{
				tTnetworkID = base.CurrentLobby.ID;
			}
			SteamFriends.ActivateGameOverlayInviteDialog(tTnetworkID.ToSteamID());
		}
		if (base.IsMultiplayer)
		{
			ReadAllNetworkData();
		}
		else
		{
			ReadNetworkData(m_ChannelPing);
		}
	}

	private int GetFriendCount()
	{
		return SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
	}

	private CSteamID GetLobbyIDForFriendPlayingOurGameByIndex(int friendIdx)
	{
		CSteamID result = CSteamID.Nil;
		CSteamID friendByIndex = SteamFriends.GetFriendByIndex(friendIdx, EFriendFlags.k_EFriendFlagImmediate);
		if (friendByIndex.IsValid() && SteamFriends.GetFriendGamePlayed(friendByIndex, out var pFriendGameInfo) && pFriendGameInfo.m_gameID == Singleton.Manager<ManSteamworks>.inst.GameID && pFriendGameInfo.m_steamIDLobby.IsValid())
		{
			result = pFriendGameInfo.m_steamIDLobby;
		}
		return result;
	}

	private void AddFriendLobbies()
	{
		int friendCount = GetFriendCount();
		for (int i = 0; i < friendCount; i++)
		{
			CSteamID lobbyIDForFriendPlayingOurGameByIndex = GetLobbyIDForFriendPlayingOurGameByIndex(i);
			if (lobbyIDForFriendPlayingOurGameByIndex.IsValid())
			{
				TTNetworkID lobbyID = lobbyIDForFriendPlayingOurGameByIndex.ToTTID();
				HandleLobbyMatchListAddLobby(lobbyID, refreshData: true);
			}
		}
	}

	public void ReadAllNetworkData()
	{
		ReadNetworkData(m_ChannelReliable);
		ReadNetworkData(m_ChannelUnreliable);
		ReadNetworkData(m_ChannelPing);
	}

	private void ReadNetworkData(int channelId)
	{
		uint pcubMsgSize;
		while (SteamNetworking.IsP2PPacketAvailable(out pcubMsgSize, channelId))
		{
			byte[] array = new byte[pcubMsgSize];
			if (!SteamNetworking.ReadP2PPacket(array, pcubMsgSize, out pcubMsgSize, out var psteamIDRemote, channelId))
			{
				continue;
			}
			TTNetworkID remoteHostID = psteamIDRemote.ToTTID();
			if (channelId == m_ChannelPing)
			{
				if (pcubMsgSize == 0)
				{
					continue;
				}
				if (array[0] == 5)
				{
					m_PingBuffer[0] = 6;
					if (SteamNetworking.SendP2PPacket(psteamIDRemote, m_PingBuffer, 1u, EP2PSend.k_EP2PSendReliable, channelId))
					{
					}
				}
				else if (array[0] == 6)
				{
					HandlePingResponse(remoteHostID);
				}
			}
			else if (base.CurrentLobby != null)
			{
				CurrentLobby_Platform.ReadNetworkData(channelId, pcubMsgSize, array, psteamIDRemote);
			}
		}
	}

	private LobbyErrorCode ConvertErrorCode(string errorCode)
	{
		errorCode = errorCode.ToLower();
		if (errorCode.Contains("exist"))
		{
			return LobbyErrorCode.DoesntExist;
		}
		if (errorCode.Contains("notallowed"))
		{
			return LobbyErrorCode.NotAllowed;
		}
		if (errorCode.Contains("full"))
		{
			return LobbyErrorCode.LobbyFull;
		}
		if (errorCode.Contains("error"))
		{
			return LobbyErrorCode.Error;
		}
		if (errorCode.Contains("banned"))
		{
			return LobbyErrorCode.Banned;
		}
		if (errorCode.Contains("limited"))
		{
			return LobbyErrorCode.Limited;
		}
		if (errorCode.Contains("blockedyou"))
		{
			return LobbyErrorCode.MemberBlockedYou;
		}
		if (errorCode.Contains("blockedmember"))
		{
			return LobbyErrorCode.YouBlockedMember;
		}
		return LobbyErrorCode.FailedToConnect;
	}

	private void OnLobbyCreated(LobbyCreated_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_eResult == EResult.k_EResultOK && !bIOFailure)
		{
			TTNetworkID lobbyID = new CSteamID(pCallback.m_ulSteamIDLobby).ToTTID();
			Singleton.Manager<ManMods>.inst.PreLobbyCreated();
			HandleLobbyCreationSuccess(lobbyID);
		}
		else
		{
			HandleLobbyCreationFailure(pCallback.m_eResult.ToString(), ConvertErrorCode(pCallback.m_eResult.ToString()));
		}
	}

	private void OnLobbyMatchList(LobbyMatchList_t pCallback, bool bIOFailure)
	{
		HandleLobbyMatchListBegin();
		for (int i = 0; i < pCallback.m_nLobbiesMatching; i++)
		{
			TTNetworkID lobbyID = SteamMatchmaking.GetLobbyByIndex(i).ToTTID();
			HandleLobbyMatchListAddLobby(lobbyID, refreshData: true);
		}
		AddFriendLobbies();
		HandleLobbyMatchListEnd();
	}

	private void OnLobbyDataUpdated(LobbyDataUpdate_t pCallback)
	{
		TTNetworkID tTNetworkID = new CSteamID(pCallback.m_ulSteamIDLobby).ToTTID();
		m_QueuedLobbyUpdates.Remove(tTNetworkID);
		HandleLobbyDataUpdated(tTNetworkID, (pCallback.m_bSuccess != 0) ? true : false);
		if (pCallback.m_bSuccess == 0 && m_PendingJoinRequest != null && m_PendingJoinRequest.m_LobbyID == tTNetworkID)
		{
			ClientInvitationEvent.Send(m_ClientInviteStatus);
			LobbyJoinFailedEvent.Send(LobbyErrorCode.DoesntExist);
			m_PendingJoinRequest = null;
		}
	}

	private void OnLobbyChatUpdated(LobbyChatUpdate_t pCallback)
	{
		TTNetworkID lobbyID = new CSteamID(pCallback.m_ulSteamIDLobby).ToTTID();
		TTNetworkID changedPlayerID = new CSteamID(pCallback.m_ulSteamIDUserChanged).ToTTID();
		HandleLobbyStateUpdated(lobbyID, changedPlayerID, ConvertSteamMemberStateMask((EChatMemberStateChange)pCallback.m_rgfChatMemberStateChange));
	}

	private void OnLobbyJoinRequested(GameLobbyJoinRequested_t pCallback)
	{
		TTNetworkID lobbyID = new CSteamID(pCallback.m_steamIDLobby.m_SteamID).ToTTID();
		m_PendingJoinRequest = new LobbyJoinRequest(lobbyID, fromInvite: true);
		AddLobbyToList(lobbyID, refreshData: true, fromInvite: true);
	}

	private Lobby.MemberLobbyStateMask ConvertSteamMemberStateMask(EChatMemberStateChange m)
	{
		Lobby.MemberLobbyStateMask memberLobbyStateMask = (Lobby.MemberLobbyStateMask)0;
		if ((m & EChatMemberStateChange.k_EChatMemberStateChangeBanned) != 0)
		{
			memberLobbyStateMask |= Lobby.MemberLobbyStateMask.MLS_Banned;
		}
		if ((m & EChatMemberStateChange.k_EChatMemberStateChangeEntered) != 0)
		{
			memberLobbyStateMask |= Lobby.MemberLobbyStateMask.MLS_Entered;
		}
		if ((m & EChatMemberStateChange.k_EChatMemberStateChangeDisconnected) != 0)
		{
			memberLobbyStateMask |= Lobby.MemberLobbyStateMask.MLS_Disconnected;
		}
		if ((m & EChatMemberStateChange.k_EChatMemberStateChangeKicked) != 0)
		{
			memberLobbyStateMask |= Lobby.MemberLobbyStateMask.MLS_Kicked;
		}
		if ((m & EChatMemberStateChange.k_EChatMemberStateChangeLeft) != 0)
		{
			memberLobbyStateMask |= Lobby.MemberLobbyStateMask.MLS_Left;
		}
		return memberLobbyStateMask;
	}

	private void OnLobbyEntered(LobbyEnter_t pCallback, bool bIOFailure)
	{
		if (pCallback.m_EChatRoomEnterResponse == 1)
		{
			TTNetworkID lobbyID = new CSteamID(pCallback.m_ulSteamIDLobby).ToTTID();
			HandleLobbyJoinSuccess(lobbyID);
		}
		else
		{
			string errCode = pCallback.m_EChatRoomEnterResponse.ToString();
			EChatRoomEnterResponse eChatRoomEnterResponse = (EChatRoomEnterResponse)pCallback.m_EChatRoomEnterResponse;
			HandleLobbyJoinFailure(errCode, ConvertErrorCode(eChatRoomEnterResponse.ToString()));
		}
	}
}
