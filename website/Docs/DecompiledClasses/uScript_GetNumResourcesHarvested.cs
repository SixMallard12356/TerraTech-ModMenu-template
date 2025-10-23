[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_GetNumResourcesHarvested", "Get number of resources we have harvested at this point")]
public class uScript_GetNumResourcesHarvested : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Resources", "Get the total combined number of resources harvested")]
	public int AllResources(ChunkTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumResourcesHarvested();
	}

	[FriendlyName("Resources of Type", "Get the number of resources harvested for the specified type")]
	public int ResourcesOfType([SocketState(false, false)][FriendlyName("Resource Type", "Type of the resource that was harvested.")] ChunkTypes resourceType)
	{
		return Singleton.Manager<ManStats>.inst.GetNumResourcesHarvested(resourceType);
	}
}
