using UnityEngine.Networking;

public class ReskinBlockMessage : MessageBase
{
	public uint m_BlockPoolID;

	public byte m_SkinID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.WritePackedUInt32(m_SkinID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_SkinID = (byte)reader.ReadPackedUInt32();
	}
}
