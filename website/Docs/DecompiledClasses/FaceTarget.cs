using BehaviorDesigner.Runtime.Tasks;

public class FaceTarget : Action
{
	public SharedVisible m_Target;

	public float m_FacingAngleThreshold = 5f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = gameObject.GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Target != null && m_Target.Value != null)
		{
			if (m_AI.Tech.control.Movement.IsFacingVisible(m_AI.Tech, m_Target.Value, m_FacingAngleThreshold))
			{
				return TaskStatus.Success;
			}
			m_AI.Tech.control.Movement.FaceVisible(m_AI.Tech, m_Target.Value, 1f);
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}
}
