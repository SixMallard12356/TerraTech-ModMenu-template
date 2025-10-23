using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vendor Spawner", menuName = "Asset/Vendor Spawner")]
public class VendorSpawner : ScriptableObject
{
	[SerializeField]
	private TankPreset m_VendorPreset;

	[SerializeField]
	private TankPreset m_VendorPresetCoop;

	[SerializeField]
	private BoxCollider m_SceneryBlockerCollider;

	[SerializeField]
	private GridBasedDistributor m_Placer;

	private List<GridBasedDistributor.PlacementData> m_PerTilePlacementWorking = new List<GridBasedDistributor.PlacementData>();

	private List<ManSpawn.TankSpawnParams> m_SpawnList = new List<ManSpawn.TankSpawnParams>();

	private PlacementBlocker m_PlacementBlocker;

	private float m_PlacementBlockerSeparation;

	public bool Enabled { get; set; }

	public GridBasedDistributor Placer => m_Placer;

	public void Reset(bool enable)
	{
		m_Placer.NumSpawnChoices = 1;
		m_Placer.BiomeValidationFunc = BiomeCheckFunction;
		m_Placer.ClearCachedPlacements();
		Enabled = enable;
		m_Placer.BlockedFn = CheckVendorBlockedAtWorldPos;
		m_Placer.PerformVendorLookupCheck = true;
	}

	public void SpawnVendors(WorldTile tile)
	{
		if (!Enabled || !ManNetwork.IsHostOrWillBe)
		{
			return;
		}
		m_SpawnList.Clear();
		GenerateSpawnData(tile.Coord, m_SpawnList);
		foreach (ManSpawn.TankSpawnParams spawn in m_SpawnList)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(spawn, addToObjectManager: true);
			trackedVisible.RadarType = RadarTypes.Vendor;
			Tank tank = trackedVisible.visible.tank;
			tank.SetInvulnerable(invulnerable: true, forever: true);
			tank.Holders.SetHeartbeatSpeed(TechHolders.HeartbeatSpeed.Vendor);
		}
	}

	public void SpawnOverlappingSceneryBlockers(WorldTile tile)
	{
		foreach (SceneryBlocker item in SceneryBlockersOnTile(tile.Coord))
		{
			Singleton.Manager<ManWorld>.inst.AddSceneryBlockerToTile(item, tile);
		}
	}

	public static TankPreset GetVendorForGameMode()
	{
		if (Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
		{
			return Singleton.Manager<ManWorld>.inst.VendorSpawner.m_VendorPresetCoop;
		}
		return Singleton.Manager<ManWorld>.inst.VendorSpawner.m_VendorPreset;
	}

	public static Visible ReplaceVendor(ManSaveGame.StoredVisible storedVis)
	{
		ManSaveGame.StoredTech storedTech = storedVis as ManSaveGame.StoredTech;
		ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
		{
			techData = GetVendorForGameMode().GetTechDataFormatted(),
			placement = ManSpawn.TankSpawnParams.Placement.BaseCentredAtPosition,
			grounded = storedTech.m_Grounded,
			position = storedTech.GetBackwardsCompatiblePosition(),
			rotation = storedTech.m_Rotation,
			teamID = storedTech.m_TeamID,
			forceSpawn = true
		};
		using (new ManSaveGame.OverrideNextVisibleIDHelper(storedTech.m_ID))
		{
			Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: false);
			tank.SetInvulnerable(invulnerable: true, forever: true);
			tank.Holders.SetHeartbeatSpeed(TechHolders.HeartbeatSpeed.Vendor);
			return tank.visible;
		}
	}

	public bool TryFindNearestVendorPos(Vector3 worldPos, out Vector3 nearestVendorPosWorld)
	{
		nearestVendorPosWorld = Vector3.zero;
		bool searchOriginCell = true;
		return m_Placer.TryFindNearestSpawnPos(worldPos, out nearestVendorPosWorld, searchOriginCell);
	}

	public void ListNearbyVendors(Vector3 worldPos, int searchSize, List<Vector3> list)
	{
		m_Placer.ListNearbySpawnPositions(worldPos, searchSize, list);
	}

	public void ListVendorsInRange(Vector3 worldPos, float radius, List<Vector3> list)
	{
		m_Placer.ListSpawnPositionsInRange(worldPos, radius, list);
	}

	public bool TryFindNearestNeighbouringVendorPos(Vector3 worldPos, out Vector3 nearestNeighbouringVendorPos)
	{
		bool result = false;
		nearestNeighbouringVendorPos = Vector3.zero;
		if (TryFindNearestVendorPos(worldPos, out var nearestVendorPosWorld))
		{
			bool searchOriginCell = false;
			result = m_Placer.TryFindNearestSpawnPos(nearestVendorPosWorld, out nearestNeighbouringVendorPos, searchOriginCell);
		}
		return result;
	}

	public void SetPlacementBlocker(PlacementBlocker blocker, float minSeparation)
	{
		m_PlacementBlocker = blocker;
		m_PlacementBlockerSeparation = minSeparation;
	}

	public bool CheckVendorBlockedAtWorldPos(Vector3 worldPos)
	{
		bool flag = false;
		if (!flag && m_PlacementBlocker != null && m_PlacementBlocker.GetNearestBlocker(worldPos, out var blockerPos))
		{
			Vector3 vector = blockerPos - worldPos;
			if (Mathf.Abs(vector.x) <= m_PlacementBlockerSeparation && Mathf.Abs(vector.z) <= m_PlacementBlockerSeparation)
			{
				flag = true;
			}
		}
		return flag;
	}

	public IEnumerable<SceneryBlocker> SceneryBlockersOnTile(IntVector2 tileCoord)
	{
		Vector2 worldCoordMin = Singleton.Manager<ManWorld>.inst.TileManager.CalcMinWorldCoords(in tileCoord) + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		Vector2 worldCoordMax = Singleton.Manager<ManWorld>.inst.TileManager.CalcMaxWorldCoords(in tileCoord) + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		foreach (SceneryBlocker item in SceneryBlockersOverlappingWorldCoords(worldCoordMin, worldCoordMax))
		{
			yield return item;
		}
	}

	public IEnumerable<SceneryBlocker> SceneryBlockersOverlappingWorldCoords(Vector2 worldCoordMin, Vector2 worldCoordMax)
	{
		if (!Enabled)
		{
			yield break;
		}
		Vector2 vector = m_SceneryBlockerCollider.size.ToVector2XZ();
		IntVector2 minTile = Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord((worldCoordMin - vector).ToVector3XZ());
		IntVector2 maxTile = Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord((worldCoordMax + vector).ToVector3XZ());
		int y = minTile.y;
		while (y <= maxTile.y)
		{
			int num;
			for (int x = minTile.x; x <= maxTile.x; x = num)
			{
				IntVector2 tileCoord = new IntVector2(x, y);
				m_SpawnList.Clear();
				GenerateSpawnData(tileCoord, m_SpawnList);
				foreach (ManSpawn.TankSpawnParams spawn in m_SpawnList)
				{
					ManSpawn.TankSpawnParams current = spawn;
					SceneryBlocker sceneryBlocker = SceneryBlocker.CreateRectangularPrismBlocker(SceneryBlocker.BlockMode.Spawn, WorldPosition.FromScenePosition(in current.position), current.rotation, m_SceneryBlockerCollider.size);
					if (sceneryBlocker.OverlapsBoundsApprox(worldCoordMin, worldCoordMax))
					{
						yield return sceneryBlocker;
					}
				}
				num = x + 1;
			}
			num = y + 1;
			y = num;
		}
	}

	private static bool BiomeCheckFunction(Biome biome)
	{
		return biome.AllowVendors;
	}

	private void GenerateSpawnData(IntVector2 tileCoord, List<ManSpawn.TankSpawnParams> spawnList)
	{
		if (!Enabled)
		{
			return;
		}
		m_PerTilePlacementWorking.Clear();
		m_Placer.TryPlaceOnTile(tileCoord, m_PerTilePlacementWorking);
		if (m_PerTilePlacementWorking.Count != 0)
		{
			for (int i = 0; i < m_PerTilePlacementWorking.Count; i++)
			{
				GridBasedDistributor.PlacementData placementData = m_PerTilePlacementWorking[i];
				Vector3 position = placementData.worldPos.ToVector3XZ() + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
				spawnList.Add(new ManSpawn.TankSpawnParams
				{
					techData = GetVendorForGameMode().GetTechDataFormatted(),
					placement = ManSpawn.TankSpawnParams.Placement.BaseCentredAtPosition,
					grounded = true,
					position = position,
					rotation = Quaternion.Euler(0f, m_PerTilePlacementWorking[i].randomRotHV.y * 360f, 0f),
					teamID = -2,
					forceSpawn = true
				});
			}
		}
	}
}
