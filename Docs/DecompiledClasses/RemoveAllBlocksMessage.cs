using UnityEngine.Networking;

public class RemoveAllBlocksMessage : MessageBase
{
	public BlockManager.RemoveAllAction m_Action;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_Action);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_Action = (BlockManager.RemoveAllAction)reader.ReadInt32();
	}
}
