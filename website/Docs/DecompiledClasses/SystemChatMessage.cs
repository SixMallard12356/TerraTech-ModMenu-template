using Unity;
using UnityEngine.Networking;

public class SystemChatMessage : MessageBase
{
	public LocalisationEnums.StringBanks m_Bank;

	public int m_StringID;

	public string[] m_Params;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_Bank);
		writer.WritePackedUInt32((uint)m_StringID);
		GeneratedNetworkCode._WriteArrayString_None(writer, m_Params);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Bank = (LocalisationEnums.StringBanks)reader.ReadInt32();
		m_StringID = (int)reader.ReadPackedUInt32();
		m_Params = GeneratedNetworkCode._ReadArrayString_None(reader);
	}
}
