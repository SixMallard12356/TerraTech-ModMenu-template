[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_RemoveAllGhostBlocksOnTech : uScriptLogic
{
	public bool Out => true;

	public void In(Tank targetTech)
	{
		Singleton.Manager<ManTechBuildingTutorial>.inst.RemoveGhostBlocksOnTech(targetTech);
	}
}
