using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class VisibleInRangeOfPOI : Conditional
{
	public SharedVisible m_Visible;

	public SharedFloat m_Range = 30f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Visible != null && m_Visible.Value != null && m_Range != null && (m_Visible.Value.trans.position - m_AI.PointOfInterest).SetY(0f).sqrMagnitude <= m_Range.Value * m_Range.Value)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
