using UnityEngine;
using UnityEngine.Networking;

public class BlockReleaseMessage : MessageBase
{
	public NetworkInstanceId m_NetId;

	public uint m_BlockPoolID;

	public WorldPosition m_Position;

	public Quaternion m_Rotation;

	public Vector3 m_Velocity;

	public Vector3 m_AngularVelocity;

	public Vector3 m_DraggingItemVelocity;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_NetId);
		writer.Write(m_BlockPoolID);
		writer.Write(m_Position);
		writer.Write(m_Rotation);
		writer.Write(m_Velocity);
		writer.Write(m_AngularVelocity);
		writer.Write(m_DraggingItemVelocity);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_NetId = reader.ReadNetworkId();
		m_BlockPoolID = reader.ReadUInt32();
		m_Position = reader.ReadWorldPosition();
		m_Rotation = reader.ReadQuaternion();
		m_Velocity = reader.ReadVector3();
		m_AngularVelocity = reader.ReadVector3();
		m_DraggingItemVelocity = reader.ReadVector3();
	}
}
