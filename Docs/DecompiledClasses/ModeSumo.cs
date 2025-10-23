#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Snapshots;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ModeSumo : Mode<ModeSumo>
{
	public class Contestants
	{
		public struct ContestantData
		{
			public SnapshotTwitter snapshot;

			public int index;
		}

		public List<ContestantData> m_ContestantData = new List<ContestantData>();

		public int NumContestants => m_ContestantData.Count;

		public void AddContestant(Snapshot snapshot, int contestantIndex = -1)
		{
			SnapshotTwitter snapshotTwitter = snapshot as SnapshotTwitter;
			if (snapshotTwitter == null)
			{
				if (snapshot is SnapshotDisk snapshotDisk)
				{
					snapshotTwitter = SnapshotTwitter.ConvertFromDisk(snapshotDisk);
				}
				else
				{
					d.LogError("UIScreenSumoRanked.StartRankedLadder - could not parse the provided snapshot: " + snapshot);
				}
			}
			if (snapshotTwitter != null)
			{
				ContestantData item = new ContestantData
				{
					snapshot = snapshotTwitter,
					index = ((contestantIndex == -1) ? m_ContestantData.Count : contestantIndex)
				};
				m_ContestantData.Add(item);
			}
		}

		public ContestantData GetContestant(int index)
		{
			return m_ContestantData[index];
		}

		public void Clear()
		{
			m_ContestantData.Clear();
		}
	}

	public class Participant
	{
		private Tank.WeakReference m_TechReference = new Tank.WeakReference();

		public Snapshot snapshot;

		public int playerID;

		public bool isAlive;

		public Tank tech
		{
			get
			{
				return m_TechReference.Get();
			}
			set
			{
				m_TechReference.Set(value);
			}
		}
	}

	private enum GameState
	{
		Null,
		Design,
		Versus,
		SplashScreen,
		PreReadyUp,
		ReadyUp,
		Countdown,
		Playing,
		MatchEnd
	}

	public ManGameMode.GameType m_MyType;

	public BiomeMap biomeMap;

	public string seed;

	public TankPreset defaultTank;

	public PositionWithFacing cameraSpawn = PositionWithFacing.identity;

	public PositionWithFacing platformSpawn = PositionWithFacing.identity;

	[FormerlySerializedAs("platformPrefab")]
	[SerializeField]
	private Transform m_PlatformPrefab;

	[SerializeField]
	private Transform m_BuildPlatformKillVolumesPrefab;

	[SerializeField]
	[FormerlySerializedAs("spawnPointCentreRadius")]
	private float m_SpawnPointCentreRadius;

	public float confettiInterval = 0.1f;

	[SerializeField]
	private float m_ResultScreenDelay = 1.5f;

	public Font messageFont;

	public float messageFontHeightScreen = 0.067f;

	public Color messageColor;

	public Color messageColorBest = Color.green;

	public Color messageColorShadow;

	public GUIManager.LabelWrapper announceLabel;

	public GUIManager.LabelWrapper victoryLabel;

	public GUIManager.LabelWrapper victorNameLabel;

	public GUIManager.LabelWrapper rematchButton;

	public GUIManager.LabelWrapper backToCleanButton;

	public GUIManager.LabelWrapper controllsScreenTitle;

	public GUIManager.LabelWrapper controllsScreenBottom;

	public GUIManager.LabelAnchor clockAnchor;

	public GUIManager.LabelAnchor clockAnchorVictoryScreen;

	public Font clockFont;

	public float clockFontHeightScreen = 0.067f;

	public GUIManager.ImageAnchor controlsOverlayAnchor;

	public GUIManager.LabelWrapper readyUpLabel;

	public GUIManager.LabelWrapper readyUpSubLabel;

	public Color readyColour;

	public Color readyInputReactColour;

	public float readyMessageWorldYOffset = 5f;

	public float allReadyCountdownPause = 1f;

	public float readyUpSubLabelScreenYOffset = 0.01f;

	public float m_NoActionCountoutPause = 7f;

	public float m_CountOutTime = 10f;

	public HUDSumo m_SumoHUDPrefab;

	[SerializeField]
	private int m_DefaultBuildSizeLimit = 16;

	private Vector3 m_KillVolumeCentre;

	private Contestants m_CurrentContestants;

	private List<Participant> m_FightingTech;

	private ParticleSystem[] m_ConfettiParticles;

	private int m_CurrentConfettiIndex;

	private float m_LastConfettiLaunchTime;

	private bool[] m_ReadyStatus = new bool[4];

	private bool[] m_AnyInput = new bool[4];

	private string m_SumoRankedTag = "#TTSumoRank";

	private int m_StalemateCount;

	private GameState m_GameState;

	private bool m_IsRankedMatch;

	private float m_Timer;

	private float m_GameTime;

	private Vector3 m_CentrePoint;

	private HUDSumo m_SumoHUD;

	private IInventory<BlockTypes> m_Inventory;

	private IInventory<BlockTypes> m_FullInventory;

	private bool m_ControlTechManually;

	private float m_ArenaRadius = 30f;

	private float m_ArenaHeight = 60f;

	private InitSettings m_LastSettings = new InitSettings();

	private GUIManager.LabelWriter clockWriter = new GUIManager.LabelWriter();

	private bool m_PlayerRespawning;

	private TechData m_PlayerRespawnData;

	private DamageVolume[] m_DamageVolumes;

	private List<Visible.WeakReference> m_BlocksToRecycle = new List<Visible.WeakReference>();

	private TechData m_LastSavedTechData;

	private bool m_ShownResultsScreen;

	private OnGUICallback m_GuiCallback;

	private static string[] countdownLabels = new string[4] { "SUMO!", "1", "2", "3" };

	private static string[] countoutLabels = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

	public bool Playing => m_GameState == GameState.Playing;

	public bool Designing => m_GameState == GameState.Design;

	public int CurrentRank { get; set; }

	public int PreviousRank { get; set; }

	public int EnemyRank { get; set; }

	public int RankedGameCount { get; set; }

	public string RankedTweetLevel => m_SumoRankedTag + CurrentRank;

	public string RankedHashtag => m_SumoRankedTag;

	public InitSettings LastInitSettings => m_LastSettings;

	public void ClearRankedSettings()
	{
		CurrentRank = 1;
		PreviousRank = 0;
		EnemyRank = 0;
		RankedGameCount = 0;
	}

	private Tank SpawnTank(TechData techToSpawn, Vector3 spawnPos, Quaternion rotation, int techTeam)
	{
		ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
		{
			techData = techToSpawn,
			teamID = techTeam,
			position = spawnPos,
			rotation = rotation,
			grounded = false
		};
		if (m_Inventory != null)
		{
			param.inventory = m_Inventory;
		}
		Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: false);
		if (tank != null && tank.blockman.blockCount != techToSpawn.m_BlockSpecs.Count)
		{
			tank.visible.RemoveFromGame();
			tank = null;
		}
		if (tank != null)
		{
			tank.PositionBaseCentred(spawnPos);
			tank.trans.rotation = rotation * Quaternion.Inverse(tank.rootBlockTrans.localRotation);
		}
		else
		{
			d.Assert(condition: false, "ModeSumo.TrySpawnTechFromInventory - There was a problem spawning tech '" + param.techData.Name + "'. Either could not be spawned, or not all blocks could be taken from the inventory!");
		}
		return tank;
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
		Singleton.Manager<ManWorld>.inst.SeedString = seed;
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(biomeMap, cameraSpawn.position, cameraSpawn.orientation);
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		m_LastSettings.Clear();
		foreach (KeyValuePair<string, object> initSetting in initSettings)
		{
			m_LastSettings.Add(initSetting.Key, initSetting.Value);
		}
		int blockLimit = LoadSetting(initSettings, "BuildSizeLimit", m_DefaultBuildSizeLimit);
		Singleton.Manager<ManSpawn>.inst.BlockLimit = blockLimit;
		m_CurrentContestants = LoadSetting<Contestants>(initSettings, "contestants", null);
		d.Assert(m_CurrentContestants == null || m_CurrentContestants.NumContestants > 1, "Entering Mode sumo with contestant data, but not enough contestants to fight were present! (Found " + ((m_CurrentContestants != null) ? m_CurrentContestants.NumContestants.ToString() : "null") + ")");
		bool flag = m_CurrentContestants == null;
		m_ControlTechManually = LoadSetting(initSettings, "ManualControl", defaultValue: false);
		m_IsRankedMatch = HasSetting(initSettings, "Ranked");
		if (m_CurrentContestants != null && m_ReadyStatus.Length < m_CurrentContestants.NumContestants)
		{
			Array.Resize(ref m_ReadyStatus, m_CurrentContestants.NumContestants);
			Array.Resize(ref m_AnyInput, m_CurrentContestants.NumContestants);
		}
		Array.Clear(m_ReadyStatus, 0, m_ReadyStatus.Length);
		SpawnPlatform(flag);
		if (TryLoadSetting<InventoryAsset>(initSettings, "Inventory", out var outValue) && outValue != null)
		{
			m_FullInventory = new SingleplayerInventory();
			outValue.BuildInventory(m_FullInventory);
		}
		if (flag)
		{
			Singleton.Manager<CameraManager>.inst.Switch<TankCamera>();
			if (outValue != null)
			{
				m_Inventory = new SingleplayerInventory();
				outValue.BuildInventory(m_Inventory);
			}
			Singleton.Manager<ManSFX>.inst.SuppressUISFX();
			Singleton.Manager<ManPurchases>.inst.ShowPalette(show: true);
			Singleton.Manager<ManPurchases>.inst.SetInventory(m_Inventory);
			Singleton.Manager<ManPointer>.inst.OnBlockPaintedEvent.Subscribe(OnBlockPainted);
			TechData techData = new TechData();
			techData.CopyWithoutSaveData(defaultTank.GetTechDataFormatted());
			SpawnDesignerTank(techData).name = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 75);
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(expand: true, UIShopBlockSelect.ExpandReason.Button);
			Singleton.playerTank.beam.EnableBeam(enable: true, force: true);
			if (TryLoadSetting<float>(initSettings, "PointerGrabDistance", out var outValue2))
			{
				Singleton.Manager<ManPointer>.inst.SetPickupRange(outValue2);
			}
			Singleton.Manager<FTUE>.inst.Execute(TutorialMessages());
			Singleton.Manager<ManSnapshots>.inst.PresetSavedEvent.Subscribe(OnSnapshot);
			EnterState(GameState.Design);
		}
		else
		{
			Singleton.Manager<CameraManager>.inst.Switch<FramingCamera>();
			PrepareFight();
			Singleton.Manager<ManTechs>.inst.TankDamagedEvent.Subscribe(OnAnyTankDamaged);
			GameState gameState = (m_ControlTechManually ? GameState.SplashScreen : (m_IsRankedMatch ? GameState.Versus : GameState.Countdown));
			EnterState(gameState);
		}
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		return FunctionStatus.Done;
	}

	protected override void EnterModeUpdateImpl()
	{
		m_GuiCallback = OnGUICallback.AddGUICallback(base.gameObject);
		m_GuiCallback.OnGUIEvent.Subscribe(DrawLegacyGUI);
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		switch (m_GameState)
		{
		case GameState.Design:
			if (Singleton.playerTank == null)
			{
				if (!m_PlayerRespawning && !Singleton.Manager<ManUndo>.inst.UndoInProgress && !Singleton.Manager<ManPointer>.inst.IsDraggingController)
				{
					m_PlayerRespawning = true;
					m_Timer = 2f;
				}
				else if (m_PlayerRespawning && m_Timer < 0f)
				{
					Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem();
					Singleton.Manager<CameraManager>.inst.ResetCamera(cameraSpawn.position, cameraSpawn.orientation);
					RecyclePlayerBlocks();
					ManSpawn.RemoveAllBlocksAndTechAroundPosition(m_CentrePoint, 70f, 0);
					SpawnDesignerTank(m_PlayerRespawnData);
				}
				m_Timer -= Time.deltaTime;
				UpdateMissionScoreboardButtonPress();
			}
			break;
		case GameState.Versus:
			m_Timer -= Time.deltaTime;
			if (m_Timer <= 0f)
			{
				Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
				EnterState(GameState.Countdown);
			}
			break;
		case GameState.SplashScreen:
		{
			bool flag4 = false;
			for (int m = 0; m < m_FightingTech.Count; m++)
			{
				if (m_FightingTech[m].tech.control.TestBoostControl())
				{
					flag4 = true;
				}
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || flag4)
			{
				EnterState(GameState.PreReadyUp);
			}
			break;
		}
		case GameState.PreReadyUp:
		{
			bool flag5 = true;
			for (int n = 0; n < m_FightingTech.Count; n++)
			{
				if (m_FightingTech[n].tech.control.TestBoostControl())
				{
					flag5 = false;
				}
			}
			if (flag5)
			{
				EnterState(GameState.ReadyUp);
			}
			break;
		}
		case GameState.ReadyUp:
		{
			Array.Clear(m_AnyInput, 0, m_ReadyStatus.Length);
			for (int k = 0; k < m_FightingTech.Count; k++)
			{
				if (m_FightingTech[k].tech.control.TestAnyControl())
				{
					m_AnyInput[k] = true;
				}
			}
			bool flag3 = true;
			if (m_Timer == 0f)
			{
				for (int l = 0; l < m_FightingTech.Count; l++)
				{
					if (m_FightingTech[l].tech.control.TestBoostControl())
					{
						m_ReadyStatus[l] = true;
					}
					if (!m_ReadyStatus[l])
					{
						flag3 = false;
					}
				}
				if (flag3)
				{
					m_Timer = allReadyCountdownPause;
				}
			}
			else
			{
				m_Timer -= Time.deltaTime;
				if (m_Timer <= 0f)
				{
					EnterState(GameState.Countdown);
				}
			}
			break;
		}
		case GameState.Countdown:
			m_Timer -= Time.deltaTime;
			if ((int)m_Timer <= 0)
			{
				for (int j = 0; j < m_FightingTech.Count; j++)
				{
					m_FightingTech[j].tech.beam.EnableBeam(enable: false, force: true);
				}
				EnterState(GameState.Playing);
			}
			break;
		case GameState.Playing:
		{
			m_GameTime -= Time.deltaTime;
			Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.Generic);
			int num2 = 0;
			for (int num3 = 0; num3 < m_FightingTech.Count; num3++)
			{
				Participant participant3 = m_FightingTech[num3];
				Tank tech = participant3.tech;
				if (participant3.isAlive)
				{
					if (tech == null || !tech.visible.isActive)
					{
						participant3.isAlive = false;
					}
					else if (tech.boundsCentreWorld.y - m_KillVolumeCentre.y > m_ArenaHeight || (m_KillVolumeCentre - tech.boundsCentreWorld).SetY(0f).magnitude > m_ArenaRadius)
					{
						tech.blockman.Disintegrate();
						participant3.isAlive = false;
					}
					else
					{
						num2++;
					}
				}
			}
			if (num2 == 1)
			{
				m_LastConfettiLaunchTime = Time.time;
				ParticleSystem[] confettiParticles = m_ConfettiParticles;
				for (int num4 = 0; num4 < confettiParticles.Length; num4++)
				{
					confettiParticles[num4].Play();
				}
				EnterState(GameState.MatchEnd);
			}
			else if (AllTanksImmobile() || m_GameTime <= 0f)
			{
				m_Timer += Time.deltaTime;
				if (m_Timer >= m_CountOutTime + m_NoActionCountoutPause)
				{
					EnterState(GameState.MatchEnd);
				}
			}
			else
			{
				m_Timer = 0f;
			}
			break;
		}
		case GameState.MatchEnd:
		{
			m_Timer += Time.deltaTime;
			int num = 0;
			Participant participant = null;
			for (int i = 0; i < m_FightingTech.Count; i++)
			{
				if (m_FightingTech[i].isAlive)
				{
					num++;
					if (participant == null)
					{
						participant = m_FightingTech[i];
					}
				}
			}
			bool flag = num > 1;
			Participant participant2 = (flag ? null : participant);
			if (participant2 != null)
			{
				while (Time.time > m_LastConfettiLaunchTime + confettiInterval)
				{
					m_LastConfettiLaunchTime += confettiInterval;
					m_ConfettiParticles[m_CurrentConfettiIndex--].Play();
					if (m_CurrentConfettiIndex == -1)
					{
						m_CurrentConfettiIndex += m_ConfettiParticles.Length;
					}
				}
			}
			if (m_ShownResultsScreen || !(m_Timer >= m_ResultScreenDelay))
			{
				break;
			}
			m_ShownResultsScreen = true;
			if (m_IsRankedMatch)
			{
				bool flag2 = true;
				if (flag)
				{
					if (m_StalemateCount < 3)
					{
						flag2 = false;
						if (m_SumoHUD != null)
						{
							m_SumoHUD.SetStalemate(m_StalemateCount);
						}
						m_StalemateCount++;
						PrepareFight();
						EnterState(GameState.Countdown);
					}
					else
					{
						participant2 = m_FightingTech[1];
					}
				}
				if (flag2)
				{
					bool won = false;
					if (participant2 != null && participant2.playerID == 0)
					{
						won = true;
						PreviousRank = CurrentRank;
						CurrentRank = EnemyRank + 1;
						RankedGameCount++;
					}
					if (m_SumoHUD != null)
					{
						Singleton.Manager<ManUI>.inst.ParentUIScreen(m_SumoHUD.transform, Singleton.Manager<ManUI>.inst.m_InactiveUI);
						m_SumoHUD.gameObject.SetActive(value: false);
					}
					UIScreenSumoResults uIScreenSumoResults = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SumoRankedResults) as UIScreenSumoResults;
					uIScreenSumoResults.SetScreenInfo(m_CurrentContestants, won);
					Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenSumoResults);
				}
			}
			else
			{
				UIScreenSumoVersusResult uIScreenSumoVersusResult = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SumoVersusResult) as UIScreenSumoVersusResult;
				uIScreenSumoVersusResult.Setup(m_GameTime, participant2, m_FightingTech);
				Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenSumoVersusResult);
			}
			break;
		}
		}
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: true, ManPointer.DragDisableReason.SumoMode);
		(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader).RemoveScreenHandlers(LoadTechPreset);
		m_GameState = GameState.Null;
		ClearFightingTech();
		m_FightingTech = null;
		m_ConfettiParticles = null;
		m_Inventory = null;
		m_PlayerRespawnData = null;
		m_BlocksToRecycle.Clear();
		m_LastSavedTechData = null;
		if (m_DamageVolumes != null)
		{
			for (int i = 0; i < m_DamageVolumes.Length; i++)
			{
				m_DamageVolumes[i].OnDamageVolumeKillEvent.Unsubscribe(OnDamageVolumeKill);
			}
		}
		m_DamageVolumes = null;
		Singleton.Manager<ManSnapshots>.inst.PresetSavedEvent.Unsubscribe(OnSnapshot);
		if (!Designing)
		{
			Singleton.Manager<ManTechs>.inst.TankDamagedEvent.Unsubscribe(OnAnyTankDamaged);
		}
		Singleton.Manager<ManPurchases>.inst.ShowPalette(show: false);
		Singleton.Manager<ManPurchases>.inst.SetInventory(null);
		Singleton.Manager<ManPointer>.inst.OnBlockPaintedEvent.Unsubscribe(OnBlockPainted);
		if (m_SumoHUD != null)
		{
			m_SumoHUD.transform.SetParent(null, worldPositionStays: false);
			m_SumoHUD.Recycle();
			m_SumoHUD = null;
		}
		m_GuiCallback.OnGUIEvent.Unsubscribe(DrawLegacyGUI);
		OnGUICallback.RemoveGUICallback(m_GuiCallback);
		m_GuiCallback = null;
	}

	public override ManGameMode.GameType GetGameType()
	{
		return m_MyType;
	}

	public override string GetGameMode()
	{
		return "Sumo";
	}

	public override string GetGameSubmode()
	{
		return "";
	}

	public override ManHUD.HUDType GetDefaultHUDType()
	{
		return ManHUD.HUDType.MainGame;
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		return biomeMap.WorldGenVersionData;
	}

	private void ClearFightingTech()
	{
		if (m_FightingTech == null)
		{
			return;
		}
		for (int i = 0; i < m_FightingTech.Count; i++)
		{
			Tank tech = m_FightingTech[i].tech;
			if (tech != null)
			{
				tech.trans.Recycle();
			}
		}
		m_FightingTech.Clear();
	}

	private void PrepareFight()
	{
		if (m_FightingTech == null)
		{
			m_FightingTech = new List<Participant>(m_CurrentContestants.NumContestants);
		}
		ClearFightingTech();
		m_Timer = 3.99f;
		m_GameTime = 0f;
		Singleton.Manager<CameraManager>.inst.ResetCamera(cameraSpawn.position, cameraSpawn.orientation);
		float num = 360f / (float)m_CurrentContestants.NumContestants;
		float num2 = m_CurrentContestants.NumContestants switch
		{
			3 => -30f, 
			4 => -45f, 
			_ => 0f, 
		};
		bool flag = Input.GetJoystickNames().Length < m_CurrentContestants.NumContestants;
		int num3 = 0;
		for (int i = 0; i < m_CurrentContestants.NumContestants; i++)
		{
			Contestants.ContestantData contestantData = m_CurrentContestants.m_ContestantData[i];
			TechData techData = contestantData.snapshot.techData;
			float y = num2 + (float)i * num;
			Vector3 vector = Quaternion.Euler(new Vector3(0f, y, 0f)) * (-Vector3.right * m_SpawnPointCentreRadius);
			Quaternion rotation = Quaternion.LookRotation(-vector);
			Tank tank = SpawnTank(techData, m_CentrePoint + vector, rotation, -1);
			if (tank != null)
			{
				if (flag)
				{
					tank.control.SetControllerIndex(-1);
					flag = false;
				}
				else
				{
					tank.control.SetControllerIndex(num3++);
				}
				if (m_ControlTechManually)
				{
					tank.control.SetControllerHandlesInput(handlesInput: true);
				}
				BlockManager.BlockIterator<ModuleVision>.Enumerator enumerator = tank.blockman.IterateBlockComponents<ModuleVision>().GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.SetRange(m_SpawnPointCentreRadius * 3f);
				}
				ModuleDriveBot moduleDriveBot = tank.blockman.IterateBlockComponents<ModuleDriveBot>().FirstOrDefault();
				if (moduleDriveBot != null)
				{
					moduleDriveBot.SetState(ModuleDriveBot.StateType.AIState_SumoCharge);
				}
				tank.beam.EnableBeam(enable: true, force: true);
				Participant item = new Participant
				{
					tech = tank,
					snapshot = contestantData.snapshot,
					playerID = i,
					isAlive = true
				};
				m_FightingTech.Add(item);
			}
		}
		Singleton.Manager<FTUE>.inst.Execute(FightTutorialMessage());
	}

	private Tank SpawnDesignerTank(TechData techData)
	{
		Tank tank = SpawnTank(techData, m_CentrePoint, Quaternion.identity, 0);
		Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
		m_PlayerRespawnData = techData;
		m_LastSavedTechData = techData;
		m_PlayerRespawning = false;
		if ((bool)Singleton.playerTank)
		{
			if (m_Inventory != null)
			{
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					enumerator.Current.visible.RecycledEvent.Subscribe(OnInventoryBlockRecycled);
				}
			}
		}
		else
		{
			string text = ((techData != null) ? ManSaveGame.SaveObjectToRawJson(techData.m_BlockSpecs) : string.Empty);
			d.LogError(string.Concat("ModeSumo.SpawnDesignerTank - Failed to spawn tank from tech data: ", techData, " \n Block Spec: ", text));
		}
		return tank;
	}

	public override void ResetPlayerPosition()
	{
		if ((bool)Singleton.playerTank && Designing)
		{
			Quaternion rotation = Quaternion.LookRotation(Singleton.playerTank.trans.forward.SetY(0f));
			Singleton.playerTank.visible.Teleport(m_CentrePoint, rotation);
			Singleton.Manager<CameraManager>.inst.ResetCamera(cameraSpawn.position, cameraSpawn.orientation);
		}
	}

	public override bool CanPlayerChangeTech(Tank targetTech)
	{
		return false;
	}

	public override bool CanPlayerPlaceTech()
	{
		return false;
	}

	public override InventoryMetaData GetReferenceInventory()
	{
		if (m_FullInventory == null)
		{
			d.LogError("Inventory has not been initialised yet, are you trying to access it before 'EnterPreModeImpl'? Returning nil inventory for now");
			return new InventoryMetaData(null, locked: true);
		}
		return new InventoryMetaData(m_FullInventory);
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		bool unsavedChangesToDesign = false;
		if (Designing && Singleton.playerTank != null)
		{
			TechData techData = new TechData();
			techData.SaveTech(Singleton.playerTank, saveRuntimeState: true);
			unsavedChangesToDesign = !TankPreset.CheckBlockSpecListsMatch(techData.m_BlockSpecs.ToArray(), m_LastSavedTechData.m_BlockSpecs.ToArray());
		}
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 26);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				if (unsavedChangesToDesign)
				{
					Singleton.Manager<ManUI>.inst.PopScreen();
					ShowExitDoubleConfirm();
				}
				else
				{
					Singleton.Manager<ManUI>.inst.ExitAllScreens();
					Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				}
			},
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
			m_Callback = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
			},
			m_RewiredAction = 22
		};
		exitScreen.Set(localisedString, accept, decline);
	}

	private void ShowExitDoubleConfirm()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 5);
		Action decline = delegate
		{
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		};
		Action accept = delegate
		{
			Singleton.Manager<ManUI>.inst.PopScreen();
		};
		string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 6);
		string localisedString3 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 5);
		uIScreenNotifications.Set(localisedString, accept, decline, localisedString3, localisedString2);
		Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications, ManUI.PauseType.Pause);
	}

	private void OnSnapshot(TechData techData, Texture2D image, bool isPlayerTech)
	{
		d.Assert(isPlayerTech, "Assumption that saving the tech here can only ever be the player tech, is false.");
		m_LastSavedTechData = techData;
		Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
		Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(new ManOnScreenMessages.OnScreenMessage(new string[2]
		{
			Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 7),
			Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 8)
		}, ManOnScreenMessages.MessagePriority.Medium));
	}

	private IEnumerator TutorialMessages()
	{
		bool sizeWarningShowed = false;
		bool snapshotWhenReady = false;
		float startTime = Time.time;
		float beamNotActiveTime = 0f;
		while (Designing)
		{
			if ((bool)Singleton.playerTank)
			{
				if (!sizeWarningShowed && (Singleton.playerTank.blockBounds.extents.x >= 4f || Singleton.playerTank.blockBounds.extents.y >= 4f || Singleton.playerTank.blockBounds.extents.z >= 4f))
				{
					Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(new ManOnScreenMessages.OnScreenMessage(new string[1] { "In Sumo Showdown, the size limit for warriors is 16x16x16 block units" }, ManOnScreenMessages.MessagePriority.Medium));
					sizeWarningShowed = true;
				}
				if (!snapshotWhenReady)
				{
					if (startTime > 90f || beamNotActiveTime > 15f)
					{
						Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(new ManOnScreenMessages.OnScreenMessage(new string[1] { "When your sumo warrior is ready, tap the snapshot button to continue." }, ManOnScreenMessages.MessagePriority.Medium));
						snapshotWhenReady = true;
					}
					beamNotActiveTime = (Singleton.playerTank.beam.IsActive ? 0f : (beamNotActiveTime + Time.deltaTime));
				}
			}
			yield return null;
		}
	}

	private IEnumerator FightTutorialMessage()
	{
		yield return null;
		if (Designing || m_ControlTechManually)
		{
			yield break;
		}
		int presses = 0;
		while (true)
		{
			if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
			{
				presses++;
			}
			if (presses > 5)
			{
				string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.SumoMode, 24);
				Singleton.Manager<ManOnScreenMessages>.inst.AddMessage(new ManOnScreenMessages.OnScreenMessage(new string[1] { localisedString }, ManOnScreenMessages.MessagePriority.Medium));
				presses = 0;
			}
			yield return null;
		}
	}

	private void DrawInfoPage()
	{
		GUI.Box(new Rect(0f, 0f, Screen.width, Screen.height), "");
		controlsOverlayAnchor.DrawTextureFit();
		controllsScreenTitle.DoLabel();
		controllsScreenBottom.SetWrap(wrap: true);
		controllsScreenBottom.DoLabel();
	}

	private bool AllTanksImmobile()
	{
		bool result = true;
		for (int i = 0; i < m_FightingTech.Count; i++)
		{
			if (m_FightingTech[i].isAlive)
			{
				ModuleDriveBot moduleDriveBot = m_FightingTech[i].tech.blockman.IterateBlockComponents<ModuleDriveBot>().FirstOrDefault();
				if (moduleDriveBot != null && moduleDriveBot.TankIsMoving)
				{
					result = false;
					break;
				}
			}
		}
		return result;
	}

	private void OnAnyTankDamaged(Tank tank, ManDamage.DamageInfo info)
	{
		if (m_GameState == GameState.Playing)
		{
			m_Timer = 0f;
		}
	}

	private void DoFadedScaledLabel(GUIManager.LabelWrapper wrapper, string text, float fade, float scale)
	{
		float textHeightScreen = wrapper.textHeightScreen;
		wrapper.textHeightScreen *= scale;
		wrapper.SetFade(fade);
		wrapper.DoLabel(text);
		wrapper.textHeightScreen = textHeightScreen;
		wrapper.SetFade(1f);
	}

	private void DoCountDownGUI()
	{
		int num = (int)Mathf.Max(m_Timer, 0f);
		if (num == 0)
		{
			DoFadedScaledLabel(announceLabel, countdownLabels[num], m_Timer, 3f - m_Timer * 2f);
		}
		else if (num > 0)
		{
			announceLabel.DoLabel(countdownLabels[num]);
		}
	}

	private void DoCountOutGUI()
	{
		if (m_Timer > m_NoActionCountoutPause)
		{
			int num = Mathf.Max((int)(m_CountOutTime - (m_Timer - m_NoActionCountoutPause)), 0);
			announceLabel.DoLabel(countoutLabels[num]);
		}
	}

	private void SpawnPlatform(bool spawnKillVolumes)
	{
		Transform transform = m_PlatformPrefab.Spawn(platformSpawn.position, platformSpawn.orientation);
		Transform transform2 = null;
		if (spawnKillVolumes)
		{
			transform2 = m_BuildPlatformKillVolumesPrefab.Spawn(platformSpawn.position, platformSpawn.orientation);
			m_DamageVolumes = transform2.GetComponentsInChildren<DamageVolume>();
			if (m_DamageVolumes != null)
			{
				for (int i = 0; i < m_DamageVolumes.Length; i++)
				{
					m_DamageVolumes[i].OnDamageVolumeKillEvent.Subscribe(OnDamageVolumeKill);
				}
			}
		}
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(transform.position);
		if (worldTile != null)
		{
			transform.parent = worldTile.StaticParent;
			if (transform2 != null)
			{
				transform2.parent = worldTile.StaticParent;
			}
		}
		else
		{
			d.LogError("failed to find containing tile for " + transform);
		}
		m_ConfettiParticles = transform.GetComponentsInChildren<ParticleSystem>();
		Collider collider = transform.GetComponentsInChildren<Collider>().SingleOrDefault((Collider c) => !c.isTrigger);
		m_KillVolumeCentre = transform.position + Vector3.up * collider.bounds.extents.y;
		m_ArenaRadius = collider.bounds.extents.x;
		m_ArenaHeight = m_ArenaRadius * 2f;
		m_CentrePoint = collider.bounds.center + Vector3.up * collider.bounds.extents.y;
	}

	private void SetTechLoaderVisibility(bool enabled)
	{
		if (enabled)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoader);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoader);
		}
	}

	private void LoadTechPreset(Snapshot capture)
	{
		TechData techData = capture.techData;
		if ((bool)Singleton.playerTank)
		{
			Tank playerTank = Singleton.playerTank;
			Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(null);
			playerTank.visible.RemoveFromGame();
		}
		ManSpawn.RemoveAllBlocksAndTechAroundPosition(m_CentrePoint, 70f, 0);
		SpawnDesignerTank(techData);
		d.Assert(Singleton.playerTank != null, "ModeSumo.LoadTechPreset - Failed to spawn and set tank '" + techData.Name + "' as player tank!");
	}

	private void RecyclePlayerBlocks()
	{
		for (int i = 0; i < m_BlocksToRecycle.Count; i++)
		{
			Visible visible = m_BlocksToRecycle[i].Get();
			if ((bool)visible)
			{
				visible.trans.Recycle();
			}
		}
		m_BlocksToRecycle.Clear();
	}

	private void OnBlockPainted(TankBlock block)
	{
		if (m_Inventory != null)
		{
			block.visible.RecycledEvent.Subscribe(OnInventoryBlockRecycled);
		}
	}

	private void OnInventoryBlockRecycled(Visible blockVisible)
	{
		if (m_Inventory != null)
		{
			m_Inventory.HostAddItem(blockVisible.block.BlockType);
		}
		blockVisible.RecycledEvent.Unsubscribe(OnInventoryBlockRecycled);
	}

	private void OnDamageVolumeKill(Visible visible)
	{
		if ((bool)visible.tank && visible.tank == Singleton.playerTank)
		{
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				Visible.WeakReference weakReference = new Visible.WeakReference();
				weakReference.Set(current.visible);
				m_BlocksToRecycle.Add(weakReference);
			}
			m_PlayerRespawnData.SaveTech(Singleton.playerTank, saveRuntimeState: true);
		}
		else if (visible.block != null)
		{
			Visible.WeakReference weakReference2 = new Visible.WeakReference();
			weakReference2.Set(visible);
			m_BlocksToRecycle.Add(weakReference2);
		}
	}

	private void Awake()
	{
		ClearRankedSettings();
	}

	private void EnterState(GameState gameState)
	{
		switch (gameState)
		{
		case GameState.Design:
		{
			m_Timer = 0f;
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ResetPosition);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
			UnityAction<bool> context = SetTechLoaderVisibility;
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoaderButton, context);
			(Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader).SetupScreenHandlers(LoadTechPreset);
			break;
		}
		case GameState.Versus:
		{
			m_Timer = 3f;
			m_StalemateCount = 0;
			UIScreenSumoVS uIScreenSumoVS = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.SumoRankedVS) as UIScreenSumoVS;
			uIScreenSumoVS.SetScreenInfo(m_CurrentContestants);
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenSumoVS);
			break;
		}
		case GameState.Countdown:
			if (m_IsRankedMatch)
			{
				if (m_SumoHUD == null)
				{
					m_SumoHUD = m_SumoHUDPrefab.Spawn();
				}
				if (m_SumoHUD != null)
				{
					m_SumoHUD.SetPlayerData(m_CurrentContestants);
					Singleton.Manager<ManUI>.inst.ParentUIScreen(m_SumoHUD.transform, Singleton.Manager<ManUI>.inst.m_ActiveUI);
					Singleton.Manager<ManUI>.inst.ClearFade(3f);
					m_SumoHUD.gameObject.SetActive(value: true);
				}
			}
			m_Timer = 3.99f;
			break;
		case GameState.Playing:
			Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: false, ManPointer.DragDisableReason.SumoMode);
			m_GameTime = 180f;
			m_Timer = 0f;
			break;
		case GameState.MatchEnd:
			m_Timer = 0f;
			m_ShownResultsScreen = false;
			m_GameTime = Mathf.Max(m_GameTime, 0f);
			break;
		}
		m_GameState = gameState;
	}

	private void DoClockGUI(GUIManager.LabelAnchor anchor, string prefix)
	{
		float height = (float)Screen.height * clockFontHeightScreen;
		clockWriter.SetFont(messageFont, height, TextAnchor.MiddleLeft);
		clockWriter.SetColors(messageColor, messageColorShadow);
		string text = prefix + Singleton.Manager<Localisation>.inst.GetTimeDisplayString(m_GameTime, forceHourDisplay: false, displayMilliseconds: true);
		anchor.DoLabel(clockWriter, text);
	}

	private void DrawLegacyGUI()
	{
		if (Singleton.Manager<DebugUtil>.inst.hideTheGUI || !Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			return;
		}
		switch (m_GameState)
		{
		case GameState.SplashScreen:
			DrawInfoPage();
			break;
		case GameState.Countdown:
			DoCountDownGUI();
			break;
		case GameState.ReadyUp:
		{
			for (int i = 0; i < m_FightingTech.Count; i++)
			{
				Vector3 position = m_FightingTech[i].tech.boundsCentreWorld + Vector3.up * readyMessageWorldYOffset;
				Vector3 vector = Singleton.Manager<ManUI>.inst.WorldToScreenPoint(position);
				Color textColorMain = readyUpLabel.textColorMain;
				if (m_ReadyStatus[i])
				{
					readyUpLabel.textColorMain = readyColour;
				}
				if (m_AnyInput[i])
				{
					readyUpLabel.textColorMain = readyInputReactColour;
				}
				readyUpLabel.anchor.screenPos = new Vector2(vector.x / (float)Screen.width, 1f - vector.y / (float)Screen.height);
				readyUpLabel.DoLabel(m_ReadyStatus[i] ? "Ready" : "Ready?");
				readyUpLabel.textColorMain = textColorMain;
			}
			readyUpSubLabel.DoLabel();
			break;
		}
		case GameState.Playing:
			DoCountOutGUI();
			DoClockGUI(clockAnchor, "");
			break;
		case GameState.PreReadyUp:
		case GameState.MatchEnd:
			break;
		}
	}
}
