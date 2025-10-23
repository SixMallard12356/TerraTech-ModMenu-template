using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class ManLineRenderer : Singleton.Manager<ManLineRenderer>
{
	[SerializeField]
	private Material m_LineMaterial;

	private VectorLineRenderer m_Renderer;

	private List<VectorLineRenderer.Line> m_Lines;

	private Canvas m_Canvas;

	public void AddLineForOverlay(Vector2 sourceUIPos, float sourceUIZPos, Vector3 targetPos)
	{
		Vector3 start = Singleton.camera.ViewportToWorldPoint(Singleton.Manager<ManUI>.inst.m_UICamera.WorldToViewportPoint(sourceUIPos).SetZ(sourceUIZPos));
		AddWorldSpaceLine(start, targetPos);
	}

	public void AddWorldSpaceLine(Vector3 start, Vector3 end)
	{
		if (base.enabled)
		{
			m_Lines.Add(new VectorLineRenderer.Line
			{
				start = start,
				end = end,
				width = 1f
			});
		}
	}

	public void SetEnabled(bool linesEnabled)
	{
		base.enabled = linesEnabled;
		if (!base.enabled)
		{
			m_Canvas.enabled = false;
			m_Lines.Clear();
		}
	}

	private void Awake()
	{
		m_Canvas = GetComponent<Canvas>();
		m_Lines = new List<VectorLineRenderer.Line>(16);
		m_Renderer = new VectorLineRenderer(m_LineMaterial, m_Canvas);
		base.enabled = false;
	}

	private void LateUpdate()
	{
		if (m_Lines.Count > 0)
		{
			if (m_Canvas.worldCamera == null)
			{
				m_Canvas.worldCamera = Singleton.camera;
			}
			m_Canvas.enabled = true;
			m_Renderer.Draw3D(m_Lines);
			m_Lines.Clear();
		}
		else
		{
			m_Canvas.enabled = false;
		}
	}
}
