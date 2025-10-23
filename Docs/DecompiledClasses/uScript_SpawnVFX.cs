using UnityEngine;

public class uScript_SpawnVFX : uScriptLogic
{
	private Encounter m_Encounter;

	public bool Out => true;

	public void In(GameObject ownerNode, Transform vfxToSpawn, string spawnPosName)
	{
		if ((bool)ownerNode && !m_Encounter)
		{
			m_Encounter = ownerNode.GetComponent<Encounter>();
		}
		if ((bool)m_Encounter)
		{
			Vector3 position = m_Encounter.GetPosition(spawnPosName);
			Quaternion rotation = m_Encounter.GetRotation(spawnPosName);
			vfxToSpawn.Spawn(position, rotation);
		}
	}
}
