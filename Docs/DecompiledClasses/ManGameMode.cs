#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Rewired;
using UnityEngine;
using UnityEngine.Serialization;

public class ManGameMode : Singleton.Manager<ManGameMode>
{
	public enum GameType
	{
		Attract,
		MainGame,
		RacingChallenge,
		FlyingChallenge,
		SumoShowdown,
		RaD,
		Misc,
		Defense,
		Deathmatch,
		Gauntlet,
		Creative,
		CoOpCreative,
		CoOpCampaign
	}

	public enum GameState
	{
		NotInMode,
		Setup,
		PreGame,
		InGame,
		FadeToBlack,
		PostGame,
		CleaningUp
	}

	public class ModeSettings
	{
		public Action m_SwitchAction;

		public string m_LoadVehicleModeRestriction;

		public string m_LoadVehicleSubModeRestriction;

		public string m_LoadVehicleUserDataRestriction;

		private Mode.InitSettings m_nextModeCachedSettings = new Mode.InitSettings();

		public void SwitchToMode()
		{
			Singleton.Manager<ManGameMode>.inst.ClearModeInitSettings();
			foreach (KeyValuePair<string, object> nextModeCachedSetting in m_nextModeCachedSettings)
			{
				Singleton.Manager<ManGameMode>.inst.AddModeInitSetting(nextModeCachedSetting.Key, nextModeCachedSetting.Value);
			}
			m_SwitchAction();
			m_nextModeCachedSettings.Clear();
		}

		public void AddModeInitSetting(string settingName, object settingData)
		{
			m_nextModeCachedSettings[settingName] = settingData;
		}

		public void AddModeInitSettings(Mode.InitSettings initSettings)
		{
			foreach (KeyValuePair<string, object> initSetting in initSettings)
			{
				AddModeInitSetting(initSetting.Key, initSetting.Value);
			}
		}

		public bool GetModeInitSetting(string settingName, out object settingData)
		{
			return m_nextModeCachedSettings.TryGetValue(settingName, out settingData);
		}

		public void SetModeInitSettings(Mode.InitSettings initSettings)
		{
			ClearCachedSettings();
			AddModeInitSettings(initSettings);
		}

		public void ClearCachedSettings()
		{
			m_nextModeCachedSettings.Clear();
		}
	}

	[FormerlySerializedAs("currentMode")]
	public Mode m_StartMode;

	public float m_ReturnToMenuInactivityTime;

	[SerializeField]
	[Tooltip("Time interval at which time spent in mode is sent to the server. Time in minutes")]
	private int m_SendModeTimeAnalyticsEventIntervalMins = 10;

	[SerializeField]
	protected int[] m_AutoSaveIntervalMinutesOptions = new int[4] { 1, 5, 15, 0 };

	public EventNoParams ModeSwitchEvent;

	public Event<Mode> ModeSetupEvent;

	public Event<Mode> ModeStartEvent;

	public Event<Mode> ModeFinishedEvent;

	public Event<Mode> ModeCleanUpEvent;

	private Mode[] m_Modes;

	private Mode m_CurrentMode;

	private Mode m_NextMode;

	private GameState m_CurrentGameState;

	private Mode.InitSettings m_NextModeInitSettings = new Mode.InitSettings();

	private Mode.InitSettings m_CurrentModeInitSettings;

	private float m_LastInputTime;

	private AsyncOperation m_UnloadAssetsAsyncOp;

	private bool m_SuppressFadeIn;

	private int m_CameraCullingMask;

	private bool m_HasErrorDialogue;

	private string m_ErrorBodyMsg;

	private string m_ErrorAcceptMsg;

	private bool m_CancelEventHandled;

	private bool m_CancelEventFired;

	private float m_AutoSaveTimeSec = 60f;

	private float m_AutoSaveSecsElapsed;

	public ModeSettings NextModeSetting { get; set; }

	public bool LockPlayerControls { get; set; }

	public bool IsSwitchingMode
	{
		get
		{
			if (!(m_NextMode != null))
			{
				if (m_CurrentGameState != GameState.NotInMode)
				{
					return m_CurrentGameState != GameState.InGame;
				}
				return false;
			}
			return true;
		}
	}

	public bool IsCancelEventHandled => m_CancelEventHandled;

	public IEnumerable<Mode> AllModes => m_Modes;

	public bool IsCurrent<T>() where T : Mode
	{
		return m_CurrentMode is T;
	}

	public bool IsCurrent(Mode mode)
	{
		return mode == m_CurrentMode;
	}

	public bool UnlimitedMultiplayerInventory()
	{
		return IsCurrent<ModeCoOpCreative>();
	}

	public bool CanEarnXp()
	{
		if (!Globals.inst.m_AwardXPandBB)
		{
			return false;
		}
		if (!IsCurrent<ModeMain>())
		{
			return IsCurrent<ModeCoOpCampaign>();
		}
		return true;
	}

	public bool CanEarnMoney()
	{
		if (!Globals.inst.m_AwardXPandBB)
		{
			return false;
		}
		if (!IsCurrent<ModeMain>() && !IsCurrent<ModeCoOpCampaign>())
		{
			return IsCurrent<ModeMisc>();
		}
		return true;
	}

	public bool ShareEncounters()
	{
		return IsCurrent<ModeCoOpCampaign>();
	}

	public bool IsCurrentModeCampaign()
	{
		if (!IsCurrent<ModeMain>())
		{
			return IsCurrent<ModeCoOpCampaign>();
		}
		return true;
	}

	public GameType GetCurrentGameType()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetGameType();
		}
		return GameType.Attract;
	}

	public string GetCurrentGameMode()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetGameMode();
		}
		return "";
	}

	public string GetCurrentGameSubmode()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetGameSubmode();
		}
		return "";
	}

	public GameState GetModePhase()
	{
		return m_CurrentGameState;
	}

	public float GetSceneryRegrowTime()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetSceneryRegrowTime();
		}
		return Globals.inst.m_DefaultSceneryRegrowTime;
	}

	public float GetAutoExpireDelay(ObjectTypes type)
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetAutoExpireDelay(type);
		}
		return 0f;
	}

	public bool AutoExpireIfOffScreen()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.AutoExpireIfOffScreen();
		}
		return true;
	}

	public float GetAutoExpireRange()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetAutoExpireRange();
		}
		return 0f;
	}

	public float GetSafeAreaRadius()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.GetSafeAreaRadius();
		}
		return 0f;
	}

	public bool IsFriendlyFireEnabled()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.FriendlyFireEnabled();
		}
		return false;
	}

	public float GetCurrentModeRunningTime()
	{
		float result = 0f;
		if ((bool)m_CurrentMode)
		{
			result = m_CurrentMode.ModeRunningTime;
		}
		return result;
	}

	public bool GetIsInPlayableMode()
	{
		if (m_CurrentMode == null || m_CurrentMode.GetGameType() == GameType.Attract)
		{
			return false;
		}
		return true;
	}

	public bool GetIsInAnyMode()
	{
		return m_CurrentMode != null;
	}

	public bool GetIsModeSteamWorkshopSave()
	{
		return m_CurrentMode.IsLoadedFromWorkshopSave();
	}

	public bool CurrentModeHasSaveGameSupport()
	{
		bool result = false;
		if ((bool)m_CurrentMode)
		{
			result = m_CurrentMode.HasSaveGameSupport();
		}
		return result;
	}

	public void SetModeDefaultCamera(bool forceSwitch = true)
	{
		if (m_CurrentMode != null)
		{
			m_CurrentMode.EnterDefaultCameraMode(forceSwitch);
		}
	}

	public bool DoesModeSupportPhotoModeCamera()
	{
		if (m_CurrentMode != null)
		{
			return m_CurrentMode.AllowsPhotoMode;
		}
		return false;
	}

	public bool CurrentModeCanSave()
	{
		bool result = false;
		if ((bool)m_CurrentMode)
		{
			result = m_CurrentMode.CanSave();
		}
		return result;
	}

	public void SetAutoSaveTime(float timeSec)
	{
		m_AutoSaveTimeSec = timeSec;
	}

	private float GetAutoSaveTime()
	{
		return Mathf.Max(m_AutoSaveTimeSec, QualitySettingsExtended.MinimumAutoSaveTime);
	}

	private void UpdateAutoSaveMode()
	{
		if (m_AutoSaveSecsElapsed >= GetAutoSaveTime())
		{
			if (!Singleton.Manager<ManTechSwapper>.inst.CheckOperationInProgress() && m_CurrentMode.TryAutoSave())
			{
				m_AutoSaveSecsElapsed = 0f;
			}
		}
		else
		{
			m_AutoSaveSecsElapsed += Time.deltaTime;
		}
	}

	public bool IsCurrentModeMultiplayer()
	{
		if ((bool)m_CurrentMode)
		{
			return m_CurrentMode.IsMultiplayer;
		}
		return false;
	}

	public bool SaveCurrentMode()
	{
		bool result = false;
		if ((bool)m_CurrentMode && m_CurrentMode.CanSave())
		{
			result = m_CurrentMode.SaveMode();
		}
		return result;
	}

	public bool SaveCurrentMode(string saveName)
	{
		bool result = false;
		if ((bool)m_CurrentMode && m_CurrentMode.CanSave())
		{
			result = m_CurrentMode.SaveMode(saveName);
		}
		else
		{
			d.LogWarning("ManGameMode.SaveCurrentMode - Trying to save in a mode that does not support game saves");
		}
		return result;
	}

	public string GetCurrentModeDefaultSaveName()
	{
		return m_CurrentMode.GetDefaultSaveName();
	}

	public InventoryMetaData GetReferenceInventory()
	{
		if (m_CurrentMode == null)
		{
			d.LogError("ManGameMode - m_CurrentMode is not set, returning nil inventory");
			return new InventoryMetaData(null, locked: true);
		}
		return m_CurrentMode.GetReferenceInventory();
	}

	public bool CurrentModeFloatingOriginEnabled()
	{
		bool result = false;
		if ((bool)m_CurrentMode)
		{
			result = m_CurrentMode.UsesFloatingOrigin();
		}
		return result;
	}

	public float GetCurrentModeSeaLevel()
	{
		if (!(m_CurrentMode != null))
		{
			return 0f;
		}
		return m_CurrentMode.GetSeaLevel();
	}

	public float GetCurrentModeAltitude(Vector3 worldPos)
	{
		return GetCurrentModeAltitude(worldPos.y);
	}

	public float GetCurrentModeAltitude(float worldY)
	{
		return worldY - GetCurrentModeSeaLevel();
	}

	public float GetCurrentModeAtmosphereDensityAtSeaLevel()
	{
		return GetCurrentModeAtmosphereCurve().Evaluate(GetCurrentModeSeaLevel());
	}

	public AnimationCurve GetCurrentModeAtmosphereCurve()
	{
		if (!(m_CurrentMode != null))
		{
			return null;
		}
		return m_CurrentMode.GetAtmosphereCurve();
	}

	public void RestartCurrentMode(bool reloadSave)
	{
		if (!reloadSave)
		{
			m_CurrentModeInitSettings.Remove("SaveName");
			m_CurrentModeInitSettings.Remove("SaveWorkshopPath");
		}
		NextModeSetting.SetModeInitSettings(m_CurrentModeInitSettings);
		NextModeSetting.SwitchToMode();
	}

	public bool CurrentModeDisplaysSeed()
	{
		bool result = false;
		if ((bool)m_CurrentMode)
		{
			result = m_CurrentMode.DisplaysSeed();
		}
		return result;
	}

	public bool CurrentModeOverlayShowsAllNetPlayerTechs()
	{
		bool result = false;
		if (m_CurrentMode.IsNotNull())
		{
			result = m_CurrentMode.OverlayShowsAllNetPlayerTechs();
		}
		return result;
	}

	public bool CurrentModeCanResetPosition()
	{
		bool result = false;
		if ((bool)m_CurrentMode)
		{
			result = m_CurrentMode.CanResetPosition();
		}
		return result;
	}

	public WorldGenVersionData GetCurrentModeLatestMapVersion()
	{
		if (m_CurrentMode == null)
		{
			return WorldGenVersionData.kUninitialised;
		}
		return m_CurrentMode.GetLatestMapVersion();
	}

	public void TriggerSwitch<T>() where T : Mode
	{
		TriggerSwitch(typeof(T));
	}

	public void TriggerSwitch(Type modeType)
	{
		d.Assert(m_NextMode == null, "Switching mode when we already have a next mode");
		m_NextMode = m_Modes.Single((Mode m) => m.GetType() == modeType);
		if (LockPlayerControls)
		{
			d.LogWarning("Attention!  Player Controls are still LOCKED!  Forcing an unlock now.  CurrentMode=" + GetCurrentGameType().ToString() + " NextMode=" + ((m_NextMode != null) ? m_NextMode.GetGameType().ToString() : "NULL"));
			LockPlayerControls = false;
		}
	}

	private void SwitchToNextMode()
	{
		m_LastInputTime = Time.unscaledTime;
		m_CurrentMode = m_NextMode;
		m_CurrentGameState = GameState.NotInMode;
		m_NextMode = null;
		ModeSwitchEvent.Send();
	}

	public void ResetPlayerPosition()
	{
		if ((bool)m_CurrentMode)
		{
			m_CurrentMode.ResetPlayerPosition();
		}
	}

	public bool CanPlayerChangeTech(Tank targetTech)
	{
		d.Assert(targetTech.IsNotNull(), "CanPlayerChangeTech called with null targetTech! Please check calling conditions");
		if (!(m_CurrentMode != null))
		{
			return false;
		}
		return m_CurrentMode.CanPlayerChangeTech(targetTech);
	}

	public bool CanPlayerSwapOrPlaceTech()
	{
		if (m_CurrentMode == null)
		{
			return false;
		}
		if (!CanPlayerSwapTech())
		{
			return CanPlayerPlaceTech();
		}
		return true;
	}

	public bool CanPlayerSwapTech()
	{
		if (!(m_CurrentMode != null))
		{
			return false;
		}
		return m_CurrentMode.CanPlayerSwapTech();
	}

	public bool CanPlayerPlaceTech()
	{
		if (!(m_CurrentMode != null))
		{
			return false;
		}
		return m_CurrentMode.CanPlayerPlaceTech();
	}

	public bool CheckBlockAllowed(BlockTypes blockType)
	{
		if (!(m_CurrentMode != null))
		{
			return true;
		}
		return m_CurrentMode.CheckBlockAllowed(blockType);
	}

	public void RegenerateWorld(BiomeMap biomeMap, Vector3 cameraPos, Quaternion cameraRot)
	{
		Singleton.Manager<CameraManager>.inst.ResetCamera(cameraPos, cameraRot);
		Singleton.Manager<ManWorld>.inst.Reset(biomeMap);
		Singleton.Manager<ManWorld>.inst.VendorSpawner.Enabled = false;
	}

	private static bool TechIsNonAnchoredControllable(Tank tech)
	{
		if (tech.ControllableByAnyPlayer)
		{
			return !tech.IsAnchored;
		}
		return false;
	}

	public bool AtLeastOnePlayerControllableTankExists()
	{
		if (Singleton.Manager<ManPointer>.inst.IsDraggingController)
		{
			return true;
		}
		if (Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsNonAnchoredControllable).Any())
		{
			return true;
		}
		if (Singleton.Manager<ManUndo>.inst.UndoInProgress)
		{
			return true;
		}
		return false;
	}

	public void AddModeInitSetting(string settingName, object settingData)
	{
		m_NextModeInitSettings[settingName] = settingData;
	}

	public object GetModeInitSetting(string settingName)
	{
		object value = null;
		m_NextModeInitSettings.TryGetValue(settingName, out value);
		return value;
	}

	public void ClearModeInitSetting(string settingName)
	{
		m_NextModeInitSettings.Remove(settingName);
	}

	public void ClearModeInitSettings()
	{
		m_NextModeInitSettings.Clear();
	}

	public void SetupSaveGameToLoad(GameType gameType, string saveName, WorldGenVersionData worldGenData, string saveWorkshopPath = null)
	{
		NextModeSetting.AddModeInitSetting("SaveName", saveName);
		if (worldGenData != new WorldGenVersionData(0, BiomeMap.WorldGenVersioningType.ChangleListVersionInt))
		{
			NextModeSetting.AddModeInitSetting("WorldGenVersionID", worldGenData.m_VersionID);
			NextModeSetting.AddModeInitSetting("WorldGenVersioningType", (int)worldGenData.m_VersioningType);
		}
		if (saveWorkshopPath != null)
		{
			NextModeSetting.AddModeInitSetting("SaveWorkshopPath", saveWorkshopPath);
		}
		SetupModeSwitchAction(Singleton.Manager<ManGameMode>.inst.NextModeSetting, gameType);
	}

	public void SuppressFadeIn()
	{
		m_SuppressFadeIn = true;
	}

	public void SetupModeSwitchAction(ModeSettings modeSettings, GameType modeToSet, string miscSubMode = null)
	{
		switch (modeToSet)
		{
		case GameType.MainGame:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeMain>;
			break;
		case GameType.SumoShowdown:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeSumo>;
			break;
		case GameType.Attract:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeAttract>;
			break;
		case GameType.RaD:
			modeSettings.AddModeInitSetting("ModeName", "R&D Test Chamber");
			modeSettings.m_SwitchAction = TriggerSwitch<ModeMisc>;
			break;
		case GameType.Creative:
			modeSettings.AddModeInitSetting("ModeName", "Creative");
			modeSettings.m_SwitchAction = TriggerSwitch<ModeMisc>;
			break;
		case GameType.Misc:
			if (!miscSubMode.NullOrEmpty())
			{
				modeSettings.AddModeInitSetting("ModeName", miscSubMode);
			}
			else
			{
				d.LogError("ManGameMode.SetupModeSwitchAction - Missing Mode Sub name trying to initialise Gametype Misc!");
			}
			modeSettings.m_SwitchAction = TriggerSwitch<ModeMisc>;
			break;
		case GameType.Deathmatch:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeDeathmatch>;
			break;
		case GameType.Gauntlet:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeGauntlet>;
			break;
		case GameType.CoOpCreative:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeCoOpCreative>;
			break;
		case GameType.CoOpCampaign:
			modeSettings.m_SwitchAction = TriggerSwitch<ModeCoOpCampaign>;
			break;
		default:
			d.LogError("ManGameMode.SetupModeSwitchAction - GameType " + modeToSet.ToString() + " not set up!");
			break;
		}
	}

	public void SetErrorDialogue(string bodyMsg, string acceptMsg)
	{
		m_HasErrorDialogue = true;
		m_ErrorBodyMsg = bodyMsg;
		m_ErrorAcceptMsg = acceptMsg;
	}

	private void Awake()
	{
		m_Modes = (from c in GetComponents(typeof(Mode))
			select c as Mode).ToArray();
		d.Assert(!m_StartMode || m_Modes.Contains(m_StartMode), "unknown start mode");
		NextModeSetting = new ModeSettings();
		Mode[] modes = m_Modes;
		for (int num = 0; num < modes.Length; num++)
		{
			modes[num].InitSingle();
		}
	}

	private void Start()
	{
		if ((bool)m_StartMode)
		{
			TriggerSwitch(m_StartMode.GetType());
		}
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(TankKilled);
		m_CameraCullingMask = Singleton.camera.cullingMask;
		Singleton.camera.cullingMask = 0;
	}

	public void SetCancelEventWasHandled(bool eventWasHandled)
	{
		m_CancelEventFired = true;
		m_CancelEventHandled |= eventWasHandled;
	}

	private void HandleButtonPresses()
	{
		bool buttonDown = Singleton.Manager<ManInput>.inst.GetButtonDown(24, ControllerType.Keyboard);
		if ((m_CancelEventFired || buttonDown) && !m_CancelEventHandled)
		{
			bool flag = false;
			if (Singleton.Manager<ManUI>.inst.IsStackEmpty())
			{
				flag = Singleton.Manager<ManHUD>.inst.HandleEscapeKey();
			}
			if (Singleton.Manager<ManPauseGame>.inst.AllowPause && !flag)
			{
				if (Singleton.Manager<ManPauseGame>.inst.IsPauseMenuShowing())
				{
					Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: false);
				}
				else if (Singleton.Manager<ManUI>.inst.IsScreenShowing(ManUI.ScreenType.MainMenu))
				{
					if (buttonDown)
					{
						Singleton.Manager<ManPauseGame>.inst.ShowExitConfirmScreen();
					}
				}
				else if (!Singleton.Manager<ManUI>.inst.IsStackEmpty())
				{
					if (!Singleton.Manager<ManUI>.inst.IsExitBlockedOnCurrentScreen())
					{
						Singleton.Manager<ManUI>.inst.GoBack();
					}
				}
				else if (buttonDown)
				{
					bool flag2 = false;
					if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && Singleton.Manager<ManNetwork>.inst.NetController != null && Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase >= NetController.Phase.TechSelection)
					{
						UIMPChat uIMPChat = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.MPChat) as UIMPChat;
						if ((bool)uIMPChat && uIMPChat.HasFocus())
						{
							uIMPChat.SetFocus(isFocus: false);
							flag2 = true;
						}
					}
					if (!flag2)
					{
						Singleton.Manager<ManPauseGame>.inst.PauseGame(pauseState: true);
					}
				}
			}
		}
		else if (Singleton.Manager<ManInput>.inst.GetButtonDown(43) && (Singleton.Manager<ManUI>.inst.IsStackEmpty() || Singleton.Manager<ManPauseGame>.inst.IsPauseMenuInScreenStack()))
		{
			Singleton.Manager<ManPauseGame>.inst.ToggleUniversalPauseGame();
		}
		if (Singleton.Manager<ManInput>.inst.GetButtonDown(115) && Singleton.Manager<ManUI>.inst.IsStackEmpty())
		{
			Singleton.Manager<ManPauseGame>.inst.TryTogglePhotoMode();
		}
		UIMPChat.HandleActivateChatInput();
		m_CancelEventFired = false;
		m_CancelEventHandled = false;
	}

	private static bool TechIsPlayerControllable(Tank tech)
	{
		return tech.ControllableByAnyPlayer;
	}

	private void TankKilled(Tank tank, ManDamage.DamageInfo info)
	{
		if (tank == Singleton.playerTank && !Singleton.Manager<ManPointer>.inst.IsDraggingController && !Singleton.Manager<ManTechs>.inst.IterateTechsWhere(TechIsPlayerControllable).Any())
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.ResetPosition);
		}
	}

	private void UpdateModeTimeAnalyticsEvent()
	{
	}

	private void Update()
	{
		HandleButtonPresses();
		UpdateCurrentMode();
		if (m_CurrentMode == null && m_NextMode != null)
		{
			SwitchToNextMode();
			UpdateCurrentMode();
		}
		UpdateModeTimeAnalyticsEvent();
	}

	private void GoToAttractModeIfUserIdle()
	{
		if (m_CurrentMode is ModeAttract || (bool)m_NextMode || Application.isEditor)
		{
			return;
		}
		if (Input.anyKey)
		{
			m_LastInputTime = Time.unscaledTime;
		}
		for (int i = 0; i < ReInput.players.playerCount; i++)
		{
			Player player = ReInput.players.GetPlayer(i);
			if (player != null && player.GetAnyButton())
			{
				m_LastInputTime = Time.unscaledTime;
				break;
			}
		}
		if (Time.unscaledTime - m_LastInputTime > m_ReturnToMenuInactivityTime)
		{
			Time.timeScale = 1f;
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		}
	}

	public void UpdateCurrentMode()
	{
		switch (m_CurrentGameState)
		{
		case GameState.NotInMode:
			if (m_CurrentMode != null)
			{
				EnterState(GameState.Setup);
			}
			break;
		case GameState.Setup:
			if (m_CurrentMode.UpdateModeSetup() == Mode.FunctionStatus.Done)
			{
				EnterState(GameState.PreGame);
			}
			break;
		case GameState.PreGame:
			if (m_CurrentMode.UpdatePreMode(m_CurrentModeInitSettings) == Mode.FunctionStatus.Done)
			{
				if (m_UnloadAssetsAsyncOp == null)
				{
					m_UnloadAssetsAsyncOp = Resources.UnloadUnusedAssets();
				}
				if (m_UnloadAssetsAsyncOp != null && m_UnloadAssetsAsyncOp.isDone)
				{
					m_UnloadAssetsAsyncOp = null;
					EnterState(GameState.InGame);
				}
			}
			break;
		case GameState.InGame:
			if (!(m_NextMode != null))
			{
				UpdateAutoSaveMode();
				d.Assert(m_CurrentMode.UpdateMode() != Mode.FunctionStatus.Done, "Mode state returned - illegal");
			}
			else
			{
				EnterState(GameState.FadeToBlack);
			}
			break;
		case GameState.FadeToBlack:
			if (Singleton.Manager<ManUI>.inst.FadeFinished())
			{
				EnterState(GameState.PostGame);
			}
			break;
		case GameState.PostGame:
			if (m_CurrentMode.UpdatePostMode() == Mode.FunctionStatus.Done)
			{
				EnterState(GameState.CleaningUp);
			}
			break;
		case GameState.CleaningUp:
			m_CurrentMode = null;
			EnterState(GameState.NotInMode);
			break;
		}
	}

	private void EnterState(GameState entryState)
	{
		m_CurrentGameState = entryState;
		if (entryState != GameState.NotInMode)
		{
			d.Log("Mode " + m_CurrentMode.ToString() + " switching to state " + entryState);
		}
		switch (entryState)
		{
		case GameState.Setup:
			m_HasErrorDialogue = false;
			if (!EnterSetupState())
			{
				EnterState(GameState.FadeToBlack);
				HandleFailSetup(m_CurrentMode);
			}
			break;
		case GameState.PreGame:
			m_CurrentMode.EnterPreMode(m_CurrentModeInitSettings);
			break;
		case GameState.InGame:
			Singleton.camera.cullingMask = m_CameraCullingMask;
			EnterInGameState();
			ManNintendoSwitch.SetFastLoad(fastLoad: false, SwitchFastLoadChannel.GameMode);
			break;
		case GameState.FadeToBlack:
			Singleton.Manager<ManUI>.inst.FadeToBlack();
			break;
		case GameState.PostGame:
			m_CameraCullingMask = Singleton.camera.cullingMask;
			Singleton.camera.cullingMask = 0;
			ManNintendoSwitch.SetFastLoad(fastLoad: true, SwitchFastLoadChannel.GameMode);
			Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.NotInGame, show: false);
			m_CurrentMode.EnterPostMode();
			ModeFinishedEvent.Send(m_CurrentMode);
			break;
		case GameState.CleaningUp:
			EnterCleanupState();
			break;
		case GameState.NotInMode:
			break;
		}
	}

	private bool EnterSetupState()
	{
		HandleEnterSetupState();
		bool result = false;
		if (m_CurrentMode.ModePreInit(m_CurrentModeInitSettings))
		{
			Singleton.Manager<ManSaveGame>.inst.Clear();
			ModeSetupEvent.Send(m_CurrentMode);
			result = m_CurrentMode.EnterModeSetup(m_CurrentModeInitSettings);
		}
		return result;
	}

	private void EnterSetupStateAsync(Action<bool> callback)
	{
		HandleEnterSetupState();
		if (m_CurrentMode.ModePreInit(m_CurrentModeInitSettings))
		{
			Singleton.Manager<ManSaveGame>.inst.Clear();
			ModeSetupEvent.Send(m_CurrentMode);
			m_CurrentMode.EnterModeSetupAsync(m_CurrentModeInitSettings, callback);
		}
	}

	private void HandleEnterSetupState()
	{
		m_CurrentModeInitSettings = new Mode.InitSettings(m_NextModeInitSettings);
		ClearModeInitSettings();
		Singleton.Manager<ManSpawn>.inst.BlockLimit = BlockManager.DefaultBlockLimit;
		if (m_CurrentModeInitSettings.TryGetValue("WorldSeed", out var value))
		{
			Singleton.Manager<ManWorld>.inst.SeedString = value as string;
		}
		if (m_CurrentModeInitSettings.TryGetValue("WorldBiome", out value))
		{
			Singleton.Manager<ManWorld>.inst.BiomeChoice = value as string;
		}
	}

	private void HandleFailSetup(Mode failedMode)
	{
		string text = ManSaveGame.SaveObjectToRawJson(m_CurrentModeInitSettings);
		d.LogError(string.Concat("ERROR - Failed to initialise Mode '", failedMode.GetType(), "' with InitSettings (", text, ") \n Exiting to AttractMode!"));
		if (m_HasErrorDialogue)
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			uIScreenNotifications.Set(m_ErrorBodyMsg, HandleAcceptSetupFailure, m_ErrorAcceptMsg);
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications, ManUI.PauseType.Pause);
			m_HasErrorDialogue = false;
		}
		else
		{
			HandleAcceptSetupFailure();
		}
	}

	private void HandleAcceptSetupFailure()
	{
		Singleton.Manager<ManUI>.inst.ExitAllScreens();
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
	}

	private void EnterInGameState()
	{
		Singleton.Manager<CameraManager>.inst.RenderReflectionProbe();
		if (!m_CurrentMode.EnteredThisSession && m_CurrentMode.GetGameType() != GameType.Attract)
		{
			m_CurrentMode.EnteredThisSession = true;
			new Dictionary<string, object>
			{
				{
					"mode_start",
					m_CurrentMode.GetGameType().ToString()
				},
				{
					"submode",
					m_CurrentMode.GetGameSubmode()
				},
				{
					"mods",
					Singleton.Manager<ManMods>.inst.GetModsInCurrentSession()
				}
			};
		}
		if (!m_SuppressFadeIn)
		{
			Singleton.Manager<ManUI>.inst.ClearFade(3f);
		}
		m_SuppressFadeIn = false;
		Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.NotInGame, show: true);
		m_CurrentMode.EnterModeUpdate();
		m_AutoSaveSecsElapsed = 0f;
		ModeStartEvent.Send(m_CurrentMode);
	}

	private void EnterCleanupState()
	{
		Time.timeScale = 1f;
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur, enabled: false);
		Singleton.Manager<FTUE>.inst.Stop();
		Singleton.Manager<ManOnScreenMessages>.inst.ClearAllMessages();
		Singleton.Manager<ManOverlay>.inst.ClearAll();
		Singleton.Manager<ManPurchases>.inst.Clear();
		Singleton.Manager<ManPointer>.inst.ResetPickupRange();
		Singleton.Manager<ManSaveGame>.inst.Clear(createInvalidState: true);
		ModeCleanUpEvent.Send(m_CurrentMode);
	}
}
