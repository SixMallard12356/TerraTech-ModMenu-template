#define UNITY_EDITOR
using UnityEngine;

public abstract class TerrainDataModifier
{
	protected Vector3 m_CentrePos;

	protected float m_Radius;

	public void Attach(Vector3 centrePos, float radius)
	{
		m_CentrePos = centrePos;
		m_Radius = radius;
	}

	public void Detach()
	{
	}

	private bool GetArenaCellBounds(Terrain terrain, float boundingRadius, out IntVector2[] range)
	{
		Vector3[] array = new Vector3[2]
		{
			m_CentrePos - Vector3.one * boundingRadius - terrain.transform.position,
			default(Vector3)
		};
		array[0].x /= terrain.terrainData.size.x;
		array[0].z /= terrain.terrainData.size.z;
		array[1] = m_CentrePos + Vector3.one * boundingRadius - terrain.transform.position;
		array[1].x /= terrain.terrainData.size.x;
		array[1].z /= terrain.terrainData.size.z;
		range = new IntVector2[2]
		{
			new IntVector2(Mathf.Max(0, (int)(array[0].x * (float)terrain.terrainData.heightmapWidth)), Mathf.Max(0, (int)(array[0].z * (float)terrain.terrainData.heightmapHeight))),
			new IntVector2(Mathf.Min(terrain.terrainData.heightmapWidth - 1, (int)(array[1].x * (float)terrain.terrainData.heightmapWidth)), Mathf.Min(terrain.terrainData.heightmapHeight - 1, (int)(array[1].z * (float)terrain.terrainData.heightmapHeight)))
		};
		if (array[0].x < 0f || array[0].x > 1f || array[0].z < 0f || array[0].z > 1f || array[1].x < 0f || array[1].x > 1f || array[1].z < 0f || array[1].z > 1f)
		{
			return false;
		}
		return true;
	}

	public virtual bool PrepareToModifyHeights(Terrain terrain, float[,] heightData)
	{
		return false;
	}

	public virtual float NewHeightPerCell(int x, int y, float originalHeight, float radius)
	{
		return 0f;
	}

	public virtual bool ClearSceneryRadius(int x, int y, float radius)
	{
		return false;
	}

	public virtual TerrainObject ReplaceScenery(TerrainObject prefab)
	{
		return prefab;
	}

	private void ModifyHeightmap(Terrain terrain)
	{
		IntVector2[] range;
		bool arenaCellBounds = GetArenaCellBounds(terrain, m_Radius, out range);
		if (range == null || !arenaCellBounds)
		{
			return;
		}
		int num = range[1].x - range[0].x;
		int num2 = range[1].y - range[0].y;
		float[,] heights = terrain.terrainData.GetHeights(range[0].x, range[0].y, num, num2);
		float num3 = terrain.terrainData.size.x / (float)terrain.terrainData.heightmapWidth;
		float num4 = terrain.terrainData.size.z / (float)terrain.terrainData.heightmapHeight;
		Vector3 vector = terrain.transform.position - m_CentrePos;
		if (!PrepareToModifyHeights(terrain, heights))
		{
			return;
		}
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				float magnitude = (new Vector3((float)(i + range[0].x) * num3, 0f, (float)(j + range[0].y) * num4) + vector).SetY(0f).magnitude;
				heights[j, i] = NewHeightPerCell(i, j, heights[j, i], magnitude);
			}
		}
		terrain.terrainData.SetHeights(range[0].x, range[0].y, heights);
	}

	private void ClearSceneryRadius(Terrain terrain, BiomeMap.SpawnDetail[,] spawnDetail)
	{
		GetArenaCellBounds(terrain, m_Radius, out var range);
		if (range == null)
		{
			return;
		}
		for (int i = range[0].x; i < range[1].x; i++)
		{
			for (int j = range[0].y; j < range[1].y; j++)
			{
				Vector3 vector = new Vector3(i, 0f, j) * Singleton.Manager<ManWorld>.inst.CellScale + terrain.transform.position;
				if (ClearSceneryRadius(i, j, (vector - m_CentrePos).SetY(0f).magnitude))
				{
					ref BiomeMap.SpawnDetail reference = ref spawnDetail[j, i];
					reference.t0 = null;
					reference.t1 = null;
					reference.t2 = null;
				}
			}
		}
	}

	private void ReplaceScenery(Terrain terrain, BiomeMap.SpawnDetail[,] spawnDetail)
	{
		GetArenaCellBounds(terrain, m_Radius, out var range);
		if (range == null)
		{
			return;
		}
		for (int i = range[0].x; i < range[1].x; i++)
		{
			for (int j = range[0].y; j < range[1].y; j++)
			{
				ref BiomeMap.SpawnDetail reference = ref spawnDetail[j, i];
				reference.t0 = ReplaceScenery(reference.t0);
				reference.t1 = ReplaceScenery(reference.t1);
				reference.t2 = ReplaceScenery(reference.t2);
			}
		}
	}

	private void OnTileCreationStage(WorldTile tile, TileManager.CreationStage stage)
	{
		if (Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in m_CentrePos) == tile)
		{
			switch (stage)
			{
			case TileManager.CreationStage.PreCreate:
				ModifyHeightmap(tile.Terrain);
				break;
			case TileManager.CreationStage.PrePopulate:
				d.Assert(tile.BiomeMapData.sceneryPlacement != null);
				ClearSceneryRadius(tile.Terrain, tile.BiomeMapData.sceneryPlacement);
				ReplaceScenery(tile.Terrain, tile.BiomeMapData.sceneryPlacement);
				break;
			}
		}
	}
}
