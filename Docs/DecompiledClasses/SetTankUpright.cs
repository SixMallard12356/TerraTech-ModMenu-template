using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Sets the vehicle the correct way up. Returns RUNNING until it's righted, then SUCCESS")]
public class SetTankUpright : Conditional
{
	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_AI)
		{
			if (m_AI.RightTank())
			{
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}
		return TaskStatus.Failure;
	}
}
