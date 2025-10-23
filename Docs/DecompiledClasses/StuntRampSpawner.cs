using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StuntRamp Spawner", menuName = "Asset/StuntRamp Spawner")]
public class StuntRampSpawner : ScriptableObject, PlacementBlocker
{
	private struct SpawnData
	{
		public TerrainObject prefabObj;

		public Vector3 position;

		public Vector2 randomRotHV;
	}

	[SerializeField]
	private TerrainObject[] m_PrefabObjects;

	[SerializeField]
	private GridBasedDistributor m_Placer;

	private List<GridBasedDistributor.PlacementData> m_PerTilePlacementWorking = new List<GridBasedDistributor.PlacementData>();

	private List<SpawnData> m_SpawnList = new List<SpawnData>();

	public bool m_DebugAllowStuntRampsInAllBiomes;

	public bool Enabled { get; set; }

	public void Reset(bool enable)
	{
		m_Placer.NumSpawnChoices = m_PrefabObjects.Length;
		m_Placer.BiomeValidationFunc = BiomeCheckFunction;
		m_Placer.ClearCachedPlacements();
		Enabled = enable;
	}

	public void SpawnStuntRamps(WorldTile tile)
	{
		if (!Enabled)
		{
			return;
		}
		m_SpawnList.Clear();
		GenerateRoughPositions(tile.Coord, m_SpawnList);
		foreach (SpawnData spawn in m_SpawnList)
		{
			Vector3 scenePos = spawn.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			Vector2 normalisedPosInTile = Singleton.Manager<ManWorld>.inst.TileManager.GetNormalisedPosInTile(in scenePos, tile.Coord);
			scenePos.y = tile.WorldOrigin.y + tile.BiomeMapData.terrainData.GetInterpolatedHeight(normalisedPosInTile.x, normalisedPosInTile.y);
			bool flag = true;
			Vector3 scenePos2 = scenePos;
			Quaternion rot = Quaternion.identity;
			PlacementRefiner component = spawn.prefabObj.GetComponent<PlacementRefiner>();
			if (component != null)
			{
				if (component.TryFindExactSpawnTransform(scenePos, out var outPos, out var outRotation))
				{
					flag = true;
					scenePos2 = outPos;
					rot = outRotation;
				}
				else
				{
					flag = false;
				}
			}
			if (flag)
			{
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in scenePos2);
				if (worldTile != null)
				{
					spawn.prefabObj.SpawnFromPrefab(worldTile, scenePos2, rot);
				}
			}
		}
	}

	public bool GetNearestBlocker(Vector3 worldPos, out Vector3 blockerPos)
	{
		return m_Placer.GetNearestBlocker(worldPos, out blockerPos);
	}

	private bool BiomeCheckFunction(Biome biome)
	{
		if (!biome.AllowStuntRamps)
		{
			return m_DebugAllowStuntRampsInAllBiomes;
		}
		return true;
	}

	private void GenerateRoughPositions(IntVector2 tileCoord, List<SpawnData> spawnList)
	{
		if (!Enabled)
		{
			return;
		}
		m_PerTilePlacementWorking.Clear();
		m_Placer.TryPlaceOnTile(tileCoord, m_PerTilePlacementWorking);
		if (m_PerTilePlacementWorking.Count == 0)
		{
			return;
		}
		foreach (GridBasedDistributor.PlacementData item in m_PerTilePlacementWorking)
		{
			GridBasedDistributor.PlacementData current = item;
			spawnList.Add(new SpawnData
			{
				prefabObj = m_PrefabObjects[current.spawnChoice],
				position = current.worldPos.ToVector3XZ(),
				randomRotHV = current.randomRotHV
			});
		}
	}
}
