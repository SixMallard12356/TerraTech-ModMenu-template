using UnityEngine.Networking;

public class AddGhostBlockMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public uint m_BlockPoolID;

	public BlockTypes m_BlockType;

	public IntVector3 m_BlockPosition;

	public int m_BlockOrthoRotation;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechNetId.Value);
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.WritePackedInt32((int)m_BlockType);
		writer.WritePackedInt32(m_BlockPosition.x);
		writer.WritePackedInt32(m_BlockPosition.y);
		writer.WritePackedInt32(m_BlockPosition.z);
		writer.Write((byte)m_BlockOrthoRotation);
	}

	public override void Deserialize(NetworkReader reader)
	{
		uint value = reader.ReadUInt32();
		m_TechNetId = new NetworkInstanceId(value);
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_BlockType = (BlockTypes)reader.ReadPackedInt32();
		m_BlockPosition.x = reader.ReadPackedInt32();
		m_BlockPosition.y = reader.ReadPackedInt32();
		m_BlockPosition.z = reader.ReadPackedInt32();
		m_BlockOrthoRotation = reader.ReadByte();
	}
}
