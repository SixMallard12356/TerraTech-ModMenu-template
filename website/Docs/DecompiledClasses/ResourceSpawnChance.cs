using System;
using UnityEngine;

[Serializable]
public struct ResourceSpawnChance
{
	[SearchableEnum(SortedEnum.EnumSortType.AlphabeticalAscending, false)]
	public ChunkTypes chunkType;

	public int spawnWeight;

	public ResourceSpawnChance(ChunkTypes type, int weight)
	{
		chunkType = type;
		spawnWeight = weight;
	}

	public static ChunkTypes GetChunkFromSpawnGroup(ResourceSpawnChance[] spawnGroup)
	{
		int num = 0;
		for (int i = 0; i < spawnGroup.Length; i++)
		{
			num += spawnGroup[i].spawnWeight;
		}
		num = UnityEngine.Random.Range(0, num);
		for (int j = 0; j < spawnGroup.Length; j++)
		{
			num -= spawnGroup[j].spawnWeight;
			if (num <= 0)
			{
				return spawnGroup[j].chunkType;
			}
		}
		return ChunkTypes.Null;
	}
}
