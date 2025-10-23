#define UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Spring.Social.Twitter.Api;
using UnityEngine;

public class ManInvasion : Singleton.Manager<ManInvasion>, Mode.IManagerModeEvents, IWorldTreadmill
{
	public class Invader
	{
		public TechData m_TechData;

		public string m_Owner;

		public long m_TweetID;

		[JsonIgnore]
		public TrackedVisible m_TrackedVis;

		[JsonIgnore]
		public Tank Tech
		{
			get
			{
				if (m_TrackedVis == null || !(m_TrackedVis.visible != null))
				{
					return null;
				}
				return m_TrackedVis.visible.tank;
			}
		}

		[JsonConstructor]
		public Invader()
		{
		}

		public Invader(TechData techData, string owner, long tweetID)
		{
			m_TechData = techData;
			m_Owner = owner;
			m_TweetID = tweetID;
		}
	}

	private class SentInvader
	{
		public string m_Name;

		public int m_VisibleID = -1;

		public int m_FavouriteCount;

		public int m_LastFavouriteCount;

		public SentInvader(string name, int visID)
		{
			m_Name = name;
			m_VisibleID = visID;
		}
	}

	private class SaveData
	{
		public InvasionState m_State;

		public float m_Timer;

		public float m_InvasionTime;

		public float m_CurrentTargetAbovePlayer;

		public Invader m_ActiveInvader;

		public V3Serial m_SpawnPos;

		public WorldPosition m_SpawnPosWorld;

		public int m_InvaderID = -1;

		public bool m_WarnedPlayer;

		public bool m_InvaderAggresive;

		public long m_LastTweetID;

		public Dictionary<long, Invader> m_StoredInvaders = new Dictionary<long, Invader>();

		public List<long> m_DefeatedBy = new List<long>();

		public List<long> m_Defeated = new List<long>();

		public Dictionary<long, SentInvader> m_SentInvaderScore = new Dictionary<long, SentInvader>();

		public SentInvader m_SentInvader;

		public void ClearInvader()
		{
			m_ActiveInvader = null;
			m_InvaderID = -1;
			m_WarnedPlayer = false;
			m_InvaderAggresive = false;
		}
	}

	private enum InvasionState
	{
		Inactive,
		Waiting,
		Countdown,
		Invasion
	}

	[SerializeField]
	[Tooltip("How long before first invasion happens")]
	private float m_FirstInvasionTime = 900f;

	[Tooltip("Time between invasions")]
	[SerializeField]
	private float m_InvasionTime = 60f;

	[SerializeField]
	[Tooltip("How long before invader spawns")]
	private float m_CountdownTime = 180f;

	[SerializeField]
	[Tooltip("How long Invasion lasts")]
	private float m_InvasionLastsForTime = 300f;

	[SerializeField]
	[Tooltip("How long before invader become aggressive")]
	private float m_InvasionWarningTime = 30f;

	[Tooltip("How long after a failed spawn that we try again")]
	[SerializeField]
	private float m_TryAgainSpawnDelay = 10f;

	[SerializeField]
	private float m_MinSpawnRange = 100f;

	[SerializeField]
	private float m_MaxSpawnRange = 200f;

	[SerializeField]
	private float m_InvadersRange = 250f;

	[SerializeField]
	private float m_InvaserChaseDistance = 100f;

	[SerializeField]
	private float m_InvaderBatteryFullness = 0.5f;

	[SerializeField]
	private Projector m_TargetProjectorPrefab;

	[SerializeField]
	private float m_ProjectorHeight = 4f;

	public Biome[] m_SafeBiomes;

	public bool m_RemoveInvadersOnSpawn;

	public bool m_RemoveInvadersOnDefeat;

	public bool m_RemoveInvadersWhenDefeatBy;

	[Tooltip("Invaders Target value above the players Val")]
	public float m_TargetAbovePlayer = 100f;

	[Tooltip("How much to increment the Target value by when an Invader is defeated")]
	public float m_TargetIncrementVal = 10f;

	[Tooltip("The Maximum Target value above the players value")]
	public float m_TargetAbovePlayerMax = 200f;

	public float m_PresetSelectStdDeviation = 100f;

	public float m_MaxDeviation = 500f;

	public float m_CheckSentInvaderTime = 300f;

	public AnimationCurve m_RiseCurve;

	public int m_MoneyOnVictory = 100;

	public int m_ChargeMaximumBatteriesCapacity = 13000;

	public Event<Tank> OnSentInvaderRemoveFromPlay;

	private bool m_Online;

	private SaveData m_SaveData = new SaveData();

	private List<Invader> m_InvaderPopulation = new List<Invader>();

	private List<Invader> m_ValidInvaderPopulation = new List<Invader>();

	private Tank m_InvaderBeingSent;

	private bool m_SendingInvader;

	private bool m_SendSuccess;

	private bool m_SendFail;

	private WorldPosition m_SpawnPos;

	private ManFreeSpace.FreeSpaceParams m_SpawnParams;

	private Projector m_TargetProjector;

	private Transform m_TargetProjectorTrans;

	private SnapshotCollectionTwitter m_LoadedCaptures;

	private TwitterAPI.TweetWithMediaDataThreaded m_FetchedTweets;

	private bool m_LoadedInvaders;

	private bool m_FetchInvaders;

	private bool m_FinishedFetchingVehicles;

	private bool m_InvadersRequireValidating;

	private int m_UpdateValidPopulationIndex;

	private const int k_InvadersToProcessPerUpdate = 100;

	private bool m_CheckingSentInvaders;

	private int m_SentInvaderCheckCount;

	private ManTimedEvents.ManagedEvent m_CheckSentInvadersEvent = new ManTimedEvents.ManagedEvent();

	private const int m_TotalTimoutTime = 10;

	private bool m_RemovingInvader;

	private string m_Hashtag = "TTInvader";

	private OnGUICallback m_GUICallbackObject;

	public bool InvasionOccurring
	{
		get
		{
			if (m_SaveData.m_State != InvasionState.Countdown)
			{
				return m_SaveData.m_State == InvasionState.Invasion;
			}
			return true;
		}
	}

	public bool IsSettingUp
	{
		get
		{
			if (!m_InvadersRequireValidating)
			{
				return m_FetchInvaders;
			}
			return true;
		}
	}

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		Init();
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManInvasion, out var saveData) && saveData != null)
		{
			m_SaveData = saveData;
			if (m_SaveData.m_InvaderID != -1)
			{
				if (m_SaveData.m_ActiveInvader != null)
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_SaveData.m_InvaderID);
					if (trackedVisible != null)
					{
						RegisterSpawnEvents(trackedVisible);
						if (m_SaveData.m_State == InvasionState.Countdown)
						{
							if (m_SaveData.m_SpawnPosWorld == default(WorldPosition))
							{
								m_SpawnPos = WorldPosition.FromGameWorldPosition((Vector3)m_SaveData.m_SpawnPos);
							}
							else
							{
								m_SpawnPos = m_SaveData.m_SpawnPosWorld;
							}
							EnableLandingMarker(enable: true);
							SetLandingMarkerPos(m_SpawnPos.ScenePosition);
						}
						m_SaveData.m_ActiveInvader.m_TrackedVis = trackedVisible;
					}
					else
					{
						m_SaveData.ClearInvader();
					}
				}
				else
				{
					m_SaveData.ClearInvader();
				}
			}
			foreach (Invader value in m_SaveData.m_StoredInvaders.Values)
			{
				m_InvaderPopulation.Add(value);
			}
			if (m_SaveData.m_State == InvasionState.Countdown)
			{
				OnEnterState_Countdown();
			}
		}
		if (!m_LoadedInvaders)
		{
			m_Online = false;
			m_FetchInvaders = true;
		}
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
	}

	public void Save(ManSaveGame.State saveState)
	{
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManInvasion, m_SaveData);
	}

	public void ModeExit()
	{
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
	}

	public float GetInvasionRange(bool includeChaseRange)
	{
		return m_InvadersRange + (includeChaseRange ? m_InvaserChaseDistance : 0f);
	}

	public Vector3 GetInvasionPosScene()
	{
		return m_SaveData.m_SpawnPosWorld.ScenePosition;
	}

	public Vector3 GetInvasionPosWorld()
	{
		return m_SaveData.m_SpawnPosWorld.GameWorldPosition;
	}

	public bool InvasionEngaged()
	{
		bool result = false;
		if (InvasionOccurring && Singleton.playerTank != null)
		{
			result = (GetInvasionPosScene() - Singleton.playerPos).SetY(0f).sqrMagnitude < m_InvadersRange * m_InvadersRange;
		}
		return result;
	}

	private void Init()
	{
		m_SaveData = new SaveData();
		m_SaveData.m_CurrentTargetAbovePlayer = m_TargetAbovePlayer;
		m_SaveData.m_ActiveInvader = null;
		m_SpawnPos = default(WorldPosition);
		m_InvaderPopulation.Clear();
		m_ValidInvaderPopulation.Clear();
		m_Online = false;
		m_InvaderBeingSent = null;
		m_SendingInvader = false;
		m_SendSuccess = false;
		m_SendFail = false;
		m_LoadedInvaders = false;
		m_FetchInvaders = false;
		m_FinishedFetchingVehicles = false;
		m_CheckingSentInvaders = false;
		m_SentInvaderCheckCount = 0;
	}

	public void Clear()
	{
		EnableLandingMarker(enable: false);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ModeTimer);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Announcement);
		UnregisterSpawnEvents();
		UnregisterDamageEventsOnActiveInvader();
	}

	public bool IsSendingInvader()
	{
		return m_InvaderBeingSent;
	}

	public void SetActive(bool active)
	{
		if (active)
		{
			if (m_SaveData.m_State == InvasionState.Inactive)
			{
				if (!m_LoadedInvaders)
				{
					m_Online = false;
					m_FetchInvaders = true;
				}
				m_SaveData.m_InvasionTime = m_FirstInvasionTime;
				EnterState(InvasionState.Waiting);
			}
		}
		else if (m_SaveData.m_State != InvasionState.Inactive)
		{
			EnterState(InvasionState.Inactive);
		}
	}

	public void StartSendingInvader(Tank invader, string vehicleName)
	{
		m_InvaderBeingSent = invader;
		m_SaveData.m_SentInvader = new SentInvader(vehicleName, invader.visible.ID);
		m_InvaderBeingSent.beam.EnableBeam(enable: true, force: true);
		BlockManager.BlockIterator<ModuleEnergyStore>.Enumerator enumerator = m_InvaderBeingSent.visible.tank.blockman.IterateBlockComponents<ModuleEnergyStore>().GetEnumerator();
		while (enumerator.MoveNext())
		{
			enumerator.Current.DrainEnergy();
		}
		TankCamera.inst.FreezeCamera(freezeCamera: true);
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = true;
		BlockManager.BlockIterator<TankBlock>.Enumerator enumerator2 = m_InvaderBeingSent.blockman.IterateBlocks().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			TankBlock current = enumerator2.Current;
			current.visible.SetLockTimout(Visible.LockTimerTypes.Interactible, 10f);
			current.visible.SetLockTimout(Visible.LockTimerTypes.Grabbable, 10f);
		}
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Snapshot);
		m_SendingInvader = true;
		m_SendSuccess = false;
		m_SendFail = false;
	}

	public void SendInvaderSuccess(Tank invader, long tweetID)
	{
		m_SaveData.m_SentInvaderScore.Add(tweetID, m_SaveData.m_SentInvader);
		m_SendSuccess = true;
	}

	public void SendInvaderFail(Tank invader)
	{
		m_SaveData.m_SentInvader = null;
		m_SendFail = true;
	}

	public TrackedVisible GetInvaderTrackedVisible()
	{
		if (m_SaveData.m_ActiveInvader == null)
		{
			return null;
		}
		return m_SaveData.m_ActiveInvader.m_TrackedVis;
	}

	public void ToggleDebugGUI()
	{
		if (m_GUICallbackObject == null)
		{
			m_GUICallbackObject = OnGUICallback.AddGUICallback(base.gameObject);
			m_GUICallbackObject.OnGUIEvent.Subscribe(DebugGUIDraw);
		}
		else
		{
			m_GUICallbackObject.OnGUIEvent.Unsubscribe(DebugGUIDraw);
			OnGUICallback.RemoveGUICallback(m_GUICallbackObject);
		}
	}

	private void UpdateSendingInvader()
	{
		if (!m_SendingInvader)
		{
			return;
		}
		if (m_SendSuccess)
		{
			float num = 75f;
			if (m_InvaderBeingSent.beam.HoverHeight >= num)
			{
				OnSentInvaderRemoveFromPlay.Send(m_InvaderBeingSent);
				m_InvaderBeingSent.beam.hoverClearance = 2f;
				m_InvaderBeingSent.beam.beamInterpSpeed = 1f;
				m_InvaderBeingSent.trans.Recycle();
				m_InvaderBeingSent = null;
				m_SendingInvader = false;
			}
			else
			{
				m_InvaderBeingSent.beam.hoverClearance = num;
				m_InvaderBeingSent.beam.beamInterpSpeed = 5f;
			}
		}
		else if (m_SendFail)
		{
			m_InvaderBeingSent.beam.EnableBeam(enable: false, force: true);
			m_SendingInvader = false;
		}
		if (!m_SendingInvader)
		{
			TankCamera.inst.FreezeCamera(freezeCamera: false);
			Singleton.Manager<ManGameMode>.inst.LockPlayerControls = false;
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
		}
	}

	private void OnEnterState_Countdown()
	{
		EnableLandingMarker(enable: true);
		SetLandingMarkerPos(m_SaveData.m_SpawnPosWorld.ScenePosition);
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 0);
		DisplayMessage(localisedString);
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ModeTimer);
	}

	private void EnterState(InvasionState state)
	{
		m_SaveData.m_State = state;
		switch (state)
		{
		case InvasionState.Waiting:
			m_SaveData.m_Timer = 0f;
			break;
		case InvasionState.Countdown:
			m_SaveData.m_SpawnPos = Vector3.zero;
			m_SaveData.m_SpawnPosWorld = m_SpawnPos;
			OnEnterState_Countdown();
			m_SaveData.m_Timer = 0f;
			break;
		case InvasionState.Invasion:
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ModeTimer);
			SpawnInvader();
			EnableLandingMarker(enable: false);
			break;
		case InvasionState.Inactive:
			break;
		}
	}

	private void UpdateState()
	{
		switch (m_SaveData.m_State)
		{
		case InvasionState.Waiting:
			if (m_SaveData.m_Timer >= m_SaveData.m_InvasionTime)
			{
				bool flag2 = true;
				bool flag3 = true;
				if (Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Valid)
				{
					for (int i = 0; i < Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.NumWeights; i++)
					{
						Biome biome = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Biome(i);
						if (biome != null && Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights.Weight(i) > 0f && m_SafeBiomes.Contains(biome))
						{
							flag2 = false;
							break;
						}
					}
				}
				else
				{
					flag2 = false;
				}
				if (flag2)
				{
					m_SaveData.m_ActiveInvader = GetInvaderToSpawn();
					if (m_SaveData.m_ActiveInvader != null)
					{
						Vector3 scenePos = Singleton.playerPos + Singleton.playerTank.trans.forward * Random.Range(m_MinSpawnRange, m_MaxSpawnRange);
						bool flag4 = Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(scenePos);
						while (!flag4)
						{
							scenePos -= Singleton.playerTank.trans.forward * 10f;
							flag4 = Singleton.Manager<ManWorld>.inst.CheckIsTileAtPositionLoaded(scenePos);
						}
						scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
						m_SpawnParams.m_CircleRadius = Mathf.Max(m_SaveData.m_ActiveInvader.m_TechData.m_BoundsExtents.x, m_SaveData.m_ActiveInvader.m_TechData.m_BoundsExtents.z) + 0.5f;
						flag2 = Singleton.Manager<ManFreeSpace>.inst.IsSpawnPosValid(in m_SpawnParams, scenePos) && !ManEncounterPlacement.IsOverlappingEncounter(scenePos, m_InvadersRange) && !Singleton.Manager<ManWorld>.inst.CircleOverlapsAnySetPieceXZ(scenePos, m_InvadersRange);
						if (flag2)
						{
							flag2 = !Singleton.Manager<ManFreeSpace>.inst.IsOverlappingSafeArea(scenePos, m_InvadersRange, Singleton.playerTank.trans);
						}
						else
						{
							flag3 = false;
						}
						m_SpawnPos = WorldPosition.FromScenePosition(in scenePos);
					}
					else
					{
						flag2 = false;
					}
				}
				if (flag2)
				{
					EnterState(InvasionState.Countdown);
				}
				else if (flag3)
				{
					m_SaveData.m_Timer = 0f;
				}
				else
				{
					m_SaveData.m_Timer = Mathf.Max(m_SaveData.m_InvasionTime - m_TryAgainSpawnDelay, 0f);
				}
			}
			else
			{
				m_SaveData.m_Timer += Time.deltaTime;
			}
			break;
		case InvasionState.Countdown:
		{
			if (m_SaveData.m_Timer >= m_CountdownTime)
			{
				EnterState(InvasionState.Invasion);
				break;
			}
			float num = m_CountdownTime - m_SaveData.m_Timer;
			int num2 = (int)(num / 60f);
			int stringID;
			if (num2 > 0)
			{
				stringID = ((num2 != 1) ? 1 : 6);
			}
			else
			{
				stringID = ((num2 == 1) ? 7 : 2);
				num2 = Mathf.RoundToInt(num);
			}
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, stringID);
			SetCoundownText(string.Format(localisedString2, num2));
			m_SaveData.m_Timer += Time.deltaTime;
			break;
		}
		case InvasionState.Invasion:
			if (m_SaveData.m_ActiveInvader != null)
			{
				if (m_SaveData.m_Timer >= m_InvasionLastsForTime)
				{
					break;
				}
				if ((bool)Singleton.playerTank && !m_SaveData.m_InvaderAggresive)
				{
					bool flag = (m_SaveData.m_ActiveInvader.m_TrackedVis.Position - Singleton.playerTank.boundsCentreWorld).magnitude < m_InvadersRange;
					if (flag && !m_SaveData.m_WarnedPlayer)
					{
						m_SaveData.m_InvasionTime = m_SaveData.m_Timer + m_InvasionWarningTime;
						m_SaveData.m_WarnedPlayer = true;
					}
					if (m_SaveData.m_WarnedPlayer)
					{
						if (m_SaveData.m_Timer > m_SaveData.m_InvasionTime)
						{
							if (flag)
							{
								SetInvaderAggressive();
							}
							else
							{
								Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ModeTimer);
							}
						}
						else
						{
							string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 9);
							SetCoundownText(string.Format(localisedString, Mathf.RoundToInt(m_SaveData.m_InvasionTime - m_SaveData.m_Timer)));
						}
					}
				}
				m_SaveData.m_Timer += Time.deltaTime;
			}
			else
			{
				m_SaveData.m_InvasionTime = m_InvasionTime;
				EnterState(InvasionState.Waiting);
			}
			break;
		case InvasionState.Inactive:
			break;
		}
	}

	private void RegisterSpawnEvents(TrackedVisible trackedVis)
	{
		trackedVis.OnRespawnEvent.Subscribe(InvaderRespawned);
		trackedVis.OnDespawnEvent.Subscribe(InvaderDespawned);
	}

	private void UnregisterSpawnEvents()
	{
		if (m_SaveData.m_ActiveInvader != null && m_SaveData.m_ActiveInvader.m_TrackedVis != null)
		{
			m_SaveData.m_ActiveInvader.m_TrackedVis.OnRespawnEvent.Unsubscribe(InvaderRespawned);
			m_SaveData.m_ActiveInvader.m_TrackedVis.OnDespawnEvent.Unsubscribe(InvaderDespawned);
		}
	}

	private void RegisterDamageEvents()
	{
		if (m_SaveData.m_ActiveInvader != null && m_SaveData.m_ActiveInvader.Tech != null)
		{
			Visible visible = m_SaveData.m_ActiveInvader.Tech.visible;
			visible.RecycledEvent.Subscribe(OnInvaderRecycled);
			visible.tank.DamageEvent.Subscribe(OnInvaderDamage);
		}
	}

	private void UnregisterDamageEventsOnActiveInvader()
	{
		if (m_SaveData.m_ActiveInvader != null && m_SaveData.m_ActiveInvader.Tech != null)
		{
			UnregisterDamageEvents(m_SaveData.m_ActiveInvader.Tech.visible);
		}
	}

	private void UnregisterDamageEvents(Visible visible)
	{
		if (visible != null)
		{
			visible.RecycledEvent.Unsubscribe(OnInvaderRecycled);
			visible.tank.DamageEvent.Unsubscribe(OnInvaderDamage);
		}
	}

	private void SpawnInvader()
	{
		if (m_SaveData.m_ActiveInvader == null)
		{
			return;
		}
		Invader activeInvader = m_SaveData.m_ActiveInvader;
		Quaternion rotation = Quaternion.LookRotation(Singleton.playerPos - m_SaveData.m_SpawnPosWorld.ScenePosition);
		TechEnergy.AddSerialData(activeInvader.m_TechData.m_TechSaveState, m_InvaderBatteryFullness);
		ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
		{
			techData = activeInvader.m_TechData,
			blockIDs = null,
			teamID = -1,
			position = m_SaveData.m_SpawnPosWorld.ScenePosition,
			rotation = rotation,
			grounded = true
		};
		activeInvader.m_TrackedVis = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
		RegisterSpawnEvents(m_SaveData.m_ActiveInvader.m_TrackedVis);
		m_SaveData.m_InvaderID = m_SaveData.m_ActiveInvader.m_TrackedVis.ID;
		if ((bool)m_SaveData.m_ActiveInvader.m_TrackedVis.visible)
		{
			SetupInvader();
			if (m_RemoveInvadersOnSpawn)
			{
				RemoveInvaderFromList(m_SaveData.m_ActiveInvader);
			}
			string message = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 3), activeInvader.m_TechData.Name, activeInvader.m_Owner);
			DisplayMessage(message);
		}
	}

	private void InvaderRespawned(Visible visible)
	{
		if (m_SaveData.m_ActiveInvader == null || m_SaveData.m_ActiveInvader.m_TrackedVis == null)
		{
			return;
		}
		if (visible != m_SaveData.m_ActiveInvader.m_TrackedVis.visible)
		{
			d.LogError("ManInvasion.InvaderRespawned TrackedVisible.Visible != respawned visible");
			if (m_SaveData.m_ActiveInvader.m_TrackedVis.visible == null)
			{
				d.LogError("ManInvasion.InvaderRespawned TrackedVisible.Visible is null");
			}
		}
		SetupInvader();
	}

	private void InvaderDespawned(Visible visible)
	{
		UnregisterDamageEventsOnActiveInvader();
	}

	private void SetupInvader()
	{
		Visible visible = m_SaveData.m_ActiveInvader.m_TrackedVis.visible;
		if ((bool)visible)
		{
			if ((bool)visible.tank)
			{
				RegisterDamageEvents();
				if (m_SaveData.m_InvaderAggresive)
				{
					SetInvaderAggressive();
				}
				else
				{
					visible.tank.AI.SetBehaviorType(AITreeType.AITypes.Idle);
				}
				float num = 0f;
				BlockManager.BlockIterator<ModuleEnergyStore>.Enumerator enumerator = visible.tank.blockman.IterateBlockComponents<ModuleEnergyStore>().GetEnumerator();
				while (enumerator.MoveNext())
				{
					ModuleEnergyStore current = enumerator.Current;
					float num2 = Mathf.Min(current.SpareCapacity, (float)m_ChargeMaximumBatteriesCapacity - num);
					current.AddEnergy(num2);
					num += num2;
				}
			}
			else
			{
				d.LogError("ManInvasion.SetupInvader - visible.tank is null");
			}
		}
		else
		{
			d.LogError("ManInvasion.SetupInvader - visible is null");
		}
	}

	private Invader GetInvaderToSpawn()
	{
		Invader result = null;
		if (m_ValidInvaderPopulation.Count > 0)
		{
			float num = (float)Singleton.playerTank.GetValue() + m_SaveData.m_CurrentTargetAbovePlayer;
			float num2 = 0f;
			float num3 = float.MaxValue;
			num2 = Maths.RandomStdDev(num, m_PresetSelectStdDeviation);
			foreach (Invader item in m_ValidInvaderPopulation)
			{
				if (Singleton.Manager<ManPop>.inst.CheckTechDataContainsPrototypeParts(item.m_TechData))
				{
					continue;
				}
				float num4 = item.m_TechData.GetValue();
				if (num4 <= num + m_MaxDeviation)
				{
					float num5 = Mathf.Abs(num4 - num2);
					if (num5 < num3)
					{
						num3 = num5;
						result = item;
					}
				}
			}
		}
		return result;
	}

	private void RemoveInvaderFromList(Invader invader)
	{
		m_ValidInvaderPopulation.Remove(invader);
		m_InvaderPopulation.Remove(invader);
		if (m_Online)
		{
			m_SaveData.m_StoredInvaders.Remove(invader.m_TweetID);
		}
	}

	private void EnableLandingMarker(bool enable)
	{
		if (m_TargetProjector != null)
		{
			m_TargetProjector.gameObject.SetActive(enable);
		}
	}

	private void SetLandingMarkerPos(Vector3 pos)
	{
		if (m_TargetProjector != null)
		{
			m_TargetProjectorTrans.position = Singleton.Manager<ManWorld>.inst.ProjectToGround(pos, hitScenery: true) + Vector3.up * m_ProjectorHeight;
		}
	}

	private void RequestInvaderValidation()
	{
		m_UpdateValidPopulationIndex = 0;
		m_InvadersRequireValidating = true;
	}

	private void GetTweetsIfLoggedOn(bool loggedIn)
	{
		m_Online = loggedIn;
		m_FetchInvaders = true;
	}

	private void FetchInvaders()
	{
		if (m_Online)
		{
			m_LoadedCaptures = new SnapshotCollectionTwitter(Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleModeRestriction, Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleSubModeRestriction, Singleton.Manager<ManGameMode>.inst.NextModeSetting.m_LoadVehicleUserDataRestriction);
			m_FetchedTweets = new TwitterAPI.TweetWithMediaDataThreaded();
			m_FinishedFetchingVehicles = false;
			Singleton.Manager<TwitterAPI>.inst.RetrieveTaggedTweetsSinceAsync(m_Hashtag, myTweets: false, m_SaveData.m_LastTweetID, m_FetchedTweets, OnFinishedLoadingCaptures);
			StartCoroutine(GetInvadersFromTweets());
		}
		else
		{
			LoadOfflineInvaders();
			RequestInvaderValidation();
		}
		m_LoadedInvaders = true;
	}

	private void LoadOfflineInvaders()
	{
		long tweetID = -1L;
		string owner = "Payload Studios";
		TankPreset[] offlineInvaderPresets = Singleton.Manager<ManPresetFilter>.inst.PopulationTable.m_OfflineInvaderPresets;
		foreach (TankPreset tankPreset in offlineInvaderPresets)
		{
			if (tankPreset != null)
			{
				TechData techDataFormatted = tankPreset.GetTechDataFormatted();
				if (techDataFormatted.m_BlockSpecs != null && techDataFormatted.m_BlockSpecs.Count > 1)
				{
					Invader item = new Invader(techDataFormatted, owner, tweetID);
					m_InvaderPopulation.Add(item);
				}
			}
		}
	}

	private void OnFinishedLoadingCaptures()
	{
		m_FinishedFetchingVehicles = true;
	}

	private IEnumerator GetInvadersFromTweets()
	{
		int currentTweet = 0;
		int currentCapture = 0;
		while (!m_FinishedFetchingVehicles || (m_FetchedTweets.m_Links.Count > 0 && currentTweet != m_FetchedTweets.m_Links.Count))
		{
			lock (m_FetchedTweets.m_Lock)
			{
				for (; currentTweet < m_FetchedTweets.m_Links.Count(); currentTweet++)
				{
					TwitterAPI.TweetWithMedia tweetData = m_FetchedTweets.m_Links[currentTweet];
					yield return m_LoadedCaptures.TryAddFromImage(tweetData);
				}
			}
			for (; currentCapture < m_LoadedCaptures.Snapshots.Count; currentCapture++)
			{
				SnapshotTwitter snapshotTwitter = m_LoadedCaptures.Snapshots[currentCapture];
				if (snapshotTwitter.techData.m_BlockSpecs != null && snapshotTwitter.techData.m_BlockSpecs.Count > 1)
				{
					if (snapshotTwitter.creator != Singleton.Manager<TwitterAPI>.inst.ScreenName)
					{
						Invader invader = new Invader(snapshotTwitter.techData, snapshotTwitter.creator, snapshotTwitter.tweetID);
						if (!m_SaveData.m_StoredInvaders.ContainsKey(snapshotTwitter.tweetID))
						{
							m_SaveData.m_StoredInvaders.Add(snapshotTwitter.tweetID, invader);
							m_InvaderPopulation.Add(invader);
						}
					}
					m_SaveData.m_LastTweetID = snapshotTwitter.tweetID;
				}
				yield return null;
			}
			yield return null;
		}
		d.Log($"ManInvasion - Loaded {m_InvaderPopulation.Count} invaders from Twitter population.");
		if (m_InvaderPopulation.Count == 0)
		{
			m_Online = false;
			LoadOfflineInvaders();
		}
		RequestInvaderValidation();
	}

	private void UpdateValidPopulation()
	{
		bool inlcudeInactiveDLC = true;
		int num = 0;
		bool flag = false;
		while (m_UpdateValidPopulationIndex < m_InvaderPopulation.Count && !flag)
		{
			bool flag2 = true;
			List<TankPreset.BlockSpec> blockSpecs = m_InvaderPopulation[m_UpdateValidPopulationIndex].m_TechData.m_BlockSpecs;
			for (int i = 0; i < blockSpecs.Count; i++)
			{
				if (!Singleton.Manager<ManSpawn>.inst.IsTankBlockLoaded(blockSpecs[i].GetBlockType(), inlcudeInactiveDLC))
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				m_ValidInvaderPopulation.Add(m_InvaderPopulation[m_UpdateValidPopulationIndex]);
			}
			m_UpdateValidPopulationIndex++;
			num++;
			flag = num >= 100;
		}
		if (m_UpdateValidPopulationIndex >= m_InvaderPopulation.Count)
		{
			m_UpdateValidPopulationIndex = 0;
			m_InvadersRequireValidating = false;
		}
	}

	private void SetInvaderAggressive()
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ModeTimer);
		if (m_SaveData.m_ActiveInvader != null && (bool)m_SaveData.m_ActiveInvader.Tech)
		{
			m_SaveData.m_ActiveInvader.Tech.AI.SetBehaviorType(AITreeType.AITypes.Invader);
			m_SaveData.m_ActiveInvader.Tech.AI.SetPOI(usePOI: true, m_SaveData.m_SpawnPosWorld.ScenePosition, m_InvadersRange + m_InvaserChaseDistance);
			m_SaveData.m_InvaderAggresive = true;
		}
	}

	private void OnInvaderDamage(ManDamage.DamageInfo info)
	{
		if (!m_SaveData.m_InvaderAggresive && (bool)Singleton.playerTank && info.SourceTank == Singleton.playerTank)
		{
			SetInvaderAggressive();
		}
	}

	private void OnInvaderRecycled(Visible visible)
	{
		if (!visible.Killed)
		{
			return;
		}
		if (m_SaveData.m_ActiveInvader != null && !m_RemovingInvader)
		{
			if (m_Online && !m_SaveData.m_Defeated.Contains(m_SaveData.m_ActiveInvader.m_TweetID))
			{
				m_SaveData.m_Defeated.Add(m_SaveData.m_ActiveInvader.m_TweetID);
				if (m_RemoveInvadersOnDefeat)
				{
					RemoveInvaderFromList(m_SaveData.m_ActiveInvader);
				}
			}
			string message = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 4), m_SaveData.m_ActiveInvader.m_TechData.Name, m_SaveData.m_ActiveInvader.m_Owner);
			DisplayMessage(message);
			Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(new ManOnScreenMessages.OnScreenMessage(LocalisationEnums.FTUEMessages.msgToInvade, ManOnScreenMessages.MessagePriority.Medium));
			float a = m_SaveData.m_CurrentTargetAbovePlayer + m_TargetIncrementVal;
			m_SaveData.m_CurrentTargetAbovePlayer = Mathf.Min(a, m_TargetAbovePlayerMax);
			Singleton.Manager<ManPlayer>.inst.AddMoney(m_MoneyOnVictory);
			Singleton.Manager<ManStats>.inst.InvaderDestroyed(m_SaveData.m_ActiveInvader, visible.tank.FatalDamage);
		}
		UnregisterSpawnEvents();
		UnregisterDamageEvents(visible);
		m_SaveData.m_ActiveInvader = null;
		m_SaveData.ClearInvader();
	}

	private void OnTankKilled(Tank tank, ManDamage.DamageInfo info)
	{
		if (m_SaveData.m_ActiveInvader == null || m_SaveData.m_ActiveInvader.m_TrackedVis == null || !(m_SaveData.m_ActiveInvader.m_TrackedVis.visible != null))
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		if (tank == Singleton.playerTank)
		{
			flag2 = true;
		}
		else if (ManSpawn.IsPlayerTeam(tank.Team) && Singleton.playerTank == null)
		{
			flag2 = true;
		}
		if (!flag2)
		{
			return;
		}
		if ((bool)info.SourceTank && info.SourceTank == m_SaveData.m_ActiveInvader.Tech)
		{
			flag = true;
		}
		string text = null;
		if (flag)
		{
			text = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 5), m_SaveData.m_ActiveInvader.m_TechData.Name, m_SaveData.m_ActiveInvader.m_Owner);
			if (m_Online && !m_SaveData.m_DefeatedBy.Contains(m_SaveData.m_ActiveInvader.m_TweetID))
			{
				Singleton.Manager<TwitterAPI>.inst.FlagTweetAsFavouriteAsync(m_SaveData.m_ActiveInvader.m_TweetID);
				m_SaveData.m_DefeatedBy.Add(m_SaveData.m_ActiveInvader.m_TweetID);
				if (m_RemoveInvadersWhenDefeatBy)
				{
					RemoveInvaderFromList(m_SaveData.m_ActiveInvader);
				}
			}
		}
		else
		{
			text = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.InGameMessages, 8);
		}
		DisplayMessage(text);
		Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(Time.time + 2.5f, RemoveInvader);
	}

	private void RemoveInvader()
	{
		if (m_SaveData.m_ActiveInvader != null && (bool)m_SaveData.m_ActiveInvader.Tech)
		{
			Visible visible = m_SaveData.m_ActiveInvader.m_TrackedVis.visible;
			if (visible.isActive)
			{
				m_RemovingInvader = true;
				visible.RemoveFromGame();
				m_RemovingInvader = false;
			}
		}
	}

	private void CheckSentInvadersTimed()
	{
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>())
		{
			Singleton.Manager<TwitterAPI>.inst.TryAuthenticate(SentInvaderCheckCallback);
			m_CheckingSentInvaders = true;
			StartCoroutine(CheckSentInvaders());
		}
		m_CheckSentInvadersEvent.Reset(m_CheckSentInvaderTime);
	}

	private void SentInvaderCheckCallback(bool loggedIn)
	{
		if (loggedIn)
		{
			foreach (long key in m_SaveData.m_SentInvaderScore.Keys)
			{
				Singleton.Manager<TwitterAPI>.inst.RetrieveTweetAsync(key, SentInvaderStatus);
			}
			m_SentInvaderCheckCount = m_SaveData.m_SentInvaderScore.Count;
		}
		else
		{
			m_SentInvaderCheckCount = 0;
		}
		m_CheckingSentInvaders = false;
	}

	private void SentInvaderStatus(Tweet tweet)
	{
		if (tweet != null)
		{
			m_SaveData.m_SentInvaderScore[tweet.ID].m_FavouriteCount = tweet.FavoriteCount;
		}
		m_SentInvaderCheckCount--;
	}

	private IEnumerator CheckSentInvaders()
	{
		while (m_CheckingSentInvaders || m_SentInvaderCheckCount > 0)
		{
			yield return null;
		}
		foreach (long key in m_SaveData.m_SentInvaderScore.Keys)
		{
			SentInvader sentInvader = m_SaveData.m_SentInvaderScore[key];
			if (sentInvader.m_FavouriteCount > sentInvader.m_LastFavouriteCount)
			{
				int num = sentInvader.m_FavouriteCount - sentInvader.m_LastFavouriteCount;
				ManOnScreenMessages.OnScreenMessage message = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Invader " + sentInvader.m_Name + " has Defeated " + num + " more opponents. " + sentInvader.m_FavouriteCount + " Total Kills!" }, ManOnScreenMessages.MessagePriority.Medium);
				Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(message, boolVal: false);
				sentInvader.m_LastFavouriteCount = sentInvader.m_FavouriteCount;
			}
			yield return null;
		}
	}

	private void DisplayMessage(string message)
	{
		UIOnScreenAnnouncement.MessageParams context = new UIOnScreenAnnouncement.MessageParams
		{
			m_MessageText = message,
			m_FadeInTime = 0.5f,
			m_ShowTime = 0.5f,
			m_FadeOutTime = 0.5f
		};
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Announcement, context);
	}

	private void SetCoundownText(string text)
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ModeTimer, text);
	}

	public void OnMoveWorldOrigin(IntVector3 amountMoved)
	{
		if (m_TargetProjector != null && m_TargetProjector.gameObject.activeSelf)
		{
			m_TargetProjectorTrans.position += amountMoved;
		}
	}

	private void DebugGUIDraw()
	{
		string text = "";
		switch (m_SaveData.m_State)
		{
		case InvasionState.Countdown:
			text = "Invader Arrival Imminent";
			break;
		case InvasionState.Inactive:
			text = "Invader Inactive";
			break;
		case InvasionState.Invasion:
			text = "Invader In World";
			break;
		case InvasionState.Waiting:
			text = "Invader Delay:" + (int)(m_SaveData.m_InvasionTime - m_SaveData.m_Timer) + "s";
			break;
		}
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.fontSize = 30;
		GUI.Label(new Rect(10f, Screen.height / 2, 200f, 50f), text, gUIStyle);
	}

	private void Start()
	{
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankKilled);
		m_CheckSentInvadersEvent.Set(m_CheckSentInvaderTime, CheckSentInvadersTimed);
		if ((bool)m_TargetProjectorPrefab)
		{
			m_TargetProjector = Object.Instantiate(m_TargetProjectorPrefab);
			m_TargetProjectorTrans = m_TargetProjector.transform;
			m_TargetProjectorTrans.parent = base.transform;
			m_TargetProjector.gameObject.SetActive(value: false);
		}
		m_SpawnParams = new ManFreeSpace.FreeSpaceParams
		{
			m_ObjectsToAvoid = ManSpawn.AvoidSceneryVehiclesCrates,
			m_CircleRadius = 0f,
			m_CenterPosWorld = default(WorldPosition),
			m_CircleIndex = 0,
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.OnCamera,
			m_CheckSafeArea = false,
			m_RejectFunc = null
		};
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		if (m_FetchInvaders && Singleton.Manager<ManPresetFilter>.inst.PopulationTable != null)
		{
			FetchInvaders();
			m_FetchInvaders = false;
		}
		if (m_InvadersRequireValidating)
		{
			UpdateValidPopulation();
		}
		if (Singleton.Manager<ManGameMode>.inst.IsCurrent<ModeMain>() && Singleton.playerTank.IsNotNull())
		{
			UpdateSendingInvader();
			UpdateState();
		}
	}
}
