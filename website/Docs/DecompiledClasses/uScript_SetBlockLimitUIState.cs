public class uScript_SetBlockLimitUIState : uScriptLogic
{
	public bool Out => true;

	public void Show(UIBlockLimit.ShowReason showReason)
	{
		UIBlockLimit.ShowUI(showReason);
	}

	public void Hide(UIBlockLimit.ShowReason showReason)
	{
		UIBlockLimit.HideUI(showReason);
	}
}
