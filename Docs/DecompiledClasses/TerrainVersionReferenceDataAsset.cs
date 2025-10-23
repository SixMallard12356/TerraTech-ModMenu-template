using System;
using UnityEngine;

public class TerrainVersionReferenceDataAsset : ScriptableObject
{
	[Serializable]
	public class ReferenceData
	{
		public BiomeSamples[] biomes;

		public TerrainSample[] terrainSamples;

		public SpawnPlacementSamples[] vendorPlacements;

		public SpawnPlacementSamples[] landmarkPlacements;
	}

	[Serializable]
	public struct TerrainSample
	{
		public Vector2 sampledPoint;

		public int worldSeed;

		public float height;

		public string biomeName;
	}

	[Serializable]
	public struct BiomeSamples
	{
		public string biomeName;

		public int numSamples;
	}

	[Serializable]
	public struct SpawnPlacementSamples
	{
		public IntVector2 cellCoord;

		public int worldSeed;

		public Vector2 worldPos;

		public Vector2 randomRotHV;

		public int spawnChoice;
	}

	[SerializeField]
	public string m_BiomeMapBaseName;

	[SerializeField]
	public int m_GeneratorVersion;

	[SerializeField]
	public BiomeMap.WorldGenVersioningType m_GeneratorVersioningType;

	[SerializeField]
	public ReferenceData m_ReferenceData;
}
