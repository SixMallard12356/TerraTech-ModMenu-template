using UnityEngine.Networking;

public class OnScreenMessageFinishedMessage : MessageBase
{
	public int m_ID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_ID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_ID = (int)reader.ReadPackedUInt32();
	}
}
