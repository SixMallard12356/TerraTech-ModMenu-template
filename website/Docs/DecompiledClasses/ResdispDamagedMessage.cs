using UnityEngine.Networking;

public class ResdispDamagedMessage : MessageBase
{
	public IntVector2 m_TileCoord;

	public IntVector2 m_CellCoord;

	public NetworkInstanceId m_PlayerNetId;

	public ManDamage.DamageInfo m_DamageInfo;

	public bool m_ThresholdPassed;

	public int m_DamageStage;

	public int m_NumChunksSpawned;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_TileCoord);
		writer.Write(m_CellCoord);
		writer.Write(m_PlayerNetId.Value);
		m_DamageInfo.NetSerialize(writer);
		writer.Write(m_ThresholdPassed);
		writer.Write(m_DamageStage);
		writer.Write(m_NumChunksSpawned);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TileCoord = reader.ReadVector2();
		m_CellCoord = reader.ReadVector2();
		m_PlayerNetId = new NetworkInstanceId(reader.ReadUInt32());
		m_DamageInfo = default(ManDamage.DamageInfo);
		m_DamageInfo.NetDeserialize(reader);
		m_ThresholdPassed = reader.ReadBoolean();
		m_DamageStage = reader.ReadInt32();
		m_NumChunksSpawned = reader.ReadInt32();
	}
}
