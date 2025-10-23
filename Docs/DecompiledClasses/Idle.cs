using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Idle : Action
{
	public float m_MinIdleTime = 3f;

	public float m_MaxIdleTime = 5f;

	public float m_RotateTime = 1f;

	public float m_MinAbsRotateAngle = 45f;

	public float m_MaxAbsRotateAngle = 90f;

	public SharedFloat m_MaxTurnThrottle = 1f;

	private TechAI m_AI;

	private bool m_Idling;

	private float m_IdleTime;

	private float m_Timer;

	private float m_TargetAngleOffset;

	private Vector3 m_FacingVec;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
		m_Idling = true;
		m_IdleTime = m_MinIdleTime;
	}

	public override TaskStatus OnUpdate()
	{
		m_Timer += Time.deltaTime;
		if (m_Idling)
		{
			if (m_Timer > m_IdleTime && m_RotateTime >= 0f)
			{
				m_Timer = 0f;
				m_Idling = false;
				m_FacingVec = m_AI.Tech.trans.forward;
				m_TargetAngleOffset = Random.Range(m_MinAbsRotateAngle, m_MaxAbsRotateAngle);
				if (Random.value < 0.5f)
				{
					m_TargetAngleOffset = 0f - m_TargetAngleOffset;
				}
				return TaskStatus.Success;
			}
		}
		else
		{
			float num = ((m_RotateTime > 0f) ? Mathf.Clamp01(Mathf.InverseLerp(0f, m_RotateTime, m_Timer)) : 1f);
			Vector3 direction = Quaternion.AngleAxis(m_TargetAngleOffset * num, Vector3.up) * m_FacingVec;
			float num2 = 1f - Mathf.Abs(num * 2f - 1f);
			num2 *= num2;
			m_AI.Tech.control.Movement.FaceDirection(m_AI.Tech, direction, num2 * m_MaxTurnThrottle.Value);
			if (m_Timer > m_RotateTime && m_MaxIdleTime >= 0f)
			{
				m_Timer = 0f;
				m_IdleTime = Random.Range(m_MinIdleTime, m_MaxIdleTime);
				m_Idling = true;
			}
		}
		return TaskStatus.Running;
	}
}
