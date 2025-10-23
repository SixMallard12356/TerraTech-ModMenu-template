#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BiomeMapStack
{
	[SerializeField]
	private BiomeMap[] m_BiomeMaps;

	public BiomeMap LatestMap => SortedMaps.FirstOrDefault();

	public IEnumerable<BiomeMap> SortedMaps => m_BiomeMaps.OrderByDescending((BiomeMap m) => m.WorldGenVersionData);

	public BiomeMap SelectCompatibleBiomeMap()
	{
		if (m_BiomeMaps.Length == 1 && m_BiomeMaps[0].RequiredWorldGenVersion == -1)
		{
			return m_BiomeMaps[0];
		}
		if (m_BiomeMaps.Any((BiomeMap m) => m.RequiredWorldGenVersion == -1))
		{
			d.LogError("MapStack {" + string.Join(", ", m_BiomeMaps.Select((BiomeMap m) => m.name)) + "} contains multiple maps, some of which have worldGenVersion -1 (unversioned).. This is not supported!");
		}
		foreach (BiomeMap sortedMap in SortedMaps)
		{
			if (sortedMap.CompatibleWithSavedWorldGen())
			{
				return sortedMap;
			}
		}
		d.LogError("BiomeMapStack.SelectCompatibleBiomeMap - Failed to find a valid worldgen version!");
		return null;
	}
}
