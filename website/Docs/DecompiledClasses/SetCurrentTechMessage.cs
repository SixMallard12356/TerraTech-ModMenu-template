using UnityEngine.Networking;

public class SetCurrentTechMessage : MessageBase
{
	public NetworkInstanceId m_PlayerId;

	public NetworkInstanceId m_TechId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_PlayerId);
		writer.Write(m_TechId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_PlayerId = reader.ReadNetworkId();
		m_TechId = reader.ReadNetworkId();
	}
}
