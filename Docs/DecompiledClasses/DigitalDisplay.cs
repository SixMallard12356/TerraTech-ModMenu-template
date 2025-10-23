using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DigitalDisplay : MonoBehaviour
{
	protected enum DisplayTypes
	{
		DigitOffset,
		TextMeshPro
	}

	private class OffsetDigitRenderData
	{
		private Renderer Renderer;

		private MaterialPropertyBlock MatPropertyBlock;

		private Vector4 CachedMainTex_ST;

		private float CharacterUVSize;

		public OffsetDigitRenderData(Renderer renderer, float CharUVSize)
		{
			Renderer = renderer;
			CharacterUVSize = CharUVSize;
			CachedMainTex_ST = Renderer.sharedMaterial.GetVector(m_ScaleOffsetPropertyID);
			MatPropertyBlock = new MaterialPropertyBlock();
			Renderer.GetPropertyBlock(MatPropertyBlock);
			CachedMainTex_ST.x = CharUVSize;
			MatPropertyBlock.SetVector(m_ScaleOffsetPropertyID, CachedMainTex_ST);
			Renderer.SetPropertyBlock(MatPropertyBlock);
		}

		public void SetCharacterIndex(int idx)
		{
			CachedMainTex_ST.z = (float)idx * CharacterUVSize;
			CachedMainTex_ST.w = 0f;
			MatPropertyBlock.SetVector(m_ScaleOffsetPropertyID, CachedMainTex_ST);
			Renderer.SetPropertyBlock(MatPropertyBlock);
		}
	}

	[SerializeField]
	protected float m_AutoClearDelay;

	[SerializeField]
	protected string m_DefaultText = "";

	[SerializeField]
	protected string m_Format = "{0}";

	[SerializeField]
	protected DisplayTypes m_DisplayType;

	[Tooltip("m_CharMapping")]
	[SerializeField]
	protected string m_DigitOffsetCharMapping = "0123456789:.    ";

	private float m_autoClear = 3f;

	private string m_CurrentText = "";

	private float m_CharUVSize;

	private static int m_ScaleOffsetPropertyID = -1;

	private OffsetDigitRenderData[] m_DigitOffsetRenderersData;

	[HideInInspector]
	[SerializeField]
	private Renderer[] m_DigitOffsetRenderers;

	private TextMeshPro[] m_TextsMeshPro;

	private static List<Renderer> _s_TempRenderersInChildren = new List<Renderer>(4);

	public bool IsClear => m_CurrentText.Length == 0;

	public void SetValue(int v)
	{
		SetTextImpl(string.Format(m_Format, v));
		if (m_AutoClearDelay > 0f)
		{
			m_autoClear = m_AutoClearDelay;
			base.enabled = true;
		}
	}

	public void SetValue(float v)
	{
		SetTextImpl(string.Format(m_Format, v.ToString("0.00")));
		if (m_AutoClearDelay > 0f)
		{
			m_autoClear = m_AutoClearDelay;
			base.enabled = true;
		}
	}

	private void SetTextImpl(string text)
	{
		m_CurrentText = text;
		switch (m_DisplayType)
		{
		case DisplayTypes.DigitOffset:
			SetTextImpl_DigitOffset(text);
			break;
		case DisplayTypes.TextMeshPro:
			SetTextImpl_TextMeshPro(text);
			break;
		}
	}

	private void SetTextImpl_DigitOffset(string text)
	{
		int num = text.Length - 1;
		for (int num2 = m_DigitOffsetRenderersData.Length - 1; num2 >= 0; num2--)
		{
			int characterIndex = ((num >= 0) ? m_DigitOffsetCharMapping.IndexOf(text[num]) : m_DigitOffsetCharMapping.IndexOf(' '));
			m_DigitOffsetRenderersData[num2].SetCharacterIndex(characterIndex);
			num--;
		}
	}

	private void SetTextImpl_TextMeshPro(string text)
	{
		TextMeshPro[] textsMeshPro = m_TextsMeshPro;
		for (int i = 0; i < textsMeshPro.Length; i++)
		{
			textsMeshPro[i].text = text;
		}
	}

	private void PrePool()
	{
		if (m_ScaleOffsetPropertyID == -1)
		{
			m_ScaleOffsetPropertyID = Shader.PropertyToID("_MainTex_ST");
		}
		GetComponentsInChildren(includeInactive: true, _s_TempRenderersInChildren);
		_s_TempRenderersInChildren.RemoveAll((Renderer r) => r.gameObject == base.gameObject);
		_s_TempRenderersInChildren.Sort((Renderer a, Renderer b) => a.name.CompareTo(b.name));
		m_DigitOffsetRenderers = _s_TempRenderersInChildren.Where((Renderer r) => r.GetComponent<TextMeshPro>() == null).ToArray();
		_s_TempRenderersInChildren.Clear();
	}

	private void OnPool()
	{
		m_TextsMeshPro = GetComponentsInChildren<TextMeshPro>();
		m_CharUVSize = 1f / (float)m_DigitOffsetCharMapping.Length;
		m_DigitOffsetRenderersData = new OffsetDigitRenderData[m_DigitOffsetRenderers.Length];
		for (int i = 0; i < m_DigitOffsetRenderers.Length; i++)
		{
			m_DigitOffsetRenderersData[i] = new OffsetDigitRenderData(m_DigitOffsetRenderers[i], m_CharUVSize);
		}
		SetTextImpl(m_DefaultText);
	}

	private void OnSpawn()
	{
		base.enabled = false;
	}

	private void OnRecycle()
	{
		base.enabled = false;
		SetTextImpl(m_DefaultText);
	}

	private void Update()
	{
		m_autoClear -= Time.deltaTime;
		if (m_autoClear <= 0f)
		{
			m_autoClear = 0f;
			SetTextImpl(m_DefaultText);
			base.enabled = false;
		}
	}
}
