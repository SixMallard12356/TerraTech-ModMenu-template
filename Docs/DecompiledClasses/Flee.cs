#define UNITY_EDITOR
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class Flee : Action
{
	public SharedVisible m_Target;

	public SharedFloat m_FleeDistance;

	public float m_LookAheadDistance;

	public TankControl.DriveRestriction m_DriveRestriction;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_AI != null)
		{
			d.Assert(m_Target != null, "Target is null, how did that happen");
			if (m_Target != null && m_Target.Value != null)
			{
				if (!m_Target.Value.gameObject.activeInHierarchy)
				{
					return TaskStatus.Success;
				}
				Vector3 boundsCentreWorld = m_AI.Tech.boundsCentreWorld;
				Vector3 vector = (boundsCentreWorld - m_Target.Value.trans.position).SetY(0f);
				bool flag = false;
				if (m_AI.UsePOI)
				{
					Vector3 vector2 = (m_AI.PointOfInterest - boundsCentreWorld).SetY(0f);
					if (vector2.magnitude >= m_AI.POIDist)
					{
						vector = vector2;
						flag = true;
					}
					else if (vector2.magnitude >= m_AI.POIDist * 0.9f)
					{
						Vector3 input = Vector3.Cross(Vector3.up, vector);
						float f = vector.normalized.DotClamped(vector2.normalized);
						float num = input.SetY(0f).normalized.DotClamped(vector2.normalized);
						float num2 = Mathf.Acos(f) * 57.29578f * 0.5f;
						if (num < 0f)
						{
							num2 = 0f - num2;
						}
						vector = Quaternion.AngleAxis(num2, Vector3.up) * vector;
					}
				}
				Vector3 targetPos = transform.position + m_AI.Tech.rbody.velocity + vector.normalized * m_LookAheadDistance;
				float num3 = ((m_FleeDistance != null) ? m_FleeDistance.Value : 100f);
				if (vector.sqrMagnitude >= num3 * num3 && !flag)
				{
					return TaskStatus.Success;
				}
				m_AI.Tech.control.Movement.DriveToPosition(m_AI.Tech, targetPos, 1f, m_DriveRestriction);
				return TaskStatus.Running;
			}
		}
		return TaskStatus.Failure;
	}

	private bool LeaderLookingAtAgent(Vector3 leaderToAgent)
	{
		return Vector3.Dot(m_Target.Value.trans.forward, leaderToAgent.normalized) > 0.5f;
	}
}
