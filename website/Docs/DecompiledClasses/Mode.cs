#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Rewired;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public abstract class Mode : MonoBehaviour
{
	public enum FunctionStatus
	{
		Done,
		Running
	}

	public class InitSettings : Dictionary<string, object>
	{
		public InitSettings()
		{
		}

		public InitSettings(InitSettings otherSettings)
			: base((IDictionary<string, object>)otherSettings)
		{
		}
	}

	public interface IManagerModeEvents
	{
		void ModeStart(ManSaveGame.State optionalLoadState);

		void Save(ManSaveGame.State saveState);

		void ModeExit();
	}

	[SerializeField]
	private bool m_IsMultiplayer;

	[SerializeField]
	private CameraManager.Camera.Types m_DefaultCameraMode = CameraManager.Camera.Types.TankCamera;

	[SerializeField]
	protected bool m_AllowsPhotoMode;

	protected Event<ManSaveGame.State> ModeStartEvent;

	protected Event<ManSaveGame.State> SaveEvent;

	protected EventNoParams ModeExitEvent;

	private int m_ModeRunningTimeRunningWhole;

	private float m_ModeRunningTimeRunningFraction;

	private float m_ModeRunningTimeTotal;

	private bool m_IsLoadedFromSave;

	private bool m_IsLoadedFromWorkshopSave;

	private bool m_ModeEntrySuccess;

	private bool m_CanAutoSave;

	[FormerlySerializedAs("m_AutoSaveEnabled")]
	[SerializeField]
	protected bool m_SupportsAutoSave;

	public float ModeRunningTime => m_ModeRunningTimeTotal;

	public Vector3 StartPositionScene { get; protected set; }

	public float SpawnAreaClearRadius { get; protected set; }

	public bool EnteredThisSession { get; set; }

	public virtual bool AllowsPhotoMode => m_AllowsPhotoMode;

	public bool IsMultiplayer => m_IsMultiplayer;

	protected bool TechManagerShowsOnlyPlayerTeam { get; set; } = true;

	public abstract void InitSingle();

	public abstract ManGameMode.GameType GetGameType();

	public abstract string GetGameMode();

	public abstract string GetGameSubmode();

	public void SubscribeToEvents(IManagerModeEvents manager)
	{
		ModeStartEvent.Subscribe(manager.ModeStart);
		SaveEvent.Subscribe(manager.Save);
		ModeExitEvent.Subscribe(manager.ModeExit);
	}

	public void UnsubscribeFromEvents(IManagerModeEvents manager)
	{
		ModeStartEvent.Unsubscribe(manager.ModeStart);
		SaveEvent.Unsubscribe(manager.Save);
		ModeExitEvent.Unsubscribe(manager.ModeExit);
	}

	public bool IsLoadedFromWorkshopSave()
	{
		return m_IsLoadedFromWorkshopSave;
	}

	public void UpdateMissionScoreboardButtonPress(bool showTechManager = true)
	{
		if ((!Singleton.Manager<ManInput>.inst.GetButtonDown(46) && !Singleton.Manager<ManInput>.inst.GetButtonDown(29, ControllerType.Joystick)) || (ManNetwork.IsNetworked && (!(Singleton.Manager<ManNetwork>.inst.NetController != null) || Singleton.Manager<ManNetwork>.inst.NetController.CurrentPhase != NetController.Phase.Playing)))
		{
			return;
		}
		ManHUD.HUDElementType? hUDElementType = null;
		if (Singleton.Manager<ManInput>.inst.IsCurrentlyUsingGamepad())
		{
			hUDElementType = ManHUD.HUDElementType.GamepadQuickMenu;
		}
		else if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch)
		{
			hUDElementType = ManHUD.HUDElementType.ScoreBoard;
		}
		else if (Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCreative || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Creative || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.CoOpCampaign || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.MainGame || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Misc || Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD)
		{
			hUDElementType = ManHUD.HUDElementType.TechManager;
		}
		if (hUDElementType.HasValue)
		{
			if (Singleton.Manager<ManHUD>.inst.IsHudElementVisible(hUDElementType.Value))
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(hUDElementType.Value);
			}
			else if (hUDElementType == ManHUD.HUDElementType.GamepadQuickMenu && !Singleton.Manager<ManHUD>.inst.QuickMenuDisabled)
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(hUDElementType.Value);
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(hUDElementType.Value);
			}
		}
	}

	public bool EnterModeSetup(InitSettings initSettings)
	{
		SetModeRunningTime(0f);
		SetupModeLoadSaveListeners();
		m_IsLoadedFromSave = false;
		m_IsLoadedFromWorkshopSave = false;
		m_ModeEntrySuccess = true;
		ManSaveGame.State optionalLoadState = null;
		if (initSettings.TryGetValue("ProfileSwitch", out var value))
		{
			string saveName = value as string;
			int userIndexFromSaveName = Singleton.Manager<ManProfile>.inst.GetUserIndexFromSaveName(saveName);
			if (userIndexFromSaveName >= 0)
			{
				Singleton.Manager<ManProfile>.inst.SetCurrentUser(userIndexFromSaveName);
			}
		}
		if (initSettings.TryGetValue("SaveName", out var value2))
		{
			string saveWorkshopPath = null;
			if (initSettings.TryGetValue("SaveWorkshopPath", out var value3))
			{
				saveWorkshopPath = value3 as string;
				m_IsLoadedFromWorkshopSave = true;
			}
			string saveName2 = value2 as string;
			m_ModeEntrySuccess = LoadMode(saveName2, saveWorkshopPath);
			m_IsLoadedFromSave = m_ModeEntrySuccess;
			if (m_IsLoadedFromSave)
			{
				optionalLoadState = Singleton.Manager<ManSaveGame>.inst.CurrentState;
			}
		}
		UITechManagerHUD.ShowOnlyPlayerTeam = TechManagerShowsOnlyPlayerTeam;
		return FinishModeSetup(initSettings, optionalLoadState);
	}

	private bool FinishModeSetup(InitSettings initSettings, ManSaveGame.State optionalLoadState)
	{
		bool result = true;
		if (m_ModeEntrySuccess)
		{
			ModeStartEvent.Send(optionalLoadState);
			Singleton.Manager<ManPauseGame>.inst.LockPause(lockIt: true, ManPauseGame.DisablePauseReason.LoadingLevel);
			Singleton.Manager<ManPauseGame>.inst.PushPauseMenu(ManUI.ScreenType.Pause, pauseGameplay: true, ConfigureExitConfirmMenu);
			EnterGenerateTerrain(initSettings);
		}
		else
		{
			string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.NewMenuMain, 12);
			string localisedString2 = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.Social, 4);
			Singleton.Manager<ManGameMode>.inst.SetErrorDialogue(localisedString, localisedString2);
			result = false;
		}
		return result;
	}

	public void EnterModeSetupAsync(InitSettings initSettings, Action<bool> callback)
	{
		SetModeRunningTime(0f);
		SetupModeLoadSaveListeners();
		m_IsLoadedFromSave = false;
		m_IsLoadedFromWorkshopSave = false;
		m_ModeEntrySuccess = true;
		ManSaveGame.State optionalLoadState = null;
		UITechManagerHUD.ShowOnlyPlayerTeam = TechManagerShowsOnlyPlayerTeam;
		bool success = true;
		if (initSettings.TryGetValue("SaveName", out var value))
		{
			string text = value as string;
			if (!text.NullOrEmpty())
			{
				LoadModeAsync(text, delegate(bool modeEntrySuccess)
				{
					m_ModeEntrySuccess = modeEntrySuccess;
					m_IsLoadedFromSave = m_ModeEntrySuccess;
					if (m_IsLoadedFromSave)
					{
						optionalLoadState = Singleton.Manager<ManSaveGame>.inst.CurrentState;
					}
					success = FinishModeSetup(initSettings, optionalLoadState);
					callback(success);
				});
			}
			else
			{
				d.Log("saveNameObj was NullOrEmpty despite being found in initSettings\n");
				success = FinishModeSetup(initSettings, optionalLoadState);
				callback(success);
			}
		}
		else
		{
			success = FinishModeSetup(initSettings, optionalLoadState);
			callback(success);
		}
	}

	public FunctionStatus UpdateModeSetup()
	{
		FunctionStatus result = FunctionStatus.Running;
		if (Singleton.Manager<ManPresetFilter>.inst.IsSettingUp || Singleton.Manager<ManPop>.inst.IsSettingUp || Singleton.Manager<ManInvasion>.inst.IsSettingUp || Singleton.Manager<ManHUD>.inst.IsSettingUp || Singleton.Manager<ManMods>.inst.IsSettingUp)
		{
			return FunctionStatus.Running;
		}
		if ((bool)Singleton.Manager<ManWorld>.inst.CurrentBiomeMap && (!Singleton.Manager<ManWorld>.inst.TileManager.IsGenerating || IsModeSetupEarlyExit()))
		{
			result = FunctionStatus.Done;
		}
		return result;
	}

	public void EnterDefaultCameraMode(bool forceSwitch = true)
	{
		switch (m_DefaultCameraMode)
		{
		case CameraManager.Camera.Types.FirstPersonFlyCam:
			Singleton.Manager<CameraManager>.inst.Switch<FirstPersonFlyCam>(forceSwitch);
			break;
		case CameraManager.Camera.Types.FramingCamera:
			Singleton.Manager<CameraManager>.inst.Switch<FramingCamera>(forceSwitch);
			break;
		case CameraManager.Camera.Types.TankCamera:
			Singleton.Manager<CameraManager>.inst.Switch<TankCamera>(forceSwitch);
			break;
		case CameraManager.Camera.Types.DebugCamera:
			Singleton.Manager<CameraManager>.inst.Switch<DebugCamera>(forceSwitch);
			break;
		case CameraManager.Camera.Types.PlayerFreeCamera:
			Singleton.Manager<CameraManager>.inst.Switch<PlayerFreeCamera>(forceSwitch);
			break;
		}
	}

	public void EnterPreMode(InitSettings initSettings)
	{
		EnterPreModeImpl(initSettings);
	}

	public FunctionStatus UpdatePreMode(InitSettings initSettings)
	{
		return UpdatePreModeImpl(initSettings);
	}

	public void EnterModeUpdate()
	{
		m_CanAutoSave = true;
		Singleton.Manager<ManPauseGame>.inst.LockPause(lockIt: false, ManPauseGame.DisablePauseReason.LoadingLevel);
		if (m_ModeEntrySuccess && !m_IsLoadedFromSave && SpawnAreaClearRadius > 0f)
		{
			Vector3 scenePos = Singleton.Manager<ManWorld>.inst.ProjectToGround(StartPositionScene);
			ManSpawn.SceneryRemovalFlags sceneryRemovalSettings = ManSpawn.SceneryRemovalFlags.SpawnNoChunks | ManSpawn.SceneryRemovalFlags.PreventRegrow | ManSpawn.SceneryRemovalFlags.RemoveInstant | ManSpawn.SceneryRemovalFlags.RemovePersistentDamageStage;
			ManSpawn.RemoveAllSceneryAroundPosition(scenePos, SpawnAreaClearRadius, sceneryRemovalSettings);
		}
		EnterModeUpdateImpl();
	}

	public FunctionStatus UpdateMode()
	{
		FunctionStatus result = UpdateModeImpl();
		m_ModeRunningTimeRunningFraction += Time.deltaTime;
		if (m_ModeRunningTimeRunningFraction >= 1f)
		{
			m_ModeRunningTimeRunningFraction -= 1f;
			m_ModeRunningTimeRunningWhole++;
		}
		m_ModeRunningTimeTotal = (float)m_ModeRunningTimeRunningWhole + m_ModeRunningTimeRunningFraction;
		return result;
	}

	public void EnterPostMode()
	{
		ExitModeImpl();
		ModeExitEvent.Send();
		Singleton.Manager<ManPauseGame>.inst.PopPauseMenu();
		Singleton.Manager<ManWorld>.inst.Reset(null);
		CleanupModeLoadSaveListeners();
	}

	public FunctionStatus UpdatePostMode()
	{
		FunctionStatus result = FunctionStatus.Running;
		if (Singleton.Manager<ManWorld>.inst.TileManager.IsCleared)
		{
			ModeStartEvent.Clear();
			SaveEvent.Clear();
			ModeExitEvent.Clear();
			m_IsLoadedFromSave = false;
			m_IsLoadedFromWorkshopSave = false;
			result = FunctionStatus.Done;
		}
		return result;
	}

	public virtual ManHUD.HUDType GetDefaultHUDType()
	{
		return ManHUD.HUDType.None;
	}

	public abstract WorldGenVersionData GetLatestMapVersion();

	public virtual void ResetPlayerPosition()
	{
	}

	public virtual bool CanPlayerChangeTech(Tank targetTech)
	{
		return true;
	}

	public virtual bool CanPlayerSwapTech()
	{
		return true;
	}

	public virtual bool CanPlayerPlaceTech()
	{
		return false;
	}

	public virtual bool CheckBlockAllowed(BlockTypes blockType)
	{
		return true;
	}

	public virtual InventoryMetaData GetReferenceInventory()
	{
		return new InventoryMetaData(null, locked: true);
	}

	public virtual float GetSeaLevel()
	{
		return -50f;
	}

	public virtual AnimationCurve GetAtmosphereCurve()
	{
		return Singleton.Manager<ManWorld>.inst.UniversalAtmosphereDensityCurve;
	}

	public virtual bool DisplaysSeed()
	{
		return false;
	}

	public virtual bool OverlayShowsAllNetPlayerTechs()
	{
		return false;
	}

	public virtual bool CanResetPosition()
	{
		return false;
	}

	public virtual float GetSceneryRegrowTime()
	{
		return Globals.inst.m_DefaultSceneryRegrowTime;
	}

	public virtual float GetAutoExpireDelay(ObjectTypes type)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			if (Singleton.Manager<ManNetwork>.inst.IsServer)
			{
				return 10f;
			}
			return -1f;
		}
		switch (type)
		{
		case ObjectTypes.Block:
			return Globals.inst.autoExpireTimeoutBlocks;
		case ObjectTypes.Chunk:
			return Globals.inst.autoExpireTimeoutChunks;
		case ObjectTypes.Crate:
			return Globals.inst.autoExpireTimeoutCrates;
		default:
			d.Assert(condition: false, "GetAutoExpireDelay() - no expire delay defined for type " + type);
			return 0f;
		}
	}

	public virtual bool AutoExpireIfOffScreen()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return false;
		}
		return true;
	}

	public virtual float GetAutoExpireRange()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return 0f;
		}
		return Globals.inst.autoExpireRangeCameraFacing;
	}

	public virtual float GetSafeAreaRadius()
	{
		if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			return 30f;
		}
		return Singleton.Manager<ManFreeSpace>.inst.DefaultSafeAreaRadius;
	}

	public virtual bool FriendlyFireEnabled()
	{
		return false;
	}

	public void SetModeRunningTime(float time)
	{
		m_ModeRunningTimeTotal = time;
		m_ModeRunningTimeRunningWhole = Mathf.FloorToInt(m_ModeRunningTimeTotal);
		m_ModeRunningTimeRunningFraction = m_ModeRunningTimeTotal - (float)m_ModeRunningTimeRunningWhole;
	}

	public virtual bool UsesFloatingOrigin()
	{
		return false;
	}

	public virtual bool HasSaveGameSupport()
	{
		return false;
	}

	public virtual bool CanSave()
	{
		return false;
	}

	public string GetDefaultSaveName()
	{
		ManGameMode.GameType gameType = GetGameType();
		string defaultSaveName = ManSaveGame.GetDefaultSaveName(gameType);
		return ManSaveGame.GetNextAvailableSaveName(gameType, defaultSaveName);
	}

	public bool SaveMode()
	{
		string defaultSaveName = GetDefaultSaveName();
		return SaveMode(defaultSaveName);
	}

	public bool SaveMode(string saveName, bool async = false)
	{
		bool flag = false;
		if (CanSave())
		{
			ManGameMode.GameType gameType = GetGameType();
			ManSaveGame.State currentState = Singleton.Manager<ManSaveGame>.inst.CurrentState;
			currentState.m_WorldSeed = Singleton.Manager<ManWorld>.inst.SeedString;
			currentState.m_BiomeChoice = Singleton.Manager<ManWorld>.inst.BiomeChoice;
			currentState.m_RunningTime = m_ModeRunningTimeTotal;
			currentState.m_CameraPos = new ManSaveGame.CameraPosition
			{
				m_WorldPosition = WorldPosition.FromScenePosition(Singleton.cameraTrans.position),
				m_Forward = Singleton.cameraTrans.forward
			};
			Singleton.Manager<ManWorld>.inst.TileManager.StoreAllLoadedTileData();
			SaveEvent.Send(currentState);
			Save(currentState);
			flag = Singleton.Manager<ManSaveGame>.inst.Save(gameType, saveName, async);
			ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
			if (!Singleton.Manager<ManGameMode>.inst.IsCurrentModeMultiplayer())
			{
				if (currentUser.m_LastUsedSaveType != gameType || currentUser.m_LastUsedSaveName != saveName)
				{
					currentUser.UpdateCumulativePlaytime();
					currentUser.m_LastUsedSaveType = gameType;
					currentUser.m_LastUsedSaveName = saveName;
					currentUser.m_LastUsedSave_WorldGenVersionData = currentState.WorldGenVersionData;
					bool flag2 = Singleton.Manager<ManProfile>.inst.Save();
					flag = flag && flag2;
				}
			}
			else
			{
				currentUser.UpdateCumulativePlaytime();
				bool flag2 = Singleton.Manager<ManProfile>.inst.Save();
				flag = flag && flag2;
			}
		}
		return flag;
	}

	private bool LoadMode(string saveName, string saveWorkshopPath = null)
	{
		bool flag = Singleton.Manager<ManSaveGame>.inst.Load(GetGameType(), saveName, saveWorkshopPath);
		if (flag)
		{
			ManSaveGame.State currentState = Singleton.Manager<ManSaveGame>.inst.CurrentState;
			Singleton.Manager<ManWorld>.inst.SeedString = currentState.m_WorldSeed;
			Singleton.Manager<ManWorld>.inst.BiomeChoice = currentState.m_BiomeChoice;
			SetModeRunningTime(currentState.m_RunningTime);
			Singleton.cameraTrans.position = currentState.m_CameraPos.GetBackwardsCompatiblePosition();
			Singleton.cameraTrans.forward = currentState.m_CameraPos.m_Forward;
			try
			{
				Load(currentState);
			}
			catch (Exception ex)
			{
				d.LogError("Error loading Save game '" + saveName + "' - " + ex);
				flag = false;
			}
		}
		return flag;
	}

	private void LoadModeAsync(string saveName, Action<bool> callback)
	{
		d.Log("[Mode.LoadModeAsync] saveName: " + saveName);
		Singleton.Manager<ManSaveGame>.inst.LoadAsync(GetGameType(), saveName, delegate(bool loadSuccess)
		{
			if (loadSuccess)
			{
				ManSaveGame.State currentState = Singleton.Manager<ManSaveGame>.inst.CurrentState;
				Singleton.Manager<ManWorld>.inst.SeedString = currentState.m_WorldSeed;
				Singleton.Manager<ManWorld>.inst.BiomeChoice = currentState.m_BiomeChoice;
				SetModeRunningTime(currentState.m_RunningTime);
				Singleton.cameraTrans.position = currentState.m_CameraPos.GetBackwardsCompatiblePosition();
				Singleton.cameraTrans.forward = currentState.m_CameraPos.m_Forward;
				try
				{
					Load(currentState);
				}
				catch (Exception ex)
				{
					d.LogError("Error loading Save game '" + saveName + "' - " + ex);
					loadSuccess = false;
				}
			}
			callback(loadSuccess);
		});
	}

	public bool TryAutoSave()
	{
		bool result = false;
		if (m_CanAutoSave && CanSave() && IsAutoSaveEnabled())
		{
			bool num = SaveMode(ManSaveGame.AutoSaveName, async: true);
			result = true;
			if (!num)
			{
				d.LogError("Mode.UpdateAutoSave - unable to update autosave");
			}
		}
		return result;
	}

	public void SetCanAutoSave(bool canAutoSave)
	{
		m_CanAutoSave = canAutoSave;
	}

	protected virtual void ConfigureExitConfirmMenu(UIScreenNotifications exitScreen)
	{
		string localisedString = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 26);
		UIButtonData accept = new UIButtonData
		{
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_Callback = delegate
			{
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
		exitScreen.Set(localisedString, accept, decline);
	}

	protected abstract void EnterGenerateTerrain(InitSettings initSettings);

	protected virtual void EnterPreModeImpl(InitSettings initSettings)
	{
	}

	protected virtual FunctionStatus UpdatePreModeImpl(InitSettings initSettings)
	{
		return FunctionStatus.Done;
	}

	protected virtual void EnterModeUpdateImpl()
	{
	}

	protected virtual FunctionStatus UpdateModeImpl()
	{
		return FunctionStatus.Running;
	}

	protected abstract void ExitModeImpl();

	protected bool IsLoadedFromSaveGame()
	{
		return m_IsLoadedFromSave;
	}

	protected virtual void Save(ManSaveGame.State saveState)
	{
	}

	protected virtual void Load(ManSaveGame.State saveState)
	{
	}

	protected virtual bool IsAutoSaveEnabled()
	{
		return m_SupportsAutoSave;
	}

	public virtual bool ModePreInit(InitSettings initSettings)
	{
		return true;
	}

	protected virtual void SetupModeLoadSaveListeners()
	{
		SubscribeToEvents(Singleton.Manager<ManVisible>.inst);
		SubscribeToEvents(Singleton.Manager<ManTechs>.inst);
		SubscribeToEvents(Singleton.Manager<ManTireTracks>.inst);
		SubscribeToEvents(Singleton.Manager<ManMods>.inst);
		SubscribeToEvents(Singleton.Manager<ManSpawn>.inst);
	}

	protected virtual void CleanupModeLoadSaveListeners()
	{
		UnsubscribeFromEvents(Singleton.Manager<ManVisible>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTechs>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManTireTracks>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManMods>.inst);
		UnsubscribeFromEvents(Singleton.Manager<ManSpawn>.inst);
	}

	protected virtual bool IsModeSetupEarlyExit()
	{
		return false;
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}
}
public abstract class Mode<T> : Mode where T : class
{
	private static T _instance;

	public static T inst
	{
		get
		{
			d.Assert(_instance != null);
			return _instance;
		}
	}

	public override void InitSingle()
	{
		_instance = this as T;
	}

	protected TSetting LoadSetting<TSetting>(InitSettings initSettings, string settingName, TSetting defaultValue)
	{
		TSetting result = defaultValue;
		if (initSettings.TryGetValue(settingName, out var value))
		{
			result = (TSetting)value;
		}
		return result;
	}

	protected bool TryLoadSetting<TSetting>(InitSettings initSettings, string settingName, out TSetting outValue)
	{
		object value;
		bool flag = initSettings.TryGetValue(settingName, out value);
		outValue = (flag ? ((TSetting)value) : default(TSetting));
		return flag;
	}

	protected bool HasSetting(InitSettings initSettings, string settingName)
	{
		return initSettings.ContainsKey(settingName);
	}
}
