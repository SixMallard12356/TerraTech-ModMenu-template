#define UNITY_EDITOR
using System.Collections.Generic;
using FMODUnity;
using TerraTech.Network;
using UnityEngine;

public class ManPauseGame : Singleton.Manager<ManPauseGame>
{
	public enum DisablePauseReason
	{
		LoadingLevel,
		UScriptNewGameIntro,
		Tutorial,
		Startup,
		TeleportScreenFX
	}

	public delegate void ConfigureExitConfirmCallback(UIScreenNotifications confirmScreen);

	private struct PauseMenuEntry
	{
		public ManUI.ScreenType screen;

		public bool haltsGameplay;

		public ConfigureExitConfirmCallback exitConfirmConfig;
	}

	private class DelayedPauseRequest
	{
		public bool pauseState;

		public bool withPauseMenu;
	}

	public Event<bool> PauseEvent;

	private bool m_Inited;

	private bool m_IsPaused;

	private bool m_IsPhotoCamPause;

	private bool m_PausedWhenFocusLost;

	private bool m_WasPausedBeforeSystemOverlay;

	private bool m_HasFocus;

	private bool m_HasFocusLiveValue;

	private Bitfield<DisablePauseReason> m_PauseDisabledReasons = new Bitfield<DisablePauseReason>();

	private DelayedPauseRequest m_QueuedPauseRequest;

	private bool m_UsePhotoCam;

	private List<PauseMenuEntry> m_PauseMenuStack = new List<PauseMenuEntry>(8);

	public bool IsPaused
	{
		get
		{
			return m_IsPaused;
		}
		private set
		{
			m_IsPaused = value;
		}
	}

	public bool AllowPause => m_PauseDisabledReasons.IsNull;

	public bool PhotoCamToggle
	{
		get
		{
			return m_UsePhotoCam;
		}
		set
		{
			m_UsePhotoCam = value;
		}
	}

	public void PushPauseMenu(ManUI.ScreenType pauseScreen, bool pauseGameplay, ConfigureExitConfirmCallback exitCallback)
	{
		m_PauseMenuStack.Add(new PauseMenuEntry
		{
			screen = pauseScreen,
			haltsGameplay = pauseGameplay,
			exitConfirmConfig = exitCallback
		});
	}

	public void PopPauseMenu()
	{
		d.Assert(m_PauseMenuStack.Count > 0, "Trying to pop from an empty stack");
		if (m_PauseMenuStack.Count > 0)
		{
			m_PauseMenuStack.RemoveAt(m_PauseMenuStack.Count - 1);
		}
	}

	public void Pause(bool applyScreenFilter = true)
	{
		d.Assert(!Singleton.Manager<ManNetwork>.inst.IsMultiplayer(), "[ManPauseGame] Trying to pause in MP");
		if (Singleton.Manager<CameraManager>.inst.IsCurrent<PlayerFreeCamera>())
		{
			applyScreenFilter = false;
		}
		Pause(pause: true, hideHUD: true, applyScreenFilter);
	}

	public void Resume()
	{
		bool usePhotoCam = m_UsePhotoCam;
		Pause(pause: false, usePhotoCam);
	}

	public void TryTogglePhotoMode()
	{
		if (!GetIsGamePausedOrShowingPauseMenu() && CanUsePhotoMode())
		{
			m_UsePhotoCam = !m_UsePhotoCam;
			HideHUD(m_UsePhotoCam);
			RefreshChangeCamera();
		}
	}

	public bool CanUsePhotoMode()
	{
		if (!SKU.ConsoleUI)
		{
			return Singleton.Manager<ManGameMode>.inst.DoesModeSupportPhotoModeCamera();
		}
		return false;
	}

	public void ToggleUniversalPauseGame()
	{
		PauseGame(!GetIsGamePausedOrShowingPauseMenu(), withPauseMenu: false);
	}

	public void TogglePauseGame()
	{
		PauseGame(!GetIsGamePausedOrShowingPauseMenu());
	}

	public bool IsMultiPlayerActive()
	{
		bool result = false;
		if (Singleton.Manager<ManNetwork>.inst != null && Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
		{
			result = true;
		}
		else if ((bool)Singleton.Manager<ManNetworkLobby>.inst && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem != null && Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby != null)
		{
			result = true;
		}
		else
		{
			ManUI manUI = Singleton.Manager<ManUI>.inst;
			if (manUI != null && (manUI.IsScreenShowing(ManUI.ScreenType.JoinMultiplayer) || manUI.IsScreenShowing(ManUI.ScreenType.MultiplayerSetupTEMP) || manUI.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyScreen) || manUI.IsScreenShowing(ManUI.ScreenType.MatchmakingLobbyList)))
			{
				result = true;
			}
		}
		return result;
	}

	public void PauseGame(bool pauseState, bool withPauseMenu = true)
	{
		if (pauseState && GetIsPauseDelayed())
		{
			if (m_QueuedPauseRequest == null)
			{
				m_QueuedPauseRequest = new DelayedPauseRequest
				{
					pauseState = pauseState,
					withPauseMenu = withPauseMenu
				};
			}
		}
		else
		{
			if (!CanPause(pauseState, autoPause: false))
			{
				return;
			}
			Singleton.Manager<ManUI>.inst.ExitAllScreens();
			bool flag = (m_PauseMenuStack.Count == 0 || m_PauseMenuStack[m_PauseMenuStack.Count - 1].haltsGameplay) && !IsMultiPlayerActive();
			m_IsPhotoCamPause = m_UsePhotoCam && !withPauseMenu && flag;
			if (pauseState)
			{
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Open);
				if (!m_IsPhotoCamPause)
				{
					ManUI.ScreenType pauseScreen = GetPauseScreen();
					Singleton.Manager<ManUI>.inst.GoToScreen(pauseScreen, flag ? ManUI.PauseType.Pause : ManUI.PauseType.None);
				}
				if (flag)
				{
					Pause(pause: true, hideHUD: true, !m_IsPhotoCamPause);
				}
				else
				{
					HideHUD(hideHUD: true);
				}
			}
			else
			{
				Singleton.Manager<ManSFX>.inst.PlayUISFX(ManSFX.UISfxType.Close);
				bool usePhotoCam = m_UsePhotoCam;
				if (flag)
				{
					Pause(pause: false, usePhotoCam);
				}
				else
				{
					HideHUD(usePhotoCam);
				}
			}
			RefreshChangeCamera();
		}
	}

	public bool IsPauseMenuShowing()
	{
		ManUI.ScreenType pauseScreen = GetPauseScreen();
		return Singleton.Manager<ManUI>.inst.IsScreenShowing(pauseScreen);
	}

	public bool IsPauseMenuInScreenStack()
	{
		return Singleton.Manager<ManUI>.inst.IsScreenInStack(GetPauseScreen());
	}

	public bool GetIsGamePausedOrShowingPauseMenu()
	{
		if (!IsPauseMenuInScreenStack())
		{
			return m_IsPaused;
		}
		return true;
	}

	public void LockPause(bool lockIt, DisablePauseReason disableReason)
	{
		m_PauseDisabledReasons.Set((int)disableReason, lockIt);
	}

	public void ShowExitConfirmScreen()
	{
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		if ((bool)uIScreenNotifications)
		{
			((m_PauseMenuStack.Count > 0) ? m_PauseMenuStack[m_PauseMenuStack.Count - 1].exitConfirmConfig : null)?.Invoke(uIScreenNotifications);
			Singleton.Manager<ManUI>.inst.GoToScreen(uIScreenNotifications.Type);
		}
	}

	public void OnSystemOverlayActive(bool active)
	{
		if (active)
		{
			if (!IsMultiPlayerActive())
			{
				m_WasPausedBeforeSystemOverlay = IsPaused;
				Pause();
			}
		}
		else
		{
			if (!m_WasPausedBeforeSystemOverlay || IsMultiPlayerActive())
			{
				Resume();
			}
			m_WasPausedBeforeSystemOverlay = false;
		}
	}

	public void SetApplicationFocus(bool hasFocus)
	{
		if (m_HasFocus != hasFocus)
		{
			d.Log($"ManPauseGame.SetApplicationFocus {hasFocus}");
			m_HasFocus = hasFocus;
		}
	}

	private void PostExitMode(Mode exitedMode)
	{
		m_PauseDisabledReasons.Clear();
		m_UsePhotoCam = false;
		Pause(pause: false, hideHUD: false);
		HideHUD(hideHUD: false);
	}

	private void Pause(bool pause, bool hideHUD, bool applyScreenFilter = true)
	{
		if (pause != m_IsPaused)
		{
			d.Log($"ManPauseGame.Pause {pause} hideHUD={hideHUD} applyScreenFilter={applyScreenFilter}");
			HideHUD(hideHUD);
			Singleton.Manager<ManOnScreenMessages>.inst.Hide(pause);
			m_IsPaused = pause;
			Time.timeScale = (pause ? 0f : 1f);
			Singleton.Manager<ManMusic>.inst.SetPaused(pause);
			if (!pause || applyScreenFilter)
			{
				Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.ScreenBlur, pause && applyScreenFilter);
			}
			PauseEvent.Send(pause);
		}
	}

	private void HideHUD(bool hideHUD)
	{
		if (hideHUD)
		{
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechLoader);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TechManager);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.MissionLog);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.GamepadQuickMenu);
			Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.TeleportMenu);
		}
		Singleton.Manager<ManHUD>.inst.Show(ManHUD.HideReason.Paused, !hideHUD);
	}

	private void RefreshChangeCamera()
	{
		if (m_UsePhotoCam && (!m_IsPaused || m_IsPhotoCamPause))
		{
			Singleton.Manager<CameraManager>.inst.Switch<PlayerFreeCamera>(forceSwitch: false);
		}
		else
		{
			Singleton.Manager<ManGameMode>.inst.SetModeDefaultCamera(forceSwitch: false);
		}
	}

	private ManUI.ScreenType GetPauseScreen()
	{
		if (m_PauseMenuStack.Count > 0)
		{
			return m_PauseMenuStack[m_PauseMenuStack.Count - 1].screen;
		}
		return ManUI.ScreenType.Pause;
	}

	private bool GetIsPauseDelayed()
	{
		return Singleton.Manager<ManTechSwapper>.inst.CheckOperationInProgress();
	}

	private bool CanPause(bool pauseState, bool autoPause)
	{
		bool num = (autoPause ? Singleton.Manager<ManGameMode>.inst.GetIsInAnyMode() : Singleton.Manager<ManGameMode>.inst.GetIsInPlayableMode());
		bool flag = false;
		if ((Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Deathmatch && Mode<ModeDeathmatch>.inst.IsRestarting) || Singleton.Manager<ManNetwork>.inst.IsInPhase(NetController.Phase.Outro) || Singleton.Manager<ManNetwork>.inst.IsInPhase(NetController.Phase.GatherPlayers) || Singleton.Manager<ManNetwork>.inst.IsInPhase(NetController.Phase.Intro))
		{
			flag = pauseState;
		}
		if (num && Singleton.Manager<ManPauseGame>.inst.AllowPause && !Singleton.Manager<ManUI>.inst.IsExitBlockedOnCurrentScreen() && !Singleton.Manager<ManGameMode>.inst.IsSwitchingMode)
		{
			return !flag;
		}
		return false;
	}

	private void Init()
	{
		m_Inited = true;
		m_HasFocus = Application.isFocused;
		m_HasFocusLiveValue = !m_HasFocus;
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		d.Log($"ManPauseGame.OnApplicationFocus {hasFocus}");
		m_HasFocus = hasFocus;
	}

	private void Start()
	{
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(PostExitMode);
		Singleton.Manager<ManStartup>.inst.DoOnceAfterComponentPoolInitialised(Init);
	}

	private void Update()
	{
		if (IsMultiPlayerActive() && !m_HasFocusLiveValue)
		{
			RuntimeManager.ApplicationPausedManual(pauseStatus: false);
			m_HasFocusLiveValue = true;
		}
		if (m_Inited && m_HasFocusLiveValue != m_HasFocus)
		{
			bool flag = Singleton.Manager<ManProfile>.inst.GetCurrentUser()?.m_GameplaySettings.m_PauseOnFocusLost ?? false;
			if (IsMultiPlayerActive())
			{
				flag = false;
			}
			if (flag)
			{
				if (!m_HasFocus)
				{
					if (CanPause(pauseState: true, autoPause: true))
					{
						m_HasFocusLiveValue = m_HasFocus;
						m_PausedWhenFocusLost = m_IsPaused;
						if (!m_PausedWhenFocusLost)
						{
							Pause();
							RuntimeManager.ApplicationPausedManual(pauseStatus: true);
						}
					}
				}
				else
				{
					m_HasFocusLiveValue = m_HasFocus;
					if (!m_PausedWhenFocusLost)
					{
						Resume();
						RuntimeManager.ApplicationPausedManual(pauseStatus: false);
					}
				}
			}
			if (IsMultiPlayerActive() && m_IsPaused)
			{
				Resume();
			}
		}
		if (m_QueuedPauseRequest != null)
		{
			PauseGame(m_QueuedPauseRequest.pauseState, m_QueuedPauseRequest.withPauseMenu);
			m_QueuedPauseRequest = null;
		}
	}
}
