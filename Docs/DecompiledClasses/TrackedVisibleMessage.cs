using UnityEngine.Networking;

public abstract class TrackedVisibleMessage : MessageBase
{
	public int m_HostID;

	public int m_TeamID;

	public int m_RadarTeamID;

	public WorldPosition m_WorldPosition;

	public RadarTypes m_RadarType;

	public bool m_IsQuestObject;

	public bool m_IsOverlayEnabled;

	public RadarMarker m_RadarMarkerConfig;

	public TrackedVisibleMessage()
	{
	}

	public TrackedVisibleMessage(TrackedVisible trackedVis)
	{
		m_HostID = trackedVis.ID;
		m_TeamID = trackedVis.TeamID;
		m_RadarTeamID = trackedVis.RawRadarTeamID;
		m_WorldPosition = trackedVis.GetWorldPosition();
		m_RadarType = trackedVis.RadarType;
		m_RadarMarkerConfig = trackedVis.RadarMarkerConfig;
		m_IsQuestObject = trackedVis.IsQuestObject;
		m_IsOverlayEnabled = trackedVis.WaypointOverlayEnabled;
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_HostID = reader.ReadPackedInt32();
		m_TeamID = reader.ReadPackedInt32();
		m_RadarTeamID = reader.ReadPackedInt32();
		m_WorldPosition = reader.ReadWorldPosition();
		m_RadarType = (RadarTypes)reader.ReadByte();
		m_RadarMarkerConfig = reader.ReadRadarMarker();
		m_IsQuestObject = reader.ReadBoolean();
		m_IsOverlayEnabled = reader.ReadBoolean();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedInt32(m_HostID);
		writer.WritePackedInt32(m_TeamID);
		writer.WritePackedInt32(m_RadarTeamID);
		writer.Write(m_WorldPosition);
		writer.Write((byte)m_RadarType);
		writer.Write(m_RadarMarkerConfig);
		writer.Write(m_IsQuestObject);
		writer.Write(m_IsOverlayEnabled);
	}
}
