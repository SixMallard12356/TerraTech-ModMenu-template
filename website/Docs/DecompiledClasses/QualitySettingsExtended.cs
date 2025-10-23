#define UNITY_EDITOR
using System;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "QualitySettingsExtended", menuName = "Asset/QualitySettingsExtended")]
public class QualitySettingsExtended : ScriptableObject
{
	[Serializable]
	public struct PerQualitySettings
	{
		[SerializeField]
		[Header("Presentation")]
		[FormerlySerializedAs("m_LocalisedName")]
		public LocalisedString m_SettingName;

		[GlobalsRangeProp("m_DrawDistanceRange")]
		[Tooltip("Distance to draw the world to")]
		[SerializeField]
		[Header("Camera")]
		public float m_DefaultDrawDistance;

		[GlobalsRangeProp("m_DetailDistanceRange")]
		[Tooltip("Distance to draw world detail to (eg trees)")]
		[SerializeField]
		public float m_DefaultDetailDistance;

		[Tooltip("Distance to draw world shadows to. Replaces ShadowDistance in QualitySettings!")]
		[GlobalsRangeProp("m_ShadowDistanceRange")]
		[SerializeField]
		public float m_DefaultShadowDistance;

		[SerializeField]
		public bool m_DefaultAntialiasingEnabled;

		[SerializeField]
		public bool m_DefaultHBAOEnabled;

		[SerializeField]
		public bool m_DefaultDOFEnabled;

		[SerializeField]
		public bool m_DefaultHDREnabled;

		[SerializeField]
		[Tooltip("Height of the fog scale")]
		public float m_FogHeightScale;

		[Tooltip("Whether Render probes are enabled at all. Includes both Tile and Camera render probes")]
		[SerializeField]
		public bool m_EnableReflectionProbes;

		[Tooltip("Seconds in between each update of the reflection probe on the player camera (with -1=never)")]
		[SerializeField]
		public float m_CameraReflectionProbeUpdateDelay;

		[Tooltip("Number of times per game day the Tile reflection probes want to be updated")]
		[SerializeField]
		public int m_TileReflectionProbeRendersPerDay;

		[SerializeField]
		public AntialiasingModel.Method m_AntialiasingMethod;

		[Tooltip("Enables night time lighting on scenery objects and twilight effects")]
		[SerializeField]
		[Header("World")]
		public bool m_AllowLightsOnSceneryObjects;

		[SerializeField]
		[Tooltip("Limit number of loose blocks & chunks in the world. 0 = unlimited")]
		public int m_MaxLooseItemCount;

		[SerializeField]
		public int m_TerrainLODPixelError;

		[SerializeField]
		public int m_TerrainBaseMapDistance;

		[SerializeField]
		[Tooltip("Maximum quality type of shadows lights on Blocks can cast")]
		[Header("Blocks")]
		public LightShadows m_BlockLightShadowType;

		[SerializeField]
		[Tooltip("Use a simplified collider when blocks are attached to tech")]
		public bool m_UseSimplerCollidersForAttachedBlocks;

		[Header("Misc")]
		[SerializeField]
		public CasingSpawnMode m_ShellCasingSpawnMode;

		[Tooltip("Smoke particles on weapon firing")]
		[SerializeField]
		public bool m_DisableWeaponFireParticles;

		[SerializeField]
		[Tooltip("Dust and Spark particles on each wheel")]
		public bool m_WheelParticles;

		[SerializeField]
		public bool m_HQTyreTracks;

		[SerializeField]
		public bool m_UnimportantShadowCasters;

		[SerializeField]
		public bool m_FullQualitySky;

		[SerializeField]
		public float m_TODLightPositionUpdateInterval;

		[SerializeField]
		public float m_TODAmbientUpdateInterval;

		[SerializeField]
		public bool m_OverrideFixedTimeStep;

		[InspectorVisibilityControl("m_OverrideFixedTimeStep", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
		[SerializeField]
		public float m_FixedTimeStepOverride;

		[SerializeField]
		public bool m_OverrideMaximumDeltaTime;

		[InspectorVisibilityControl("m_OverrideMaximumDeltaTime", InspectorVisibilityControlAttribute.ComparisonType.Equals)]
		[SerializeField]
		public float m_MaximumDeltaTimeOverride;

		[SerializeField]
		public int m_MinimumAutoSaveTime;

		[SerializeField]
		public int m_ReducedHeightmapDetail;
	}

	public enum CasingSpawnMode
	{
		All,
		Throttled,
		None
	}

	[SerializeField]
	private PerQualitySettings[] m_PerQualitySettings;

	[HideInInspector]
	[SerializeField]
	private string[] m_QualitySettingNames;

	public static EventNoParams QualitySettingChangedEvent;

	private int m_CurrentUnityQualityLevel = -1;

	private int m_QualityLevelInLookup;

	public static float DefaultDrawDistance => inst.CurrentQualityLevel.m_DefaultDrawDistance;

	public static float DefaultDetailDistance => inst.CurrentQualityLevel.m_DefaultDetailDistance;

	public static float DefaultShadowDistance => inst.CurrentQualityLevel.m_DefaultShadowDistance;

	public static bool DefaultAntialiasingEnabled => inst.CurrentQualityLevel.m_DefaultAntialiasingEnabled;

	public static bool DefaultHBAOEnabled => inst.CurrentQualityLevel.m_DefaultHBAOEnabled;

	public static bool DefaultDOFEnabled => inst.CurrentQualityLevel.m_DefaultDOFEnabled;

	public static bool DefaultHDREnabled => inst.CurrentQualityLevel.m_DefaultHDREnabled;

	public static float FogHeightScale => inst.CurrentQualityLevel.m_FogHeightScale;

	public static bool EnableReflectionProbes => inst.CurrentQualityLevel.m_EnableReflectionProbes;

	public static float CameraReflectionProbeUpdateDelay => inst.CurrentQualityLevel.m_CameraReflectionProbeUpdateDelay;

	public static int TileReflectionProbeRendersPerDay => inst.CurrentQualityLevel.m_TileReflectionProbeRendersPerDay;

	public static AntialiasingModel.Method AntialiasingMethod => inst.CurrentQualityLevel.m_AntialiasingMethod;

	public static bool AllowLightsOnSceneryObjects => inst.CurrentQualityLevel.m_AllowLightsOnSceneryObjects;

	public static int MaxLooseItemCount => inst.CurrentQualityLevel.m_MaxLooseItemCount;

	public static int TerrainLODPixelError => inst.CurrentQualityLevel.m_TerrainLODPixelError;

	public static int TerrainBaseMapDistance => inst.CurrentQualityLevel.m_TerrainBaseMapDistance;

	public static LightShadows BlockLightShadowType => inst.CurrentQualityLevel.m_BlockLightShadowType;

	public static bool UseSimplerCollidersForAttachedBlocks => inst.CurrentQualityLevel.m_UseSimplerCollidersForAttachedBlocks;

	public static CasingSpawnMode ShellCasingSpawnMode => inst.CurrentQualityLevel.m_ShellCasingSpawnMode;

	public static bool DisableWeaponFireParticles => inst.CurrentQualityLevel.m_DisableWeaponFireParticles;

	public static bool WheelParticlesEnabled => inst.CurrentQualityLevel.m_WheelParticles;

	public static bool HQTyreTracks => inst.CurrentQualityLevel.m_HQTyreTracks;

	public static bool UnimportantShadowCasters => inst.CurrentQualityLevel.m_UnimportantShadowCasters;

	public static bool FullQualitySky => inst.CurrentQualityLevel.m_FullQualitySky;

	public static float TODLightPositionUpdateInterval => inst.CurrentQualityLevel.m_TODLightPositionUpdateInterval;

	public static float TODAmbientUpdateInterval => inst.CurrentQualityLevel.m_TODAmbientUpdateInterval;

	public static bool OverrideFixedTimeStep => inst.CurrentQualityLevel.m_OverrideFixedTimeStep;

	public static float FixedTimeStepOverride => inst.CurrentQualityLevel.m_FixedTimeStepOverride;

	public static bool OverrideMaximumDeltaTime => inst.CurrentQualityLevel.m_OverrideMaximumDeltaTime;

	public static float MaximumDeltaTimeOverride => inst.CurrentQualityLevel.m_MaximumDeltaTimeOverride;

	public static int MinimumAutoSaveTime => inst.CurrentQualityLevel.m_MinimumAutoSaveTime;

	public static MinMaxFloat ViewDistanceRange => Globals.inst.m_DrawDistanceRange;

	public static MinMaxFloat DetailDistanceRange => Globals.inst.m_DetailDistanceRange;

	public static MinMaxFloat ShadowDistanceRange => Globals.inst.m_ShadowDistanceRange;

	public static int ReducedHeightmapDetail => inst.CurrentQualityLevel.m_ReducedHeightmapDetail;

	public static int NumQualityLevels => QualitySettings.names.Length;

	private static QualitySettingsExtended inst => Singleton.instance.qualitySettingsExtended;

	private PerQualitySettings CurrentQualityLevel
	{
		get
		{
			int qualityLevel = QualitySettings.GetQualityLevel();
			if (qualityLevel != m_CurrentUnityQualityLevel)
			{
				m_CurrentUnityQualityLevel = qualityLevel;
				string text = QualitySettings.names[qualityLevel];
				m_QualityLevelInLookup = Array.IndexOf(m_QualitySettingNames, text);
				if (m_QualityLevelInLookup < 0)
				{
					d.LogError("Failed to find Unity Quality setting '" + text + "' in serialized quality settings in QualitySettingsExtended");
					m_QualityLevelInLookup = 0;
				}
			}
			return m_PerQualitySettings[m_QualityLevelInLookup];
		}
	}

	public static PerQualitySettings GetQualitySettings(int level)
	{
		string text = QualitySettings.names[level];
		int num = Array.IndexOf(inst.m_QualitySettingNames, text);
		if (num < 0)
		{
			d.LogError("Failed to find Unity Quality setting '" + text + "' in serialized quality settings in QualitySettingsExtended");
			num = 0;
		}
		return inst.m_PerQualitySettings[num];
	}
}
