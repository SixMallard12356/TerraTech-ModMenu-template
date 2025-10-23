public class uScript_BlockPaletteOptions : uScriptLogic
{
	public bool Out => true;

	public void In(bool show, bool open)
	{
		Singleton.Manager<ManSFX>.inst.SuppressUISFX();
		Singleton.Manager<ManPurchases>.inst.ShowPalette(show);
		if (!Singleton.Manager<ManInput>.inst.IsGamepadUseEnabled())
		{
			Singleton.Manager<ManPurchases>.inst.ExpandPalette(open, UIShopBlockSelect.ExpandReason.Beam);
		}
	}
}
