using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ChargeAtVisible : Action
{
	public SharedVisible m_Target;

	public bool m_AttackWhilstCharging;

	public float m_AttackRange = 10f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = gameObject.GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_AI != null && m_Target.Value != null && m_Target.Value.gameObject.activeSelf)
		{
			Vector3 position = m_Target.Value.trans.position;
			if (m_AttackWhilstCharging && (position - m_AI.Tech.trans.position).SetY(0f).sqrMagnitude <= m_AttackRange * m_AttackRange)
			{
				m_AI.Tech.control.Weapons.FireAtTarget(m_AI.Tech, m_Target.Value.trans.position, 1f);
			}
			float arrivalDist = 0f;
			if (m_AI.Tech.control.Movement.DriveToPosition(m_AI.Tech, position, 1f, TankControl.DriveRestriction.ForwardOnly, m_Target.Value, arrivalDist))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}
}
