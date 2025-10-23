using UnityEngine.Networking;

public class EncounterUpdateMessage : MessageBase
{
	public struct ObjectiveData
	{
		public bool m_Visible;

		public bool m_ShowCount;

		public bool m_Completed;

		public int m_Count;

		public void Serialize(NetworkWriter writer)
		{
			writer.Write(m_Visible);
			writer.Write(m_ShowCount);
			writer.Write(m_Completed);
			writer.WritePackedInt32(m_Count);
		}

		public void Deserialize(NetworkReader reader)
		{
			m_Visible = reader.ReadBoolean();
			m_ShowCount = reader.ReadBoolean();
			m_Completed = reader.ReadBoolean();
			m_Count = reader.ReadPackedInt32();
		}
	}

	public struct EncounterTimerData
	{
		public float m_TotalCountdownTime;

		public float m_TimeRemaining;

		public bool m_TimerExpired;

		public bool m_Active;

		public void Serialize(NetworkWriter writer)
		{
			writer.Write(m_TotalCountdownTime);
			writer.Write(m_TimeRemaining);
			writer.Write(m_TimerExpired);
			writer.Write(m_Active);
		}

		public void Deserialize(NetworkReader reader)
		{
			m_TotalCountdownTime = reader.ReadSingle();
			m_TimeRemaining = reader.ReadSingle();
			m_TimerExpired = reader.ReadBoolean();
			m_Active = reader.ReadBoolean();
		}
	}

	public EncounterIdentifier m_Id;

	public int m_ActiveObjectiveIdx;

	public ObjectiveData[] m_ObjectiveData;

	public EncounterTimerData m_EncounterTimer;

	public int m_EncounterWaypointHostID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_Id);
		writer.WritePackedInt32(m_ActiveObjectiveIdx);
		writer.WritePackedInt32(m_ObjectiveData.Length);
		for (int i = 0; i < m_ObjectiveData.Length; i++)
		{
			m_ObjectiveData[i].Serialize(writer);
		}
		m_EncounterTimer.Serialize(writer);
		writer.WritePackedInt32(m_EncounterWaypointHostID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Id = reader.ReadEncounterID();
		m_ActiveObjectiveIdx = reader.ReadPackedInt32();
		int num = reader.ReadPackedInt32();
		m_ObjectiveData = new ObjectiveData[num];
		for (int i = 0; i < num; i++)
		{
			m_ObjectiveData[i].Deserialize(reader);
		}
		m_EncounterTimer = default(EncounterTimerData);
		m_EncounterTimer.Deserialize(reader);
		m_EncounterWaypointHostID = reader.ReadPackedInt32();
	}
}
