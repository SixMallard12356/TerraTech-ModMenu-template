public class UIButtonGoToScreenOrCheckPremium : UIButtonGoToScreen
{
	public override void OnButtonClicked()
	{
		base.OnButtonClicked();
	}

	private void OnPermissionsCallback(bool success)
	{
		if (success)
		{
			base.OnButtonClicked();
		}
	}
}
