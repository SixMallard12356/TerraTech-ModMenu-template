#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace TerraTech.Network;

public class PlatformLobbySystem_LAN : LobbySystem
{
	public class PlatformLobbyData
	{
		public TTNetworkID m_HostId;

		public List<TTNetworkID> m_Players = new List<TTNetworkID>();

		public Dictionary<string, string> m_Data = new Dictionary<string, string>();

		public TCPConnection m_ClientConnection;

		public string GetInLobbyStringData()
		{
			m_Data["_playerlist"] = string.Join(",", m_Players);
			return ManSaveGame.SaveObjectToRawJson(m_Data);
		}

		public void LoadFromStringData(string stringData)
		{
			Dictionary<string, string> objectToLoad = null;
			ManSaveGame.LoadObjectFromRawJson(ref objectToLoad, stringData);
			if (objectToLoad.ContainsKey("_playerlist"))
			{
				m_Players = (from i in objectToLoad["_playerlist"].Split(',')
					select new TTNetworkID(i)).ToList();
			}
			foreach (KeyValuePair<string, string> item in objectToLoad)
			{
				m_Data[item.Key] = item.Value;
			}
		}

		public string GetBroadcastData(int tcpPort)
		{
			m_Data["networkPort"] = tcpPort.ToString();
			m_Data["protocolVersion"] = LobbySystem.PROTOCOL_VERSION.ToString();
			string text = ManSaveGame.SaveObjectToRawJson(m_Data) + "\0";
			return text.Length.ToString("D4") + text;
		}
	}

	public const string kLobbyKeyLobbyID = "lobbyID";

	public const string kLobbyKeyHostID = "hostID";

	public const string kLobbyKeyNetworkAddress = "networkAddress";

	public const string kLobbyKeyNetworkPort = "networkPort";

	private TTNetworkID m_LocalPlayerNetworkID;

	private Dictionary<TTNetworkID, PlatformLobbyData> m_PlatformLobbies = new Dictionary<TTNetworkID, PlatformLobbyData>();

	private Dictionary<TTNetworkID, TCPConnection> m_idToLobbyConnection = new Dictionary<TTNetworkID, TCPConnection>();

	private TTNetworkID m_CurrentLobbyID;

	private bool m_UseNetworkDiscovery;

	private LANNetworkDiscovery m_LANNetworkDiscovery;

	private LANFakeDiscoveryUI m_FakeDiscovery;

	private float m_NextLobbyDataUpdate;

	private const float m_LobbyDataUpdateInterval = 2f;

	private TTNetworkID m_PendingJoinRequest = TTNetworkID.Invalid;

	private TcpServerBehaviour m_TcpServer;

	private int m_TcpServerPort = 7778;

	private ConnectionConfig m_ConnectionConfig;

	private int m_ChannelReliable;

	private int m_ChannelUnreliable;

	private int m_ChannelPing;

	private List<TTNetworkID> m_SimulatedNameBatchToRetrieve = new List<TTNetworkID>();

	private List<TTNetworkID> m_SimulatedNamesCurrentlyBeingRetrieved = new List<TTNetworkID>();

	private Dictionary<TTNetworkID, string> m_SimulatedRetrievedNames = new Dictionary<TTNetworkID, string>();

	private Coroutine m_SimulatedBatchFetchUserNamesRoutine;

	private Coroutine m_SimulatedDelayedNameRetrievalHandler;

	private Coroutine m_UpdateBroadcastDataCoroutine;

	private static byte[] s_MessageBuffer = new byte[1024];

	protected override Lobby Platform_CreateLobbyObject(LobbyData data, MultiplayerModeType gameType)
	{
		return new PlatformLobby_LAN(this, data, m_ConnectionConfig);
	}

	public override bool Init(LobbyConstants constants, GameStateQuerier gsq)
	{
		base.Init(constants, gsq);
		m_ConnectionConfig = new ConnectionConfig();
		m_ConnectionConfig.PacketSize = 1200;
		m_ChannelReliable = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		m_ChannelUnreliable = m_ConnectionConfig.AddChannel(QosType.Unreliable);
		m_ChannelPing = m_ConnectionConfig.AddChannel(QosType.ReliableSequenced);
		return true;
	}

	public override bool IsJoinOrCreateLobbyRequestActive()
	{
		return false;
	}

	public override bool SupportsOpenFriendInviteScreen()
	{
		return false;
	}

	public override void OpenFriendInviteScreen()
	{
	}

	protected override void Platform_CreateLobby(MultiplayerModeType gameType, Lobby.LobbyVisibility visibility, int maxPlayerCount)
	{
		TTNetworkID tTNetworkID = (m_CurrentLobbyID = GetLocalPlayerID());
		PlatformLobbyData platformLobbyData = new PlatformLobbyData
		{
			m_HostId = tTNetworkID,
			m_Players = new List<TTNetworkID> { tTNetworkID }
		};
		platformLobbyData.m_Data["name"] = SystemInfo.deviceName;
		platformLobbyData.m_Data["lobbyID"] = m_CurrentLobbyID.ToString();
		platformLobbyData.m_Data["hostID"] = tTNetworkID.ToString();
		m_PlatformLobbies[m_CurrentLobbyID] = platformLobbyData;
		if (m_TcpServer == null)
		{
			m_TcpServer = base.gameObject.AddComponent<TcpServerBehaviour>();
		}
		if (m_TcpServer.StartServer(m_TcpServerPort, HandleLobbyMessageServer))
		{
			HandleLobbyCreationSuccess(m_CurrentLobbyID);
			if (m_UseNetworkDiscovery)
			{
				string broadcastData = platformLobbyData.GetBroadcastData(m_TcpServerPort);
				d.Log("Start broadcasting data! " + broadcastData);
				m_LANNetworkDiscovery.StartBroadcastingServer(broadcastData);
			}
		}
		else
		{
			HandleLobbyCreationFailure("Failed", LobbyErrorCode.Error);
		}
	}

	private void HandleLobbyMessageClient(BinaryReader reader, int messageLength)
	{
		string text = reader.ReadString();
		d.Log("Message from server (" + messageLength + " bytes): " + text);
		if (text.StartsWith("chat:"))
		{
			TTNetworkID sender = new TTNetworkID(text.Substring(5));
			base.CurrentLobby.HandleChatMessage(reader, messageLength, sender);
		}
		else if (text.StartsWith("lobbydata:"))
		{
			if (base.CurrentLobby != null && m_PlatformLobbies.ContainsKey(m_CurrentLobbyID))
			{
				string stringData = text.Substring(10);
				m_PlatformLobbies[m_CurrentLobbyID].LoadFromStringData(stringData);
				base.CurrentLobby.HandleLobbyDataUpdated(wasSuccessful: true);
			}
		}
		else if (text == "lobby_closed")
		{
			LobbyErrorEvent.Send(LobbyErrorCode.HostDisconnected);
		}
	}

	private void HandleLobbyMessageClient(TCPConnection connection, BinaryReader reader, int messageLength)
	{
		if (base.CurrentLobby != null && m_CurrentLobbyID != TTNetworkID.Invalid && m_PlatformLobbies.ContainsKey(m_CurrentLobbyID) && m_PlatformLobbies[m_CurrentLobbyID].m_ClientConnection == connection)
		{
			HandleLobbyMessageClient(reader, messageLength);
		}
	}

	public void RemoveConnection(TTNetworkID networkId)
	{
		if (m_idToLobbyConnection.TryGetValue(networkId, out var value))
		{
			m_TcpServer.RemoveConnection(value);
			m_idToLobbyConnection.Remove(networkId);
		}
		if (m_PlatformLobbies.ContainsKey(m_CurrentLobbyID) && m_PlatformLobbies[m_CurrentLobbyID].m_Players.Contains(networkId))
		{
			m_PlatformLobbies[m_CurrentLobbyID].m_Players.Remove(networkId);
			HandleLobbyStateUpdated(m_CurrentLobbyID, networkId, Lobby.MemberLobbyStateMask.MLS_Left);
		}
	}

	private void HandleLobbyMessageServer(TCPConnection connection, BinaryReader reader, int messageLength)
	{
		if (base.CurrentLobby == null || !(m_CurrentLobbyID != TTNetworkID.Invalid) || !m_PlatformLobbies.ContainsKey(m_CurrentLobbyID))
		{
			return;
		}
		string text = reader.ReadString();
		d.Log(string.Concat("Message from ", connection, ": ", text));
		if (text.StartsWith("join:"))
		{
			TTNetworkID tTNetworkID = new TTNetworkID(text.Substring(5));
			d.Log("Join " + tTNetworkID);
			if (!m_PlatformLobbies[m_CurrentLobbyID].m_Players.Contains(tTNetworkID))
			{
				m_idToLobbyConnection[tTNetworkID] = connection;
				m_PlatformLobbies[m_CurrentLobbyID].m_Players.Add(tTNetworkID);
				HandleLobbyStateUpdated(m_CurrentLobbyID, tTNetworkID, Lobby.MemberLobbyStateMask.MLS_Entered);
				SendToAllClients("player_joined:" + tTNetworkID.ToString(), alsoServer: false);
			}
		}
		else if (text.StartsWith("leave:"))
		{
			TTNetworkID tTNetworkID2 = new TTNetworkID(text.Substring(6));
			m_PlatformLobbies[m_CurrentLobbyID].m_Players.Remove(tTNetworkID2);
			HandleLobbyStateUpdated(m_CurrentLobbyID, tTNetworkID2, Lobby.MemberLobbyStateMask.MLS_Left);
			m_idToLobbyConnection.Remove(tTNetworkID2);
			m_TcpServer.RemoveConnection(connection);
		}
		else
		{
			if (!text.StartsWith("chat:"))
			{
				return;
			}
			new TTNetworkID(text.Substring(5));
			if (!(base.CurrentLobby is PlatformLobby_LAN platformLobby_LAN) || !platformLobby_LAN.IsLobbyOwner())
			{
				return;
			}
			int count = messageLength - (int)reader.BaseStream.Position;
			using MemoryStream memoryStream = new MemoryStream(s_MessageBuffer);
			using BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
			binaryWriter.Write(text);
			byte[] buffer = reader.ReadBytes(count);
			binaryWriter.Write(buffer, 0, count);
			SendToAllClients(s_MessageBuffer, 0, (int)memoryStream.Position, alsoServer: true);
		}
	}

	public override void JoinLobbyAfterUpdate(TTNetworkID lobbyID)
	{
		SetupLobbyData(lobbyID, requestRefreshedData: true);
		m_PendingJoinRequest = lobbyID;
	}

	public override bool SupportsVisibilityType(Lobby.LobbyVisibility visibilityType)
	{
		return visibilityType == Lobby.LobbyVisibility.Public;
	}

	protected override void Platform_JoinLobby(TTNetworkID lobbyID, bool fromInvite)
	{
		if (IsJoinOrCreateLobbyRequestActive())
		{
			return;
		}
		if (m_PlatformLobbies.TryGetValue(lobbyID, out var value))
		{
			if (value.m_HostId == GetLocalPlayerID())
			{
				HandleLobbyJoinSuccess(m_CurrentLobbyID);
				return;
			}
			string server = value.m_Data["networkAddress"];
			int port = int.Parse(value.m_Data["networkPort"]);
			value.m_ClientConnection = new TCPConnection(server, port, HandleLobbyMessageClient);
			if (value.m_ClientConnection.IsConnected())
			{
				value.m_ClientConnection.Send("join:" + GetLocalPlayerID().ToString());
				m_CurrentLobbyID = lobbyID;
				HandleLobbyJoinSuccess(lobbyID);
			}
			else
			{
				value.m_ClientConnection.Dispose();
				value.m_ClientConnection = null;
				HandleLobbyJoinFailure("Failed", LobbyErrorCode.FailedToConnect);
			}
		}
		else
		{
			d.LogError("Platform_JoinLobby - trying to join lobby that is not in our local list of lobbies!");
			HandleLobbyJoinFailure("LobbyID: " + lobbyID.ToString(), LobbyErrorCode.DoesntExist);
		}
	}

	protected override void Platform_LeaveLobby(TTNetworkID lobbyID)
	{
		if (!(lobbyID == m_CurrentLobbyID) || !m_PlatformLobbies.ContainsKey(lobbyID))
		{
			return;
		}
		PlatformLobbyData platformLobbyData = m_PlatformLobbies[m_CurrentLobbyID];
		platformLobbyData.m_Players.Remove(GetLocalPlayerID());
		if (platformLobbyData.m_ClientConnection != null)
		{
			platformLobbyData.m_ClientConnection.Send("leave:" + GetLocalPlayerID());
			platformLobbyData.m_ClientConnection.Dispose();
			platformLobbyData.m_ClientConnection = null;
		}
		if (platformLobbyData.m_HostId == GetLocalPlayerID())
		{
			if ((bool)m_TcpServer)
			{
				SendToAllClients("lobby_closed", alsoServer: false);
				m_idToLobbyConnection.Clear();
				m_TcpServer.StopServer();
			}
			if (m_UseNetworkDiscovery && m_LANNetworkDiscovery.isServer)
			{
				d.Log("Server StopBroadcastingServer");
				if (m_UpdateBroadcastDataCoroutine != null)
				{
					StopCoroutine(m_UpdateBroadcastDataCoroutine);
				}
				if (m_LANNetworkDiscovery.running)
				{
					m_LANNetworkDiscovery.StopBroadcastingServer();
				}
			}
			platformLobbyData.m_HostId = ((platformLobbyData.m_Players.Count == 0) ? TTNetworkID.Invalid : platformLobbyData.m_Players[0]);
			platformLobbyData.m_Data["name"] = ((platformLobbyData.m_HostId.IsValid() && platformLobbyData.m_Players.Contains(platformLobbyData.m_HostId)) ? GetUserName(platformLobbyData.m_HostId) : "-");
			HandleLobbyDataUpdated(lobbyID, wasSuccessful: true);
		}
		m_CurrentLobbyID = TTNetworkID.Invalid;
	}

	protected override void Platform_RequestLobbyData(TTNetworkID lobbyID)
	{
	}

	public override string GetLobbyData(TTNetworkID lobbyID, string keyName)
	{
		string value = null;
		if (m_PlatformLobbies.TryGetValue(lobbyID, out var value2))
		{
			value2.m_Data.TryGetValue(keyName, out value);
		}
		if (!value.NullOrEmpty())
		{
			return value;
		}
		return "";
	}

	public override string GetUserName(TTNetworkID playerID)
	{
		if (playerID == GetLocalPlayerID())
		{
			return SystemInfo.deviceName;
		}
		return "Player " + playerID.m_NetworkID;
	}

	public override void Platform_GetUserName(TTNetworkID playerID, Action<TTNetworkID, string> onUsernameRetrieved)
	{
		if (!m_SimulatedRetrievedNames.ContainsKey(playerID) && !m_SimulatedNameBatchToRetrieve.Contains(playerID) && !m_SimulatedNamesCurrentlyBeingRetrieved.Contains(playerID))
		{
			m_SimulatedNameBatchToRetrieve.Add(playerID);
			if (m_SimulatedBatchFetchUserNamesRoutine == null)
			{
				m_SimulatedBatchFetchUserNamesRoutine = StartCoroutine(BatchFetchMissingUserNames());
			}
		}
		StartCoroutine(WaitForNameHasBecomeAvailable());
		IEnumerator BatchFetchMissingUserNames()
		{
			yield return new WaitForSeconds(0.1f);
			m_SimulatedNamesCurrentlyBeingRetrieved.AddRange(m_SimulatedNameBatchToRetrieve);
			m_SimulatedBatchFetchUserNamesRoutine = null;
			m_SimulatedNameBatchToRetrieve.Clear();
			if (m_SimulatedDelayedNameRetrievalHandler == null)
			{
				m_SimulatedDelayedNameRetrievalHandler = StartCoroutine(SimulateDelayedNameRetrievalResponse());
			}
		}
		IEnumerator SimulateDelayedNameRetrievalResponse()
		{
			while (m_SimulatedNamesCurrentlyBeingRetrieved.Count > 0)
			{
				yield return new WaitForSeconds(UnityEngine.Random.value * 0.3f);
				int index = UnityEngine.Random.Range(0, m_SimulatedNamesCurrentlyBeingRetrieved.Count - 1);
				TTNetworkID tTNetworkID = m_SimulatedNamesCurrentlyBeingRetrieved[index];
				string userName = GetUserName(tTNetworkID);
				m_SimulatedRetrievedNames.Add(tTNetworkID, userName);
				m_SimulatedNamesCurrentlyBeingRetrieved.RemoveAt(index);
			}
			m_SimulatedDelayedNameRetrievalHandler = null;
		}
		IEnumerator WaitForNameHasBecomeAvailable()
		{
			string value;
			while (!m_SimulatedRetrievedNames.TryGetValue(playerID, out value))
			{
				yield return null;
			}
			onUsernameRetrieved(playerID, value);
		}
	}

	public override int GetLobbyNumFriends(TTNetworkID lobbyID)
	{
		return 0;
	}

	public override bool HasFriend(TTNetworkID playerID)
	{
		return false;
	}

	public override int GetNumLobbyMembers(TTNetworkID lobbyID)
	{
		if (m_PlatformLobbies.TryGetValue(lobbyID, out var value))
		{
			return value.m_Players.Count;
		}
		return 0;
	}

	public override int GetLobbyMemberLimit(TTNetworkID lobbyID)
	{
		return 16;
	}

	public override TTNetworkID GetLocalPlayerID()
	{
		if (!m_LocalPlayerNetworkID.IsValid())
		{
			int num = 17 * (31 + SystemInfo.deviceName.GetHashCode()) * (31 + Process.GetCurrentProcess().Id);
			m_LocalPlayerNetworkID = new TTNetworkID((ulong)num);
		}
		return m_LocalPlayerNetworkID;
	}

	public override TTNetworkID GetLobbyMemberByIndex(TTNetworkID lobbyID, int idx)
	{
		if (m_PlatformLobbies.TryGetValue(lobbyID, out var value))
		{
			return value.m_Players[idx];
		}
		return TTNetworkID.Invalid;
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

	public override bool Platform_SupportsVoiceChat()
	{
		return false;
	}

	protected override void Platform_SendPingRequest(TTNetworkID hostID)
	{
	}

	protected override void Platform_RefreshLobbyList(LobbyFilterOptions filterOptions)
	{
		ManUI inst = Singleton.Manager<ManUI>.inst;
		inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Combine(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
		if (m_UseNetworkDiscovery && !m_LANNetworkDiscovery.running)
		{
			d.Log("StartSearchingForServer");
			m_LANNetworkDiscovery.OnBroadcastReceived.Subscribe(OnServerDiscoveryResult);
			m_LANNetworkDiscovery.StartSearchingForServer();
		}
		if (!m_UseNetworkDiscovery)
		{
			m_FakeDiscovery.enabled = true;
			m_FakeDiscovery.PopulateLobbies(m_PlatformLobbies, filterOptions);
		}
		UpdateLobbyList();
	}

	public PlatformLobbyData GetPlatformLobby(PlatformLobby_LAN from)
	{
		m_PlatformLobbies.TryGetValue(from.ID, out var value);
		return value;
	}

	private void UpdateLobbyList()
	{
		HandleLobbyMatchListBegin();
		foreach (KeyValuePair<TTNetworkID, PlatformLobbyData> platformLobby in m_PlatformLobbies)
		{
			TTNetworkID key = platformLobby.Key;
			HandleLobbyMatchListAddLobby(key, refreshData: true);
		}
		HandleLobbyMatchListEnd();
	}

	private void OnServerDiscoveryResult(string fromAddress, string rawBroadcastData)
	{
		if (!int.TryParse(rawBroadcastData.Substring(0, 4), out var result))
		{
			d.LogError("OnServerDiscoveryResult - Failed to get data string length from first 4 characters of broadcast data! " + rawBroadcastData);
			return;
		}
		string text = rawBroadcastData.Substring(4, result);
		d.Log("OnServerDiscoveryResult " + text);
		Dictionary<string, string> objectToLoad = null;
		ManSaveGame.LoadObjectFromRawJson(ref objectToLoad, text);
		if (objectToLoad == null || !objectToLoad.TryGetValue("lobbyID", out var value))
		{
			d.LogError("OnServerDiscoveryResult - Failed to get lobby ID from broadcast data!");
			return;
		}
		TTNetworkID key = new TTNetworkID(value);
		if (!m_PlatformLobbies.TryGetValue(key, out var value2))
		{
			if (!objectToLoad.TryGetValue("hostID", out var value3))
			{
				d.LogError("OnServerDiscoveryResult - Failed to get host ID from broadcast data!");
				return;
			}
			TTNetworkID tTNetworkID = new TTNetworkID(value3);
			value2 = new PlatformLobbyData
			{
				m_HostId = tTNetworkID,
				m_Players = new List<TTNetworkID> { tTNetworkID }
			};
			m_PlatformLobbies[key] = value2;
			d.Log("OnServerDiscoveryResult  - created lobby " + key.ToString());
		}
		string[] array = fromAddress.Split(':');
		objectToLoad["networkAddress"] = array[array.Length - 1];
		value2.m_Data = objectToLoad;
		d.Log("OnServerDiscoveryResult  - updated lobby " + key.ToString());
		UpdateLobbyList();
	}

	private void OnScreenChanged(bool shown, ManUI.ScreenType screenType)
	{
		if (screenType != ManUI.ScreenType.MatchmakingLobbyList)
		{
			ManUI inst = Singleton.Manager<ManUI>.inst;
			inst.OnScreenChangeEvent = (Action<bool, ManUI.ScreenType>)Delegate.Remove(inst.OnScreenChangeEvent, new Action<bool, ManUI.ScreenType>(OnScreenChanged));
			if (m_UseNetworkDiscovery && m_LANNetworkDiscovery.isClient)
			{
				d.Log("Client StopSearchingForServer");
				m_LANNetworkDiscovery.OnBroadcastReceived.Unsubscribe(OnServerDiscoveryResult);
				m_LANNetworkDiscovery.StopSearchingForServer();
			}
			if (!m_UseNetworkDiscovery)
			{
				m_FakeDiscovery.enabled = false;
			}
		}
	}

	private void Start()
	{
		if (m_LANNetworkDiscovery == null)
		{
			GameObject gameObject = new GameObject("LANNetworkDiscovery");
			m_LANNetworkDiscovery = gameObject.AddComponent<LANNetworkDiscovery>();
			m_LANNetworkDiscovery.useNetworkManager = false;
			m_LANNetworkDiscovery.broadcastKey = "TTLanBroadcast".GetHashCode();
			m_LANNetworkDiscovery.showGUI = false;
		}
		if (m_FakeDiscovery == null)
		{
			m_FakeDiscovery = base.gameObject.AddComponent<LANFakeDiscoveryUI>();
			m_FakeDiscovery.enabled = false;
		}
	}

	public override void Update()
	{
		if (m_UseNetworkDiscovery && m_LANNetworkDiscovery.running && m_LANNetworkDiscovery.isServer && m_CurrentLobbyID.IsValid() && m_PlatformLobbies.TryGetValue(m_CurrentLobbyID, out var value) && Time.time >= m_NextLobbyDataUpdate)
		{
			m_NextLobbyDataUpdate = Time.time + 2f;
			SetBroadcastData(value.GetBroadcastData(m_TcpServerPort));
		}
		if (m_PendingJoinRequest != TTNetworkID.Invalid)
		{
			LobbyData lobby = GetLobby(m_PendingJoinRequest);
			string lobbyData = GetLobbyData(m_PendingJoinRequest, "protocolVersion");
			if (lobby != null && lobbyData != "")
			{
				JoinLobby(m_PendingJoinRequest, fromInvite: false);
				m_PendingJoinRequest = TTNetworkID.Invalid;
			}
		}
		base.Update();
	}

	public void OnGUI()
	{
		if (base.CurrentLobby != null && GUI.Button(new Rect(400f, 0f, 100f, 16f), "Break connection"))
		{
			LobbyErrorEvent.Send(LobbyErrorCode.LostConnection);
		}
	}

	private void SetBroadcastData(string newData)
	{
		if (!m_LANNetworkDiscovery.running)
		{
			d.Log("Broadcast not running, setting data: " + newData);
			m_LANNetworkDiscovery.broadcastData = newData;
		}
		else
		{
			if (!(m_LANNetworkDiscovery.broadcastData != newData))
			{
				return;
			}
			if (m_UpdateBroadcastDataCoroutine == null)
			{
				if (m_LANNetworkDiscovery.running)
				{
					d.Log("Stopping broadcast to update data!");
					m_LANNetworkDiscovery.StopBroadcastingServer();
				}
			}
			else
			{
				StopCoroutine(m_UpdateBroadcastDataCoroutine);
			}
			m_UpdateBroadcastDataCoroutine = StartCoroutine(DoUpdateBroadcastData(newData));
		}
	}

	private IEnumerator DoUpdateBroadcastData(string newData)
	{
		while (NetworkTransport.IsBroadcastDiscoveryRunning())
		{
			d.Log("Waiting for broadcast to stop running!");
			yield return null;
		}
		yield return new WaitForSecondsRealtime(0.5f);
		d.Log("Updating broadcast data and restarting server! " + newData);
		m_LANNetworkDiscovery.StartBroadcastingServer(newData);
		m_UpdateBroadcastDataCoroutine = null;
	}

	public void SendToAllClients(string msg, bool alsoServer)
	{
		using MemoryStream memoryStream = new MemoryStream(s_MessageBuffer);
		using BinaryWriter binaryWriter = new BinaryWriter(memoryStream, Encoding.UTF8);
		binaryWriter.Write(msg);
		int len = (int)memoryStream.Position;
		SendToAllClients(s_MessageBuffer, 0, len, alsoServer);
	}

	public void SendToAllClients(byte[] data, int offset, int len, bool alsoServer)
	{
		if (!(m_TcpServer != null))
		{
			return;
		}
		m_TcpServer.SendToAll(data, offset, len);
		if (!alsoServer)
		{
			return;
		}
		using MemoryStream input = new MemoryStream(data, offset, len);
		using BinaryReader reader = new BinaryReader(input, Encoding.UTF8);
		HandleLobbyMessageClient(reader, len);
	}
}
