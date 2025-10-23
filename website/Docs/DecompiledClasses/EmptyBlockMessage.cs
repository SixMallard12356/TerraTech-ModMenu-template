using UnityEngine.Networking;

public class EmptyBlockMessage : BlockMessage_Base
{
	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
	}
}
