using UnityEngine;

public class uScript_PlayAnim : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public Animator In(GameObject ownerNode, GameObject animPrefab, string spawnPosName)
	{
		Animator result = null;
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			Vector3 position = m_Encounter.GetPosition(spawnPosName);
			Quaternion rotation = m_Encounter.GetRotation(spawnPosName);
			result = Object.Instantiate(animPrefab, position, rotation).GetComponent<Animator>();
		}
		return result;
	}
}
