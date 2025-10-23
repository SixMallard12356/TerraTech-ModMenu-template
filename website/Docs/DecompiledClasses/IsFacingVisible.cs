using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class IsFacingVisible : Conditional
{
	public SharedVisible m_Target;

	public SharedFloat m_AngleThreshold = 5f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		bool flag = false;
		if (m_Target != null && m_Target.Value != null)
		{
			flag = m_AI.Tech.control.Movement.IsFacingVisible(m_AI.Tech, m_Target.Value, m_AngleThreshold.Value);
		}
		if (!flag)
		{
			return TaskStatus.Failure;
		}
		return TaskStatus.Success;
	}
}
