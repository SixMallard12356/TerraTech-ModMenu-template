using UnityEngine;

[FriendlyName("Spawn/Spawn encounter block")]
public class uScript_SpawnBlockForEncounter : uScriptLogic
{
	private TankBlock m_Block;

	private TrackedVisible m_Visible;

	private bool m_Spawned;

	private Encounter m_DataComponent;

	private string m_UniqueName;

	private bool m_WasDragged;

	public bool Out => true;

	public TankBlock In(Vector3 position, BlockTypes block, GameObject owner, string uniqueName)
	{
		if (!m_Spawned)
		{
			m_UniqueName = uniqueName;
			if ((bool)owner && !m_DataComponent)
			{
				m_DataComponent = owner.GetComponent<Encounter>();
			}
			EncounterVisibleData visible = m_DataComponent.GetVisible(uniqueName);
			if (visible != null && visible.m_VisibleId != -1)
			{
				m_Visible = Singleton.Manager<ManVisible>.inst.GetTrackedVisible(visible.m_VisibleId);
				m_Spawned = true;
			}
			else if (!visible)
			{
				m_Visible = Singleton.Manager<ManSpawn>.inst.SpawnItemRef(new ItemTypeInfo(ObjectTypes.Block, (int)block), position + Vector3.up, Quaternion.identity, addToObjectManager: true, forceSpawn: false);
				m_DataComponent.AddVisible(m_UniqueName, m_Visible, ObjectTypes.Block);
				m_Spawned = true;
			}
			m_WasDragged = false;
		}
		if (m_Spawned && m_Visible != null)
		{
			if (m_Visible.visible != null)
			{
				m_Block = m_Visible.visible.block;
			}
			else
			{
				m_Block = null;
			}
		}
		if ((bool)m_Block)
		{
			m_Block.visible.KeepAwake();
			if (Singleton.Manager<ManPointer>.inst.DraggingItem != m_Block.visible && !m_WasDragged && !m_Block.tank)
			{
				m_Block.rbody.velocity = Vector3.zero;
			}
			else if (Singleton.Manager<ManPointer>.inst.DraggingItem == m_Block.visible || (bool)m_Block.tank)
			{
				m_WasDragged = true;
			}
		}
		return m_Block;
	}

	public void OnDisable()
	{
		m_Spawned = false;
		m_Block = null;
		m_Visible = null;
	}
}
