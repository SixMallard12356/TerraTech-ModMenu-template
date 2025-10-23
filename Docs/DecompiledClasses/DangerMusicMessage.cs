using UnityEngine.Networking;

public class DangerMusicMessage : MessageBase
{
	public struct DangerContextData
	{
		public ManMusic.DangerContext.Circumstance m_Circumstance;

		public FactionSubTypes m_Corporation;

		public float m_Timeout;

		public int m_BlockCount;

		public int m_VisibleID;

		public void Serialize(NetworkWriter writer)
		{
			writer.WritePackedInt32((int)m_Circumstance);
			writer.WritePackedInt32((int)m_Corporation);
			writer.Write(m_Timeout);
			writer.WritePackedInt32(m_BlockCount);
			writer.WritePackedInt32(m_VisibleID);
		}

		public void Deserialize(NetworkReader reader)
		{
			m_Circumstance = (ManMusic.DangerContext.Circumstance)reader.ReadPackedInt32();
			m_Corporation = (FactionSubTypes)reader.ReadPackedInt32();
			m_Timeout = reader.ReadSingle();
			m_BlockCount = reader.ReadPackedInt32();
			m_VisibleID = reader.ReadPackedInt32();
		}
	}

	public DangerContextData context;

	public override void Serialize(NetworkWriter writer)
	{
		context.Serialize(writer);
	}

	public override void Deserialize(NetworkReader reader)
	{
		context = default(DangerContextData);
		context.Deserialize(reader);
	}
}
