using UnityEngine.Networking;

public class BlockExplodedMessage : MessageBase
{
	public uint m_BlockPoolID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
	}
}
