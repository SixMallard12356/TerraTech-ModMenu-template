using UnityEngine;

public class uScript_RemoveSceneryAtPosition : uScriptLogic
{
	public bool Out => true;

	public void In([FriendlyName("position", "Name of position to spawn at")] Vector3 position, [FriendlyName("radius", "area around spawn pos to clear scenery")] float radius, [FriendlyName("Prevent Chunks Spawning", "Stop destroyed scenery spawning chunks")] bool preventChunksSpawning)
	{
		ManSpawn.SceneryRemovalFlags sceneryRemovalFlags = (ManSpawn.SceneryRemovalFlags)0;
		if (preventChunksSpawning)
		{
			sceneryRemovalFlags |= ManSpawn.SceneryRemovalFlags.SpawnNoChunks;
		}
		ManSpawn.RemoveAllSceneryAroundPosition(position, radius, sceneryRemovalFlags);
	}
}
