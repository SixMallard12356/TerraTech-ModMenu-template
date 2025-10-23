using Unity;
using UnityEngine.Networking;

public class BlockAttachedMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public IntVector3 m_BlockPosition;

	public int m_BlockOrthoRotation;

	public uint m_BlockPoolID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechNetId);
		GeneratedNetworkCode._WriteIntVector3_None(writer, m_BlockPosition);
		writer.WritePackedUInt32((uint)m_BlockOrthoRotation);
		writer.WritePackedUInt32(m_BlockPoolID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechNetId = reader.ReadNetworkId();
		m_BlockPosition = GeneratedNetworkCode._ReadIntVector3_None(reader);
		m_BlockOrthoRotation = (int)reader.ReadPackedUInt32();
		m_BlockPoolID = reader.ReadPackedUInt32();
	}
}
