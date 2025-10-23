using UnityEngine.Networking;

public class EncounterMessage : MessageBase
{
	public NetworkInstanceId m_PlayerNetID = NetworkInstanceId.Invalid;

	public EncounterToSpawn m_EncounterSpawn;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_PlayerNetID);
		m_EncounterSpawn.OnSerialize(writer, initialState: true);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_PlayerNetID = reader.ReadNetworkId();
		m_EncounterSpawn = new EncounterToSpawn();
		m_EncounterSpawn.OnDeserialize(reader);
	}
}
