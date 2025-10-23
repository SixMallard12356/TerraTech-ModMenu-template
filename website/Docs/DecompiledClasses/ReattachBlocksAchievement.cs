#define UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/ReattachBlocksAchievement")]
public class ReattachBlocksAchievement : AchievementObject
{
	private struct DetachedBlockData
	{
		public int blockID;

		public float detachTime;
	}

	[SerializeField]
	private IntStat m_BlocksReattachedStat;

	[SerializeField]
	private bool m_OnlyFromEnemyFire = true;

	[SerializeField]
	private int m_NumBlocksToReattach;

	[SerializeField]
	private float m_TimeToReattachWithin;

	private List<DetachedBlockData> m_BlocksDetachedAsResultOfDamage = new List<DetachedBlockData>(16);

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Subscribe(OnPlayerTankChanged);
		Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Subscribe(OnBlockAttached);
		Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Subscribe(OnBlockDetached);
		d.Log("[ReattachBlocksAchievement] Initialise - m_BlocksReattachedStat.IntValue = " + m_BlocksReattachedStat.IntValue);
	}

	public override void Update()
	{
		base.Update();
		if (!IsActive())
		{
			return;
		}
		float currentModeRunningTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime();
		for (int num = m_BlocksDetachedAsResultOfDamage.Count - 1; num >= 0; num--)
		{
			if (currentModeRunningTime > m_BlocksDetachedAsResultOfDamage[num].detachTime + m_TimeToReattachWithin)
			{
				m_BlocksDetachedAsResultOfDamage.RemoveAt(num);
			}
		}
	}

	private void OnPlayerTankChanged(Tank tank, bool playerFocus)
	{
		if (IsActive() && playerFocus)
		{
			m_BlocksDetachedAsResultOfDamage.Clear();
		}
	}

	private void OnBlockAttached(Tank tech, TankBlock block)
	{
		if (!IsActive() || !(tech == Singleton.playerTank))
		{
			return;
		}
		int num = m_BlocksDetachedAsResultOfDamage.FindIndex((DetachedBlockData x) => x.blockID == block.visible.ID);
		if (num < 0)
		{
			return;
		}
		DetachedBlockData detachedBlockData = m_BlocksDetachedAsResultOfDamage[num];
		if (Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime() < detachedBlockData.detachTime + m_TimeToReattachWithin)
		{
			m_BlocksReattachedStat.IntValue++;
			UpdateStat(m_BlocksReattachedStat);
			d.Log("[ReattachBlocksAchievement]OnBlockAttached: inc stat - m_BlocksReattachedStat.IntValue = " + m_BlocksReattachedStat.IntValue);
			m_BlocksDetachedAsResultOfDamage.RemoveAt(num);
			d.Log("[ReattachBlocksAchievement] Complete achievement test : m_BlocksReattachedStat.IntValue (" + m_BlocksReattachedStat.IntValue + ") >= m_NumBlocksToReattach (" + m_NumBlocksToReattach + ")");
			if (m_BlocksReattachedStat.IntValue >= m_NumBlocksToReattach)
			{
				d.Log("[ReattachBlocksAchievement] Complete! ");
				CompleteAchievement();
				Singleton.Manager<ManTechs>.inst.PlayerTankChangedEvent.Unsubscribe(OnPlayerTankChanged);
				Singleton.Manager<ManTechs>.inst.TankBlockAttachedEvent.Unsubscribe(OnBlockAttached);
				Singleton.Manager<ManTechs>.inst.TankBlockDetachedEvent.Unsubscribe(OnBlockDetached);
				m_BlocksDetachedAsResultOfDamage.Clear();
			}
		}
	}

	private void OnBlockDetached(Tank tech, TankBlock block)
	{
		if (IsActive() && tech == Singleton.playerTank && tech.DamageInEffect.Damage > 0f && (!m_OnlyFromEnemyFire || (tech.DamageInEffect.SourceTank != null && tech.DamageInEffect.SourceTank.IsEnemy(0))))
		{
			DetachedBlockData item = new DetachedBlockData
			{
				blockID = block.visible.ID,
				detachTime = Singleton.Manager<ManGameMode>.inst.GetCurrentModeRunningTime()
			};
			d.Assert(m_BlocksDetachedAsResultOfDamage.FindIndex((DetachedBlockData x) => x.blockID == block.visible.ID) == -1, "ReattachBlocksAchievement.OnBlockDetached - Detached block was already in our buffer of detached blocks!");
			m_BlocksDetachedAsResultOfDamage.Add(item);
		}
	}
}
