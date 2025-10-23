using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/EnemyDestroyedHigherValueAchievement")]
public class EnemyDestroyedHigherValueAchievement : AchievementObject
{
	[SerializeField]
	private int m_MinValueDifference;

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyed);
	}

	private void OnTankDestroyed(Tank tank, ManDamage.DamageInfo info)
	{
		if (IsActive() && tank.IsEnemy(0) && tank.DamagedByPlayer && Singleton.playerTank != null && tank.OriginalValue >= (float)(Singleton.playerTank.GetValue() + m_MinValueDifference))
		{
			CompleteAchievement();
			Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankDestroyed);
		}
	}
}
