using System;
using UnityEngine;

[Serializable]
public class LocationConditions
{
	[SerializeField]
	private BiomeCondition[] m_BiomeConditions;

	public bool TilePasses(WorldTile tile)
	{
		bool result = true;
		if (m_BiomeConditions != null)
		{
			for (int i = 0; i < m_BiomeConditions.Length; i++)
			{
				if (!m_BiomeConditions[i].TilePasses(tile))
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	public bool Passes(WorldTile.TilePosInfo tilePosInfo)
	{
		bool result = true;
		if (m_BiomeConditions != null)
		{
			for (int i = 0; i < m_BiomeConditions.Length; i++)
			{
				if (!m_BiomeConditions[i].Passes(tilePosInfo))
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}
}
