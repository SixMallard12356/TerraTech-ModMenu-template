using UnityEngine;

[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_KeepVisibleInEncounterArea : uScriptLogic
{
	private Encounter m_Encounter;

	private bool m_PositionResetThisFrame;

	public bool Out => true;

	[FriendlyName("Inside Area", "The visible is within the area")]
	public bool InsideArea => !m_PositionResetThisFrame;

	[FriendlyName("Reset From Outside", "The visible went outside the area and was reset to the Reset Position")]
	public bool ResetFromOutsideArea => m_PositionResetThisFrame;

	public void In(GameObject ownerNode, object objectToEnclose, [FriendlyName("Reset Pos Name", "The name of the position within the encounter to reset the visible to if it leaves the area")] string resetPosName, [FriendlyName("Last Position", "The position of the visible before a potential reset")] out Vector3 positionBeforeReset)
	{
		m_PositionResetThisFrame = false;
		positionBeforeReset = Vector3.zero;
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if (!m_Encounter)
		{
			return;
		}
		Visible visible = null;
		if (objectToEnclose != null)
		{
			visible = Singleton.Manager<ManVisible>.inst.GetVisibleFromObject(objectToEnclose);
		}
		if (!(visible != null))
		{
			return;
		}
		Vector3 position = m_Encounter.Position;
		positionBeforeReset = visible.centrePosition;
		float encounterRadius = m_Encounter.EncounterRadius;
		float num = encounterRadius * encounterRadius;
		if ((positionBeforeReset - position).sqrMagnitude > num)
		{
			if (Singleton.Manager<ManPointer>.inst.DraggingItem != null && Singleton.Manager<ManPointer>.inst.DraggingItem == visible)
			{
				Singleton.Manager<ManPointer>.inst.ReleaseDraggingItem();
			}
			if (visible.block != null && visible.block.tank != null)
			{
				Singleton.Manager<ManLooseBlocks>.inst.HostDetachBlock(visible.block, allowHeadlessTech: false, propagate: true);
			}
			else if (visible.holderStack != null)
			{
				visible.SetHolder(null);
			}
			Vector3 position2 = m_Encounter.GetPosition(resetPosName);
			visible.rbody.velocity = Vector3.zero;
			visible.rbody.MovePosition(position2);
			m_PositionResetThisFrame = true;
		}
	}
}
