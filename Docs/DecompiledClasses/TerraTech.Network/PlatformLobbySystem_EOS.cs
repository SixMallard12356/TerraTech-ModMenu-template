#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Epic.OnlineServices;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.UI;
using PlayEveryWare.EpicOnlineServices;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class PlatformLobbySystem_EOS : LobbySystem
{
	private struct CloseP2PData
	{
		public TTNetworkID m_ClientID;

		public float m_Delay;
	}

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

	private class ExternalAccountTypeComparer : IEqualityComparer<ExternalAccountType>
	{
		public bool Equals(ExternalAccountType x, ExternalAccountType y)
		{
			return x == y;
		}

		public int GetHashCode(ExternalAccountType obj)
		{
			return (int)obj;
		}
	}

	private struct LobbySearchResults
	{
		private uint m_NumResults;

		private LobbySearch m_SearchHandle;

		public Result Result { get; private set; }

		public IEnumerable<LobbyDetails> Lobbies
		{
			get
			{
				if (Result == Result.Success)
				{
					uint i = 0u;
					while (i < m_NumResults)
					{
						LobbySearchCopySearchResultByIndexOptions options = new LobbySearchCopySearchResultByIndexOptions
						{
							LobbyIndex = i
						};
						d.AssertFormat(m_SearchHandle.CopySearchResultByIndex(ref options, out var outLobbyDetailsHandle) == Result.Success, "Failed to Copy lobby SearchResult at index {0}", i);
						yield return outLobbyDetailsHandle;
						uint num = i + 1;
						i = num;
					}
				}
			}
		}

		public LobbySearchResults(LobbySearchFindCallbackInfo callbackInfo)
		{
			m_NumResults = 0u;
			Result = callbackInfo.ResultCode;
			m_SearchHandle = callbackInfo.ClientData as LobbySearch;
			if (Result != Result.Success)
			{
				d.LogErrorFormat("Failed to execute lobby search! Error: {0}", Result);
			}
			else
			{
				LobbySearchGetSearchResultCountOptions options = default(LobbySearchGetSearchResultCountOptions);
				m_NumResults = m_SearchHandle.GetSearchResultCount(ref options);
			}
		}
	}

	private List<NotifyEventHandle> m_NotifyEventHandles = new List<NotifyEventHandle>();

	private TTNetworkID m_LocalPlayerNetworkID;

	private Dictionary<byte, GetNextReceivedPacketSizeOptions> m_NextSizeReceivedOptionsByChannel = new Dictionary<byte, GetNextReceivedPacketSizeOptions>();

	private PlatformLobby_EOS.LobbyData_EOS CurrentLobbyData = new PlatformLobby_EOS.LobbyData_EOS();

	private Dictionary<Utf8String, PlatformLobby_EOS.LobbyData_EOS> m_CachedLobbyDataLookup = new Dictionary<Utf8String, PlatformLobby_EOS.LobbyData_EOS>();

	private bool m_CreatingOrJoiningLobby;

	private const byte PING_ENQ = 5;

	private const byte PING_ACK = 6;

	private byte[] m_PingBuffer = new byte[1];

	private bool m_Inited;

	private Dictionary<ExternalAccountType, EOSAccountPlatform_Base> m_AccountPlatforms = new Dictionary<ExternalAccountType, EOSAccountPlatform_Base>(new ExternalAccountTypeComparer());

	private EOSAccountPlatform_Base m_NativeAccountPlatform;

	private ExternalAccountType[] kSupportedNativePlatforms = new ExternalAccountType[2]
	{
		ExternalAccountType.Epic,
		ExternalAccountType.Steam
	};

	private ConnectionConfig m_ConnectionConfig;

	private int m_ChannelReliable;

	private int m_ChannelUnreliable;

	private int m_ChannelPing;

	private LobbyJoinRequest m_PendingJoinRequest;

	private ClientInviteStatus m_ClientInviteStatus = new ClientInviteStatus();

	private List<TTNetworkID> m_OpenP2PConnections = new List<TTNetworkID>();

	private List<TTNetworkID> m_QueuedLobbyUpdates = new List<TTNetworkID>();

	private List<CloseP2PData> m_P2PConnectionsToClose = new List<CloseP2PData>();

	private Coroutine m_WaitForConnectLoginCoroutine;

	private LobbyFilterOptions m_LobbyDisplayFilterOptions;

	private const int kEOSMaxResults = 200;

	private byte[] m_PacketReceiveBuffer;

	public EOSAccountPlatform_Base NativeAccountPlatform => m_NativeAccountPlatform;

	private PlatformLobby_EOS CurrentLobby_Platform => base.CurrentLobby as PlatformLobby_EOS;

	public int m_ChannelLobbyChat { get; private set; }

	public override bool Init(LobbyConstants constants, GameStateQuerier gsq)
	{
		base.Init(constants, gsq);
		d.Assert(SKU.UsesEOS, "[PlatformLobbySystem_EOS] EOS lobby system is not valid for given SKU");
		m_ConnectionConfig = new ConnectionConfig();
		m_ConnectionConfig.PacketSize = 1170;
		m_ChannelReliable = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_ChannelUnreliable = m_ConnectionConfig.AddChannel(QosType.Unreliable);
		m_ChannelPing = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_ChannelLobbyChat = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_WaitForConnectLoginCoroutine = StartCoroutine(Init_WaitForLogin());
		return true;
	}

	private IEnumerator Init_WaitForLogin()
	{
		float timeout;
		for (timeout = 5f; timeout > 0f; timeout -= Time.deltaTime)
		{
			if (Singleton.Manager<ManEOS>.inst.IsConnected)
			{
				InitLobbyInternals();
				break;
			}
			yield return null;
		}
		d.Assert(Singleton.Manager<ManEOS>.inst.IsValidlyMissingAuth || timeout > 0f, "Timed out trying to initialise lobby system EOS. Online capabilities won't be available! Did login fail, or was postponed somehow?");
		m_WaitForConnectLoginCoroutine = null;
	}

	private void InitLobbyInternals()
	{
		d.Assert(Singleton.Manager<ManEOS>.inst.IsConnected, "[PlatformLobbySystem_EOS] Can't initialise lobby system before ManEOS");
		d.Assert(!m_Inited, "[PlatformLobbySystem_EOS] Already initialised ?!");
		if (SKU.IsEpicGS)
		{
			m_NativeAccountPlatform = AddAccountPlatform(ExternalAccountType.Epic);
		}
		else if (SKU.IsSteam)
		{
			m_NativeAccountPlatform = AddAccountPlatform(ExternalAccountType.Steam);
			AddAccountPlatform(ExternalAccountType.Epic);
		}
		else
		{
			d.LogErrorFormat("Unimplemented EOS platform SKU {0}", SKU.CurrentBuildType);
		}
		SubscribeToNotifications();
		m_NativeAccountPlatform.RequestUserInformation(GetLocalPlayerID(), null);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnNetPlayerAdded);
		m_Inited = true;
	}

	public override void Shutdown()
	{
		d.Assert(m_NativeAccountPlatform == null, "Account platform and EOS Lobby notification handlers were not properly cleaned up! The EOS Platform interface is likely already gone at this point... (will crash)");
		CurrentLobbyData.Clear();
		base.Shutdown();
	}

	public override bool IsJoinOrCreateLobbyRequestActive()
	{
		if (!CurrentLobbyData._BeingCreated)
		{
			return m_CreatingOrJoiningLobby;
		}
		return true;
	}

	protected override void Platform_CreateLobby(MultiplayerModeType gameType, Lobby.LobbyVisibility visibility, int maxPlayerCount)
	{
		if (IsJoinOrCreateLobbyRequestActive())
		{
			d.LogError("Already in the process of creating or joining a lobby?!");
			return;
		}
		CreateLobbyOptions options = new CreateLobbyOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			MaxLobbyMembers = (uint)maxPlayerCount,
			PermissionLevel = visibility.AsPremissionLevel(),
			PresenceEnabled = true,
			AllowInvites = true,
			BucketId = "0",
			DisableHostMigration = true,
			EnableRTCRoom = false,
			EnableJoinById = true,
			RejoinAfterKickRequiresInvite = false,
			CrossplayOptOut = false
		};
		m_CreatingOrJoiningLobby = true;
		CurrentLobbyData.Clear();
		CurrentLobbyData._BeingCreated = true;
		CurrentLobbyData.LobbyOwner = options.LocalUserId;
		EOSManager.Instance.GetEOSLobbyInterface().CreateLobby(ref options, GetLocalPlayerID(), OnLobbyCreated);
	}

	protected override Lobby Platform_CreateLobbyObject(LobbyData data, MultiplayerModeType gameType)
	{
		if (!SKU.UsesEOS || !Singleton.Manager<ManEOS>.inst.IsConnected)
		{
			return null;
		}
		return new PlatformLobby_EOS(this, data, m_ConnectionConfig, gameType);
	}

	protected override void Platform_JoinLobby(TTNetworkID lobbyID, bool fromInvite)
	{
		if (IsJoinOrCreateLobbyRequestActive())
		{
			d.LogError("Already in the process of creating or joining a lobby?!");
			return;
		}
		Utf8String utf8String = lobbyID.ToEOSLobbyID();
		if (CurrentLobbyData.IsValid())
		{
			if (CurrentLobbyData.Id == utf8String)
			{
				d.LogError("Lobbies (JoinLobby): Already in the same lobby!");
			}
			else
			{
				d.LogError("Lobbies (JoinLobby): Leaving lobby now (must Join again, Active Join Not Implemented)!");
			}
			return;
		}
		CopyLobbyDetailsHandleOptions copyLobbyDetailsHandleOptions = new CopyLobbyDetailsHandleOptions
		{
			LobbyId = utf8String,
			LocalUserId = EOSManager.Instance.GetProductUserId()
		};
		PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(utf8String);
		if (platformLobbyData == null)
		{
			HandleLobbyJoinFailure("Missing lobby data", LobbyErrorCode.DoesntExist);
			return;
		}
		LobbyDetails lobbyDetailsHandle = platformLobbyData.LobbyDetailsHandle;
		m_CreatingOrJoiningLobby = true;
		JoinLobbyOptions options = new JoinLobbyOptions
		{
			LobbyDetailsHandle = lobbyDetailsHandle,
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			PresenceEnabled = true,
			CrossplayOptOut = false
		};
		EOSManager.Instance.GetEOSLobbyInterface().JoinLobby(ref options, null, OnLobbyEntered);
	}

	protected override void Platform_LeaveLobby(TTNetworkID lobbyID)
	{
		if (!m_Inited)
		{
			d.LogError("Lobbies (LeaveLobby): Lobby system not initialised.");
			CurrentLobbyData.Clear();
			return;
		}
		if (!CurrentLobbyData.IsValid() || !EOSManager.Instance.GetProductUserId().IsValid())
		{
			d.LogWarning("Lobbies (LeaveLobby): Not currently in a lobby.");
			CurrentLobbyData.Clear();
			return;
		}
		d.AssertFormat(CurrentLobbyData.Id == lobbyID.ToEOSLobbyID(), "Trying to leave a lobby we're not currently in? {0} - {1}", lobbyID, CurrentLobbyData.Id);
		ProductUserId productUserId = EOSManager.Instance.GetProductUserId();
		CurrentLobbyData.IsOwner(productUserId);
		LeaveLobbyOptions options = new LeaveLobbyOptions
		{
			LocalUserId = productUserId,
			LobbyId = CurrentLobbyData.Id
		};
		d.LogFormat("Lobbies (LeaveLobby): Attempting to leave lobby: Id='{0}', LocalUserId='{1}'", options.LobbyId, options.LocalUserId);
		EOSManager.Instance.GetEOSLobbyInterface().LeaveLobby(ref options, null, OnLeaveLobbyCompleted);
	}

	protected override void Platform_RequestLobbyData(TTNetworkID lobbyID)
	{
		Utf8String key = lobbyID.ToEOSLobbyID();
		if (m_CachedLobbyDataLookup.TryGetValue(key, out var _))
		{
			return;
		}
		SearchForLobbies(delegate(LobbySearch search)
		{
			LobbySearchSetLobbyIdOptions options = new LobbySearchSetLobbyIdOptions
			{
				LobbyId = lobbyID.ToEOSLobbyID()
			};
			search.SetLobbyId(ref options);
		}, delegate(ref LobbySearchFindCallbackInfo data)
		{
			if (data.ResultCode != Result.Success)
			{
				d.LogErrorFormat("Failed to execute lobby search! Error: {0}", data.ResultCode);
				return;
			}
			foreach (LobbyDetails lobby in new LobbySearchResults(data).Lobbies)
			{
				Utf8String lobbyId = lobby.GetDetailsInfo().LobbyId;
				PlatformLobby_EOS.LobbyData_EOS lobbyData_EOS = new PlatformLobby_EOS.LobbyData_EOS();
				lobbyData_EOS.InitFromLobbyDetails(lobby);
				m_CachedLobbyDataLookup[lobbyId] = lobbyData_EOS;
				AddOrUpdateLobbyInList(lobbyID, refreshData: true, fromInvite: true);
			}
		});
	}

	public override string GetLobbyData(TTNetworkID lobbyID, string keyName)
	{
		Utf8String lobbyId = lobbyID.ToEOSLobbyID();
		PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(lobbyId, allowMissing: true);
		if (platformLobbyData == null)
		{
			d.LogError($"lobby data for id {lobbyID} is missing - lobby not recognised");
			return null;
		}
		if (keyName == "lobbyPublic")
		{
			return platformLobbyData.LobbyVisibility.ToString();
		}
		if (keyName == "ownerID")
		{
			return platformLobbyData.LobbyOwner.ToTTID().ToString();
		}
		string lobbyAttribute = platformLobbyData.GetLobbyAttribute(keyName);
		if (lobbyAttribute == null)
		{
			return null;
		}
		return lobbyAttribute;
	}

	private bool TryGetUserName(TTNetworkID playerID, out string userName)
	{
		return m_NativeAccountPlatform.TryGetUserName(playerID, out userName);
	}

	public override string GetUserName(TTNetworkID playerID)
	{
		if (!TryGetUserName(playerID, out var userName))
		{
			d.LogWarningFormat("Username for id {0} was not yet known to the system. Use Platform_GetUserName to retrieve it asynchronously", playerID);
		}
		return userName;
	}

	public override void Platform_GetUserName(TTNetworkID playerID, Action<TTNetworkID, string> onUsernameRetrieved)
	{
		if (TryGetUserName(playerID, out var userName))
		{
			onUsernameRetrieved(playerID, userName);
			return;
		}
		m_NativeAccountPlatform.RequestUserNameAsync(playerID, delegate(TTNetworkID userNameID, string arg)
		{
			onUsernameRetrieved?.Invoke(userNameID, arg);
		});
	}

	public override int GetLobbyNumFriends(TTNetworkID lobbyID)
	{
		return 0;
	}

	public override bool HasFriend(TTNetworkID eosPlayerID)
	{
		return false;
	}

	private void OnAvatarImageLoaded(TTNetworkID platformID, int imageID, int width, int height)
	{
		if (base.CurrentLobby != null && m_NativeAccountPlatform.TryGetEOSID(platformID, out var eosID))
		{
			base.CurrentLobby.HandleAvatarImageLoaded(eosID, imageID, width, height);
		}
	}

	public override void Update()
	{
		UpdateInternal();
		float deltaTime = Time.deltaTime;
		for (int num = m_P2PConnectionsToClose.Count - 1; num >= 0; num--)
		{
			CloseP2PData value = m_P2PConnectionsToClose[num];
			float num2 = value.m_Delay - deltaTime;
			if (num2 <= 0f)
			{
				CloseConnection(value.m_ClientID);
				m_P2PConnectionsToClose.RemoveAt(num);
			}
			else
			{
				value.m_Delay = num2;
				m_P2PConnectionsToClose[num] = value;
			}
		}
		base.Update();
	}

	public override int GetNumLobbyMembers(TTNetworkID lobbyID)
	{
		PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(lobbyID.ToEOSLobbyID(), allowMissing: true);
		if (platformLobbyData == null)
		{
			d.LogError("GetNumLobbyMembers Missing lobby data - is this bad?");
			return 0;
		}
		return platformLobbyData.NumLobbyMembers;
	}

	public override int GetLobbyMemberLimit(TTNetworkID lobbyID)
	{
		PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(lobbyID.ToEOSLobbyID(), allowMissing: true);
		if (platformLobbyData == null)
		{
			d.LogError("GetLobbyMemberLimit Missing lobby data - is this bad?");
			return 0;
		}
		return (int)platformLobbyData.MaxNumLobbyMembers;
	}

	public override TTNetworkID GetLocalPlayerID()
	{
		if (!m_LocalPlayerNetworkID.IsValid())
		{
			m_LocalPlayerNetworkID = EOSManager.Instance.GetProductUserId().ToTTID();
		}
		return m_LocalPlayerNetworkID;
	}

	public override TTNetworkID GetLobbyMemberByIndex(TTNetworkID lobbyID, int idx)
	{
		d.Assert(base.CurrentLobby != null && base.CurrentLobby.ID == lobbyID, "Member data is not available if we are not part of the lobby!");
		PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(lobbyID.ToEOSLobbyID());
		if (platformLobbyData == null)
		{
			d.LogError("GetLobbyMemberByIndex Missing lobby data!");
			return TTNetworkID.Invalid;
		}
		if (idx < 0 || idx >= platformLobbyData.Members.Count)
		{
			d.LogErrorFormat("GetLobbyMemberByIndex Invalid member index {0}/{1}!", idx, platformLobbyData.Members.Count - 1);
			return TTNetworkID.Invalid;
		}
		return platformLobbyData.Members[idx].ProductId.ToTTID();
	}

	public override void SendInvites()
	{
	}

	public override List<TTNetworkID> GetPlayersThatAreTalking()
	{
		return null;
	}

	public override void GlobalMuteAll(bool mute)
	{
	}

	public override void MuteNetworkPlayer(TTNetworkID playerID, bool mute)
	{
	}

	protected override void Platform_SendPingRequest(TTNetworkID hostID)
	{
		ProductUserId targetUser = hostID.ToEOSProductUserID();
		m_PingBuffer[0] = 5;
		SendPacket(new ArraySegment<byte>(m_PingBuffer, 0, 1), targetUser, m_ChannelPing, PacketReliability.ReliableUnordered, "PingEnq");
	}

	protected override void Platform_RefreshLobbyList(LobbyFilterOptions filterOptions)
	{
		if (m_Inited)
		{
			m_LobbyDisplayFilterOptions = filterOptions;
			SearchForLobbies(SetLobbySearchParams, OnLobbySearchComplete);
		}
		else
		{
			HandleLobbyMatchListFailed();
		}
		void SetLobbySearchParams(LobbySearch lobbySearchHandle)
		{
			lobbySearchHandle.AddSearchAttribute("protocolVersion", LobbySystem.PROTOCOL_VERSION.ToString());
			if (m_LobbyDisplayFilterOptions.m_GameModeIndex != -1)
			{
				MultiplayerModeType gameModeIndex = (MultiplayerModeType)m_LobbyDisplayFilterOptions.m_GameModeIndex;
				lobbySearchHandle.AddSearchAttribute("gameModeIndex", gameModeIndex.ToString());
			}
			if (m_LobbyDisplayFilterOptions.m_ShowGamesInProgress == 0)
			{
				lobbySearchHandle.AddSearchAttribute("gameInProgress", "yes", ComparisonOp.Notequal);
			}
			if (m_LobbyDisplayFilterOptions.m_LanguageIndex == 0)
			{
				string localisedLanguageName = StringLookup.GetLocalisedLanguageName(Singleton.Manager<Localisation>.inst.CurrentLanguage);
				lobbySearchHandle.AddSearchAttribute("language", localisedLanguageName);
			}
			if (!SKU.SupportsMods || m_LobbyDisplayFilterOptions.m_ShowModdedGames == 0)
			{
				lobbySearchHandle.AddSearchAttribute("hasMods", "false");
			}
		}
	}

	private void SearchForLobbies(Action<LobbySearch> setSearchParamsFunc, LobbySearchOnFindCallback lobbySearchCompleteHandler, int maxResults = 200)
	{
		d.AssertFormat(maxResults >= 1 && maxResults <= 200, "EOS Lobby Search results are limited to between 1 and 200 results. Cannot ask for {0} results.", maxResults);
		CreateLobbySearchOptions options = new CreateLobbySearchOptions
		{
			MaxResults = (uint)maxResults
		};
		LobbySearch outLobbySearchHandle;
		Result result = EOSManager.Instance.GetEOSLobbyInterface().CreateLobbySearch(ref options, out outLobbySearchHandle);
		d.AssertFormat(result == Result.Success, "Failed to CreateLobbySearch: {0}", result);
		LobbySearchSetMaxResultsOptions options2 = new LobbySearchSetMaxResultsOptions
		{
			MaxResults = (uint)maxResults
		};
		outLobbySearchHandle.SetMaxResults(ref options2);
		setSearchParamsFunc?.Invoke(outLobbySearchHandle);
		LobbySearchFindOptions options3 = new LobbySearchFindOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId()
		};
		outLobbySearchHandle.Find(ref options3, outLobbySearchHandle, delegate(ref LobbySearchFindCallbackInfo callbackInfo)
		{
			LobbySearch obj = callbackInfo.ClientData as LobbySearch;
			lobbySearchCompleteHandler?.Invoke(ref callbackInfo);
			obj.Release();
		});
	}

	public override void OpenFriendInviteScreen()
	{
		m_NativeAccountPlatform.OpenFriendInviteScreen(base.CurrentLobby.ID);
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
		if (!m_Inited || !Singleton.Manager<ManEOS>.inst.IsConnected)
		{
			return;
		}
		if (m_PendingJoinRequest != null && GetPlatformLobbyData(m_PendingJoinRequest.m_LobbyID.ToEOSLobbyID(), allowMissing: true) != null)
		{
			LobbyData lobby = GetLobby(m_PendingJoinRequest.m_LobbyID);
			string lobbyData = GetLobbyData(m_PendingJoinRequest.m_LobbyID, "protocolVersion");
			if (lobby != null && !lobbyData.NullOrEmpty())
			{
				bool flag = true;
				if (m_PendingJoinRequest.m_FromInvite)
				{
					m_ClientInviteStatus.readyToJoin = false;
					ClientInvitationEvent.Send(m_ClientInviteStatus);
					flag = m_ClientInviteStatus.readyToJoin;
				}
				if (flag)
				{
					if (SKU.SupportsMods && lobby.NumMods > 0)
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
		if (Input.GetKeyDown(KeyCode.F5) && base.CurrentLobby != null)
		{
			OpenFriendInviteScreen();
		}
		ReadAllNetworkData();
	}

	public void ModifyLobby(PlatformLobby_EOS.LobbyParams lobbyParamUpdates, Action<Result> ModififyLobbyCompleted)
	{
		if (!CurrentLobbyData.IsValid())
		{
			d.LogError("Lobbies (ModifyLobby): Current Lobby {0} is invalid!");
			ModififyLobbyCompleted?.Invoke(Result.InvalidState);
			return;
		}
		ProductUserId productUserId = EOSManager.Instance.GetProductUserId();
		if (!productUserId.IsValid())
		{
			d.LogError("Lobbies (ModifyLobby): Current player is invalid!");
			ModififyLobbyCompleted?.Invoke(Result.InvalidProductUserID);
			return;
		}
		if (!CurrentLobbyData.IsOwner(productUserId))
		{
			d.LogError("Lobbies (ModifyLobby): Current player is not lobby owner!");
			ModififyLobbyCompleted?.Invoke(Result.LobbyNotOwner);
			return;
		}
		UpdateLobbyModificationOptions options = new UpdateLobbyModificationOptions
		{
			LobbyId = CurrentLobbyData.Id,
			LocalUserId = productUserId
		};
		Result result = EOSManager.Instance.GetEOSLobbyInterface().UpdateLobbyModification(ref options, out var outLobbyModificationHandle);
		if (result != Result.Success)
		{
			d.LogErrorFormat("Lobbies (ModifyLobby): Could not create lobby modification. Error code: {0}", result);
			ModififyLobbyCompleted?.Invoke(result);
			return;
		}
		if (!string.Equals(lobbyParamUpdates.BucketId, CurrentLobbyData.BucketId))
		{
			LobbyModificationSetBucketIdOptions options2 = new LobbyModificationSetBucketIdOptions
			{
				BucketId = lobbyParamUpdates.BucketId
			};
			result = outLobbyModificationHandle.SetBucketId(ref options2);
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (ModifyLobby): Could not set bucket id. Error code: {0}", result);
				ModififyLobbyCompleted?.Invoke(result);
				return;
			}
		}
		if (lobbyParamUpdates.MaxNumLobbyMembers != CurrentLobbyData.MaxNumLobbyMembers)
		{
			LobbyModificationSetMaxMembersOptions options3 = new LobbyModificationSetMaxMembersOptions
			{
				MaxMembers = lobbyParamUpdates.MaxNumLobbyMembers
			};
			result = outLobbyModificationHandle.SetMaxMembers(ref options3);
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (ModifyLobby): Could not set max players. Error code: {0}", result);
				ModififyLobbyCompleted?.Invoke(result);
				return;
			}
		}
		for (int i = 0; i < lobbyParamUpdates.Attributes.Count; i++)
		{
			AttributeData asAttribute = lobbyParamUpdates.Attributes[i].AsAttribute;
			d.AssertFormat(asAttribute.Key != null, "Lobbies (ModifyLobby): Attributes with null key!");
			LobbyModificationAddAttributeOptions options4 = new LobbyModificationAddAttributeOptions
			{
				Attribute = asAttribute,
				Visibility = lobbyParamUpdates.Attributes[i].Visibility
			};
			result = outLobbyModificationHandle.AddAttribute(ref options4);
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (ModifyLobby): Could not add attribute {1}:{2}. Error code: {0}", result, asAttribute.Key, asAttribute.Value.AsUtf8);
				ModififyLobbyCompleted?.Invoke(result);
				return;
			}
		}
		if (lobbyParamUpdates.LobbyVisibility != CurrentLobbyData.LobbyVisibility)
		{
			LobbyModificationSetPermissionLevelOptions options5 = new LobbyModificationSetPermissionLevelOptions
			{
				PermissionLevel = lobbyParamUpdates.LobbyVisibility.AsPremissionLevel()
			};
			result = outLobbyModificationHandle.SetPermissionLevel(ref options5);
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (ModifyLobby): Could not set permission level to {1}. Error code: {0}", result, options5.PermissionLevel);
				ModififyLobbyCompleted?.Invoke(result);
				return;
			}
		}
		if (lobbyParamUpdates.AllowInvites != CurrentLobbyData.AllowInvites)
		{
			LobbyModificationSetInvitesAllowedOptions options6 = new LobbyModificationSetInvitesAllowedOptions
			{
				InvitesAllowed = lobbyParamUpdates.AllowInvites
			};
			result = outLobbyModificationHandle.SetInvitesAllowed(ref options6);
			if (result != Result.Success)
			{
				d.LogErrorFormat("Lobbies (ModifyLobby): Could not set allow invites to {1}. Error code: {0}", result, options6.InvitesAllowed);
				ModififyLobbyCompleted?.Invoke(result);
				return;
			}
		}
		CurrentLobbyData.GetParams();
		UpdateLobbyOptions options7 = new UpdateLobbyOptions
		{
			LobbyModificationHandle = outLobbyModificationHandle
		};
		EOSManager.Instance.GetEOSLobbyInterface().UpdateLobby(ref options7, ModififyLobbyCompleted, OnLobbyModifiedComplete);
	}

	private bool TryGetLobbyModificationHandle(out LobbyModification lobbyModificationHandle)
	{
		lobbyModificationHandle = null;
		if (!CurrentLobbyData.IsValid())
		{
			d.LogError("Lobbies (TryGetLobbyModificationHandle): Current Lobby {0} is invalid!");
			return false;
		}
		ProductUserId productUserId = EOSManager.Instance.GetProductUserId();
		if (!productUserId.IsValid())
		{
			d.LogError("Lobbies (TryGetLobbyModificationHandle): Current player is invalid!");
			return false;
		}
		UpdateLobbyModificationOptions options = new UpdateLobbyModificationOptions
		{
			LobbyId = CurrentLobbyData.Id,
			LocalUserId = productUserId
		};
		LobbyModification outLobbyModificationHandle;
		Result result = EOSManager.Instance.GetEOSLobbyInterface().UpdateLobbyModification(ref options, out outLobbyModificationHandle);
		if (result != Result.Success)
		{
			d.LogErrorFormat("Lobbies (TryGetLobbyModificationHandle): Could not create lobby modification. Error code: {0}", result);
			return false;
		}
		lobbyModificationHandle = outLobbyModificationHandle;
		return true;
	}

	private void SetCurrentLobbyMemberAttribute(string key, string value)
	{
		d.AssertFormat(!key.NullOrEmpty(), "Lobbies (SetCurrentLobbyMemberAttribute): Attributes with null key!");
		if (!TryGetLobbyModificationHandle(out var lobbyModificationHandle))
		{
			d.LogError("SetCurrentLobbyMemberAttribute Failed to GetLobbyModificationHandle (See previous error)");
			return;
		}
		PlatformLobby_EOS.LobbyAttribute lobbyAttribute = new PlatformLobby_EOS.LobbyAttribute(key, value);
		LobbyModificationAddMemberAttributeOptions options = new LobbyModificationAddMemberAttributeOptions
		{
			Attribute = lobbyAttribute.AsAttribute,
			Visibility = lobbyAttribute.Visibility
		};
		Result result = lobbyModificationHandle.AddMemberAttribute(ref options);
		if (result != Result.Success)
		{
			d.LogErrorFormat("Lobbies (SetCurrentLobbyMemberAttribute): Could not add attribute {1}:{2}. Error code: {0}", result, lobbyAttribute.Key, lobbyAttribute.AsString);
		}
	}

	private void RemoveCurrentLobbyMemberAttribute(string key)
	{
		d.AssertFormat(!key.NullOrEmpty(), "Lobbies (SetCurrentLobbyMemberAttribute): Attributes with null key!");
		if (!TryGetLobbyModificationHandle(out var lobbyModificationHandle))
		{
			d.LogError("RemoveCurrentLobbyMemberAttribute Failed to GetLobbyModificationHandle (See previous error)");
			return;
		}
		LobbyModificationRemoveMemberAttributeOptions options = new LobbyModificationRemoveMemberAttributeOptions
		{
			Key = key
		};
		Result result = lobbyModificationHandle.RemoveMemberAttribute(ref options);
		if (result != Result.Success)
		{
			d.LogErrorFormat("Lobbies (RemoveCurrentLobbyMemberAttribute): Could not remove attribute {1}. Error code: {0}", result, options.Key);
		}
	}

	public void ReadAllNetworkData(bool forceAll = false)
	{
		ReadNetworkData(m_ChannelPing);
		if (base.CurrentLobby != null || forceAll)
		{
			ReadNetworkData(m_ChannelLobbyChat);
		}
		if (base.IsMultiplayer || forceAll)
		{
			ReadNetworkData(m_ChannelReliable);
			ReadNetworkData(m_ChannelUnreliable);
		}
	}

	private void ReadNetworkData(int channelId)
	{
		P2PInterface eOSP2PInterface = EOSManager.Instance.GetEOSP2PInterface();
		EOSManager.Instance.GetProductUserId();
		if (!m_NextSizeReceivedOptionsByChannel.TryGetValue((byte)channelId, out var value))
		{
			value = new GetNextReceivedPacketSizeOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				RequestedChannel = (byte)channelId
			};
			m_NextSizeReceivedOptionsByChannel.Add((byte)channelId, value);
		}
		Result nextReceivedPacketSize;
		uint outPacketSizeBytes;
		while ((nextReceivedPacketSize = eOSP2PInterface.GetNextReceivedPacketSize(ref value, out outPacketSizeBytes)) == Result.Success)
		{
			if (m_PacketReceiveBuffer == null || m_PacketReceiveBuffer.Length < outPacketSizeBytes)
			{
				m_PacketReceiveBuffer = new byte[outPacketSizeBytes];
			}
			ArraySegment<byte> outData = new ArraySegment<byte>(m_PacketReceiveBuffer, 0, (int)outPacketSizeBytes);
			ReceivePacketOptions options = new ReceivePacketOptions
			{
				LocalUserId = EOSManager.Instance.GetProductUserId(),
				MaxDataSizeBytes = outPacketSizeBytes,
				RequestedChannel = (byte)channelId
			};
			ProductUserId outPeerId = null;
			SocketId outSocketId = default(SocketId);
			byte outChannel;
			uint outBytesWritten;
			Result result = eOSP2PInterface.ReceivePacket(ref options, ref outPeerId, ref outSocketId, out outChannel, outData, out outBytesWritten);
			channelId = outChannel;
			if (result == Result.Success)
			{
				if (channelId == m_ChannelPing)
				{
					if (outPacketSizeBytes == 0)
					{
						continue;
					}
					if (m_PacketReceiveBuffer[0] == 5)
					{
						m_PingBuffer[0] = 6;
						if (SendPacket(new ArraySegment<byte>(m_PingBuffer, 0, 1), outPeerId, channelId, PacketReliability.ReliableUnordered, "PingAck"))
						{
						}
					}
					else if (m_PacketReceiveBuffer[0] == 6)
					{
						HandlePingResponse(outPeerId.ToTTID());
					}
				}
				else if (base.CurrentLobby != null)
				{
					if (channelId == m_ChannelLobbyChat)
					{
						if (outPacketSizeBytes == 0)
						{
							continue;
						}
						using MemoryStream input = new MemoryStream(m_PacketReceiveBuffer, 0, (int)outBytesWritten, writable: false);
						using BinaryReader reader = new BinaryReader(input);
						CurrentLobby_Platform.HandleChatMessage(reader, (int)outBytesWritten, outPeerId.ToTTID());
					}
					else
					{
						CurrentLobby_Platform.ReadNetworkData(m_PacketReceiveBuffer, outPacketSizeBytes, channelId, outPeerId);
					}
				}
				else
				{
					if (channelId != m_ChannelLobbyChat || outPacketSizeBytes == 0)
					{
						continue;
					}
					using MemoryStream input2 = new MemoryStream(m_PacketReceiveBuffer, 0, (int)outBytesWritten, writable: false);
					using BinaryReader binaryReader = new BinaryReader(input2);
					Lobby.ChatMessageType chatMessageType = (Lobby.ChatMessageType)binaryReader.ReadByte();
					d.AssertFormat(chatMessageType == Lobby.ChatMessageType.ChatMessage, "[PlatformLobbySystem_EOS] Error trying to discard non-Chat message sent on the chat channel! Losing game-state message: '{0}'", chatMessageType);
				}
			}
			else
			{
				d.LogErrorFormat("Error receiving package queued up {0}", result);
			}
		}
		d.AssertFormat(nextReceivedPacketSize == Result.NotFound, "Unexpected result trying to read network data! {0}", nextReceivedPacketSize);
	}

	public void SendPacketToAll(ArraySegment<byte> data, int channelId, PacketReliability reliability = PacketReliability.ReliableUnordered, string socketName = "", bool errorOnFail = true, bool sendToSelf = false)
	{
		int numLobbyMembers = CurrentLobbyData.NumLobbyMembers;
		for (int i = 0; i < numLobbyMembers; i++)
		{
			ProductUserId productId = CurrentLobbyData.Members[i].ProductId;
			if (sendToSelf || productId.ToTTID() != GetLocalPlayerID())
			{
				SendPacket(data, productId, channelId, reliability, socketName, errorOnFail);
			}
		}
	}

	public bool SendPacket(ArraySegment<byte> data, ProductUserId targetUser, int channelId, PacketReliability reliability = PacketReliability.ReliableUnordered, string socketName = null, bool errorOnFail = true)
	{
		SocketId value = new SocketId
		{
			SocketName = socketName
		};
		SendPacketOptions options = new SendPacketOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			RemoteUserId = targetUser,
			SocketId = value,
			AllowDelayedDelivery = true,
			Channel = (byte)channelId,
			Reliability = reliability,
			Data = data
		};
		Result result = EOSManager.Instance.GetEOSP2PInterface().SendPacket(ref options);
		if (errorOnFail)
		{
		}
		return result == Result.Success;
	}

	private void CloseConnection(TTNetworkID networkID, string socketName = null)
	{
		d.LogFormat("[PlatformLobby_EOS] Closing P2P session with user {0}", networkID);
		CloseConnectionOptions options = new CloseConnectionOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			RemoteUserId = networkID.ToEOSProductUserID(),
			SocketId = ((socketName != null) ? new SocketId?(new SocketId
			{
				SocketName = socketName
			}) : ((SocketId?)null))
		};
		EOSManager.Instance.GetEOSP2PInterface().CloseConnection(ref options);
	}

	public void CloseP2PSessionAfterDelay(TTNetworkID networkID)
	{
		m_P2PConnectionsToClose.Add(new CloseP2PData
		{
			m_ClientID = networkID,
			m_Delay = 2f
		});
	}

	private void OnP2PConnectionRequest(ref OnIncomingConnectionRequestInfo data)
	{
		AcceptConnectionOptions options = new AcceptConnectionOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			RemoteUserId = data.RemoteUserId,
			SocketId = data.SocketId
		};
		EOSManager.Instance.GetEOSP2PInterface().AcceptConnection(ref options);
	}

	private void OnP2PConnectionEstablished(ref OnPeerConnectionEstablishedInfo data)
	{
		m_OpenP2PConnections.Add(data.RemoteUserId.ToTTID());
	}

	private void OnP2PConnectionInterrupted(ref OnPeerConnectionInterruptedInfo data)
	{
	}

	private void OnP2PConnectionLost(ref OnRemoteConnectionClosedInfo data)
	{
		if (data.Reason != ConnectionClosedReason.ClosedByLocalUser && data.Reason != ConnectionClosedReason.ClosedByPeer)
		{
			TTNetworkID tTNetworkID = data.RemoteUserId.ToTTID();
			TTNetworkConnection tTNetworkConnection = CurrentLobby_Platform?.GetTTNetworkConnection(tTNetworkID);
			if (tTNetworkConnection != null)
			{
				CurrentLobby_Platform.RemoveClientConnectionFromServer(tTNetworkID);
				try
				{
					tTNetworkConnection.InvokeHandlerNoData(33);
				}
				catch (Exception)
				{
					ForceClientDisconnectionEvent.Send(tTNetworkConnection);
				}
			}
			HandleConnectionFailure(tTNetworkID, data.Reason.ToString());
		}
		m_OpenP2PConnections.Remove(data.RemoteUserId.ToTTID());
	}

	private void OnP2PPacketQueueFull(ref OnIncomingPacketQueueFullInfo data)
	{
		d.LogErrorFormat("P2P Packet queue is full! Packet for '{0}' on channel {1} size {2} would overload queue of max size '{3}'", data.OverflowPacketLocalUserId, data.OverflowPacketChannel, data.OverflowPacketSizeBytes, data.PacketQueueMaxSizeBytes);
		ReadAllNetworkData(forceAll: true);
	}

	private LobbyErrorCode ConvertErrorCode(Result resultCode)
	{
		switch (resultCode)
		{
		case Result.NoConnection:
		case Result.NetworkDisconnected:
			return LobbyErrorCode.LostConnection;
		case Result.Canceled:
			return LobbyErrorCode.Cancelled;
		case Result.NotFound:
		case Result.LobbyInvalidSession:
			return LobbyErrorCode.DoesntExist;
		case Result.LimitExceeded:
		case Result.SessionsTooManyInvites:
		case Result.SessionsPlayerSanctioned:
			return LobbyErrorCode.Limited;
		case Result.SessionsTooManyPlayers:
		case Result.SessionsHostAtCapacity:
		case Result.LobbyTooManyPlayers:
		case Result.LobbyHostAtCapacity:
			return LobbyErrorCode.LobbyFull;
		case Result.SessionsNoPermission:
		case Result.SessionsNotAllowed:
		case Result.LobbyNoPermission:
		case Result.LobbyNotAllowed:
			return LobbyErrorCode.NotAllowed;
		case Result.UserIsInBlocklist:
			return LobbyErrorCode.YouBlockedMember;
		case Result.UserBanned:
			return LobbyErrorCode.MemberBlockedYou;
		case Result.UserKicked:
			return LobbyErrorCode.LostConnection;
		case Result.SessionsInviteFailed:
		case Result.SessionsInviteNotFound:
		case Result.LobbyLobbyAlreadyExists:
			return LobbyErrorCode.Error;
		default:
			return LobbyErrorCode.FailedToConnect;
		}
	}

	public PlatformLobby_EOS.LobbyData_EOS GetPlatformLobbyData(Utf8String lobbyId, bool allowMissing = false)
	{
		PlatformLobby_EOS.LobbyData_EOS value;
		bool flag = m_CachedLobbyDataLookup.TryGetValue(lobbyId, out value);
		d.AssertFormat(allowMissing || flag, "Failed to get cached lobby data for lobby {0}", lobbyId);
		return value;
	}

	[Conditional("MANUALLY_MIRROR_LOBBIES_TO_NON_EOS_PLATFORMS")]
	public void JoinOrCreateMirrorLobby(EOSAccountPlatform_Base accountPlatform, TTNetworkID eosLobbyID)
	{
		if (CurrentLobby_Platform == null || eosLobbyID != CurrentLobby_Platform.ID)
		{
			d.LogErrorFormat("Trying to Enter a platform mirrored lobby, but local player is not in a lobby! Abort!");
		}
		else if (accountPlatform.MirrorEOSLobbyToPlatform)
		{
			TTNetworkID mirrorLobbyID = CurrentLobby_Platform.GetMirrorLobbyID(accountPlatform);
			if (mirrorLobbyID.IsValid())
			{
				accountPlatform.JoinMirrorLobby(eosLobbyID, mirrorLobbyID);
			}
			else
			{
				accountPlatform.CreateMirrorLobby(CurrentLobby_Platform.ID, CurrentLobbyData.LobbyVisibility, (int)CurrentLobbyData.MaxNumLobbyMembers, OnMirrorLobbyCreated);
			}
		}
		void OnMirrorLobbyCreated(bool success, TTNetworkID _eosLobbyID, TTNetworkID createdPlatformLobbyID)
		{
			if (success)
			{
				ProductUserId productUserId = EOSManager.Instance.GetProductUserId();
				if (CurrentLobbyData.IsOwner(productUserId))
				{
					CurrentLobby_Platform.SetMirrorLobbyID(accountPlatform, createdPlatformLobbyID);
					accountPlatform.JoinMirrorLobby(_eosLobbyID, createdPlatformLobbyID);
				}
				else
				{
					string mirrorLobbyKey = CurrentLobby_Platform.GetMirrorLobbyKey(accountPlatform.PlatformType);
					string value = createdPlatformLobbyID.ToString();
					SetCurrentLobbyMemberAttribute(mirrorLobbyKey, value);
				}
			}
			else
			{
				d.LogErrorFormat("Failed to Create Mirror lobby for eosLobby {0}!", _eosLobbyID);
			}
		}
	}

	[Conditional("MANUALLY_MIRROR_LOBBIES_TO_NON_EOS_PLATFORMS")]
	public void LeaveMirrorLobby(EOSAccountPlatform_Base accountPlatform, TTNetworkID eosLobbyID, bool persistLobby)
	{
		if (CurrentLobby_Platform == null || eosLobbyID != CurrentLobby_Platform.ID)
		{
			d.LogErrorFormat("Trying to leave a platform mirrored lobby, but local player is not in a lobby! Abort!");
		}
		else if (accountPlatform.MirrorEOSLobbyToPlatform)
		{
			TTNetworkID mirrorLobbyID = CurrentLobby_Platform.GetMirrorLobbyID(accountPlatform);
			if (mirrorLobbyID.IsNull())
			{
				d.LogErrorFormat("Trying to Leave a platform mirrored lobby, but could not get mirrored lobby ID from lobby data! Abort!");
			}
			else if (!accountPlatform.LeaveMirrorLobby(eosLobbyID, mirrorLobbyID, persistLobby))
			{
				CurrentLobby_Platform.SetMirrorLobbyID(accountPlatform, TTNetworkID.Invalid);
				accountPlatform.DestroyMirrorLobby(eosLobbyID, mirrorLobbyID);
			}
		}
	}

	[Conditional("MANUALLY_MIRROR_LOBBIES_TO_NON_EOS_PLATFORMS")]
	private void UpdateMirrorLobbyFromData(TTNetworkID eosLobbyID)
	{
		ProductUserId productUserId = EOSManager.Instance.GetProductUserId();
		CurrentLobbyData.IsOwner(productUserId);
	}

	[Conditional("MANUALLY_MIRROR_LOBBIES_TO_NON_EOS_PLATFORMS")]
	private void UpdateMirrorLobbyFromData(EOSAccountPlatform_Base accountPlatform, TTNetworkID eosLobbyID)
	{
		if (CurrentLobby_Platform == null || eosLobbyID != CurrentLobby_Platform.ID)
		{
			d.LogErrorFormat("Trying to update a platform mirrored lobby, but local player is not in a lobby! Abort!");
		}
		else
		{
			if (!accountPlatform.MirrorEOSLobbyToPlatform)
			{
				return;
			}
			d.Assert(!CurrentLobbyData.IsOwner(EOSManager.Instance.GetProductUserId()), "This should only be handled for members; the host must listen for all platform updates from members; even those unavailable to itself.");
			TTNetworkID mirrorLobbyID = CurrentLobby_Platform.GetMirrorLobbyID(accountPlatform);
			TTNetworkID currentMirrorLobbyPlatformID = accountPlatform.CurrentMirrorLobbyPlatformID;
			if (mirrorLobbyID != currentMirrorLobbyPlatformID)
			{
				if (currentMirrorLobbyPlatformID.IsValid())
				{
					accountPlatform.LeaveMirrorLobby(eosLobbyID, currentMirrorLobbyPlatformID, persistLobby: false);
					accountPlatform.DestroyMirrorLobby(eosLobbyID, currentMirrorLobbyPlatformID);
				}
				if (mirrorLobbyID.IsValid())
				{
					accountPlatform.JoinMirrorLobby(eosLobbyID, mirrorLobbyID);
					string mirrorLobbyKey = CurrentLobby_Platform.GetMirrorLobbyKey(accountPlatform.PlatformType);
					RemoveCurrentLobbyMemberAttribute(mirrorLobbyKey);
				}
			}
		}
	}

	[Conditional("MANUALLY_MIRROR_LOBBIES_TO_NON_EOS_PLATFORMS")]
	private void Host_UpdateMirrorLobbiesFromData(TTNetworkID eosLobbyID)
	{
		if (CurrentLobby_Platform == null || eosLobbyID != CurrentLobby_Platform.ID)
		{
			d.LogErrorFormat("Trying to update a platform mirrored lobby, but local player is not in a lobby! Abort!");
			return;
		}
		d.Assert(CurrentLobbyData.IsOwner(EOSManager.Instance.GetProductUserId()), "This should only be handled for members; the host must listen for all platform updates from members; even those unavailable to itself.");
		ExternalAccountType[] array = kSupportedNativePlatforms;
		foreach (ExternalAccountType externalAccountType in array)
		{
			if (m_AccountPlatforms.TryGetValue(externalAccountType, out var value))
			{
				break;
			}
			string text = null;
			string mirrorLobbyKey = CurrentLobby_Platform.GetMirrorLobbyKey(externalAccountType);
			foreach (PlatformLobby_EOS.LobbyMember member in CurrentLobbyData.Members)
			{
				if (member.MemberAttributes.TryGetValue(mirrorLobbyKey, out var value2))
				{
					text = value2.AsString;
					break;
				}
			}
			if (!text.NullOrEmpty())
			{
				if (!CurrentLobby_Platform.GetMirrorLobbyID(value).IsValid())
				{
					CurrentLobby_Platform.SetMirrorLobbyID(value, new TTNetworkID(text));
				}
			}
			else
			{
				CurrentLobby_Platform.SetMirrorLobbyID(value, TTNetworkID.Invalid);
			}
		}
	}

	private EOSAccountPlatform_Base AddAccountPlatform(ExternalAccountType platform)
	{
		EOSAccountPlatform_Base eOSAccountPlatform_Base;
		switch (platform)
		{
		case ExternalAccountType.Epic:
			eOSAccountPlatform_Base = new EOSAccountPlatform_Epic();
			break;
		case ExternalAccountType.Steam:
			eOSAccountPlatform_Base = new EOSAccountPlatform_Steam();
			eOSAccountPlatform_Base.AvatarImageLoadedEvent = OnAvatarImageLoaded;
			break;
		default:
			eOSAccountPlatform_Base = null;
			d.LogErrorFormat("Unimplemented Account platform {0} requested!", platform);
			break;
		}
		if (eOSAccountPlatform_Base != null)
		{
			m_AccountPlatforms.Add(platform, eOSAccountPlatform_Base);
		}
		return eOSAccountPlatform_Base;
	}

	public EOSAccountPlatform_Base GetAccountPlatform(ExternalAccountType platform)
	{
		m_AccountPlatforms.TryGetValue(platform, out var value);
		return value;
	}

	private void OnLobbyCreated(ref CreateLobbyCallbackInfo data)
	{
		if (data.ResultCode == Result.Success)
		{
			if (SKU.SupportsMods)
			{
				Singleton.Manager<ManMods>.inst.PreLobbyCreated();
			}
			Utf8String lobbyId = data.LobbyId;
			d.AssertFormat(m_CreatingOrJoiningLobby, "[PlatformLobbySystem_EOS] OnLobbyCreated called but no lobby was requested! {0}", lobbyId);
			d.AssertFormat(CurrentLobbyData._BeingCreated, "[PlatformLobbySystem_EOS] OnLobbyCreated called but expected lobby was not marked for creation?! {0}", lobbyId);
			CurrentLobbyData.IsLobbyMember = true;
			CurrentLobbyData.TryInitFromLobbyHandle(lobbyId);
			CurrentLobbyData._BeingCreated = false;
			m_CreatingOrJoiningLobby = false;
			m_CachedLobbyDataLookup.Add(lobbyId, CurrentLobbyData);
			TTNetworkID lobbyID = lobbyId.ToTTID();
			HandleLobbyCreationSuccess(lobbyID);
		}
		else
		{
			CurrentLobbyData.Clear();
			HandleLobbyCreationFailure(data.ResultCode.ToString(), ConvertErrorCode(data.ResultCode));
		}
	}

	private void OnLobbyEntered(ref JoinLobbyCallbackInfo data)
	{
		if (data.ResultCode == Result.Success)
		{
			TTNetworkID lobbyID = data.LobbyId.ToTTID();
			CurrentLobbyData = GetPlatformLobbyData(data.LobbyId);
			CurrentLobbyData.IsLobbyMember = true;
			CurrentLobbyData.TryInitFromLobbyHandle(data.LobbyId);
			HandleLobbyJoinSuccess(lobbyID);
			foreach (PlatformLobby_EOS.LobbyMember member in CurrentLobbyData.Members)
			{
				TTNetworkID playerID = member.ProductId.ToTTID();
				if (m_NativeAccountPlatform.HasUserInformation(playerID))
				{
					continue;
				}
				m_NativeAccountPlatform.RequestUserInformation(playerID, delegate(TTNetworkID playerId)
				{
					if (base.CurrentLobby != null && base.CurrentLobby.IsUserInLobby(playerId))
					{
						base.CurrentLobby.HandleLobbyDataUpdated(wasSuccessful: true);
					}
				});
			}
		}
		else
		{
			HandleLobbyJoinFailure(data.ResultCode.ToString(), ConvertErrorCode(data.ResultCode));
		}
		m_CreatingOrJoiningLobby = false;
	}

	private void OnLeaveLobbyCompleted(ref LeaveLobbyCallbackInfo data)
	{
		if (data.ResultCode != Result.Success)
		{
			d.LogWarningFormat("Lobbies (OnLeaveLobbyCompleted): error code: {0}", data.ResultCode);
		}
		else
		{
			d.Log("Lobbies (OnLeaveLobbyCompleted): Successfully left lobby: " + data.LobbyId);
		}
		foreach (TTNetworkID openP2PConnection in m_OpenP2PConnections)
		{
			CloseConnection(openP2PConnection);
		}
		m_OpenP2PConnections.Clear();
		CurrentLobbyData.Clear();
		m_CachedLobbyDataLookup.Clear();
	}

	private void OnLobbyModifiedComplete(ref UpdateLobbyCallbackInfo data)
	{
		bool flag = data.ResultCode == Result.Success;
		if (!flag)
		{
			d.LogErrorFormat("Lobbies (OnUpdateLobbyCallBack): error code: {0}", data.ResultCode);
		}
		HandleLobbyDataUpdated_EOS(data.LobbyId.ToTTID(), flag);
	}

	private void OnLobbySearchComplete(ref LobbySearchFindCallbackInfo data)
	{
		if (data.ResultCode != Result.Success)
		{
			d.LogErrorFormat("Failed to execute lobby search! Error: {0}", data.ResultCode);
			return;
		}
		m_CachedLobbyDataLookup.Clear();
		HandleLobbyMatchListBegin();
		foreach (LobbyDetails lobby in new LobbySearchResults(data).Lobbies)
		{
			LobbyDetailsInfo detailsInfo = lobby.GetDetailsInfo();
			Utf8String lobbyId = detailsInfo.LobbyId;
			d.AssertFormat(!m_CachedLobbyDataLookup.ContainsKey(lobbyId), "Search results contained lobby twice?! {0}", lobbyId);
			PlatformLobby_EOS.LobbyData_EOS lobbyData_EOS = new PlatformLobby_EOS.LobbyData_EOS();
			lobbyData_EOS.InitFromLobbyDetails(lobby);
			m_CachedLobbyDataLookup[lobbyId] = lobbyData_EOS;
			TTNetworkID lobbyID = detailsInfo.LobbyId.ToTTID();
			HandleLobbyMatchListAddLobby(lobbyID, refreshData: true);
		}
		HandleLobbyMatchListEnd();
		if (CurrentLobbyData != null && CurrentLobbyData.IsValid())
		{
			m_CachedLobbyDataLookup[CurrentLobbyData.Id] = CurrentLobbyData;
		}
	}

	private void HandleLobbyDataUpdated_EOS(TTNetworkID lobbyID, bool success = true)
	{
		if (success && lobbyID.IsValid() && base.CurrentLobby != null && lobbyID == base.CurrentLobby.ID)
		{
			Utf8String lobbyId = lobbyID.ToEOSLobbyID();
			PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(lobbyId);
			platformLobbyData.TryInitFromLobbyHandle(platformLobbyData.Id);
		}
		if (!success && m_PendingJoinRequest != null && m_PendingJoinRequest.m_LobbyID == lobbyID)
		{
			ClientInvitationEvent.Send(m_ClientInviteStatus);
			LobbyJoinFailedEvent.Send(LobbyErrorCode.DoesntExist);
			m_PendingJoinRequest = null;
		}
		HandleLobbyDataUpdated(lobbyID, success);
	}

	private void OnLobbyAttributesChanged(ref LobbyUpdateReceivedCallbackInfo data)
	{
		d.AssertFormat(data.LobbyId.ToTTID().IsValid(), "Invalid data received in {0}", "OnLobbyAttributesChanged");
		HandleLobbyDataUpdated_EOS(data.LobbyId.ToTTID());
	}

	private void OnMemberAttributesChanged(ref LobbyMemberUpdateReceivedCallbackInfo data)
	{
		d.AssertFormat(data.LobbyId.ToTTID().IsValid(), "Invalid data received in {0}", "OnMemberAttributesChanged");
		HandleLobbyDataUpdated_EOS(data.LobbyId.ToTTID());
	}

	private void OnMemberStatusChanged(ref LobbyMemberStatusReceivedCallbackInfo data)
	{
		TTNetworkID tTNetworkID = data.LobbyId.ToTTID();
		d.AssertFormat(tTNetworkID.IsValid(), "Invalid data received in {0}", "OnMemberStatusChanged");
		if (!data.TargetUserId.IsValid())
		{
			d.LogWarning("Lobbies  (OnMemberStatusReceived): Current player is invalid!");
			HandleLobbyDataUpdated_EOS(tTNetworkID);
			return;
		}
		TTNetworkID tTNetworkID2 = data.TargetUserId.ToTTID();
		if (data.CurrentStatus == LobbyMemberStatus.Joined)
		{
			if (!m_NativeAccountPlatform.HasUserInformation(tTNetworkID2))
			{
				m_NativeAccountPlatform.RequestUserInformation(tTNetworkID2, delegate(TTNetworkID playerId)
				{
					if (base.CurrentLobby != null && base.CurrentLobby.IsUserInLobby(playerId))
					{
						base.CurrentLobby.HandleLobbyDataUpdated(wasSuccessful: true);
					}
				});
			}
			PlatformLobby_EOS.LobbyData_EOS platformLobbyData = GetPlatformLobbyData(data.LobbyId);
			platformLobbyData.TryInitFromLobbyHandle(platformLobbyData.Id);
		}
		if (data.CurrentStatus == LobbyMemberStatus.Left || data.CurrentStatus == LobbyMemberStatus.Disconnected || data.CurrentStatus == LobbyMemberStatus.Kicked || data.CurrentStatus == LobbyMemberStatus.Closed)
		{
			CloseConnection(tTNetworkID2);
		}
		if (PlatformLobby_EOS_Helpers.TryGetMemberLobbyState(data.CurrentStatus, out var outLobbyState) && base.CurrentLobby != null && base.CurrentLobby.ID == tTNetworkID)
		{
			if (tTNetworkID2 == GetLocalPlayerID() && outLobbyState == Lobby.MemberLobbyStateMask.MLS_Disconnected)
			{
				LobbyErrorEvent.Send(LobbyErrorCode.HostDisconnected);
			}
			else
			{
				base.CurrentLobby.HandleLobbyStateUpdated(tTNetworkID2, outLobbyState);
			}
		}
		else
		{
			HandleLobbyDataUpdated_EOS(tTNetworkID);
		}
	}

	private void OnLobbyJoinAccepted(ref JoinLobbyAcceptedCallbackInfo data)
	{
		CopyLobbyDetailsHandleByUiEventIdOptions options = new CopyLobbyDetailsHandleByUiEventIdOptions
		{
			UiEventId = data.UiEventId
		};
		Result result = EOSManager.Instance.GetEOSLobbyInterface().CopyLobbyDetailsHandleByUiEventId(ref options, out var outLobbyDetailsHandle);
		d.AssertFormat(result == Result.Success && outLobbyDetailsHandle != null, "Failed to Copy lobby details for Join action from Social overlay {0}", result);
		if (outLobbyDetailsHandle == null)
		{
			return;
		}
		if (result != Result.Success)
		{
			outLobbyDetailsHandle.Release();
			return;
		}
		LobbyDetailsCopyInfoOptions options2 = default(LobbyDetailsCopyInfoOptions);
		result = outLobbyDetailsHandle.CopyInfo(ref options2, out var outLobbyDetailsInfo);
		d.AssertFormat(result == Result.Success, "Failed to Copy lobby details trying to join from Social overlay: {0}", result);
		TTNetworkID lobbyID = outLobbyDetailsInfo.Value.LobbyId.ToTTID();
		if (!TrySetupPendingJoin(lobbyID, outLobbyDetailsHandle))
		{
			AcknowledgeEventIdOptions options3 = new AcknowledgeEventIdOptions
			{
				UiEventId = data.UiEventId,
				Result = Result.NotFound
			};
			EOSManager.Instance.GetEOSUIInterface().AcknowledgeEventId(ref options3);
		}
	}

	private void OnLobbyInviteAccepted(ref LobbyInviteAcceptedCallbackInfo data)
	{
		CopyLobbyDetailsHandleByInviteIdOptions options = new CopyLobbyDetailsHandleByInviteIdOptions
		{
			InviteId = data.InviteId
		};
		Result result = EOSManager.Instance.GetEOSLobbyInterface().CopyLobbyDetailsHandleByInviteId(ref options, out var outLobbyDetailsHandle);
		d.AssertFormat(result == Result.Success && outLobbyDetailsHandle != null, "Failed to Copy lobby details for Invite accept action from Social overlay: {0}", result);
		if (!(outLobbyDetailsHandle == null))
		{
			if (result != Result.Success)
			{
				outLobbyDetailsHandle.Release();
				return;
			}
			LobbyDetailsCopyInfoOptions options2 = default(LobbyDetailsCopyInfoOptions);
			result = outLobbyDetailsHandle.CopyInfo(ref options2, out var outLobbyDetailsInfo);
			d.AssertFormat(result == Result.Success, "Failed to Copy lobby details trying to Accept invite from Social overlay: {0}", result);
			TTNetworkID lobbyID = outLobbyDetailsInfo.Value.LobbyId.ToTTID();
			TrySetupPendingJoin(lobbyID, outLobbyDetailsHandle);
		}
	}

	public void TryJoinLobbyFromMirrorInvite(TTNetworkID eosLobbyID)
	{
		SearchForLobbies(delegate(LobbySearch search)
		{
			LobbySearchSetLobbyIdOptions options = new LobbySearchSetLobbyIdOptions
			{
				LobbyId = eosLobbyID.ToEOSLobbyID()
			};
			search.SetLobbyId(ref options);
		}, delegate(ref LobbySearchFindCallbackInfo data)
		{
			LobbySearchResults lobbySearchResults = new LobbySearchResults(data);
			bool flag = false;
			using (IEnumerator<LobbyDetails> enumerator = lobbySearchResults.Lobbies.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					LobbyDetails current = enumerator.Current;
					TrySetupPendingJoin(eosLobbyID, current);
					flag = true;
				}
			}
			if (!flag)
			{
				d.LogError("Failed to find EOS lobby from Steam invite. It may no longer exist!");
				Singleton.Manager<ManNetworkLobby>.inst.ShowLobbyError(LobbyErrorCode.DoesntExist, returnToAttract: true);
			}
		});
	}

	private bool TrySetupPendingJoin(TTNetworkID lobbyID, LobbyDetails lobbyDetails)
	{
		if (base.CurrentLobby == null || lobbyID != base.CurrentLobby.ID)
		{
			Utf8String key = lobbyID.ToEOSLobbyID();
			m_PendingJoinRequest = new LobbyJoinRequest(lobbyID, fromInvite: true);
			PlatformLobby_EOS.LobbyData_EOS lobbyData_EOS = new PlatformLobby_EOS.LobbyData_EOS();
			lobbyData_EOS.InitFromLobbyDetails(lobbyDetails);
			m_CachedLobbyDataLookup[key] = lobbyData_EOS;
			AddOrUpdateLobbyInList(lobbyID, refreshData: true, fromInvite: true);
			return true;
		}
		return false;
	}

	private void OnLeaveLobbyRequested(ref LeaveLobbyRequestedCallbackInfo data)
	{
		d.Assert(base.CurrentLobby != null && base.CurrentLobby.ID == data.LobbyId.ToTTID(), "Requested to leave the Current Party, but the player either wasn't in one, or in a different one than expected!");
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeAttract>())
		{
			LeaveLobby();
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MainMenu);
		}
		else
		{
			ManSaveGame.ShouldStore = false;
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManNetwork>.inst.IgnoreServerDisconnect = true;
			LeaveLobby();
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		}
	}

	private void OnNetPlayerAdded(NetPlayer netPlayer)
	{
		if (netPlayer.IsActuallyLocalPlayer)
		{
			string userName = GetUserName(GetLocalPlayerID());
			netPlayer.SetName(userName);
		}
	}

	private bool IsEventHandleValid(NotifyEventHandle handle)
	{
		return handle?.IsValid() ?? false;
	}

	private void SubscribeToNotifications()
	{
		d.Assert(m_NotifyEventHandles.Count == 0, "Lobbies (SubscribeToLobbyUpdates): SubscribeToLobbyUpdates called but already subscribed!");
		LobbyInterface eOSLobbyInterface = EOSManager.Instance.GetEOSLobbyInterface();
		AddNotifyLobbyUpdateReceivedOptions options = default(AddNotifyLobbyUpdateReceivedOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSLobbyInterface.AddNotifyLobbyUpdateReceived(ref options, null, OnLobbyAttributesChanged), eOSLobbyInterface.RemoveNotifyLobbyUpdateReceived));
		AddNotifyLobbyMemberUpdateReceivedOptions options2 = default(AddNotifyLobbyMemberUpdateReceivedOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSLobbyInterface.AddNotifyLobbyMemberUpdateReceived(ref options2, null, OnMemberAttributesChanged), eOSLobbyInterface.RemoveNotifyLobbyMemberUpdateReceived));
		AddNotifyLobbyMemberStatusReceivedOptions options3 = default(AddNotifyLobbyMemberStatusReceivedOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSLobbyInterface.AddNotifyLobbyMemberStatusReceived(ref options3, null, OnMemberStatusChanged), eOSLobbyInterface.RemoveNotifyLobbyMemberStatusReceived));
		AddNotifyJoinLobbyAcceptedOptions options4 = default(AddNotifyJoinLobbyAcceptedOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSLobbyInterface.AddNotifyJoinLobbyAccepted(ref options4, null, OnLobbyJoinAccepted), eOSLobbyInterface.RemoveNotifyJoinLobbyAccepted));
		AddNotifyLobbyInviteAcceptedOptions options5 = default(AddNotifyLobbyInviteAcceptedOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSLobbyInterface.AddNotifyLobbyInviteAccepted(ref options5, null, OnLobbyInviteAccepted), eOSLobbyInterface.RemoveNotifyLobbyInviteAccepted));
		AddNotifyLeaveLobbyRequestedOptions options6 = default(AddNotifyLeaveLobbyRequestedOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSLobbyInterface.AddNotifyLeaveLobbyRequested(ref options6, null, OnLeaveLobbyRequested), eOSLobbyInterface.RemoveNotifyLeaveLobbyRequested));
		P2PInterface eOSP2PInterface = EOSManager.Instance.GetEOSP2PInterface();
		AddNotifyPeerConnectionRequestOptions options7 = new AddNotifyPeerConnectionRequestOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			SocketId = null
		};
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSP2PInterface.AddNotifyPeerConnectionRequest(ref options7, null, OnP2PConnectionRequest), eOSP2PInterface.RemoveNotifyPeerConnectionRequest));
		AddNotifyPeerConnectionEstablishedOptions options8 = new AddNotifyPeerConnectionEstablishedOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			SocketId = null
		};
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSP2PInterface.AddNotifyPeerConnectionEstablished(ref options8, null, OnP2PConnectionEstablished), eOSP2PInterface.RemoveNotifyPeerConnectionEstablished));
		AddNotifyPeerConnectionInterruptedOptions options9 = new AddNotifyPeerConnectionInterruptedOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			SocketId = null
		};
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSP2PInterface.AddNotifyPeerConnectionInterrupted(ref options9, null, OnP2PConnectionInterrupted), eOSP2PInterface.RemoveNotifyPeerConnectionInterrupted));
		AddNotifyPeerConnectionClosedOptions options10 = new AddNotifyPeerConnectionClosedOptions
		{
			LocalUserId = EOSManager.Instance.GetProductUserId(),
			SocketId = null
		};
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSP2PInterface.AddNotifyPeerConnectionClosed(ref options10, null, OnP2PConnectionLost), eOSP2PInterface.RemoveNotifyPeerConnectionClosed));
		AddNotifyIncomingPacketQueueFullOptions options11 = default(AddNotifyIncomingPacketQueueFullOptions);
		m_NotifyEventHandles.Add(new NotifyEventHandle(eOSP2PInterface.AddNotifyIncomingPacketQueueFull(ref options11, null, OnP2PPacketQueueFull), eOSP2PInterface.RemoveNotifyIncomingPacketQueueFull));
		Singleton.Manager<ManEOS>.inst.BeforeLogoutEvent.Subscribe(OnBeforeLogout);
	}

	private void OnBeforeLogout()
	{
		if (base.CurrentLobby != null && base.CurrentLobby.IsLobbyOwner())
		{
			LeaveLobby();
		}
		m_CachedLobbyDataLookup.Clear();
		UnsubscribeFromLobbyUpdates();
		foreach (EOSAccountPlatform_Base value in m_AccountPlatforms.Values)
		{
			value.Shutdown();
		}
		m_AccountPlatforms.Clear();
		m_NativeAccountPlatform = null;
	}

	private void UnsubscribeFromLobbyUpdates()
	{
		foreach (NotifyEventHandle notifyEventHandle in m_NotifyEventHandles)
		{
			notifyEventHandle.Dispose();
		}
	}
}
