#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManTechMaterialSwap : Singleton.Manager<ManTechMaterialSwap>
{
	[Serializable]
	public struct MatReplacePairs
	{
		[EnumArray(typeof(MatType))]
		public Material[] m_Materials;

		[EnumArray(typeof(MatType))]
		public bool[] m_ScrollMaterial;
	}

	[Serializable]
	public struct MatSwapGroup
	{
		[EnumArray(typeof(MatType))]
		public MatSwapInfo[] m_Materials;

		public FactionSubTypes m_Corp;

		public MatSwapInfo this[int key]
		{
			get
			{
				return m_Materials[key];
			}
			set
			{
				m_Materials[key] = value;
			}
		}
	}

	[Serializable]
	public struct MatSwapInfo
	{
		public Material m_Material;

		public bool m_Scroll;
	}

	public enum MaterialTypes : byte
	{
		Normal,
		Damage,
		Healing
	}

	public enum MaterialColour : byte
	{
		Normal,
		Damage,
		Healing,
		AntiGrav,
		Scavenge,
		OutOfShield,
		Clogged,
		Max
	}

	public enum MatType : byte
	{
		Default,
		Alpha,
		Ghost,
		Halloween
	}

	public struct GroupTypePair
	{
		public int Group;

		public int MaterialType;

		public GroupTypePair(int group, int matType)
		{
			Group = group;
			MaterialType = matType;
		}
	}

	private struct GroupTextureArray
	{
		public readonly Texture2D Albedo;

		public readonly Texture2D Metal;

		public readonly Texture2D Emissive;

		public readonly Texture2D Variable;

		public GroupTextureArray(Texture2D albedo, Texture2D metal, Texture2D emissive, Texture2D variable)
		{
			Albedo = albedo;
			Metal = metal;
			Emissive = emissive;
			Variable = variable;
		}
	}

	[SerializeField]
	[Header("Material Swap Info (Non-Atlassed)")]
	[Space(10f)]
	public List<MatSwapGroup> m_MaterialsToSwap;

	[SerializeField]
	private float m_ScrollSpeed = 1f;

	[SerializeField]
	private AnimationCurve m_AntiGravAnimCurve = new AnimationCurve();

	[SerializeField]
	private Color m_AntiGravColour = Color.blue;

	[SerializeField]
	private float m_AntiGravColourPhase = 3f;

	[SerializeField]
	[EnumArray(typeof(FactionSubTypes))]
	private float[] m_MinEmissivePerCorporation;

	public const int kMaxSkinsV = 8;

	private List<GroupTextureArray> m_GroupTextureArray = new List<GroupTextureArray>(16);

	private Dictionary<string, int> m_OriginalMaterialLookup = new Dictionary<string, int>();

	private Dictionary<Material, GroupTypePair> m_SharedMaterialDict = new Dictionary<Material, GroupTypePair>();

	private Dictionary<Material, GroupTypePair> m_UsedMaterialDict = new Dictionary<Material, GroupTypePair>();

	private List<Material>[][] m_UnusedMaterialsLookup;

	private List<MatReplacePairs> m_MaterialPairsInstances = new List<MatReplacePairs>();

	public Dictionary<int, Material> m_FinalCorpMaterials = new Dictionary<int, Material>();

	private Dictionary<int, float> m_MinEmissiveForCorp = new Dictionary<int, float>();

	private Texture2D m_BlockDamageCLUT;

	private float m_BlockDamageScaleV;

	private int m_ScrollingFrameNo;

	private Vector2 m_UVAnimationRate = Vector2.down;

	private Vector2 m_UVOffset = Vector2.zero;

	private List<Material> m_ScrollingMaterials = new List<Material>();

	public Color AntiGravMaterialColour => m_AntiGravColour;

	public float AntiGravMaterialAlpha { get; private set; }

	public float AntiGravAnimTime { get; private set; }

	public bool AntiGravAnimLooped { get; private set; }

	public Material GetMaterial(MatType materialType, Material currentMaterial)
	{
		bool isSharedMaterial = false;
		return GetMaterialPairInstance(materialType, currentMaterial, out isSharedMaterial);
	}

	public Material GetSharedDefaultMaterial(Material material)
	{
		Material result = null;
		if (material != null && m_OriginalMaterialLookup.TryGetValue(material.name, out var value))
		{
			result = m_MaterialPairsInstances[value].m_Materials[0];
		}
		return result;
	}

	public float GetMinEmissiveForCorporation(FactionSubTypes corp)
	{
		return m_MinEmissiveForCorp[(int)corp];
	}

	public float GetDamageColourFloat(MaterialColour damageColourValue)
	{
		return ((float)(int)damageColourValue + 0.5f) * m_BlockDamageScaleV;
	}

	private Material GetMaterialPairInstance(MatType materialType, Material currentMaterial, out bool isSharedMaterial)
	{
		int num;
		if (m_UsedMaterialDict.TryGetValue(currentMaterial, out var value))
		{
			num = value.Group;
			ReturnInstancedMaterial(num, value.MaterialType, currentMaterial);
		}
		else
		{
			if (!m_SharedMaterialDict.TryGetValue(currentMaterial, out value))
			{
				isSharedMaterial = false;
				return null;
			}
			num = value.Group;
		}
		isSharedMaterial = true;
		return m_MaterialPairsInstances[num].m_Materials[(uint)materialType];
	}

	private Material GetInstancedMaterial(int groupIndex, int matTypeID)
	{
		Material material = null;
		if (m_UnusedMaterialsLookup[groupIndex][matTypeID].Count > 0)
		{
			material = m_UnusedMaterialsLookup[groupIndex][matTypeID][0];
			m_UnusedMaterialsLookup[groupIndex][matTypeID].RemoveAt(0);
		}
		else
		{
			material = CreateMaterialDuplicate(m_MaterialPairsInstances[groupIndex].m_Materials[matTypeID]);
		}
		GroupTypePair value = new GroupTypePair(groupIndex, matTypeID);
		m_UsedMaterialDict.Add(material, value);
		if (m_MaterialsToSwap[groupIndex][matTypeID].m_Scroll)
		{
			m_ScrollingMaterials.Add(material);
		}
		return material;
	}

	private void ReturnInstancedMaterial(int groupIndex, int matTypeID, Material material)
	{
		m_UnusedMaterialsLookup[groupIndex][matTypeID].Add(material);
		m_UsedMaterialDict.Remove(material);
		if (m_MaterialsToSwap[groupIndex][matTypeID].m_Scroll)
		{
			m_ScrollingMaterials.Remove(material);
		}
	}

	private Material CreateMaterialDuplicate(Material sourceMat)
	{
		return new Material(sourceMat);
	}

	private Texture2D MakeCorpArrayTexture(Texture2D srcTexture, int numSkins, TextureFormat format)
	{
		if (srcTexture == null)
		{
			return null;
		}
		int width = srcTexture.width * (numSkins / 8 + 1);
		int height = srcTexture.height * Math.Min(numSkins, 8);
		return new Texture2D(width, height, format, mipChain: true)
		{
			mipMapBias = srcTexture.mipMapBias,
			filterMode = srcTexture.filterMode,
			wrapMode = srcTexture.wrapMode
		};
	}

	private void CopyCorpTexture(Texture2D destArray, Texture2D sourceTex, int wDest, int hDest, int skinIndex)
	{
		int num = 1;
		if (!sourceTex)
		{
			return;
		}
		int num2 = Math.Min(wDest, hDest);
		int width = sourceTex.width;
		int height = sourceTex.height;
		int num3 = skinIndex / 8 * wDest;
		int num4 = skinIndex % 8 * hDest;
		if (width == wDest && height == hDest)
		{
			for (int i = 0; num2 >> i >= num && i < sourceTex.mipmapCount; i++)
			{
				Color32[] pixels = sourceTex.GetPixels32(i);
				destArray.SetPixels32(num3 >> i, num4 >> i, wDest >> i, hDest >> i, pixels, i);
			}
			return;
		}
		d.Log($"Stitching texture for skin of non-standard size {width}x{height}");
		for (int j = 0; num2 >> j >= num && j < sourceTex.mipmapCount; j++)
		{
			Color32[] pixels2 = sourceTex.GetPixels32(j);
			int num5 = wDest >> j;
			int num6 = hDest >> j;
			int num7 = width >> j;
			int num8 = height >> j;
			Color32[] array = new Color32[num5 * num6];
			for (int k = 0; k < num5; k++)
			{
				for (int l = 0; l < num6; l++)
				{
					array[k + l * num5] = pixels2[k * num7 / num5 + l * num8 / num6 * num7];
				}
			}
			destArray.SetPixels32(num3 >> j, num4 >> j, wDest >> j, hDest >> j, array, j);
		}
	}

	private void CopyCorpArraySkinTextures(Texture2D albedoArray, Texture2D metalArray, Texture2D emissiveArray, Texture2D variableArray, List<SkinTextures> skins)
	{
		if ((bool)albedoArray)
		{
			for (int i = 0; i < skins.Count; i++)
			{
				CopyCorpTexture(albedoArray, skins[i].m_Albedo, skins[0].m_Albedo.width, skins[0].m_Albedo.height, i);
			}
		}
		if ((bool)metalArray)
		{
			for (int j = 0; j < skins.Count; j++)
			{
				CopyCorpTexture(metalArray, skins[j].m_Metal, skins[0].m_Metal.width, skins[0].m_Metal.height, j);
			}
		}
		if ((bool)emissiveArray)
		{
			for (int k = 0; k < skins.Count; k++)
			{
				CopyCorpTexture(emissiveArray, skins[k].m_Emissive, skins[0].m_Emissive.width, skins[0].m_Emissive.height, k);
			}
		}
		if ((bool)variableArray)
		{
			for (int l = 0; l < skins.Count; l++)
			{
				CopyCorpTexture(variableArray, skins[l].m_Variable, skins[0].m_Variable.width, skins[0].m_Variable.height, l);
			}
		}
		albedoArray.Compress(highQuality: false);
		metalArray.Compress(highQuality: false);
		emissiveArray.Compress(highQuality: false);
		variableArray.Compress(highQuality: false);
		albedoArray.Apply(updateMipmaps: false, makeNoLongerReadable: true);
		metalArray.Apply(updateMipmaps: false, makeNoLongerReadable: true);
		emissiveArray.Apply(updateMipmaps: false, makeNoLongerReadable: true);
		variableArray.Apply(updateMipmaps: false, makeNoLongerReadable: true);
	}

	public void RebuildCorpArrayTextures()
	{
		m_FinalCorpMaterials.Clear();
		for (int i = 0; i < m_MaterialPairsInstances.Count; i++)
		{
			int num = m_MaterialPairsInstances[i].m_Materials.Length;
			List<SkinTextures> corpSkinTextureInfos = Singleton.Manager<ManCustomSkins>.inst.GetCorpSkinTextureInfos(m_MaterialsToSwap[i].m_Corp);
			int count = corpSkinTextureInfos.Count;
			Texture2D texture2D = null;
			Texture2D texture2D2 = null;
			Texture2D texture2D3 = null;
			Texture2D texture2D4 = null;
			for (int j = 0; j < num; j++)
			{
				Material material = m_MaterialPairsInstances[i].m_Materials[j];
				Material material2 = m_MaterialsToSwap[i][j].m_Material;
				if (count > 1 && material != null && material2 != null)
				{
					material.EnableKeyword("_SKINS");
					if ((byte)j == 0)
					{
						texture2D = MakeCorpArrayTexture((Texture2D)material2.GetTexture("_MainTex"), count, TextureFormat.RGB24);
						texture2D2 = MakeCorpArrayTexture((Texture2D)material2.GetTexture("_MetallicGlossMap"), count, TextureFormat.RGBA32);
						texture2D3 = MakeCorpArrayTexture((Texture2D)material2.GetTexture("_EmissionMap"), count, TextureFormat.RGB24);
						texture2D4 = MakeCorpArrayTexture((Texture2D)material2.GetTexture("_VariableMap"), count, TextureFormat.RGB24);
						CopyCorpArraySkinTextures(texture2D, texture2D2, texture2D3, texture2D4, corpSkinTextureInfos);
						material.SetTexture("_MainTex", texture2D);
						material.SetTexture("_MetallicGlossMap", texture2D2);
						material.SetTexture("_EmissionMap", texture2D3);
						material.SetTexture("_VariableMap", texture2D4);
						material.SetFloat("_RcpNumSkinsU", 1f / (float)(count / 8 + 1));
						material.SetFloat("_RcpNumSkinsV", 1f / (float)Math.Min(count, 8));
						m_GroupTextureArray.Add(new GroupTextureArray(texture2D, texture2D2, texture2D3, texture2D4));
						m_FinalCorpMaterials.Add((int)m_MaterialsToSwap[i].m_Corp, material);
					}
					else if ((byte)j == 1)
					{
						material.SetTexture("_MainTex", texture2D);
						material.SetTexture("_MetallicGlossMap", texture2D2);
						material.SetTexture("_EmissionMap", texture2D3);
						material.SetTexture("_VariableMap", texture2D4);
						material.SetFloat("_RcpNumSkinsU", 1f / (float)(count / 8 + 1));
						material.SetFloat("_RcpNumSkinsV", 1f / (float)Math.Min(count, 8));
					}
				}
				m_MaterialPairsInstances[i].m_Materials[j] = material;
			}
		}
	}

	public void BuildCustomCorpArrayTextures(Dictionary<int, List<ModdedSkinDefinition>> corps)
	{
		foreach (KeyValuePair<int, List<ModdedSkinDefinition>> item in corps)
		{
			int key = item.Key;
			List<ModdedSkinDefinition> value = item.Value;
			int count = value.Count;
			if (!m_MinEmissiveForCorp.ContainsKey(key))
			{
				m_MinEmissiveForCorp.Add(key, 0f);
			}
			if (count != 0)
			{
				Material material = new Material(m_MaterialPairsInstances[0].m_Materials[0]);
				material.name = Singleton.Manager<ManMods>.inst.FindCorpShortName((FactionSubTypes)key);
				ModdedSkinDefinition moddedSkinDefinition = value[0];
				moddedSkinDefinition.SetFallbacks();
				Texture2D texture2D = MakeCorpArrayTexture(moddedSkinDefinition.m_Albedo, count, TextureFormat.RGB24);
				Texture2D texture2D2 = MakeCorpArrayTexture(moddedSkinDefinition.m_Combined, count, TextureFormat.RGBA32);
				Texture2D texture2D3 = MakeCorpArrayTexture(moddedSkinDefinition.m_Emissive, count, TextureFormat.RGB24);
				Texture2D texture2D4 = MakeCorpArrayTexture(moddedSkinDefinition.m_Variable, count, TextureFormat.RGB24);
				List<SkinTextures> list = new List<SkinTextures>(count);
				for (int i = 0; i < count; i++)
				{
					list.Add(new SkinTextures
					{
						m_Albedo = value[i].m_Albedo,
						m_Emissive = value[i].m_Emissive,
						m_Metal = value[i].m_Combined,
						m_Variable = value[i].m_Variable
					});
				}
				CopyCorpArraySkinTextures(texture2D, texture2D2, texture2D3, texture2D4, list);
				material.SetTexture("_MainTex", texture2D);
				material.SetTexture("_MetallicGlossMap", texture2D2);
				material.SetTexture("_EmissionMap", texture2D3);
				material.SetTexture("_VariableMap", texture2D4);
				material.SetFloat("_RcpNumSkinsU", 1f / (float)(count / 8 + 1));
				material.SetFloat("_RcpNumSkinsV", 1f / (float)Math.Min(count, 8));
				m_GroupTextureArray.Add(new GroupTextureArray(texture2D, texture2D2, texture2D3, texture2D4));
				m_FinalCorpMaterials.Add(key, material);
			}
		}
	}

	private void Start()
	{
		bool flag = false;
		string[] commandLineArgs = CommandLineReader.GetCommandLineArgs();
		for (int i = 0; i < commandLineArgs.Length; i++)
		{
			if (commandLineArgs[i] == "-noskins")
			{
				flag = true;
				break;
			}
		}
		m_UnusedMaterialsLookup = new List<Material>[m_MaterialsToSwap.Count][];
		for (int j = 0; j < m_MaterialsToSwap.Count; j++)
		{
			int num = m_MaterialsToSwap[j].m_Materials.Length;
			MatReplacePairs item = new MatReplacePairs
			{
				m_Materials = new Material[num]
			};
			m_UnusedMaterialsLookup[j] = new List<Material>[m_MaterialsToSwap[j].m_Materials.Length];
			Texture2D texture2D = null;
			Texture2D texture2D2 = null;
			Texture2D texture2D3 = null;
			Texture2D texture2D4 = null;
			List<SkinTextures> corpSkinTextureInfos = Singleton.Manager<ManCustomSkins>.inst.GetCorpSkinTextureInfos(m_MaterialsToSwap[j].m_Corp);
			int count = corpSkinTextureInfos.Count;
			for (int k = 0; k < num; k++)
			{
				if (!m_MaterialsToSwap[j][k].m_Material)
				{
					continue;
				}
				Material material = CreateMaterialDuplicate(m_MaterialsToSwap[j][k].m_Material);
				if (m_MaterialsToSwap[j][k].m_Scroll)
				{
					m_ScrollingMaterials.Add(material);
				}
				if (count > 1 && !flag)
				{
					material.EnableKeyword("_SKINS");
					if ((byte)k == 0)
					{
						texture2D = MakeCorpArrayTexture((Texture2D)material.GetTexture("_MainTex"), count, TextureFormat.RGB24);
						texture2D2 = MakeCorpArrayTexture((Texture2D)material.GetTexture("_MetallicGlossMap"), count, TextureFormat.RGBA32);
						texture2D3 = MakeCorpArrayTexture((Texture2D)material.GetTexture("_EmissionMap"), count, TextureFormat.RGB24);
						texture2D4 = MakeCorpArrayTexture((Texture2D)material.GetTexture("_VariableMap"), count, TextureFormat.RGB24);
						CopyCorpArraySkinTextures(texture2D, texture2D2, texture2D3, texture2D4, corpSkinTextureInfos);
						material.SetTexture("_MainTex", texture2D);
						material.SetTexture("_MetallicGlossMap", texture2D2);
						material.SetTexture("_EmissionMap", texture2D3);
						material.SetTexture("_VariableMap", texture2D4);
						material.SetFloat("_RcpNumSkinsU", 1f / (float)(count / 8 + 1));
						material.SetFloat("_RcpNumSkinsV", 1f / (float)Math.Min(count, 8));
						m_GroupTextureArray.Add(new GroupTextureArray(texture2D, texture2D2, texture2D3, texture2D4));
					}
					else if ((byte)k == 1)
					{
						material.SetTexture("_MainTex", texture2D);
						material.SetTexture("_MetallicGlossMap", texture2D2);
						material.SetTexture("_EmissionMap", texture2D3);
						material.SetTexture("_VariableMap", texture2D4);
						material.SetFloat("_RcpNumSkinsU", 1f / (float)(count / 8 + 1));
						material.SetFloat("_RcpNumSkinsV", 1f / (float)Math.Min(count, 8));
					}
				}
				m_SharedMaterialDict.Add(material, new GroupTypePair(j, k));
				item.m_Materials[k] = material;
				m_UnusedMaterialsLookup[j][k] = new List<Material>();
			}
			int corp = (int)m_MaterialsToSwap[j].m_Corp;
			if (!m_MinEmissiveForCorp.ContainsKey(corp))
			{
				m_MinEmissiveForCorp.Add(corp, m_MinEmissivePerCorporation[corp]);
			}
			m_MaterialPairsInstances.Add(item);
			if (!m_OriginalMaterialLookup.TryGetValue(m_MaterialsToSwap[j][0].m_Material.name, out var _))
			{
				m_OriginalMaterialLookup.Add(m_MaterialsToSwap[j][0].m_Material.name, j);
			}
			else
			{
				d.LogError("Name clash for materials: \"" + m_MaterialsToSwap[j][0].m_Material.name + "\"");
			}
		}
		m_BlockDamageCLUT = new Texture2D(1, 7);
		m_BlockDamageCLUT.SetPixel(0, 0, new Color(1f, 1f, 1f, 0f));
		m_BlockDamageCLUT.SetPixel(0, 1, Globals.inst.moduleDamageParams.DamageColour);
		m_BlockDamageCLUT.SetPixel(0, 2, Globals.inst.moduleDamageParams.HealColour);
		m_BlockDamageCLUT.SetPixel(0, 3, AntiGravMaterialColour);
		m_BlockDamageCLUT.SetPixel(0, 6, Globals.inst.moduleDamageParams.CloggedColour);
		m_BlockDamageCLUT.SetPixel(0, 4, Globals.inst.moduleDamageParams.ScavengeColour);
		m_BlockDamageCLUT.SetPixel(0, 5, Globals.inst.moduleDamageParams.OutOfShieldColour);
		m_BlockDamageCLUT.Apply();
		Shader.SetGlobalTexture("_DamageCLUT", m_BlockDamageCLUT);
		m_BlockDamageScaleV = 1f / (float)m_BlockDamageCLUT.height;
	}

	private void Update()
	{
		if (m_ScrollingFrameNo != Time.frameCount)
		{
			m_UVOffset += m_UVAnimationRate * m_ScrollSpeed * Time.deltaTime;
			for (int i = 0; i < m_ScrollingMaterials.Count; i++)
			{
				m_ScrollingMaterials[i].SetTextureOffset("_MainTex", m_UVOffset);
			}
			m_ScrollingFrameNo = Time.frameCount;
		}
		float num = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() % m_AntiGravColourPhase;
		AntiGravAnimLooped = num < AntiGravAnimTime;
		AntiGravAnimTime = num;
		AntiGravMaterialAlpha = m_AntiGravAnimCurve.Evaluate(num / m_AntiGravColourPhase);
		m_BlockDamageCLUT.SetPixel(0, 3, AntiGravMaterialColour.SetAlpha(AntiGravMaterialAlpha));
		m_BlockDamageCLUT.Apply();
	}
}
