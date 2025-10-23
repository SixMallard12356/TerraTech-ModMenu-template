using UnityEngine.Networking;

public class LockTechSendToSCUMessage : MessageBase
{
	public NetworkInstanceId m_TechId;

	public float m_LockTime;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechId);
		writer.Write(m_LockTime);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechId = reader.ReadNetworkId();
		m_LockTime = reader.ReadSingle();
	}
}
