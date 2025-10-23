using BehaviorDesigner.Runtime.Tasks;

public class AttackTarget : Action
{
	public SharedVisible m_Target;

	public bool m_UntilDestroyed;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_Target.Value != null)
		{
			m_AI.Tech.control.Weapons.FireAtTarget(m_AI.Tech, m_Target.Value.trans.position, 1f);
			if (m_UntilDestroyed && m_Target.Value.isActive)
			{
				return TaskStatus.Running;
			}
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
