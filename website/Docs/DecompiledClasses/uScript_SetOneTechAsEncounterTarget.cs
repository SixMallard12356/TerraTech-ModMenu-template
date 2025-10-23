using UnityEngine;

public class uScript_SetOneTechAsEncounterTarget : uScriptLogic
{
	private Encounter m_DataComponent;

	public bool Out => true;

	public Tank In(GameObject owner, Tank[] techs)
	{
		if ((bool)owner && !m_DataComponent)
		{
			m_DataComponent = owner.GetComponent<Encounter>();
		}
		bool flag = false;
		Tank tank = null;
		if ((bool)m_DataComponent && techs != null)
		{
			for (int i = 0; i < techs.Length; i++)
			{
				if (!(techs[i] != null) || flag)
				{
					continue;
				}
				tank = techs[i];
				Visible visible = tank.visible;
				if (visible != null)
				{
					TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.ID);
					if (trackedVisible != null)
					{
						m_DataComponent.SetTrackedWaypoint(trackedVisible);
						flag = true;
					}
				}
			}
		}
		return tank;
	}

	public void OnDisable()
	{
	}
}
