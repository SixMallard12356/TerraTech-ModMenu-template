using UnityEngine;
using UnityEngine.Networking;

public class BlockScavengedMessage : MessageBase
{
	public BlockTypes m_BlockType;

	public WorldPosition m_Position;

	public Quaternion m_Rotation;

	public NetworkInstanceId m_OriginalOwnerId;

	public int m_OriginalTeamId;

	public bool m_IsLoadoutBlock;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_BlockType);
		writer.Write(m_Position);
		writer.Write(m_Rotation);
		writer.Write(m_OriginalOwnerId);
		writer.Write(m_OriginalTeamId);
		writer.Write(m_IsLoadoutBlock);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockType = (BlockTypes)reader.ReadInt32();
		m_Position = reader.ReadWorldPosition();
		m_Rotation = reader.ReadQuaternion();
		m_OriginalOwnerId = reader.ReadNetworkId();
		m_OriginalTeamId = reader.ReadInt32();
		m_IsLoadoutBlock = reader.ReadBoolean();
	}
}
