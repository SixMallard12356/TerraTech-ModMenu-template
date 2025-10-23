using UnityEngine.Networking;

public class ItemFilterSelectionMessage : MessageBase, IBlockMessage
{
	public uint m_BlockPoolID;

	public ModuleItemFilter.AcceptMode m_FilterAcceptMode;

	public int m_ChunkItemType;

	uint IBlockMessage.BlockPoolID
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

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write((int)m_FilterAcceptMode);
		writer.WritePackedUInt32((uint)m_ChunkItemType);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_FilterAcceptMode = (ModuleItemFilter.AcceptMode)reader.ReadInt32();
		m_ChunkItemType = (int)reader.ReadPackedUInt32();
	}
}
