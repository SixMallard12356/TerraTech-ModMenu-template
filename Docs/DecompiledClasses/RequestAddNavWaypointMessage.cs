using Unity;
using UnityEngine.Networking;

public class RequestAddNavWaypointMessage : MessageBase
{
	public IntVector3 worldPos;

	public override void Serialize(NetworkWriter writer)
	{
		GeneratedNetworkCode._WriteIntVector3_None(writer, worldPos);
	}

	public override void Deserialize(NetworkReader reader)
	{
		worldPos = GeneratedNetworkCode._ReadIntVector3_None(reader);
	}
}
