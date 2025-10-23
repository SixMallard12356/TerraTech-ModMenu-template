using UnityEngine.Networking;

public class SetHeartbeatSpeedMessage : MessageBase
{
	public TechHolders.HeartbeatSpeed m_Speed;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_Speed);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Speed = (TechHolders.HeartbeatSpeed)reader.ReadInt32();
	}
}
