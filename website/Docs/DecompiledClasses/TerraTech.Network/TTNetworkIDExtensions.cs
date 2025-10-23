using UnityEngine.Networking;

namespace TerraTech.Network;

public static class TTNetworkIDExtensions
{
	public static TTNetworkID ReadTTNetworkID(this NetworkReader reader)
	{
		return new TTNetworkID
		{
			m_NetworkID = reader.ReadString()
		};
	}

	public static void Write(this NetworkWriter writer, TTNetworkID networkID)
	{
		writer.Write(networkID.m_NetworkID);
	}
}
