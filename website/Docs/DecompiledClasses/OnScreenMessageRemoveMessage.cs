using UnityEngine.Networking;

public class OnScreenMessageRemoveMessage : MessageBase
{
	public int m_ID;

	public bool m_Instant;

	public bool m_Explicit;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_ID);
		writer.Write(m_Instant);
		writer.Write(m_Explicit);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_ID = (int)reader.ReadPackedUInt32();
		m_Instant = reader.ReadBoolean();
		m_Explicit = reader.ReadBoolean();
	}
}
