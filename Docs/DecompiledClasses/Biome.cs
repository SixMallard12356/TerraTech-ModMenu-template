#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class Biome : ScriptableObject
{
	[Serializable]
	public class DetailLayer
	{
		public MapGenerator generator;

		public SceneryDistributor distributor;
	}

	[Serializable]
	public class SceneryDistributor
	{
		public enum SpawnModifier
		{
			Null,
			ScaleByGenerator,
			CullBySteepness
		}

		[Serializable]
		public struct SpawnModifierParams
		{
			public SpawnModifier type;

			public MapGenerator generator;

			public RangeFloat range;
		}

		[Serializable]
		public class PrefabGroup
		{
			public string weightingTag = "";

			public int weightingTagHash;

			public TerrainObject[] terrainObject = new TerrainObject[1];
		}

		[Serializable]
		public struct UpgradeRule
		{
			public float upgradeChance;

			public bool randomPrefab;

			public PrefabGroup upgrade;
		}

		public Vector2 layer0Translation;

		public float layer0Rotation;

		public float layer0Scale = 1f;

		public MapFuncVoronoi.DistanceMethod layer0DistMethod = MapFuncVoronoi.DistanceMethod.Length2;

		public Vector2 layer1Translation;

		public float layer1Rotation;

		public float layer1Scale = 1f;

		public MapFuncVoronoi.DistanceMethod layer1DistMethod = MapFuncVoronoi.DistanceMethod.Length4;

		public float bandTolerance = 1f;

		public bool enableRegions = true;

		public SpawnModifierParams[] spawnModifiers;

		public PrefabGroup decoration;

		public PrefabGroup basic;

		public PrefabGroup[] variants;

		public float nonBasicThreshold = 0.5f;

		public UpgradeRule[] upgradeRules;

		public MapGenerator.LayerXForm layer0XForm;

		public MapGenerator.LayerXForm layer1XForm;
	}

	[Serializable]
	public class LightColour
	{
		public string lightName;

		public Color colour;
	}

	[Serializable]
	public class ImpactEffectLookup
	{
		public string projectileName;

		public Transform effectPrefab;
	}

	[SerializeField]
	private Color editorRenderColour = Color.green;

	[SerializeField]
	private MapGenerator heightMapGenerator;

	[SerializeField]
	private MapGenerator multiTextureGenerator;

	[SerializeField]
	[HideInInspector]
	private DetailLayer[] layers;

	[SerializeField]
	private TerrainLayer m_MainMaterialLayer;

	[SerializeField]
	private TerrainLayer m_AltMaterialLayer;

	[SerializeField]
	private Vector2 textureBlendSteepnessRange = new Vector2(0f, 0.05f);

	[SerializeField]
	private float textureBlendSteepnessWeighting = 0.5f;

	[SerializeField]
	private BiomeTODLightingParams m_DayLighting;

	[SerializeField]
	private BiomeTODLightingParams m_NightLighting;

	[SerializeField]
	private BiomeCloudParams m_CloudParams;

	[SerializeField]
	private ParticleSystem m_WeatherParticles;

	[SerializeField]
	private Color m_DustVFXColor;

	[SerializeField]
	private ImpactEffectLookup[] impactEffects;

	[SerializeField]
	private AudioClip m_Ambience;

	[SerializeField]
	private AudioClip[] m_MusicExplore;

	[SerializeField]
	private AudioClip[] m_MusicDanger;

	[SerializeField]
	private bool m_AllowLandmarks = true;

	[SerializeField]
	private bool m_AllowVendors = true;

	[SerializeField]
	private bool m_AllowStuntRamps;

	[SerializeField]
	private float surfaceFriction = 0.7f;

	[SerializeField]
	private BiomeTypes m_BiomeType;

	[SerializeField]
	private Texture2D m_MapTexture;

	private Dictionary<string, Transform> impactEffectLookup;

	public Color EditorRenderColour
	{
		get
		{
			return editorRenderColour;
		}
		set
		{
			editorRenderColour = value;
		}
	}

	public MapGenerator HeightMapGenerator => heightMapGenerator;

	public MapGenerator MultiTextureGenerator => multiTextureGenerator;

	public DetailLayer[] DetailLayers => layers;

	public TerrainLayer MainMaterialLayer => m_MainMaterialLayer;

	public TerrainLayer AltMaterialLayer => m_AltMaterialLayer;

	public Vector2 TextureBlendSteepnessRange => textureBlendSteepnessRange;

	public float TextureBlendSteepnessWeighting => textureBlendSteepnessWeighting;

	public BiomeTODLightingParams DayLighting => m_DayLighting;

	public BiomeTODLightingParams NightLighting => m_NightLighting;

	public TOD_CloudParameters CloudParams => m_CloudParams.Params;

	public ParticleSystem WeatherParticles => m_WeatherParticles;

	public Color DustVFXColor => m_DustVFXColor;

	public AudioClip Ambience => m_Ambience;

	public AudioClip[] ExploreMusic => m_MusicExplore;

	public AudioClip[] DangerMusic => m_MusicDanger;

	public bool AllowLandmarks => m_AllowLandmarks;

	public bool AllowVendors => m_AllowVendors;

	public bool AllowStuntRamps => m_AllowStuntRamps;

	public float SurfaceFriction => surfaceFriction;

	public BiomeTypes BiomeType => m_BiomeType;

	public Texture2D MapTexture => m_MapTexture;

	public Transform GetImpactPrefab(string projectileName)
	{
		if (this.impactEffectLookup == null)
		{
			this.impactEffectLookup = new Dictionary<string, Transform>();
			ImpactEffectLookup[] array = impactEffects;
			foreach (ImpactEffectLookup impactEffectLookup in array)
			{
				this.impactEffectLookup.Add(impactEffectLookup.projectileName, impactEffectLookup.effectPrefab);
			}
		}
		Transform value = null;
		this.impactEffectLookup.TryGetValue(projectileName, out value);
		return value;
	}

	private void OnValidate()
	{
		if (m_MapTexture != null)
		{
			d.Assert(m_MapTexture.isReadable, m_MapTexture.name + " was not readable! ManMap requires mapTextures to be readable.");
			d.Assert(m_MapTexture.width == 64 && m_MapTexture.height == 64, "Map texture " + m_MapTexture.name + " was of unexpected size. Currently, ManMap expects textures at 64x64 for a 1:1 copy!");
		}
	}
}
