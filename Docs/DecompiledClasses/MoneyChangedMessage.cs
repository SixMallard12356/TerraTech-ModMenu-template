using UnityEngine.Networking;

public class MoneyChangedMessage : MessageBase
{
	public int m_Money;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)m_Money);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Money = (int)reader.ReadPackedUInt32();
	}
}
