using UnityEngine.Networking;

public class EncounterFinishedMessage : MessageBase
{
	public NetworkInstanceId m_PlayerNetID = NetworkInstanceId.Invalid;

	public EncounterIdentifier m_Id;

	public ManEncounter.FinishState m_State;

	public int m_WaypointHostID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_PlayerNetID);
		writer.Write(m_Id);
		writer.WritePackedInt32((int)m_State);
		writer.WritePackedInt32(m_WaypointHostID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_PlayerNetID = reader.ReadNetworkId();
		m_Id = reader.ReadEncounterID();
		m_State = (ManEncounter.FinishState)reader.ReadPackedInt32();
		m_WaypointHostID = reader.ReadPackedInt32();
	}
}
