using BehaviorDesigner.Runtime.Tasks;

public class DriveAlongPath : Action
{
	public SharedVisible m_Target;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = gameObject.GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_AI != null)
		{
			if (m_AI.DriveToTarget(m_Target.Value))
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}
}
