using UnityEngine.Networking;

public class ForceReleaseNotableBlock : MessageBase
{
	public NetworkInstanceId m_BlockNetId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_BlockNetId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockNetId = reader.ReadNetworkId();
	}
}
