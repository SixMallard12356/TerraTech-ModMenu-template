using UnityEngine.Networking;

public class TimeOfDayUpdateMessage : MessageBase
{
	public bool m_TimeProgression;

	public bool m_Daytime;

	public ushort m_Year;

	public byte m_Month;

	public byte m_Day;

	public float m_Hour;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TimeProgression);
		writer.Write(m_Daytime);
		writer.WritePackedUInt32(m_Year);
		writer.WritePackedUInt32(m_Month);
		writer.WritePackedUInt32(m_Day);
		writer.Write(m_Hour);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TimeProgression = reader.ReadBoolean();
		m_Daytime = reader.ReadBoolean();
		m_Year = (ushort)reader.ReadPackedUInt32();
		m_Month = (byte)reader.ReadPackedUInt32();
		m_Day = (byte)reader.ReadPackedUInt32();
		m_Hour = reader.ReadSingle();
	}
}
