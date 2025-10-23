#define UNITY_EDITOR
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public class TTNetworkManager : NetworkManager
{
	private struct Spawnable
	{
		public ItemTypeInfo itemType;

		public Transform prefab;
	}

	[SerializeField]
	private NetPlayer m_NetPlayerPrefab;

	public EventNoParams OnServerStarted;

	public EventNoParams OnServerStopped;

	public EventNoParams OnClientStarted;

	public EventNoParams OnClientStopped;

	public EventNoParams OnClientObjectSpawned;

	public const uint kHOST_BLOCK_POOL_SIZE = 10000000u;

	public const uint kPLAYER_BLOCK_POOL_SIZE = 1000000u;

	private Dictionary<NetworkHash128, Spawnable> m_AssetItemLookup = new Dictionary<NetworkHash128, Spawnable>();

	private int m_NextFreePlayerID;

	private uint m_NextHostBlockPoolID;

	private uint m_NextBlockPoolSegment = 10000000u;

	public uint GetNextHostBlockPoolID()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "GetNextHostBlockPoolID - We are not the host. This ID will be bad.");
		uint num = m_NextHostBlockPoolID++;
		d.Assert(num < 5000000, "Danger of exceeding host block ID allowance. Currently at " + num + ". We may need to code a resizing allocation.");
		return num;
	}

	public void AddSpawnableType(Transform prefab)
	{
		NetworkIdentity component = prefab.GetComponent<NetworkIdentity>();
		d.Assert(component != null, "TTNetworkManager.AddSpawnableType: Object " + prefab.name + " does not have a NetworkIdentity Component");
		if (component != null)
		{
			Spawnable value = new Spawnable
			{
				prefab = prefab
			};
			Visible component2 = prefab.GetComponent<Visible>();
			if (component2 != null)
			{
				value.itemType = component2.m_ItemType;
			}
			m_AssetItemLookup.Add(component.assetId, value);
		}
	}

	public static TrackedVisible SpawnEmptyTechRef(int team, Vector3 position, Quaternion rotation, bool grounded, bool addToObjectManager = true)
	{
		return Singleton.Manager<ManSpawn>.inst.SpawnNetEmptyTechRef(team, position, rotation, grounded, "EmptyTech", addToObjectManager);
	}

	private void RegisterClientSpawnHandlers()
	{
		foreach (KeyValuePair<NetworkHash128, Spawnable> item in m_AssetItemLookup)
		{
			ClientScene.RegisterSpawnHandler(item.Key, OnClientSpawnHandler, OnClientUnspawnHandler);
		}
	}

	public override void OnStartClient(NetworkClient client)
	{
		base.OnStartClient(client);
		OnClientStarted.Send();
	}

	public override void OnStopClient()
	{
		base.OnStopClient();
		RemoveAllNetworkedObjects();
		Singleton.Manager<ManNetwork>.inst.UnregisterClientMessageHandlers();
		OnClientStopped.Send();
	}

	public override void OnStartHost()
	{
		base.OnStartHost();
	}

	public override void OnStopHost()
	{
		base.OnStopHost();
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		Singleton.Manager<ManNetwork>.inst.RegisterServerMessageHandlers();
		m_NextFreePlayerID = 0;
		m_NextBlockPoolSegment = 10000000u;
		OnServerStarted.Send();
	}

	public override void OnStopServer()
	{
		base.OnStopServer();
		RemoveAllNetworkedObjects();
		OnServerStopped.Send();
	}

	public override void OnServerConnect(NetworkConnection conn)
	{
		base.OnServerConnect(conn);
		Singleton.Manager<ManNetwork>.inst.OnServerConnect(conn);
	}

	public override void OnServerDisconnect(NetworkConnection conn)
	{
		d.Log("OnServerDisconnect Begin: NetworkConnectionId=" + conn.connectionId + " Type=" + conn.GetType().Name);
		NetPlayer player = Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(conn);
		Singleton.Manager<ManNetTechs>.inst.OnPlayerDisconnect(player);
		for (int i = 0; i < conn.playerControllers.Count; i++)
		{
			PlayerController playerController = conn.playerControllers[i];
			if (playerController.IsValid && playerController.unetView != null)
			{
				NetworkServer.UnSpawn(playerController.unetView.gameObject);
				playerController.unetView.transform.Recycle();
			}
		}
		conn.playerControllers.Clear();
		Singleton.Manager<ManNetwork>.inst.OnServerDisconnect(conn);
		if (conn is TTNetworkConnection tTNetworkConnection && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			TTNetworkID remoteClientID = tTNetworkConnection.RemoteClientID;
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.RemoveClientConnectionFromServer(remoteClientID);
		}
		else
		{
			d.LogError("Client has disconnected but connection type is NOT TTNetworkConnection!  Type=" + conn.GetType().Name);
		}
	}

	public override void OnServerReady(NetworkConnection conn)
	{
		base.OnServerReady(conn);
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		ClientConnectedMessage clientConnectedMessage = extraMessageReader.ReadMessage<ClientConnectedMessage>();
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.GetClientConfig(clientConnectedMessage.m_clientID, out var playerConfig))
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.RemoveClientConfig(clientConnectedMessage.m_clientID);
			Singleton.Manager<ManNetwork>.inst.StorePlayerConfig(conn, playerConfig);
		}
		NetPlayer component = m_NetPlayerPrefab.transform.Spawn(Vector3.zero, Quaternion.identity).GetComponent<NetPlayer>();
		int teamID = 0;
		if (Singleton.Manager<ManNetwork>.inst.RetrieveAndDeletePlayerConfig(conn, out var config))
		{
			component.SetName(config.m_Name);
			component.OnServerSetLobbyID(config.m_PlayerID);
			component.OnServerSetColour(config.m_Colour);
			component.OnServerSetInitialBlockPoolID(m_NextBlockPoolSegment);
			m_NextBlockPoolSegment += 1000000u;
			if (config.m_TeamID != -1)
			{
				teamID = config.m_TeamID;
			}
			else if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
			{
				for (int i = 0; i < Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.DeathmatchColours.Length; i++)
				{
					if (config.m_Colour.Equals(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.DeathmatchColours[i]))
					{
						teamID = i;
						break;
					}
				}
			}
		}
		else
		{
			d.AssertFormat(false, "ASSERT: TTNetworkManager.OnServerAddPlayer has no player config for conn Id={0}", conn.connectionId);
			component.SetName("Player" + m_NextFreePlayerID);
		}
		component.OnServerSetPlayerID(m_NextFreePlayerID);
		component.OnServerSetTeamID(teamID);
		m_NextFreePlayerID++;
		Singleton.Manager<ManNetwork>.inst.SetConnectionHasLoadedTiles(conn, clientConnectedMessage.m_loadedTiles);
		NetworkServer.AddPlayerForConnection(conn, component.gameObject, playerControllerId);
	}

	public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
	{
		base.OnServerRemovePlayer(conn, player);
	}

	public override void OnServerError(NetworkConnection conn, int errorCode)
	{
		base.OnServerError(conn, errorCode);
	}

	public override void OnClientConnect(NetworkConnection conn)
	{
		RegisterClientSpawnHandlers();
		Singleton.Manager<ManNetwork>.inst.RegisterClientMessageHandlers();
		ClientScene.Ready(conn);
		if (base.autoCreatePlayer)
		{
			ClientConnectedMessage clientConnectedMessage = new ClientConnectedMessage();
			clientConnectedMessage.m_clientID = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLocalPlayerID();
			clientConnectedMessage.m_loadedTiles = Singleton.Manager<ManWorld>.inst.TileManager.GetLoadedTiles();
			ClientScene.AddPlayer(null, 0, clientConnectedMessage);
		}
	}

	public override void OnClientDisconnect(NetworkConnection conn)
	{
		Singleton.Manager<ManNetwork>.inst.UnregisterClientMessageHandlers();
		base.OnClientDisconnect(conn);
	}

	public override void OnClientError(NetworkConnection conn, int errorCode)
	{
		base.OnClientError(conn, errorCode);
	}

	public override void OnClientNotReady(NetworkConnection conn)
	{
		base.OnClientNotReady(conn);
	}

	private GameObject OnClientSpawnHandler(Vector3 position, NetworkHash128 assetID)
	{
		GameObject gameObject = null;
		if (m_AssetItemLookup.TryGetValue(assetID, out var value))
		{
			if (value.itemType != null)
			{
				if (value.itemType.ObjectType == ObjectTypes.Vehicle)
				{
					gameObject = SpawnEmptyTechRef(int.MaxValue, position, Quaternion.identity, grounded: false, addToObjectManager: false).visible.gameObject;
				}
				else if (value.itemType.ObjectType == ObjectTypes.Crate)
				{
					Crate.Definition crateDef = new Crate.Definition
					{
						m_Contents = new Crate.ItemDefinition[0],
						m_Locked = true
					};
					gameObject = Singleton.Manager<ManSpawn>.inst.SpawnEmptyCrateDef("CrateDropClient", crateDef, position, Quaternion.identity, grounded: false, isNetVersion: true, forceSpawn: true, value.prefab.GetComponent<Crate>().CorpType).visible.gameObject;
				}
				else if (value.itemType.ObjectType == ObjectTypes.Waypoint)
				{
					if (value.prefab != null)
					{
						gameObject = value.prefab.Spawn(position, Quaternion.identity).gameObject;
						gameObject.name = "WaypointClient";
					}
				}
				else
				{
					d.Assert(condition: false, "TTNetworkManager.OnClientSpawnHandler no spawn handler exists for Visible type " + value.itemType.ObjectType);
				}
				if (!gameObject.IsNull())
				{
				}
			}
			else if (value.prefab != null)
			{
				gameObject = value.prefab.Spawn(position, Quaternion.identity).gameObject;
				gameObject.name = value.prefab.name;
			}
		}
		_ = gameObject != null;
		OnClientObjectSpawned.Send();
		return gameObject;
	}

	private void OnClientUnspawnHandler(GameObject gameObject)
	{
		bool worldPosStays = true;
		Visible component = gameObject.GetComponent<Visible>();
		if (component.IsNotNull())
		{
			if (component.type == ObjectTypes.Vehicle)
			{
				ManSaveGame.Storing = true;
				gameObject.transform.Recycle(worldPosStays);
				ManSaveGame.Storing = false;
			}
			else
			{
				component.RemoveFromGame();
			}
		}
		else
		{
			gameObject.transform.Recycle(worldPosStays);
		}
	}

	private void RemoveAllNetworkedObjects()
	{
		List<NetworkIdentity> list = new List<NetworkIdentity>();
		Dictionary<NetworkInstanceId, NetworkIdentity> objects = ClientScene.objects;
		foreach (NetworkInstanceId key in objects.Keys)
		{
			NetworkIdentity networkIdentity = objects[key];
			if (networkIdentity != null && networkIdentity.gameObject != null)
			{
				list.Add(networkIdentity);
			}
		}
		bool active = NetworkServer.active;
		byte[] buffer = new byte[12];
		NetworkWriter networkWriter = new NetworkWriter(buffer);
		NetworkReader networkReader = new NetworkReader(buffer);
		for (int i = 0; i < list.Count; i++)
		{
			if (active)
			{
				NetworkServer.UnSpawn(list[i].gameObject);
				list[i].transform.Recycle();
				continue;
			}
			networkWriter.SeekZero();
			networkWriter.Write(list[i].netId);
			networkReader.SeekZero();
			client.connection.InvokeHandler(1, networkReader, 0);
		}
	}

	private void Start()
	{
		AddSpawnableType(m_NetPlayerPrefab.transform);
	}
}
