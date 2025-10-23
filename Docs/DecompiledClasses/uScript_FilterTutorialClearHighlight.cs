public class uScript_FilterTutorialClearHighlight : uScriptLogic
{
	public bool Out => true;

	public void In()
	{
		Singleton.Manager<ManUI>.inst.HideTutorialHighlight();
		Singleton.Manager<ManTechBuildingTutorial>.inst.HideHelpArrow();
	}
}
