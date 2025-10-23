#define UNITY_EDITOR
using UnityEngine;

public class uScript_SpawnEncounterObject : uScriptLogic
{
	public bool Out => true;

	public void In(GameObject ownerNode, Transform encounterObjectToSpawn, string nameWithinEncounter, string positionName)
	{
		if (!ownerNode)
		{
			return;
		}
		Encounter component = ownerNode.GetComponent<Encounter>();
		if ((bool)component)
		{
			if ((bool)encounterObjectToSpawn)
			{
				if (!component.GetStoredObject(encounterObjectToSpawn.name, nameWithinEncounter))
				{
					Vector3 position = component.GetPosition(positionName);
					Quaternion rotation = component.GetRotation(positionName);
					Vector3 vector = Singleton.Manager<ManWorld>.inst.ProjectToGround(position);
					Vector3 terrainNormal = Singleton.Manager<ManWorld>.inst.GetTerrainNormal(vector);
					Transform transform = encounterObjectToSpawn.Spawn(vector, rotation);
					transform.name = encounterObjectToSpawn.name;
					component.AddStoredObject(transform, nameWithinEncounter);
					transform.LookAt(vector + terrainNormal);
				}
				else
				{
					d.LogError("ERROR: uScript_SpawnEncounterObject - Object " + encounterObjectToSpawn.name + " has already spawned");
				}
			}
			else
			{
				d.LogError("ERROR: uScript_SpawnEncounterObject - no object to spawn");
			}
		}
		else
		{
			string text = ((ownerNode != null) ? ("No Encounter Component on " + ownerNode.name) : "Owner Node Null");
			d.LogError("Error: uScript_SpawnEncounterObject - " + text);
		}
	}
}
