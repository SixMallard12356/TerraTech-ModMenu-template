using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class GetLastAttacker : Conditional
{
	public SharedVisible m_LastAttacker;

	public bool m_UseTime = true;

	public float m_WithinSeconds = 5f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		m_LastAttacker.Value = null;
		if ((bool)m_AI.LastAttacker && (!m_UseTime || Time.time - m_AI.LastAttackedTime <= m_WithinSeconds))
		{
			m_LastAttacker.Value = m_AI.LastAttacker;
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
