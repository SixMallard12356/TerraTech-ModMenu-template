using System.Collections.Generic;

[FriendlyName("Heal damage from source")]
public class uScript_HealDamageFromSource : uScriptLogic
{
	private struct AutoRepair
	{
		public Damageable damageable;

		public TankBlock block;

		public float health;
	}

	private bool m_Started;

	private Tank m_SourceToCheck;

	private List<AutoRepair> m_AutoRepair = new List<AutoRepair>();

	public bool Out => true;

	public void In(Tank source)
	{
		if (!source)
		{
			return;
		}
		m_SourceToCheck = source;
		if (!m_Started && (bool)Singleton.playerTank)
		{
			Singleton.playerTank.DamageEvent.Subscribe(OnPlayerDamaged);
			BlockManager.BlockIterator<Damageable>.Enumerator enumerator = Singleton.playerTank.blockman.IterateBlockComponents<Damageable>().GetEnumerator();
			while (enumerator.MoveNext())
			{
				Damageable current = enumerator.Current;
				m_AutoRepair.Add(new AutoRepair
				{
					damageable = current,
					block = current.GetComponent<TankBlock>(),
					health = current.Health
				});
			}
			m_Started = true;
		}
	}

	private void OnPlayerDamaged(ManDamage.DamageInfo info)
	{
		if (!info.SourceTank || !(info.SourceTank == m_SourceToCheck) || !Singleton.playerTank)
		{
			return;
		}
		foreach (AutoRepair item in m_AutoRepair)
		{
			if (item.block.tank == Singleton.playerTank && item.damageable.Health < item.health)
			{
				item.damageable.Repair(info.Damage, sendEvent: false);
			}
		}
		BlockManager.BlockIterator<ModuleDamage>.Enumerator enumerator2 = Singleton.playerTank.blockman.IterateBlockComponents<ModuleDamage>().GetEnumerator();
		while (enumerator2.MoveNext())
		{
			enumerator2.Current.ResetDetachMeter();
		}
	}

	public void OnDisable()
	{
		if ((bool)Singleton.playerTank)
		{
			Singleton.playerTank.DamageEvent.Unsubscribe(OnPlayerDamaged);
		}
		m_Started = false;
		m_AutoRepair.Clear();
	}
}
