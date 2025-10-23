using UnityEngine.Networking;

public class CrateStateUpdateMessage : MessageBase
{
	public Crate.State m_State;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write((int)m_State);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_State = (Crate.State)reader.ReadInt32();
	}
}
