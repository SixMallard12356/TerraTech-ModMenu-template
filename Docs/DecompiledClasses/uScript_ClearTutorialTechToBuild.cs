[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_ClearTutorialTechToBuild : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManTechBuildingTutorial>.inst.ClearTutorialTechToBuild();
	}
}
