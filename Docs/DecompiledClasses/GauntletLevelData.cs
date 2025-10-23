using System.Collections.Generic;
using UnityEngine;

public class GauntletLevelData : MonoBehaviour
{
	[SerializeField]
	private string m_TrackName;

	[SerializeField]
	private string m_SeedString;

	[SerializeField]
	private int m_OverrideTerrainGenVersion = -1;

	[SerializeField]
	private BiomeMap.WorldGenVersioningType m_OverrideTerrainGenVersioningType;

	[SerializeField]
	private SpawnList m_SpawnList = new SpawnList();

	[SerializeField]
	private SpawnList m_TutorialSpawnList = new SpawnList();

	[SerializeField]
	private CheckpointChallengeData m_CheckpointChallengeData;

	[SerializeField]
	private PositionWithFacing m_PlayerSpawn = PositionWithFacing.identity;

	[SerializeField]
	private PositionWithFacing m_CameraSpawn = PositionWithFacing.identity;

	[SerializeField]
	private PositionWithFacing m_ChallengeSpawn = PositionWithFacing.identity;

	[SerializeField]
	private TankPreset m_PlayerPreset;

	[SerializeField]
	private List<IntVector2> m_FixedTilesLoaded;

	[SerializeField]
	private List<IntVector2> m_FixedTilesUnpopulated;

	[SerializeField]
	private float m_MaxDragFromSpawnDistance;

	public string TrackName => m_TrackName;

	public string SeedString => m_SeedString;

	public int OverrideTerrainGenVersion => m_OverrideTerrainGenVersion;

	public BiomeMap.WorldGenVersioningType OverrideTerrainGenVersioningType => m_OverrideTerrainGenVersioningType;

	public SpawnList SpawnList => m_SpawnList;

	public SpawnList TutorialSpawnList => m_TutorialSpawnList;

	public CheckpointChallengeData CheckpointChallengeData => m_CheckpointChallengeData;

	public PositionWithFacing PlayerSpawn => m_PlayerSpawn;

	public PositionWithFacing CameraSpawn => m_CameraSpawn;

	public PositionWithFacing ChallengeSpawn => m_ChallengeSpawn;

	public TankPreset PlayerPreset => m_PlayerPreset;

	public List<IntVector2> FixedTilesLoaded => m_FixedTilesLoaded;

	public List<IntVector2> FixedTilesUnpopulated => m_FixedTilesUnpopulated;

	public float MaxDragFromSpawnDistance => m_MaxDragFromSpawnDistance;
}
