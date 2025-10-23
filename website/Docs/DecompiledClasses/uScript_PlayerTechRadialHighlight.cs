public class uScript_PlayerTechRadialHighlight : uScriptLogic
{
	public bool Out => true;

	public void In(UIRadialTechAndBlockActionsMenu.PlayerCommands playerCommand)
	{
		UIRadialTechAndBlockActionsMenu uIRadialTechAndBlockActionsMenu = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.TechAndBlockActions) as UIRadialTechAndBlockActionsMenu;
		if (uIRadialTechAndBlockActionsMenu != null)
		{
			UIRadialMenuOption radialMenuButton = uIRadialTechAndBlockActionsMenu.GetRadialMenuButton(playerCommand);
			if (radialMenuButton != null)
			{
				bool showTutorialHighlight = !Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled();
				uScript_FilterTutorialHighlightMode.ShowHelpArrow(radialMenuButton.transform, showTutorialHighlight);
			}
		}
	}
}
