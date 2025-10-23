#define UNITY_EDITOR
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.PostProcessing;
using UnityStandardAssets.ImageEffects;
using cakeslice;

public class CameraManager : Singleton.Manager<CameraManager>, IGlobalFog
{
	public abstract class Camera : MonoBehaviour
	{
		public enum Types
		{
			FirstPersonFlyCam,
			FramingCamera,
			TankCamera,
			DebugCamera,
			PlayerFreeCamera
		}

		public abstract void Enable();
	}

	public enum GraphicOption
	{
		Fog,
		ScreenBlur,
		AA,
		DOF,
		AO,
		CA,
		Bloom,
		HDR
	}

	[SerializeField]
	private Camera currentCamera;

	[SerializeField]
	[Tooltip("Percentage of clipping distance that Global Fob gets set to")]
	private int m_FogDistancePercentage = 75;

	[SerializeField]
	private float m_DepthOfFieldReferenceFocusDistance = 15f;

	[SerializeField]
	private float m_DepthOfFieldAperture = 1f;

	[SerializeField]
	private float m_DepthOfFieldFocalLength = 46f;

	[SerializeField]
	private float m_DepthOfFieldScreenResScale = 0.015f;

	public Event<Camera, Camera> CameraSwitchEvent;

	public Event<GraphicOption, bool> OnGraphicOptionChanged;

	private Camera[] cameras;

	private Vector3 initialPosition;

	private Quaternion initialOrientation;

	private ReflectionProbe m_ReflectionProbe;

	private float m_ReflectionProbeLastUpdate;

	private bool m_ReflectionProbeUpdateEnabled;

	private float m_ScreenSize = 1f;

	private Bitfield<GraphicOption> m_EnabledSettings = new Bitfield<GraphicOption>();

	private Bitfield<GraphicOption> m_ForceDisabledSettings = new Bitfield<GraphicOption>();

	public Skybox Sky { get; private set; }

	public OutlineEffect OutlineEffect { get; private set; }

	public DamageFeedbackEffect DamageFeedbackEffect { get; private set; }

	public TeleportEffect TeleportEffect { get; private set; }

	public GammaCorrectionEffect GammaCorrection { get; private set; }

	public SunShafts SunShafts { get; private set; }

	public GlobalFog Fog { get; private set; }

	public BlurOptimized ScreenBlur { get; private set; }

	public PostProcessingBehaviour PostProcessing { get; private set; }

	public AntialiasingModel AA { get; private set; }

	public DepthOfFieldModel DOF { get; private set; }

	public AmbientOcclusionModel AO { get; private set; }

	public ChromaticAberrationModel CA { get; private set; }

	public BloomModel Bloom { get; private set; }

	public float DrawDist01 { get; private set; }

	public float DetailDist01 { get; private set; }

	public float ShadowDist01 { get; private set; }

	public float ScreenSize
	{
		get
		{
			return m_ScreenSize;
		}
		set
		{
			if (value != m_ScreenSize)
			{
				m_ScreenSize = value;
				Singleton.Manager<ManUI>.inst.transform.localScale = new Vector3(m_ScreenSize, m_ScreenSize, 1f);
				if (!Singleton.Manager<ManUI>.inst.ResizeScreenActive)
				{
					Singleton.camera.rect = new Rect((1f - value) * 0.5f, (1f - value) * 0.5f, value, value);
				}
			}
		}
	}

	public void ResetCamera()
	{
		ResetCamera(initialPosition, initialOrientation);
	}

	public void ResetCamera(Vector3 position, Quaternion rotation)
	{
		Singleton.cameraTrans.position = position;
		Singleton.cameraTrans.rotation = rotation;
		currentCamera.Enable();
	}

	public bool IsCurrent<T>() where T : Camera
	{
		return currentCamera is T;
	}

	public void Switch<T>(bool forceSwitch = true) where T : Camera
	{
		if (forceSwitch || !IsCurrent<T>())
		{
			Switch(cameras.Single((Camera m) => m is T));
		}
	}

	public void Switch(Camera camera)
	{
		if ((bool)camera && (!currentCamera || currentCamera.enabled))
		{
			currentCamera.enabled = false;
			CameraSwitchEvent.Send(currentCamera, camera);
			camera.Enable();
			camera.enabled = true;
			currentCamera = camera;
		}
	}

	public T GetCamera<T>() where T : Camera
	{
		for (int i = 0; i < cameras.Length; i++)
		{
			if (cameras[i] is T)
			{
				return cameras[i] as T;
			}
		}
		return null;
	}

	public void StartModifyScreenSize()
	{
		Singleton.Manager<ManUI>.inst.ResizeScreenActive = true;
	}

	public void EndModifyScreenSize()
	{
		float screenSize = ScreenSize;
		Singleton.camera.rect = new Rect((1f - screenSize) * 0.5f, (1f - screenSize) * 0.5f, screenSize, screenSize);
		Singleton.Manager<ManUI>.inst.ResizeScreenActive = false;
	}

	public void SetGraphicOptionEnabled(GraphicOption option, bool enabled)
	{
		m_EnabledSettings.Set((int)option, enabled);
		if (!m_ForceDisabledSettings.Contains((int)option))
		{
			UpdateGraphicOption(option, enabled);
		}
	}

	public bool GetIsGraphicOptionEnabled(GraphicOption option)
	{
		return m_EnabledSettings.Contains((int)option);
	}

	public void SetForceDisableGraphicOption(GraphicOption option, bool disabled)
	{
		m_ForceDisabledSettings.Set((int)option, disabled);
		bool flag = !disabled && m_EnabledSettings.Contains((int)option);
		UpdateGraphicOption(option, flag);
	}

	public void SetDOFFocusDistance(float distance)
	{
		if (DOF != null)
		{
			distance = Mathf.Max(0.5f, distance * 0.5f);
			DepthOfFieldModel.Settings settings = DOF.settings;
			settings.focusDistance = distance;
			settings.aperture = m_DepthOfFieldAperture * (m_DepthOfFieldReferenceFocusDistance / distance);
			settings.focalLength = m_DepthOfFieldFocalLength - ((float)Singleton.camera.pixelHeight - 1080f) * m_DepthOfFieldScreenResScale;
			DOF.settings = settings;
		}
	}

	public void SetChromaticAberrationIntensity(float newIntensity)
	{
		if (CA != null)
		{
			ChromaticAberrationModel.Settings settings = CA.settings;
			settings.intensity = newIntensity;
			CA.settings = settings;
		}
	}

	public void SetBloomIntensity(float bloomIntensity, float lensDirtIntensity)
	{
		if (Bloom != null)
		{
			BloomModel.Settings settings = Bloom.settings;
			settings.bloom.intensity = bloomIntensity;
			settings.lensDirt.intensity = lensDirtIntensity;
			Bloom.settings = settings;
		}
	}

	public bool IsPosInsideCamFrustrum(Vector3 scenePos)
	{
		Vector3 a = scenePos - Singleton.cameraTrans.position;
		float sqrMagnitude = a.sqrMagnitude;
		float farClipPlane = Singleton.camera.farClipPlane;
		if (sqrMagnitude < farClipPlane * farClipPlane)
		{
			float num = a.Dot(Singleton.cameraTrans.forward);
			if (num > 0f && Mathf.Acos(num / Mathf.Max(a.magnitude, Mathf.Epsilon)) * 57.29578f <= HorizontalFOV() * 0.5f)
			{
				return true;
			}
		}
		return false;
	}

	public float HorizontalFOV()
	{
		float num = Singleton.camera.fieldOfView * ((float)Math.PI / 180f);
		return 2f * Mathf.Atan(Mathf.Tan(num / 2f) * Singleton.camera.aspect) * 57.29578f;
	}

	public void RenderReflectionProbe()
	{
		if (QualitySettingsExtended.EnableReflectionProbes && !Singleton.Manager<ManWorld>.inst.TileManager.IsClearing)
		{
			m_ReflectionProbe.RenderProbe();
			m_ReflectionProbeLastUpdate = Time.time;
		}
	}

	public void SetDrawDist01(float drawDist01)
	{
		DrawDist01 = drawDist01;
		SetClipDistInternal(QualitySettingsExtended.ViewDistanceRange.Lerp(DrawDist01));
	}

	public void SetDetailDist01(float detailDist01)
	{
		DetailDist01 = detailDist01;
	}

	public void SetShadowDist01(float shadowDist01)
	{
		ShadowDist01 = shadowDist01;
		QualitySettings.shadowDistance = QualitySettingsExtended.ShadowDistanceRange.Lerp(shadowDist01);
	}

	public void SetGlobalFogColour(Color targetFogColour)
	{
		if ((bool)Fog && Fog.gameObject.activeInHierarchy)
		{
			Fog.globalFogColor = targetFogColour;
		}
	}

	public void SetVSyncAndFramerateLimit(bool useVSync, int maxFramerate)
	{
		bool flag = maxFramerate > 0;
		if (!useVSync)
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = (flag ? maxFramerate : int.MaxValue);
			return;
		}
		int vSyncCount = 1;
		if (flag)
		{
			vSyncCount = Mathf.RoundToInt((float)Screen.currentResolution.refreshRate / (float)maxFramerate);
			vSyncCount = Mathf.Clamp(vSyncCount, 1, 4);
		}
		QualitySettings.vSyncCount = vSyncCount;
		Application.targetFrameRate = 60;
	}

	public void BeginSpinControl()
	{
		if (IsCurrent<TankCamera>())
		{
			(currentCamera as TankCamera).BeginSpinControlMouse();
		}
		else if (IsCurrent<FramingCamera>())
		{
			(currentCamera as FramingCamera).BeginSpinControl();
		}
	}

	public void EndSpinControl()
	{
		if (IsCurrent<TankCamera>())
		{
			(currentCamera as TankCamera).EndSpinControlMouse();
		}
		else if (IsCurrent<FramingCamera>())
		{
			(currentCamera as FramingCamera).EndSpinControl();
		}
	}

	private void UpdateGraphicOption(GraphicOption option, bool enabled)
	{
		switch (option)
		{
		case GraphicOption.Fog:
			if (Fog != null)
			{
				Fog.enabled = enabled;
			}
			break;
		case GraphicOption.ScreenBlur:
			if (ScreenBlur != null)
			{
				ScreenBlur.enabled = enabled;
			}
			break;
		case GraphicOption.AA:
			if (AA != null)
			{
				AA.enabled = enabled;
			}
			break;
		case GraphicOption.DOF:
			if (DOF != null)
			{
				DOF.enabled = enabled;
			}
			break;
		case GraphicOption.AO:
			if (AO != null)
			{
				AO.enabled = enabled;
			}
			break;
		case GraphicOption.CA:
			if (CA != null)
			{
				CA.enabled = enabled;
			}
			break;
		case GraphicOption.Bloom:
			if (Bloom != null)
			{
				Bloom.enabled = enabled;
			}
			break;
		case GraphicOption.HDR:
			Singleton.camera.allowHDR = enabled;
			break;
		default:
			d.LogError($"UpdateGraphicOption - Unsupported graphics option {option}");
			break;
		}
		OnGraphicOptionChanged.Send(option, enabled);
	}

	private void SetClipDistInternal(float clipDistance)
	{
		if (Singleton.camera != null)
		{
			Singleton.camera.farClipPlane = clipDistance;
			if (Fog != null)
			{
				float startDistance = clipDistance * (float)m_FogDistancePercentage / 100f;
				Fog.startDistance = startDistance;
				Fog.heightDensity = 1f / QualitySettingsExtended.FogHeightScale;
			}
		}
	}

	private void EnableReflectionProbe(Mode obj)
	{
		m_ReflectionProbeUpdateEnabled = QualitySettingsExtended.EnableReflectionProbes;
	}

	private void DisableReflectionProbe(Mode obj)
	{
		m_ReflectionProbeUpdateEnabled = false;
	}

	private void UpdateSwitchHandheldMode(bool isHandheld)
	{
		SetForceDisableGraphicOption(GraphicOption.Fog, isHandheld);
		SetForceDisableGraphicOption(GraphicOption.DOF, isHandheld);
	}

	private void Awake()
	{
		cameras = (from c in GetComponents(typeof(Camera))
			select c as Camera).ToArray();
		d.Assert(!currentCamera || cameras.Contains(currentCamera), "unknown current mode");
		Camera[] array = cameras;
		for (int num = 0; num < array.Length; num++)
		{
			array[num].enabled = false;
		}
		initialPosition = Singleton.cameraTrans.position;
		initialOrientation = Singleton.cameraTrans.rotation;
		Fog = Singleton.camera.GetComponent<GlobalFog>();
		Sky = Singleton.camera.GetComponent<Skybox>();
		GammaCorrection = Singleton.camera.GetComponent<GammaCorrectionEffect>();
		SunShafts = Singleton.camera.GetComponent<SunShafts>();
		ScreenBlur = Singleton.camera.GetComponent<BlurOptimized>();
		OutlineEffect = Singleton.camera.GetComponent<OutlineEffect>();
		DamageFeedbackEffect = Singleton.camera.GetComponent<DamageFeedbackEffect>();
		TeleportEffect = Singleton.camera.GetComponent<TeleportEffect>();
		PostProcessing = Singleton.camera.GetComponent<PostProcessingBehaviour>();
		if ((bool)PostProcessing && (bool)PostProcessing.profile)
		{
			PostProcessing.profile = UnityEngine.Object.Instantiate(PostProcessing.profile);
			AA = PostProcessing.profile.antialiasing;
			DOF = PostProcessing.profile.depthOfField;
			AO = PostProcessing.profile.ambientOcclusion;
			CA = PostProcessing.profile.chromaticAberration;
			Bloom = PostProcessing.profile.bloom;
		}
		m_ReflectionProbe = Singleton.camera.GetComponentInChildren<ReflectionProbe>();
	}

	private void Start()
	{
		if ((bool)currentCamera)
		{
			currentCamera.enabled = true;
			Switch(currentCamera);
		}
		AntialiasingModel.Settings settings = AA.settings;
		settings.method = QualitySettingsExtended.AntialiasingMethod;
		AA.settings = settings;
		Application.targetFrameRate = 60;
		Singleton.Manager<ManGameMode>.inst.ModeStartEvent.Subscribe(EnableReflectionProbe);
		Singleton.Manager<ManGameMode>.inst.ModeFinishedEvent.Subscribe(DisableReflectionProbe);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.TankCameraDistanceOverrideRequest, OnTankCameraDistanceOverrideRequest);
		if (SKU.SwitchUI)
		{
			UpdateSwitchHandheldMode(ManNintendoSwitch.IsHandheldMode);
			ManNintendoSwitch.HandheldModeChangedEvent.Subscribe(UpdateSwitchHandheldMode);
		}
	}

	private void Update()
	{
		if (Singleton.Manager<DebugUtil>.inst.AreCheatsEnabled(DebugUtil.CheatType.Development) && Input.GetKey(KeyCode.Tab))
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				bool flag = !IsCurrent<DebugCamera>();
				SetForceDisableGraphicOption(GraphicOption.ScreenBlur, flag);
				if (flag)
				{
					Switch<DebugCamera>();
				}
				else
				{
					Singleton.Manager<ManGameMode>.inst.SetModeDefaultCamera();
				}
			}
			else if (Input.GetKeyDown(KeyCode.R))
			{
				CameraRotateForTrailer component = GetComponent<CameraRotateForTrailer>();
				if ((bool)component)
				{
					component.Toggle();
				}
			}
		}
		if (m_ReflectionProbeUpdateEnabled)
		{
			float cameraReflectionProbeUpdateDelay = QualitySettingsExtended.CameraReflectionProbeUpdateDelay;
			if (cameraReflectionProbeUpdateDelay >= 0f && Time.time - m_ReflectionProbeLastUpdate >= cameraReflectionProbeUpdateDelay)
			{
				RenderReflectionProbe();
			}
		}
	}

	private void OnTankCameraDistanceOverrideRequest(NetworkMessage netMsg)
	{
		TankCameraDistanceOverrideRequest tankCameraDistanceOverrideRequest = netMsg.ReadMessage<TankCameraDistanceOverrideRequest>();
		if (tankCameraDistanceOverrideRequest.enable)
		{
			TankCamera.inst.DistanceMaxOverride(tankCameraDistanceOverrideRequest.distance);
		}
		else
		{
			TankCamera.inst.DistanceMaxOverrideClear();
		}
	}
}
