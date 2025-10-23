using UnityEngine;

public class BiomeGrouping : ScriptableObject
{
	[SerializeField]
	public BiomeTypes[] m_BiomeTypes;

	[SerializeField]
	public Biome[] m_Biomes;
}
