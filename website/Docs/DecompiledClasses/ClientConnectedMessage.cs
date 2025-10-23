using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine.Networking;

public class ClientConnectedMessage : MessageBase
{
	public TTNetworkID m_clientID;

	public List<IntVector2> m_loadedTiles;

	public override void Deserialize(NetworkReader reader)
	{
		m_clientID = reader.ReadTTNetworkID();
		int num = reader.ReadInt32();
		m_loadedTiles = new List<IntVector2>(num);
		for (int i = 0; i < num; i++)
		{
			int x = reader.ReadInt32();
			int y = reader.ReadInt32();
			m_loadedTiles.Add(new IntVector2(x, y));
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(m_clientID);
		writer.Write(m_loadedTiles.Count);
		for (int i = 0; i < m_loadedTiles.Count; i++)
		{
			writer.Write(m_loadedTiles[i].x);
			writer.Write(m_loadedTiles[i].y);
		}
	}
}
