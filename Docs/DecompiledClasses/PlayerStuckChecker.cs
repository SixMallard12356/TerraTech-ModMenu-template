using System;
using UnityEngine;

[Serializable]
public class PlayerStuckChecker
{
	[SerializeField]
	private float m_PlayerStuckDuration = 5f;

	[SerializeField]
	private float m_PlayerStuckMinDistMoved = 10f;

	private float m_PlayerStuckTimer;

	private bool m_HasPrevPos;

	private WorldPosition m_PrevPos;

	private bool m_Stuck;

	public bool Stuck => m_Stuck;

	private PlayerStuckChecker()
	{
		Reset();
	}

	public void Reset()
	{
		m_PlayerStuckTimer = 0f;
		m_HasPrevPos = false;
		m_PrevPos = default(WorldPosition);
		m_Stuck = false;
	}

	public void Update()
	{
		m_PlayerStuckTimer += Time.deltaTime;
		Tank playerTank = Singleton.playerTank;
		if (playerTank == null || playerTank.beam.IsActive)
		{
			m_PlayerStuckTimer = 0f;
		}
		if ((bool)playerTank)
		{
			if (m_HasPrevPos)
			{
				if ((playerTank.rbody.position - m_PrevPos.ScenePosition).sqrMagnitude >= m_PlayerStuckMinDistMoved * m_PlayerStuckMinDistMoved)
				{
					m_PrevPos = WorldPosition.FromScenePosition(playerTank.rbody.position);
					m_PlayerStuckTimer = 0f;
				}
			}
			else
			{
				m_PrevPos = WorldPosition.FromScenePosition(playerTank.rbody.position);
				m_HasPrevPos = true;
			}
		}
		else
		{
			m_PlayerStuckTimer = 0f;
			m_HasPrevPos = false;
		}
		m_Stuck = m_PlayerStuckTimer >= m_PlayerStuckDuration;
	}
}
