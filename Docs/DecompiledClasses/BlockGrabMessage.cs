using UnityEngine.Networking;

public class BlockGrabMessage : MessageBase
{
	public NetworkInstanceId m_NetId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_NetId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_NetId = reader.ReadNetworkId();
	}
}
