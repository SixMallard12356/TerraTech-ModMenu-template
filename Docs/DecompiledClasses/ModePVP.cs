#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public abstract class ModePVP<T> : Mode<T> where T : Mode
{
	private enum RestartState
	{
		Countdown,
		TidyUp,
		TriggerStart,
		RegenTerrain,
		Wait
	}

	[Serializable]
	public struct DispenserSpawn
	{
		public V3Serial pos;

		public BlockTypes blockType;

		public int quantity;
	}

	[SerializeField]
	private BiomeMap m_BiomeMap;

	[SerializeField]
	private NetMapDataAsset m_DefaultMapData;

	[SerializeField]
	private NetMapDataAsset m_SmallMapData;

	[SerializeField]
	private NetMapDataAsset m_MediumMapData;

	[SerializeField]
	private NetMapDataAsset m_LargeMapData;

	[SerializeField]
	private ManSFX.MiscSfxType m_SelfDestructWarningSFXType = ManSFX.MiscSfxType.CabDetachKlaxon;

	[SerializeField]
	private MultiplayerTechSelectGroupAsset m_AvailableLoadouts;

	private NetOptions m_Options;

	private UIOutOfBoundsHUD m_OutOfBoundsHUD;

	private Vector3 m_CenterPoint = Vector3.zero;

	private NetMapDataAsset m_chosenMapData;

	private float m_OutOfBoundsTimer;

	private bool m_IsRegeneratingTerrain;

	private bool m_SpawnPrefabs;

	private TubeMeshGenerator.MeshDefinition m_MeshDef;

	private Transform m_SpawnedWorldObject;

	private MultiplayerTechSelectGroupAsset m_LoadoutsOfferedByServer;

	private bool m_Restart;

	private float m_RestartTimer;

	private RestartState m_RestartState;

	private bool m_JIPReqd;

	private NetLevelData m_ChosenLevel;

	private PrefabSpawner m_SpawnedSpawner;

	private List<TankBlock> m_CachedBlocksOnTech = new List<TankBlock>();

	private bool m_LocalClientIsChoosingTech;

	private bool m_LocalPlayerIsSelfDestructing;

	private float m_SelfDestructTimerStartValue;

	private InfoOverlay m_SelfDestructOverlay;

	private int m_SelfDestructMessageIndex;

	private const float m_SelfDestructWarningMessageCycleDelay = 1f;

	private float m_SelfDestructWarningMessageCycleTimer;

	private bool m_NextSpawnIsModeRespawn;

	private NetworkInstanceId m_NextNetSpawnPointID;

	private TechSpawnHelper m_TechSpawner;

	private CrateSpawner m_CrateSpawner;

	public bool IsRestarting => m_Restart;

	public void Dump(StringBuilder builder)
	{
		builder.AppendFormat("choosing tech={0} nextSpawnIsModeRespawn={1}\n", m_LocalClientIsChoosingTech, m_NextSpawnIsModeRespawn);
		builder.AppendFormat("rebuildingTerrain={0} spawnPrefabs={1}\n", m_IsRegeneratingTerrain, m_SpawnPrefabs);
		builder.AppendFormat("restart={0} state={1} restartTimer={2}\n", m_Restart, m_RestartState, m_RestartTimer);
		builder.AppendFormat("jipdReq={0}\n", m_JIPReqd);
		builder.AppendFormat("localPlayerDestructing={0} destructTimerStartVal={1} destructMsgInd={2} destructCycleTimer={3}\n", m_LocalPlayerIsSelfDestructing, m_SelfDestructTimerStartValue, m_SelfDestructMessageIndex, m_SelfDestructWarningMessageCycleTimer);
	}

	public MultiplayerTechSelectGroupAsset GetAvailableLoadouts()
	{
		if (m_LoadoutsOfferedByServer.IsNotNull())
		{
			return m_LoadoutsOfferedByServer;
		}
		return m_AvailableLoadouts;
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		GenerateTerrain(regen: false);
	}

	public void GenerateTerrain(bool regen)
	{
		m_IsRegeneratingTerrain = regen;
		m_SpawnPrefabs = true;
		Singleton.Manager<ManTimeOfDay>.inst.EnableSkyDome(enable: false);
		Singleton.Manager<ManTimeOfDay>.inst.EnableTimeProgression(enable: false);
		Singleton.Manager<ManTimeOfDay>.inst.SetDate(2700, 2, 22);
		Singleton.Manager<ManTimeOfDay>.inst.SetTimeOfDay(11, 0, 0);
		m_Options = Singleton.Manager<ManNetwork>.inst.Options;
		m_chosenMapData = m_DefaultMapData;
		switch (Singleton.Manager<ManNetwork>.inst.MapSize)
		{
		case ManNetwork.MapSizeOption.Small:
			m_chosenMapData = m_SmallMapData;
			break;
		case ManNetwork.MapSizeOption.Medium:
			m_chosenMapData = m_MediumMapData;
			break;
		case ManNetwork.MapSizeOption.Large:
			m_chosenMapData = m_LargeMapData;
			break;
		case ManNetwork.MapSizeOption.Auto:
			d.LogError("Trying to generate multiplayer map with size 'Auto'. This should have been resolved into a definite size already.");
			m_chosenMapData = m_LargeMapData;
			break;
		}
		Singleton.Manager<ManWorld>.inst.SeedString = null;
		if ((bool)m_SpawnedSpawner)
		{
			m_SpawnedSpawner.Recycle();
			m_SpawnedSpawner = null;
		}
		m_ChosenLevel = null;
		int chosenLevelDataId = Singleton.Manager<ManNetwork>.inst.ChosenLevelDataId;
		List<NetLevelData> levelData = m_Options.m_LevelData;
		if (levelData != null && levelData.Count > 0 && chosenLevelDataId <= levelData.Count && levelData[chosenLevelDataId].m_BiomeMap != null)
		{
			m_ChosenLevel = levelData[chosenLevelDataId];
			Singleton.Manager<ManWorld>.inst.SetTerrainGenerationTileOffset(new IntVector2(m_ChosenLevel.m_TileGenerationOffsetX, m_ChosenLevel.m_TileGenerationOffsetY));
			Singleton.Manager<ManGameMode>.inst.RegenerateWorld(m_ChosenLevel.m_BiomeMap, Vector3.zero, Quaternion.identity);
		}
		else
		{
			Singleton.Manager<ManWorld>.inst.SetTerrainGenerationTileOffset(new IntVector2(m_chosenMapData.m_MapData.m_TileGenerationOffsetX, m_chosenMapData.m_MapData.m_TileGenerationOffsetY));
			Singleton.Manager<ManGameMode>.inst.RegenerateWorld(m_BiomeMap, Vector3.zero, Quaternion.identity);
		}
		Singleton.Manager<ManWorld>.inst.TileManager.SetFixedTilesLoaded(m_chosenMapData.m_MapData.m_FixedTilesLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.SetUnloadBehaviour(TileManager.UnloadBehaviour.ExternallyControlled);
		m_CenterPoint = Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentre(new IntVector2(m_chosenMapData.m_MapData.m_OriginTileX, m_chosenMapData.m_MapData.m_OriginTileY));
		Singleton.Manager<ManNetwork>.inst.SetMapSettings(WorldPosition.FromScenePosition(in m_CenterPoint), m_chosenMapData.m_MapData.m_DangerDistance);
		Singleton.Manager<ManWorld>.inst.SetFocalPointOverride(WorldPosition.FromScenePosition(in m_CenterPoint));
		m_OutOfBoundsTimer = 0f;
		if (Singleton.Manager<ManNetwork>.inst.IsServerOrWillBe)
		{
			SpawnPointBank spawnPointBank = new SpawnPointBank(Singleton.Manager<ManNetwork>.inst.Options.m_LevelData[Singleton.Manager<ManNetwork>.inst.ChosenLevelDataId].m_SpawnPointBankConfig, m_chosenMapData.m_MapData.m_SpawnPointsDistance);
			Singleton.Manager<ManNetwork>.inst.SetSpawnBank(spawnPointBank);
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				spawnPointBank.SetAllSpawnShieldsEnabled(m_Options.m_GameData.m_PreGameData.m_InsideShield);
			}
		}
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		int chosenLevelDataId = Singleton.Manager<ManNetwork>.inst.ChosenLevelDataId;
		List<NetLevelData> levelData = (m_Options ?? Singleton.Manager<ManNetwork>.inst.Options).m_LevelData;
		BiomeMap biomeMap = ((levelData == null || levelData.Count <= 0 || chosenLevelDataId > levelData.Count || !(levelData[chosenLevelDataId].m_BiomeMap != null)) ? m_BiomeMap : levelData[chosenLevelDataId].m_BiomeMap);
		return biomeMap.WorldGenVersionData;
	}

	private void CreateBoundaryMesh()
	{
		if (m_Options == null || !(m_Options.prefab != null))
		{
			return;
		}
		d.Assert(m_SpawnedWorldObject == null, "Called ModeMultiplayer.CreateBoundaryMesh, but we never cleaned up the one from last game. Doing it now");
		if (m_SpawnedWorldObject != null)
		{
			ClearTerrainBoundaryMesh();
		}
		m_SpawnedWorldObject = m_Options.prefab.Spawn(m_CenterPoint.SetY(0f));
		if (!m_SpawnedWorldObject)
		{
			return;
		}
		MeshFilter component = m_SpawnedWorldObject.gameObject.GetComponent<MeshFilter>();
		if (component != null)
		{
			List<Vector3> list = new List<Vector3>();
			for (int i = 0; i < 360; i++)
			{
				Vector3 zero = Vector3.zero;
				zero += new Vector3(Singleton.Manager<ManNetwork>.inst.DangerDistance, 0f, 0f);
				zero = Quaternion.AngleAxis(i, Vector3.up) * zero;
				list.Add(zero);
			}
			Vector3[] array = CatmullRom.GenerateCurve(list.ToArray(), loops: true, 4);
			for (int j = 0; j < array.Length; j++)
			{
				array[j].y = Singleton.Manager<ManWorld>.inst.ProjectToGround(array[j]).y + 0.15f;
			}
			m_MeshDef = TubeMeshGenerator.GenerateMeshDef(array, 4f, 6);
			component.mesh = m_MeshDef.GenerateMesh();
			m_SpawnedWorldObject.gameObject.SetActive(value: true);
		}
		else
		{
			d.LogWarning("Track edge prefab needs to have a MeshFilter added to it");
		}
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		Singleton.Manager<ManNetwork>.inst.OnPreGameStarted.Subscribe(OnPreEnterNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStarted.Subscribe(OnServerHostStarted);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStopped.Subscribe(OnServerHostStopped);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Subscribe(OnPlayerRemoved);
		Singleton.Manager<ManNetwork>.inst.OnGenerateTerrainForced.Subscribe(GenerateTerrain);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ChatMessageEvent.Subscribe(OnChatMessage);
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Subscribe(OnModeCleanup);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.RequestClientRespawn, OnClientRespawnRequested);
		Singleton.Manager<CameraManager>.inst.ResetCamera(Vector3.up * 90f, Quaternion.LookRotation(Vector3.down));
		EnterDefaultCameraMode();
		Singleton.Manager<ManPurchases>.inst.ShowPalette(show: true);
		if (initSettings.ContainsKey("showLobbyUI"))
		{
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.MultiplayerSetupTEMP);
			Singleton.Manager<ManNetwork>.inst.SetupWaitsForModeSwitch = false;
		}
		m_OutOfBoundsHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.OutOfBounds) as UIOutOfBoundsHUD;
		ManCombat.Projectiles.InitWeaponRoundUIDRange(0, int.MaxValue);
	}

	protected override FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		return FunctionStatus.Done;
	}

	protected override void EnterModeUpdateImpl()
	{
		Singleton.Manager<ManBtnPrompt>.inst.HidePromptForced();
		Singleton.Manager<ManUI>.inst.PopAllPopups();
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby == null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.ReshowLobbyError();
		}
		Singleton.Manager<ManNetwork>.inst.SetChatMaxMessageDisplayCount(25);
		Singleton.Manager<ManNetwork>.inst.SetChatMessageDisplayTime(120f);
		Singleton.Manager<ManMap>.inst.ExploreArea(m_CenterPoint, m_chosenMapData.m_MapData.m_DangerDistance + 150);
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		bool flag = true;
		if (m_Restart)
		{
			flag = false;
		}
		if (flag && Singleton.Manager<ManNetwork>.inst.CurState == ManNetwork.State.Inactive && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		}
		if (m_IsRegeneratingTerrain && !Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating)
		{
			m_IsRegeneratingTerrain = false;
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ScoreBoard))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ScoreBoard);
			}
			Singleton.Manager<ManUI>.inst.ClearFade(3f);
		}
		if (m_SpawnPrefabs && !Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating)
		{
			if ((bool)m_ChosenLevel && (bool)m_ChosenLevel.m_PrefabSpawner)
			{
				m_SpawnedSpawner = m_ChosenLevel.m_PrefabSpawner.Spawn();
			}
			m_SpawnPrefabs = false;
			CreateBoundaryMesh();
		}
		UpdateOutOfBounds();
		if (Singleton.Manager<ManNetwork>.inst.NetController != null)
		{
			if (m_JIPReqd)
			{
				JoinInProgress();
			}
			if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing)
			{
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					if (CheckWinConditions() || CheckGameEndConditions())
					{
						Singleton.Manager<ManNetwork>.inst.NetController.ServerChangePhase(NetController.Phase.Outro);
					}
					else
					{
						ServerUpdateGameMode();
					}
				}
				if (Singleton.Manager<ManNetwork>.inst.Client != null)
				{
					ClientUpdateGameMode();
				}
				if (Singleton.playerTank != null && Singleton.Manager<ManTechs>.inst.IsEnemyInRange(Singleton.playerPos, Globals.inst.m_DangerPlayerMPRadius))
				{
					FactionSubTypes mainCorp = Singleton.playerTank.GetMainCorp();
					Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.SetPiece, mainCorp);
				}
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManNetwork>.inst.CurState == ManNetwork.State.InGame)
		{
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
			{
				NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
				if (player != null && !player.m_HasRequestedSpawn && !player.HasTech())
				{
					player.m_SwitchTechDelay -= Time.deltaTime;
					if (player.m_SwitchTechDelay < 0f && player.OnServerRemoveLife())
					{
						player.m_SwitchTechDelay = 3f;
						player.m_HasRequestedSpawn = true;
						Singleton.Manager<ManNetwork>.inst.SendToClient(player.connectionToClient.connectionId, TTMsgType.RequestClientRespawn, new PromptClientRespawnMessage
						{
							m_Loadouts = m_AvailableLoadouts
						});
					}
				}
			}
		}
		if (m_LocalClientIsChoosingTech)
		{
			if (Singleton.Manager<ManNetwork>.inst.MyPlayer == null)
			{
				d.LogError("Trying to select tech choice locally, but we no longer have a player.");
				m_LocalClientIsChoosingTech = false;
			}
			if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.UpdateTechSelection())
			{
				m_LocalClientIsChoosingTech = false;
				ClientRespawnConfirmationMessage message = new ClientRespawnConfirmationMessage
				{
					m_PlayerNetID = Singleton.Manager<ManNetwork>.inst.MyPlayer.netId.Value,
					m_CorporationChoice = Singleton.Manager<ManNetwork>.inst.StartingTechLoadoutCorp,
					m_LoadoutChoice = Singleton.Manager<ManNetwork>.inst.StartingTechLoadout,
					m_SkinIDChoice = Singleton.Manager<ManNetwork>.inst.StartingSkinID
				};
				Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ClientRespawnConfirmation, message);
				m_SelfDestructTimerStartValue = ManNetwork.CabSelfDestructTimeRanges[Singleton.Manager<ManNetwork>.inst.CabSelfDestructTimeIndex];
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			m_TechSpawner.UpdateSpawnQueue();
			if (m_CrateSpawner != null)
			{
				m_CrateSpawner.UpdateCrateSpawning();
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.CabSelfDestruct)
		{
			for (int j = 0; j < Singleton.Manager<ManNetTechs>.inst.GetNumTechs(); j++)
			{
				NetTech tech = Singleton.Manager<ManNetTechs>.inst.GetTech(j);
				if (tech.SelfDestructController != null)
				{
					bool flag2 = !tech.SelfDestructController.block.HasNeighbours;
					if (flag2 != tech.IsSetToSelfDestruct)
					{
						float fuseDuration = ((Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech == tech) ? m_SelfDestructTimerStartValue : ((float)ManNetwork.CabSelfDestructTimeRanges[Singleton.Manager<ManNetwork>.inst.CabSelfDestructTimeIndex]));
						tech.SetToSelfDestruct(flag2, fuseDuration);
					}
				}
			}
			bool flag3 = Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.IsSetToSelfDestruct;
			if (flag3)
			{
				if (!m_LocalPlayerIsSelfDestructing)
				{
					Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SelfDestruct);
					if (m_SelfDestructOverlay == null)
					{
						m_SelfDestructOverlay = Singleton.Manager<ManOverlay>.inst.AddWarningOverlay(Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.SelfDestructController.block.visible, showWhileBuilding: true);
						m_SelfDestructOverlay.OverrideDataHook(SelfDestructInfoPanelDataHandler);
					}
					m_SelfDestructMessageIndex = 0;
					m_SelfDestructWarningMessageCycleTimer = 1f;
					Singleton.Manager<ManNetwork>.inst.MyPlayer.SetMultiHUDToSelfDestruct(selfDestructActive: true, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 101 + m_SelfDestructMessageIndex));
					Singleton.Manager<ManSFX>.inst.PlayMiscLoopingSFX(m_SelfDestructWarningSFXType);
				}
				else
				{
					if (m_SelfDestructOverlay != null)
					{
						m_SelfDestructOverlay.OverrideDataHook(SelfDestructInfoPanelDataHandler);
						m_SelfDestructOverlay.ResetDismissTimer();
					}
					m_SelfDestructWarningMessageCycleTimer -= Time.deltaTime;
					if (m_SelfDestructWarningMessageCycleTimer <= 0f)
					{
						m_SelfDestructMessageIndex = ((m_SelfDestructMessageIndex == 0) ? 1 : 0);
						m_SelfDestructWarningMessageCycleTimer = 1f;
					}
					Singleton.Manager<ManNetwork>.inst.MyPlayer.SetMultiHUDToSelfDestruct(selfDestructActive: true, Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 101 + m_SelfDestructMessageIndex));
					if (Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.SelfDestructController != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.SelfDestructController.block.damage.SelfDestructTimeRemaining() <= 0.25f)
					{
						Singleton.Manager<ManSFX>.inst.StopMiscLoopingSFX(m_SelfDestructWarningSFXType);
					}
					m_SelfDestructTimerStartValue = Mathf.Max(m_SelfDestructTimerStartValue - Time.deltaTime, 0.01f);
				}
			}
			else
			{
				if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.HasTech())
				{
					Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SelfDestruct);
				}
				if (m_LocalPlayerIsSelfDestructing)
				{
					if (m_SelfDestructOverlay != null)
					{
						Singleton.Manager<ManOverlay>.inst.RemoveWarningOverlay(m_SelfDestructOverlay);
						m_SelfDestructOverlay = null;
					}
					if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
					{
						Singleton.Manager<ManNetwork>.inst.MyPlayer.SetMultiHUDToSelfDestruct(selfDestructActive: false);
					}
					Singleton.Manager<ManSFX>.inst.StopMiscLoopingSFX(m_SelfDestructWarningSFXType);
				}
				float num = ManNetwork.CabSelfDestructTimeRanges[Singleton.Manager<ManNetwork>.inst.CabSelfDestructTimeIndex];
				if (m_SelfDestructTimerStartValue < num)
				{
					m_SelfDestructTimerStartValue = Mathf.Min(m_SelfDestructTimerStartValue + Time.deltaTime * 0.5f, num);
				}
			}
			m_LocalPlayerIsSelfDestructing = flag3;
		}
		UpdateMissionScoreboardButtonPress();
		if (m_Restart)
		{
			switch (m_RestartState)
			{
			case RestartState.Countdown:
				m_RestartTimer += Time.deltaTime;
				if (m_RestartTimer >= 0.5f)
				{
					m_RestartState = RestartState.TidyUp;
					m_RestartTimer = 0f;
				}
				break;
			case RestartState.TidyUp:
				m_ChosenLevel = null;
				m_RestartState = RestartState.TriggerStart;
				Singleton.Manager<ManNetwork>.inst.ClearForRestart();
				m_RestartTimer = 0f;
				break;
			case RestartState.TriggerStart:
				m_RestartTimer += Time.deltaTime;
				if (m_RestartTimer >= 1.5f)
				{
					Singleton.Manager<ManNetwork>.inst.SetupWaitsForModeSwitch = false;
					Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.TriggerGameStart();
					m_RestartState = RestartState.RegenTerrain;
					m_RestartTimer = 0f;
				}
				break;
			case RestartState.RegenTerrain:
				m_RestartTimer += Time.deltaTime;
				if (m_RestartTimer >= 1.5f)
				{
					ClearTerrainBoundaryMesh();
					Resources.UnloadUnusedAssets();
					GenerateTerrain(regen: true);
					m_RestartState = RestartState.Wait;
					Singleton.Manager<ManNetwork>.inst.InventoryAvailable = true;
				}
				break;
			case RestartState.Wait:
				if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
				{
					Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.CheckForStartGame();
				}
				break;
			}
		}
		if (!SKU.ConsoleUI)
		{
			UIMPChat.UpdateVisibility(ManHUD.HUDElementType.MPChat);
		}
		return FunctionStatus.Running;
	}

	private void ClearTerrainBoundaryMesh()
	{
		if ((bool)m_SpawnedWorldObject)
		{
			MeshFilter component = m_SpawnedWorldObject.GetComponent<MeshFilter>();
			if (component != null)
			{
				UnityEngine.Object.Destroy(component.mesh);
				component.mesh = null;
			}
			m_SpawnedWorldObject.Recycle();
			m_SpawnedWorldObject = null;
		}
	}

	protected override void ExitModeImpl()
	{
		d.Log("ModeMultiplayer.ExitModeImpl Called");
		Singleton.Manager<ManPurchases>.inst.ShowPalette(show: false);
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ScoreBoard))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ScoreBoard);
		}
		CleanupSelfDestructState();
		Singleton.Manager<ManNetwork>.inst.SetupWaitsForModeSwitch = true;
		ClearTerrainBoundaryMesh();
		if ((bool)m_SpawnedSpawner)
		{
			m_SpawnedSpawner.Recycle();
			m_SpawnedSpawner = null;
		}
		m_LoadoutsOfferedByServer = null;
		m_ChosenLevel = null;
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.RequestClientRespawn, OnClientRespawnRequested);
		Singleton.Manager<ManNetwork>.inst.OnPostGameExited.Subscribe(OnPostExitNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnPreGameStarted.Unsubscribe(OnPreEnterNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnGenerateTerrainForced.Unsubscribe(GenerateTerrain);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ChatMessageEvent.Unsubscribe(OnChatMessage);
	}

	private void OnModeCleanup(Mode mode)
	{
		Singleton.Manager<ManNetwork>.inst.OnServerHostStarted.Unsubscribe(OnServerHostStarted);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStopped.Unsubscribe(OnServerHostStopped);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.OnPlayerRemoved.Unsubscribe(OnPlayerRemoved);
		Singleton.Manager<ManGameMode>.inst.ModeCleanUpEvent.Unsubscribe(OnModeCleanup);
	}

	public override string GetGameSubmode()
	{
		return "";
	}

	public override ManHUD.HUDType GetDefaultHUDType()
	{
		return ManHUD.HUDType.MainGame;
	}

	public override bool CanPlayerChangeTech(Tank targetTech)
	{
		return false;
	}

	public void JoinInProgress()
	{
		if ((bool)Singleton.Manager<ManNetwork>.inst.NetController && (bool)Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			GameModeInit();
			if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing)
			{
				m_Restart = false;
				SetupPlayerHUD();
			}
			m_JIPReqd = false;
		}
		else
		{
			m_JIPReqd = true;
		}
	}

	protected override void SetupModeLoadSaveListeners()
	{
		base.SetupModeLoadSaveListeners();
		SubscribeToEvents(Singleton.Manager<ManMap>.inst);
	}

	protected override void CleanupModeLoadSaveListeners()
	{
		UnsubscribeFromEvents(Singleton.Manager<ManMap>.inst);
		base.CleanupModeLoadSaveListeners();
	}

	private void OnPlayerKicked(TTNetworkID playerID, string playerName, bool isLocalUser)
	{
		if (isLocalUser)
		{
			d.Log(string.Concat("ModeMultiplayer.OnPlayerKicked - ID:", playerID, " + name:", playerName));
			Singleton.Manager<ManNetwork>.inst.IgnoreServerDisconnect = true;
			Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
			Singleton.Manager<ManNetwork>.inst.KickedTidyUp();
			Singleton.Manager<ManUI>.inst.FadeToBlack();
			Action accept = delegate
			{
				Singleton.Manager<ManUI>.inst.PopScreen();
				Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
				Singleton.Manager<ManUI>.inst.ClearFade(3f);
			};
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 118);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 11);
			(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications).Set(localisedString, accept, localisedString2);
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
		}
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 26);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				d.Log("ModeMultiplayer.ExitMenu - Confirm Quit");
				Singleton.Manager<ManUI>.inst.ExitAllScreens();
				Singleton.Manager<ManNetwork>.inst.IgnoreServerDisconnect = true;
				Singleton.Manager<ManNetworkLobby>.inst.LeaveLobby();
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
		exitScreen.Set(localisedString, accept, decline);
	}

	private void SetupPlayerHUD()
	{
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			Singleton.Manager<ManNetwork>.inst.MyPlayer.InitScoreHUD();
		}
		if (Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.AllowCollaboration)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.AnchorTech);
		}
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ControlSchema);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.VoiceIndicator);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.WorldMapButton);
		if (!SKU.ConsoleUI)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MPChat);
		}
	}

	private bool CheckWinConditions()
	{
		bool result = false;
		switch (Singleton.Manager<ManNetwork>.inst.NetController.CurrentVictoryConditions)
		{
		case NetController.VictoryConditions.ScoreExceeds:
			result = CheckGameEndScore();
			break;
		default:
			d.LogError("ModeMultiplayer.CheckWinConditions - Unhandled Victory Condition " + Singleton.Manager<ManNetwork>.inst.NetController.CurrentVictoryConditions);
			break;
		case NetController.VictoryConditions.FinishWaves:
		case NetController.VictoryConditions.None:
			break;
		}
		return result;
	}

	private bool CheckGameEndConditions()
	{
		bool result = true;
		int numPlayers = Singleton.Manager<ManNetwork>.inst.GetNumPlayers();
		for (int i = 0; i < numPlayers; i++)
		{
			if (Singleton.Manager<ManNetwork>.inst.GetPlayer(i).IsPlayerActive)
			{
				result = false;
				break;
			}
		}
		return result;
	}

	private bool CheckGameEndScore()
	{
		bool result = false;
		NetController.ScorePolicy currentScorePolicy = Singleton.Manager<ManNetwork>.inst.NetController.CurrentScorePolicy;
		float scoreToWin = Singleton.Manager<ManNetwork>.inst.NetController.ScoreToWin;
		if (Singleton.Manager<ManNetwork>.inst.NetController.IsMultiTeamGame)
		{
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetTeamCount(); i++)
			{
				int teamID = i;
				if (Singleton.Manager<ManNetwork>.inst.NetController.GetTeamScore(teamID) >= scoreToWin)
				{
					result = true;
					break;
				}
			}
		}
		else
		{
			for (int j = 0; j < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); j++)
			{
				if (Singleton.Manager<ManNetwork>.inst.GetPlayer(j).Score.Evaluate(currentScorePolicy) >= scoreToWin)
				{
					result = true;
					break;
				}
			}
		}
		return result;
	}

	private void UpdateOutOfBounds()
	{
		if (!(Singleton.Manager<ManNetwork>.inst.MyPlayer != null) || !(Singleton.Manager<ManNetwork>.inst.NetController != null))
		{
			return;
		}
		NetTech curTech = Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech;
		if (!curTech)
		{
			return;
		}
		ModuleTechController selfDestructController = curTech.SelfDestructController;
		ModuleDamage moduleDamage = ((selfDestructController != null) ? selfDestructController.GetComponent<ModuleDamage>() : null);
		float magnitude = (curTech.tech.boundsCentreWorldNoCheck - m_CenterPoint).SetY(0f).magnitude;
		if (magnitude >= (float)m_chosenMapData.m_MapData.m_KillDistance)
		{
			if ((bool)moduleDamage)
			{
				moduleDamage.DebugKillTechBlock();
			}
			d.Assert(m_CachedBlocksOnTech.Count == 0, "m_CachedBlocksOnTech was not cleaned up properly!?");
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = curTech.tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current = enumerator.Current;
				m_CachedBlocksOnTech.Add(current);
			}
			foreach (TankBlock item in m_CachedBlocksOnTech)
			{
				if ((bool)item.damage)
				{
					item.damage.MultiplayerOutOfBoundsDamageBlock((float)item.damage.maxHealth * 2f);
				}
			}
			m_CachedBlocksOnTech.Clear();
		}
		else if (magnitude > (float)m_chosenMapData.m_MapData.m_DangerDistance)
		{
			float percentageOutOfBounds = (magnitude - (float)m_chosenMapData.m_MapData.m_DangerDistance) / (float)(m_chosenMapData.m_MapData.m_KillDistance - m_chosenMapData.m_MapData.m_DangerDistance);
			ShowOutOfBoundsWarning(show: true, percentageOutOfBounds);
			m_OutOfBoundsTimer -= Time.deltaTime;
			if (!(m_OutOfBoundsTimer <= 0f))
			{
				return;
			}
			if ((bool)moduleDamage)
			{
				moduleDamage.MultiplayerOutOfBoundsDamageBlock((float)moduleDamage.maxHealth / 30f);
			}
			BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = curTech.tech.blockman.IterateBlocks().GetEnumerator();
			while (enumerator.MoveNext())
			{
				TankBlock current3 = enumerator.Current;
				if (current3.damage != moduleDamage)
				{
					current3.damage.MultiplayerFakeDamagePulse();
				}
			}
			m_OutOfBoundsTimer = 1f;
		}
		else
		{
			ShowOutOfBoundsWarning(show: false, 0f);
			m_OutOfBoundsTimer = 1f;
		}
	}

	private void ShowOutOfBoundsWarning(bool show, float percentageOutOfBounds)
	{
		if (!(m_OutOfBoundsHUD != null))
		{
			return;
		}
		if (show)
		{
			m_OutOfBoundsHUD.Show(null);
			m_OutOfBoundsHUD.SetOutOfBoundsPercentage(percentageOutOfBounds);
			if ((bool)Singleton.playerTank)
			{
				Vector3 directionToCenter = m_CenterPoint - Singleton.playerTank.boundsCentreWorld;
				m_OutOfBoundsHUD.SetOutOfBoundsDirection(directionToCenter, Singleton.cameraTrans.forward, Singleton.cameraTrans.right);
			}
		}
		else
		{
			m_OutOfBoundsHUD.Hide(null);
		}
	}

	private void CleanupSelfDestructState()
	{
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SelfDestruct);
		if (m_SelfDestructOverlay != null)
		{
			Singleton.Manager<ManOverlay>.inst.RemoveWarningOverlay(m_SelfDestructOverlay);
			m_SelfDestructOverlay = null;
		}
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			Singleton.Manager<ManNetwork>.inst.MyPlayer.SetMultiHUDToSelfDestruct(selfDestructActive: false);
		}
		Singleton.Manager<ManSFX>.inst.StopMiscLoopingSFX(m_SelfDestructWarningSFXType);
		m_LocalPlayerIsSelfDestructing = false;
	}

	private void OnPreEnterNetworkGame()
	{
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Clear();
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Clear();
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Clear();
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Subscribe(OnServerPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Subscribe(OnClientPhaseEnter);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Subscribe(OnPlayerKicked);
	}

	private void OnPostExitNetworkGame()
	{
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Unsubscribe(OnServerPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Unsubscribe(OnClientPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.OnPostGameExited.Unsubscribe(OnPostExitNetworkGame);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Unsubscribe(OnPlayerKicked);
		m_Options = null;
		Singleton.Manager<ManPointer>.inst.ForceRemoveDraggedItem();
	}

	private void OnServerHostStarted()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.ClientRespawnConfirmation, OnServerRecieveClientRespawnConfirmation);
		m_TechSpawner = new TechSpawnHelper();
		m_TechSpawner.OnBeforeSpawnTech.Subscribe(OnBeforeSpawnHelperSpawnTech);
		m_CrateSpawner = new CrateSpawner();
		m_CrateSpawner.RegisterMessageHandlers();
		Singleton.Manager<ManNetTechs>.inst.OnBeforeServerSpawnTechWithAuthorityEvent.Subscribe(OnBeforeServerSpawnTechWithAuthority);
		Singleton.Manager<ManNetTechs>.inst.OnServerTechUnspawnedEvent.Subscribe(OnServerTechUnpawned);
		Singleton.Manager<ManNetTechs>.inst.OnTechSpawnedEvent.Subscribe(OnTechSpawned);
	}

	private void OnServerHostStopped()
	{
		Singleton.Manager<ManNetTechs>.inst.OnBeforeServerSpawnTechWithAuthorityEvent.Unsubscribe(OnBeforeServerSpawnTechWithAuthority);
		Singleton.Manager<ManNetTechs>.inst.OnServerTechUnspawnedEvent.Unsubscribe(OnServerTechUnpawned);
		Singleton.Manager<ManNetTechs>.inst.OnTechSpawnedEvent.Unsubscribe(OnTechSpawned);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromServerMessage(TTMsgType.ClientRespawnConfirmation, OnServerRecieveClientRespawnConfirmation);
		m_TechSpawner.OnBeforeSpawnTech.Unsubscribe(OnBeforeSpawnHelperSpawnTech);
		m_TechSpawner = null;
		m_CrateSpawner.UnregisterMessageHandlers();
		m_CrateSpawner = null;
		Singleton.Manager<ManNetwork>.inst.SetSpawnBank(null);
	}

	private void OnBeforeSpawnHelperSpawnTech(TechData techData, Vector3 position, Quaternion rotation, NetPlayer playerOwner)
	{
		Vector3 position2 = position;
		position2.y += 3.5f;
		NetSpawnPoint netSpawnPoint = Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.SpawnShieldBubble(position2, rotation);
		m_NextNetSpawnPointID = netSpawnPoint.netId;
		m_NextSpawnIsModeRespawn = true;
	}

	private void OnBeforeServerSpawnTechWithAuthority(NetTech netTech)
	{
		d.Assert(Singleton.Manager<ManNetwork>.inst.IsServer);
		if (m_NextSpawnIsModeRespawn)
		{
			Vector3 position = netTech.tech.trans.position;
			position.y += 3.5f;
			netTech.tech.trans.position = position;
			bool startInvulnerable = Singleton.Manager<ManNetwork>.inst.NetController.RespawnInvulnerabilityTime > 0f;
			netTech.OnServerSetStartsInvulnerable(startInvulnerable);
			netTech.InitialSpawnShieldID = m_NextNetSpawnPointID.Value;
			m_NextNetSpawnPointID = NetworkInstanceId.Invalid;
		}
		m_NextSpawnIsModeRespawn = false;
	}

	private void OnTechSpawned(NetTech netTech)
	{
	}

	private void OnServerTechUnpawned(NetTech netTech)
	{
		Singleton.Manager<ManNetwork>.inst.ServerSpawnBank.SetShieldEnabled(netTech.InitialSpawnShieldID, enabled: false);
	}

	protected virtual void OnServerPhaseEnter(NetController.Phase phase)
	{
		d.Log("Server entering phase: " + phase);
		switch (phase)
		{
		case NetController.Phase.Intro:
			GameModeInit();
			break;
		case NetController.Phase.Playing:
			m_Restart = false;
			SetupPlayerHUD();
			m_CrateSpawner.Init(m_chosenMapData.m_MapData.m_SpawnPointsDistance);
			break;
		case NetController.Phase.Outro:
			if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MultiplayerTechSelect) || Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack() || Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.ControlSchema))
			{
				Singleton.Manager<ManNetwork>.inst.CleanUpAllScreens();
			}
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ScoreBoard);
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPaletteButton))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
			}
			CleanupSelfDestructState();
			Singleton.Manager<ManNetwork>.inst.InventoryAvailable = false;
			Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
			Singleton.Manager<ManPointer>.inst.ForceRemoveDraggedItem();
			m_CrateSpawner.DeInit();
			m_NextSpawnIsModeRespawn = false;
			m_LocalClientIsChoosingTech = false;
			GameModeExit();
			break;
		case NetController.Phase.Restarting:
			Singleton.Manager<ManUI>.inst.FadeToBlack();
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ScoreBoard))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ScoreBoard);
			}
			m_Restart = true;
			m_RestartTimer = 0f;
			m_RestartState = RestartState.Countdown;
			break;
		case NetController.Phase.Exiting:
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
			{
				Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
			}
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPaletteButton))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
			}
			break;
		case NetController.Phase.GatherPlayers:
		case NetController.Phase.TechSelection:
			break;
		}
	}

	protected virtual void OnClientPhaseEnter(NetController.Phase phase)
	{
		d.Log("Client entering phase: " + phase);
		switch (phase)
		{
		case NetController.Phase.Intro:
			GameModeInit();
			m_JIPReqd = false;
			break;
		case NetController.Phase.Playing:
			m_Restart = false;
			SetupPlayerHUD();
			break;
		case NetController.Phase.Outro:
			if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MultiplayerTechSelect) || Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack() || Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.ControlSchema))
			{
				Singleton.Manager<ManNetwork>.inst.CleanUpAllScreens();
			}
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ScoreBoard);
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPaletteButton))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
			}
			CleanupSelfDestructState();
			Singleton.Manager<ManNetwork>.inst.InventoryAvailable = false;
			Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
			GameModeExit();
			break;
		case NetController.Phase.Restarting:
			d.Log("[ModeMultiplayer] Restarting...");
			Singleton.Manager<ManUI>.inst.FadeToBlack();
			if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen))
			{
				(Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.MatchmakingLobbyScreen) as UIScreenNetworkLobby).RepeatingClient = true;
				Singleton.Manager<ManNetwork>.inst.CleanUpAllScreens();
			}
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.ScoreBoard))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ScoreBoard);
			}
			m_Restart = true;
			m_RestartTimer = 0f;
			m_RestartState = RestartState.RegenTerrain;
			Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Unsubscribe(OnServerPhaseEnter);
			Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Unsubscribe(OnClientPhaseEnter);
			break;
		case NetController.Phase.Exiting:
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
			{
				Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
			}
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.SkinsPaletteButton))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
			}
			break;
		case NetController.Phase.GatherPlayers:
		case NetController.Phase.TechSelection:
			break;
		}
	}

	private void OnClientRespawnRequested(NetworkMessage netMsg)
	{
		PromptClientRespawnMessage promptClientRespawnMessage = netMsg.ReadMessage<PromptClientRespawnMessage>();
		m_LoadoutsOfferedByServer = promptClientRespawnMessage.m_Loadouts;
		m_LocalClientIsChoosingTech = true;
	}

	private void OnServerRecieveClientRespawnConfirmation(NetworkMessage netMsg)
	{
		ClientRespawnConfirmationMessage clientRespawnConfirmationMessage = netMsg.ReadMessage<ClientRespawnConfirmationMessage>();
		NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(clientRespawnConfirmationMessage.m_PlayerNetID);
		int corporationChoice = clientRespawnConfirmationMessage.m_CorporationChoice;
		if (corporationChoice >= 0 && corporationChoice < m_AvailableLoadouts.GetTechPresets().Count)
		{
			MultiplayerTechSelectPresetAsset multiplayerTechSelectPresetAsset = m_AvailableLoadouts.GetTechPresets()[corporationChoice];
			TechSpawnHelper.SpawnParams spawnParams = new TechSpawnHelper.SpawnParams
			{
				m_MaxSpawnRange = m_chosenMapData.m_MapData.m_SpawnPointsDistance,
				m_TechData = multiplayerTechSelectPresetAsset.m_TankPreset.GetTechDataFormatted(),
				m_Player = netPlayer,
				m_TeamID = netPlayer.TechTeamID
			};
			for (int i = 0; i < spawnParams.m_TechData.m_BlockSpecs.Count; i++)
			{
				TankPreset.BlockSpec value = spawnParams.m_TechData.m_BlockSpecs[i];
				value.m_SkinID = (byte)clientRespawnConfirmationMessage.m_SkinIDChoice;
				spawnParams.m_TechData.m_BlockSpecs[i] = value;
			}
			m_TechSpawner.SpawnTechAtOptimalPosition(spawnParams, Singleton.Manager<ManNetwork>.inst.NetController.ServerSpawnPolicy, Singleton.Manager<ManNetwork>.inst.ServerSpawnBank);
			netPlayer.InitDeathStreakRewards(multiplayerTechSelectPresetAsset);
			if (netPlayer.Inventory.IsNotNull())
			{
				NetInventory inventory = netPlayer.Inventory;
				inventory.Clear();
				inventory.FillTo((clientRespawnConfirmationMessage.m_LoadoutChoice == 0) ? multiplayerTechSelectPresetAsset.m_InventoryBlockList1 : multiplayerTechSelectPresetAsset.m_InventoryBlockList2);
				netPlayer.AddRewardAndDeathStreakToInventory();
			}
			else
			{
				d.LogError("Player " + netPlayer.name + " does not have inventory, so we can't give them their loadout");
			}
		}
		else
		{
			d.LogError("Player " + netPlayer.name + " selected invalid loadout corporation");
		}
	}

	private InfoOverlayDataValues SelfDestructInfoPanelDataHandler(InfoOverlayDataValues data)
	{
		float num = 0f;
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech != null && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.IsSetToSelfDestruct && Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.SelfDestructController != null)
		{
			num = Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech.SelfDestructController.block.damage.SelfDestructTimeRemaining();
		}
		else
		{
			d.LogError("SelfDestructInfoPanelDataHandler - Invalid Self destruct target for current player!");
		}
		string subtitle = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 100), num.ToString("F1"));
		data.m_Subtitle = subtitle;
		data.m_Description = "Self destruct description";
		data.IconSprite = null;
		return data;
	}

	[Conditional("USE_ANALYTICS")]
	private void SendStartNetHostAnalytics()
	{
	}

	[Conditional("USE_ANALYTICS")]
	private void SendStartNetGameAnalytics(string eventName = "NetStartGame")
	{
		new Dictionary<string, object>
		{
			{
				"Version",
				SKU.ChangelistVersion
			},
			{
				"NumPlayers",
				Singleton.Manager<ManNetwork>.inst.GetNumPlayers()
			},
			{
				"Mode",
				Singleton.Manager<ManNetwork>.inst.NetController.GameModeType.ToString()
			}
		};
	}

	private void OnPlayerAdded(NetPlayer player)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			NetInventory component = Singleton.Manager<ManNetwork>.inst.NetInventoryPrefab.transform.Spawn(Vector3.zero, Quaternion.identity).GetComponent<NetInventory>();
			component.name = "NetInventory " + player.name;
			InventoryBlockList inventoryBlockList = Singleton.Manager<ManNetwork>.inst.Options.m_InventoryBlockList;
			if (inventoryBlockList.Blocks.Length != 0)
			{
				inventoryBlockList.BuildInventory(component);
			}
			player.SetInventory(component);
			component.OnServerRegisterUser(player);
			NetworkServer.Spawn(component.gameObject);
		}
	}

	private void OnPlayerRemoved(NetPlayer player)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer && player.Inventory.IsNotNull())
		{
			NetworkServer.UnSpawn(player.Inventory.gameObject);
			player.Inventory.OnServerUnregisterUser(player);
			player.Inventory.transform.Recycle();
			player.SetInventory(null);
		}
	}

	protected abstract void GameModeInit();

	protected abstract void ServerUpdateGameMode();

	protected abstract void ClientUpdateGameMode();

	protected abstract void OnServerPlayerOutOfBounds();

	protected abstract void GameModeExit();

	public abstract MultiplayerModeType GetMultiplayerGameType();

	private void OnChatMessage(LobbyPlayerData playerData, uint netId, int teamChannel, string message)
	{
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
		if (!uIMPChat)
		{
			return;
		}
		if (teamChannel > -1)
		{
			if ((bool)Singleton.Manager<ManNetwork>.inst.MyPlayer && Singleton.Manager<ManNetwork>.inst.MyPlayer.LobbyTeamID == teamChannel && !uIMPChat.IsPlayerMuted(new NetworkInstanceId(netId)))
			{
				uIMPChat.AddChatMessage(playerData, "{0}: {1}", message, teamChat: true);
			}
		}
		else if (netId != NetworkInstanceId.Invalid.Value)
		{
			if (!uIMPChat.IsPlayerMuted(new NetworkInstanceId(netId)))
			{
				uIMPChat.AddChatMessage(playerData, "{0}: {1}", message, teamChat: false);
			}
		}
		else
		{
			uIMPChat.AddChatMessage(playerData, "{0}: {1}", message, teamChat: false);
		}
	}
}
