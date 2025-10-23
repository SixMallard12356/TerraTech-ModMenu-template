#define UNITY_EDITOR
using System;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetBlock : NetBlockChunk, ManNetwork.IDumpableBehaviour
{
	private const float kScavengeDisabledDuration = 1f;

	private const float kScavengeOriginalOwnerDuration = 2f;

	private BlockTypes m_BlockType;

	private NetworkInstanceId m_OwnerDetachedFromID;

	private bool m_IsOwnerLoadoutBlock;

	private byte m_SkinID;

	private uint m_InitialSpawnShieldID;

	private float m_ScavengeTimer;

	private bool m_RemovalRequested;

	private const uint kSer_OwnerDetachedFromID_F = 16u;

	private const uint kSer_InitialSpawnShieldID_F = 32u;

	private const uint kSer_ScavengeTimer_F = 64u;

	private const uint kSer_OwnerLoadoutBlock_F = 128u;

	private const uint kSer_BlockType_F = 256u;

	private const uint kSer_SkinIndex_F = 512u;

	private const uint kSer_Energy_F = 1024u;

	private const uint kSer_AllFlagMask = 2047u;

	private static BlockDamagedMessage s_ReusableBlockDamagedMessage = new BlockDamagedMessage();

	public override Rigidbody rbody => block?.rbody;

	public override Visible visible => block?.visible;

	public override bool IsAcceptingUpdates => !m_RemovalRequested;

	public TankBlock block { get; private set; }

	public BlockTypes BlockType => m_BlockType;

	public byte SkinID
	{
		get
		{
			return m_SkinID;
		}
		set
		{
			m_SkinID = value;
		}
	}

	public NetworkInstanceId OwnerId => m_OwnerDetachedFromID;

	public bool IsOwnerLoadoutBlock => m_IsOwnerLoadoutBlock;

	public uint InitialSpawnShieldID
	{
		get
		{
			return m_InitialSpawnShieldID;
		}
		set
		{
			m_InitialSpawnShieldID = value;
			SetDirtyBit(32u);
		}
	}

	public override string GetDebugTypeName()
	{
		return "NetBlock:" + m_BlockType;
	}

	public void Dump(StringBuilder builder)
	{
		builder.AppendFormat("type={0} poolId={1}\n", m_BlockType, m_BlockPoolID);
		builder.AppendFormat("ownerId={0} removeRequested={1}\n", OwnerId, m_RemovalRequested);
		builder.AppendFormat("isHeld={0} authorityReason={1}\n", m_IsHeld, m_AuthorityReason);
		builder.AppendFormat("scenePos={0}\n", visible ? visible.trans.position.ToString() : "[null visible]");
	}

	public void ConnectTo(TankBlock to)
	{
		if (to == null)
		{
			d.LogError("Trying to connect to a null block. BlockPoolID was " + m_BlockPoolID + ", Block type was " + m_BlockType.ToString());
			return;
		}
		if (to.blockPoolID != m_BlockPoolID)
		{
			d.LogError("Trying to connect NetBlock to TankBlock with incorrect ID. Expected: " + m_BlockPoolID + ", Block had: " + to.blockPoolID);
			return;
		}
		block = to;
		block.netBlock = this;
		m_RemovalRequested = false;
		if (base.NetIdentity.HasEffectiveAuthority())
		{
			UpdateFromConnectedBlock();
		}
	}

	public void ClientPresumptiveDisconnect()
	{
		m_RemovalRequested = true;
		Disconnect();
	}

	public void Disconnect()
	{
		if (block != null)
		{
			block.visible.EnablePhysics(enable: true);
			block.netBlock = null;
			block = null;
		}
		else
		{
			d.LogWarning("Tried to disconnect NetBlock that was not connected");
		}
		m_AboutToBeDestroyed = true;
	}

	private void UpdateFromConnectedBlock()
	{
		if (!block.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? 2047u : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		OnSerializeCommon(writer, num);
		if ((num & 0x200) != 0)
		{
			writer.Write(m_SkinID);
		}
		if ((num & 0x100) != 0)
		{
			writer.Write((int)m_BlockType);
		}
		if ((num & 0x10) != 0)
		{
			writer.Write(m_OwnerDetachedFromID);
		}
		if ((num & 0x20) != 0)
		{
			writer.WritePackedUInt32(m_InitialSpawnShieldID);
		}
		if ((num & 0x40) != 0)
		{
			writer.Write(m_ScavengeTimer);
		}
		if ((num & 0x80) != 0)
		{
			writer.Write(m_IsOwnerLoadoutBlock);
		}
		OnSerializeHeld(writer, num);
		if (initialState)
		{
			d.Assert(block.IsNotNull());
			writer.Write(block.NetworkedModules.Length);
			NetworkWriter networkWriter = new NetworkWriter();
			INetworkedModule[] networkedModules = block.NetworkedModules;
			foreach (INetworkedModule networkedModule in networkedModules)
			{
				writer.Write((short)networkedModule.GetModuleID());
				networkWriter.SeekZero();
				networkedModule.OnSerialize(networkWriter);
				writer.WriteBytesFull(networkWriter.ToArray());
			}
		}
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		OnDeserializeCommon(reader, num);
		if ((num & 0x200) != 0)
		{
			m_SkinID = reader.ReadByte();
		}
		if ((num & 0x100) != 0)
		{
			m_BlockType = (BlockTypes)reader.ReadInt32();
		}
		if (block.IsNull() && !m_AboutToBeDestroyed)
		{
			Singleton.Manager<ManLooseBlocks>.inst.OnClientBlockDeserialize(this);
		}
		if ((num & 0x200) != 0 && block.IsNotNull())
		{
			block.SetSkinByUniqueID(m_SkinID);
		}
		if ((num & 0x10) != 0)
		{
			m_OwnerDetachedFromID = reader.ReadNetworkId();
		}
		if ((num & 0x20) != 0)
		{
			m_InitialSpawnShieldID = reader.ReadPackedUInt32();
		}
		if ((num & 0x40) != 0)
		{
			m_ScavengeTimer = reader.ReadSingle();
		}
		if ((num & 0x80) != 0)
		{
			m_IsOwnerLoadoutBlock = reader.ReadBoolean();
		}
		if (m_AboutToBeDestroyed)
		{
			return;
		}
		OnDeserializeHeld(reader, num, initialState);
		if (!initialState)
		{
			return;
		}
		d.Assert(block.IsNotNull());
		int num2 = reader.ReadInt32();
		for (int i = 0; i < num2; i++)
		{
			NetworkedModuleID moduleID = (NetworkedModuleID)reader.ReadInt16();
			NetworkReader reader2 = new NetworkReader(reader.ReadBytesAndSize());
			Array.Find(block.NetworkedModules, (INetworkedModule m) => m.GetModuleID() == moduleID).OnDeserialize(reader2);
		}
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.BlockRequestUndoAuthority, OnServerHandleUndoAuthorityRequest);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.BlockDamaged, OnServerBlockDamaged);
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.ClientBlockDamaged, OnClientBlockDamaged);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(base.netId, TTMsgType.BlockPlayRewardSpawnFx, OnClientPlayRewardSpawnFx);
	}

	public override void OnNetworkDestroy()
	{
		base.OnNetworkDestroy();
		if (block != null && block.tank != null)
		{
			Disconnect();
		}
		if (block != null)
		{
			TankBlock tankBlock = block;
			Disconnect();
			tankBlock.trans.Recycle();
		}
	}

	public void SetScavengeTimer()
	{
		m_ScavengeTimer = 3f;
		SetDirtyBit(64u);
	}

	[Server]
	public void OnServerSetSkinIndex(byte skinIndex)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetBlock::OnServerSetSkinIndex(System.Byte)' called on client");
			return;
		}
		m_SkinID = Singleton.Manager<ManCustomSkins>.inst.SkinIndexToID(skinIndex, Singleton.Manager<ManSpawn>.inst.GetCorporation(BlockType));
		SetDirtyBit(512u);
	}

	[Server]
	public void OnServerSetSkinID(byte skinID)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetBlock::OnServerSetSkinID(System.Byte)' called on client");
			return;
		}
		m_SkinID = skinID;
		SetDirtyBit(512u);
	}

	[Server]
	public void OnServerSetBlockType(BlockTypes type)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetBlock::OnServerSetBlockType(BlockTypes)' called on client");
			return;
		}
		m_BlockType = type;
		SetDirtyBit(256u);
	}

	[Server]
	public void OnServerSetDetachInfo(uint blockPoolID, NetworkInstanceId ownerDetachedFromID, bool isOwnerLoadoutBlock)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetBlock::OnServerSetDetachInfo(System.UInt32,UnityEngine.Networking.NetworkInstanceId,System.Boolean)' called on client");
			return;
		}
		m_BlockPoolID = blockPoolID;
		SetDirtyBit(2u);
		m_OwnerDetachedFromID = ownerDetachedFromID;
		SetDirtyBit(16u);
		m_IsOwnerLoadoutBlock = isOwnerLoadoutBlock;
		SetDirtyBit(128u);
	}

	private void OnServerHandleUndoAuthorityRequest(NetworkMessage msg)
	{
		bool flag = true;
		if (base.NetIdentity.clientAuthorityOwner != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.GetAuthorityReason(base.NetIdentity.netId) > ManNetwork.AuthorityReason.Collision)
			{
				flag = false;
			}
			else
			{
				RemoveClientAuthority();
			}
		}
		if (flag)
		{
			AssignClientAuthority(ManNetwork.AuthorityReason.UndoActive, msg.conn);
			return;
		}
		BlockRequestUndoAuthorityFailed message = new BlockRequestUndoAuthorityFailed
		{
			m_NetId = base.netId
		};
		NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerByConnection(msg.conn);
		Singleton.Manager<ManNetwork>.inst.SendToClient(msg.conn.connectionId, TTMsgType.BlockRequestUndoAuthorityFailed, message, netPlayer.netId);
	}

	private void OnServerBlockDamaged(NetworkMessage netMsg)
	{
		netMsg.ReadMessage(s_ReusableBlockDamagedMessage);
		BlockDamagedMessage message = s_ReusableBlockDamagedMessage;
		Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.ClientBlockDamaged, message, base.NetIdentity.netId);
	}

	private void OnClientBlockDamaged(NetworkMessage netMsg)
	{
		netMsg.ReadMessage(s_ReusableBlockDamagedMessage);
		BlockDamagedMessage blockDamagedMessage = s_ReusableBlockDamagedMessage;
		if (block != null)
		{
			if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.netId != blockDamagedMessage.m_PlayerNetId)
			{
				block.damage.OnNetDamaged(blockDamagedMessage.m_DamageInfo, detatched: false, Singleton.Manager<ManNetwork>.inst.MyPlayer.isServer);
			}
		}
		else
		{
			d.LogError("OnClientBlockDamaged - NetBlock is missing a TankBlock. Did we just receive a damage message after a destruct message?");
		}
	}

	private void OnClientPlayRewardSpawnFx(NetworkMessage netMsg)
	{
		Singleton.Manager<ManSpawn>.inst.RewardSpawner.CueBlockSpawnFx(block.trans.position);
	}

	private void OnPool()
	{
	}

	private void OnSpawn()
	{
		m_OwnerDetachedFromID = NetworkInstanceId.Invalid;
		m_IsOwnerLoadoutBlock = false;
		m_InitialSpawnShieldID = 0u;
		m_SkinID = 0;
		m_RemovalRequested = false;
	}

	public bool CanPlayerScavenge(NetworkInstanceId playerNetId)
	{
		if (block.BlockType == BlockTypes.SPE_Crown_111)
		{
			return false;
		}
		if (block.visible.holderStack != null)
		{
			return false;
		}
		if (block.visible.IsLocked(Visible.LockTimerTypes.StackAccept))
		{
			return false;
		}
		if (m_ScavengeTimer <= 0f)
		{
			return true;
		}
		if (m_ScavengeTimer <= 1f)
		{
			return playerNetId == m_OwnerDetachedFromID;
		}
		return false;
	}

	private void Update()
	{
		if (base.isServer && m_ScavengeTimer > 0f)
		{
			m_ScavengeTimer -= Time.deltaTime;
			SetDirtyBit(64u);
		}
	}

	private void UNetVersion()
	{
	}
}
