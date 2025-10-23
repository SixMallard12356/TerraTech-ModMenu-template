#define UNITY_EDITOR
namespace Payload.UI.Commands;

public abstract class Command<T>
{
	public Event<T> Completed;

	public Event<T> Cancelled;

	public bool m_Completed;

	public bool m_Cancelled;

	public void Reset()
	{
		m_Completed = false;
		m_Cancelled = false;
		Completed.Clear();
		Cancelled.Clear();
	}

	public abstract void Execute(T data);

	protected virtual void SetComplete(T data)
	{
		if (m_Completed)
		{
			d.LogError("SaveCommand.SetComplete. Save command has already been set to cancelled. Illegal to set complete");
			return;
		}
		m_Completed = true;
		Completed.Send(data);
	}

	protected virtual void SetCancelled(T data)
	{
		if (m_Completed)
		{
			d.LogError("SaveCommand.SetCancelled. Save command has already been set to complete. Illegal to set to cancel");
			return;
		}
		m_Cancelled = true;
		Cancelled.Send(data);
	}
}
