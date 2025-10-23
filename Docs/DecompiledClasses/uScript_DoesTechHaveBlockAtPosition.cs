#define UNITY_EDITOR
using UnityEngine;

public class uScript_DoesTechHaveBlockAtPosition : uScriptLogic
{
	private bool m_Attached;

	public bool True => m_Attached;

	public bool False => !m_Attached;

	public void In(Tank tech, BlockTypes blockType, Vector3 localPosition)
	{
		m_Attached = false;
		if (tech != null)
		{
			TankBlock blockAtPosition = tech.blockman.GetBlockAtPosition(localPosition);
			if (blockAtPosition != null)
			{
				m_Attached = blockAtPosition.visible.ItemType == (int)blockType;
			}
		}
		else
		{
			d.LogError("ERROR: uScript_IsBlockAttachedToTechAtPosition - tech is null");
		}
	}
}
