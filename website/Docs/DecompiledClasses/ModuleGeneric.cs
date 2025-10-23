#define UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

[Il2CppSetOption(Option.ArrayBoundsChecks, false)]
[Il2CppSetOption(Option.NullChecks, false)]
public class ModuleGeneric : Module
{
	public enum HandlerMethod
	{
		Null,
		OnAttach,
		OnDetach
	}

	[Serializable]
	public struct EventHandlerDefinition
	{
		public MonoBehaviour target;

		public HandlerMethod method;
	}

	private delegate void HandlerDelegate(ModuleGeneric module);

	[SerializeField]
	private EventHandlerDefinition[] m_EventHandlers;

	private List<HandlerDelegate>[] m_Handlers;

	private List<MethodInfo> m_Methods;

	[SerializeField]
	[HideInInspector]
	private List<EventHandlerDefinition> m_Targets;

	[SerializeField]
	[HideInInspector]
	private int m_PrefabInstanceID;

	private static Dictionary<int, List<MethodInfo>> s_MethodTable = new Dictionary<int, List<MethodInfo>>();

	private static readonly int k_MaxHandlerMethods = Enum.GetValues(typeof(HandlerMethod)).Length;

	public static MethodInfo FindCompatibleMethod(MonoBehaviour target, string hookName)
	{
		if (target == null)
		{
			return null;
		}
		MethodInfo method = target.GetType().GetMethod(hookName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (method == null || method.ReturnType != typeof(void))
		{
			return null;
		}
		ParameterInfo[] parameters = method.GetParameters();
		if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != typeof(ModuleGeneric))
		{
			return null;
		}
		return method;
	}

	private void OnAttached()
	{
		foreach (HandlerDelegate item in m_Handlers[1])
		{
			item(this);
		}
	}

	private void OnDetaching()
	{
		foreach (HandlerDelegate item in m_Handlers[2])
		{
			item(this);
		}
	}

	private void PrePool()
	{
		m_Targets = new List<EventHandlerDefinition>();
		m_PrefabInstanceID = GetInstanceID();
		List<MethodInfo> list = new List<MethodInfo>();
		s_MethodTable.Add(m_PrefabInstanceID, list);
		EventHandlerDefinition[] eventHandlers = m_EventHandlers;
		for (int i = 0; i < eventHandlers.Length; i++)
		{
			EventHandlerDefinition item = eventHandlers[i];
			if (item.method == HandlerMethod.Null)
			{
				d.LogError(base.name + " ignored Null method handler ");
				continue;
			}
			MethodInfo methodInfo = FindCompatibleMethod(item.target, item.method.ToString());
			if (methodInfo != null)
			{
				list.Add(methodInfo);
				m_Targets.Add(item);
				continue;
			}
			d.LogError(base.name + " failed to find matching event handler for target " + item.target.name + " handler " + item.method);
		}
		m_EventHandlers = null;
	}

	private void OnPool()
	{
		base.block.AttachedEvent.Subscribe(OnAttached);
		base.block.DetachingEvent.Subscribe(OnDetaching);
		List<MethodInfo> list = s_MethodTable[m_PrefabInstanceID];
		d.Assert(list != null, "failed to find method table in static lookup");
		m_Handlers = new List<HandlerDelegate>[k_MaxHandlerMethods];
		for (int i = 0; i < list.Count; i++)
		{
			d.Assert(list[i] != null && m_Targets[i].method != HandlerMethod.Null && m_Targets[i].target != null);
			int method = (int)m_Targets[i].method;
			if (m_Handlers[method] == null)
			{
				m_Handlers[method] = new List<HandlerDelegate>();
			}
			m_Handlers[method].Add((HandlerDelegate)Delegate.CreateDelegate(typeof(HandlerDelegate), m_Targets[i].target, list[i]));
		}
	}
}
