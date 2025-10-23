using System;
using UnityEngine.Serialization;

[Serializable]
public struct SpawnBlockData
{
	public BlockTypes m_BlockType;

	public string m_UniqueName;

	public string m_PositionName;

	[FormerlySerializedAs("m_UseDeliveryBomb")]
	public ManSpawn.SpawnVisualType m_SpawnVisualType;

	public ManSpawn.CustomSpawnEffectType m_customSpawnEffectType;

	public bool m_SpawnInFreeSpace;
}
