using UnityEngine;

public class uScript_UpdateEncounterPosFromBlock : uScriptLogic
{
	private Encounter m_DataComponent;

	private TrackedVisible m_TrackedVisible;

	private TankBlock m_Block;

	public bool Out => true;

	public void In(GameObject ownerNode, TankBlock block)
	{
		if ((bool)ownerNode)
		{
			if (!m_DataComponent)
			{
				m_DataComponent = ownerNode.GetComponent<Encounter>();
			}
			if (m_Block != block && block != null)
			{
				m_Block = block;
				m_TrackedVisible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(m_Block.visible.ID);
			}
			m_DataComponent.FollowTrackedVisible(m_TrackedVisible);
		}
	}

	public void OnDisable()
	{
		m_DataComponent = null;
		m_TrackedVisible = null;
		m_Block = null;
	}
}
