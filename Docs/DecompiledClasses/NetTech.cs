#define UNITY_EDITOR
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetTech : NetworkBehaviour, ManNetwork.IDumpableBehaviour
{
	private struct SerializableBlockData
	{
		public uint m_BlockPoolID;

		public NetworkedModuleID m_ModuleID;

		public NetworkReader m_Data;
	}

	private class ProjectileUpdate
	{
		public int m_ProjectileID;

		public NetworkInstanceId m_LockedOnToNetId;

		public uint m_LockedOnToBlockID;

		public Vector3 m_Pos;

		public Quaternion m_Rot;
	}

	[SerializeField]
	private Transform m_ShieldGeomPrefab;

	public const float kRebuildTimeInfinite = 1000f;

	public EventNoParams StartClientEvent;

	public EventNoParams StartServerEvent;

	private NetPlayer m_NetPlayer;

	private TechData m_TechData;

	private Dictionary<int, TechComponent.SerialData> m_OriginalSaveData;

	private uint[] m_TechDataBlockPoolIDs;

	private bool m_RecycleFailedAddsOnSpawn = true;

	private TankControl.State m_LastSentControls;

	private NetworkInstanceId m_LastTargettedId;

	private bool m_NeedsTargetCheckAndSet;

	private float m_NeedsTargetCheckAndSetTimeout;

	private TechWeapon.State m_WeaponState;

	private Vector3 m_AI_LastTargetPosition;

	private float m_AI_LastTargetRadius;

	private bool m_AnchorsDirty;

	private bool m_QueuedSaveTechData;

	private int m_HostID;

	private bool m_NeedsToDisableBuildBeam;

	private bool m_Invulnerable;

	private float m_RebuildTimer;

	private Transform m_ShieldGeom;

	private uint m_InitialSpawnShieldID;

	private bool m_HasClearedInventoryAfterRespawn;

	private List<INetworkedModule> m_NetworkedModules = new List<INetworkedModule>(16);

	private INetworkedTechComponent[] m_NetworkedTechComponents;

	private HashSet<INetworkedModule> m_DirtyModules = new HashSet<INetworkedModule>();

	private Bitfield<NetworkedTechComponentID> m_DirtyTechComponents = new Bitfield<NetworkedTechComponentID>();

	private List<SerializableBlockData> m_PendingBlockData = new List<SerializableBlockData>();

	private List<ProjectileUpdate> m_PendingProjectileUpdates = new List<ProjectileUpdate>();

	private NetworkInstanceId m_ClientOwnerNetId = NetworkInstanceId.Invalid;

	private bool m_ObserversInitialised;

	private bool m_IsMaterialOverride;

	private ManTechMaterialSwap.MatType m_MaterialOverrideType;

	private const uint kSer_OwnerId_F = 1u;

	private const uint kSer_TechData_F = 2u;

	private const uint kSer_Controls_F = 4u;

	private const uint kSer_Target_F = 8u;

	private const uint kSer_Invulnerable_F = 16u;

	private const uint kSer_RebuildTimer_F = 32u;

	private const uint kSer_ControlsAI_F = 64u;

	private const uint kSer_Weapons_F = 128u;

	private const uint kSer_InitialSpawnShieldID_F = 256u;

	private const uint kSer_Team_F = 512u;

	private const uint kSer_ModuleData_F = 1024u;

	private const uint kSer_TechComponentData_F = 2048u;

	private const uint kSer_HostID_F = 4096u;

	private const uint kSer_ControlSchemesInitial_F = 32768u;

	private const uint kSer_ProjectileUpdates_F = 131072u;

	private const uint kSer_IsPopulation_F = 262144u;

	private const uint kSer_LockSendtoSCU_F = 524288u;

	private const uint kSer_LockAttach_F = 1048576u;

	private const uint kSer_LockDetach_F = 2097152u;

	private const uint kSer_ShouldShowOverlay_F = 4194304u;

	private const uint kSer_CustomMaterialOverride_F = 8388608u;

	private const uint kSer_AllFlagMask = uint.MaxValue;

	private static List<INetworkedModule> s_ModulesToWrite = new List<INetworkedModule>();

	private static List<INetworkedTechComponent> s_TechComponentsToWrite = new List<INetworkedTechComponent>();

	private static NetworkWriter s_DataSubStream = new NetworkWriter();

	private static UpdateTechControlsMessage s_ReusableUpdateTechControlsMessage = new UpdateTechControlsMessage();

	private static UpdateTechWeaponsMessage s_ReusableUpdateTechWeaponsMessage = new UpdateTechWeaponsMessage();

	private static TechBlockDamagedMessage s_ReusableTechBlockDamagedMessage = new TechBlockDamagedMessage();

	public static bool ForceBuildBeamEnabled { get; set; }

	public static bool ForceInvulnerable { get; set; }

	public NetworkIdentity NetIdentity { get; private set; }

	public NetworkInstanceId OwnerNetId
	{
		get
		{
			if (!(NetPlayer == null))
			{
				return NetPlayer.netId;
			}
			return NetworkInstanceId.Invalid;
		}
	}

	public int Team { get; private set; }

	public Tank tech { get; private set; }

	public Color Colour { get; set; }

	public NetPlayer LastDamager { get; set; }

	public int SpawnShieldCount { get; set; }

	public uint InitialSpawnShieldID
	{
		get
		{
			return m_InitialSpawnShieldID;
		}
		set
		{
			m_InitialSpawnShieldID = value;
			SetDirtyBit(256u);
			m_HasClearedInventoryAfterRespawn = false;
		}
	}

	public float RebuildTimer => m_RebuildTimer;

	public ModuleTechController SelfDestructController => tech.control.FirstController;

	public bool IsSetToSelfDestruct { get; private set; }

	public bool ObserversInitialised => m_ObserversInitialised;

	public NetworkInstanceId ClientOwnerNetId => m_ClientOwnerNetId;

	public int HostID
	{
		get
		{
			return m_HostID;
		}
		set
		{
			if (m_HostID != value)
			{
				m_HostID = value;
				SetDirtyBit(4096u);
			}
		}
	}

	public NetPlayer NetPlayer
	{
		get
		{
			if (m_NetPlayer == null && m_ClientOwnerNetId != NetworkInstanceId.Invalid)
			{
				d.LogErrorFormat("NetTech {0} has owner {1} but reference has not been resolved yet", NetIdentity.netId, m_ClientOwnerNetId);
			}
			return m_NetPlayer;
		}
		private set
		{
			m_NetPlayer = value;
		}
	}

	public float EncompassingRadius => new Vector3(tech.blockBounds.extents.x + 0.5f, tech.blockBounds.extents.y + 0.5f, tech.blockBounds.extents.z + 0.5f).magnitude;

	private static bool isMultiplayer => Singleton.Manager<ManNetwork>.inst.IsMultiplayer();

	private static bool isHost
	{
		get
		{
			if (isMultiplayer)
			{
				return Singleton.Manager<ManNetwork>.inst.IsServer;
			}
			return true;
		}
	}

	public bool IsInvulnerable()
	{
		return m_Invulnerable;
	}

	public void SetMaterialOverrideType(bool enable, ManTechMaterialSwap.MatType materialType)
	{
		if (m_IsMaterialOverride != enable || m_MaterialOverrideType != materialType)
		{
			m_IsMaterialOverride = enable;
			m_MaterialOverrideType = materialType;
			SetDirtyBit(8388608u);
		}
	}

	public void Dump(StringBuilder builder)
	{
		builder.AppendFormat("PlayerNetId={0}\n", (NetPlayer != null) ? NetPlayer.netId.ToString() : "n/a");
		builder.AppendFormat("TargetNetId={0}\n", (m_LastTargettedId != NetworkInstanceId.Invalid) ? m_LastTargettedId.ToString() : "n/a");
		builder.AppendFormat("Invulnerable={0}\n", m_Invulnerable);
		builder.AppendFormat("Blocks={0} (bounds:{1}-{2}) Centre={3}\n", tech.blockman.blockCount, tech.blockman.blockCentreBounds.min, tech.blockman.blockCentreBounds.max, tech.boundsCentreWorldNoCheck);
	}

	[Server]
	public void OnServerSetupTech(TechData techData, uint[] techDataBlockPoolIDs, bool recycleFailedAdds)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetupTech(TechData,System.UInt32[],System.Boolean)' called on client");
			return;
		}
		d.Assert(techData != null);
		d.Assert(techDataBlockPoolIDs != null);
		d.Assert(techData.m_BlockSpecs.Count == techDataBlockPoolIDs.Length);
		m_TechData = new TechData();
		m_TechData.CopyWithoutSaveData(techData);
		m_OriginalSaveData = techData.m_TechSaveState;
		m_TechDataBlockPoolIDs = techDataBlockPoolIDs;
		m_RecycleFailedAddsOnSpawn = recycleFailedAdds;
		SetDirtyBit(2u);
	}

	[Server]
	public void ServerSetOwner(NetPlayer owner, bool isBeingDestroyed = false)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::ServerSetOwner(NetPlayer,System.Boolean)' called on client");
			return;
		}
		NetPlayer = owner;
		SetDirtyBit(1u);
		if (m_ObserversInitialised && !isBeingDestroyed)
		{
			NetIdentity.RebuildObservers(initialize: false);
		}
		Singleton.Manager<ManBlockLimiter>.inst.TagAsInteresting(tech);
	}

	[Server]
	public void OnServerSetTeam(int team)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetTeam(System.Int32)' called on client");
		}
		else
		{
			OnServerSetTeam(team, tech.IsPopulation);
		}
	}

	[Server]
	public void OnServerSetTeam(int team, bool isPopulation, bool justSerialisedValue = false)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetTeam(System.Int32,System.Boolean,System.Boolean)' called on client");
			return;
		}
		d.Assert(team != -1, "Team has not been assigned, but we are serializing tech");
		Team = team;
		if (!justSerialisedValue)
		{
			tech.SetTeam(team, isPopulation);
			ChangeTechTeamRequestMessage message = new ChangeTechTeamRequestMessage
			{
				m_Team = team,
				m_IsPopulation = isPopulation
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.ChangeTechTeamRequest, message, base.netId);
		}
		SetDirtyBit(512u);
		SetDirtyBit(262144u);
	}

	[Server]
	public void OnServerSetLockTechSendToSCU()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetLockTechSendToSCU()' called on client");
		}
		else
		{
			SetDirtyBit(524288u);
		}
	}

	[Server]
	public void OnServerSetLockBlockAttach()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetLockBlockAttach()' called on client");
		}
		else
		{
			SetDirtyBit(1048576u);
		}
	}

	[Server]
	public void OnServerSetLockBlockDetach()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetLockBlockDetach()' called on client");
		}
		else
		{
			SetDirtyBit(2097152u);
		}
	}

	[Server]
	public void OnServerSetShowOverlayDirty()
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetShowOverlayDirty()' called on client");
		}
		else
		{
			SetDirtyBit(4194304u);
		}
	}

	[Server]
	public void OnServerSetStartsInvulnerable(bool startInvulnerable)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetStartsInvulnerable(System.Boolean)' called on client");
			return;
		}
		m_Invulnerable = startInvulnerable;
		TankControl.State curState = tech.control.CurState;
		curState.m_Beam = startInvulnerable;
		tech.control.CurState = curState;
	}

	[Server]
	public void OnServerSetName(string name)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetName(System.String)' called on client");
			return;
		}
		m_TechData.Name = name;
		m_TechData.RestoreTechName(tech);
		SetDirtyBit(2u);
	}

	[Server]
	public void OnServerSetRadarMarker(RadarMarker info)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetRadarMarker(RadarMarker)' called on client");
			return;
		}
		m_TechData.RadarMarkerConfig = info;
		m_TechData.RestoreTechRadarMarker(tech);
		SetDirtyBit(2u);
	}

	[Server]
	public void OnServerSetAuthor(string author)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::OnServerSetAuthor(System.String)' called on client");
			return;
		}
		m_TechData.Author = author;
		m_TechData.RestoreTechAuthor(tech);
		SetDirtyBit(2u);
	}

	private void OnSetAuthor(NetworkMessage netMsg)
	{
		SetTechAuthorMessage setTechAuthorMessage = netMsg.ReadMessage<SetTechAuthorMessage>();
		d.Assert(setTechAuthorMessage != null, "NetMessage not received correctly in - OnSetAuthor - while attempting to set Authorship");
		SetAuthorMultiplayerSafe(setTechAuthorMessage.m_NetPlayerName, setFromHere: false);
	}

	[Client]
	private void OnClientSetTechTeam(NetworkMessage netMsg)
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void NetTech::OnClientSetTechTeam(UnityEngine.Networking.NetworkMessage)' called on server");
			return;
		}
		ChangeTechTeamRequestMessage changeTechTeamRequestMessage = netMsg.ReadMessage<ChangeTechTeamRequestMessage>();
		tech.SetTeam(changeTechTeamRequestMessage.m_Team, changeTechTeamRequestMessage.m_IsPopulation);
	}

	public void SetAuthorMultiplayerSafe(string authorName, bool setFromHere = true)
	{
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return;
		}
		tech.Author = authorName;
		if (setFromHere || ManNetwork.IsHost)
		{
			SetTechAuthorMessage message = new SetTechAuthorMessage
			{
				m_NetPlayerName = authorName
			};
			if (ManNetwork.IsHost)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.SetAuthor, message, base.netId);
				OnServerSetAuthor(authorName);
			}
			else
			{
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetAuthor, message, base.netId);
			}
		}
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.UpdateTechControls, OnServerControlsUpdated);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.UpdateTechTarget, OnServerTargetUpdated);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.UpdateTechWeapons, OnServerWeaponsUpdated);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.RequestTeamChange, OnServerTeamChangeRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.UpdateTechAnchor, OnServerUpdateTechAnchor);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.ModifyControlledCategories, OnServerModifyControlledCategories);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetCategoryActive, OnServerSetKillSwitched);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.UpdateHoverAndGyro, OnServerSetHoverAndGyro);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetAIMode, OnServerSetAIMode);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetHeartbeatSpeed, OnServerSetHeartbeatSpeed);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.FlipConveyorLoopDirection, OnServerFlipConveyorLoopDirection);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetCraftingRecipe, OnServerSetCraftingRecipe);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SendMissionPromptResponse, OnServerMissionPromptResponse);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.DebugSetInvulnerable, OnServerDebugSetInvulnerable);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetAuthor, OnSetAuthor);
		if (ManSpawn.IsEnemyTeam(Team))
		{
			bool wrapBlock = m_TechData.m_BlockSpecs.Count == 1;
			Singleton.Manager<ManSpawn>.inst.SetupNetTech(tech, m_TechData, m_OriginalSaveData, m_TechDataBlockPoolIDs, wrapBlock, m_RecycleFailedAddsOnSpawn);
			Singleton.Manager<ManSpawn>.inst.SetTankController(tech);
			tech.AI.SetOldBehaviour();
		}
		if (m_Invulnerable)
		{
			m_RebuildTimer = Singleton.Manager<ManNetwork>.inst.NetController.RespawnInvulnerabilityTime;
			SetDirtyBit(32u);
		}
		StartServerEvent.Send();
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		d.Log($"[NetTech] Spawned tech called {base.name} on client. netID={base.netId} ownerNetId={ClientOwnerNetId} World origin is {Singleton.Manager<ManWorld>.inst.FloatingOrigin}");
		if (Singleton.Manager<ManWorldTreadmill>.inst.HasPendingNetworkMove)
		{
			d.Log($"[NetTech] World treadmill has a pending move to {Singleton.Manager<ManWorldTreadmill>.inst.PendingNetworkOrigin}");
		}
		Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(tech, null);
		bool wrapBlock = m_TechData.m_BlockSpecs.Count == 1;
		Singleton.Manager<ManSpawn>.inst.SetupNetTech(tech, m_TechData, m_OriginalSaveData, m_TechDataBlockPoolIDs, wrapBlock, m_RecycleFailedAddsOnSpawn);
		if (!base.isServer)
		{
			Singleton.Manager<ManLooseBlocks>.inst.RegisterBlockPoolIDsFromTank(tech);
		}
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock current = enumerator.Current;
			m_NetworkedModules.AddRange(current.NetworkedModules);
		}
		if (!base.isServer)
		{
			UpdateBlockDataFromNetwork();
			tech.Anchors.UpdateAnchorsFromNetwork();
		}
		Singleton.Manager<ManLooseBlocks>.inst.AttachPendingVisiblesToTech(this);
		if (m_NetPlayer == null && m_ClientOwnerNetId != NetworkInstanceId.Invalid)
		{
			NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(m_ClientOwnerNetId.Value);
			if (netPlayer != null)
			{
				OnClientSetOwner(netPlayer);
			}
		}
		d.Assert(NetPlayer == null || NetPlayer.TechTeamID == Team, "Team mismatch between tech and player");
		Singleton.Manager<ManSpawn>.inst.SetTankController(tech);
		if (!base.isServer)
		{
			tech.AI.SetBehaviorType(AITreeType.AITypes.Idle);
		}
		tech.SetSafeArea();
		Singleton.Manager<ManNetTechs>.inst.OnTechAdded(this);
		if (m_Invulnerable)
		{
			bool force = true;
			tech.beam.EnableBeam(enable: true, force);
		}
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.TechBlockDamaged, OnClientTechBlockDamaged);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.RemoveAllBlocksFromTech, OnClientRemoveAllBlocksMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.SetAuthor, OnSetAuthor);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.ChangeTechTeamRequest, OnClientSetTechTeam);
		StartClientEvent.Send();
	}

	public override void OnStartAuthority()
	{
		base.OnStartAuthority();
		d.Log($"[NetTech] Took authority of {base.name}. World origin is {Singleton.Manager<ManWorld>.inst.FloatingOrigin}");
		if (Singleton.Manager<ManWorldTreadmill>.inst.HasPendingNetworkMove)
		{
			d.Log($"[NetTech] World treadmill has a pending move to {Singleton.Manager<ManWorldTreadmill>.inst.PendingNetworkOrigin}");
		}
		ResetWheelNetworkedState();
		m_AnchorsDirty = false;
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer == null)
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				d.LogWarning("Tech spawned before our player did. Should be valid ONCE ONLY on loading into game because of the first-frame MyPlayer glitch");
			}
		}
		else if (Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech == null && Singleton.Manager<ManNetwork>.inst.MyPlayer == NetPlayer)
		{
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tech);
		}
	}

	public override void OnStopAuthority()
	{
		base.OnStopAuthority();
		ResetWheelNetworkedState();
	}

	public void ResetWheelNetworkedState()
	{
		ModuleWheels[] componentsInChildren = GetComponentsInChildren<ModuleWheels>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].ResetNetworkedState();
		}
	}

	public override void OnNetworkDestroy()
	{
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer == NetPlayer)
		{
			Visible draggingItem = Singleton.Manager<ManPointer>.inst.DraggingItem;
			if (draggingItem != null && draggingItem.block != null && !draggingItem.block.IsController && !draggingItem.block.Anchor)
			{
				if (Singleton.Manager<ManPointer>.inst.BuildMode == ManPointer.BuildingMode.PaintBlock)
				{
					Singleton.Manager<ManPointer>.inst.RemovePaintingBlock();
				}
				else
				{
					Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem();
				}
			}
		}
		if (base.isServer)
		{
			if (NetPlayer != null)
			{
				NetPlayer.ServerSetTech(null, isBeingDestroyed: true);
			}
			Singleton.Manager<ManNetwork>.inst.NetTechDestroyed.Send(tech);
		}
		Singleton.Manager<ManNetTechs>.inst.OnTechRemoved(this);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromMessages(base.netId);
		m_LastSentControls = default(TankControl.State);
		if (base.isServer && m_InitialSpawnShieldID != 0)
		{
			foreach (KeyValuePair<NetworkInstanceId, NetworkIdentity> @object in NetworkServer.objects)
			{
				NetBlock component = @object.Value.gameObject.GetComponent<NetBlock>();
				if (component != null && component.InitialSpawnShieldID == m_InitialSpawnShieldID)
				{
					component.InitialSpawnShieldID = 0u;
				}
			}
			NetSpawnPoint pNSP = null;
			if (Singleton.Manager<ManNetwork>.inst.IsSpawnShieldActive(InitialSpawnShieldID, ref pNSP))
			{
				pNSP.ServerSetShieldEnabled(enabled: false);
				pNSP.ServerSetBarrierEnabled(barrierEnabled: false);
			}
		}
		base.OnNetworkDestroy();
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? uint.MaxValue : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		if ((num & 1) != 0)
		{
			writer.Write(OwnerNetId);
		}
		if ((num & 2) != 0)
		{
			m_TechData.NetSerialize(writer);
			d.Assert(m_TechDataBlockPoolIDs != null);
			d.Assert(m_TechData.m_BlockSpecs.Count == m_TechDataBlockPoolIDs.Length);
			writer.Write(m_TechDataBlockPoolIDs.Length);
			for (int i = 0; i < m_TechDataBlockPoolIDs.Length; i++)
			{
				writer.WritePackedUInt32(m_TechDataBlockPoolIDs[i]);
			}
			writer.Write(m_RecycleFailedAddsOnSpawn);
		}
		if ((num & 4) != 0)
		{
			tech.control.CurState.NetSerialize(writer);
		}
		if ((num & 8) != 0)
		{
			writer.Write(m_LastTargettedId.Value);
		}
		if ((num & 0x10) != 0)
		{
			writer.Write(m_Invulnerable);
		}
		if ((num & 0x20) != 0)
		{
			writer.Write(m_RebuildTimer);
		}
		if ((num & 0x40) != 0)
		{
			writer.Write(tech.control.TargetPositionWorld);
			writer.Write(tech.control.TargetRadiusWorld);
		}
		if ((num & 0x80) != 0)
		{
			m_WeaponState.NetSerialize(writer);
			if (m_WeaponState.FiredBarrels != null)
			{
				m_WeaponState.FiredBarrels.Clear();
			}
		}
		if ((num & 0x100) != 0)
		{
			writer.WritePackedUInt32(m_InitialSpawnShieldID);
		}
		if ((num & 0x200) != 0)
		{
			writer.Write(Team);
		}
		if ((num & 0x400) != 0)
		{
			foreach (INetworkedModule networkedModule in m_NetworkedModules)
			{
				if (initialState || m_DirtyModules.Contains(networkedModule))
				{
					s_ModulesToWrite.Add(networkedModule);
				}
			}
			writer.Write(s_ModulesToWrite.Count);
			foreach (INetworkedModule item in s_ModulesToWrite)
			{
				uint blockPoolID = item.GetBlock().blockPoolID;
				writer.WritePackedUInt32(blockPoolID);
				writer.WritePackedInt32((int)item.GetModuleID());
				s_DataSubStream.SeekZero();
				BlockMessage_Base.s_ModuleBlockPoolID = blockPoolID;
				item.OnSerialize(s_DataSubStream);
				BlockMessage_Base.s_ModuleBlockPoolID = uint.MaxValue;
				writer.WriteBytesFull(s_DataSubStream.ToArray());
			}
			s_ModulesToWrite.Clear();
		}
		if ((num & 0x800) != 0)
		{
			INetworkedTechComponent[] networkedTechComponents = m_NetworkedTechComponents;
			foreach (INetworkedTechComponent networkedTechComponent in networkedTechComponents)
			{
				if (initialState || m_DirtyTechComponents.Contains((int)networkedTechComponent.GetTechComponentID()))
				{
					s_TechComponentsToWrite.Add(networkedTechComponent);
				}
			}
			writer.Write(s_TechComponentsToWrite.Count);
			foreach (INetworkedTechComponent item2 in s_TechComponentsToWrite)
			{
				writer.Write((byte)item2.GetTechComponentID());
				s_DataSubStream.SeekZero();
				item2.OnSerialize(s_DataSubStream);
				writer.WriteBytesFull(s_DataSubStream.ToArray());
			}
			s_TechComponentsToWrite.Clear();
		}
		if ((num & 0x1000) != 0)
		{
			writer.Write(m_HostID);
		}
		if ((num & 0x8000) != 0)
		{
			tech.control.OnSerialiseControlSchemesInitial(writer);
		}
		if ((num & 0x20000) != 0)
		{
			writer.Write(m_PendingProjectileUpdates.Count);
			foreach (ProjectileUpdate pendingProjectileUpdate in m_PendingProjectileUpdates)
			{
				writer.Write(pendingProjectileUpdate.m_ProjectileID);
				writer.Write(pendingProjectileUpdate.m_LockedOnToNetId);
				writer.Write(pendingProjectileUpdate.m_LockedOnToBlockID);
				writer.Write(pendingProjectileUpdate.m_Pos);
				writer.Write(pendingProjectileUpdate.m_Rot);
			}
			m_PendingProjectileUpdates.Clear();
		}
		if ((num & 0x40000) != 0)
		{
			writer.Write(tech.IsPopulation);
		}
		if ((num & 0x80000) != 0)
		{
			writer.Write(tech.visible.GetLockedRemainingDuration(Visible.LockTimerTypes.SendToSCU));
		}
		if ((num & 0x100000) != 0)
		{
			writer.Write(tech.visible.GetLockedRemainingDuration(Visible.LockTimerTypes.BlocksAttachable));
		}
		if ((num & 0x200000) != 0)
		{
			writer.Write(tech.visible.GetLockedRemainingDuration(Visible.LockTimerTypes.BlocksAttachable));
		}
		if ((num & 0x400000) != 0)
		{
			writer.Write(tech.ShouldShowOverlay);
		}
		if ((num & 0x800000) != 0)
		{
			writer.Write(m_IsMaterialOverride);
			writer.WritePackedInt32((int)m_MaterialOverrideType);
		}
		m_DirtyModules.Clear();
		m_DirtyTechComponents.Clear();
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		if ((num & 1) != 0)
		{
			m_ClientOwnerNetId = reader.ReadNetworkId();
			NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(m_ClientOwnerNetId.Value);
			if ((m_ClientOwnerNetId == NetworkInstanceId.Invalid) ? (netPlayer == null) : (netPlayer != null))
			{
				OnClientSetOwner(netPlayer);
			}
		}
		if ((num & 2) != 0)
		{
			m_TechData = new TechData();
			m_TechData.NetDeserialize(reader);
			m_OriginalSaveData = m_TechData.m_TechSaveState;
			int num2 = reader.ReadInt32();
			d.Assert(num2 == m_TechData.m_BlockSpecs.Count);
			m_TechDataBlockPoolIDs = new uint[num2];
			for (int i = 0; i < num2; i++)
			{
				m_TechDataBlockPoolIDs[i] = reader.ReadPackedUInt32();
			}
			m_RecycleFailedAddsOnSpawn = reader.ReadBoolean();
			string text = tech.name;
			m_TechData.RestoreTechName(tech);
			if (text != tech.name)
			{
				Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(tech, null);
			}
			m_TechData.RestoreTechRadarMarker(tech);
			m_TechData.RestoreTechAuthor(tech);
		}
		if ((num & 4) != 0)
		{
			TankControl.State curState = default(TankControl.State);
			curState.NetDeserialize(reader);
			if (!base.hasAuthority || NetPlayer == null)
			{
				tech.control.CurState = curState;
			}
		}
		if ((num & 8) != 0)
		{
			NetworkInstanceId networkInstanceId = new NetworkInstanceId(reader.ReadUInt32());
			if (!base.hasAuthority || NetPlayer == null)
			{
				SetTargettedTechById(networkInstanceId, shouldSetNeedsTargetIfReqd: true);
				m_LastTargettedId = networkInstanceId;
			}
		}
		if ((num & 0x10) != 0)
		{
			m_Invulnerable = reader.ReadBoolean();
		}
		if ((num & 0x20) != 0)
		{
			m_RebuildTimer = reader.ReadSingle();
		}
		if ((num & 0x40) != 0)
		{
			tech.control.TargetPositionWorld = reader.ReadVector3();
			tech.control.TargetRadiusWorld = reader.ReadSingle();
		}
		if ((num & 0x80) != 0)
		{
			TechWeapon.State weaponState = default(TechWeapon.State);
			weaponState.NetDeserialize(reader);
			if (!base.hasAuthority || NetPlayer == null)
			{
				m_WeaponState = weaponState;
				if (weaponState.FiredBarrels != null)
				{
					SetFiredBarrels(weaponState.FiredBarrels);
				}
			}
		}
		if ((num & 0x100) != 0)
		{
			m_InitialSpawnShieldID = reader.ReadPackedUInt32();
		}
		if ((num & 0x200) != 0)
		{
			int team = reader.ReadInt32();
			Team = team;
			tech.SetTeam(Team);
		}
		if ((num & 0x400) != 0)
		{
			int num3 = reader.ReadInt32();
			m_PendingBlockData.Clear();
			if (num3 > m_PendingBlockData.Capacity)
			{
				m_PendingBlockData.Capacity = num3;
			}
			for (int j = 0; j < num3; j++)
			{
				SerializableBlockData item = new SerializableBlockData
				{
					m_BlockPoolID = reader.ReadPackedUInt32(),
					m_ModuleID = (NetworkedModuleID)reader.ReadPackedInt32(),
					m_Data = new NetworkReader(reader.ReadBytesAndSize())
				};
				m_PendingBlockData.Add(item);
			}
			UpdateBlockDataFromNetwork();
		}
		if ((num & 0x800) != 0)
		{
			int num4 = reader.ReadInt32();
			for (int k = 0; k < num4; k++)
			{
				NetworkedTechComponentID networkedTechComponentID = (NetworkedTechComponentID)reader.ReadByte();
				NetworkReader reader2 = new NetworkReader(reader.ReadBytesAndSize());
				bool condition = false;
				INetworkedTechComponent[] networkedTechComponents = m_NetworkedTechComponents;
				foreach (INetworkedTechComponent networkedTechComponent in networkedTechComponents)
				{
					if (networkedTechComponent.GetTechComponentID() == networkedTechComponentID)
					{
						condition = true;
						networkedTechComponent.OnDeserialize(reader2);
						break;
					}
				}
				d.AssertFormat(condition, "Could not find techComponent with ID {0} on tech {1} to receive network data", networkedTechComponentID, base.name);
			}
		}
		if ((num & 0x1000) != 0)
		{
			m_HostID = reader.ReadInt32();
			Singleton.Manager<ManVisible>.inst.TryLinkVisibleToTrackedVisible(tech.visible, m_HostID);
		}
		if ((num & 0x8000) != 0)
		{
			tech.control.OnDeserialiseControlSchemesInitial(reader);
		}
		if ((num & 0x20000) != 0)
		{
			m_PendingProjectileUpdates.Clear();
			int num5 = reader.ReadInt32();
			for (int m = 0; m < num5; m++)
			{
				ProjectileUpdate projectileUpdate = new ProjectileUpdate();
				projectileUpdate.m_ProjectileID = reader.ReadInt32();
				projectileUpdate.m_LockedOnToNetId = reader.ReadNetworkId();
				projectileUpdate.m_LockedOnToBlockID = reader.ReadUInt32();
				projectileUpdate.m_Pos = reader.ReadVector3();
				projectileUpdate.m_Rot = reader.ReadQuaternion();
				ClientProcessProjectileUpdate(projectileUpdate);
			}
		}
		if ((num & 0x40000) != 0)
		{
			bool isPopulation = reader.ReadBoolean();
			tech.SetTeam(tech.Team, isPopulation);
		}
		if ((num & 0x80000) != 0)
		{
			float additionalTime = reader.ReadSingle();
			tech.visible.SetLockTimout(Visible.LockTimerTypes.SendToSCU, additionalTime);
		}
		if ((num & 0x100000) != 0)
		{
			float additionalTime2 = reader.ReadSingle();
			tech.visible.SetLockTimout(Visible.LockTimerTypes.BlocksAttachable, additionalTime2);
		}
		if ((num & 0x200000) != 0)
		{
			float additionalTime3 = reader.ReadSingle();
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.visible.SetLockTimout(Visible.LockTimerTypes.Grabbable, additionalTime3);
			}
		}
		if ((num & 0x400000) != 0)
		{
			tech.ShouldShowOverlay = reader.ReadBoolean();
		}
		if ((num & 0x800000) != 0)
		{
			m_IsMaterialOverride = reader.ReadBoolean();
			m_MaterialOverrideType = (ManTechMaterialSwap.MatType)reader.ReadPackedInt32();
			tech.SetCustomMaterialOverride(m_IsMaterialOverride, m_MaterialOverrideType);
		}
	}

	[Server]
	public void ServerQueueProjectileUpdate(int projectileID, Visible lockedOnToVis, Visible.WeakReference lockedOnToBlock, Vector3 pos, Quaternion rot)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::ServerQueueProjectileUpdate(System.Int32,Visible,Visible/WeakReference,UnityEngine.Vector3,UnityEngine.Quaternion)' called on client");
			return;
		}
		TankBlock tankBlock = lockedOnToBlock.Get()?.block;
		NetworkIdentity networkIdentity = lockedOnToVis?.GetComponent<NetworkIdentity>();
		m_PendingProjectileUpdates.Add(new ProjectileUpdate
		{
			m_ProjectileID = projectileID,
			m_LockedOnToNetId = (networkIdentity.IsNotNull() ? networkIdentity.netId : NetworkInstanceId.Invalid),
			m_LockedOnToBlockID = ((tankBlock != null) ? tankBlock.blockPoolID : uint.MaxValue),
			m_Pos = pos,
			m_Rot = rot
		});
		SetDirtyBit(131072u);
	}

	[Client]
	private void ClientProcessProjectileUpdate(ProjectileUpdate update)
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void NetTech::ClientProcessProjectileUpdate(NetTech/ProjectileUpdate)' called on server");
			return;
		}
		WeaponRound weaponRound = ManCombat.Projectiles.FindWeaponRound(update.m_ProjectileID);
		if (!weaponRound.IsNotNull())
		{
			return;
		}
		weaponRound.transform.position = update.m_Pos;
		weaponRound.transform.rotation = update.m_Rot;
		if (weaponRound is Projectile && ((Projectile)weaponRound).SeekingProjectile.IsNotNull())
		{
			Visible target = ClientScene.FindLocalObject(update.m_LockedOnToNetId)?.GetComponent<Visible>();
			TankBlock block = null;
			if (update.m_LockedOnToBlockID != uint.MaxValue)
			{
				block = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(update.m_LockedOnToBlockID);
			}
			((Projectile)weaponRound).SeekingProjectile.ClientSetTarget(target, block);
		}
		else
		{
			d.LogError("Recieved update for non-seeking projectile");
		}
	}

	[Client]
	private void UpdateBlockDataFromNetwork()
	{
		if (!NetworkClient.active)
		{
			Debug.LogWarning("[Client] function 'System.Void NetTech::UpdateBlockDataFromNetwork()' called on server");
		}
		else if (tech.blockman.blockCount != 0)
		{
			int i = -1;
			foreach (SerializableBlockData pendingBlockDatum in m_PendingBlockData)
			{
				bool condition = false;
				i++;
				if (i >= m_NetworkedModules.Count)
				{
					d.LogError("UpdateBlockDataFromNetwork looped around at least once! Assumption about data-order is somehow not valid!");
					i = 0;
				}
				for (; i < m_NetworkedModules.Count; i++)
				{
					INetworkedModule networkedModule = m_NetworkedModules[i];
					if (networkedModule.GetBlock().blockPoolID == pendingBlockDatum.m_BlockPoolID && networkedModule.GetModuleID() == pendingBlockDatum.m_ModuleID)
					{
						condition = true;
						BlockMessage_Base.s_ModuleBlockPoolID = pendingBlockDatum.m_BlockPoolID;
						networkedModule.OnDeserialize(pendingBlockDatum.m_Data);
						BlockMessage_Base.s_ModuleBlockPoolID = uint.MaxValue;
						break;
					}
				}
				d.AssertFormat(condition, "Could not find module with blockPoolID {0} and moduleID {1} on tech {2} to receive network data", pendingBlockDatum.m_BlockPoolID, pendingBlockDatum.m_ModuleID, base.name);
			}
			m_PendingBlockData.Clear();
		}
		else
		{
			d.LogFormat("Tech + {0} has no blocks, so deferring networked module update to OnStartClient", base.name);
		}
	}

	public override bool OnCheckObserver(NetworkConnection conn)
	{
		return Singleton.Manager<ManNetwork>.inst.CheckObserver(this, conn);
	}

	public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize)
	{
		if (initialize)
		{
			m_ObserversInitialised = true;
		}
		return Singleton.Manager<ManNetwork>.inst.RebuildObservers(this, observers);
	}

	public void SaveTechData()
	{
		m_TechData.SaveTech(tech);
		int blockCount = tech.blockman.blockCount;
		d.Assert(m_TechData.m_BlockSpecs.Count == blockCount);
		if (m_TechDataBlockPoolIDs == null || m_TechDataBlockPoolIDs.Length != blockCount)
		{
			m_TechDataBlockPoolIDs = new uint[blockCount];
		}
		d.Assert(m_TechDataBlockPoolIDs != null && m_TechDataBlockPoolIDs.Length == blockCount);
		for (int i = 0; i < blockCount; i++)
		{
			TankBlock blockWithIndex = tech.blockman.GetBlockWithIndex(i);
			m_TechDataBlockPoolIDs[i] = blockWithIndex.blockPoolID;
		}
	}

	public void QueueSaveTechData()
	{
		m_QueuedSaveTechData = true;
	}

	public bool RequestRemoveFromGame(NetPlayer previouslyOwningPlayer, bool wasKilled)
	{
		bool result = false;
		if (isMultiplayer)
		{
			Singleton.Manager<ManNetwork>.inst.AddToDisposalContainer(base.transform);
			if (isHost)
			{
				if (wasKilled)
				{
					if (previouslyOwningPlayer != null && previouslyOwningPlayer != Singleton.Manager<ManNetwork>.inst.MyPlayer)
					{
						Singleton.Manager<ManNetwork>.inst.SendToClient(previouslyOwningPlayer.connectionToClient.connectionId, TTMsgType.TechDestroyed, new TargetTechMessage
						{
							m_TechId = NetIdentity.netId
						});
					}
					DoScoringOnTechRemove(previouslyOwningPlayer);
					if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp() && tech.IsNotNull() && !tech.blockman.PlayerInitiatedRemoveBlock && m_TechData.m_BlockSpecs.Count > 0)
					{
						SendDestroyedChatMessage(previouslyOwningPlayer);
					}
				}
				DoRemoveFromGame();
				result = true;
			}
			else
			{
				d.LogError("Should not be trying to remove techs directly on clients");
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	private void SendDestroyedChatMessage(NetPlayer previouslyOwningPlayer)
	{
		if (previouslyOwningPlayer.IsNull() && tech.IsEnemy() && tech.DamagedByPlayer && tech.OriginalValue > 0f)
		{
			int num = (int)(tech.OriginalValue * Globals.inst.m_TechDestroyedBBMultiplier);
			int num2 = (int)(tech.OriginalValue * Globals.inst.m_TechDestroyedXPMultiplier);
			FactionSubTypes mainCorp = tech.GetMainCorp();
			SystemChatMessage systemChatMessage = new SystemChatMessage();
			systemChatMessage.m_Bank = LocalisationEnums.StringBanks.SystemChatMessage;
			systemChatMessage.m_StringID = 3;
			systemChatMessage.m_Params = new string[4]
			{
				ColourConverter.RecolourRichText(Color.red, tech.name),
				num2.ToString(),
				mainCorp.ToString(),
				num.ToString()
			};
			SystemChatMessage message = systemChatMessage;
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.AddSystemChatMessage, message);
		}
		else if (previouslyOwningPlayer.IsNotNull())
		{
			SystemChatMessage systemChatMessage = new SystemChatMessage();
			systemChatMessage.m_Bank = LocalisationEnums.StringBanks.SystemChatMessage;
			systemChatMessage.m_StringID = 2;
			systemChatMessage.m_Params = new string[1] { previouslyOwningPlayer.GetColouredName() };
			SystemChatMessage message2 = systemChatMessage;
			Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.AddSystemChatMessage, message2);
		}
	}

	private void DoScoringOnTechRemove(NetPlayer previouslyOwningPlayer)
	{
		if (base.isServer && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing && !tech.blockman.PlayerInitiatedRemoveBlock)
		{
			if (LastDamager != null)
			{
				LastDamager.OnServerAddKill();
			}
			if (previouslyOwningPlayer != null && m_TechData.m_BlockSpecs.Count > 0)
			{
				previouslyOwningPlayer.OnServerAddDeath(LastDamager != null);
			}
		}
	}

	private void DoRemoveFromGame()
	{
		if (NetPlayer != null)
		{
			NetPlayer.ServerSetTech(null, isBeingDestroyed: true);
		}
		NetworkServer.UnSpawn(base.gameObject);
	}

	public void OnClientSetOwner(NetPlayer netPlayer)
	{
		NetPlayer = netPlayer;
		if ((bool)Singleton.Manager<ManNetwork>.inst.MyPlayer && NetPlayer == Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			Singleton.Manager<ManTechs>.inst.SetPlayerTankLocally(tech);
		}
		Singleton.Manager<ManTechs>.inst.TankNameChangedEvent.Send(tech, null);
		m_ClientOwnerNetId = NetworkInstanceId.Invalid;
		ResetWheelNetworkedState();
		if (ManNetwork.IsHost && m_ObserversInitialised)
		{
			NetIdentity.RebuildObservers(initialize: false);
		}
	}

	private NetworkInstanceId GetIdOfTargettedTech()
	{
		NetworkInstanceId invalid = NetworkInstanceId.Invalid;
		Visible visible = null;
		if (NetPlayer.IsNotNull() ? (NetPlayer == Singleton.Manager<ManNetwork>.inst.MyPlayer) : ManNetwork.IsHost)
		{
			visible = tech.Weapons.GetManualTarget();
		}
		if (visible == null)
		{
			visible = tech.Vision.GetFirstVisibleTechIsEnemy(tech.Team);
		}
		if (visible != null && visible.tank != null && visible.tank.netTech != null)
		{
			invalid = visible.tank.netTech.netId;
		}
		return invalid;
	}

	private bool SetTargettedTechById(NetworkInstanceId id, bool shouldSetNeedsTargetIfReqd)
	{
		if (shouldSetNeedsTargetIfReqd)
		{
			m_NeedsTargetCheckAndSet = false;
			m_NeedsTargetCheckAndSetTimeout = 0f;
		}
		Visible visible = null;
		if (id != NetworkInstanceId.Invalid)
		{
			GameObject gameObject = (base.isServer ? NetworkServer.FindLocalObject(id) : ClientScene.FindLocalObject(id));
			if (gameObject != null)
			{
				NetTech component = gameObject.GetComponent<NetTech>();
				if (component != null)
				{
					visible = component.tech.visible;
				}
			}
			else if (shouldSetNeedsTargetIfReqd)
			{
				m_NeedsTargetCheckAndSet = true;
				m_NeedsTargetCheckAndSetTimeout = Time.realtimeSinceStartup + 5f;
			}
		}
		tech.Vision.SetClosestEnemy(visible);
		return visible != null;
	}

	private void OnServerUpdateTechAnchor(NetworkMessage netMsg)
	{
		tech.Anchors.OnDeserialize(netMsg.reader);
	}

	[Server]
	public void SetModuleDirty(INetworkedModule module)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetTech::SetModuleDirty(INetworkedModule)' called on client");
			return;
		}
		m_DirtyModules.Add(module);
		SetDirtyBit(1024u);
	}

	public void SetTechComponentDirty(INetworkedTechComponent component)
	{
		m_DirtyTechComponents.Set((int)component.GetTechComponentID(), enabled: true);
		SetDirtyBit(2048u);
	}

	public void SetAnchorsDirty()
	{
		SetTechComponentDirty(tech.Anchors);
		m_AnchorsDirty = true;
	}

	public void SetAIDisplayCategoryDirty()
	{
		SetTechComponentDirty(tech.AI);
	}

	private void OnServerModifyControlledCategories(NetworkMessage netMsg)
	{
		ModifyControlledCategoriesMessage modifyControlledCategoriesMessage = netMsg.ReadMessage<ModifyControlledCategoriesMessage>();
		TankBlock tankBlock = tech.blockman.GetBlockWithID(modifyControlledCategoriesMessage.m_BlockPoolID);
		if (tankBlock.IsNull())
		{
			d.LogWarning("OnServerModifyControlledCategories - Could not find block on our tech");
			tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(modifyControlledCategoriesMessage.m_BlockPoolID);
			if (tankBlock.IsNull())
			{
				d.LogError("OnServerModifyControlledCategories - Could not find block anywhere.");
				return;
			}
		}
		ModuleBlockStateController component = tankBlock.GetComponent<ModuleBlockStateController>();
		if (component.IsNotNull())
		{
			component.ModifyControlledCategory(modifyControlledCategoriesMessage.m_Category, modifyControlledCategoriesMessage.m_Controlled);
			SetModuleDirty(component);
		}
		else
		{
			d.LogError("OnServerModifyControlledCategories - Found a block that did not have a ModuleBlockStateController on it");
		}
	}

	private void OnServerSetKillSwitched(NetworkMessage netMsg)
	{
		SetCategoryActiveMessage setCategoryActiveMessage = netMsg.ReadMessage<SetCategoryActiveMessage>();
		tech.BlockStateController.SetCategoryActive(setCategoryActiveMessage.m_ModuleCategory, setCategoryActiveMessage.m_Active, ignoreRegistration: true);
		SetTechComponentDirty(tech.BlockStateController);
	}

	private void OnServerSetHoverAndGyro(NetworkMessage netMsg)
	{
		UpdateHoverAndGyroMessage updateHoverAndGyroMessage = netMsg.ReadMessage<UpdateHoverAndGyroMessage>();
		tech.BlockStateController.GyroTrim = updateHoverAndGyroMessage.m_Gyro;
		tech.BlockStateController.HoverPower = updateHoverAndGyroMessage.m_Hover;
		SetTechComponentDirty(tech.BlockStateController);
	}

	private void OnServerSetHeartbeatSpeed(NetworkMessage netMsg)
	{
		SetHeartbeatSpeedMessage setHeartbeatSpeedMessage = netMsg.ReadMessage<SetHeartbeatSpeedMessage>();
		tech.Holders.SetHeartbeatSpeed(setHeartbeatSpeedMessage.m_Speed);
		SetTechComponentDirty(tech.Holders);
	}

	private void OnServerFlipConveyorLoopDirection(NetworkMessage netMsg)
	{
		EmptyBlockMessage emptyBlockMessage = netMsg.ReadMessage<EmptyBlockMessage>();
		if (TryGetModuleFromBlockID<ModuleItemConveyor>(emptyBlockMessage.m_BlockPoolID, out var moduleOut))
		{
			moduleOut.FlipLoopDirection();
		}
	}

	private void OnServerSetCraftingRecipe(NetworkMessage netMsg)
	{
		SetCraftingRecipeMessage setCraftingRecipeMessage = netMsg.ReadMessage<SetCraftingRecipeMessage>();
		if (TryGetModuleFromBlockID<ModuleItemConsume>(setCraftingRecipeMessage.m_BlockPoolID, out var moduleOut))
		{
			if (setCraftingRecipeMessage.m_HasRecipe)
			{
				RecipeTable.Recipe recipe = moduleOut.GetRecipe(setCraftingRecipeMessage.m_RecipeDef);
				d.Assert(recipe != null, "Failed to find recipe from received message data!");
				moduleOut.BeginCraftingRecipe(recipe);
			}
			else
			{
				moduleOut.CancelRecipe();
			}
			moduleOut.UpdateNetworkedState(triggerSync: true);
		}
	}

	private void OnServerSetAIMode(NetworkMessage netMsg)
	{
		SetAIModeMessage setAIModeMessage = netMsg.ReadMessage<SetAIModeMessage>();
		tech.AI.SetBehaviorType(setAIModeMessage.m_AIAction);
	}

	private void SetFiredBarrels(List<TechWeapon.FiredBarrel> firedBarrels)
	{
		if (firedBarrels != null)
		{
			for (int i = 0; i < firedBarrels.Count; i++)
			{
				tech.Weapons.QueueIncomingProjectileLaunch(firedBarrels[i]);
			}
		}
	}

	private void OnServerControlsUpdated(NetworkMessage msg)
	{
		msg.ReadMessage(s_ReusableUpdateTechControlsMessage);
		tech.control.CurState = s_ReusableUpdateTechControlsMessage.m_State;
		SetDirtyBit(4u);
	}

	private void OnServerTargetUpdated(NetworkMessage msg)
	{
		UpdateTechTargetMessage updateTechTargetMessage = msg.ReadMessage<UpdateTechTargetMessage>();
		SetTargettedTechById(updateTechTargetMessage.m_TargetId, shouldSetNeedsTargetIfReqd: true);
		m_LastTargettedId = updateTechTargetMessage.m_TargetId;
		SetDirtyBit(8u);
	}

	private bool IsServerNpcTech()
	{
		if (base.isServer)
		{
			return NetIdentity.clientAuthorityOwner == null;
		}
		return false;
	}

	private void OnServerWeaponsUpdated(NetworkMessage msg)
	{
		msg.ReadMessage(s_ReusableUpdateTechWeaponsMessage);
		UpdateTechWeaponsMessage updateTechWeaponsMessage = s_ReusableUpdateTechWeaponsMessage;
		if ((!base.hasAuthority || NetPlayer == null) && !IsServerNpcTech() && updateTechWeaponsMessage.m_State.FiredBarrels != null)
		{
			SetFiredBarrels(updateTechWeaponsMessage.m_State.FiredBarrels);
		}
		m_WeaponState.Append(updateTechWeaponsMessage.m_State);
		if (m_WeaponState.FiredBarrels != null && m_WeaponState.FiredBarrels.Count > 0)
		{
			SetDirtyBit(128u);
		}
	}

	private void OnServerMissionPromptResponse(NetworkMessage netMsg)
	{
		MissionPromptResponseMessage missionPromptResponseMessage = netMsg.ReadMessage<MissionPromptResponseMessage>();
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(missionPromptResponseMessage.m_BlockPoolID);
		ModuleMissionPrompt moduleMissionPrompt = ((tankBlock != null) ? tankBlock.GetComponent<ModuleMissionPrompt>() : null);
		if (moduleMissionPrompt != null)
		{
			moduleMissionPrompt.HandlePromptResponse(missionPromptResponseMessage.m_Accepted);
		}
		else
		{
			d.LogError("OnServerMissionPromptResponse - ModuleMissionPrompt could not be found on the block in question. Something is wrong.");
		}
	}

	private void OnServerDebugSetInvulnerable(NetworkMessage netMsg)
	{
		tech.SetInvulnerable(invulnerable: true, forever: false);
	}

	public void OnBlockDetached(TankBlock block)
	{
		for (int num = m_NetworkedModules.Count - 1; num >= 0; num--)
		{
			if (m_NetworkedModules[num].GetBlock().blockPoolID == block.blockPoolID)
			{
				m_NetworkedModules.RemoveAt(num);
			}
		}
	}

	public void OnBlockAttached(TankBlock block)
	{
		m_NetworkedModules.AddRange(block.NetworkedModules);
	}

	private void OnClientRemoveAllBlocksMessage(NetworkMessage netMsg)
	{
		tech.blockman.OnClientRemoveAllBlocksMessage(netMsg);
	}

	private void OnClientTechBlockDamaged(NetworkMessage netMsg)
	{
		netMsg.ReadMessage(s_ReusableTechBlockDamagedMessage);
		TechBlockDamagedMessage techBlockDamagedMessage = s_ReusableTechBlockDamagedMessage;
		TankBlock blockWithID = tech.blockman.GetBlockWithID(techBlockDamagedMessage.m_DamageBlockPoolID);
		if ((bool)blockWithID)
		{
			blockWithID.damage.OnNetDamaged(techBlockDamagedMessage.m_DamageInfo, techBlockDamagedMessage.m_RemovedBlocks, Singleton.Manager<ManNetwork>.inst.IsServer);
		}
		if (NetPlayer != null)
		{
			NetPlayer.HasBeenDamagedByOpponent = true;
			UISelfDestructButton uISelfDestructButton = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.SelfDestruct) as UISelfDestructButton;
			if ((bool)uISelfDestructButton)
			{
				uISelfDestructButton.HasBeenDamaged(wasDamaged: true);
			}
		}
	}

	private void ForceBuildBeamState(bool enabled)
	{
		if (tech.beam.IsActive != enabled)
		{
			bool force = true;
			tech.beam.EnableBeam(enabled, force);
		}
	}

	private void SendTechControls()
	{
		if (tech.control.CurState != m_LastSentControls)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.UpdateTechControls, new UpdateTechControlsMessage
			{
				m_State = tech.control.CurState
			}, base.netId);
			m_LastSentControls = tech.control.CurState;
		}
	}

	private void SendCurrentTarget()
	{
		NetworkInstanceId idOfTargettedTech = GetIdOfTargettedTech();
		if (idOfTargettedTech != m_LastTargettedId)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.UpdateTechTarget, new UpdateTechTargetMessage
			{
				m_TargetId = idOfTargettedTech
			}, base.netId);
			m_LastTargettedId = idOfTargettedTech;
			m_NeedsTargetCheckAndSet = false;
			m_NeedsTargetCheckAndSetTimeout = 0f;
		}
	}

	private void SendTechWeapons()
	{
		if (tech.Weapons.HasQueuedProjectiles())
		{
			UpdateTechWeaponsMessage message = new UpdateTechWeaponsMessage
			{
				m_State = new TechWeapon.State
				{
					FiredBarrels = tech.Weapons.GetQueuedProjectiles()
				}
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.UpdateTechWeapons, message, base.netId);
			tech.Weapons.ClearQueuedProjectiles();
		}
	}

	public void SendAnchorState()
	{
		if (m_AnchorsDirty && !base.isServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.UpdateTechAnchor, new AnchorTechMessage(tech.Anchors), base.netId);
			m_AnchorsDirty = false;
		}
	}

	private void SendAIControls()
	{
		if (m_AI_LastTargetRadius != tech.control.TargetRadiusWorld || m_AI_LastTargetPosition != tech.control.TargetPositionWorld)
		{
			m_AI_LastTargetRadius = tech.control.TargetRadiusWorld;
			m_AI_LastTargetPosition = tech.control.TargetPositionWorld;
			SetDirtyBit(64u);
		}
	}

	private void UpdateNeedsTargetCheckAndSet()
	{
		d.Assert(m_NeedsTargetCheckAndSet);
		if (m_LastTargettedId.IsEmpty() || m_LastTargettedId == NetworkInstanceId.Invalid)
		{
			m_NeedsTargetCheckAndSet = false;
			m_NeedsTargetCheckAndSetTimeout = 0f;
		}
		else if (SetTargettedTechById(m_LastTargettedId, shouldSetNeedsTargetIfReqd: false))
		{
			m_NeedsTargetCheckAndSet = false;
			m_NeedsTargetCheckAndSetTimeout = 0f;
		}
		else if (Time.realtimeSinceStartup > m_NeedsTargetCheckAndSetTimeout)
		{
			m_NeedsTargetCheckAndSet = false;
			m_NeedsTargetCheckAndSetTimeout = 0f;
		}
	}

	private bool TryGetModuleFromBlockID<T>(uint blockID, out T moduleOut, bool errorIfNotFound = true) where T : Module
	{
		moduleOut = null;
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.FindTankBlock(blockID);
		d.AssertFormat(!errorIfNotFound || tankBlock.IsNotNull(), "NetTech: Tried getting block with ID: {0} - But could not find block anywhere!", blockID);
		if (tankBlock.IsNull())
		{
			return false;
		}
		moduleOut = tankBlock.GetComponent<T>();
		if (moduleOut == null)
		{
			d.LogError("NetTech: Tried to get module '" + typeof(T).Name + "' on block '" + base.gameObject.name + "' but no such module exists on that block!");
			return false;
		}
		return true;
	}

	private void OnPool()
	{
		tech = GetComponent<Tank>();
		NetIdentity = GetComponent<NetworkIdentity>();
		m_NetworkedTechComponents = GetComponents<INetworkedTechComponent>();
	}

	private void OnSpawn()
	{
		ClearAllDirtyBits();
		m_LastTargettedId = NetworkInstanceId.Invalid;
		m_NeedsTargetCheckAndSet = false;
		m_NeedsTargetCheckAndSetTimeout = 0f;
		Team = int.MaxValue;
		NetPlayer = null;
		m_ClientOwnerNetId = NetworkInstanceId.Invalid;
		m_HostID = 0;
		m_NeedsToDisableBuildBeam = false;
		m_Invulnerable = false;
		SpawnShieldCount = 0;
		m_InitialSpawnShieldID = 0u;
		m_HasClearedInventoryAfterRespawn = false;
		m_AnchorsDirty = false;
		LastDamager = null;
		m_LastSentControls = default(TankControl.State);
		m_RebuildTimer = 0f;
		IsSetToSelfDestruct = false;
		m_TechData = null;
		m_OriginalSaveData = null;
		m_TechDataBlockPoolIDs = null;
		m_NetworkedModules.Clear();
		m_WeaponState = default(TechWeapon.State);
	}

	private void OnRecycle()
	{
		if (m_ShieldGeom != null)
		{
			m_ShieldGeom.Recycle();
			m_ShieldGeom = null;
		}
		m_ObserversInitialised = false;
		m_IsMaterialOverride = false;
		m_MaterialOverrideType = ManTechMaterialSwap.MatType.Default;
	}

	private void Update()
	{
		if (m_QueuedSaveTechData)
		{
			m_QueuedSaveTechData = false;
			SaveTechData();
		}
		if (base.hasAuthority)
		{
			if (ForceBuildBeamEnabled)
			{
				if (!tech.IsAnchored)
				{
					ForceBuildBeamState(enabled: true);
				}
				m_NeedsToDisableBuildBeam = true;
			}
			else if (m_NeedsToDisableBuildBeam)
			{
				if (!tech.IsAnchored)
				{
					ForceBuildBeamState(enabled: false);
				}
				m_NeedsToDisableBuildBeam = false;
			}
			SendTechControls();
			SendCurrentTarget();
			SendTechWeapons();
			SendAnchorState();
		}
		else if (IsServerNpcTech())
		{
			SendTechControls();
			SendCurrentTarget();
			SendTechWeapons();
			SendAnchorState();
		}
		if (m_RebuildTimer > 0f)
		{
			if (m_RebuildTimer < 1000f)
			{
				m_RebuildTimer = Mathf.Max(m_RebuildTimer - Time.deltaTime, 0f);
				SetDirtyBit(32u);
			}
			if (base.isServer)
			{
				if (!tech.beam.IsActive)
				{
					m_RebuildTimer = 0f;
					SetDirtyBit(32u);
				}
				if (InitialSpawnShieldID == 0)
				{
					m_RebuildTimer = 0f;
					SetDirtyBit(32u);
				}
			}
		}
		if (base.isServer)
		{
			if (!base.hasAuthority && NetPlayer == null)
			{
				SendAIControls();
			}
			if (Singleton.Manager<ManNetwork>.inst.ServerSpawnBank != null && InitialSpawnShieldID != 0 && !Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.IsShieldActive(InitialSpawnShieldID))
			{
				InitialSpawnShieldID = 0u;
			}
			bool flag = ForceInvulnerable || (SpawnShieldCount > 0 && InitialSpawnShieldID != 0) || (tech.beam.IsActive && Singleton.Manager<ManNetwork>.inst.NetController.BuildBeamProtects) || m_RebuildTimer > 0f;
			if (m_Invulnerable != flag)
			{
				m_Invulnerable = flag;
				SetDirtyBit(16u);
			}
		}
		if (m_Invulnerable)
		{
			bool invulnerable = true;
			bool forever = false;
			tech.SetInvulnerable(invulnerable, forever);
		}
		bool flag2 = m_Invulnerable && InitialSpawnShieldID == 0;
		if (flag2 && m_ShieldGeom == null && m_ShieldGeomPrefab != null)
		{
			m_ShieldGeom = m_ShieldGeomPrefab.Spawn();
			m_ShieldGeom.parent = base.transform;
		}
		if (m_ShieldGeom != null)
		{
			if (flag2)
			{
				Bounds blockBounds = tech.blockBounds;
				m_ShieldGeom.localPosition = blockBounds.center;
				float num = EncompassingRadius * 2f;
				m_ShieldGeom.localScale = new Vector3(num, num, num);
			}
			if (m_ShieldGeom.gameObject.activeSelf != flag2)
			{
				m_ShieldGeom.gameObject.SetActive(flag2);
			}
		}
		if (base.isServer && !m_HasClearedInventoryAfterRespawn && Singleton.Manager<ManNetwork>.inst.ClearUnusedInventoryAfterSpawnBubbleEnabled && !m_Invulnerable && InitialSpawnShieldID != 0)
		{
			m_HasClearedInventoryAfterRespawn = true;
			NetPlayer.Inventory.Clear();
		}
		if (m_NeedsTargetCheckAndSet)
		{
			UpdateNeedsTargetCheckAndSet();
		}
	}

	public void SetToSelfDestruct(bool on, float fuseDuration)
	{
		d.LogFormat("NetTech - SetToSelfDestruct for Tech={0} NetId={1} State={2}", base.name, base.netId, on.ToString());
		if (on)
		{
			IsSetToSelfDestruct = true;
			if (SelfDestructController != null)
			{
				SelfDestructController.block.damage.SelfDestruct(fuseDuration);
				return;
			}
			d.LogErrorFormat("NetTech.SetToSelfDestruct - Tech with no control block! Cannot self destruct : {0}", base.name);
		}
		else if (IsSetToSelfDestruct)
		{
			IsSetToSelfDestruct = false;
			if (SelfDestructController != null)
			{
				SelfDestructController.block.damage.AbortSelfDestruct();
			}
		}
	}

	public void RequestCycleTeam(bool overrideHostRestrictions = false)
	{
		if (!Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp())
		{
			return;
		}
		int num = Team;
		if (num == -2)
		{
			return;
		}
		if (ManSpawn.IsPlayerTeam(num))
		{
			if (ManNetwork.IsHost || overrideHostRestrictions)
			{
				num++;
				if (num >= 1073741829)
				{
					num = -1;
				}
			}
			else if (num == Singleton.Manager<ManNetwork>.inst.MyPlayer.TechTeamID)
			{
				num = -1;
			}
			RequestChangeTeam(num);
		}
		else if (num != int.MaxValue)
		{
			RequestChangeTeam(ManNetwork.IsHost ? 1073741824 : Singleton.Manager<ManNetwork>.inst.MyPlayer.TechTeamID);
		}
	}

	public void RequestChangeTeam(int techTeamID)
	{
		d.Assert(NetPlayer == null);
		if (NetPlayer == null && techTeamID != 0)
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
		netMsg.GetSender();
		d.Assert(Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp(), "Should players be able to request team changes on techs in other gamemodes?");
		if (NetPlayer == null)
		{
			RequestTeamChangeMessage requestTeamChangeMessage = netMsg.ReadMessage<RequestTeamChangeMessage>();
			if (requestTeamChangeMessage.m_TechTeamID == -1 || ManSpawn.IsPlayerTeam(requestTeamChangeMessage.m_TechTeamID))
			{
				int team = Singleton.Manager<ManSpawn>.inst.GenerateAutomaticTeamID(requestTeamChangeMessage.m_TechTeamID);
				bool flag = ManSpawn.IsPlayerTeam(team);
				OnServerSetTeam(team, !flag);
				if (flag)
				{
					tech.AI.SetBehaviorType(AITreeType.AITypes.Idle);
				}
				else
				{
					tech.AI.SetOldBehaviour();
				}
			}
		}
		else
		{
			d.LogError("Player " + netMsg.GetSender().name + " sent a request to change team of tech " + base.name + " with a player in it.");
		}
	}

	public bool CanPlayerModify(NetPlayer player, bool controllerBlock = false)
	{
		if (player.IsNull())
		{
			return false;
		}
		if ((object)NetPlayer == player)
		{
			d.Assert(Team == player.TechTeamID, $"NetTech.CanPlayerModify: mismatched team IDs {tech.name}: netTeam={Team}, playerTeam={player.TechTeamID}");
			return true;
		}
		if (Team != player.TechTeamID)
		{
			return false;
		}
		if (NetPlayer.IsNull())
		{
			return true;
		}
		if (!Singleton.Manager<ManNetwork>.inst.CoOpAllowPlayerTechMods || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
		{
			return false;
		}
		if (controllerBlock)
		{
			return false;
		}
		return true;
	}

	private void UNetVersion()
	{
	}
}
