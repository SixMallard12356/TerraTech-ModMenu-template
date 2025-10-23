using Unity;
using UnityEngine.Networking;

public class LockTreadmillMessage : MessageBase
{
	public IntVector3 m_FloatingOrigin;

	public override void Serialize(NetworkWriter writer)
	{
		GeneratedNetworkCode._WriteIntVector3_None(writer, m_FloatingOrigin);
	}

	public override void Deserialize(NetworkReader reader)
	{
		m_FloatingOrigin = GeneratedNetworkCode._ReadIntVector3_None(reader);
	}
}
