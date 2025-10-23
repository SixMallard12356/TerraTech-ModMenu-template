[FriendlyName("Hide HUD")]
public class uScript_HideHUD : uScriptLogic
{
	public bool Out => true;

	public void In(bool hide)
	{
		if (Singleton.Manager<ManHUD>.inst.CurrentHUD != null)
		{
			if (hide)
			{
				Singleton.Manager<ManHUD>.inst.HideHudElement(ManHUD.HUDElementType.Snapshot);
			}
			else
			{
				Singleton.Manager<ManHUD>.inst.ShowHudElement(ManHUD.HUDElementType.Snapshot);
			}
		}
	}
}
