using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class UIEventParser : MonoBehaviour
{
	public Graphic m_Graphic;

	public Event<bool, object> OnHoverEvent;

	public object m_ObjectToSend;

	public Event<PointerEventData> OnScrollEvent;

	public void OnHoverEnter(bool enter)
	{
		OnHoverEvent.Send(enter, m_ObjectToSend);
	}

	public void OnScroll(BaseEventData data)
	{
		if (data is PointerEventData)
		{
			OnScrollEvent.Send(data as PointerEventData);
		}
	}
}
