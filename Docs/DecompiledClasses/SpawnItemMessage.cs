using UnityEngine;
using UnityEngine.Networking;

public class SpawnItemMessage : MessageBase
{
	public ObjectTypes m_ObjectType;

	public int m_ItemType;

	public WorldPosition m_Pos;

	public Quaternion m_Rot;

	public uint m_BlockPoolID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((byte)m_ObjectType);
		writer.Write((short)m_ItemType);
		writer.Write(m_Pos);
		writer.Write(m_Rot);
		writer.Write(m_BlockPoolID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_ObjectType = (ObjectTypes)reader.ReadByte();
		m_ItemType = reader.ReadInt16();
		m_Pos = reader.ReadWorldPosition();
		m_Rot = reader.ReadQuaternion();
		m_BlockPoolID = reader.ReadUInt32();
	}
}
