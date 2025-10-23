using UnityEngine.Networking;

public class EncounterStartedMessage : EncounterMessage
{
	public int m_WaypointHostID;

	public bool m_SetTracked;

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.WritePackedInt32(m_WaypointHostID);
		writer.Write(m_SetTracked);
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		m_WaypointHostID = reader.ReadPackedInt32();
		m_SetTracked = reader.ReadBoolean();
	}
}
