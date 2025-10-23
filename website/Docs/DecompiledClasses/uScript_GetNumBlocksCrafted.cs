[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_GetNumBlocksCrafted", "Get number of blocks we have crafted at this point")]
public class uScript_GetNumBlocksCrafted : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Blocks", "Get the total of all blocks crafted of all types combined")]
	public int AllBlocks(BlockTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumBlocksCrafted();
	}

	[FriendlyName("Blocks of Type", "Get the total of blocks crafted of a specific type")]
	public int BlocksOfType([FriendlyName("Block Type", "Type of the block that was crafted.")][SocketState(false, false)] BlockTypes blockType)
	{
		return Singleton.Manager<ManStats>.inst.GetNumBlocksCrafted(blockType);
	}
}
