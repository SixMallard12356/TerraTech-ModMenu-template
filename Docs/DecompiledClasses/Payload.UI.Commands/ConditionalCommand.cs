#define UNITY_EDITOR
using System;

namespace Payload.UI.Commands;

public class ConditionalCommand<T> : Command<T>
{
	public Func<T, bool> m_Condition;

	private CommandOperation<T> m_OptionalBranch;

	public void Setup(Func<T, bool> condition, params Command<T>[] commands)
	{
		if (m_OptionalBranch != null)
		{
			d.LogError("ConditionalCommand.Setup - illegal attempt to call Setup twice");
			return;
		}
		m_Condition = condition;
		m_OptionalBranch = new CommandOperation<T>();
		m_OptionalBranch.Completed.Subscribe(SetComplete);
		m_OptionalBranch.Cancelled.Subscribe(SetCancelled);
		for (int i = 0; i < commands.Length; i++)
		{
			m_OptionalBranch.Add(commands[i]);
		}
	}

	public override void Execute(T data)
	{
		if (m_Condition != null && m_Condition(data))
		{
			m_OptionalBranch.Execute(data);
		}
		else
		{
			SetComplete(data);
		}
	}
}
