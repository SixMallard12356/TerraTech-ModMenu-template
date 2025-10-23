using BehaviorDesigner.Runtime.Tasks;

public class DetonateExplosiveBolts : Action
{
	public bool m_ApplyToPlayerTeam;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if (m_ApplyToPlayerTeam || !ManSpawn.IsPlayerTeam(m_AI.Tech.Team))
		{
			m_AI.Tech.control.ServerDetonateExplosiveBolt();
		}
		return TaskStatus.Success;
	}
}
