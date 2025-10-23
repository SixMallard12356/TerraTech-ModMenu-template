[FriendlyName("uScript_GetNumEnemyTechsDestroyed", "Get number of enemy techs we have destroyed to this point")]
[NodePath("TerraTech/Progression/Stats")]
public class uScript_GetNumEnemyTechsDestroyed : uScriptLogic
{
	public bool Out => true;

	[FriendlyName("Total Enemies Destroyed", "Get the total number of enemies destroyed")]
	public int TotalEnemiesDestroyed(FactionSubTypes unused)
	{
		return Singleton.Manager<ManStats>.inst.GetTotalNumEnemyTechsDestroyed();
	}

	[FriendlyName("Enemies Destroyed of Faction", "Get the number of enemy tech destroyed that belong to the specified faction")]
	public int EnemiesDestroyedOfFaction([SocketState(false, false)][FriendlyName("Faction", "Faction to query number destroyed techs for")] FactionSubTypes faction)
	{
		return Singleton.Manager<ManStats>.inst.GetNumEnemyTechsDestroyedByFaction(faction);
	}
}
