using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Is the vehicle upside down / on it's side")]
public class IsTankOverturned : Conditional
{
	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_AI && m_AI.IsTankOverturned())
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
