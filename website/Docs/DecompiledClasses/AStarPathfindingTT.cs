using System;

public class AStarPathfindingTT
{
	private IntVector2 m_CurrentTilePos;

	private int m_OnClosedListVal;

	private int m_OnOpenListVal;

	private int[] m_OpenList;

	private IntVector2[] m_OpenPos;

	private int[] m_TotalCost;

	private int[] m_HeuristicCost;

	private int[,] m_WhichPathingList;

	private IntVector2[,] m_ParentPosArray;

	private int[,] m_MovementCost;

	private int m_NewListPos;

	private int m_CurrentListPos;

	private int m_Temp;

	private int m_Index;

	private int m_NoOfOpenListItems;

	private int m_AddedGCost;

	private int m_TempGcost;

	private IntVector2[,] m_PathNodePosArray;

	private static readonly int kSearchingForPath = 0;

	private static readonly int kPathFound = 1;

	private static readonly int kNonExistantPath = 2;

	private static readonly int kNumTilesDiagonal = 8;

	private static readonly int kNumTilesNonDiagonal = 4;

	private static readonly int kGCostNonDiagonal = 10;

	private static readonly int kGCostDiagonal = 14;

	private static readonly int kClosedListMaxValSize = 1000000;

	private static readonly int kInitialOnClosedListVal = 10;

	private static readonly int kInitialOnOpenListVal = 0;

	private static readonly IntVector2[] m_SurroundingTileOffsetArray = new IntVector2[8]
	{
		new IntVector2(-1, 0),
		new IntVector2(0, -1),
		new IntVector2(1, 0),
		new IntVector2(0, 1),
		new IntVector2(-1, 1),
		new IntVector2(-1, -1),
		new IntVector2(1, -1),
		new IntVector2(1, 1)
	};

	public AStarPathfindingTT(IntVector2 maxGridSize)
	{
		m_OnClosedListVal = kInitialOnClosedListVal;
		m_OnOpenListVal = kInitialOnOpenListVal;
		m_OpenList = new int[maxGridSize.x * maxGridSize.y + 2];
		m_WhichPathingList = new int[maxGridSize.x + 1, maxGridSize.y + 1];
		m_OpenPos = new IntVector2[maxGridSize.x * maxGridSize.y + 2];
		m_ParentPosArray = new IntVector2[maxGridSize.x + 1, maxGridSize.y + 1];
		m_TotalCost = new int[maxGridSize.x * maxGridSize.y + 2];
		m_MovementCost = new int[maxGridSize.x + 1, maxGridSize.y + 1];
		m_HeuristicCost = new int[maxGridSize.x * maxGridSize.y + 2];
		m_PathNodePosArray = new IntVector2[maxGridSize.x + 1, maxGridSize.y + 1];
		for (int i = 0; i <= maxGridSize.x; i++)
		{
			for (int j = 0; j <= maxGridSize.y; j++)
			{
				m_PathNodePosArray[i, j] = new IntVector2(i, j);
			}
		}
	}

	public IntVector2[] DoPathfinding(ManPath.SearchData searchData, IntVector2 gridStartPos, IntVector2 gridSize, bool useDiagonals, Func<IntVector2, IntVector2, ManPath.SearchData, bool> IsWalkable)
	{
		IntVector2 startGridPos = searchData.m_StartGridPos;
		IntVector2 targetGridPos = searchData.m_TargetGridPos;
		int num = kSearchingForPath;
		int num2 = 0;
		if (m_OnClosedListVal > kClosedListMaxValSize)
		{
			Array.Clear(m_WhichPathingList, 0, m_WhichPathingList.Length);
			m_OnClosedListVal = kInitialOnClosedListVal;
		}
		m_OnClosedListVal += 2;
		m_OnOpenListVal = m_OnClosedListVal - 1;
		m_NoOfOpenListItems = 1;
		m_OpenList[1] = 1;
		m_OpenPos[1] = startGridPos;
		while (!searchData.m_SearchingCancelled)
		{
			if (m_NoOfOpenListItems != 0)
			{
				m_CurrentTilePos = m_OpenPos[m_OpenList[1]];
				m_WhichPathingList[m_CurrentTilePos.x, m_CurrentTilePos.y] = m_OnClosedListVal;
				m_OpenList[1] = m_OpenList[m_NoOfOpenListItems];
				m_NoOfOpenListItems--;
				m_NewListPos = 1;
				while (!searchData.m_SearchingCancelled)
				{
					m_CurrentListPos = m_NewListPos;
					int num3 = 2 * m_CurrentListPos;
					int num4 = num3 + 1;
					if (num4 <= m_NoOfOpenListItems)
					{
						if (m_TotalCost[m_OpenList[m_CurrentListPos]] >= m_TotalCost[m_OpenList[num3]])
						{
							m_NewListPos = num3;
						}
						if (m_TotalCost[m_OpenList[m_NewListPos]] >= m_TotalCost[m_OpenList[num4]])
						{
							m_NewListPos = num4;
						}
					}
					else if (num3 <= m_NoOfOpenListItems && m_TotalCost[m_OpenList[m_CurrentListPos]] >= m_TotalCost[m_OpenList[num3]])
					{
						m_NewListPos = num3;
					}
					if (m_CurrentListPos == m_NewListPos)
					{
						break;
					}
					m_Temp = m_OpenList[m_CurrentListPos];
					m_OpenList[m_CurrentListPos] = m_OpenList[m_NewListPos];
					m_OpenList[m_NewListPos] = m_Temp;
				}
				int num5 = (useDiagonals ? kNumTilesDiagonal : kNumTilesNonDiagonal);
				for (int i = 0; i < num5; i++)
				{
					if (searchData.m_SearchingCancelled)
					{
						break;
					}
					IntVector2 intVector = m_CurrentTilePos + m_SurroundingTileOffsetArray[i];
					if (intVector.x < gridStartPos.x || intVector.x >= gridSize.x || intVector.y < gridStartPos.y || intVector.y >= gridSize.y || !IsWalkable(m_CurrentTilePos, intVector, searchData) || m_WhichPathingList[intVector.x, intVector.y] == m_OnClosedListVal)
					{
						continue;
					}
					m_AddedGCost = ((i >= kNumTilesNonDiagonal) ? kGCostDiagonal : kGCostNonDiagonal);
					if (m_WhichPathingList[intVector.x, intVector.y] != m_OnOpenListVal)
					{
						num2++;
						m_Index = m_NoOfOpenListItems + 1;
						m_OpenList[m_Index] = num2;
						m_OpenPos[num2] = intVector;
						m_MovementCost[intVector.x, intVector.y] = m_MovementCost[m_CurrentTilePos.x, m_CurrentTilePos.y] + m_AddedGCost;
						m_HeuristicCost[m_OpenList[m_Index]] = (int)CalculateHeuristicCost(intVector, targetGridPos);
						m_TotalCost[m_OpenList[m_Index]] = m_MovementCost[intVector.x, intVector.y] + m_HeuristicCost[m_OpenList[m_Index]];
						m_ParentPosArray[intVector.x, intVector.y] = m_CurrentTilePos;
						while (m_Index != 1 && m_TotalCost[m_OpenList[m_Index]] <= m_TotalCost[m_OpenList[m_Index / 2]])
						{
							m_Temp = m_OpenList[m_Index / 2];
							m_OpenList[m_Index / 2] = m_OpenList[m_Index];
							m_OpenList[m_Index] = m_Temp;
							m_Index /= 2;
						}
						m_NoOfOpenListItems++;
						m_WhichPathingList[intVector.x, intVector.y] = m_OnOpenListVal;
						continue;
					}
					m_TempGcost = m_MovementCost[m_CurrentTilePos.x, m_CurrentTilePos.y] + m_AddedGCost;
					m_TotalCost[m_OpenList[m_Index]] = m_MovementCost[intVector.x, intVector.y] + m_HeuristicCost[m_OpenList[m_Index]];
					if (m_TempGcost >= m_MovementCost[intVector.x, intVector.y])
					{
						continue;
					}
					m_ParentPosArray[intVector.x, intVector.y] = m_CurrentTilePos;
					m_MovementCost[intVector.x, intVector.y] = m_TempGcost;
					for (int j = 1; j <= m_NoOfOpenListItems; j++)
					{
						if (m_OpenPos[m_OpenList[j]] == intVector)
						{
							m_TotalCost[m_OpenList[j]] = m_MovementCost[intVector.x, intVector.y] + m_HeuristicCost[m_OpenList[j]];
							m_Index = j;
							while (m_Index != 1 && m_TotalCost[m_OpenList[m_Index]] < m_TotalCost[m_OpenList[m_Index / 2]])
							{
								m_Temp = m_OpenList[m_Index / 2];
								m_OpenList[m_Index / 2] = m_OpenList[m_Index];
								m_OpenList[m_Index] = m_Temp;
								m_Index /= 2;
							}
							break;
						}
					}
				}
				if (m_WhichPathingList[targetGridPos.x, targetGridPos.y] == m_OnOpenListVal)
				{
					num = kPathFound;
					break;
				}
				continue;
			}
			num = kNonExistantPath;
			break;
		}
		IntVector2[] array = null;
		if (!searchData.m_SearchingCancelled && num == kPathFound)
		{
			IntVector2 intVector2 = targetGridPos;
			int num6 = 0;
			while (startGridPos != intVector2)
			{
				intVector2 = m_ParentPosArray[intVector2.x, intVector2.y];
				num6++;
			}
			num6++;
			array = new IntVector2[num6];
			intVector2 = targetGridPos;
			while (startGridPos != intVector2)
			{
				num6--;
				array[num6] = m_PathNodePosArray[intVector2.x, intVector2.y];
				intVector2 = m_ParentPosArray[intVector2.x, intVector2.y];
			}
			num6--;
			array[num6] = m_PathNodePosArray[startGridPos.x, startGridPos.y];
		}
		return array;
	}

	private float CalculateHeuristicCost(IntVector2 currentPos, IntVector2 targetPos)
	{
		float num = targetPos.x - currentPos.x;
		float num2 = targetPos.y - currentPos.y;
		float num3 = num * num;
		float num4 = num2 * num2;
		return num3 + num4;
	}
}
