[FriendlyName("uScript_GetNumResourcesSold", "Get number of resources we have sold at this point")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_GetNumResourcesSold : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Resources", "Get the total combined number of resources sold")]
	public int AllResources(ChunkTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumResourcesSold();
	}

	[FriendlyName("Resources of Type", "Get the number of resources sold of the specified type")]
	public int ResourcesOfType([SocketState(false, false)][FriendlyName("Resource Type", "Type of the resource that was sold.")] ChunkTypes resourceType)
	{
		return Singleton.Manager<ManStats>.inst.GetNumResourcesSold(resourceType);
	}
}
