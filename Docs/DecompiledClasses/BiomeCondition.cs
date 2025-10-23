using System;
using UnityEngine;

[Serializable]
public class BiomeCondition : LocationCondition
{
	[SerializeField]
	[EnumArray(typeof(BiomeTypes))]
	[Tooltip("Minimum biome weight required for this encounter to spawn (between 0 and 1)")]
	private float[] m_MinBiomeWeight = new float[0];

	[SerializeField]
	[Tooltip("Not Used - Now uses below")]
	private BiomeTypes[] m_ExcludedBiomes;

	[SerializeField]
	private BiomeGrouping m_BiomesToExclude;

	public override bool TilePasses(WorldTile tile)
	{
		bool flag = true;
		if (m_MinBiomeWeight != null && m_MinBiomeWeight.Length != 0)
		{
			EnumValuesIterator<BiomeTypes> enumerator = EnumIterator<BiomeTypes>.Values().GetEnumerator();
			while (enumerator.MoveNext())
			{
				BiomeTypes current = enumerator.Current;
				if ((int)current < m_MinBiomeWeight.Length && tile.GetMaximumBiomeWeight(current) < m_MinBiomeWeight[(int)current])
				{
					flag = false;
					break;
				}
			}
		}
		if (flag && m_BiomesToExclude != null)
		{
			bool flag2 = false;
			EnumValuesIterator<BiomeTypes> enumerator = EnumIterator<BiomeTypes>.Values().GetEnumerator();
			while (enumerator.MoveNext())
			{
				BiomeTypes current2 = enumerator.Current;
				if (!tile.HasAnyOfBiomeType(current2))
				{
					continue;
				}
				bool flag3 = false;
				for (int i = 0; i < m_BiomesToExclude.m_BiomeTypes.Length; i++)
				{
					if (current2 == m_BiomesToExclude.m_BiomeTypes[i])
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					flag2 = true;
					break;
				}
			}
			if (flag2 && m_BiomesToExclude.m_Biomes.Length != 0)
			{
				flag2 = tile.HasOtherBiomes(m_BiomesToExclude.m_Biomes);
			}
			if (!flag2)
			{
				flag = false;
			}
		}
		return flag;
	}

	public override bool Passes(WorldTile.TilePosInfo tilePosInfo)
	{
		bool flag = true;
		if (m_MinBiomeWeight != null && m_MinBiomeWeight.Length != 0)
		{
			EnumValuesIterator<BiomeTypes> enumerator = EnumIterator<BiomeTypes>.Values().GetEnumerator();
			while (enumerator.MoveNext())
			{
				int current = (int)enumerator.Current;
				if (current < m_MinBiomeWeight.Length && m_MinBiomeWeight[current] > 0f && tilePosInfo.biomeWeighting[current] < m_MinBiomeWeight[current])
				{
					flag = false;
					break;
				}
			}
		}
		if (m_BiomesToExclude != null)
		{
			if (flag)
			{
				for (int i = 0; i < m_BiomesToExclude.m_BiomeTypes.Length; i++)
				{
					BiomeTypes biomeTypes = m_BiomesToExclude.m_BiomeTypes[i];
					if (tilePosInfo.biomeWeighting[(int)biomeTypes] > 0f)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				for (int j = 0; j < m_BiomesToExclude.m_Biomes.Length; j++)
				{
					if (tilePosInfo.biomeList.Contains(m_BiomesToExclude.m_Biomes[j]))
					{
						flag = false;
					}
				}
			}
		}
		return flag;
	}
}
