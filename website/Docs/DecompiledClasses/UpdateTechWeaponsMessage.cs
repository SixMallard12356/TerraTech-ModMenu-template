using UnityEngine.Networking;

public class UpdateTechWeaponsMessage : MessageBase
{
	public TechWeapon.State m_State;

	public override void Deserialize(NetworkReader reader)
	{
		m_State.NetDeserialize(reader);
	}

	public override void Serialize(NetworkWriter writer)
	{
		m_State.NetSerialize(writer);
	}
}
