using UnityEngine.Networking;

public class RequestControlTechMessage : MessageBase
{
	public NetworkInstanceId m_TechId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechId = reader.ReadNetworkId();
	}
}
