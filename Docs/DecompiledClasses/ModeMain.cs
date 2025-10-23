#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Snapshots;
using UnityEngine;

public class ModeMain : Mode<ModeMain>
{
	private enum TutorialProgress
	{
		Start,
		BuiltFirstTank,
		DestroyedFirstEnemy,
		FoundTheBase,
		FabricatedMiniTracktor,
		Sent5Sapphires,
		Done
	}

	private class SaveData
	{
		public bool m_CanRespawn;

		public bool m_NeedsPlayerRespawn;

		public WorldPosition m_LastAlivePlayerPosition;

		public TechData m_LastUsedPlayerTechData;

		public ObjectSpawner.SaveData m_PlayerRespawnerData;

		public bool m_PlayerRespawnWithCustomRejectFunc;

		public bool m_StartedResourceDiscoveries;

		public List<ItemTypeInfo> m_CurrentKnownItems;
	}

	public ManGameMode.GameType m_MyType;

	[SerializeField]
	public BiomeMapStackAsset m_BiomeMaps;

	public float triggerAttractTime = 300f;

	public PositionWithFacing playerSpawn = PositionWithFacing.identity;

	public PositionWithFacing cameraSpawn = PositionWithFacing.identity;

	[SerializeField]
	private float m_SpawnAreaClearRadius = 15f;

	[Tooltip("Family of FTUE like tech to offer to restore upon death")]
	[SerializeField]
	private TankPreset[] m_RespawnPlayerTankPresets = new TankPreset[0];

	public float m_RespawnDelay = 2f;

	public int numPickupChunkWarnings = 3;

	public List<ItemTypeInfo> m_KnownItems;

	public List<ItemTypeInfo> m_KnownRecipes = new List<ItemTypeInfo>();

	public List<ItemTypeInfo> m_DemoKnownItems;

	public BlockTypes[] m_DemoAvailableBlocks;

	private const int initialNumberOfStoredTiles = 20;

	private const int tankPresetPoolSize = 50;

	public float m_RestartTutorialButtonStayTime = 10f;

	public float m_ResourceDiscoverCheckDist = 30f;

	public float m_ShowMashingFireTutorialAfterPresses = 5f;

	public float m_PressingFireWithinSec = 2f;

	public Vector3 m_GameStartPosOffset = new Vector3(0f, 120f, 50f);

	public float m_GameStartBiomeSearchRadius = 100f;

	public float m_GameStartBiomeSearchSamples = 10f;

	[SerializeField]
	[Tooltip("Change this from -1 if overriding")]
	private float m_OverrideGrabDistance = -1f;

	[SerializeField]
	[Tooltip("Camera XZ distance from respawn position")]
	private float m_RespawnCameralookDistXZ = 10f;

	[Tooltip("Camera Height from respawn position")]
	[SerializeField]
	private float m_RespawnCameralookDistY = 10f;

	[Tooltip("Distance in which we'll search for friendly bases to respawn at")]
	[SerializeField]
	private float m_RespawnPlayerFriendlyBaseSearchDistance = 300f;

	[Tooltip("Distance Player Respawns from where they died if no nearby base was found")]
	[SerializeField]
	private float m_RespawnPlayerDistance = 100f;

	[Tooltip("Danger timeout - Must be same as ManMusic danger time to preserve music queue!")]
	[SerializeField]
	private float m_PlayerDangerTimeout = 3f;

	[SerializeField]
	private float m_PlayerDangerSaveTimeout = 30f;

	[SerializeField]
	private float m_PlayerTechEditSaveTimeout = 10f;

	[SerializeField]
	[Tooltip("Fraction of full block price to pay for blocks that can be recovered")]
	private float m_PlayerRespawnBlockRecoverCostFraction = 0.3f;

	[Tooltip("Fraction of the full blocks prive to pay for remaining blocks bought from the shop after we've tried recovery and inventory.")]
	[SerializeField]
	private float m_PlayerRespawnBlockShopCostFraction = 1f;

	[SerializeField]
	[Tooltip("On respawn as existing ally tech, if the tech is within this limit the camera will move to it smoothly. Outside this range the camera will teleport to the tech.")]
	private float m_PlayerRespawnMaxCameraInterpDistance = 50f;

	[SerializeField]
	private LocalisedString m_ExitConfirmString;

	[SerializeField]
	private LocalisedString m_ExitWithoutSaveString;

	[HideInInspector]
	public SpawnList spawnList = new SpawnList();

	private bool m_CanRespawn;

	private bool m_Respawning;

	private float m_RespawnTimer;

	private bool m_ShouldRetrySpawn;

	private float m_PlayerSpawnSearchRadiusMultiplier = 1f;

	private TechData m_TechDataToSpawnAs;

	private int tankAttachEventCount;

	private float m_RestartTutorialButtonTimer;

	private float m_ShowTimer;

	private float m_SpacebarMashingTimer;

	private int m_SpacebarAmountPressed;

	private bool m_SpaceMashingShown;

	private bool m_AttachingChunksShown;

	private bool m_PlayerInDanger;

	private float m_PlayerInDangerTime;

	private bool m_PlayerTechBackupIsDirty;

	private float m_PlayerTechLastEditTime;

	private bool m_IsTechAutoSaveAfterDanger;

	private bool m_IsTechInBuildBeam;

	private TechData m_PlayerTechBackup;

	private List<Visible.WeakReference> m_PlayerTechBlocks = new List<Visible.WeakReference>();

	private ObjectSpawner m_PlayerRespawner = new ObjectSpawner();

	public bool m_StartedResourceDiscoveries;

	public List<ItemTypeInfo> m_CurrentKnownItems;

	private static readonly Bitfield<ObjectTypes> k_SceneryAndChunks = new Bitfield<ObjectTypes>(new ObjectTypes[2]
	{
		ObjectTypes.Scenery,
		ObjectTypes.Chunk
	});

	private SaveData m_SaveData = new SaveData();

	public bool TutorialLockBeam { get; set; }

	public bool TutorialLockBeamMove { get; set; }

	public bool TutorialDisableBlockRotation { get; set; }

	public bool TutorialDisableBlockRemoval { get; set; }

	public bool ReduceBlockDragReleaseSpeed { get; set; }

	public bool SuppressTooltips { get; private set; }

	public bool AllowBlockDecay { get; set; }

	public bool HideEnemyMarkers { get; set; }

	public bool SkipTutorial { get; private set; }

	public bool DebugSkipTutorial { get; set; }

	private void HandlePlayerRespawn()
	{
		bool flag = m_RespawnTimer == 0f;
		if (!m_Respawning)
		{
			if (flag)
			{
				d.Log("[ModuleMain] player tech destroyed");
			}
			else if (m_RespawnTimer >= m_RespawnDelay)
			{
				Singleton.Manager<ManMusic>.inst.ResetDangerMusic();
				Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages(instant: true);
				UIScreenPlayerTechChoice screen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.RespawnTechChoice) as UIScreenPlayerTechChoice;
				if (m_PlayerTechBackup != null)
				{
					string saveToDiskName = m_PlayerTechBackup.Name;
					Singleton.Manager<ManScreenshot>.inst.RenderTechImage(m_PlayerTechBackup, screen.TechImageSize, encodeTechData: true, delegate(TechData techData, Texture2D techImage)
					{
						Singleton.Manager<ManSnapshots>.inst.SaveSnapshotRender(m_PlayerTechBackup, techImage, saveToDiskName, isPlayerTech: true);
						PlayerTechRecoveryData recoveryData = new PlayerTechRecoveryData(m_PlayerTechBackup, m_PlayerRespawnBlockShopCostFraction, m_PlayerTechBlocks, m_PlayerRespawnBlockRecoverCostFraction);
						screen.SetLastPlayerTech(recoveryData, techImage, OnRecoverTechChosen);
					});
				}
				else
				{
					d.LogError("Trying to respawn player but no backup tech available! What went wrong here?");
				}
				SetupRespawnOtherTechOption();
				Singleton.Manager<ManUI>.inst.GoToScreen(screen);
				m_Respawning = true;
			}
		}
		else if (m_ShouldRetrySpawn)
		{
			d.Log("Retrying player spawn code. This time, we are allowed to spawn on scenery.");
			RespawnPlayer(m_TechDataToSpawnAs, ManSpawn.kVisibleMaskTechAndCrates);
			m_ShouldRetrySpawn = false;
		}
		m_RespawnTimer += Time.deltaTime;
	}

	private void OnFactionTechChosen(TechData chosenTech)
	{
		RespawnPlayer(chosenTech);
	}

	private void OnExistingPlayerTechChosen(Tank playerTech)
	{
		Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(playerTech);
		float sqrMagnitude = (playerTech.boundsCentreWorld - Singleton.cameraTrans.position).sqrMagnitude;
		float num = m_PlayerRespawnMaxCameraInterpDistance * m_PlayerRespawnMaxCameraInterpDistance;
		if (sqrMagnitude > num)
		{
			Vector3 scenePosition = m_SaveData.m_LastAlivePlayerPosition.ScenePosition;
			Vector3 vector = Singleton.cameraTrans.position - scenePosition;
			Singleton.Manager<CameraManager>.inst.ResetCamera(playerTech.boundsCentreWorld + vector, Singleton.cameraTrans.rotation);
		}
		m_RespawnTimer = 0f;
		m_Respawning = false;
		m_PlayerSpawnSearchRadiusMultiplier = 1f;
	}

	private void OnRecoverTechChosen(PlayerTechRecoveryData recoveryData)
	{
		Singleton.Manager<ManPlayer>.inst.PayMoney(recoveryData.m_TotalRecoveryCost);
		RecoverBackupTechBlocks();
		if (!Singleton.Manager<ManPlayer>.inst.InventoryIsUnrestricted)
		{
			IInventory<BlockTypes> playerInventory = Singleton.Manager<ManPlayer>.inst.PlayerInventory;
			foreach (KeyValuePair<int, int> item in recoveryData.m_BlocksAvailableFromInventory)
			{
				BlockTypes key = (BlockTypes)item.Key;
				int value = item.Value;
				playerInventory.HostConsumeItem(-1, key, value);
			}
		}
		RespawnPlayer(recoveryData.m_RecoveryTechData);
	}

	private void RespawnPlayer(TechData techToSpawnAs)
	{
		RespawnPlayer(techToSpawnAs, ManSpawn.AvoidSceneryVehiclesCrates);
	}

	private void RespawnPlayer(TechData techToSpawnAs, Bitfield<ObjectTypes> avoidFlags)
	{
		m_TechDataToSpawnAs = techToSpawnAs;
		Vector3 scenePosition = m_SaveData.m_LastAlivePlayerPosition.ScenePosition;
		float num = m_RespawnPlayerFriendlyBaseSearchDistance * m_RespawnPlayerFriendlyBaseSearchDistance;
		float num2 = m_RespawnPlayerDistance * m_RespawnPlayerDistance;
		float num3 = 1000000f;
		float num4 = float.MaxValue;
		Tank tank = null;
		bool flag = false;
		foreach (Tank item in Singleton.Manager<ManTechs>.inst.IteratePlayerTechs())
		{
			float sqrMagnitude = (item.boundsCentreWorld - scenePosition).sqrMagnitude;
			if (sqrMagnitude >= num2 && sqrMagnitude < num3 && sqrMagnitude < num4)
			{
				tank = item;
				num4 = sqrMagnitude;
				if (sqrMagnitude < num)
				{
					flag = true;
				}
			}
		}
		float num5 = techToSpawnAs.Radius;
		bool flag2 = false;
		Vector3 scenePos = Vector3.zero;
		Vector3 facingDir = Vector3.zero;
		if (Singleton.Manager<ManEncounter>.inst.GetPlayerRespawnOverride(ref scenePos, ref facingDir))
		{
			flag2 = true;
		}
		else if (flag)
		{
			scenePos = tank.boundsCentreWorld;
			num5 = num5 + 3f + num5 * 0.15f;
		}
		else
		{
			Vector3 vector;
			if (tank != null)
			{
				vector = (tank.boundsCentreWorld - scenePosition).normalized;
			}
			else
			{
				float y = UnityEngine.Random.value * 360f;
				vector = Quaternion.Euler(0f, y, 0f) * Vector3.forward;
			}
			scenePos = scenePosition + vector * m_RespawnPlayerDistance;
		}
		scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(scenePos);
		Vector3 vector2 = (flag2 ? facingDir : (scenePosition - scenePos));
		vector2.y = 0f;
		Quaternion quaternion = Quaternion.LookRotation(vector2.normalized);
		ManSpawn.TechSpawnParams objectSpawnParams = new ManSpawn.TechSpawnParams
		{
			m_TechToSpawn = techToSpawnAs,
			m_Team = 0,
			m_Rotation = quaternion,
			m_Grounded = true,
			m_SpawnVisualType = ManSpawn.SpawnVisualType.Bomb
		};
		ManFreeSpace.FreeSpaceParams freeSpaceParams = new ManFreeSpace.FreeSpaceParams
		{
			m_ObjectsToAvoid = avoidFlags,
			m_CircleRadius = num5,
			m_CenterPosWorld = WorldPosition.FromScenePosition(in scenePos),
			m_CircleIndex = 0,
			m_CameraSpawnConditions = ManSpawn.CameraSpawnConditions.Anywhere,
			m_CheckSafeArea = false,
			m_SearchRadiusMultiplier = m_PlayerSpawnSearchRadiusMultiplier
		};
		if (!flag2)
		{
			freeSpaceParams.m_RejectFunc = SpawnAwayFromLastPlayerPosition;
		}
		bool autoRetry = true;
		m_PlayerRespawner.TrySpawn(objectSpawnParams, freeSpaceParams, null, "PlayerRespawn", autoRetry);
	}

	private void RecoverBackupTechBlocks()
	{
		for (int i = 0; i < m_PlayerTechBlocks.Count; i++)
		{
			Visible visible = m_PlayerTechBlocks[i].Get();
			if ((bool)visible)
			{
				visible.trans.Recycle();
			}
		}
		m_PlayerTechBlocks.Clear();
	}

	private bool SpawnAwayFromLastPlayerPosition(Vector3 pos, float radius, object context)
	{
		Vector3 scenePosition = m_SaveData.m_LastAlivePlayerPosition.ScenePosition;
		float sqrMagnitude = (pos - scenePosition).sqrMagnitude;
		float num = m_RespawnPlayerDistance * m_RespawnPlayerDistance;
		return sqrMagnitude < num;
	}

	private void PlayerRespawnPositionFound(Vector3 spawnPosition, string identifier)
	{
		Vector3 scenePosition = m_SaveData.m_LastAlivePlayerPosition.ScenePosition;
		Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(spawnPosition);
		Vector3 forward = vector - scenePosition;
		forward.y = 0f;
		Quaternion quaternion = Quaternion.LookRotation(forward);
		Vector3 vector2 = vector + quaternion * Vector3.forward * m_RespawnCameralookDistXZ + Vector3.up * m_RespawnCameralookDistY;
		Quaternion rotation = Quaternion.LookRotation(vector - vector2);
		Singleton.Manager<CameraManager>.inst.ResetCamera(vector2, rotation);
		if (Singleton.Manager<CameraManager>.inst.DOF != null && Singleton.Manager<CameraManager>.inst.DOF.enabled)
		{
			Singleton.Manager<CameraManager>.inst.SetDOFFocusDistance((spawnPosition - Singleton.cameraTrans.position).magnitude);
		}
		ManSpawn.SceneryRemovalFlags sceneryRemovalSettings = ManSpawn.SceneryRemovalFlags.RemoveInstant;
		ManSpawn.RemoveAllSceneryAroundPosition(vector, m_TechDataToSpawnAs.Radius, sceneryRemovalSettings);
	}

	private void PlayerRespawned(TrackedVisible tv, string identifier, PerVisibleParams encounterParams, string debugLog)
	{
		if (tv != null)
		{
			m_PlayerSpawnSearchRadiusMultiplier = 1f;
			Tank tank = ((tv.visible != null) ? tv.visible.tank : null);
			if (tank != null)
			{
				Singleton.Manager<ManTechs>.inst.RequestSetPlayerTank(tank);
				m_RespawnTimer = 0f;
				m_Respawning = false;
			}
			else
			{
				d.LogError("ModeMain.PlayerRespawned has spawned a player tracked visible which doesn't have a visible or tank attached");
			}
		}
		else
		{
			d.LogWarning("Player failed to find a spawn point. This shouldn't really be possible, but we'll try again");
			m_PlayerSpawnSearchRadiusMultiplier *= 2f;
			m_ShouldRetrySpawn = true;
		}
	}

	private Tank GetNearestMobilePlayerTech(Vector3 scenePos, Tank excludeTank = null)
	{
		Tank result = null;
		if (Singleton.Manager<ManGameMode>.inst.AtLeastOnePlayerControllableTankExists())
		{
			float num = float.MaxValue;
			foreach (Tank item in Singleton.Manager<ManTechs>.inst.IterateTechs())
			{
				if (item.ControllableByLocalPlayer && !item.IsAnchored && item != excludeTank)
				{
					float sqrMagnitude = (item.boundsCentreWorld - scenePos).sqrMagnitude;
					if (sqrMagnitude < num)
					{
						result = item;
						num = sqrMagnitude;
					}
				}
			}
		}
		return result;
	}

	private TechData GetStarterTechDataForGrade(out LocalisedString techName)
	{
		FactionSubTypes corporation = FactionSubTypes.GSO;
		int currentLevel = Singleton.Manager<ManLicenses>.inst.GetCurrentLevel(corporation);
		int num = Mathf.Min(currentLevel, m_RespawnPlayerTankPresets.Length - 1);
		d.Assert(num >= 0, "Invalid corporation license level for " + corporation.ToString() + " when attempting to find Starter Tech to respawn as!");
		TankPreset tankPreset = null;
		while (num >= 0)
		{
			tankPreset = m_RespawnPlayerTankPresets[num];
			if (tankPreset != null)
			{
				if (num != currentLevel)
				{
					d.LogWarning("Failed to find Starter Tech of grade " + (currentLevel + 1) + " for faction " + corporation.ToString() + " - had to fall back to grade " + (num + 1) + " before finding a valid tech!");
				}
				break;
			}
			num--;
		}
		techName = new LocalisedString
		{
			m_Bank = "InGameMessages",
			m_Id = "RespawnTechName_" + corporation.ToString() + "_" + (num + 1).ToString("D2")
		};
		return tankPreset.GetTechDataFormatted();
	}

	private void SetupRespawnOtherTechOption()
	{
		UIScreenPlayerTechChoice screen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.RespawnTechChoice) as UIScreenPlayerTechChoice;
		SetupRespawnOtherTechOption(screen, Singleton.playerTank);
	}

	private void SetupRespawnOtherTechOption(UIScreenPlayerTechChoice screen, Tank tankToExclude)
	{
		d.Assert(screen != null, "Screen reference is null, this happened at a time when user input is disabled and will break the game");
		if (screen == null)
		{
			return;
		}
		Tank nearestPlayerControllableTech = GetNearestMobilePlayerTech(m_SaveData.m_LastAlivePlayerPosition.ScenePosition, tankToExclude);
		if (nearestPlayerControllableTech != null)
		{
			Singleton.Manager<ManScreenshot>.inst.RenderTechImage(nearestPlayerControllableTech, screen.TechImageSize, encodeTechData: false, delegate(TechData techData, Texture2D techImage)
			{
				screen.SetExistingPlayerTech(nearestPlayerControllableTech, techImage, OnExistingPlayerTechChosen);
			});
			return;
		}
		LocalisedString techName;
		TechData gradeAppropriateStarterTech = GetStarterTechDataForGrade(out techName);
		Singleton.Manager<ManScreenshot>.inst.RenderTechImage(gradeAppropriateStarterTech, screen.TechImageSize, encodeTechData: false, delegate(TechData techData, Texture2D techImage)
		{
			screen.SetFactionTechOption(gradeAppropriateStarterTech, techImage, techName.Value, OnFactionTechChosen);
		});
	}

	private void ReturnToMenu()
	{
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
	}

	protected override void SetupModeLoadSaveListeners()
	{
		SubscribeToEvents(Singleton.Manager<ManVisible>.inst);
		SubscribeToEvents(Singleton.Manager<ManWorld>.inst);
		SubscribeToEvents(Singleton.Manager<ManLicenses>.inst);
		SubscribeToEvents(Singleton.Manager<ManInvasion>.inst);
		SubscribeToEvents(Singleton.Manager<ManPresetFilter>.inst);
		SubscribeToEvents(Singleton.Manager<ManPop>.inst);
		SubscribeToEvents(Singleton.Manager<ManQuestLog>.inst);
		SubscribeToEvents(Singleton.Manager<ManFTUE>.inst);
		SubscribeToEvents(Singleton.Manager<ManNewFTUE>.inst);
		SubscribeToEvents(Singleton.Manager<ManChallenge>.inst);
		SubscribeToEvents(Singleton.Manager<ManEncounter>.inst);
		SubscribeToEvents(Singleton.Manager<ManTechs>.inst);
		SubscribeToEvents(Singleton.Manager<ManPurchases>.inst);
		SubscribeToEvents(Singleton.Manager<ManStats>.inst);
		SubscribeToEvents(Singleton.Manager<ManTimeOfDay>.inst);
		SubscribeToEvents(Singleton.Manager<ManPlayer>.inst);
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
		UnsubscribeFromEvents(Singleton.Manager<ManLicenses>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManInvasion>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPresetFilter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPop>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManQuestLog>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManFTUE>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManNewFTUE>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManChallenge>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManEncounter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTechs>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPurchases>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManStats>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTimeOfDay>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPlayer>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTireTracks>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManBlockLimiter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManLooseChunkLimiter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManMods>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManLooseBlocks>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManMap>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManSpawn>.inst);
	}

	public void SetOverridePickupRange()
	{
		if (m_OverrideGrabDistance != -1f)
		{
			Singleton.Manager<ManPointer>.inst.SetPickupRange(m_OverrideGrabDistance);
		}
	}

	public void ShowPlayerProfile(bool show)
	{
		SetMainHudVisible(show);
		Singleton.Manager<ManLicenses>.inst.SetHUDVisible(show);
	}

	public void SetMainHudVisible(bool visible)
	{
		if (visible)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.AnchorTech);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MoneyCounter);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoaderButton);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechManagerButton);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ControlSchema);
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.WorldMapButton);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Snapshot);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.AnchorTech);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.MoneyCounter);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoaderButton);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechManagerButton);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ControlSchema);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.WorldMapButton);
		}
	}

	public override ManGameMode.GameType GetGameType()
	{
		return m_MyType;
	}

	public override string GetGameMode()
	{
		return "ModeMain";
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
		return m_BiomeMaps.MapStack.LatestMap.WorldGenVersionData;
	}

	public override bool CanPlayerChangeTech(Tank targetTech)
	{
		return !m_Respawning;
	}

	public override bool CanPlayerSwapTech()
	{
		return true;
	}

	public override bool CanPlayerPlaceTech()
	{
		return true;
	}

	public override InventoryMetaData GetReferenceInventory()
	{
		return new InventoryMetaData(Singleton.Manager<ManPurchases>.inst.GetInventory(), !Singleton.Manager<ManPlayer>.inst.PaletteUnlocked);
	}

	public void StartResourceDiscoveries()
	{
		m_StartedResourceDiscoveries = true;
	}

	public void RestartTutorial()
	{
	}

	public void SetPlayerInDanger(bool inDanger, bool forceAutoSnapshot = false)
	{
		if (inDanger != m_PlayerInDanger)
		{
			if (inDanger)
			{
				if ((forceAutoSnapshot || m_PlayerTechBackupIsDirty) && !m_IsTechInBuildBeam && !m_IsTechAutoSaveAfterDanger)
				{
					SaveLastUsedTech();
				}
			}
			else if (!m_IsTechInBuildBeam)
			{
				MarkPlayerTechForBackup(m_PlayerDangerSaveTimeout);
				m_IsTechAutoSaveAfterDanger = true;
			}
			m_PlayerInDanger = inDanger;
		}
		if (inDanger)
		{
			m_PlayerInDangerTime = Time.time + m_PlayerDangerTimeout;
		}
	}

	private void OnTankAttach(Tank tank, TankBlock block)
	{
		if (tank == Singleton.playerTank)
		{
			tankAttachEventCount++;
			BackupPlayerTechOnEdit();
		}
	}

	private void OnTankDetach(Tank tank, TankBlock block)
	{
		if (tank == Singleton.playerTank)
		{
			BackupPlayerTechOnEdit();
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool isSwitchedTo)
	{
		if (isSwitchedTo && (bool)tank)
		{
			SaveLastUsedTech();
		}
	}

	private void OnTankDestroyed(Tank tank, ManDamage.DamageInfo info)
	{
		if (tank.IsFriendly())
		{
			string value = ((!(info.SourceTank == null)) ? ((info.SourceTank.GetLocalisedName() != null) ? info.SourceTank.GetLocalisedName().m_Id : info.SourceTank.name) : "None");
			new Dictionary<string, object>
			{
				{ "killed_by", value },
				{
					"total_license_grades",
					Singleton.Manager<ManLicenses>.inst.GetTotalLicenseGrades()
				}
			};
		}
	}

	private void OnTankBeamEnabled(Tank tank, bool enabledBeam)
	{
		if (!(tank == Singleton.playerTank))
		{
			return;
		}
		m_IsTechInBuildBeam = enabledBeam;
		if (!m_PlayerInDanger)
		{
			if (enabledBeam)
			{
				SaveLastUsedTech();
			}
			else
			{
				BackupPlayerTechOnEdit();
			}
		}
	}

	private void BackupPlayerTechOnEdit()
	{
		if (!m_PlayerInDanger && !m_IsTechInBuildBeam)
		{
			if (m_IsTechAutoSaveAfterDanger && Time.time >= m_PlayerTechLastEditTime)
			{
				m_IsTechAutoSaveAfterDanger = false;
			}
			if (!m_IsTechAutoSaveAfterDanger)
			{
				MarkPlayerTechForBackup(m_PlayerTechEditSaveTimeout);
			}
		}
	}

	private void MarkPlayerTechForBackup(float backupTimeout)
	{
		m_PlayerTechBackupIsDirty = true;
		m_PlayerTechLastEditTime = Mathf.Max(m_PlayerTechLastEditTime, Time.time + backupTimeout);
	}

	private void SaveLastUsedTech()
	{
		if (!(Singleton.playerTank == null) && !Singleton.Manager<ManTechSwapper>.inst.CheckOperatingOnTech(Singleton.playerTank) && !Singleton.Manager<ManUndo>.inst.UndoInProgress)
		{
			if (m_PlayerTechBackup == null)
			{
				m_PlayerTechBackup = new TechData();
			}
			m_PlayerTechBackup.SaveTechIfChanged(Singleton.playerTank);
			m_PlayerTechBackup.Name = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 73), DateTime.Now.ToString("yyyy-MM-dd_HH-mm"));
			m_PlayerTechBlocks.Clear();
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				Visible.WeakReference weakReference = new Visible.WeakReference();
				weakReference.Set(current.visible);
				m_PlayerTechBlocks.Add(weakReference);
			}
			m_PlayerTechBackupIsDirty = false;
			m_PlayerTechLastEditTime = 0f;
			m_IsTechAutoSaveAfterDanger = false;
		}
	}

	private void OnTechSaved(TechData techData, Texture2D image, bool isPlayerTech)
	{
		if (isPlayerTech && !(Singleton.playerTank == null))
		{
			m_PlayerTechBackup = techData;
			m_PlayerTechBlocks.Clear();
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				Visible.WeakReference weakReference = new Visible.WeakReference();
				weakReference.Set(current.visible);
				m_PlayerTechBlocks.Add(weakReference);
			}
			m_PlayerTechBackupIsDirty = false;
			m_PlayerTechLastEditTime = 0f;
			m_IsTechAutoSaveAfterDanger = false;
		}
	}

	private void OnInvaderSent(Tank sentInvader)
	{
		if (sentInvader == Singleton.playerTank)
		{
			Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages(instant: true);
			UIScreenPlayerTechChoice screen = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.InvaderSentTechChoice) as UIScreenPlayerTechChoice;
			Singleton.Manager<ManScreenshot>.inst.RenderTechImage(m_PlayerTechBackup, screen.TechImageSize, encodeTechData: false, delegate(TechData techData, Texture2D techImage)
			{
				PlayerTechRecoveryData recoveryData = new PlayerTechRecoveryData(m_PlayerTechBackup);
				screen.SetLastPlayerTech(recoveryData, techImage, OnBuyCopyOfSentInvaderChosen);
				SetupRespawnOtherTechOption(screen, sentInvader);
				Singleton.Manager<ManUI>.inst.GoToScreen(screen);
			});
			m_Respawning = true;
		}
	}

	private void OnBuyCopyOfSentInvaderChosen(PlayerTechRecoveryData recoveryData)
	{
		Singleton.Manager<ManPlayer>.inst.PayMoney(recoveryData.m_TotalRecoveryCost);
		if (!Singleton.Manager<ManPlayer>.inst.InventoryIsUnrestricted)
		{
			IInventory<BlockTypes> playerInventory = Singleton.Manager<ManPlayer>.inst.PlayerInventory;
			foreach (KeyValuePair<int, int> item in recoveryData.m_BlocksAvailableFromInventory)
			{
				BlockTypes key = (BlockTypes)item.Key;
				playerInventory.HostConsumeItem(-1, key, item.Value);
			}
		}
		m_TechDataToSpawnAs = recoveryData.m_RecoveryTechData;
		Singleton.Manager<ManPurchases>.inst.TrySpawnPurchase(m_TechDataToSpawnAs, m_SaveData.m_LastAlivePlayerPosition, m_PlayerRespawner);
	}

	private void OnNearestPlayerTechDestroyed(Tank recycledTank)
	{
		SetupRespawnOtherTechOption();
	}

	private void DiscoverBlockCheck(Visible visible, ManPointer.DragAction action, Vector3 pos)
	{
		if (action == ManPointer.DragAction.Grab && (bool)Singleton.playerTank && Singleton.playerTank.ControllableByLocalPlayer && (bool)visible.block)
		{
			Singleton.Manager<ManLicenses>.inst.DiscoverBlock((BlockTypes)visible.ItemType);
		}
	}

	private void DiscoverItem(ItemTypeInfo item)
	{
		if (m_CurrentKnownItems == null)
		{
			m_CurrentKnownItems = new List<ItemTypeInfo>();
		}
		m_CurrentKnownItems.Add(item);
	}

	private bool IsItemDiscovered(ItemTypeInfo item)
	{
		if (m_CurrentKnownItems != null)
		{
			return m_CurrentKnownItems.Contains(item);
		}
		return false;
	}

	public override bool DisplaysSeed()
	{
		return true;
	}

	public override bool UsesFloatingOrigin()
	{
		return m_CanRespawn;
	}

	public override bool HasSaveGameSupport()
	{
		return true;
	}

	public override bool CanSave()
	{
		if (HasSaveGameSupport() && Singleton.Manager<ManFTUE>.inst.CanSave)
		{
			return !m_Respawning;
		}
		return false;
	}

	protected override bool IsAutoSaveEnabled()
	{
		if (m_SupportsAutoSave && Singleton.playerTank != null)
		{
			return !Singleton.Manager<ManChallenge>.inst.IsChallengeRunning;
		}
		return false;
	}

	protected override void Save(ManSaveGame.State saveState)
	{
		if (CanSave())
		{
			m_SaveData.m_CanRespawn = m_CanRespawn;
			m_SaveData.m_NeedsPlayerRespawn = m_CanRespawn;
			if (m_PlayerTechBackup != null)
			{
				m_SaveData.m_LastUsedPlayerTechData = m_PlayerTechBackup;
			}
			ObjectSpawner.SaveData playerRespawnerData = m_PlayerRespawner.Save();
			m_SaveData.m_PlayerRespawnerData = playerRespawnerData;
			m_SaveData.m_PlayerRespawnWithCustomRejectFunc = playerRespawnerData.m_FreeSpaceParams.m_RejectFunc != null;
			m_SaveData.m_StartedResourceDiscoveries = m_StartedResourceDiscoveries;
			m_SaveData.m_CurrentKnownItems = m_CurrentKnownItems;
			saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ModeData, m_SaveData);
		}
	}

	protected override void Load(ManSaveGame.State saveState)
	{
		if (saveState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ModeData, out m_SaveData) && m_SaveData != null)
		{
			m_CanRespawn = m_SaveData.m_CanRespawn || m_SaveData.m_NeedsPlayerRespawn;
			if (m_SaveData.m_LastUsedPlayerTechData != null)
			{
				m_PlayerTechBackup = m_SaveData.m_LastUsedPlayerTechData;
			}
			ObjectSpawner.SaveData playerRespawnerData = m_SaveData.m_PlayerRespawnerData;
			if (m_SaveData.m_PlayerRespawnWithCustomRejectFunc)
			{
				playerRespawnerData.m_FreeSpaceParams.m_RejectFunc = SpawnAwayFromLastPlayerPosition;
			}
			bool autoRetry = true;
			m_PlayerRespawner.Load(playerRespawnerData, autoRetry);
			m_StartedResourceDiscoveries = m_SaveData.m_StartedResourceDiscoveries;
			m_CurrentKnownItems = m_SaveData.m_CurrentKnownItems;
		}
		else
		{
			m_SaveData = new SaveData();
		}
	}

	private void ResetState()
	{
		TutorialLockBeam = false;
		TutorialLockBeamMove = false;
		TutorialDisableBlockRotation = false;
		TutorialDisableBlockRemoval = false;
		ReduceBlockDragReleaseSpeed = false;
		Singleton.Manager<ManGameMode>.inst.LockPlayerControls = false;
		SuppressTooltips = false;
		AllowBlockDecay = false;
		HideEnemyMarkers = false;
		SkipTutorial = false;
		DebugSkipTutorial = false;
		m_RestartTutorialButtonTimer = 0f;
		m_ShowTimer = 0f;
		m_SpacebarMashingTimer = 0f;
		m_SpacebarAmountPressed = 0;
		m_SpaceMashingShown = false;
		m_AttachingChunksShown = false;
		m_RespawnTimer = 0f;
		m_Respawning = false;
		m_PlayerSpawnSearchRadiusMultiplier = 1f;
		m_PlayerInDanger = false;
		m_PlayerTechBackupIsDirty = false;
		m_IsTechInBuildBeam = false;
		m_PlayerTechBackup = null;
		m_PlayerTechBlocks.Clear();
		m_PlayerRespawner.Cancel();
	}

	private bool PlayerTankIsTippedOver()
	{
		if ((bool)Singleton.playerTank)
		{
			return Vector3.Dot(Singleton.playerTank.trans.up, Vector3.up) < Mathf.Cos(1.3962634f);
		}
		return false;
	}

	private bool PlayerTankIsRecovered()
	{
		if ((bool)Singleton.playerTank)
		{
			return Vector3.Dot(Singleton.playerTank.trans.up, Vector3.up) > Mathf.Cos(0.17453292f);
		}
		return false;
	}

	private void CheckSpacebarMashing()
	{
		if (m_SpaceMashingShown)
		{
			return;
		}
		if (!Singleton.Manager<ManNetwork>.inst.IsMultiplayerAndInvulnerable() && Singleton.Manager<ManInput>.inst.GetButtonDown(2))
		{
			m_SpacebarMashingTimer = 0f;
			m_SpacebarAmountPressed++;
			if ((float)m_SpacebarAmountPressed > m_ShowMashingFireTutorialAfterPresses)
			{
				m_SpacebarAmountPressed = 0;
				m_SpaceMashingShown = true;
			}
		}
		if (m_SpacebarAmountPressed > 0)
		{
			m_SpacebarMashingTimer += Time.deltaTime;
			if (m_SpacebarMashingTimer > m_PressingFireWithinSec)
			{
				m_SpacebarAmountPressed = 0;
			}
		}
	}

	private void OnDragChunk(Visible vis, ManPointer.DragAction action, Vector3 pos)
	{
		if (!m_AttachingChunksShown && action == ManPointer.DragAction.Grab && vis.type == ObjectTypes.Chunk)
		{
			m_AttachingChunksShown = true;
		}
	}

	private void SetStartPosition()
	{
		base.StartPositionScene = Vector3.zero;
		base.SpawnAreaClearRadius = m_SpawnAreaClearRadius;
		Biome biome = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(base.StartPositionScene).Biome(0);
		Vector3 zero = Vector3.zero;
		Vector3 vector = Vector3.forward * m_GameStartBiomeSearchRadius;
		Quaternion quaternion = Quaternion.Euler(0f, 360f / m_GameStartBiomeSearchSamples, 0f);
		bool flag = false;
		for (int i = 0; (float)i < m_GameStartBiomeSearchSamples; i++)
		{
			if (Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(base.StartPositionScene + vector).Biome(0) == biome)
			{
				zero += vector;
			}
			else
			{
				flag = true;
			}
			vector = quaternion * vector;
		}
		if (flag)
		{
			base.StartPositionScene += zero.normalized * m_GameStartBiomeSearchRadius;
		}
	}

	public static bool CheckType<T>(TankBlock block) where T : Component
	{
		return block.GetComponent<T>();
	}

	public static bool CheckName(TankBlock block, string nameFragment)
	{
		return block.name.ToLower().Contains(nameFragment);
	}

	public static bool CheckTypeAndName<T>(TankBlock block, string nameFragment) where T : Component
	{
		if ((bool)block.GetComponent<T>())
		{
			return block.name.ToLower().Contains(nameFragment);
		}
		return false;
	}

	public static bool CheckTypeAndNotName<T>(TankBlock block, string nameFragment) where T : Component
	{
		if ((bool)block.GetComponent<T>())
		{
			return !block.name.ToLower().Contains(nameFragment);
		}
		return false;
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string notification = (CanSave() ? m_ExitConfirmString.Value : m_ExitWithoutSaveString.Value);
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

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		Vector3 cameraPos;
		Quaternion cameraRot;
		if (!IsLoadedFromSaveGame())
		{
			cameraPos = cameraSpawn.position;
			cameraRot = cameraSpawn.orientation;
			m_CanRespawn = false;
			Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: true);
			Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(10, 0, 23);
			Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
			Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
			ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
			if (currentUser != null)
			{
				foreach (ItemTypeInfo recipe in m_KnownRecipes)
				{
					currentUser.DiscoverRecipe((from o in Singleton.Manager<RecipeManager>.inst.recipeTable.m_RecipeLists.SelectMany((RecipeTable.RecipeList x) => x.m_Recipes)
						where o.m_OutputType == RecipeTable.Recipe.OutputType.Items && o.m_OutputItems.Length == 1 && o.m_OutputItems[0].m_Quantity == 1
						where o.Output_Deprecated == recipe
						select o.Output_Deprecated).FirstOrDefault(), saveImmediate: false);
				}
			}
			else
			{
				d.Log("(ModeMain::EnterGenerateTerrain) Current User is null");
			}
			m_CurrentKnownItems = new List<ItemTypeInfo>(m_KnownItems);
			if (currentUser != null && initSettings.TryGetValue("SkipTutorial", out var value))
			{
				SkipTutorial = (bool)value;
				if (currentUser.m_TutorialSkipSettings.m_WantsSkip != SkipTutorial)
				{
					currentUser.m_TutorialSkipSettings.m_WantsSkip = SkipTutorial;
					Singleton.Manager<ManProfile>.inst.Save();
				}
			}
		}
		else
		{
			new Dictionary<string, object>
			{
				{
					"Version",
					Singleton.Manager<ManSaveGame>.inst.CurrentState.m_WorldGenVersion
				},
				{
					"VersioningType",
					Singleton.Manager<ManSaveGame>.inst.CurrentState.m_WorldGenVersioningType
				}
			};
			ManSaveGame.CameraPosition cameraPos2 = Singleton.Manager<ManSaveGame>.inst.CurrentState.m_CameraPos;
			cameraPos = cameraPos2.GetBackwardsCompatiblePosition();
			cameraRot = Quaternion.LookRotation(cameraPos2.m_Forward);
		}
		if (TryLoadSetting<int>(initSettings, "BuildSizeLimit", out var outValue))
		{
			Singleton.Manager<ManSpawn>.inst.BlockLimit = outValue;
		}
		d.Log("Entered ModeMain: world seed '" + ((Singleton.Manager<ManWorld>.inst.SeedString != null) ? Singleton.Manager<ManWorld>.inst.SeedString : "<null>") + "'");
		EnterDefaultCameraMode();
		BiomeMap biomeMap = m_BiomeMaps.MapStack.SelectCompatibleBiomeMap();
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(biomeMap, cameraPos, cameraRot);
		Singleton.Manager<ManWorld>.inst.VendorSpawner.Enabled = true;
		SetStartPosition();
	}

	public override bool ModePreInit(InitSettings initSettings)
	{
		ResetState();
		return base.ModePreInit(initSettings);
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		spawnList.SpawnAll();
		if (!IsLoadedFromSaveGame())
		{
			Singleton.Manager<ManGameMode>.inst.SuppressFadeIn();
		}
		Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(DiscoverBlockCheck);
		TankBeam.OnBeamEnabled.Subscribe(OnTankBeamEnabled);
		Singleton.Manager<ManInvasion>.inst.OnSentInvaderRemoveFromPlay.Subscribe(OnInvaderSent);
		Singleton.Manager<ManSnapshots>.inst.PresetSavedEvent.Subscribe(OnTechSaved);
		Singleton.Manager<ManProgression>.inst.SetEncounterList(ManProgression.EncounterListType.Singleplayer);
		SetOverridePickupRange();
		bool flag = false;
		if (Singleton.Manager<ManSaveGame>.inst.CurrentState.m_SaveVersion != 0)
		{
			SKU.ParseChangeListVersionNumberToInt(SKU.ChangelistVersion, out var versionInt);
			flag = ManSaveGame.SavedInVersionPriorTo(versionInt);
		}
		if (Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) is ITechLoader techLoader)
		{
			techLoader.SetupScreenHandlers(LoadTechPreset);
			techLoader.SetupPlaceTechScreenHandler(OnPlaceTech);
		}
		bool num = !IsLoadedFromSaveGame();
		SetMainHudVisible(visible: true);
		bool show = !num;
		ShowPlayerProfile(show);
		Singleton.Manager<ManMap>.inst.EnableExploreAroundPlayer();
		if (num)
		{
			Singleton.Manager<ManMap>.inst.ExploreArea(base.StartPositionScene, base.SpawnAreaClearRadius);
		}
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		return FunctionStatus.Done;
	}

	protected override void EnterModeUpdateImpl()
	{
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyed);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnTankAttach);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnTankDetach);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		Singleton.Manager<ManPointer>.inst.DragEvent.Subscribe(OnDragChunk);
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.RespawnTechChoice) as UIScreenPlayerTechChoice).OnNearbyRespawnTechDestroyed.Subscribe(OnNearestPlayerTechDestroyed);
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		CheckSpacebarMashing();
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.RestartTutorial))
		{
			m_RestartTutorialButtonTimer += Time.deltaTime;
			if (m_RestartTutorialButtonTimer >= m_RestartTutorialButtonStayTime)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.RestartTutorial);
			}
		}
		if ((bool)Singleton.playerTank)
		{
			if (m_StartedResourceDiscoveries)
			{
				TechVision.VisibleIterator enumerator = Singleton.playerTank.Vision.IterateVisibles(k_SceneryAndChunks).GetEnumerator();
				while (enumerator.MoveNext())
				{
					Visible current = enumerator.Current;
					if (!IsItemDiscovered(current.m_ItemType) && (Singleton.playerTank.boundsCentreWorld - current.centrePosition).magnitude < m_ResourceDiscoverCheckDist)
					{
						DiscoverItem(current.m_ItemType);
					}
				}
			}
			bool flag = Time.time <= m_PlayerInDangerTime;
			if (m_PlayerInDanger && !flag)
			{
				SetPlayerInDanger(inDanger: false);
			}
			if (!m_PlayerInDanger && m_PlayerTechBackupIsDirty && Time.time >= m_PlayerTechLastEditTime)
			{
				SaveLastUsedTech();
			}
		}
		if (!Singleton.Manager<ManPauseGame>.inst.IsPaused)
		{
			if ((bool)Singleton.playerTank)
			{
				m_SaveData.m_LastAlivePlayerPosition = WorldPosition.FromScenePosition(Singleton.playerPos);
				if (!m_CanRespawn)
				{
					m_CanRespawn = true;
				}
			}
			else if (m_CanRespawn && !Singleton.Manager<ManPointer>.inst.IsDraggingController && !Singleton.Manager<ManPurchases>.inst.IsHotswappingTechs && Singleton.Manager<ManWorld>.inst.TileManager.IsTileAtPositionLoaded(Singleton.cameraTrans.position))
			{
				HandlePlayerRespawn();
			}
		}
		m_ShowTimer += Time.deltaTime;
		UpdateMissionScoreboardButtonPress();
		return FunctionStatus.Running;
	}

	protected override void ExitModeImpl()
	{
		Singleton.Manager<ManFTUE>.inst.DisableAll();
		Singleton.Manager<ManInvasion>.inst.Clear();
		Singleton.Manager<ManLicenses>.inst.Clear();
		Singleton.Manager<ManPlayer>.inst.Reset();
		Singleton.Manager<ManQuestLog>.inst.Reset();
		ResetState();
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankDestroyed);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Unsubscribe(OnTankAttach);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Unsubscribe(OnTankDetach);
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
		Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(OnDragChunk);
		(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.RespawnTechChoice) as UIScreenPlayerTechChoice).OnNearbyRespawnTechDestroyed.Unsubscribe(OnNearestPlayerTechDestroyed);
		Singleton.Manager<ManPointer>.inst.DragEvent.Unsubscribe(DiscoverBlockCheck);
		Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
		TankBeam.OnBeamEnabled.Unsubscribe(OnTankBeamEnabled);
		Singleton.Manager<ManInvasion>.inst.OnSentInvaderRemoveFromPlay.Unsubscribe(OnInvaderSent);
		Singleton.Manager<ManSnapshots>.inst.PresetSavedEvent.Unsubscribe(OnTechSaved);
		if (Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) is ITechLoader techLoader)
		{
			techLoader.RemoveScreenHandlers(LoadTechPreset);
			techLoader.RemovePlaceTechScreenHandler(OnPlaceTech);
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
	}

	private void Start()
	{
		m_PlayerRespawner.OnSpawnPosLocated.Subscribe(PlayerRespawnPositionFound);
		m_PlayerRespawner.OnObjectSpawned.Subscribe(PlayerRespawned);
	}
}
