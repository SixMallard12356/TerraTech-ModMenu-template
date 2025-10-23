using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NetOptions
{
	[SerializeField]
	public MultiplayerModeType m_GameModeType;

	[SerializeField]
	public LocalisedString m_LocName;

	[SerializeField]
	public int m_NumTeams = 1;

	[SerializeField]
	public List<NetLevelData> m_LevelData;

	[SerializeField]
	public InventoryBlockList m_InventoryBlockList;

	[SerializeField]
	public NetController.GameData m_GameData;

	[SerializeField]
	public ModePVP<ModeDeathmatch>.DispenserSpawn[] m_DispenserSpawns;

	[SerializeField]
	public float m_IdealSpawnDistanceEnemies = 250f;

	[SerializeField]
	public float m_IdealSpawnDistanceAllies = 100f;

	[SerializeField]
	public Transform prefab;
}
