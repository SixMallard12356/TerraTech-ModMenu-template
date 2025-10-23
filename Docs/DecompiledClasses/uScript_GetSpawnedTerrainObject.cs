using UnityEngine;

[FriendlyName("Get Spawned TerrainObject")]
public class uScript_GetSpawnedTerrainObject : uScriptLogic
{
	private Encounter m_Encounter;

	private TrackedObjectReference m_TrackedObjectRef;

	private TrackableObject m_TrackedObject;

	public bool Out => true;

	public bool ObjectRefInvalid => m_TrackedObjectRef == null;

	public bool CurrentlySpawned => m_TrackedObject != null;

	public bool CurrentlyNotSpawned => m_TrackedObject == null;

	public GameObject In(GameObject ownerNode, string uniqueName)
	{
		m_TrackedObjectRef = null;
		m_TrackedObject = null;
		if (m_Encounter == null && ownerNode != null)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if (m_Encounter != null)
		{
			m_TrackedObjectRef = m_Encounter.GetTrackedObject(uniqueName);
			m_TrackedObject = ((m_TrackedObjectRef != null) ? m_TrackedObjectRef.TrackedObject : null);
		}
		if (!(m_TrackedObject != null))
		{
			return null;
		}
		return m_TrackedObject.gameObject;
	}

	public void OnDisable()
	{
		m_TrackedObjectRef = null;
		m_TrackedObject = null;
	}
}
