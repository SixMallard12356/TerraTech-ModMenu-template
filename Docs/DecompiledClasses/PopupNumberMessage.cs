using UnityEngine.Networking;

public class PopupNumberMessage : MessageBase
{
	public enum Type
	{
		Money,
		XP
	}

	public Type m_Type;

	public int m_Number;

	public WorldPosition m_Position;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((byte)m_Type);
		writer.WritePackedInt32(m_Number);
		writer.Write(m_Position);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Type = (Type)reader.ReadByte();
		m_Number = reader.ReadPackedInt32();
		m_Position = reader.ReadWorldPosition();
	}
}
