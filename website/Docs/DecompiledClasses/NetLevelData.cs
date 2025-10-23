using UnityEngine;

public class NetLevelData : ScriptableObject
{
	[SerializeField]
	public BiomeMap m_BiomeMap;

	[SerializeField]
	public PrefabSpawner m_PrefabSpawner;

	[SerializeField]
	public SpawnPointBank.Config m_SpawnPointBankConfig;

	[SerializeField]
	public int m_TileGenerationOffsetX;

	[SerializeField]
	public int m_TileGenerationOffsetY;

	[SerializeField]
	public LocalisedString m_levelName;
}
