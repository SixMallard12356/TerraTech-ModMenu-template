using UnityEngine;

public class uScript_MoveEncounterWithVisible : uScriptLogic
{
	private Encounter m_DataComponent;

	public bool Out => true;

	public void In(GameObject ownerNode, object visibleObject)
	{
		if (!ownerNode)
		{
			return;
		}
		if (!m_DataComponent)
		{
			m_DataComponent = ownerNode.GetComponent<Encounter>();
		}
		if (visibleObject == null)
		{
			return;
		}
		Visible visibleFromObject = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(visibleObject);
		if (visibleFromObject != null)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visibleFromObject.ID);
			if (trackedVisible != null)
			{
				m_DataComponent.FollowTrackedVisible(trackedVisible);
			}
		}
	}
}
