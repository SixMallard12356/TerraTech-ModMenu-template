#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UIAutoScrollItemController : MonoBehaviour
{
	private static float SCROLL_MARGIN = 0.3f;

	private ScrollRect m_ScrollRect;

	private RectTransform m_TargetItem;

	public void ScrollToItem(RectTransform target)
	{
		d.Assert(m_ScrollRect != null, "UIAutoScrollItemContainer game object is missing a scroll rect component. Name: " + base.name);
		m_TargetItem = target;
	}

	private void LateUpdate()
	{
		if (!(m_TargetItem != null))
		{
			return;
		}
		float height = m_ScrollRect.content.rect.height;
		float height2 = m_ScrollRect.viewport.rect.height;
		if (height > height2)
		{
			float num = height * m_TargetItem.anchorMax.y + m_TargetItem.anchoredPosition.y + m_TargetItem.rect.yMax;
			float num2 = height * m_TargetItem.anchorMin.y + m_TargetItem.anchoredPosition.y + m_TargetItem.rect.yMin;
			float num3 = (height - height2) * m_ScrollRect.normalizedPosition.y;
			float num4 = num3 + height2;
			float num5;
			if (num > num4)
			{
				num5 = num - height2 + m_TargetItem.rect.height * SCROLL_MARGIN;
			}
			else
			{
				if (!(num2 < num3))
				{
					m_TargetItem = null;
					return;
				}
				num5 = num2 - m_TargetItem.rect.height * SCROLL_MARGIN;
			}
			float value = num5 / (height - height2);
			m_ScrollRect.normalizedPosition = new Vector2(0f, Mathf.Clamp01(value));
		}
		m_TargetItem = null;
	}

	private void OnEnable()
	{
		if (m_ScrollRect == null)
		{
			m_ScrollRect = GetComponent<ScrollRect>();
		}
	}
}
