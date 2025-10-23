using UnityEngine;
using UnityEngine.Networking;

public class CustomSpawnEffectRequest : MessageBase
{
	public ManSpawn.CustomSpawnEffectType m_CustomSpawnEffectType;

	public Vector3 m_Position;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedInt32((int)m_CustomSpawnEffectType);
		writer.Write(m_Position);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_CustomSpawnEffectType = (ManSpawn.CustomSpawnEffectType)reader.ReadPackedInt32();
		m_Position = reader.ReadVector3();
	}
}
