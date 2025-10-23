using UnityEngine.Networking;

public class ShowExplosiveBoltsFXMessage : MessageBase
{
	public uint m_BlockId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockId = reader.ReadPackedUInt32();
	}
}
