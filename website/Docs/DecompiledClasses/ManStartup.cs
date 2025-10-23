#define UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TerraTech.Network;
using UnityEngine;

public class ManStartup : Singleton.Manager<ManStartup>
{
	[Serializable]
	public struct ModeInitSetting
	{
		public string key;

		public string value;
	}

	[SerializeField]
	private float m_MaxWorkTimePerFrameStatic;

	[SerializeField]
	private float m_MaxWorkTimePerFrameMoving;

	private float yieldTestFrameTime;

	private int yieldTestFrameNumber;

	private bool m_ComponentPoolInitialised;

	private float m_InitScreenSize = 1f;

	public bool GameStarted { get; set; }

	public bool ScreenStatic { get; set; }

	public bool FastStart { get; set; }

	private event Action m_ComponentPoolInitializedEvent;

	public void DoOnceAfterComponentPoolInitialised(Action action)
	{
		if (m_ComponentPoolInitialised)
		{
			action();
		}
		else
		{
			m_ComponentPoolInitializedEvent += action;
		}
	}

	private IEnumerator EnterGame()
	{
		bool modeSwitched = false;
		Action<Mode> modeSwitchHandler = delegate
		{
			modeSwitched = true;
		};
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(modeSwitchHandler);
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch<ModeAttract>();
		while (!modeSwitched)
		{
			yield return null;
		}
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Unsubscribe(modeSwitchHandler);
	}

	private IEnumerator EnterMode(Type modeType, List<ModeInitSetting> modeSettings)
	{
		Singleton.Manager<ManGameMode>.inst.TriggerSwitch(modeType);
		foreach (ModeInitSetting modeSetting in modeSettings)
		{
			Singleton.Manager<ManGameMode>.inst.AddModeInitSetting(modeSetting.key, modeSetting.value);
		}
		yield break;
	}

	private IEnumerator EnsureUserProfile()
	{
		d.Assert(Application.isEditor, "Only expected to use this in the Editor, outside of the regular startup flow!");
		if (Singleton.Manager<ManProfile>.inst.GetCurrentUser() == null)
		{
			ManProfile.Profile profile = new ManProfile.Profile("NewUser", "NewUser");
			Singleton.Manager<ManProfile>.inst.AddUser(profile, setCurrent: true);
		}
		yield break;
	}

	private IEnumerator WaitForFinalSplash()
	{
		while (!Singleton.Manager<ManSplashScreen>.inst.FinalSplashState(ManSplashScreen.Splash.State.FadeOut))
		{
			yield return null;
		}
	}

	private IEnumerator WaitForSplashScreensDone()
	{
		Singleton.Manager<ManSplashScreen>.inst.CanExit = true;
		while (!Singleton.Manager<ManSplashScreen>.inst.HasExited)
		{
			yield return null;
		}
		Singleton.Manager<ManUI>.inst.FadeToColour(Color.black, 100f, forceFront: true);
	}

	private IEnumerator ForceFadeOnAndToFront()
	{
		Singleton.Manager<ManUI>.inst.SetFadeLayer(atFront: true);
		Singleton.Manager<ManGameMode>.inst.SuppressFadeIn();
		yield break;
	}

	private IEnumerator TriggerFadeIn()
	{
		float initScreenSize = m_InitScreenSize;
		Singleton.Manager<CameraManager>.inst.ScreenSize = initScreenSize;
		Singleton.Manager<ManUI>.inst.ClearFade(3f);
		Singleton.Manager<ManPauseGame>.inst.LockPause(lockIt: false, ManPauseGame.DisablePauseReason.Startup);
		yield break;
	}

	private IEnumerator SetupUI()
	{
		Singleton.Manager<ManSplashScreen>.inst.SetUICamera(Singleton.Manager<ManUI>.inst.m_UICamera);
		Singleton.Manager<ManUI>.inst.ClearFade(100f);
		yield break;
	}

	private IEnumerator EnableSwitchFastLoad(bool enable)
	{
		ManNintendoSwitch.SetFastLoad(enable, SwitchFastLoadChannel.Startup);
		yield break;
	}

	private IEnumerator SetCursorVisible()
	{
		Cursor.visible = true;
		yield break;
	}

	private IEnumerator LoadUpdateNotifier()
	{
		GetComponent<UpdateNotifier>().StartLoading();
		yield break;
	}

	private IEnumerator ReenableFog()
	{
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.Fog, enabled: true);
		yield break;
	}

	private bool ShouldYield()
	{
		if (Time.frameCount != yieldTestFrameNumber)
		{
			yieldTestFrameNumber = Time.frameCount;
			yieldTestFrameTime = Time.realtimeSinceStartup;
			return false;
		}
		float num = (ScreenStatic ? m_MaxWorkTimePerFrameStatic : m_MaxWorkTimePerFrameMoving);
		return Time.realtimeSinceStartup - yieldTestFrameTime > num;
	}

	private void InitializeGraphicsSettings(ManProfile.GraphicsSettings settings)
	{
		if (settings == null)
		{
			return;
		}
		if (true)
		{
			QualitySettings.SetQualityLevel(settings.m_QualityLevel, applyExpensiveChanges: true);
			QualitySettingsExtended.QualitySettingChangedEvent.Send();
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AO, settings.m_HBAO);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, settings.m_DOF);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AA, settings.m_AA);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.HDR, settings.m_HDR);
			Singleton.Manager<CameraManager>.inst.SetDrawDist01(settings.m_DrawDist);
			Singleton.Manager<CameraManager>.inst.SetDetailDist01(settings.m_DetailDist);
			Singleton.Manager<CameraManager>.inst.SetShadowDist01(settings.m_ShadowDist);
			if (settings.m_MaxFramerate == int.MinValue)
			{
				settings.m_VSyncEnabled = QualitySettings.vSyncCount > 0;
				settings.m_MaxFramerate = (settings.m_VSyncEnabled ? (-1) : 60);
			}
			Singleton.Manager<CameraManager>.inst.SetVSyncAndFramerateLimit(settings.m_VSyncEnabled, settings.m_MaxFramerate);
		}
		else
		{
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AO, QualitySettingsExtended.DefaultHBAOEnabled);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, QualitySettingsExtended.DefaultDOFEnabled);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AA, QualitySettingsExtended.DefaultAntialiasingEnabled);
			Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.HDR, QualitySettingsExtended.DefaultHDREnabled);
			Singleton.Manager<CameraManager>.inst.SetDrawDist01(QualitySettingsExtended.ViewDistanceRange.InverseLerp(QualitySettingsExtended.DefaultDrawDistance));
			Singleton.Manager<CameraManager>.inst.SetDetailDist01(QualitySettingsExtended.DetailDistanceRange.InverseLerp(QualitySettingsExtended.DefaultDetailDistance));
			Singleton.Manager<CameraManager>.inst.SetShadowDist01(QualitySettingsExtended.ShadowDistanceRange.InverseLerp(QualitySettingsExtended.DefaultShadowDistance));
		}
		if ((bool)Singleton.Manager<CameraManager>.inst.GammaCorrection)
		{
			Singleton.Manager<CameraManager>.inst.GammaCorrection.gamma = settings.m_Gamma;
		}
		GameCursor.SetCursorSize(settings.m_MousePointerSize);
		m_InitScreenSize = settings.m_ScreenSize;
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.Fog, enabled: false);
		Singleton.Manager<CameraManager>.inst.DamageFeedbackEffect.Enabled = settings.m_DamageFeedbackEffect;
		new Dictionary<string, object>
		{
			{ "ambient_occlusion", settings.m_HBAO },
			{ "depth_of_field", settings.m_DOF },
			{ "anti_aliasing", settings.m_AA },
			{ "high_dynamic_range", settings.m_HDR },
			{
				"gamma",
				Math.Round(settings.m_Gamma).ToString()
			},
			{
				"draw_distance",
				Math.Round(settings.m_DrawDist, 1).ToString()
			},
			{ "damage_feedback_fx", settings.m_DamageFeedbackEffect }
		};
	}

	private void InitializeSoundSettings(ManProfile.SoundSettings settings)
	{
		if (settings != null)
		{
			Singleton.Manager<ManSFX>.inst.SFXVolume = settings.m_SFXVolume;
			Singleton.Manager<ManMusic>.inst.SetMasterMixerVolume(settings.m_MasterVolume);
			Singleton.Manager<ManMusic>.inst.SetSFXMixerVolume(settings.m_SFXVolume);
			Singleton.Manager<ManMusic>.inst.SetMusicMixerVolume(settings.m_MusicVolume);
			new Dictionary<string, object>
			{
				{ "sfx_volume", settings.m_SFXVolume },
				{ "music_volume", settings.m_MusicVolume },
				{ "master_volume", settings.m_MasterVolume }
			};
		}
	}

	private void InitializeControlsSettings(ManProfile.ControllerSettings settings)
	{
		if (settings != null)
		{
			Globals.inst.m_DisableControllers = settings.m_DisableControllers;
			Globals.inst.reversingSteerInversion = settings.m_ReverseInverseSteering;
			if (!settings.m_ControllerMapping.NullOrEmpty())
			{
				Singleton.Manager<ManInput>.inst.LoadControllerMaps(settings.m_ControllerMapping);
			}
			Singleton.Manager<ManInput>.inst.SetInitialControllerMaps();
			Singleton.Manager<ManInput>.inst.SetUseConsoleStyleJoypad(settings.m_ConsoleStyleJoypad);
		}
		else
		{
			d.LogError("ManStartup.InitializeControlsSettings, settings were NULL");
		}
	}

	private void InitializeCameraSettings(ManProfile.CameraSettings settings)
	{
		if (settings != null)
		{
			Globals.inst.m_RuntimeCameraSpinSensHorizontal = settings.m_Horizontal;
			Globals.inst.m_RuntimeCameraSpinSensVertical = settings.m_Vertical;
			Globals.inst.m_RuntimeCameraSpinInterpSpeed = settings.m_InterpolationSpeed;
			TankCamera.inst.SetFollowSpringStrength(settings.m_SpringLookup);
		}
	}

	private void InitializeGamepadSettings(ManProfile.GamepadSettings settings)
	{
		if (settings != null)
		{
			Globals.inst.m_CurrentGamepadCursorSpeed = settings.m_GamepadCursorSpeed;
			Globals.inst.m_CurrentGamepadVibration = settings.m_EnableControllerVibration;
		}
	}

	private IEnumerator InitComponentPool(ComponentPool pool)
	{
		yield return StartCoroutine(pool.InitPoolsFromTable(ShouldYield));
		yield return null;
	}

	private IEnumerator SendComponentPoolInitialisedEvents()
	{
		m_ComponentPoolInitialised = true;
		if (this.m_ComponentPoolInitializedEvent != null)
		{
			this.m_ComponentPoolInitializedEvent();
		}
		yield return null;
	}

	private IEnumerator InitSettings()
	{
		Singleton.Manager<ManProfile>.inst.OnUserChanged.Subscribe(InitialiseUserSettings);
		Singleton.Manager<ManProfile>.inst.DoDesktopFirstLoadStep();
		Singleton.Manager<ManProfile>.inst.RequestFirstUserActivation();
		if (QualitySettingsExtended.OverrideFixedTimeStep)
		{
			Time.fixedDeltaTime = QualitySettingsExtended.FixedTimeStepOverride;
			if (Time.fixedDeltaTime > 0.021f)
			{
				ManWheels.UsingLargeFixedTimeDelta = true;
			}
		}
		if (QualitySettingsExtended.OverrideMaximumDeltaTime)
		{
			Time.maximumDeltaTime = QualitySettingsExtended.MaximumDeltaTimeOverride;
		}
		yield break;
	}

	public void ReInitSettings()
	{
		Singleton.Manager<CameraManager>.inst.ScreenSize = m_InitScreenSize;
		Singleton.Manager<ManProfile>.inst.RequestFirstUserActivation();
	}

	private void InitialiseUserSettings(ManProfile.Profile activeProfile)
	{
		if (activeProfile != null)
		{
			if (Singleton.Manager<Localisation>.inst.CurrentLanguage != activeProfile.m_CurrentLanguage)
			{
				Singleton.Manager<Localisation>.inst.ChangeLanguage(activeProfile.m_CurrentLanguage);
			}
			InitializeGraphicsSettings(activeProfile.m_GraphicsSettings);
			InitializeSoundSettings(activeProfile.m_SoundSettings);
			InitializeControlsSettings(activeProfile.m_ControllerSettings);
			InitializeGamepadSettings(activeProfile.m_GamepadSettings);
			InitializeCameraSettings(activeProfile.m_CameraSettings);
		}
		else
		{
			d.LogError("ManStartup.InitialiseUserSettings, activeProfile was NULL");
		}
	}

	private void Start()
	{
		Singleton.Manager<ManPauseGame>.inst.LockPause(lockIt: true, ManPauseGame.DisablePauseReason.Startup);
		d.Log("ChangelistVersion: \"" + SKU.ChangelistVersion + "\"");
		d.Log("DisplayVersion: \"" + SKU.DisplayVersion + "\"");
		d.Log("Built Repo Hash: " + ChangelistProxy.COMMIT_ID);
		d.Log("Built Repo Branch: " + ChangelistProxy.COMMIT_BRANCH);
		d.Log("Built Job Desc: " + ChangelistProxy.BUILD_TITLE);
		d.Log("Built Job Num: " + ChangelistProxy.BUILD_ID);
		d.Log("System Memory Size: " + SystemInfo.systemMemorySize + "MB");
		d.Log("OS: " + SystemInfo.operatingSystem);
		d.Log("initial quality setting: " + QualitySettings.GetQualityLevel());
		d.Log($"Current Build Type:{SKU.CurrentBuildType}");
		string value = "Standard";
		new Dictionary<string, object>
		{
			{
				"Version",
				SKU.ChangelistVersion
			},
			{ "SKU", value },
			{
				"OS",
				SystemInfo.operatingSystem
			},
			{
				"SystemMemory",
				SystemInfo.systemMemorySize.ToString()
			},
			{ "Canary", false },
			{
				"GraphicsDevice",
				SystemInfo.graphicsDeviceType.ToString()
			}
		};
		new Dictionary<string, object>
		{
			{
				"quality",
				QualitySettings.GetQualityLevel().ToString()
			},
			{
				"resolution",
				Screen.width + "x" + Screen.height
			},
			{
				"aspect_ratio",
				((float)Screen.width / (float)Screen.height).ToString("N2", CultureInfo.InvariantCulture)
			}
		};
		GameStarted = false;
		Cursor.visible = false;
		Singleton.Manager<ManSplashScreen>.inst.CanExit = false;
		Type type = null;
		FastStart = false;
		if (FastStart)
		{
			Singleton.Manager<ComponentPool>.inst.DisableInitPools = true;
			Singleton.Manager<ManSplashScreen>.inst.AutoSkip = true;
		}
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(SetupUI());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(EnableSwitchFastLoad(enable: true));
		if (SKU.IsEpicGS)
		{
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<ManEOS>.inst.AwaitPlatformUserID());
		}
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(InitSettings());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<ManMusic>.inst.StartLoadingScreenMusic());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<ManNetworkLobby>.inst.InitialisePlatform());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(InitComponentPool(Singleton.Manager<ComponentPool>.inst));
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(SendComponentPoolInitialisedEvents());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<AutoSpriteCreate>.inst.CreateAndLoadItemSprites());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<ManSnapshots>.inst.UpdateSnapshotCacheOnStartup());
		Screen.SetResolution(Screen.width, Screen.height, Screen.fullScreenMode);
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(ForceFadeOnAndToFront());
		if (SKU.IsEpicGS)
		{
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(Singleton.Manager<ManEOS>.inst.AwaitDLCLoadComplete());
		}
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(WaitForSplashScreensDone());
		Singleton.Manager<ManGameSight>.inst.PostGameLaunchEvent();
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(SetCursorVisible());
		if (type != null)
		{
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(EnsureUserProfile());
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(EnterMode(type, Singleton.Manager<DebugUtil>.inst.m_Settings.m_StartModeSettings));
		}
		else
		{
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(EnterGame());
			Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(LoadUpdateNotifier());
		}
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(ReenableFog());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(TriggerFadeIn());
		Singleton.Manager<ManSplashScreen>.inst.AddCoroutine(EnableSwitchFastLoad(enable: false));
		GameStarted = true;
	}
}
