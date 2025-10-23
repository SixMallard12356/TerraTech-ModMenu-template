using UnityEngine;

public class uScript_GetCurrentEncounterPosition : uScriptLogic
{
	private Encounter m_DataComponent;

	public bool Out => true;

	public Vector3 In(GameObject ownerNode)
	{
		Vector3 result = Vector3.zero;
		if ((bool)ownerNode && !m_DataComponent)
		{
			m_DataComponent = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_DataComponent)
		{
			result = m_DataComponent.Position;
		}
		return result;
	}
}
