using UnityEngine;

public class uScript_SaveNamedTank : uScriptLogic
{
	private TrackedVisible m_LastSaved;

	private Encounter m_Data;

	public bool Out => true;

	public void In(Tank tank, string uniqueName, GameObject owner)
	{
		if (!owner || !tank)
		{
			return;
		}
		if (!m_Data)
		{
			m_Data = owner.GetComponent<Encounter>();
		}
		if (((bool)m_Data && m_LastSaved == null) || m_LastSaved.ID != tank.visible.ID)
		{
			TrackedVisible trackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(tank.visible.ID);
			if (trackedVisible == null)
			{
				m_LastSaved = new TrackedVisible(tank.visible.ID, tank.visible, ObjectTypes.Vehicle, (!tank.IsBase) ? RadarTypes.Vehicle : RadarTypes.Base);
				Singleton.Manager<ManVisible>.inst.TrackVisible(m_LastSaved);
			}
			else
			{
				m_LastSaved = trackedVisible;
			}
			m_Data.AddVisible(uniqueName, m_LastSaved, ObjectTypes.Vehicle);
		}
	}

	public void OnDisable()
	{
		m_LastSaved = null;
	}
}
