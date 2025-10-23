#define UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace Payload.UI.Commands;

public class CommandOperation<T>
{
	public Event<T> Completed;

	public Event<T> Cancelled;

	private List<Command<T>> m_Commands = new List<Command<T>>();

	private bool m_Running;

	public bool IsRunning => m_Running;

	public void Add(Command<T> command)
	{
		m_Commands.Add(command);
	}

	public void AddConditional(Func<T, bool> condition, params Command<T>[] commands)
	{
		ConditionalCommand<T> conditionalCommand = new ConditionalCommand<T>();
		conditionalCommand.Setup(condition, commands);
		Add(conditionalCommand);
	}

	public void Execute(T data)
	{
		d.Assert(!m_Running, "CommandOperation.Execute - previous operation has not yet completed");
		if (m_Commands.Count < 1)
		{
			return;
		}
		Command<T> command = m_Commands[0];
		Command<T> command2 = m_Commands[m_Commands.Count - 1];
		Reset();
		command2.Completed.Subscribe(OnCompleted);
		for (int num = m_Commands.Count - 1; num >= 0; num--)
		{
			Command<T> command3 = m_Commands[num];
			command3.Cancelled.Subscribe(OnCancelled);
			if (num > 0)
			{
				m_Commands[num - 1].Completed.Subscribe(command3.Execute);
			}
		}
		m_Running = true;
		command.Execute(data);
	}

	private void OnCompleted(T data)
	{
		Reset();
		m_Running = false;
		Completed.Send(data);
	}

	private void OnCancelled(T data)
	{
		Reset();
		m_Running = false;
		Cancelled.Send(data);
	}

	private void Reset()
	{
		for (int i = 0; i < m_Commands.Count; i++)
		{
			m_Commands[i].Reset();
		}
	}
}
