using UnityEngine.Networking;

public class BuyBlockMessage : MessageBase
{
	public uint m_ShopBlockPoolID;

	public BlockTypes m_BlockType;

	public int m_Count;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_ShopBlockPoolID);
		writer.Write((int)m_BlockType);
		writer.WritePackedUInt32((uint)m_Count);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_ShopBlockPoolID = reader.ReadPackedUInt32();
		m_BlockType = (BlockTypes)reader.ReadInt32();
		m_Count = (int)reader.ReadPackedUInt32();
	}
}
