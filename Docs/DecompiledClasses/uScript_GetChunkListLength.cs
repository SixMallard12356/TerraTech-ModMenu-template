#define UNITY_EDITOR
[NodePath("TerraTech/Actions/Crafting")]
[FriendlyName("uScript_GetChunkListLength", "Returns the length of a chunk list")]
public class uScript_GetChunkListLength : uScriptLogic
{
	public bool Out => true;

	public int In(ChunkTypes[] chunkList)
	{
		int result = 0;
		if (chunkList != null)
		{
			result = chunkList.Length;
		}
		else
		{
			d.LogError("uScript_GetChunkListLength: chunk list is null");
		}
		return result;
	}
}
