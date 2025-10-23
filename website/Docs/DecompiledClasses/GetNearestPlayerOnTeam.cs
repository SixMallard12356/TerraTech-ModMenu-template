using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetNearestPlayerOnTeam : Conditional
{
	public SharedVisible m_Player;

	public SharedFloat m_MaxRange = 30f;

	private TechAI m_AI;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		m_Player.Value = null;
		if (ManSpawn.IsPlayerTeam(m_AI.Tech.Team))
		{
			if (Singleton.Manager<ManNetwork>.inst.IsMultiplayer())
			{
				float num = m_MaxRange.Value * m_MaxRange.Value;
				Visible visible = null;
				for (int i = 0; i < Singleton.Manager<ManNetwork>.inst.GetNumPlayers(); i++)
				{
					NetPlayer player = Singleton.Manager<ManNetwork>.inst.GetPlayer(i);
					if (player.TechTeamID == m_AI.Tech.Team && player.CurTech != null)
					{
						float sqrMagnitude = (player.CurTech.transform.position - m_AI.transform.position).sqrMagnitude;
						if (sqrMagnitude <= num)
						{
							num = sqrMagnitude;
							visible = player.CurTech.tech.visible;
						}
					}
				}
				if (visible != null)
				{
					m_Player.Value = visible;
					return TaskStatus.Success;
				}
			}
			else if ((bool)Singleton.playerTank && (Singleton.playerTank.transform.position - m_AI.transform.position).sqrMagnitude <= m_MaxRange.Value * m_MaxRange.Value)
			{
				m_Player.Value = Singleton.playerTank.visible;
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Failure;
	}
}
