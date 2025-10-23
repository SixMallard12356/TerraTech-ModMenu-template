#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using TerraTech.Network;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIOptionsGameplay : UIOptions
{
	[Header("Language")]
	[SerializeField]
	private Dropdown m_LanguageDropdown;

	[SerializeField]
	private LocalisationEnums.Languages[] m_LanguageOrder;

	[Header("Application")]
	[SerializeField]
	[FormerlySerializedAs("m_PauseOnFocusLost")]
	private Toggle m_KeepPlayingInBackground;

	[Header("Hints")]
	[SerializeField]
	private Toggle m_ShowGameplayHints;

	[SerializeField]
	private Button m_ResetHintsButton;

	[SerializeField]
	private LocalisedString m_ResetAllHintsConfirmMessage;

	[Header("Gameplay Modifiers")]
	[SerializeField]
	private GameObject m_EnemyDifficultySettingContainer;

	[SerializeField]
	private Dropdown m_EnemyDifficultyDropdown;

	[SerializeField]
	private GameObject m_PlayerIndestructibleInCreativeContainer;

	[SerializeField]
	private Toggle m_PlayerIndestructibleInCreativeToggle;

	[SerializeField]
	private Toggle m_UseBuildBeamGizmosToggle;

	[SerializeField]
	private UIOptionsBehaviourToggle m_SkipPowerupDelay;

	[Header("Camera")]
	[SerializeField]
	private Slider m_VerticalSensitivitySlider;

	[SerializeField]
	private Slider m_HorizontalSensitivitySlider;

	[SerializeField]
	private Slider m_CameraSpringStrength;

	[SerializeField]
	private Slider m_CameraSmoothingSlider;

	[SerializeField]
	private Toggle m_InvertYToggle;

	[Obsolete("No longer used, setting is managed from UISchemaMenuBindingsPanel.m_InvertReversingToggle instead")]
	[SerializeField]
	private Toggle m_InvertReverseControls;

	[Header("Gamepad")]
	[SerializeField]
	private Slider m_GamepadCursorSpeedSlider;

	[SerializeField]
	private UIOptionsBehaviourToggle m_GamepadVibration;

	[SerializeField]
	[Header("Multiplayer/Crossplay")]
	private GameObject m_LobbyVisibility;

	[SerializeField]
	private UILobbyVisibilityDropdown m_LobbyVisibilityDropdown;

	[SerializeField]
	private UIOptionsBehaviourToggle m_ModifyOtherPlayerTechs;

	[SerializeField]
	private GameObject m_EnableEOSCrossplayContainer;

	[SerializeField]
	private Toggle m_EnableEOSCrossplayToggle;

	[SerializeField]
	private GameObject m_LinkEpicAccountContainer;

	[SerializeField]
	private Button m_LinkEpicAccountButton;

	[SerializeField]
	private LocalisedString m_EnableEOSCrossplay_Restart_Prompt;

	[SerializeField]
	private LocalisedString m_DisableEOSCrossplay_Restart_Prompt;

	[Header("Non-Gameplay Other")]
	[SerializeField]
	private Slider m_Gamma;

	[SerializeField]
	private Toggle m_DamageFeedbackEffectToggle;

	[SerializeField]
	private UIOptionsBehaviourScreenSize m_ScreenSize;

	[SerializeField]
	private Slider m_SFXVolume;

	[SerializeField]
	private Slider m_MusicVolume;

	private bool m_Initialised;

	private EventNoParams ChangesMadeEventToCall;

	private Dictionary<LocalisationEnums.Languages, int> m_LangToIndex = new Dictionary<LocalisationEnums.Languages, int>();

	private Dictionary<int, LocalisationEnums.Languages> m_IndexToLang = new Dictionary<int, LocalisationEnums.Languages>();

	public bool ShowVibrationOption => SKU.SwitchUI;

	public override void Setup(EventNoParams onChangeEvent)
	{
		ChangesMadeEventToCall = onChangeEvent;
		Init();
		if (m_InvertReverseControls != null)
		{
			m_InvertReverseControls.isOn = Globals.inst.reversingSteerInversion;
		}
		float value = Mathf.InverseLerp(Globals.inst.m_CameraSpinSensHorizontal.Min, Globals.inst.m_CameraSpinSensHorizontal.Max, Globals.inst.m_RuntimeCameraSpinSensHorizontal);
		m_HorizontalSensitivitySlider.normalizedValue = Mathf.Clamp01(value);
		float value2 = Mathf.InverseLerp(Globals.inst.m_CameraSpinSensVertical.Min, Globals.inst.m_CameraSpinSensVertical.Max, Mathf.Abs(Globals.inst.m_RuntimeCameraSpinSensVertical));
		m_VerticalSensitivitySlider.normalizedValue = Mathf.Clamp01(value2);
		m_CameraSmoothingSlider.normalizedValue = 1f - Globals.inst.m_CameraSpinInterpolationSpeedRange.FindNearestT(Globals.inst.m_RuntimeCameraSpinInterpSpeed);
		if (m_GamepadCursorSpeedSlider != null)
		{
			float value3 = Mathf.InverseLerp(Globals.inst.m_GamepadCursorSpeed.Min, Globals.inst.m_GamepadCursorSpeed.Max, Globals.inst.m_CurrentGamepadCursorSpeed);
			m_GamepadCursorSpeedSlider.normalizedValue = Mathf.Clamp01(value3);
		}
		if (m_GamepadVibration != null)
		{
			m_GamepadVibration.gameObject.SetActive(ShowVibrationOption);
			m_GamepadVibration.SetValue(Globals.inst.m_CurrentGamepadVibration);
		}
		m_InvertYToggle.isOn = Globals.inst.m_RuntimeCameraSpinSensVertical > 0f;
		if (m_LanguageDropdown != null)
		{
			m_LanguageDropdown.value = m_LangToIndex[Singleton.Manager<Localisation>.inst.CurrentLanguage];
		}
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			m_UseBuildBeamGizmosToggle.isOn = currentUser.m_GameplaySettings.m_UseForceGizmosInBuildBeam;
			m_CameraSpringStrength.value = currentUser.m_CameraSettings.m_SpringLookup;
			if (m_KeepPlayingInBackground != null)
			{
				m_KeepPlayingInBackground.isOn = !currentUser.m_GameplaySettings.m_PauseOnFocusLost;
			}
			m_ShowGameplayHints.isOn = currentUser.m_GameplaySettings.m_ShowGameplayHints;
			if (m_DamageFeedbackEffectToggle != null)
			{
				m_DamageFeedbackEffectToggle.isOn = currentUser.m_GraphicsSettings.m_DamageFeedbackEffect;
			}
		}
		bool flag = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCreative();
		bool flag2 = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD;
		bool flag3 = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Misc;
		bool isNetworked = ManNetwork.IsNetworked;
		bool flag4 = isNetworked && Singleton.Manager<ManGameMode>.inst.GetCurrentGameType().IsCoOp();
		bool isHost = ManNetwork.IsHost;
		bool flag5 = (flag || flag2 || flag3) && !flag4;
		if (m_PlayerIndestructibleInCreativeContainer != null)
		{
			m_PlayerIndestructibleInCreativeContainer.SetActive(flag5);
		}
		if (flag5)
		{
			m_PlayerIndestructibleInCreativeToggle.isOn = Singleton.Manager<ManPlayer>.inst.PlayerIndestructible;
		}
		if (SKU.ConsoleUI)
		{
			m_Gamma.value = (Singleton.Manager<CameraManager>.inst.GammaCorrection ? Singleton.Manager<CameraManager>.inst.GammaCorrection.gamma : 0f);
			if (m_ScreenSize != null)
			{
				bool flag6 = SKU.IsTeyon && SKU.PS4UI;
				bool active = !SKU.SwitchUI && !flag6;
				m_ScreenSize.gameObject.SetActive(active);
				m_ScreenSize.value = Singleton.Manager<CameraManager>.inst.ScreenSize;
			}
			m_MusicVolume.value = Singleton.Manager<ManMusic>.inst.GetMusicMixerVolume();
			m_SFXVolume.value = Singleton.Manager<ManMusic>.inst.GetSFXMixerVolume();
		}
		if (m_LobbyVisibility != null)
		{
			m_LobbyVisibility.gameObject.SetActive(flag4 && isNetworked && isHost);
			if (flag4 && isNetworked && isHost)
			{
				m_LobbyVisibilityDropdown.Visibility = Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLobbyVisibility(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.ID);
				m_LobbyVisibilityDropdown.DropDown.RefreshShownValue();
			}
		}
		if (m_ModifyOtherPlayerTechs != null)
		{
			m_ModifyOtherPlayerTechs.gameObject.SetActive(flag4 && isNetworked && isHost);
			if (flag4 && isNetworked && isHost)
			{
				m_ModifyOtherPlayerTechs.SetValue(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.Data.CoOpAllowPlayerTechModsChoice == 1);
			}
		}
		if (m_SkipPowerupDelay != null)
		{
			bool flag7 = !isNetworked && flag5;
			m_SkipPowerupDelay.gameObject.SetActive(flag7);
			if (flag7)
			{
				m_SkipPowerupDelay.SetValue(Singleton.Manager<ManPlayer>.inst.SkipPowerupSequencing);
			}
		}
		if (m_EnemyDifficultySettingContainer != null)
		{
			m_EnemyDifficultySettingContainer.SetActive(flag && isHost);
			if (flag && isHost)
			{
				m_EnemyDifficultyDropdown.SetValue((int)Singleton.Manager<ManPop>.inst.CreativePopulationDifficulty);
			}
		}
		TryRefresh_EOSCrossplayToggle();
		TryRefresh_ShowLinkAccountButton();
	}

	public override void SaveSettings()
	{
		float runtimeCameraSpinSensHorizontal = Mathf.Lerp(Globals.inst.m_CameraSpinSensHorizontal.Min, Globals.inst.m_CameraSpinSensHorizontal.Max, m_HorizontalSensitivitySlider.normalizedValue);
		Globals.inst.m_RuntimeCameraSpinSensHorizontal = runtimeCameraSpinSensHorizontal;
		float num = Mathf.Lerp(Globals.inst.m_CameraSpinSensVertical.Min, Globals.inst.m_CameraSpinSensVertical.Max, m_VerticalSensitivitySlider.normalizedValue);
		Globals.inst.m_RuntimeCameraSpinSensVertical = num * (float)(m_InvertYToggle.isOn ? 1 : (-1));
		if (m_InvertReverseControls != null)
		{
			Globals.inst.reversingSteerInversion = m_InvertReverseControls.isOn;
		}
		Globals.inst.m_RuntimeCameraSpinInterpSpeed = Globals.inst.m_CameraSpinInterpolationSpeedRange.Evaluate(1f - m_CameraSmoothingSlider.normalizedValue);
		TankCamera.inst.SetFollowSpringStrength(m_CameraSpringStrength.value);
		if (m_GamepadCursorSpeedSlider != null)
		{
			float currentGamepadCursorSpeed = Mathf.Lerp(Globals.inst.m_GamepadCursorSpeed.Min, Globals.inst.m_GamepadCursorSpeed.Max, m_GamepadCursorSpeedSlider.normalizedValue);
			Globals.inst.m_CurrentGamepadCursorSpeed = currentGamepadCursorSpeed;
		}
		if (ShowVibrationOption && m_GamepadVibration != null)
		{
			Globals.inst.m_CurrentGamepadVibration = m_GamepadVibration.isOn;
		}
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			currentUser.m_GameplaySettings.m_UseForceGizmosInBuildBeam = m_UseBuildBeamGizmosToggle.isOn;
			if (Singleton.playerTank != null && Singleton.playerTank.beam != null)
			{
				Singleton.playerTank.beam.RefreshForceGizmosActive();
			}
			currentUser.m_CurrentLanguage = Singleton.Manager<Localisation>.inst.CurrentLanguage;
			if (m_KeepPlayingInBackground != null)
			{
				currentUser.m_GameplaySettings.m_PauseOnFocusLost = !m_KeepPlayingInBackground.isOn;
			}
			currentUser.m_GameplaySettings.m_ShowGameplayHints = m_ShowGameplayHints.isOn;
			if (m_DamageFeedbackEffectToggle != null)
			{
				currentUser.m_GraphicsSettings.m_DamageFeedbackEffect = m_DamageFeedbackEffectToggle.isOn;
				Singleton.Manager<CameraManager>.inst.DamageFeedbackEffect.Enabled = m_DamageFeedbackEffectToggle.isOn;
			}
			ManProfile.CameraSettings cameraSettings = currentUser.m_CameraSettings;
			cameraSettings.m_Horizontal = Globals.inst.m_RuntimeCameraSpinSensHorizontal;
			cameraSettings.m_Vertical = Globals.inst.m_RuntimeCameraSpinSensVertical;
			cameraSettings.m_SpringLookup = m_CameraSpringStrength.value;
			cameraSettings.m_InterpolationSpeed = Globals.inst.m_RuntimeCameraSpinInterpSpeed;
			currentUser.m_GamepadSettings.m_GamepadCursorSpeed = Globals.inst.m_CurrentGamepadCursorSpeed;
			currentUser.m_GamepadSettings.m_EnableControllerVibration = Globals.inst.m_CurrentGamepadVibration;
			if (m_InvertReverseControls != null)
			{
				currentUser.m_ControllerSettings.m_ReverseInverseSteering = m_InvertReverseControls.isOn;
			}
			if (SKU.ConsoleUI)
			{
				ManProfile.GraphicsSettings graphicsSettings = currentUser.m_GraphicsSettings;
				graphicsSettings.m_Gamma = m_Gamma.value;
				graphicsSettings.m_ScreenSize = m_ScreenSize.value;
				ManProfile.SoundSettings soundSettings = currentUser.m_SoundSettings;
				soundSettings.m_MusicVolume = Singleton.Manager<ManMusic>.inst.GetMusicMixerVolume();
				soundSettings.m_SFXVolume = Singleton.Manager<ManMusic>.inst.GetSFXMixerVolume();
			}
		}
		if (m_EnemyDifficultySettingContainer.activeSelf && m_EnemyDifficultyDropdown.value != (int)Singleton.Manager<ManPop>.inst.CreativePopulationDifficulty)
		{
			ManPop.CreativeModePopulationDifficulty value = (ManPop.CreativeModePopulationDifficulty)m_EnemyDifficultyDropdown.value;
			Singleton.Manager<ManPop>.inst.SetCreativePopulationDifficulty(value);
		}
		if (m_PlayerIndestructibleInCreativeContainer.activeSelf && m_PlayerIndestructibleInCreativeToggle.isOn != Singleton.Manager<ManPlayer>.inst.PlayerIndestructible)
		{
			Singleton.Manager<ManPlayer>.inst.PlayerIndestructible = m_PlayerIndestructibleInCreativeToggle.isOn;
		}
		if ((bool)m_SkipPowerupDelay && m_SkipPowerupDelay.gameObject.activeInHierarchy && m_SkipPowerupDelay.isOn != Singleton.Manager<ManPlayer>.inst.SkipPowerupSequencing)
		{
			Singleton.Manager<ManPlayer>.inst.SkipPowerupSequencing = m_SkipPowerupDelay.isOn;
		}
		if (m_LobbyVisibility != null && m_LobbyVisibility.activeSelf)
		{
			Lobby.LobbyVisibility visibility = m_LobbyVisibilityDropdown.Visibility;
			if (Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.GetLobbyVisibility(Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.ID) != visibility)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SetVisibility(visibility);
			}
			bool isOn = m_ModifyOtherPlayerTechs.isOn;
			bool flag = isOn != Singleton.Manager<ManNetwork>.inst.CoOpAllowPlayerTechMods;
			Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.SetLobbyChoice(LobbyData.EnumLobbyChoiceIndex.LCI_CO_OP_ALLOW_PLAYER_TECH_MODS, isOn ? 1 : 0);
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer() && flag)
			{
				SystemChatMessage message = new SystemChatMessage
				{
					m_Bank = LocalisationEnums.StringBanks.SystemChatMessage,
					m_StringID = ((!isOn) ? 1 : 0)
				};
				Singleton.Manager<ManNetwork>.inst.SendToAllClients(TTMsgType.AddSystemChatMessage, message);
			}
			if (flag)
			{
				Singleton.Manager<ManNetworkLobby>.inst.LobbySystem.CurrentLobby.HandleLobbyDataUpdated(wasSuccessful: true);
			}
		}
	}

	public override UIScreenOptions.SaveFailureType CanSave()
	{
		return UIScreenOptions.SaveFailureType.None;
	}

	public override void OnCloseScreen()
	{
		Singleton.Manager<CameraManager>.inst.EndModifyScreenSize();
	}

	public override void ClearSettings()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null && Singleton.Manager<Localisation>.inst.CurrentLanguage != currentUser.m_CurrentLanguage)
		{
			Singleton.Manager<Localisation>.inst.ChangeLanguage(currentUser.m_CurrentLanguage);
		}
	}

	public override void ResetSettings()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null && Singleton.Manager<Localisation>.inst.CurrentLanguage != currentUser.m_CurrentLanguage)
		{
			Singleton.Manager<Localisation>.inst.ChangeLanguage(currentUser.m_CurrentLanguage);
		}
		m_ShowGameplayHints.isOn = Singleton.Manager<ManProfile>.inst.m_DefaultGameplaySettings.m_ShowGameplayHints;
		if (m_DamageFeedbackEffectToggle != null)
		{
			m_DamageFeedbackEffectToggle.isOn = Singleton.Manager<ManProfile>.inst.m_DefaultGraphicsSettings.m_DamageFeedbackEffect;
		}
		if (m_InvertReverseControls != null)
		{
			m_InvertReverseControls.isOn = Singleton.Manager<ManProfile>.inst.m_DefaultControllerSettings.m_ReverseInverseSteering;
		}
		if (m_GamepadCursorSpeedSlider != null)
		{
			float value = Mathf.InverseLerp(Globals.inst.m_GamepadCursorSpeed.Min, Globals.inst.m_GamepadCursorSpeed.Max, Singleton.Manager<ManProfile>.inst.m_DefaultGamepadSettings.m_GamepadCursorSpeed);
			m_GamepadCursorSpeedSlider.normalizedValue = Mathf.Clamp01(value);
		}
		if (m_GamepadVibration != null)
		{
			m_GamepadVibration.SetValue(Singleton.Manager<ManProfile>.inst.m_DefaultGamepadSettings.m_EnableControllerVibration);
		}
		m_VerticalSensitivitySlider.value = Mathf.InverseLerp(Globals.inst.m_CameraSpinSensHorizontal.Min, Globals.inst.m_CameraSpinSensHorizontal.Max, Mathf.Abs(Singleton.Manager<ManProfile>.inst.m_DefaultCameraSettings.m_Vertical));
		m_HorizontalSensitivitySlider.value = Mathf.InverseLerp(Globals.inst.m_CameraSpinSensHorizontal.Min, Globals.inst.m_CameraSpinSensHorizontal.Max, Mathf.Abs(Singleton.Manager<ManProfile>.inst.m_DefaultCameraSettings.m_Horizontal));
		m_CameraSpringStrength.value = Singleton.Manager<ManProfile>.inst.m_DefaultCameraSettings.m_SpringLookup;
		m_CameraSmoothingSlider.value = 1f - Globals.inst.m_CameraSpinInterpolationSpeedRange.FindNearestT(Singleton.Manager<ManProfile>.inst.m_DefaultCameraSettings.m_InterpolationSpeed);
		m_InvertYToggle.isOn = Singleton.Manager<ManProfile>.inst.m_DefaultCameraSettings.m_InvertY;
		m_UseBuildBeamGizmosToggle.isOn = Singleton.Manager<ManProfile>.inst.m_DefaultGameplaySettings.m_UseForceGizmosInBuildBeam;
		m_Gamma.value = (Singleton.Manager<CameraManager>.inst.GammaCorrection.gamma = Singleton.Manager<ManProfile>.inst.m_DefaultGraphicsSettings.m_Gamma);
		m_SFXVolume.value = Singleton.Manager<ManProfile>.inst.m_DefaultSoundSettings.m_SFXVolume;
		OnSFXVolumeChange(Singleton.Manager<ManProfile>.inst.m_DefaultSoundSettings.m_SFXVolume);
		m_MusicVolume.value = Singleton.Manager<ManProfile>.inst.m_DefaultSoundSettings.m_MusicVolume;
		OnMusicVolumeChange(Singleton.Manager<ManProfile>.inst.m_DefaultSoundSettings.m_MusicVolume);
	}

	public void ResetAllSeenHints()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
			Action callback = delegate
			{
				currentUser.ResetHintsSeen();
				Singleton.Manager<ManProfile>.inst.Save();
				Singleton.Manager<ManUI>.inst.RemovePopup();
				Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.Options);
			};
			Action callback2 = delegate
			{
				Singleton.Manager<ManUI>.inst.RemovePopup();
				Singleton.Manager<ManUI>.inst.ShowScreenPrompt(ManUI.ScreenType.Options);
			};
			UIButtonData accept = new UIButtonData
			{
				m_Callback = callback,
				m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
				m_RewiredAction = 21
			};
			UIButtonData decline = new UIButtonData
			{
				m_Callback = callback2,
				m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
				m_RewiredAction = 22
			};
			uIScreenNotifications.Set(m_ResetAllHintsConfirmMessage.Value, accept, decline);
			Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
		}
	}

	private void Init()
	{
		if (m_Initialised)
		{
			return;
		}
		d.Log(string.Concat(m_VerticalSensitivitySlider, ", ", m_HorizontalSensitivitySlider, m_CameraSpringStrength, m_InvertYToggle, m_UseBuildBeamGizmosToggle, m_Gamma, m_SFXVolume, m_MusicVolume, ChangesMadeEventToCall));
		List<string> outNamesList = new List<string>();
		if (m_LanguageDropdown != null)
		{
			List<LocalisationEnums.Languages> supportedLanguages = Singleton.Manager<Localisation>.inst.GetSupportedLanguages();
			List<LocalisationEnums.Languages> list = new List<LocalisationEnums.Languages>();
			LocalisationEnums.Languages[] languageOrder = m_LanguageOrder;
			foreach (LocalisationEnums.Languages item in languageOrder)
			{
				if (supportedLanguages.Contains(item))
				{
					list.Add(item);
				}
			}
			foreach (LocalisationEnums.Languages item2 in supportedLanguages)
			{
				if (!list.Contains(item2))
				{
					d.LogWarning($"Adding supported language {item2} to drop down, as it isn't contained in the Language Order");
					list.Add(item2);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				LocalisationEnums.Languages languages = list[j];
				string localisedLanguageName = StringLookup.GetLocalisedLanguageName(languages);
				outNamesList.Add(localisedLanguageName);
				m_LangToIndex.Add(languages, j);
				m_IndexToLang.Add(j, languages);
			}
			m_LanguageDropdown.AddOptions(outNamesList);
			m_LanguageDropdown.onValueChanged.AddListener(OnLanguageChanged);
			m_LanguageDropdown.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		m_VerticalSensitivitySlider.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_HorizontalSensitivitySlider.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_CameraSpringStrength.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_CameraSmoothingSlider.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_InvertYToggle.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_UseBuildBeamGizmosToggle.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		if (m_GamepadCursorSpeedSlider != null)
		{
			m_GamepadCursorSpeedSlider.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (m_GamepadVibration != null)
		{
			m_GamepadVibration.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (m_KeepPlayingInBackground != null)
		{
			m_KeepPlayingInBackground.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (m_ShowGameplayHints != null)
		{
			m_ShowGameplayHints.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (m_ResetHintsButton != null)
		{
			m_ResetHintsButton.onClick.AddListener(ResetAllSeenHints);
		}
		if (m_DamageFeedbackEffectToggle != null)
		{
			m_DamageFeedbackEffectToggle.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (m_EnemyDifficultyDropdown != null)
		{
			outNamesList.Clear();
			Singleton.Manager<ManPop>.inst.GetCreativePopDifficultyNames(ref outNamesList);
			m_EnemyDifficultyDropdown.AddOptions(outNamesList);
			m_EnemyDifficultyDropdown.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (m_InvertReverseControls != null)
		{
			m_InvertReverseControls.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		if (SKU.IsEOSCrossplayPlatform)
		{
			if (m_EnableEOSCrossplayToggle != null)
			{
				m_EnableEOSCrossplayToggle.onValueChanged.AddListener(OnEnableEOSCrossplayToggleChanged);
			}
			if (m_LinkEpicAccountButton != null)
			{
				m_LinkEpicAccountButton.onClick.AddListener(OnLinkEpicAccountButtonClicked);
			}
		}
		if (SKU.ConsoleUI)
		{
			m_Gamma.onValueChanged.AddListener(OnGammaChange);
			m_Gamma.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_ScreenSize.onValueChanged.AddListener(OnScreenSizeChange);
			m_ScreenSize.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_SFXVolume.onValueChanged.AddListener(OnSFXVolumeChange);
			m_SFXVolume.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
			m_MusicVolume.onValueChanged.AddListener(OnMusicVolumeChange);
			m_MusicVolume.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		m_Initialised = true;
	}

	private bool TryRefresh_EOSCrossplayToggle()
	{
		if (m_EnableEOSCrossplayToggle == null)
		{
			return false;
		}
		bool active = false;
		m_EnableEOSCrossplayContainer.gameObject.SetActive(active);
		m_EnableEOSCrossplayToggle.gameObject.SetActive(active);
		m_EnableEOSCrossplayToggle.isOn = Singleton.Manager<ManEOS>.inst.IsCrossplayRequestedActive;
		return true;
	}

	private bool TryRefresh_ShowLinkAccountButton()
	{
		if (m_LinkEpicAccountButton == null)
		{
			return false;
		}
		bool active = Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.Attract && SKU.IsEOSCrossplayPlatform && !Singleton.Manager<ManEOS>.inst.IsCrossplayAccountLinked && Singleton.Manager<ManEOS>.inst.IsCrossplayAwaitingAccountLink;
		m_LinkEpicAccountContainer.gameObject.SetActive(active);
		m_LinkEpicAccountButton.gameObject.SetActive(active);
		return true;
	}

	private void OnEnableEOSCrossplayToggleChanged(bool crossplayEnabled)
	{
		d.Assert(SKU.IsEOSCrossplayPlatform, "Shouldn't be able to link accounts on a non-eos crossplay platform");
		UIScreenNotifications uIScreenNotifications = Singleton.Manager<ManUI>.inst.GetScreen(ManUI.ScreenType.NotificationScreen) as UIScreenNotifications;
		UIButtonData accept = new UIButtonData
		{
			m_Callback = delegate
			{
				Singleton.Manager<ManEOS>.inst.SetEOSCrossplayRequested(crossplayEnabled);
				Application.Quit();
			},
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 29),
			m_RewiredAction = 21
		};
		UIButtonData decline = new UIButtonData
		{
			m_Callback = delegate
			{
				TryRefresh_EOSCrossplayToggle();
				Singleton.Manager<ManUI>.inst.RemovePopup();
			},
			m_Label = Singleton.Manager<Localisation>.inst.GetLocalisedString(LocalisationEnums.StringBanks.MenuMain, 30),
			m_RewiredAction = 22
		};
		uIScreenNotifications.Set((crossplayEnabled ? m_EnableEOSCrossplay_Restart_Prompt : m_DisableEOSCrossplay_Restart_Prompt).Value, accept, decline);
		Singleton.Manager<ManUI>.inst.PushScreenAsPopup(uIScreenNotifications);
	}

	private void OnLinkEpicAccountButtonClicked()
	{
		Singleton.Manager<ManEOS>.inst.LinkAccountsWithCurrentContinuanceToken(delegate
		{
			TryRefresh_ShowLinkAccountButton();
		});
	}

	private void OnLanguageChanged(int value)
	{
		LocalisationEnums.Languages language = m_IndexToLang[value];
		Singleton.Manager<Localisation>.inst.ChangeLanguage(language);
	}

	private void OnGammaChange(float value)
	{
		Singleton.Manager<CameraManager>.inst.GammaCorrection.gamma = value;
	}

	private void OnScreenSizeChange(float value)
	{
		Singleton.Manager<CameraManager>.inst.ScreenSize = value;
	}

	private void OnMusicVolumeChange(float value)
	{
		Singleton.Manager<ManMusic>.inst.SetMusicMixerVolume(value);
	}

	private void OnSFXVolumeChange(float value)
	{
		Singleton.Manager<ManMusic>.inst.SetSFXMixerVolume(value);
		Singleton.Manager<ManSFX>.inst.SFXVolume = value;
	}
}
