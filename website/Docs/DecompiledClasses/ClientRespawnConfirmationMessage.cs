using UnityEngine.Networking;

public class ClientRespawnConfirmationMessage : MessageBase
{
	public uint m_PlayerNetID;

	public int m_CorporationChoice;

	public int m_LoadoutChoice;

	public int m_SkinIDChoice;

	public override void Deserialize(NetworkReader reader)
	{
		m_PlayerNetID = reader.ReadUInt32();
		m_CorporationChoice = reader.ReadInt16();
		m_LoadoutChoice = reader.ReadInt16();
		m_SkinIDChoice = reader.ReadInt16();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_PlayerNetID);
		writer.Write((short)m_CorporationChoice);
		writer.Write((short)m_LoadoutChoice);
		writer.Write((short)m_SkinIDChoice);
	}
}
