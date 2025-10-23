#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ManTechs : Singleton.Manager<ManTechs>, Mode.IManagerModeEvents
{
	public struct TeamChangeInfo
	{
		public int m_OldTeam;

		public int m_NewTeam;

		public bool m_OldIsPopulation;

		public bool m_NewIsPopulation;
	}

	private class TankWrapper
	{
		public Tank tank;

		public void ConnectEvents()
		{
			tank.PostSpawnEvent.Subscribe(OnTankSpawned);
			tank.DamageEvent.Subscribe(OnDamaged);
			tank.visible.RecycledEvent.Subscribe(OnVisibleRecycled);
			tank.AttachEvent.Subscribe(OnAttached);
			tank.DetachEvent.Subscribe(OnDetached);
			tank.TriggerEvent.Subscribe(OnTrigger);
			tank.Boosters.FuelEmptyEvent.Subscribe(OnFuelEmpty);
			tank.Boosters.FuelFullEvent.Subscribe(OnFuelFull);
		}

		public void DisconnectEvents()
		{
			tank.PostSpawnEvent.Unsubscribe(OnTankSpawned);
			tank.DamageEvent.Unsubscribe(OnDamaged);
			tank.AttachEvent.Unsubscribe(OnAttached);
			tank.DetachEvent.Unsubscribe(OnDetached);
			tank.TriggerEvent.Unsubscribe(OnTrigger);
			tank.Boosters.FuelEmptyEvent.Unsubscribe(OnFuelEmpty);
			tank.Boosters.FuelFullEvent.Unsubscribe(OnFuelFull);
		}

		private void OnTankSpawned()
		{
			Singleton.Manager<ManTechs>.inst.TankPostSpawnEvent.Send(tank);
		}

		private void OnDamaged(ManDamage.DamageInfo info)
		{
			Singleton.Manager<ManTechs>.inst.TankDamagedEvent.Send(tank, info);
		}

		private void OnVisibleRecycled(Visible visible)
		{
			if (visible.Killed)
			{
				Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Send(tank, tank.FatalDamage);
				if (tank.DamagedByPlayer)
				{
					FactionSubTypes mainCorp = tank.GetMainCorp();
					if (tank.OriginalValue > 0f)
					{
						Singleton.Manager<ManStats>.inst.EnemyTechDestroyed(tank, mainCorp, tank.FatalDamage);
					}
					if (Singleton.Manager<ManGameMode>.inst.CanEarnXp())
					{
						int num = (int)(tank.OriginalValue * Globals.inst.m_TechDestroyedXPMultiplier);
						if (ManNetwork.IsHost)
						{
							Singleton.Manager<ManLicenses>.inst.AddXP(mainCorp, num);
							WorldPosition position = Singleton.Manager<ManOverlay>.inst.WorldPositionForFloatingText(visible);
							if (num != 0)
							{
								Singleton.Manager<ManOverlay>.inst.AddFloatingTextOverlayXP(num, position);
							}
							if (Singleton.Manager<ManNetwork>.inst.IsServer)
							{
								PopupNumberMessage message = new PopupNumberMessage
								{
									m_Type = PopupNumberMessage.Type.XP,
									m_Number = num,
									m_Position = position
								};
								Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.AddFloatingNumberPopupMessage, message);
							}
						}
					}
					if (Singleton.Manager<ManGameMode>.inst.CanEarnMoney() && ManNetwork.IsHost)
					{
						int amount = (int)(tank.OriginalValue * Globals.inst.m_TechDestroyedBBMultiplier);
						Singleton.Manager<ManPlayer>.inst.AddMoney(amount);
					}
				}
			}
			Singleton.Manager<ManTechs>.inst.TankRecycledEvent.Send(visible.tank);
			visible.RecycledEvent.Unsubscribe(OnVisibleRecycled);
		}

		private void OnAttached(TankBlock block, Tank tech)
		{
			d.Assert(tech == tank);
			Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Send(tank, block);
		}

		private void OnDetached(TankBlock block, Tank tech)
		{
			d.Assert(tech == tank);
			Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Send(tank, block);
			Singleton.Manager<ManUndo>.inst.BufferDetachedBlock(block, tank);
		}

		private void OnTrigger(Collider collider, int state)
		{
			Singleton.Manager<ManTechs>.inst.TankTriggerVolumeEvent.Send(tank, collider, state);
		}

		private void OnFuelEmpty()
		{
			Singleton.Manager<ManTechs>.inst.TankFuelEmptyEvent.Send(tank);
		}

		private void OnFuelFull()
		{
			Singleton.Manager<ManTechs>.inst.TankFuelFullEvent.Send(tank);
		}
	}

	public struct TechIterator : IDisposable
	{
		private Dictionary<Tank, TankWrapper>.KeyCollection.Enumerator m_Enumerator;

		private Func<Tank, bool> m_Filter;

		public Tank Current => m_Enumerator.Current;

		public TechIterator(ManTechs manTechs, Func<Tank, bool> filter)
		{
			m_Enumerator = manTechs.m_WrappedTanksKeys.GetEnumerator();
			m_Filter = filter;
		}

		public TechIterator GetEnumerator()
		{
			return this;
		}

		public void Dispose()
		{
			m_Enumerator.Dispose();
		}

		public bool MoveNext()
		{
			do
			{
				if (!m_Enumerator.MoveNext())
				{
					return false;
				}
				if (!Current && !m_ErrorOncePerRun)
				{
					d.LogError("ignoring null tank while iterating ManTechs.m_WrappedTanks");
					m_ErrorOncePerRun = true;
				}
			}
			while (m_Filter != null && !m_Filter(Current));
			return true;
		}

		public int Count()
		{
			int num = 0;
			using TechIterator techIterator = GetEnumerator();
			while (techIterator.MoveNext())
			{
				if ((bool)techIterator.Current)
				{
					num++;
				}
			}
			return num;
		}

		public bool Any(Func<Tank, bool> predicate = null)
		{
			using (TechIterator techIterator = GetEnumerator())
			{
				while (techIterator.MoveNext())
				{
					Tank current = techIterator.Current;
					if (predicate == null || predicate(current))
					{
						return true;
					}
				}
			}
			return false;
		}

		public Tank FirstOrDefault(Func<Tank, bool> predicate = null)
		{
			using (TechIterator techIterator = GetEnumerator())
			{
				while (techIterator.MoveNext())
				{
					Tank current = techIterator.Current;
					if (predicate == null || predicate(current))
					{
						return current;
					}
				}
			}
			return null;
		}
	}

	private struct TechOverlappingTileEdgeData
	{
		public WorldPosition centrePosition;

		public QuatSerial rotation;

		public V3Serial boundsSize;

		public Transform blockerVolume;
	}

	private class SaveData
	{
		public Dictionary<int, TechOverlappingTileEdgeData> m_TechOverlappingTileEdgeData = new Dictionary<int, TechOverlappingTileEdgeData>();

		public List<string> m_TechEventLog;
	}

	[SerializeField]
	private float m_SleepRangeFromCamera = 200f;

	[SerializeField]
	private float m_SleepersWakeTestInterval = 1f;

	[SerializeField]
	private bool m_HotswappingEnabled;

	[SerializeField]
	[Tooltip("Blocker volume scaled to size of the tech - present when a tech is on a tile border and not currently loaded")]
	private Transform m_UnloadedTechBlockerPrefab;

	public List<string> dbgRO_WheelSimProfile = new List<string>();

	public Event<Tank, ManDamage.DamageInfo> TankDestroyedEvent;

	public Event<Tank> TankRecycledEvent;

	public Event<Tank, bool> PlayerTankChangedEvent;

	public Event<ControlScheme> PlayerTankControlSchemeChangedEvent;

	public Event<Tank> TankPostSpawnEvent;

	public Event<Tank, ManDamage.DamageInfo> TankDamagedEvent;

	public Event<Tank, TankBlock> TankBlockAttachedEvent;

	public Event<Tank, TankBlock> TankBlockDetachedEvent;

	public Event<Tank, Collider, int> TankTriggerVolumeEvent;

	public Event<Tank> TankFuelEmptyEvent;

	public Event<Tank> TankFuelFullEvent;

	public Event<Tank, TeamChangeInfo> TankTeamChangedEvent;

	public Event<Tank> TankDriverChangedEvent;

	public Event<Tank, TrackedVisible> TankNameChangedEvent;

	public EventNoParams PlayerTankAnchorFailedEvent;

	private Dictionary<Tank, TankWrapper> m_WrappedTanks;

	private Dictionary<Tank, TankWrapper>.KeyCollection m_WrappedTanksKeys;

	private List<Tank> m_TempWrappedTankKeys = new List<Tank>();

	private List<Tank> m_SleepingTechs = new List<Tank>();

	private float m_WakeTestTimeout;

	private Comparison<KeyValuePair<Tank, float>> TechPrioritySort;

	private List<KeyValuePair<Tank, float>> m_TechPriorityCache = new List<KeyValuePair<Tank, float>>();

	private static bool m_ErrorOncePerRun = false;

	private TechData m_LastSpawnedTech;

	private Dictionary<int, TechOverlappingTileEdgeData> m_TechOverlappingTileEdgeData = new Dictionary<int, TechOverlappingTileEdgeData>();

	private List<string> m_TechEventLog;

	private Func<Tank, bool> m_TechIsPlayerTeamDelegate;

	private Func<Tank, bool> m_TechIsEnemyTeamDelegate;

	private Func<Tank, bool> m_TechIsEnemyAITeamDelegate;

	private Func<Tank, bool> m_TechIsPlayerNonAnchoredControllableDelegate;

	private const int k_OverlapperBlockerIDOffset = 1000000000;

	private static int enemyTeamIndex = 1;

	public int Count => m_WrappedTanks.Count;

	public IEnumerable<Tank> CurrentTechs => m_WrappedTanksKeys;

	public bool HotswapEnbled
	{
		get
		{
			if (Application.isEditor)
			{
				return m_HotswappingEnabled;
			}
			return false;
		}
	}

	public ManTechs()
	{
		m_WrappedTanks = new Dictionary<Tank, TankWrapper>();
		m_WrappedTanksKeys = m_WrappedTanks.Keys;
		TechPrioritySort = TechPrioritySortFunction;
	}

	public void RegisterTank(Tank t)
	{
		if (!t)
		{
			d.LogError("ignoring null tank in ManTechs.RegisterTank()");
			return;
		}
		TankWrapper tankWrapper = new TankWrapper
		{
			tank = t
		};
		m_WrappedTanks.Add(t, tankWrapper);
		tankWrapper.ConnectEvents();
	}

	private void OnTechPostSpawned(Tank spawnedTech)
	{
		SetOverlappingTechBlockerEnabled(spawnedTech.visible.ID, enable: false);
	}

	public void UnregisterTank(Tank t)
	{
		if (m_WrappedTanks.TryGetValue(t, out var value))
		{
			m_WrappedTanks.Remove(t);
			value.DisconnectEvents();
		}
		else
		{
			d.Assert(condition: false, "despawning tank" + t.name + "unknown to TankManager");
		}
		if (m_TechOverlappingTileEdgeData.TryGetValue(t.visible.ID, out var value2))
		{
			if (t.visible.Killed)
			{
				RemoveOverlappingTechData(t.visible.ID);
			}
			else if (BlockingVolumeOverlapsLoadedTile(value2))
			{
				SetOverlappingTechBlockerEnabled(t.visible.ID, enable: true);
			}
		}
	}

	private void SetOverlappingTechBlockerEnabled(int visibleID, bool enable)
	{
		if (m_TechOverlappingTileEdgeData.TryGetValue(visibleID, out var value))
		{
			if (value.blockerVolume != null)
			{
				value.blockerVolume.Recycle();
				value.blockerVolume = null;
				RemoveSceneryBlockingVolumeForTech(visibleID);
			}
			if (enable)
			{
				value = SetupBlockingVolumeForTech(visibleID, value);
			}
			m_TechOverlappingTileEdgeData[visibleID] = value;
		}
	}

	public void AddOverlappingTechData(Visible visible)
	{
		AddOverlappingTechData(visible.ID, visible.tank.boundsCentreWorld, visible.trans.rotation, visible.tank.blockBounds.size + Vector3.one);
	}

	public void AddOverlappingTechData(ManSaveGame.StoredTech storedTech)
	{
		AddOverlappingTechData(storedTech.m_ID, storedTech.GetBackwardsCompatiblePosition(), storedTech.m_Rotation, storedTech.m_TechData.m_BoundsExtents * 2f + Vector3.one);
		SetOverlappingTechBlockerEnabled(storedTech.m_ID, enable: true);
	}

	public void RemoveOverlappingTechData(int visibleID)
	{
		SetOverlappingTechBlockerEnabled(visibleID, enable: false);
		m_TechOverlappingTileEdgeData.Remove(visibleID);
	}

	public void RequestSetPlayerTank(Tank tank, bool cancelBeam = true)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			Singleton.Manager<ManNetTechs>.inst.RequestControlTech(tank);
		}
		else
		{
			SetPlayerTankLocally(tank, cancelBeam);
		}
	}

	public void SetPlayerTankLocally(Tank tank, bool cancelBeam = true)
	{
		Tank playerTankInternal = Singleton.GetPlayerTankInternal();
		if (tank == playerTankInternal)
		{
			return;
		}
		if (playerTankInternal != null)
		{
			PlayerTankChangedEvent.Send(playerTankInternal, paramB: false);
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				TankDriverChangedEvent.Send(playerTankInternal);
			}
			if (cancelBeam)
			{
				playerTankInternal.beam.EnableBeam(enable: false, force: true);
			}
		}
		if (tank != null && !tank.gameObject.activeSelf)
		{
			d.LogErrorFormat("ManTechs.SetPlayerTank - Trying to set PlayerTank to tech {0}, but it's been recycled!", tank.name);
			tank = null;
		}
		Singleton.SetPlayerTankInternal(tank);
		if ((bool)tank)
		{
			PlayerTankChangedEvent.Send(tank, paramB: true);
			if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				TankDriverChangedEvent.Send(tank);
			}
		}
	}

	public void CheckSleepRange(Tank tech)
	{
		if ((tech.boundsCentreWorld - Singleton.cameraTrans.position).sqrMagnitude > Singleton.Manager<ManTechs>.inst.m_SleepRangeFromCamera * Singleton.Manager<ManTechs>.inst.m_SleepRangeFromCamera && !tech.IsSleeping && tech.grounded && !Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			tech.SetSleeping(sleep: true);
			tech.visible.RecycledEvent.Subscribe(SleepingTechRecycled);
			m_SleepingTechs.Add(tech);
		}
	}

	private void TestWakeSleepingTechs()
	{
		if (!(Time.time > m_WakeTestTimeout))
		{
			return;
		}
		if (m_WakeTestTimeout == 0f)
		{
			m_WakeTestTimeout = Time.time;
		}
		m_WakeTestTimeout += m_SleepersWakeTestInterval;
		for (int num = m_SleepingTechs.Count - 1; num >= 0; num--)
		{
			Tank tank = m_SleepingTechs[num];
			if (!tank.IsSleeping)
			{
				d.LogWarning(base.name + " in sleeping techs list but not asleep " + (tank.visible.isActive ? "(ACTIVE)" : "(INACTIVE)"));
				m_SleepingTechs.RemoveAt(num);
				tank.visible.RecycledEvent.Unsubscribe(SleepingTechRecycled);
			}
			else if ((tank.boundsCentreWorld - Singleton.cameraTrans.position).sqrMagnitude < Singleton.Manager<ManTechs>.inst.m_SleepRangeFromCamera * Singleton.Manager<ManTechs>.inst.m_SleepRangeFromCamera)
			{
				m_SleepingTechs.RemoveAt(num);
				tank.visible.RecycledEvent.Unsubscribe(SleepingTechRecycled);
				tank.SetSleeping(sleep: false);
			}
		}
	}

	private void UpdateTechAudioPriority()
	{
		m_TechPriorityCache.Clear();
		foreach (Tank item in IterateTechs())
		{
			float num = item.TechAudio.CalculateRawPriority();
			if (num < 0f)
			{
				item.TechAudio.PriorityRank = -1;
			}
			else
			{
				m_TechPriorityCache.Add(new KeyValuePair<Tank, float>(item, num));
			}
		}
		m_TechPriorityCache.Sort(TechPrioritySort);
		for (int i = 0; i < m_TechPriorityCache.Count; i++)
		{
			m_TechPriorityCache[i].Key.TechAudio.PriorityRank = i;
		}
	}

	private int TechPrioritySortFunction(KeyValuePair<Tank, float> a, KeyValuePair<Tank, float> b)
	{
		if (a.Value > b.Value)
		{
			return 1;
		}
		if (a.Value < b.Value)
		{
			return -1;
		}
		return 0;
	}

	public TechIterator IterateTechs()
	{
		return new TechIterator(this, null);
	}

	public TechIterator IterateTechsWhere(Func<Tank, bool> predicate = null)
	{
		return new TechIterator(this, predicate);
	}

	public TechIterator IteratePlayerTechs()
	{
		return new TechIterator(this, m_TechIsPlayerTeamDelegate);
	}

	public TechIterator IterateEnemyTechs()
	{
		return new TechIterator(this, m_TechIsEnemyTeamDelegate);
	}

	public TechIterator IteratePlayerTechsControllable()
	{
		return new TechIterator(this, m_TechIsPlayerNonAnchoredControllableDelegate);
	}

	public bool IsEnemyInRange(Vector3 position, float range, bool enemyAITeamOnly = false)
	{
		Func<Tank, bool> predicate = (enemyAITeamOnly ? m_TechIsEnemyAITeamDelegate : m_TechIsEnemyTeamDelegate);
		foreach (Tank item in IterateTechsWhere(predicate))
		{
			if ((item.boundsCentreWorld - position).sqrMagnitude <= range * range)
			{
				return true;
			}
		}
		return false;
	}

	public bool CanEnemyProximitySensitiveActionBeExecuted(Vector3 position, float range)
	{
		bool result = true;
		if (!Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCreative() && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() != ManGameMode.GameType.RaD)
		{
			result = !IsEnemyInRange(position, range, enemyAITeamOnly: true);
		}
		return result;
	}

	public bool IsAnyTechOverlappingRange(Vector3 position, float range)
	{
		foreach (Tank item in IterateTechs())
		{
			float sqrMagnitude = (item.boundsCentreWorld - position).sqrMagnitude;
			range += item.dragSphere.radius;
			if (sqrMagnitude < range * range)
			{
				return true;
			}
		}
		return false;
	}

	[Conditional("LOG_PLAYER_TECH_EVENTS")]
	public void LogTechEvent(string logFormat, params object[] args)
	{
	}

	[Conditional("LOG_PLAYER_TECH_EVENTS")]
	public void LogTechEvent(bool includeStacktrace, string logFormat, params object[] args)
	{
		if (m_TechEventLog == null)
		{
			m_TechEventLog = new List<string>();
		}
		TimeSpan timeSpan = TimeSpan.FromSeconds(Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime());
		string text = string.Concat("[", timeSpan, "] - ", string.Format(logFormat, args));
		if (includeStacktrace)
		{
			string stackTrace = d.GetStackTrace(1);
			text = text + "\t stackTrace: " + stackTrace;
		}
		m_TechEventLog.Add(text);
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		SetPlayerTankLocally(null);
		CleanupTechBlockers();
		if (m_TechEventLog != null)
		{
			m_TechEventLog.Clear();
		}
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManTechs, out var saveData) && saveData != null)
		{
			foreach (KeyValuePair<int, TechOverlappingTileEdgeData> techOverlappingTileEdgeDatum in saveData.m_TechOverlappingTileEdgeData)
			{
				int key = techOverlappingTileEdgeDatum.Key;
				TechOverlappingTileEdgeData techOverlappingTileEdgeData = techOverlappingTileEdgeDatum.Value;
				if (BlockingVolumeOverlapsLoadedTile(techOverlappingTileEdgeData))
				{
					techOverlappingTileEdgeData = SetupBlockingVolumeForTech(key, techOverlappingTileEdgeData);
				}
				m_TechOverlappingTileEdgeData[key] = techOverlappingTileEdgeData;
			}
			if (saveData.m_TechEventLog != null)
			{
				if (m_TechEventLog == null)
				{
					m_TechEventLog = new List<string>();
				}
				m_TechEventLog.AddRange(saveData.m_TechEventLog);
			}
		}
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Subscribe(OnTileLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.TileUnloadedEvent.Subscribe(OnTileUnloaded);
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		foreach (KeyValuePair<int, TechOverlappingTileEdgeData> techOverlappingTileEdgeDatum in m_TechOverlappingTileEdgeData)
		{
			TechOverlappingTileEdgeData value = techOverlappingTileEdgeDatum.Value;
			value.blockerVolume = null;
			saveData.m_TechOverlappingTileEdgeData[techOverlappingTileEdgeDatum.Key] = value;
		}
		if (m_TechEventLog != null && m_TechEventLog.Count > 0)
		{
			saveData.m_TechEventLog = new List<string>();
			saveData.m_TechEventLog.AddRange(m_TechEventLog);
		}
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManTechs, saveData);
	}

	public void ModeExit()
	{
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnTileLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.TileUnloadedEvent.Unsubscribe(OnTileUnloaded);
	}

	private bool TechIsPlayerTeam(Tank tech)
	{
		return ManSpawn.IsPlayerTeam(tech.Team);
	}

	private bool TechIsEnemyTeam(Tank tech)
	{
		return tech.IsEnemy();
	}

	private bool TechIsEnemyAITeam(Tank tech)
	{
		if (TechIsEnemyTeam(tech))
		{
			return !ManSpawn.IsPlayerTeam(tech.Team);
		}
		return false;
	}

	private static bool TechIsPlayerNonAnchoredControllable(Tank tech)
	{
		if (tech.ControllableByAnyPlayer)
		{
			return !tech.IsAnchored;
		}
		return false;
	}

	private void HotswapTech(int key)
	{
		if (!HotswapEnbled || key < 1 || key >= Globals.inst.maxHotswapSlots + 1 || Singleton.Manager<ManPurchases>.inst.IsHotswappingTechs)
		{
			return;
		}
		TechData hotswapTech = Singleton.Manager<ManPlayer>.inst.GetHotswapTech(key);
		if (!(Singleton.playerTank == null) && hotswapTech != null && hotswapTech != m_LastSpawnedTech)
		{
			IInventory<BlockTypes> inventory = Singleton.Manager<ManPurchases>.inst.GetInventory();
			if (inventory == null || inventory.HasItemsToSpawnTech(hotswapTech))
			{
				m_LastSpawnedTech = hotswapTech;
				Singleton.Manager<ManPurchases>.inst.HotswapTechs(Singleton.playerTank, hotswapTech);
			}
		}
	}

	private void AddOverlappingTechData(int visibleID, Vector3 scenePosition, Quaternion rotation, Vector3 size)
	{
		Transform transform = null;
		if (m_TechOverlappingTileEdgeData.TryGetValue(visibleID, out var value) && value.blockerVolume != null)
		{
			transform = value.blockerVolume;
			transform.position = scenePosition;
			transform.rotation = rotation;
			transform.localScale = size;
		}
		m_TechOverlappingTileEdgeData[visibleID] = new TechOverlappingTileEdgeData
		{
			centrePosition = WorldPosition.FromScenePosition(in scenePosition),
			rotation = rotation,
			boundsSize = size,
			blockerVolume = transform
		};
	}

	private TechOverlappingTileEdgeData SetupBlockingVolumeForTech(int visibleID, TechOverlappingTileEdgeData overlapData)
	{
		if (m_UnloadedTechBlockerPrefab != null)
		{
			Transform transform = m_UnloadedTechBlockerPrefab.Spawn(overlapData.centrePosition.ScenePosition, overlapData.rotation);
			transform.localScale = overlapData.boundsSize;
			overlapData.blockerVolume = transform;
		}
		SceneryBlocker blocker = SceneryBlocker.CreateSphereBlocker(SceneryBlocker.BlockMode.Regrow, overlapData.centrePosition, ((Vector3)overlapData.boundsSize).magnitude * 0.5f);
		if (!Singleton.Manager<ManWorld>.inst.HasDynamicSceneryBlocker(visibleID + 1000000000))
		{
			Singleton.Manager<ManWorld>.inst.AddDynamicSceneryBlocker(visibleID + 1000000000, blocker);
		}
		return overlapData;
	}

	private void RemoveSceneryBlockingVolumeForTech(int visibleID)
	{
		if (Singleton.Manager<ManWorld>.inst.HasDynamicSceneryBlocker(visibleID + 1000000000))
		{
			Singleton.Manager<ManWorld>.inst.RemoveDynamicSceneryBlocker(visibleID + 1000000000);
		}
	}

	private void CleanupTechBlockers()
	{
		foreach (KeyValuePair<int, TechOverlappingTileEdgeData> techOverlappingTileEdgeDatum in m_TechOverlappingTileEdgeData)
		{
			RemoveSceneryBlockingVolumeForTech(techOverlappingTileEdgeDatum.Key);
			Transform blockerVolume = techOverlappingTileEdgeDatum.Value.blockerVolume;
			if (blockerVolume != null)
			{
				blockerVolume.Recycle();
			}
		}
		m_TechOverlappingTileEdgeData.Clear();
	}

	private static bool BlockingVolumeOverlapsLoadedTile(TechOverlappingTileEdgeData overlapData)
	{
		float radius = Mathf.Max(overlapData.boundsSize.x, overlapData.boundsSize.z);
		return Singleton.Manager<ManWorld>.inst.CheckAtLeastOneTileAtPositionHasReachedLoadStep(overlapData.centrePosition.ScenePosition, radius);
	}

	private void SleepingTechRecycled(Visible sleeper)
	{
		if ((bool)sleeper && (bool)sleeper.tank)
		{
			m_SleepingTechs.Remove(sleeper.tank);
			sleeper.RecycledEvent.Unsubscribe(SleepingTechRecycled);
		}
	}

	private void SubscribeToModeCleanupEvent()
	{
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
	}

	private void OnModeCleanup(Mode mode)
	{
		CleanupTechBlockers();
	}

	private void OnTileLoaded(WorldTile tile)
	{
		List<KeyValuePair<int, TechOverlappingTileEdgeData>> list = null;
		foreach (KeyValuePair<int, TechOverlappingTileEdgeData> techOverlappingTileEdgeDatum in m_TechOverlappingTileEdgeData)
		{
			TechOverlappingTileEdgeData value = techOverlappingTileEdgeDatum.Value;
			if (!(value.blockerVolume == null))
			{
				continue;
			}
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(techOverlappingTileEdgeDatum.Key);
			if ((trackedVisible == null || !(trackedVisible.visible != null)) && BlockingVolumeOverlapsLoadedTile(value))
			{
				if (list == null)
				{
					list = new List<KeyValuePair<int, TechOverlappingTileEdgeData>>();
				}
				KeyValuePair<int, TechOverlappingTileEdgeData> item = new KeyValuePair<int, TechOverlappingTileEdgeData>(techOverlappingTileEdgeDatum.Key, value);
				list.Add(item);
			}
		}
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<int, TechOverlappingTileEdgeData> keyValuePair = list[i];
				int key = keyValuePair.Key;
				TechOverlappingTileEdgeData value2 = keyValuePair.Value;
				value2 = SetupBlockingVolumeForTech(key, value2);
				m_TechOverlappingTileEdgeData[key] = value2;
			}
		}
	}

	private void OnTileUnloaded(WorldTile tile)
	{
		List<KeyValuePair<int, TechOverlappingTileEdgeData>> list = null;
		foreach (KeyValuePair<int, TechOverlappingTileEdgeData> techOverlappingTileEdgeDatum in m_TechOverlappingTileEdgeData)
		{
			TechOverlappingTileEdgeData value = techOverlappingTileEdgeDatum.Value;
			if (value.blockerVolume != null && !BlockingVolumeOverlapsLoadedTile(value))
			{
				if (list == null)
				{
					list = new List<KeyValuePair<int, TechOverlappingTileEdgeData>>();
				}
				KeyValuePair<int, TechOverlappingTileEdgeData> item = new KeyValuePair<int, TechOverlappingTileEdgeData>(techOverlappingTileEdgeDatum.Key, value);
				list.Add(item);
			}
		}
		if (list != null)
		{
			for (int i = 0; i < list.Count; i++)
			{
				KeyValuePair<int, TechOverlappingTileEdgeData> keyValuePair = list[i];
				int key = keyValuePair.Key;
				TechOverlappingTileEdgeData value2 = keyValuePair.Value;
				RemoveSceneryBlockingVolumeForTech(key);
				value2.blockerVolume.Recycle();
				value2.blockerVolume = null;
				m_TechOverlappingTileEdgeData[key] = value2;
			}
		}
	}

	private void OnFirstFixedUpdate()
	{
		foreach (Tank wrappedTanksKey in m_WrappedTanksKeys)
		{
			m_TempWrappedTankKeys.Add(wrappedTanksKey);
		}
		foreach (Tank tempWrappedTankKey in m_TempWrappedTankKeys)
		{
			if ((bool)tempWrappedTankKey && tempWrappedTankKey.gameObject.activeSelf)
			{
				tempWrappedTankKey.CheckResetPhysics();
			}
		}
		m_TempWrappedTankKeys.Clear();
	}

	private void Awake()
	{
		m_TechIsPlayerTeamDelegate = TechIsPlayerTeam;
		m_TechIsEnemyTeamDelegate = TechIsEnemyTeam;
		m_TechIsPlayerNonAnchoredControllableDelegate = TechIsPlayerNonAnchoredControllable;
		m_TechIsEnemyAITeamDelegate = TechIsEnemyAITeam;
		TankPostSpawnEvent.Subscribe(OnTechPostSpawned);
		Singleton.DoOnceAfterStart(SubscribeToModeCleanupEvent);
		Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.FixedUpdate, ManUpdate.Order.First, OnFirstFixedUpdate, 200);
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	public void Update()
	{
		TestWakeSleepingTechs();
		UpdateTechAudioPriority();
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(33))
		{
			HotswapTech(1);
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(34))
		{
			HotswapTech(2);
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(35))
		{
			HotswapTech(3);
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(37))
		{
			HotswapTech(4);
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(38))
		{
			HotswapTech(5);
		}
	}

	public static void DrawTankPresetControls(TankPreset preset)
	{
	}
}
