using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITooltip : MonoBehaviour
{
	public Text m_Text;

	public Vector2 offset;

	private CanvasGroup m_CanvasGroup;

	public void Set(string text)
	{
		m_CanvasGroup = ((m_CanvasGroup == null) ? base.gameObject.AddComponent<CanvasGroup>() : m_CanvasGroup);
		m_CanvasGroup.alpha = 0f;
		m_CanvasGroup.ignoreParentGroups = true;
		m_CanvasGroup.blocksRaycasts = false;
		m_Text.text = text;
	}

	public void SetEvents(EventTrigger trigger)
	{
		EventTrigger.Entry entry = new EventTrigger.Entry();
		EventTrigger.Entry entry2 = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry2.eventID = EventTriggerType.PointerExit;
		entry.callback = new EventTrigger.TriggerEvent();
		entry2.callback = new EventTrigger.TriggerEvent();
		UnityAction<BaseEventData> call = OnHoverEnter;
		UnityAction<BaseEventData> call2 = OnHoverExit;
		entry.callback.AddListener(call);
		entry2.callback.AddListener(call2);
		trigger.triggers.Add(entry);
		trigger.triggers.Add(entry2);
	}

	public void OnHoverEnter(BaseEventData data)
	{
		m_CanvasGroup.alpha = 1f;
	}

	public void OnHoverExit(BaseEventData data)
	{
		m_CanvasGroup.alpha = 0f;
	}
}
