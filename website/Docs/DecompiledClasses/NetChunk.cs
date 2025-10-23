#define UNITY_EDITOR
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetChunk : NetBlockChunk, ManNetwork.IDumpableBehaviour
{
	private ChunkTypes m_ChunkType;

	private const uint kSer_ChunkType_F = 16u;

	private const uint kSer_AllFlagMask = 31u;

	public ResourcePickup chunk { get; private set; }

	public ChunkTypes ChunkType => m_ChunkType;

	public override Rigidbody rbody => chunk?.rbody;

	public override Visible visible => chunk?.visible;

	public override bool IsAcceptingUpdates => true;

	public override string GetDebugTypeName()
	{
		return "NetChunk:" + m_ChunkType;
	}

	public void Dump(StringBuilder builder)
	{
		builder.AppendFormat("type={0} poolId={1}\n", m_ChunkType, m_BlockPoolID);
		builder.AppendFormat("isHeld={0} authorityReason={1}\n", m_IsHeld, m_AuthorityReason);
		builder.AppendFormat("scenePos={0}\n", visible ? visible.trans.position.ToString() : "[null visible]");
	}

	public void ConnectTo(ResourcePickup to)
	{
		if (to == null)
		{
			d.LogError("Trying to connect to a null chunk. BlockPoolID was " + m_BlockPoolID + ", Block type was " + m_ChunkType.ToString());
		}
		else if (to.blockPoolID != m_BlockPoolID)
		{
			d.LogError("Trying to connect NetChunk to TankBlock with incorrect ID. Expected: " + m_BlockPoolID + ", Block had: " + to.blockPoolID);
		}
		else
		{
			chunk = to;
			chunk.netChunk = this;
			if (base.NetIdentity.HasEffectiveAuthority())
			{
				UpdateFromConnectedBlock();
			}
		}
	}

	public void Disconnect()
	{
		if (chunk != null)
		{
			chunk.visible.EnablePhysics(enable: true);
			chunk.netChunk = null;
			chunk = null;
		}
		else
		{
			d.LogWarning("Tried to disconnect NetChunk that was not connected");
		}
		m_AboutToBeDestroyed = true;
	}

	private void UpdateFromConnectedBlock()
	{
		if (!chunk.gameObject.activeInHierarchy)
		{
			base.gameObject.SetActive(value: false);
		}
	}

	public override bool OnSerialize(NetworkWriter writer, bool initialState)
	{
		uint num = (initialState ? 31u : base.syncVarDirtyBits);
		writer.WritePackedUInt32(num);
		OnSerializeCommon(writer, num);
		if ((num & 0x10) != 0)
		{
			writer.WritePackedInt32((int)m_ChunkType);
		}
		OnSerializeHeld(writer, num);
		return num != 0;
	}

	public override void OnDeserialize(NetworkReader reader, bool initialState)
	{
		uint num = reader.ReadPackedUInt32();
		OnDeserializeCommon(reader, num);
		if (!m_AboutToBeDestroyed)
		{
			if ((num & 0x10) != 0)
			{
				m_ChunkType = (ChunkTypes)reader.ReadPackedInt32();
			}
			if (chunk.IsNull())
			{
				Singleton.Manager<ManLooseBlocks>.inst.OnClientChunkDeserialize(this);
			}
			OnDeserializeHeld(reader, num, initialState);
		}
	}

	public override void OnStartServer()
	{
		base.OnStartServer();
	}

	public override void OnStartClient()
	{
		base.OnStartClient();
	}

	public override void OnNetworkDestroy()
	{
		base.OnNetworkDestroy();
		if (chunk != null)
		{
			ResourcePickup resourcePickup = chunk;
			Disconnect();
			resourcePickup.trans.Recycle();
		}
	}

	[Server]
	public void OnServerSetChunkType(ChunkTypes type)
	{
		if (!NetworkServer.active)
		{
			Debug.LogWarning("[Server] function 'System.Void NetChunk::OnServerSetChunkType(ChunkTypes)' called on client");
			return;
		}
		m_ChunkType = type;
		SetDirtyBit(16u);
	}

	private void UNetVersion()
	{
	}
}
