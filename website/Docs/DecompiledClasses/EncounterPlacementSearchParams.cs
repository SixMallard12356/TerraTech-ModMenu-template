#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class EncounterPlacementSearchParams
{
	public delegate void SearchCompleteCallback(EncounterToSpawn encounterToSpawn, bool isValidPos, WorldPosition foundPosition, Quaternion foundRotation);

	public struct SearchTile
	{
		public IntVector2 m_TileCoord;

		public bool m_HasFixedPositionForSetPiece;

		public IntVector2 m_FixedPosition;

		public int m_FixedHeading;
	}

	public EncounterToSpawn encounterToSpawn;

	public WorldPosition searchOrigin;

	public float minDistanceFromOrigin;

	public float maxDistanceFromOrigin;

	public int maxNearbyEncounters;

	internal DRNG rng = new DRNG();

	private TileSearchHandler m_NonLoadedTileSearchHandler;

	private TileSearchHandler m_LoadedTileSearchHandler;

	private List<SearchTile> searchTiles = new List<SearchTile>();

	private int currentTileIndex;

	private WorldTile currentTile;

	public bool searchStarted;

	private bool subTileIteratorInitialised;

	private TileSearchHandler tileSearchHandler;

	private SearchCompleteCallback searchCompleteCallback;

	public bool validPos;

	public WorldPosition foundPosition;

	public Quaternion foundRotation;

	private bool validNonLimiterPos;

	private WorldPosition foundNonLimiterPosition;

	private Quaternion foundNonLimiterRotation;

	private static HashSet<int> s_OverLimitAnalyticsSent;

	public bool InProgress => searchStarted;

	public bool IsFinished
	{
		get
		{
			d.Assert(searchStarted, "Search was never started!");
			return currentTileIndex >= searchTiles.Count;
		}
	}

	public IntVector2 CurrentTileCoord => searchTiles[currentTileIndex].m_TileCoord;

	public SearchTile CurrentSearchTile => searchTiles[currentTileIndex];

	public WorldTile CurrentTile
	{
		get
		{
			if (currentTile == null)
			{
				currentTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(CurrentTileCoord, allowEmpty: true);
			}
			return currentTile;
		}
	}

	public void SetSearchParams(EncounterToSpawn encounterToSpawn, Vector3 searchOriginScene, SearchCompleteCallback searchCompleteCallback, float minDistanceFromOrigin = 0f, float maxDistanceFromOrigin = 1000f, int maxNearbyEncounters = int.MaxValue)
	{
		this.encounterToSpawn = encounterToSpawn;
		searchOrigin = WorldPosition.FromScenePosition(in searchOriginScene);
		this.minDistanceFromOrigin = minDistanceFromOrigin;
		this.maxDistanceFromOrigin = maxDistanceFromOrigin;
		this.maxNearbyEncounters = maxNearbyEncounters;
		this.searchCompleteCallback = searchCompleteCallback;
		searchStarted = false;
		validPos = false;
		validNonLimiterPos = false;
		foundRotation = Quaternion.identity;
	}

	public void SetFoundPosition(WorldPosition pos, Quaternion rot, bool limiterOK)
	{
		if (limiterOK)
		{
			validPos = true;
			foundPosition = pos;
			foundRotation = rot;
		}
		if (!validNonLimiterPos)
		{
			validNonLimiterPos = true;
			foundNonLimiterPosition = pos;
			foundNonLimiterRotation = rot;
		}
	}

	public void StartSearch()
	{
		searchStarted = true;
		rng.SetSeed((uint)searchOrigin.TileCoord.x, (uint)searchOrigin.TileCoord.y, (uint)Singleton.Manager<ManWorld>.inst.SeedValue, (uint)encounterToSpawn.m_EncounterDef.GetHashCode());
		InitTileCoordsToSearch();
		currentTileIndex = 0;
	}

	public bool TryNextPosition(out Vector3 searchedPos)
	{
		bool flag;
		bool flag2;
		do
		{
			flag = (WasTileIterationStarted() ? tileSearchHandler.IsSearchTileValid() : TryStartIteratingTile());
			flag2 = flag && tileSearchHandler.MoveToNextSearchOnTile();
			if (flag2)
			{
				tileSearchHandler.EvaluateSearchPos();
			}
			else
			{
				MoveToNextTile();
			}
		}
		while (!flag && !IsFinished);
		searchedPos = (flag2 ? tileSearchHandler.GetSearchPos() : Vector3.zero);
		return flag2;
	}

	public bool TryStartIteratingTile()
	{
		if (!encounterToSpawn.m_EncounterData.m_CanSpawnOffTile || (CurrentTile != null && CurrentTile.IsLoaded && CurrentTile.HasTilePosInfo))
		{
			if (m_LoadedTileSearchHandler == null)
			{
				m_LoadedTileSearchHandler = new LoadedTileEncounterSearchHandler();
			}
			tileSearchHandler = m_LoadedTileSearchHandler;
		}
		else
		{
			if (m_NonLoadedTileSearchHandler == null)
			{
				m_NonLoadedTileSearchHandler = new NonLoadedTileEncounterSearchHandler();
			}
			tileSearchHandler = m_NonLoadedTileSearchHandler;
		}
		subTileIteratorInitialised = tileSearchHandler.InitSearchOnTile(CurrentSearchTile, this);
		return subTileIteratorInitialised;
	}

	public void MoveToNextTile()
	{
		currentTile = null;
		subTileIteratorInitialised = false;
		currentTileIndex++;
	}

	public void SendSearchCompletedEvent()
	{
		if (!validPos && validNonLimiterPos && !Singleton.Manager<ManBlockLimiter>.inst.LimiterActive)
		{
			if (Singleton.Manager<ManEncounter>.inst.TestBlockLimiterForAnalytics)
			{
				if (s_OverLimitAnalyticsSent == null)
				{
					s_OverLimitAnalyticsSent = new HashSet<int>();
				}
				s_OverLimitAnalyticsSent.Add(encounterToSpawn.m_EncounterDef.GetHashCode());
			}
			searchCompleteCallback(encounterToSpawn, validNonLimiterPos, foundNonLimiterPosition, foundNonLimiterRotation);
		}
		else
		{
			searchCompleteCallback(encounterToSpawn, validPos, foundPosition, foundRotation);
		}
	}

	public void ClearSearch()
	{
		searchTiles.Clear();
		encounterToSpawn = null;
		currentTile = null;
		subTileIteratorInitialised = false;
		searchStarted = false;
		validPos = false;
		validNonLimiterPos = false;
	}

	public bool WasTileIterationStarted()
	{
		return subTileIteratorInitialised;
	}

	private void InitTileCoordsToSearch()
	{
		float num = minDistanceFromOrigin;
		float num2 = maxDistanceFromOrigin;
		Vector3 scenePosition = searchOrigin.ScenePosition;
		float num3 = Mathf.Max(num - Singleton.Manager<ManWorld>.inst.TileSize * 0.5f, 0f);
		float num4 = num3 * num3;
		float num5 = Mathf.Max(num2 + Singleton.Manager<ManWorld>.inst.TileSize * 0.5f, 0f);
		float num6 = num5 * num5;
		Bounds sceneBounds = new Bounds(scenePosition, new Vector3(num2 * 2f, 0f, num2 * 2f));
		Singleton.Manager<ManWorld>.inst.TileManager.GetTileCoordRange(sceneBounds, out var min, out var max);
		Util.CoordIterator enumerator = new Util.CoordIterator(min, max).GetEnumerator();
		while (enumerator.MoveNext())
		{
			IntVector2 tileCoordWorld = enumerator.Current;
			float sqrMagnitude = (Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(in tileCoordWorld) - scenePosition).sqrMagnitude;
			if (sqrMagnitude < num6 && sqrMagnitude > num4)
			{
				searchTiles.Add(new SearchTile
				{
					m_TileCoord = tileCoordWorld
				});
			}
		}
		int num7 = searchTiles.Count;
		while (num7 > 1)
		{
			num7--;
			int index = rng.Range(0, num7);
			SearchTile value = searchTiles[index];
			searchTiles[index] = searchTiles[num7];
			searchTiles[num7] = value;
		}
		Encounter encounterPrefab = encounterToSpawn.m_EncounterData.m_EncounterPrefab;
		TerrainSetPiece requiredSetPiece = encounterPrefab.GetRequiredSetPiece();
		if (!requiredSetPiece.IsNotNull())
		{
			return;
		}
		if (!encounterToSpawn.m_EncounterData.m_CanSpawnOffTile)
		{
			d.LogError("Set piece terrain requires CanSpawnOffTile to be true in EncounterData.");
		}
		if (encounterPrefab.CanSpawnSetPiece && Singleton.Manager<ManWorld>.inst.CanPlaceNewTerrainSetPiece(requiredSetPiece))
		{
			for (int num8 = searchTiles.Count - 1; num8 >= 0; num8--)
			{
				if (!Singleton.Manager<ManWorld>.inst.IsTileUsableForNewSetPiece(searchTiles[num8].m_TileCoord))
				{
					searchTiles.RemoveAt(num8);
				}
			}
			searchTiles.Sort(RoughDistanceSort);
		}
		else
		{
			searchTiles.Clear();
		}
		if (!encounterPrefab.CanUseExistingSetPiece)
		{
			return;
		}
		foreach (ManWorld.TerrainSetPiecePlacement item in Singleton.Manager<ManWorld>.inst.GetSetPiecesByDistance(requiredSetPiece, searchOrigin))
		{
			IntVector2 fixedPosition = new IntVector2(Mathf.RoundToInt(item.m_WorldPosition.TileRelativePos.x), Mathf.RoundToInt(item.m_WorldPosition.TileRelativePos.z));
			searchTiles.Insert(0, new SearchTile
			{
				m_TileCoord = item.m_WorldPosition.TileCoord,
				m_HasFixedPositionForSetPiece = true,
				m_FixedPosition = fixedPosition,
				m_FixedHeading = item.m_Rotation
			});
		}
	}

	private int RoughDistanceSort(SearchTile i, SearchTile j)
	{
		int num = Mathf.FloorToInt((Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(in i.m_TileCoord) - searchOrigin.ScenePosition).magnitude / 500f);
		int value = Mathf.FloorToInt((Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(in j.m_TileCoord) - searchOrigin.ScenePosition).magnitude / 500f);
		return num.CompareTo(value);
	}
}
