#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsGraphics : UIOptions
{
	[Serializable]
	private struct FullScreenModeName
	{
		public FullScreenMode mode;

		public LocalisedString name;
	}

	[SerializeField]
	private Dropdown m_ResolutionDropdown;

	[SerializeField]
	private Dropdown m_QualityDropdown;

	[SerializeField]
	private Dropdown m_MousePointerSizeDropdown;

	[SerializeField]
	private Dropdown m_FullscreenModeDropdown;

	[SerializeField]
	private FullScreenModeName[] m_FullscreenModeNames;

	[SerializeField]
	private Toggle m_ConfineMouse;

	[SerializeField]
	private Toggle m_DepthOfField;

	[SerializeField]
	private Toggle m_HDR;

	[SerializeField]
	private Toggle m_HBAO;

	[SerializeField]
	private Toggle m_AntiAliasing;

	[SerializeField]
	private Slider m_Gamma;

	[SerializeField]
	private Slider m_DrawDist;

	[SerializeField]
	private Slider m_DetailDist;

	[SerializeField]
	private Slider m_ShadowDist;

	[SerializeField]
	private Toggle m_DamageFeedbackEffectToggle;

	[SerializeField]
	private Toggle m_VSyncToggle;

	[SerializeField]
	private Slider m_FramerateLimit;

	[SerializeField]
	private Text m_FramerateLimitText;

	[SerializeField]
	private LocalisedString m_FramerateUnlimitedString;

	private bool m_Initialised;

	private EventNoParams ChangesMadeEventToCall;

	private List<Resolution> m_Resolutions = new List<Resolution>();

	private HashSet<int> m_ResolutionUniqueLookup = new HashSet<int>();

	private MousePointer mousePointer;

	private List<FullScreenMode> m_FullScreenModes = new List<FullScreenMode>();

	private const int kMaxFramerateUnlimited = 201;

	public override void Setup(EventNoParams onChangeEvent)
	{
		ChangesMadeEventToCall = onChangeEvent;
		Init();
		m_ResolutionDropdown.SetValue(GetResolutionIndex(Screen.width, Screen.height));
		int value = m_FullScreenModes.IndexOf(Screen.fullScreenMode);
		m_FullscreenModeDropdown.SetValue(value);
		m_ConfineMouse.SetValue(Cursor.lockState == CursorLockMode.Confined);
		if ((bool)m_MousePointerSizeDropdown)
		{
			m_MousePointerSizeDropdown.gameObject.SetActive((bool)mousePointer && mousePointer.HasSizeOptions());
			m_MousePointerSizeDropdown.SetValue(GameCursor.CursorSize);
		}
		m_QualityDropdown.SetValue(QualitySettings.GetQualityLevel());
		m_HBAO.SetValue(Singleton.Manager<CameraManager>.inst.AO != null && Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.AO));
		m_DepthOfField.SetValue(Singleton.Manager<CameraManager>.inst.DOF != null && Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.DOF));
		m_AntiAliasing.SetValue(Singleton.Manager<CameraManager>.inst.AA != null && Singleton.Manager<CameraManager>.inst.GetIsGraphicOptionEnabled(CameraManager.GraphicOption.AA));
		m_HDR.SetValue(Singleton.camera.allowHDR);
		m_Gamma.SetValue(Singleton.Manager<CameraManager>.inst.GammaCorrection ? Singleton.Manager<CameraManager>.inst.GammaCorrection.gamma : 0f);
		m_DrawDist.SetValue(Singleton.Manager<CameraManager>.inst.DrawDist01);
		m_DetailDist.SetValue(Singleton.Manager<CameraManager>.inst.DetailDist01);
		m_ShadowDist.SetValue(Singleton.Manager<CameraManager>.inst.ShadowDist01);
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			m_DamageFeedbackEffectToggle.SetValue(currentUser.m_GraphicsSettings.m_DamageFeedbackEffect);
			m_VSyncToggle.SetValue(currentUser.m_GraphicsSettings.m_VSyncEnabled);
			int num = currentUser.m_GraphicsSettings.m_MaxFramerate;
			if (num <= 0 || num >= 201)
			{
				num = 201;
			}
			m_FramerateLimit.SetValue(Mathf.RoundToInt(num));
			SetFramerateText(num);
			m_ConfineMouse.SetValue(currentUser.m_GraphicsSettings.m_ConfineMouseToScreen);
		}
	}

	public override void SaveSettings()
	{
		FullScreenMode fullScreenMode = m_FullScreenModes[m_FullscreenModeDropdown.value];
		if (m_ResolutionDropdown.value != GetResolutionIndex(Screen.width, Screen.height))
		{
			Resolution resolution = m_Resolutions[m_ResolutionDropdown.value];
			Screen.SetResolution(resolution.width, resolution.height, fullScreenMode);
			Singleton.Manager<ManGameSight>.inst.InitIdentifiers();
		}
		else if (fullScreenMode != Screen.fullScreenMode)
		{
			Screen.fullScreenMode = fullScreenMode;
		}
		if (m_QualityDropdown.value != QualitySettings.GetQualityLevel())
		{
			QualitySettings.SetQualityLevel(m_QualityDropdown.value, applyExpensiveChanges: true);
			QualitySettingsExtended.QualitySettingChangedEvent.Send();
		}
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			ManProfile.GraphicsSettings graphicsSettings = currentUser.m_GraphicsSettings;
			graphicsSettings.m_QualityLevel = m_QualityDropdown.value;
			graphicsSettings.m_HBAO = m_HBAO.isOn;
			graphicsSettings.m_DOF = m_DepthOfField.isOn;
			graphicsSettings.m_HDR = m_HDR.isOn;
			graphicsSettings.m_AA = m_AntiAliasing.isOn;
			graphicsSettings.m_DrawDist = m_DrawDist.value;
			graphicsSettings.m_DetailDist = m_DetailDist.value;
			graphicsSettings.m_ShadowDist = m_ShadowDist.value;
			graphicsSettings.m_Gamma = m_Gamma.value;
			graphicsSettings.m_DamageFeedbackEffect = m_DamageFeedbackEffectToggle.isOn;
			graphicsSettings.m_MousePointerSize = GameCursor.CursorSize;
			graphicsSettings.m_VSyncEnabled = m_VSyncToggle.isOn;
			int num = ((m_FramerateLimit.value < 201f) ? Mathf.RoundToInt(m_FramerateLimit.value) : (-1));
			graphicsSettings.m_MaxFramerate = Mathf.RoundToInt(num);
			graphicsSettings.m_ConfineMouseToScreen = m_ConfineMouse.isOn;
			mousePointer?.UpdateMouseConfinement();
		}
	}

	public override UIScreenOptions.SaveFailureType CanSave()
	{
		return UIScreenOptions.SaveFailureType.None;
	}

	public override void OnCloseScreen()
	{
	}

	public override void ClearSettings()
	{
		ManProfile.Profile currentUser = Singleton.Manager<ManProfile>.inst.GetCurrentUser();
		if (currentUser != null)
		{
			ManProfile.GraphicsSettings graphicsSettings = currentUser.m_GraphicsSettings;
			OnGammaChange(graphicsSettings.m_Gamma);
			OnDrawDistChange(graphicsSettings.m_DrawDist);
			OnDetailDistChange(graphicsSettings.m_DetailDist);
			OnShadowDistChange(graphicsSettings.m_ShadowDist);
			OnDOFChanged(graphicsSettings.m_DOF);
			OnHDRChanged(graphicsSettings.m_HDR);
			OnHBAOChanged(graphicsSettings.m_HBAO);
			OnAAChanged(graphicsSettings.m_AA);
			OnDamageFeedbackFXChanged(graphicsSettings.m_DamageFeedbackEffect);
			OnMousePointerSizeChange(graphicsSettings.m_MousePointerSize);
			Singleton.Manager<CameraManager>.inst.SetVSyncAndFramerateLimit(graphicsSettings.m_VSyncEnabled, graphicsSettings.m_MaxFramerate);
		}
	}

	public override void ResetSettings()
	{
	}

	private void Init()
	{
		if (m_Initialised)
		{
			return;
		}
		mousePointer = UnityEngine.Object.FindObjectOfType<MousePointer>();
		List<string> list = new List<string>();
		RefreshResolutions();
		for (int i = 0; i < m_Resolutions.Count; i++)
		{
			list.Add(m_Resolutions[i].width + "x" + m_Resolutions[i].height);
		}
		m_ResolutionDropdown.AddOptions(list);
		m_ResolutionDropdown.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_ConfineMouse.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_FullscreenModeDropdown.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		OnLangaugeChanged();
		m_QualityDropdown.onValueChanged.AddListener(OnQualityChange);
		m_QualityDropdown.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		Singleton.Manager<Localisation>.inst.OnLanguageChanged.Subscribe(OnLangaugeChanged);
		if (m_MousePointerSizeDropdown != null)
		{
			m_MousePointerSizeDropdown.onValueChanged.AddListener(OnMousePointerSizeChange);
			m_MousePointerSizeDropdown.onValueChanged.AddListener(delegate
			{
				ChangesMadeEventToCall.Send();
			});
		}
		m_Gamma.onValueChanged.AddListener(OnGammaChange);
		m_Gamma.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_DrawDist.onValueChanged.AddListener(OnDrawDistChange);
		m_DrawDist.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_DetailDist.onValueChanged.AddListener(OnDetailDistChange);
		m_DetailDist.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_ShadowDist.onValueChanged.AddListener(OnShadowDistChange);
		m_ShadowDist.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_DepthOfField.onValueChanged.AddListener(OnDOFChanged);
		m_DepthOfField.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_HDR.onValueChanged.AddListener(OnHDRChanged);
		m_HDR.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_HBAO.onValueChanged.AddListener(OnHBAOChanged);
		m_HBAO.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_AntiAliasing.onValueChanged.AddListener(OnAAChanged);
		m_AntiAliasing.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_DamageFeedbackEffectToggle.onValueChanged.AddListener(OnDamageFeedbackFXChanged);
		m_DamageFeedbackEffectToggle.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_VSyncToggle.onValueChanged.AddListener(OnVSyncChanged);
		m_VSyncToggle.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_FramerateLimit.onValueChanged.AddListener(OnFramerateLimitChanged);
		m_FramerateLimit.onValueChanged.AddListener(delegate
		{
			ChangesMadeEventToCall.Send();
		});
		m_Initialised = true;
	}

	private void RefreshResolutions()
	{
		m_Resolutions.Clear();
		m_ResolutionUniqueLookup.Clear();
		for (int i = 0; i < Screen.resolutions.Length; i++)
		{
			Resolution resolution = Screen.resolutions[i];
			int resolutionHash = GetResolutionHash(resolution);
			if (!m_ResolutionUniqueLookup.Contains(resolutionHash))
			{
				m_ResolutionUniqueLookup.Add(resolutionHash);
				m_Resolutions.Add(resolution);
			}
		}
	}

	private int GetResolutionHash(Resolution resolution)
	{
		return (resolution.width << 16) | resolution.height;
	}

	private int GetResolutionIndex(int width, int height)
	{
		for (int i = 0; i < m_Resolutions.Count; i++)
		{
			Resolution resolution = m_Resolutions[i];
			if (resolution.width == width && resolution.height == height)
			{
				return i;
			}
		}
		d.LogErrorFormat("UIOptionsGraphics.GetResolutionIndex - could not find a resolution with width {0} height {1} out of {2} options in the list. Do you need to call RefreshResolutions()? Unity editor? {3} Fullscreen? {4} ", width, height, m_Resolutions.Count, Application.isEditor, Screen.fullScreen);
		return 0;
	}

	private void SetFramerateText(int framerateLimit)
	{
		bool flag = framerateLimit > 0 && framerateLimit < 201;
		m_FramerateLimitText.text = (flag ? framerateLimit.ToString() : m_FramerateUnlimitedString.Value);
	}

	private void OnQualityChange(int newLevel)
	{
		QualitySettings.SetQualityLevel(m_QualityDropdown.value, applyExpensiveChanges: false);
		QualitySettingsExtended.PerQualitySettings qualitySettings = QualitySettingsExtended.GetQualitySettings(newLevel);
		m_HBAO.isOn = qualitySettings.m_DefaultHBAOEnabled;
		m_DepthOfField.isOn = qualitySettings.m_DefaultDOFEnabled;
		m_AntiAliasing.isOn = qualitySettings.m_DefaultAntialiasingEnabled;
		m_HDR.isOn = qualitySettings.m_DefaultHDREnabled;
		m_DrawDist.value = QualitySettingsExtended.ViewDistanceRange.InverseLerp(qualitySettings.m_DefaultDrawDistance);
		m_DetailDist.value = QualitySettingsExtended.DetailDistanceRange.InverseLerp(qualitySettings.m_DefaultDetailDistance);
		m_ShadowDist.value = QualitySettingsExtended.ShadowDistanceRange.InverseLerp(qualitySettings.m_DefaultShadowDistance);
	}

	private void OnMousePointerSizeChange(int newSize)
	{
		GameCursor.SetCursorSize(newSize);
	}

	private void OnGammaChange(float value)
	{
		Singleton.Manager<CameraManager>.inst.GammaCorrection.gamma = value;
	}

	private void OnDrawDistChange(float value)
	{
		Singleton.Manager<CameraManager>.inst.SetDrawDist01(value);
	}

	private void OnDetailDistChange(float value)
	{
		Singleton.Manager<CameraManager>.inst.SetDetailDist01(value);
	}

	private void OnShadowDistChange(float value)
	{
		Singleton.Manager<CameraManager>.inst.SetShadowDist01(value);
	}

	private void OnDOFChanged(bool value)
	{
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.DOF, value);
	}

	private void OnHDRChanged(bool value)
	{
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.HDR, value);
	}

	private void OnHBAOChanged(bool value)
	{
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AO, value);
	}

	private void OnAAChanged(bool value)
	{
		Singleton.Manager<CameraManager>.inst.SetGraphicOptionEnabled(CameraManager.GraphicOption.AA, value);
	}

	private void OnDamageFeedbackFXChanged(bool value)
	{
		Singleton.Manager<CameraManager>.inst.DamageFeedbackEffect.Enabled = value;
	}

	private void OnVSyncChanged(bool value)
	{
		int maxFramerate = ((m_FramerateLimit.value < 201f) ? Mathf.RoundToInt(m_FramerateLimit.value) : (-1));
		Singleton.Manager<CameraManager>.inst.SetVSyncAndFramerateLimit(value, maxFramerate);
	}

	private void OnFramerateLimitChanged(float value)
	{
		int num = ((value > 0f && value < 201f) ? Mathf.RoundToInt(value) : (-1));
		Singleton.Manager<CameraManager>.inst.SetVSyncAndFramerateLimit(m_VSyncToggle.isOn, num);
		SetFramerateText(num);
	}

	private void OnLangaugeChanged()
	{
		m_QualityDropdown.ClearOptions();
		List<string> list = new List<string>();
		for (int i = 0; i < QualitySettings.names.Length; i++)
		{
			string item = $"{i + 1}. {QualitySettingsExtended.GetQualitySettings(i).m_SettingName.Value}";
			list.Add(item);
		}
		m_QualityDropdown.AddOptions(list);
		if ((bool)m_MousePointerSizeDropdown && (bool)mousePointer)
		{
			m_MousePointerSizeDropdown.ClearOptions();
			List<string> list2 = new List<string>();
			mousePointer.GetLocalisedSizeNames(list2);
			m_MousePointerSizeDropdown.AddOptions(list2);
		}
		if (m_FullscreenModeDropdown != null)
		{
			m_FullscreenModeDropdown.ClearOptions();
			m_FullScreenModes.Clear();
			List<string> list3 = new List<string>();
			FullScreenModeName[] fullscreenModeNames = m_FullscreenModeNames;
			for (int j = 0; j < fullscreenModeNames.Length; j++)
			{
				FullScreenModeName fullScreenModeName = fullscreenModeNames[j];
				m_FullScreenModes.Add(fullScreenModeName.mode);
				list3.Add(fullScreenModeName.name.Value);
			}
			m_FullscreenModeDropdown.AddOptions(list3);
		}
	}
}
