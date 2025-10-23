using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ManPath : Singleton.Manager<ManPath>, IWorldTreadmill
{
	public class SearchData : ThreadWorker.ThreadedData
	{
		public IntVector2 m_StartGridPos;

		public IntVector2 m_TargetGridPos;

		public IntVector2 m_InitialTileCoord;

		public IntVector2[] m_TargetPath;

		public WorldPosition[] m_PrefixPath;

		public int m_TechRadius;

		public bool m_SearchingFlag;

		public bool m_SearchingCancelled;

		public Event<WorldPosition[]> OnPathFound;
	}

	public bool m_UseDiagonals;

	public bool m_RenderModifiedQuadTree;

	public bool m_RenderDebugGrid;

	public bool m_RenderOnlyFilledTrees;

	public bool m_RenderStandardQuadTree;

	public bool m_RenderDebugPath;

	public Transform m_DebugStart;

	public Transform m_DebugTarget;

	public int m_DebugRadius = 1;

	private ThreadWorker m_ThreadWorker;

	private AStarPathfindingTT m_Pathfinder;

	private List<SearchData> m_SearchingList = new List<SearchData>();

	private int m_TileSize;

	private int m_GridSquareSize = 3;

	private int m_GridSize;

	private int m_TotalGridSquares;

	private IntVector2 m_FullGridSearchSize;

	private SearchData m_DebugSearchData = new SearchData();

	private IntVector2 m_DebugStartGridPos;

	private IntVector2 m_DebugTargetGridPos;

	public int GridSquareSize => m_GridSquareSize;

	public bool TryFindPath(Vector3 startPosScene, Vector3 targetPosScene, float radius, Action<WorldPosition[]> pathFoundAction)
	{
		bool result = true;
		SearchData searchData = InitSearchData(startPosScene, targetPosScene);
		if (searchData != null)
		{
			int num = Mathf.CeilToInt(radius);
			if (IsTileFree(searchData.m_StartGridPos, searchData.m_InitialTileCoord, num))
			{
				FindPath(searchData, num, pathFoundAction, null);
			}
			else
			{
				WorldPosition[] pathToNearestWalkablePos = GetPathToNearestWalkablePos(startPosScene, num);
				if (pathToNearestWalkablePos == null)
				{
					return false;
				}
				WorldPosition[] prefixPathWorldPos = new WorldPosition[1] { pathToNearestWalkablePos[0] };
				Vector3 scenePosition = pathToNearestWalkablePos[1].ScenePosition;
				searchData = InitSearchData(scenePosition, targetPosScene);
				if (searchData == null)
				{
					return false;
				}
				FindPath(searchData, num, pathFoundAction, prefixPathWorldPos);
			}
		}
		else
		{
			result = false;
		}
		return result;
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
	}

	public void Save(ManSaveGame.State saveState)
	{
	}

	public void ModeExit()
	{
		ClearPendingSearches();
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
	}

	private WorldPosition[] GetPathToNearestWalkablePos(Vector3 scenePos, int techRadius)
	{
		IntVector2 intVector = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in scenePos);
		IntVector2 intVector2 = ConvertScenePosToFullSearchSpace(scenePos, intVector, IntVector2.zero);
		IntVector2 intVector3 = FindNearestFreeTile(scenePos, intVector2, intVector, techRadius);
		WorldPosition[] result = null;
		if (intVector2 != intVector3)
		{
			result = new WorldPosition[2]
			{
				WorldPosition.FromScenePosition(GetScenePosFromFullSearchSpace(intVector2, intVector)),
				WorldPosition.FromScenePosition(GetScenePosFromFullSearchSpace(intVector3, intVector))
			};
		}
		return result;
	}

	private IntVector2 FindNearestFreeTile(Vector3 scenePos, IntVector2 fullSearchPos, IntVector2 initialTileCoord, int techRadius)
	{
		int num = 3;
		List<IntVector2> list = new List<IntVector2>();
		for (int i = 1; i <= num; i++)
		{
			for (int j = -i; j <= i; j++)
			{
				for (int k = -i; k <= i; k++)
				{
					IntVector2 intVector = fullSearchPos + new IntVector2(j, k);
					if (intVector.x >= 0 && intVector.x < m_FullGridSearchSize.x && intVector.y >= 0 && intVector.y < m_FullGridSearchSize.y && IsTileFree(intVector, initialTileCoord, techRadius))
					{
						list.Add(intVector);
					}
				}
			}
			if (list.Count > 0)
			{
				break;
			}
		}
		IntVector2 result = fullSearchPos;
		if (list.Count > 0)
		{
			float num2 = float.MaxValue;
			for (int l = 0; l < list.Count; l++)
			{
				float magnitude = (GetScenePosFromFullSearchSpace(list[l], initialTileCoord) - scenePos).magnitude;
				if (magnitude < num2)
				{
					result = list[l];
					num2 = magnitude;
				}
			}
		}
		return result;
	}

	private bool IsTileFree(IntVector2 fullSearchPos, IntVector2 initialTileCoord, int techRadius)
	{
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(GetTileCoord(fullSearchPos, initialTileCoord));
		IntVector2 localTileGridPos = GetLocalTileGridPos(fullSearchPos);
		bool result = false;
		if (worldTile != null && worldTile.m_ModifiedQuadTree.IsWalkable(localTileGridPos.x, localTileGridPos.y, techRadius))
		{
			result = true;
		}
		return result;
	}

	private SearchData InitSearchData(Vector3 startPosScene, Vector3 targetPosScene)
	{
		SearchData searchData = null;
		int num = 1;
		IntVector2 intVector = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in startPosScene);
		IntVector2 intVector2 = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in targetPosScene);
		if (intVector2.x <= intVector.x + num && intVector2.x >= intVector.x - num && intVector2.y <= intVector.y + num && intVector2.y >= intVector.y - num)
		{
			IntVector2 offsetFromCentalTile = intVector2 - intVector;
			searchData = new SearchData();
			searchData.m_InitialTileCoord = intVector;
			searchData.m_StartGridPos = ConvertScenePosToFullSearchSpace(startPosScene, intVector, IntVector2.zero);
			searchData.m_TargetGridPos = ConvertScenePosToFullSearchSpace(targetPosScene, intVector2, offsetFromCentalTile);
		}
		return searchData;
	}

	private IntVector2 ConvertScenePosToFullSearchSpace(Vector3 scenePos, IntVector2 tileCoordinateWorld, IntVector2 offsetFromCentalTile)
	{
		IntVector2 intVector = tileCoordinateWorld - Singleton.Manager<ManWorld>.inst.FloatingOriginTile;
		int num = m_TileSize / 2;
		int num2 = -num + intVector.x * m_TileSize;
		int num3 = -num + intVector.y * m_TileSize;
		Rect rect = new Rect(num2, num3, m_TileSize, m_TileSize);
		int num4 = Mathf.FloorToInt((scenePos.x - rect.x) / (float)m_GridSquareSize);
		int num5 = Mathf.FloorToInt((scenePos.z - rect.y) / (float)m_GridSquareSize);
		int x = num4 + (offsetFromCentalTile.x * m_GridSize + m_GridSize);
		num5 += offsetFromCentalTile.y * m_GridSize + m_GridSize;
		return new IntVector2(x, num5);
	}

	private void FindPath(SearchData searchData, int radius, Action<WorldPosition[]> pathFoundAction, WorldPosition[] prefixPathWorldPos)
	{
		searchData.m_TechRadius = radius;
		searchData.OnPathFound.Subscribe(pathFoundAction);
		searchData.m_PrefixPath = prefixPathWorldPos;
		searchData.m_SearchingFlag = true;
		m_SearchingList.Add(searchData);
		m_ThreadWorker.AddAction(PathFindOnThread(searchData, IntVector2.zero, m_FullGridSearchSize, m_UseDiagonals, IsModifiedQuadWalkable), null);
	}

	private IEnumerator PathFindOnThread(SearchData searchData, IntVector2 gridStartPos, IntVector2 gridSize, bool useDiagonals, Func<IntVector2, IntVector2, SearchData, bool> IsWalkable)
	{
		searchData.m_TargetPath = m_Pathfinder.DoPathfinding(searchData, gridStartPos, gridSize, useDiagonals, IsWalkable);
		lock (searchData.m_Lock)
		{
			searchData.m_SearchingFlag = false;
		}
		yield break;
	}

	private bool IsModifiedQuadWalkable(IntVector2 fromPos, IntVector2 toPos, SearchData searchData)
	{
		IntVector2 coord = GetTileCoord(fromPos, searchData.m_InitialTileCoord);
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord);
		IntVector2 coord2 = GetTileCoord(toPos, searchData.m_InitialTileCoord);
		WorldTile worldTile2 = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord2);
		IntVector2 localTileGridPos = GetLocalTileGridPos(toPos);
		bool result = false;
		if (worldTile != null && worldTile2 != null && worldTile2.m_ModifiedQuadTree.IsWalkable(localTileGridPos.x, localTileGridPos.y, searchData.m_TechRadius))
		{
			if (fromPos.x == toPos.x || fromPos.y == toPos.y)
			{
				result = true;
			}
			else
			{
				bool num = toPos.x > fromPos.x;
				bool flag = toPos.y > fromPos.y;
				IntVector2 intVector = new IntVector2(fromPos.x + 1, fromPos.y);
				IntVector2 intVector2 = new IntVector2(fromPos.x, fromPos.y + 1);
				IntVector2 intVector3 = new IntVector2(fromPos.x - 1, fromPos.y);
				IntVector2 intVector4 = new IntVector2(fromPos.x, fromPos.y - 1);
				IntVector2 coord3 = GetTileCoord(intVector, searchData.m_InitialTileCoord);
				IntVector2 coord4 = GetTileCoord(intVector2, searchData.m_InitialTileCoord);
				IntVector2 coord5 = GetTileCoord(intVector3, searchData.m_InitialTileCoord);
				IntVector2 coord6 = GetTileCoord(intVector4, searchData.m_InitialTileCoord);
				WorldTile tile = ((coord3 == coord) ? worldTile : Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord3));
				WorldTile tile2 = ((coord4 == coord) ? worldTile : Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord4));
				WorldTile tile3 = ((coord5 == coord) ? worldTile : Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord5));
				WorldTile tile4 = ((coord6 == coord) ? worldTile : Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in coord6));
				int techRadius = searchData.m_TechRadius;
				if (num)
				{
					if (flag)
					{
						if (IsTileWalkable(tile, intVector, techRadius) && IsTileWalkable(tile2, intVector2, techRadius))
						{
							result = true;
						}
					}
					else if (IsTileWalkable(tile, intVector, techRadius) && IsTileWalkable(tile4, intVector4, techRadius))
					{
						result = true;
					}
				}
				else if (flag)
				{
					if (IsTileWalkable(tile3, intVector3, techRadius) && IsTileWalkable(tile2, intVector2, techRadius))
					{
						result = true;
					}
				}
				else if (IsTileWalkable(tile3, intVector3, techRadius) && IsTileWalkable(tile4, intVector4, techRadius))
				{
					result = true;
				}
			}
		}
		return result;
	}

	private bool IsTileWalkable(WorldTile tile, IntVector2 worldPos, int techRadius)
	{
		if (tile == null)
		{
			return false;
		}
		IntVector2 localTileGridPos = GetLocalTileGridPos(worldPos);
		return tile.m_ModifiedQuadTree.IsWalkable(localTileGridPos.x, localTileGridPos.y, techRadius);
	}

	private IntVector2 GetTileCoord(IntVector2 gridPos, IntVector2 initialTilePos)
	{
		int num = 0;
		int num2 = 0;
		if (gridPos.x < m_GridSize)
		{
			num = -1;
		}
		else if (gridPos.x > 2 * m_GridSize - 1)
		{
			num = 1;
		}
		if (gridPos.y < m_GridSize)
		{
			num2 = -1;
		}
		else if (gridPos.y > 2 * m_GridSize - 1)
		{
			num2 = 1;
		}
		return new IntVector2(initialTilePos.x + num, initialTilePos.y + num2);
	}

	private IntVector2 GetLocalTileGridPos(IntVector2 gridPos)
	{
		int x = gridPos.x % m_GridSize;
		int y = gridPos.y % m_GridSize;
		return new IntVector2(x, y);
	}

	private void ConvertAndSendPath(SearchData searchData)
	{
		WorldPosition[] worldSpacePath = GetWorldSpacePath(searchData);
		searchData.OnPathFound.Send(worldSpacePath);
	}

	private WorldPosition[] GetWorldSpacePath(SearchData searchData, float yVal = 160f)
	{
		WorldPosition[] array = null;
		if (!searchData.m_SearchingCancelled && searchData.m_TargetPath != null)
		{
			int num = 0;
			int num2 = searchData.m_TargetPath.Length;
			if (searchData.m_PrefixPath != null)
			{
				num2 += searchData.m_PrefixPath.Length;
				array = new WorldPosition[num2];
				for (int i = 0; i < searchData.m_PrefixPath.Length; i++)
				{
					array[i] = searchData.m_PrefixPath[i];
				}
				num = searchData.m_PrefixPath.Length;
			}
			else
			{
				array = new WorldPosition[num2];
			}
			for (int j = 0; j < searchData.m_TargetPath.Length; j++)
			{
				int num3 = j + num;
				array[num3] = WorldPosition.FromScenePosition(GetScenePosFromFullSearchSpace(searchData.m_TargetPath[j], searchData.m_InitialTileCoord));
			}
		}
		return array;
	}

	private Vector3 GetScenePosFromFullSearchSpace(IntVector2 fullSpacePos, IntVector2 initialTileCoordWorld)
	{
		int num = m_TileSize / 2;
		int num2 = m_GridSquareSize / 2;
		IntVector2 intVector = initialTileCoordWorld - Singleton.Manager<ManWorld>.inst.FloatingOriginTile;
		int num3 = -num + intVector.x * m_TileSize;
		int num4 = -num + intVector.y * m_TileSize;
		int num5 = num3 - m_TileSize;
		int num6 = num4 - m_TileSize;
		IntVector2 intVector2 = new IntVector2(fullSpacePos.x * m_GridSquareSize + num5, fullSpacePos.y * m_GridSquareSize + num6);
		return new Vector3(intVector2.x + num2, 0f, intVector2.y + num2);
	}

	private void DrawDebugMultiTiles(IntVector2 startTileCoordinates)
	{
		for (int i = -1; i < 2; i++)
		{
			for (int j = -1; j < 2; j++)
			{
				WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(startTileCoordinates + new IntVector2(i, j));
				if (worldTile != null)
				{
					Vector3 drawOrigin = worldTile.CalcSceneOrigin();
					if (m_RenderModifiedQuadTree)
					{
						worldTile.m_ModifiedQuadTree.Draw(drawOrigin, m_RenderOnlyFilledTrees);
					}
				}
			}
		}
	}

	private void ClearPendingSearches()
	{
		for (int i = 0; i < m_SearchingList.Count; i++)
		{
			SearchData searchData = m_SearchingList[i];
			lock (searchData.m_Lock)
			{
				searchData.m_SearchingCancelled = true;
			}
		}
	}

	public void OnMoveWorldOrigin(IntVector3 amountMoved)
	{
		ClearPendingSearches();
	}

	private void Start()
	{
		m_ThreadWorker = new ThreadWorker();
		Thread thread = new Thread(m_ThreadWorker.DoWork);
		thread.Name = "ManPath";
		thread.Start();
		m_TileSize = 384;
		m_GridSize = m_TileSize / m_GridSquareSize;
		m_TotalGridSquares = 3 * m_GridSize;
		m_FullGridSearchSize = new IntVector2(m_TotalGridSquares, m_TotalGridSquares);
		m_Pathfinder = new AStarPathfindingTT(m_FullGridSearchSize);
	}

	private void Update()
	{
		for (int num = m_SearchingList.Count - 1; num >= 0; num--)
		{
			SearchData searchData = m_SearchingList[num];
			bool flag = false;
			if (Monitor.TryEnter(searchData.m_Lock))
			{
				try
				{
					if (!searchData.m_SearchingFlag)
					{
						ConvertAndSendPath(searchData);
						flag = true;
					}
				}
				finally
				{
					Monitor.Exit(searchData.m_Lock);
				}
			}
			if (flag)
			{
				searchData.OnPathFound.Clear();
				m_SearchingList.RemoveAt(num);
			}
		}
		if (m_RenderDebugGrid)
		{
			IntVector2 startTileCoordinates = ((m_DebugStart != null && m_DebugTarget != null) ? m_DebugSearchData.m_InitialTileCoord : Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(Singleton.playerPos));
			DrawDebugMultiTiles(startTileCoordinates);
		}
		if (m_DebugStart != null && m_DebugTarget != null)
		{
			SearchData searchData2 = InitSearchData(m_DebugStart.position, m_DebugTarget.position);
			if (searchData2 != null && (searchData2.m_StartGridPos != m_DebugStartGridPos || searchData2.m_TargetGridPos != m_DebugTargetGridPos))
			{
				m_DebugStartGridPos = searchData2.m_StartGridPos;
				m_DebugTargetGridPos = searchData2.m_TargetGridPos;
				m_DebugSearchData = searchData2;
				FindPath(m_DebugSearchData, m_DebugRadius, null, null);
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (!m_RenderDebugPath)
		{
			return;
		}
		WorldPosition[] worldSpacePath = GetWorldSpacePath(m_DebugSearchData);
		if (worldSpacePath != null)
		{
			for (int i = 1; i < worldSpacePath.Length; i++)
			{
				Debug.DrawLine(worldSpacePath[i - 1].ScenePosition, worldSpacePath[i].ScenePosition, Color.magenta, 0f, depthTest: false);
			}
		}
	}
}
