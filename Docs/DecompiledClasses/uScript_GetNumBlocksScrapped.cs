[FriendlyName("uScript_GetNumBlocksScrapped", "Get number of blocks we have scrapped at this point")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_GetNumBlocksScrapped : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Blocks", "Get the total of all blocks scrapped of all types combined")]
	public int AllBlocks(BlockTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumBlocksScrapped();
	}

	[FriendlyName("Blocks of Type", "Get the total of blocks scrapped of a specific type")]
	public int BlocksOfType([SocketState(false, false)][FriendlyName("Block Type", "Type of the block that was scrapped.")] BlockTypes blockType)
	{
		return Singleton.Manager<ManStats>.inst.GetNumBlocksScrapped(blockType);
	}
}
