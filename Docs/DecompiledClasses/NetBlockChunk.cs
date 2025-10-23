#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class NetBlockChunk : NetworkBehaviour
{
	protected bool m_IsHeld;

	protected uint m_BlockPoolID = uint.MaxValue;

	protected ManNetwork.AuthorityReason m_AuthorityReason;

	protected bool m_ObserversInitialised;

	protected bool m_AboutToBeDestroyed;

	private NetworkInstanceId m_HolderTechId;

	private uint m_HolderBlockPoolID;

	private int m_HolderStackIndex;

	private int m_HolderStackPosition;

	protected const uint kSer_AuthorityReason_F = 1u;

	protected const uint kSer_BlockPoolId_F = 2u;

	protected const uint kSer_Held_F = 4u;

	protected const uint kSer_HoldingStack_F = 8u;

	protected const uint kSerNextUnusedFlag = 16u;

	public abstract Rigidbody rbody { get; }

	public abstract Visible visible { get; }

	public abstract bool IsAcceptingUpdates { get; }

	public NetworkIdentity NetIdentity { get; private set; }

	public uint BlockPoolID
	{
		get
		{
			return m_BlockPoolID;
		}
		set
		{
			m_BlockPoolID = value;
		}
	}

	public ManNetwork.AuthorityReason GetAuthorityReason => m_AuthorityReason;

	public bool ObserversInitialised => m_ObserversInitialised;

	public NetworkInstanceId HoldingTechNetworkID => m_HolderTechId;

	public bool IsInHolder => m_HolderTechId != NetworkInstanceId.Invalid;

	public bool HasValidBlockPoolID()
	{
		return TankBlock.IsBlockPoolIDValid(m_BlockPoolID);
	}

	public abstract string GetDebugTypeName();

	[Server]
	public void OnServerSetAuthorityReason(ManNetwork.AuthorityReason authorityReason)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetBlockChunk::OnServerSetAuthorityReason(ManNetwork/AuthorityReason)' called on client");
			return;
		}
		m_AuthorityReason = authorityReason;
		SetDirtyBit(1u);
	}

	protected void OnSerializeCommon(NetworkWriter writer, uint myDirtyBits)
	{
		if ((myDirtyBits & 2) != 0)
		{
			writer.Write(m_BlockPoolID);
		}
		if ((myDirtyBits & 1) != 0)
		{
			writer.WritePackedInt32((int)m_AuthorityReason);
		}
	}

	protected void OnSerializeHeld(NetworkWriter writer, uint myDirtyBits)
	{
		if ((myDirtyBits & 4) != 0)
		{
			writer.Write(m_IsHeld);
		}
		if ((myDirtyBits & 8) != 0)
		{
			bool flag = m_HolderTechId != NetworkInstanceId.Invalid;
			writer.Write(flag);
			if (flag)
			{
				writer.Write(m_HolderTechId);
				writer.Write(m_HolderBlockPoolID);
				writer.WritePackedInt32(m_HolderStackIndex);
				writer.WritePackedInt32(m_HolderStackPosition);
			}
		}
	}

	protected void OnDeserializeCommon(NetworkReader reader, uint myDirtyBits)
	{
		if ((myDirtyBits & 2) != 0)
		{
			m_BlockPoolID = reader.ReadUInt32();
		}
		if ((myDirtyBits & 1) != 0)
		{
			m_AuthorityReason = (ManNetwork.AuthorityReason)reader.ReadPackedInt32();
		}
	}

	protected void OnDeserializeHeld(NetworkReader reader, uint myDirtyBits, bool initialState)
	{
		if ((myDirtyBits & 4) != 0)
		{
			m_IsHeld = reader.ReadBoolean();
			d.AssertFormat(visible != null, "Deserializing {0}. Our Visible is still null", GetDebugTypeName());
			if (visible != null)
			{
				visible.EnablePhysics(!m_IsHeld);
			}
		}
		if ((myDirtyBits & 8) != 0)
		{
			d.AssertFormat(visible != null, "Deserializing {0}. Our Visible is still null", GetDebugTypeName());
			if (reader.ReadBoolean())
			{
				m_HolderTechId = reader.ReadNetworkId();
				m_HolderBlockPoolID = reader.ReadUInt32();
				m_HolderStackIndex = reader.ReadPackedInt32();
				m_HolderStackPosition = reader.ReadPackedInt32();
			}
			else
			{
				m_HolderTechId = NetworkInstanceId.Invalid;
				m_HolderBlockPoolID = uint.MaxValue;
			}
			if (!initialState)
			{
				ApplyHolderState();
			}
		}
	}

	private void ApplyHolderState()
	{
		if (m_HolderTechId == NetworkInstanceId.Invalid)
		{
			if (visible.holderStack != null)
			{
				visible.SetHolder(null, notifyRelease: true, isBeingRecycled: false, netSend: false);
			}
			return;
		}
		GameObject gameObject = ClientScene.FindLocalObject(m_HolderTechId);
		Tank tank = ((gameObject != null) ? gameObject.GetComponent<Tank>() : null);
		if (tank != null && tank.blockman.blockCount > 0)
		{
			ApplyHolderState(tank);
		}
		else
		{
			Singleton.Manager<ManLooseBlocks>.inst.AddPendingStackItemForTech(this);
		}
	}

	public void ApplyHolderState(Tank tech)
	{
		if (!(tech.netTech.netId == m_HolderTechId))
		{
			return;
		}
		TankBlock blockWithID = tech.blockman.GetBlockWithID(m_HolderBlockPoolID);
		if (!(blockWithID != null))
		{
			return;
		}
		ModuleItemHolder component = blockWithID.GetComponent<ModuleItemHolder>();
		if (component != null && component.GetStack(m_HolderStackIndex) != null)
		{
			ModuleItemHolder.Stack stack = component.GetStack(m_HolderStackIndex);
			if (stack != visible.holderStack && stack != null)
			{
				_ = (bool)stack.Take(visible, force: true, m_HolderStackPosition, fromNetworkData: true);
			}
		}
	}

	public void ReleaseFromHolder()
	{
		if (NetIdentity.HasEffectiveAuthority())
		{
			SetHolderBlock(null, 0, 0);
			return;
		}
		SetVisibleReleasedMessage message = new SetVisibleReleasedMessage();
		Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.RequestVisibleReleased, message, base.netId);
	}

	public void SetHolderBlock(TankBlock holderBlock, int stackIndex, int stackPosition)
	{
		bool flag = false;
		if (holderBlock.IsNotNull() && holderBlock.tank.IsNotNull() && holderBlock.tank.netTech.IsNotNull())
		{
			m_HolderTechId = holderBlock.tank.netTech.netId;
			m_HolderBlockPoolID = holderBlock.blockPoolID;
			m_HolderStackIndex = stackIndex;
			m_HolderStackPosition = stackPosition;
			flag = true;
		}
		else if (m_HolderTechId != NetworkInstanceId.Invalid)
		{
			m_HolderTechId = NetworkInstanceId.Invalid;
			m_HolderBlockPoolID = uint.MaxValue;
			m_HolderStackIndex = 0;
			flag = true;
		}
		if (flag)
		{
			if (base.isServer)
			{
				SetDirtyBit(8u);
				return;
			}
			SetVisibleHeldMessage message = new SetVisibleHeldMessage
			{
				m_BlockPoolID = m_HolderBlockPoolID,
				m_StackIndex = m_HolderStackIndex,
				m_TechNetId = m_HolderTechId,
				m_StackPosition = m_HolderStackPosition
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.SetVisibleHeld, message, base.netId);
		}
	}

	[Server]
	public void OnServerSetHeld(bool held)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetBlockChunk::OnServerSetHeld(System.Boolean)' called on client");
			return;
		}
		m_IsHeld = held;
		SetDirtyBit(4u);
		if ((bool)visible)
		{
			visible.EnablePhysics(!held);
		}
	}

	public override void OnStartAuthority()
	{
		base.OnStartAuthority();
		Singleton.Manager<ManNetwork>.inst.OnBlockGainedAuthority(this);
	}

	public override void OnStopAuthority()
	{
		base.OnStopAuthority();
	}

	public void AssignClientAuthority(ManNetwork.AuthorityReason reason, NetworkConnection connectionToClient)
	{
		bool num = NetIdentity.AssignClientAuthority(connectionToClient);
		Singleton.Manager<ManNetwork>.inst.SetAuthorityReason(NetIdentity.netId, reason);
		OnServerSetAuthorityReason(reason);
		if (num)
		{
			d.Log(string.Concat(GetDebugTypeName(), ": NetId=", base.netId, " Name:", base.name, " NewClientAuthority:", connectionToClient.connectionId, " Reason:", reason.ToString()));
		}
	}

	public void RemoveClientAuthority()
	{
		if (NetIdentity.clientAuthorityOwner != null)
		{
			NetIdentity.RemoveClientAuthority(NetIdentity.clientAuthorityOwner);
			d.Log(string.Concat("NetBlock: RemoveClientAuthority NetId=", base.netId, " Name=", base.gameObject.name));
		}
		OnServerSetAuthorityReason(ManNetwork.AuthorityReason.NoAuthority);
		Singleton.Manager<ManNetwork>.inst.ClearAuthorityReason(NetIdentity.netId);
	}

	public void ClientForciblyClearLocalAuthorityOnceBlockReleased()
	{
		if (m_AuthorityReason != ManNetwork.AuthorityReason.NoAuthority)
		{
			d.Assert(!Singleton.Manager<ManNetwork>.inst.IsServer);
			d.Assert(m_AuthorityReason == ManNetwork.AuthorityReason.HeldVisible);
			m_AuthorityReason = ManNetwork.AuthorityReason.NoAuthority;
		}
	}

	private void OnServerSetVisibleReleased(NetworkMessage netMsg)
	{
		netMsg.ReadMessage<SetVisibleReleasedMessage>();
		if (NetIdentity.HasEffectiveAuthority())
		{
			m_HolderTechId = NetworkInstanceId.Invalid;
			m_HolderBlockPoolID = uint.MaxValue;
			if ((bool)visible)
			{
				ApplyHolderState();
			}
		}
		SetDirtyBit(8u);
	}

	private void OnServerSetVisibleHeld(NetworkMessage netMsg)
	{
		SetVisibleHeldMessage setVisibleHeldMessage = netMsg.ReadMessage<SetVisibleHeldMessage>();
		m_HolderTechId = setVisibleHeldMessage.m_TechNetId;
		m_HolderBlockPoolID = setVisibleHeldMessage.m_BlockPoolID;
		m_HolderStackIndex = setVisibleHeldMessage.m_StackIndex;
		m_HolderStackPosition = setVisibleHeldMessage.m_StackPosition;
		SetDirtyBit(8u);
		if ((bool)visible)
		{
			ApplyHolderState();
		}
	}

	public override bool OnCheckObserver(NetworkConnection conn)
	{
		if (visible == null)
		{
			return false;
		}
		return Singleton.Manager<ManNetwork>.inst.CheckObserver(visible, conn);
	}

	public override bool OnRebuildObservers(HashSet<NetworkConnection> observers, bool initialize)
	{
		if (initialize)
		{
			m_ObserversInitialised = true;
		}
		if (visible == null)
		{
			return true;
		}
		return Singleton.Manager<ManNetwork>.inst.RebuildObservers(visible, observers);
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.RequestVisibleReleased, OnServerSetVisibleReleased);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(base.netId, TTMsgType.SetVisibleHeld, OnServerSetVisibleHeld);
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
		ApplyHolderState();
	}

	private void OnPool()
	{
		NetIdentity = GetComponent<NetworkIdentity>();
	}

	private void OnSpawn()
	{
		m_IsHeld = false;
		m_BlockPoolID = 4294967293u;
		m_AuthorityReason = ManNetwork.AuthorityReason.NoAuthority;
		m_HolderTechId = NetworkInstanceId.Invalid;
		m_HolderBlockPoolID = uint.MaxValue;
		m_AboutToBeDestroyed = false;
	}

	private void OnRecycle()
	{
		_ = visible != null;
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromMessages(base.netId);
		BlockPoolID = 4294967292u;
		m_ObserversInitialised = false;
	}

	private void UNetVersion()
	{
	}

	public override bool OnSerialize(NetworkWriter writer, bool forceAll)
	{
		bool result = default(bool);
		return result;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
	}
}
