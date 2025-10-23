#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Netease.Oddish.Ingame.Sdk;
using Netease.Oddish.Ingame.Sdk.Entity.ContentFilter;
using Netease.Oddish.Ingame.Sdk.Entity.Matchmake;
using Netease.Oddish.Ingame.Sdk.Entity.User;
using Netease.Oddish.Ingame.Sdk.Task;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public class PlatformLobby_NetEase : TerraTech.Network.Lobby
{
	private struct LobbyMemberId
	{
		public string id;

		public bool haveHandledEnter;
	}

	private TTNetworkID m_LobbyOwner;

	private bool m_RequireRefresh;

	private bool m_Inited;

	private byte[] m_MemBuffer;

	private List<Member> m_LobbyMembers;

	private List<LobbyMemberId> m_LobbyMemberIds;

	private List<User> m_PlayerData;

	private MemoryStream m_MemStream;

	private BinaryReader m_BinaryReader;

	private BinaryWriter m_BinaryWriter;

	public const string kLobbyMemberCount = "lobbyMemberCount";

	public const string kLobbyStartGameDetails = "StartGameDetails";

	public const string kLobbyMemberRequestColour = "RequestedColour";

	private int m_ConnectionIndex = 1;

	private HostTopology m_HostTopology;

	private Dictionary<string, float> m_GracePeriodReconnections;

	private PlatformLobbySystem_NetEase m_LobbySystem_Platform;

	private Dictionary<TTNetworkID, TTNetworkConnection> m_P2PConnections;

	private PlatformLobbySystem_NetEase.LobbyData_NetEase m_InternalLobbyData;

	private float m_PlayerRefreshTimer = 10f;

	private bool m_RequestingPlayerData;

	private bool m_UpdatedUsername;

	public int MemberIDCount => m_LobbyMemberIds.Count;

	public PlatformLobby_NetEase(LobbySystem system, TerraTech.Network.LobbyData data, ConnectionConfig connConfig, MultiplayerModeType gameType, PlatformLobbySystem_NetEase.LobbyData_NetEase cachedData, int memBufferSize = 65536)
		: base(system, data, memBufferSize)
	{
		Debug.Log("[PlatformLobby_NetEase] Creating Object & Memory Streams/Buffers");
		m_MemBuffer = new byte[65536];
		m_MemStream = new MemoryStream(m_MemBuffer);
		m_BinaryReader = new BinaryReader(m_MemStream);
		m_BinaryWriter = new BinaryWriter(m_MemStream);
		m_HostTopology = new HostTopology(connConfig, base.LobbySystem.GetMaxPlayerCount(gameType));
		m_LobbyMembers = new List<Member>();
		m_LobbyMemberIds = new List<LobbyMemberId>();
		m_PlayerData = new List<User>();
		m_GracePeriodReconnections = new Dictionary<string, float>();
		m_P2PConnections = new Dictionary<TTNetworkID, TTNetworkConnection>();
		m_InternalLobbyData = cachedData;
		m_LobbySystem_Platform = system as PlatformLobbySystem_NetEase;
		Debug.Log("[PlatformLobby_NetEase] Retrieving Owner (" + cachedData.LobbyOwner + ") from cached data on creation");
		m_LobbyOwner = new TTNetworkID(cachedData.LobbyOwner);
		List<string> list = new List<string>(cachedData.KnownMembers);
		m_PlayerData.Clear();
		m_LobbyMembers.Clear();
		foreach (string item in list)
		{
			if (item.Equals(Singleton.Manager<ManNetEase>.inst.PlayerId))
			{
				m_PlayerData.Add(new User
				{
					EntityId = item,
					Nickname = Singleton.Manager<ManNetEase>.inst.PlayerNickname
				});
			}
			else
			{
				m_PlayerData.Add(new User
				{
					EntityId = item,
					Nickname = "Waiting For Data"
				});
			}
			m_LobbyMembers.Add(new Member
			{
				UserId = item,
				LobbyId = base.ID.ToString()
			});
			if (!MemberIdExists(item))
			{
				m_LobbyMemberIds.Add(new LobbyMemberId
				{
					id = item,
					haveHandledEnter = false
				});
			}
		}
		m_UpdatedUsername = m_LobbyOwner.Equals(Singleton.Manager<ManNetEase>.inst.PlayerNetworkID);
		RefreshPlayerData();
	}

	private bool MemberIdExists(string memberId)
	{
		for (int i = 0; i < m_LobbyMemberIds.Count; i++)
		{
			if (m_LobbyMemberIds[i].id == memberId)
			{
				return true;
			}
		}
		return false;
	}

	public override string GetLocalPlayerName()
	{
		return Singleton.Manager<ManNetEase>.inst.PlayerNickname;
	}

	public override bool IsLobbyOwner()
	{
		return GetLobbyOwner().Equals(Singleton.Manager<ManNetEase>.inst.PlayerId);
	}

	public override void RemoveClientConnectionFromServer(TTNetworkID deadNetworkId)
	{
		d.Log("[PlatformLobby_NetEase] CreateCloseP2pSessionWithUser in removeClient");
		m_P2PConnections.Remove(deadNetworkId);
	}

	public override TTNetworkID GetLobbyOwner()
	{
		return m_LobbyOwner;
	}

	public override void SendChat(string text, int teamChannel, uint netId)
	{
		OddishSdk.ContentFilter.CreateBannedWordCheck().ExecAsync(text, BannedWordCheckType.Substitute, delegate(BannedWordCheckTask.SuccessResult onSuccess)
		{
			Debug.Log(("[PlatformLobby_NetEase] Sending Message=" + onSuccess.Check.Content) ?? "");
			OddishSdk.Matchmake.CreateSendLobbyChatMessage().ExecAsync(base.ID.ToString(), onSuccess.Check.Content, delegate(SendLobbyChatMessageTask.FailResult result)
			{
				d.LogErrorFormat("[PlatformLobby_NetEase] failed to send chat message {0}, reason {1}", onSuccess.Check.Content, result);
			});
		}, delegate
		{
			d.LogErrorFormat("[PlatformLobby_NetEase] failed to run banned word check on {0}", text);
		});
	}

	protected override void SetLobbyVisibility(LobbyVisibility visibility)
	{
		LobbyTypeEnum lobbyType = visibility switch
		{
			LobbyVisibility.Private => LobbyTypeEnum.Private, 
			LobbyVisibility.FriendsOnly => LobbyTypeEnum.FriendsOnly, 
			LobbyVisibility.Public => LobbyTypeEnum.Public, 
			_ => throw new ArgumentOutOfRangeException("visibility", visibility, null), 
		};
		OddishSdk.Matchmake.CreateSetLobbyType().ExecAsync(base.ID.ToString(), lobbyType, delegate
		{
			d.LogErrorFormat("[PlatformLobby_NetEase] failed to set lobby visibility to {0}", visibility);
		});
	}

	protected override void SendLobbyChatMsg(byte[] memBuffer, int numBytesToWrite)
	{
		d.LogError("PlatformLobby_NetEase.SendLobbyChatMsg is no longer supported");
	}

	public void OnLobbyMemberDataUpdated(LobbyMemberDataUpdatedNotification notification)
	{
		for (int i = 0; i < notification.LobbyMemberData.Length; i++)
		{
			CustomData customData = notification.LobbyMemberData[i];
			if (customData.Key == "RequestedColour")
			{
				if (GetLobbyOwner() == LocalPlayerNetworkID())
				{
					byte[] array = Convert.FromBase64String(customData.Value);
					m_MemStream.Seek(0L, SeekOrigin.Begin);
					Array.Copy(array, 0, m_MemBuffer, 0, array.Length);
					if (ColourConverter.TryParseColourString(m_BinaryReader.ReadString(), out var col))
					{
						HostSetPlayerColour(new TTNetworkID(notification.MemberId), col);
					}
				}
			}
			else
			{
				d.LogWarning("PlatformLobby_NetEase unhandled lobby member data type " + customData.Key);
			}
		}
	}

	public void OnChatMessageReceived(LobbyChatMessageUpdatedNotification lobbyChat)
	{
		TTNetworkID tTNetworkID = new TTNetworkID(lobbyChat.MemberId);
		NetPlayer netPlayer = null;
		for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
		{
			NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
			if (player.GetPlayerIDInLobby() == tTNetworkID)
			{
				netPlayer = player;
				break;
			}
		}
		uint netId = ((netPlayer != null) ? netPlayer.netId.Value : NetworkInstanceId.Invalid.Value);
		int teamChannel = -1;
		if (netPlayer != null && base.LobbySystem.QueryGameState.IsInProgress())
		{
			teamChannel = netPlayer.LobbyTeamID;
		}
		DispatchTextChatMessage(lobbyChat.Message.Content, tTNetworkID, netId, teamChannel);
	}

	protected override int GetLargeFriendAvatarImageID(TTNetworkID playerID)
	{
		return -1;
	}

	protected override Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight)
	{
		throw new NotImplementedException();
	}

	protected override void CleanUpPreviousSession()
	{
		m_LobbySystem_Platform.ReadAllNetworkData();
	}

	protected override TTNetworkConnection CreateConnectionToHost(TTNetworkID hostID)
	{
		CleanUpPreviousSession();
		Debug.Log("[PlatformLobby_NetEase] Establishing connection with host " + hostID);
		TTNetworkConnection tTNetworkConnection = CreateTTNetworkConnection(hostID, 0);
		m_P2PConnections.Add(hostID, tTNetworkConnection);
		return tTNetworkConnection;
	}

	protected override void Update()
	{
		FlushNetworkData();
		if (!m_UpdatedUsername && Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			Singleton.Manager<ManNetwork>.inst.MyPlayer.SetName(Singleton.Manager<ManNetEase>.inst.PlayerNickname);
		}
		bool flag = false;
		for (int i = 0; i < m_LobbyMemberIds.Count; i++)
		{
			if (m_LobbyMemberIds[i].haveHandledEnter)
			{
				continue;
			}
			bool flag2 = false;
			for (int j = 0; j < m_PlayerData.Count; j++)
			{
				if (m_PlayerData[j].EntityId == m_LobbyMemberIds[i].id)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2)
			{
				m_LobbyMemberIds[i] = new LobbyMemberId
				{
					id = m_LobbyMemberIds[i].id,
					haveHandledEnter = true
				};
				if (m_LobbyMemberIds[i].id != GetLobbyOwner().ToString())
				{
					d.LogFormat("[PlatformLobby_NetEase] Triggering HandleLobbyStateUpdated for newly added member {0}", m_LobbyMemberIds[i].id);
					base.LobbySystem.HandleLobbyStateUpdated(base.ID, new TTNetworkID(m_LobbyMemberIds[i].id), MemberLobbyStateMask.MLS_Entered);
				}
				else
				{
					d.LogFormat("[PlatformLobby_NetEase] Owner is now counted as properly entered");
				}
			}
			else
			{
				flag = true;
			}
		}
		if (!m_RequestingPlayerData)
		{
			m_PlayerRefreshTimer -= Time.deltaTime;
			if (m_PlayerRefreshTimer <= 0f || flag)
			{
				m_PlayerRefreshTimer = 10f;
				RefreshPlayerData();
			}
		}
	}

	private void FlushNetworkData()
	{
		foreach (TTNetworkConnection value in m_P2PConnections.Values)
		{
			value.Flush();
		}
	}

	private void RefreshPlayerData()
	{
		if (!m_RequestingPlayerData && m_LobbyMemberIds.Count > 0)
		{
			m_RequestingPlayerData = true;
			d.Log($"[PlatformLobby_NetEase] Requesting Player-data from member listings count={m_LobbyMemberIds.Count}");
			string[] array = new string[m_LobbyMemberIds.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = m_LobbyMemberIds[i].id;
			}
			try
			{
				OddishSdk.User.CreateGetUsersByIds().ExecAsync(array, delegate(GetUsersByIdsTask.SuccessResult onSuccess)
				{
					Debug.Log($"[PlatformLobby_NetEase] Received player data from member-listing got:{onSuccess.Users.Length}");
					if (m_PlayerData.Count > 0)
					{
						m_PlayerData = new List<User>(onSuccess.Users);
					}
					m_RequestingPlayerData = false;
				}, delegate(GetUsersByIdsTask.FailResult onFailure)
				{
					Debug.LogError(string.Concat("PlatformLobby_NetEase.RefreshPlayerData - Failed to refresh player data of members. [", onFailure.Code, "] ", onFailure.Message));
					m_RequestingPlayerData = false;
				});
				return;
			}
			catch (Exception ex)
			{
				d.LogErrorFormat("[PlatformLobby_NetEase] exception during GetUserIds {0}", ex);
				m_RequestingPlayerData = false;
				return;
			}
		}
		d.Log("Ignoring Request for player data because either m_RequestingPlayerData=true(" + m_RequestingPlayerData.ToString() + ") or m_LobbyMemberIds.Count=0(" + m_LobbyMemberIds.Count + ")");
	}

	private void AddServerConnectionToClient(TTNetworkID clientNetworkId, LobbyPlayerData config)
	{
		TTNetworkConnection tTNetworkConnection = CreateTTNetworkConnection(clientNetworkId, m_ConnectionIndex);
		m_ConnectionIndex++;
		base.LobbySystem.SendEventStorePlayerConfig(tTNetworkConnection, config);
		m_P2PConnections.Add(clientNetworkId, tTNetworkConnection);
		NetworkServer.AddExternalConnection(tTNetworkConnection);
	}

	public void ReadNetworkData(byte[] packetData, uint packetSize, string remoteUser, int packetChannel)
	{
		TTNetworkID tTNetworkID = new TTNetworkID(remoteUser);
		bool flag = m_P2PConnections.ContainsKey(tTNetworkID);
		if (!flag && GetClientConfig(tTNetworkID, out var playerConfig))
		{
			d.LogFormat("[PlatformLobby_NetEase] Server adding connection to user {0}", tTNetworkID);
			RemoveClientConfig(tTNetworkID);
			AddServerConnectionToClient(tTNetworkID, playerConfig);
			flag = true;
		}
		if (flag && packetData.Length != 0 && packetSize != 0)
		{
			m_P2PConnections[tTNetworkID].TransportReceive(packetData, (int)packetSize, packetChannel);
		}
	}

	private TTNetworkConnection CreateTTNetworkConnection(TTNetworkID networkId, int nextConnectionIndex)
	{
		Debug.Log(string.Concat("[PlatformLobby_NetEase] Creating Network Connection with ", networkId, " with index=", nextConnectionIndex));
		TTNetworkConnection tTNetworkConnection = new TTNetworkConnection(m_HostTopology, networkId);
		string networkAddress = networkId.ToString();
		int networkHostId = -1;
		tTNetworkConnection.Initialize(networkAddress, networkHostId, nextConnectionIndex, m_HostTopology);
		tTNetworkConnection.OnDisposed.Subscribe(HandleConnectionDisposed);
		return tTNetworkConnection;
	}

	private void HandleConnectionDisposed(TTNetworkConnection obj)
	{
		if (NetworkServer.active)
		{
			NetworkServer.RemoveExternalConnection(obj.connectionId);
		}
		m_P2PConnections.Remove(obj.RemoteClientID);
		d.Log("[PlatformLobby_NetEase] CreateCloseP2pSessionWithUser on disposed");
	}

	protected override void SendTeamData()
	{
		if (IsLobbyOwner())
		{
			string value = ManSaveGame.SaveObjectToRawJson(base.PlayerTeams);
			SetLobbyData("playerTeams", value);
		}
	}

	protected override void UpdateUsedColoursLobbyData()
	{
		string value = ConvertColoursUsedToString();
		SetLobbyData("usedColours", value);
	}

	protected override void SetLobbyData(string keyName, string value)
	{
		m_InternalLobbyData.SetDataDirect(keyName, value);
		OddishSdk.Matchmake.CreateSetLobbyData().ExecAsync(base.ID.ToString(), keyName, value, LobbyDataScopeEnum.Public, delegate(SetLobbyDataTask.FailResult result)
		{
			Debug.LogError("[] Failed to set lobby data " + keyName + " because " + result.Message);
		});
	}

	public override TTNetworkConnection.NetworkStats GetNetworkStats()
	{
		throw new NotImplementedException();
	}

	public void AddMember(MemberEnteredNotification member)
	{
		if (!MemberIdExists(member.MemberId))
		{
			d.LogFormat("[PlatformLobby_NetEase] Member {0} Joined", member.MemberId);
			m_LobbyMemberIds.Add(new LobbyMemberId
			{
				id = member.MemberId,
				haveHandledEnter = false
			});
			if (IsLobbyOwner())
			{
				SetLobbyData("lobbyMemberCount", m_LobbyMemberIds.Count.ToString());
			}
		}
	}

	public void HandleMemberKicked(MemberKickedNotification member)
	{
		if (member.InitiatorMemberId == GetLobbyOwner().ToString() && member.TargetMemberId == LocalPlayerNetworkID().ToString())
		{
			base.LobbySystem.LeaveLobby();
		}
		base.LobbySystem.HandleLobbyStateUpdated(base.ID, new TTNetworkID(member.TargetMemberId), MemberLobbyStateMask.MLS_Kicked);
	}

	public void RemoveMember(string member)
	{
		if (!MemberIdExists(member))
		{
			return;
		}
		Debug.Log("[PlatformLobby_NetEase] Member Left lobby, removing from listings");
		for (int i = 0; i < m_LobbyMemberIds.Count; i++)
		{
			if (m_LobbyMemberIds[i].id == member)
			{
				m_LobbyMemberIds.RemoveAt(i);
				break;
			}
		}
		if (IsLobbyOwner())
		{
			SetLobbyData("lobbyMemberCount", m_LobbyMemberIds.Count.ToString());
		}
		base.LobbySystem.HandleLobbyStateUpdated(base.ID, new TTNetworkID(member), MemberLobbyStateMask.MLS_Left);
	}

	public TTNetworkID GetMemberAtIndex(int idx)
	{
		if (idx >= m_LobbyMemberIds.Count)
		{
			Debug.LogError("PlatformLobby_NetEase.GetMemberAtIndex - Trying to access index outside of bounds");
			return TTNetworkID.Invalid;
		}
		return new TTNetworkID(m_LobbyMemberIds[idx].id);
	}

	public int GetFriendCount()
	{
		int num = 0;
		string[] friends = Singleton.Manager<ManNetEase>.inst.Friends;
		foreach (string text in friends)
		{
			for (int j = 0; j < m_LobbyMemberIds.Count; j++)
			{
				if (m_LobbyMemberIds[j].id == text)
				{
					num++;
					break;
				}
			}
		}
		return num;
	}

	public string GetPlayerName(TTNetworkID playerId)
	{
		if (playerId == Singleton.Manager<ManNetEase>.inst.PlayerNetworkID)
		{
			return Singleton.Manager<ManNetEase>.inst.PlayerNickname;
		}
		foreach (User playerDatum in m_PlayerData)
		{
			if (playerDatum.EntityId.Equals(playerId.ToString()))
			{
				return playerDatum.Nickname;
			}
		}
		Debug.Log("[PlatformLobby_NetEase] Couldn't find nickname for " + playerId);
		return $"#{playerId}";
	}

	public void RemoveClosedConnection(TTNetworkID userToClose)
	{
		m_P2PConnections.Remove(userToClose);
	}

	public void SetLobbyOwner(string ownerCurrentOwnerMemberId)
	{
		m_LobbyOwner = new TTNetworkID(ownerCurrentOwnerMemberId);
	}

	public void MemberDisconnected(string notificationMemberId)
	{
	}

	protected override void SendStartGameMessage()
	{
		m_MemStream.Seek(0L, SeekOrigin.Begin);
		WriteStartGameSettings(m_BinaryWriter);
		string value = Convert.ToBase64String(m_MemBuffer, 0, (int)m_MemStream.Position);
		SetLobbyData("StartGameDetails", value);
	}

	protected override void SendLobbyStateToJIPPlayer(TTNetworkID changedPlayerID)
	{
	}

	public override void CheckForStartGame()
	{
		string cachedLobbyData = m_InternalLobbyData.GetCachedLobbyData("StartGameDetails", null, requestIfMissing: false);
		if (cachedLobbyData != null)
		{
			byte[] array = Convert.FromBase64String(cachedLobbyData);
			m_MemStream.Seek(0L, SeekOrigin.Begin);
			Array.Copy(array, 0, m_MemBuffer, 0, array.Length);
			ReadStartGameSettings(m_BinaryReader);
			ClientStartGame();
		}
	}

	public override void KickPlayer(TTNetworkID playerID)
	{
		d.Log($"Kicking player {playerID}");
		OddishSdk.Matchmake.CreateKickLobbyMember().ExecAsync(base.ID.m_NetworkID.ToString(), playerID.m_NetworkID.ToString(), delegate(KickLobbyMemberTask.FailResult result)
		{
			d.LogError($"PlatformLobby_NetEase.KickPlayer failed to kick player {playerID} because {result}.");
		});
	}

	public override void RequestSetColour(Color32 colour)
	{
		if (IsLobbyOwner())
		{
			TTNetworkID playerID = LocalPlayerNetworkID();
			HostSetPlayerColour(playerID, colour);
			return;
		}
		m_MemStream.Seek(0L, SeekOrigin.Begin);
		m_BinaryWriter.Write(ColourConverter.ColourToString(colour));
		string value = Convert.ToBase64String(m_MemBuffer, 0, (int)m_MemStream.Position);
		OddishSdk.Matchmake.CreateSetLobbyMemberData().ExecAsync(base.ID.m_NetworkID.ToString(), "RequestedColour", value, delegate(SetLobbyMemberDataTask.FailResult result)
		{
			d.LogError($"PlatformLobby_NetEase failed to set requested colour for reason {result}");
		});
	}
}
