using UnityEngine.Networking;

public class TechBlockDamagedMessage : MessageBase
{
	public NetworkInstanceId m_NetId;

	public uint m_DamageBlockPoolID;

	public ManDamage.DamageInfo m_DamageInfo;

	public bool m_RemovedBlocks;

	public int m_RemovalSeed;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_NetId.Value);
		writer.Write(m_DamageBlockPoolID);
		m_DamageInfo.NetSerialize(writer);
		writer.Write(m_RemovedBlocks);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_NetId = new NetworkInstanceId(reader.ReadUInt32());
		m_DamageBlockPoolID = reader.ReadUInt32();
		m_DamageInfo = default(ManDamage.DamageInfo);
		m_DamageInfo.NetDeserialize(reader);
		m_RemovedBlocks = reader.ReadBoolean();
	}
}
