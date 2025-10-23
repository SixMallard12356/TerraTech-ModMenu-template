using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetInvasionRanges : Action
{
	public SharedFloat m_InvasionRange;

	public SharedFloat m_InvasionChaseRange;

	public override TaskStatus OnUpdate()
	{
		if (m_InvasionRange != null && m_InvasionChaseRange != null && (bool)Singleton.Manager<ManInvasion>.inst)
		{
			m_InvasionRange.Value = Singleton.Manager<ManInvasion>.inst.GetInvasionRange(includeChaseRange: false);
			m_InvasionChaseRange.Value = Singleton.Manager<ManInvasion>.inst.GetInvasionRange(includeChaseRange: true);
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
