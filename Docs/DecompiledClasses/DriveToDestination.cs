using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class DriveToDestination : Action
{
	public SharedVisible m_Target;

	public TankControl.DriveRestriction m_Restriction;

	public bool m_DriveToPOI;

	public bool m_StayWithinPOI;

	public bool m_UseArrivalTollerance = true;

	public float m_ObjectRadius = 3f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = gameObject.GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_AI != null && (m_DriveToPOI || (m_Target.Value != null && m_Target.Value.gameObject.activeSelf)))
		{
			bool flag = false;
			Vector3 vector = (m_DriveToPOI ? m_AI.PointOfInterest : m_Target.Value.trans.position);
			if (m_StayWithinPOI && m_AI.UsePOI)
			{
				Vector3 vector2 = (vector - m_AI.PointOfInterest).SetY(0f);
				if (vector2.sqrMagnitude >= m_AI.POIDist * m_AI.POIDist)
				{
					vector = m_AI.PointOfInterest + vector2.normalized * m_AI.POIDist;
					flag = true;
				}
			}
			if (m_DriveToPOI || flag)
			{
				if (m_AI.Tech.control.Movement.DriveToPosition(m_AI.Tech, vector, 1f, m_Restriction))
				{
					return TaskStatus.Success;
				}
			}
			else if (m_AI.Tech.control.Movement.DriveToVisible(m_AI.Tech, m_Target.Value, 1f, m_Restriction))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}
}
