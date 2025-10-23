using UnityEngine.Networking;

public class RequestRemoveNavWaypointMessage : MessageBase
{
	public int hostID;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedUInt32((uint)hostID);
	}

	public override void Deserialize(NetworkReader reader)
	{
		hostID = (int)reader.ReadPackedUInt32();
	}
}
