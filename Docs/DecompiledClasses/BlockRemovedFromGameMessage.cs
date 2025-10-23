using UnityEngine.Networking;

public class BlockRemovedFromGameMessage : MessageBase
{
	public uint m_BlockPoolID;

	public bool m_RequestScavenge;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write(m_RequestScavenge);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_RequestScavenge = reader.ReadBoolean();
	}
}
