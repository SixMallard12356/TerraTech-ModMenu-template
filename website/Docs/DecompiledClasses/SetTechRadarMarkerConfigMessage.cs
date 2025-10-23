using UnityEngine.Networking;

public class SetTechRadarMarkerConfigMessage : MessageBase
{
	public int m_HostId;

	public int m_NewColor;

	public int m_NewIcon;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_HostId);
		writer.WritePackedUInt32((uint)m_NewColor);
		writer.WritePackedUInt32((uint)m_NewIcon);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_HostId = (int)reader.ReadPackedUInt32();
		m_NewColor = (int)reader.ReadPackedUInt32();
		m_NewIcon = (int)reader.ReadPackedUInt32();
	}
}
