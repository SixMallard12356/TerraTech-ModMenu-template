#define UNITY_EDITOR
using UnityEngine;

[NodePath("TerraTech/Actions/Techs")]
[FriendlyName("uScript_SpawnTechWaveFromData", "Spawn array of Techs from SpawnTechData and add to the encounter")]
public class uScript_SpawnTechWaveFromData : uScriptLogic
{
	private Encounter m_Encounter;

	private int m_TotalNumSpawned;

	private float m_RespawnTimer;

	private bool m_WaitingRespawn;

	private bool m_RespawnedWaveGroup;

	private bool m_AllTechKilled;

	private const string kUniqueNameFormat = "{0}_waveTech_{1}";

	public const string kUniqueNamePrefix = "{0}_waveTech_";

	public bool Out => true;

	public bool RespawnedWaveGroup => m_RespawnedWaveGroup;

	public bool AllTechKilled => m_AllTechKilled;

	[FriendlyName("Tick Wave", "Spawns the initial wave, then ticks it and ensures repspawns. Returns total number of tech spawned since start.")]
	public int TickWave([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode, [FriendlyName("Tech Data", "The tech data to spawn from")] SpawnTechData spawnData, [FriendlyName("Wave size", "Number of tech to spawn in this wave")] WaveSizeSpecification waveSize, [DefaultValue(0.3f)][FriendlyName("Delay between tech spawns", "Delay between spawning each tech")] float delayBetweenSpawns = 0.3f, [FriendlyName("Num Spawned so far", "Total number of tech spawned to this point. Reflected in Return value. If less than WaveSize - will complete initial spawn.")][DefaultValue(0)] int numSpawnedSoFar = 0, [FriendlyName("Allow respawn", "Do we allow respawning of tech in this wave")][DefaultValue(true)] bool allowRespawn = true, [FriendlyName("Respawn size", "Size of tech group to respawn to top up wave")][DefaultValue(1)] int respawnGroupSize = 1, [FriendlyName("Delay between respawns", "Time between respawning subsequent tech groups")][DefaultValue(0f)] float delayBetweenRespawnGroups = 0f)
	{
		if ((bool)ownerNode && m_Encounter == null)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		m_TotalNumSpawned = 0;
		m_RespawnedWaveGroup = false;
		m_AllTechKilled = false;
		if ((bool)m_Encounter && spawnData != null && spawnData.CanSpawnOnCurrentSKU())
		{
			m_TotalNumSpawned = numSpawnedSoFar;
			if (m_TotalNumSpawned < waveSize.WaveSize)
			{
				for (int i = m_TotalNumSpawned; i < waveSize.WaveSize; i++)
				{
					float spawnDelay = (float)(i - m_TotalNumSpawned) * delayBetweenSpawns;
					string nameOverride = $"{spawnData.UniqueName}_waveTech_{m_TotalNumSpawned}";
					if (spawnData.SpawnTechInEncounter(m_Encounter, spawnDelay, nameOverride))
					{
						m_TotalNumSpawned++;
					}
				}
			}
			else
			{
				int num = 0;
				for (int j = 0; j < m_TotalNumSpawned; j++)
				{
					if (WasTechKilled(spawnData, j))
					{
						num++;
					}
				}
				m_AllTechKilled = !allowRespawn && num == m_TotalNumSpawned;
				if (allowRespawn)
				{
					if (!m_WaitingRespawn)
					{
						int num2 = m_TotalNumSpawned - num;
						if (waveSize.WaveSize - num2 >= respawnGroupSize)
						{
							m_WaitingRespawn = true;
							m_RespawnTimer = delayBetweenRespawnGroups;
						}
					}
					else
					{
						m_RespawnTimer -= Time.deltaTime;
						if (m_RespawnTimer <= 0f)
						{
							for (int k = 0; k < respawnGroupSize; k++)
							{
								string nameOverride2 = $"{spawnData.UniqueName}_waveTech_{m_TotalNumSpawned}";
								if (spawnData.SpawnTechInEncounter(m_Encounter, (float)k * delayBetweenSpawns, nameOverride2))
								{
									m_TotalNumSpawned++;
								}
							}
							m_RespawnedWaveGroup = true;
							m_WaitingRespawn = false;
						}
					}
				}
			}
		}
		else
		{
			string text = ((spawnData == null) ? ("Tech data is null for " + m_Encounter.name) : ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null"));
			d.LogError("ERROR: uScript_SpawnTechWaveFromData - " + text);
		}
		return m_TotalNumSpawned;
	}

	private bool WasTechKilled(SpawnTechData spawnData, int spawnIndex)
	{
		string uniqueName = $"{spawnData.UniqueName}_waveTech_{spawnIndex}";
		return m_Encounter.GetVisibleState(uniqueName) == Encounter.EncounterVisibleState.Killed;
	}
}
