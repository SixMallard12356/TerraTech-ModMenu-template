#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner
{
	private ObjectSpawner m_CrateDropSpawner = new ObjectSpawner();

	private DeliveryBombSpawner m_CrateDropDeliveryBombSpawner;

	private CrateDropPopulator m_CrateDropPopulator;

	private float m_CrateDropSpawnCountdownTimer;

	private float m_MaxCrateSpawnDistanceFromCentre;

	private bool m_CanSpawnCrateDrop = true;

	private bool m_CrateDropApplySpawnDelay = true;

	private List<Vector3> m_TempCrateDropPoints;

	public void RegisterMessageHandlers()
	{
		NetCrate.OnNetCrateOpened.Subscribe(OnNetCrateOpened);
	}

	public void UnregisterMessageHandlers()
	{
		NetCrate.OnNetCrateOpened.Unsubscribe(OnNetCrateOpened);
	}

	public void Init(float maxCrateSpawnDistanceFromCentre)
	{
		d.Assert(m_CrateDropDeliveryBombSpawner == null);
		m_MaxCrateSpawnDistanceFromCentre = maxCrateSpawnDistanceFromCentre;
		m_CrateDropSpawnCountdownTimer = 0f;
		m_CanSpawnCrateDrop = true;
		m_CrateDropApplySpawnDelay = true;
	}

	public void DeInit()
	{
		m_CrateDropSpawner.Cancel();
		if (m_CrateDropDeliveryBombSpawner != null)
		{
			m_CrateDropDeliveryBombSpawner.Recycle();
			m_CrateDropDeliveryBombSpawner = null;
		}
	}

	public void UpdateCrateSpawning()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer, "UpdateCrateSpawning - Called from non-server client!");
		if (Singleton.Manager<ManNetwork>.inst.CrateDropsEnabled)
		{
			d.Assert(Singleton.Manager<ManNetwork>.inst.NetController != null);
			UpdateServerCrateDrops();
		}
	}

	private static Crate.Definition CreateCrateDefinition(InventoryBlockList blockList)
	{
		List<Crate.ItemDefinition> list = new List<Crate.ItemDefinition>(16);
		for (int i = 0; i < blockList.Blocks.Length; i++)
		{
			BlockCount blockCount = blockList.Blocks[i];
			for (int j = 0; j < blockCount.m_Quantity; j++)
			{
				Crate.ItemDefinition item = new Crate.ItemDefinition
				{
					m_BlockType = blockCount.m_BlockType
				};
				list.Add(item);
			}
		}
		return new Crate.Definition
		{
			m_Contents = list.ToArray(),
			m_Locked = true
		};
	}

	private void UpdateServerCrateDrops()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.CrateDropsEnabled);
		if (!m_CanSpawnCrateDrop)
		{
			return;
		}
		if (m_CrateDropSpawnCountdownTimer <= 0f)
		{
			m_CrateDropSpawnCountdownTimer = ManNetwork.CrateDropFrequencies[Singleton.Manager<ManNetwork>.inst.CrateDropFrequencyIndex];
			if (m_CrateDropApplySpawnDelay)
			{
				m_CrateDropApplySpawnDelay = false;
				m_CrateDropSpawnCountdownTimer = (float)ManNetwork.CrateDropDelayMinsChoices[Singleton.Manager<ManNetwork>.inst.CrateDropDelayMinsIndex] * 60f;
				if (m_CrateDropSpawnCountdownTimer <= 0f)
				{
					SpawnCrateDrop();
				}
			}
		}
		else
		{
			m_CrateDropSpawnCountdownTimer -= Time.deltaTime;
			if (m_CrateDropSpawnCountdownTimer <= 0f)
			{
				SpawnCrateDrop();
				m_CanSpawnCrateDrop = false;
				m_CrateDropSpawnCountdownTimer = ManNetwork.CrateDropFrequencies[Singleton.Manager<ManNetwork>.inst.CrateDropFrequencyIndex];
			}
		}
	}

	private BlockCount[] PopulateCrateContents(int nBlocksReqd)
	{
		if (m_CrateDropPopulator == null)
		{
			m_CrateDropPopulator = new CrateDropPopulator();
		}
		d.Assert(m_CrateDropPopulator != null);
		return m_CrateDropPopulator.PopulateCrate(nBlocksReqd);
	}

	private void SpawnCrateDrop()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		Vector3 scenePos = FindSafeSpawnPosForCrateDrop();
		ManFreeSpace.FreeSpaceParams.RejectFunction rejectFunc = BoundsFreeSpaceRejectFunction;
		_ = Singleton.Manager<ManNetwork>.inst.NetController.ServerSpawnPolicy;
		Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos);
		InventoryBlockList inventoryBlockList = new InventoryBlockList();
		int nBlocksReqd = ManNetwork.CrateDropBlockCounts[Singleton.Manager<ManNetwork>.inst.CrateDropBlockQuantityIndex];
		inventoryBlockList.m_BlockList = PopulateCrateContents(nBlocksReqd);
		ManSpawn.CrateSpawnParams objectSpawnParams = new ManSpawn.CrateSpawnParams
		{
			m_Name = "CrateDrop",
			m_CrateDef = CreateCrateDefinition(inventoryBlockList),
			m_CorpType = FactionSubTypes.NULL,
			m_Rotation = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up),
			m_VisibleOnRadar = true
		};
		ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
		{
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
			m_CenterPosWorld = WorldPosition.FromScenePosition(in scenePos),
			m_CheckSafeArea = false,
			m_CircleIndex = 0,
			m_CircleRadius = Singleton.Manager<ManSpawn>.inst.GetCrateSpawnClearance() * 1.5f,
			m_DebugName = "CrateDrop",
			m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCratesBlocks,
			m_RejectFunc = rejectFunc
		};
		bool autoRetry = false;
		m_CrateDropSpawner.TrySpawn(objectSpawnParams, freeSpaceParams, null, "CrateDrop", autoRetry);
	}

	private bool BoundsFreeSpaceRejectFunction(Vector3 position, float radius, object context)
	{
		return !IsPositionWithinBounds(position);
	}

	private bool IsPositionWithinBounds(Vector3 pos)
	{
		float maxCrateSpawnDistanceFromCentre = m_MaxCrateSpawnDistanceFromCentre;
		return pos.SetY(0f).sqrMagnitude <= maxCrateSpawnDistanceFromCentre * maxCrateSpawnDistanceFromCentre;
	}

	private Vector3 FindSafeSpawnPosForCrateDrop()
	{
		float num = ManNetwork.CrateDropMinDistances[Singleton.Manager<ManNetwork>.inst.CrateDropMinDistanceIndex];
		float num2 = num * num;
		if (m_TempCrateDropPoints == null)
		{
			m_TempCrateDropPoints = new List<Vector3>(25);
		}
		m_TempCrateDropPoints.Clear();
		d.Assert(m_TempCrateDropPoints.Capacity > 0);
		float num3 = m_MaxCrateSpawnDistanceFromCentre * 2f;
		float num4 = num3 / 4f;
		Vector3 vector = Vector3.zero - new Vector3(num3 / 2f, 0f, num3 / 2f) + new Vector3(num4 * 0.5f, 0f, num4 * 0.5f);
		Vector3 scenePos = vector;
		Vector3 result = Vector3.zero;
		float num5 = 0f;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (IsPositionWithinBounds(scenePos) && Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(in scenePos))
				{
					bool flag = false;
					for (int k = 0; k < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); k++)
					{
						NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(k);
						NetTech netTech = ((player != null) ? player.CurTech : null);
						if (netTech != null && netTech.tech != null)
						{
							flag = true;
							float sqrMagnitude = (netTech.tech.trans.position - scenePos).SetY(0f).sqrMagnitude;
							if (sqrMagnitude >= num2)
							{
								m_TempCrateDropPoints.Add(scenePos);
							}
							else if (sqrMagnitude > num5)
							{
								result = scenePos;
								num5 = sqrMagnitude;
							}
						}
					}
					if (!flag)
					{
						m_TempCrateDropPoints.Add(scenePos);
					}
				}
				scenePos.x += num4;
			}
			scenePos.x = vector.x;
			scenePos.z += num4;
		}
		if (m_TempCrateDropPoints.Count > 0)
		{
			int index = Random.Range(0, m_TempCrateDropPoints.Count);
			return m_TempCrateDropPoints[index];
		}
		return result;
	}

	private void AllowNextCrateSpawn()
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		m_CanSpawnCrateDrop = true;
	}

	private void OnNetCrateOpened(NetCrate crate)
	{
		AllowNextCrateSpawn();
	}
}
