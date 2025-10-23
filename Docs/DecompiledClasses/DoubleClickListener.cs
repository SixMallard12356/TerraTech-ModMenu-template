using System;
using UnityEngine;
using UnityEngine.EventSystems;

[Serializable]
public class DoubleClickListener
{
	private const float DOUBLE_CLICK_TIME = 0.25f;

	private float m_LastClickTime;

	public bool WasClickEventDoubleClick(bool doubleClickConditionPassed = true)
	{
		return HandleClick(null, doubleClickConditionPassed);
	}

	public bool WasPointerClickEventDoubleClick(PointerEventData eventData, bool doubleClickConditionPassed = true)
	{
		return HandleClick(eventData, doubleClickConditionPassed);
	}

	private bool HandleClick(PointerEventData eventData, bool doubleClickConditionPassed)
	{
		bool result = false;
		if (eventData == null || eventData.button == PointerEventData.InputButton.Left)
		{
			if (m_LastClickTime > 0f && Time.realtimeSinceStartup < m_LastClickTime + 0.25f && doubleClickConditionPassed)
			{
				result = true;
				m_LastClickTime = 0f;
			}
			else
			{
				m_LastClickTime = Time.realtimeSinceStartup;
			}
		}
		return result;
	}
}
