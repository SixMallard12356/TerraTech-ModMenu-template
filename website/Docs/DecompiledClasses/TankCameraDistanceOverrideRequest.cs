using UnityEngine.Networking;

public class TankCameraDistanceOverrideRequest : MessageBase
{
	public bool enable;

	public float distance;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(distance);
		writer.Write(enable);
	}

	public override void Deserialize(NetworkReader reader)
	{
		distance = reader.ReadSingle();
		enable = reader.ReadBoolean();
	}
}
