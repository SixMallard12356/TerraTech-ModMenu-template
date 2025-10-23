using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMaskCutout : UIHUDElement
{
	public struct UIMaskContext
	{
		public RectTransform[] unmaskedTransforms;
	}

	[SerializeField]
	private Image m_RaycastMask;

	private RectTransform[] m_SourceRects;

	private List<Rect> m_UnmaskedRects = new List<Rect>();

	public override void Show(object context)
	{
		UIMaskContext context2 = ((context == null) ? default(UIMaskContext) : ((UIMaskContext)context));
		if (ChangeCheck(context2))
		{
			m_UnmaskedRects.Clear();
			m_SourceRects = context2.unmaskedTransforms;
			int num = ((m_SourceRects != null) ? m_SourceRects.Length : 0);
			if (num > 0)
			{
				RectTransform referenceTransform = base.transform as RectTransform;
				for (int i = 0; i < num; i++)
				{
					Rect transformRectInLocalTransform = Singleton.Manager<ManHUD>.inst.GetTransformRectInLocalTransform(m_SourceRects[i], referenceTransform);
					m_UnmaskedRects.Add(transformRectInLocalTransform);
				}
			}
			else
			{
				m_UnmaskedRects.Clear();
			}
			Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: false, ManPointer.DragDisableReason.HudMasked);
		}
		base.Show(context);
	}

	public override void Hide(object context)
	{
		base.Hide(context);
		m_UnmaskedRects.Clear();
		m_SourceRects = null;
		Singleton.Manager<ManPointer>.inst.SetDragEnabled(enabled: true, ManPointer.DragDisableReason.HudMasked);
	}

	private void FixedUpdate()
	{
		bool flag = false;
		if (m_UnmaskedRects.Count > 0)
		{
			RectTransformUtility.ScreenPointToLocalPointInRectangle(base.transform as RectTransform, Input.mousePosition, Singleton.Manager<ManHUD>.inst.Canvas.worldCamera, out var localPoint);
			flag = true;
			foreach (Rect unmaskedRect in m_UnmaskedRects)
			{
				if (unmaskedRect.Contains(localPoint))
				{
					flag = false;
					break;
				}
			}
		}
		if (m_RaycastMask.raycastTarget != flag)
		{
			m_RaycastMask.raycastTarget = flag;
		}
	}

	private bool ChangeCheck(UIMaskContext context)
	{
		if (m_SourceRects == null != (context.unmaskedTransforms == null))
		{
			return true;
		}
		if (m_SourceRects != null && context.unmaskedTransforms != null)
		{
			if (m_SourceRects.Length != context.unmaskedTransforms.Length)
			{
				return true;
			}
			int num = m_SourceRects.Length;
			for (int i = 0; i < num; i++)
			{
				if (m_SourceRects[i] != context.unmaskedTransforms[i])
				{
					return true;
				}
			}
		}
		return false;
	}
}
