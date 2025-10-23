using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class VisibleInRange : Conditional
{
	public SharedVisible m_Target;

	public SharedFloat m_Range = 30f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Target != null && m_Target.Value != null && m_Range != null && (m_Target.Value.trans.position - m_AI.Tech.trans.position).SetY(0f).sqrMagnitude <= m_Range.Value * m_Range.Value)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
