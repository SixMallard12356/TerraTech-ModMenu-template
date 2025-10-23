[FriendlyName("uScript_GetMoneyFromBlockSales", "Get the amount of money we have earned off' block sales up till now")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_GetMoneyFromBlockSales : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Blocks", "Get the total money from all block sales")]
	public int AllBlocks(BlockTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalMoneyFromBlockSales();
	}

	[FriendlyName("Blocks of Type", "Get the money from resource sales for a specific block")]
	public int BlocksOfType([FriendlyName("Block Type", "Type of the block that we've earned it from.")][SocketState(false, false)] BlockTypes blockType)
	{
		return Singleton.Manager<ManStats>.inst.GetMoneyFromBlockSales(blockType);
	}
}
