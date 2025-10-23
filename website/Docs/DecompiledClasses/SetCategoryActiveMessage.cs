using UnityEngine.Networking;

public class SetCategoryActiveMessage : MessageBase
{
	public ModuleControlCategory m_ModuleCategory;

	public bool m_Active;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_ModuleCategory);
		writer.Write(m_Active);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_ModuleCategory = (ModuleControlCategory)reader.ReadInt32();
		m_Active = reader.ReadBoolean();
	}
}
