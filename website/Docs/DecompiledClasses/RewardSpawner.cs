#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RewardSpawner
{
	public class SaveData
	{
		public Dictionary<string, ObjectSpawner.SaveData> m_RewardSpawnerSaveData;

		public MultiObjectSpawner.DebugHistory m_RewardSpawnerHistory;
	}

	private struct CachedSpawnData
	{
		public BlockTypes block;

		public Vector3 worldPos;
	}

	[SerializeField]
	public float m_RewardSpawnInterval = 0.1f;

	[SerializeField]
	public float m_RewardSpawnHeight = 5f;

	[SerializeField]
	public float m_RewardSpawnRadius = 3f;

	[SerializeField]
	public float m_RewardSpawnCameraLookAhead = 15f;

	[SerializeField]
	public float m_RewardSpawnHeightTech = 5f;

	[SerializeField]
	public float m_RewardSpawnRadiusTech = 2f;

	[SerializeField]
	public ParticleSystem m_SpawnRewardParticles;

	[SerializeField]
	public FMODEvent m_BlockSpawnSfxEvent;

	private List<CachedSpawnData> m_RewardSpawnList = new List<CachedSpawnData>();

	private float m_RewardSpawnTimer;

	private MultiObjectSpawner m_ObjectSpawner = new MultiObjectSpawner();

	public void Load(SaveData saveData)
	{
		if (saveData != null)
		{
			m_ObjectSpawner.Load(saveData.m_RewardSpawnerSaveData, saveData.m_RewardSpawnerHistory, autoRetry: true);
		}
	}

	public void Save(ref SaveData saveData)
	{
		if (saveData == null)
		{
			saveData = new SaveData();
		}
		m_ObjectSpawner.Save(ref saveData.m_RewardSpawnerSaveData, ref saveData.m_RewardSpawnerHistory);
	}

	public void Clear()
	{
		m_RewardSpawnList.Clear();
	}

	public void RewardBlocksByCrate(IList<BlockTypes> blocks, Vector3 scenePos, FactionSubTypes corporation)
	{
		AddBlocksByCrate(blocks, scenePos, corporation);
	}

	public void AddBlocksInFrontOfCamera(IList<BlockTypes> blocks)
	{
		Vector3 spawnPos = Singleton.cameraTrans.position + Singleton.cameraTrans.forward * m_RewardSpawnCameraLookAhead;
		Vector3 offsetVec = Quaternion.AngleAxis(180f, Vector3.up) * new Vector3(0f, m_RewardSpawnHeight, m_RewardSpawnRadius);
		AddBlocks(blocks, spawnPos, offsetVec);
	}

	public void AddBlocksAroundTech(IList<BlockTypes> blocks, Tank tech)
	{
		Vector3 boundsCentreWorld = tech.boundsCentreWorld;
		Vector3 offsetVec = Quaternion.AngleAxis(180f, Vector3.up) * new Vector3(0f, m_RewardSpawnHeightTech, m_RewardSpawnRadiusTech + tech.visible.Radius);
		AddBlocks(blocks, boundsCentreWorld, offsetVec);
	}

	public void AddBlocksAtPosition(IList<BlockTypes> blocks, Vector3 spawnPos)
	{
		Vector3 offsetVec = Quaternion.AngleAxis(180f, Vector3.up) * new Vector3(0f, m_RewardSpawnHeight, m_RewardSpawnRadius);
		AddBlocks(blocks, spawnPos, offsetVec);
	}

	public void CueBlockSpawnFx(Vector3 pos)
	{
		if (m_BlockSpawnSfxEvent.IsValid())
		{
			m_BlockSpawnSfxEvent.PlayOneShot(pos);
		}
		if ((bool)m_SpawnRewardParticles)
		{
			m_SpawnRewardParticles.transform.Spawn(pos);
		}
	}

	public void Update()
	{
		if (m_RewardSpawnList.Count <= 0 || !(Time.time > m_RewardSpawnTimer))
		{
			return;
		}
		m_RewardSpawnTimer += m_RewardSpawnInterval;
		int index = m_RewardSpawnList.Count - 1;
		BlockTypes block = m_RewardSpawnList[index].block;
		Vector3 worldPos = m_RewardSpawnList[index].worldPos;
		Quaternion rot = Quaternion.AngleAxis(UnityEngine.Random.value * 360f, Vector3.up);
		TankBlock tankBlock = Singleton.Manager<ManLooseBlocks>.inst.HostSpawnBlock(block, worldPos, rot);
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			CueBlockSpawnFx(worldPos);
		}
		else
		{
			d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "RewardSpawner.Update was not expecting client to be spawning blocks");
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.BlockPlayRewardSpawnFx, new EmptyMessage(), tankBlock.netBlock.netId);
			}
		}
		m_RewardSpawnList.RemoveAt(index);
	}

	private void AddBlocks(IList<BlockTypes> blocks, Vector3 spawnPos, Vector3 offsetVec)
	{
		Quaternion quaternion = Quaternion.AngleAxis(360f / (float)blocks.Count, Vector3.up);
		if (m_RewardSpawnList.Count == 0)
		{
			m_RewardSpawnTimer = Time.time;
		}
		for (int num = blocks.Count - 1; num >= 0; num--)
		{
			m_RewardSpawnList.Add(new CachedSpawnData
			{
				block = blocks[num],
				worldPos = spawnPos + offsetVec
			});
			offsetVec = quaternion * offsetVec;
		}
	}

	public void AddBlocksByCrate(IList<BlockTypes> blocks, Vector3 spawnPosScene, FactionSubTypes corporation)
	{
		Crate.Definition crateDef = new Crate.Definition
		{
			m_Contents = new Crate.ItemDefinition[blocks.Count],
			m_Locked = false
		};
		for (int i = 0; i < blocks.Count; i++)
		{
			crateDef.m_Contents[i].m_BlockType = blocks[i];
		}
		ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
		{
			m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
			m_AvoidLandmarks = true,
			m_CircleRadius = Singleton.Manager<ManSpawn>.inst.GetCrateSpawnClearance(corporation),
			m_CenterPosWorld = WorldPosition.FromScenePosition(in spawnPosScene),
			m_CircleIndex = 0,
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
			m_CheckSafeArea = false,
			m_RejectFunc = null
		};
		ManSpawn.CrateSpawnParams objectSpawnParams = new ManSpawn.CrateSpawnParams
		{
			m_CrateDef = crateDef,
			m_Rotation = Quaternion.identity,
			m_CorpType = corporation,
			m_Name = "RewardSpawner Crate",
			m_VisibleOnRadar = true
		};
		bool autoRetry = true;
		m_ObjectSpawner.TrySpawn(objectSpawnParams, freeSpaceParams, "RewardSpawner Crate", autoRetry);
	}
}
