[NodePath("TerraTech/Progression/Stats")]
[FriendlyName("uScript_GetNumSceneryObjectsDestroyed", "Get number of resources we have sold at this point")]
public class uScript_GetNumSceneryObjectsDestroyed : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("All Scenery Objects", "Get the total combined number of scenery objects destroyed")]
	public int AllSceneryObjects(SceneryTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumSceneryObjectsDestroyed();
	}

	[FriendlyName("Resources of Type", "Get the number of scenery objects destroyed of the specified type")]
	public int SceneryObjectsOfType([FriendlyName("Scenery Type", "Type of scenery object (resource giver) that was destroyed.")][SocketState(false, false)] SceneryTypes sceneryType)
	{
		return Singleton.Manager<ManStats>.inst.GetNumSceneryObjectsDestroyed(sceneryType);
	}
}
