using System;
using System.Collections.Generic;
using UnityEngine;

public class VectorLineRenderer
{
	public struct Line
	{
		public Vector3 start;

		public Vector3 end;

		public float width;
	}

	private Vector3[] m_MeshVerts;

	private Vector2[] m_MeshUVs;

	private Color32[] m_MeshCols;

	private int[] m_Triangles;

	private GameObject m_GameObject;

	private CanvasRenderer m_CanvasRenderer;

	private RectTransform m_RectTransform;

	private int m_MaxLineCount;

	private int m_LastActiveLinesCount;

	private Mesh m_mesh;

	private static Vector3 v3zero = Vector3.zero;

	public VectorLineRenderer(Material lineMaterial, Canvas canvas)
	{
		m_MaxLineCount = 16;
		m_GameObject = new GameObject("Line renderer");
		m_GameObject.transform.SetParent(canvas.transform, worldPositionStays: false);
		m_CanvasRenderer = m_GameObject.AddComponent<CanvasRenderer>();
		m_CanvasRenderer.SetMaterial(lineMaterial, null);
		m_RectTransform = m_GameObject.AddComponent<RectTransform>();
		SetupTransform(m_RectTransform);
		m_mesh = new Mesh();
		m_mesh.MarkDynamic();
		m_MeshVerts = new Vector3[m_MaxLineCount * 4];
		m_MeshCols = new Color32[m_MaxLineCount * 4];
		m_MeshUVs = new Vector2[m_MaxLineCount * 4];
		SetUVsAndColours(0, m_MaxLineCount);
		m_mesh.vertices = m_MeshVerts;
		m_mesh.uv = m_MeshUVs;
		m_mesh.colors32 = m_MeshCols;
		m_mesh.SetIndices(GetTriangles(), MeshTopology.Triangles, 0, calculateBounds: false);
	}

	public void Draw3D(List<Line> lines)
	{
		int count = lines.Count;
		bool flag = ResizeToFit(count);
		Camera camera = Singleton.camera;
		Vector3 zero = Vector3.zero;
		int num = 0;
		for (int i = 0; i < count; i++)
		{
			Vector3 vector = camera.WorldToScreenPoint(lines[i].start);
			Vector3 vector2 = camera.WorldToScreenPoint(lines[i].end);
			zero.x = vector2.y - vector.y;
			zero.y = vector.x - vector2.x;
			if (zero.x != 0f || zero.y != 0f)
			{
				float num2 = lines[i].width * 0.5f;
				zero *= num2 / Mathf.Sqrt(zero.x * zero.x + zero.y * zero.y);
			}
			m_MeshVerts[num] = camera.ScreenToWorldPoint(vector - zero);
			m_MeshVerts[num + 1] = camera.ScreenToWorldPoint(vector2 - zero);
			m_MeshVerts[num + 2] = camera.ScreenToWorldPoint(vector2 + zero);
			m_MeshVerts[num + 3] = camera.ScreenToWorldPoint(vector + zero);
			num += 4;
		}
		int num3 = Mathf.Min(m_LastActiveLinesCount, m_MaxLineCount);
		for (int j = count; j < num3; j++)
		{
			m_MeshVerts[num] = v3zero;
			m_MeshVerts[num + 1] = v3zero;
			m_MeshVerts[num + 2] = v3zero;
			m_MeshVerts[num + 3] = v3zero;
			num += 4;
		}
		m_mesh.vertices = m_MeshVerts;
		m_mesh.bounds = new Bounds(m_MeshVerts[0], Vector3.one);
		if (flag)
		{
			m_mesh.uv = m_MeshUVs;
			m_mesh.colors32 = m_MeshCols;
			m_mesh.SetIndices(GetTriangles(), MeshTopology.Triangles, 0, calculateBounds: false);
		}
		m_CanvasRenderer.SetMesh(m_mesh);
		m_LastActiveLinesCount = count;
	}

	private static void SetupTransform(RectTransform rectTransform)
	{
		rectTransform.offsetMin = Vector2.zero;
		rectTransform.offsetMax = Vector2.zero;
		rectTransform.anchorMin = Vector2.zero;
		rectTransform.anchorMax = Vector2.zero;
		rectTransform.pivot = Vector2.zero;
		rectTransform.anchoredPosition = Vector2.zero;
	}

	private bool ResizeToFit(int activeLinesCount)
	{
		int maxLineCount = m_MaxLineCount;
		if (m_MaxLineCount == 0)
		{
			m_MaxLineCount = 1;
		}
		while (m_MaxLineCount < activeLinesCount)
		{
			m_MaxLineCount *= 2;
		}
		while (m_MaxLineCount > activeLinesCount * 4 && m_MaxLineCount > 16)
		{
			m_MaxLineCount /= 2;
		}
		int num = m_MaxLineCount * 4;
		if (m_MeshVerts.Length != num)
		{
			Array.Resize(ref m_MeshVerts, num);
			Array.Resize(ref m_MeshCols, num);
			Array.Resize(ref m_MeshUVs, num);
			if (m_MaxLineCount > maxLineCount)
			{
				SetUVsAndColours(maxLineCount, m_MaxLineCount);
			}
			else
			{
				m_mesh.SetIndices(GetTriangles(), MeshTopology.Triangles, 0, calculateBounds: false);
			}
		}
		return m_MaxLineCount != maxLineCount;
	}

	private void SetUVsAndColours(int startIndex, int endIndex)
	{
		int num = startIndex * 4;
		int num2 = endIndex * 4;
		for (int i = num; i < num2; i++)
		{
			m_MeshCols[i] = Color.white;
		}
		for (int j = startIndex; j < endIndex; j++)
		{
			m_MeshUVs[num] = new Vector2(0f, 1f);
			m_MeshUVs[num + 1] = new Vector2(1f, 1f);
			m_MeshUVs[num + 2] = new Vector2(1f, 0f);
			m_MeshUVs[num + 3] = new Vector2(0f, 0f);
			num += 4;
		}
	}

	private int[] GetTriangles()
	{
		int num = m_MaxLineCount * 6;
		if (m_Triangles == null || num != m_Triangles.Length)
		{
			m_Triangles = new int[num];
			int num2 = 0;
			for (int i = 0; i < num; i += 6)
			{
				m_Triangles[i] = num2;
				m_Triangles[i + 1] = num2 + 1;
				m_Triangles[i + 2] = num2 + 3;
				m_Triangles[i + 3] = num2 + 2;
				m_Triangles[i + 4] = num2 + 3;
				m_Triangles[i + 5] = num2 + 1;
				num2 += 4;
			}
		}
		return m_Triangles;
	}
}
