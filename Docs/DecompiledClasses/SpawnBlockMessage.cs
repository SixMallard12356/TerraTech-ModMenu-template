using UnityEngine;
using UnityEngine.Networking;

public class SpawnBlockMessage : MessageBase
{
	public BlockTypes m_BlockType;

	public WorldPosition m_Pos;

	public Quaternion m_Rot;

	public uint m_BlockPoolID;

	public uint m_InitialSpawnShieldID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_BlockType);
		writer.Write(m_Pos);
		writer.Write(m_Rot);
		writer.Write(m_BlockPoolID);
		writer.Write(m_InitialSpawnShieldID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockType = (BlockTypes)reader.ReadInt32();
		m_Pos = reader.ReadWorldPosition();
		m_Rot = reader.ReadQuaternion();
		m_BlockPoolID = reader.ReadUInt32();
		m_InitialSpawnShieldID = reader.ReadUInt32();
	}
}
