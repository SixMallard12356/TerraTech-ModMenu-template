using UnityEngine.Networking;

public class UpdateHoverAndGyroMessage : MessageBase
{
	public float m_Hover;

	public float m_Gyro;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_Hover);
		writer.Write(m_Gyro);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Hover = reader.ReadSingle();
		m_Gyro = reader.ReadSingle();
	}
}
