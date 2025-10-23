#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
	private class BlockMatConfigProperties
	{
		private Dictionary<int, VariableColorOverrides> m_RegisteredRendererOverrides;

		private Vector2 m_SkinValueUV;

		private float m_DamageValue;

		private bool m_IsDamaged;

		private bool m_IsAntiGrav;

		private bool m_IsClogged;

		private bool m_IsNight;

		private float m_MinEmitScale;

		private float m_EmitValue;

		private static List<Renderer> _s_FilteredRenderersCache;

		public Vector2 SkinValueUVs
		{
			get
			{
				return m_SkinValueUV;
			}
			set
			{
				m_SkinValueUV = value;
			}
		}

		public float DamageValue
		{
			get
			{
				return m_DamageValue;
			}
			set
			{
				m_DamageValue = value;
			}
		}

		public bool IsDamaged
		{
			get
			{
				return m_IsDamaged;
			}
			set
			{
				m_IsDamaged = value;
			}
		}

		public bool IsAntiGrav
		{
			get
			{
				return m_IsAntiGrav;
			}
			set
			{
				m_IsAntiGrav = value;
			}
		}

		public bool IsClogged
		{
			get
			{
				return m_IsClogged;
			}
			set
			{
				m_IsClogged = value;
			}
		}

		public bool IsNight
		{
			get
			{
				return m_IsNight;
			}
			set
			{
				m_IsNight = value;
				RefreshEmitValue();
			}
		}

		public float MinEmitScale
		{
			get
			{
				return m_MinEmitScale;
			}
			set
			{
				m_MinEmitScale = value;
				RefreshEmitValue();
			}
		}

		public float EmitValue
		{
			get
			{
				return m_EmitValue;
			}
			set
			{
				m_EmitValue = value;
				RefreshEmitValue();
			}
		}

		private void RefreshEmitValue()
		{
			m_EmitValue = Mathf.Max(MinEmitScale, m_IsNight ? 1f : 0f);
		}

		public BlockMatConfigProperties()
		{
			ClearChanges();
		}

		public void SetSkin(int index)
		{
			SkinValueUVs = new Vector2(index / 8, index % 8);
		}

		public void SetDamageColour(ManTechMaterialSwap.MaterialColour type)
		{
			DamageValue = Singleton.Manager<ManTechMaterialSwap>.inst.GetDamageColourFloat(type);
		}

		public void RegisterRendererOverride(Renderer renderer, Color colorOverride, Color emissionColorOverride)
		{
			m_RegisteredRendererOverrides[renderer.GetInstanceID()] = new VariableColorOverrides
			{
				m_VariableColorOverride = colorOverride,
				m_VariableEmissionColorOverride = emissionColorOverride
			};
		}

		public void ClearRendererOverride(Renderer renderer)
		{
			m_RegisteredRendererOverrides[renderer.GetInstanceID()] = VariableColorOverrides.empty;
		}

		internal void ApplyToSpecificRenderers(List<Renderer> renderers)
		{
			if (_s_FilteredRenderersCache == null)
			{
				_s_FilteredRenderersCache = new List<Renderer>();
			}
			_s_FilteredRenderersCache.Clear();
			for (int i = 0; i < renderers.Count; i++)
			{
				int instanceID = renderers[i].GetInstanceID();
				if (!m_RegisteredRendererOverrides.ContainsKey(instanceID))
				{
					_s_FilteredRenderersCache.Add(renderers[i]);
					continue;
				}
				VariableColorOverrides variableOverride = m_RegisteredRendererOverrides[instanceID];
				SetMaterialPropertiesOnRenderer(DamageValue, EmitValue, SkinValueUVs, variableOverride, renderers[i]);
			}
			if (_s_FilteredRenderersCache.Count > 0)
			{
				SetMaterialPropertiesOnRenderers(DamageValue, EmitValue, SkinValueUVs, VariableColorOverrides.empty, _s_FilteredRenderersCache);
			}
		}

		public void ClearChanges()
		{
			SetSkin(0);
			SetDamageColour(ManTechMaterialSwap.MaterialColour.Normal);
			EmitValue = MinEmitScale;
			IsDamaged = false;
			IsNight = false;
			IsAntiGrav = false;
			IsClogged = false;
			if (m_RegisteredRendererOverrides == null)
			{
				m_RegisteredRendererOverrides = new Dictionary<int, VariableColorOverrides>();
			}
			else
			{
				m_RegisteredRendererOverrides.Clear();
			}
		}
	}

	public struct VariableColorOverrides
	{
		public Color m_VariableColorOverride;

		public Color m_VariableEmissionColorOverride;

		public static VariableColorOverrides empty => new VariableColorOverrides
		{
			m_VariableColorOverride = default(Color),
			m_VariableEmissionColorOverride = default(Color)
		};

		public bool IsSet()
		{
			if (!(m_VariableColorOverride != default(Color)))
			{
				return m_VariableEmissionColorOverride != default(Color);
			}
			return true;
		}
	}

	private Damageable m_Damageable;

	private FactionSubTypes m_Corporation;

	private BlockMatConfigProperties m_BlockMaterialConfigProperties = new BlockMatConfigProperties();

	private List<Renderer> m_Renderers = new List<Renderer>();

	private Renderer m_ProxyRenderer;

	private Dictionary<Material, List<Renderer>> m_MatRendererLookup = new Dictionary<Material, List<Renderer>>();

	private bool m_CustomMaterialOverride;

	private static MaterialPropertyBlock s_matPropBlock;

	private static int s_matPropCoreFourId;

	private static int s_matPropBaseColour_EmissionId;

	private static int s_matPropBaseColour_BaseId;

	public const string k_ExcludeFromMaterialSwapperTag = "ExcludeFromMaterialSwapper";

	private float m_PulseEffectStartTime;

	private float m_EffectLevelCurrent;

	private ManTechMaterialSwap.MaterialTypes m_CurrentMaterialType;

	private ManTechMaterialSwap.MaterialColour m_CurrentMatColor;

	private ManTechMaterialSwap.MatType m_CurrentMatType;

	private bool m_PreExplodePulse;

	private MonoBehaviourEvent<MB_Update> m_MBUpdateEvent;

	public bool PreExplodePulse
	{
		get
		{
			return m_PreExplodePulse;
		}
		set
		{
			m_PreExplodePulse = value;
		}
	}

	public ManTechMaterialSwap.MaterialTypes CurrentMatType => m_CurrentMaterialType;

	public void RestoreDefaultMat()
	{
		m_CurrentMaterialType = ManTechMaterialSwap.MaterialTypes.Normal;
		SwapMaterialDamage(damaged: false);
	}

	public void SetCustomMaterialOverride(ManTechMaterialSwap.MatType customMatType)
	{
		m_CustomMaterialOverride = false;
		SwapMaterial(customMatType);
		m_CustomMaterialOverride = true;
	}

	public Material GetCurrentMaterial()
	{
		if (m_Renderers.Count <= 0)
		{
			return null;
		}
		return m_Renderers[0].sharedMaterial;
	}

	public bool IsCustomMaterialOverride()
	{
		return m_CustomMaterialOverride;
	}

	public ManTechMaterialSwap.MatType GetCurrentMatType()
	{
		return m_CurrentMatType;
	}

	public void RevertCustomMaterialOverride()
	{
		m_CustomMaterialOverride = false;
		SwapMaterial(ManTechMaterialSwap.MatType.Default);
	}

	public void ResetMaterialToDefault()
	{
		m_BlockMaterialConfigProperties.ClearChanges();
		m_CustomMaterialOverride = false;
		SwapMaterial(ManTechMaterialSwap.MatType.Default);
	}

	public void SetupMaterial(Damageable damageable, FactionSubTypes corporation = FactionSubTypes.NULL)
	{
		if (m_MBUpdateEvent != null)
		{
			m_MBUpdateEvent.Unsubscribe(OnUpdate);
		}
		m_Corporation = corporation;
		m_Damageable = damageable;
		m_Damageable.HealEvent.Subscribe(OnHealed);
		m_MBUpdateEvent = ((m_Damageable.Block != null) ? m_Damageable.Block.BlockUpdate : new MonoBehaviourEvent<MB_Update>(m_Damageable.gameObject));
		m_MBUpdateEvent.Subscribe(OnUpdate);
		m_BlockMaterialConfigProperties.MinEmitScale = Singleton.Manager<ManTechMaterialSwap>.inst.GetMinEmissiveForCorporation(m_Corporation);
		m_BlockMaterialConfigProperties.ClearChanges();
		d.Assert(m_ProxyRenderer == null, "Expect proxy renderer to be set after setupMaterial; otherwise it won't be part of the renderers list");
		RefreshRenderers();
	}

	public void RefreshRenderers()
	{
		ManTechMaterialSwap.MatType currentMatType = m_CurrentMatType;
		RevertCustomMaterialOverride();
		GetComponentsInChildren(includeInactive: true, m_Renderers);
		m_Renderers.RemoveAll(IsExcludedRenderer);
		Renderer proxyRenderer = null;
		if (m_ProxyRenderer != null)
		{
			proxyRenderer = m_ProxyRenderer;
			m_ProxyRenderer = null;
		}
		m_MatRendererLookup.Clear();
		for (int i = 0; i < m_Renderers.Count; i++)
		{
			Renderer renderer = m_Renderers[i];
			Material sharedDefaultMaterial = Singleton.Manager<ManTechMaterialSwap>.inst.GetSharedDefaultMaterial(renderer.sharedMaterial);
			if (sharedDefaultMaterial != null)
			{
				renderer.sharedMaterial = sharedDefaultMaterial;
				if (!m_MatRendererLookup.TryGetValue(sharedDefaultMaterial, out var value))
				{
					value = new List<Renderer>();
				}
				value.Add(renderer);
				m_MatRendererLookup[sharedDefaultMaterial] = value;
			}
		}
		if (m_Renderers.Count > 0)
		{
			m_BlockMaterialConfigProperties.ApplyToSpecificRenderers(m_Renderers);
		}
		else
		{
			d.LogWarningFormat(base.gameObject, "Object {0} does not have any valid Renderers. This makes MaterialSwapper unable to function.", base.name);
		}
		SetProxyRenderer(proxyRenderer);
		SetCustomMaterialOverride(currentMatType);
		bool IsExcludedRenderer(Renderer r)
		{
			if (m_Damageable != null && IsDamageableInParentInclInactive(m_Damageable.transform, r.transform))
			{
				return true;
			}
			if (r.gameObject.CompareTag("ExcludeFromMaterialSwapper"))
			{
				return true;
			}
			return false;
		}
	}

	public void SetProxyRenderer(Renderer renderer)
	{
		if (renderer != m_ProxyRenderer)
		{
			if (m_ProxyRenderer != null)
			{
				d.Assert(m_Renderers[m_Renderers.Count - 1] == m_ProxyRenderer, "Expect proxy renderer to exist in last slot here.. Something changed?");
				m_Renderers.RemoveAt(m_Renderers.Count - 1);
			}
			m_ProxyRenderer = renderer;
			if (m_ProxyRenderer != null)
			{
				m_Renderers.Add(m_ProxyRenderer);
			}
		}
		if ((bool)renderer)
		{
			Material sharedDefaultMaterial = Singleton.Manager<ManTechMaterialSwap>.inst.GetSharedDefaultMaterial(renderer.sharedMaterial);
			if (sharedDefaultMaterial != null)
			{
				renderer.sharedMaterial = sharedDefaultMaterial;
			}
		}
	}

	public bool HasMaterialPulse()
	{
		return m_PulseEffectStartTime > 0f;
	}

	public void StartMaterialPulse(ManTechMaterialSwap.MaterialTypes matType, ManTechMaterialSwap.MaterialColour colour)
	{
		m_PulseEffectStartTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		if (m_CurrentMaterialType != matType)
		{
			SwapMaterials(colour, 1f);
			m_CurrentMaterialType = matType;
		}
		else
		{
			SetEffectLevel(1f);
		}
		base.enabled = true;
	}

	public void ClearVariableColours(IEnumerable<Renderer> renderers)
	{
		foreach (Renderer renderer in renderers)
		{
			m_BlockMaterialConfigProperties.ClearRendererOverride(renderer);
		}
		RefreshMaterialColourAndConfig();
	}

	public void RegisterVariableColours(IEnumerable<Renderer> renderers, Color color, Color emissionColor)
	{
		foreach (Renderer renderer in renderers)
		{
			m_BlockMaterialConfigProperties.RegisterRendererOverride(renderer, color, emissionColor);
		}
		RefreshMaterialColourAndConfig();
	}

	public void SwapMaterialTime(bool night)
	{
		m_BlockMaterialConfigProperties.IsNight = night;
		RefreshMaterialColourAndConfig();
	}

	public void SwapMaterialClogged(bool clogged)
	{
		m_BlockMaterialConfigProperties.IsClogged = clogged;
		RefreshMaterialColourAndConfig();
	}

	public void SwapMaterialAntiGrav(bool antiGrav)
	{
		m_BlockMaterialConfigProperties.IsAntiGrav = antiGrav;
		RefreshMaterialColourAndConfig();
	}

	public void SwapMaterialDamage(bool damaged)
	{
		m_BlockMaterialConfigProperties.IsDamaged = damaged;
		RefreshMaterialColourAndConfig();
	}

	public void SetSkinIndex(int skinIndex, float blockSkinMinEmitScale)
	{
		m_BlockMaterialConfigProperties.SetSkin(skinIndex);
		m_BlockMaterialConfigProperties.MinEmitScale = blockSkinMinEmitScale;
		RefreshMaterialColourAndConfig();
	}

	public static void SetMaterialPropertiesOnRenderers(ManTechMaterialSwap.MaterialColour damageColour, float emissiveScale, int skinIndex, VariableColorOverrides variableOverride, List<Renderer> renderers = null)
	{
		SetMaterialPropertiesOnRenderers(Singleton.Manager<ManTechMaterialSwap>.inst.GetDamageColourFloat(damageColour), emissiveScale, new Vector2(skinIndex / 8, skinIndex % 8), variableOverride, renderers);
	}

	public static void SetMaterialPropertiesOnRenderer(ManTechMaterialSwap.MaterialColour damageColour, float emissiveScale, int skinIndex, VariableColorOverrides variableOverride, Renderer renderer = null)
	{
		SetMaterialPropertiesOnRenderer(Singleton.Manager<ManTechMaterialSwap>.inst.GetDamageColourFloat(damageColour), emissiveScale, new Vector2(skinIndex / 8, skinIndex % 8), variableOverride, renderer);
	}

	private static void SetMaterialPropertiesOnRenderers(float damageColorValue, float emissiveScale, Vector2 skinUVCoord, VariableColorOverrides variableOverride, List<Renderer> renderers = null)
	{
		if (renderers == null || renderers.Count == 0)
		{
			throw new ArgumentNullException("List of Renderers passed in must be a valid, non-null list!");
		}
		InitMaterialPropertyBlock(damageColorValue, emissiveScale, skinUVCoord, variableOverride);
		foreach (Renderer renderer in renderers)
		{
			renderer.SetPropertyBlock(s_matPropBlock);
		}
	}

	private static void SetMaterialPropertiesOnRenderer(float damageColorValue, float emissiveScale, Vector2 skinUVCoord, VariableColorOverrides variableOverride, Renderer renderer = null)
	{
		if (renderer == null)
		{
			throw new ArgumentNullException("Renderer passed in must be non-null!");
		}
		InitMaterialPropertyBlock(damageColorValue, emissiveScale, skinUVCoord, variableOverride);
		renderer.SetPropertyBlock(s_matPropBlock);
	}

	private static void InitMaterialPropertyBlock(float damageColorValue, float emissiveScale, Vector2 skinUVCoord, VariableColorOverrides variableOverride)
	{
		InitStatic();
		s_matPropBlock.Clear();
		s_matPropBlock.SetVector(s_matPropCoreFourId, new Vector4(damageColorValue, emissiveScale, skinUVCoord.x, skinUVCoord.y));
		bool flag = variableOverride.IsSet();
		s_matPropBlock.SetFloat("_VariableT", flag ? 1f : 0f);
		if (flag)
		{
			s_matPropBlock.SetColor(s_matPropBaseColour_BaseId, variableOverride.m_VariableColorOverride);
			s_matPropBlock.SetColor(s_matPropBaseColour_EmissionId, variableOverride.m_VariableEmissionColorOverride);
		}
	}

	private void OnHealed(float healAmount)
	{
		if (healAmount != 0f && m_Damageable.Health > 0f)
		{
			StartMaterialPulse(ManTechMaterialSwap.MaterialTypes.Healing, ManTechMaterialSwap.MaterialColour.Healing);
		}
	}

	private void SwapMaterial(ManTechMaterialSwap.MatType materialType)
	{
		if (m_CustomMaterialOverride)
		{
			return;
		}
		foreach (KeyValuePair<Material, List<Renderer>> item in m_MatRendererLookup)
		{
			Renderer renderer = item.Value[0];
			if (renderer != null)
			{
				Material material = Singleton.Manager<ManTechMaterialSwap>.inst.GetMaterial(materialType, renderer.sharedMaterial);
				SetMaterial(material, item.Value);
			}
			else
			{
				d.LogError("Prevented a crash by checking for null!");
			}
		}
		m_CurrentMatType = materialType;
	}

	private void SetMaterial(Material mat, List<Renderer> renderers)
	{
		if (mat != null)
		{
			for (int i = 0; i < renderers.Count; i++)
			{
				renderers[i].sharedMaterial = mat;
			}
		}
	}

	private void RefreshMaterialColourAndConfig()
	{
		m_BlockMaterialConfigProperties.SetDamageColour(m_BlockMaterialConfigProperties.IsDamaged ? m_CurrentMatColor : (m_BlockMaterialConfigProperties.IsAntiGrav ? ManTechMaterialSwap.MaterialColour.AntiGrav : (m_BlockMaterialConfigProperties.IsClogged ? ManTechMaterialSwap.MaterialColour.Clogged : ManTechMaterialSwap.MaterialColour.Normal)));
		if (m_Renderers.Count > 0)
		{
			m_BlockMaterialConfigProperties.ApplyToSpecificRenderers(m_Renderers);
		}
	}

	private static void InitStatic()
	{
		if (s_matPropBlock == null)
		{
			s_matPropBlock = new MaterialPropertyBlock();
			s_matPropCoreFourId = Shader.PropertyToID("_DamageEmitSkin");
			s_matPropBaseColour_EmissionId = Shader.PropertyToID("_VariableEmissionColorInstance");
			s_matPropBaseColour_BaseId = Shader.PropertyToID("_VariableEmissionColorInstance");
		}
	}

	private static bool IsDamageableInParentInclInactive(Transform top, Transform start)
	{
		if (top == start)
		{
			return false;
		}
		Transform parent = start.parent;
		while (parent != top)
		{
			if ((bool)parent.GetComponent<Damageable>() || (bool)parent.GetComponent<DigitalDisplay>())
			{
				return true;
			}
			parent = parent.parent;
		}
		return false;
	}

	private void SetEffectLevel(float level)
	{
		if (level != m_EffectLevelCurrent)
		{
			m_EffectLevelCurrent = level;
			SwapMaterialDamage(m_EffectLevelCurrent > 0f);
		}
	}

	private void SwapMaterials(ManTechMaterialSwap.MaterialColour color, float level)
	{
		m_CurrentMatColor = color;
		m_EffectLevelCurrent = level;
		SwapMaterialDamage(m_EffectLevelCurrent > 0f);
	}

	private bool IsAboveLowHealthThreshold()
	{
		float num = m_Damageable.MaxHealth * Globals.inst.moduleDamageParams.lowHealthFlashThreshold;
		return m_Damageable.Health >= num;
	}

	private void OnSpawn()
	{
		m_PulseEffectStartTime = 0f;
		m_PreExplodePulse = false;
		SetEffectLevel(0f);
		base.enabled = false;
	}

	private void OnRecycle()
	{
		if (m_CustomMaterialOverride)
		{
			RevertCustomMaterialOverride();
		}
		RestoreDefaultMat();
		base.enabled = false;
	}

	private void OnUpdate()
	{
		if (m_CurrentMaterialType != ManTechMaterialSwap.MaterialTypes.Normal)
		{
			float num;
			bool flag;
			if (m_CurrentMaterialType == ManTechMaterialSwap.MaterialTypes.Healing)
			{
				num = Globals.inst.moduleDamageParams.healFlashTime;
				flag = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() != m_PulseEffectStartTime;
			}
			else if (!m_PreExplodePulse)
			{
				num = Globals.inst.moduleDamageParams.flashPeriodBelowThreshold;
				flag = IsAboveLowHealthThreshold();
			}
			else
			{
				num = Globals.inst.moduleDamageParams.flashPeriodAboutToExplode;
				flag = false;
			}
			float effectLevel = Mathf.RoundToInt((Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() - m_PulseEffectStartTime) / num + 0.5f) & 1;
			SetEffectLevel(effectLevel);
			if (m_EffectLevelCurrent == 0f && flag)
			{
				m_PulseEffectStartTime = 0f;
				RestoreDefaultMat();
				base.enabled = false;
			}
		}
	}
}
