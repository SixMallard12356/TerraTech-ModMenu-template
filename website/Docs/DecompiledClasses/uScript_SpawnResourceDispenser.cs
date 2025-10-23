#define UNITY_EDITOR
using UnityEngine;

[NodeToolTip("prefab MUST reside in the Resources\\Prefabs\\EncounterObjects Folder")]
public class uScript_SpawnResourceDispenser : uScriptLogic
{
	private Encounter m_Encounter;

	private ResourceDispenser m_ResourceDispenser;

	private bool m_Spawned;

	private bool m_Dead;

	public bool Out => true;

	public bool Alive => !m_Dead;

	public bool Dead
	{
		get
		{
			if (m_Spawned)
			{
				return m_Dead;
			}
			return false;
		}
	}

	public ResourceDispenser In(Transform prefab, Vector3 position, string uniqueName, GameObject owner)
	{
		if ((bool)owner)
		{
			m_Encounter = owner.GetComponent<Encounter>();
			if (m_Encounter != null)
			{
				Transform transform = m_Encounter.GetStoredObject(prefab.name, uniqueName);
				if (transform == null)
				{
					position = Singleton.Manager<ManWorld>.inst.ProjectToGround(position);
					transform = prefab.Spawn(position);
					float yAngle = Random.Range(0f, 360f);
					transform.Rotate(0f, yAngle, 0f);
					transform.name = prefab.name;
					m_Encounter.AddStoredObject(transform, uniqueName);
				}
				m_ResourceDispenser = GetDispenserFromTransform(transform);
				if ((bool)m_ResourceDispenser)
				{
					m_ResourceDispenser.visible.damageable.deathEvent.Subscribe(OnDeath);
				}
				m_Spawned = true;
			}
			else
			{
				DebugUtil.AssertRelease(condition: false, "uScript_SpawnResourceDispenser - Encounter Null");
			}
		}
		return m_ResourceDispenser;
	}

	private ResourceDispenser GetDispenserFromTransform(Transform resTrans)
	{
		return resTrans.GetComponent<ResourceDispenser>();
	}

	private void OnDeath(Damageable deadThing, ManDamage.DamageInfo info)
	{
		m_Dead = true;
	}

	public void OnDisable()
	{
		m_Encounter = null;
		m_ResourceDispenser = null;
		m_Spawned = false;
	}
}
