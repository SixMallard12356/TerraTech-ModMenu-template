using Unity;
using UnityEngine.Networking;

public class ClientLoadedTileMessage : MessageBase
{
	public IntVector2 m_TilePos;

	public bool m_Loaded;

	public override void Serialize(NetworkWriter writer)
	{
		GeneratedNetworkCode._WriteIntVector2_None(writer, m_TilePos);
		writer.Write(m_Loaded);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_TilePos = GeneratedNetworkCode._ReadIntVector2_None(reader);
		m_Loaded = reader.ReadBoolean();
	}
}
