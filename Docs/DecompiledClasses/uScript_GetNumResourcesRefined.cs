[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_GetNumResourcesRefined", "Get number of resources we have refined at this point")]
public class uScript_GetNumResourcesRefined : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Resources", "Get the total combined number of resources refined")]
	public int AllResources(ChunkTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumResourcesRefined();
	}

	[FriendlyName("Resources of Type", "Get the number of resources refined for the specified type")]
	public int ResourcesOfType([SocketState(false, false)][FriendlyName("Resource Type", "Type of the resource that was refined.")] ChunkTypes resourceType)
	{
		return Singleton.Manager<ManStats>.inst.GetNumResourcesRefined(resourceType);
	}
}
