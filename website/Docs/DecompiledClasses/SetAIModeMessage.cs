using UnityEngine.Networking;

public class SetAIModeMessage : MessageBase
{
	public AITreeType.AITypes m_AIAction;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_AIAction);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_AIAction = (AITreeType.AITypes)reader.ReadInt32();
	}
}
