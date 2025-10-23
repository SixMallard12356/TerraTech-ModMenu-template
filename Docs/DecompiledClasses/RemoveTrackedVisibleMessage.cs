using UnityEngine.Networking;

public class RemoveTrackedVisibleMessage : MessageBase
{
	public int m_HostID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_HostID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_HostID = (int)reader.ReadPackedUInt32();
	}
}
