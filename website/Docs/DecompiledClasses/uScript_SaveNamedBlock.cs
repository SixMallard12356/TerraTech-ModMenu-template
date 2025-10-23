using UnityEngine;

public class uScript_SaveNamedBlock : uScriptLogic
{
	private TrackedVisible m_LastSaved;

	private Encounter m_Data;

	public bool Out => true;

	public void In(TankBlock block, string uniqueName, GameObject owner)
	{
		if ((bool)owner && (bool)block)
		{
			if (!m_Data)
			{
				m_Data = owner.GetComponent<Encounter>();
			}
			if (((bool)m_Data && m_LastSaved == null) || m_LastSaved.ID != block.visible.ID)
			{
				m_LastSaved = new TrackedVisible(block.visible.ID, block.visible, ObjectTypes.Block, RadarTypes.Block);
				Singleton.Manager<ManVisible>.inst.TrackVisible(m_LastSaved);
				m_Data.AddVisible(uniqueName, m_LastSaved, ObjectTypes.Block);
			}
		}
	}

	public void OnDisable()
	{
		m_LastSaved = null;
	}
}
