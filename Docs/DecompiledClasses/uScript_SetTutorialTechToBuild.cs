[NodePath("TerraTech/Actions/Tutorial")]
public class uScript_SetTutorialTechToBuild : uScriptLogic
{
	public bool Out => true;

	public void In(TankPreset completedTechPreset, Tank tutorialBuildTech)
	{
		if (completedTechPreset != null && tutorialBuildTech != null)
		{
			Singleton.Manager<ManTechBuildingTutorial>.inst.SetTutorialTechToBuild(completedTechPreset.GetTechDataFormatted(), tutorialBuildTech);
		}
	}
}
