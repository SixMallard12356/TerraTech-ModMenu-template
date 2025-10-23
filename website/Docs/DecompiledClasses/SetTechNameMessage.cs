using UnityEngine.Networking;

public class SetTechNameMessage : MessageBase
{
	public int m_HostId;

	public string m_NewName;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_HostId);
		writer.Write(m_NewName);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_HostId = (int)reader.ReadPackedUInt32();
		m_NewName = reader.ReadString();
	}
}
