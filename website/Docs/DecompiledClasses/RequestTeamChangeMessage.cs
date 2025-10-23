using UnityEngine.Networking;

public class RequestTeamChangeMessage : MessageBase
{
	public int m_TechTeamID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_TechTeamID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechTeamID = (int)reader.ReadPackedUInt32();
	}
}
