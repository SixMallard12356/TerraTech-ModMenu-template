#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR;

namespace cakeslice;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
[ExecuteAlways]
public class OutlineEffect : MonoBehaviour
{
	[Serializable]
	public class OutlineData
	{
		[HideInInspector]
		public Material m_Material;

		public Gradient m_Gradient;

		public float m_TimeScale;
	}

	private static readonly int k_NUM_OUTLINE_CHANNELS = 3;

	private static OutlineEffect m_instance;

	private readonly List<Outline> m_Outlines = new List<Outline>();

	[Range(1f, 6f)]
	public float lineThickness = 1.25f;

	[Range(0f, 10f)]
	public float lineIntensity = 0.5f;

	[Range(0f, 1f)]
	public float fillAmount = 0.2f;

	public Gradient m_CanPaintSkinColour;

	public Gradient m_CannotPaintSkinColour;

	public bool backfaceCulling = true;

	[Header("These settings can affect performance!")]
	public bool cornerOutlines;

	[Header("Advanced settings")]
	public bool scaleWithScreenSize = true;

	[Range(0.1f, 0.9f)]
	public float alphaCutoff = 0.5f;

	public bool flipY;

	public Camera sourceCamera;

	[SerializeField]
	private OutlineData[] m_OutlineData = new OutlineData[k_NUM_OUTLINE_CHANNELS];

	private Material outlineEraseMaterial;

	[SerializeField]
	private Shader outlineShader;

	[SerializeField]
	private Shader outlineBufferShader;

	[HideInInspector]
	public Material outlineShaderMaterial;

	private float m_Time;

	private CommandBuffer commandBuffer;

	private int m_RenderTargetWidth;

	private int m_RenderTargetHeight;

	private int m_ShaderPropID_MainTex = Shader.PropertyToID("_MainTex");

	private int m_ShaderPropID_Culling = Shader.PropertyToID("_Culling");

	private int m_ShaderPropID_OutlineSource = Shader.PropertyToID("_OutlineSource");

	private int m_ShaderPropID_LineThicknessX = Shader.PropertyToID("_LineThicknessX");

	private int m_ShaderPropID_LineThicknessY = Shader.PropertyToID("_LineThicknessY");

	private int m_ShaderPropID_LineIntensity = Shader.PropertyToID("_LineIntensity");

	private int m_ShaderPropID_FillAmount = Shader.PropertyToID("_FillAmount");

	private int m_ShaderPropID_LineColours = Shader.PropertyToID("_LineColours");

	private int m_ShaderPropID_FlipY = Shader.PropertyToID("_FlipY");

	private int m_ShaderPropID_CornerOutlines = Shader.PropertyToID("_CornerOutlines");

	private int m_ShaderPropID_OutlineAlphaCutoff = Shader.PropertyToID("_OutlineAlphaCutoff");

	private List<Material> materialBuffer = new List<Material>();

	public static OutlineEffect Instance
	{
		get
		{
			if (object.Equals(m_instance, null))
			{
				return m_instance = UnityEngine.Object.FindObjectOfType(typeof(OutlineEffect)) as OutlineEffect;
			}
			return m_instance;
		}
	}

	private OutlineEffect()
	{
	}

	private Material GetMaterialFromID(int ID)
	{
		return m_OutlineData[ID].m_Material;
	}

	private Material CreateMaterial(Color emissionColor)
	{
		Material material = new Material(outlineBufferShader);
		material.SetColor("_Color", emissionColor);
		material.SetInt("_SrcBlend", 5);
		material.SetInt("_DstBlend", 10);
		material.SetInt("_ZWrite", 0);
		material.DisableKeyword("_ALPHATEST_ON");
		material.EnableKeyword("_ALPHABLEND_ON");
		material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
		material.renderQueue = 3000;
		material.SetInt(m_ShaderPropID_Culling, backfaceCulling ? 2 : 0);
		return material;
	}

	private void Awake()
	{
		m_instance = this;
	}

	private void Start()
	{
		m_OutlineData[2].m_Gradient = m_CanPaintSkinColour;
		CreateMaterials();
		UpdateMaterialsPublicProperties();
		if (sourceCamera == null)
		{
			sourceCamera = GetComponent<Camera>();
			if (sourceCamera == null)
			{
				sourceCamera = Camera.main;
			}
		}
		m_RenderTargetWidth = sourceCamera.pixelWidth;
		m_RenderTargetHeight = sourceCamera.pixelHeight;
		commandBuffer = new CommandBuffer();
		commandBuffer.name = "OutlineEffect";
		sourceCamera.AddCommandBuffer(CameraEvent.BeforeImageEffects, commandBuffer);
	}

	private void Update()
	{
		m_Time += Time.deltaTime;
	}

	private Material GetOutlineMaterialForBaseMaterial(Outline outline, Material material)
	{
		Material material2 = null;
		Texture texture = ((material != null && material.HasProperty(m_ShaderPropID_MainTex)) ? material.mainTexture : null);
		if (!(texture != null))
		{
			material2 = ((!outline.eraseRenderer) ? GetMaterialFromID(outline.color) : outlineEraseMaterial);
		}
		else
		{
			foreach (Material item in materialBuffer)
			{
				if ((object)item.mainTexture == texture)
				{
					if (outline.eraseRenderer && item.color == outlineEraseMaterial.color)
					{
						material2 = item;
					}
					else if (item.color == GetMaterialFromID(outline.color).color)
					{
						material2 = item;
					}
				}
			}
			if (material2 == null)
			{
				material2 = ((!outline.eraseRenderer) ? new Material(GetMaterialFromID(outline.color)) : new Material(outlineEraseMaterial));
				material2.SetInt(m_ShaderPropID_Culling, backfaceCulling ? 2 : 0);
				material2.mainTexture = texture;
				materialBuffer.Add(material2);
			}
		}
		return material2;
	}

	public void OnPreRender()
	{
		if (commandBuffer == null)
		{
			return;
		}
		commandBuffer.Clear();
		if (m_Outlines.Count == 0)
		{
			return;
		}
		UpdateMaterialsPublicProperties();
		commandBuffer.GetTemporaryRT(m_ShaderPropID_OutlineSource, Singleton.camera.scaledPixelWidth, Singleton.camera.scaledPixelHeight, 0, FilterMode.Bilinear, RenderTextureFormat.Default);
		commandBuffer.SetRenderTarget(m_ShaderPropID_OutlineSource);
		commandBuffer.ClearRenderTarget(clearDepth: true, clearColor: true, new Color(0f, 0f, 0f, 0f));
		for (int i = 0; i < m_Outlines.Count; i++)
		{
			Outline outline = m_Outlines[i];
			for (int j = 0; j < outline.MeshData.Count; j++)
			{
				Outline.MeshDataPair value = outline.MeshData[j];
				if (!(value.renderer != null) || !value.renderer.gameObject.activeInHierarchy)
				{
					continue;
				}
				MeshFilter mesh = value.mesh;
				SkinnedMeshRenderer skinnedMeshRenderer = value.skinnedMeshRenderer;
				Material material = value.m_CachedOutlineMaterial;
				if ((object)material == null)
				{
					material = (value.m_CachedOutlineMaterial = GetOutlineMaterialForBaseMaterial(outline, value.renderer.sharedMaterial));
					outline.MeshData[j] = value;
				}
				commandBuffer.DrawRenderer(value.renderer, material, 0, 0);
				if (mesh.IsNotNull())
				{
					if (mesh.sharedMesh != null)
					{
						for (int k = 1; k < mesh.sharedMesh.subMeshCount; k++)
						{
							commandBuffer.DrawRenderer(value.renderer, material, k, 0);
						}
					}
					else
					{
						d.LogErrorFormat("Mesh Filter on object {0} has a null mesh!", mesh.gameObject.name);
					}
				}
				if (!skinnedMeshRenderer.IsNotNull())
				{
					continue;
				}
				if (skinnedMeshRenderer.sharedMesh != null)
				{
					d.AssertFormat(skinnedMeshRenderer.sharedMesh != null, "Skinned Mesh Renderer on object {0} has a null mesh!", skinnedMeshRenderer.gameObject.name);
					for (int l = 1; l < skinnedMeshRenderer.sharedMesh.subMeshCount; l++)
					{
						commandBuffer.DrawRenderer(value.renderer, material, l, 0);
					}
				}
				else
				{
					d.LogErrorFormat("Skinned Mesh Renderer on object {0} has a null mesh!", skinnedMeshRenderer.gameObject.name);
				}
			}
		}
		commandBuffer.Blit(m_ShaderPropID_OutlineSource, BuiltinRenderTextureType.CameraTarget, outlineShaderMaterial, 0);
		commandBuffer.ReleaseTemporaryRT(m_ShaderPropID_OutlineSource);
	}

	private void OnDestroy()
	{
		if (commandBuffer != null)
		{
			sourceCamera.RemoveCommandBuffer(CameraEvent.BeforeImageEffects, commandBuffer);
		}
		DestroyMaterials();
	}

	private void CreateMaterials()
	{
		outlineShaderMaterial = new Material(outlineShader);
		outlineShaderMaterial.hideFlags = HideFlags.HideAndDontSave;
		outlineEraseMaterial = CreateMaterial(new Color(0f, 0f, 0f, 0f));
		for (int i = 0; i < k_NUM_OUTLINE_CHANNELS; i++)
		{
			int num = 255 << i * 8;
			float r = (float)((num & 0xFF0000) >> 16) / 255f;
			float g = (float)((num & 0xFF00) >> 8) / 255f;
			float b = (float)(num & 0xFF) / 255f;
			m_OutlineData[i].m_Material = CreateMaterial(new Color(r, g, b));
		}
	}

	private void DestroyMaterials()
	{
		foreach (Material item in materialBuffer)
		{
			UnityEngine.Object.DestroyImmediate(item);
		}
		materialBuffer.Clear();
		UnityEngine.Object.DestroyImmediate(outlineShaderMaterial);
		UnityEngine.Object.DestroyImmediate(outlineEraseMaterial);
		OutlineData[] outlineData = m_OutlineData;
		foreach (OutlineData obj in outlineData)
		{
			UnityEngine.Object.DestroyImmediate(obj.m_Material);
			obj.m_Material = null;
		}
		outlineShaderMaterial = null;
		outlineEraseMaterial = null;
	}

	public void SetSkinPaintingColour(bool canPaint)
	{
		Gradient gradient = (canPaint ? m_CanPaintSkinColour : m_CannotPaintSkinColour);
		if (m_OutlineData[2].m_Gradient != gradient)
		{
			m_OutlineData[2].m_Gradient = gradient;
			UpdateMaterialsPublicProperties();
		}
	}

	public void UpdateMaterialsPublicProperties()
	{
		if (!outlineShaderMaterial)
		{
			return;
		}
		float num = 1f;
		if (scaleWithScreenSize)
		{
			num = (float)Screen.height / 360f;
		}
		if (scaleWithScreenSize && num < 1f)
		{
			if (XRSettings.isDeviceActive && sourceCamera.stereoTargetEye != StereoTargetEyeMask.None)
			{
				outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessX, 0.001f * (1f / (float)XRSettings.eyeTextureWidth) * 1000f);
				outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessY, 0.001f * (1f / (float)XRSettings.eyeTextureHeight) * 1000f);
			}
			else
			{
				outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessX, 0.001f * (1f / (float)Screen.width) * 1000f);
				outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessY, 0.001f * (1f / (float)Screen.height) * 1000f);
			}
		}
		else if (XRSettings.isDeviceActive && sourceCamera.stereoTargetEye != StereoTargetEyeMask.None)
		{
			outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessX, num * (lineThickness / 1000f) * (1f / (float)XRSettings.eyeTextureWidth) * 1000f);
			outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessY, num * (lineThickness / 1000f) * (1f / (float)XRSettings.eyeTextureHeight) * 1000f);
		}
		else
		{
			outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessX, num * (lineThickness / 1000f) * (1f / (float)Screen.width) * 1000f);
			outlineShaderMaterial.SetFloat(m_ShaderPropID_LineThicknessY, num * (lineThickness / 1000f) * (1f / (float)Screen.height) * 1000f);
		}
		outlineShaderMaterial.SetFloat(m_ShaderPropID_LineIntensity, lineIntensity);
		outlineShaderMaterial.SetFloat(m_ShaderPropID_FillAmount, fillAmount);
		Color[] array = new Color[k_NUM_OUTLINE_CHANNELS];
		for (int i = 0; i < k_NUM_OUTLINE_CHANNELS; i++)
		{
			float time = m_Time * m_OutlineData[i].m_TimeScale % 1f;
			array[i] = m_OutlineData[i].m_Gradient.Evaluate(time);
		}
		outlineShaderMaterial.SetColorArray(m_ShaderPropID_LineColours, array);
		if (flipY)
		{
			outlineShaderMaterial.SetInt(m_ShaderPropID_FlipY, 1);
		}
		else
		{
			outlineShaderMaterial.SetInt(m_ShaderPropID_FlipY, 0);
		}
		if (cornerOutlines)
		{
			outlineShaderMaterial.SetInt(m_ShaderPropID_CornerOutlines, 1);
		}
		else
		{
			outlineShaderMaterial.SetInt(m_ShaderPropID_CornerOutlines, 0);
		}
		Shader.SetGlobalFloat(m_ShaderPropID_OutlineAlphaCutoff, alphaCutoff);
	}

	public void AddOutline(Outline outline)
	{
		if (m_Outlines.Count == 0)
		{
			base.enabled = true;
			m_Time = Time.time;
		}
		m_Outlines.Add(outline);
	}

	public void RemoveOutline(Outline outline)
	{
		m_Outlines.Remove(outline);
		if (m_Outlines.Count == 0)
		{
			commandBuffer?.Clear();
			base.enabled = false;
		}
	}
}
