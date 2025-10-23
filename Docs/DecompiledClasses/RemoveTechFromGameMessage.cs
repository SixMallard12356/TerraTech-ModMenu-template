using UnityEngine.Networking;

public class RemoveTechFromGameMessage : MessageBase
{
	public bool m_FromHost;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_FromHost);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_FromHost = reader.ReadBoolean();
	}
}
