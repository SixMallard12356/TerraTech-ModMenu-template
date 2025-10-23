using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Sets a Visible value")]
public class SetVisible : Action
{
	[Tooltip("The Visible value to set")]
	public SharedVisible m_VisibleValue;

	[Tooltip("The variable to store the result")]
	public SharedVisible storeResult;

	public override TaskStatus OnUpdate()
	{
		storeResult.Value = m_VisibleValue.Value;
		return TaskStatus.Success;
	}

	public override void OnReset()
	{
		m_VisibleValue.Value = null;
		storeResult.Value = null;
	}
}
