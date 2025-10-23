using UnityEngine;
using UnityEngine.Networking;

public class ProjectileStuckSyncMessage : MessageBase
{
	public uint m_BlockStuckToPoolID;

	public int m_ProjectileUID;

	public Vector3 m_ProjectileLocalPosition;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockStuckToPoolID);
		writer.WritePackedUInt32((uint)m_ProjectileUID);
		writer.Write(m_ProjectileLocalPosition);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockStuckToPoolID = reader.ReadPackedUInt32();
		m_ProjectileUID = (int)reader.ReadPackedUInt32();
		m_ProjectileLocalPosition = reader.ReadVector3();
	}
}
