using UnityEngine.Networking;

public class SetVisibleHeldMessage : MessageBase
{
	public NetworkInstanceId m_TechNetId;

	public uint m_BlockPoolID;

	public int m_StackIndex;

	public int m_StackPosition;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TechNetId);
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.WritePackedUInt32((uint)m_StackIndex);
		writer.WritePackedUInt32((uint)m_StackPosition);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TechNetId = reader.ReadNetworkId();
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_StackIndex = (int)reader.ReadPackedUInt32();
		m_StackPosition = (int)reader.ReadPackedUInt32();
	}
}
