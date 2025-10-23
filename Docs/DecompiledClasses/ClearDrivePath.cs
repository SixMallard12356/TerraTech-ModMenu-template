using BehaviorDesigner.Runtime.Tasks;

public class ClearDrivePath : Action
{
	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = gameObject.GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_AI != null)
		{
			m_AI.ClearPath();
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
