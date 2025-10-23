#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ManEncounterPlacement : Singleton.Manager<ManEncounterPlacement>, Mode.IManagerModeEvents
{
	[SerializeField]
	private int m_MaxNearbyEncounters = 3;

	[SerializeField]
	private float m_MinEncounterSpawnDistSP = 100f;

	[SerializeField]
	private float m_MaxEncounterSpawnDistSP = 800f;

	[SerializeField]
	private float m_MinEncounterSpawnDistMP = 800f;

	[SerializeField]
	private float m_MaxEncounterSpawnDistMP = 2000f;

	[SerializeField]
	private float m_MaxEncounterSpawnDistSetPiece = 1400f;

	[SerializeField]
	[Range(0f, 24f)]
	private float m_TimeOfDayToReset = 6f;

	public const int kTileSearchGridSize = 3;

	private static long kMaxSearchCostPerFrameMs = 2L;

	private Stopwatch m_SearchCostTimer = new Stopwatch();

	private Queue<EncounterPlacementSearchParams> m_EncounterPlacementSearches = new Queue<EncounterPlacementSearchParams>(2);

	private EncounterPlacementSearchParams m_AutoAcceptEncounterSearchParams = new EncounterPlacementSearchParams();

	private EncounterPlacementSearchParams m_LocalEncounterSearchParams = new EncounterPlacementSearchParams();

	private List<EncounterToSpawn> m_ReservedEncounters = new List<EncounterToSpawn>();

	private List<QueuedEncounterPlacementSearch> m_QueuedEncounterPlacementSearches = new List<QueuedEncounterPlacementSearch>();

	private int m_LastMissionRefreshDay;

	private bool m_CollectingLocalEncounters;

	private Stack<EncounterToSpawn> m_PotentialLocalCoreEncounter = new Stack<EncounterToSpawn>();

	private Dictionary<FactionSubTypes, List<EncounterToSpawn>> m_RandomEncountersToSpawn = new Dictionary<FactionSubTypes, List<EncounterToSpawn>>(default(FactionSubTypesComparer));

	private List<EncounterToSpawn> m_LocalAvailableEncounters = new List<EncounterToSpawn>();

	private EncounterCollectionParams m_CollectionParams;

	private int m_CurrentCollectionRequesterID;

	private WeightedGroup<FactionSubTypes> m_CollectingWeightedFactions;

	private int m_CollectingMaxSideMissionRetryCount;

	private List<EncounterToSpawn> m_AvailableCoreEncounters = new List<EncounterToSpawn>();

	private WeightedGroup<FactionSubTypes> m_weightedFactions;

	private Event<EncounterToSpawn> OnLocalEncounterCollected;

	private EventNoParams OnFinishedCollectingLocalEncounters;

	private float MinEncounterSpawnDist
	{
		get
		{
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				return m_MinEncounterSpawnDistSP;
			}
			return m_MinEncounterSpawnDistMP;
		}
	}

	public bool CanSpawnAutoAcceptEncounter => !m_AutoAcceptEncounterSearchParams.InProgress;

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
	}

	public float GetMaxEncounterSpawnDist(EncounterToSpawn e)
	{
		float num = (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() ? m_MaxEncounterSpawnDistMP : m_MaxEncounterSpawnDistSP);
		if ((bool)e.m_EncounterData.m_EncounterPrefab.GetRequiredSetPiece())
		{
			num = Mathf.Max(num, m_MaxEncounterSpawnDistSetPiece);
		}
		return num;
	}

	public void ClearReservedEncounters()
	{
		m_ReservedEncounters.Clear();
	}

	public List<EncounterToSpawn> GetReservedEncounters(EncounterCollectionParams collectionParams)
	{
		d.Assert(ManNetwork.IsHost, "CollectAvailableEncounters was not called from Host!");
		if ((Singleton.Manager<ManTimeOfDay>.inst.GameDay == m_LastMissionRefreshDay + 1 && (float)Singleton.Manager<ManTimeOfDay>.inst.TimeOfDay >= m_TimeOfDayToReset) || Singleton.Manager<ManTimeOfDay>.inst.GameDay > m_LastMissionRefreshDay + 1)
		{
			ClearReservedEncounters();
			m_LastMissionRefreshDay = Singleton.Manager<ManTimeOfDay>.inst.GameDay;
		}
		List<EncounterToSpawn> list = new List<EncounterToSpawn>();
		int num = 0;
		int num2 = 0;
		if (collectionParams.includeNearbyHiddenEncounters)
		{
			foreach (Encounter nearbyHiddenEncounter in GetNearbyHiddenEncounters(collectionParams))
			{
				if (list.Count >= collectionParams.maxTotalCount || num2 >= collectionParams.maxSideCount)
				{
					break;
				}
				EncounterToSpawn item = new EncounterToSpawn(nearbyHiddenEncounter.EncounterDef);
				list.Add(item);
				num2++;
			}
		}
		foreach (EncounterToSpawn reservedEncounter in m_ReservedEncounters)
		{
			float magnitude = (collectionParams.centrePosition - reservedEncounter.m_Position.ScenePosition).magnitude;
			bool num3 = magnitude > MinEncounterSpawnDist && magnitude < GetMaxEncounterSpawnDist(reservedEncounter) && !Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(reservedEncounter.m_EncounterDef);
			bool isCoreEncounter = reservedEncounter.m_EncounterDef.IsCoreEncounter;
			if (num3 && (!isCoreEncounter || !Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(reservedEncounter.m_EncounterDef)) && (!isCoreEncounter || num < collectionParams.maxCoreCount) && (isCoreEncounter || num2 < collectionParams.maxSideCount))
			{
				list.Add(reservedEncounter);
				if (isCoreEncounter)
				{
					num++;
				}
				else
				{
					num2++;
				}
				if (list.Count >= collectionParams.maxTotalCount || (num >= collectionParams.maxCoreCount && num2 >= collectionParams.maxSideCount))
				{
					break;
				}
			}
		}
		return list;
	}

	public bool IsValidListedEncounter(EncounterToSpawn encounterToSpawn)
	{
		bool flag = false;
		if (encounterToSpawn != null)
		{
			flag = IsReservedEncounter(encounterToSpawn.m_EncounterDef);
			if (!flag)
			{
				foreach (Encounter activeEncounter in Singleton.Manager<ManEncounter>.inst.ActiveEncounters)
				{
					if (activeEncounter.EncounterDef == encounterToSpawn.m_EncounterDef && !activeEncounter.VisibleInLog && activeEncounter.CanAcceptFromQuestGiver && !activeEncounter.HasNoPosition)
					{
						flag = true;
						break;
					}
				}
			}
		}
		else
		{
			d.LogError("IsValidListedEncounter was passed NULL EncounterToSpawn!");
		}
		return flag;
	}

	private bool IsReservedEncounter(EncounterIdentifier encounterDef)
	{
		return m_ReservedEncounters.Exists((EncounterToSpawn e) => e.m_EncounterDef == encounterDef);
	}

	private void CollectLocalAvailableEncounters(QueuedEncounterPlacementSearch search)
	{
		CollectLocalAvailableEncounters(search.collectionParams, search.requesterID, search.potentialCoreEncounters, search.potentialRandomEncounters, search.corpWeights, search.encounterCollectedEvent, search.finishedCollectingEvent, search.initialList);
	}

	public void CollectLocalAvailableEncounters(EncounterCollectionParams collectionParams, int requesterID, List<EncounterToSpawn> potentialCoreEncounters, Dictionary<FactionSubTypes, List<EncounterToSpawn>> potentialRandomEncounters, WeightedGroup<FactionSubTypes> corpWeights, Action<EncounterToSpawn> encounterCollectedEvent, Action finishedCollectingEvent, List<EncounterToSpawn> initialList = null)
	{
		if (m_CollectingLocalEncounters)
		{
			m_QueuedEncounterPlacementSearches.Add(new QueuedEncounterPlacementSearch
			{
				collectionParams = collectionParams,
				requesterID = requesterID,
				potentialCoreEncounters = potentialCoreEncounters,
				potentialRandomEncounters = potentialRandomEncounters,
				corpWeights = corpWeights,
				encounterCollectedEvent = encounterCollectedEvent,
				finishedCollectingEvent = finishedCollectingEvent,
				initialList = initialList
			});
			return;
		}
		d.Assert(collectionParams.maxCoreCount <= collectionParams.maxTotalCount && collectionParams.maxSideCount <= collectionParams.maxTotalCount, "ManEncounter.CollectLocalAvailableEncounters - Specified number of maxima for core and/or side missions was greater than the maximum total number of missions we wish to return!");
		m_PotentialLocalCoreEncounter.Clear();
		m_LocalAvailableEncounters.Clear();
		m_AvailableCoreEncounters = potentialCoreEncounters;
		m_RandomEncountersToSpawn = potentialRandomEncounters;
		m_CollectingWeightedFactions = corpWeights;
		if (initialList != null && initialList.Count > 0)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < initialList.Count; i++)
			{
				if (initialList[i].m_EncounterDef.IsCoreEncounter)
				{
					num++;
				}
				else
				{
					num2++;
				}
			}
			if (initialList.Count >= collectionParams.maxTotalCount && (num >= collectionParams.maxCoreCount || num2 >= collectionParams.maxSideCount))
			{
				StopCollectingLocalEncounters(requesterID);
				return;
			}
			m_LocalAvailableEncounters.AddRange(initialList);
		}
		List<EncounterToSpawn> list = new List<EncounterToSpawn>(m_AvailableCoreEncounters.Count);
		for (int j = 0; j < m_AvailableCoreEncounters.Count; j++)
		{
			EncounterToSpawn encounterToSpawn = m_AvailableCoreEncounters[j];
			if (!encounterToSpawn.m_EncounterData.m_SpawnWithoutUserAccept && encounterToSpawn.m_EncounterData.CheckSpawnConditions())
			{
				list.Add(encounterToSpawn);
			}
		}
		int num3 = 0;
		int num4 = EnumValuesIterator<FactionSubTypes>.Count - 1;
		while (list.Count > 0)
		{
			for (int num5 = list.Count - 1; num5 >= 0; num5--)
			{
				if (list[num5].m_EncounterDef.m_Corp == (FactionSubTypes)num3)
				{
					m_PotentialLocalCoreEncounter.Push(list[num5]);
					list.RemoveAt(num5);
					break;
				}
			}
			num3++;
			if (num3 > num4)
			{
				num3 = 0;
			}
		}
		m_CollectionParams = collectionParams;
		m_CurrentCollectionRequesterID = requesterID;
		m_CollectingMaxSideMissionRetryCount = m_CollectionParams.maxSideCount * 5;
		for (int k = 0; k < m_LocalAvailableEncounters.Count; k++)
		{
			if (!Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(m_LocalAvailableEncounters[k].m_EncounterDef))
			{
				ReduceWeightingForEncounter(ref m_CollectingWeightedFactions, m_LocalAvailableEncounters[k].m_EncounterDef);
			}
		}
		OnLocalEncounterCollected.Unsubscribe(encounterCollectedEvent);
		OnFinishedCollectingLocalEncounters.Unsubscribe(finishedCollectingEvent);
		OnLocalEncounterCollected.Subscribe(encounterCollectedEvent);
		OnFinishedCollectingLocalEncounters.Subscribe(finishedCollectingEvent);
		m_CollectingLocalEncounters = true;
	}

	public void StopCollectingLocalEncounters(int requesterID)
	{
		if (!m_CollectingLocalEncounters)
		{
			return;
		}
		if (m_CurrentCollectionRequesterID == requesterID)
		{
			m_CollectingLocalEncounters = false;
			m_CurrentCollectionRequesterID = int.MinValue;
			m_LocalAvailableEncounters.Clear();
			m_LocalEncounterSearchParams.ClearSearch();
			OnLocalEncounterCollected.Clear();
			OnFinishedCollectingLocalEncounters.Clear();
			return;
		}
		for (int i = 0; i < m_QueuedEncounterPlacementSearches.Count; i++)
		{
			if (m_QueuedEncounterPlacementSearches[i].requesterID == requesterID)
			{
				m_QueuedEncounterPlacementSearches.RemoveAt(i);
				break;
			}
		}
	}

	public bool SpawnAutoAcceptEncounter(EncounterToSpawn encounterToSpawn, EncounterPlacementSearchParams.SearchCompleteCallback callback)
	{
		if (!m_AutoAcceptEncounterSearchParams.InProgress)
		{
			d.Assert(encounterToSpawn.m_EncounterData.m_SpawnWithoutUserAccept, "Trying to AutoAccept an encounter that does not support it");
			if (encounterToSpawn.m_EncounterData.CheckSpawnConditions())
			{
				m_AutoAcceptEncounterSearchParams.SetSearchParams(encounterToSpawn, Singleton.playerPos, callback, MinEncounterSpawnDist, GetMaxEncounterSpawnDist(encounterToSpawn), m_MaxNearbyEncounters);
				FindPositionForEncounterAsync(m_AutoAcceptEncounterSearchParams);
				return true;
			}
		}
		return false;
	}

	private void FindPositionForEncounterAsync(EncounterPlacementSearchParams searchParams)
	{
		if (StartEncounterPlacementSearch(searchParams))
		{
			searchParams.SendSearchCompletedEvent();
			searchParams.ClearSearch();
		}
		else
		{
			m_EncounterPlacementSearches.Enqueue(searchParams);
		}
	}

	public int GetLimiterCostOfEncountersNear(Vector3 pos, float radius)
	{
		int num = Singleton.Manager<ManEncounter>.inst.GetLimiterCostOfEncountersNear(pos, radius);
		for (int i = 0; i < m_ReservedEncounters.Count; i++)
		{
			EncounterToSpawn encounterToSpawn = m_ReservedEncounters[i];
			Encounter encounterPrefab = encounterToSpawn.m_EncounterData.m_EncounterPrefab;
			num += Singleton.Manager<ManEncounter>.inst.GetLimiterCostOfEncounterNear(pos, radius, encounterPrefab, encounterToSpawn.m_Position.ScenePosition);
		}
		return num;
	}

	private bool StartEncounterPlacementSearch(EncounterPlacementSearchParams searchParams)
	{
		d.Assert(!searchParams.searchStarted, "StartEncounterPlacementSearch - Passed in params is already in progress!");
		searchParams.searchStarted = true;
		Encounter encounterPrefab = searchParams.encounterToSpawn.m_EncounterData.m_EncounterPrefab;
		if (searchParams.encounterToSpawn.m_EncounterData.m_HasNoPosition || searchParams.encounterToSpawn.m_UsePosForPlacement)
		{
			d.Assert(encounterPrefab.GetRequiredSetPiece() == null, "Encounter " + encounterPrefab.name + " has a terrain requirement, which is incompatible with EncounterData which has no position (or a fixed position).");
			Quaternion identity = Quaternion.identity;
			WorldPosition pos = ((!searchParams.encounterToSpawn.m_UsePosForPlacement) ? WorldPosition.FromScenePosition(Singleton.playerTank ? Singleton.playerPos : Mode<ModeMain>.inst.StartPositionScene) : searchParams.encounterToSpawn.m_Position);
			bool limiterOK = true;
			if ((Singleton.Manager<ManBlockLimiter>.inst.LimiterActive || Singleton.Manager<ManEncounter>.inst.TestBlockLimiterForAnalytics) && Singleton.Manager<ManEncounter>.inst.GetLimiterCostEstimateForEncounter(encounterPrefab) > 0)
			{
				int maxNearbyLimitForMission = Singleton.Manager<ManEncounter>.inst.GetMaxNearbyLimitForMission(encounterPrefab);
				if (GetLimiterCostOfEncountersNear(searchParams.foundPosition.ScenePosition, Singleton.Manager<ManEncounter>.inst.m_ExpandMissionRadiusForLimiter) > maxNearbyLimitForMission)
				{
					limiterOK = false;
				}
			}
			searchParams.SetFoundPosition(pos, identity, limiterOK);
			return true;
		}
		bool num = Singleton.Manager<ManBlockLimiter>.inst.LimiterActive && Singleton.Manager<ManEncounter>.inst.GetMaxNearbyLimitForMission(encounterPrefab) < 0;
		Vector3 scenePosition = searchParams.searchOrigin.ScenePosition;
		int numNearbyEncounters = Singleton.Manager<ManEncounter>.inst.GetNumNearbyEncounters(scenePosition);
		if (!num && Singleton.playerTank != null && numNearbyEncounters < searchParams.maxNearbyEncounters)
		{
			searchParams.StartSearch();
			return false;
		}
		return true;
	}

	private bool UpdateEncounterPlacementSearch(EncounterPlacementSearchParams searchParams)
	{
		m_SearchCostTimer.Restart();
		while (!searchParams.IsFinished && !searchParams.validPos && m_SearchCostTimer.ElapsedMilliseconds < kMaxSearchCostPerFrameMs)
		{
			searchParams.TryNextPosition(out var _);
		}
		m_SearchCostTimer.Stop();
		if (!searchParams.IsFinished)
		{
			return searchParams.validPos;
		}
		return true;
	}

	public static bool IsValidTileForEncounter(WorldTile tile, EncounterToSpawn encounterToSpawn)
	{
		bool result = false;
		if (tile != null && tile.HasTilePosInfo && (encounterToSpawn.m_EncounterData.m_IgnoreSceneryWhenSpawning || (float)tile.LargestFreeSpaceOnTile >= encounterToSpawn.m_EncounterData.m_EncounterPrefab.SpawnRadius) && encounterToSpawn.m_EncounterData.m_LocationConditions.TilePasses(tile))
		{
			result = true;
		}
		return result;
	}

	public static bool IsValidTilePositionForEncounter(WorldTile.TilePosInfo tilePosInfo, EncounterToSpawn encounterToSpawn)
	{
		bool flag = true;
		Vector3 scenePos = tilePosInfo.ScenePos;
		float spawnRadius = encounterToSpawn.m_EncounterData.m_EncounterPrefab.SpawnRadius;
		if (Singleton.Manager<ManWorld>.inst.CheckAllTilesAtPositionHaveReachedLoadStep(scenePos, spawnRadius))
		{
			flag = !(encounterToSpawn.m_EncounterData.m_IgnoreSceneryWhenSpawning ? (!Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Spawn, scenePos, spawnRadius)) : (tilePosInfo.biggestFreeRadius >= spawnRadius));
		}
		if (!flag)
		{
			float encounterRadius = encounterToSpawn.m_EncounterData.m_EncounterPrefab.EncounterRadius;
			flag = !encounterToSpawn.m_EncounterData.m_LocationConditions.Passes(tilePosInfo);
			if (!flag && encounterToSpawn.m_EncounterData.m_CameraSpawnCondition != ManSpawn.CameraSpawnConditions.Anywhere)
			{
				bool flag2 = Singleton.Manager<CameraManager>.inst.IsPosInsideCamFrustrum(scenePos);
				flag = (encounterToSpawn.m_EncounterData.m_CameraSpawnCondition == ManSpawn.CameraSpawnConditions.OffCamera && flag2) || (encounterToSpawn.m_EncounterData.m_CameraSpawnCondition == ManSpawn.CameraSpawnConditions.OnCamera && !flag2);
			}
			flag = flag || IsOverlappingSafeAreaOrEncounter(scenePos, encounterRadius);
			if (!flag && encounterToSpawn.m_EncounterData.m_EncounterPrefab.FlatTerrainRadius > 0f)
			{
				flag = !Singleton.Manager<ManWorld>.inst.TestCurvatureAndLogResult(scenePos, encounterToSpawn.m_EncounterData.m_EncounterPrefab.FlatTerrainRadius, encounterToSpawn.m_EncounterData.m_Name);
			}
			flag = flag || Singleton.Manager<ManTechs>.inst.IsAnyTechOverlappingRange(scenePos, encounterRadius);
		}
		return !flag;
	}

	public static bool IsOverlappingSafeAreaOrEncounter(Vector3 scenePos, float radius)
	{
		bool flag = false;
		Transform transToIgnore = (Singleton.playerTank ? Singleton.playerTank.trans : null);
		if (Singleton.Manager<ManFreeSpace>.inst.IsOverlappingSafeArea(scenePos, radius, transToIgnore))
		{
			return true;
		}
		return IsOverlappingEncounter(scenePos, radius);
	}

	private static void ReduceWeightingForEncounter(ref WeightedGroup<FactionSubTypes> weightedGroup, EncounterIdentifier encounterDef)
	{
		FactionSubTypes corp = encounterDef.m_Corp;
		float weight = weightedGroup.GetWeight(corp);
		if (weight != 0f)
		{
			weight *= 0.5f;
			weightedGroup.SetWeight(corp, weight);
		}
	}

	public static bool IsOverlappingReservedSetPiece(TerrainSetPiece t, WorldPosition pos, int rotation)
	{
		bool result = false;
		Singleton.Manager<ManWorld>.inst.GetTilesTouchedBySetPiece(t, pos, rotation, out var tileMin, out var tileMax);
		List<EncounterToSpawn> reservedEncounters = Singleton.Manager<ManEncounterPlacement>.inst.m_ReservedEncounters;
		for (int i = 0; i < reservedEncounters.Count; i++)
		{
			TerrainSetPiece requiredSetPiece = reservedEncounters[i].m_EncounterData.m_EncounterPrefab.GetRequiredSetPiece();
			if (requiredSetPiece.IsNotNull())
			{
				Singleton.Manager<ManWorld>.inst.GetTilesTouchedBySetPiece(requiredSetPiece, reservedEncounters[i].m_Position, reservedEncounters[i].GetRotationForSetPiece(), out var tileMin2, out var tileMax2);
				bool num = tileMin2.x <= tileMax.x && tileMax2.x >= tileMin.x;
				bool flag = tileMin2.y <= tileMax.y && tileMax2.y >= tileMin.y;
				if (num && flag)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	public static bool IsOverlappingEncounter(Vector3 scenePos, float radius, bool includeUnspawnedEncounters = true)
	{
		foreach (Encounter activeEncounter in Singleton.Manager<ManEncounter>.inst.ActiveEncounters)
		{
			if (scenePos.IsInCircle(activeEncounter.Position, activeEncounter.EncounterRadius, radius, ClampY: true))
			{
				return true;
			}
		}
		foreach (InActiveSetPieceBlocker inActiveSetPieceBlocker in Singleton.Manager<ManEncounter>.inst.InActiveSetPieceBlockers)
		{
			if (scenePos.IsInCircle(inActiveSetPieceBlocker.ScenePos, inActiveSetPieceBlocker.EncounterRadius, radius, ClampY: true))
			{
				return true;
			}
		}
		List<EncounterToSpawn> reservedEncounters = Singleton.Manager<ManEncounterPlacement>.inst.m_ReservedEncounters;
		if (includeUnspawnedEncounters && reservedEncounters.Count > 0)
		{
			for (int i = 0; i < reservedEncounters.Count; i++)
			{
				if (!Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(reservedEncounters[i].m_EncounterDef) && !reservedEncounters[i].m_EncounterData.m_HasNoPosition)
				{
					Vector3 vector = (reservedEncounters[i].m_Position.ScenePosition - scenePos).SetY(0f);
					float num = radius + reservedEncounters[i].m_EncounterData.m_EncounterPrefab.EncounterRadius;
					if (vector.sqrMagnitude < num * num)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private void ProcessCollectingLocalAvailableEncounters()
	{
		if (!m_CollectingLocalEncounters || m_LocalEncounterSearchParams.InProgress)
		{
			return;
		}
		int numNearbyEncounters = Singleton.Manager<ManEncounter>.inst.GetNumNearbyEncounters(m_CollectionParams.centrePosition);
		int num = m_MaxNearbyEncounters - numNearbyEncounters;
		int num2 = Mathf.Min(m_CollectionParams.maxTotalCount, num - m_LocalAvailableEncounters.Count);
		bool flag = false;
		if (m_PotentialLocalCoreEncounter.Count > 0)
		{
			Mathf.Min(m_CollectionParams.maxCoreCount, num2);
			if (GetNumCoreEncountersInLocalEncounters() < m_CollectionParams.maxCoreCount && num2 > 0)
			{
				EncounterToSpawn encounterToSpawn = m_PotentialLocalCoreEncounter.Pop();
				m_LocalEncounterSearchParams.SetSearchParams(encounterToSpawn, m_CollectionParams.centrePosition, OnLocalEncounterSearchComplete, MinEncounterSpawnDist, GetMaxEncounterSpawnDist(encounterToSpawn), m_MaxNearbyEncounters);
				FindPositionForEncounterAsync(m_LocalEncounterSearchParams);
				flag = true;
			}
		}
		if (flag)
		{
			return;
		}
		if (Singleton.Manager<DebugUtil>.inst.SpawnAllMissions)
		{
			m_ReservedEncounters.Clear();
			float num3 = 100f;
			foreach (List<EncounterToSpawn> value in m_RandomEncountersToSpawn.Values)
			{
				int count = value.Count;
				float num4 = (float)Math.PI * 2f / (float)count;
				for (int i = 0; i < count; i++)
				{
					EncounterToSpawn encounterToSpawn2 = new EncounterToSpawn(value[i].m_EncounterDef);
					if (!AlreadyInLocalEncounters(encounterToSpawn2))
					{
						Vector3 scenePosition = m_LocalEncounterSearchParams.searchOrigin.ScenePosition;
						encounterToSpawn2.m_Position = WorldPosition.FromScenePosition(scenePosition + new Vector3(num3 * Mathf.Cos(num4 * (float)i), 0f, num3 * Mathf.Sin(num4 * (float)i)));
						encounterToSpawn2.m_Rotation = Quaternion.identity;
						AddLocalEncounter(encounterToSpawn2);
						m_ReservedEncounters.Add(encounterToSpawn2);
					}
				}
				num3 += 50f;
			}
			OnFinishedCollectingLocalEncounters.Send();
			StopCollectingLocalEncounters(m_CurrentCollectionRequesterID);
			return;
		}
		int num5 = Mathf.Min(m_CollectionParams.maxSideCount, num2);
		EncounterToSpawn encounterToSpawn3 = null;
		if (num5 > 0 && m_CollectingMaxSideMissionRetryCount-- > 0 && m_CollectingWeightedFactions.HasWeights())
		{
			FactionSubTypes random = m_CollectingWeightedFactions.GetRandom();
			List<EncounterToSpawn> list = m_RandomEncountersToSpawn[random];
			if (list.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, list.Count);
				encounterToSpawn3 = list[index];
			}
			if (encounterToSpawn3 != null && !Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(encounterToSpawn3.m_EncounterDef))
			{
				EncounterToSpawn localEncounter = new EncounterToSpawn(encounterToSpawn3.m_EncounterDef);
				if (!AlreadyInLocalEncounters(localEncounter) && !IsReservedEncounter(encounterToSpawn3.m_EncounterDef))
				{
					m_LocalEncounterSearchParams.SetSearchParams(encounterToSpawn3, m_CollectionParams.centrePosition, OnLocalEncounterSearchComplete, MinEncounterSpawnDist, GetMaxEncounterSpawnDist(encounterToSpawn3), m_MaxNearbyEncounters);
					FindPositionForEncounterAsync(m_LocalEncounterSearchParams);
				}
			}
			return;
		}
		if (m_CollectionParams.includeNearbyHiddenEncounters)
		{
			num5 = m_CollectionParams.maxSideCount - (m_LocalAvailableEncounters.Count - GetNumCoreEncountersInLocalEncounters());
			num5 = Mathf.Min(num5, num2);
			foreach (Encounter nearbyHiddenEncounter in GetNearbyHiddenEncounters(m_CollectionParams))
			{
				if (num5 <= 0)
				{
					break;
				}
				EncounterToSpawn encounterToSpawn4 = new EncounterToSpawn(nearbyHiddenEncounter.EncounterDef);
				if (!AlreadyInLocalEncounters(encounterToSpawn4))
				{
					AddLocalEncounter(encounterToSpawn4);
					num5--;
				}
			}
		}
		OnFinishedCollectingLocalEncounters.Send();
		StopCollectingLocalEncounters(m_CurrentCollectionRequesterID);
	}

	private IEnumerable<Encounter> GetNearbyHiddenEncounters(EncounterCollectionParams collectionParams)
	{
		foreach (Encounter activeEncounter in Singleton.Manager<ManEncounter>.inst.ActiveEncounters)
		{
			if (!activeEncounter.VisibleInLog && activeEncounter.CanAcceptFromQuestGiver && !activeEncounter.HasNoPosition && (collectionParams.centrePosition - activeEncounter.Position).SetY(0f).sqrMagnitude <= collectionParams.maxNearbyHiddenEncounterRange * collectionParams.maxNearbyHiddenEncounterRange)
			{
				yield return activeEncounter;
			}
		}
	}

	private void OnLocalEncounterSearchComplete(EncounterToSpawn encounterToSpawn, bool validPos, WorldPosition foundPosition, Quaternion foundRotation)
	{
		if (validPos)
		{
			encounterToSpawn.m_Position = foundPosition;
			encounterToSpawn.m_Rotation = foundRotation;
			if (encounterToSpawn.m_EncounterDef.IsCoreEncounter && Singleton.Manager<ManProgression>.inst.IsCoreEncounterCompleted(encounterToSpawn.m_EncounterDef))
			{
				d.Log("ManEncounter placement found a position for a core encounter, but it's already been skipped: " + encounterToSpawn.m_EncounterDef);
				return;
			}
			AddLocalEncounter(encounterToSpawn);
			m_ReservedEncounters.Add(encounterToSpawn);
		}
	}

	private void AddLocalEncounter(EncounterToSpawn encounterSpawnParams)
	{
		EncounterIdentifier encounterDef = encounterSpawnParams.m_EncounterDef;
		ReduceWeightingForEncounter(ref m_CollectingWeightedFactions, encounterDef);
		m_LocalAvailableEncounters.Add(encounterSpawnParams);
		OnLocalEncounterCollected.Send(encounterSpawnParams);
	}

	private bool AlreadyInLocalEncounters(EncounterToSpawn localEncounter)
	{
		bool flag = false;
		for (int i = 0; i < m_LocalAvailableEncounters.Count; i++)
		{
			flag = m_LocalAvailableEncounters[i].m_EncounterDef == localEncounter.m_EncounterDef;
			if (flag)
			{
				break;
			}
		}
		return flag;
	}

	private int GetNumCoreEncountersInLocalEncounters()
	{
		int num = 0;
		for (int i = 0; i < m_LocalAvailableEncounters.Count; i++)
		{
			if (m_LocalAvailableEncounters[i].m_EncounterDef.IsCoreEncounter && !Singleton.Manager<ManEncounter>.inst.IsActiveEncounter(m_LocalAvailableEncounters[i].m_EncounterDef))
			{
				num++;
			}
		}
		return num;
	}

	public bool IsCollectingEncountersForRequester(int requesterID)
	{
		bool result = false;
		if (m_CollectingLocalEncounters)
		{
			if (m_CurrentCollectionRequesterID == requesterID)
			{
				result = true;
			}
			else
			{
				for (int i = 0; i < m_QueuedEncounterPlacementSearches.Count; i++)
				{
					if (m_QueuedEncounterPlacementSearches[i].requesterID == requesterID)
					{
						result = true;
						break;
					}
				}
			}
		}
		return result;
	}

	private void OnModeExit(Mode mode)
	{
		d.Assert(!m_CollectingLocalEncounters && m_QueuedEncounterPlacementSearches.Count == 0, "ManEncounterPlacement.OnModeExit - Still collecting local encounters!? Should have been stopped by now!");
	}

	private void Load(ManEncounter.SaveData data)
	{
		if (data != null)
		{
			m_LastMissionRefreshDay = data.m_LastMissionRefreshDay;
		}
	}

	private void Save(ManEncounter.SaveData data)
	{
		data.m_LastMissionRefreshDay = m_LastMissionRefreshDay;
	}

	private void OnEncounterAccepted(EncounterToSpawn encounterToSpawn)
	{
		int num = m_ReservedEncounters.RemoveAll((EncounterToSpawn spawnParams) => spawnParams.m_EncounterDef == encounterToSpawn.m_EncounterDef && spawnParams.m_Position.TileCoord == encounterToSpawn.m_Position.TileCoord);
		d.Assert(num > 0, $"Failed to remove encounter {encounterToSpawn} from m_ReservedEncounters. That's unexpected");
		d.Assert(num < 2, $"Remove more than 1 reserved encounter {encounterToSpawn} that was matched by Identifier + TileCoord. That's unexpected");
	}

	private void OnEncounterCompleted(Encounter encounter, bool success)
	{
		if (success && encounter.EncounterDef.IsCoreEncounter)
		{
			m_ReservedEncounters.RemoveAll((EncounterToSpawn spawnParams) => spawnParams.m_EncounterDef == encounter.EncounterDef);
		}
	}

	public void RemoveSkippedCoreEncounter(EncounterIdentifier encounterDef)
	{
		m_ReservedEncounters.RemoveAll((EncounterToSpawn spawnParams) => spawnParams.m_EncounterDef == encounterDef);
	}

	private void Start()
	{
		m_weightedFactions.Weights = new Dictionary<FactionSubTypes, float>(default(FactionSubTypesComparer));
		Singleton.Manager<ManEncounter>.inst.SaveEvent.Subscribe(Save);
		Singleton.Manager<ManEncounter>.inst.LoadEvent.Subscribe(Load);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(OnModeExit);
		Singleton.Manager<ManEncounter>.inst.EncounterAcceptedEvent.Subscribe(OnEncounterAccepted);
		Singleton.Manager<ManEncounter>.inst.EncounterCompletedEvent.Subscribe(OnEncounterCompleted);
	}

	private void Update()
	{
		if (!Singleton.Manager<ManEncounter>.inst.UpdateEnabled)
		{
			return;
		}
		if (m_EncounterPlacementSearches.Count > 0)
		{
			EncounterPlacementSearchParams encounterPlacementSearchParams = m_EncounterPlacementSearches.Peek();
			if (!encounterPlacementSearchParams.InProgress)
			{
				m_EncounterPlacementSearches.Dequeue();
			}
			else if (UpdateEncounterPlacementSearch(encounterPlacementSearchParams))
			{
				m_EncounterPlacementSearches.Dequeue();
				encounterPlacementSearchParams.SendSearchCompletedEvent();
				encounterPlacementSearchParams.ClearSearch();
			}
		}
		ProcessCollectingLocalAvailableEncounters();
		if (!m_CollectingLocalEncounters && m_QueuedEncounterPlacementSearches.Count > 0)
		{
			QueuedEncounterPlacementSearch search = m_QueuedEncounterPlacementSearches[0];
			m_QueuedEncounterPlacementSearches.RemoveAt(0);
			CollectLocalAvailableEncounters(search);
		}
	}
}
