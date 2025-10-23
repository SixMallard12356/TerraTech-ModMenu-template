#define UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetPlayer : NetworkBehaviour, ManNetwork.IDumpableBehaviour
{
	private enum HealState
	{
		NotHealing,
		WarmingUp,
		Healing
	}

	public enum ConnectionMsgTypes
	{
		Arrival,
		Departure
	}

	public class ScavengedBlock
	{
		public enum ScavengedBlockState
		{
			Rising,
			Moving
		}

		public ScavengedBlockState m_State;

		public TankBlock m_Block;

		public float m_StartingHeight;

		private float m_Timer = 1f;

		private const float kScavengedBlockHeight = 2f;

		private const float kScavengedBlockRiseSpeed = 3.5f;

		private const float kMinSize = 0.125f;

		public void Update(Vector3 tankPos)
		{
			switch (m_State)
			{
			case ScavengedBlockState.Rising:
				if (m_Block.transform.position.y - m_StartingHeight < 2f)
				{
					float y = m_Block.transform.position.y + 3.5f * Time.deltaTime;
					m_Block.transform.position = new Vector3(m_Block.transform.position.x, y, m_Block.transform.position.z);
				}
				else
				{
					m_State = ScavengedBlockState.Moving;
					m_Timer = 1f;
				}
				break;
			case ScavengedBlockState.Moving:
			{
				m_Timer = Mathf.Clamp(m_Timer - Time.deltaTime, 0f, 1f);
				m_Block.transform.position = Vector3.Lerp(m_Block.transform.position, tankPos, 1f - m_Timer);
				Vector3 b = new Vector3(0.125f, 0.125f, 0.125f);
				m_Block.transform.localScale = Vector3.Lerp(m_Block.transform.localScale, b, 1f - m_Timer);
				break;
			}
			}
		}
	}

	public const int k_InvalidPlayerID = -1;

	public Event<NetPlayer> NameSetEvent;

	public Event<NetPlayer> OnTeamChanged;

	public Event<NetPlayer> OnAvatarChanged;

	private const float kNotableBlockCooldownDelay = 5f;

	private const int kMaxTechLevels = 1;

	private const int kMaxCorporations = 4;

	private NetTech m_CurTech;

	private NetworkInstanceId m_ClientTechId = NetworkInstanceId.Invalid;

	private bool m_SwitchingTech;

	private uint m_InitialBlockPoolID;

	private uint m_NextBlockPoolID;

	private int m_PlayerID;

	private int m_LobbyTeamID;

	private NetInventory m_Inventory;

	private Color32 m_Colour;

	public const float kTimeBeforeRespawn = 3f;

	public float m_SwitchTechDelay;

	public bool m_HasRequestedSpawn;

	private bool m_FirstSpawn;

	private bool m_PickingTechToSpawn;

	private bool m_PickingTechToSpawnComplete;

	private float m_NotableBlockCooldownTimer;

	private NetScore m_Score = new NetScore();

	private UIScoreHUD m_ScoreHud;

	private UIMPTimeRemainingHUD m_TimeRemainingHud;

	private UIMPKillStreakClaimRewardHUD m_KillStreakClaimRewardHud;

	private bool m_PlayerActive = true;

	private int m_Lives = -1;

	private bool m_PlayerHost;

	private float m_OutOfBoundsPercentage;

	private TTNetworkID m_PlayerIDInLobby;

	private Sprite m_Sprite;

	private int m_NumberOfDeathsToEarnReward;

	private int m_CurrentDeathStreakRewardLevel;

	private MultiplayerKillStreakRewardAsset m_KillstreakRewards;

	private UIMultiplayerHUD m_MultiHud;

	private float m_DeferUIDelay;

	private int[] m_TechLevels = new int[4];

	private int m_LastCorporationSelected;

	private int m_LastBlockPaletteSelected;

	private bool m_FirstTechSelected;

	private float m_ConnectionMsgTimer;

	private HealState m_HealState;

	private bool m_BuildBeamOn;

	private float m_HealTimer;

	private float m_HealInterruptTimer;

	private const int kSer_PlayerAndTeamID = 1;

	private const int kSer_Name_F = 2;

	private const int kSer_Colour_F = 4;

	private const int kSer_Score_F = 8;

	private const int kSer_LobbyID_F = 16;

	private const int kSer_PlayerActive_F = 32;

	private const int kSer_PlayerLives_F = 64;

	private const int kSer_OutOfBoundsPercentage = 128;

	private const int kSer_BlockPoolID_F = 256;

	private const int kSer_TechSelected = 512;

	private const int kSer_Host_F = 1024;

	private const int kSer_CurrentHeldBlockPooldID = 2048;

	private const int kSer_CurrentTech_F = 4096;

	private const int kSer_AllFlagMask = 8191;

	private List<BlockTypes> m_LootedBlocks = new List<BlockTypes>(8);

	private List<BlockTypes> m_DeathStreakItems = new List<BlockTypes>(16);

	private List<ScavengedBlock> m_ScavengedBlocks = new List<ScavengedBlock>();

	private List<uint> m_LoadoutBlockPoolIDs;

	private List<ScavengedBlock> m_RemovedBlocks = new List<ScavengedBlock>();

	private bool m_SwitchToNextTech;

	public NetworkIdentity NetIdentity { get; private set; }

	public NetTech CurTech => m_CurTech;

	public int PlayerID => m_PlayerID;

	public int LobbyTeamID => m_LobbyTeamID;

	public int TechTeamID => ManSpawn.TechTeamIDFromLobbyTeamID(m_LobbyTeamID);

	public uint CurrentHeldBlockID { get; private set; }

	public NetScore Score => m_Score;

	public Color Colour => m_Colour;

	public Sprite Sprite => m_Sprite;

	public float OutOfBoundsPercentage => m_OutOfBoundsPercentage;

	public bool IsPlayerActive => m_PlayerActive;

	public bool IsFirstTechSelected => m_FirstTechSelected;

	public bool HasBeenDamagedByOpponent { get; set; }

	public bool IsBuildBeamOn => m_BuildBeamOn;

	public bool IsHostPlayer => m_PlayerHost;

	public bool IsSwitchingTech => m_SwitchingTech;

	public NetInventory Inventory => m_Inventory;

	public bool IsActuallyLocalPlayer => this == Singleton.Manager<ManNetwork>.inst.MyPlayer;

	public bool IsFirstSpawn => m_FirstSpawn;

	public void ResetDeathStreakRewards()
	{
		m_NumberOfDeathsToEarnReward = Singleton.Manager<ManNetwork>.inst.DeathStreakInitialDeathsRequired;
		m_CurrentDeathStreakRewardLevel = 0;
	}

	public void SetInventory(NetInventory inventory)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer && inventory.IsNotNull())
		{
			d.Assert(m_Inventory == null, "Should only call NetPlayer.SetInventory once on the server");
		}
		if (m_Inventory.IsNotNull())
		{
			m_Inventory.InventoryChanged.Unsubscribe(OnInventoryChanged);
		}
		m_Inventory = inventory;
		if (IsActuallyLocalPlayer)
		{
			Singleton.Manager<ManPurchases>.inst.SetInventory(inventory);
		}
		if (m_Inventory != null)
		{
			m_Inventory.InventoryChanged.Subscribe(OnInventoryChanged);
		}
	}

	public void Dump(StringBuilder builder)
	{
		builder.AppendFormat("PlayerID={0} LobbyTeamID={1}\n", PlayerID, LobbyTeamID);
		builder.AppendFormat("IsHost={0} IsActive={1} IsSwitching={2}\n", IsHostPlayer, IsPlayerActive, IsSwitchingTech);
		builder.AppendFormat("CurTech NetId={0}\n", (CurTech != null) ? CurTech.netId.ToString() : "n/a");
		builder.AppendFormat("HeldBlockId={0}\n", (CurrentHeldBlockID != uint.MaxValue) ? CurrentHeldBlockID.ToString() : "n/a");
		builder.AppendFormat("ReqSpawn={0} FirstSpawn={1} PickingSpawn={2} PickingSpawnComplete={3}\n", m_HasRequestedSpawn, m_FirstSpawn, m_PickingTechToSpawn, m_PickingTechToSpawnComplete);
		builder.AppendFormat("Colour={0} Score={1}\n", m_Colour, m_Score.Points);
		builder.AppendFormat("Defer UI delay={0}\n", m_DeferUIDelay);
	}

	public uint GetNextBlockPoolID()
	{
		d.Assert(IsActuallyLocalPlayer, "We are picking block IDs from another player's pool. This will cause all sorts of conflicts.");
		uint result = m_NextBlockPoolID++;
		d.Assert(m_NextBlockPoolID < m_InitialBlockPoolID + 500000, "We have exceeded half our block pool allowance! We should add code support for obtaining more block pool");
		return result;
	}

	public void UpdateNotablePlayerHUD(NetPlayer netPlayer)
	{
		m_ScoreHud.SetNotablePlayer(netPlayer);
	}

	public void InitScoreHUD()
	{
		if (!IsActuallyLocalPlayer)
		{
			return;
		}
		if (m_ScoreHud != null)
		{
			m_ScoreHud.InitScore(Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy);
			m_ScoreHud.InitLives(m_Lives);
			m_ScoreHud.Show(null);
		}
		if (m_TimeRemainingHud != null)
		{
			m_TimeRemainingHud.InitTimeRemaining();
			switch (Singleton.Manager<ManNetwork>.inst.NetController.GameModeType)
			{
			case MultiplayerModeType.CoOpCreative:
			case MultiplayerModeType.CoOpCampaign:
				Singleton.Manager<ManHUD>.inst.HideHudElement(m_TimeRemainingHud.HudElementType);
				break;
			case MultiplayerModeType.Deathmatch:
			case MultiplayerModeType.KingAnton:
				Singleton.Manager<ManHUD>.inst.ShowHudElement(m_TimeRemainingHud.HudElementType);
				break;
			default:
				d.LogError("Unsupported mode in InitScoreHUD");
				break;
			}
		}
		if (m_KillStreakClaimRewardHud != null)
		{
			m_KillStreakClaimRewardHud.InitKillStreakReward();
		}
	}

	[Server]
	public void OnServerSetCurrentHeldBlock(NetBlockChunk block)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetCurrentHeldBlock(NetBlockChunk)' called on client");
			return;
		}
		uint num = ((block == null) ? uint.MaxValue : block.BlockPoolID);
		if (CurrentHeldBlockID != num)
		{
			CurrentHeldBlockID = num;
			SetDirtyBit(2048u);
		}
	}

	[Server]
	public void OnServerSetOutOfBounds(float percentage)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetOutOfBounds(System.Single)' called on client");
		}
		else if (m_OutOfBoundsPercentage != percentage)
		{
			m_OutOfBoundsPercentage = percentage;
			SetDirtyBit(128u);
		}
	}

	[Server]
	public void OnServerSetLives(int numLives)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetLives(System.Int32)' called on client");
			return;
		}
		m_Lives = numLives;
		SetDirtyBit(64u);
	}

	[Server]
	public bool OnServerRemoveLife()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Boolean NetPlayer::OnServerRemoveLife()' called on client");
			return false;
		}
		if (m_Lives != -1)
		{
			m_Lives--;
			SetDirtyBit(64u);
			UpdateLivesHUD();
			if (m_Lives <= 0)
			{
				m_PlayerActive = false;
				SetDirtyBit(32u);
			}
		}
		return m_PlayerActive;
	}

	[Server]
	public void OnServerSetPlayerID(int playerID)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetPlayerID(System.Int32)' called on client");
			return;
		}
		m_PlayerID = playerID;
		SetDirtyBit(1u);
	}

	[Server]
	public void OnServerSetTeamID(int teamID)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetTeamID(System.Int32)' called on client");
			return;
		}
		if (m_LobbyTeamID != teamID)
		{
			m_LobbyTeamID = teamID;
			OnTeamChanged.Send(this);
		}
		SetDirtyBit(1u);
	}

	[Server]
	public void OnServerSetLobbyID(TTNetworkID lobbyId)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetLobbyID(TerraTech.Network.TTNetworkID)' called on client");
			return;
		}
		m_PlayerIDInLobby = lobbyId;
		FindSprite();
		SetDirtyBit(16u);
	}

	[Server]
	public void OnServerSetColour(Color32 colour)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetColour(UnityEngine.Color32)' called on client");
			return;
		}
		m_Colour = colour;
		SetDirtyBit(4u);
	}

	[Server]
	public void OnServerSetInitialBlockPoolID(uint blockPoolID)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetInitialBlockPoolID(System.UInt32)' called on client");
			return;
		}
		m_InitialBlockPoolID = blockPoolID;
		m_NextBlockPoolID = blockPoolID;
		SetDirtyBit(256u);
	}

	[Server]
	public void OnServerAddPoints(float addValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerAddPoints(System.Single)' called on client");
			return;
		}
		m_Score.AddPoints(addValue);
		SetDirtyBit(8u);
		UpdateScoreHUD();
	}

	[Server]
	public void OnServerSetPoints(float setValue)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerSetPoints(System.Single)' called on client");
			return;
		}
		m_Score.SetPoints(setValue);
		SetDirtyBit(8u);
		UpdateScoreHUD();
	}

	[Server]
	public void OnServerAddDeath(bool incDeathStreak)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerAddDeath(System.Boolean)' called on client");
			return;
		}
		m_Score.IncDeaths(incDeathStreak);
		SetDirtyBit(8u);
		UpdateScoreHUD();
	}

	[Server]
	public void OnServerAddKill()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::OnServerAddKill()' called on client");
			return;
		}
		m_Score.IncKills();
		SetDirtyBit(8u);
		UpdateScoreHUD();
	}

	public void SetName(string name, bool netPropogate = true)
	{
		base.name = name;
		NameSetEvent.Send(this);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			d.Assert(netPropogate, "We are setting the name on the server without net propogation which shouldn't be the case as all name changes propogate through the server");
			SetDirtyBit(2u);
		}
		else if (netPropogate)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetPlayerName, new SetPlayerName
			{
				name = name
			}, base.netId);
		}
	}

	public void SelectFirstTech()
	{
		if (NetworkServer.active)
		{
			m_FirstTechSelected = true;
			SetDirtyBit(512u);
		}
		else
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetFirstTechSelected, new SetFirstTechSelected(), base.netId);
		}
	}

	public void SwitchToNextTech()
	{
		m_SwitchToNextTech = true;
	}

	[Server]
	public void ServerSetTech(NetTech netTech, bool isBeingDestroyed)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetPlayer::ServerSetTech(NetTech,System.Boolean)' called on client");
		}
		else
		{
			if (!(netTech == null) && !(netTech.NetPlayer == null))
			{
				return;
			}
			if (m_CurTech != null)
			{
				m_CurTech.tech.beam.EnableBeam(enable: false);
				if (m_CurTech.NetIdentity.clientAuthorityOwner != null)
				{
					d.Assert(m_CurTech.NetIdentity.clientAuthorityOwner == base.connectionToClient, "ServerSetTech - Someone else had auth on our old tech");
					m_CurTech.NetIdentity.RemoveClientAuthority(m_CurTech.NetIdentity.clientAuthorityOwner);
				}
				m_CurTech.ServerSetOwner(null, isBeingDestroyed);
			}
			OnClientSetCurrentTech(netTech);
			SetDirtyBit(4096u);
			m_HasRequestedSpawn = false;
			m_SwitchTechDelay = 3f;
			if (m_CurTech != null)
			{
				m_CurTech.ServerSetOwner(this);
				if (m_CurTech.isServer)
				{
					m_CurTech.NetIdentity.AssignClientAuthority(base.connectionToClient);
				}
			}
		}
	}

	private void OnClientSetCurrentTech(NetTech netTech)
	{
		if (m_CurTech != null)
		{
			Singleton.Manager<ManTechs>.inst.TankDriverChangedEvent.Send(m_CurTech.tech);
		}
		OnSetTech(netTech);
		m_CurTech = netTech;
		m_ClientTechId = NetworkInstanceId.Invalid;
		if (m_CurTech != null)
		{
			m_CurTech.ResetWheelNetworkedState();
			Singleton.Manager<ManTechs>.inst.TankDriverChangedEvent.Send(m_CurTech.tech);
		}
		if (IsActuallyLocalPlayer)
		{
			if (netTech == null)
			{
				Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(null);
			}
			else
			{
				Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(netTech.tech);
			}
		}
	}

	public void OnSetTech(NetTech netTech)
	{
		if (netTech != null && netTech.tech != null)
		{
			netTech.Colour = m_Colour;
			if (CurTech == null)
			{
				m_LoadoutBlockPoolIDs = null;
			}
			_fixupLoadoutBlockReferences(netTech);
			if (Singleton.Manager<ManNetwork>.inst.IsServer && !m_FirstSpawn)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.TechRespawned, new UnityEngine.Networking.NetworkSystem.EmptyMessage(), base.netId);
			}
			m_FirstSpawn = false;
			m_ScavengedBlocks.Clear();
			if (this == Singleton.Manager<ManNetwork>.inst.MyPlayer)
			{
				if (Singleton.playerTank == null || m_SwitchToNextTech)
				{
					m_SwitchToNextTech = false;
				}
				if (Singleton.playerTank == null)
				{
					m_OutOfBoundsPercentage = 0f;
				}
			}
		}
		m_SwitchingTech = false;
	}

	private void OnClientTechSwapRejection(NetworkMessage netMsg)
	{
		m_SwitchingTech = false;
	}

	public bool IsHoldingBlock(TankBlock block)
	{
		bool result = false;
		if (TankBlock.IsBlockPoolIDValid(CurrentHeldBlockID) && Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(CurrentHeldBlockID) != null)
		{
			result = true;
		}
		return result;
	}

	public bool HasTech()
	{
		bool result = false;
		if (CurTech != null)
		{
			result = true;
		}
		else if (TankBlock.IsBlockPoolIDValid(CurrentHeldBlockID))
		{
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(CurrentHeldBlockID);
			if (tankBlock != null && tankBlock.IsController)
			{
				result = true;
			}
		}
		return result;
	}

	public bool HasTechWithId(NetworkInstanceId techNetId)
	{
		if ((bool)CurTech)
		{
			return CurTech.netId == techNetId;
		}
		return false;
	}

	public void ClientRequestReplaceTech(TechData newTechData, bool bypassInventory = false)
	{
		if (m_CurTech != null)
		{
			Vector3 scenePos = m_CurTech.tech.boundsCentreWorld;
			Quaternion rotation = m_CurTech.tech.trans.rotation * Quaternion.Inverse(m_CurTech.tech.rootBlockTrans.localRotation);
			if (m_CurTech.tech.grounded)
			{
				Singleton.Manager<ManWorld>.inst.GetTerrainHeight(scenePos, out var outHeight);
				Singleton.Manager<ManWorld>.inst.GetTerrainNormal(scenePos, out var outNormal);
				rotation = Maths.LookRotationUpInvariant(m_CurTech.tech.rootBlockTrans.forward, outNormal);
				float num = Singleton.Manager<ManTechSwapper>.inst.WheelClearancePad + 0.5f + newTechData.m_BoundsExtents.y;
				float num2 = outHeight + num;
				scenePos.y += num;
				if (scenePos.y < num2)
				{
					scenePos.y = num2;
				}
			}
			m_SwitchingTech = true;
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SwapTech, new SpawnTechMessage
			{
				m_TechData = newTechData,
				m_Position = WorldPosition.FromScenePosition(in scenePos),
				m_Rotation = rotation,
				m_Team = TechTeamID,
				m_IsPopulation = false,
				m_PlayerNetID = base.netId,
				m_CheatBypassInventory = bypassInventory,
				m_IsSpawnedByPlayer = true
			});
		}
		else
		{
			d.LogWarning("OnClientRequestReplaceTech called while NetPlayer didn't have a m_CurTech! Spawning in front of Camera.");
			d.Assert(this == Singleton.Manager<ManNetwork>.inst.MyPlayer, "OnClientRequestReplaceTech called without m_CurTech and from a different client than is the local player!");
			Vector3 scenePos = Singleton.cameraTrans.position + Singleton.cameraTrans.forward * 10f;
			scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos, hitScenery: true) + Vector3.up * (Singleton.Manager<ManTechSwapper>.inst.WheelClearancePad + 0.5f + newTechData.m_BoundsExtents.y);
			Quaternion rotation = Quaternion.LookRotation(Singleton.cameraTrans.forward, Vector3.up);
			ClientRequestSpawnTech(newTechData, scenePos, rotation, replace: true, bypassInventory);
		}
	}

	public void ClientRequestSpawnTech(TechData techData, Vector3 position, Quaternion rotation, bool replace, bool cheatBypassInventory = false, bool spawnTechWithUnavailableBlocksMissing = false)
	{
		techData.ValidateBlockSkins();
		SpawnTechMessage message = new SpawnTechMessage
		{
			m_TechData = techData,
			m_Position = WorldPosition.FromScenePosition(in position),
			m_Rotation = rotation,
			m_Team = TechTeamID,
			m_IsPopulation = false,
			m_PlayerNetID = (replace ? base.netId : NetworkInstanceId.Invalid),
			m_PlayerWhoCalledSpawn = base.netId,
			m_CheatBypassInventory = cheatBypassInventory,
			m_SpawnTechWithUnavailableBlocksMissing = spawnTechWithUnavailableBlocksMissing,
			m_IsSpawnedByPlayer = true
		};
		m_HasRequestedSpawn = replace;
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SpawnTech, message);
	}

	public void InformPlayerArrival(NetPlayer player)
	{
		InformPlayerConnectionMsg(player, ConnectionMsgTypes.Arrival);
	}

	public void InformPlayerDeparture(NetPlayer player)
	{
		InformPlayerConnectionMsg(player, ConnectionMsgTypes.Departure);
	}

	private void InformPlayerConnectionMsg(NetPlayer player, ConnectionMsgTypes msgType)
	{
		player.NameSetEvent.Subscribe(_onPlayerNameChangedDuringConnectionMessage);
		m_MultiHud.Message1.SetTextWithTimeout(_getConnectionText(), UIMultiplayerHUD.Message.StateTypes.Connection, 2.5f, _onConnectionMessageTimedOut);
		string _getConnectionText()
		{
			LocalisationEnums.Multiplayer stringID = ((msgType == ConnectionMsgTypes.Arrival) ? LocalisationEnums.Multiplayer.playerHasJoinedTheGame : ((msgType == ConnectionMsgTypes.Departure) ? LocalisationEnums.Multiplayer.playerDisconnected : LocalisationEnums.Multiplayer.playerDisconnected));
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, (int)stringID), player.name);
		}
		void _onConnectionMessageTimedOut()
		{
			player.NameSetEvent.Unsubscribe(_onPlayerNameChangedDuringConnectionMessage);
		}
		void _onPlayerNameChangedDuringConnectionMessage(NetPlayer _)
		{
			m_MultiHud.Message1.UpdateText(_getConnectionText());
		}
	}

	public void ServerKickPlayer()
	{
		d.Assert(base.isServer);
		d.Assert(!IsActuallyLocalPlayer);
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.KickPlayer(m_PlayerIDInLobby);
		}
	}

	public TTNetworkID GetPlayerIDInLobby()
	{
		return m_PlayerIDInLobby;
	}

	public bool IsLoadoutBlock(uint blockPoolID)
	{
		d.Assert(m_LoadoutBlockPoolIDs != null, "IsLoadoutBlock - Loadout blocks was null!?");
		if (m_LoadoutBlockPoolIDs != null && m_LoadoutBlockPoolIDs.Contains(blockPoolID))
		{
			return true;
		}
		return false;
	}

	private void OnClientGrabFailed(NetworkMessage netMsg)
	{
		BlockGrabFailedMessage blockGrabFailedMessage = netMsg.ReadMessage<BlockGrabFailedMessage>();
		Visible draggingItem = Singleton.Manager<ManPointer>.inst.DraggingItem;
		if (draggingItem != null && draggingItem.block != null && draggingItem.block.netBlock != null && draggingItem.block.netBlock.netId == blockGrabFailedMessage.m_NetId)
		{
			Singleton.Manager<ManNetwork>.inst.GrabFailed(draggingItem.block.netBlock);
		}
	}

	private void OnClientUndoFailed(NetworkMessage netMsg)
	{
		GameObject gameObject = ClientScene.FindLocalObject(netMsg.ReadMessage<BlockRequestUndoAuthorityFailed>().m_NetId);
		TankBlock tankBlock = ((gameObject != null) ? gameObject.GetComponent<TankBlock>() : null);
		if (tankBlock != null && tankBlock.netBlock != null)
		{
			Singleton.Manager<ManNetwork>.inst.HandleBlockUndoAuthorityDenied(tankBlock.netBlock);
		}
	}

	private void OnClientTechRespawned(NetworkMessage netMsg)
	{
	}

	private void OnClientBlockScavenged(NetworkMessage netMsg)
	{
		BlockScavengedMessage blockScavengedMessage = netMsg.ReadMessage<BlockScavengedMessage>();
		Singleton.Manager<ManLicenses>.inst.DiscoverBlock(blockScavengedMessage.m_BlockType);
		TankBlock tankBlock = Singleton.Manager<ManSpawn>.inst.SpawnBlock(blockScavengedMessage.m_BlockType, blockScavengedMessage.m_Position.ScenePosition, blockScavengedMessage.m_Rotation);
		if (tankBlock != null)
		{
			tankBlock.visible.EnablePhysics(enable: false);
			ScavengedBlock item = new ScavengedBlock
			{
				m_State = ScavengedBlock.ScavengedBlockState.Rising,
				m_Block = tankBlock,
				m_StartingHeight = tankBlock.transform.position.y
			};
			m_ScavengedBlocks.Add(item);
		}
	}

	public void WrapSingleBlock(NetBlock netBlock, bool takeOwnership = false)
	{
		if ((bool)netBlock)
		{
			TankBlock block = netBlock.block;
			_ = block.visible.centrePosition;
			_ = block.visible.trans.rotation;
			NetPlayer owner = (takeOwnership ? this : null);
			Singleton.Manager<ManSpawn>.inst.WrapSingleBlock(owner, block, TechTeamID, StringLookup.GetItemName(new ItemTypeInfo(ObjectTypes.Block, (int)block.BlockType)), base.name);
		}
	}

	public void OnServerForceDropNotableBlock()
	{
		NetBlock notableBlock = Singleton.Manager<ManNetwork>.inst.NetController.GetNotableBlock();
		d.AssertFormat(notableBlock != null, "NetPlayer.OnServerForceDropNotableBlock has null block");
		if (notableBlock != null)
		{
			Singleton.Manager<ManNetwork>.inst.SendToClient(NetIdentity.connectionToClient.connectionId, TTMsgType.ForceReleaseNotableBlock, new ForceReleaseNotableBlock
			{
				m_BlockNetId = notableBlock.netId
			}, base.netId);
			Transform transform = notableBlock.transform;
			OnServerReleaseBlock(notableBlock.BlockPoolID, transform.position, transform.rotation, Vector3.zero, Vector3.zero, Vector3.zero);
			m_NotableBlockCooldownTimer = 5f;
		}
	}

	public void CollideScavengeBlock(BlockTypes blockType, NetworkInstanceId originalOwnerId, int originalTeamId, bool isLoadoutBlock)
	{
		if (base.isServer && m_Inventory.IsNotNull())
		{
			m_Inventory.HostAddItem(blockType);
		}
		ScavengeBlock(blockType, originalOwnerId, originalTeamId, isLoadoutBlock);
	}

	public bool ScavengeBlock(BlockTypes blockType, NetworkInstanceId originalOwnerId, int originalTeamId, bool isLoadoutBlock)
	{
		bool result = false;
		if (Singleton.Manager<ManNetwork>.inst.ScavengeItems && Singleton.Manager<ManNetwork>.inst.NetController != null && (!isLoadoutBlock || originalOwnerId != base.netId))
		{
			m_LootedBlocks.Add(blockType);
			result = true;
		}
		return result;
	}

	public void AddBlockReward(BlockTypes blockType, int qty)
	{
		d.LogError("NEEDS REIMPLEMENTING");
	}

	private void OnServerPlayerClaimedKillstreak(NetworkMessage netMsg)
	{
		ClaimKillstreakMessage claimKillstreakMessage = netMsg.ReadMessage<ClaimKillstreakMessage>();
		if (!(m_KillstreakRewards != null))
		{
			return;
		}
		if (claimKillstreakMessage.m_Level >= 0 && claimKillstreakMessage.m_Level < m_KillstreakRewards.m_RewardLevels.Length)
		{
			MultiplayerKillStreakRewardLevel multiplayerKillStreakRewardLevel = m_KillstreakRewards.m_RewardLevels[claimKillstreakMessage.m_Level];
			if (m_Score.KillStreak >= multiplayerKillStreakRewardLevel.m_KillsRequired)
			{
				for (int i = 0; i < multiplayerKillStreakRewardLevel.m_BlockReward.m_Quantity; i++)
				{
					m_LootedBlocks.Add(multiplayerKillStreakRewardLevel.m_BlockReward.m_BlockType);
					m_Inventory.HostAddItem(multiplayerKillStreakRewardLevel.m_BlockReward.m_BlockType);
				}
				m_Score.ResetKillStreak();
				SetDirtyBit(8u);
			}
			else
			{
				d.LogError("Player " + base.name + " tried to claim killstreak reward without having enough kills");
			}
		}
		else
		{
			d.LogError("Player " + base.name + " tried to claim out-of-range killstreak reward");
		}
	}

	private void OnServerBlockExploded(NetworkMessage netMsg)
	{
		BlockExplodedMessage blockExplodedMessage = netMsg.ReadMessage<BlockExplodedMessage>();
		HandleBlockExploded(blockExplodedMessage.m_BlockPoolID);
		Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.BlockExploded, blockExplodedMessage, base.netId);
	}

	private void OnClientBlockExploded(NetworkMessage netMsg)
	{
		BlockExplodedMessage blockExplodedMessage = netMsg.ReadMessage<BlockExplodedMessage>();
		if (!base.isServer)
		{
			HandleBlockExploded(blockExplodedMessage.m_BlockPoolID);
		}
	}

	private void HandleBlockExploded(uint blockPoolID)
	{
		if (this != Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(blockPoolID);
			if (tankBlock != null)
			{
				bool withDamage = false;
				tankBlock.damage.Explode(withDamage);
			}
		}
	}

	private void OnClientForceReleaseNotableBlock(NetworkMessage netMsg)
	{
		ForceReleaseNotableBlock forceReleaseNotableBlock = netMsg.ReadMessage<ForceReleaseNotableBlock>();
		Visible draggingItem = Singleton.Manager<ManPointer>.inst.DraggingItem;
		TankBlock tankBlock = ((draggingItem != null) ? draggingItem.block : null);
		NetBlock netBlock = ((tankBlock != null) ? tankBlock.netBlock : null);
		if (netBlock != null && netBlock.netId == forceReleaseNotableBlock.m_BlockNetId)
		{
			tankBlock.LockBlockAttach();
			Singleton.Manager<ManNetwork>.inst.ReleaseDraggedItemWithoutSendingCommand();
			tankBlock.UnlockBlockAttach();
			draggingItem.SetLockTimout(Visible.LockTimerTypes.Grabbable, 5f);
		}
	}

	private void OnServerGrabBlock(NetworkMessage netMsg)
	{
		BlockGrabMessage blockGrabMessage = netMsg.ReadMessage<BlockGrabMessage>();
		GameObject gameObject = NetworkServer.FindLocalObject(blockGrabMessage.m_NetId);
		if (!gameObject)
		{
			return;
		}
		NetBlockChunk component = gameObject.GetComponent<NetBlockChunk>();
		if (!component)
		{
			return;
		}
		_ = component.visible;
		NetBlock netBlock = component as NetBlock;
		bool flag = false;
		bool flag2 = false;
		if (netBlock.IsNotNull())
		{
			uint num = (CurTech ? CurTech.InitialSpawnShieldID : 0u);
			uint initialSpawnShieldID = netBlock.InitialSpawnShieldID;
			if (Singleton.Manager<ManNetwork>.inst.ServerSpawnBank != null && num != 0 && Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.IsShieldActive(num) && initialSpawnShieldID != num && initialSpawnShieldID != 0)
			{
				flag = false;
				flag2 = true;
			}
		}
		if (!flag2)
		{
			if (component.NetIdentity.clientAuthorityOwner != null)
			{
				ManNetwork.AuthorityReason authorityReason = Singleton.Manager<ManNetwork>.inst.GetAuthorityReason(component.NetIdentity.netId);
				if (component.NetIdentity.clientAuthorityOwner == base.connectionToClient)
				{
					if (authorityReason != ManNetwork.AuthorityReason.HeldVisible)
					{
						flag = true;
					}
				}
				else if (authorityReason == ManNetwork.AuthorityReason.Collision)
				{
					flag = true;
					component.RemoveClientAuthority();
				}
				else
				{
					flag2 = true;
					if (authorityReason != ManNetwork.AuthorityReason.NoAuthority)
					{
					}
				}
			}
			else
			{
				flag = true;
			}
		}
		if (flag && m_NotableBlockCooldownTimer > 0f && Singleton.Manager<ManNetwork>.inst.NetController.GetNotableBlock() == component)
		{
			flag = false;
			flag2 = true;
		}
		if (flag)
		{
			component.AssignClientAuthority(ManNetwork.AuthorityReason.HeldVisible, base.connectionToClient);
			component.OnServerSetHeld(held: true);
			OnServerSetCurrentHeldBlock(component);
		}
		else if (flag2)
		{
			BlockGrabFailedMessage message = new BlockGrabFailedMessage
			{
				m_NetId = blockGrabMessage.m_NetId
			};
			Singleton.Manager<ManNetwork>.inst.SendToClient(base.connectionToClient.connectionId, TTMsgType.BlockGrabFailed, message, NetIdentity.netId);
			component.OnServerSetHeld(held: false);
			OnServerSetCurrentHeldBlock(null);
		}
	}

	private void OnServerHandleReleaseBlockMessage(NetworkMessage netMsg)
	{
		BlockReleaseMessage blockReleaseMessage = netMsg.ReadMessage<BlockReleaseMessage>();
		OnServerReleaseBlock(blockReleaseMessage.m_BlockPoolID, blockReleaseMessage.m_Position.ScenePosition, blockReleaseMessage.m_Rotation, blockReleaseMessage.m_Velocity, blockReleaseMessage.m_AngularVelocity, blockReleaseMessage.m_DraggingItemVelocity);
	}

	private void OnServerReleaseBlock(uint blockPoolID, Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity, Vector3 draggingItemVelocity)
	{
		Visible visible = Singleton.Manager<ManLooseBlocks>.inst.FindVisible(blockPoolID);
		if (!visible)
		{
			return;
		}
		Rigidbody rigidbody = null;
		NetBlockChunk netBlockChunk = (visible.block ? ((NetBlockChunk)visible.block.netBlock) : ((NetBlockChunk)(visible.pickup ? visible.pickup.netChunk : null)));
		if (netBlockChunk.IsNotNull())
		{
			TankBlock block = visible.block;
			BlockTypes blockType = BlockTypes.GSOAIController_111;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			bool takeOwnership = false;
			if (block.IsNotNull())
			{
				blockType = block.BlockType;
				flag = HasInventoryAndIsPainting(blockType);
				flag2 = CanAffordToPaint(blockType);
				flag3 = CurTech == null && block.IsController;
				takeOwnership = (CurTech == null || !CurTech.tech.ControllableByAnyPlayer) && block.IsController;
			}
			if (flag2)
			{
				visible.trans.position = position;
				visible.trans.rotation = rotation;
				netBlockChunk.OnServerSetHeld(held: false);
				if (flag)
				{
					Inventory.HostConsumeItem(PlayerID, blockType);
				}
				if (netBlockChunk.NetIdentity.clientAuthorityOwner == NetIdentity.connectionToClient)
				{
					NetworkConnection clientAuthorityOwner = netBlockChunk.NetIdentity.clientAuthorityOwner;
					netBlockChunk.RemoveClientAuthority();
					d.Assert(CurrentHeldBlockID == blockPoolID, "Player requested to drop a block they are not holding");
					OnServerSetCurrentHeldBlock(null);
					rigidbody = visible.rbody;
					if (block.IsNotNull())
					{
						ModuleAnchor component = block.GetComponent<ModuleAnchor>();
						bool flag4 = (bool)component && component.WouldAnchorToGround();
						if ((block.IsController || flag4) && (flag3 || Singleton.Manager<ManBlockLimiter>.inst.AllowPlayerAttachBlock(block)))
						{
							Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(clientAuthorityOwner).WrapSingleBlock(block.netBlock, takeOwnership);
						}
					}
				}
			}
			else if (block.IsNotNull())
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyBlock(block);
			}
			else if (visible.pickup.IsNotNull())
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDestroyChunk(visible.pickup);
			}
		}
		if ((bool)rigidbody)
		{
			float num = Singleton.Manager<ManPointer>.inst.DraggingItemReleaseSpeedMul * (Mode<ModeMain>.inst.ReduceBlockDragReleaseSpeed ? 0.1f : 1f);
			rigidbody.velocity = draggingItemVelocity * num;
			rigidbody.angularVelocity = draggingItemVelocity.magnitude * Singleton.Manager<ManPointer>.inst.DraggingItemReleaseSpinMul * Random.onUnitSphere;
		}
	}

	public void SendTechBlockDamagedMessage(NetworkInstanceId objectID, uint blockPoolID, ManDamage.DamageInfo damageInfo, bool removedBlocks, int removalKey, int removalSeed)
	{
		TechBlockDamagedMessage techBlockDamagedMessage = new TechBlockDamagedMessage
		{
			m_NetId = objectID,
			m_DamageBlockPoolID = blockPoolID,
			m_DamageInfo = damageInfo,
			m_RemovedBlocks = removedBlocks,
			m_RemovalSeed = removalSeed
		};
		Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TechBlockDamaged, techBlockDamagedMessage, techBlockDamagedMessage.m_NetId);
	}

	private void UpdateScoreHUD()
	{
		if (!IsActuallyLocalPlayer || !Singleton.Manager<ManNetwork>.inst.NetController || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.Deathmatch)
		{
			return;
		}
		if (m_ScoreHud != null)
		{
			m_ScoreHud.SetPlayerScore(this);
		}
		if (!(m_KillStreakClaimRewardHud != null))
		{
			return;
		}
		int num = m_KillStreakClaimRewardHud.MinKillsRequired();
		bool flag = Score.KillStreak >= num;
		bool isVisible = m_KillStreakClaimRewardHud.IsVisible;
		if (flag != isVisible)
		{
			if (flag)
			{
				m_KillStreakClaimRewardHud.Show(null);
			}
			else
			{
				m_KillStreakClaimRewardHud.Hide(null);
			}
		}
	}

	private void UpdateLivesHUD()
	{
		if (m_ScoreHud != null)
		{
			m_ScoreHud.SetLives(m_Lives);
		}
	}

	private void FindSprite()
	{
		m_Sprite = null;
		if (m_PlayerIDInLobby.IsValid() && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			Lobby currentLobby = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby;
			m_Sprite = currentLobby.LookupPlayerSprite(m_PlayerIDInLobby);
			OnAvatarChanged.Send(this);
			currentLobby.OnPlayerSpriteLoaded.Unsubscribe(OnPlayerSpriteLoaded);
			currentLobby.OnPlayerSpriteLoaded.Subscribe(OnPlayerSpriteLoaded);
		}
	}

	private void OnPlayerSpriteLoaded(TTNetworkID networkId, Sprite sprite)
	{
		if (m_PlayerIDInLobby == networkId)
		{
			m_Sprite = sprite;
			OnAvatarChanged.Send(this);
		}
	}

	private void OnPlayerTankChanged(Tank t, bool setting)
	{
		if (setting)
		{
			m_HasRequestedSpawn = false;
			if (IsActuallyLocalPlayer && m_KillStreakClaimRewardHud != null)
			{
				m_KillStreakClaimRewardHud.MarkAsDirty();
			}
		}
	}

	private void OnServerHandleSetNameMessage(NetworkMessage msg)
	{
		if (Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(msg.conn) == this)
		{
			SetPlayerName setPlayerName = msg.ReadMessage<SetPlayerName>();
			SetName(setPlayerName.name);
		}
	}

	private void OnServerHandlerSelectFirstTechMessage(NetworkMessage msg)
	{
		if (Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(msg.conn) == this)
		{
			SelectFirstTech();
		}
	}

	private void OnReturnToLobby(NetworkMessage msg)
	{
		if (!base.isServer)
		{
			_ = (bool)(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyScreen) as UIScreenNetworkLobby);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MatchmakingLobbyScreen);
		}
	}

	public override void OnStartAuthority()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
	}

	public override void OnStopAuthority()
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.BlockExploded, OnServerBlockExploded);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetPlayerName, OnServerHandleSetNameMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetFirstTechSelected, OnServerHandlerSelectFirstTechMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.BlockGrab, OnServerGrabBlock);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.BlockReleased, OnServerHandleReleaseBlockMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.ClaimKillStreak, OnServerPlayerClaimedKillstreak);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.RequestTeamChange, OnServerTeamChangeRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.DetonateExplosiveBolts, OnServerDetonateBolts);
		m_Score.Reset();
		m_NotableBlockCooldownTimer = 0f;
		if (base.isServer)
		{
			m_PlayerHost = false;
			SetDirtyBit(1024u);
		}
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		Singleton.Manager<ManNetwork>.inst.AddPlayer(this);
		UpdateScoreHUD();
		if (m_CurTech == null && m_ClientTechId != NetworkInstanceId.Invalid)
		{
			GameObject gameObject = ClientScene.FindLocalObject(m_ClientTechId);
			NetTech netTech = (gameObject.IsNotNull() ? gameObject.GetComponent<NetTech>() : null);
			d.Assert(netTech != null, "NetPlayer - OnStartClient: Can't find my tech");
			if (netTech != null)
			{
				OnClientSetCurrentTech(netTech);
			}
		}
		FixupLinksToOwnedTechs();
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.ForceReleaseNotableBlock, OnClientForceReleaseNotableBlock);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.BlockExploded, OnClientBlockExploded);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.BlockGrabFailed, OnClientGrabFailed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.BlockRequestUndoAuthorityFailed, OnClientUndoFailed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.TechRespawned, OnClientTechRespawned);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.BlockScavenged, OnClientBlockScavenged);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.ReturnToLobby, OnReturnToLobby);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.TechSwapRejected, OnClientTechSwapRejection);
	}

	public override void OnStartLocalPlayer()
	{
		base.OnStartLocalPlayer();
		if (base.isServer)
		{
			m_PlayerHost = true;
			SetDirtyBit(1024u);
		}
		m_ScoreHud = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Score) as UIScoreHUD;
		m_TimeRemainingHud = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPTimeRemaining) as UIMPTimeRemainingHUD;
		m_KillStreakClaimRewardHud = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPKillStreakClaimReward) as UIMPKillStreakClaimRewardHUD;
		UpdateScoreHUD();
	}

	public override void OnNetworkDestroy()
	{
		base.OnNetworkDestroy();
		bool isActuallyLocalPlayer = IsActuallyLocalPlayer;
		if (base.isClient)
		{
			Singleton.Manager<ManNetwork>.inst.RemovePlayer(this);
		}
		if (base.isServer && !isActuallyLocalPlayer && CurTech.IsNotNull())
		{
			d.LogError("Disconnected client being destroyed, but player still had a tech?! Current design states tech controlled by a Client player is removed when they leave the game.");
			Singleton.Manager<ManNetTechs>.inst.StorePlayerTech(this, despawn: true);
		}
		if (m_ScoreHud != null)
		{
			m_ScoreHud.Hide(null);
			m_ScoreHud = null;
		}
		if (m_TimeRemainingHud != null)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(m_TimeRemainingHud.HudElementType);
			m_TimeRemainingHud = null;
		}
		if (m_KillStreakClaimRewardHud != null)
		{
			m_KillStreakClaimRewardHud.Hide(null);
			m_KillStreakClaimRewardHud = null;
		}
		if (IsActuallyLocalPlayer)
		{
			Singleton.Manager<ManPointer>.inst.ForceRemoveDraggedItem();
		}
		if (!IsActuallyLocalPlayer && Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer != this && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase != NetController.Phase.Outro)
		{
			Singleton.Manager<ManNetwork>.inst.MyPlayer.InformPlayerDeparture(this);
		}
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby?.OnPlayerSpriteLoaded.Unsubscribe(OnPlayerSpriteLoaded);
	}

	public void FixupLinksToOwnedTechs()
	{
		int numTechs = Singleton.Manager<ManNetTechs>.inst.GetNumTechs();
		for (int i = 0; i < numTechs; i++)
		{
			NetTech tech = Singleton.Manager<ManNetTechs>.inst.GetTech(i);
			if (tech.ClientOwnerNetId == NetIdentity.netId && tech.NetPlayer.IsNull())
			{
				tech.OnClientSetOwner(this);
			}
		}
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? 8191u : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		if ((num & 1) != 0)
		{
			writer.Write(m_PlayerID);
			writer.Write(m_LobbyTeamID);
		}
		if ((num & 2) != 0)
		{
			writer.Write(base.name);
		}
		if ((num & 4) != 0)
		{
			writer.Write(ColourConverter.ColourToString(m_Colour));
		}
		if ((num & 8) != 0)
		{
			m_Score.NetSerialize(writer);
		}
		if ((num & 0x10) != 0)
		{
			writer.Write(m_PlayerIDInLobby.ToString());
		}
		if ((num & 0x20) != 0)
		{
			writer.Write(m_PlayerActive);
		}
		if ((num & 0x40) != 0)
		{
			writer.Write(m_Lives);
		}
		if ((num & 0x80) != 0)
		{
			writer.Write(m_OutOfBoundsPercentage);
		}
		if ((num & 0x100) != 0)
		{
			writer.Write(m_InitialBlockPoolID);
		}
		if ((num & 0x400) != 0)
		{
			writer.Write(m_PlayerHost);
		}
		if ((num & 0x800) != 0)
		{
			writer.WritePackedUInt32(CurrentHeldBlockID);
		}
		if ((num & 0x1000) != 0)
		{
			writer.Write((m_CurTech == null) ? NetworkInstanceId.Invalid : m_CurTech.netId);
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			m_PlayerID = reader.ReadInt32();
			int num2 = reader.ReadInt32();
			if (m_LobbyTeamID != num2)
			{
				m_LobbyTeamID = num2;
				OnTeamChanged.Send(this);
			}
		}
		if ((num & 2) != 0)
		{
			base.name = reader.ReadString();
		}
		if ((num & 4) != 0)
		{
			ColourConverter.TryParseColourString(reader.ReadString(), out m_Colour);
		}
		if ((num & 8) != 0)
		{
			m_Score.NetDeserialize(reader);
			UpdateScoreHUD();
		}
		if ((num & 0x10) != 0)
		{
			m_PlayerIDInLobby = new TTNetworkID(reader.ReadString());
			FindSprite();
		}
		if ((num & 0x20) != 0)
		{
			m_PlayerActive = reader.ReadBoolean();
		}
		if ((num & 0x40) != 0)
		{
			m_Lives = reader.ReadInt32();
			UpdateLivesHUD();
		}
		if ((num & 0x80) != 0)
		{
			m_OutOfBoundsPercentage = reader.ReadSingle();
		}
		if ((num & 0x100) != 0)
		{
			m_InitialBlockPoolID = (m_NextBlockPoolID = reader.ReadUInt32());
			if (IsActuallyLocalPlayer)
			{
				ManCombat.Projectiles.InitWeaponRoundUIDRange((int)m_InitialBlockPoolID, 1000000);
			}
		}
		if ((num & 0x400) != 0)
		{
			m_PlayerHost = reader.ReadBoolean();
		}
		if ((num & 0x800) != 0)
		{
			CurrentHeldBlockID = reader.ReadPackedUInt32();
		}
		if ((num & 0x1000) != 0)
		{
			m_ClientTechId = reader.ReadNetworkId();
			GameObject gameObject = ClientScene.FindLocalObject(m_ClientTechId);
			NetTech netTech = (gameObject.IsNotNull() ? gameObject.GetComponent<NetTech>() : null);
			if ((m_ClientTechId == NetworkInstanceId.Invalid) ? netTech.IsNull() : netTech.IsNotNull())
			{
				OnClientSetCurrentTech(netTech);
			}
		}
	}

	private void UpdateRebuildTimerMessage()
	{
		if (!base.hasAuthority)
		{
			return;
		}
		float num = ((m_CurTech != null) ? m_CurTech.RebuildTimer : 0f);
		if (num > 0f && num < 1000f)
		{
			if (m_MultiHud.Message1.State == UIMultiplayerHUD.Message.StateTypes.Rebuild || m_MultiHud.Message1.Text == string.Empty)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 95);
				bool showMinutes = false;
				bool showMilliseconds = false;
				m_MultiHud.Message1.SetText(string.Format(localisedString, Util.GetTimeString(num, showMinutes, showMilliseconds)), UIMultiplayerHUD.Message.StateTypes.Rebuild);
			}
		}
		else if (m_MultiHud.Message1.State == UIMultiplayerHUD.Message.StateTypes.Rebuild)
		{
			m_MultiHud.Message1.Clear();
		}
	}

	public void SetMultiHUDToSelfDestruct(bool selfDestructActive, string message = null)
	{
		if (selfDestructActive || m_MultiHud.Message1.State == UIMultiplayerHUD.Message.StateTypes.SelfDestruct)
		{
			m_MultiHud.Message1.SetText(message ?? string.Empty, selfDestructActive ? UIMultiplayerHUD.Message.StateTypes.SelfDestruct : UIMultiplayerHUD.Message.StateTypes.None);
		}
	}

	private void OnHudMessageChanged(string message)
	{
	}

	private void OnInventoryChanged(BlockTypes type, int quantityOfType)
	{
		if (base.isServer)
		{
			int num = 0;
			for (int i = 0; i < m_LootedBlocks.Count; i++)
			{
				if (m_LootedBlocks[i] == type)
				{
					num++;
				}
			}
			if (num > quantityOfType)
			{
				m_LootedBlocks.Remove(type);
			}
		}
		if (!IsActuallyLocalPlayer)
		{
			return;
		}
		int num2 = 0;
		for (int j = 0; j < m_DeathStreakItems.Count; j++)
		{
			if (m_DeathStreakItems[j] == type)
			{
				num2++;
			}
		}
		if (num2 > quantityOfType)
		{
			m_DeathStreakItems.Remove(type);
		}
	}

	private void OnPool()
	{
		NetIdentity = GetComponent<NetworkIdentity>();
	}

	private void OnSpawn()
	{
		m_SwitchTechDelay = 0f;
		m_HasRequestedSpawn = false;
		m_FirstSpawn = true;
		m_PickingTechToSpawn = false;
		m_PickingTechToSpawnComplete = false;
		m_LoadoutBlockPoolIDs = null;
		m_PlayerActive = true;
		m_Lives = -1;
		m_MultiHud = (UIMultiplayerHUD)Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Multiplayer);
		m_MultiHud.Message1.SetEvent.Subscribe(OnHudMessageChanged);
		m_MultiHud.Message1.Clear();
		m_DeferUIDelay = 0f;
		m_NextBlockPoolID = 0u;
		for (int i = 0; i < 4; i++)
		{
			m_TechLevels[i] = 0;
		}
		m_LastCorporationSelected = 0;
		m_LastBlockPaletteSelected = 0;
		m_LootedBlocks.Clear();
		m_FirstTechSelected = false;
		m_HealState = HealState.NotHealing;
		m_HealInterruptTimer = 0f;
		m_BuildBeamOn = false;
		m_NumberOfDeathsToEarnReward = Singleton.Manager<ManNetwork>.inst.DeathStreakInitialDeathsRequired;
		m_CurrentDeathStreakRewardLevel = 0;
		m_DeathStreakItems.Clear();
		m_CurTech = null;
		m_ClientTechId = NetworkInstanceId.Invalid;
		CurrentHeldBlockID = uint.MaxValue;
	}

	private void OnRecycle()
	{
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromMessages(base.netId);
		if (m_Inventory != null)
		{
			m_Inventory.InventoryChanged.Unsubscribe(OnInventoryChanged);
		}
		m_Inventory = null;
		m_PlayerIDInLobby = TTNetworkID.Invalid;
		m_Sprite = null;
		m_MultiHud.Message1.SetEvent.Unsubscribe(OnHudMessageChanged);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
		OnTeamChanged.Clear();
		OnAvatarChanged.Clear();
	}

	private void OnDestroy()
	{
		_ = DebugUtil.isShuttingDown;
	}

	private void Update()
	{
		if (base.isServer)
		{
			m_NotableBlockCooldownTimer = Mathf.Max(m_NotableBlockCooldownTimer - Time.deltaTime, 0f);
		}
		UpdateRebuildTimerMessage();
		m_RemovedBlocks.Clear();
		foreach (ScavengedBlock scavengedBlock in m_ScavengedBlocks)
		{
			if (!(scavengedBlock.m_Block != null) || !(scavengedBlock.m_Block.trans != null))
			{
				continue;
			}
			if ((bool)CurTech)
			{
				scavengedBlock.Update(CurTech.transform.position);
				if (scavengedBlock.m_State == ScavengedBlock.ScavengedBlockState.Moving && (CurTech.transform.position - scavengedBlock.m_Block.transform.position).sqrMagnitude <= 0.125f)
				{
					scavengedBlock.m_Block.trans.Recycle(worldPosStays: false);
					m_RemovedBlocks.Add(scavengedBlock);
				}
			}
			else
			{
				scavengedBlock.m_Block.trans.Recycle(worldPosStays: false);
				m_RemovedBlocks.Add(scavengedBlock);
			}
		}
		foreach (ScavengedBlock removedBlock in m_RemovedBlocks)
		{
			m_ScavengedBlocks.Remove(removedBlock);
		}
		m_RemovedBlocks.Clear();
		if (!Singleton.Manager<ManNetwork>.inst.HealInBuildBeam)
		{
			return;
		}
		switch (m_HealState)
		{
		case HealState.WarmingUp:
			m_HealTimer -= Time.deltaTime;
			if (m_HealTimer <= 0f)
			{
				m_HealState = HealState.Healing;
				m_HealTimer = 1f;
			}
			break;
		case HealState.Healing:
			m_HealTimer -= Time.deltaTime;
			if (!(m_HealTimer <= 0f))
			{
				break;
			}
			if ((bool)CurTech && (bool)CurTech.tech && (bool)CurTech.tech.blockman)
			{
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = CurTech.tech.blockman.IterateBlocks().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					TankBlock current3 = enumerator2.Current;
					if (current3.visible.damageable.Health < current3.visible.damageable.MaxHealth)
					{
						current3.visible.damageable.Repair(Singleton.Manager<ManNetwork>.inst.HealRate);
					}
				}
			}
			m_HealTimer = 1f;
			break;
		}
		if (m_HealInterruptTimer > 0f)
		{
			m_HealInterruptTimer -= Time.deltaTime;
		}
	}

	private void _fixupLoadoutBlockReferences(NetTech netTech)
	{
		d.Assert(netTech != null);
		d.Assert(netTech.tech != null);
		int blockCount = netTech.tech.blockman.blockCount;
		if (m_LoadoutBlockPoolIDs == null)
		{
			m_LoadoutBlockPoolIDs = new List<uint>(blockCount);
		}
		for (int i = 0; i < blockCount; i++)
		{
			m_LoadoutBlockPoolIDs.Add(netTech.tech.blockman.GetBlockWithIndex(i).blockPoolID);
		}
	}

	public bool UpdateTechSelection()
	{
		if (Singleton.Manager<ManNetwork>.inst.NetController == null)
		{
			return false;
		}
		if (Singleton.Manager<ManNetwork>.inst.NetController.GameModeType != MultiplayerModeType.Deathmatch)
		{
			return true;
		}
		if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase != NetController.Phase.Playing && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase != NetController.Phase.TechSelection)
		{
			return false;
		}
		if (!m_PickingTechToSpawn)
		{
			m_DeferUIDelay -= Time.deltaTime;
			if (!(m_DeferUIDelay < 0f))
			{
				return false;
			}
			m_DeferUIDelay = 3f;
			HasBeenDamagedByOpponent = false;
			UISelfDestructButton uISelfDestructButton = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.SelfDestruct) as UISelfDestructButton;
			if ((bool)uISelfDestructButton)
			{
				uISelfDestructButton.HasBeenDamaged(wasDamaged: false);
			}
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SelfDestruct);
			Singleton.Manager<ManMusic>.inst.ResetDangerMusic();
			UIScreenMultiplayerTechSelect component = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MultiplayerTechSelect).GetComponent<UIScreenMultiplayerTechSelect>();
			d.Assert(component != null);
			component.SetCorporationSelected(m_LastCorporationSelected);
			component.SetBlockPaletteSelected(m_LastBlockPaletteSelected);
			component.ApplySelection();
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
			{
				return false;
			}
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MultiplayerTechSelect);
			m_PickingTechToSpawn = true;
		}
		if (m_PickingTechToSpawnComplete)
		{
			m_PickingTechToSpawn = false;
			m_PickingTechToSpawnComplete = false;
			return true;
		}
		return false;
	}

	public void OnTechToSpawnSelected(int corpSelected, int techLevel, int blockPaletteSelected, int skinID)
	{
		if (m_PickingTechToSpawn)
		{
			m_LastCorporationSelected = corpSelected;
			m_LastBlockPaletteSelected = blockPaletteSelected;
			m_TechLevels[corpSelected] = techLevel;
			Singleton.Manager<ManNetwork>.inst.StartingTechLoadoutCorp = corpSelected;
			Singleton.Manager<ManNetwork>.inst.StartingTechLoadout = blockPaletteSelected;
			Singleton.Manager<ManNetwork>.inst.StartingSkinID = skinID;
			m_PickingTechToSpawnComplete = true;
		}
	}

	public void InitDeathStreakRewards(MultiplayerTechSelectPresetAsset selectedLoadout)
	{
		m_DeathStreakItems.Clear();
		if (Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled && m_Score.DeathStreak >= m_NumberOfDeathsToEarnReward)
		{
			m_NumberOfDeathsToEarnReward += Singleton.Manager<ManNetwork>.inst.DeathStreakSubsequentDeathsRequired;
			if (selectedLoadout.m_DeathStreakRewards != null && selectedLoadout.m_DeathStreakRewards.m_RewardLevels.Length != 0)
			{
				MultiplayerDeathStreakReward[] rewardLevels = selectedLoadout.m_DeathStreakRewards.m_RewardLevels;
				int num = Mathf.Clamp(m_CurrentDeathStreakRewardLevel, 0, rewardLevels.Length - 1);
				BlockCount[] rewards = rewardLevels[num].m_Rewards;
				if (rewards != null)
				{
					foreach (BlockCount blockCount in rewards)
					{
						for (int j = 0; j < blockCount.m_Quantity; j++)
						{
							m_DeathStreakItems.Add(blockCount.m_BlockType);
						}
					}
				}
			}
			m_CurrentDeathStreakRewardLevel++;
		}
		if (Singleton.Manager<ManNetwork>.inst.KillStreakRewardsEnabled)
		{
			m_KillstreakRewards = selectedLoadout.m_KillStreakRewards;
		}
	}

	public void AddRewardAndDeathStreakToInventory()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "Should not be adding inventory items on a client");
		if (!m_Inventory.IsNotNull())
		{
			return;
		}
		if (Singleton.Manager<ManNetwork>.inst.KeepLootedBlocksOnRespawnEnabled)
		{
			for (int i = 0; i < m_LootedBlocks.Count; i++)
			{
				m_Inventory.HostAddItem(m_LootedBlocks[i]);
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled)
		{
			for (int j = 0; j < m_DeathStreakItems.Count; j++)
			{
				m_Inventory.HostAddItem(m_DeathStreakItems[j]);
			}
		}
	}

	public void SelfDestructFromUI()
	{
		if (!(CurTech != null) || !(CurTech.SelfDestructController != null) || !(CurTech.SelfDestructController.block != null))
		{
			return;
		}
		for (int i = 0; i < CurTech.SelfDestructController.block.ConnectedBlocksByAP.Length; i++)
		{
			TankBlock tankBlock = CurTech.SelfDestructController.block.ConnectedBlocksByAP[i];
			if ((bool)tankBlock)
			{
				Singleton.Manager<ManLooseBlocks>.inst.RequestDetachBlock(tankBlock, allowHeadlessTech: false);
			}
		}
	}

	public void EnterBuildBeam(bool isEntering, bool wasDamaged)
	{
		if (isEntering == m_BuildBeamOn)
		{
			return;
		}
		m_BuildBeamOn = isEntering;
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
		{
			m_HealTimer = Singleton.Manager<ManNetwork>.inst.HealWarmUpTimerInSecs;
			m_HealState = (isEntering ? HealState.WarmingUp : HealState.NotHealing);
			if (!isEntering && wasDamaged)
			{
				m_HealInterruptTimer = Singleton.Manager<ManNetwork>.inst.HealInterruptCooldownInSecs;
			}
			else
			{
				m_HealInterruptTimer = 0f;
			}
		}
	}

	public bool BuildBeamCooldownActive()
	{
		return m_HealInterruptTimer > 0f;
	}

	public bool IsBlockDeathStreakReward(BlockTypes type)
	{
		if (Singleton.Manager<ManNetwork>.inst.DeathStreakEnabled)
		{
			for (int i = 0; i < m_DeathStreakItems.Count; i++)
			{
				if (m_DeathStreakItems[i] == type)
				{
					return true;
				}
			}
		}
		return false;
	}

	public void RequestCycleTeam()
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
		{
			int techTeamID = TechTeamID;
			techTeamID++;
			if (techTeamID >= 1073741829)
			{
				techTeamID = 1073741824;
			}
			RequestChangeTeam(techTeamID);
		}
	}

	public void RequestChangeTeam(int techTeamID)
	{
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp() && ManSpawn.IsPlayerTeam(techTeamID) && techTeamID != 0)
		{
			RequestTeamChangeMessage message = new RequestTeamChangeMessage
			{
				m_TechTeamID = techTeamID
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestTeamChange, message, base.netId);
		}
	}

	private void OnServerTeamChangeRequest(NetworkMessage netMsg)
	{
		if (netMsg.GetSender() == this || netMsg.GetSender() == Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			if (!Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
			{
				return;
			}
			RequestTeamChangeMessage requestTeamChangeMessage = netMsg.ReadMessage<RequestTeamChangeMessage>();
			int num = ManSpawn.LobbyTeamIDFromTechTeamID(requestTeamChangeMessage.m_TechTeamID);
			if (ManSpawn.IsPlayerTeam(requestTeamChangeMessage.m_TechTeamID) && num != int.MaxValue)
			{
				if (CurTech.IsNotNull())
				{
					CurTech.OnServerSetTeam(requestTeamChangeMessage.m_TechTeamID, isPopulation: false);
				}
				OnServerSetTeamID(num);
			}
			else
			{
				d.LogError("Player " + base.name + " requested switching to a non-player team");
			}
		}
		else
		{
			d.LogError("Player " + netMsg.GetSender().name + " sent a request to change teams while pretending to be " + base.name);
		}
	}

	private void OnServerDetonateBolts(NetworkMessage netMsg)
	{
		if (netMsg.GetSender() == this)
		{
			if (CurTech != null)
			{
				CurTech.tech.control.ServerDetonateExplosiveBolt();
			}
			else
			{
				d.LogError("Player tried to det their explosive bolts while not driving a tech");
			}
		}
		else
		{
			d.LogError("Player tried to detonate someone else's explosive bolts");
		}
	}

	public bool HasInventoryAndIsPainting(BlockTypes blockType)
	{
		if (Inventory.IsNotNull())
		{
			return Inventory.HasReservedItem(PlayerID, blockType);
		}
		return false;
	}

	public bool CanAffordToPaint(BlockTypes blockType)
	{
		if (HasInventoryAndIsPainting(blockType))
		{
			return Inventory.CanConsumeItem(PlayerID, blockType);
		}
		return true;
	}

	public string GetColouredName()
	{
		return ColourConverter.RecolourRichText(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetMultiplayerColour(this), base.name);
	}

	private void UNetVersion()
	{
	}
}
