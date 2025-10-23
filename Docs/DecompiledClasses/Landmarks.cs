#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "Landmark Spawner", menuName = "Asset/Landmark Spawner")]
[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public class Landmarks : ScriptableObject, PlacementBlocker
{
	private struct SpawnData
	{
		public Prefab prefab;

		public Vector3 position;

		public Vector2 randomRotHV;
	}

	[Serializable]
	private struct Prefab
	{
		public TerrainObject terrain;

		public BoxCollider box;
	}

	[SerializeField]
	private Prefab[] m_Prefabs;

	[SerializeField]
	private GridBasedDistributor m_Placer;

	private List<GridBasedDistributor.PlacementData> m_PerTilePlacementWorking = new List<GridBasedDistributor.PlacementData>();

	private List<SpawnData> m_SpawnList = new List<SpawnData>();

	public bool Enabled { get; set; }

	public GridBasedDistributor Placer => m_Placer;

	public void Reset(bool enable)
	{
		m_Placer.NumSpawnChoices = m_Prefabs.Length;
		m_Placer.BiomeValidationFunc = BiomeCheckFunction;
		m_Placer.ClearCachedPlacements();
		Enabled = enable;
	}

	public void SpawnLandmarks(WorldTile tile)
	{
		foreach (SpawnData item in LandmarksPotentiallyOverlappingTile(tile.Coord))
		{
			SpawnData current = item;
			Vector3 scenePos = current.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			scenePos.y = Singleton.Manager<ManWorld>.inst.TileManager.GetTerrainHeightAtPosition(scenePos, out var _);
			Vector2 normalisedPosInTile = Singleton.Manager<ManWorld>.inst.TileManager.GetNormalisedPosInTile(in scenePos, tile.Coord);
			Vector3 interpolatedNormal = tile.Terrain.terrainData.GetInterpolatedNormal(normalisedPosInTile.x, normalisedPosInTile.y);
			Quaternion normalWeightedSpawnOrientation = current.prefab.terrain.GetNormalWeightedSpawnOrientation(interpolatedNormal, current.randomRotHV);
			if (Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord(in current.position) == tile.Coord)
			{
				d.Assert(current.prefab.terrain.gameObject.layer == (int)Globals.inst.layerTerrain, "Landmark" + current.prefab.terrain.name + "not flagged as Terrain");
				current.prefab.terrain.SpawnFromPrefab(tile, scenePos, normalWeightedSpawnOrientation);
			}
			SceneryBlocker sceneryBlocker = SceneryBlocker.CreateRectangularPrismBlocker(SceneryBlocker.BlockMode.Spawn, WorldPosition.FromScenePosition(in scenePos), normalWeightedSpawnOrientation, current.prefab.box.size);
			if (sceneryBlocker.OverlapsBoundsApprox(Singleton.Manager<ManWorld>.inst.TileManager.CalcMinWorldCoords(tile.Coord), Singleton.Manager<ManWorld>.inst.TileManager.CalcMaxWorldCoords(tile.Coord)))
			{
				Singleton.Manager<ManWorld>.inst.AddSceneryBlockerToTile(sceneryBlocker, tile);
			}
		}
	}

	public bool GetNearestBlocker(Vector3 worldPos, out Vector3 blockerPosWorld)
	{
		return m_Placer.GetNearestBlocker(worldPos, out blockerPosWorld);
	}

	public IEnumerable<SceneryBlocker> SceneryBlockersOverlappingWorldCoords(Vector2 worldCoordsMin, Vector2 worldCoordsMax)
	{
		foreach (SpawnData item in LandmarksPotentiallyOverlappingWorldCoords(worldCoordsMin, worldCoordsMax))
		{
			SceneryBlocker sceneryBlocker = SceneryBlocker.Create2DCircularBlocker(SceneryBlocker.BlockMode.Spawn, WorldPosition.FromScenePosition(item.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene), item.prefab.box.size.SetY(0f).magnitude * 0.5f);
			if (sceneryBlocker.OverlapsBoundsApprox(worldCoordsMin, worldCoordsMax))
			{
				yield return sceneryBlocker;
			}
		}
	}

	private static bool BiomeCheckFunction(Biome biome)
	{
		return biome.AllowLandmarks;
	}

	private void GenerateLandmarks(IntVector2 tileCoord, List<SpawnData> spawnList)
	{
		if (!Enabled || Singleton.Manager<ManWorld>.inst.GetSetPieceDataForTile(tileCoord))
		{
			return;
		}
		m_PerTilePlacementWorking.Clear();
		m_Placer.TryPlaceOnTile(tileCoord, m_PerTilePlacementWorking);
		if (m_PerTilePlacementWorking.Count == 0)
		{
			return;
		}
		foreach (GridBasedDistributor.PlacementData item2 in m_PerTilePlacementWorking)
		{
			GridBasedDistributor.PlacementData current = item2;
			SpawnData item = new SpawnData
			{
				prefab = m_Prefabs[current.spawnChoice],
				position = current.worldPos.ToVector3XZ(),
				randomRotHV = current.randomRotHV
			};
			spawnList.Add(item);
		}
	}

	private IEnumerable<SpawnData> LandmarksPotentiallyOverlappingTile(IntVector2 tileCoord)
	{
		Vector2 vector = new Vector2(Singleton.Manager<ManWorld>.inst.TileSize * 0.5f, Singleton.Manager<ManWorld>.inst.TileSize * 0.5f);
		Vector2 worldCoordMin = Singleton.Manager<ManWorld>.inst.TileManager.CalcMinWorldCoords(in tileCoord) - vector + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		Vector2 worldCoordMax = Singleton.Manager<ManWorld>.inst.TileManager.CalcMaxWorldCoords(in tileCoord) + vector + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		foreach (SpawnData item in LandmarksPotentiallyOverlappingWorldCoords(worldCoordMin, worldCoordMax))
		{
			yield return item;
		}
	}

	private IEnumerable<SpawnData> LandmarksPotentiallyOverlappingWorldCoords(Vector2 worldCoordMin, Vector2 worldCoordMax)
	{
		if (!Enabled)
		{
			yield break;
		}
		IntVector2 minTile = Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord(worldCoordMin.ToVector3XZ());
		IntVector2 maxTile = Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord(worldCoordMax.ToVector3XZ());
		int y = minTile.y;
		while (y <= maxTile.y)
		{
			int num;
			for (int x = minTile.x; x <= maxTile.x; x = num)
			{
				IntVector2 tileCoord = new IntVector2(x, y);
				m_SpawnList.Clear();
				GenerateLandmarks(tileCoord, m_SpawnList);
				foreach (SpawnData spawn in m_SpawnList)
				{
					yield return spawn;
				}
				num = x + 1;
			}
			num = y + 1;
			y = num;
		}
	}
}
