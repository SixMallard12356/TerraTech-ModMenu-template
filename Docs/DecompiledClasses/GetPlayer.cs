using BehaviorDesigner.Runtime.Tasks;

public class GetPlayer : Conditional
{
	public SharedVisible m_Player;

	public override TaskStatus OnUpdate()
	{
		if ((bool)Singleton.playerTank)
		{
			m_Player.Value = Singleton.playerTank.visible;
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
