using UnityEngine;

[FriendlyName("Techs/Destroy Techs From Data")]
public class uScript_DestroyTechsFromData : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(SpawnTechData[] techData, bool shouldExplode, GameObject ownerNode)
	{
		Tank[] array = new Tank[techData.Length];
		if (!m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (!techData[i].CanSpawnOnCurrentSKU())
			{
				continue;
			}
			Visible visible;
			Encounter.EncounterVisibleState visibleState = m_Encounter.GetVisibleState(techData[i].UniqueName, out visible);
			switch (visibleState)
			{
			case Encounter.EncounterVisibleState.NotCurrentlySpawned:
			{
				EncounterVisibleData visible2 = m_Encounter.GetVisible(techData[i].UniqueName);
				Singleton.Manager<ManVisible>.inst.ObliterateTrackedVisibleFromWorld(visible2.m_VisibleId);
				m_Encounter.RemoveDeadVisible(techData[i].UniqueName);
				continue;
			}
			case Encounter.EncounterVisibleState.NotFoundInEncounter:
			case Encounter.EncounterVisibleState.TrackedVisibleNotFound:
				continue;
			}
			array[i] = (visible ? visible.tank : null);
			if (visibleState == Encounter.EncounterVisibleState.Killed)
			{
				continue;
			}
			if (shouldExplode)
			{
				BlockManager.BlockIterator<TankBlock>.Enumerator enumerator = array[i].blockman.IterateBlocks().GetEnumerator();
				while (enumerator.MoveNext())
				{
					Damageable damageable = enumerator.Current.visible.damageable;
					if ((bool)damageable)
					{
						damageable.SetInvulnerable(invulnerable: false, unlimitedInvulnerability: false);
						Singleton.Manager<ManDamage>.inst.DealDamage(damageable, damageable.MaxHealth * 999f, ManDamage.DamageType.Standard, m_Encounter);
					}
				}
			}
			else
			{
				visible.RemoveFromGame();
			}
		}
	}
}
