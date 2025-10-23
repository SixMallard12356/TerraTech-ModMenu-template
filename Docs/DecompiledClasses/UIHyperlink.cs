#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(TextMeshProUGUI))]
public class UIHyperlink : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	[SerializeField]
	private Color32 m_HoverColour = new Color(0.2f, 0.4f, 11f / 15f);

	private TextMeshProUGUI m_TextMesh;

	private Camera m_Camera;

	private Canvas m_Canvas;

	private int m_HoveredLink = -1;

	private List<Color32[]> m_OriginalCharColours = new List<Color32[]>();

	public static string ConvertLinkToTMProLinkCode(string linkURL = null, string linkText = null)
	{
		if (linkURL.NullOrEmpty() && linkText.NullOrEmpty())
		{
			return "";
		}
		if (linkText.NullOrEmpty())
		{
			linkText = linkURL;
		}
		return "<color=#3c78ff><u><link=\"" + linkURL + "\">" + linkText + "</link></u></color>";
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		int num = TMP_TextUtilities.FindIntersectingLink(m_TextMesh, Input.mousePosition, m_Camera);
		if (num != -1)
		{
			TMP_LinkInfo tMP_LinkInfo = m_TextMesh.textInfo.linkInfo[num];
			Application.OpenURL(tMP_LinkInfo.GetLinkID());
		}
	}

	private List<Color32[]> SetLinkToColour(int linkIndex, Func<int, int, Color32> colorForLinkAndVert, Camera camera)
	{
		TMP_LinkInfo tMP_LinkInfo = m_TextMesh.textInfo.linkInfo[linkIndex];
		List<Color32[]> list = new List<Color32[]>();
		for (int i = 0; i < tMP_LinkInfo.linkTextLength; i++)
		{
			int num = tMP_LinkInfo.linkTextfirstCharacterIndex + i;
			TMP_CharacterInfo tMP_CharacterInfo = m_TextMesh.textInfo.characterInfo[num];
			int materialReferenceIndex = tMP_CharacterInfo.materialReferenceIndex;
			int vertexIndex = tMP_CharacterInfo.vertexIndex;
			Color32[] colors = m_TextMesh.textInfo.meshInfo[materialReferenceIndex].colors32;
			list.Add(colors.ToArray());
			if (tMP_CharacterInfo.isVisible)
			{
				colors[vertexIndex] = colorForLinkAndVert(i, vertexIndex);
				colors[vertexIndex + 1] = colorForLinkAndVert(i, vertexIndex + 1);
				colors[vertexIndex + 2] = colorForLinkAndVert(i, vertexIndex + 2);
				colors[vertexIndex + 3] = colorForLinkAndVert(i, vertexIndex + 3);
			}
		}
		m_TextMesh.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
		return list;
	}

	private void UpdateHoverHighlight()
	{
		if (m_Canvas == null)
		{
			m_Canvas = m_TextMesh.canvas;
			if ((bool)m_Canvas)
			{
				if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
				{
					m_Camera = null;
				}
				else
				{
					m_Camera = m_Canvas.worldCamera;
				}
			}
		}
		int num = (TMP_TextUtilities.IsIntersectingRectTransform(m_TextMesh.rectTransform, Input.mousePosition, m_Camera) ? TMP_TextUtilities.FindIntersectingLink(m_TextMesh, Input.mousePosition, m_Camera) : (-1));
		if (m_HoveredLink != -1 && num != m_HoveredLink)
		{
			SetLinkToColour(m_HoveredLink, (int linkIdx, int vertIdx) => m_OriginalCharColours[linkIdx][vertIdx], m_Camera);
			m_OriginalCharColours.Clear();
			m_HoveredLink = -1;
		}
		if (num != -1 && num != m_HoveredLink)
		{
			m_HoveredLink = num;
			m_OriginalCharColours = SetLinkToColour(num, (int _linkIdx, int _vertIdx) => m_HoverColour, m_Camera);
		}
	}

	private void OnPool()
	{
		m_TextMesh = GetComponent<TextMeshProUGUI>();
		d.AssertFormat(m_TextMesh != null, "A component with UIHyperlink attached must also have a TextMeshProUGUI component");
	}

	private void Update()
	{
		UpdateHoverHighlight();
	}
}
