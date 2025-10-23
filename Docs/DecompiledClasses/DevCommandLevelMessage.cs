using UnityEngine.Networking;

public class DevCommandLevelMessage : MessageBase
{
	public int m_CommandLevel;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_CommandLevel);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_CommandLevel = (int)reader.ReadPackedUInt32();
	}
}
