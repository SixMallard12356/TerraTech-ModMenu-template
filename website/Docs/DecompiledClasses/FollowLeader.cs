using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class FollowLeader : Action
{
	public SharedVisible m_Target;

	public SharedFloat m_FollowDistance;

	public SharedFloat m_LookAheadDistance;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Target != null && m_Target.Value != null && m_FollowDistance != null && m_LookAheadDistance != null)
		{
			Vector3 targetPos = m_Target.Value.trans.position + m_Target.Value.trans.forward * (0f - m_FollowDistance.Value);
			Vector3 vector = m_AI.Tech.trans.position - m_Target.Value.trans.position;
			if (m_Target.Value.trans.forward.Dot(vector.normalized) > 0.5f && vector.sqrMagnitude < m_LookAheadDistance.Value)
			{
				targetPos = m_AI.Tech.trans.position + vector.normalized * m_LookAheadDistance.Value;
			}
			m_AI.Tech.control.Movement.DriveToPosition(m_AI.Tech, targetPos, 1f);
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}

	private bool LeaderLookingAtAgent(Vector3 leaderToAgent)
	{
		return Vector3.Dot(m_Target.Value.trans.forward, leaderToAgent.normalized) > 0.5f;
	}
}
