using UnityEngine;

[FriendlyName("Spawn/Spawn embedded delivery cannon")]
public class uScript_SpawnEmbeddedDeliveryCannon : uScriptLogic
{
	private Transform m_Spawned;

	private bool m_Done;

	private bool m_Alive;

	private TrackedVisible m_DeliveryCannon;

	private Encounter m_DataComponent;

	private string m_UniqueName;

	private bool m_GetVisFromEncounter;

	public bool Out => true;

	public bool ResourceDestroyed => !m_Alive;

	public TankBlock In(Transform prefab, Vector3 position, string uniqueName, GameObject owner)
	{
		if (!m_Done && (bool)owner)
		{
			m_DataComponent = owner.GetComponent<Encounter>();
			m_UniqueName = uniqueName;
			m_Spawned = m_DataComponent.GetStoredObject(prefab.name, uniqueName);
			if (m_Spawned == null)
			{
				Visible.DisableAddToTileOnSpawn = true;
				m_Spawned = prefab.Spawn(null, position);
				Visible.DisableAddToTileOnSpawn = false;
				m_Spawned.name = prefab.name;
				Visible component = m_Spawned.GetComponent<Visible>();
				if (component != null)
				{
					component.StopManagingVisible();
				}
				m_DataComponent.AddStoredObject(m_Spawned, uniqueName);
			}
			m_Alive = m_Spawned.gameObject.activeInHierarchy;
			if (m_Alive)
			{
				DispenseOnDeath component2 = m_Spawned.GetComponent<DispenseOnDeath>();
				if ((bool)component2)
				{
					component2.OnDispense.Subscribe(OnCannonSpawned);
				}
				ResourceDispenser component3;
				if ((object)(component3 = m_Spawned.GetComponent<ResourceDispenser>()) != null)
				{
					component3.SetAwake(awake: true);
				}
			}
			else
			{
				m_GetVisFromEncounter = true;
			}
			m_Done = true;
		}
		if (m_GetVisFromEncounter)
		{
			EncounterVisibleData visible = m_DataComponent.GetVisible(uniqueName);
			if (visible != null && visible.m_VisibleId != -2)
			{
				m_DeliveryCannon = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.m_VisibleId);
				m_GetVisFromEncounter = false;
			}
		}
		if (m_DeliveryCannon == null)
		{
			return null;
		}
		if (!(m_DeliveryCannon.visible != null))
		{
			return null;
		}
		return m_DeliveryCannon.visible.block;
	}

	private void OnCannonSpawned(Visible vis)
	{
		m_Alive = false;
		m_DeliveryCannon = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(vis.ID);
		m_DataComponent.AddVisible(m_UniqueName, m_DeliveryCannon, ObjectTypes.Block);
		ResourceDispenser component;
		if ((object)(component = m_Spawned.GetComponent<ResourceDispenser>()) != null)
		{
			component.SetAwake(awake: false);
		}
		UnsubscribeDeathDispenseEvent();
	}

	private void UnsubscribeDeathDispenseEvent()
	{
		if ((bool)m_Spawned)
		{
			m_Spawned.GetComponent<DispenseOnDeath>().OnDispense.Unsubscribe(OnCannonSpawned);
		}
	}

	public void OnDisable()
	{
		ResourceDispenser component;
		if (m_Alive && m_Spawned != null && (object)(component = m_Spawned.GetComponent<ResourceDispenser>()) != null)
		{
			component.SetAwake(awake: false);
		}
		UnsubscribeDeathDispenseEvent();
		m_Alive = false;
		m_DeliveryCannon = null;
		m_Done = false;
		m_Spawned = null;
	}
}
