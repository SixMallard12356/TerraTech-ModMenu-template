#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Netease.Oddish.Ingame.Sdk;
using Netease.Oddish.Ingame.Sdk.Entity.Matchmake;
using Netease.Oddish.Ingame.Sdk.Entity.Networking;
using Netease.Oddish.Ingame.Sdk.Task;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public class PlatformLobbySystem_NetEase : LobbySystem
{
	public class LobbyData_NetEase
	{
		private int m_SlotLimit;

		private int m_SlotCount;

		private Action<TTNetworkID, bool> m_OnUpdateComplete;

		private Dictionary<string, string> m_LobbyDataCache;

		private string m_rawLobbyId;

		private TTNetworkID m_LobbyId;

		private bool m_UpdatingMembers;

		private List<string> m_Members = new List<string>();

		private bool m_LockDataRequestsOnJoin;

		private List<string> m_PendingData = new List<string>();

		public int SlotLimit => int.Parse(GetCachedLobbyData("maxPlayers", "4"));

		public TTNetworkID LobbyId => m_LobbyId;

		public List<string> KnownMembers => m_Members;

		public int MemberCount => int.Parse(GetCachedLobbyData("lobbyMemberCount", "0"));

		public string LobbyOwner => GetCachedLobbyData("ownerID");

		public LobbyData_NetEase(TTNetworkID lobbyID, bool updateData, string ownerID = "")
		{
			Debug.Log("[LobbyData_NetEase] Creating new Data object for lobby " + lobbyID);
			m_rawLobbyId = lobbyID.m_NetworkID.ToString();
			m_LobbyId = lobbyID;
			m_LobbyDataCache = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(ownerID))
			{
				Debug.Log("[LobbyData_NetEase] Setting Owner ID on creation to " + ownerID);
				m_LobbyDataCache["ownerID"] = ownerID;
				m_Members.Add(ownerID);
				m_LobbyDataCache["lobbyMemberCount"] = "1";
			}
			if (updateData)
			{
				UpdateInternalData();
			}
		}

		public void AddMember(string id)
		{
			if (!m_Members.Contains(id))
			{
				m_LobbyDataCache["lobbyMemberCount"] = (MemberCount + 1).ToString();
				m_Members.Add(id);
			}
		}

		private void RequestLobbyData(string key)
		{
			d.LogFormat("[LobbyData_NetEase] RequestLobbyData({0}) b", key);
			if (!m_LockDataRequestsOnJoin && !m_PendingData.Contains(key))
			{
				m_PendingData.Add(key);
				try
				{
					OddishSdk.Matchmake.CreateGetLobbyData().ExecAsync(m_rawLobbyId, key, OnReceiveData, delegate(GetLobbyDataTask.FailResult result)
					{
						d.LogWarningFormat("[LobbyData_NetEase] failed to request lobby data {0}, reason {1}", key, result);
					});
				}
				catch (Exception ex)
				{
					d.LogErrorFormat("[LobbyData_NetEase] Exception while updating lobby data {0}" + ex);
				}
			}
			d.LogFormat("[LobbyData_NetEase] RequestLobbyData({0}) e", key);
		}

		public string GetCachedLobbyData(string key, string defaultValue = "", bool requestIfMissing = true)
		{
			if (m_LobbyDataCache.ContainsKey(key))
			{
				return m_LobbyDataCache[key];
			}
			if (requestIfMissing)
			{
				RequestLobbyData(key);
			}
			return defaultValue;
		}

		public void SetAsJoining()
		{
			m_LockDataRequestsOnJoin = true;
		}

		public void SetLobbyJoined()
		{
			m_LockDataRequestsOnJoin = false;
		}

		public void UpdateLobby(Action<TTNetworkID, bool> onUpdateComplete = null)
		{
			if (!m_LockDataRequestsOnJoin)
			{
				UpdateInternalData(onUpdateComplete);
			}
			else
			{
				onUpdateComplete?.Invoke(LobbyId, arg2: true);
			}
		}

		private void UpdateInternalData(Action<TTNetworkID, bool> onUpdateComplete = null)
		{
			Debug.Log("[LobbyData_NetEase] Updating internal data for " + LobbyId);
			RequestLobbyData("ownerID");
			RequestLobbyData("playerTeams");
			RequestLobbyData("choices");
			RequestLobbyData("maxPlayers");
			RequestLobbyData("lobbyMemberCount");
			m_OnUpdateComplete = onUpdateComplete;
		}

		private void OnReceiveData(GetLobbyDataTask.SuccessResult dataResult)
		{
			if (m_LockDataRequestsOnJoin || !LobbyId.ToString().Equals(dataResult.LobbyId))
			{
				return;
			}
			Debug.Log("NetEase - OnReceiveData. Updating Data. " + dataResult.Key + "=" + dataResult.Value);
			if (m_PendingData.Contains(dataResult.Key))
			{
				m_PendingData.Remove(dataResult.Key);
				if (m_PendingData.Count == 0)
				{
					CompleteInternalUpdate();
				}
			}
			if (m_LobbyDataCache == null)
			{
				m_LobbyDataCache = new Dictionary<string, string>();
			}
			m_LobbyDataCache[dataResult.Key] = dataResult.Value;
		}

		private void CompleteInternalUpdate()
		{
			Singleton.Manager<ManNetEase>.inst.AddCallback(delegate
			{
				m_OnUpdateComplete?.Invoke(LobbyId, arg2: true);
			});
			m_OnUpdateComplete = null;
		}

		public void SetDataDirect(string keyName, string value)
		{
			m_LobbyDataCache[keyName] = value;
		}

		public void SetDataDirectBatch(Netease.Oddish.Ingame.Sdk.Entity.Matchmake.LobbyData[] data)
		{
			foreach (Netease.Oddish.Ingame.Sdk.Entity.Matchmake.LobbyData lobbyData in data)
			{
				SetDataDirect(lobbyData.Key, lobbyData.Value);
			}
		}
	}

	private bool m_Inited;

	private PlatformLobby_NetEase m_ActiveLobby;

	private bool m_RefreshingLobbyData;

	private int m_LobbyRefreshTargetCount;

	private Queue<Action> m_QueuedActions;

	private bool m_IsJoinInProgress;

	private LobbyData_NetEase m_CurrentLobbyCache;

	private ConnectionConfig m_ConnectionConfig;

	private Dictionary<TTNetworkID, float> m_PendingP2PClosures;

	private int m_ChannelReliable;

	private int m_ChannelUnreliable = 1;

	private int m_ChannelPing = 2;

	private const byte PING_ENQ = 5;

	private const byte PING_ACK = 6;

	private byte[] m_PingBuffer = new byte[1];

	private readonly Dictionary<TTNetworkID, LobbyData_NetEase> m_CurrentLobbies = new Dictionary<TTNetworkID, LobbyData_NetEase>();

	private volatile bool m_EstablishingP2PConnection;

	private volatile Queue<Action> m_PendingEstablishActions;

	private bool m_SendingPingResponse;

	private ClientInviteStatus m_ClientInviteStatus = new ClientInviteStatus();

	public override bool Init(LobbyConstants constants, GameStateQuerier gameStateQuerier)
	{
		base.Init(constants, gameStateQuerier);
		d.Assert(!SKU.IsNetEase || Singleton.Manager<ManNetEase>.inst.Inited, "[PlatformLobbySystem_NetEase] Can't initialise lobby system before ManNetEase");
		if (!Singleton.Manager<ManNetEase>.inst.Inited || !SKU.IsNetEase)
		{
			return false;
		}
		m_QueuedActions = new Queue<Action>();
		OddishSdk.P2pSessionEstablishRequested += OnEstablishP2PConnection;
		OddishSdk.P2pSessionEstablishFailed += OnFailP2PConnection;
		m_PendingEstablishActions = new Queue<Action>();
		m_PendingP2PClosures = new Dictionary<TTNetworkID, float>();
		m_ConnectionConfig = new ConnectionConfig();
		m_ConnectionConfig.PacketSize = 1200;
		m_ChannelReliable = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_ChannelUnreliable = m_ConnectionConfig.AddChannel(QosType.Unreliable);
		m_ChannelPing = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_Inited = true;
		Debug.Log("[PlatformLobbySystem_NetEase] Finished Initialising Lobby System");
		return true;
	}

	private void OnEstablishP2PConnection(P2pSessionEstablishRequestedNotification p2pEstablishRequest)
	{
		Debug.Log("[PlatformLobbySystem_NetEase] Received P2P Establish request from " + p2pEstablishRequest.RemoteUserId);
		lock (m_PendingEstablishActions)
		{
			m_PendingEstablishActions.Enqueue(delegate
			{
				SendP2pEstablishment(p2pEstablishRequest);
			});
		}
	}

	private void SendP2pEstablishment(P2pSessionEstablishRequestedNotification p2pEstablishRequest)
	{
		bool flag = false;
		if (base.CurrentLobby != null)
		{
			foreach (LobbyPlayerData player in base.CurrentLobby.GetPlayerList())
			{
				if (player.m_PlayerID.ToString() == p2pEstablishRequest.RemoteUserId)
				{
					flag = true;
					break;
				}
			}
		}
		if (flag)
		{
			d.LogFormat("[PlatformLobbySystem_NetEase] Establishing P2P connection with {0}...", p2pEstablishRequest.RemoteUserId);
			m_EstablishingP2PConnection = true;
			try
			{
				OddishSdk.Networking.CreateAcceptP2pSessionRequest().ExecAsync(p2pEstablishRequest.RemoteUserId, delegate
				{
					Debug.Log("[PlatformLobbySystem_NetEase] Established P2P connection with " + p2pEstablishRequest.RemoteUserId);
					m_EstablishingP2PConnection = false;
				}, delegate(AcceptP2pSessionRequestTask.FailResult onFailure)
				{
					Debug.LogError(string.Concat("PlatformLobbySystem_NetEase.EstablishP2PConnection - Failed to establish connection. Error code ", onFailure.Code, " and message=", onFailure.Message));
					m_EstablishingP2PConnection = false;
				});
				return;
			}
			catch (Exception ex)
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] Exception establishing p2p with {0}: {1}", p2pEstablishRequest.RemoteUserId, ex);
				m_EstablishingP2PConnection = false;
				return;
			}
		}
		d.LogWarningFormat("Not Establishing connection with user {0} because they or we are not in the lobby", p2pEstablishRequest.RemoteUserId);
	}

	private void OnFailP2PConnection(P2pSessionEstablishFailedNotification p2pEstablishFail)
	{
		Debug.LogError(string.Concat("PlatformLobbySystem_NetEase.EstablishP2PConnection - Failed to establish connection. Error code ", p2pEstablishFail.SessionError, " to user ", p2pEstablishFail.RemoteUserId));
	}

	protected override TerraTech.Network.Lobby Platform_CreateLobbyObject(TerraTech.Network.LobbyData data, MultiplayerModeType gameType)
	{
		m_ActiveLobby = ((SKU.IsNetEase && Singleton.Manager<ManNetEase>.inst.Inited) ? new PlatformLobby_NetEase(this, data, m_ConnectionConfig, gameType, m_CurrentLobbyCache) : null);
		return m_ActiveLobby;
	}

	public override bool IsJoinOrCreateLobbyRequestActive()
	{
		return m_IsJoinInProgress;
	}

	public override void JoinLobbyAfterUpdate(TTNetworkID lobbyID)
	{
		Debug.Log(string.Concat("[PlatformLobbySystem_NetEase] Starting to join lobby ", lobbyID, " after update"));
		InternalJoinLobby(lobbyID, fromInvite: false);
	}

	protected override void Platform_CreateLobby(MultiplayerModeType gameType, TerraTech.Network.Lobby.LobbyVisibility visibility, int maxPlayerCount)
	{
		Debug.Log($"[PlatformLobbySystem_NetEase] Creating Lobby with maxPlayers {maxPlayerCount} and lobby Type {gameType}");
		LobbyEventSet handlerSet = CreateTTLobbyHandlerSet();
		OddishSdk.Matchmake.CreateCreateLobby().ExecAsync(1, maxPlayerCount, delegate(CreateLobbyTask.SuccessResult onSuccess)
		{
			Debug.Log("[PlatformLobbySystem_NetEase] Created Lobby with id " + onSuccess.LobbyId);
			TTNetworkID tTNetworkID = new TTNetworkID(onSuccess.LobbyId);
			m_CurrentLobbyCache = new LobbyData_NetEase(tTNetworkID, updateData: false, Singleton.Manager<ManNetEase>.inst.PlayerId);
			m_CurrentLobbyCache.SetAsJoining();
			m_CurrentLobbies.Add(tTNetworkID, m_CurrentLobbyCache);
			LobbyTypeEnum lobbyType = LobbyTypeEnum.Public;
			switch (visibility)
			{
			case TerraTech.Network.Lobby.LobbyVisibility.Private:
				lobbyType = LobbyTypeEnum.Private;
				break;
			case TerraTech.Network.Lobby.LobbyVisibility.FriendsOnly:
				lobbyType = LobbyTypeEnum.FriendsOnly;
				break;
			case TerraTech.Network.Lobby.LobbyVisibility.Public:
				lobbyType = LobbyTypeEnum.Public;
				break;
			}
			OddishSdk.Matchmake.CreateSetLobbyType().ExecAsync(onSuccess.LobbyId, lobbyType, delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to set lobby type to {0}", onSuccess.LobbyId);
			});
			OddishSdk.Matchmake.CreateSetLobbyJoinable().ExecAsync(onSuccess.LobbyId, LobbyStatusEnum.Opened, delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to set lobby joinable on {0}", onSuccess.LobbyId);
			});
			OddishSdk.Matchmake.CreateSetLobbyTag().ExecAsync(onSuccess.LobbyId, "gameSetup", "true", delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to set lobby tag on {0}", onSuccess.LobbyId);
			});
			OddishSdk.Matchmake.CreateSetLobbyData().ExecAsync(onSuccess.LobbyId, "lobbyPublic", visibility.ToString(), LobbyDataScopeEnum.Public, delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to set lobby visibility data on {0}", onSuccess.LobbyId);
			});
			OddishSdk.Matchmake.CreateSetLobbyData().ExecAsync(onSuccess.LobbyId, "maxPlayers", maxPlayerCount.ToString(), LobbyDataScopeEnum.Public, delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to set max player count tag on {0}", onSuccess.LobbyId);
			});
			OddishSdk.Matchmake.CreateSetLobbyData().ExecAsync(onSuccess.LobbyId, "lobbyMemberCount", "1", LobbyDataScopeEnum.Public, delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to set member count on {0}", onSuccess.LobbyId);
			});
			m_QueuedActions.Enqueue(delegate
			{
				HandleLobbyCreationSuccess(new TTNetworkID(onSuccess.LobbyId));
			});
		}, handlerSet, delegate(CreateLobbyTask.FailResult onFailure)
		{
			Debug.LogError($"PlatformLobbySystem_NetEase.Platform_CreateLobby - Encountered error while creating lobby. Code {onFailure.Code} with message {onFailure.Message}");
			m_QueuedActions.Enqueue(delegate
			{
				HandleLobbyCreationFailure(onFailure.Code.ToString(), LobbyErrorCode.None);
			});
		});
	}

	private LobbyEventSet CreateTTLobbyHandlerSet()
	{
		Debug.Log("[PlatformLobbySystem_NetEase] Creating LobbyHandlerSet");
		LobbyEventSet lobbyEventSet = new LobbyEventSet();
		lobbyEventSet.MemberEntered += delegate(MemberEnteredNotification member)
		{
			d.Log("[LobbyHandlerSet] Member Entered=" + member.MemberId);
			if (m_ActiveLobby != null)
			{
				m_QueuedActions.Enqueue(delegate
				{
					m_ActiveLobby.AddMember(member);
				});
			}
			else
			{
				m_CurrentLobbyCache.AddMember(member.MemberId);
			}
		};
		lobbyEventSet.MemberDisconnected += delegate(MemberDisconnectedNotification member)
		{
			d.Log("[LobbyHandlerSet] Member Disconnected: " + member);
		};
		lobbyEventSet.OwnerChanged += delegate(OwnerChangedNotification owner)
		{
			d.Log("[LobbyHandlerSet] Owner Changed=" + owner.CurrentOwnerMemberId + " from=" + owner.PreviousOwnerMemberId);
			m_ActiveLobby?.SetLobbyOwner(owner.CurrentOwnerMemberId);
		};
		lobbyEventSet.LobbyTagsUpdated += delegate
		{
			d.Log("[LobbyHandlerSet] Tags Updated");
		};
		lobbyEventSet.LobbyJoinableUpdated += delegate
		{
			d.Log("LobbyJoinable Updated");
		};
		lobbyEventSet.LobbyDataUpdated += delegate(LobbyDataUpdatedNotification data)
		{
			d.Log("[LobbyHandler Set] LobbyDataUpdated: " + data);
			if (m_CurrentLobbyCache.LobbyId.ToString().Equals(data.LobbyId))
			{
				m_CurrentLobbyCache.SetDataDirectBatch(data.LobbyData);
				m_QueuedActions.Enqueue(delegate
				{
					m_ActiveLobby.HandleLobbyDataUpdated(wasSuccessful: true);
				});
			}
		};
		lobbyEventSet.LobbyGameServerUpdated += delegate(LobbyGameServerUpdatedNotification a)
		{
			d.Log("[LobbyHandlerSet] LobbyGameServerUpdated: " + a);
		};
		lobbyEventSet.LobbyGameServerHeartbeatTimeout += delegate(LobbyGameServerHeartbeatTimeoutNotification a)
		{
			d.Log("[LobbyHandlerSet] LobbyGameServerHeartbeatTimeout: " + a);
		};
		lobbyEventSet.LobbyChatMessageUpdated += delegate(LobbyChatMessageUpdatedNotification msg)
		{
			d.Log("[PlatformLobbySystem_NetEase] Received Chat Message=" + msg.Message.Content);
			if (m_ActiveLobby != null)
			{
				m_QueuedActions.Enqueue(delegate
				{
					m_ActiveLobby.OnChatMessageReceived(msg);
				});
			}
		};
		lobbyEventSet.MemberKicked += delegate(MemberKickedNotification member)
		{
			d.Log("[LobbyHandlerSet] MemberKicked: " + member);
			if (m_ActiveLobby != null)
			{
				m_QueuedActions.Enqueue(delegate
				{
					m_ActiveLobby.HandleMemberKicked(member);
				});
			}
		};
		lobbyEventSet.MemberLeft += delegate(MemberLeftNotification member)
		{
			d.Log("[LobbyHandlerSet] MemberLeft: " + member);
			if (m_ActiveLobby != null)
			{
				m_QueuedActions.Enqueue(delegate
				{
					m_ActiveLobby.RemoveMember(member.MemberId);
				});
			}
		};
		lobbyEventSet.LobbyTypeUpdated += delegate(LobbyTypeUpdatedNotification lobby)
		{
			d.Log("Lobby type updated to " + lobby.LobbyType);
		};
		lobbyEventSet.LobbyMemberDataUpdated += delegate(LobbyMemberDataUpdatedNotification data)
		{
			d.Log("[LobbyHandlerSet] LobbyMemberDataUpdated: " + data);
			if (m_ActiveLobby != null)
			{
				m_QueuedActions.Enqueue(delegate
				{
					m_ActiveLobby.OnLobbyMemberDataUpdated(data);
				});
			}
		};
		lobbyEventSet.MemberReconnected += delegate(MemberReconnectedNotification data)
		{
			d.Log("[LobbyHandlerSet] MemberReconnected: " + data);
		};
		return lobbyEventSet;
	}

	protected override void Platform_JoinLobby(TTNetworkID lobbyID, bool fromInvite)
	{
		m_IsJoinInProgress = true;
		InternalJoinLobby(lobbyID, fromInvite);
	}

	private void InternalJoinLobby(TTNetworkID lobbyID, bool fromInvite)
	{
		m_IsJoinInProgress = true;
		LobbyEventSet handlerSet = CreateTTLobbyHandlerSet();
		OddishSdk.Matchmake.CreateJoinLobby().ExecAsync(lobbyID.ToString(), delegate(JoinLobbyTask.SuccessResult onJoin)
		{
			Debug.Log("[PlatformLobbySystem_NetEase] Joined lobby " + lobbyID);
			Debug.Log("[PlatformLobbySystem_NetEase] onJoin: LobbyId=" + onJoin.LobbyId + " MatchmakeId=" + onJoin.MatchmakeId + " MemberId=" + onJoin.MemberId + " TicketIds='" + string.Join(";", onJoin.TicketIds) + "'");
			m_CurrentLobbyCache = (fromInvite ? new LobbyData_NetEase(lobbyID, updateData: true) : m_CurrentLobbies[lobbyID]);
			m_CurrentLobbyCache.SetAsJoining();
			OddishSdk.Matchmake.CreateGetLobbyMembers().ExecAsync(lobbyID.ToString(), delegate(GetLobbyMembersTask.SuccessResult onGetMembers)
			{
				Debug.Log($"[PlatformLobbySystem_NetEase] onGetMembers: LobbyId={onGetMembers.LobbyId} LobbyMembers#={onGetMembers.LobbyMembers.Length}");
				m_IsJoinInProgress = false;
				Member[] lobbyMembers = onGetMembers.LobbyMembers;
				foreach (Member member in lobbyMembers)
				{
					Debug.Log("[PlatformLobbySystem_NetEase] onGetMembers: UserId=" + member.UserId + " EntityId=" + member.EntityId);
					m_CurrentLobbyCache.AddMember(member.EntityId);
				}
				if (onGetMembers.LobbyMembers.Length == 0)
				{
					Debug.LogError("PlatformLobbySystem_NetEase.CreateGetLobbyMembers - Empty lobby found");
					m_QueuedActions.Enqueue(delegate
					{
						HandleLobbyJoinFailure("Empty Lobby", LobbyErrorCode.None);
					});
				}
				else
				{
					m_QueuedActions.Enqueue(delegate
					{
						SetupLobbyData(lobbyID);
						HandleLobbyJoinSuccess(lobbyID);
						m_CurrentLobbyCache.SetLobbyJoined();
					});
				}
			}, delegate(GetLobbyMembersTask.FailResult onFailedGetMembers)
			{
				Debug.LogError($"PlatformLobbySystem_NetEase.CreateGetLobbyMembers - Failed to join lobby with error code {onFailedGetMembers.Code} with message {onFailedGetMembers.Message}");
				m_IsJoinInProgress = false;
				m_QueuedActions.Enqueue(delegate
				{
					HandleLobbyJoinFailure(onFailedGetMembers.Code.ToString(), LobbyErrorCode.None);
				});
			});
		}, handlerSet, delegate(JoinLobbyTask.FailResult onFailedJoin)
		{
			Debug.LogError($"PlatformLobbySystem_NetEase.JoinLobbyAfterUpdate - Failed to join lobby with error code {onFailedJoin.Code} with message {onFailedJoin.Message}");
			m_IsJoinInProgress = false;
			m_QueuedActions.Enqueue(delegate
			{
				HandleLobbyJoinFailure(onFailedJoin.Code.ToString(), LobbyErrorCode.None);
			});
		});
	}

	protected override void Platform_LeaveLobby(TTNetworkID lobbyID)
	{
		Debug.Log($"[PlatformLobbySystem_NetEase] Starting to leave party {lobbyID}");
		if (!Singleton.Manager<ManNetEase>.inst.IsDisposed)
		{
			OddishSdk.Matchmake.CreateLeaveLobby().ExecAsync(lobbyID.ToString(), delegate
			{
				d.LogErrorFormat("[PlatformLobbySystem_NetEase] failed to leave lobby {0}", lobbyID);
			});
		}
		m_ActiveLobby = null;
		m_CurrentLobbyCache = null;
		m_CurrentLobbies.Clear();
	}

	public override string GetUserName(TTNetworkID playerID)
	{
		return m_ActiveLobby.GetPlayerName(playerID);
	}

	public override void Platform_GetUserName(TTNetworkID playerID, Action<TTNetworkID, string> onUsernameRetrieved)
	{
		onUsernameRetrieved(playerID, GetUserName(playerID));
	}

	public override bool HasFriend(TTNetworkID playerID)
	{
		bool flag = false;
		for (int i = 0; i < Singleton.Manager<ManNetEase>.inst.Friends.Length; i++)
		{
			flag |= Singleton.Manager<ManNetEase>.inst.Friends[i].Equals(playerID.ToString());
		}
		return flag;
	}

	public override TTNetworkID GetLocalPlayerID()
	{
		return Singleton.Manager<ManNetEase>.inst.PlayerNetworkID;
	}

	protected override void Platform_RequestLobbyData(TTNetworkID lobbyID)
	{
		if (m_CurrentLobbies.ContainsKey(lobbyID))
		{
			m_CurrentLobbies[lobbyID].UpdateLobby(base.HandleLobbyDataUpdated);
		}
	}

	public override string GetLobbyData(TTNetworkID lobbyID, string keyName)
	{
		if (m_CurrentLobbies.ContainsKey(lobbyID))
		{
			return m_CurrentLobbies[lobbyID].GetCachedLobbyData(keyName);
		}
		return string.Empty;
	}

	public override int GetNumLobbyMembers(TTNetworkID lobbyID)
	{
		if (m_ActiveLobby != null && m_ActiveLobby.ID == lobbyID)
		{
			return m_ActiveLobby.MemberIDCount;
		}
		if (m_CurrentLobbies.ContainsKey(lobbyID))
		{
			return m_CurrentLobbies[lobbyID].MemberCount;
		}
		return -1;
	}

	public override int GetLobbyMemberLimit(TTNetworkID lobbyID)
	{
		if (m_CurrentLobbies.ContainsKey(lobbyID))
		{
			return m_CurrentLobbies[lobbyID].SlotLimit;
		}
		return -1;
	}

	public override int GetLobbyNumFriends(TTNetworkID lobbyID)
	{
		if (m_ActiveLobby != null && m_ActiveLobby.ID == lobbyID)
		{
			return m_ActiveLobby.GetFriendCount();
		}
		return 0;
	}

	public override TTNetworkID GetLobbyMemberByIndex(TTNetworkID lobbyID, int idx)
	{
		if (m_ActiveLobby != null && m_ActiveLobby.ID == lobbyID)
		{
			return m_ActiveLobby.GetMemberAtIndex(idx);
		}
		return TTNetworkID.Invalid;
	}

	public override List<TTNetworkID> GetPlayersThatAreTalking()
	{
		return new List<TTNetworkID>(0);
	}

	public override void MuteNetworkPlayer(TTNetworkID playerID, bool mute)
	{
	}

	public override void GlobalMuteAll(bool mute)
	{
	}

	protected override void Platform_SendPingRequest(TTNetworkID hostID)
	{
	}

	private void AddNetEaseLobbies(string[] lobbies)
	{
		for (int i = 0; i < lobbies.Length; i++)
		{
			TTNetworkID tTNetworkID = new TTNetworkID(lobbies[i]);
			if (!m_CurrentLobbies.ContainsKey(tTNetworkID))
			{
				m_CurrentLobbies.Add(tTNetworkID, new LobbyData_NetEase(tTNetworkID, updateData: true));
			}
			HandleLobbyMatchListAddLobby(tTNetworkID, refreshData: false);
		}
		HandleLobbyMatchListEnd();
	}

	public override void Update()
	{
		if (!m_EstablishingP2PConnection)
		{
			lock (m_PendingEstablishActions)
			{
				if (m_PendingEstablishActions.Count > 0)
				{
					m_PendingEstablishActions.Dequeue()();
				}
			}
		}
		if (m_QueuedActions.Count > 0)
		{
			while (m_QueuedActions.Count > 0)
			{
				m_QueuedActions.Dequeue()();
			}
		}
		if (m_Inited && SKU.IsNetEase && OddishSdk.HasInit)
		{
			ReadAllNetworkData();
		}
		List<TTNetworkID> list = new List<TTNetworkID>();
		foreach (KeyValuePair<TTNetworkID, float> pendingP2PClosure in m_PendingP2PClosures)
		{
			if (Time.time > pendingP2PClosure.Value)
			{
				CloseTTNetworkConnection(pendingP2PClosure.Key);
				list.Add(pendingP2PClosure.Key);
			}
		}
		if (list.Count > 0)
		{
			foreach (TTNetworkID item in list)
			{
				m_PendingP2PClosures.Remove(item);
			}
		}
		TTNetworkConnectionImplNetEase.StaticUpdate();
		if (Singleton.Manager<ManNetEase>.inst.PendingLobbyJoin != null)
		{
			m_ClientInviteStatus.readyToJoin = false;
			ClientInvitationEvent.Send(m_ClientInviteStatus);
			if (m_ClientInviteStatus.readyToJoin)
			{
				d.LogFormat("[PlatformLobbySystem_NetEase] Handling invite to lobby {0}", Singleton.Manager<ManNetEase>.inst.PendingLobbyJoin);
				AddLobbyToList(new TTNetworkID(Singleton.Manager<ManNetEase>.inst.PendingLobbyJoin), refreshData: true, fromInvite: true);
				JoinLobby(new TTNetworkID(Singleton.Manager<ManNetEase>.inst.PendingLobbyJoin), fromInvite: true);
				Singleton.Manager<ManNetEase>.inst.ClearPendingLobbyJoin();
			}
		}
		base.Update();
	}

	private void CloseTTNetworkConnection(TTNetworkID userToClose)
	{
		if (!(userToClose != Singleton.Manager<ManNetEase>.inst.PlayerNetworkID))
		{
			return;
		}
		Debug.Log("[PlatformLobbySystem_NetEase] CreateCloseP2pSessionWithUser Attempting to close P2P session with " + userToClose);
		try
		{
			OddishSdk.Networking.CreateCloseP2pSessionWithUser().ExecAsync(userToClose.ToString(), delegate
			{
				if (m_ActiveLobby != null)
				{
					m_ActiveLobby.RemoveClosedConnection(userToClose);
				}
				Debug.Log("[PlatformLobbySystem_NetEase] Closed P2P Connection with remote user");
			}, delegate(CloseP2pSessionWithUserTask.FailResult onFailure)
			{
				Debug.LogError(string.Concat("PlatformLobbySystem_NetEase.CloseTTNetworkConnection - Unable to close network connection. [", onFailure.Code, "] ", onFailure.Message));
			});
		}
		catch (Exception ex)
		{
			d.LogErrorFormat("[PlatformLobbySystem_NetEase] CreateCloseP2pSessionWithUser threw exception {0}", ex);
		}
	}

	public void ReadAllNetworkData()
	{
		ReadNetworkData(m_ChannelReliable);
		ReadNetworkData(m_ChannelUnreliable);
		ReadNetworkData(m_ChannelPing);
	}

	public byte[] DecodeNetworkData(string data)
	{
		return Convert.FromBase64String(data);
	}

	private void ReadNetworkData(int channel)
	{
		uint num = 0u;
		int num2 = 0;
		IsP2pPacketAvailableResult isP2pPacketAvailableResult;
		ReadP2pPacketResult readP2pPacketResult;
		while (++num2 <= 32 && OddishSdk.Networking.CreateIsP2pPacketAvailable().Exec(out isP2pPacketAvailableResult, channel) && isP2pPacketAvailableResult.PacketSize != 0 && OddishSdk.Networking.CreateReadP2pPacket().Exec(out readP2pPacketResult, channel))
		{
			num = readP2pPacketResult.PacketSize;
			channel = readP2pPacketResult.ChannelId;
			string data = readP2pPacketResult.PacketContent.ToString(0, (int)readP2pPacketResult.PacketSize);
			byte[] array = DecodeNetworkData(data);
			if (channel == m_ChannelPing)
			{
				if (num != 0 && (array[0] != 5 || m_SendingPingResponse))
				{
					Debug.Log("[PlatformLobbySystem_NetEase] Handling Ping Response from " + readP2pPacketResult.RemoteUserId);
					HandlePingResponse(new TTNetworkID(readP2pPacketResult.RemoteUserId.ToString()));
				}
			}
			else if (base.CurrentLobby != null && Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && num != 0)
			{
				m_ActiveLobby.ReadNetworkData(array, (uint)array.Length, readP2pPacketResult.RemoteUserId.ToString(), channel);
			}
		}
	}

	protected override void Platform_RefreshLobbyList(LobbyFilterOptions filterOptions)
	{
		if (m_Inited)
		{
			if (filterOptions.m_FriendsGamesOnly == 1)
			{
				Debug.Log("[" + GetType().Name + "] Beginning Lobby Friends filter search");
				HandleLobbyMatchListBegin();
				Singleton.Manager<ManNetEase>.inst.UpdateFriends();
				SearchLobbiesByFriendsTask searchLobbiesByFriendsTask = OddishSdk.Matchmake.CreateSearchLobbiesByFriends();
				d.Log("[PlatformLobbySystem_NetEase] Starting an async task CreateSearchLobbiesByFriends");
				searchLobbiesByFriendsTask.ExecAsync(Singleton.Manager<ManNetEase>.inst.Friends, delegate(SearchLobbiesByFriendsTask.SuccessResult onSuccess)
				{
					Debug.Log($"[PlatformLobbySystem_NetEase] Friends Search filter complete, found {onSuccess.LobbyIds.Length} matching lobbies");
					m_QueuedActions.Enqueue(delegate
					{
						AddNetEaseLobbies(onSuccess.LobbyIds);
					});
				}, delegate(SearchLobbiesByFriendsTask.FailResult onFailure)
				{
					Debug.LogError($"PlatformLobbySystem_NetEase.Platform_RefreshLobbyList - Unable to locate lobbies with friends only. Error code {onFailure.Code} - {onFailure.Message}");
					m_QueuedActions.Enqueue(base.HandleLobbyMatchListFailed);
				});
				return;
			}
			SearchLobbiesByTagFiltersTask searchLobbiesByTagFiltersTask = OddishSdk.Matchmake.CreateSearchLobbiesByTagFilters();
			TagFilter[] array = new TagFilter[1]
			{
				new TagFilter()
			};
			array[0].Key = "gameSetup";
			array[0].Value = "true";
			d.Log("[PlatformLobbySystem_NetEase] Starting an async task CreateSearchLobbiesByTagFilters");
			searchLobbiesByTagFiltersTask.ExecAsync(16, 1, array, delegate(SearchLobbiesByTagFiltersTask.SuccessResult onSuccess)
			{
				m_QueuedActions.Enqueue(base.HandleLobbyMatchListBegin);
				Debug.Log("[" + GetType().Name + "] OnSuccess for TagFilter search");
				m_QueuedActions.Enqueue(delegate
				{
					AddNetEaseLobbies(onSuccess.LobbyIds);
				});
			}, delegate(SearchLobbiesByTagFiltersTask.FailResult onFailure)
			{
				Debug.LogError($"PlatformLobbySystem_NetEase.Platform_RefreshLobbyList - Unable to locate lobbies. Error code {onFailure.Code} - {onFailure.Message}");
				m_QueuedActions.Enqueue(base.HandleLobbyMatchListFailed);
			});
		}
		else
		{
			HandleLobbyMatchListFailed();
		}
	}

	public override void OpenFriendInviteScreen()
	{
	}

	public override bool SupportsOpenFriendInviteScreen()
	{
		return false;
	}

	public override void SendInvites()
	{
	}

	public override bool SupportsVisibilityType(TerraTech.Network.Lobby.LobbyVisibility visibilityType)
	{
		return visibilityType != TerraTech.Network.Lobby.LobbyVisibility.FriendsOnly;
	}

	public void CloseP2PSessionAfterDelay(TTNetworkID objRemoteClientId)
	{
		if (!m_PendingP2PClosures.ContainsKey(objRemoteClientId))
		{
			m_PendingP2PClosures.Add(objRemoteClientId, Time.time + 2f);
		}
	}
}
