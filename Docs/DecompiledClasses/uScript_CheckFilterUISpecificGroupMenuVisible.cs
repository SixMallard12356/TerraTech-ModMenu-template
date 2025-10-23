public class uScript_CheckFilterUISpecificGroupMenuVisible : uScriptLogic
{
	private bool m_Visible;

	public bool Visible => m_Visible;

	public bool NotVisible => !m_Visible;

	public void In()
	{
		m_Visible = false;
		UIFilterMenu uIFilterMenu = Singleton.Manager<ManHUD>.inst.GetHudElement(ManHUD.HUDElementType.FilterMenu) as UIFilterMenu;
		if (uIFilterMenu != null)
		{
			m_Visible = uIFilterMenu.SpecificGroupMenuActive;
		}
	}

	public void OnEnable()
	{
		m_Visible = false;
	}
}
