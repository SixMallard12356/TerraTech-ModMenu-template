#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TriggerCatcher
{
	[Flags]
	public enum Interaction
	{
		Enter = 1,
		Exit = 2,
		Stay = 4
	}

	private static Dictionary<GameObject, Dictionary<Interaction, TriggerEventListener>> s_EventListeners = new Dictionary<GameObject, Dictionary<Interaction, TriggerEventListener>>();

	private static HashSet<TriggerEventListener> s_TriggerEventListenersBeingDestroyed = new HashSet<TriggerEventListener>();

	private static bool s_HasSubscribedToCleanup = false;

	public static void Subscribe(GameObject owner, Interaction eventType, Action<Interaction, Collider> _delegate)
	{
		if (!s_EventListeners.TryGetValue(owner, out var value))
		{
			value = new Dictionary<Interaction, TriggerEventListener>();
			s_EventListeners[owner] = value;
		}
		foreach (Interaction item in MatchingEvents(eventType))
		{
			if (!value.TryGetValue(item, out var value2))
			{
				Type listenerType = GetListenerType(item);
				Component component = owner.GetComponent(listenerType);
				if (component != null)
				{
					if (!s_TriggerEventListenersBeingDestroyed.Remove(component as TriggerEventListener))
					{
					}
				}
				else
				{
					component = owner.AddComponent(listenerType);
				}
				value2 = (value[item] = component as TriggerEventListener);
			}
			value2.Event.Subscribe(_delegate);
		}
	}

	public static void Unsubscribe(GameObject owner, Interaction eventType, Action<Interaction, Collider> _delegate)
	{
		if (!s_EventListeners.TryGetValue(owner, out var value))
		{
			return;
		}
		foreach (Interaction item in MatchingEvents(eventType))
		{
			TriggerEventListener value2;
			bool flag = value.TryGetValue(item, out value2);
			d.AssertFormat(flag, "Failed to unsubscribe event Trigger {0}, delegate {1}", eventType, _delegate);
			if (flag)
			{
				value2.Event.Unsubscribe(_delegate);
				if (!value2.Event.HasSubscribers())
				{
					DeferDestroyListener(value2);
					value.Remove(item);
				}
			}
		}
	}

	private static IEnumerable<Interaction> MatchingEvents(Interaction eventTypes)
	{
		EnumValuesIterator<Interaction> enumerator = EnumIterator<Interaction>.Values().GetEnumerator();
		while (enumerator.MoveNext())
		{
			Interaction current = enumerator.Current;
			if ((current & eventTypes) != 0)
			{
				yield return current;
			}
		}
	}

	private static Type GetListenerType(Interaction interaction)
	{
		Type type = null;
		switch (interaction)
		{
		case Interaction.Enter:
			type = typeof(TriggerCatcherListenerEnter);
			break;
		case Interaction.Stay:
			type = typeof(TriggerCatcherListenerStay);
			break;
		case Interaction.Exit:
			type = typeof(TriggerCatcherListenerExit);
			break;
		}
		d.AssertFormat(type != null && typeof(TriggerEventListener).IsAssignableFrom(type), "Not Supported event type {0}", interaction);
		return type;
	}

	private static void DeferDestroyListener(TriggerEventListener listener)
	{
		if (!s_HasSubscribedToCleanup)
		{
			Singleton.Manager<ManUpdate>.inst.AddAction(ManUpdate.Type.LateUpdate, ManUpdate.Order.Last, DestroyQueuedUpListeners, -100);
			s_HasSubscribedToCleanup = true;
		}
		s_TriggerEventListenersBeingDestroyed.Add(listener);
	}

	private static void DestroyQueuedUpListeners()
	{
		foreach (TriggerEventListener item in s_TriggerEventListenersBeingDestroyed)
		{
			UnityEngine.Object.Destroy(item);
		}
		s_TriggerEventListenersBeingDestroyed.Clear();
	}
}
