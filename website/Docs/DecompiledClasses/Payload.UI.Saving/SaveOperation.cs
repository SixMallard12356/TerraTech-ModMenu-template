#define UNITY_EDITOR
using System;
using System.Collections.Generic;

namespace Payload.UI.Saving;

public class SaveOperation
{
	public Action<SaveOperationData> Completed;

	public Action<SaveOperationData> Cancelled;

	private List<SaveCommand> m_Commands = new List<SaveCommand>();

	private bool m_Running;

	public void Add(SaveCommand command)
	{
		m_Commands.Add(command);
	}

	public void AddConditional(Func<SaveOperationData, bool> condition, params SaveCommand[] commands)
	{
		ConditionalCommand conditionalCommand = new ConditionalCommand();
		conditionalCommand.Setup(condition, commands);
		Add(conditionalCommand);
	}

	public void Execute(SaveOperationData data)
	{
		d.Assert(!m_Running, "SaveOperation.Execute - previous operation has not yet completed");
		if (m_Commands.Count < 1)
		{
			return;
		}
		SaveCommand saveCommand = m_Commands[0];
		SaveCommand saveCommand2 = m_Commands[m_Commands.Count - 1];
		Reset();
		saveCommand2.Completed.Subscribe(OnCompleted);
		for (int num = m_Commands.Count - 1; num >= 0; num--)
		{
			SaveCommand saveCommand3 = m_Commands[num];
			saveCommand3.Cancelled.Subscribe(OnCancelled);
			if (num > 0)
			{
				m_Commands[num - 1].Completed.Subscribe(saveCommand3.Execute);
			}
		}
		saveCommand.Execute(data);
	}

	private void OnCompleted(SaveOperationData data)
	{
		Reset();
		m_Running = false;
		Completed.Send(data);
	}

	private void OnCancelled(SaveOperationData data)
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
