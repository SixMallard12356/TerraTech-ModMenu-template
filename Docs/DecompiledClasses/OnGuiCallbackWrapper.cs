#define UNITY_EDITOR
using System;
using UnityEngine;

public class OnGuiCallbackWrapper
{
	private OnGUICallback m_Wrapped;

	private Action m_Action;

	public bool IsSet => m_Wrapped.IsNotNull();

	public void Set(GameObject parentGameObject, Action action)
	{
		d.Assert(parentGameObject != null);
		d.Assert(action != null);
		Clear();
		if (m_Wrapped == null)
		{
			m_Wrapped = OnGUICallback.AddGUICallback(parentGameObject);
			m_Wrapped.OnGUIEvent.Subscribe(action);
			m_Action = action;
		}
	}

	public void SetEnabled(bool enabled, GameObject parentGameObject, Action action)
	{
		if (enabled != IsSet)
		{
			if (enabled)
			{
				Set(parentGameObject, action);
			}
			else
			{
				Clear();
			}
		}
	}

	public void Clear()
	{
		if (m_Wrapped != null)
		{
			m_Wrapped.OnGUIEvent.Unsubscribe(m_Action);
			OnGUICallback.RemoveGUICallback(m_Wrapped);
			m_Wrapped = null;
			m_Action = null;
		}
	}
}
