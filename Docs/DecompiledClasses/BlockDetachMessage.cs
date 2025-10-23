using UnityEngine.Networking;

public class BlockDetachMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public uint m_blockPoolID;

	public bool m_AllowHeadlessTech;

	public int m_RemovalSeed;

	public bool m_ManuallyRemoved;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechNetId);
		writer.WritePackedUInt32(m_blockPoolID);
		writer.Write(m_AllowHeadlessTech);
		writer.WritePackedUInt32((uint)m_RemovalSeed);
		writer.Write(m_ManuallyRemoved);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechNetId = reader.ReadNetworkId();
		m_blockPoolID = reader.ReadPackedUInt32();
		m_AllowHeadlessTech = reader.ReadBoolean();
		m_RemovalSeed = (int)reader.ReadPackedUInt32();
		m_ManuallyRemoved = reader.ReadBoolean();
	}
}
