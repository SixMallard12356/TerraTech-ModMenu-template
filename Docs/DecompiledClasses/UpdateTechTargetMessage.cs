using UnityEngine.Networking;

public class UpdateTechTargetMessage : MessageBase
{
	public NetworkInstanceId m_TargetId;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TargetId);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TargetId = reader.ReadNetworkId();
	}
}
