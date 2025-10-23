public class uScript_FilterTutorialHighlightCategory : uScriptLogic
{
	public bool Out => true;

	public void In(ChunkCategory chunkCategory)
	{
		UIFilterMenu uIFilterMenu = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.FilterMenu) as UIFilterMenu;
		if (uIFilterMenu != null)
		{
			QueryableSelectable chunkCategoryButton = uIFilterMenu.GetChunkCategoryButton(chunkCategory);
			if (chunkCategoryButton != null)
			{
				bool showTutorialHighlight = !Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
				uScript_FilterTutorialHighlightMode.ShowHelpArrow(chunkCategoryButton.transform, showTutorialHighlight);
			}
		}
	}
}
