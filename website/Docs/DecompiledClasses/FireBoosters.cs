using BehaviorDesigner.Runtime.Tasks;

public class FireBoosters : Action
{
	private Tank m_Tech;

	public override void OnAwake()
	{
		m_Tech = gameObject.GetComponent<Tank>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Tech != null)
		{
			m_Tech.control.Movement.FireBoosters(m_Tech);
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
