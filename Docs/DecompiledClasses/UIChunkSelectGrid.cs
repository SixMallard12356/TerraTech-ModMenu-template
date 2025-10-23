using System;
using System.Collections.Generic;

[Serializable]
public class UIChunkSelectGrid : UIItemSelectGrid
{
	public delegate bool ChunkFilterFn(ChunkTypes chunkType);

	private static ChunkTypes[] s_AllChunkTypes;

	public ChunkFilterFn ChunkFilter { get; set; }

	protected override void GetFilteredItemTypes(List<ItemTypeInfo> itemTypes)
	{
		if (s_AllChunkTypes == null)
		{
			s_AllChunkTypes = EnumValuesIterator<ChunkTypes>.Values;
		}
		ChunkTypes[] array = s_AllChunkTypes;
		foreach (ChunkTypes chunkTypes in array)
		{
			if (ChunkFilter == null || ChunkFilter(chunkTypes))
			{
				itemTypes.Add(new ItemTypeInfo(ObjectTypes.Chunk, (int)chunkTypes));
			}
		}
	}
}
