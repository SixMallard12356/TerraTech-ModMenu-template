using UnityEngine;

[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_SpawnEnemyInFrontOfPlayer : uScriptLogic
{
	private bool m_Spawned;

	private Encounter m_Encounter;

	public bool Spawned => m_Spawned;

	public bool NotSpawned => !m_Spawned;

	public void In([FriendlyName("Preset", "Preset to Spawn")] TankPreset preset, [FriendlyName("Unique Name", "Unique Name for Saving & Retrieving Tech")] string uniqueName, [FriendlyName("Dist From Player", "Distance to spawn enemy in front of player")] float distFromPlayer, [FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		m_Spawned = false;
		if ((bool)Singleton.playerTank)
		{
			ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
			{
				m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
				m_CircleRadius = preset.Radius,
				m_CenterPosWorld = WorldPosition.FromScenePosition(Singleton.playerTank.trans.position + Singleton.playerTank.trans.forward * distFromPlayer),
				m_CircleIndex = 0,
				m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
				m_CheckSafeArea = false,
				m_RejectFunc = null
			};
			ManSpawn.TechSpawnParams objectSpawnParams = new ManSpawn.TechSpawnParams
			{
				m_TechToSpawn = preset.GetTechDataFormatted(),
				m_Team = -1,
				m_Rotation = Quaternion.Euler(0f, Random.value * 360f, 0f),
				m_Grounded = true,
				m_SpawnVisualType = ManSpawn.SpawnVisualType.Bomb
			};
			m_Encounter.SpawnObject(objectSpawnParams, freeSpaceParams, uniqueName);
			m_Spawned = true;
		}
	}
}
