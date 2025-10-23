using UnityEngine.Networking;

public class BlockDamagedMessage : MessageBase
{
	public NetworkInstanceId m_PlayerNetId;

	public ManDamage.DamageInfo m_DamageInfo;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_PlayerNetId.Value);
		m_DamageInfo.NetSerialize(writer);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_PlayerNetId = new NetworkInstanceId(reader.ReadUInt32());
		m_DamageInfo = default(ManDamage.DamageInfo);
		m_DamageInfo.NetDeserialize(reader);
	}
}
