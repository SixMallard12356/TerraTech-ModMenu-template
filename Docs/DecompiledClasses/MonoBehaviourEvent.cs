#define UNITY_EDITOR
using System;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.NullChecks, false)]
[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
public abstract class MonoBehaviourEvent : MonoBehaviour
{
	public EventNoParams Event;
}
public class MonoBehaviourEvent<T> where T : MonoBehaviourEvent
{
	private GameObject m_GameObject;

	private T m_MonoBehaviourReceiver;

	public MonoBehaviourEvent(GameObject owner, bool forceUnique = false)
	{
		m_GameObject = owner;
		m_MonoBehaviourReceiver = (forceUnique ? null : owner.GetComponent<T>());
	}

	public void Subscribe(Action _delegate)
	{
		if (m_MonoBehaviourReceiver.IsNull())
		{
			m_MonoBehaviourReceiver = m_GameObject.AddComponent<T>();
		}
		m_MonoBehaviourReceiver.Event.Subscribe(_delegate);
	}

	public void Unsubscribe(Action _delegate)
	{
		d.Assert(m_MonoBehaviourReceiver != null, "UpdateComponent was already cleaned up, but still trying to unsubscribe from it!");
		m_MonoBehaviourReceiver.Event.Unsubscribe(_delegate);
		if (!m_MonoBehaviourReceiver.Event.HasSubscribers())
		{
			UnityEngine.Object.Destroy(m_MonoBehaviourReceiver);
			m_MonoBehaviourReceiver = null;
		}
	}
}
