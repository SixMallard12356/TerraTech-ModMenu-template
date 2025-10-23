using UnityEngine;

[RequireComponent(typeof(ResourceDispenser))]
[RequireComponent(typeof(Damageable))]
public class DispenseOnDeath : MonoBehaviour
{
	[SerializeField]
	private ItemTypeInfo m_ItemToDispense;

	[SerializeField]
	private Transform m_SpawnSpot;

	public Event<Visible> OnDispense;

	private void OnDeath(Damageable damageable, ManDamage.DamageInfo damage)
	{
		if (damage.SourceTank != null)
		{
			OnDispense.Send(Singleton.Manager<ManSpawn>.inst.SpawnItem(m_ItemToDispense, m_SpawnSpot.position, m_SpawnSpot.rotation, addToObjectManager: true));
		}
	}

	private void OnPool()
	{
		GetComponent<Visible>().damageable.deathEvent.Subscribe(OnDeath);
	}
}
