using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class InRangeOfPOI : Conditional
{
	public SharedFloat m_DistanceFromSpawnPos;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_DistanceFromSpawnPos != null && (m_AI.Tech.trans.position - m_AI.PointOfInterest).SetY(0f).sqrMagnitude < m_DistanceFromSpawnPos.Value * m_DistanceFromSpawnPos.Value)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
