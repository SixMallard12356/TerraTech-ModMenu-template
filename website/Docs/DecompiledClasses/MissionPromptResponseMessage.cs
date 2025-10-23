using UnityEngine.Networking;

public class MissionPromptResponseMessage : MessageBase
{
	public uint m_BlockPoolID;

	public bool m_Accepted;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write(m_Accepted);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_Accepted = reader.ReadBoolean();
	}
}
