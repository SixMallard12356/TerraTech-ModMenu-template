using BehaviorDesigner.Runtime.Tasks;

public class CanSeeVisible : Conditional
{
	public SharedVisible m_Visible;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Visible != null && m_AI.Tech.Vision.CanSee(m_Visible.Value))
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
