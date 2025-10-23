#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridBasedDistributor
{
	public struct PlacementData
	{
		public uint ID;

		public IntVector2 cellCoord;

		public Vector2 worldPos;

		public Vector2 randomRotHV;

		public int spawnChoice;
	}

	private struct PlacementDataInternal
	{
		public PlacementDataState placementState;

		public PlacementData data;

		public bool shouldSpawn
		{
			get
			{
				if (placementState != PlacementDataState.VerifiedValid)
				{
					return placementState == PlacementDataState.AssumeValid;
				}
				return true;
			}
		}
	}

	private enum PlacementDataState
	{
		AssumeValid,
		VerifiedValid,
		Invalid
	}

	public delegate bool BiomeCheckFunction(Biome biome);

	[Serializable]
	public class SlopeCheckParams
	{
		public bool enabled;

		public float radius = 1f;

		public float maxSlopeVariance = 1f;
	}

	public delegate bool CheckBlockedFn(Vector3 worldPos);

	[SerializeField]
	private float m_AverageSeparation;

	[SerializeField]
	private float m_MinimumSeparation;

	[SerializeField]
	private SlopeCheckParams m_SlopeCheckParams;

	[NonSerialized]
	public int NumSpawnChoices;

	[NonSerialized]
	public BiomeCheckFunction BiomeValidationFunc;

	public CheckBlockedFn BlockedFn;

	public bool PerformVendorLookupCheck;

	private Dictionary<uint, PlacementDataInternal> m_CachedPlacements = new Dictionary<uint, PlacementDataInternal>();

	private DRNG m_DRNG = new DRNG();

	public void TryPlaceOnTile(IntVector2 tileCoord, List<PlacementData> outSpawnData)
	{
		d.Assert(outSpawnData != null, "Output list passed to Place function is null");
		d.Assert(outSpawnData.Count == 0, "Output list passed to Place function has items within it");
		Vector2 vector = Singleton.Manager<ManWorld>.inst.TileManager.CalcMinWorldCoords(in tileCoord) + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		Vector2 vector2 = Singleton.Manager<ManWorld>.inst.TileManager.CalcMaxWorldCoords(in tileCoord) + Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
		d.Assert(m_MinimumSeparation >= 0f && m_MinimumSeparation < m_AverageSeparation);
		float averageSeparation = m_AverageSeparation;
		IntVector2 intVector = vector / averageSeparation;
		IntVector2 intVector2 = vector2 / averageSeparation;
		for (int i = intVector.x; i <= intVector2.x; i++)
		{
			for (int j = intVector.y; j <= intVector2.y; j++)
			{
				PlacementDataInternal placementDataAtCell = GetPlacementDataAtCell(i, j);
				if (placementDataAtCell.shouldSpawn)
				{
					PlacementData data = placementDataAtCell.data;
					if (data.worldPos.x >= vector.x && data.worldPos.x < vector2.x && data.worldPos.y >= vector.y && data.worldPos.y < vector2.y)
					{
						data.worldPos -= Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset;
						outSpawnData.Add(data);
					}
				}
			}
		}
	}

	public void ClearCachedPlacements()
	{
		m_CachedPlacements.Clear();
	}

	public void ListSpawnPositionsInRange(Vector3 worldPosition, float radius, List<Vector3> list)
	{
		int searchSize = Mathf.CeilToInt(radius / m_AverageSeparation);
		ListNearbySpawnPositions(worldPosition, searchSize, list);
		float distSqr = radius * radius;
		list.RemoveAll((Vector3 pos) => (pos - worldPosition).sqrMagnitude > distSqr);
	}

	public void ListNearbySpawnPositions(Vector3 worldPosition, int searchSize, List<Vector3> list)
	{
		if (m_AverageSeparation <= 0f)
		{
			d.LogError("GridBasedDistributor has a non-positive average separation");
			return;
		}
		worldPosition += Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset.ToVector3XZ();
		IntVector2 intVector = new IntVector2
		{
			x = Mathf.FloorToInt((worldPosition.x + m_AverageSeparation / 2f) / m_AverageSeparation),
			y = Mathf.FloorToInt((worldPosition.z + m_AverageSeparation / 2f) / m_AverageSeparation)
		};
		for (int i = intVector.y - searchSize; i <= intVector.y + searchSize; i++)
		{
			for (int j = intVector.x - searchSize; j <= intVector.x + searchSize; j++)
			{
				bool hasFoundSpawnPos = false;
				float bestDistSq = 0f;
				Vector3 nearestSpawnPos = Vector3.zero;
				LookForSpawnPosInCell(j, i, worldPosition, ref hasFoundSpawnPos, ref bestDistSq, ref nearestSpawnPos);
				if (hasFoundSpawnPos)
				{
					list.Add(nearestSpawnPos);
				}
			}
		}
	}

	public bool TryFindNearestSpawnPos(Vector3 worldPosition, out Vector3 nearestSpawnPos, bool searchOriginCell)
	{
		nearestSpawnPos = Vector3.zero;
		if (m_AverageSeparation <= 0f)
		{
			d.LogError("GridBasedDistributor has a non-positive average separation");
			return false;
		}
		worldPosition += Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset.ToVector3XZ();
		IntVector2 intVector = new IntVector2
		{
			x = Mathf.FloorToInt((worldPosition.x + m_AverageSeparation / 2f) / m_AverageSeparation),
			y = Mathf.FloorToInt((worldPosition.z + m_AverageSeparation / 2f) / m_AverageSeparation)
		};
		bool hasFoundSpawnPos = false;
		float bestDistSq = 0f;
		if (searchOriginCell)
		{
			LookForSpawnPosInCell(intVector.x, intVector.y, worldPosition, ref hasFoundSpawnPos, ref bestDistSq, ref nearestSpawnPos);
		}
		int num = 0;
		while (true)
		{
			float num2 = m_MinimumSeparation + m_AverageSeparation * (float)num;
			if (hasFoundSpawnPos && num2 * num2 >= bestDistSq)
			{
				break;
			}
			num++;
			if (num >= 16)
			{
				break;
			}
			for (int i = -num; i <= num; i++)
			{
				bool flag = Math.Abs(i) == num;
				for (int j = -num; j <= num; j++)
				{
					if (flag || Math.Abs(j) == num)
					{
						LookForSpawnPosInCell(intVector.x + j, intVector.y + i, worldPosition, ref hasFoundSpawnPos, ref bestDistSq, ref nearestSpawnPos);
					}
				}
			}
		}
		nearestSpawnPos -= Singleton.Manager<ManWorld>.inst.TerrainGenerationOffset.ToVector3XZ();
		return hasFoundSpawnPos;
	}

	public bool GetNearestBlocker(Vector3 worldPos, out Vector3 blockerPos)
	{
		bool searchOriginCell = true;
		return TryFindNearestSpawnPos(worldPos, out blockerPos, searchOriginCell);
	}

	private void LookForSpawnPosInCell(int cellX, int cellY, Vector3 worldOrigin, ref bool hasFoundSpawnPos, ref float bestDistSq, ref Vector3 nearestSpawnPos)
	{
		PlacementDataInternal placementDataAtCell = GetPlacementDataAtCell(cellX, cellY);
		if (!placementDataAtCell.shouldSpawn)
		{
			return;
		}
		Vector3 posWorld = placementDataAtCell.data.worldPos.ToVector3XZ();
		float sqrMagnitude = (worldOrigin - posWorld).sqrMagnitude;
		if (hasFoundSpawnPos && !(bestDistSq > sqrMagnitude))
		{
			return;
		}
		bool flag = false;
		if (PerformVendorLookupCheck)
		{
			IntVector2 intVector = Singleton.Manager<ManWorld>.inst.TileManager.WorldToTileCoord(in posWorld);
			bool flag2 = false;
			if (placementDataAtCell.placementState == PlacementDataState.AssumeValid)
			{
				ManSaveGame.StoredTile storedTile = Singleton.Manager<ManSaveGame>.inst.GetStoredTile(intVector, createNewIfNotFound: false);
				if (storedTile != null && storedTile.m_HasBeenSavedBefore)
				{
					flag2 = true;
					flag = !CheckTileContainsVendor(storedTile);
				}
				placementDataAtCell.placementState = ((!flag) ? PlacementDataState.VerifiedValid : PlacementDataState.Invalid);
				uint key = HashCodeUtility.FNVHash(cellX, cellY);
				m_CachedPlacements[key] = placementDataAtCell;
			}
			if (!flag2)
			{
				flag = Singleton.Manager<ManWorld>.inst.GetSetPieceDataForTile(intVector);
			}
		}
		if (!flag)
		{
			hasFoundSpawnPos = true;
			bestDistSq = sqrMagnitude;
			nearestSpawnPos = posWorld;
		}
	}

	private static bool CheckTileContainsVendor(ManSaveGame.StoredTile storedTile)
	{
		bool result = false;
		if (storedTile.m_StoredVisibles.TryGetValue(1, out var value))
		{
			foreach (ManSaveGame.StoredVisible item in value)
			{
				TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(item.m_ID);
				if (trackedVisible != null && trackedVisible.RadarType == RadarTypes.Vendor)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private PlacementDataInternal GetPlacementDataAtCell(int x, int y)
	{
		uint key = HashCodeUtility.FNVHash(x, y);
		if (!m_CachedPlacements.TryGetValue(key, out var value))
		{
			value = new PlacementDataInternal
			{
				data = GetRawPlacementDataAtCell(x, y, Singleton.Manager<ManWorld>.inst.SeedValue),
				placementState = PlacementDataState.AssumeValid
			};
			if (x == 0 && y == 0)
			{
				value.placementState = PlacementDataState.Invalid;
			}
			Vector3 vector = value.data.worldPos.ToVector3XZ();
			Vector3 scenePos = vector + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			if (BlockedFn != null && BlockedFn(vector))
			{
				value.placementState = PlacementDataState.Invalid;
			}
			if (BiomeValidationFunc != null)
			{
				Biome biome = null;
				if (Singleton.Manager<ManWorld>.inst.CurrentBiomeMap == null)
				{
					d.LogError("GridBasedDistributor.GetPlacementDataAtCell - Called after CurrentBiomeMap has been set to null; This shouldn't happen! No valid biome data available! Placement of Landmarks/TradingStations will fail!");
				}
				else
				{
					biome = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(scenePos).Biome(0);
				}
				if (!(biome != null) || !BiomeValidationFunc(biome))
				{
					value.placementState = PlacementDataState.Invalid;
				}
			}
			if (m_SlopeCheckParams.enabled && Singleton.Manager<ManWorld>.inst.QueryHeightVariance(scenePos, m_SlopeCheckParams.radius) > m_SlopeCheckParams.maxSlopeVariance)
			{
				value.placementState = PlacementDataState.Invalid;
			}
			m_CachedPlacements[key] = value;
		}
		return value;
	}

	public PlacementData GetRawPlacementDataAtCell(int x, int y, int worldSeed)
	{
		PlacementData result = new PlacementData
		{
			cellCoord = new Vector2(x, y)
		};
		uint previous = HashCodeUtility.FNVHash(x, y);
		result.ID = HashCodeUtility.FNVHashCombine(previous, worldSeed);
		m_DRNG.SetSeed(result.ID);
		float num = (m_AverageSeparation - m_MinimumSeparation) * 0.5f;
		result.worldPos = new Vector2(x, y) * m_AverageSeparation + new Vector2(m_DRNG.OnePosNeg(), m_DRNG.OnePosNeg()) * num;
		result.spawnChoice = m_DRNG.Range(0, NumSpawnChoices);
		result.randomRotHV = new Vector2(m_DRNG.One(), m_DRNG.OneInclusive());
		return result;
	}
}
