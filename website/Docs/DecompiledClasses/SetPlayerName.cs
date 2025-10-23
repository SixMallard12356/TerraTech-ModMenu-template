using UnityEngine.Networking;

public class SetPlayerName : MessageBase
{
	public string name;

	public override void Serialize(NetworkWriter writer)
	{
		writer.Write(name);
	}

	public override void Deserialize(NetworkReader reader)
	{
		name = reader.ReadString();
	}
}
