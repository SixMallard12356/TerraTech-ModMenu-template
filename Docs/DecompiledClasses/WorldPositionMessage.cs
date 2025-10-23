using UnityEngine.Networking;

public class WorldPositionMessage : MessageBase
{
	public WorldPosition m_Position;

	public override void Deserialize(NetworkReader reader)
	{
		m_Position = reader.ReadWorldPosition();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_Position);
	}
}
