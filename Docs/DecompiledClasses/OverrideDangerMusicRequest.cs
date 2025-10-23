using UnityEngine.Networking;

public class OverrideDangerMusicRequest : MessageBase
{
	public ManMusic.MiscDangerMusicType m_DangerMusicType;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedInt32((int)m_DangerMusicType);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_DangerMusicType = (ManMusic.MiscDangerMusicType)reader.ReadPackedInt32();
	}
}
