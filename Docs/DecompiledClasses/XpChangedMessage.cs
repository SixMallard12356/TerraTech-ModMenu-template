using UnityEngine.Networking;

public class XpChangedMessage : MessageBase
{
	public bool m_ShowUI;

	public FactionSubTypes m_Corporation;

	public bool m_Discovered;

	public int m_AbsoluteXP;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_ShowUI);
		writer.Write((int)m_Corporation);
		writer.Write(m_Discovered);
		writer.WritePackedUInt32((uint)m_AbsoluteXP);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_ShowUI = reader.ReadBoolean();
		m_Corporation = (FactionSubTypes)reader.ReadInt32();
		m_Discovered = reader.ReadBoolean();
		m_AbsoluteXP = (int)reader.ReadPackedUInt32();
	}
}
