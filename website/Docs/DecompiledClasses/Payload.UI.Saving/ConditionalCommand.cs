#define UNITY_EDITOR
using System;

namespace Payload.UI.Saving;

public class ConditionalCommand : SaveCommand
{
	public Func<SaveOperationData, bool> m_Condition;

	private SaveOperation m_OptionalBranch;

	public void Setup(Func<SaveOperationData, bool> condition, params SaveCommand[] commands)
	{
		if (m_OptionalBranch != null)
		{
			d.LogError("SaveCommandBranch.Setup - illegal attempt to call Setup twice");
			return;
		}
		m_Condition = condition;
		m_OptionalBranch = new SaveOperation();
		SaveOperation optionalBranch = m_OptionalBranch;
		optionalBranch.Completed = (Action<SaveOperationData>)Delegate.Combine(optionalBranch.Completed, new Action<SaveOperationData>(base.SetComplete));
		SaveOperation optionalBranch2 = m_OptionalBranch;
		optionalBranch2.Cancelled = (Action<SaveOperationData>)Delegate.Combine(optionalBranch2.Cancelled, new Action<SaveOperationData>(base.SetCancelled));
		for (int i = 0; i < commands.Length; i++)
		{
			m_OptionalBranch.Add(commands[i]);
		}
	}

	public override void Execute(SaveOperationData data)
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
