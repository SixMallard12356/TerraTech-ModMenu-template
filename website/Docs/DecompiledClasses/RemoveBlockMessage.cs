using UnityEngine.Networking;

public class RemoveBlockMessage : MessageBase
{
	public uint m_BlockPoolID;

	public DespawnReason m_Reason;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write((int)m_Reason);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_Reason = (DespawnReason)reader.ReadInt32();
	}
}
