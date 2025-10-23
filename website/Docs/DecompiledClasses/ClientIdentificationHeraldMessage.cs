using TerraTech.Network;
using UnityEngine.Networking;

public class ClientIdentificationHeraldMessage : MessageBase
{
	public TTNetworkID m_clientID;

	public override void Deserialize(NetworkReader reader)
	{
		m_clientID = reader.ReadTTNetworkID();
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_clientID);
	}
}
