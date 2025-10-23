using UnityEngine;

public class uScript_SetEncounterTarget : uScriptLogic
{
	private Encounter m_DataComponent;

	public bool Out => true;

	public void In(GameObject owner, object visibleObject)
	{
		if ((bool)owner && !m_DataComponent)
		{
			m_DataComponent = owner.GetComponent<Encounter>();
		}
		if (!m_DataComponent || visibleObject == null)
		{
			return;
		}
		Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(visibleObject);
		if (visibleFromObject != null)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visibleFromObject.ID);
			if (trackedVisible != null)
			{
				m_DataComponent.SetTrackedWaypoint(trackedVisible);
			}
		}
	}
}
