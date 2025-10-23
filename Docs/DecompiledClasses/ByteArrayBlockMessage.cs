using System;
using UnityEngine.Networking;

public class ByteArrayBlockMessage : BlockMessage_Base
{
	public byte[] value;

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		writer.WritePackedInt32((value != null) ? value.Length : 0);
		if (value != null)
		{
			for (int i = 0; i < value.Length; i++)
			{
				writer.Write(value[i]);
			}
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		int num = reader.ReadPackedInt32();
		if (value == null)
		{
			value = new byte[num];
		}
		else
		{
			Array.Resize(ref value, num);
		}
		for (int i = 0; i < num; i++)
		{
			value[i] = reader.ReadByte();
		}
	}
}
