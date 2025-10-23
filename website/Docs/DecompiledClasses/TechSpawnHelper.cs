#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TechSpawnHelper
{
	public struct SpawnParams
	{
		public TechData m_TechData;

		public int m_TeamID;

		public NetPlayer m_Player;

		public bool m_UseInventory;

		public bool m_AllowSpawnWithBlocksMissing;

		public float m_MaxSpawnRange;

		public Vector3 m_EncounterLocation;

		public Vector3 m_EncounterFacingDir;
	}

	private struct SpawnRequest
	{
		public SpawnParams m_SpawnParams;

		public SpawnPointBank m_SpawnPointBank;

		public NetController.SpawnPolicy m_SpawnPolicy;

		public Vector3 m_DesiredSpawnPosition;

		public SpawnPointBank.SpawnPointInfo m_DesiredSpawnPoint;

		public ManFreeSpace.FreeSpaceParams m_FreeSpaceParams;

		public int m_SpawnRetryCount;
	}

	[Flags]
	private enum TeamAffiliation
	{
		Ally = 1,
		Enemy = 2,
		Any = 3
	}

	public Event<TechData, Vector3, Quaternion, NetPlayer> OnBeforeSpawnTech;

	private List<SpawnRequest> m_SpawnRequests = new List<SpawnRequest>();

	private FreeSpaceFinder m_FreeSpaceFinder;

	public bool IsSearching => m_SpawnRequests.Count > 0;

	public void SpawnTechAtOptimalPosition(SpawnParams spawnParams, NetController.SpawnPolicy spawnPolicy, SpawnPointBank spawnPointBank = null, int retryCount = 0, ManFreeSpace.FreeSpaceParams.RejectFunction customFreeSpaceRejectFunc = null)
	{
		if (m_FreeSpaceFinder == null)
		{
			m_FreeSpaceFinder = new FreeSpaceFinder();
			m_FreeSpaceFinder.FreeSpaceFoundEvent.Subscribe(OnFreeSpaceFound);
		}
		SpawnPointBank.SpawnPointInfo spawnPointInfo = null;
		Vector3 scenePos;
		ManFreeSpace.FreeSpaceParams.RejectFunction rejectFunc;
		switch (spawnPolicy)
		{
		case NetController.SpawnPolicy.AtSpawnPoint:
			spawnPointInfo = FindBestSpawnLocation(spawnParams, spawnPointBank);
			scenePos = spawnPointInfo.position;
			rejectFunc = SpawnPointFreeSpaceRejectFunction;
			break;
		case NetController.SpawnPolicy.CloseToAllies:
			scenePos = FindSafeSpawnPos(spawnParams);
			rejectFunc = BoundsFreeSpaceRejectFunction;
			break;
		case NetController.SpawnPolicy.AtEncounter:
			scenePos = spawnParams.m_EncounterLocation;
			rejectFunc = BoundsFreeSpaceRejectFunction;
			break;
		default:
			d.AssertFormat(false, "SpawnTechAtOptimalSpawnPoint has unhandled spawn policy {0}", spawnPolicy);
			scenePos = Vector3.zero;
			rejectFunc = BoundsFreeSpaceRejectFunction;
			break;
		}
		Singleton.Manager<ManWorld>.inst.TryProjectToGround(ref scenePos);
		ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
		{
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
			m_CenterPosWorld = WorldPosition.FromScenePosition(in scenePos),
			m_CheckSafeArea = false,
			m_CircleIndex = 0,
			m_CircleRadius = spawnParams.m_TechData.Radius + 5f,
			m_DebugName = "NetSpawnTech",
			m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCratesBlocks,
			m_RejectFunc = rejectFunc,
			m_RejectFuncContext = customFreeSpaceRejectFunc
		};
		m_SpawnRequests.Add(new SpawnRequest
		{
			m_SpawnParams = spawnParams,
			m_SpawnPointBank = spawnPointBank,
			m_SpawnPolicy = spawnPolicy,
			m_DesiredSpawnPosition = scenePos,
			m_DesiredSpawnPoint = spawnPointInfo,
			m_FreeSpaceParams = freeSpaceParams,
			m_SpawnRetryCount = retryCount
		});
	}

	public void UpdateSpawnQueue()
	{
		if (m_FreeSpaceFinder != null && !m_FreeSpaceFinder.IsValid && m_SpawnRequests.Count > 0)
		{
			m_FreeSpaceFinder.Setup(m_SpawnRequests[0].m_FreeSpaceParams, "NetSpawnTech_FreeSpace", autoRetry: false);
		}
	}

	private bool GetClosestDistanceSqrFromPointToOtherTechs(Vector3 pos, SpawnParams spawnParams, out float closestDistSqr, TeamAffiliation affiliation = TeamAffiliation.Any, bool playerOccupiedTechOnly = false)
	{
		closestDistSqr = float.PositiveInfinity;
		foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
		{
			d.Assert(currentTech.netTech != null, "NULL tech found in ManTechs.CurrentTechs while in MP mode.. Unexpected?");
			if (currentTech.netTech != null && (!playerOccupiedTechOnly || currentTech.netTech.NetPlayer != null) && (affiliation == TeamAffiliation.Any || affiliation == TeamAffiliation.Ally == currentTech.IsFriendly(spawnParams.m_TeamID)) && (currentTech.netTech.NetPlayer == null || currentTech.netTech.NetPlayer != spawnParams.m_Player))
			{
				float sqrMagnitude = (pos - currentTech.boundsCentreWorld).sqrMagnitude;
				if (sqrMagnitude < closestDistSqr)
				{
					closestDistSqr = sqrMagnitude;
				}
			}
		}
		return !float.IsPositiveInfinity(closestDistSqr);
	}

	private float GetClosestDistanceSqrFromPointToReservedSpawns(Vector3 pos, SpawnParams spawnParams, TeamAffiliation affiliation = TeamAffiliation.Any)
	{
		float num = float.PositiveInfinity;
		for (int i = 0; i < m_SpawnRequests.Count; i++)
		{
			if (affiliation == TeamAffiliation.Any || affiliation == TeamAffiliation.Ally == Tank.IsFriendly(m_SpawnRequests[i].m_SpawnParams.m_TeamID, spawnParams.m_TeamID))
			{
				float sqrMagnitude = (pos - m_SpawnRequests[i].m_DesiredSpawnPosition).sqrMagnitude;
				num = Mathf.Min(num, sqrMagnitude);
			}
		}
		return num;
	}

	private SpawnPointBank.SpawnPointInfo FindBestSpawnLocation(SpawnParams spawnParams, SpawnPointBank spawnPointBank)
	{
		SpawnPointBank.SpawnPointInfo spawnPointInfo = null;
		List<SpawnPointBank.SpawnPointInfo> possibleSpawnLocations = spawnPointBank.GetPossibleSpawnLocations();
		if (Singleton.Manager<ManTechs>.inst.Count > 0 || m_SpawnRequests.Count > 0)
		{
			bool flag = false;
			float num = 0f;
			float num2 = float.PositiveInfinity;
			SpawnPointBank.SpawnPointInfo result = null;
			float num3 = float.MinValue;
			SpawnPointBank.SpawnPointInfo result2 = null;
			float idealEnemySpawnDistance = Singleton.Manager<ManNetwork>.inst.NetController.IdealEnemySpawnDistance;
			float num4 = idealEnemySpawnDistance * idealEnemySpawnDistance;
			float idealAllySpawnDistance = Singleton.Manager<ManNetwork>.inst.NetController.IdealAllySpawnDistance;
			float num5 = idealAllySpawnDistance * idealAllySpawnDistance;
			foreach (SpawnPointBank.SpawnPointInfo item in possibleSpawnLocations)
			{
				if (!item.IsAvailable)
				{
					continue;
				}
				Vector3 position = item.position;
				d.Assert(IsPositionWithinRange(position, spawnParams.m_MaxSpawnRange), "SpawnPoint up for consideration is outside the spawn range!??");
				float closestDistSqr;
				bool closestDistanceSqrFromPointToOtherTechs = GetClosestDistanceSqrFromPointToOtherTechs(position, spawnParams, out closestDistSqr, TeamAffiliation.Enemy);
				float closestDistSqr2;
				bool closestDistanceSqrFromPointToOtherTechs2 = GetClosestDistanceSqrFromPointToOtherTechs(position, spawnParams, out closestDistSqr2, TeamAffiliation.Ally, playerOccupiedTechOnly: true);
				float closestDistanceSqrFromPointToReservedSpawns = GetClosestDistanceSqrFromPointToReservedSpawns(position, spawnParams, TeamAffiliation.Enemy);
				float closestDistanceSqrFromPointToReservedSpawns2 = GetClosestDistanceSqrFromPointToReservedSpawns(position, spawnParams, TeamAffiliation.Ally);
				float num6 = Mathf.Min(closestDistSqr, closestDistanceSqrFromPointToReservedSpawns);
				float num7 = Mathf.Min(closestDistSqr2, closestDistanceSqrFromPointToReservedSpawns2);
				if (num6 > num4 && num7 > num5)
				{
					if ((!closestDistanceSqrFromPointToOtherTechs || num6 >= num) && (!closestDistanceSqrFromPointToOtherTechs2 || num7 <= num2))
					{
						num = num6;
						num2 = num7;
						result = item;
						flag = true;
					}
				}
				else if (!flag)
				{
					float num8 = Mathf.Min(num7, num6);
					if (num8 > num3)
					{
						num3 = num8;
						result2 = item;
					}
				}
			}
			if (flag)
			{
				return result;
			}
			return result2;
		}
		int index = UnityEngine.Random.Range(0, possibleSpawnLocations.Count);
		return possibleSpawnLocations[index];
	}

	private Vector3 FindSafeSpawnPos(SpawnParams spawnParams)
	{
		Vector3 vector = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition;
		float num = 0f;
		float num2 = float.PositiveInfinity;
		float num3 = spawnParams.m_MaxSpawnRange * 2f;
		float num4 = 75f;
		int num5 = Mathf.CeilToInt(num3 / num4);
		Vector3 vector2 = Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition - new Vector3(num3 / 2f, 0f, num3 / 2f);
		Vector3 scenePos = vector2;
		for (int i = 0; i < num5; i++)
		{
			for (int j = 0; j < num5; j++)
			{
				if (IsPositionWithinRange(scenePos, spawnParams.m_MaxSpawnRange) && Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(in scenePos))
				{
					float closestDistSqr;
					bool flag = GetClosestDistanceSqrFromPointToOtherTechs(scenePos, spawnParams, out closestDistSqr, TeamAffiliation.Ally, playerOccupiedTechOnly: true);
					if (!flag)
					{
						flag = GetClosestDistanceSqrFromPointToOtherTechs(scenePos, spawnParams, out closestDistSqr, TeamAffiliation.Ally);
					}
					float closestDistSqr2;
					bool closestDistanceSqrFromPointToOtherTechs = GetClosestDistanceSqrFromPointToOtherTechs(scenePos, spawnParams, out closestDistSqr2, TeamAffiliation.Enemy);
					if (!flag && !closestDistanceSqrFromPointToOtherTechs)
					{
						closestDistSqr = (Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition - scenePos).sqrMagnitude;
						flag = true;
					}
					if (closestDistSqr < num2 || (!flag && closestDistSqr2 > num))
					{
						Vector3 vector3 = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
						if (Singleton.Manager<ManWorld>.inst.CheckIfInsideSceneryBlocker(SceneryBlocker.BlockMode.Spawn, vector3, 5f))
						{
							d.Log($"FindSafeSpawnPos: Reject {vector3} as it overlaps a scenery blocker");
						}
						else
						{
							d.Log($"FindSafeSpawnPos: Had pos {vector} at {Mathf.Sqrt(num2)} distance ({Mathf.Sqrt(num)} from enemy) but found better pos {scenePos} at {Mathf.Sqrt(closestDistSqr)} units ({Mathf.Sqrt(closestDistSqr2)} from enemy)");
							num = closestDistSqr2;
							num2 = closestDistSqr;
							vector = scenePos;
						}
					}
				}
				scenePos.x += num4;
			}
			scenePos.x = vector2.x;
			scenePos.z += num4;
		}
		return vector;
	}

	private bool BoundsFreeSpaceRejectFunction(Vector3 position, float radius, object context)
	{
		bool flag = !IsPositionWithinRange(position, m_SpawnRequests[0].m_SpawnParams.m_MaxSpawnRange);
		if (context is ManFreeSpace.FreeSpaceParams.RejectFunction rejectFunction && !flag)
		{
			flag = rejectFunction(position, radius, context);
		}
		return flag;
	}

	private bool SpawnPointFreeSpaceRejectFunction(Vector3 position, float radius, object context)
	{
		SpawnRequest spawnRequest = m_SpawnRequests[0];
		float sqrMagnitude = (position - spawnRequest.m_DesiredSpawnPosition).SetY(0f).sqrMagnitude;
		float num = Singleton.Manager<ManNetwork>.inst.SpawnPrefab.Radius - radius;
		bool flag = !(sqrMagnitude <= num * num) || !IsPositionWithinRange(position, spawnRequest.m_SpawnParams.m_MaxSpawnRange);
		if (context is ManFreeSpace.FreeSpaceParams.RejectFunction rejectFunction && !flag)
		{
			flag = rejectFunction(position, radius, context);
		}
		return flag;
	}

	private bool IsPositionWithinRange(Vector3 pos, float maxRange)
	{
		return (pos - Singleton.Manager<ManWorld>.inst.FocalPoint.ScenePosition).SetY(0f).sqrMagnitude <= maxRange * maxRange;
	}

	private Quaternion DetermineSpawnRotation(Vector3 spawnPos)
	{
		NetController.SpawnOrientation currentSpawnOrientation = Singleton.Manager<ManNetwork>.inst.NetController.CurrentSpawnOrientation;
		switch (currentSpawnOrientation)
		{
		case NetController.SpawnOrientation.Random:
			return Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.up);
		case NetController.SpawnOrientation.FacingOutwards:
		case NetController.SpawnOrientation.FacingInwards:
		{
			Vector3 vector = spawnPos.SetY(0f);
			Vector3 vector2 = ((vector != Vector3.zero) ? vector.normalized : Vector3.forward);
			if (currentSpawnOrientation == NetController.SpawnOrientation.FacingInwards)
			{
				vector2 = -vector2;
			}
			return Quaternion.LookRotation(vector2);
		}
		default:
			d.AssertFormat(false, "NetPlayer.NetTechSpawnParams.DetermineSpawnRotation has unhandled rotation type {0}", currentSpawnOrientation);
			return Quaternion.identity;
		}
	}

	private void OnFreeSpaceFound(WorldPosition? foundPosition)
	{
		SpawnRequest spawnRequest = m_SpawnRequests[0];
		m_SpawnRequests.RemoveAt(0);
		d.Log($"TechSpawnHelper OnFreeSpaceFound retry={spawnRequest.m_SpawnRetryCount} foundPosition={foundPosition}");
		if (!foundPosition.HasValue && spawnRequest.m_SpawnRetryCount > 5)
		{
			foundPosition = Singleton.Manager<ManWorld>.inst.FocalPoint;
			d.Log($"TechSpawnHelper OnFreeSpaceFound final fallback retry={spawnRequest.m_SpawnRetryCount} foundPosition={foundPosition}");
		}
		if (foundPosition.HasValue)
		{
			Vector3 scenePosition = foundPosition.Value.ScenePosition;
			scenePosition = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePosition);
			float y = spawnRequest.m_SpawnParams.m_TechData.m_BoundsExtents.y;
			scenePosition.y += y;
			Quaternion quaternion = DetermineSpawnRotation(scenePosition);
			if (spawnRequest.m_SpawnParams.m_EncounterFacingDir.magnitude > 0f)
			{
				quaternion = Quaternion.LookRotation(spawnRequest.m_SpawnParams.m_EncounterFacingDir);
			}
			NetworkInstanceId playerID = NetworkInstanceId.Invalid;
			if (spawnRequest.m_SpawnParams.m_Player != null)
			{
				playerID = spawnRequest.m_SpawnParams.m_Player.netId;
			}
			OnBeforeSpawnTech.Send(spawnRequest.m_SpawnParams.m_TechData, scenePosition, quaternion, spawnRequest.m_SpawnParams.m_Player);
			Singleton.Manager<ManNetTechs>.inst.ServerSpawnTech(spawnRequest.m_SpawnParams.m_TechData, WorldPosition.FromScenePosition(in scenePosition), quaternion, spawnRequest.m_SpawnParams.m_TeamID, playerID, spawnRequest.m_SpawnParams.m_UseInventory, spawnRequest.m_SpawnParams.m_Player.IsNotNull() ? spawnRequest.m_SpawnParams.m_Player.netId : NetworkInstanceId.Invalid, playerCalledSpawn: true, spawnRequest.m_SpawnParams.m_AllowSpawnWithBlocksMissing);
		}
		else if (spawnRequest.m_SpawnRetryCount < 5)
		{
			if (spawnRequest.m_DesiredSpawnPoint != null)
			{
				spawnRequest.m_DesiredSpawnPoint.SetTemporarilyBlocked();
			}
			SpawnTechAtOptimalPosition(spawnRequest.m_SpawnParams, spawnRequest.m_SpawnPolicy, spawnRequest.m_SpawnPointBank, spawnRequest.m_SpawnRetryCount + 1);
		}
		else
		{
			SpawnTechAtOptimalPosition(spawnRequest.m_SpawnParams, NetController.SpawnPolicy.CloseToAllies, spawnRequest.m_SpawnPointBank, spawnRequest.m_SpawnRetryCount + 1);
		}
	}
}
