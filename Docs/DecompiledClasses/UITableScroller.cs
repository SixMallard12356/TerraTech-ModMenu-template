#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

public class UITableScroller : MonoBehaviour
{
	private bool m_NeedsScroll;

	private int m_ScrollIndex;

	private ScrollRect m_ScrollRect;

	public void ScrollToEntry(int index)
	{
		m_NeedsScroll = true;
		m_ScrollIndex = index;
	}

	private void LateUpdate()
	{
		if (m_NeedsScroll)
		{
			RectTransform rectTransform = base.transform as RectTransform;
			if (rectTransform != null && m_ScrollIndex < rectTransform.childCount)
			{
				RectTransform rectTransform2 = rectTransform.GetChild(m_ScrollIndex) as RectTransform;
				if ((bool)rectTransform2)
				{
					UIHelpers.VertScrollToItem(rectTransform, rectTransform2, m_ScrollRect.viewport.rect.height);
				}
			}
		}
		m_NeedsScroll = false;
	}

	private void Awake()
	{
		m_ScrollRect = GetComponentInParent<ScrollRect>();
		d.Assert(m_ScrollRect != null, "UITableScroller - Did not find ScrollRect in parent objects!");
	}
}
