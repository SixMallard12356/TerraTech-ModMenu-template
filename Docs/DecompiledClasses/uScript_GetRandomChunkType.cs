#define UNITY_EDITOR
using UnityEngine;

[FriendlyName("uScript_GetRandomChunkType", "Returns a random chunk type from a list of chunk types")]
[NodePath("TerraTech/Actions/Crafting")]
public class uScript_GetRandomChunkType : uScriptLogic
{
	public bool Out => true;

	public ChunkTypes In(ChunkTypes[] chunkList)
	{
		if (chunkList != null && chunkList.Length != 0)
		{
			return chunkList[Random.Range(0, chunkList.Length)];
		}
		d.LogError("uScript_GetRandomChunkType: chunk list is null or empty");
		return ChunkTypes.Wood;
	}
}
