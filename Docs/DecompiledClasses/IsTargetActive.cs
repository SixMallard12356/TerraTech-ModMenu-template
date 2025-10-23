using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsTargetActive : Conditional
{
	public SharedTransform m_Target;

	public SharedVisible m_Visible;

	public override TaskStatus OnUpdate()
	{
		if (m_Target != null && (bool)m_Target.Value)
		{
			if (m_Target.Value.gameObject.activeInHierarchy)
			{
				return TaskStatus.Success;
			}
		}
		else if (m_Visible != null && (bool)m_Visible.Value && m_Visible.Value.gameObject.activeInHierarchy)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
