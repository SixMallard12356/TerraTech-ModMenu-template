using System.Collections.Generic;
using UnityEngine;

public class NonLoadedTileEncounterSearchHandler : TileSearchHandler
{
	private static Vector3[] s_TileRelativeCoordsToTestDefault = new Vector3[4]
	{
		new Vector3(Singleton.Manager<ManWorld>.inst.TileSize * 0.25f, 0f, Singleton.Manager<ManWorld>.inst.TileSize * 0.25f),
		new Vector3(Singleton.Manager<ManWorld>.inst.TileSize * 0.75f, 0f, Singleton.Manager<ManWorld>.inst.TileSize * 0.25f),
		new Vector3(Singleton.Manager<ManWorld>.inst.TileSize * 0.25f, 0f, Singleton.Manager<ManWorld>.inst.TileSize * 0.75f),
		new Vector3(Singleton.Manager<ManWorld>.inst.TileSize * 0.75f, 0f, Singleton.Manager<ManWorld>.inst.TileSize * 0.75f)
	};

	private static Vector3[] s_TileRelativeCoordsToTestPreplacedTerrain = new Vector3[1];

	private EncounterPlacementSearchParams m_SearchParams;

	private Vector3[] m_TileRelativeCoordsToTest;

	private int m_CurrentIndex;

	private EncounterPlacementSearchParams.SearchTile m_SearchTile;

	private float minDistanceSq;

	private float maxDistanceSq;

	private bool checkLimiterCost;

	private bool checkLimiterCostHard;

	private float checkLimiterRadius;

	private int limiterMaxNearbyLimit;

	private List<SceneryBlocker> m_SceneryBlockersOnTile = new List<SceneryBlocker>();

	private WorldTile.TilePosInfo m_DummyTilePosInfo = new WorldTile.TilePosInfo();

	private List<KeyValuePair<Vector3, float>> m_TechLocationsOnTile = new List<KeyValuePair<Vector3, float>>();

	public override bool InitSearchOnTile(EncounterPlacementSearchParams.SearchTile searchTile, object context)
	{
		IntVector2 tileCoord = searchTile.m_TileCoord;
		m_SearchParams = context as EncounterPlacementSearchParams;
		m_SearchTile = searchTile;
		m_CurrentIndex = -1;
		minDistanceSq = m_SearchParams.minDistanceFromOrigin * m_SearchParams.minDistanceFromOrigin;
		maxDistanceSq = m_SearchParams.maxDistanceFromOrigin * m_SearchParams.maxDistanceFromOrigin;
		Encounter encounterPrefab = m_SearchParams.encounterToSpawn.m_EncounterData.m_EncounterPrefab;
		Vector2 vector = new Vector2(encounterPrefab.SpawnRadius, encounterPrefab.SpawnRadius);
		Vector2 vector2 = Singleton.Manager<ManWorld>.inst.TileManager.CalcMinWorldCoords(in tileCoord) + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset - vector;
		Vector2 vector3 = Singleton.Manager<ManWorld>.inst.TileManager.CalcMaxWorldCoords(in tileCoord) + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset + vector;
		m_SceneryBlockersOnTile.Clear();
		foreach (SceneryBlocker item in Singleton.Manager<ManWorld>.inst.VendorSpawner.SceneryBlockersOverlappingWorldCoords(vector2, vector3))
		{
			m_SceneryBlockersOnTile.Add(item);
		}
		foreach (SceneryBlocker item2 in Singleton.Manager<ManWorld>.inst.LandmarkSpawner.SceneryBlockersOverlappingWorldCoords(vector2, vector3))
		{
			m_SceneryBlockersOnTile.Add(item2);
		}
		m_TechLocationsOnTile.Clear();
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(m_SearchParams.CurrentTileCoord);
		if (worldTile != null && worldTile.IsLoaded)
		{
			foreach (KeyValuePair<int, Visible> item3 in worldTile.Visibles[1])
			{
				Vector3 centrePosition = item3.Value.centrePosition;
				float radius = item3.Value.Radius;
				m_TechLocationsOnTile.Add(new KeyValuePair<Vector3, float>(centrePosition, radius));
			}
		}
		else
		{
			ManSaveGame.StoredTile storedTile = Singleton.Manager<ManSaveGame>.inst.GetStoredTile(m_SearchParams.CurrentTileCoord, createNewIfNotFound: false);
			if (storedTile != null && storedTile.m_StoredVisibles.TryGetValue(1, out var value) && value != null)
			{
				foreach (ManSaveGame.StoredVisible item4 in value)
				{
					ManSaveGame.StoredTech obj = item4 as ManSaveGame.StoredTech;
					Vector3 scenePosition = obj.m_WorldPosition.ScenePosition;
					float radius2 = obj.m_TechData.Radius;
					m_TechLocationsOnTile.Add(new KeyValuePair<Vector3, float>(scenePosition, radius2));
				}
			}
		}
		bool flag = Singleton.Manager<ManEncounter>.inst.GetLimiterCostEstimateForEncounter(encounterPrefab) > 0;
		checkLimiterCost = flag && (Singleton.Manager<ManBlockLimiter>.inst.LimiterActive || Singleton.Manager<ManEncounter>.inst.TestBlockLimiterForAnalytics);
		checkLimiterCostHard = flag && Singleton.Manager<ManBlockLimiter>.inst.LimiterActive;
		checkLimiterRadius = 0f;
		limiterMaxNearbyLimit = 0;
		if (checkLimiterCost)
		{
			limiterMaxNearbyLimit = Singleton.Manager<ManEncounter>.inst.GetMaxNearbyLimitForMission(encounterPrefab);
			checkLimiterRadius = Singleton.Manager<ManEncounter>.inst.GetEncounterRadiusForLimiter(encounterPrefab);
		}
		if (searchTile.m_HasFixedPositionForSetPiece)
		{
			s_TileRelativeCoordsToTestPreplacedTerrain[0] = new Vector3(searchTile.m_FixedPosition.x, 0f, searchTile.m_FixedPosition.y);
			m_TileRelativeCoordsToTest = s_TileRelativeCoordsToTestPreplacedTerrain;
		}
		else
		{
			m_TileRelativeCoordsToTest = s_TileRelativeCoordsToTestDefault;
		}
		return true;
	}

	public override bool MoveToNextSearchOnTile()
	{
		m_CurrentIndex++;
		return m_CurrentIndex < m_TileRelativeCoordsToTest.Length;
	}

	public WorldPosition GetSearchPosWorld()
	{
		Vector3 vector = m_TileRelativeCoordsToTest[m_CurrentIndex];
		Vector3 scenePos = Singleton.Manager<ManWorld>.inst.TileManager.CalcTileOriginScene(m_SearchParams.CurrentTileCoord) + vector;
		scenePos.y = Singleton.Manager<ManWorld>.inst.TileManager.GetTerrainHeightAtPosition(scenePos, out var _);
		return WorldPosition.FromScenePosition(in scenePos);
	}

	public override Vector3 GetSearchPos()
	{
		return GetSearchPosWorld().ScenePosition;
	}

	public override bool EvaluateSearchPos()
	{
		WorldPosition searchPosWorld = GetSearchPosWorld();
		Vector3 scenePos = searchPosWorld.ScenePosition;
		Encounter encounterPrefab = m_SearchParams.encounterToSpawn.m_EncounterData.m_EncounterPrefab;
		float sqrMagnitude = (m_SearchParams.searchOrigin.GameWorldPosition - searchPosWorld.GameWorldPosition).sqrMagnitude;
		float spawnRadius = encounterPrefab.SpawnRadius;
		bool flag = sqrMagnitude >= minDistanceSq && sqrMagnitude < maxDistanceSq;
		bool flag2 = true;
		if (flag)
		{
			foreach (SceneryBlocker item in m_SceneryBlockersOnTile)
			{
				if (item.IsBlockingPos(SceneryBlocker.BlockMode.Spawn, scenePos, spawnRadius))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				TerrainSetPiece requiredSetPiece = encounterPrefab.GetRequiredSetPiece();
				if (requiredSetPiece.IsNotNull() && !m_SearchTile.m_HasFixedPositionForSetPiece)
				{
					flag = flag && Singleton.Manager<ManWorld>.inst.CanPlaceNewTerrainSetPiece(requiredSetPiece, searchPosWorld, -1) && !ManEncounterPlacement.IsOverlappingReservedSetPiece(requiredSetPiece, searchPosWorld, -1);
				}
			}
			if (flag)
			{
				ManWorld.CachedBiomeBlendWeights biomeWeightsAtScenePosition = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(scenePos);
				int numWeights = biomeWeightsAtScenePosition.NumWeights;
				for (int i = 0; i < numWeights; i++)
				{
					Biome biome = biomeWeightsAtScenePosition.Biome(i);
					float num = biomeWeightsAtScenePosition.Weight(i);
					if (biome != null && num > 0f)
					{
						BiomeTypes biomeType = biome.BiomeType;
						m_DummyTilePosInfo.biomeWeighting[(int)biomeType] += num;
						if (!m_DummyTilePosInfo.biomeList.Contains(biome))
						{
							m_DummyTilePosInfo.biomeList.Add(biome);
						}
					}
				}
				flag = m_SearchParams.encounterToSpawn.m_EncounterData.m_LocationConditions.Passes(m_DummyTilePosInfo);
				m_DummyTilePosInfo.Clear();
			}
			flag = flag && !ManEncounterPlacement.IsOverlappingSafeAreaOrEncounter(scenePos, spawnRadius) && !IsAnyTechOverlappingRange(scenePos, spawnRadius);
			if (flag && checkLimiterCost)
			{
				flag2 = Singleton.Manager<ManEncounter>.inst.GetLimiterCostOfEncountersNear(scenePos, checkLimiterRadius) <= limiterMaxNearbyLimit;
				if (!flag2 && checkLimiterCostHard)
				{
					flag = false;
				}
			}
			if (flag)
			{
				Quaternion rot = ((!m_SearchTile.m_HasFixedPositionForSetPiece) ? GetDesiredHeading(encounterPrefab, scenePos) : Quaternion.AngleAxis(m_SearchTile.m_FixedHeading, Vector3.up));
				m_SearchParams.SetFoundPosition(WorldPosition.FromScenePosition(in scenePos), rot, flag2);
			}
		}
		return flag;
	}

	public override bool IsSearchTileValid()
	{
		return true;
	}

	private bool IsAnyTechOverlappingRange(Vector3 position, float range)
	{
		bool result = false;
		foreach (KeyValuePair<Vector3, float> item in m_TechLocationsOnTile)
		{
			Vector3 key = item.Key;
			float value = item.Value;
			float sqrMagnitude = (key - position).sqrMagnitude;
			range += value;
			if (sqrMagnitude < range * range)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
