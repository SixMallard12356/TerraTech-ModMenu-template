using System.Collections.Generic;
using Newtonsoft.Json;

public class MultiObjectSpawner
{
	[JsonObject(MemberSerialization.OptIn)]
	public class DebugHistory
	{
		private const int kHistoryLen = 10;

		[JsonProperty]
		private string[] m_Logs;

		[JsonProperty]
		private int m_Next;

		public DebugHistory()
		{
			m_Logs = new string[10];
			m_Next = 0;
		}

		public void Clear()
		{
			for (int i = 0; i < 10; i++)
			{
				m_Logs[i] = string.Empty;
			}
			m_Next = 0;
		}

		public void AddLog(string log)
		{
			m_Logs[m_Next] = log;
			m_Next = (m_Next + 1) % 10;
		}

		public void CopyFrom(DebugHistory other)
		{
			for (int i = 0; i < 10; i++)
			{
				m_Logs[i] = other.m_Logs[i];
			}
			m_Next = other.m_Next;
		}
	}

	public Event<TrackedVisible, string, PerVisibleParams> OnObjectSpawned;

	private Dictionary<string, ObjectSpawner> m_PendingSpawns = new Dictionary<string, ObjectSpawner>();

	private DebugHistory m_DebugHistory = new DebugHistory();

	public void Clear()
	{
		foreach (KeyValuePair<string, ObjectSpawner> pendingSpawn in m_PendingSpawns)
		{
			ObjectSpawner value = pendingSpawn.Value;
			value.Cancel();
			value.OnObjectSpawned.Unsubscribe(OnSpawnInternal);
		}
		m_PendingSpawns.Clear();
		m_DebugHistory.Clear();
	}

	public void Load(Dictionary<string, ObjectSpawner.SaveData> saveData, DebugHistory history, bool autoRetry)
	{
		Clear();
		if (saveData != null)
		{
			foreach (KeyValuePair<string, ObjectSpawner.SaveData> saveDatum in saveData)
			{
				ObjectSpawner.SaveData value = saveDatum.Value;
				ObjectSpawner objectSpawner = new ObjectSpawner();
				objectSpawner.OnObjectSpawned.Subscribe(OnSpawnInternal);
				m_PendingSpawns.Add(saveDatum.Key, objectSpawner);
				objectSpawner.Load(value, autoRetry);
			}
		}
		if (history != null)
		{
			m_DebugHistory.CopyFrom(history);
		}
	}

	public void Save(ref Dictionary<string, ObjectSpawner.SaveData> saveData, ref DebugHistory history)
	{
		if (saveData != null)
		{
			saveData.Clear();
		}
		else
		{
			saveData = new Dictionary<string, ObjectSpawner.SaveData>();
		}
		foreach (KeyValuePair<string, ObjectSpawner> pendingSpawn in m_PendingSpawns)
		{
			ObjectSpawner.SaveData value = pendingSpawn.Value.Save();
			saveData.Add(pendingSpawn.Key, value);
		}
		if (history == null)
		{
			history = new DebugHistory();
		}
		history.CopyFrom(m_DebugHistory);
	}

	public void TrySpawn(ManSpawn.ObjectSpawnParams objectSpawnParams, ManFreeSpace.FreeSpaceParams freeSpaceParams, string name, bool autoRetry)
	{
		TrySpawn(objectSpawnParams, freeSpaceParams, null, name, autoRetry);
	}

	public void TrySpawn(ManSpawn.ObjectSpawnParams objectSpawnParams, ManFreeSpace.FreeSpaceParams freeSpaceParams, PerVisibleParams encounterParams, string name, bool autoRetry)
	{
		if (!m_PendingSpawns.ContainsKey(name))
		{
			ObjectSpawner objectSpawner = new ObjectSpawner();
			objectSpawner.OnObjectSpawned.Subscribe(OnSpawnInternal);
			m_PendingSpawns.Add(name, objectSpawner);
			objectSpawner.TrySpawn(objectSpawnParams, freeSpaceParams, encounterParams, name, autoRetry);
		}
		else
		{
			m_DebugHistory.AddLog($"already spawning {name}");
		}
	}

	private void OnSpawnInternal(TrackedVisible tv, string name, PerVisibleParams encounterParams, string debugLog)
	{
		if (m_PendingSpawns.TryGetValue(name, out var value))
		{
			value.OnObjectSpawned.Unsubscribe(OnSpawnInternal);
			m_PendingSpawns.Remove(name);
		}
		m_DebugHistory.AddLog(debugLog);
		if (tv != null)
		{
			OnObjectSpawned.Send(tv, name, encounterParams);
		}
	}
}
