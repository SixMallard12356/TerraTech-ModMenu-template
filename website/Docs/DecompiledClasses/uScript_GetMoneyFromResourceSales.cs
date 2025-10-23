[FriendlyName("uScript_GetMoneyFromResourceSales", "Get the amount of money we have earned off' resource sales up till now")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_GetMoneyFromResourceSales : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Resources", "Get the total money from all resource sales")]
	public int AllResources(ChunkTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalMoneyFromResourceSales();
	}

	[FriendlyName("Resources of Type", "Get the money from resource sales for a specific resource")]
	public int ResourcesOfType([SocketState(false, false)][FriendlyName("Resource Type", "Type of the resource that we've earned it from.")] ChunkTypes resourceType)
	{
		return Singleton.Manager<ManStats>.inst.GetMoneyFromResourceSales(resourceType);
	}
}
