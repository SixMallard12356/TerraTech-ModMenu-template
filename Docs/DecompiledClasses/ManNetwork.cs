#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(TTNetworkManager))]
public class ManNetwork : Singleton.Manager<ManNetwork>
{
	public interface IDumpableBehaviour
	{
		void Dump(StringBuilder builder);
	}

	public struct MatchDurationOption
	{
		public float m_durationSecs;

		public MatchDurationOption(int mins)
		{
			m_durationSecs = (float)mins * 60f;
		}

		public string localisedString()
		{
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 67), Mathf.FloorToInt(m_durationSecs / 60f));
		}
	}

	private class NetworkMsgMetrics
	{
		public TTMsgType msgType;

		public uint nCount;

		public ulong nTotalBytes;

		public NetworkMsgMetrics(TTMsgType m)
		{
			msgType = m;
			d.Assert(nCount == 0);
			d.Assert(nTotalBytes == 0);
		}
	}

	private enum EnumAutoDumpStats
	{
		ADS_NONE,
		ADS_EVERY_SEC,
		ADS_EVERY_FIFTEEN_SECS,
		ADS_EVERY_THIRTY_SECS
	}

	public enum MapSizeOption
	{
		Auto,
		Small,
		Medium,
		Large
	}

	private struct MsgIdentifier : IEquatable<MsgIdentifier>
	{
		public enum Recipient
		{
			Server,
			Client
		}

		public uint id;

		public short msgType;

		public Recipient recipient;

		public static bool operator ==(MsgIdentifier s1, MsgIdentifier s2)
		{
			if (s1.id == s2.id && s1.msgType == s2.msgType)
			{
				return s1.recipient == s2.recipient;
			}
			return false;
		}

		public static bool operator !=(MsgIdentifier s1, MsgIdentifier s2)
		{
			return !(s1 == s2);
		}

		public override bool Equals(object obj)
		{
			if (obj is MsgIdentifier other)
			{
				return Equals(other);
			}
			return false;
		}

		public bool Equals(MsgIdentifier other)
		{
			return other == this;
		}

		public override int GetHashCode()
		{
			return (msgType << 16) | ((ushort)id << 1) | ((recipient == Recipient.Server) ? 1 : 0);
		}
	}

	private class ConnectionData
	{
		public NetworkConnection m_Connection;

		public HashSet<IntVector2> m_LoadedTiles;

		public NetPlayer m_Player;
	}

	public enum AuthorityReason
	{
		NoAuthority,
		Collision,
		UndoActive,
		HeldVisible
	}

	public delegate void MessageHandler(NetworkMessage netMsg);

	public enum State
	{
		Inactive,
		SettingUp,
		InGame
	}

	private enum SetupState
	{
		Idle,
		BeginHostingGame,
		BeginJoiningGame,
		WaitToJoin
	}

	private class SplitMessageJoiner
	{
		private byte[] m_JoinedBuffer;

		private int m_TotalLength;

		public void OnHandleMsg(NetworkMessage netMsg, Dictionary<short, NetworkMessageDelegate> delegateDict)
		{
			if (netMsg.reader.ReadByte() == 0)
			{
				byte[] array = netMsg.reader.ReadBytesAndSize();
				if (m_JoinedBuffer == null || m_TotalLength + array.Length > m_JoinedBuffer.Length)
				{
					Array.Resize(ref m_JoinedBuffer, (m_TotalLength + array.Length) * 3 / 2);
				}
				Array.Copy(array, 0, m_JoinedBuffer, m_TotalLength, array.Length);
				m_TotalLength += array.Length;
				return;
			}
			d.LogFormat("Reconstructed split packet - total size = {0}", m_TotalLength);
			NetworkReader networkReader = new NetworkReader(m_JoinedBuffer);
			networkReader.ReadInt16();
			short num = networkReader.ReadInt16();
			m_TotalLength = 0;
			if (delegateDict.TryGetValue(num, out var value))
			{
				NetworkMessage netMsg2 = new NetworkMessage
				{
					conn = netMsg.conn,
					channelId = netMsg.channelId,
					msgType = num,
					reader = networkReader
				};
				value(netMsg2);
			}
		}
	}

	[SerializeField]
	[EnumArray(typeof(DebugNetworkLog.LogFilter))]
	private bool[] m_DebugLogging;

	[SerializeField]
	private TankPreset m_DefaultTech;

	[SerializeField]
	private NetController m_NetControllerPrefab;

	[SerializeField]
	private NetSpawnPoint m_NetSpawnPrefab;

	[SerializeField]
	private NetInventory m_NetInventoryPrefab;

	[SerializeField]
	private InventoryAsset m_CrateDropWhiteList;

	[SerializeField]
	private Transform m_DefaultClientFlyDespawnEffect;

	public const int k_MaxPartyCount = 16;

	public static MatchDurationOption[] MatchDurationOptions = new MatchDurationOption[6]
	{
		new MatchDurationOption(10),
		new MatchDurationOption(15),
		new MatchDurationOption(20),
		new MatchDurationOption(30),
		new MatchDurationOption(45),
		new MatchDurationOption(60)
	};

	public static int[] KillStreakClaimDangerRanges = new int[6] { 10, 20, 30, 50, 70, 100 };

	public static int[] KillStreakKillThresholdMultiplierOptions = new int[3] { 1, 2, 3 };

	public static int[] CabSelfDestructTimeRanges = new int[4] { 5, 10, 15, 20 };

	public static int[] CrateDropMinDistances = new int[8] { 10, 20, 50, 100, 200, 300, 400, 500 };

	public static int[] CrateDropFrequencies = new int[5] { 15, 30, 60, 90, 120 };

	public static int[] CrateDropBlockCounts = new int[5] { 2, 4, 6, 8, 10 };

	public static int[] CrateDropPickupRanges = new int[6] { 10, 20, 50, 100, 150, 200 };

	public static int[] CrateDropDelayMinsChoices = new int[6] { 0, 1, 2, 3, 4, 5 };

	public static int[] HealWarmUpTimeRanges = new int[4] { 0, 1, 3, 5 };

	public static int[] HealRateRanges = new int[9] { 25, 50, 100, 150, 200, 250, 300, 400, 500 };

	public static int[] HealInterruptCooldownRanges = new int[4] { 0, 1, 3, 5 };

	public static int[] DeathStreakMinDeathsReqdRanges = new int[3] { 1, 2, 3 };

	public static int[] DeathStreakSubsDeathsReqdRanges = new int[3] { 1, 2, 3 };

	private bool m_SetupWaitsForModeSwitch = true;

	public EventNoParams OnPreGameStarted;

	public EventNoParams OnPostGameExited;

	public EventNoParams OnServerHostStarted;

	public EventNoParams OnServerHostStopped;

	public Event<bool> OnGenerateTerrainForced;

	public Event<NetPlayer> OnPlayerAdded;

	public Event<NetPlayer> OnPlayerRemoved;

	public Event<NetPlayer> OnPlayerChangedTeam;

	public Event<TankBlock, TankBlock> BlockReplacedWithNetBlockEvent;

	public Event<NetBlock> BlockUndoAuthorityDenied;

	public Event<Tank> NetTechDestroyed;

	public Event<Tank, NetBlock, TankBlock> ServerNetBlockAttachedToTech;

	public Event<Tank, TankBlock, NetBlock> ServerBlockRemovedFromTechTech;

	public EventNoParams OnAllExpectedPlayersJoined;

	public Event<NetController.Phase> ServerPhaseEnterEvent;

	public Event<NetController.Phase> ClientPhaseEnterEvent;

	public Event<NetworkConnection, IntVector2, bool> OnNetPlayerLoadedTile;

	private TTNetworkManager m_NetworkManager;

	private NetController m_NetController;

	private NetPlayer m_MyPlayer;

	private List<NetPlayer> m_Players = new List<NetPlayer>();

	private Dictionary<int, ConnectionData> m_PerConnectionData = new Dictionary<int, ConnectionData>();

	private Dictionary<int, SplitMessageJoiner> m_MessageJoiners = new Dictionary<int, SplitMessageJoiner>();

	private const int k_MaxChunkSize = 900;

	private byte[] m_SplittingBuffer = new byte[900];

	private NetworkWriter m_ChunkWriter = new NetworkWriter();

	private readonly List<NetworkConnection> m_ListOfOneConnection = new List<NetworkConnection> { null };

	private Dictionary<uint, AuthorityReason> m_ItemsWithAuthority = new Dictionary<uint, AuthorityReason>();

	private List<ResourcePickup> m_NonNetworkedChunksToReplace = new List<ResourcePickup>();

	private List<NetBlockChunk> m_NetBlocksAwaitingGrabAuthority = new List<NetBlockChunk>();

	private List<NetBlockChunk> m_NetBlocksReleasedBeforeReceivingGrabAuthority = new List<NetBlockChunk>();

	private bool m_PreventReleaseCommand;

	private Dictionary<MsgIdentifier, MessageHandler> m_MessageHandlers = new Dictionary<MsgIdentifier, MessageHandler>();

	private Dictionary<uint, List<MsgIdentifier>> m_MessageSubscriptions = new Dictionary<uint, List<MsgIdentifier>>();

	private HashSet<short> m_IgnoreUnhandledTargettedMessageWarning = new HashSet<short> { 82, 83, 84, 117, 72, 68 };

	[NonSerialized]
	public bool OnHostDuringPhysicsCallback;

	private List<TankBlock> m_BlocksToRecycle = new List<TankBlock>();

	private Transform m_DisposalContainer;

	private Dictionary<int, LobbyPlayerData> m_PlayerConfigs = new Dictionary<int, LobbyPlayerData>();

	private SetupState m_SetupState;

	private float m_JoinTimeoutCountdown;

	private NetworkConnection m_SetupHostConn;

	private int m_LogFilterCount;

	private bool m_AllClientsConnected;

	private bool m_AllPlayersPresent;

	private Dictionary<int, List<int>> m_PlayersOnTeam = new Dictionary<int, List<int>>();

	private List<Tank> m_AllPlayerTechs = new List<Tank>(16);

	private bool m_IgnoreServerDisconnect;

	private static Dictionary<short, NetworkMsgMetrics> m_networkMsgMetrics = new Dictionary<short, NetworkMsgMetrics>(128);

	private static bool m_HasCheckedNetLog = false;

	private static bool m_IsNetLogEnabled = false;

	private static bool m_LogNetworkMessageWrites = false;

	private static bool m_IsNetworkMetricsEnabled = false;

	private static EnumAutoDumpStats m_AutoDumpStats = EnumAutoDumpStats.ADS_NONE;

	private static string m_separator = "========================================================================================";

	private static float m_autoDumpStatsTimer = 0f;

	public Transform DefaultClientFlyDespawnEffect => m_DefaultClientFlyDespawnEffect;

	public static bool IsNetworked => Singleton.Manager<ManNetwork>.inst.IsMultiplayer();

	public static bool IsHost
	{
		get
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				return Singleton.Manager<ManNetwork>.inst.IsServer;
			}
			return true;
		}
	}

	public static bool IsHostOrWillBe
	{
		get
		{
			if (Singleton.Manager<ManNetwork>.inst.m_SetupState != SetupState.BeginJoiningGame && Singleton.Manager<ManNetwork>.inst.m_SetupState != SetupState.WaitToJoin)
			{
				return IsHost;
			}
			return false;
		}
	}

	public int NetworkPort => m_NetworkManager.networkPort;

	public int MyTechTeamID
	{
		get
		{
			if (!(m_MyPlayer != null))
			{
				return int.MaxValue;
			}
			return m_MyPlayer.TechTeamID;
		}
	}

	public NetPlayer MyPlayer => m_MyPlayer;

	public NetworkClient Client => m_NetworkManager.client;

	public bool IsServer => NetworkServer.active;

	public bool IsServerOrWillBe
	{
		get
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				return Singleton.Manager<ManNetwork>.inst.m_SetupState == SetupState.BeginHostingGame;
			}
			return true;
		}
	}

	public TechData DefaultTechData => m_DefaultTech.GetTechDataFormatted();

	public int StartingTechLoadoutCorp { get; set; }

	public int StartingTechLoadout { get; set; }

	public int StartingSkinID { get; set; }

	public MultiplayerKillStreakRewardAsset KillStreakRewards { get; set; }

	public NetSpawnPoint SpawnPrefab => m_NetSpawnPrefab;

	public NetInventory NetInventoryPrefab => m_NetInventoryPrefab;

	public SpawnPointBank ServerSpawnBank => m_ServerSpawnBank;

	public NetOptions Options { get; set; }

	public InventoryAsset CrateDropWhiteList => m_CrateDropWhiteList;

	public float GameDurationTimeInSecs { get; set; }

	public bool InventoryAvailable { get; set; }

	public bool ScavengeItems { get; set; }

	public bool KillStreakRewardsEnabled { get; set; }

	public bool KillStreakResetsWhenDestroyedEnabled { get; set; }

	public bool KillStreakRewardStackEnabled { get; set; }

	public bool KillStreakMaxedAutoClaimRewardEnabled { get; set; }

	public bool KillStreakPreventClaimNearDangerEnabled { get; set; }

	public int KillStreakClaimDangerRangeIndex { get; set; }

	public int KillStreakKillThresholdMultiplierIndex { get; set; }

	public bool CabSelfDestruct { get; set; }

	public int CabSelfDestructTimeIndex { get; set; }

	public bool IgnoreServerDisconnect
	{
		get
		{
			return m_IgnoreServerDisconnect;
		}
		set
		{
			m_IgnoreServerDisconnect = value;
		}
	}

	public bool CollideToScavengeBlocks { get; set; }

	public bool KeepLootedBlocksOnRespawnEnabled { get; set; }

	public bool ClearUnusedInventoryAfterSpawnBubbleEnabled { get; set; }

	public bool CrateDropsEnabled { get; set; }

	public int CrateDropMinDistanceIndex { get; set; }

	public int CrateDropFrequencyIndex { get; set; }

	public int CrateDropBlockQuantityIndex { get; set; }

	public int CrateDropPickupRangeIndex { get; set; }

	public int CrateDropDelayMinsIndex { get; set; }

	public bool HealInBuildBeam { get; set; }

	public float HealWarmUpTimerInSecs { get; set; }

	public float HealRate { get; set; }

	public float HealInterruptCooldownInSecs { get; set; }

	public bool DeathStreakEnabled { get; set; }

	public int DeathStreakInitialDeathsRequired { get; set; }

	public int DeathStreakSubsequentDeathsRequired { get; set; }

	public bool CoOpAllowPlayerTechMods { get; set; }

	public WorldPosition MapCenter { get; private set; }

	public float DangerDistance { get; private set; }

	public float TeleportDistance { get; private set; }

	public bool PushBackOutOfBounds { get; private set; }

	public float PushBackConst { get; private set; }

	public float PushBackDistance { get; private set; }

	public float PushBackVelocityCancel { get; private set; }

	public MapSizeOption MapSize { get; private set; }

	public int ChatMessageDisplayCountLimit { get; private set; }

	public float ChatMessageDisplayTimeSecs { get; private set; }

	public int ChosenLevelDataId { get; private set; }

	public string WorldSeed { get; set; }

	public string BiomeChoice { get; set; }

	public int WorldGenVersionID { get; set; }

	public int WorldGenVersionType { get; set; }

	public List<ManWorld.SavedSetPiece> SetPiecePlacements { get; set; }

	public LogFilter.FilterLevel LogLevel => m_NetworkManager.logLevel;

	public NetController NetController
	{
		get
		{
			return m_NetController;
		}
		set
		{
			if (m_NetController != null)
			{
				m_NetController.ServerPhaseEnterEvent.Unsubscribe(ForwardPhaseEvent);
				m_NetController.ClientPhaseEnterEvent.Unsubscribe(ForwardClientPhaseEvent);
			}
			if (value != null)
			{
				value.ServerPhaseEnterEvent.Subscribe(ForwardPhaseEvent);
				value.ClientPhaseEnterEvent.Subscribe(ForwardClientPhaseEvent);
			}
			m_NetController = value;
		}
	}

	public bool SetupWaitsForModeSwitch
	{
		get
		{
			return m_SetupWaitsForModeSwitch;
		}
		set
		{
			m_SetupWaitsForModeSwitch = value;
		}
	}

	public bool IsActive => CurState != State.Inactive;

	public State CurState
	{
		get
		{
			if (m_SetupState != SetupState.Idle)
			{
				return State.SettingUp;
			}
			if (NetworkServer.active || NetworkClient.active)
			{
				return State.InGame;
			}
			return State.Inactive;
		}
	}

	public SpawnPointBank m_ServerSpawnBank { get; private set; }

	public bool IsClientWaitingToJoin()
	{
		if (m_SetupState != SetupState.BeginJoiningGame)
		{
			return m_SetupState == SetupState.WaitToJoin;
		}
		return true;
	}

	public bool HasSecureTunnelEndPoint()
	{
		return m_NetworkManager.secureTunnelEndpoint != null;
	}

	public static void NetDumpObjects()
	{
		if (!(Singleton.Manager<ManNetwork>.inst != null))
		{
			return;
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder.AppendFormat("Network dump made on {0} at {1} (i am {2})\n\n", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), NetworkServer.active ? "server" : "client");
		stringBuilder.AppendFormat("===Begin Dump of All Network Objects===\n\n");
		foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> @object in ClientScene.objects)
		{
			NetworkIdentity value = @object.Value;
			if (value.IsNull())
			{
				continue;
			}
			IDumpableBehaviour dumpableBehaviour = null;
			NetworkBehaviour[] components = value.GetComponents<NetworkBehaviour>();
			for (int i = 0; i < components.Length; i++)
			{
				dumpableBehaviour = components[i] as IDumpableBehaviour;
				if (dumpableBehaviour != null)
				{
					break;
				}
			}
			if (dumpableBehaviour != null)
			{
				stringBuilder.AppendFormat("Object \"{0}\" ({1}) netId={2}\n{3}\n", value.name, dumpableBehaviour.GetType().Name, value.netId, "{");
				dumpableBehaviour.Dump(stringBuilder2);
				stringBuilder.Append("\t");
				while (stringBuilder2.Length > 0 && stringBuilder2[stringBuilder2.Length - 1] == '\n')
				{
					stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
				}
				stringBuilder2.Replace("\n", "\n\t");
				stringBuilder.Append(stringBuilder2.ToString());
				stringBuilder2.Clear();
				stringBuilder.Append("\n}\n");
			}
			else
			{
				stringBuilder.AppendFormat("Object \"{0}\" (Not Dumpable) netId={1}\n", value.name, value.netId);
			}
		}
		stringBuilder.AppendFormat("\n===End Dump of All Network Objects===\n\n");
		stringBuilder.AppendFormat("===Begin Dump of Mode===\n\n");
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent(Mode<ModeDeathmatch>.inst))
		{
			Mode<ModeDeathmatch>.inst.Dump(stringBuilder);
		}
		else
		{
			stringBuilder.AppendFormat("Unable to dump mode {0}\n", Singleton.Manager<ManGameMode>.inst.GetCurrentGameMode());
		}
		stringBuilder.AppendFormat("\nScreen type is \"{0}\"\n", (Singleton.Manager<ManUI>.inst.CurrentScreen() != null) ? Singleton.Manager<ManUI>.inst.CurrentScreen().ToString() : "none");
		stringBuilder.AppendFormat("Paused = {0}\n\n", Singleton.Manager<ManPauseGame>.inst.IsPaused);
		stringBuilder.AppendFormat("===End Dump of Mode===\n\n");
		stringBuilder.AppendFormat("===Begin Dump of Loaded Tiles===\n\n");
		if (Singleton.Manager<ManWorld>.inst != null && Singleton.Manager<ManWorld>.inst.TileManager != null)
		{
			foreach (IntVector2 loadedTile in Singleton.Manager<ManWorld>.inst.TileManager.GetLoadedTiles())
			{
				IntVector2 coord = loadedTile;
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord);
				if (worldTile != null)
				{
					stringBuilder.AppendFormat("Coord [{0}, {1}] = {2}->{3}\n", coord.x, coord.y, worldTile.m_LoadStep, worldTile.m_RequestState);
				}
			}
		}
		stringBuilder.AppendFormat("\n===End Dump of Loaded Tiles===\n\n");
		if (IsHost)
		{
			stringBuilder.AppendFormat("===Begin Dump of host per-connection data===\n\n");
			foreach (KeyValuePair<int, ConnectionData> perConnectionDatum in Singleton.Manager<ManNetwork>.inst.m_PerConnectionData)
			{
				ConnectionData value2 = perConnectionDatum.Value;
				string text = ((value2.m_Player == null) ? "[NULL]" : value2.m_Player.name);
				string text2 = ((value2.m_Player == null) ? "[NULL]" : value2.m_Player.netId.ToString());
				stringBuilder.AppendFormat($"Connection ID: {perConnectionDatum.Key}  Player name: {text}  Player NetID: {text2}  Num loaded tiles: {value2.m_LoadedTiles.Count}\n");
				foreach (IntVector2 item in from intVector in value2.m_LoadedTiles
					orderby -intVector.x
					orderby -intVector.y
					select intVector)
				{
					stringBuilder.AppendFormat($"\tLoaded tile: [{item.x}, {item.y}]\n");
				}
			}
			stringBuilder.AppendFormat("\n===End Dump of host per-connection data===\n\n");
		}
		stringBuilder.AppendFormat("===Begin Dump of Lobby===\n\n");
		if (Singleton.Manager<ManNetworkLobby>.inst != null && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Lobby currentLobby = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby;
			stringBuilder.AppendFormat("Lobby name={0} id={1} ownerId={2}\n\n", currentLobby.Name, currentLobby.ID, currentLobby.GetLobbyOwner());
			foreach (LobbyPlayerData player in currentLobby.GetPlayerList())
			{
				stringBuilder.AppendFormat("Player name={0}\tid={1}\tcolour={2}\tteamId={3}\n", player.m_Name, player.m_PlayerID, player.m_Colour, player.m_TeamID);
			}
		}
		else
		{
			stringBuilder.AppendFormat("Not in a lobby\n");
		}
		stringBuilder.AppendFormat("\n===End Dump of Lobby===\n\n");
		stringBuilder.AppendFormat("End of network dump");
		d.Log(stringBuilder.ToString());
	}

	private static void _checkNetLog()
	{
		d.Assert(!m_HasCheckedNetLog);
		m_HasCheckedNetLog = true;
		bool keepLogs = false;
		string path = "_debugHelper.txt";
		if (File.Exists(path))
		{
			string text = File.ReadAllText(path).ToLower();
			if (text.Contains("netlog"))
			{
				m_IsNetLogEnabled = true;
			}
			if (text.Contains("netmsg"))
			{
				m_LogNetworkMessageWrites = true;
			}
			if (text.Contains("netmetric"))
			{
				m_IsNetworkMetricsEnabled = true;
			}
			if (text.Contains("autodump1sec"))
			{
				m_AutoDumpStats = EnumAutoDumpStats.ADS_EVERY_SEC;
			}
			if (text.Contains("autodump15sec"))
			{
				m_AutoDumpStats = EnumAutoDumpStats.ADS_EVERY_FIFTEEN_SECS;
			}
			if (text.Contains("autodump30sec"))
			{
				m_AutoDumpStats = EnumAutoDumpStats.ADS_EVERY_THIRTY_SECS;
			}
			if (text.Contains("keeplogs"))
			{
				keepLogs = true;
			}
		}
		if (m_IsNetLogEnabled && !DebugNetworkLog.InitNetworkLog(keepLogs))
		{
			m_IsNetLogEnabled = false;
		}
		d.Assert(m_HasCheckedNetLog);
	}

	private static void _dumpNetworkMessageMetrics()
	{
		d.Assert(m_IsNetLogEnabled);
		d.Assert(m_IsNetworkMetricsEnabled);
		IOrderedEnumerable<KeyValuePair<short, NetworkMsgMetrics>> orderedEnumerable = m_networkMsgMetrics.OrderByDescending(delegate(KeyValuePair<short, NetworkMsgMetrics> entry)
		{
			KeyValuePair<short, NetworkMsgMetrics> keyValuePair = entry;
			return keyValuePair.Value.nTotalBytes;
		});
		ulong num = 0uL;
		ulong num2 = 0uL;
		foreach (KeyValuePair<short, NetworkMsgMetrics> item in orderedEnumerable)
		{
			NetworkMsgMetrics value = item.Value;
			if (value.nCount != 0)
			{
				_ = (double)value.nTotalBytes / (double)value.nCount;
			}
			num += value.nCount;
			num2 += value.nTotalBytes;
		}
		if (num != 0L)
		{
			_ = (double)num2 / (double)num;
		}
	}

	private static void _dumpNetworkStats()
	{
		if (m_IsNetLogEnabled && m_IsNetworkMetricsEnabled && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			TTNetworkConnection.NetworkStats networkStats = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.GetNetworkStats();
			if (networkStats.nReliableBytesReceived == 0 && networkStats.nUnreliableBytesSent == 0 && networkStats.nReliableBytesSent == 0)
			{
				_ = networkStats.nUnreliableBytesReceived;
				_ = 0;
			}
		}
	}

	private static void _autoDumpStatsAndResetTime(float tmToReset)
	{
		d.Assert(m_autoDumpStatsTimer >= tmToReset);
		m_autoDumpStatsTimer -= tmToReset;
		_dumpNetworkStats();
	}

	private static void _autoDumpStats()
	{
		if (!m_IsNetLogEnabled || !m_IsNetworkMetricsEnabled)
		{
			return;
		}
		m_autoDumpStatsTimer += Time.deltaTime;
		switch (m_AutoDumpStats)
		{
		case EnumAutoDumpStats.ADS_EVERY_SEC:
			if (m_autoDumpStatsTimer >= 1f)
			{
				_autoDumpStatsAndResetTime(1f);
			}
			break;
		case EnumAutoDumpStats.ADS_EVERY_FIFTEEN_SECS:
			if (m_autoDumpStatsTimer >= 15f)
			{
				_autoDumpStatsAndResetTime(15f);
			}
			break;
		case EnumAutoDumpStats.ADS_EVERY_THIRTY_SECS:
			if (m_autoDumpStatsTimer >= 30f)
			{
				_autoDumpStatsAndResetTime(30f);
			}
			break;
		default:
			d.Assert(condition: false);
			break;
		case EnumAutoDumpStats.ADS_NONE:
			break;
		}
	}

	private static string GetMsgString(TTMsgType msgType, string msg)
	{
		return "MSGTYPE=" + msgType.ToString() + " " + msg;
	}

	public uint GetNextHostBlockPoolID()
	{
		d.Assert(IsServerOrWillBe, "This function should only be used to get 'neutral' blockIDs for stuff spawned by the host such as starter techs and enemies");
		return m_NetworkManager.GetNextHostBlockPoolID();
	}

	public void ForceClientDisconnection(TTNetworkConnection netConn)
	{
		d.Assert(netConn != null);
		m_NetworkManager.OnClientDisconnect(netConn);
	}

	public bool IsMultiplayerAvailable()
	{
		if (SKU.IsEpicGS || SKU.UsesEOS)
		{
			return Singleton.Manager<ManEOS>.inst.IsConnected;
		}
		if (!SKU.IsSteam && !SKU.IsLAN_MP)
		{
			return SKU.IsNetEase;
		}
		return true;
	}

	public bool IsMultiplayerAndInvulnerable()
	{
		if (IsMultiplayer() && NetController != null && NetController.GameModeType == MultiplayerModeType.Deathmatch && m_MyPlayer != null && m_MyPlayer.CurTech != null && m_MyPlayer.CurTech.IsInvulnerable())
		{
			return true;
		}
		return false;
	}

	private void RemovePlayerFromTeamList(NetPlayer player)
	{
		if (m_PlayersOnTeam.TryGetValue(player.LobbyTeamID, out var value))
		{
			value.Remove(player.PlayerID);
			if (value.Count == 0)
			{
				m_PlayersOnTeam.Remove(player.LobbyTeamID);
			}
		}
	}

	public int GetTeamCount()
	{
		return m_PlayersOnTeam.Keys.Count;
	}

	public void AddPlayerToTeamList(NetPlayer player)
	{
		if (!m_PlayersOnTeam.TryGetValue(player.LobbyTeamID, out var value))
		{
			m_PlayersOnTeam.Add(player.LobbyTeamID, new List<int>());
			value = m_PlayersOnTeam[player.LobbyTeamID];
		}
		value.Add(player.PlayerID);
	}

	public bool IsMultiplayerAndNotPlaying()
	{
		if (IsMultiplayer())
		{
			if (NetController != null)
			{
				return NetController.CurrentPhase != NetController.Phase.Playing;
			}
			return true;
		}
		return false;
	}

	public void StartAsHost()
	{
		d.AssertFormat(m_SetupState == SetupState.Idle, "ManNetwork.QueueStartAsHost is not in idle state (is in state {0})", m_SetupState);
		m_SetupState = SetupState.BeginHostingGame;
		m_SetupHostConn = null;
		m_IgnoreServerDisconnect = true;
		m_AllClientsConnected = false;
		m_AllPlayersPresent = false;
		if (!SetupWaitsForModeSwitch)
		{
			BeginSetup();
		}
	}

	public void StopHost()
	{
		m_NetworkManager.StopHost();
	}

	public void StartAsClient(NetworkConnection conn)
	{
		d.AssertFormat(m_SetupState == SetupState.Idle, "ManNetwork.QueueStartAsHost is not in idle state (is in state {0})", m_SetupState);
		m_SetupState = SetupState.BeginJoiningGame;
		m_SetupHostConn = conn;
		m_IgnoreServerDisconnect = false;
		if (!SetupWaitsForModeSwitch)
		{
			BeginSetup();
		}
	}

	public void RegisterNetworkObject(Transform prefab)
	{
		m_NetworkManager.AddSpawnableType(prefab);
	}

	public void RegisterServerMessageHandlers()
	{
		RegisterServerHandler(97, OnHandleServerTargettedMessage);
		RegisterServerHandler(145, OnHandleServerSplitMessage);
		RegisterServerHandler(112, TTNetworkTransform.HandleTransform);
	}

	public void RegisterClientMessageHandlers()
	{
		RegisterClientHandler(97, OnHandleClientTargettedMessage);
		RegisterClientHandler(145, OnHandleClientSplitMessage);
		Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(OnDrag);
	}

	public void UnregisterClientMessageHandlers()
	{
		UnregisterClientHandler(97);
		UnregisterClientHandler(145);
		Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(OnDrag);
	}

	public void SendToServer(TTMsgType msgType, MessageBase message)
	{
		SendToServer(msgType, message, NetworkInstanceId.Invalid);
	}

	public void SendToServer(TTMsgType msgType, MessageBase message, NetworkInstanceId targetNetId)
	{
		_ = m_LogNetworkMessageWrites;
		if (Client.isConnected)
		{
			m_ListOfOneConnection[0] = Client.connection;
			SendWrappedMessage(msgType, message, targetNetId, m_ListOfOneConnection);
		}
		else
		{
			d.LogError("ManNetwork: Failed to send message to Server, not connected");
		}
	}

	public void SendToClient(int connectionId, TTMsgType msgType, MessageBase message)
	{
		SendToClient(connectionId, msgType, message, NetworkInstanceId.Invalid);
	}

	public void SendToClient(int connectionId, TTMsgType msgType, MessageBase message, NetworkInstanceId targetNetId)
	{
		_ = m_LogNetworkMessageWrites;
		if (connectionId >= 0 && connectionId < NetworkServer.connections.Count && NetworkServer.connections[connectionId] != null)
		{
			m_ListOfOneConnection[0] = NetworkServer.connections[connectionId];
			SendWrappedMessage(msgType, message, targetNetId, m_ListOfOneConnection);
		}
		else
		{
			d.LogError("ManNetwork: Failed to send message to connection ID '" + connectionId + ", not found in NetworkServer connection list");
		}
	}

	public void SendToAllExceptHost(TTMsgType msgType, MessageBase message)
	{
		SendToAllExceptClient(-1, msgType, message, alsoExcludeHost: true);
	}

	public void SendToAllExceptClient(int connectionId, TTMsgType msgType, MessageBase message, bool alsoExcludeHost = false)
	{
		SendToAllExceptClient(connectionId, msgType, message, NetworkInstanceId.Invalid, alsoExcludeHost);
	}

	public void SendToAllExceptHost(TTMsgType msgType, MessageBase message, NetworkInstanceId targetNetId)
	{
		SendToAllExceptClient(-1, msgType, message, targetNetId, alsoExcludeHost: true);
	}

	public void SendToAllExceptClient(int connectionId, TTMsgType msgType, MessageBase message, NetworkInstanceId targetNetId, bool alsoExcludeHost = false)
	{
		foreach (NetPlayer player in m_Players)
		{
			if (player != null && player.connectionToClient != null && player.connectionToClient.connectionId != connectionId && (!alsoExcludeHost || !(player == MyPlayer)))
			{
				SendToClient(player.connectionToClient.connectionId, msgType, message, targetNetId);
			}
		}
	}

	public void SendToAllClients(TTMsgType msgType, MessageBase message)
	{
		SendToAllClients(msgType, message, NetworkInstanceId.Invalid);
	}

	public void SendToAllClients(TTMsgType msgType, MessageBase message, NetworkInstanceId targetNetId)
	{
		_ = m_LogNetworkMessageWrites;
		SendWrappedMessage(msgType, message, targetNetId, NetworkServer.connections);
	}

	public bool IsMultiplayer()
	{
		return NetworkClient.active;
	}

	public bool IsInPhase(NetController.Phase phase)
	{
		if (NetController != null && NetController.CurrentPhase == phase)
		{
			return true;
		}
		return false;
	}

	public void StorePlayerConfig(NetworkConnection conn, LobbyPlayerData config)
	{
		if (!m_PlayerConfigs.ContainsKey(conn.connectionId))
		{
			m_PlayerConfigs.Add(conn.connectionId, config);
		}
		else
		{
			d.LogError("ManNetwork.StorePlayerConfig Trying to add duplicate config " + conn.connectionId);
		}
	}

	public void StoreLocalPlayerConfig(LobbyPlayerData config)
	{
		if (!m_PlayerConfigs.ContainsKey(0))
		{
			m_PlayerConfigs.Add(0, config);
		}
		else
		{
			d.LogError("ManNetwork.StoreLocalPlayerConfig Trying to add duplicate user 0!");
		}
	}

	public void OnServerConnect(NetworkConnection connection)
	{
		if (!m_PerConnectionData.ContainsKey(connection.connectionId))
		{
			ConnectionData value = new ConnectionData
			{
				m_Connection = connection,
				m_LoadedTiles = new HashSet<IntVector2>(Singleton.Manager<ManWorld>.inst.TileManager.GetLoadedTiles())
			};
			m_PerConnectionData.Add(connection.connectionId, value);
		}
	}

	public void OnServerDisconnect(NetworkConnection connection)
	{
		m_PerConnectionData.Remove(connection.connectionId);
	}

	public void SetConnectionHasLoadedTile(NetworkConnection connection, IntVector2 tilePos, bool loaded)
	{
		d.Log($"Connection {connection} has loaded {tilePos}={loaded}");
		if (m_PerConnectionData.TryGetValue(connection.connectionId, out var value))
		{
			if (loaded && !value.m_LoadedTiles.Contains(tilePos))
			{
				value.m_LoadedTiles.Add(tilePos);
				OnNetPlayerLoadedTile.Send(connection, tilePos, loaded);
			}
			if (!loaded && value.m_LoadedTiles.Contains(tilePos))
			{
				value.m_LoadedTiles.Remove(tilePos);
				OnNetPlayerLoadedTile.Send(connection, tilePos, loaded);
			}
		}
	}

	public void SetConnectionHasLoadedTiles(NetworkConnection connection, List<IntVector2> tiles)
	{
		if (!m_PerConnectionData.TryGetValue(connection.connectionId, out var value))
		{
			return;
		}
		HashSet<IntVector2> loadedTiles = value.m_LoadedTiles;
		HashSet<IntVector2> hashSet = (value.m_LoadedTiles = new HashSet<IntVector2>(tiles));
		if (!OnNetPlayerLoadedTile.HasSubscribers())
		{
			return;
		}
		foreach (IntVector2 item in hashSet)
		{
			if (!loadedTiles.Contains(item))
			{
				OnNetPlayerLoadedTile.Send(connection, item, paramC: true);
			}
		}
		foreach (IntVector2 item2 in loadedTiles)
		{
			if (!hashSet.Contains(item2))
			{
				OnNetPlayerLoadedTile.Send(connection, item2, paramC: false);
			}
		}
	}

	public bool GetAllObservers(HashSet<NetworkConnection> observers)
	{
		foreach (KeyValuePair<int, ConnectionData> perConnectionDatum in m_PerConnectionData)
		{
			if (perConnectionDatum.Value.m_Connection != null)
			{
				observers.Add(perConnectionDatum.Value.m_Connection);
			}
		}
		return true;
	}

	public bool CheckObserver(Visible visible, NetworkConnection connection)
	{
		if (visible.holderStack != null)
		{
			Tank tank = visible.holderStack.myHolder.block.tank;
			if (tank.IsNull())
			{
				return false;
			}
			visible = tank.visible;
		}
		return CheckObserver(visible.tileCache.tile, connection);
	}

	public bool CheckObserver(WorldTile tile, NetworkConnection connection)
	{
		if (m_PerConnectionData.TryGetValue(connection.connectionId, out var value))
		{
			if (m_MyPlayer.IsNotNull() && m_MyPlayer.connectionToClient == connection)
			{
				return true;
			}
			if (tile != null && value.m_LoadedTiles.Contains(tile.Coord))
			{
				return true;
			}
		}
		else
		{
			d.LogError($"Unknown connection {connection} passed to ManNetwork.CheckObserver(visible)");
		}
		return false;
	}

	public bool RebuildObservers(Visible visible, HashSet<NetworkConnection> observers)
	{
		if (visible.holderStack != null)
		{
			Tank tank = visible.holderStack.myHolder.block.tank;
			if (tank.IsNull())
			{
				return true;
			}
			visible = tank.visible;
		}
		return RebuildObservers(visible.tileCache.tile, observers);
	}

	public bool RebuildObservers(WorldTile tile, HashSet<NetworkConnection> observers)
	{
		if (m_MyPlayer.IsNotNull())
		{
			observers.Add(MyPlayer.connectionToClient);
		}
		if (tile != null)
		{
			foreach (KeyValuePair<int, ConnectionData> perConnectionDatum in m_PerConnectionData)
			{
				ConnectionData value = perConnectionDatum.Value;
				if (value.m_Connection.isReady && value.m_LoadedTiles.Contains(tile.Coord))
				{
					observers.Add(value.m_Connection);
				}
			}
		}
		return true;
	}

	public bool CheckObserver(NetTech tech, NetworkConnection connection)
	{
		if (CheckObserver(tech.tech.visible, connection))
		{
			return true;
		}
		if (m_PerConnectionData.TryGetValue(connection.connectionId, out var value) && value.m_Player.IsNotNull() && value.m_Player.CurTech == tech)
		{
			return true;
		}
		return false;
	}

	public bool RebuildObservers(NetTech tech, HashSet<NetworkConnection> observers)
	{
		if (tech.NetPlayer != null)
		{
			return GetAllObservers(observers);
		}
		RebuildObservers(tech.tech.visible, observers);
		for (int i = 0; i < m_Players.Count; i++)
		{
			NetPlayer netPlayer = m_Players[i];
			if (netPlayer.CurTech == tech)
			{
				observers.Add(netPlayer.connectionToClient);
			}
		}
		return true;
	}

	public bool IsPlayerTechID(int id)
	{
		foreach (NetPlayer player in m_Players)
		{
			if (player.CurTech.IsNotNull() && player.CurTech.tech.visible.ID == id)
			{
				return true;
			}
		}
		return false;
	}

	public bool RetrieveAndDeletePlayerConfig(NetworkConnection conn, out LobbyPlayerData config)
	{
		bool num = m_PlayerConfigs.TryGetValue(conn.connectionId, out config);
		if (num)
		{
			m_PlayerConfigs.Remove(conn.connectionId);
		}
		return num;
	}

	public void SetAllClientsConnected()
	{
		m_AllClientsConnected = true;
		if (!m_AllPlayersPresent && m_PlayerConfigs.Count == 0)
		{
			m_AllPlayersPresent = true;
			OnAllExpectedPlayersJoined.Send();
		}
	}

	private bool IsLocalPlayer_FirstFrameCheck(NetPlayer player)
	{
		if (player.isLocalPlayer)
		{
			return true;
		}
		return player.GetPlayerIDInLobby() == Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLocalPlayerID();
	}

	public void AddPlayer(NetPlayer player)
	{
		bool flag = IsLocalPlayer_FirstFrameCheck(player);
		if (SKU.IsNetEase && flag)
		{
			player.name = Singleton.Manager<ManNetEase>.inst.PlayerNickname;
			player.SetName(Singleton.Manager<ManNetEase>.inst.PlayerNickname);
		}
		m_Players.Add(player);
		if (IsHost && player.connectionToClient != null && m_PerConnectionData.TryGetValue(player.connectionToClient.connectionId, out var value))
		{
			value.m_Player = player;
		}
		if (flag)
		{
			m_MyPlayer = player;
		}
		else if ((bool)NetController && NetController.CurrentPhase == NetController.Phase.Playing && (bool)m_MyPlayer)
		{
			m_MyPlayer.InformPlayerArrival(player);
		}
		AddPlayerToTeamList(player);
		if (!flag && IsHost)
		{
			SendToClient(player.connectionToClient.connectionId, TTMsgType.LockTreadmill, new LockTreadmillMessage
			{
				m_FloatingOrigin = Singleton.Manager<ManWorld>.inst.FloatingOrigin
			});
			Singleton.Manager<ManVisible>.inst.SendTrackedVisiblesToClient(player);
			if (Singleton.Manager<ManGameMode>.inst.CanEarnMoney())
			{
				Singleton.Manager<ManPlayer>.inst.SendMoneyLevelToClient(player);
			}
			if (Singleton.Manager<ManGameMode>.inst.CanEarnXp())
			{
				Singleton.Manager<ManLicenses>.inst.SendXpLevelsToClient(player);
			}
			if (Singleton.Manager<ManGameMode>.inst.ShareEncounters())
			{
				Singleton.Manager<ManEncounter>.inst.SendEncountersToClient(player);
			}
			Singleton.Manager<ManWorld>.inst.NetSendAllSetPiecesTo(player);
		}
		OnPlayerAdded.Send(player);
		player.OnTeamChanged.Subscribe(OnPlayerTeamChanged);
		if (!m_AllPlayersPresent && m_AllClientsConnected && m_PlayerConfigs.Count == 0)
		{
			m_AllPlayersPresent = true;
			OnAllExpectedPlayersJoined.Send();
		}
	}

	public void RemovePlayer(NetPlayer player)
	{
		m_Players.Remove(player);
		if (player.IsActuallyLocalPlayer)
		{
			m_MyPlayer = null;
		}
		foreach (KeyValuePair<int, ConnectionData> perConnectionDatum in m_PerConnectionData)
		{
			if (perConnectionDatum.Value.m_Player == player)
			{
				perConnectionDatum.Value.m_Player = null;
			}
		}
		RemovePlayerFromTeamList(player);
		OnPlayerRemoved.Send(player);
		player.OnTeamChanged.Unsubscribe(OnPlayerTeamChanged);
	}

	public int GetNumPlayers()
	{
		return m_Players.Count;
	}

	public NetPlayer GetPlayer(int ind)
	{
		return m_Players[ind];
	}

	public NetPlayer FindPlayerByConnection(NetworkConnection connection)
	{
		NetPlayer result = null;
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].NetIdentity.connectionToClient == connection)
			{
				result = m_Players[i];
				break;
			}
		}
		return result;
	}

	public NetPlayer FindPlayerById(uint idValue)
	{
		NetPlayer result = null;
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].netId.Value == idValue)
			{
				result = m_Players[i];
				break;
			}
		}
		return result;
	}

	public NetPlayer FindPlayerByNetworkID(TTNetworkID netID)
	{
		NetPlayer result = null;
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].GetPlayerIDInLobby() == netID)
			{
				result = m_Players[i];
				break;
			}
		}
		return result;
	}

	public List<Tank> GetAllPlayerTechs()
	{
		m_AllPlayerTechs.Clear();
		if (IsMultiplayer())
		{
			for (int i = 0; i < m_Players.Count; i++)
			{
				NetTech curTech = m_Players[i].CurTech;
				if (curTech != null && curTech.tech.IsNotNull())
				{
					m_AllPlayerTechs.Add(curTech.tech);
				}
			}
		}
		else if ((bool)Singleton.playerTank)
		{
			m_AllPlayerTechs.Add(Singleton.playerTank);
		}
		return m_AllPlayerTechs;
	}

	public NetPlayer FindPlayerByPlayerID(int playerID)
	{
		NetPlayer result = null;
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].PlayerID == playerID)
			{
				result = m_Players[i];
				break;
			}
		}
		return result;
	}

	public bool ServerHasTileVisibilityChangedForAnyClient(WorldTile oldTile, WorldTile newTile)
	{
		d.Assert(newTile != null);
		foreach (KeyValuePair<int, ConnectionData> perConnectionDatum in m_PerConnectionData)
		{
			if ((bool)m_MyPlayer && perConnectionDatum.Value.m_Connection == m_MyPlayer.connectionToClient)
			{
				continue;
			}
			HashSet<IntVector2> loadedTiles = perConnectionDatum.Value.m_LoadedTiles;
			if (oldTile == null)
			{
				if (loadedTiles.Contains(newTile.Coord))
				{
					return true;
				}
			}
			else if (loadedTiles.Contains(oldTile.Coord) != loadedTiles.Contains(newTile.Coord))
			{
				return true;
			}
		}
		return false;
	}

	public void SetAuthorityReason(NetworkInstanceId netId, AuthorityReason reason)
	{
		ClearAuthorityReason(netId);
		m_ItemsWithAuthority.Add(netId.Value, reason);
	}

	public void ClearAuthorityReason(NetworkInstanceId netId)
	{
		if (m_ItemsWithAuthority.ContainsKey(netId.Value))
		{
			m_ItemsWithAuthority.Remove(netId.Value);
		}
	}

	public AuthorityReason GetAuthorityReason(NetworkInstanceId netId)
	{
		if (!m_ItemsWithAuthority.TryGetValue(netId.Value, out var value))
		{
			return AuthorityReason.NoAuthority;
		}
		return value;
	}

	[Obsolete("Method is not used in up-to-date code. Prefer ManSpawn.SpawnNetworkedTechRef instead")]
	public TrackedVisible SpawnNetworkedNonPlayerTech(TechData techData, uint[] techDataBlockPoolIDs, Vector3 pos, Quaternion rot, bool grounded)
	{
		TrackedVisible trackedVisible = null;
		if (IsServer)
		{
			trackedVisible = TTNetworkManager.SpawnEmptyTechRef(-1, pos, rot, grounded);
			Tank tank = trackedVisible.visible.tank;
			d.Assert(techData != null);
			d.Assert(techDataBlockPoolIDs != null);
			d.Assert(techData.m_BlockSpecs.Count == techDataBlockPoolIDs.Length);
			tank.netTech.OnServerSetupTech(techData, techDataBlockPoolIDs, recycleFailedAdds: true);
			tank.netTech.OnServerSetTeam(tank.Team, tank.IsPopulation, justSerialisedValue: true);
			NetworkServer.Spawn(tank.gameObject);
			tank.SetTeam(tank.Team, tank.IsPopulation);
		}
		return trackedVisible;
	}

	public void QueueChunkForNetworkReplacement(ResourcePickup chunk)
	{
		if (!m_NonNetworkedChunksToReplace.Contains(chunk))
		{
			m_NonNetworkedChunksToReplace.Add(chunk);
		}
	}

	public void GrabFailed(NetBlock netBlock)
	{
		ReleaseDraggedItemWithoutSendingCommand();
		m_NetBlocksAwaitingGrabAuthority.Remove(netBlock);
		m_NetBlocksReleasedBeforeReceivingGrabAuthority.Remove(netBlock);
	}

	public void ReleaseDraggedItemWithoutSendingCommand()
	{
		m_PreventReleaseCommand = true;
		Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem(applyVelocity: false);
		m_PreventReleaseCommand = false;
	}

	public void OnBlockGainedAuthority(NetBlockChunk netBC)
	{
		m_NetBlocksAwaitingGrabAuthority.Remove(netBC);
		if (m_NetBlocksReleasedBeforeReceivingGrabAuthority.Remove(netBC))
		{
			Visible visible = netBC.visible;
			SendBlockReleaseMessage(visible, Vector3.zero);
		}
	}

	public void HandleBlockUndoAuthorityDenied(NetBlock netBlock)
	{
		BlockUndoAuthorityDenied.Send(netBlock);
	}

	public void SetSpawnBank(SpawnPointBank spawnBank)
	{
		m_ServerSpawnBank = spawnBank;
	}

	public void SetMapCenter(WorldPosition mapCenter)
	{
		MapCenter = mapCenter;
	}

	public bool IsPointOutsidePushbackBarrier(Vector3 scenePos)
	{
		if (!IsMultiplayer() || !PushBackOutOfBounds)
		{
			return false;
		}
		return (scenePos - MapCenter.ScenePosition).ToVector2XZ().magnitude > PushBackDistance;
	}

	public void SetMapSettings(WorldPosition mapCenter, float dangerDistance)
	{
		MapCenter = mapCenter;
		DangerDistance = dangerDistance;
	}

	public void SetBoundaryPushbackSettings(float boundaryTeleportDistance, float pushBackConst, float pushBackDistance, float pushBackVelocityCancel)
	{
		PushBackOutOfBounds = true;
		TeleportDistance = boundaryTeleportDistance;
		PushBackConst = pushBackConst;
		PushBackDistance = pushBackDistance;
		PushBackVelocityCancel = pushBackVelocityCancel;
	}

	public void SetMapSize(MapSizeOption mapSize)
	{
		MapSize = mapSize;
	}

	public void SetChosenLevelId(int levelId)
	{
		ChosenLevelDataId = levelId;
	}

	public void SetChatMaxMessageDisplayCount(int maxNumLines)
	{
		ChatMessageDisplayCountLimit = maxNumLines;
	}

	public void SetChatMessageDisplayTime(float displayTimeSecs)
	{
		ChatMessageDisplayTimeSecs = displayTimeSecs;
	}

	public void OnClientBlockDamaged(NetBlock networkedTankBlock, ManDamage.DamageInfo info)
	{
		if (NetworkClient.active)
		{
			BlockDamagedMessage message = new BlockDamagedMessage
			{
				m_PlayerNetId = MyPlayer.netId,
				m_DamageInfo = info
			};
			SendToServer(TTMsgType.BlockDamaged, message, networkedTankBlock.netId);
		}
	}

	public void SubscribeToServerMessage(NetworkInstanceId id, TTMsgType msgType, MessageHandler handler)
	{
		SubscribeToMessage(id, msgType, MsgIdentifier.Recipient.Server, handler);
	}

	public void SubscribeToClientMessage(NetworkInstanceId id, TTMsgType msgType, MessageHandler handler)
	{
		SubscribeToMessage(id, msgType, MsgIdentifier.Recipient.Client, handler);
	}

	public void SubscribeToServerMessage(TTMsgType msgType, MessageHandler handler)
	{
		SubscribeToMessage(NetworkInstanceId.Invalid, msgType, MsgIdentifier.Recipient.Server, handler);
	}

	public void SubscribeToClientMessage(TTMsgType msgType, MessageHandler handler)
	{
		SubscribeToMessage(NetworkInstanceId.Invalid, msgType, MsgIdentifier.Recipient.Client, handler);
	}

	private void SubscribeToMessage(NetworkInstanceId id, TTMsgType TTMsgType, MsgIdentifier.Recipient recipient, MessageHandler handler)
	{
		MsgIdentifier msgIdentifier = new MsgIdentifier
		{
			id = id.Value,
			msgType = (short)TTMsgType,
			recipient = recipient
		};
		if (!m_MessageHandlers.ContainsKey(msgIdentifier))
		{
			m_MessageHandlers.Add(msgIdentifier, handler);
			if (!m_MessageSubscriptions.TryGetValue(id.Value, out var value))
			{
				value = new List<MsgIdentifier>(1);
				m_MessageSubscriptions[id.Value] = value;
			}
			value.Add(msgIdentifier);
		}
		else
		{
			d.AssertFormat(false, "Already registered to handle the event {0}", TTMsgType);
		}
	}

	public void UnsubscribeFromServerMessage(NetworkInstanceId id, TTMsgType msgType, MessageHandler handler)
	{
		UnsubscribeFromMessage(id, msgType, MsgIdentifier.Recipient.Server, handler);
	}

	public void UnsubscribeFromClientMessage(NetworkInstanceId id, TTMsgType msgType, MessageHandler handler)
	{
		UnsubscribeFromMessage(id, msgType, MsgIdentifier.Recipient.Client, handler);
	}

	public void UnsubscribeFromServerMessage(TTMsgType msgType, MessageHandler handler)
	{
		UnsubscribeFromMessage(NetworkInstanceId.Invalid, msgType, MsgIdentifier.Recipient.Server, handler);
	}

	public void UnsubscribeFromClientMessage(TTMsgType msgType, MessageHandler handler)
	{
		UnsubscribeFromMessage(NetworkInstanceId.Invalid, msgType, MsgIdentifier.Recipient.Client, handler);
	}

	private void UnsubscribeFromMessage(NetworkInstanceId id, TTMsgType TTMsgType, MsgIdentifier.Recipient recipientType, MessageHandler handler)
	{
		MsgIdentifier msgIdentifier = new MsgIdentifier
		{
			id = id.Value,
			msgType = (short)TTMsgType,
			recipient = recipientType
		};
		if (m_MessageHandlers.Remove(msgIdentifier))
		{
			if (m_MessageSubscriptions.TryGetValue(id.Value, out var value))
			{
				value.Remove(msgIdentifier);
			}
		}
		else
		{
			d.LogError("UnsubscribeFromMessage - Could not find handler registered for this event");
		}
	}

	public void UnsubscribeFromMessages(NetworkInstanceId id)
	{
		if (m_MessageSubscriptions.TryGetValue(id.Value, out var value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				MsgIdentifier key = value[i];
				m_MessageHandlers.Remove(key);
			}
			m_MessageSubscriptions.Remove(id.Value);
		}
	}

	public void AddToDisposalContainer(Transform obj)
	{
		obj.parent = m_DisposalContainer;
	}

	public void ClearForRestart()
	{
		d.Assert(NetController.CurrentPhase == NetController.Phase.Restarting);
		TidyUp();
		TrackedVisible[] array = Singleton.Manager<ManVisible>.inst.AllTrackedVisibles.ToArray();
		foreach (TrackedVisible trackedVisible in array)
		{
			Singleton.Manager<ManVisible>.inst.StopTrackingVisible(trackedVisible.ID);
		}
	}

	public void KickedTidyUp()
	{
		TidyUp();
	}

	public void CleanUpAllScreens()
	{
		bool flag = false;
		if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack())
		{
			flag = true;
		}
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		if (flag)
		{
			Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
		}
	}

	private void SendBlockReleaseMessage(Visible visible, Vector3 draggingVelocity)
	{
		TankBlock block = visible.block;
		ResourcePickup pickup = visible.pickup;
		d.Assert(block != null || pickup != null, "Calling SendBlockReleaseMessage with non-chunk, non-block visible " + visible.name);
		uint blockPoolID = (block.IsNotNull() ? block.blockPoolID : pickup.blockPoolID);
		NetBlockChunk netBlockChunk = (block.IsNotNull() ? ((NetBlockChunk)block.netBlock) : ((NetBlockChunk)pickup.netChunk));
		Vector3 velocity = (visible.rbody.IsNotNull() ? visible.rbody.velocity : Vector3.zero);
		Vector3 angularVelocity = (visible.rbody.IsNotNull() ? visible.rbody.angularVelocity : Vector3.zero);
		BlockReleaseMessage message = new BlockReleaseMessage
		{
			m_BlockPoolID = blockPoolID,
			m_Position = WorldPosition.FromScenePosition(visible.trans.position),
			m_Rotation = visible.trans.rotation,
			m_Velocity = velocity,
			m_AngularVelocity = angularVelocity,
			m_DraggingItemVelocity = draggingVelocity
		};
		_ = m_IsNetLogEnabled;
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.BlockReleased, message, Singleton.Manager<ManNetwork>.inst.MyPlayer.netId);
		if (!Singleton.Manager<ManNetwork>.inst.IsServer && netBlockChunk != null)
		{
			netBlockChunk.ClientForciblyClearLocalAuthorityOnceBlockReleased();
		}
	}

	public void RecordSendMetrics(TTMsgType msgType, ulong nBytes)
	{
		if (m_IsNetworkMetricsEnabled)
		{
			if (!m_networkMsgMetrics.TryGetValue((short)msgType, out var value))
			{
				value = new NetworkMsgMetrics(msgType);
				m_networkMsgMetrics.Add((short)msgType, value);
			}
			value.nCount++;
			value.nTotalBytes += nBytes;
		}
	}

	public void RecordSendMetrics(TTMsgType msgType, NetworkWriter theNW)
	{
		if (m_IsNetworkMetricsEnabled)
		{
			RecordSendMetrics(msgType, (ulong)theNW.Position);
		}
	}

	public bool IsSpawnShieldActive(uint shieldID, ref NetSpawnPoint pNSP)
	{
		GameObject gameObject = ClientScene.FindLocalObject(new NetworkInstanceId(shieldID));
		if (gameObject != null && gameObject.activeInHierarchy)
		{
			NetSpawnPoint netSpawnPoint = (pNSP = gameObject.GetComponent<NetSpawnPoint>());
			if (netSpawnPoint != null)
			{
				return netSpawnPoint.IsBarrierEnabled();
			}
		}
		return false;
	}

	public bool BlockBlacklisted(BlockTypes type)
	{
		if (NetController != null && NetController.GameModeType.IsCoOp())
		{
			BlockCount[] blocks = Options.m_InventoryBlockList.Blocks;
			foreach (BlockCount blockCount in blocks)
			{
				if (type == blockCount.m_BlockType)
				{
					return true;
				}
			}
		}
		return false;
	}

	private void SendSplitMessage(byte[] bytes, int bytesToSend, IList<NetworkConnection> connections)
	{
		d.LogFormat("Sending split packet - total size = {0} in {1} packets", bytesToSend, (bytesToSend + 900 - 1) / 900);
		int num = 0;
		while (bytesToSend > 0)
		{
			int num2 = Math.Min(bytesToSend, 900);
			Array.Copy(bytes, num, m_SplittingBuffer, 0, num2);
			m_ChunkWriter.StartMessage(145);
			m_ChunkWriter.Write((byte)0);
			m_ChunkWriter.WriteBytesAndSize(m_SplittingBuffer, num2);
			m_ChunkWriter.FinishMessage();
			SendBytesRaw(m_ChunkWriter.ToArray(), m_ChunkWriter.Position, connections);
			num += num2;
			bytesToSend -= num2;
		}
		m_ChunkWriter.StartMessage(145);
		m_ChunkWriter.Write((byte)1);
		m_ChunkWriter.FinishMessage();
		SendBytesRaw(m_ChunkWriter.ToArray(), m_ChunkWriter.Position, connections);
	}

	private void SendBytesRaw(byte[] data, int dataLen, IList<NetworkConnection> connections)
	{
		int num = connections.Count();
		for (int i = 0; i < num; i++)
		{
			connections[i]?.SendBytes(data, dataLen, 0);
		}
	}

	private void SendWrappedMessage(TTMsgType msgType, MessageBase message, NetworkInstanceId targetNetId, IList<NetworkConnection> connections)
	{
		NetworkWriter networkWriter = new NetworkWriter();
		networkWriter.StartMessage(97);
		networkWriter.Write(targetNetId.Value);
		networkWriter.Write((short)msgType);
		message.Serialize(networkWriter);
		networkWriter.FinishMessage();
		if (m_IsNetworkMetricsEnabled)
		{
			RecordSendMetrics(msgType, networkWriter);
		}
		if (networkWriter.AsArray().Length >= 32768)
		{
			byte[] array = networkWriter.ToArray();
			SendSplitMessage(array, array.Length, connections);
		}
		else
		{
			SendBytesRaw(networkWriter.AsArray(), networkWriter.Position, connections);
		}
	}

	private void RegisterClientHandler(short msgType, NetworkMessageDelegate handler)
	{
		m_NetworkManager.client?.RegisterHandler(msgType, handler);
	}

	private void UnregisterClientHandler(short msgType)
	{
		m_NetworkManager.client?.UnregisterHandler(msgType);
	}

	private void RegisterServerHandler(short msgType, NetworkMessageDelegate handler)
	{
		NetworkServer.RegisterHandler(msgType, handler);
	}

	private void UnregisterServerHandler(short msgType)
	{
		NetworkServer.UnregisterHandler(msgType);
	}

	private void BeginSetup()
	{
		if (m_SetupState == SetupState.BeginHostingGame)
		{
			OnPreGameStarted.Send();
			if (SKU.IsLAN_MP)
			{
				NetworkServer.SetNetworkConnectionClass<TTNetworkConnection>();
			}
			m_NetworkManager.StartHost();
			m_SetupState = SetupState.Idle;
			OnServerHostStarted.Send();
			NetworkServer.Spawn(m_NetControllerPrefab.transform.Spawn().gameObject);
			Singleton.Manager<ManWorld>.inst.TileManager.OnHostNetworkInitialized();
		}
		else if (m_SetupState == SetupState.BeginJoiningGame)
		{
			OnPreGameStarted.Send();
			if (SetupWaitsForModeSwitch)
			{
				Singleton.Manager<ManGameMode>.inst.SuppressFadeIn();
			}
			m_JoinTimeoutCountdown = 15f;
			m_SetupState = SetupState.WaitToJoin;
			NetworkClient networkClient = null;
			networkClient = new NetworkClient(m_SetupHostConn);
			networkClient.SetNetworkConnectionClass<TTNetworkConnection>();
			bool isLAN_MP = SKU.IsLAN_MP;
			if (isLAN_MP)
			{
				m_NetworkManager.secureTunnelEndpoint = null;
				TTNetworkConnection obj = m_SetupHostConn as TTNetworkConnection;
				string networkAddress = obj.NetworkAddress;
				int networkPort = obj.NetworkPort;
				d.LogFormat("Establishing connection to IP '{0}' on port '{1}'", networkAddress, networkPort);
				networkClient.Connect(networkAddress, networkPort);
			}
			m_SetupHostConn = null;
			m_NetworkManager.UseExternalClient(networkClient);
			if (!isLAN_MP)
			{
				d.AssertFormat(SKU.IsSteam || SKU.UsesEOS || SKU.IsNetEase, "Unexpected SKU {0} might not handle NetworkClient Connection initialisation correctly! (Does it need adding to one of the 2 SKU lists?)", SKU.CurrentBuildType);
				d.Log("[ManNetwork] sending connect signal");
				networkClient.connection.InvokeHandlerNoData(32);
			}
		}
	}

	private void TidyUp()
	{
		if (NetworkServer.active)
		{
			StopHost();
			m_PlayerConfigs.Clear();
		}
		else if (NetworkClient.active)
		{
			m_NetworkManager.StopClient();
		}
		if (NetworkServer.active)
		{
			NetworkServer.Shutdown();
		}
		NetworkClient.ShutdownAll();
		d.Assert(!NetworkServer.active);
		d.Assert(!NetworkClient.active);
		TidyupData();
	}

	private void TidyupData()
	{
		m_PlayersOnTeam.Clear();
		PushBackOutOfBounds = false;
		m_PerConnectionData.Clear();
		OnPostGameExited.Send();
	}

	private void DoGrab(NetBlockChunk netBlockChunk)
	{
		if (!m_NetBlocksAwaitingGrabAuthority.Contains(netBlockChunk))
		{
			m_NetBlocksAwaitingGrabAuthority.Add(netBlockChunk);
			BlockGrabMessage message = new BlockGrabMessage
			{
				m_NetId = netBlockChunk.netId
			};
			SendToServer(TTMsgType.BlockGrab, message, MyPlayer.netId);
		}
		m_NetBlocksReleasedBeforeReceivingGrabAuthority.Remove(netBlockChunk);
	}

	private void OnDrag(Visible held, ManPointer.DragAction da, Vector3 pos)
	{
		NetPlayer myPlayer = MyPlayer;
		if (!(myPlayer != null))
		{
			return;
		}
		if ((bool)held.pickup)
		{
			NetChunk netChunk = held.pickup.netChunk;
			switch (da)
			{
			case ManPointer.DragAction.Grab:
				if (netChunk != null)
				{
					DoGrab(netChunk);
				}
				break;
			case ManPointer.DragAction.ReleaseLoose:
			case ManPointer.DragAction.ReleaseAllowPlace:
				if (m_PreventReleaseCommand)
				{
					break;
				}
				if ((bool)netChunk)
				{
					if (netChunk.hasAuthority)
					{
						SendBlockReleaseMessage(held, Singleton.Manager<ManPointer>.inst.DraggingItemVelocity);
					}
					else if (m_NetBlocksAwaitingGrabAuthority.Contains(netChunk))
					{
						m_NetBlocksReleasedBeforeReceivingGrabAuthority.Add(netChunk);
					}
				}
				else
				{
					SendBlockReleaseMessage(held, Singleton.Manager<ManPointer>.inst.DraggingItemVelocity);
				}
				break;
			}
		}
		else
		{
			if (!held.block)
			{
				return;
			}
			NetBlock netBlock = held.block.netBlock;
			if (!(held.block.tank == null))
			{
				return;
			}
			switch (da)
			{
			case ManPointer.DragAction.Grab:
			{
				if (Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock || Singleton.Manager<ManSpawn>.inst.IsSpawningDebugPaintingBlock)
				{
					return;
				}
				bool flag = netBlock != null;
				if (netBlock != null)
				{
					uint num = ((myPlayer.CurTech != null) ? myPlayer.CurTech.InitialSpawnShieldID : 0u);
					uint initialSpawnShieldID = netBlock.InitialSpawnShieldID;
					if (initialSpawnShieldID != 0 && initialSpawnShieldID != num)
					{
						NetSpawnPoint pNSP = null;
						if (IsSpawnShieldActive(initialSpawnShieldID, ref pNSP))
						{
							flag = false;
						}
					}
				}
				if (flag)
				{
					DoGrab(netBlock);
				}
				return;
			}
			case ManPointer.DragAction.ReleaseLoose:
			case ManPointer.DragAction.ReleaseAllowPlace:
				if (!held.gameObject.activeInHierarchy || m_PreventReleaseCommand)
				{
					return;
				}
				if ((bool)netBlock)
				{
					if (netBlock.hasAuthority)
					{
						if (Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab)
						{
							myPlayer.SwitchToNextTech();
						}
						if (Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock && (bool)myPlayer.CurTech)
						{
							NetTech curTech = myPlayer.CurTech;
							NetSpawnPoint pNSP2 = null;
							if (curTech != null && IsSpawnShieldActive(curTech.InitialSpawnShieldID, ref pNSP2))
							{
								netBlock.InitialSpawnShieldID = curTech.InitialSpawnShieldID;
							}
						}
						SendBlockReleaseMessage(held, Singleton.Manager<ManPointer>.inst.DraggingItemVelocity);
					}
					else if (m_NetBlocksAwaitingGrabAuthority.Contains(netBlock))
					{
						m_NetBlocksReleasedBeforeReceivingGrabAuthority.Add(netBlock);
					}
				}
				else
				{
					if (Singleton.Manager<ManTechBuilder>.inst.DraggingPlayerCab)
					{
						myPlayer.SwitchToNextTech();
					}
					SendBlockReleaseMessage(held, Singleton.Manager<ManPointer>.inst.DraggingItemVelocity);
				}
				return;
			}
			if (!(netBlock != null) || !netBlock.block.damage.DisplayOutOfShieldFeedback())
			{
				return;
			}
			uint shieldID = (myPlayer.CurTech ? myPlayer.CurTech.InitialSpawnShieldID : 0u);
			NetSpawnPoint pNSP3 = null;
			if (IsSpawnShieldActive(shieldID, ref pNSP3))
			{
				Vector3 position = netBlock.block.trans.position;
				float num2 = pNSP3.Radius * pNSP3.Radius;
				if (!((pNSP3.trans.position - position).sqrMagnitude <= num2) && !netBlock.block.damage.HasMaterialPulse())
				{
					netBlock.block.damage.SetOutOfShieldFeedback();
				}
			}
		}
	}

	private void ForwardPhaseEvent(NetController.Phase phase)
	{
		ServerPhaseEnterEvent.Send(phase);
		if ((uint)phase > 1u)
		{
			_ = 5;
		}
	}

	private void ForwardClientPhaseEvent(NetController.Phase phase)
	{
		d.Log("Setting phase: " + phase);
		ClientPhaseEnterEvent.Send(phase);
		if (phase == NetController.Phase.Outro)
		{
			m_IgnoreServerDisconnect = true;
		}
	}

	private void UpdateMissingMyPlayerReference()
	{
		if (!(m_MyPlayer == null))
		{
			return;
		}
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].isLocalPlayer)
			{
				d.LogError("THIS MYPLAYER REFERENCE FIXUP SHOULD NO LONGER BE NECESSARY");
				m_MyPlayer = m_Players[i];
				if (m_MyPlayer.CurTech != null)
				{
					m_MyPlayer.CurTech.ResetWheelNetworkedState();
				}
				break;
			}
		}
	}

	private void OnHandleServerSplitMessage(NetworkMessage netMsg)
	{
		OnHandleSplitMessage(netMsg, MsgIdentifier.Recipient.Server);
	}

	private void OnHandleClientSplitMessage(NetworkMessage netMsg)
	{
		OnHandleSplitMessage(netMsg, MsgIdentifier.Recipient.Client);
	}

	private void OnHandleSplitMessage(NetworkMessage netMsg, MsgIdentifier.Recipient recipient)
	{
		if (!m_MessageJoiners.TryGetValue(netMsg.conn.connectionId, out var value))
		{
			value = new SplitMessageJoiner();
			m_MessageJoiners.Add(netMsg.conn.connectionId, value);
		}
		if (recipient == MsgIdentifier.Recipient.Client)
		{
			value.OnHandleMsg(netMsg, m_NetworkManager.client.handlers);
		}
		else
		{
			value.OnHandleMsg(netMsg, NetworkServer.handlers);
		}
	}

	private void OnHandleServerTargettedMessage(NetworkMessage msg)
	{
		OnHandleTargettedMessage(msg, MsgIdentifier.Recipient.Server);
	}

	private void OnHandleClientTargettedMessage(NetworkMessage msg)
	{
		OnHandleTargettedMessage(msg, MsgIdentifier.Recipient.Client);
	}

	private void OnHandleTargettedMessage(NetworkMessage netMsg, MsgIdentifier.Recipient recipient)
	{
		uint num = netMsg.reader.ReadUInt32();
		short num2 = netMsg.reader.ReadInt16();
		MsgIdentifier key = new MsgIdentifier
		{
			id = num,
			msgType = num2,
			recipient = recipient
		};
		if (m_MessageHandlers.TryGetValue(key, out var value))
		{
			value(netMsg);
		}
		else if (!m_IgnoreUnhandledTargettedMessageWarning.Contains(num2))
		{
			object arg = num;
			object arg2 = num2;
			TTMsgType tTMsgType = (TTMsgType)num2;
			_ = $"No message handler registered for NetId={arg} and MsgType={arg2} ({tTMsgType.ToString()})";
		}
	}

	private void OnPlayerTeamChanged(NetPlayer player)
	{
		OnPlayerChangedTeam.Send(player);
	}

	private void OnModeEntered(Mode mode)
	{
		if (mode.IsMultiplayer)
		{
			BeginSetup();
		}
	}

	private void OnModePreExit(Mode mode)
	{
		if (IsActive)
		{
			TidyUp();
		}
		if (mode.IsMultiplayer)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
		}
	}

	private void OnModeCleanup(Mode mode)
	{
		if (mode.IsMultiplayer)
		{
			MapSize = MapSizeOption.Medium;
			ChatMessageDisplayCountLimit = 0;
			ChatMessageDisplayTimeSecs = 0f;
		}
	}

	private void OnApplicationQuit()
	{
		if (IsMultiplayer())
		{
			TidyUp();
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
		}
	}

	private void OnServerStarted()
	{
		m_MessageJoiners.Clear();
	}

	private void OnServerStopped()
	{
		OnServerHostStopped.Send();
		m_ServerSpawnBank = null;
	}

	private void OnClientObjectSpawned()
	{
		if (m_SetupState == SetupState.WaitToJoin)
		{
			if (SetupWaitsForModeSwitch)
			{
				Singleton.Manager<ManUI>.inst.ClearFade(3f);
			}
			m_SetupState = SetupState.Idle;
		}
	}

	private void OnClientStopped()
	{
		if (m_SetupState == SetupState.WaitToJoin)
		{
			Singleton.Manager<ManUI>.inst.ClearFade(3f);
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
			};
			if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeAttract>())
			{
				accept = delegate
				{
					Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MainMenu);
				};
			}
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Networking, 0);
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString2, accept, localisedString);
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
			m_SetupState = SetupState.Idle;
		}
		else if (CurState == State.InGame && !m_IgnoreServerDisconnect)
		{
			if (!Singleton.Manager<ManGameMode>.inst.IsSwitchingMode || !Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
			{
				Singleton.Manager<ManUI>.inst.ClearFade(3f);
			}
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
			TidyupData();
			if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MultiplayerTechSelect))
			{
				CleanUpAllScreens();
			}
			Action accept2 = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
				if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MultiplayerTechSelect))
				{
					CleanUpAllScreens();
				}
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
			};
			string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 47);
			string localisedString4 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 46);
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString3, accept2, localisedString4);
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.NotInGame, show: false);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
		}
		else if (CurState == State.InGame && m_IgnoreServerDisconnect)
		{
			m_IgnoreServerDisconnect = false;
		}
	}

	private void Awake()
	{
		_checkNetLog();
		m_LogFilterCount = Enum.GetNames(typeof(DebugNetworkLog.LogFilter)).Length;
		for (int i = 0; i < m_LogFilterCount; i++)
		{
			if (m_DebugLogging[i])
			{
				DebugNetworkLog.SetLogLevel(i, set: true);
			}
		}
		m_NetworkManager = GetComponent<TTNetworkManager>();
		m_NetworkManager.connectionConfig.NetworkDropThreshold = 90;
		m_DisposalContainer = new GameObject("_disposalcontainer").transform;
		m_DisposalContainer.parent = base.transform;
		m_DisposalContainer.gameObject.SetActive(value: false);
		m_NetworkManager.AddSpawnableType(m_NetInventoryPrefab.transform);
		m_NetworkManager.AddSpawnableType(m_NetControllerPrefab.transform);
		m_NetworkManager.AddSpawnableType(m_NetSpawnPrefab.transform);
		m_NetworkManager.AddSpawnableType(Singleton.Manager<ManWorld>.inst.m_NetworkedTilePrefab.transform);
	}

	private void OnDestroy()
	{
		if (m_IsNetLogEnabled && m_IsNetworkMetricsEnabled)
		{
			_dumpNetworkMessageMetrics();
			DebugNetworkLog.Shutdown();
			m_IsNetLogEnabled = false;
		}
	}

	private void Start()
	{
		MapSize = MapSizeOption.Medium;
		ChatMessageDisplayCountLimit = 0;
		ChatMessageDisplayTimeSecs = 0f;
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(OnModeEntered);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModePreExit);
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
		m_NetworkManager.OnServerStarted.Subscribe(OnServerStarted);
		m_NetworkManager.OnServerStopped.Subscribe(OnServerStopped);
		m_NetworkManager.OnClientObjectSpawned.Subscribe(OnClientObjectSpawned);
		m_NetworkManager.OnClientStopped.Subscribe(OnClientStopped);
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.Update, ManUpdate.Order.First, UpdateMissingMyPlayerReference);
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		OnHostDuringPhysicsCallback = false;
		for (int i = 0; i < m_BlocksToRecycle.Count; i++)
		{
			m_BlocksToRecycle[i].trans.Recycle();
		}
		m_BlocksToRecycle.Clear();
		_autoDumpStats();
		if (m_SetupState == SetupState.WaitToJoin)
		{
			m_JoinTimeoutCountdown -= Time.deltaTime;
			if (m_JoinTimeoutCountdown < 0f)
			{
				d.LogError("Timeout when connecting to game");
				m_NetworkManager.StopClient();
			}
		}
		if (CurState == State.InGame && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.IsLobbyOwner() && !Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			d.LogError("Host migrated to client");
			m_NetworkManager.StopClient();
		}
	}

	public void SetEndPoint(EndPoint endPoint)
	{
		m_NetworkManager.secureTunnelEndpoint = endPoint;
	}
}
