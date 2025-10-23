using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Asset/Achievements/EnemyDestroyedFromDistanceAchievement")]
public class EnemyDestroyedFromDistanceAchievement : AchievementObject
{
	[SerializeField]
	private int m_MinDistance;

	public override void Initialise()
	{
		base.Initialise();
		Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Subscribe(OnTankDestroyed);
	}

	private void OnTankDestroyed(Tank tank, ManDamage.DamageInfo info)
	{
		if (IsActive() && tank.IsEnemy(0) && info.SourceTank != null && info.SourceTank == Singleton.playerTank && Vector3.SqrMagnitude(tank.boundsCentreWorld - Singleton.playerPos) >= (float)(m_MinDistance * m_MinDistance))
		{
			CompleteAchievement();
			Singleton.Manager<ManTechs>.inst.TankDestroyedEvent.Unsubscribe(OnTankDestroyed);
		}
	}
}
