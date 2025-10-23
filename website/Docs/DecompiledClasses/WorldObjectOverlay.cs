using UnityEngine;

public class WorldObjectOverlay : Overlay
{
	private WorldObjectOverlayData m_Data;

	private object m_Context;

	private OverlayPanel m_PanelInst;

	public WorldObjectOverlay(object context, WorldObjectOverlayData data)
	{
		m_Context = context;
		m_Data = data;
	}

	public override void Update()
	{
		Vector3 vector = Vector3.zero;
		float num = 0f;
		bool flag = m_Data.ShouldDisplay(m_Context);
		if (flag)
		{
			vector = m_Data.GetTargetPosition(m_Context);
			num = (Singleton.cameraTrans.position - vector).sqrMagnitude;
			flag = num <= m_Data.m_PanelMaxDisplayDistance * m_Data.m_PanelMaxDisplayDistance;
		}
		if (flag)
		{
			if (m_PanelInst == null)
			{
				m_PanelInst = m_Data.m_PanelPrefab.Spawn();
				m_PanelInst.SetContext(m_Context);
			}
			if (!(m_PanelInst != null))
			{
				return;
			}
			bool flag2 = Vector3.Dot(vector - Singleton.camera.transform.position, Singleton.camera.transform.forward) > 0f;
			if (flag2)
			{
				Vector2 vector2 = UIHelpers.WorldToUILocalPosition(vector, Singleton.camera, Singleton.Manager<ManHUD>.inst.Canvas, m_PanelInst.ParentRect);
				m_PanelInst.Rect.localPosition = new Vector3(vector2.x, vector2.y, m_Data.m_ZPos);
				Vector2 vector3 = Vector2.Scale(b: new Vector2((float)Screen.width / Singleton.Manager<ManUI>.inst.m_ReferenceResolution.x, (float)Screen.height / Singleton.Manager<ManUI>.inst.m_ReferenceResolution.y), a: m_Data.m_PanelOffset);
				float num2 = ((!m_Data.m_ScaleWithDistance) ? 1f : Mathf.Clamp(1f - Mathf.Sqrt(num) / m_Data.m_PanelMaxDisplayDistance, 0.2f, 1f));
				m_PanelInst.Rect.localScale = new Vector3(num2, num2, 1f);
				m_PanelInst.Rect.anchoredPosition += num2 * vector3;
				Rect other = new Rect(m_PanelInst.ParentRect.anchoredPosition.x, m_PanelInst.ParentRect.anchoredPosition.y - m_PanelInst.ParentRect.rect.height, m_PanelInst.ParentRect.rect.width, m_PanelInst.ParentRect.rect.height);
				Rect rect = new Rect(m_PanelInst.Rect.anchoredPosition.x - m_PanelInst.Rect.rect.width * 0.5f, m_PanelInst.Rect.anchoredPosition.y, m_PanelInst.Rect.rect.width, m_PanelInst.Rect.rect.height);
				flag2 = rect.Overlaps(other);
				if (flag2)
				{
					Singleton.Manager<ManLineRenderer>.inst.AddLineForOverlay(m_PanelInst.Rect.position, m_Data.m_ZPos, vector);
				}
			}
			if (flag2 != m_PanelInst.gameObject.activeSelf)
			{
				m_PanelInst.gameObject.SetActive(flag2);
			}
		}
		else if (m_PanelInst != null)
		{
			RecyclePanel();
		}
	}

	public override bool HasExpired()
	{
		return false;
	}

	public override void PerformCleanup()
	{
		RecyclePanel();
	}

	public void RefreshPanel(object context = null)
	{
		if (m_PanelInst != null)
		{
			m_PanelInst.RefreshPanel(context);
		}
	}

	private void RecyclePanel()
	{
		if (m_PanelInst != null)
		{
			m_PanelInst.Recycle();
			m_PanelInst = null;
		}
	}
}
