#define UNITY_EDITOR
using UnityEngine;

public class uScript_RemoveScenery : uScriptLogic
{
	public bool Out => true;

	public void In([FriendlyName("Owner Node", "Owner Node of Encounter")] GameObject ownerNode, [FriendlyName("positionName", "Name of position in encounter to spawn at")] string positionName, [FriendlyName("radius", "area around spawn pos to clear scenery")] float radius, [FriendlyName("Prevent Chunks Spawning", "Stop destroyed scenery spawning chunks")] bool preventChunksSpawning)
	{
		if ((bool)ownerNode)
		{
			Encounter component = ownerNode.GetComponent<Encounter>();
			if ((bool)component)
			{
				Vector3 position = component.GetPosition(positionName);
				ManSpawn.SceneryRemovalFlags sceneryRemovalFlags = (ManSpawn.SceneryRemovalFlags)0;
				if (preventChunksSpawning)
				{
					sceneryRemovalFlags |= ManSpawn.SceneryRemovalFlags.SpawnNoChunks;
				}
				ManSpawn.RemoveAllSceneryAroundPosition(position, radius, sceneryRemovalFlags);
			}
			else
			{
				d.LogError("ERROR: uScript_RemoveScenery - No Encounter Component on " + ownerNode.name);
			}
		}
		else
		{
			d.LogError("ERROR: uScript_RemoveScenery - Owner Node Null");
		}
	}
}
