#define UNITY_EDITOR
using UnityEngine;

public struct CollisionCatcher
{
	public enum Type
	{
		Enter,
		Exit,
		Stay
	}

	public Event<Type, Collision> CollisionEvent;

	private GameObject m_GameObject;

	private CollisionCatcherEventListener[] m_CollisionEventListeners;

	public CollisionCatcher(GameObject gameObject)
	{
		CollisionEvent = default(Event<Type, Collision>);
		m_GameObject = gameObject;
		m_CollisionEventListeners = new CollisionCatcherEventListener[3];
	}

	public void AddListener(Type eventType)
	{
		d.Assert(m_CollisionEventListeners[(int)eventType] == null, string.Concat("AddListener - Already have Collision Event Listeners of type ", eventType, " on object ", m_GameObject));
		CollisionCatcherEventListener collisionCatcherEventListener;
		switch (eventType)
		{
		case Type.Enter:
			collisionCatcherEventListener = m_GameObject.AddComponent<CollisionCatcherListenerEnter>();
			break;
		case Type.Exit:
			collisionCatcherEventListener = m_GameObject.AddComponent<CollisionCatcherListenerExit>();
			break;
		case Type.Stay:
			collisionCatcherEventListener = m_GameObject.AddComponent<CollisionCatcherListenerStay>();
			break;
		default:
			collisionCatcherEventListener = null;
			d.LogError("Not Supported event type " + eventType);
			break;
		}
		collisionCatcherEventListener.IsEnabled = true;
		collisionCatcherEventListener.CollisionEvent.Subscribe(OnCollisionEvent);
		m_CollisionEventListeners[(int)eventType] = collisionCatcherEventListener;
	}

	public void RemoveListener(Type eventType)
	{
		CollisionCatcherEventListener collisionCatcherEventListener = m_CollisionEventListeners[(int)eventType];
		if (collisionCatcherEventListener != null)
		{
			collisionCatcherEventListener.IsEnabled = false;
			collisionCatcherEventListener.CollisionEvent.Unsubscribe(OnCollisionEvent);
			Object.Destroy(collisionCatcherEventListener);
			m_CollisionEventListeners[(int)eventType] = null;
		}
	}

	public void Clear()
	{
		for (int i = 0; i < m_CollisionEventListeners.Length; i++)
		{
			if (m_CollisionEventListeners[i] != null)
			{
				RemoveListener((Type)i);
			}
		}
		CollisionEvent.EnsureNoSubscribers();
	}

	private void OnCollisionEvent(Type eventType, Collision collision)
	{
		CollisionEvent.Send(eventType, collision);
	}
}
