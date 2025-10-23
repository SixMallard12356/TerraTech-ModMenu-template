using BehaviorDesigner.Runtime.Tasks;

public class IsTechAnchored : Conditional
{
	private Tank m_Tech;

	public override void OnAwake()
	{
		m_Tech = GetComponent<Tank>();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_Tech && m_Tech.IsAnchored)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
