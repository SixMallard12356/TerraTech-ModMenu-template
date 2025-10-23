using UnityEngine.Networking;

public class IntParamBlockMessage : BlockMessage_Base
{
	public int value;

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.WritePackedInt32(value);
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		value = reader.ReadPackedInt32();
	}
}
