#define UNITY_EDITOR
using System.Text;
using UnityEngine;

public class ObjectSpawner
{
	public struct SaveData
	{
		public ManSpawn.ObjectSpawnParams m_ObjectParams;

		public ManFreeSpace.FreeSpaceParams m_FreeSpaceParams;

		public string m_Identifier;

		public ManFreeSpace.FreeSpaceEnumeratorParams m_SearchParams;

		public bool m_BombSpawned;

		public DeliveryBombSpawner.Data m_BombData;

		public string m_DebugLog;

		public bool Valid => m_ObjectParams != null;

		public void Clear()
		{
			this = default(SaveData);
		}
	}

	public Event<Vector3, string> OnSpawnPosLocated;

	public Event<TrackedVisible, string, PerVisibleParams, string> OnObjectSpawned;

	private bool m_Spawning;

	private string m_Name;

	private string m_UniqueIdentifier;

	private ManSpawn.ObjectSpawnParams m_ObjectParams;

	private PerVisibleParams m_EncounterParams;

	private FreeSpaceFinder m_SpaceFinder = new FreeSpaceFinder();

	private DeliveryBombSpawner m_DeliveryBomb;

	private StringBuilder m_DebugLog = new StringBuilder();

	private static uint s_UniqueIdentifierIndex;

	public bool IsBusy => m_Spawning;

	public ObjectSpawner()
	{
		m_SpaceFinder.FreeSpaceFoundEvent.Subscribe(OnFreeSpaceFound);
	}

	public void TrySpawn(ManSpawn.ObjectSpawnParams objectSpawnParams, ManFreeSpace.FreeSpaceParams freeSpaceParams, PerVisibleParams encounterParams, string name, bool autoRetry)
	{
		if (m_Spawning)
		{
			d.LogError($"ObjectSpawner told to spawn {name} whilst busy, cancelling spawning {m_Name} to make way for new request");
			Cancel();
		}
		m_Spawning = true;
		if (string.IsNullOrEmpty(name))
		{
			d.LogError("ObjectSpawner.TrySpawn given NULL/empty name!");
			name = "UNKNOWN";
		}
		m_Name = name;
		m_UniqueIdentifier = GetNameAsUniqueID(name);
		m_ObjectParams = objectSpawnParams;
		m_EncounterParams = encounterParams;
		LogFormat("setup {0} retry={1}", name, autoRetry);
		m_SpaceFinder.Setup(freeSpaceParams, name, autoRetry);
	}

	public void Load(SaveData data, bool autoRetry)
	{
		if (m_Spawning)
		{
			d.LogError($"ObjectSpawner told to spawn {data.m_Identifier} whilst busy, cancelling spawning {m_Name} to make way for new request");
			Cancel();
		}
		if (data.m_ObjectParams == null)
		{
			return;
		}
		m_Name = data.m_Identifier;
		if (m_Name == null)
		{
			d.LogError("ObjectSpawner.Load - m_Name was NULL!");
			m_Name = "";
		}
		m_UniqueIdentifier = GetNameAsUniqueID(m_Name);
		m_ObjectParams = data.m_ObjectParams;
		m_Spawning = true;
		if (m_DebugLog.Length > 0)
		{
			m_DebugLog.Remove(0, m_DebugLog.Length);
		}
		m_DebugLog.Append(data.m_DebugLog);
		LogFormat("load {0} retry={1} bombSpawned={2}", m_Name, autoRetry, data.m_BombSpawned);
		if (data.m_BombSpawned)
		{
			Vector3 targetImpactPosition = data.m_BombData.m_Pos;
			if (data.m_BombData.m_WorldPosition != default(WorldPosition) && data.m_BombData.m_Pos == Vector3.zero)
			{
				targetImpactPosition = data.m_BombData.m_WorldPosition.ScenePosition;
			}
			SpawnBomb(targetImpactPosition);
		}
		m_SpaceFinder.Load(data.m_FreeSpaceParams, data.m_SearchParams, data.m_Identifier, autoRetry);
	}

	public SaveData Save()
	{
		LogFormat("save {0}", m_Name);
		return new SaveData
		{
			m_ObjectParams = m_ObjectParams,
			m_Identifier = m_Name,
			m_FreeSpaceParams = m_SpaceFinder.FreeSpaceParams,
			m_SearchParams = m_SpaceFinder.SearchParams,
			m_BombSpawned = (m_DeliveryBomb != null),
			m_BombData = ((m_DeliveryBomb != null) ? m_DeliveryBomb.GetSaveData() : null),
			m_DebugLog = m_DebugLog.ToString()
		};
	}

	public void Cancel()
	{
		if (m_Spawning)
		{
			LogFormat("cancel {0}", m_Name);
			m_SpaceFinder.Cancel();
			CleanUp(instant: true);
		}
	}

	private void LogFormat(string format, object arg0 = null, object arg1 = null, object arg2 = null, object arg3 = null)
	{
		if (m_DebugLog.Length > 0)
		{
			m_DebugLog.Append(",");
		}
		m_DebugLog.AppendFormat(format, arg0, arg1, arg2, arg3);
	}

	private void CleanUp(bool instant)
	{
		if (m_DeliveryBomb != null)
		{
			RemoveReservedSpace();
			m_DeliveryBomb.BombDeliveredEvent.Unsubscribe(SpawnObjectAndCleanUp);
			if (instant || !m_DeliveryBomb.SelfManagedRecycle)
			{
				m_DeliveryBomb.Recycle();
			}
			m_DeliveryBomb = null;
		}
		m_Name = string.Empty;
		m_UniqueIdentifier = string.Empty;
		m_ObjectParams = null;
		m_Spawning = false;
		if (m_DebugLog.Length > 0)
		{
			m_DebugLog.Remove(0, m_DebugLog.Length);
		}
	}

	private void SpawnBomb(Vector3 targetImpactPosition)
	{
		LogFormat("spawn bomb {0}", m_Name);
		DeliveryBombSpawner.ImpactMarkerType bombImpactMarkerType = m_ObjectParams.GetBombImpactMarkerType();
		m_DeliveryBomb = Singleton.Manager<ManSpawn>.inst.SpawnDeliveryBombNew(targetImpactPosition, bombImpactMarkerType, m_ObjectParams.m_DelayBeforeBombSpawn);
		m_DeliveryBomb.BombDeliveredEvent.Subscribe(SpawnObjectAndCleanUp);
		ReserveFreeSpaceAtTargetPosition(targetImpactPosition);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			SpawnDeliveryBombVisualMessage message = new SpawnDeliveryBombVisualMessage
			{
				m_Position = WorldPosition.FromScenePosition(in targetImpactPosition),
				m_ImpactMarkerType = bombImpactMarkerType,
				m_Delay = m_ObjectParams.m_DelayBeforeBombSpawn
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.SpawnDeliveryBombVisual, message);
		}
	}

	private void SpawnWithEffect(Vector3 pos)
	{
		ManSpawn.CustomSpawnEffectType customSpawnEffectType = m_ObjectParams.GetCustomSpawnEffectType();
		if (customSpawnEffectType != ManSpawn.CustomSpawnEffectType.None)
		{
			ParticleSystem customSpawnEffectPrefabs = Singleton.Manager<ManSpawn>.inst.GetCustomSpawnEffectPrefabs(customSpawnEffectType);
			if ((bool)customSpawnEffectPrefabs)
			{
				customSpawnEffectPrefabs.transform.Spawn(pos);
			}
		}
		SpawnObjectAndCleanUp(pos);
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			CustomSpawnEffectRequest message = new CustomSpawnEffectRequest
			{
				m_CustomSpawnEffectType = customSpawnEffectType,
				m_Position = pos
			};
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.CustomSpawnEffectRequest, message);
		}
	}

	private void SpawnObjectAndCleanUp(Vector3 pos)
	{
		TrackedVisible trackedVisible = m_ObjectParams.SpawnCurrentFreeSpaceObject(pos);
		if (trackedVisible != null)
		{
			LogFormat("spawn object {0} id:{1} at {2}", m_Name, trackedVisible.ID, pos);
		}
		else
		{
			LogFormat("Failed spawn object {0} at {1}", m_Name, pos);
		}
		string name = m_Name;
		string paramD = m_DebugLog.ToString();
		CleanUp(instant: false);
		OnObjectSpawned.Send(trackedVisible, name, m_EncounterParams, paramD);
	}

	private void ReserveFreeSpaceAtTargetPosition(Vector3 targetImpactPosition)
	{
		if (m_ObjectParams.ObjectSize > 0f)
		{
			ObjectTypes type;
			switch (m_ObjectParams.GetBombImpactMarkerType())
			{
			case DeliveryBombSpawner.ImpactMarkerType.Tech:
			case DeliveryBombSpawner.ImpactMarkerType.FriendlyTech:
				type = ObjectTypes.Vehicle;
				break;
			case DeliveryBombSpawner.ImpactMarkerType.Crate:
				type = ObjectTypes.Crate;
				break;
			default:
				d.LogErrorFormat("ObjectSpawner.ReserveFreeSpaceAtTargetPosition - Bomb marker type {0} is not set up to map to a valid Object Type!", m_ObjectParams.GetBombImpactMarkerType());
				type = ObjectTypes.Null;
				break;
			}
			float radius = Mathf.Max(m_ObjectParams.ObjectSize, m_DeliveryBomb.ImpactMarkerSize);
			Singleton.Manager<ManFreeSpace>.inst.AddReservedSpace(targetImpactPosition, radius, type, m_UniqueIdentifier);
			m_DeliveryBomb.BombDeliveredEvent.Subscribe(RemoveReservedSpace);
		}
	}

	private string GetNameAsUniqueID(string name)
	{
		s_UniqueIdentifierIndex++;
		if (s_UniqueIdentifierIndex == uint.MaxValue)
		{
			s_UniqueIdentifierIndex = 1u;
		}
		return $"{name}[{s_UniqueIdentifierIndex:00000}]";
	}

	private void OnFreeSpaceFound(WorldPosition? pos)
	{
		if (pos.HasValue)
		{
			LogFormat("space found {0}", m_Name);
			Vector3 scenePosition = pos.Value.ScenePosition;
			OnSpawnPosLocated.Send(scenePosition, m_Name);
			switch (m_ObjectParams.GetSpawnVisualType())
			{
			case ManSpawn.SpawnVisualType.Default:
				SpawnObjectAndCleanUp(scenePosition);
				break;
			case ManSpawn.SpawnVisualType.Bomb:
				SpawnBomb(scenePosition);
				break;
			case ManSpawn.SpawnVisualType.Effect:
				SpawnWithEffect(scenePosition);
				break;
			default:
				SpawnObjectAndCleanUp(scenePosition);
				break;
			}
		}
		else
		{
			LogFormat("failed find {0}", m_Name);
			string name = m_Name;
			string paramD = m_DebugLog.ToString();
			CleanUp(instant: true);
			OnObjectSpawned.Send(null, name, m_EncounterParams, paramD);
		}
	}

	private void RemoveReservedSpace(Vector3 pos = default(Vector3))
	{
		m_DeliveryBomb.BombDeliveredEvent.Unsubscribe(RemoveReservedSpace);
		Singleton.Manager<ManFreeSpace>.inst.RemoveReservedSpace(m_UniqueIdentifier);
	}
}
