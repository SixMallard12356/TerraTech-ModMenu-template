using UnityEngine.Networking;

public class BoolParamBlockMessage : BlockMessage_Base
{
	public bool value;

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.Write(value);
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		value = reader.ReadBoolean();
	}
}
