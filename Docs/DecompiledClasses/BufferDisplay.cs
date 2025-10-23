#define UNITY_EDITOR
using System;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BufferDisplay : MonoBehaviour
{
	public enum ColorTypes
	{
		None,
		Any,
		Positive,
		Negative
	}

	[EnumArray(typeof(ColorTypes))]
	[SerializeField]
	private Color[] displayColors;

	private Renderer m_DisplayRenderer;

	private int m_MainTexPropertyID = -1;

	private int m_EmissionMapPropertyID = -1;

	private MaterialPropertyBlock m_MaterialPropertyBlock;

	private byte[] m_ColorTypeData;

	private Texture2D m_Texture;

	private NativeArray<Color32> m_TextureData;

	private Texture2D m_DisplayTexture;

	public void Init()
	{
		m_DisplayRenderer = GetComponent<Renderer>();
		d.Assert(m_DisplayRenderer != null, $"Trying to initialise a buffer display without a valid Renderer assigned! Fix this on: {base.transform.GetTransformHeirarchyPath()}");
		m_MainTexPropertyID = Shader.PropertyToID("_MainTex");
		m_EmissionMapPropertyID = Shader.PropertyToID("_EmissionMap");
		m_MaterialPropertyBlock = new MaterialPropertyBlock();
		m_DisplayRenderer.GetPropertyBlock(m_MaterialPropertyBlock);
		m_MaterialPropertyBlock.SetColor("_Color", Color.white);
		m_MaterialPropertyBlock.SetColor("_EmissionColor", new Color(1f, 1f, 1f, 1f));
		m_DisplayRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
		m_ColorTypeData = new byte[256];
		for (int i = 0; i < m_ColorTypeData.Length; i++)
		{
			m_ColorTypeData[i] = 0;
		}
		Clear();
	}

	public void Clear()
	{
		if (m_DisplayRenderer != null)
		{
			ClearBufferTexture();
			for (int i = 0; i < m_ColorTypeData.Length; i++)
			{
				m_ColorTypeData[i] = 0;
			}
			m_DisplayTexture = Texture2D.blackTexture;
			UpdatePropertyBlockTexture();
		}
	}

	public void SetBufferSize(int size)
	{
		if (m_DisplayRenderer != null)
		{
			ClearBufferTexture();
			m_Texture = new Texture2D(1, size, TextureFormat.RGBA32, mipChain: false);
			m_Texture.wrapMode = TextureWrapMode.Clamp;
			m_Texture.filterMode = FilterMode.Point;
			m_TextureData = m_Texture.GetRawTextureData<Color32>();
			Array.Resize(ref m_ColorTypeData, size);
			for (int i = 0; i < size; i++)
			{
				SetColorAtIndex(i, (byte)0);
			}
			m_DisplayTexture = m_Texture;
			UpdatePropertyBlockTexture();
		}
	}

	public void ApplyTextureAndUpdatePropertyBlock()
	{
		if (!(m_Texture == null))
		{
			m_Texture.Apply(updateMipmaps: false);
			UpdatePropertyBlockTexture();
		}
	}

	public byte[] GetColorTypeData()
	{
		return m_ColorTypeData;
	}

	public void SetAndApplyFromColorTypeData(byte[] otherColorTypeData)
	{
		if (!(m_Texture == null))
		{
			if (m_ColorTypeData.Length != otherColorTypeData.Length)
			{
				SetBufferSize(otherColorTypeData.Length);
			}
			for (int i = 0; i < otherColorTypeData.Length; i++)
			{
				SetColorAtIndex(i, otherColorTypeData[i]);
			}
			ApplyTextureAndUpdatePropertyBlock();
		}
	}

	public void SetColorAtIndex(int index, ColorTypes colorType)
	{
		SetColorAtIndex(index, (byte)colorType);
	}

	private void SetColorAtIndex(int index, byte typeIndex)
	{
		m_TextureData[index] = displayColors[typeIndex];
		m_ColorTypeData[index] = typeIndex;
	}

	private void UpdatePropertyBlockTexture()
	{
		m_MaterialPropertyBlock.SetTexture(m_MainTexPropertyID, m_DisplayTexture);
		m_MaterialPropertyBlock.SetTexture(m_EmissionMapPropertyID, m_DisplayTexture);
		m_DisplayRenderer.SetPropertyBlock(m_MaterialPropertyBlock);
	}

	private void ClearBufferTexture()
	{
		if (m_Texture != null)
		{
			UnityEngine.Object.Destroy(m_Texture);
			m_Texture = null;
		}
	}
}
