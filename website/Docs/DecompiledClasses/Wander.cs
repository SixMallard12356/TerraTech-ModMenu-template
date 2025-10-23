using System;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Wander : BehaviorDesigner.Runtime.Tasks.Action
{
	public float m_WanderAmount = 35f;

	public float m_CircleRadius = 5f;

	public float m_LookAheadDistance = 5f;

	private TechAI m_AI;

	private float m_Angle;

	private Vector3 m_NewDir;

	private Vector3 m_WanderPos;

	public override void OnAwake()
	{
		m_AI = gameObject.GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		m_NewDir = new Quaternion(0f, 1f, 0f, m_Angle * ((float)Math.PI / 180f)) * Vector3.forward;
		m_WanderPos = m_AI.Tech.boundsCentreWorld + m_NewDir * m_LookAheadDistance;
		m_AI.Tech.control.Movement.DriveToPosition(m_AI.Tech, m_WanderPos, 1f);
		m_Angle += UnityEngine.Random.value * m_WanderAmount - m_WanderAmount * 0.5f;
		return TaskStatus.Running;
	}

	public override void OnDrawGizmos()
	{
		gameObject.EditorSelectedSingle();
	}
}
