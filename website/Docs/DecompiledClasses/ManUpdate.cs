#define UNITY_EDITOR
using System;
using System.Collections.Generic;

public class ManUpdate : Singleton.Manager<ManUpdate>
{
	public enum Type
	{
		Update,
		LateUpdate,
		FixedUpdate
	}

	public enum Order
	{
		First,
		Last
	}

	public struct Action
	{
		public System.Action action;

		public int priority;
	}

	private List<Action>[][] m_Actions;

	private List<Action> m_CurrentlyIterating;

	private int m_CurrentIndex;

	public void AddAction(Type type, Order order, System.Action action, int priority = 0)
	{
		InitActionTable();
		if (action == null)
		{
			return;
		}
		List<Action> list = m_Actions[(int)order][(int)type];
		int num = list.FindIndex((Action a) => a.priority <= priority);
		Action item = new Action
		{
			action = action,
			priority = priority
		};
		if (num == -1)
		{
			list.Add(item);
			return;
		}
		list.Insert(num, item);
		if (list == m_CurrentlyIterating && num <= m_CurrentIndex)
		{
			m_CurrentIndex++;
		}
	}

	public void RemoveAction(Type type, Order order, System.Action action)
	{
		InitActionTable();
		if (action == null)
		{
			return;
		}
		List<Action> list = m_Actions[(int)order][(int)type];
		int num = list.FindIndex((Action a) => a.action == action);
		if (num != -1)
		{
			m_Actions[(int)order][(int)type].RemoveAt(num);
			if (list == m_CurrentlyIterating && num <= m_CurrentIndex)
			{
				m_CurrentIndex--;
			}
		}
	}

	public void InitActionTable()
	{
		if (m_Actions == null)
		{
			m_Actions = new List<Action>[2][];
			int length = Enum.GetValues(typeof(Type)).Length;
			m_Actions[0] = new List<Action>[length];
			m_Actions[1] = new List<Action>[length];
			for (int i = 0; i < length; i++)
			{
				m_Actions[0][i] = new List<Action>();
				m_Actions[1][i] = new List<Action>();
			}
		}
	}

	private void CallActions(List<Action> actions)
	{
		d.Assert(m_CurrentlyIterating == null, "ManUpdate: Cannot nest updates!");
		if (m_CurrentlyIterating != null)
		{
			return;
		}
		try
		{
			m_CurrentlyIterating = actions;
			for (m_CurrentIndex = 0; m_CurrentIndex < actions.Count; m_CurrentIndex++)
			{
				actions[m_CurrentIndex].action();
			}
		}
		finally
		{
			m_CurrentlyIterating = null;
			m_CurrentIndex = -1;
		}
	}

	private void OnFirstUpdate(Type type)
	{
		CallActions(m_Actions[0][(int)type]);
	}

	private void OnLastUpdate(Type type)
	{
		CallActions(m_Actions[1][(int)type]);
	}

	private void Start()
	{
		InitActionTable();
		GetComponent<ScriptOrderFirst>().UpdateHandler.Subscribe(OnFirstUpdate);
		GetComponent<ScriptOrderLast>().UpdateHandler.Subscribe(OnLastUpdate);
	}
}
