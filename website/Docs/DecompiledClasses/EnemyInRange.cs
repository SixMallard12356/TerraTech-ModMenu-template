using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class EnemyInRange : Conditional
{
	public SharedVisible m_Target;

	public float m_Angle = 360f;

	private TechAI m_AI;

	private Visible m_Closest;

	private float m_SqrDistClosest = float.MaxValue;

	public override void OnAwake()
	{
		m_AI = GetComponent<TechAI>();
	}

	public override TaskStatus OnUpdate()
	{
		if ((bool)m_AI)
		{
			UpdateVision();
			_ = m_Closest == null;
			if ((bool)m_Closest)
			{
				m_Target.Value = m_Closest;
				if (m_Closest.tank == Singleton.playerTank)
				{
					if (Mode<ModeMain>.inst != null)
					{
						Mode<ModeMain>.inst.SetPlayerInDanger(inDanger: true, forceAutoSnapshot: true);
					}
					Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.Enemy, m_AI.Tech, m_Closest.tank);
				}
				else if (m_Closest.tank.IsFriendly())
				{
					float sqrMagnitude = (Singleton.playerPos - m_AI.Tech.trans.position).sqrMagnitude;
					float num = ((Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD) ? Globals.inst.m_DangerPlayerRaDRadius : Globals.inst.m_DangerPlayerRadius);
					float num2 = num * num;
					if (sqrMagnitude < num2)
					{
						Singleton.Manager<ManMusic>.inst.SetDanger(ManMusic.DangerContext.Circumstance.Enemy, m_AI.Tech, m_Closest.tank);
					}
				}
				if (Singleton.Manager<ManNetwork>.inst.IsServer)
				{
					foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
					{
						if (currentTech != Singleton.playerTank && currentTech.IsFriendly() && currentTech.netTech != null && currentTech.netTech.NetPlayer != null && m_Closest.tank.IsFriendly())
						{
							float sqrMagnitude2 = (currentTech.transform.position - m_AI.Tech.trans.position).sqrMagnitude;
							float num3 = ((Singleton.Manager<ManGameMode>.inst.GetCurrentGameType() == ManGameMode.GameType.RaD) ? Globals.inst.m_DangerPlayerRaDRadius : Globals.inst.m_DangerPlayerRadius);
							float num4 = num3 * num3;
							if (sqrMagnitude2 < num4)
							{
								Singleton.Manager<ManMusic>.inst.SetDangerClient(ManMusic.DangerContext.Circumstance.Enemy, m_AI.Tech, currentTech);
							}
						}
					}
				}
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Failure;
	}

	private void UpdateVision()
	{
		m_Closest = null;
		m_SqrDistClosest = float.MaxValue;
		float num = m_Angle / 2f;
		Vector3 position = m_AI.Tech.trans.position;
		float num2 = m_AI.Tech.Vision.SearchRadius * m_AI.Tech.Vision.SearchRadius;
		foreach (Tank currentTech in Singleton.Manager<ManTechs>.inst.CurrentTechs)
		{
			if (!currentTech.IsEnemy(m_AI.Tech.Team))
			{
				continue;
			}
			Vector3 vector = currentTech.trans.position - position;
			float sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude < m_SqrDistClosest && sqrMagnitude <= num2)
			{
				float f = vector.normalized.Dot(m_AI.Tech.trans.forward);
				f = Mathf.Acos(f) * 57.29578f;
				if (m_Angle >= 360f || f <= num)
				{
					m_Closest = currentTech.visible;
					m_SqrDistClosest = sqrMagnitude;
				}
			}
		}
	}
}
