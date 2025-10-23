#define UNITY_EDITOR
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
	public Event<int> HandleEvent;

	private bool m_EventBeingHandled;

	private void Event(int param)
	{
		if (!HandleEvent.HasSubscribers())
		{
			return;
		}
		if (!m_EventBeingHandled)
		{
			m_EventBeingHandled = true;
			try
			{
				HandleEvent.Send(param);
				return;
			}
			finally
			{
				m_EventBeingHandled = false;
			}
		}
		d.LogError("AnimEvent being sent recursively");
	}
}
