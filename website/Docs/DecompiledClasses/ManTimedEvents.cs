#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ManTimedEvents : Singleton.Manager<ManTimedEvents>
{
	public struct TimedEvent
	{
		public bool delayed;

		public float timeout;

		public Action action;

		public bool IsValid => action != null;

		public void Delay(float newTimeOut)
		{
			timeout = newTimeOut;
			delayed = true;
		}

		public override string ToString()
		{
			return "[" + timeout + "," + (delayed ? "delayed" : "NOTdelayed") + "," + ((action != null) ? string.Concat(action.Target, ":", action.Method, "]") : "null");
		}
	}

	public class ManagedEvent
	{
		private LinkedListNode<TimedEvent> node;

		private Action action;

		private readonly Action DoActionDelegate;

		private readonly Action EventActionDelegate;

		public bool IsSet => node != null;

		public float TimeRemaining
		{
			get
			{
				if (node == null)
				{
					return 0f;
				}
				return node.Value.timeout - Time.time;
			}
		}

		private bool HasPersistentEventHandler => EventActionDelegate != null;

		public ManagedEvent()
		{
			DoActionDelegate = DoAction;
			EventActionDelegate = null;
		}

		public ManagedEvent(Action action)
		{
			DoActionDelegate = DoAction;
			EventActionDelegate = action;
		}

		public void Set(float delay, Action action)
		{
			d.Assert(node == null, "node already set");
			d.Assert(!HasPersistentEventHandler, "action already set");
			if (node != null)
			{
				Clear();
			}
			node = Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(Time.time + delay, DoActionDelegate);
			this.action = action;
		}

		public void Set(float delay)
		{
			d.Assert(node == null, "node already set");
			d.Assert(HasPersistentEventHandler, "EventActionDelegate not set!");
			if (node != null)
			{
				Clear();
			}
			node = Singleton.Manager<ManTimedEvents>.inst.AddTimedEvent(Time.time + delay, DoActionDelegate);
			action = EventActionDelegate;
		}

		public void Reset(float delay)
		{
			d.Assert(node != null, "node not set");
			if (node != null)
			{
				Singleton.Manager<ManTimedEvents>.inst.DelayTimedEvent(Time.time + delay, node);
			}
		}

		public void Clear()
		{
			if (node != null)
			{
				Singleton.Manager<ManTimedEvents>.inst.CancelTimedEvent(node);
				node = null;
			}
		}

		private void DoAction()
		{
			action();
			if (node != null && !node.Value.delayed)
			{
				if (!HasPersistentEventHandler)
				{
					action = null;
				}
				node = null;
			}
		}
	}

	public Event<int> UnityUpdateEvent;

	private LinkedList<Func<bool>> updateEvents = new LinkedList<Func<bool>>();

	private LinkedList<Func<bool>> fixedUpdateEvents = new LinkedList<Func<bool>>();

	private LinkedList<Func<bool>> onGUIEvents = new LinkedList<Func<bool>>();

	private LinkedList<Func<bool>> preRenderEvents = new LinkedList<Func<bool>>();

	private const int kEventListAllocSize = 4000;

	private const int kEventListGrowBy = 400;

	private Stack<LinkedListNode<Func<bool>>> eventListNodePool = new Stack<LinkedListNode<Func<bool>>>(4000);

	private LinkedList<TimedEvent> timedEvents = new LinkedList<TimedEvent>();

	private Stack<LinkedListNode<TimedEvent>> timedEventNodePool = new Stack<LinkedListNode<TimedEvent>>(4000);

	private int m_MainThreadID;

	private bool m_SoftLock;

	public int RepeatingEventPoolFree => eventListNodePool.Count;

	public int TimedEventPoolFree => timedEventNodePool.Count;

	public LinkedListNode<TimedEvent> AddTimedEvent(float timeout, Action action)
	{
		LinkedListNode<TimedEvent> linkedListNode = timedEvents.First;
		while (linkedListNode != null && (linkedListNode.Value.delayed || linkedListNode.Value.timeout <= timeout))
		{
			linkedListNode = linkedListNode.Next;
		}
		if (timedEventNodePool.Count == 0)
		{
			d.LogWarning("timedEventNodePool empty");
			for (int i = 0; i < 400; i++)
			{
				timedEventNodePool.Push(new LinkedListNode<TimedEvent>(default(TimedEvent)));
			}
		}
		LinkedListNode<TimedEvent> linkedListNode2 = timedEventNodePool.Pop();
		linkedListNode2.Value = new TimedEvent
		{
			timeout = timeout,
			action = action,
			delayed = false
		};
		InsertOrAddTimedEvent(linkedListNode, linkedListNode2);
		return linkedListNode2;
	}

	public void RemoveTimedEvent(LinkedListNode<TimedEvent> node, bool warn = true)
	{
		d.Assert(!warn, "ARE YOU SURE you want to remove the event rather than canceling it?");
		timedEvents.Remove(node);
	}

	public void CancelTimedEvent(LinkedListNode<TimedEvent> node)
	{
		node.Value = default(TimedEvent);
	}

	private void ReinsertDelayedTimedEvent(LinkedListNode<TimedEvent> delayedNode)
	{
		LinkedListNode<TimedEvent> linkedListNode = timedEvents.First;
		while (linkedListNode != null && (linkedListNode.Value.delayed || linkedListNode.Value.timeout <= delayedNode.Value.timeout))
		{
			linkedListNode = linkedListNode.Next;
		}
		delayedNode.Value = new TimedEvent
		{
			timeout = delayedNode.Value.timeout,
			action = delayedNode.Value.action,
			delayed = false
		};
		InsertOrAddTimedEvent(linkedListNode, delayedNode);
	}

	private void InsertOrAddTimedEvent(LinkedListNode<TimedEvent> nodeExisting, LinkedListNode<TimedEvent> nodeToAdd)
	{
		DebugUtil.AssertRelease(Thread.CurrentThread.ManagedThreadId == m_MainThreadID, "timed event queue modified off main thread");
		DebugUtil.AssertRelease(nodeToAdd.List == null, "Added node " + nodeToAdd.Value.ToString() + " " + ((nodeToAdd.List == timedEvents) ? " still in timed events list" : " belongs to a list"));
		if (nodeExisting != null)
		{
			try
			{
				timedEvents.AddBefore(nodeExisting, nodeToAdd);
				return;
			}
			catch
			{
				DebugUtil.AssertRelease(condition: false, "InvalidOperationException trying to replace bad TimedEvent node " + ((nodeToAdd.List == null) ? "(new node was NOT already in a list)" : ""));
				nodeToAdd = new LinkedListNode<TimedEvent>(new TimedEvent
				{
					timeout = nodeToAdd.Value.timeout,
					action = nodeToAdd.Value.action,
					delayed = false
				});
				timedEvents.AddBefore(nodeExisting, nodeToAdd);
				return;
			}
		}
		timedEvents.AddLast(nodeToAdd);
	}

	public void DelayTimedEvent(float newTimeout, LinkedListNode<TimedEvent> node)
	{
		node.Value = new TimedEvent
		{
			timeout = newTimeout,
			action = node.Value.action,
			delayed = true
		};
	}

	private void AddEventGeneric(LinkedList<Func<bool>> list, Func<bool> e)
	{
		if (eventListNodePool.Count == 0)
		{
			d.LogWarning("eventListNodePool empty");
			for (int i = 0; i < 400; i++)
			{
				eventListNodePool.Push(new LinkedListNode<Func<bool>>(e));
			}
		}
		LinkedListNode<Func<bool>> linkedListNode = eventListNodePool.Pop();
		linkedListNode.Value = e;
		list.AddLast(linkedListNode);
	}

	private void RemoveEventGeneric(LinkedList<Func<bool>> list, Func<bool> e)
	{
		LinkedListNode<Func<bool>> linkedListNode = list.Find(e);
		if (linkedListNode != null)
		{
			list.Remove(linkedListNode);
			eventListNodePool.Push(linkedListNode);
		}
		else
		{
			d.LogError("event callback not found");
		}
	}

	private void ProcessEventList(LinkedList<Func<bool>> list)
	{
		LinkedListNode<Func<bool>> linkedListNode = list.First;
		while (linkedListNode != null)
		{
			LinkedListNode<Func<bool>> linkedListNode2 = (linkedListNode.Value() ? linkedListNode : null);
			linkedListNode = linkedListNode.Next;
			if (linkedListNode2 != null)
			{
				list.Remove(linkedListNode2);
				eventListNodePool.Push(linkedListNode2);
			}
		}
	}

	public void AddUpdateEvent(Func<bool> repeatingEvent)
	{
		AddEventGeneric(updateEvents, repeatingEvent);
	}

	public void RemoveUpdateEvent(Func<bool> repeatingEvent)
	{
		RemoveEventGeneric(updateEvents, repeatingEvent);
	}

	public void AddPreRenderEvent(Func<bool> repeatingEvent)
	{
		AddEventGeneric(preRenderEvents, repeatingEvent);
	}

	public void RemovePreRenderEvent(Func<bool> repeatingEvent)
	{
		RemoveEventGeneric(preRenderEvents, repeatingEvent);
	}

	public void AddFixedUpdateEvent(Func<bool> repeatingEvent)
	{
		AddEventGeneric(fixedUpdateEvents, repeatingEvent);
	}

	public void RemoveFixedUpdateEvent(Func<bool> repeatingEvent)
	{
		RemoveEventGeneric(fixedUpdateEvents, repeatingEvent);
	}

	public void AddOnGUIEvent(Func<bool> repeatingEvent)
	{
		AddEventGeneric(onGUIEvents, repeatingEvent);
	}

	public void RemoveOnGUIEvent(Func<bool> repeatingEvent)
	{
		RemoveEventGeneric(onGUIEvents, repeatingEvent);
	}

	public void PreRender()
	{
		ProcessEventList(preRenderEvents);
	}

	private void Awake()
	{
		m_MainThreadID = Thread.CurrentThread.ManagedThreadId;
		m_SoftLock = false;
		for (int i = 0; i < 4000; i++)
		{
			eventListNodePool.Push(new LinkedListNode<Func<bool>>(null));
			timedEventNodePool.Push(new LinkedListNode<TimedEvent>(default(TimedEvent)));
		}
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void Update()
	{
		UnityUpdateEvent.Send(0);
		ProcessEventList(updateEvents);
		LinkedListNode<TimedEvent> linkedListNode = timedEvents.First;
		while (linkedListNode != null)
		{
			if (linkedListNode.Value.delayed)
			{
				LinkedListNode<TimedEvent> next = linkedListNode.Next;
				timedEvents.Remove(linkedListNode);
				ReinsertDelayedTimedEvent(linkedListNode);
				linkedListNode = next;
				continue;
			}
			if (linkedListNode.Value.timeout > Time.time)
			{
				break;
			}
			if (linkedListNode.Value.IsValid)
			{
				m_SoftLock = true;
				linkedListNode.Value.action();
				m_SoftLock = false;
			}
			if (linkedListNode.Value.delayed)
			{
				linkedListNode = linkedListNode.Next;
				continue;
			}
			timedEventNodePool.Push(linkedListNode);
			LinkedListNode<TimedEvent> next2 = linkedListNode.Next;
			timedEvents.Remove(linkedListNode);
			linkedListNode = next2;
			DebugUtil.AssertRelease(timedEventNodePool.Count == 0 || timedEventNodePool.Peek().List == null, "top pooled event still in list");
		}
		DebugUtil.AssertRelease(timedEventNodePool.Count == 0 || timedEventNodePool.Peek().List == null, "top pooled event still in list (after loop)");
	}

	private void FixedUpdate()
	{
		ProcessEventList(fixedUpdateEvents);
	}

	private void OnDebugGUI()
	{
		ProcessEventList(onGUIEvents);
	}
}
