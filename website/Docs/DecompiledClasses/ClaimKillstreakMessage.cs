using UnityEngine.Networking;

public class ClaimKillstreakMessage : MessageBase
{
	public int m_Level;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedInt32(m_Level);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Level = reader.ReadPackedInt32();
	}
}
