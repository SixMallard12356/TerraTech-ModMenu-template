using UnityEngine.Networking;

public class RemoveGhostBlocksOnTechMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechNetId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechNetId = reader.ReadNetworkId();
	}
}
