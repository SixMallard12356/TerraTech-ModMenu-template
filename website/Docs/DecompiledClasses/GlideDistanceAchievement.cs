using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/GlideDistanceAchievement")]
public class GlideDistanceAchievement : AchievementObject
{
	[SerializeField]
	private float m_DistanceToGlide;

	[SerializeField]
	private bool m_RequiresWings;

	private float m_CurrentGlideDistance;

	private int m_CurrentGlidingTechVisID = -1;

	public override void Update()
	{
		base.Update();
		if (!IsActive() || !(Singleton.playerTank != null))
		{
			return;
		}
		bool flag = true;
		if (m_CurrentGlidingTechVisID != Singleton.playerTank.visible.ID)
		{
			flag = false;
			m_CurrentGlidingTechVisID = Singleton.playerTank.visible.ID;
		}
		else if (Singleton.playerTank.grounded || Singleton.playerTank.Boosters.BoostersFiring)
		{
			flag = false;
		}
		else
		{
			if (m_RequiresWings)
			{
				bool flag2 = false;
				BlockManager.BlockIterator<ModuleWing>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlockComponents<ModuleWing>().GetEnumerator();
				if (enumerator.MoveNext())
				{
					_ = enumerator.Current;
					flag2 = true;
				}
				if (!flag2)
				{
					flag = false;
				}
			}
			if (flag)
			{
				BlockManager.BlockIterator<ModuleBooster>.Enumerator enumerator2 = Singleton.playerTank.blockman.IterateBlockComponents<ModuleBooster>().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					ModuleBooster current = enumerator2.Current;
					if (current.IsFiring || current.FireRate > 0.1f)
					{
						flag = false;
						break;
					}
				}
			}
			if (flag)
			{
				float num = Singleton.playerTank.rbody.velocity.SetY(0f).magnitude * Time.deltaTime;
				m_CurrentGlideDistance += num;
				if (m_CurrentGlideDistance >= m_DistanceToGlide)
				{
					CompleteAchievement();
				}
			}
		}
		if (!flag)
		{
			m_CurrentGlideDistance = 0f;
		}
	}
}
