#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class PlatformLobby_Steam : Lobby
{
	protected Callback<AvatarImageLoaded_t> m_AvatarImageLoadedCallback;

	protected Callback<P2PSessionRequest_t> m_P2PSessionRequestCallback;

	protected Callback<P2PSessionConnectFail_t> m_P2PSessionConnectFailedRequestCallback;

	protected Callback<LobbyChatMsg_t> m_LobbyChatMsg;

	private List<TTNetworkID> m_DeadConnections = new List<TTNetworkID>(16);

	private byte[] m_MemBuffer;

	private MemoryStream m_MemStream;

	private BinaryReader m_MemReader;

	private Dictionary<TTNetworkID, TTNetworkConnection> m_NetConnection = new Dictionary<TTNetworkID, TTNetworkConnection>();

	private int m_NextConnectionID = 1;

	private HostTopology m_HostTopology;

	private PlatformLobbySystem_Steam LobbySystem_Platform => base.LobbySystem as PlatformLobbySystem_Steam;

	public PlatformLobby_Steam(LobbySystem system, LobbyData data, ConnectionConfig config, MultiplayerModeType gameType)
		: base(system, data)
	{
		m_P2PSessionRequestCallback = Callback<P2PSessionRequest_t>.Create(OnP2PSessionRequest);
		m_P2PSessionConnectFailedRequestCallback = Callback<P2PSessionConnectFail_t>.Create(OnP2PAttemptFailed);
		m_LobbyChatMsg = Callback<LobbyChatMsg_t>.Create(OnChatMessageReceived);
		m_AvatarImageLoadedCallback = Callback<AvatarImageLoaded_t>.Create(OnAvatarImageLoaded);
		m_HostTopology = new HostTopology(config, base.LobbySystem.GetMaxPlayerCount(gameType));
		m_MemBuffer = new byte[65536];
		m_MemStream = new MemoryStream(m_MemBuffer);
		m_MemReader = new BinaryReader(m_MemStream);
	}

	public override void Shutdown()
	{
		base.Shutdown();
		foreach (TTNetworkID key in m_NetConnection.Keys)
		{
			if (!m_DeadConnections.Contains(key))
			{
				m_DeadConnections.Add(key);
			}
		}
		foreach (TTNetworkID deadConnection in m_DeadConnections)
		{
			if (m_NetConnection.ContainsKey(deadConnection))
			{
				OnConnectionDisposed(m_NetConnection[deadConnection]);
			}
		}
		m_DeadConnections.Clear();
		m_NetConnection.Clear();
		m_P2PSessionRequestCallback.Dispose();
		m_P2PSessionConnectFailedRequestCallback.Dispose();
		m_LobbyChatMsg.Dispose();
		m_AvatarImageLoadedCallback.Dispose();
	}

	public override string GetLocalPlayerName()
	{
		return SteamFriends.GetPersonaName();
	}

	public override bool IsLobbyOwner()
	{
		return base.LobbySystem.GetLocalPlayerID() == GetLobbyOwner();
	}

	public override void RemoveClientConnectionFromServer(TTNetworkID deadNetworkId)
	{
		m_DeadConnections.Add(deadNetworkId);
	}

	protected override void SetLobbyVisibility(LobbyVisibility visibility)
	{
		ELobbyType eLobbyType = (ELobbyType)visibility;
		CSteamID steamIDLobby = base.ID.ToSteamID();
		if (visibility == LobbyVisibility.Private)
		{
			eLobbyType = ELobbyType.k_ELobbyTypeFriendsOnly;
		}
		SteamMatchmaking.SetLobbyType(steamIDLobby, eLobbyType);
	}

	protected override void SendLobbyChatMsg(byte[] memBuffer, int numBytesToWrite)
	{
		SteamMatchmaking.SendLobbyChatMsg(base.ID.ToSteamID(), memBuffer, numBytesToWrite);
	}

	public override TTNetworkID GetLobbyOwner()
	{
		return SteamMatchmaking.GetLobbyOwner(base.ID.ToSteamID()).ToTTID();
	}

	protected override int GetLargeFriendAvatarImageID(TTNetworkID playerID)
	{
		TryGetLargeFriendAvatarImageID(playerID, out var imageID);
		return imageID;
	}

	protected override Sprite GetSpriteFromImageID(int imageID, int imageWidthHeight)
	{
		TryGetSpriteFromImageID(imageID, imageWidthHeight, out var sprite);
		return sprite;
	}

	public static bool TryGetLargeFriendAvatarImageID(TTNetworkID playerID, out int imageID)
	{
		CSteamID steamIDFriend = playerID.ToSteamID();
		imageID = SteamFriends.GetLargeFriendAvatar(steamIDFriend);
		bool num = imageID != 0;
		if (!num)
		{
			imageID = -1;
		}
		return num;
	}

	public static bool TryGetSpriteFromImageID(int imageID, int imageWidthHeight, out Sprite sprite)
	{
		sprite = null;
		if (imageID != -1)
		{
			int num = 4 * imageWidthHeight * imageWidthHeight;
			byte[] array = new byte[num];
			if (SteamUtils.GetImageRGBA(imageID, array, num))
			{
				FlipRGBAImage(array, imageWidthHeight);
				Texture2D texture2D = new Texture2D(imageWidthHeight, imageWidthHeight, TextureFormat.RGBA32, mipChain: false);
				texture2D.wrapMode = TextureWrapMode.Clamp;
				texture2D.LoadRawTextureData(array);
				texture2D.Apply();
				sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			}
		}
		return sprite != null;
	}

	protected override void CleanUpPreviousSession()
	{
		d.AssertFormat(m_NetConnection.Count == 0, "[PlatformLobby_Steam] CleanUpPreviousSession already is connected to {0} endpoints - disconnecting", m_NetConnection.Count);
		foreach (KeyValuePair<TTNetworkID, TTNetworkConnection> item in m_NetConnection)
		{
			if (NetworkServer.active)
			{
				NetworkServer.RemoveExternalConnection(item.Value.connectionId);
			}
		}
		m_NetConnection.Clear();
		m_DeadConnections.Clear();
		LobbySystem_Platform.ReadAllNetworkData();
	}

	protected override TTNetworkConnection CreateConnectionToHost(TTNetworkID hostNetworkID)
	{
		CleanUpPreviousSession();
		TTNetworkConnection tTNetworkConnection = CreateTTNetworkConnection(hostNetworkID, 0);
		m_NetConnection.Add(hostNetworkID, tTNetworkConnection);
		return tTNetworkConnection;
	}

	protected override void Update()
	{
		FlushNetworkData();
		if (m_DeadConnections.Count > 0)
		{
			for (int i = 0; i < m_DeadConnections.Count; i++)
			{
				TTNetworkID key = m_DeadConnections[i];
				if (m_NetConnection.ContainsKey(key))
				{
					TTNetworkConnection conn = m_NetConnection[key];
					OnConnectionDisposed(conn);
				}
			}
		}
		m_DeadConnections.Clear();
	}

	protected override void SetLobbyData(string keyName, string value)
	{
		if (!SteamMatchmaking.SetLobbyData(base.ID.ToSteamID(), keyName, value))
		{
			d.LogError("Failed to set lobby data " + keyName + " - " + value);
		}
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

	public override TTNetworkConnection.NetworkStats GetNetworkStats()
	{
		TTNetworkConnection.NetworkStats result = default(TTNetworkConnection.NetworkStats);
		foreach (KeyValuePair<TTNetworkID, TTNetworkConnection> item in m_NetConnection)
		{
			result.accummulate(item.Value.GetNetworkStats());
		}
		return result;
	}

	private static void FlipRGBAImage(byte[] imageData, int imageWidthHeight)
	{
		int num = imageWidthHeight * 4;
		byte[] array = new byte[num];
		int num2 = imageWidthHeight / 2;
		for (int i = 0; i < num2; i++)
		{
			int num3 = imageWidthHeight - (1 + i);
			int num4 = i * num;
			int num5 = num3 * num;
			Array.Copy(imageData, num4, array, 0, num);
			Array.Copy(imageData, num5, imageData, num4, num);
			Array.Copy(array, 0, imageData, num5, num);
		}
	}

	private TTNetworkConnection CreateTTNetworkConnection(TTNetworkID hostNetworkID, int connectionID)
	{
		TTNetworkConnection tTNetworkConnection = new TTNetworkConnection(m_HostTopology, hostNetworkID);
		string networkAddress = hostNetworkID.ToString();
		int networkHostId = -1;
		tTNetworkConnection.Initialize(networkAddress, networkHostId, connectionID, m_HostTopology);
		tTNetworkConnection.OnDisposed.Subscribe(OnConnectionDisposed);
		return tTNetworkConnection;
	}

	private void FlushNetworkData()
	{
		foreach (TTNetworkConnection value in m_NetConnection.Values)
		{
			value.Flush();
		}
	}

	private bool IsUserInLobby(TTNetworkID lobbyID, TTNetworkID userID)
	{
		bool result = false;
		CSteamID steamIDLobby = lobbyID.ToSteamID();
		CSteamID cSteamID = userID.ToSteamID();
		int numLobbyMembers = SteamMatchmaking.GetNumLobbyMembers(steamIDLobby);
		for (int i = 0; i < numLobbyMembers; i++)
		{
			CSteamID lobbyMemberByIndex = SteamMatchmaking.GetLobbyMemberByIndex(steamIDLobby, i);
			if (cSteamID == lobbyMemberByIndex)
			{
				result = true;
				break;
			}
		}
		return result;
	}

	private void OnP2PSessionRequest(P2PSessionRequest_t pCallback)
	{
		SteamNetworking.AcceptP2PSessionWithUser(pCallback.m_steamIDRemote);
	}

	private void OnP2PAttemptFailed(P2PSessionConnectFail_t pCallback)
	{
		string friendPersonaName = SteamFriends.GetFriendPersonaName(pCallback.m_steamIDRemote);
		if (friendPersonaName == null)
		{
			pCallback.m_steamIDRemote.m_SteamID.ToString();
		}
		TTNetworkID tTNetworkID = pCallback.m_steamIDRemote.ToTTID();
		if (m_NetConnection.TryGetValue(tTNetworkID, out var value))
		{
			try
			{
				value.InvokeHandlerNoData(33);
			}
			catch (Exception)
			{
				base.LobbySystem.ForceClientDisconnectionEvent.Send(value);
			}
			if (NetworkServer.active)
			{
				NetworkServer.RemoveExternalConnection(value.connectionId);
			}
			m_NetConnection.Remove(tTNetworkID);
		}
		EP2PSessionError eP2PSessionError = (EP2PSessionError)pCallback.m_eP2PSessionError;
		string errCode = eP2PSessionError.ToString();
		base.LobbySystem.HandleConnectionFailure(tTNetworkID, errCode);
	}

	private void OnChatMessageReceived(LobbyChatMsg_t param)
	{
		m_MemStream.Seek(0L, SeekOrigin.Begin);
		CSteamID pSteamIDUser;
		EChatEntryType peChatEntryType;
		int lobbyChatEntry = SteamMatchmaking.GetLobbyChatEntry(base.ID.ToSteamID(), (int)param.m_iChatID, out pSteamIDUser, m_MemBuffer, m_MemBuffer.Length, out peChatEntryType);
		if (lobbyChatEntry > 0)
		{
			HandleChatMessage(m_MemReader, lobbyChatEntry, pSteamIDUser.ToTTID());
		}
	}

	private void AddServerConnectionToClient(TTNetworkID clientNetworkId, LobbyPlayerData config)
	{
		TTNetworkConnection tTNetworkConnection = CreateTTNetworkConnection(clientNetworkId, m_NextConnectionID);
		m_NextConnectionID++;
		base.LobbySystem.SendEventStorePlayerConfig(tTNetworkConnection, config);
		m_NetConnection.Add(clientNetworkId, tTNetworkConnection);
		NetworkServer.AddExternalConnection(tTNetworkConnection);
	}

	public bool ReadNetworkData(int channelId, uint pcubMsgSize, byte[] pubDest, CSteamID clientSteamIDRemote)
	{
		TTNetworkID tTNetworkID = clientSteamIDRemote.ToTTID();
		bool flag = m_NetConnection.ContainsKey(tTNetworkID);
		if (!flag && GetClientConfig(tTNetworkID, out var playerConfig))
		{
			RemoveClientConfig(tTNetworkID);
			AddServerConnectionToClient(tTNetworkID, playerConfig);
			flag = true;
		}
		if (flag)
		{
			int num = Convert.ToInt32(pcubMsgSize);
			if (num != 0)
			{
				m_NetConnection[tTNetworkID].TransportReceive(pubDest, num, channelId);
			}
		}
		return flag;
	}

	private void OnAvatarImageLoaded(AvatarImageLoaded_t pCallback)
	{
		TTNetworkID playerID = pCallback.m_steamID.ToTTID();
		HandleAvatarImageLoaded(playerID, pCallback.m_iImage, pCallback.m_iWide, pCallback.m_iTall);
	}

	private void OnConnectionDisposed(TTNetworkConnection conn)
	{
		d.LogFormat("[PlatformLobby_Steam] Connection disposed {0}", conn.RemoteClientID.m_NetworkID);
		if (NetworkServer.active)
		{
			NetworkServer.RemoveExternalConnection(conn.connectionId);
		}
		m_NetConnection.Remove(conn.RemoteClientID);
		LobbySystem_Platform.CloseP2PSessionAfterDelay(conn.RemoteClientID.ToSteamID());
	}
}
