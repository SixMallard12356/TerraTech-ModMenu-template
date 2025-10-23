using UnityEngine.Networking;

public class FloatParamBlockMessage : BlockMessage_Base
{
	public float value;

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.Write(value);
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		value = reader.ReadSingle();
	}
}
