using UnityEngine.Networking;

public class CircuitBlockChargeDataBlockMessage : BlockMessage_Base
{
	private Circuits.BlockChargeData _value;

	public Circuits.BlockChargeData value
	{
		get
		{
			if (_value == null)
			{
				_value = Circuits.BlockChargeData.empty;
			}
			return _value;
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		value.Serialize(writer);
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		value.Deserialize(reader);
	}
}
