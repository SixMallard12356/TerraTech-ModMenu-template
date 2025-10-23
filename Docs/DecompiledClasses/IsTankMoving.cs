using BehaviorDesigner.Runtime.Tasks;

[TaskDescription("Is the Vehicle Moving?")]
public class IsTankMoving : Conditional
{
	[Tooltip("The minimum speed to be considered Moving")]
	public float m_MinimumSpeed;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_AI && m_AI.IsTankMoving(m_MinimumSpeed))
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
