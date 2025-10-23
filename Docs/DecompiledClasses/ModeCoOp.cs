#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Snapshots;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Networking;

public abstract class ModeCoOp<T> : Mode<T> where T : ModeCoOp<T>
{
	private class SaveData
	{
		public WorldPosition m_CenterPoint;

		public Dictionary<TTNetworkID, RemovedClientTechData> m_RemovedClientTechData;
	}

	[Serializable]
	private class RemovedClientTechData
	{
		public TechData m_TechData;

		public bool m_ClientActiveWhenStoredFlag;

		public RemovedClientTechData(TechData techData)
		{
			m_TechData = techData;
			m_ClientActiveWhenStoredFlag = ManSaveGame.Storing;
		}
	}

	public const int NumPlayerTeams = 5;

	public const int NeutralTeam = 1073741828;

	[SerializeField]
	private BiomeMapStackSetAsset m_BiomeChoices;

	[SerializeField]
	protected BlockFilterTable m_AllowedBlocks;

	[SerializeField]
	private float m_SpawnAreaClearRadius = 15f;

	[SerializeField]
	private float m_SpawnAreaExplorationRadius = 200f;

	[Header("Boundary")]
	[SerializeField]
	private Transform m_BoundaryEdgePrefab;

	[SerializeField]
	private int m_BoundaryDistance;

	[SerializeField]
	private int m_BoundaryTeleportDistance;

	[SerializeField]
	private float m_BoundaryMessageDistance;

	[Tooltip("Acceleration pushing you back into world (constant term)")]
	[SerializeField]
	[Header("Boundary pushback")]
	private float m_PushBackConst;

	[Tooltip("Acceleration pushing you back into world (proportional to proximity to teleporting edge)")]
	[SerializeField]
	private float m_PushBackDistance;

	[SerializeField]
	[Tooltip("Extra acceleration based on how fast you're going out-of-bounds")]
	private float m_PushBackVelocityCancel;

	[SerializeField]
	[Header("Tethering")]
	private bool m_TetheringMovesByTile;

	[SerializeField]
	private float m_TetheringMoveMinimum;

	[SerializeField]
	private bool m_TetheringMoveCanPushTechs;

	[SerializeField]
	private float m_TetherMoveTime = 1f;

	private bool m_JIPReqd;

	private WorldPosition m_CenterPoint;

	private Transform m_SpawnedBoundaryObject;

	private BoundaryMesh m_SpawnedBoundaryMesh;

	private TechSpawnHelper m_TechSpawner;

	private HashSet<IntVector2> m_TilesToRebuildObservers = new HashSet<IntVector2>();

	private float m_NextTetherMoveTime;

	private UIMultiplayerHUD m_MultiHud;

	private Dictionary<TTNetworkID, RemovedClientTechData> m_ClientTechData = new Dictionary<TTNetworkID, RemovedClientTechData>();

	public static string CreateTeamNameFromID(int id)
	{
		switch (id)
		{
		case 1073741828:
			return Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.HUD, 33);
		case 1073741824:
		case 1073741825:
		case 1073741826:
		case 1073741827:
		{
			int num = id - 1073741824;
			return string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 115), num + 1);
		}
		default:
			d.AssertFormat(false, "Invalid Team ID {0}", id);
			return $"Invalid team {id}";
		}
	}

	public override string GetGameSubmode()
	{
		return "";
	}

	public override ManHUD.HUDType GetDefaultHUDType()
	{
		return ManHUD.HUDType.MainGame;
	}

	public override bool CheckBlockAllowed(BlockTypes blockType)
	{
		return m_AllowedBlocks.CheckBlockAllowed(blockType);
	}

	public override WorldGenVersionData GetLatestMapVersion()
	{
		return m_BiomeChoices.GetStack(Singleton.Manager<ManWorld>.inst.BiomeChoice).LatestMap.WorldGenVersionData;
	}

	protected override void EnterGenerateTerrain(InitSettings initSettings)
	{
		ManSaveGame.SetCustomTechStoreHandlers(IsClientControlledMPTech, StoreClientControlledMPTechInSaveData);
		GenerateTerrain(regen: false);
	}

	public void GenerateTerrain(bool regen)
	{
		Singleton.Manager<ManWorld>.inst.BiomeChoice = Singleton.Manager<ManNetwork>.inst.BiomeChoice;
		Singleton.Manager<ManWorld>.inst.SeedString = Singleton.Manager<ManNetwork>.inst.WorldSeed;
		if (Singleton.Manager<ManNetwork>.inst.SetPiecePlacements != null && !ManNetwork.IsHostOrWillBe)
		{
			Singleton.Manager<ManWorld>.inst.AddTerrainSetPiecesForNetworkedGame(Singleton.Manager<ManNetwork>.inst.SetPiecePlacements);
		}
		if (!Singleton.Manager<ManNetwork>.inst.IsServerOrWillBe)
		{
			Singleton.Manager<ManSaveGame>.inst.CurrentState.m_WorldGenVersion = Singleton.Manager<ManNetwork>.inst.WorldGenVersionID;
			Singleton.Manager<ManSaveGame>.inst.CurrentState.m_WorldGenVersioningType = (BiomeMap.WorldGenVersioningType)Singleton.Manager<ManNetwork>.inst.WorldGenVersionType;
		}
		BiomeMap biomeMap = m_BiomeChoices.SelectCompatibleBiomeMap(Singleton.Manager<ManWorld>.inst.BiomeChoice);
		Vector3 cameraPos = (IsLoadedFromSaveGame() ? Singleton.Manager<ManSaveGame>.inst.CurrentState.m_CameraPos.GetBackwardsCompatiblePosition() : Vector3.zero);
		Quaternion cameraRot = (IsLoadedFromSaveGame() ? Quaternion.LookRotation(Singleton.Manager<ManSaveGame>.inst.CurrentState.m_CameraPos.m_Forward) : Quaternion.identity);
		Singleton.Manager<ManGameMode>.inst.RegenerateWorld(biomeMap, cameraPos, cameraRot);
		if (Singleton.Manager<ManNetwork>.inst.IsServerOrWillBe)
		{
			if (!IsLoadedFromSaveGame())
			{
				m_CenterPoint = WorldPosition.FromScenePosition(new Vector3(0f, 0f, 0f));
			}
			Singleton.Manager<ManWorld>.inst.TileManager.SetUnloadBehaviour(TileManager.UnloadBehaviour.MultiplayerHost);
		}
		else
		{
			m_CenterPoint = Singleton.Manager<ManNetwork>.inst.MapCenter;
			Singleton.Manager<ManWorld>.inst.TileManager.SetUnloadBehaviour(TileManager.UnloadBehaviour.ExternallyControlled);
		}
		Singleton.Manager<ManNetwork>.inst.SetMapSettings(m_CenterPoint, m_BoundaryDistance);
		Singleton.Manager<ManNetwork>.inst.SetBoundaryPushbackSettings(m_BoundaryTeleportDistance, m_PushBackConst, m_PushBackDistance, m_PushBackVelocityCancel);
		Singleton.Manager<ManWorld>.inst.SetFocalPointOverride(m_CenterPoint);
		CreateBoundaryMesh();
	}

	protected override void ExitModeImpl()
	{
		Singleton.Manager<ManNetwork>.inst.SetupWaitsForModeSwitch = true;
		if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.TechManager))
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechManager);
		}
		Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.CoopPlayerInfo);
		ITechLoader obj = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader;
		obj.RemoveScreenHandlers(LoadTechPreset);
		obj.RemovePlaceTechScreenHandler(OnPlaceTech);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ChatMessageEvent.Unsubscribe(OnChatMessage);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobbyUpdatedEvent.Unsubscribe(OnLobbyUpdated);
		Singleton.Manager<ManQuestLog>.inst.OnMissionNotification.Unsubscribe(OnMissionNotification);
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Unsubscribe(OnClientTileLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.TileUnloadedEvent.Unsubscribe(OnClientTileUnloaded);
		ClearTerrainBoundaryMesh();
	}

	protected override void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		saveData.m_CenterPoint = m_CenterPoint;
		saveData.m_RemovedClientTechData = new Dictionary<TTNetworkID, RemovedClientTechData>(m_ClientTechData);
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ModeData, saveData);
	}

	protected override void Load(ManSaveGame.State saveState)
	{
		Singleton.Manager<ManNetwork>.inst.Options = Singleton.Manager<ManNetworkLobby>.inst.AvailableGameTypes[1].m_Options;
		if (saveState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ModeData, out var saveData))
		{
			m_CenterPoint = saveData.m_CenterPoint;
			RestoreClientControllerMPTechFromSaveData(saveData);
		}
	}

	public void JoinInProgress()
	{
		if ((bool)Singleton.Manager<ManNetwork>.inst.NetController && (bool)Singleton.Manager<ManNetwork>.inst.MyPlayer)
		{
			if (Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase == NetController.Phase.Playing)
			{
				SetupPlayerHUD();
			}
			m_JIPReqd = false;
		}
		else
		{
			m_JIPReqd = true;
		}
	}

	public override bool CanSave()
	{
		return ManNetwork.IsHost;
	}

	public override bool DisplaysSeed()
	{
		return true;
	}

	public override bool OverlayShowsAllNetPlayerTechs()
	{
		return true;
	}

	public override bool UsesFloatingOrigin()
	{
		return true;
	}

	protected virtual TechData GetTechDataForPlayerSpawn(NetPlayer player)
	{
		return Singleton.Manager<ManNetwork>.inst.DefaultTechData;
	}

	protected override void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, Singleton.Manager<ManNetwork>.inst.IsServer ? 27 : 26);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
				ManSaveGame.ShouldStore = false;
				d.Log("ModeCoOp.ExitMenu - Confirm Quit");
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

	private void OnPlayerKicked(TTNetworkID playerID, string playerName, bool isLocalUser)
	{
		if (isLocalUser)
		{
			d.Log(string.Concat("ModeCoOp.OnPlayerKicked - ID:", playerID, " + name:", playerName));
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
			Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.NotInGame, show: false);
			Singleton.Manager<ManUI>.inst.GoToScreen(ManUI.ScreenType.NotificationScreen);
		}
	}

	private void OnPreEnterNetworkGame()
	{
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Subscribe(OnServerPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Subscribe(OnClientPhaseEnter);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Subscribe(OnPlayerKicked);
	}

	private void OnPostExitNetworkGame()
	{
		Singleton.Manager<ManNetwork>.inst.ServerPhaseEnterEvent.Unsubscribe(OnServerPhaseEnter);
		Singleton.Manager<ManNetwork>.inst.ClientPhaseEnterEvent.Unsubscribe(OnClientPhaseEnter);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.LobbyKickedEvent.Unsubscribe(OnPlayerKicked);
		Singleton.Manager<ManPointer>.inst.ForceRemoveDraggedItem();
		Singleton.Manager<ManNetwork>.inst.OnPreGameStarted.Unsubscribe(OnPreEnterNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnPostGameExited.Unsubscribe(OnPostExitNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStarted.Unsubscribe(OnServerHostStarted);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStopped.Unsubscribe(OnServerHostStopped);
		Singleton.Manager<ManNetwork>.inst.OnGenerateTerrainForced.Unsubscribe(GenerateTerrain);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.BoundaryMoved, OnBoundaryMovedMessage);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.AddFloatingNumberPopupMessage, OnAddFloatingNumberPopup);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.AddSystemChatMessage, OnSystemChatMessage);
		ClearTerrainBoundaryMesh();
	}

	private void OnServerPhaseEnter(NetController.Phase phase)
	{
		switch (phase)
		{
		case NetController.Phase.Playing:
			SetupPlayerHUD();
			break;
		case NetController.Phase.Outro:
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack())
			{
				Singleton.Manager<ManNetwork>.inst.CleanUpAllScreens();
			}
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechManager);
			Singleton.Manager<ManNetwork>.inst.InventoryAvailable = true;
			Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
			Singleton.Manager<ManPointer>.inst.ForceRemoveDraggedItem();
			break;
		case NetController.Phase.Exiting:
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
			{
				Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
			}
			break;
		case NetController.Phase.GatherPlayers:
		case NetController.Phase.Intro:
		case NetController.Phase.TechSelection:
		case NetController.Phase.Restarting:
			break;
		}
	}

	private void OnClientPhaseEnter(NetController.Phase phase)
	{
		d.Log("Client entering phase: " + phase);
		switch (phase)
		{
		case NetController.Phase.Intro:
			m_JIPReqd = false;
			break;
		case NetController.Phase.Playing:
			SetupPlayerHUD();
			break;
		case NetController.Phase.Outro:
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack())
			{
				Singleton.Manager<ManNetwork>.inst.CleanUpAllScreens();
			}
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechManager);
			Singleton.Manager<ManNetwork>.inst.InventoryAvailable = true;
			Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
			break;
		case NetController.Phase.Exiting:
			if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
			{
				Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
			}
			break;
		case NetController.Phase.GatherPlayers:
		case NetController.Phase.TechSelection:
		case NetController.Phase.Restarting:
			break;
		}
	}

	private void OnNetPlayerLoadedTile(NetworkConnection connection, IntVector2 tile, bool loaded)
	{
		m_TilesToRebuildObservers.Add(tile);
	}

	private void OnServerClientLoadedTile(NetworkMessage msg)
	{
		ClientLoadedTileMessage clientLoadedTileMessage = msg.ReadMessage<ClientLoadedTileMessage>();
		Singleton.Manager<ManNetwork>.inst.SetConnectionHasLoadedTile(msg.conn, clientLoadedTileMessage.m_TilePos, clientLoadedTileMessage.m_Loaded);
		OnNetPlayerLoadedTile(msg.conn, clientLoadedTileMessage.m_TilePos, clientLoadedTileMessage.m_Loaded);
	}

	private void OnServerVisibleChangedTileEvent(Visible visible, WorldTile oldTile, WorldTile newTile)
	{
		if (newTile == null || !Singleton.Manager<ManNetwork>.inst.ServerHasTileVisibilityChangedForAnyClient(oldTile, newTile))
		{
			return;
		}
		visible.GetComponent<NetworkIdentity>();
		switch (visible.type)
		{
		case ObjectTypes.Vehicle:
		{
			Tank tank = visible.tank;
			if (tank.IsNotNull() && tank.netTech.IsNotNull() && tank.netTech.ObserversInitialised)
			{
				tank.netTech.NetIdentity.RebuildObservers(initialize: false);
			}
			break;
		}
		case ObjectTypes.Block:
		{
			TankBlock block = visible.block;
			if (block.IsNotNull() && block.netBlock.IsNotNull() && block.netBlock.ObserversInitialised)
			{
				block.netBlock.NetIdentity.RebuildObservers(initialize: false);
			}
			break;
		}
		case ObjectTypes.Crate:
		{
			Crate crate = visible.crate;
			if (crate.IsNotNull() && crate.netCrate.IsNotNull() && crate.netCrate.ObserversInitialised)
			{
				crate.netCrate.NetIdentity.RebuildObservers(initialize: false);
			}
			break;
		}
		case ObjectTypes.Chunk:
		{
			ResourcePickup pickup = visible.pickup;
			if (pickup.IsNotNull() && pickup.netChunk.IsNotNull() && pickup.netChunk.ObserversInitialised)
			{
				pickup.netChunk.NetIdentity.RebuildObservers(initialize: false);
			}
			break;
		}
		case ObjectTypes.Scenery:
		case ObjectTypes.Waypoint:
			break;
		}
	}

	private void RebuildObserversForTile(IntVector2 tilePos)
	{
		WorldTile worldTile = Singleton.Manager<ManWorld>.inst.TileManager.LookupTile(in tilePos);
		if (worldTile == null)
		{
			return;
		}
		TileManager.VisibleIterator enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Vehicle, worldTile.Coord).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Tank tank = enumerator.Current.tank;
			if (tank.IsNotNull() && tank.netTech.IsNotNull() && tank.netTech.ObserversInitialised)
			{
				tank.netTech.NetIdentity.RebuildObservers(initialize: false);
			}
		}
		enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Block, worldTile.Coord).GetEnumerator();
		while (enumerator.MoveNext())
		{
			TankBlock block = enumerator.Current.block;
			if (block.IsNotNull() && block.netBlock.IsNotNull() && block.netBlock.ObserversInitialised)
			{
				block.netBlock.NetIdentity.RebuildObservers(initialize: false);
			}
		}
		enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Crate, worldTile.Coord).GetEnumerator();
		while (enumerator.MoveNext())
		{
			Crate crate = enumerator.Current.crate;
			if (crate.IsNotNull() && crate.netCrate.IsNotNull() && crate.netCrate.ObserversInitialised)
			{
				crate.netCrate.NetIdentity.RebuildObservers(initialize: false);
			}
		}
		enumerator = Singleton.Manager<ManWorld>.inst.TileManager.IterateVisibles(ObjectTypes.Chunk, worldTile.Coord).GetEnumerator();
		while (enumerator.MoveNext())
		{
			ResourcePickup pickup = enumerator.Current.pickup;
			if (pickup.IsNotNull() && pickup.netChunk.IsNotNull() && pickup.netChunk.ObserversInitialised)
			{
				pickup.netChunk.NetIdentity.RebuildObservers(initialize: false);
			}
		}
	}

	private void OnClientTileLoaded(WorldTile tile)
	{
		if (!ManNetwork.IsHost)
		{
			MessageBase message = new ClientLoadedTileMessage
			{
				m_TilePos = tile.Coord,
				m_Loaded = true
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ClientLoadedTile, message);
		}
	}

	private void OnClientTileUnloaded(WorldTile tile)
	{
		if (!ManNetwork.IsHost)
		{
			MessageBase message = new ClientLoadedTileMessage
			{
				m_TilePos = tile.Coord,
				m_Loaded = false
			};
			Singleton.Manager<ManNetwork>.inst.SendToServer(TTMsgType.ClientLoadedTile, message);
		}
	}

	private void OnAddFloatingNumberPopup(NetworkMessage netMsg)
	{
		PopupNumberMessage popupNumberMessage = netMsg.ReadMessage<PopupNumberMessage>();
		switch (popupNumberMessage.m_Type)
		{
		case PopupNumberMessage.Type.Money:
			Singleton.Manager<ManOverlay>.inst.AddFloatingTextOverlay(Singleton.Manager<Localisation>.inst.GetMoneyStringWithSymbol(popupNumberMessage.m_Number), popupNumberMessage.m_Position);
			break;
		case PopupNumberMessage.Type.XP:
			if (popupNumberMessage.m_Number != 0)
			{
				Singleton.Manager<ManOverlay>.inst.AddFloatingTextOverlayXP(popupNumberMessage.m_Number, popupNumberMessage.m_Position);
			}
			break;
		default:
			d.LogError("Unknown type for PopupNumberMessage: " + popupNumberMessage.m_Type);
			break;
		}
	}

	private void OnBoundaryMovedMessage(NetworkMessage msg)
	{
		WorldPositionMessage worldPositionMessage = msg.ReadMessage<WorldPositionMessage>();
		m_CenterPoint = worldPositionMessage.m_Position;
		if ((bool)m_SpawnedBoundaryObject)
		{
			Vector3 oldPos = (m_SpawnedBoundaryMesh ? m_SpawnedBoundaryMesh.transform.position : Vector3.zero);
			m_SpawnedBoundaryObject.position = worldPositionMessage.m_Position.ScenePosition.SetY(0f);
			if ((bool)m_SpawnedBoundaryMesh)
			{
				m_SpawnedBoundaryMesh.Move(oldPos, m_SpawnedBoundaryObject.position, m_TetherMoveTime);
			}
		}
		Singleton.Manager<ManNetwork>.inst.SetMapCenter(worldPositionMessage.m_Position);
		Singleton.Manager<ManWorld>.inst.SetFocalPointOverride(worldPositionMessage.m_Position);
	}

	private WorldPosition GetTargetScenePosition()
	{
		Vector3 scenePos = Vector3.zero;
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
		{
			NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
			if (player != null)
			{
				if (player.CurTech == null)
				{
					num2++;
					continue;
				}
				num++;
				scenePos += player.CurTech.tech.boundsCentreWorld;
			}
		}
		if (num2 > 0)
		{
			return Singleton.Manager<ManNetwork>.inst.MapCenter;
		}
		if (num > 0)
		{
			scenePos /= (float)num;
			if (m_TetheringMovesByTile)
			{
				IntVector2 tileCoordWorld = Singleton.Manager<ManWorld>.inst.TileManager.SceneToTileCoord(in scenePos);
				return WorldPosition.FromScenePosition(Singleton.Manager<ManWorld>.inst.TileManager.CalcTileCentreScene(in tileCoordWorld));
			}
			if (m_TetheringMoveMinimum > 0f && (scenePos - Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition).ToVector2XZ().sqrMagnitude < m_TetheringMoveMinimum * m_TetheringMoveMinimum)
			{
				return Singleton.Manager<ManNetwork>.inst.MapCenter;
			}
			return WorldPosition.FromScenePosition(in scenePos);
		}
		return Singleton.Manager<ManNetwork>.inst.MapCenter;
	}

	private bool IsOKTetherPosition(WorldPosition newCenter)
	{
		Vector3 scenePosition = newCenter.ScenePosition;
		bool result = true;
		for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
		{
			NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
			if (player.IsNotNull() && player.CurTech.IsNotNull())
			{
				float num = m_BoundaryDistance;
				if (!m_TetheringMoveCanPushTechs)
				{
					num += player.CurTech.tech.visible.Radius;
				}
				if ((player.CurTech.tech.boundsCentreWorld - scenePosition).ToVector2XZ().sqrMagnitude > num * num)
				{
					result = false;
				}
			}
		}
		return result;
	}

	private void UpdateCenterTethering()
	{
		if (Time.time > m_NextTetherMoveTime && (m_TechSpawner == null || !m_TechSpawner.IsSearching))
		{
			WorldPosition targetScenePosition = GetTargetScenePosition();
			if (targetScenePosition != Singleton.Manager<ManNetwork>.inst.MapCenter && IsOKTetherPosition(targetScenePosition))
			{
				WorldPositionMessage worldPositionMessage = new WorldPositionMessage();
				worldPositionMessage.m_Position = targetScenePosition;
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.BoundaryMoved, worldPositionMessage);
				m_NextTetherMoveTime = Time.time + m_TetherMoveTime;
			}
		}
	}

	protected virtual void SetupPlayerHUD()
	{
		m_MultiHud = (UIMultiplayerHUD)Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.Multiplayer);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.AnchorTech);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechLoaderButton);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.TechManagerButton);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.SkinsPaletteButton);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.ControlSchema);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.VoiceIndicator);
		Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.WorldMapButton);
		if (SKU.ConsoleUI)
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MPMissionUpdates);
		}
		else
		{
			Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.MPChat);
		}
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

	protected override void EnterModeUpdateImpl()
	{
		Singleton.Manager<ManBtnPrompt>.inst.HidePromptForced();
		Singleton.Manager<ManUI>.inst.PopAllPopups();
		if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby == null)
		{
			Singleton.Manager<ManNetworkLobby>.inst.ReshowLobbyError();
		}
		Singleton.Manager<ManNetwork>.inst.SetChatMaxMessageDisplayCount(25);
	}

	protected override FunctionStatus UpdateModeImpl()
	{
		UpdateOutOfBounds();
		if (Singleton.Manager<ManNetwork>.inst.NetController != null && m_JIPReqd)
		{
			JoinInProgress();
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			UpdateCenterTethering();
			if (m_TilesToRebuildObservers.Count > 0)
			{
				foreach (IntVector2 tilesToRebuildObserver in m_TilesToRebuildObservers)
				{
					RebuildObserversForTile(tilesToRebuildObserver);
				}
				m_TilesToRebuildObservers.Clear();
			}
		}
		UpdateMissionScoreboardButtonPress();
		if (Singleton.Manager<ManNetwork>.inst.IsServer && Singleton.Manager<ManNetwork>.inst.CurState == ManNetwork.State.InGame && !ManSaveGame.DeferLoadingForMP && !ManSaveGame.HasQueuedSpawns)
		{
			for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
			{
				NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
				UpdateSpawnLogicForPlayer(player);
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && m_TechSpawner != null)
		{
			m_TechSpawner.UpdateSpawnQueue();
		}
		bool flag = Singleton.Manager<ManNetwork>.inst.MyPlayer != null && !Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.BlockPalette) && !Singleton.Manager<ManHUD>.inst.IsHudElementExpanded(ManHUD.HUDElementType.BlockShop);
		if (flag != Singleton.Manager<ManHUD>.inst.IsHudElementVisible(ManHUD.HUDElementType.CoopPlayerInfo))
		{
			if (flag)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.CoopPlayerInfo);
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.CoopPlayerInfo);
			}
		}
		if (SKU.ConsoleUI)
		{
			UIMPChat.UpdateVisibility(ManHUD.HUDElementType.MPMissionUpdates);
		}
		else
		{
			UIMPChat.UpdateVisibility(ManHUD.HUDElementType.MPChat);
		}
		return FunctionStatus.Running;
	}

	private void TestSpawnTech()
	{
		TechSpawnHelper.SpawnParams spawnParams = new TechSpawnHelper.SpawnParams
		{
			m_MaxSpawnRange = m_BoundaryDistance,
			m_TechData = Singleton.Manager<ManNetwork>.inst.DefaultTechData
		};
		NetController.SpawnPolicy spawnPolicy = NetController.SpawnPolicy.CloseToAllies;
		if (Singleton.Manager<ManEncounter>.inst.GetPlayerRespawnOverride(ref spawnParams.m_EncounterLocation, ref spawnParams.m_EncounterFacingDir))
		{
			spawnPolicy = NetController.SpawnPolicy.AtEncounter;
		}
		m_TechSpawner.SpawnTechAtOptimalPosition(spawnParams, spawnPolicy, null, 0, OutOfBoundsRejectionFunc);
	}

	protected virtual void UpdateSpawnLogicForPlayer(NetPlayer player)
	{
		if (!(player != null) || player.m_HasRequestedSpawn || player.HasTech())
		{
			return;
		}
		player.m_SwitchTechDelay -= Time.deltaTime;
		if (!(player.m_SwitchTechDelay < 0f))
		{
			return;
		}
		player.m_HasRequestedSpawn = true;
		int lastUsedTrackedVisibleID = Singleton.Manager<ManPlayer>.inst.GetLastUsedTrackedVisibleID(player);
		bool flag = false;
		if (!SKU.IsNetEase && lastUsedTrackedVisibleID != 0)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(lastUsedTrackedVisibleID);
			if (trackedVisible != null && trackedVisible.visible != null && trackedVisible.visible.tank != null && trackedVisible.visible.tank.netTech != null && trackedVisible.visible.tank.netTech.NetPlayer == null)
			{
				Singleton.Manager<ManNetTechs>.inst.HostMakePlayerControlTech(player, trackedVisible.visible.tank.netTech);
				flag = true;
			}
		}
		if (flag)
		{
			return;
		}
		bool flag2 = true;
		TTNetworkID playerIDInLobby = player.GetPlayerIDInLobby();
		TechData techData = null;
		if (m_ClientTechData.TryGetValue(playerIDInLobby, out var value) && value != null)
		{
			techData = value.m_TechData;
			m_ClientTechData.Remove(playerIDInLobby);
			bool flag3 = false;
			bool flag4 = false;
			int num = Mathf.CeilToInt((float)techData.m_BlockSpecs.Count * 0.4f);
			InventoryMetaData referenceInventory = GetReferenceInventory();
			int descriptorValue = 4096;
			Dictionary<BlockTypes, int> dictionary = new Dictionary<BlockTypes, int>();
			foreach (TankPreset.BlockSpec blockSpec in techData.m_BlockSpecs)
			{
				BlockTypes blockType = blockSpec.GetBlockType();
				bool flag5 = Singleton.Manager<ManSpawn>.inst.IsBlockAvailableForTechSpawn(blockType, referenceInventory);
				int value2 = 0;
				if (flag5 && !dictionary.TryGetValue(blockType, out value2))
				{
					value2 = referenceInventory.GetInventoryBlockCount(blockType);
					flag5 = value2 == -1 || value2 > 0;
				}
				if (flag5)
				{
					if (!flag4)
					{
						int hashCode = ItemTypeInfo.GetHashCode(ObjectTypes.Block, (int)blockType);
						flag4 = Singleton.Manager<ManSpawn>.inst.VisibleTypeInfo.IsDescriptorFlag(hashCode, typeof(BlockAttributes), descriptorValue);
					}
					num--;
					dictionary[blockType] = ((value2 == -1) ? value2 : (value2 - 1));
					if (flag4 && num <= 0)
					{
						flag3 = true;
						break;
					}
				}
			}
			flag2 = !flag3;
		}
		if (flag2)
		{
			techData = GetTechDataForPlayerSpawn(player);
		}
		TechSpawnHelper.SpawnParams spawnParams = new TechSpawnHelper.SpawnParams
		{
			m_MaxSpawnRange = m_BoundaryDistance,
			m_TechData = techData,
			m_Player = player,
			m_TeamID = player.TechTeamID,
			m_UseInventory = !flag2,
			m_AllowSpawnWithBlocksMissing = true
		};
		NetController.SpawnPolicy spawnPolicy = NetController.SpawnPolicy.CloseToAllies;
		if (Singleton.Manager<ManEncounter>.inst.GetPlayerRespawnOverride(ref spawnParams.m_EncounterLocation, ref spawnParams.m_EncounterFacingDir))
		{
			spawnPolicy = NetController.SpawnPolicy.AtEncounter;
		}
		m_TechSpawner.SpawnTechAtOptimalPosition(spawnParams, spawnPolicy, null, 0, OutOfBoundsRejectionFunc);
	}

	private void CreateBoundaryMesh()
	{
		if (m_BoundaryEdgePrefab != null)
		{
			d.Assert(m_SpawnedBoundaryObject == null, "Called ModeCoOp.CreateBoundaryMesh, but we never cleaned up the one from last game. Doing it now");
			if (m_SpawnedBoundaryObject != null)
			{
				ClearTerrainBoundaryMesh();
			}
			m_SpawnedBoundaryObject = m_BoundaryEdgePrefab.Spawn(m_CenterPoint.ScenePosition.SetY(0f));
			m_SpawnedBoundaryMesh = m_SpawnedBoundaryObject.GetComponentInChildren<BoundaryMesh>();
		}
	}

	private void ClearTerrainBoundaryMesh()
	{
		if ((bool)m_SpawnedBoundaryObject)
		{
			m_SpawnedBoundaryObject.Recycle();
			m_SpawnedBoundaryObject = null;
			m_SpawnedBoundaryMesh = null;
		}
	}

	protected override void EnterPreModeImpl(InitSettings initSettings)
	{
		base.SpawnAreaClearRadius = m_SpawnAreaClearRadius;
		Singleton.Manager<CameraManager>.inst.ResetCamera(Vector3.up * 90f, Quaternion.LookRotation(Vector3.down));
		EnterDefaultCameraMode();
		ITechLoader obj = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechLoader) as ITechLoader;
		obj.SetupScreenHandlers(LoadTechPreset);
		obj.SetupPlaceTechScreenHandler(OnPlaceTech);
		Singleton.Manager<ManNetwork>.inst.OnPreGameStarted.Subscribe(OnPreEnterNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnPostGameExited.Subscribe(OnPostExitNetworkGame);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStarted.Subscribe(OnServerHostStarted);
		Singleton.Manager<ManNetwork>.inst.OnServerHostStopped.Subscribe(OnServerHostStopped);
		Singleton.Manager<ManNetwork>.inst.OnGenerateTerrainForced.Subscribe(GenerateTerrain);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.ChatMessageEvent.Subscribe(OnChatMessage);
		Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobbyUpdatedEvent.Subscribe(OnLobbyUpdated);
		Singleton.Manager<ManQuestLog>.inst.OnMissionNotification.Subscribe(OnMissionNotification);
		Singleton.Manager<ManWorld>.inst.TileManager.TileLoadedEvent.Subscribe(OnClientTileLoaded);
		Singleton.Manager<ManWorld>.inst.TileManager.TileUnloadedEvent.Subscribe(OnClientTileUnloaded);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.BoundaryMoved, OnBoundaryMovedMessage);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.AddFloatingNumberPopupMessage, OnAddFloatingNumberPopup);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.AddSystemChatMessage, OnSystemChatMessage);
		ManCombat.Projectiles.InitWeaponRoundUIDRange(0, int.MaxValue);
		Singleton.Manager<ManMap>.inst.EnableExploreAroundPlayer();
		if (ManNetwork.IsHostOrWillBe && !IsLoadedFromSaveGame())
		{
			Singleton.Manager<ManMap>.inst.ExploreArea(m_CenterPoint.ScenePosition, m_SpawnAreaExplorationRadius);
		}
		if (TryLoadSetting<int>(initSettings, "BuildSizeLimit", out var outValue))
		{
			Singleton.Manager<ManSpawn>.inst.BlockLimit = outValue;
		}
	}

	public override InventoryMetaData GetReferenceInventory()
	{
		return new InventoryMetaData(null, locked: false, m_AllowedBlocks);
	}

	public override bool CanPlayerPlaceTech()
	{
		return true;
	}

	public override bool CanPlayerChangeTech(Tank targetTech)
	{
		if (targetTech != null && (Singleton.Manager<ManNetwork>.inst.MapCenter.ScenePosition - targetTech.boundsCentreWorld).ToVector2XZ().magnitude > Singleton.Manager<ManNetwork>.inst.DangerDistance)
		{
			return false;
		}
		return base.CanPlayerChangeTech(targetTech);
	}

	protected override void SetupModeLoadSaveListeners()
	{
		SubscribeToEvents(Singleton.Manager<ManVisible>.inst);
		SubscribeToEvents(Singleton.Manager<ManWorld>.inst);
		SubscribeToEvents(Singleton.Manager<ManPresetFilter>.inst);
		SubscribeToEvents(Singleton.Manager<ManPop>.inst);
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
		UnsubscribeFromEvents(Singleton.Manager<ManPresetFilter>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManPop>.inst);
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

	private void UpdateOutOfBounds()
	{
		if (!(Singleton.Manager<ManNetwork>.inst.MyPlayer != null) || !(Singleton.Manager<ManNetwork>.inst.NetController != null))
		{
			return;
		}
		Vector3 scenePosition = m_CenterPoint.ScenePosition;
		NetTech curTech = Singleton.Manager<ManNetwork>.inst.MyPlayer.CurTech;
		if (!curTech)
		{
			return;
		}
		float magnitude = (curTech.tech.boundsCentreWorldNoCheck - scenePosition).SetY(0f).magnitude;
		float percentageOutOfBounds = (magnitude - (float)m_BoundaryDistance) / (float)(m_BoundaryTeleportDistance - m_BoundaryDistance);
		if (m_MultiHud.IsNotNull())
		{
			if (magnitude > m_BoundaryMessageDistance)
			{
				m_MultiHud.Message2.SetText(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Multiplayer, 93));
			}
			else
			{
				m_MultiHud.Message2.Clear();
			}
		}
		if (magnitude > (float)m_BoundaryDistance)
		{
			ShowOutOfBoundsWarning(show: true, scenePosition, percentageOutOfBounds);
		}
		else
		{
			ShowOutOfBoundsWarning(show: false, scenePosition, 0f);
		}
	}

	private void ShowOutOfBoundsWarning(bool show, Vector3 centerPoint, float percentageOutOfBounds)
	{
		UIOutOfBoundsHUD uIOutOfBoundsHUD = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.OutOfBounds) as UIOutOfBoundsHUD;
		if (!(uIOutOfBoundsHUD != null))
		{
			return;
		}
		if (show)
		{
			uIOutOfBoundsHUD.Show(null);
			uIOutOfBoundsHUD.SetOutOfBoundsPercentage(percentageOutOfBounds);
			if ((bool)Singleton.playerTank)
			{
				Vector3 directionToCenter = centerPoint - Singleton.playerTank.boundsCentreWorld;
				uIOutOfBoundsHUD.SetOutOfBoundsDirection(directionToCenter, Singleton.cameraTrans.forward, Singleton.cameraTrans.right);
			}
		}
		else
		{
			uIOutOfBoundsHUD.Hide(null);
		}
	}

	private bool OutOfBoundsRejectionFunc(Vector3 position, float radius, object context)
	{
		Vector3 scenePosition = m_CenterPoint.ScenePosition;
		return !((position - scenePosition).SetY(0f).sqrMagnitude <= (float)(m_BoundaryDistance * m_BoundaryDistance));
	}

	protected virtual void OnServerHostStarted()
	{
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.ClientRespawnConfirmation, OnServerRecieveClientRespawnConfirmation);
		m_TechSpawner = new TechSpawnHelper();
		Singleton.Manager<ManWorld>.inst.TileManager.VisibleChangedTileEvent.Subscribe(OnServerVisibleChangedTileEvent);
		Singleton.Manager<ManNetwork>.inst.SubscribeToServerMessage(TTMsgType.ClientLoadedTile, OnServerClientLoadedTile);
		Singleton.Manager<ManNetwork>.inst.OnNetPlayerLoadedTile.Subscribe(OnNetPlayerLoadedTile);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnServerPlayerAdded);
	}

	protected virtual void OnServerHostStopped()
	{
		m_TechSpawner = null;
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromServerMessage(TTMsgType.ClientRespawnConfirmation, OnServerRecieveClientRespawnConfirmation);
		Singleton.Manager<ManWorld>.inst.TileManager.VisibleChangedTileEvent.Unsubscribe(OnServerVisibleChangedTileEvent);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromServerMessage(TTMsgType.ClientLoadedTile, OnServerClientLoadedTile);
		Singleton.Manager<ManNetwork>.inst.OnNetPlayerLoadedTile.Unsubscribe(OnNetPlayerLoadedTile);
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnServerPlayerAdded);
	}

	private void LoadTechPreset(Snapshot capture)
	{
		TechData techData = capture.techData;
		NetPlayer myPlayer = Singleton.Manager<ManNetwork>.inst.MyPlayer;
		if (myPlayer != null && !myPlayer.IsSwitchingTech && myPlayer.CurTech != null)
		{
			myPlayer.ClientRequestReplaceTech(techData, Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory);
		}
	}

	private void OnPlaceTech(TechData techData, Vector3 position, Quaternion rotation)
	{
		Vector3 position2 = position + Vector3.up * (Singleton.Manager<ManTechSwapper>.inst.WheelClearancePad + 0.5f + techData.m_BoundsExtents.y);
		if (Singleton.Manager<ManNetwork>.inst.MyPlayer != null)
		{
			bool flag = Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && (Singleton.Manager<DebugUtil>.inst.RemoveTechLoaderRestrictions || Singleton.Manager<DebugUtil>.inst.AllBlocksInInventory);
			bool spawnTechWithUnavailableBlocksMissing = flag || Globals.inst.m_AllowPlaceTechWithMissingBlocks;
			Singleton.Manager<ManNetwork>.inst.MyPlayer.ClientRequestSpawnTech(techData, position2, rotation, replace: false, flag, spawnTechWithUnavailableBlocksMissing);
		}
	}

	private void OnMissionNotification(string message)
	{
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(SKU.ConsoleUI ? ManHUD.HUDElementType.MPMissionUpdates : ManHUD.HUDElementType.MPChat) as UIMPChat;
		if ((bool)uIMPChat)
		{
			uIMPChat.AddMissionMessage(message);
		}
	}

	private void OnSystemChatMessage(NetworkMessage netMsg)
	{
		SystemChatMessage systemChatMessage = netMsg.ReadMessage<SystemChatMessage>();
		UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(SKU.ConsoleUI ? ManHUD.HUDElementType.MPMissionUpdates : ManHUD.HUDElementType.MPChat) as UIMPChat;
		if (!uIMPChat)
		{
			return;
		}
		string text = Singleton.Manager<Localisation>.inst.GetLocalisedString(systemChatMessage.m_Bank, systemChatMessage.m_StringID);
		if (systemChatMessage.m_Params != null && systemChatMessage.m_Params.Length != 0 && !string.IsNullOrEmpty(text))
		{
			try
			{
				switch (systemChatMessage.m_StringID)
				{
				case 4:
				{
					BlockTypes itemType = (BlockTypes)Enum.Parse(typeof(BlockTypes), systemChatMessage.m_Params[0]);
					string itemName = StringLookup.GetItemName(ObjectTypes.Block, (int)itemType);
					text = string.Format(text, itemName);
					break;
				}
				case 3:
				{
					string arg = string.Format(Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.MissionLog.ChatCompleteMissionReward), arg1: StringLookup.GetCorporationName((FactionSubTypes)Enum.Parse(typeof(FactionSubTypes), systemChatMessage.m_Params[2])), arg0: systemChatMessage.m_Params[1], arg2: systemChatMessage.m_Params[3]);
					text = string.Format(text, systemChatMessage.m_Params[0], arg);
					break;
				}
				default:
				{
					string format = text;
					object[] args = systemChatMessage.m_Params;
					text = string.Format(format, args);
					break;
				}
				}
			}
			catch (FormatException ex)
			{
				d.LogError($"FormatException while processing chat message {systemChatMessage.m_Bank},{systemChatMessage.m_StringID} with {systemChatMessage.m_Params.Length} parameters: {ex.Message}");
				text = string.Empty;
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			uIMPChat.AddMissionMessage(text);
		}
	}

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

	private void OnLobbyUpdated(Lobby lobby)
	{
		Singleton.Manager<ManNetwork>.inst.CoOpAllowPlayerTechMods = lobby.Data.CoOpAllowPlayerTechModsChoice == 1;
	}

	private bool IsClientControlledMPTech(int visID)
	{
		return Singleton.Manager<ManPlayer>.inst.IsMPClientTech(visID);
	}

	private void StoreClientControlledMPTechInSaveData(Visible vis)
	{
		d.Assert(vis.tank.netTech != null && vis.tank.netTech.NetPlayer != null);
		if (m_ClientTechData == null)
		{
			m_ClientTechData = new Dictionary<TTNetworkID, RemovedClientTechData>();
		}
		TTNetworkID playerIDInLobby = vis.tank.netTech.NetPlayer.GetPlayerIDInLobby();
		TechData techData = new TechData();
		techData.SaveTech(vis.tank, saveRuntimeState: true);
		m_ClientTechData[playerIDInLobby] = new RemovedClientTechData(techData);
	}

	private void RestoreClientControllerMPTechFromSaveData(SaveData saveData)
	{
		if (m_ClientTechData == null)
		{
			m_ClientTechData = new Dictionary<TTNetworkID, RemovedClientTechData>();
		}
		m_ClientTechData.Clear();
		if (saveData.m_RemovedClientTechData == null)
		{
			return;
		}
		foreach (KeyValuePair<TTNetworkID, RemovedClientTechData> removedClientTechDatum in saveData.m_RemovedClientTechData)
		{
			m_ClientTechData.Add(removedClientTechDatum.Key, removedClientTechDatum.Value);
		}
	}

	protected void ReturnClientTechBlocks()
	{
		foreach (RemovedClientTechData value in m_ClientTechData.Values)
		{
			if (value.m_ClientActiveWhenStoredFlag)
			{
				Mode<ModeCoOpCampaign>.inst.GetSharedInventory().HostStoreTech(value.m_TechData);
				value.m_ClientActiveWhenStoredFlag = false;
			}
		}
	}

	private void OnServerRecieveClientRespawnConfirmation(NetworkMessage netMsg)
	{
		ClientRespawnConfirmationMessage clientRespawnConfirmationMessage = netMsg.ReadMessage<ClientRespawnConfirmationMessage>();
		NetPlayer netPlayer = Singleton.Manager<ManNetwork>.inst.FindPlayerById(clientRespawnConfirmationMessage.m_PlayerNetID);
		TechSpawnHelper.SpawnParams spawnParams = new TechSpawnHelper.SpawnParams
		{
			m_MaxSpawnRange = m_BoundaryDistance,
			m_TechData = Singleton.Manager<ManNetwork>.inst.DefaultTechData,
			m_Player = netPlayer,
			m_TeamID = netPlayer.TechTeamID
		};
		NetController.SpawnPolicy spawnPolicy = NetController.SpawnPolicy.CloseToAllies;
		if (Singleton.Manager<ManEncounter>.inst.GetPlayerRespawnOverride(ref spawnParams.m_EncounterLocation, ref spawnParams.m_EncounterFacingDir))
		{
			spawnPolicy = NetController.SpawnPolicy.AtEncounter;
		}
		m_TechSpawner.SpawnTechAtOptimalPosition(spawnParams, spawnPolicy, null, 0, OutOfBoundsRejectionFunc);
	}

	private void OnServerPlayerAdded(NetPlayer player)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			WorldPositionMessage worldPositionMessage = new WorldPositionMessage();
			worldPositionMessage.m_Position = m_CenterPoint;
			Singleton.Manager<ManNetwork>.inst.SendToClient(player.connectionToClient.connectionId, TTMsgType.BoundaryMoved, worldPositionMessage);
		}
	}
}
