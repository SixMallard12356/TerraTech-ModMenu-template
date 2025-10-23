#define UNITY_EDITOR
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoScrollItem : MonoBehaviour, ISelectHandler, IEventSystemHandler
{
	private UIAutoScrollItemController m_AutoScroller;

	public void OnSelect(BaseEventData eventData)
	{
		if (m_AutoScroller == null)
		{
			m_AutoScroller = GetComponentInParent<UIAutoScrollItemController>();
		}
		d.AssertFormat(m_AutoScroller != null, "Item {0} must be parented to an item with a valid UIAutoScrollItemController and ScrollRect component", base.name);
		if (m_AutoScroller != null)
		{
			m_AutoScroller.ScrollToItem(GetComponent<RectTransform>());
		}
	}
}
