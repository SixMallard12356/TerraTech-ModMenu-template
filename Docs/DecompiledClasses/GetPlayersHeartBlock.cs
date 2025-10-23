using BehaviorDesigner.Runtime.Tasks;

public class GetPlayersHeartBlock : Conditional
{
	public SharedVisible m_HeartBlock;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		TrackedVisible nearestHeartBlock = Singleton.Manager<ManPlayer>.inst.GetNearestHeartBlock(m_AI.Tech.visible.centrePosition);
		if (m_HeartBlock != null)
		{
			m_HeartBlock.Value = nearestHeartBlock?.visible;
		}
		if (nearestHeartBlock != null)
		{
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
