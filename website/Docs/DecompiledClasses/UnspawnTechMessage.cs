using UnityEngine.Networking;

public class UnspawnTechMessage : MessageBase
{
	public int m_HostID;

	public bool m_CheatBypassInventory;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_HostID);
		writer.Write(m_CheatBypassInventory);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_HostID = (int)reader.ReadPackedUInt32();
		m_CheatBypassInventory = reader.ReadBoolean();
	}
}
