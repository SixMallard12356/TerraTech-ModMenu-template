using BehaviorDesigner.Runtime.Tasks;

public class GetTargetVisible : Conditional
{
	public SharedVisible m_TargetVisible;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		m_TargetVisible.Value = null;
		if ((bool)m_AI)
		{
			Visible targetVisible = m_AI.GetTargetVisible();
			if ((bool)targetVisible)
			{
				m_TargetVisible.Value = targetVisible;
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Failure;
	}
}
