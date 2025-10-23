using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class InRangeOfTarget : Conditional
{
	public float m_DistanceFromSpawnPos;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_AI)
		{
			Vector3 vector = m_AI.Tech.trans.position - m_AI.PointOfInterest;
			vector.y = 0f;
			if (vector.sqrMagnitude > m_DistanceFromSpawnPos * m_DistanceFromSpawnPos)
			{
				return TaskStatus.Failure;
			}
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
