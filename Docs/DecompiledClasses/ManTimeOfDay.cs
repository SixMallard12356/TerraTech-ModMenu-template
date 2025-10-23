#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ManTimeOfDay : Singleton.Manager<ManTimeOfDay>, Mode.IManagerModeEvents, IManTimeOfDay
{
	public struct DayNightBiomeColours
	{
		public float m_Weight;

		public BiomeTODLightingParams m_BlendedDayColours;

		public BiomeTODLightingParams m_BlendedNightColours;

		public DayNightBiomeColours(float weight, BiomeTODLightingParams dayColours, BiomeTODLightingParams nightColours)
		{
			m_Weight = weight;
			m_BlendedDayColours = dayColours;
			m_BlendedNightColours = nightColours;
		}
	}

	public struct BiomeCloudData
	{
		public float m_Weight;

		public TOD_CloudParameters m_CloudData;

		public BiomeCloudData(float weight, TOD_CloudParameters cloudData)
		{
			m_Weight = weight;
			m_CloudData = cloudData;
		}
	}

	[Serializable]
	public class TwilightValues
	{
		public float m_DuskStartVal;

		public float m_DuskEndVal;

		public float m_DawnStartVal;

		public float m_DawnEndVal;
	}

	[Serializable]
	public class MaterialEmissionTime
	{
		public Material m_Material;

		public TwilightValues m_TwilightValues;
	}

	private class SaveData
	{
		public bool m_ProgressTime;

		public DateTime m_DateTime;
	}

	[SerializeField]
	private TOD_Sky m_SkyDomePrefab;

	[SerializeField]
	private Light m_LightPrefab;

	[SerializeField]
	private bool m_UseFogLookahead = true;

	[SerializeField]
	private float m_BiomeBlendSpeed = 0.5f;

	[SerializeField]
	private bool m_UseCloudBlending;

	[SerializeField]
	private bool m_UseCloudLookahead = true;

	[SerializeField]
	private float m_CloudLookaheadDist = 20f;

	[SerializeField]
	private float m_CloudBlendSpeed = 0.01f;

	[SerializeField]
	private Material m_RandDMat;

	[SerializeField]
	private MaterialEmissionTime[] m_TwilightMaterialEmissionChanges;

	[SerializeField]
	[Tooltip("Length of Dusk (for effects) in seconds")]
	private float m_DuskLength = 180f;

	[Tooltip("Length of Dawn (for effects) in seconds")]
	[SerializeField]
	private float m_DawnLength = 180f;

	[SerializeField]
	private float m_FogBiomeForwardDist = 150f;

	[SerializeField]
	private bool m_UseSpaceCubeMap;

	public Event<bool> DayNightChangedEvent;

	private TOD_Sky m_Sky;

	private Light m_FallbackLight;

	private TOD_CloudQualityType m_DefaultQualityCloud;

	private TOD_SkyQualityType m_DefaultQualitySky;

	private TOD_MeshQualityType m_DefaultQualityMesh;

	private TOD_StarQualityType m_DefaultQualityStar;

	private TOD_ColorOutputType m_DefaultColourOutput;

	private bool m_BlendImmediately;

	private bool m_UpdateSunset;

	private bool m_UpdateSunrise;

	private bool m_UpdateDawnEmission;

	private bool m_UpdateDuskEmission;

	private float m_TwilightTimer;

	private float m_ClipTimer;

	private DayNightColours m_TargetDayColours;

	private DayNightColours m_TargetNightColours;

	private List<DayNightBiomeColours> m_CurrentBiomeColours;

	private List<DayNightBiomeColours> m_LookAheadBiomeColours;

	private List<BiomeCloudData> m_CloudData = new List<BiomeCloudData>();

	private TOD_CloudParameters m_TargetCloudParams = new TOD_CloudParameters();

	public bool LegacyNightTime => m_Sky.IsNight;

	public bool NightTime { get; private set; }

	public float DuskLength => m_DuskLength;

	public float DawnLength => m_DawnLength;

	public DayNightColours DayColours { get; private set; }

	public DayNightColours NightColours { get; private set; }

	public int GameDay
	{
		get
		{
			DateTime dateTime = m_Sky.Cycle.DateTime;
			return dateTime.Year * 365 + dateTime.DayOfYear;
		}
	}

	public int TimeOfDay => Mathf.FloorToInt(m_Sky.Cycle.Hour);

	public float TimeOfDayPrecise => m_Sky.Cycle.Hour;

	public static bool Initialized { get; private set; }

	public void ModeStart(ManSaveGame.State optionalLoadState)
	{
		if (optionalLoadState != null && optionalLoadState.GetSaveData<SaveData>(ManSaveGame.SaveDataJSONType.ManTimeOfDay, out var saveData) && saveData != null)
		{
			SetDate(saveData.m_DateTime.Year, saveData.m_DateTime.Month, saveData.m_DateTime.Day);
			SetTimeOfDay(saveData.m_DateTime.Hour, saveData.m_DateTime.Minute, saveData.m_DateTime.Second);
			EnableTimeProgression(saveData.m_ProgressTime);
			if (NightTime)
			{
				m_TwilightTimer = DuskLength;
				m_UpdateDuskEmission = true;
			}
			else
			{
				m_TwilightTimer = DawnLength;
				m_UpdateDawnEmission = true;
			}
		}
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Subscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.SubscribeToClientMessage(TTMsgType.TimeOfDayUpdate, OnTimeOfDayMessageReceived);
	}

	public void Save(ManSaveGame.State saveState)
	{
		SaveData saveData = new SaveData();
		saveData.m_ProgressTime = m_Sky.Components.Time.ProgressTime;
		saveData.m_DateTime = m_Sky.Cycle.DateTime;
		saveState.AddSaveData(ManSaveGame.SaveDataJSONType.ManTimeOfDay, saveData);
	}

	public void ModeExit()
	{
		Singleton.Manager<ManNetwork>.inst.OnPlayerAdded.Unsubscribe(OnPlayerAdded);
		Singleton.Manager<ManNetwork>.inst.UnsubscribeFromClientMessage(TTMsgType.TimeOfDayUpdate, OnTimeOfDayMessageReceived);
	}

	public bool UpdateBiomeColours(float dayTime, float nightTime, out DayNightColours dayColours, out DayNightColours nightColours)
	{
		if ((bool)Singleton.Manager<ManWorld>.inst.CurrentBiomeMap)
		{
			ManWorld.CachedBiomeBlendWeights currentBiomeWeights = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights;
			m_CurrentBiomeColours.Clear();
			for (int num = currentBiomeWeights.NumWeights - 1; num >= 0; num--)
			{
				float num2 = currentBiomeWeights.Weight(num) * (1f - currentBiomeWeights.SetPieceBiomeWeight);
				if (num2 > 0f)
				{
					Biome biome = currentBiomeWeights.Biome(num);
					DayNightBiomeColours item = new DayNightBiomeColours(num2, biome.DayLighting, biome.NightLighting);
					m_CurrentBiomeColours.Add(item);
				}
			}
			if (currentBiomeWeights.SetPieceBiomeWeight > 0f)
			{
				m_CurrentBiomeColours.Add(new DayNightBiomeColours(currentBiomeWeights.SetPieceBiomeWeight, currentBiomeWeights.SetPieceBiome.DayLighting, currentBiomeWeights.SetPieceBiome.NightLighting));
			}
			Vector3 scenePos = (Singleton.playerTank ? Singleton.playerPos : Singleton.cameraTrans.position);
			scenePos += Singleton.cameraTrans.forward.SetY(0f) * m_FogBiomeForwardDist;
			m_LookAheadBiomeColours.Clear();
			ManWorld.CachedBiomeBlendWeights biomeWeightsAtScenePosition = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(scenePos);
			for (int num3 = biomeWeightsAtScenePosition.NumWeights - 1; num3 >= 0; num3--)
			{
				float num4 = biomeWeightsAtScenePosition.Weight(num3) * (1f - biomeWeightsAtScenePosition.SetPieceBiomeWeight);
				if (num4 > 0f)
				{
					Biome biome2 = biomeWeightsAtScenePosition.Biome(num3);
					DayNightBiomeColours item2 = new DayNightBiomeColours(num4, biome2.DayLighting, biome2.NightLighting);
					m_LookAheadBiomeColours.Add(item2);
				}
			}
			if (biomeWeightsAtScenePosition.SetPieceBiomeWeight > 0f)
			{
				m_LookAheadBiomeColours.Add(new DayNightBiomeColours(biomeWeightsAtScenePosition.SetPieceBiomeWeight, biomeWeightsAtScenePosition.SetPieceBiome.DayLighting, biomeWeightsAtScenePosition.SetPieceBiome.NightLighting));
			}
		}
		bool result = false;
		if (m_CurrentBiomeColours.Count > 0)
		{
			SetColoursBlack(m_TargetDayColours);
			SetColoursBlack(m_TargetNightColours);
			for (int i = 0; i < m_CurrentBiomeColours.Count; i++)
			{
				AddBiomeColoursToTarget(m_TargetDayColours, m_CurrentBiomeColours[i].m_BlendedDayColours, m_CurrentBiomeColours[i].m_Weight, dayTime);
				AddBiomeColoursToTarget(m_TargetNightColours, m_CurrentBiomeColours[i].m_BlendedNightColours, m_CurrentBiomeColours[i].m_Weight, nightTime);
			}
			if (m_UseFogLookahead)
			{
				if (m_LookAheadBiomeColours.Count > 0)
				{
					m_TargetDayColours.FogColour = Color.black;
					m_TargetNightColours.FogColour = Color.black;
				}
				for (int j = 0; j < m_LookAheadBiomeColours.Count; j++)
				{
					m_TargetDayColours.FogColour = m_TargetDayColours.FogColour.Add(m_LookAheadBiomeColours[j].m_BlendedDayColours.FogColour.Evaluate(dayTime).ScaleRGBA(m_LookAheadBiomeColours[j].m_Weight));
					m_TargetNightColours.FogColour = m_TargetNightColours.FogColour.Add(m_LookAheadBiomeColours[j].m_BlendedNightColours.FogColour.Evaluate(nightTime).ScaleRGBA(m_LookAheadBiomeColours[j].m_Weight));
				}
			}
			LerpColours(m_TargetDayColours, DayColours, m_BlendImmediately);
			LerpColours(m_TargetNightColours, NightColours, m_BlendImmediately);
			result = true;
		}
		m_BlendImmediately = false;
		dayColours = DayColours;
		nightColours = NightColours;
		return result;
	}

	public void UpdateClouds()
	{
		if (!m_UseCloudBlending || !(Singleton.Manager<ManWorld>.inst.CurrentBiomeMap != null))
		{
			return;
		}
		m_CloudData.Clear();
		if (m_UseCloudLookahead)
		{
			Vector3 scenePos = (Singleton.playerTank ? Singleton.playerPos : Singleton.cameraTrans.position);
			scenePos += Singleton.cameraTrans.forward.SetY(0f) * m_CloudLookaheadDist;
			ManWorld.CachedBiomeBlendWeights biomeWeightsAtScenePosition = Singleton.Manager<ManWorld>.inst.GetBiomeWeightsAtScenePosition(scenePos);
			for (int num = biomeWeightsAtScenePosition.NumWeights - 1; num >= 0; num--)
			{
				float num2 = biomeWeightsAtScenePosition.Weight(num) * (1f - biomeWeightsAtScenePosition.SetPieceBiomeWeight);
				if (num2 > 0f)
				{
					Biome biome = biomeWeightsAtScenePosition.Biome(num);
					BiomeCloudData item = new BiomeCloudData(num2, biome.CloudParams);
					m_CloudData.Add(item);
				}
			}
			if (biomeWeightsAtScenePosition.SetPieceBiomeWeight > 0f)
			{
				m_CloudData.Add(new BiomeCloudData(biomeWeightsAtScenePosition.SetPieceBiomeWeight, biomeWeightsAtScenePosition.SetPieceBiome.CloudParams));
			}
		}
		else
		{
			ManWorld.CachedBiomeBlendWeights currentBiomeWeights = Singleton.Manager<ManWorld>.inst.CurrentBiomeWeights;
			for (int num3 = currentBiomeWeights.NumWeights - 1; num3 >= 0; num3--)
			{
				float num4 = currentBiomeWeights.Weight(num3) * (1f - currentBiomeWeights.SetPieceBiomeWeight);
				if (num4 > 0f)
				{
					Biome biome2 = currentBiomeWeights.Biome(num3);
					BiomeCloudData item2 = new BiomeCloudData(num4, biome2.CloudParams);
					m_CloudData.Add(item2);
				}
			}
			if (currentBiomeWeights.SetPieceBiomeWeight > 0f)
			{
				m_CloudData.Add(new BiomeCloudData(currentBiomeWeights.SetPieceBiomeWeight, currentBiomeWeights.SetPieceBiome.CloudParams));
			}
		}
		if (m_CloudData.Count > 0)
		{
			ClearCloudData();
			for (int i = 0; i < m_CloudData.Count; i++)
			{
				float weight = m_CloudData[i].m_Weight;
				TOD_CloudParameters cloudData = m_CloudData[i].m_CloudData;
				m_TargetCloudParams.Size += cloudData.Size * weight;
				m_TargetCloudParams.Opacity += cloudData.Opacity * weight;
				m_TargetCloudParams.Coverage += cloudData.Coverage * weight;
				m_TargetCloudParams.Sharpness += cloudData.Sharpness * weight;
				m_TargetCloudParams.Attenuation += cloudData.Attenuation * weight;
				m_TargetCloudParams.Saturation += cloudData.Saturation * weight;
				m_TargetCloudParams.Scattering += cloudData.Scattering * weight;
				m_TargetCloudParams.Brightness += cloudData.Brightness * weight;
			}
			LerpCloudData();
		}
	}

	private void LerpCloudData()
	{
		m_Sky.Clouds.Size = Mathf.Lerp(m_Sky.Clouds.Size, m_TargetCloudParams.Size, m_CloudBlendSpeed);
		m_Sky.Clouds.Opacity = Mathf.Lerp(m_Sky.Clouds.Opacity, m_TargetCloudParams.Opacity, m_CloudBlendSpeed);
		m_Sky.Clouds.Coverage = Mathf.Lerp(m_Sky.Clouds.Coverage, m_TargetCloudParams.Coverage, m_CloudBlendSpeed);
		m_Sky.Clouds.Sharpness = Mathf.Lerp(m_Sky.Clouds.Sharpness, m_TargetCloudParams.Sharpness, m_CloudBlendSpeed);
		m_Sky.Clouds.Attenuation = Mathf.Lerp(m_Sky.Clouds.Attenuation, m_TargetCloudParams.Attenuation, m_CloudBlendSpeed);
		m_Sky.Clouds.Saturation = Mathf.Lerp(m_Sky.Clouds.Saturation, m_TargetCloudParams.Saturation, m_CloudBlendSpeed);
		m_Sky.Clouds.Scattering = Mathf.Lerp(m_Sky.Clouds.Scattering, m_TargetCloudParams.Scattering, m_CloudBlendSpeed);
		m_Sky.Clouds.Brightness = Mathf.Lerp(m_Sky.Clouds.Brightness, m_TargetCloudParams.Brightness, m_CloudBlendSpeed);
	}

	private void ClearCloudData()
	{
		m_TargetCloudParams.Size = 0f;
		m_TargetCloudParams.Opacity = 0f;
		m_TargetCloudParams.Coverage = 0f;
		m_TargetCloudParams.Sharpness = 0f;
		m_TargetCloudParams.Attenuation = 0f;
		m_TargetCloudParams.Saturation = 0f;
		m_TargetCloudParams.Scattering = 0f;
		m_TargetCloudParams.Brightness = 0f;
	}

	public void BlendImmediately()
	{
		m_BlendImmediately = true;
	}

	public void SetDate(int year, int month, int day)
	{
		m_Sky.Cycle.Year = year;
		m_Sky.Cycle.Month = month;
		m_Sky.Cycle.Day = day;
	}

	public void SetTimeOfDay(int hour, int minute, int second, bool serverOnly = false)
	{
		float hour2 = m_Sky.Cycle.Hour;
		bool num = hour2 >= m_Sky.SunriseTime && hour2 < m_Sky.SunsetTime;
		m_Sky.Cycle.Hour = (float)hour + (float)minute / 60f + (float)second / 3600f;
		bool flag = m_Sky.Cycle.Hour >= m_Sky.SunriseTime && m_Sky.Cycle.Hour < m_Sky.SunsetTime;
		if (num != flag)
		{
			if (flag)
			{
				OnSunrise();
			}
			else
			{
				OnSunset();
			}
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer && !serverOnly)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	public void SkipDayOrNight()
	{
		bool isDay = m_Sky.IsDay;
		int hour = ((!isDay) ? 12 : 0);
		SetTimeOfDay(hour, 0, 0);
		if (isDay)
		{
			OnSunset();
		}
		else
		{
			OnSunrise();
		}
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	public void EnableTimeProgression(bool enable, bool clientOnly = false)
	{
		m_Sky.Components.Time.ProgressTime = enable;
		if (Singleton.Manager<ManNetwork>.inst.IsServer && !clientOnly)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	public void TogglePause()
	{
		m_Sky.Components.Time.ProgressTime = !m_Sky.Components.Time.ProgressTime;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	public void EnableSkyDome(bool enable)
	{
		d.Assert(m_Sky != null, "ASSERT: Sky is null!");
		d.Assert(m_Sky.Components != null, "ASSERT: Sky.Components is null!");
		d.Assert(m_Sky.Components.Camera != null, "ASSERT: Sky.Components.Camera is null!");
		d.Assert(m_FallbackLight != null, "ASSERT: FallbackLight is null!");
		m_Sky.gameObject.SetActive(enable);
		m_Sky.Components.Camera.enabled = enable;
		m_FallbackLight.gameObject.SetActive(!enable);
		m_Sky.TimeOfDayInterface = this;
		m_Sky.FogInterface = Singleton.Manager<CameraManager>.inst;
		if (!enable)
		{
			d.Assert(m_RandDMat != null, "m_RandDmat is null!");
			d.Assert(Singleton.camera != null, "Singleton.camera is null!");
			RenderSettings.skybox = m_RandDMat;
			Singleton.camera.clearFlags = CameraClearFlags.Skybox;
		}
	}

	private void Initialise()
	{
		m_Sky = UnityEngine.Object.Instantiate(m_SkyDomePrefab);
		m_FallbackLight = UnityEngine.Object.Instantiate(m_LightPrefab);
		m_FallbackLight.gameObject.SetActive(value: false);
		if (Singleton.Manager<CameraManager>.inst.SunShafts != null)
		{
			Singleton.Manager<CameraManager>.inst.SunShafts.sunTransform = m_Sky.Components.SunTransform;
		}
		m_Sky.Components.Time.OnSunset += OnSunset;
		m_Sky.Components.Time.OnSunrise += OnSunrise;
		TOD_Camera component = Singleton.cameraTrans.gameObject.GetComponent<TOD_Camera>();
		if ((bool)component)
		{
			component.enabled = true;
			component.SceneToWorldGetter = Singleton.Manager<ManWorld>.inst;
		}
		else
		{
			d.LogError("No TOD_Camera Component on Main Camera");
		}
		TOD_Rays component2 = Singleton.cameraTrans.gameObject.GetComponent<TOD_Rays>();
		if ((bool)component2)
		{
			component2.enabled = true;
			component2.sky = m_Sky;
		}
		else
		{
			d.LogError("No TOD_Rays Component on Main Camera");
		}
		TOD_Scattering component3 = Singleton.cameraTrans.gameObject.GetComponent<TOD_Scattering>();
		if ((bool)component3)
		{
			component3.enabled = true;
			component3.sky = m_Sky;
		}
		else
		{
			d.LogError("No TOD_Scattering Component on Main Camera");
		}
		TOD_Shadows component4 = Singleton.cameraTrans.gameObject.GetComponent<TOD_Shadows>();
		if ((bool)component4)
		{
			component4.enabled = true;
			component4.sky = m_Sky;
		}
		else
		{
			d.LogError("No TOD_Shadows Component on Main Camera");
		}
		DayColours = new DayNightColours();
		NightColours = new DayNightColours();
		m_TargetDayColours = new DayNightColours();
		m_TargetNightColours = new DayNightColours();
		m_CurrentBiomeColours = new List<DayNightBiomeColours>();
		m_LookAheadBiomeColours = new List<DayNightBiomeColours>();
		Initialized = true;
		m_Sky.m_UseTerraTechBiomeData = true;
		m_BlendImmediately = true;
		m_DefaultQualityCloud = m_Sky.CloudQuality;
		m_DefaultQualitySky = m_Sky.SkyQuality;
		m_DefaultQualityMesh = m_Sky.MeshQuality;
		m_DefaultQualityStar = m_Sky.StarQuality;
		m_DefaultColourOutput = m_Sky.ColorOutput;
		UpdateQuality();
		QualitySettingsExtended.QualitySettingChangedEvent.Subscribe(UpdateQuality);
	}

	private void UpdateQuality()
	{
		bool fullQualitySky = QualitySettingsExtended.FullQualitySky;
		m_Sky.CloudQuality = (fullQualitySky ? m_DefaultQualityCloud : TOD_CloudQualityType.Low);
		m_Sky.SkyQuality = (fullQualitySky ? m_DefaultQualitySky : TOD_SkyQualityType.PerVertex);
		m_Sky.MeshQuality = (fullQualitySky ? m_DefaultQualityMesh : TOD_MeshQualityType.Low);
		m_Sky.StarQuality = (fullQualitySky ? m_DefaultQualityStar : TOD_StarQualityType.Low);
		m_Sky.ColorOutput = (fullQualitySky ? m_DefaultColourOutput : TOD_ColorOutputType.Raw);
		m_Sky.Light.UpdateInterval = QualitySettingsExtended.TODLightPositionUpdateInterval;
		m_Sky.Ambient.UpdateInterval = QualitySettingsExtended.TODAmbientUpdateInterval;
	}

	private void OnTimeOfDayMessageReceived(NetworkMessage netMsg)
	{
		TimeOfDayUpdateMessage timeOfDayUpdateMessage = netMsg.ReadMessage<TimeOfDayUpdateMessage>();
		if (ManNetwork.IsHost)
		{
			return;
		}
		float hour = m_Sky.Cycle.Hour;
		bool num = hour >= m_Sky.SunriseTime && hour < m_Sky.SunsetTime;
		EnableTimeProgression(timeOfDayUpdateMessage.m_TimeProgression);
		m_Sky.Cycle.Hour = timeOfDayUpdateMessage.m_Hour;
		SetDate(timeOfDayUpdateMessage.m_Year, timeOfDayUpdateMessage.m_Month, timeOfDayUpdateMessage.m_Day);
		if (num != timeOfDayUpdateMessage.m_Daytime)
		{
			if (timeOfDayUpdateMessage.m_Daytime)
			{
				OnSunrise();
			}
			else
			{
				OnSunset();
			}
		}
	}

	public TimeOfDayUpdateMessage GetTimeOfDayUpdateMessage()
	{
		return new TimeOfDayUpdateMessage
		{
			m_Hour = m_Sky.Cycle.Hour,
			m_Year = (ushort)m_Sky.Cycle.Year,
			m_Month = (byte)m_Sky.Cycle.Month,
			m_Day = (byte)m_Sky.Cycle.Day,
			m_TimeProgression = m_Sky.Components.Time.ProgressTime,
			m_Daytime = (m_Sky.Cycle.Hour >= m_Sky.SunriseTime && m_Sky.Cycle.Hour < m_Sky.SunsetTime)
		};
	}

	private void OnPlayerAdded(NetPlayer player)
	{
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToClient(player.connectionToClient.connectionId, TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	private void SetColoursBlack(DayNightColours colours)
	{
		colours.SunMoonColour = new Color(0f, 0f, 0f, 0f);
		colours.LightColour = new Color(0f, 0f, 0f, 0f);
		colours.RayColour = new Color(0f, 0f, 0f, 0f);
		colours.SkyColour = new Color(0f, 0f, 0f, 0f);
		colours.CloudColour = new Color(0f, 0f, 0f, 0f);
		colours.FogColour = new Color(0f, 0f, 0f, 0f);
		colours.AmbientColour = new Color(0f, 0f, 0f, 0f);
		colours.DustVFXColour = new Color(0f, 0f, 0f, 0f);
	}

	private void AddBiomeColoursToTarget(DayNightColours targetColours, BiomeTODLightingParams biomeColours, float weight, float time)
	{
		targetColours.SunMoonColour = targetColours.SunMoonColour.Add(biomeColours.SunOrMoonColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.LightColour = targetColours.LightColour.Add(biomeColours.LightColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.RayColour = targetColours.RayColour.Add(biomeColours.RayColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.SkyColour = targetColours.SkyColour.Add(biomeColours.SkyColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.CloudColour = targetColours.CloudColour.Add(biomeColours.CloudColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.FogColour = targetColours.FogColour.Add(biomeColours.FogColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.AmbientColour = targetColours.AmbientColour.Add(biomeColours.AmbientColour.Evaluate(time).ScaleRGBA(weight));
		targetColours.DustVFXColour = targetColours.DustVFXColour.Add(biomeColours.DustVFXColour.Evaluate(time).ScaleRGBA(weight));
	}

	private void LerpColours(DayNightColours targetColours, DayNightColours currentColours, bool snap)
	{
		currentColours.SunMoonColour = LerpOrSnapColour(currentColours.SunMoonColour, targetColours.SunMoonColour, m_BiomeBlendSpeed, snap);
		currentColours.LightColour = LerpOrSnapColour(currentColours.LightColour, targetColours.LightColour, m_BiomeBlendSpeed, snap);
		currentColours.RayColour = LerpOrSnapColour(currentColours.RayColour, targetColours.RayColour, m_BiomeBlendSpeed, snap);
		currentColours.SkyColour = LerpOrSnapColour(currentColours.SkyColour, targetColours.SkyColour, m_BiomeBlendSpeed, snap);
		currentColours.CloudColour = LerpOrSnapColour(currentColours.CloudColour, targetColours.CloudColour, m_BiomeBlendSpeed, snap);
		currentColours.FogColour = LerpOrSnapColour(currentColours.FogColour, targetColours.FogColour, m_BiomeBlendSpeed, snap);
		currentColours.AmbientColour = LerpOrSnapColour(currentColours.AmbientColour, targetColours.AmbientColour, m_BiomeBlendSpeed, snap);
		currentColours.DustVFXColour = LerpOrSnapColour(currentColours.DustVFXColour, targetColours.DustVFXColour, m_BiomeBlendSpeed, snap);
	}

	private Color LerpOrSnapColour(Color from, Color to, float param, bool snap)
	{
		return Color.Lerp(from, to, snap ? 1f : param);
	}

	private void OnSunset()
	{
		NightTime = true;
		DayNightChangedEvent.Send(paramA: true);
		if (m_UseSpaceCubeMap)
		{
			m_Sky.Components.Space.SetActive(value: true);
		}
		m_UpdateSunset = true;
		m_UpdateDuskEmission = true;
		m_TwilightTimer = 0f;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	private void OnSunrise()
	{
		NightTime = false;
		DayNightChangedEvent.Send(paramA: false);
		m_UpdateSunrise = true;
		m_UpdateDawnEmission = true;
		m_TwilightTimer = 0f;
		if (Singleton.Manager<ManNetwork>.inst.IsServer)
		{
			Singleton.Manager<ManNetwork>.inst.SendToAllExceptHost(TTMsgType.TimeOfDayUpdate, GetTimeOfDayUpdateMessage());
		}
	}

	private void Start()
	{
		Singleton.DoOnceAfterStart(delegate
		{
			Initialise();
		});
	}

	private void Update()
	{
		if (!m_Sky.Components.Time.ProgressTime)
		{
			return;
		}
		if (m_UpdateSunset)
		{
			float num = m_Sky.Components.Time.DayLengthInMinutes * 60f / 24f;
			float num2 = Mathf.Clamp01(m_ClipTimer / num);
			if (num2 < 1f)
			{
				m_ClipTimer += Time.deltaTime;
			}
			else
			{
				m_ClipTimer = 0f;
				m_UpdateSunset = false;
			}
			if (m_UseSpaceCubeMap)
			{
				m_Sky.Components.SpaceMaterial.SetFloat("_Brightness", num2);
			}
		}
		if (m_UpdateSunrise)
		{
			float num3 = m_Sky.Components.Time.DayLengthInMinutes * 60f / 24f;
			float num4 = Mathf.Clamp01(1f - m_ClipTimer / num3);
			if (num4 > 0f)
			{
				m_ClipTimer += Time.deltaTime;
			}
			else
			{
				m_ClipTimer = 0f;
				m_UpdateSunrise = false;
				if (m_UseSpaceCubeMap)
				{
					m_Sky.Components.Space.SetActive(value: false);
				}
			}
			if (m_UseSpaceCubeMap)
			{
				m_Sky.Components.SpaceMaterial.SetFloat("_Brightness", num4);
			}
		}
		if (m_UpdateDuskEmission)
		{
			float num5 = Mathf.Clamp01(m_TwilightTimer / Singleton.Manager<ManTimeOfDay>.inst.DuskLength);
			for (int i = 0; i < m_TwilightMaterialEmissionChanges.Length; i++)
			{
				float duskStartVal = m_TwilightMaterialEmissionChanges[i].m_TwilightValues.m_DuskStartVal;
				float duskEndVal = m_TwilightMaterialEmissionChanges[i].m_TwilightValues.m_DuskEndVal;
				float num6 = Mathf.Lerp(duskStartVal, duskEndVal, num5);
				Color value = new Color(num6, num6, num6, 1f);
				m_TwilightMaterialEmissionChanges[i].m_Material.SetColor("_EmissionColor", value);
			}
			if (num5 >= 1f)
			{
				m_UpdateDuskEmission = false;
			}
			else
			{
				m_TwilightTimer += Time.deltaTime;
			}
		}
		if (m_UpdateDawnEmission)
		{
			float num7 = Mathf.Clamp01(m_TwilightTimer / Singleton.Manager<ManTimeOfDay>.inst.DuskLength);
			for (int j = 0; j < m_TwilightMaterialEmissionChanges.Length; j++)
			{
				float dawnStartVal = m_TwilightMaterialEmissionChanges[j].m_TwilightValues.m_DawnStartVal;
				float dawnEndVal = m_TwilightMaterialEmissionChanges[j].m_TwilightValues.m_DawnEndVal;
				float num8 = Mathf.Lerp(dawnStartVal, dawnEndVal, num7);
				Color value2 = new Color(num8, num8, num8, 1f);
				m_TwilightMaterialEmissionChanges[j].m_Material.SetColor("_EmissionColor", value2);
			}
			if (num7 >= 1f)
			{
				m_UpdateDawnEmission = false;
			}
			else
			{
				m_TwilightTimer += Time.deltaTime;
			}
		}
	}
}
