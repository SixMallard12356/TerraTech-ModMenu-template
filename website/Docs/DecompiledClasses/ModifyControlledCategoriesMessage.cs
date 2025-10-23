using UnityEngine.Networking;

public class ModifyControlledCategoriesMessage : MessageBase
{
	public uint m_BlockPoolID;

	public ModuleControlCategory m_Category;

	public bool m_Controlled;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32(m_BlockPoolID);
		writer.Write((byte)m_Category);
		writer.Write(m_Controlled);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_BlockPoolID = reader.ReadPackedUInt32();
		m_Category = (ModuleControlCategory)reader.ReadByte();
		m_Controlled = reader.ReadBoolean();
	}
}
