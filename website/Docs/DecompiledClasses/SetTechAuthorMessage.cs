using UnityEngine.Networking;

public class SetTechAuthorMessage : MessageBase
{
	public string m_NetPlayerName;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_NetPlayerName);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_NetPlayerName = reader.ReadString();
	}
}
