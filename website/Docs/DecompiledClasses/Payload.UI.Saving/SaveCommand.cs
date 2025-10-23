#define UNITY_EDITOR
namespace Payload.UI.Saving;

public abstract class SaveCommand
{
	public Event<SaveOperationData> Completed;

	public Event<SaveOperationData> Cancelled;

	public bool m_Completed;

	public bool m_Cancelled;

	public SaveOperationData m_Data;

	public void Reset()
	{
		m_Completed = false;
		m_Cancelled = false;
		Completed.Clear();
		Cancelled.Clear();
	}

	public abstract void Execute(SaveOperationData data);

	protected void SetComplete(SaveOperationData data)
	{
		if (m_Completed)
		{
			d.Log("SaveCommand.SetComplete. Save command has already been set to complete. Illegal to set complete again.");
			return;
		}
		if (m_Cancelled)
		{
			d.LogError("SaveCommand.SetComplete. Save command has already been set to cancelled. Illegal to set complete");
			return;
		}
		m_Data = data;
		m_Completed = true;
		Completed.Send(m_Data);
	}

	protected void SetCancelled(SaveOperationData data)
	{
		if (m_Completed)
		{
			d.LogError("SaveCommand.SetCancelled. Save command has already been set to complete. Illegal to set to cancel");
			return;
		}
		m_Data = data;
		m_Cancelled = true;
		Cancelled.Send(m_Data);
	}
}
