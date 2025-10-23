#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Net;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public abstract class LobbySystem : MonoBehaviour
{
	public interface GameStateQuerier
	{
		bool IsInProgress();

		bool AlreadyRunning();

		bool IsNetControllerNull();

		bool IsTerrainGenerated();

		bool IsInactive();

		bool IsClientWaitingToJoin();

		bool AllowedToTalk();

		bool AllowedToListen();
	}

	public class HostInviteStatus
	{
		public bool readyToSendInvites;
	}

	public class ClientInviteStatus
	{
		public bool readyToJoin;
	}

	public enum LobbyErrorCode
	{
		None = -1,
		Cancelled,
		FailedToConnect,
		LostConnection,
		LobbyFull,
		DoesntExist,
		NotAllowed,
		Error,
		Banned,
		Limited,
		MemberBlockedYou,
		YouBlockedMember,
		HostDisconnected,
		ModsNotSupported
	}

	public class LobbyFilterOptions
	{
		public int m_GameModeIndex = -1;

		public int m_Team;

		public int m_HideFullGames;

		public int m_FriendsGamesOnly;

		public int m_ShowNearbyGamesOnly;

		public int m_LanguageIndex = 1;

		public int m_PingMaxRequirementIndex;

		public int m_ShowGamesInProgress = 1;

		public int m_ShowModdedGames = 1;

		public LobbyFilterOptions()
		{
			m_GameModeIndex = -1;
			m_Team = 0;
			m_HideFullGames = 0;
			m_FriendsGamesOnly = 0;
			m_ShowNearbyGamesOnly = 0;
			m_LanguageIndex = 1;
			m_PingMaxRequirementIndex = 0;
			m_ShowGamesInProgress = 1;
			m_ShowModdedGames = 1;
		}

		public LobbyFilterOptions(LobbyFilterOptions copyMe)
		{
			m_GameModeIndex = copyMe.m_GameModeIndex;
			m_Team = copyMe.m_Team;
			m_HideFullGames = copyMe.m_HideFullGames;
			m_FriendsGamesOnly = copyMe.m_FriendsGamesOnly;
			m_ShowNearbyGamesOnly = copyMe.m_ShowNearbyGamesOnly;
			m_LanguageIndex = copyMe.m_LanguageIndex;
			m_PingMaxRequirementIndex = copyMe.m_PingMaxRequirementIndex;
			m_ShowGamesInProgress = copyMe.m_ShowGamesInProgress;
			m_ShowModdedGames = copyMe.m_ShowModdedGames;
		}

		public bool isLobbyAcceptable(LobbyData ld, TTNetworkID playerID)
		{
			if (ld.m_Visibility == Lobby.LobbyVisibility.Private)
			{
				return false;
			}
			MultiplayerModeType multiplayerModeType = (MultiplayerModeType)ld.m_Choices[0];
			if (m_GameModeIndex != -1 && multiplayerModeType != (MultiplayerModeType)m_GameModeIndex)
			{
				return false;
			}
			if (m_Team != 0 && multiplayerModeType == MultiplayerModeType.Deathmatch)
			{
				int num = ld.m_Choices[14];
				if (m_Team - 1 != num)
				{
					return false;
				}
			}
			if (m_HideFullGames != 0 && ld.m_NumUsers >= ld.m_MaxUserLimit)
			{
				return false;
			}
			if (m_LanguageIndex == 0 && StringLookup.GetLocalisedLanguageName(Singleton.Manager<Localisation>.inst.CurrentLanguage) != ld.m_Language)
			{
				return false;
			}
			if (m_ShowGamesInProgress == 0 && ld.m_GameInProgress)
			{
				return false;
			}
			if (m_PingMaxRequirementIndex != 0)
			{
				int num2 = PingMaxRequirements[m_PingMaxRequirementIndex];
				if (ld.m_PingTimeMS <= 0 || ld.m_PingTimeMS > num2)
				{
					return false;
				}
			}
			_ = m_ShowModdedGames;
			PersistentPlayerID persistentPlayerID = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetPersistentPlayerID(playerID);
			if (ld.m_BannedPlayers.Contains(persistentPlayerID))
			{
				return false;
			}
			return true;
		}
	}

	private class PingRequestData
	{
		public TTNetworkID m_HostID;

		public float m_AveragePingMS = -1f;

		public float m_TotalPingMS;

		public int m_PingCount;

		public float m_CurrPingStartTime;

		public float m_LastPingRequestTime;

		public bool m_IsFirstPing = true;
	}

	public const int k_MaxCoopCreativePlayerCount = 4;

	public const int k_MaxCoopCampaignPlayerCount = 4;

	public const int k_MaxDeathmatchPlayerCount = 16;

	public const int k_MaxPlayerCount = 16;

	private Lobby m_CurrentLobby;

	private bool m_RequestingLobbies;

	private List<LobbyData> m_LobbyList = new List<LobbyData>(8);

	private LobbyFilterOptions m_TempFilterOptions;

	private List<PingRequestData> m_PingRequests = new List<PingRequestData>(16);

	private LobbyConstants m_Constants;

	private bool m_JoypadDisconnected;

	private GameStateQuerier m_GameStateQuerier;

	private MultiplayerModeType m_GameTypeToSetAfterInit;

	private Lobby.LobbyVisibility m_LobbyVisibilityToSetAfterInit;

	private bool m_SetupLobbyDelayed;

	private bool m_ShouldAbortLobbyScreen;

	private bool m_PendingLeave;

	protected TTNetworkID m_JoinInviteLobbyId = TTNetworkID.Invalid;

	public const string kLobbyKeyName = "name";

	public const string kLobbyKeyProtocolVersion = "protocolVersion";

	public const string kLobbyKeyOwnerID = "ownerID";

	public const string kLobbyKeyUsedColours = "usedColours";

	public const string kLobbyKeyPlayerTeams = "playerTeams";

	public const string kLobbyChoices = "choices";

	public const string kLobbyKeyLanguage = "language";

	public const string kLobbyKeyGameInProgress = "gameInProgress";

	public const string kLobbyKeyGameMode = "gameModeIndex";

	public const string kFriendsOnly = "friendsOnly";

	public const string kNumPlayers = "numPlayers";

	public const string kLobbyVisibility = "lobbyPublic";

	public const string kMaxPlayers = "maxPlayers";

	public const string kWorkshopIds = "workshopIds";

	public const string kLobbyBannedPlayers = "bannedPlayers";

	public const string kLobbyHasActiveMods = "hasMods";

	public const string kGameTypeAny = "*";

	public static int[] PingMaxRequirements = new int[6] { 999999999, 1000, 500, 250, 100, 50 };

	public Event<Lobby> LobbyJoinedEvent;

	public Event<LobbyErrorCode> LobbyCreateFailedEvent;

	public Event<LobbyErrorCode> LobbyJoinFailedEvent;

	public Event<LobbyErrorCode> LobbyErrorEvent;

	public Event<Lobby> EventTriggerPreGameStart;

	public Event<Lobby> EventTriggerGameStart;

	public Event<TTNetworkID, string, bool> LobbyKickedEvent;

	public Event<List<LobbyData>, bool> LobbyListUpdatedEvent;

	public Event<HostInviteStatus> HostInvitationsEvent;

	public Event<ClientInviteStatus> ClientInvitationEvent;

	public Event<Lobby> CurrentLobbyUpdatedEvent;

	public Event<LobbyPlayerData, uint, int, string> ChatMessageEvent;

	public Event<Lobby> BecameOwnerEvent;

	public Event<Lobby> JIPEvent;

	public Event<Lobby> PreJIPEvent;

	public Event<Lobby> PreJoinEvent;

	public Event<LobbyPlayerData> SendPlayerDataToGameEvent;

	public Event<Lobby> SendLobbyDataToGameEvent;

	public EventNoParams AllClientsConnectedEvent;

	public Event<TTNetworkConnection> StartAsClientEvent;

	public Event<TTNetworkConnection, LobbyPlayerData> StorePlayerConfigEvent;

	public Event<TTNetworkConnection> ForceClientDisconnectionEvent;

	public Event<EndPoint> SetSecureTunnelEndpointEvent;

	public static int PROTOCOL_VERSION
	{
		get
		{
			int versionInt;
			if (SKU.ChangelistVersion.Length > 0)
			{
				if (!SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out versionInt))
				{
					d.LogError("Failed to parse changelist version for lobby system timestamp!!");
				}
			}
			else
			{
				versionInt = 63232;
			}
			return versionInt ^ GetInstalledModsHash();
		}
	}

	public bool Inited => m_Constants != null;

	public Lobby CurrentLobby => m_CurrentLobby;

	public GameStateQuerier QueryGameState => m_GameStateQuerier;

	public TTNetworkID LocalPlayerNetworkID => GetLocalPlayerID();

	public Color32[] DeathmatchColours => m_Constants.m_AllColours;

	public Color32[] CoOpColours => m_Constants.m_CoOpColours;

	public Color32[] CoOpChatColours => m_Constants.m_CoOpTextColours;

	public Color32[] UnpilotedTechColours => m_Constants.m_UnpilotedTechColours;

	public NetOptionsAsset[] AvailableGameTypes => m_Constants.m_AvailableGameTypes;

	public NetOptionsAsset TeamDeathMatchGameType => m_Constants.m_TeamDeathMatchGameType;

	public virtual bool IsBusyRequestingLobbies => m_RequestingLobbies;

	public bool JoypadDisconnected => m_JoypadDisconnected;

	public bool IsMultiplayer => NetworkClient.active;

	public bool IsServer => NetworkServer.active;

	public bool IsLobbySetupDelayed => m_SetupLobbyDelayed;

	public bool ShouldAbortLobbyScreen
	{
		get
		{
			return m_ShouldAbortLobbyScreen;
		}
		set
		{
			m_ShouldAbortLobbyScreen = value;
		}
	}

	public BanList BanList { get; private set; } = new BanList();

	public RecentPlayers RecentPlayers { get; private set; }

	public static LobbySystem CreateLobbySystem(GameObject parent)
	{
		if (SKU.IsLAN_MP)
		{
			return parent.AddComponent<PlatformLobbySystem_LAN>();
		}
		if (SKU.UsesEOS)
		{
			return parent.AddComponent<PlatformLobbySystem_EOS>();
		}
		if (SKU.IsSteam)
		{
			return parent.AddComponent<PlatformLobbySystem_Steam>();
		}
		if (SKU.IsNetEase)
		{
			return parent.AddComponent<PlatformLobbySystem_NetEase>();
		}
		d.LogError("Unsupported Platform Lobby Type for SKU!");
		return null;
	}

	public static int GetInstalledModsHash()
	{
		return 0;
	}

	public List<LobbyData>.Enumerator GetEnumerator()
	{
		return m_LobbyList.GetEnumerator();
	}

	protected abstract Lobby Platform_CreateLobbyObject(LobbyData data, MultiplayerModeType gameType);

	public abstract bool IsJoinOrCreateLobbyRequestActive();

	public abstract void JoinLobbyAfterUpdate(TTNetworkID lobbyID);

	protected abstract void Platform_CreateLobby(MultiplayerModeType gameType, Lobby.LobbyVisibility visibility, int maxPlayerCount);

	protected abstract void Platform_JoinLobby(TTNetworkID lobbyID, bool fromInvite);

	protected abstract void Platform_LeaveLobby(TTNetworkID lobbyID);

	public abstract string GetUserName(TTNetworkID playerID);

	public abstract void Platform_GetUserName(TTNetworkID playerID, Action<TTNetworkID, string> onUsernameRetrieved);

	public abstract bool HasFriend(TTNetworkID playerID);

	public abstract TTNetworkID GetLocalPlayerID();

	protected abstract void Platform_RequestLobbyData(TTNetworkID lobbyID);

	public abstract string GetLobbyData(TTNetworkID lobbyID, string keyName);

	public abstract int GetNumLobbyMembers(TTNetworkID lobbyID);

	public abstract int GetLobbyMemberLimit(TTNetworkID lobbyID);

	public abstract int GetLobbyNumFriends(TTNetworkID lobbyID);

	public abstract TTNetworkID GetLobbyMemberByIndex(TTNetworkID lobbyID, int idx);

	public abstract void SendInvites();

	public abstract List<TTNetworkID> GetPlayersThatAreTalking();

	public abstract void MuteNetworkPlayer(TTNetworkID playerID, bool mute);

	public abstract void GlobalMuteAll(bool mute);

	public virtual bool Platform_SupportsVoiceChat()
	{
		return true;
	}

	protected abstract void Platform_SendPingRequest(TTNetworkID hostID);

	protected virtual bool Platform_GetLobbyPingTime(TTNetworkID hostID, ref int pingMs)
	{
		return false;
	}

	protected abstract void Platform_RefreshLobbyList(LobbyFilterOptions filterOptions);

	protected virtual void Platform_StopListingLobbies()
	{
	}

	public abstract void OpenFriendInviteScreen();

	public virtual bool SupportsOpenFriendInviteScreen()
	{
		return true;
	}

	public abstract bool SupportsVisibilityType(Lobby.LobbyVisibility visibilityType);

	public virtual PersistentPlayerID GetPersistentPlayerID(TTNetworkID playerID)
	{
		return new PersistentPlayerID(playerID);
	}

	public virtual TTNetworkID GetTTNetworkIDFromPersistent(PersistentPlayerID persistentID)
	{
		return (TTNetworkID)persistentID;
	}

	public void SendEventTriggerPreGameStart(Lobby lobby)
	{
		EventTriggerPreGameStart.Send(lobby);
	}

	public void SendEventTriggerGameStart(Lobby lobby)
	{
		EventTriggerGameStart.Send(lobby);
	}

	public void SendEventLobbyKicked(TTNetworkID playerID, string playerName, bool isUser)
	{
		LobbyKickedEvent.Send(playerID, playerName, isUser);
	}

	public void SendEventJIP(Lobby lobby)
	{
		JIPEvent.Send(lobby);
	}

	public void SendEventPreJIP(Lobby lobby)
	{
		PreJIPEvent.Send(lobby);
	}

	public void SendEventLobbyDataToGame(Lobby data)
	{
		SendLobbyDataToGameEvent.Send(data);
	}

	public void SendEventAllClientsConnected()
	{
		AllClientsConnectedEvent.Send();
	}

	public void SendEventStartAsClient(TTNetworkConnection conn)
	{
		StartAsClientEvent.Send(conn);
	}

	public void SendEventStorePlayerConfig(TTNetworkConnection conn, LobbyPlayerData data)
	{
		StorePlayerConfigEvent.Send(conn, data);
	}

	public virtual bool Init(LobbyConstants constants, GameStateQuerier gameStateQuerier)
	{
		d.Assert(constants != null, "[LobbySystem] No constants passed to LobbySystem.Init");
		d.Assert(m_Constants == null, "[LobbySystem] Init called more than once");
		m_Constants = constants;
		m_GameStateQuerier = gameStateQuerier;
		BanList.Load();
		RecentPlayers = new RecentPlayers(this);
		return true;
	}

	public virtual void Shutdown()
	{
	}

	public void HandleLobbyDataUpdated(TTNetworkID lobbyID, bool wasSuccessful)
	{
		if (((SKU.IsSteam && !SKU.UsesEOS) || SKU.XboxOneUI) && m_JoinInviteLobbyId == lobbyID)
		{
			m_JoinInviteLobbyId = TTNetworkID.Invalid;
			JoinLobby(lobbyID, fromInvite: true, delayedRequestRejoin: true);
		}
		else
		{
			if (CurrentLobby == null || !(CurrentLobby.ID == lobbyID))
			{
				return;
			}
			if (wasSuccessful)
			{
				CurrentLobby.HandleLobbyDataUpdated(wasSuccessful);
				if (m_SetupLobbyDelayed)
				{
					SetupCurrentLobbyJoinSuccess();
				}
			}
			else
			{
				LeaveLobby();
				LobbyErrorEvent.Send(LobbyErrorCode.LostConnection);
			}
			m_SetupLobbyDelayed = false;
		}
	}

	public void HandleLobbyStateUpdated(TTNetworkID lobbyID, TTNetworkID changedPlayerID, Lobby.MemberLobbyStateMask msk)
	{
		if (CurrentLobby != null && CurrentLobby.ID == lobbyID)
		{
			CurrentLobby.HandleLobbyStateUpdated(changedPlayerID, msk);
		}
		else
		{
			d.Assert(changedPlayerID == LocalPlayerNetworkID && msk != Lobby.MemberLobbyStateMask.MLS_Entered, "[LobbySystem] Trying to update lobby state when we no longer have a lobby. This should only happen if we get a callback regarding ourselves leaving");
		}
	}

	public LobbyData SetupLobbyData(TTNetworkID lobbyID, bool requestRefreshedData = false)
	{
		string text = GetLobbyData(lobbyID, "name");
		if (text.NullOrEmpty() || requestRefreshedData)
		{
			Platform_RequestLobbyData(lobbyID);
			if (!requestRefreshedData)
			{
				text = "Lobby " + lobbyID.ToString();
			}
		}
		string lobbyData = GetLobbyData(lobbyID, "protocolVersion");
		LobbyData lobbyData2 = new LobbyData();
		lobbyData2.m_IDLobby = lobbyID;
		lobbyData2.m_LobbyName = text;
		lobbyData2.m_Language = StringLookup.GetLocalisedLanguageName(Singleton.Manager<Localisation>.inst.CurrentLanguage);
		string lobbyData3 = GetLobbyData(lobbyID, "language");
		if (!string.IsNullOrEmpty(lobbyData3) && int.TryParse(lobbyData3, out var result))
		{
			lobbyData2.m_Language = StringLookup.GetLocalisedLanguageName((LocalisationEnums.Languages)result);
		}
		lobbyData2.m_Visibility = GetLobbyVisibility(lobbyID);
		lobbyData2.m_ProtocolVersion = 0;
		if (!string.IsNullOrEmpty(lobbyData))
		{
			int.TryParse(lobbyData, out lobbyData2.m_ProtocolVersion);
		}
		string lobbyData4 = GetLobbyData(lobbyID, "bannedPlayers");
		if (!lobbyData4.NullOrEmpty())
		{
			lobbyData2.m_BannedPlayers.TryParse(lobbyData4);
		}
		lobbyData2.m_MaxUserLimit = GetLobbyMemberLimit(lobbyID);
		lobbyData2.m_NumUsers = GetNumLobbyMembers(lobbyID);
		lobbyData2.m_NumFriends = GetLobbyNumFriends(lobbyID);
		lobbyData2.m_PingTimeMS = -1;
		lobbyData2.m_GameInProgress = false;
		int num = 0;
		string lobbyData5 = GetLobbyData(lobbyID, "workshopIds");
		if (!lobbyData5.NullOrEmpty() && lobbyData5.Contains(","))
		{
			string[] array = lobbyData5.Split(',');
			num = array.Length;
			lobbyData2.m_WorkshopIds = new PublishedFileId_t[num];
			for (int i = 0; i < num; i++)
			{
				if (array[i].Length > 2)
				{
					array[i] = array[i].Substring(1, array[i].Length - 2);
					string[] array2 = array[i].Split(':');
					if (array2.Length >= 2)
					{
						if (ulong.TryParse(array2[1], out var result2))
						{
							lobbyData2.m_WorkshopIds[i] = new PublishedFileId_t(result2);
						}
						else
						{
							d.LogError("Workshop ID was in invalid number format: " + array2[1]);
						}
						continue;
					}
					d.LogError("Workshop ID was in invalid format: Expected '[name:workshopId]' but got '" + array[i] + "'  (from lobbyData: " + lobbyData5 + ")");
				}
				else
				{
					d.LogError("Workshop ID was in invalid format: Expected '[name:workshopId]' but got '" + array[i] + "'  (from lobbyData: " + lobbyData5 + ")");
				}
			}
		}
		else
		{
			lobbyData2.m_WorkshopIds = new PublishedFileId_t[0];
		}
		string lobbyData6 = GetLobbyData(lobbyID, "ownerID");
		TTNetworkID tTNetworkID = (string.IsNullOrEmpty(lobbyData6) ? TTNetworkID.Invalid : new TTNetworkID(lobbyData6));
		if (tTNetworkID.IsValid() && lobbyData2.m_ProtocolVersion == PROTOCOL_VERSION && GetLocalPlayerID() != tTNetworkID)
		{
			int pingTimeMS = 0;
			if (RetrievePingResponse(tTNetworkID, out pingTimeMS))
			{
				lobbyData2.m_PingTimeMS = pingTimeMS;
			}
			PingRequest(tTNetworkID);
		}
		lobbyData2.m_HostIsFriend = tTNetworkID.IsValid() && HasFriend(tTNetworkID);
		string lobbyData7 = GetLobbyData(lobbyID, "gameInProgress");
		if (!string.IsNullOrEmpty(lobbyData7))
		{
			lobbyData2.m_GameInProgress = lobbyData7 == "yes";
		}
		string lobbyData8 = GetLobbyData(lobbyID, "choices");
		if (!lobbyData8.NullOrEmpty() && lobbyData2.m_ProtocolVersion == PROTOCOL_VERSION)
		{
			lobbyData2.m_Choices = ParseLobbyChoicesFromString(lobbyData8);
		}
		else
		{
			_ = (lobbyData8.NullOrEmpty() ? "ChoicesIsNull/Empty" : "") + " " + ((lobbyData2.m_ProtocolVersion != PROTOCOL_VERSION) ? ("ProtocolVersion Wrong (" + lobbyData2.m_ProtocolVersion + ")") : "");
			lobbyData2.SetDefaultChoices();
		}
		return lobbyData2;
	}

	public void CreateLobby(MultiplayerModeType gameType, Lobby.LobbyVisibility visibility)
	{
		m_PendingLeave = false;
		m_GameTypeToSetAfterInit = gameType;
		m_LobbyVisibilityToSetAfterInit = visibility;
		Platform_CreateLobby(gameType, visibility, GetMaxPlayerCount(gameType));
	}

	protected virtual void RequestLobbyDataThenJoin(TTNetworkID lobbyID)
	{
		m_JoinInviteLobbyId = lobbyID;
		Platform_RequestLobbyData(lobbyID);
	}

	public void JoinLobby(TTNetworkID lobbyID, bool fromInvite, bool delayedRequestRejoin = false)
	{
		LobbyErrorCode errorCode;
		if (fromInvite && !delayedRequestRejoin && ((SKU.IsSteam && !SKU.UsesEOS) || SKU.XboxOneUI))
		{
			RequestLobbyDataThenJoin(lobbyID);
		}
		else if (!ValidateCanJoinLobby(lobbyID, fromInvite, out errorCode))
		{
			LobbyJoinFailedEvent.Send(errorCode);
		}
		else if (!IsJoinOrCreateLobbyRequestActive())
		{
			m_PendingLeave = false;
			Platform_JoinLobby(lobbyID, fromInvite);
		}
	}

	private bool ValidateCanJoinLobby(TTNetworkID lobbyID, bool fromInvite, out LobbyErrorCode errorCode)
	{
		errorCode = LobbyErrorCode.None;
		string lobbyData = GetLobbyData(lobbyID, "protocolVersion");
		if (PROTOCOL_VERSION.ToString() != lobbyData)
		{
			d.LogError("Trying to connect to lobby with incorrect protocol version! LobbyProtocol: " + lobbyData + " LocalProtocol:" + PROTOCOL_VERSION);
			errorCode = LobbyErrorCode.NotAllowed;
			return false;
		}
		if (GetLobbyVisibility(lobbyID) == Lobby.LobbyVisibility.Private && !fromInvite)
		{
			d.LogError("Failed to connect because lobby was private and user tried to join without invite.");
			errorCode = LobbyErrorCode.NotAllowed;
			return false;
		}
		if (!SKU.SupportsMods && string.Compare(GetLobbyData(lobbyID, "hasMods"), "true", ignoreCase: true) == 0)
		{
			errorCode = LobbyErrorCode.ModsNotSupported;
			return false;
		}
		if (BannedPlayers.IsPlayerBanned(GetLobbyData(lobbyID, "bannedPlayers"), GetPersistentPlayerID(LocalPlayerNetworkID)))
		{
			errorCode = LobbyErrorCode.Banned;
			return false;
		}
		return true;
	}

	public void LeaveLobby()
	{
		if (CurrentLobby != null)
		{
			CurrentLobby.OnLocalPlayerLeave();
			Platform_LeaveLobby(CurrentLobby.ID);
			m_CurrentLobby.Shutdown();
			m_CurrentLobby = null;
		}
		else
		{
			m_PendingLeave = true;
		}
	}

	public void HandleLobbyCreationFailure(string errCode, LobbyErrorCode userError)
	{
		LobbyCreateFailedEvent.Send(userError);
		m_PendingLeave = false;
	}

	public void HandleLobbyCreationSuccess(TTNetworkID lobbyID)
	{
		if (m_PendingLeave)
		{
			Platform_LeaveLobby(lobbyID);
			LobbyCreateFailedEvent.Send(LobbyErrorCode.Cancelled);
		}
		else
		{
			d.Assert(lobbyID.IsValid());
			m_CurrentLobby = Platform_CreateLobbyObject(SetupLobbyData(lobbyID), m_GameTypeToSetAfterInit);
			CurrentLobby.SetLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_GAME_TYPE, (int)m_GameTypeToSetAfterInit);
			CurrentLobby.SetupLobbyFromData();
			CurrentLobby.SetVisibility(m_LobbyVisibilityToSetAfterInit);
			LobbyJoinedEvent.Send(CurrentLobby);
			CurrentLobby.HandleBecomingOwner();
		}
		m_PendingLeave = false;
	}

	public void HandleLobbyJoinFailure(string errCode, LobbyErrorCode lobbyError)
	{
		LobbyJoinFailedEvent.Send(lobbyError);
		m_PendingLeave = false;
	}

	public void HandleLobbyJoinSuccess(TTNetworkID lobbyID)
	{
		d.Assert(lobbyID.IsValid());
		LobbyData lobbyData = GetLobby(lobbyID);
		if (lobbyData == null)
		{
			m_SetupLobbyDelayed = true;
			lobbyData = SetupLobbyData(lobbyID);
		}
		m_CurrentLobby = Platform_CreateLobbyObject(lobbyData, lobbyData.GameTypeChoice);
		if (m_SetupLobbyDelayed)
		{
			Platform_RequestLobbyData(lobbyID);
		}
		else
		{
			SetupCurrentLobbyJoinSuccess();
		}
	}

	private void SetupCurrentLobbyJoinSuccess()
	{
		CurrentLobby.SetupLobbyFromData();
		if (!CurrentLobby.IsLobbyOwner())
		{
			CurrentLobby.ParseUsedColoursFromLobbyData();
		}
		LobbyJoinedEvent.Send(CurrentLobby);
		m_PendingLeave = false;
	}

	public void HandleConnectionFailure(TTNetworkID remoteHostID, string errCode)
	{
		if (CurrentLobby != null && remoteHostID == CurrentLobby.RemoteHostExpectedToConnectTo)
		{
			LeaveLobby();
		}
	}

	public void SetJoypadDisconnected(bool dis)
	{
		m_JoypadDisconnected = dis;
	}

	public void TriggerGameStart()
	{
		if (CurrentLobby != null)
		{
			CurrentLobby.TriggerGameStart();
		}
	}

	public void RefreshLobbyList(LobbyFilterOptions filterOptions)
	{
		if (!IsBusyRequestingLobbies)
		{
			m_TempFilterOptions = new LobbyFilterOptions(filterOptions);
			m_RequestingLobbies = true;
			Platform_RefreshLobbyList(m_TempFilterOptions);
		}
	}

	public void StopListingLobbies()
	{
		Platform_StopListingLobbies();
	}

	public void UpdateCurrentLobbyList()
	{
		if (!m_RequestingLobbies)
		{
			for (int i = 0; i < m_LobbyList.Count; i++)
			{
				LobbyData value = SetupLobbyData(m_LobbyList[i].m_IDLobby, requestRefreshedData: true);
				m_LobbyList[i] = value;
			}
			LobbyListUpdatedEvent.Send(m_LobbyList, paramB: false);
		}
	}

	public void ClearPingRequests()
	{
		m_PingRequests.Clear();
	}

	public bool LobbyExistsInList(TTNetworkID lobbyID)
	{
		for (int i = 0; i < m_LobbyList.Count; i++)
		{
			if (lobbyID == m_LobbyList[i].m_IDLobby)
			{
				return true;
			}
		}
		return false;
	}

	protected void AddLobbyToList(TTNetworkID lobbyID, bool refreshData, bool fromInvite)
	{
		LobbyData lobbyData = SetupLobbyData(lobbyID, refreshData);
		if (fromInvite || m_TempFilterOptions.isLobbyAcceptable(lobbyData, LocalPlayerNetworkID))
		{
			m_LobbyList.Add(lobbyData);
		}
	}

	private void RemoveLobbyFromList(TTNetworkID lobbyID)
	{
		for (int i = 0; i < m_LobbyList.Count; i++)
		{
			if (lobbyID == m_LobbyList[i].m_IDLobby)
			{
				m_LobbyList.Remove(m_LobbyList[i]);
				break;
			}
		}
	}

	protected void AddOrUpdateLobbyInList(TTNetworkID lobbyID, bool refreshData, bool fromInvite)
	{
		LobbyData lobbyData = SetupLobbyData(lobbyID, refreshData);
		bool flag = false;
		for (int i = 0; i < m_LobbyList.Count; i++)
		{
			if (lobbyID == m_LobbyList[i].m_IDLobby)
			{
				m_LobbyList[i] = lobbyData;
				flag = true;
			}
		}
		if (!flag && (fromInvite || m_TempFilterOptions.isLobbyAcceptable(lobbyData, LocalPlayerNetworkID)))
		{
			m_LobbyList.Add(lobbyData);
		}
	}

	public LobbyData GetLobby(TTNetworkID lobbyID)
	{
		LobbyData result = null;
		for (int i = 0; i < m_LobbyList.Count; i++)
		{
			if (m_LobbyList[i].m_IDLobby == lobbyID)
			{
				result = m_LobbyList[i];
				break;
			}
		}
		return result;
	}

	public void HandleLobbyMatchListFailed()
	{
		m_RequestingLobbies = false;
		m_LobbyList.Clear();
	}

	public void HandleLobbyMatchListBegin()
	{
		m_LobbyList.Clear();
	}

	public void HandleLobbyMatchListAddLobby(TTNetworkID lobbyID, bool refreshData)
	{
		AddOrUpdateLobbyInList(lobbyID, refreshData, fromInvite: false);
	}

	public void HandleLobbyMatchListEnd()
	{
		m_RequestingLobbies = false;
		LobbyListUpdatedEvent.Send(m_LobbyList, paramB: true);
	}

	public void HandlePingResponse(TTNetworkID remoteHostID)
	{
		for (int i = 0; i < m_PingRequests.Count; i++)
		{
			PingRequestData pingRequestData = m_PingRequests[i];
			if (pingRequestData.m_HostID == remoteHostID && pingRequestData.m_CurrPingStartTime > 0f)
			{
				float num = (Time.realtimeSinceStartup - pingRequestData.m_CurrPingStartTime) * 1000f;
				if (!pingRequestData.m_IsFirstPing || !(num > 500f))
				{
					pingRequestData.m_PingCount++;
					pingRequestData.m_TotalPingMS += num;
					pingRequestData.m_AveragePingMS = pingRequestData.m_TotalPingMS / (float)pingRequestData.m_PingCount;
				}
				pingRequestData.m_CurrPingStartTime = 0f;
				pingRequestData.m_IsFirstPing = false;
				break;
			}
		}
	}

	public void PingRequest(TTNetworkID hostID)
	{
		PingRequestData pingRequestData = null;
		for (int i = 0; i < m_PingRequests.Count; i++)
		{
			if (m_PingRequests[i].m_HostID == hostID)
			{
				pingRequestData = m_PingRequests[i];
				break;
			}
		}
		bool num = pingRequestData == null;
		if (num)
		{
			pingRequestData = new PingRequestData
			{
				m_HostID = hostID
			};
		}
		if (pingRequestData.m_CurrPingStartTime == 0f)
		{
			pingRequestData.m_CurrPingStartTime = Time.realtimeSinceStartup;
			pingRequestData.m_LastPingRequestTime = pingRequestData.m_CurrPingStartTime;
			Platform_SendPingRequest(hostID);
		}
		if (num)
		{
			m_PingRequests.Add(pingRequestData);
		}
	}

	protected bool RetrievePingResponse(TTNetworkID hostID, out int pingTimeMS)
	{
		bool result = false;
		pingTimeMS = 0;
		if (Platform_GetLobbyPingTime(hostID, ref pingTimeMS))
		{
			result = true;
		}
		else
		{
			for (int i = 0; i < m_PingRequests.Count; i++)
			{
				if (m_PingRequests[i].m_HostID == hostID)
				{
					if (m_PingRequests[i].m_AveragePingMS > 0f)
					{
						result = true;
						pingTimeMS = Mathf.FloorToInt(m_PingRequests[i].m_AveragePingMS + 0.5f);
					}
					break;
				}
			}
		}
		return result;
	}

	public Lobby.LobbyVisibility GetLobbyVisibility(TTNetworkID lobbyID)
	{
		string lobbyData = GetLobbyData(lobbyID, "lobbyPublic");
		if (string.IsNullOrEmpty(lobbyData))
		{
			return Lobby.LobbyVisibility.Private;
		}
		return (Lobby.LobbyVisibility)Enum.Parse(typeof(Lobby.LobbyVisibility), lobbyData);
	}

	private void PurgeStalePingResults()
	{
		float num = Time.realtimeSinceStartup - 180f;
		for (int num2 = m_PingRequests.Count - 1; num2 >= 0; num2--)
		{
			if (m_PingRequests[num2].m_LastPingRequestTime < num)
			{
				m_PingRequests.RemoveAt(num2);
			}
		}
	}

	public int GetMaxPlayerCount(MultiplayerModeType gameType)
	{
		switch (gameType)
		{
		case MultiplayerModeType.CoOpCampaign:
			return 4;
		case MultiplayerModeType.CoOpCreative:
			return 4;
		case MultiplayerModeType.Deathmatch:
			return 16;
		default:
			d.LogErrorFormat("GetMaxPlayerCount does not handle game type {0}", gameType);
			return 1;
		}
	}

	private void Awake()
	{
		d.Assert(CurrentLobby == null, "[LobbySystem] LobbySystem pooled with a pre-existing Lobby");
		m_CurrentLobby = null;
	}

	public virtual void Update()
	{
		if (CurrentLobby != null)
		{
			CurrentLobby.UpdateLobby();
		}
		if (CurrentLobby != null)
		{
			CurrentLobby.CheckForBeingKicked();
		}
		PurgeStalePingResults();
	}

	private void OnDestroy()
	{
		if (CurrentLobby != null)
		{
			LeaveLobby();
		}
	}

	public int[] ParseLobbyChoicesFromString(string choicesString)
	{
		string[] array = choicesString.Split(',');
		int num = array.Length;
		int num2 = 34;
		if (num != 0)
		{
		}
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			if (!int.TryParse(array[i], out array2[i]))
			{
				d.Assert(condition: false, "[LobbySystem] Failed to parse Lobby Choice index: " + i);
			}
		}
		return array2;
	}

	public Color GetMultiplayerColour(NetPlayer player)
	{
		if (player == null)
		{
			return Color.white;
		}
		return GetMultiplayerTeamColour(player.LobbyTeamID, player.Colour);
	}

	public Color GetMultiplayerTeamColour(int lobbyTeamID, Color fallback)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
		{
			return CoOpColours[lobbyTeamID];
		}
		if (CurrentLobby != null && CurrentLobby.Data.TeamMatchChoice > 0)
		{
			return DeathmatchColours[lobbyTeamID];
		}
		return fallback;
	}

	public Color GetMultiplayerTeamTextColour(int lobbyTeamID, Color fallback)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
		{
			return CoOpChatColours[lobbyTeamID];
		}
		if (CurrentLobby != null && CurrentLobby.Data.TeamMatchChoice > 0)
		{
			return DeathmatchColours[lobbyTeamID];
		}
		return fallback;
	}

	public Color GetMultiplayerTechColour(NetTech netTech, int techTeam, Color fallbackColour)
	{
		int num = ManSpawn.LobbyTeamIDFromTechTeamID(techTeam);
		if (num == int.MaxValue)
		{
			return fallbackColour;
		}
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
		{
			if (netTech.IsNotNull() && netTech.NetPlayer.IsNotNull())
			{
				return m_Constants.m_CoOpColours[num];
			}
			return m_Constants.m_UnpilotedTechColours[num];
		}
		if (netTech.IsNotNull() && netTech.NetPlayer.IsNotNull())
		{
			return netTech.NetPlayer.Colour;
		}
		return m_Constants.m_AllColours[num];
	}

	public void DownloadSprite(string imageURL, int imageID, Action<bool, int, Texture2D> callback)
	{
		StartCoroutine(DownloadSprite_Coroutine(imageURL, imageID, callback));
	}

	private IEnumerator<UnityWebRequestAsyncOperation> DownloadSprite_Coroutine(string imageURL, int imageID, Action<bool, int, Texture2D> callback)
	{
		UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
		yield return request.SendWebRequest();
		if (request.isNetworkError || request.isHttpError)
		{
			d.LogError(request.error);
			callback(arg1: false, imageID, null);
		}
		else
		{
			d.Log("Successfully completed DownloadSprite coroutine");
			callback(arg1: true, imageID, ((DownloadHandlerTexture)request.downloadHandler).texture);
		}
	}
}
