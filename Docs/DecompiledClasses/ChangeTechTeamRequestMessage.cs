using UnityEngine.Networking;

public class ChangeTechTeamRequestMessage : MessageBase
{
	public int m_Team;

	public bool m_IsPopulation;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_Team);
		writer.Write(m_IsPopulation);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Team = reader.ReadInt32();
		m_IsPopulation = reader.ReadBoolean();
	}
}
