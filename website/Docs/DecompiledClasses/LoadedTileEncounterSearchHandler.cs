#define UNITY_EDITOR
using UnityEngine;

public class LoadedTileEncounterSearchHandler : TileSearchHandler
{
	private WorldTile currentTile;

	private WorldTile.TileInfoIterator tilePosIterator;

	private EncounterPlacementSearchParams m_SearchParams;

	private Encounter encounterPrefab;

	private float minDistanceSq;

	private float maxDistanceSq;

	private bool checkLimiterCost;

	private bool checkLimiterCostHard;

	private float checkLimiterRadius;

	private int limiterMaxNearbyLimit;

	private int maxPointsToCheck;

	public override bool InitSearchOnTile(EncounterPlacementSearchParams.SearchTile searchTile, object context)
	{
		IntVector2 tileCoord = searchTile.m_TileCoord;
		m_SearchParams = context as EncounterPlacementSearchParams;
		d.Assert(tileCoord == m_SearchParams.CurrentTileCoord && (m_SearchParams.CurrentTile == null || tileCoord == m_SearchParams.CurrentTile.Coord), "Missmatched coordinate in InitSearchOnTile");
		bool num = ManEncounterPlacement.IsValidTileForEncounter(m_SearchParams.CurrentTile, m_SearchParams.encounterToSpawn);
		if (num)
		{
			int max = (int)Singleton.Manager<ManWorld>.inst.TileSize;
			Vector2 vector = new Vector2(m_SearchParams.rng.Range(0, max), m_SearchParams.rng.Range(0, max));
			bool iterateForward = m_SearchParams.rng.Range(0, 2) == 0;
			maxPointsToCheck = int.MaxValue;
			if (searchTile.m_HasFixedPositionForSetPiece)
			{
				vector = searchTile.m_FixedPosition;
				maxPointsToCheck = 1;
			}
			tilePosIterator = m_SearchParams.CurrentTile.IterateTileInfo(vector, iterateForward);
			encounterPrefab = m_SearchParams.encounterToSpawn.m_EncounterData.m_EncounterPrefab;
			d.Assert(!m_SearchParams.encounterToSpawn.m_EncounterData.m_CanSpawnOffTile || (encounterPrefab.PlacementRefiner == null && encounterPrefab.ChallengeData == null && m_SearchParams.encounterToSpawn.m_EncounterData.m_IgnoreSceneryWhenSpawning), "PlacementRefiners aren't currently supported for encounters outside loaded tiles, nor are scenery checks!");
			if (encounterPrefab.GetRequiredSetPiece().IsNotNull())
			{
				d.Assert(encounterPrefab.PlacementRefiner == null, "PlacementRefiners are not supported for encounters with set piece terrain");
				d.Assert(m_SearchParams.encounterToSpawn.m_EncounterData.m_CanSpawnOffTile || !encounterPrefab.CanSpawnSetPiece, "Encounters which can spawn terrain must be set to allow spawning off loaded tiles");
				d.Assert(encounterPrefab.CanUseExistingSetPiece || encounterPrefab.CanSpawnSetPiece, "Encounters which need set piece terrain must be have at least one of CanUseExistingSetPiece and CanSpawnSetPiece");
			}
			minDistanceSq = m_SearchParams.minDistanceFromOrigin * m_SearchParams.minDistanceFromOrigin;
			maxDistanceSq = m_SearchParams.maxDistanceFromOrigin * m_SearchParams.maxDistanceFromOrigin;
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
		}
		return num;
	}

	public override bool MoveToNextSearchOnTile()
	{
		if (maxPointsToCheck == 0)
		{
			return false;
		}
		maxPointsToCheck--;
		return tilePosIterator.MoveNext();
	}

	public override Vector3 GetSearchPos()
	{
		return tilePosIterator.Current.ScenePos;
	}

	public override bool EvaluateSearchPos()
	{
		WorldTile.TilePosInfo current = tilePosIterator.Current;
		Vector3 scenePos = current.ScenePos;
		bool flag;
		if (flag = (m_SearchParams.searchOrigin.ScenePosition - scenePos).sqrMagnitude >= minDistanceSq && ManEncounterPlacement.IsValidTilePositionForEncounter(current, m_SearchParams.encounterToSpawn))
		{
			Vector3 scenePos2 = scenePos;
			Quaternion quaternion = Quaternion.identity;
			bool flag2 = true;
			if (flag && checkLimiterCost)
			{
				flag2 = Singleton.Manager<ManEncounter>.inst.GetLimiterCostOfEncountersNear(scenePos2, checkLimiterRadius) <= limiterMaxNearbyLimit;
				if (!flag2 && checkLimiterCostHard)
				{
					flag = false;
				}
			}
			if (flag && encounterPrefab.PlacementRefiner != null)
			{
				if (encounterPrefab.PlacementRefiner.TryFindExactSpawnTransform(scenePos, out var outPos, out var outRotation))
				{
					scenePos2 = outPos;
					quaternion = outRotation;
				}
				else
				{
					flag = false;
				}
			}
			if (flag && encounterPrefab.HasPreferredPlayerApproachDirection && encounterPrefab.PlacementRefiner == null)
			{
				quaternion = GetDesiredHeading(encounterPrefab, scenePos2);
			}
			if (flag && encounterPrefab.ChallengeData != null)
			{
				flag = encounterPrefab.ChallengeData.IsValidPlacementForEncounter(scenePos2, quaternion, m_SearchParams.encounterToSpawn);
			}
			if (flag)
			{
				m_SearchParams.SetFoundPosition(WorldPosition.FromScenePosition(in scenePos2), quaternion, flag2);
			}
		}
		return flag;
	}

	public override bool IsSearchTileValid()
	{
		if (m_SearchParams.CurrentTile != null)
		{
			return m_SearchParams.CurrentTile.HasTilePosInfo;
		}
		return false;
	}
}
