using System.Collections.Generic;
using UnityEngine.Networking;

public class MapDataMessage : MessageBase
{
	public List<ManMap.TileData> tileData;

	public override void Serialize(NetworkWriter writer)
	{
		writer.WritePackedInt32(tileData.Count);
		foreach (ManMap.TileData tileDatum in tileData)
		{
			writer.WritePackedInt32(tileDatum.coord.x);
			writer.WritePackedInt32(tileDatum.coord.y);
			writer.WriteBytesWithSize32(tileDatum.maskData);
		}
	}

	public override void Deserialize(NetworkReader reader)
	{
		int num = reader.ReadPackedInt32();
		if (tileData == null)
		{
			tileData = new List<ManMap.TileData>(num);
		}
		else
		{
			tileData.Clear();
			tileData.Capacity = num;
		}
		for (int i = 0; i < num; i++)
		{
			int x = reader.ReadPackedInt32();
			int y = reader.ReadPackedInt32();
			tileData.Add(new ManMap.TileData
			{
				coord = new IntVector2(x, y),
				maskData = reader.ReadBytesWithSize32()
			});
		}
	}
}
