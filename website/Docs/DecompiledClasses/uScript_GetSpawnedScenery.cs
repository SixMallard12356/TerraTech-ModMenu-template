#define UNITY_EDITOR
using UnityEngine;

public class uScript_GetSpawnedScenery : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public ResourceDispenser In(GameObject ownerNode, string uniqueSceneryName)
	{
		ResourceDispenser result = null;
		if (m_Encounter == null && ownerNode != null)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if (m_Encounter != null)
		{
			TrackedObjectReference trackedObject = m_Encounter.GetTrackedObject(uniqueSceneryName);
			if (trackedObject != null)
			{
				TerrainObject terrainObject = trackedObject.TrackedObject as TerrainObject;
				result = ((terrainObject != null) ? terrainObject.GetComponent<ResourceDispenser>() : null);
			}
			else
			{
				d.LogError("uScript_GetSceneryState - Failed to find scenery '" + uniqueSceneryName + "' in encounter '" + m_Encounter.EncounterName + "'!");
			}
		}
		return result;
	}

	public void OnDisable()
	{
		m_Encounter = null;
	}
}
