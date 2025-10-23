#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Snapshots;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ModeMisc : Mode<ModeMisc>, IWorldTreadmill
{
	[Serializable]
	public class ModeSpec
	{
		public string name;

		public ManGameMode.GameType m_MyType;

		public bool inMenu = true;

		public bool m_AllowsPhotoCam;

		public bool inDemo;

		public BiomeMapStackSetAsset m_BiomeChoices;

		[Tooltip("Ignored if 'User Biome Choices' is non-null")]
		public BiomeMapStackAsset m_BiomeMaps;

		public string seed;

		public TankPreset playerPreset;

		public PositionWithFacing playerSpawn = PositionWithFacing.identity;

		public PositionWithFacing cameraSpawn = PositionWithFacing.identity;

		public PositionWithFacing m_LastCheckpointSpawnDestination = PositionWithFacing.identity;

		public PositionWithFacing m_LastCheckpointCameraDestination = PositionWithFacing.identity;

		public float m_SpawnAreaClearRadius = 15f;

		public float m_SeaLevel = -50f;

		public bool m_EnableExploration = true;

		public float m_SpawnAreaPreExploredRadius = 100f;

		public UnityEvent m_ModeEnterFunction;

		public UnityEvent m_ModeUpdateFunction;

		[Tooltip("Will force 'Enable Time Progression' off and fix time to 'Time of Day'")]
		public bool m_UseCustomSkydome;

		[Tooltip("Allows time to progress during gameplay, otherwise it will stay at fixed")]
		public bool m_EnableTimeProgression;

		[Tooltip("Fixed or starting time")]
		public int m_TimeOfDay = 11;

		public bool m_UpdateEncounters;

		public bool m_VendorsEnabled;

		public bool m_SupportsPopulationEnemies;

		public string[] m_PopulationTypesToEnable;

		public float m_SceneryRegrowTimeOverride = -1f;

		public float m_GrabDistanceOverride = -1f;

		public HUDElements m_HudElements;

		public bool m_BuildBeamOnStart;

		public bool m_AutoRespawnPlayer;

		public float m_AutoRespawnDelay = 3f;

		public bool m_AutoRespawnPlayerToLastCheckpoint;

		public SceneryClearArea[] m_SceneryClearAreas;

		public SceneryTerrainArea[] m_SceneryTerrainAreas;

		public SpawnList nonSaveDataSpawnList = new SpawnList();

		public PrefabSpawner nonSaveDataPrefabSpawner;

		public SpawnList saveDataSpawnList = new SpawnList();

		public CustomModeBehaviourAsset m_CustomModeBehaviour;
	}

	[Serializable]
	public struct HUDElements
	{
		public bool m_ShowSnapshot;

		public bool m_ShowResetPosition;

		public bool m_ShowAnchor;

		public bool m_ShowSpeedo;

		public bool m_ShowAltimeter;

		public bool m_ShowMoney;

		public bool m_ShowTechLoader;

		public bool m_ShowTechManager;

		public bool m_ShowFullPalette;

		public bool m_ShowControlSchema;

		public bool m_ShowSkinsPaletteButton;

		public bool m_ShowWorldMap;
	}

	[Serializable]
	public struct SceneryClearArea
	{
		public string editorGUIName;

		public Vector3 position;

		public float radius;

		[EnumFlag]
		public ManSpawn.SceneryRemovalFlags removalFlags;
	}

	[Serializable]
	public struct SceneryTerrainArea
	{
		public TerrainSetPiece m_SetPiece;

		public Vector3 m_Position;

		public int m_Rotation;
	}

	[FormerlySerializedAs("modes")]
	public ModeSpec[] m_ModeSettings;

	[SerializeField]
	private LocalisedString m_ExitConfirmString;

	[SerializeField]
	private int m_ForceDirty;

	private TechData m_PlayerData;

	private int m_RespawnControlSchemeID;

	private Coroutine m_RespawnRoutine;

	private InitSettings m_CachedInitSettings;

	private List<Transform> m_SpawnedNonSavedPrefabs = new List<Transform>();

	private PrefabSpawner m_SpawnedPrefabSpawner;

	private ManOnScreenMessages.OnScreenMessage msgPlayerKilledRespawn = new ManOnScreenMessages.OnScreenMessage(new string[1] { "Oops! Try again..." }, ManOnScreenMessages.MessagePriority.Medium);

	public ModeSpec CurrentMode { get; private set; }

	public override bool AllowsPhotoMode
	{
		get
		{
			if (CurrentMode != null)
			{
				return CurrentMode.m_AllowsPhotoCam;
			}
			return false;
		}
	}

	public override float GetAutoExpireDelay(ObjectTypes type)
	{
		float num = 0f;
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return -1f;
		}
		if (CurrentMode != null && (CurrentMode.m_MyType == ManGameMode.GameType.RaD || CurrentMode.m_MyType == ManGameMode.GameType.Creative))
		{
			switch (type)
			{
			case ObjectTypes.Block:
				return Globals.inst.autoExpireTimeoutBlocksRandD;
			case ObjectTypes.Chunk:
				return Globals.inst.autoExpireTimeoutChunksRandD;
			case ObjectTypes.Crate:
				return Globals.inst.autoExpireTimeoutCratesRandD;
			default:
				d.Assert(condition: false, "GetAutoExpireDelay() - no expire delay defined for type " + type);
				return 0f;
			}
		}
		return base.GetAutoExpireDelay(type);
	}

	private TechData GetPlayerData(InitSettings initSettings)
	{
		TechData result = ((CurrentMode.playerPreset != null) ? CurrentMode.playerPreset.GetTechDataFormatted() : null);
		object value = null;
		if (initSettings.TryGetValue("LoadPlayerPreset", out value))
		{
			result = value as TechData;
		}
		return result;
	}

	public override bool ModePreInit(InitSettings initSettings)
	{
		CurrentMode = null;
		m_CachedInitSettings = initSettings;
		object value = null;
		if (initSettings.TryGetValue("ModeName", out value))
		{
			string b = value as string;
			ModeSpec[] modeSettings = m_ModeSettings;
			foreach (ModeSpec modeSpec in modeSettings)
			{
				if (modeSpec.name.EqualsNoCase(b))
				{
					CurrentMode = modeSpec;
					break;
				}
			}
			if (CurrentMode == null)
			{
				d.LogError(string.Concat("ModeMisc - Failed to find mode setting for mode name '", value, "'"));
			}
		}
		else
		{
			d.LogError("Failed to initialise ModeMisc: 'ModeName' not set in mode init settings!");
		}
		bool flag = CurrentMode != null && (CurrentMode.m_MyType == ManGameMode.GameType.Creative || CurrentMode.m_MyType == ManGameMode.GameType.RaD || CurrentMode.m_MyType == ManGameMode.GameType.Misc);
		base.TechManagerShowsOnlyPlayerTeam = !flag;
		return CurrentMode != null;
	}

	private void SpawnNonSavedPrefabs()
	{
		m_SpawnedNonSavedPrefabs.Clear();
		CurrentMode.nonSaveDataSpawnList.SpawnAll(m_SpawnedNonSavedPrefabs);
		d.Assert(CurrentMode.nonSaveDataSpawnList.SpawnedUntrackedPrefabCount == CurrentMode.nonSaveDataSpawnList.SpawnListCount, "ModeMisc - Spawn list error when spawning Non-Saved Objects Spawn List. Some (" + (CurrentMode.nonSaveDataSpawnList.SpawnListCount - CurrentMode.nonSaveDataSpawnList.SpawnedUntrackedPrefabCount) + ") of the objects were added to save data!");
		if (CurrentMode.nonSaveDataPrefabSpawner != null)
		{
			m_SpawnedPrefabSpawner = CurrentMode.nonSaveDataPrefabSpawner.Spawn(Singleton.Manager<ManWorld>.inst.GameWorldToScene);
			m_SpawnedNonSavedPrefabs.Add(m_SpawnedPrefabSpawner.transform);
		}
		List<WorldSpaceObject> list = new List<WorldSpaceObject>();
		for (int i = 0; i < m_SpawnedNonSavedPrefabs.Count; i++)
		{
			m_SpawnedNonSavedPrefabs[i].GetComponentsInChildren(list);
			for (int j = 0; j < list.Count; j++)
			{
				list[j].SetEnabled(enabled: false);
			}
		}
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		if (CurrentMode.m_MyType != ManGameMode.GameType.Creative)
		{
			Singleton.Manager<ManWorld>.inst.SeedString = CurrentMode.seed;
		}
		Singleton.Manager<ManWorldTreadmill>.inst.AddListener(this);
		SpawnNonSavedPrefabs();
		Vector3 cameraPos = (IsLoadedFromSaveGame() ? Singleton.Manager<ManSaveGame>.inst.CurrentState.m_CameraPos.GetBackwardsCompatiblePosition() : (CurrentMode.cameraSpawn.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene));
		Quaternion cameraRot = (IsLoadedFromSaveGame() ? Quaternion.LookRotation(Singleton.Manager<ManSaveGame>.inst.CurrentState.m_CameraPos.m_Forward) : CurrentMode.cameraSpawn.orientation);
		BiomeMap biomeMap = ((!(CurrentMode.m_BiomeChoices != null)) ? CurrentMode.m_BiomeMaps.MapStack.SelectCompatibleBiomeMap() : CurrentMode.m_BiomeChoices.SelectCompatibleBiomeMap(Singleton.Manager<ManWorld>.inst.BiomeChoice));
		EnterDefaultCameraMode();
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(biomeMap, cameraPos, cameraRot);
		Singleton.Manager<ManWorld>.inst.VendorSpawner.Enabled = true;
		SceneryTerrainArea[] sceneryTerrainAreas = CurrentMode.m_SceneryTerrainAreas;
		for (int i = 0; i < sceneryTerrainAreas.Length; i++)
		{
			SceneryTerrainArea sceneryTerrainArea = sceneryTerrainAreas[i];
			Vector3 scenePos = sceneryTerrainArea.m_Position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			Singleton.Manager<ManWorld>.inst.AddTerrainSetPiece(sceneryTerrainArea.m_SetPiece, WorldPosition.FromScenePosition(in scenePos), sceneryTerrainArea.m_Rotation);
		}
	}

	protected override bool IsModeSetupEarlyExit()
	{
		bool result = false;
		if (GetGameType() == ManGameMode.GameType.Misc && CurrentMode.saveDataSpawnList.SpawnListCount == 0 && CurrentMode.nonSaveDataSpawnList.SpawnListCount == 0 && CurrentMode.nonSaveDataPrefabSpawner == null && !Singleton.Manager<ManHUD>.inst.IsSettingUp)
		{
			result = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(Singleton.Manager<ManWorld>.inst.FocalPoint.TileCoord)?.IsLoaded ?? false;
		}
		return result;
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		Singleton.Manager<ManTechs>.inst.PlayerTankControlSchemeChangedEvent.Subscribe(RecordLatestControlSchemeID);
		bool num = !IsLoadedFromSaveGame();
		if (CurrentMode.m_UseCustomSkydome)
		{
			Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: false);
			Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
			Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(CurrentMode.m_TimeOfDay, 0, 0);
		}
		else
		{
			Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
			Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(CurrentMode.m_EnableTimeProgression);
			Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
			if (!CurrentMode.m_EnableTimeProgression)
			{
				Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
			}
		}
		if (num)
		{
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(CurrentMode.m_TimeOfDay, 0, 0);
		}
		m_PlayerData = GetPlayerData(initSettings);
		CurrentMode.m_LastCheckpointSpawnDestination.position = CurrentMode.playerSpawn.position;
		CurrentMode.m_LastCheckpointSpawnDestination.forward = CurrentMode.playerSpawn.forward;
		CurrentMode.m_LastCheckpointCameraDestination.position = CurrentMode.cameraSpawn.position;
		CurrentMode.m_LastCheckpointCameraDestination.forward = CurrentMode.cameraSpawn.forward;
		ResetState();
		if (TryLoadSetting<int>(initSettings, "BuildSizeLimit", out var outValue))
		{
			Singleton.Manager<ManSpawn>.inst.BlockLimit = outValue;
		}
		if (num)
		{
			for (int i = 0; i < CurrentMode.m_SceneryClearAreas.Length; i++)
			{
				Vector3 vector = CurrentMode.m_SceneryClearAreas[i].position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
				float radius = CurrentMode.m_SceneryClearAreas[i].radius;
				if (Singleton.Manager<ManWorld>.inst.CheckAllTilesAtPositionHaveReachedLoadStep(vector, radius))
				{
					vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(vector);
					ManSpawn.RemoveAllSceneryAroundPosition(vector, radius, CurrentMode.m_SceneryClearAreas[i].removalFlags);
				}
				else
				{
					d.LogErrorFormat("ModeMisc - Could not clear scenery around position {0} radius {1} because some of the tiles were not loaded!", vector, radius);
				}
			}
			CurrentMode.saveDataSpawnList.SpawnAll();
			base.StartPositionScene = CurrentMode.playerSpawn.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene;
			base.SpawnAreaClearRadius = CurrentMode.m_SpawnAreaClearRadius;
			if (CurrentMode.m_SpawnAreaPreExploredRadius > 0f)
			{
				Singleton.Manager<ManMap>.inst.ExploreArea(base.StartPositionScene, CurrentMode.m_SpawnAreaPreExploredRadius);
			}
			if (m_PlayerData != null)
			{
				ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
				{
					techData = m_PlayerData,
					blockIDs = null,
					teamID = 0,
					position = CurrentMode.playerSpawn.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene,
					rotation = CurrentMode.playerSpawn.orientation,
					grounded = true
				};
				Tank tank = Singleton.Manager<ManSpawn>.inst.SpawnTank(param, addToObjectManager: true);
				Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
			}
		}
		if (CurrentMode.m_HudElements.m_ShowSnapshot)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
		}
		if (CurrentMode.m_HudElements.m_ShowResetPosition)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ReturnToTeleporter);
		}
		if (CurrentMode.m_HudElements.m_ShowAnchor)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.AnchorTech);
		}
		if (CurrentMode.m_HudElements.m_ShowSpeedo)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Speedo);
		}
		if (CurrentMode.m_HudElements.m_ShowAltimeter)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Altimeter);
		}
		if (CurrentMode.m_HudElements.m_ShowMoney)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MoneyCounter);
		}
		if (CurrentMode.m_HudElements.m_ShowTechLoader)
		{
			UnityAction<bool> context = SetTechLoaderVisibility;
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoaderButton, context);
			ITechLoader obj = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader;
			obj.SetupScreenHandlers(LoadTechPreset);
			obj.SetupPlaceTechScreenHandler(OnPlaceTech);
		}
		if (CurrentMode.m_HudElements.m_ShowTechManager)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechManagerButton);
		}
		if (CurrentMode.m_HudElements.m_ShowSkinsPaletteButton)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
		}
		if (CurrentMode.m_HudElements.m_ShowFullPalette)
		{
			Singleton.Manager<ManPlayer>.inst.SetPlayerInventoryToUnrestricted();
			Singleton.Manager<ManPlayer>.inst.EnablePalette(enable: true);
		}
		if (CurrentMode.m_HudElements.m_ShowControlSchema)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ControlSchema);
		}
		if (CurrentMode.m_HudElements.m_ShowWorldMap)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.WorldMapButton);
		}
		if (CurrentMode.m_VendorsEnabled)
		{
			Singleton.Manager<ManWorld>.inst.Vendors.SetAllActive(active: true);
			Singleton.Manager<ManWorld>.inst.Vendors.SetVisibleOnRadar(visible: true);
		}
		if (CurrentMode.m_GrabDistanceOverride != -1f)
		{
			Singleton.Manager<ManPointer>.inst.SetPickupRange(CurrentMode.m_GrabDistanceOverride);
		}
		if (TryLoadSetting<int>(initSettings, "EnemyDifficulty", out var outValue2))
		{
			d.AssertFormat(CurrentMode.m_SupportsPopulationEnemies, "ModeMisc - InitSetting 'EnableEnemies' encountered, but mode spec {0} does not have support for population enemies enabled!", CurrentMode.name);
			Singleton.Manager<ManPop>.inst.SetCreativePopulationDifficulty((ManPop.CreativeModePopulationDifficulty)outValue2);
		}
		if (TryLoadSetting<bool>(initSettings, "PlayerIndestructibility", out var outValue3))
		{
			Singleton.Manager<ManPlayer>.inst.PlayerIndestructible = outValue3;
		}
		if (CurrentMode.m_ModeEnterFunction != null)
		{
			CurrentMode.m_ModeEnterFunction.Invoke();
		}
		if (CurrentMode.m_CustomModeBehaviour != null)
		{
			CurrentMode.m_CustomModeBehaviour.EnterPreMode();
		}
		if (CurrentMode.m_AutoRespawnPlayer)
		{
			PositionWithFacing playerSpawn = (CurrentMode.m_AutoRespawnPlayerToLastCheckpoint ? CurrentMode.m_LastCheckpointSpawnDestination : CurrentMode.playerSpawn);
			PositionWithFacing cameraSpawn = (CurrentMode.m_AutoRespawnPlayerToLastCheckpoint ? CurrentMode.m_LastCheckpointCameraDestination : CurrentMode.cameraSpawn);
			m_RespawnRoutine = StartCoroutine(LoopCheckRespawnPlayer(playerSpawn, cameraSpawn, CurrentMode.m_AutoRespawnDelay));
		}
		if (CurrentMode.m_EnableExploration)
		{
			Singleton.Manager<ManMap>.inst.EnableExploreAroundPlayer();
		}
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		FunctionStatus result = FunctionStatus.Done;
		if (!IsLoadedFromSaveGame() && CurrentMode.m_BuildBeamOnStart && Singleton.playerTank != null)
		{
			Singleton.Manager<ManSFX>.inst.SuppressUISFX();
			Singleton.playerTank.beam.EnableBeam(enable: true);
			if (!Singleton.playerTank.beam.IsActive)
			{
				result = FunctionStatus.Running;
			}
		}
		return result;
	}

	protected override void EnterModeUpdateImpl()
	{
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		if (CurrentMode.m_ModeUpdateFunction != null)
		{
			CurrentMode.m_ModeUpdateFunction.Invoke();
		}
		if (CurrentMode.m_CustomModeBehaviour != null)
		{
			CurrentMode.m_CustomModeBehaviour.UpdateMode();
		}
		if (CurrentMode != null && (CurrentMode.m_MyType == ManGameMode.GameType.RaD || CurrentMode.m_MyType == ManGameMode.GameType.Creative || CurrentMode.m_MyType == ManGameMode.GameType.Misc))
		{
			UpdateMissionScoreboardButtonPress(CurrentMode.m_HudElements.m_ShowTechManager);
		}
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		if (CurrentMode != null && CurrentMode.m_CustomModeBehaviour != null)
		{
			CurrentMode.m_CustomModeBehaviour.ExitMode();
		}
		ResetState();
		Singleton.Manager<ManPlayer>.inst.EnablePalette(enable: false);
		ITechLoader obj = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader;
		obj.RemoveScreenHandlers(LoadTechPreset);
		obj.RemovePlaceTechScreenHandler(OnPlaceTech);
		Singleton.Manager<ManPlayer>.inst.Reset();
		m_CachedInitSettings = null;
		if (CurrentMode != null)
		{
			CurrentMode.nonSaveDataSpawnList.RecycleAllPrefabs();
		}
		if (m_SpawnedPrefabSpawner != null)
		{
			m_SpawnedPrefabSpawner.Recycle();
			m_SpawnedPrefabSpawner = null;
		}
		if (CurrentMode != null)
		{
			CurrentMode.saveDataSpawnList.RecycleAllPrefabs();
		}
		Singleton.Manager<ManTechs>.inst.PlayerTankControlSchemeChangedEvent.Unsubscribe(RecordLatestControlSchemeID);
		Singleton.Manager<ManWorldTreadmill>.inst.RemoveListener(this);
	}

	public override ManGameMode.GameType GetGameType()
	{
		if (CurrentMode != null)
		{
			return CurrentMode.m_MyType;
		}
		return ManGameMode.GameType.Attract;
	}

	public override string GetGameMode()
	{
		return "ModeMisc";
	}

	public override string GetGameSubmode()
	{
		if (CurrentMode == null)
		{
			return "";
		}
		return CurrentMode.name;
	}

	public override ManHUD.HUDType GetDefaultHUDType()
	{
		return ManHUD.HUDType.MainGame;
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		BiomeMapStack biomeMapStack = null;
		biomeMapStack = ((!(CurrentMode.m_BiomeChoices != null)) ? CurrentMode.m_BiomeMaps.MapStack : CurrentMode.m_BiomeChoices.GetStack(Singleton.Manager<ManWorld>.inst.BiomeChoice));
		return biomeMapStack.LatestMap.WorldGenVersionData;
	}

	public override bool CanPlayerSwapTech()
	{
		if (CurrentMode != null)
		{
			return CurrentMode.m_HudElements.m_ShowTechLoader;
		}
		return false;
	}

	public override bool CanPlayerPlaceTech()
	{
		if (CurrentMode != null)
		{
			return CurrentMode.m_HudElements.m_ShowTechLoader;
		}
		return false;
	}

	public override InventoryMetaData GetReferenceInventory()
	{
		return new InventoryMetaData(null);
	}

	public override float GetSeaLevel()
	{
		if (CurrentMode == null)
		{
			return base.GetSeaLevel();
		}
		return CurrentMode.m_SeaLevel;
	}

	public override bool CanResetPosition()
	{
		if (Singleton.playerTank != null && CurrentMode != null)
		{
			return CurrentMode.m_HudElements.m_ShowResetPosition;
		}
		return false;
	}

	public override float GetSceneryRegrowTime()
	{
		if (CurrentMode.m_SceneryRegrowTimeOverride == -1f)
		{
			return Globals.inst.m_DefaultSceneryRegrowTime;
		}
		return CurrentMode.m_SceneryRegrowTimeOverride;
	}

	public override bool DisplaysSeed()
	{
		if (CurrentMode == null)
		{
			return false;
		}
		return CurrentMode.m_MyType == ManGameMode.GameType.Creative;
	}

	public override bool UsesFloatingOrigin()
	{
		return true;
	}

	public override void ResetPlayerPosition()
	{
		if ((bool)Singleton.playerTank)
		{
			Singleton.playerTank.visible.Teleport(CurrentMode.m_LastCheckpointSpawnDestination.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene, CurrentMode.m_LastCheckpointSpawnDestination.orientation);
			Singleton.Manager<CameraManager>.inst.ResetCamera(CurrentMode.m_LastCheckpointCameraDestination.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene, CurrentMode.m_LastCheckpointCameraDestination.orientation);
		}
	}

	public override bool HasSaveGameSupport()
	{
		bool result = false;
		if (CurrentMode != null)
		{
			result = CurrentMode.m_MyType == ManGameMode.GameType.RaD || CurrentMode.m_MyType == ManGameMode.GameType.Creative;
		}
		return result;
	}

	public override bool CanSave()
	{
		return HasSaveGameSupport();
	}

	protected override void Save(ManSaveGame.State saveState)
	{
	}

	protected override void Load(ManSaveGame.State saveState)
	{
	}

	protected override void SetupModeLoadSaveListeners()
	{
		SubscribeToEvents(Singleton.Manager<ManVisible>.inst);
		SubscribeToEvents(Singleton.Manager<ManWorld>.inst);
		if (CurrentMode.m_SupportsPopulationEnemies)
		{
			SubscribeToEvents(Singleton.Manager<ManPresetFilter>.inst);
			SubscribeToEvents(Singleton.Manager<ManPop>.inst);
		}
		SubscribeToEvents(Singleton.Manager<ManNewFTUE>.inst);
		if (CurrentMode.m_UpdateEncounters)
		{
			SubscribeToEvents(Singleton.Manager<ManEncounter>.inst);
		}
		SubscribeToEvents(Singleton.Manager<ManTechs>.inst);
		SubscribeToEvents(Singleton.Manager<ManPurchases>.inst);
		SubscribeToEvents(Singleton.Manager<ManStats>.inst);
		SubscribeToEvents(Singleton.Manager<ManTimeOfDay>.inst);
		SubscribeToEvents(Singleton.Manager<ManPlayer>.inst);
		SubscribeToEvents(Singleton.Manager<ManChallenge>.inst);
		SubscribeToEvents(Singleton.Manager<ManTireTracks>.inst);
		SubscribeToEvents(Singleton.Manager<ManBlockLimiter>.inst);
		SubscribeToEvents(Singleton.Manager<ManLooseChunkLimiter>.inst);
		SubscribeToEvents(Singleton.Manager<ManMods>.inst);
		SubscribeToEvents(Singleton.Manager<ManLooseBlocks>.inst);
		SubscribeToEvents(Singleton.Manager<ManMap>.inst);
		SubscribeToEvents(Singleton.Manager<ManSpawn>.inst);
	}

	protected override void CleanupModeLoadSaveListeners()
	{
		UnsubscribeFromEvents(Singleton.Manager<ManVisible>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManWorld>.inst);
		if (CurrentMode.m_SupportsPopulationEnemies)
		{
			UnsubscribeFromEvents(Singleton.Manager<ManPresetFilter>.inst);
			UnsubscribeFromEvents(Singleton.Manager<ManPop>.inst);
		}
		UnsubscribeFromEvents(Singleton.Manager<ManNewFTUE>.inst);
		if (CurrentMode.m_UpdateEncounters)
		{
			UnsubscribeFromEvents(Singleton.Manager<ManEncounter>.inst);
		}
		UnsubscribeFromEvents(Singleton.Manager<ManTechs>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPurchases>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManStats>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTimeOfDay>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPlayer>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManChallenge>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTireTracks>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManBlockLimiter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManLooseChunkLimiter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManMods>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManLooseBlocks>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManMap>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManSpawn>.inst);
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string notification = ((!CanSave()) ? Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 26) : m_ExitConfirmString.Value);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				ManSaveGame.ShouldStore = false;
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
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
		exitScreen.Set(notification, accept, decline);
	}

	private void ResetState()
	{
		Singleton.Manager<ManSpawn>.inst.BlockLimit = BlockManager.DefaultBlockLimit;
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = false;
		if (m_RespawnRoutine != null)
		{
			StopCoroutine(m_RespawnRoutine);
		}
	}

	public void OnMoveWorldOrigin(IntVector3 amountToMove)
	{
		if (m_SpawnedNonSavedPrefabs != null)
		{
			for (int i = 0; i < m_SpawnedNonSavedPrefabs.Count; i++)
			{
				m_SpawnedNonSavedPrefabs[i].position += amountToMove;
			}
		}
	}

	private void RecordLatestControlSchemeID(ControlScheme scheme)
	{
		m_RespawnControlSchemeID = scheme?.ID ?? 0;
	}

	public void RnDTechChamberEnter()
	{
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.RaDTestChamber);
	}

	public void CreativeModeEnter()
	{
		if (!Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_PlayerNotifications.Contains(ManProfile.kCreativeModeExplanation))
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Notifications, 13);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			Action accept = delegate
			{
				Singleton.Manager<ManProfile>.inst.GetCurrentUser().m_PlayerNotifications.Add(ManProfile.kCreativeModeExplanation);
				Singleton.Manager<ManProfile>.inst.Save();
				Singleton.Manager<ManUI>.inst.PopScreen(showPrev: false);
			};
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			uIScreenNotifications.Set(localisedString, accept, localisedString2);
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications);
		}
	}

	public IEnumerator LoopCheckRespawnPlayer(PositionWithFacing playerSpawn, PositionWithFacing cameraSpawn, float respawnDelay)
	{
		float playerRespawnTimer = respawnDelay;
		while (true)
		{
			bool flag = Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(Singleton.cameraTrans.position);
			if (m_PlayerData != null && !Singleton.playerTank && !Singleton.Manager<ManPointer>.inst.IsDraggingController && flag)
			{
				if (playerRespawnTimer > 0f)
				{
					playerRespawnTimer -= Time.deltaTime;
					if (!Singleton.Manager<ManOnScreenMessages>.inst.IsCurrentMessage(Mode<ModeMisc>.inst.msgPlayerKilledRespawn))
					{
						Singleton.Manager<ManOnScreenMessages>.inst.REMOVE_ME_I_DO_NOTHING_AddMessage(Mode<ModeMisc>.inst.msgPlayerKilledRespawn, boolVal: true);
					}
				}
				else
				{
					playerRespawnTimer = respawnDelay;
					if (Singleton.Manager<ManGameMode>.inst.AtLeastOnePlayerControllableTankExists())
					{
						float num = float.MaxValue;
						Tank tank = null;
						foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
						{
							if (item.ControllableByLocalPlayer)
							{
								float sqrMagnitude = (item.boundsCentreWorld - Singleton.cameraTrans.position).sqrMagnitude;
								if (sqrMagnitude < num)
								{
									tank = item;
									num = sqrMagnitude;
								}
							}
						}
						if ((bool)tank)
						{
							Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
						}
					}
					else
					{
						Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem();
						Singleton.Manager<CameraManager>.inst.ResetCamera(cameraSpawn.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene, cameraSpawn.orientation);
						while (Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating)
						{
							yield return null;
						}
						ManSpawn.TankSpawnParams param = new ManSpawn.TankSpawnParams
						{
							techData = m_PlayerData,
							blockIDs = null,
							teamID = 0,
							position = playerSpawn.position + Singleton.Manager<ManWorld>.inst.GameWorldToScene,
							rotation = playerSpawn.orientation,
							grounded = true
						};
						TrackedVisible tankTV = Singleton.Manager<ManSpawn>.inst.SpawnTankRef(param, addToObjectManager: true);
						if (tankTV == null)
						{
							d.LogError("Failed to spawn player tech in respawn! Falling back to spawning the default tech for this mode!");
							m_PlayerData = GetPlayerData(m_CachedInitSettings);
							playerRespawnTimer = -1f;
							continue;
						}
						while (!tankTV.visible)
						{
							yield return null;
						}
						tankTV.visible.tank.control.SetActiveSchemeFromID(m_RespawnControlSchemeID);
						Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tankTV.visible.tank);
						tankTV.visible.tank.beam.EnableBeam(enable: true, force: true);
						if (CurrentMode.m_HudElements.m_ShowSnapshot)
						{
							Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
						}
						if (CurrentMode.m_HudElements.m_ShowResetPosition)
						{
							Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ResetPosition);
						}
					}
				}
			}
			yield return null;
		}
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

	private void OnPlaceTech(TechData techData, Vector3 position, Quaternion rotation)
	{
		Singleton.Manager<ManPurchases>.inst.LoadTechFromInventoryAtPosition(techData, position, rotation);
	}

	private void LoadTechPreset(Snapshot capture)
	{
		if ((bool)Singleton.playerTank)
		{
			Singleton.Manager<ManPurchases>.inst.HotswapTechs(Singleton.playerTank, capture.techData);
		}
		m_PlayerData = capture.techData;
	}
}
