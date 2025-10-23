using UnityEngine.Networking;

public class SpawnDeliveryBombVisualMessage : MessageBase
{
	public WorldPosition m_Position;

	public DeliveryBombSpawner.ImpactMarkerType m_ImpactMarkerType;

	public float m_Delay;

	public override void Deserialize(NetworkReader reader)
	{
		m_Position = reader.ReadWorldPosition();
		m_ImpactMarkerType = (DeliveryBombSpawner.ImpactMarkerType)reader.ReadPackedInt32();
		m_Delay = reader.ReadSingle();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_Position);
		writer.WritePackedInt32((int)m_ImpactMarkerType);
		writer.Write(m_Delay);
	}
}
