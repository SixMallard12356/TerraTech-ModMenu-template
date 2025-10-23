using BehaviorDesigner.Runtime.Tasks;

public class SetResourceDispenserInfo : Action
{
	public SharedVisible m_Visible;

	public float m_RegrowTime = 5f;

	public override TaskStatus OnUpdate()
	{
		if (m_Visible.Value != null && m_Visible.Value.resdisp != null)
		{
			m_Visible.Value.resdisp.SetRegrowOverrideTime(m_RegrowTime);
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
