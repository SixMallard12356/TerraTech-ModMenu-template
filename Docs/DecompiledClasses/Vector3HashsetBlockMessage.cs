using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Vector3HashsetBlockMessage : BlockMessage_Base
{
	public HashSet<Vector3> value;

	public void InitHashset()
	{
		if (value == null)
		{
			value = new HashSet<Vector3>();
		}
	}

	public override void Serialize(NetworkWriter writer)
	{
		base.Serialize(writer);
		InitHashset();
		writer.WritePackedInt32(value.Count);
		foreach (Vector3 item in value)
		{
			writer.Write(item);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		base.Deserialize(reader);
		InitHashset();
		value.Clear();
		int num = reader.ReadPackedInt32();
		for (int i = 0; i < num; i++)
		{
			value.Add(reader.ReadVector3());
		}
	}
}
