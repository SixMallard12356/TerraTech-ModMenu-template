using UnityEngine.Networking;

public class BlockAttachFailMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public uint m_BlockPoolID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechNetId);
		writer.WritePackedUInt32(m_BlockPoolID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechNetId = reader.ReadNetworkId();
		m_BlockPoolID = reader.ReadPackedUInt32();
	}
}
